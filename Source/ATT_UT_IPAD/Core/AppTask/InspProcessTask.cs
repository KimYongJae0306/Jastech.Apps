using ATT_UT_IPAD.Core.Data;
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
using Jastech.Framework.Imaging.VisionPro;
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

namespace ATT_UT_IPAD.Core.AppTask
{
    public class InspProcessTask
    {
        #region 필드
        private object _inspAkkonLock = new object();

        private object _inspAlignLock = new object();
        #endregion

        #region 속성
        public int InspAkkonCount { get; set; } = 0;

        public int InspAlignCount { get; set; } = 0;

        public Task AkkonProcessTask { get; set; }

        public CancellationTokenSource AkkonProcessTaskCancellationTokenSource { get; set; }

        public Task AlignProcessTask { get; set; }

        public CancellationTokenSource AlignProcessTaskCancellationTokenSource { get; set; }

        public AkkonAlgorithm AkkonAlgorithm { get; set; } = new AkkonAlgorithm();

        public List<ATTInspTab> InspAkkonTabList { get; set; } = new List<ATTInspTab>();

        public List<ATTInspTab> InspAlignTabList { get; set; } = new List<ATTInspTab>();

        private Queue<ATTInspTab> InspAkkonTabQueue = new Queue<ATTInspTab>();

        private Queue<ATTInspTab> InspAlignTabQueue = new Queue<ATTInspTab>();

        public Queue<VirtualData> VirtualQueue = new Queue<VirtualData>();
        #endregion

        #region 메서드
        // Akkon Camera로 Akkon, Align 검사 실행
        private void RunAkkonImage(ATTInspTab inspTab)
        {
            RunAlign(inspTab);
            RunAkkon(inspTab);
        }

        //Akkon Camera로 Akkon만 검사
        private void RunAkkon(ATTInspTab inspTab)
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
            CoordinateTransform panelCoordinate = new CoordinateTransform();

            algorithmTool.MainMarkInspect(inspTab.MergeCogImage, tab, ref inspResult, false);
            //algorithmTool.MainMarkInspect(inspTab.MergeCogImage, tab, ref inspResult, false);

            if (inspResult.MarkResult.Judgement != Judgement.OK)
            {
                // 검사 실패
                string message = string.Format("Mark Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, inspResult.MarkResult.FpcMark.Judgement, inspResult.MarkResult.PanelMark.Judgement);
                WriteLog(message);
                Logger.Debug(LogType.Inspection, message);
                inspResult.AkkonResult = new AkkonResult();
            }
            else
            {
                // Set Coordinate Params
                //algorithmTool.GetAlignPanelLeftOffset(tab, inspResult, out double panelLeftOffsetX, out double panelLeftOffsetY);
                //algorithmTool.GetAlignPanelRightOffset(tab, inspResult, out double panelRightOffsetX, out double panelRightOffsetY);
                //SetPanelCoordinateData(panelCoordinate, inspResult, panelLeftOffsetX, panelLeftOffsetY, panelRightOffsetX, panelRightOffsetY);

                // Excuete Coordinate
                SetPanelCoordinateData(panelCoordinate, inspResult, 0, 0, 0, 0);
                panelCoordinate.ExecuteCoordinate();

                var lineCamera = LineCameraManager.Instance().GetLineCamera("AkkonCamera").Camera;

                float resolution_um = lineCamera.PixelResolution_um / lineCamera.LensScale;

                if (AppsConfig.Instance().EnableAkkon)
                {
                    var roiList = tab.AkkonParam.GetAkkonROIList();
                    var coordinateList = RenewalAkkonRoi(roiList, panelCoordinate);

                    Judgement tabJudgement = Judgement.NG;
                    var leadResultList = AkkonAlgorithm.Run(inspTab.MergeMatImage, coordinateList, tab.AkkonParam.AkkonAlgoritmParam, resolution_um, ref tabJudgement);

                    inspResult.AkkonResult = CreateAkkonResult(unitName, tab.Index, leadResultList);
                    inspResult.AkkonResult.Judgement = tabJudgement;
                    inspResult.AkkonInspMatImage = AkkonAlgorithm.ResizeMat;
                }
                else
                    inspResult.AkkonResult = new AkkonResult();

                sw.Stop();
                string resultMessage = string.Format("Tab {0} Akkon Inspection Completed.({1}ms)", (inspTab.TabScanBuffer.TabNo + 1), sw.ElapsedMilliseconds);
                Console.WriteLine(resultMessage);
                WriteLog(resultMessage, false);
            }

            AppsInspResult.Instance().AddAkkon(inspResult);
            SystemManager.Instance().UpdateAkkonResultTabButton(inspResult.TabNo);
            InspAkkonCount++;
        }

