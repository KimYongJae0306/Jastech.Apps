using ATT_UT_Remodeling.Core.Data;
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
using Jastech.Framework.Config;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Util;
using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

            var lineCamera = LineCameraManager.Instance().GetLineCamera("LineCamera").Camera;
            float resolution_um = lineCamera.PixelResolution_um / lineCamera.LensScale;

            string unitName = UnitName.Unit0.ToString();
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            Tab tab = inspModel.GetUnit(unitName).GetTab(inspTab.TabScanBuffer.TabNo);

            MainAlgorithmTool algorithmTool = new MainAlgorithmTool();

            TabInspResult tabInspResult = new TabInspResult();
            tabInspResult.TabNo = inspTab.TabScanBuffer.TabNo;
            tabInspResult.Image = inspTab.MergeMatImage;
            tabInspResult.CogImage = inspTab.MergeCogImage;
            tabInspResult.Resolution_um = resolution_um;

            tabInspResult.AlignResult = new TabAlignResult();
            tabInspResult.AlignResult.PreHead = PlcControlManager.Instance().GetPreHeadData(tabInspResult.TabNo);

            algorithmTool.MainMarkInspect(inspTab.MergeCogImage, tab, ref tabInspResult, false);

            string message = string.Empty;
            if (tabInspResult.MarkResult.Judgement != Judgement.OK)
            {
                // 검사 실패
                message = string.Format("Mark Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index + 1, tabInspResult.MarkResult.FpcMark.Judgement, tabInspResult.MarkResult.PanelMark.Judgement);
                WriteLog(message, true);
                Logger.Debug(LogType.Inspection, message);

                tabInspResult.AkkonResult = new AkkonResult();
            }
            else
            {
                message = string.Format("Akkon Mark Insp OK !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index + 1, tabInspResult.MarkResult.FpcMark.Judgement, tabInspResult.MarkResult.PanelMark.Judgement);
                WriteLog(message, true);
                Logger.Debug(LogType.Inspection, message);

                #region Align Coordinate
                var fpcLeftOrigin = tab.Mark.GetFPCMark(MarkDirection.Left, MarkName.Main).InspParam.GetOrigin();
                var fpcRightOrigin = tab.Mark.GetFPCMark(MarkDirection.Right, MarkName.Main).InspParam.GetOrigin();
                var panelLeftOrigin = tab.Mark.GetPanelMark(MarkDirection.Left, MarkName.Main).InspParam.GetOrigin();
                var panelRightOrigin = tab.Mark.GetPanelMark(MarkDirection.Right, MarkName.Main).InspParam.GetOrigin();

                PointF fpcLeftOriginPoint = new PointF(Convert.ToSingle(fpcLeftOrigin.TranslationX), Convert.ToSingle(fpcLeftOrigin.TranslationY));
                PointF fpcRightOriginPoint = new PointF(Convert.ToSingle(fpcRightOrigin.TranslationX), Convert.ToSingle(fpcRightOrigin.TranslationY));
                PointF panelLeftOriginPoint = new PointF(Convert.ToSingle(panelLeftOrigin.TranslationX), Convert.ToSingle(panelLeftOrigin.TranslationY));
                PointF panelRightOriginPoint = new PointF(Convert.ToSingle(panelRightOrigin.TranslationX), Convert.ToSingle(panelRightOrigin.TranslationY));

                PointF fpcLeftOffset = MathHelper.GetOffset(fpcLeftOriginPoint, tabInspResult.MarkResult.FpcMark.FoundedMark.Left.MaxMatchPos.FoundPos);
                PointF fpcRightOffset = MathHelper.GetOffset(fpcRightOriginPoint, tabInspResult.MarkResult.FpcMark.FoundedMark.Right.MaxMatchPos.FoundPos);
                PointF panelLeftOffset = MathHelper.GetOffset(panelLeftOriginPoint, tabInspResult.MarkResult.PanelMark.FoundedMark.Left.MaxMatchPos.FoundPos);
                PointF panelRightOffset = MathHelper.GetOffset(panelRightOriginPoint, tabInspResult.MarkResult.PanelMark.FoundedMark.Right.MaxMatchPos.FoundPos);
                #endregion

                #region Align
                double judgementX = tab.AlignSpec.LeftSpecX_um / resolution_um;
                double judgementY = tab.AlignSpec.LeftSpecY_um / resolution_um;
                double judgementCX = tab.AlignSpec.CenterSpecX_um / resolution_um;

                tabInspResult.AlignResult.Resolution_um = resolution_um;
                tabInspResult.AlignResult.CxJudegementValue_pixel = judgementCX;

                if (AppsConfig.Instance().EnableAlign)
                {
                    if (tab.AlignSpec.UseAutoTracking)
                        tabInspResult.AlignResult.LeftX = algorithmTool.RunMainLeftAutoAlignX(inspTab.MergeCogImage, tab, fpcLeftOffset, panelLeftOffset, judgementX);
                    else
                        tabInspResult.AlignResult.LeftX = algorithmTool.RunMainLeftAlignX(inspTab.MergeCogImage, tab, fpcLeftOffset, panelLeftOffset, judgementX);

                    if (tabInspResult.AlignResult.LeftX?.Judgement != Judgement.OK)
                    {
                        var leftAlignX = tabInspResult.AlignResult.LeftX;
                        message = string.Format("Left AlignX Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, leftAlignX.Fpc.Judgement, leftAlignX.Panel.Judgement);
                        Logger.Debug(LogType.Inspection, message);
                    }

                    tabInspResult.AlignResult.LeftY = algorithmTool.RunMainLeftAlignY(inspTab.MergeCogImage, tab, fpcLeftOffset, panelLeftOffset, judgementY);
                    if (tabInspResult.AlignResult.LeftY?.Judgement != Judgement.OK)
                    {
                        var leftAlignY = tabInspResult.AlignResult.LeftY;
                        message = string.Format("Left AlignY Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, leftAlignY.Fpc.Judgement, leftAlignY.Panel.Judgement);
                        Logger.Debug(LogType.Inspection, message);
                    }

                    if (tab.AlignSpec.UseAutoTracking)
                        tabInspResult.AlignResult.RightX = algorithmTool.RunMainRightAutoAlignX(inspTab.MergeCogImage, tab, fpcRightOffset, panelRightOffset, judgementX);
                    else
                        tabInspResult.AlignResult.RightX = algorithmTool.RunMainRightAlignX(inspTab.MergeCogImage, tab, fpcRightOffset, panelRightOffset, judgementX);

                    if (tabInspResult.AlignResult.RightX?.Judgement != Judgement.OK)
                    {
                        var rightAlignX = tabInspResult.AlignResult.RightX;
                        message = string.Format("Right AlignX Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, rightAlignX.Fpc.Judgement, rightAlignX.Panel.Judgement);
                        Logger.Debug(LogType.Inspection, message);
                    }

                    tabInspResult.AlignResult.RightY = algorithmTool.RunMainRightAlignY(inspTab.MergeCogImage, tab, fpcRightOffset, panelRightOffset, judgementY);
                    if (tabInspResult.AlignResult.RightY?.Judgement != Judgement.OK)
                    {
                        var rightAlignY = tabInspResult.AlignResult.RightY;
                        message = string.Format("Right AlignY Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, rightAlignY.Fpc.Judgement, rightAlignY.Panel.Judgement);
                        Logger.Debug(LogType.Inspection, message);
                    }

                    tabInspResult.AlignResult.CenterImage = algorithmTool.CropCenterAlign(inspTab.MergeCogImage, tab, fpcLeftOffset);
                }
                else
                {
                    tabInspResult.AlignResult.LeftX = new AlignResult();
                    tabInspResult.AlignResult.LeftY = new AlignResult();
                    tabInspResult.AlignResult.RightX = new AlignResult();
                    tabInspResult.AlignResult.RightY = new AlignResult();
                }
                #endregion

                #region Akkon Coordinate
                // Create Coordinate Object
                CoordinateTransform panelCoordinate = new CoordinateTransform();

                // Set Coordinate Params
                SetPanelCoordinateData(tab, panelCoordinate, tabInspResult);

                // Excuete Coordinate
                panelCoordinate.ExecuteCoordinate();
                #endregion

                #region Akkon
                if (AppsConfig.Instance().EnableAkkon)
                {
                    var roiList = tab.AkkonParam.GetAkkonROIList();
                    var coordinateRoiList = RenewalAkkonRoi(roiList, panelCoordinate);

                    // Tracking ROI Save
                    SaveTrackingAkkonROI(tabInspResult.TabNo, coordinateRoiList);

                    Judgement tabJudgement = Judgement.NG;

                    AkkonAlgorithm.UseOverCount = AppsConfig.Instance().EnableTest2;
                    var leadResultList = AkkonAlgorithm.Run(inspTab.MergeMatImage, coordinateRoiList, tab.AkkonParam.AkkonAlgoritmParam, resolution_um, ref tabJudgement);
                    var resizeRatio = tab.AkkonParam.AkkonAlgoritmParam.ImageFilterParam.ResizeRatio;

                    tabInspResult.AkkonResult = CreateAkkonResult(unitName, tab.Index, resizeRatio, leadResultList);
                    tabInspResult.AkkonResult.Judgement = tabJudgement;
                    tabInspResult.AkkonResult.TrackingROIList.AddRange(coordinateRoiList);
                    tabInspResult.AkkonInspMatImage = AkkonAlgorithm.ResizeMat;
                }
                else
                    tabInspResult.AkkonResult = new AkkonResult();
                #endregion

                sw.Stop();
                string resultMessage = string.Format("Tab {0} Inspection Completed.({1}ms)", (inspTab.TabScanBuffer.TabNo + 1), sw.ElapsedMilliseconds);
                Console.WriteLine(resultMessage);
                WriteLog(resultMessage, true);
            }

            AppsInspResult.Instance().Add(tabInspResult);
            SystemManager.Instance().UpdateResultTabButton(tabInspResult.TabNo);
            InspCount++;
        }

        public void InitalizeInspBuffer(string cameraName, List<TabScanBuffer> bufferList)
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

        private AkkonResult CreateAkkonResult(string unitName, int tabNo, float resizeRatio, List<AkkonLeadResult> leadResultList)
        {
            AkkonResult akkonResult = new AkkonResult();
            akkonResult.UnitName = unitName;
            akkonResult.TabNo = tabNo;
            akkonResult.ResizeRatio = resizeRatio;
            akkonResult.LeadResultList = leadResultList;

            List<int> leftCountList = new List<int>();
            List<int> rightCountList = new List<int>();

            List<double> leftLengthList = new List<double>();
            List<double> rightLengthList = new List<double>();

            bool isNg = false;
            foreach (var leadResult in leadResultList)
            {
                if (leadResult.ContainPos == LeadContainPos.Left)
                {
                    leftCountList.Add(leadResult.AkkonCount);
                    leftLengthList.Add(leadResult.LengthY_um);
                }
                else
                {
                    rightCountList.Add(leadResult.AkkonCount);
                    rightLengthList.Add(leadResult.LengthY_um);
                }
            }

            akkonResult.Judgement = isNg == false ? Judgement.OK : Judgement.NG;
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

            int id = 0;
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

                akkonRoi.Index = id;
                akkonRoi.SetLeftTopPoint(newLeftTop);
                akkonRoi.SetRightTopPoint(newRightTop);
                akkonRoi.SetLeftBottomPoint(newLeftBottom);
                akkonRoi.SetRightBottomPoint(newRightBottom);

                newList.Add(akkonRoi);
                id++;
            }

            return newList;
        }

        private void SaveTrackingAkkonROI(int tabNo, List<AkkonROI> roiList)
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            DateTime currentTime = AppsPreAlignResult.Instance().StartInspTime;
            string timeStamp = currentTime.ToString("yyyyMMddHHmmss");
            string month = currentTime.ToString("MM");
            string day = currentTime.ToString("dd");
            string folderPath = AppsInspResult.Instance().Cell_ID + "_" + timeStamp;

            string path = Path.Combine(ConfigSet.Instance().Path.Result, inspModel.Name, month, day, folderPath, "AkkonROI");
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            string cellId = AppsInspResult.Instance().Cell_ID + "_" + timeStamp;
            string filepath = Path.Combine(path, cellId + "_Tab_" + tabNo + ".txt");

            using (StreamWriter streamWriter = new StreamWriter(filepath, false))
            {
                foreach (var roi in roiList)
                {
                    string message = string.Format("{0} {1} {2} {3} {4} {5} {6} {7}",
                                                (int)roi.LeftTopX, (int)roi.LeftTopY,
                                                (int)roi.RightTopX, (int)roi.RightTopY,
                                                (int)roi.RightBottomX, (int)roi.RightBottomY,
                                                (int)roi.LeftBottomX, (int)roi.LeftBottomY);

                    streamWriter.WriteLine(message);
                }
            }
        }

        private void SetFpcCoordinateData(CoordinateTransform fpc, TabInspResult tabInspResult, double leftOffsetX, double leftOffsetY, double rightOffsetX, double rightOffsetY)
        {
            var teachingLeftPoint = tabInspResult.MarkResult.FpcMark.FoundedMark.Left.MaxMatchPos.ReferencePos;
            PointF teachedLeftPoint = new PointF(teachingLeftPoint.X + (float)leftOffsetX, teachingLeftPoint.Y + (float)leftOffsetY);

            PointF searchedLeftPoint = tabInspResult.MarkResult.FpcMark.FoundedMark.Left.MaxMatchPos.FoundPos;

            var teachingRightPoint = tabInspResult.MarkResult.FpcMark.FoundedMark.Right.MaxMatchPos.ReferencePos;
            PointF teachedRightPoint = new PointF(teachingRightPoint.X + (float)rightOffsetX, teachingRightPoint.Y + (float)rightOffsetY);

            PointF searchedRightPoint = tabInspResult.MarkResult.FpcMark.FoundedMark.Right.MaxMatchPos.FoundPos;

            fpc.SetReferenceData(teachedLeftPoint, teachedRightPoint);
            fpc.SetTargetData(searchedLeftPoint, searchedRightPoint);
        }

        private void SetPanelCoordinateData(Tab tab, CoordinateTransform panel, TabInspResult tabInspResult)
        {
            var leftMainMarkParam = tab.Mark.GetPanelMark(MarkDirection.Left, MarkName.Main);
            var leftMainMarkOrigin = leftMainMarkParam.InspParam.GetOrigin();
            PointF leftMainMarkOriginPoint = new PointF(Convert.ToSingle(leftMainMarkOrigin.TranslationX), Convert.ToSingle(leftMainMarkOrigin.TranslationY));
            PointF searchedLeftPoint = tabInspResult.MarkResult.PanelMark.FoundedMark.Left.MaxMatchPos.FoundPos;

            var rightMainMarkParam = tab.Mark.GetPanelMark(MarkDirection.Right, MarkName.Main);
            var lrightMainMarkOrigin = rightMainMarkParam.InspParam.GetOrigin();
            PointF rightMainMarkOriginPoint = new PointF(Convert.ToSingle(lrightMainMarkOrigin.TranslationX), Convert.ToSingle(lrightMainMarkOrigin.TranslationY));
            PointF searchedRightPoint = tabInspResult.MarkResult.PanelMark.FoundedMark.Right.MaxMatchPos.FoundPos;

            panel.SetReferenceData(leftMainMarkOriginPoint, rightMainMarkOriginPoint);
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
