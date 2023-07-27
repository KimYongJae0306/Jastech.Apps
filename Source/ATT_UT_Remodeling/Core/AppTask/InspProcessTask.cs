using Emgu.CV;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Algorithms.Akkon;
using Jastech.Framework.Algorithms.Akkon.Parameters;
using Jastech.Framework.Algorithms.Akkon.Results;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Util;
using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATT_UT_Remodeling.Core.AppTask
{
    public class InspProcessTask
    {
        #region 필드
        private object _inspLock = new object();
        #endregion

        #region 속성
        public int InspCount { get; set; } = 0;

        public Task ProcessTask { get; set; }

        public CancellationTokenSource ProcessTaskCancellationTokenSource { get; set; }

        public AkkonAlgorithm AkkonAlgorithm { get; set; } = new AkkonAlgorithm();

        public List<ATTInspTab> InspTabList { get; set; } = new List<ATTInspTab>();

        private Queue<ATTInspTab> InspTabQueue = new Queue<ATTInspTab>();

        public Queue<VirtualData> VirtualQueue = new Queue<VirtualData>();

        #endregion

        #region 메서드
        private void Run(ATTInspTab inspTab)
        {
            Stopwatch sw = new Stopwatch();
            sw.Restart();

            string unitName = UnitName.Unit0.ToString();
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            Tab tab = inspModel.GetUnit(unitName).GetTab(inspTab.TabScanBuffer.TabNo);

            MainAlgorithmTool algorithmTool = new MainAlgorithmTool();

            TabInspResult inspResult = new TabInspResult();
            inspResult.TabNo = inspTab.TabScanBuffer.TabNo;
            inspResult.Image = inspTab.MergeMatImage;
            inspResult.CogImage = inspTab.MergeCogImage;

            // Create Coordinate Object
            CoordinateTransform fpcCoordinate = new CoordinateTransform();
            CoordinateTransform panelCoordinate = new CoordinateTransform();

            algorithmTool.MainMarkInspect(inspTab.MergeCogImage, tab, ref inspResult, false);

            if (inspResult.MarkResult.Judgement != Judgement.OK)
            {
                // 검사 실패
                string message = string.Format("Mark Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, inspResult.MarkResult.FpcMark.Judgement, inspResult.MarkResult.PanelMark.Judgement);
                WriteLog(message);
                Logger.Debug(LogType.Inspection, message);

            }
            else
            {
                // Set Coordinate Params
                SetFpcCoordinateData(fpcCoordinate, inspResult);
                SetPanelCoordinateData(panelCoordinate, inspResult);

                // Excuete Coordinate
                fpcCoordinate.ExecuteCoordinate();
                panelCoordinate.ExecuteCoordinate();

                var lineCamera = LineCameraManager.Instance().GetLineCamera("LineCamera").Camera;

                float resolution_um = lineCamera.PixelResolution_um / lineCamera.LensScale;
                double judgementX = tab.AlignSpec.LeftSpecX_um / resolution_um;
                double judgementY = tab.AlignSpec.LeftSpecY_um / resolution_um;

                #region Align
                if (AppsConfig.Instance().EnableAlign)
                {
                    inspResult.AlignResult.LeftX = algorithmTool.RunMainLeftAlignX(inspTab.MergeCogImage, tab, fpcCoordinate, panelCoordinate, judgementX);
                    if (inspResult.AlignResult.LeftX?.Judgement != Judgement.OK)
                    {
                        var leftAlignX = inspResult.AlignResult.LeftX;
                        string message = string.Format("Left AlignX Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, leftAlignX.Fpc.Judgement, leftAlignX.Panel.Judgement);
                        Logger.Debug(LogType.Inspection, message);
                    }

                    inspResult.AlignResult.LeftY = algorithmTool.RunMainLeftAlignY(inspTab.MergeCogImage, tab, fpcCoordinate, panelCoordinate, judgementY);
                    if (inspResult.AlignResult.LeftY?.Judgement != Judgement.OK)
                    {
                        var leftAlignY = inspResult.AlignResult.LeftY;
                        string message = string.Format("Left AlignY Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, leftAlignY.Fpc.Judgement, leftAlignY.Panel.Judgement);
                        Logger.Debug(LogType.Inspection, message);
                    }

                    inspResult.AlignResult.RightX = algorithmTool.RunMainRightAlignX(inspTab.MergeCogImage, tab, fpcCoordinate, panelCoordinate, judgementX);
                    if (inspResult.AlignResult.RightX?.Judgement != Judgement.OK)
                    {
                        var rightAlignX = inspResult.AlignResult.RightX;
                        string message = string.Format("Right AlignX Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, rightAlignX.Fpc.Judgement, rightAlignX.Panel.Judgement);
                        Logger.Debug(LogType.Inspection, message);
                    }

                    inspResult.AlignResult.RightY = algorithmTool.RunMainRightAlignY(inspTab.MergeCogImage, tab, fpcCoordinate, panelCoordinate, judgementY);
                    if (inspResult.AlignResult.RightY?.Judgement != Judgement.OK)
                    {
                        var rightAlignY = inspResult.AlignResult.RightY;
                        string message = string.Format("Right AlignY Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, rightAlignY.Fpc.Judgement, rightAlignY.Panel.Judgement);
                        Logger.Debug(LogType.Inspection, message);
                    }
                }
                else
                {
                    inspResult.AlignResult.LeftX = new AlignResult();
                    inspResult.AlignResult.LeftY = new AlignResult();
                    inspResult.AlignResult.RightX = new AlignResult();
                    inspResult.AlignResult.RightY = new AlignResult();
                }
                #endregion

                #region Center Align
                // EnableAlign false 일때 구조 생각
                inspResult.AlignResult.CenterX = Math.Abs(inspResult.AlignResult.LeftX.ResultValue_pixel - inspResult.AlignResult.RightX.ResultValue_pixel);
                #endregion

                if (AppsConfig.Instance().EnableAkkon)
                {
                    var roiList = tab.AkkonParam.GetAkkonROIList();
                    var coordinateList = RenewalAkkonRoi(roiList, panelCoordinate);
                    var leadResultList = AkkonAlgorithm.Run(inspTab.MergeMatImage, coordinateList, tab.AkkonParam.AkkonAlgoritmParam, resolution_um);

                    inspResult.AkkonResult = CreateAkkonResult(unitName, tab.Index, leadResultList);
                    inspResult.AkkonInspMatImage = AkkonAlgorithm.ResizeMat;
                }
                else
                    inspResult.AkkonResult = new AkkonResult();

                sw.Stop();
                string resultMessage = string.Format("Tab {0} Inspection Completed.({1}ms)", (inspTab.TabScanBuffer.TabNo + 1), sw.ElapsedMilliseconds);
                Console.WriteLine(resultMessage);
                WriteLog(resultMessage, true);
            }

            inspResult.IsInspDone = true;

            AppsInspResult.Instance().Add(inspResult);
            SystemManager.Instance().UpdateResultTabButton(inspResult.TabNo);
            InspCount++;
        }

        public void Initalize(string cameraName, List<TabScanBuffer> bufferList)
        {
            InspCount = 0;
            DisposeInspTabList();
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            foreach (var buffer in bufferList)
            {
                ATTInspTab inspTab = new ATTInspTab();
                inspTab.CameraName = cameraName;
                inspTab.TabScanBuffer = buffer;
                inspTab.InspectEvent += AddInspectEventFuction;
                inspTab.StartInspTask();
                InspTabList.Add(inspTab);
            }
        }

        public void StartTask()
        {
            if (ProcessTask != null)
                return;

            ProcessTaskCancellationTokenSource = new CancellationTokenSource();
            ProcessTask = new Task(Inspection, ProcessTaskCancellationTokenSource.Token);
            ProcessTask.Start();
        }

        public void StopTask()
        {
            if (ProcessTask == null)
                return;

            while (InspTabQueue.Count > 0)
            {
                var data = InspTabQueue.Dequeue();
                data.Dispose();
            }

            ProcessTaskCancellationTokenSource.Cancel();
            ProcessTask.Wait();
            ProcessTask = null;
        }

        private void Inspection()
        {
            while (true)
            {
                if (ProcessTaskCancellationTokenSource.IsCancellationRequested)
                    break;

                if (GetInspTab() is ATTInspTab inspTab)
                    Run(inspTab);

                Thread.Sleep(50);
            }
        }

        private ATTInspTab GetInspTab()
        {
            lock (_inspLock)
            {
                if (InspTabQueue.Count() > 0)
                    return InspTabQueue.Dequeue();
                else
                    return null;
            }
        }

        public void DisposeInspTabList()
        {
            foreach (var inspTab in InspTabList)
            {
                inspTab.StopInspTask();
                inspTab.InspectEvent -= AddInspectEventFuction;
                inspTab.Dispose();
            }
            InspTabList.Clear();
        }

        private void AddInspectEventFuction(ATTInspTab inspTab)
        {
            lock (_inspLock)
                InspTabQueue.Enqueue(inspTab);
        }

        private AkkonResult CreateAkkonResult(string unitName, int tabNo, List<AkkonLeadResult> leadResultList)
        {
            AkkonResult akkonResult = new AkkonResult();
            akkonResult.UnitName = unitName;
            akkonResult.TabNo = tabNo;
            akkonResult.LeadResultList = leadResultList;

            List<int> leftCountList = new List<int>();
            List<int> rightCountList = new List<int>();

            List<double> leftLengthList = new List<double>();
            List<double> rightLengthList = new List<double>();

            bool leftCountNG = false;
            bool leftLengthNG = false;
            bool rightCountNG = false;
            bool rightLengthNG = false;

            foreach (var leadResult in leadResultList)
            {
                if (leadResult.ContainPos == LeadContainPos.Left)
                {
                    leftCountNG |= leadResult.Judgement == Judgement.NG ? true : false;
                    leftCountList.Add(leadResult.AkkonCount);

                    leftLengthNG |= leadResult.Judgement == Judgement.NG ? true : false;
                    leftLengthList.Add(leadResult.LengthY_um);
                }
                else
                {
                    rightCountNG |= leadResult.Judgement == Judgement.NG ? true : false;
                    rightCountList.Add(leadResult.AkkonCount);

                    rightLengthNG |= leadResult.Judgement == Judgement.NG ? true : false;
                    rightLengthList.Add(leadResult.LengthY_um);
                }
            }

            akkonResult.CountJudgement = (leftCountNG || rightCountNG) == true ? Judgement.NG : Judgement.OK;
            if (leftCountList.Count > 0)
            {
                akkonResult.LeftCount_Avg = (int)leftCountList.Average();
                akkonResult.LeftCount_Min = (int)leftCountList.Min();
                akkonResult.LeftCount_Max = (int)leftCountList.Max();
            }

            if (rightCountList.Count > 0)
            {
                akkonResult.RightCount_Avg = (int)rightCountList.Average();
                akkonResult.RightCount_Min = (int)rightCountList.Min();
                akkonResult.RightCount_Max = (int)rightCountList.Max();
            }

            akkonResult.LengthJudgement = (leftLengthNG || rightLengthNG) == true ? Judgement.NG : Judgement.OK;

            if (leftLengthList.Count > 0)
            {
                akkonResult.Length_Left_Avg_um = (float)leftLengthList.Average();
                akkonResult.Length_Left_Min_um = (float)leftLengthList.Min();
                akkonResult.Length_Left_Max_um = (float)leftLengthList.Max();
            }

            if (rightLengthList.Count > 0)
            {
                akkonResult.Length_Right_Avg_um = (float)rightLengthList.Average();
                akkonResult.Length_Right_Min_um = (float)rightLengthList.Min();
                akkonResult.Length_Right_Max_um = (float)rightLengthList.Max();
            }

            akkonResult.LeadResultList = leadResultList;

            return akkonResult;
        }

        private List<AkkonROI> RenewalAkkonRoi(List<AkkonROI> roiList, CoordinateTransform panelCoordinate)
        {
            List<AkkonROI> newList = new List<AkkonROI>();

            foreach (var item in roiList)
            {
                PointF leftTop = item.GetLeftTopPoint();
                PointF rightTop = item.GetRightTopPoint();
                PointF leftBottom = item.GetLeftBottomPoint();
                PointF rightBottom = item.GetRightBottomPoint();

                var newLeftTop = panelCoordinate.GetCoordinate(leftTop);
                var newRightTop = panelCoordinate.GetCoordinate(rightTop);
                var newLeftBottom = panelCoordinate.GetCoordinate(leftBottom);
                var newRightBottom = panelCoordinate.GetCoordinate(rightBottom);

                AkkonROI akkonRoi = new AkkonROI();

                akkonRoi.SetLeftTopPoint(newLeftTop);
                akkonRoi.SetRightTopPoint(newRightTop);
                akkonRoi.SetLeftBottomPoint(newLeftBottom);
                akkonRoi.SetRightBottomPoint(newRightBottom);

                newList.Add(akkonRoi);
            }

            return newList;
        }

        private void SetFpcCoordinateData(CoordinateTransform fpc, TabInspResult tabInspResult)
        {
            PointF teachedLeftPoint = tabInspResult.MarkResult.FpcMark.FoundedMark.Left.MaxMatchPos.ReferencePos;
            PointF teachedRightPoint = tabInspResult.MarkResult.FpcMark.FoundedMark.Right.MaxMatchPos.ReferencePos;
            PointF searchedLeftPoint = tabInspResult.MarkResult.FpcMark.FoundedMark.Left.MaxMatchPos.FoundPos;
            PointF searchedRightPoint = tabInspResult.MarkResult.FpcMark.FoundedMark.Right.MaxMatchPos.FoundPos;
            fpc.SetReferenceData(teachedLeftPoint, teachedRightPoint);
            fpc.SetTargetData(searchedLeftPoint, searchedRightPoint);
        }

        private void SetPanelCoordinateData(CoordinateTransform panel, TabInspResult tabInspResult)
        {
            PointF teachedLeftPoint = tabInspResult.MarkResult.PanelMark.FoundedMark.Left.MaxMatchPos.ReferencePos;
            PointF teachedRightPoint = tabInspResult.MarkResult.PanelMark.FoundedMark.Right.MaxMatchPos.ReferencePos;
            PointF searchedLeftPoint = tabInspResult.MarkResult.PanelMark.FoundedMark.Left.MaxMatchPos.FoundPos;
            PointF searchedRightPoint = tabInspResult.MarkResult.PanelMark.FoundedMark.Right.MaxMatchPos.FoundPos;
            panel.SetReferenceData(teachedLeftPoint, teachedRightPoint);
            panel.SetTargetData(searchedLeftPoint, searchedRightPoint);
        }

        private void WriteLog(string logMessage, bool isSystemLog = false)
        {
            if (isSystemLog)
                SystemManager.Instance().AddSystemLogMessage(logMessage);

            Logger.Write(LogType.Seq, logMessage);
          
        }

        public void StartVirtual()
        {
            lock(VirtualQueue)
            {
                while(VirtualQueue.Count > 0)
                {
                    var data = VirtualQueue.Dequeue();

                    var inspTab = InspTabList.Where(x => x.TabScanBuffer.TabNo == data.TabNo).FirstOrDefault();
                    inspTab?.SetVirtualImage(data.FilePath);
                }
            }
        }
        #endregion
    }

    public class VirtualData
    {
        public int TabNo { get; set; }

        public string FilePath { get; set; }
    }
}