        //Align Camera로 Align만 검사
        private void RunAlign(ATTInspTab inspTab)
        {
            Console.WriteLine("Test");
            Stopwatch sw = new Stopwatch();
            sw.Restart();

            string unitName = UnitName.Unit0.ToString();
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            Tab tab = inspModel.GetUnit(unitName).GetTab(inspTab.TabScanBuffer.TabNo);

            MainAlgorithmTool algorithmTool = new MainAlgorithmTool();

            TabInspResult tabInspResult = new TabInspResult();
            tabInspResult.TabNo = inspTab.TabScanBuffer.TabNo;
            tabInspResult.Image = inspTab.MergeMatImage;
            tabInspResult.CogImage = inspTab.MergeCogImage;

            algorithmTool.MainMarkInspect(inspTab.MergeCogImage, tab, ref tabInspResult, true);

            string message = string.Empty;
            if (tabInspResult.MarkResult.Judgement != Judgement.OK)
            {
                // 검사 실패
                message = string.Format("Mark Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, tabInspResult.MarkResult.FpcMark.Judgement, tabInspResult.MarkResult.PanelMark.Judgement);
                WriteLog(message, true);
                Logger.Debug(LogType.Inspection, message);
                tabInspResult.AlignResult = new TabAlignResult();
            }
            else
            {     // Create Coordinate Object
                CoordinateTransform fpcCoordinate = new CoordinateTransform();
                CoordinateTransform panelCoordinate = new CoordinateTransform();

                // 검사 성공
                message = string.Format("Mark Inspection OK !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, tabInspResult.MarkResult.FpcMark.Judgement, tabInspResult.MarkResult.PanelMark.Judgement);
                WriteLog(message, true);
                Logger.Debug(LogType.Inspection, message);

                algorithmTool.GetAlignFpcLeftOffset(tab, tabInspResult, out double fpcLeftOffsetX, out double fpcLeftOffsetY);
                algorithmTool.GetAlignFpcRightOffset(tab, tabInspResult, out double fpcRightOffsetX, out double fpcRightOffsetY);
                SetFpcCoordinateData(fpcCoordinate, tabInspResult, fpcLeftOffsetX, fpcLeftOffsetY, fpcRightOffsetX, fpcRightOffsetY);

                algorithmTool.GetAlignPanelLeftOffset(tab, tabInspResult, out double panelLeftOffsetX, out double panelLeftOffsetY);
                algorithmTool.GetAlignPanelRightOffset(tab, tabInspResult, out double panelRightOffsetX, out double panelRightOffsetY);
                SetPanelCoordinateData(panelCoordinate, tabInspResult, panelLeftOffsetX, panelLeftOffsetY, panelRightOffsetX, panelRightOffsetY);

                // Execute Coordinate
                fpcCoordinate.ExecuteCoordinate();
                panelCoordinate.ExecuteCoordinate();

                var lineCamera = LineCameraManager.Instance().GetLineCamera("AlignCamera").Camera;

                float resolution_um = lineCamera.PixelResolution_um / lineCamera.LensScale;
                double judgementX = tab.AlignSpec.LeftSpecX_um / resolution_um;
                double judgementY = tab.AlignSpec.LeftSpecY_um / resolution_um;

                #region Align
                if (AppsConfig.Instance().EnableAlign)
                {
                    tabInspResult.AlignResult.LeftX = algorithmTool.RunMainLeftAlignX(inspTab.MergeCogImage, tab, fpcCoordinate, panelCoordinate, judgementX);
                    if (tabInspResult.AlignResult.LeftX?.Judgement != Judgement.OK)
                    {
                        var leftAlignX = tabInspResult.AlignResult.LeftX;
                        message = string.Format("Left AlignX Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, leftAlignX.Fpc.Judgement, leftAlignX.Panel.Judgement);
                        WriteLog(message, true);
                        Logger.Debug(LogType.Inspection, message);
                    }

                    tabInspResult.AlignResult.LeftY = algorithmTool.RunMainLeftAlignY(inspTab.MergeCogImage, tab, fpcCoordinate, panelCoordinate, judgementY);
                    if (tabInspResult.AlignResult.LeftY?.Judgement != Judgement.OK)
                    {
                        var leftAlignY = tabInspResult.AlignResult.LeftY;
                        message = string.Format("Left AlignY Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, leftAlignY.Fpc.Judgement, leftAlignY.Panel.Judgement);
                        WriteLog(message, true);
                        Logger.Debug(LogType.Inspection, message);
                    }

                    tabInspResult.AlignResult.RightX = algorithmTool.RunMainRightAlignX(inspTab.MergeCogImage, tab, fpcCoordinate, panelCoordinate, judgementX);
                    if (tabInspResult.AlignResult.RightX?.Judgement != Judgement.OK)
                    {
                        var rightAlignX = tabInspResult.AlignResult.RightX;
                        message = string.Format("Right AlignX Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, rightAlignX.Fpc.Judgement, rightAlignX.Panel.Judgement);
                        WriteLog(message, true);
                        Logger.Debug(LogType.Inspection, message);
                    }

                    tabInspResult.AlignResult.RightY = algorithmTool.RunMainRightAlignY(inspTab.MergeCogImage, tab, fpcCoordinate, panelCoordinate, judgementY);
                    if (tabInspResult.AlignResult.RightY?.Judgement != Judgement.OK)
                    {
                        var rightAlignY = tabInspResult.AlignResult.RightY;
                        message = string.Format("Right AlignY Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, rightAlignY.Fpc.Judgement, rightAlignY.Panel.Judgement);
                        WriteLog(message, true);
                        Logger.Debug(LogType.Inspection, message);
                    }

                    tabInspResult.AlignResult.CenterImage = algorithmTool.CropCenterAlign(inspTab.MergeCogImage, tab, fpcCoordinate);
                }
                else
                {
                    tabInspResult.AlignResult.LeftX = new AlignResult();
                    tabInspResult.AlignResult.LeftY = new AlignResult();
                    tabInspResult.AlignResult.RightX = new AlignResult();
                    tabInspResult.AlignResult.RightY = new AlignResult();
                }
                #endregion

                #region Center Align
                // EnableAlign false 일때 구조 생각
                tabInspResult.AlignResult.CenterX = Math.Abs(tabInspResult.AlignResult.LeftX.ResultValue_pixel - tabInspResult.AlignResult.RightX.ResultValue_pixel);
                #endregion

                sw.Stop();
                string resultMessage = string.Format("Tab {0} Align Inspection Completed.({1}ms)", (inspTab.TabScanBuffer.TabNo + 1), sw.ElapsedMilliseconds);
                Console.WriteLine(resultMessage);
                WriteLog(resultMessage, false);
            }

            AppsInspResult.Instance().AddAlign(tabInspResult);
            SystemManager.Instance().UpdateAlignResultTabButton(tabInspResult.TabNo);
            InspAlignCount++;
        }

        public void InitalizeInspAkkonBuffer(string cameraName, List<TabScanBuffer> bufferList)
        {
            InspAkkonCount = 0;
            DisposeInspAkkonTabList();
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            foreach (var buffer in bufferList)
            {
                ATTInspTab inspTab = new ATTInspTab();
                inspTab.CameraName = cameraName;
                inspTab.TabScanBuffer = buffer;
                inspTab.InspectEvent += AddInspAkkonEventFuction;
                inspTab.StartInspTask();
                InspAkkonTabList.Add(inspTab);
            }
        }

        public void InitalizeInspAlignBuffer(string cameraName, List<TabScanBuffer> bufferList)
        {
            InspAlignCount = 0;
            DisposeInspAlignTabList();
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            foreach (var buffer in bufferList)
            {
                ATTInspTab inspTab = new ATTInspTab();
                inspTab.CameraName = cameraName;
                inspTab.TabScanBuffer = buffer;
                inspTab.InspectEvent += AddInspAlignEventFuction;
                inspTab.StartInspTask();
                InspAlignTabList.Add(inspTab);
            }
        }

        public void StartTask()
        {
            if (AkkonProcessTask == null)
            {
                AkkonProcessTaskCancellationTokenSource = new CancellationTokenSource();
                AkkonProcessTask = new Task(AkkonInspection, AkkonProcessTaskCancellationTokenSource.Token);
                AkkonProcessTask.Start();
            }

            if (AlignProcessTask == null)
            {
                AlignProcessTaskCancellationTokenSource = new CancellationTokenSource();
                AlignProcessTask = new Task(AlignInspection, AlignProcessTaskCancellationTokenSource.Token);
                AlignProcessTask.Start();
            }
        }

        public void StopTask()
        {
            if (AkkonProcessTask == null)
            {
                while (InspAkkonTabQueue.Count > 0)
                {
                    var data = InspAkkonTabQueue.Dequeue();
                    data.Dispose();
                }

                AkkonProcessTaskCancellationTokenSource.Cancel();
                AkkonProcessTask.Wait();
                AkkonProcessTask = null;
            }

            if (AlignProcessTask == null)
            {
                while (InspAlignTabQueue.Count > 0)
                {
                    var data = InspAlignTabQueue.Dequeue();
                    data.Dispose();
                }

                AlignProcessTaskCancellationTokenSource.Cancel();
                AlignProcessTask.Wait();
                AlignProcessTask = null;
            }
        }

        private void AkkonInspection()
        {
            while (true)
            {
                if (AkkonProcessTaskCancellationTokenSource.IsCancellationRequested)
                    break;

                if (GetInspAkkonTab() is ATTInspTab inspTab)
                {
                    if (AppsConfig.Instance().EnableTest1)
                        RunAkkonImage(inspTab);
                    else
                        RunAkkon(inspTab);
                }

                Thread.Sleep(50);
            }
        }

        private void AlignInspection()
        {
            while (true)
            {
                if (AlignProcessTaskCancellationTokenSource.IsCancellationRequested)
                    break;

                if (GetInspAlignTab() is ATTInspTab inspTab)
                {
                    if (AppsConfig.Instance().EnableTest1 == false)
                        RunAlign(inspTab);
                }

                Thread.Sleep(50);
            }
        }

        private ATTInspTab GetInspAkkonTab()
        {
            lock (_inspAkkonLock)
            {
                if (InspAkkonTabQueue.Count() > 0)
                    return InspAkkonTabQueue.Dequeue();
                else
                    return null;
            }
        }

        private ATTInspTab GetInspAlignTab()
        {
            lock (_inspAlignLock)
            {
                if (InspAlignTabQueue.Count() > 0)
                    return InspAlignTabQueue.Dequeue();
                else
                    return null;
            }
        }

        public void DisposeInspAkkonTabList()
        {
            foreach (var inspTab in InspAkkonTabList)
            {
                inspTab.StopInspTask();
                inspTab.InspectEvent -= AddInspAkkonEventFuction;
                inspTab.Dispose();
            }
            InspAkkonTabList.Clear();
        }

        public void DisposeInspAlignTabList()
        {
            foreach (var inspTab in InspAlignTabList)
            {
                inspTab.StopInspTask();
                inspTab.InspectEvent -= AddInspAlignEventFuction;
                inspTab.Dispose();
            }
            InspAlignTabList.Clear();
        }

        private void AddInspAkkonEventFuction(ATTInspTab inspTab)
        {
            lock (_inspAkkonLock)
                InspAkkonTabQueue.Enqueue(inspTab);
        }

        private void AddInspAlignEventFuction(ATTInspTab inspTab)
        {
            lock (_inspAlignLock)
                InspAlignTabQueue.Enqueue(inspTab);
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

        private void SetPanelCoordinateData(CoordinateTransform panel, TabInspResult tabInspResult, double leftOffsetX, double leftOffsetY, double rightOffsetX, double rightOffsetY)
        {
            var teachingLeftPoint = tabInspResult.MarkResult.PanelMark.FoundedMark.Left.MaxMatchPos.ReferencePos;
            PointF teachedLeftPoint = new PointF(teachingLeftPoint.X + (float)leftOffsetX, teachingLeftPoint.Y + (float)leftOffsetY);
            PointF searchedLeftPoint = tabInspResult.MarkResult.PanelMark.FoundedMark.Left.MaxMatchPos.FoundPos;

            var teachingRightPoint = tabInspResult.MarkResult.PanelMark.FoundedMark.Right.MaxMatchPos.ReferencePos;
            PointF teachedRightPoint = new PointF(teachingRightPoint.X + (float)rightOffsetX, teachingRightPoint.Y + (float)rightOffsetY);
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
            lock (VirtualQueue)
            {
                while (VirtualQueue.Count > 0)
                {
                    var data = VirtualQueue.Dequeue();

                    var akkonInspTab = InspAkkonTabList.Where(x => x.TabScanBuffer.TabNo == data.TabNo).FirstOrDefault();
                    akkonInspTab?.SetVirtualImage(data.FilePath);

                    var alingInspTab = InspAlignTabList.Where(x => x.TabScanBuffer.TabNo == data.TabNo).FirstOrDefault();
                    alingInspTab?.SetVirtualImage(data.FilePath);
                }
            }
        }

        private PointF GetMainOrginPoint(Material material, Tab tab)
        {
            if (material == Material.Panel)
            {
                var panelMainMark = tab.MarkParamter.MainPanelMarkParamList.Where(x => x.Name == MarkName.Main).First();
                var orgin = panelMainMark.InspParam.GetOrigin();
                double orginX = orgin.TranslationX;
                double orginY = orgin.TranslationY;
                return new PointF((float)orginX, (float)orginY);
            }
            else
            {
                var fpcMainMark = tab.MarkParamter.MainFpcMarkParamList.Where(x => x.Name == MarkName.Main).First();
                var orgin = fpcMainMark.InspParam.GetOrigin();

                double orginX = orgin.TranslationX;
                double orginY = orgin.TranslationY;
                return new PointF((float)orginX, (float)orginY);
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
