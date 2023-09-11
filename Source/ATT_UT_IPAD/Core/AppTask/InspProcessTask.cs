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
using Jastech.Framework.Config;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro;
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
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

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

            //algorithmTool.MainMarkInspect(inspTab.MergeCogImage, tab, ref inspResult, false);
            algorithmTool.MainPanelMarkInspect(inspTab.MergeCogImage, tab, ref inspResult);

            string message = string.Empty;
            if (inspResult.MarkResult.Judgement != Judgement.OK)
            {
                // 검사 실패
                message = string.Format("Akkon Mark Insp NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index + 1, inspResult.MarkResult.FpcMark.Judgement, inspResult.MarkResult.PanelMark.Judgement);
                WriteLog(message);
                Logger.Debug(LogType.Inspection, message);
                inspResult.AkkonResult = new AkkonResult();
            }
            else
            {
                message = string.Format("Akkon Mark Insp OK !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index + 1, inspResult.MarkResult.FpcMark.Judgement, inspResult.MarkResult.PanelMark.Judgement);
                WriteLog(message, true);
                Logger.Debug(LogType.Inspection, message);

                // Create Coordinate Object
                CoordinateTransform panelCoordinate = new CoordinateTransform();

                // Set Coordinate Params
                SetPanelCoordinateData(tab, panelCoordinate, inspResult);

                // Excuete Coordinate
                panelCoordinate.ExecuteCoordinate();

                var lineCamera = LineCameraManager.Instance().GetLineCamera("AkkonCamera").Camera;

                float resolution_um = lineCamera.PixelResolution_um / lineCamera.LensScale;

                if (AppsConfig.Instance().EnableAkkon)
                {
                    var roiList = tab.AkkonParam.GetAkkonROIList();
                    var coordinateRoiList = RenewalAkkonRoi(roiList, panelCoordinate);

                    // Tracking ROI Save        
                    SaveTrackingAkkonROI(inspResult.TabNo, coordinateRoiList);

                    Judgement tabJudgement = Judgement.NG;

                    AkkonAlgorithm.UseOverCount = AppsConfig.Instance().EnableTest2;
                    var leadResultList = AkkonAlgorithm.Run(inspTab.MergeMatImage, coordinateRoiList, tab.AkkonParam.AkkonAlgoritmParam, resolution_um, ref tabJudgement);
                    var resizeRatio = tab.AkkonParam.AkkonAlgoritmParam.ImageFilterParam.ResizeRatio;

                    inspResult.AkkonResult = CreateAkkonResult(unitName, tab.Index, resizeRatio, leadResultList);
                    inspResult.AkkonResult.Judgement = tabJudgement;
                    inspResult.AkkonResult.TrackingROIList.AddRange(coordinateRoiList);
                    inspResult.AkkonInspMatImage = AkkonAlgorithm.ResizeMat;
                }
                else
                    inspResult.AkkonResult = new AkkonResult();

                sw.Stop();
                string resultMessage = string.Format("Tab {0} Akkon Insp Completed.({1}ms)", (inspTab.TabScanBuffer.TabNo + 1), sw.ElapsedMilliseconds);
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
            Stopwatch sw = new Stopwatch();
            sw.Restart();

            var lineCamera = LineCameraManager.Instance().GetLineCamera("AlignCamera").Camera;
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
            tabInspResult.AlignResult.PreHead = PlcControlManager.Instance().GetPreHeadData(tabInspResult.TabNo);

            algorithmTool.MainMarkInspect(inspTab.MergeCogImage, tab, ref tabInspResult, true);

            string message = string.Empty;
            if (tabInspResult.MarkResult.Judgement != Judgement.OK)
            {
                // 검사 실패
                message = string.Format("Align Mark Insp NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index + 1, tabInspResult.MarkResult.FpcMark.Judgement, tabInspResult.MarkResult.PanelMark.Judgement);
                WriteLog(message, true);
                Logger.Debug(LogType.Inspection, message);
                tabInspResult.AlignResult = new TabAlignResult();
            }
            else
            {
                // 검사 성공
                message = string.Format("Align Mark Insp OK !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index + 1, tabInspResult.MarkResult.FpcMark.Judgement, tabInspResult.MarkResult.PanelMark.Judgement);
                WriteLog(message, true);
                Logger.Debug(LogType.Inspection, message);

                var fpcLeftOrigin = tab.MarkParamter.GetFPCMark(MarkDirection.Left, MarkName.Main, true).InspParam.GetOrigin();
                var fpcRightOrigin = tab.MarkParamter.GetFPCMark(MarkDirection.Right, MarkName.Main, true).InspParam.GetOrigin();
                var panelLeftOrigin = tab.MarkParamter.GetPanelMark(MarkDirection.Left, MarkName.Main, true).InspParam.GetOrigin();
                var panelRightOrigin = tab.MarkParamter.GetPanelMark(MarkDirection.Right, MarkName.Main, true).InspParam.GetOrigin();

                PointF fpcLeftOriginPoint = new PointF(Convert.ToSingle(fpcLeftOrigin.TranslationX), Convert.ToSingle(fpcLeftOrigin.TranslationY));
                PointF fpcRightOriginPoint = new PointF(Convert.ToSingle(fpcRightOrigin.TranslationX), Convert.ToSingle(fpcRightOrigin.TranslationY));
                PointF panelLeftOriginPoint = new PointF(Convert.ToSingle(panelLeftOrigin.TranslationX), Convert.ToSingle(panelLeftOrigin.TranslationY));
                PointF panelRightOriginPoint = new PointF(Convert.ToSingle(panelRightOrigin.TranslationX), Convert.ToSingle(panelRightOrigin.TranslationY));

                PointF fpcLeftOffset = MathHelper.GetOffset(fpcLeftOriginPoint, tabInspResult.MarkResult.FpcMark.FoundedMark.Left.MaxMatchPos.FoundPos);
                PointF fpcRightOffset = MathHelper.GetOffset(fpcRightOriginPoint, tabInspResult.MarkResult.FpcMark.FoundedMark.Right.MaxMatchPos.FoundPos);
                PointF panelLeftOffset = MathHelper.GetOffset(panelLeftOriginPoint, tabInspResult.MarkResult.PanelMark.FoundedMark.Left.MaxMatchPos.FoundPos);
                PointF panelRightOffset = MathHelper.GetOffset(panelRightOriginPoint, tabInspResult.MarkResult.PanelMark.FoundedMark.Right.MaxMatchPos.FoundPos);
              
                double judgementX = tab.AlignSpec.LeftSpecX_um / resolution_um;
                double judgementY = tab.AlignSpec.LeftSpecY_um / resolution_um;
                double judgementCX = tab.AlignSpec.CenterSpecX_um;
                #region Align
                if (AppsConfig.Instance().EnableAlign)
                {
                    tabInspResult.AlignResult.LeftX = algorithmTool.RunMainLeftAutoAlignX(inspTab.MergeCogImage, tab, fpcLeftOffset, panelLeftOffset, judgementX);
                    if (tabInspResult.AlignResult.LeftX?.Judgement != Judgement.OK)
                    {
                        var leftAlignX = tabInspResult.AlignResult.LeftX;
                        message = string.Format("Left AlignX Insp NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index + 1, leftAlignX.Fpc.Judgement, leftAlignX.Panel.Judgement);
                        WriteLog(message, true);
                        Logger.Debug(LogType.Inspection, message);
                    }

                    tabInspResult.AlignResult.LeftY = algorithmTool.RunMainLeftAlignY(inspTab.MergeCogImage, tab, fpcLeftOffset, panelLeftOffset, judgementY);
                    if (tabInspResult.AlignResult.LeftY?.Judgement != Judgement.OK)
                    {
                        var leftAlignY = tabInspResult.AlignResult.LeftY;
                        message = string.Format("Left AlignY Insp NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index + 1, leftAlignY.Fpc.Judgement, leftAlignY.Panel.Judgement);
                        WriteLog(message, true);
                        Logger.Debug(LogType.Inspection, message);
                    }

                    tabInspResult.AlignResult.RightX = algorithmTool.RunMainRightAutoAlignX(inspTab.MergeCogImage, tab, fpcRightOffset, panelRightOffset, judgementX);
                    if (tabInspResult.AlignResult.RightX?.Judgement != Judgement.OK)
                    {
                        var rightAlignX = tabInspResult.AlignResult.RightX;
                        message = string.Format("Right AlignX Insp NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index + 1, rightAlignX.Fpc.Judgement, rightAlignX.Panel.Judgement);
                        WriteLog(message, true);
                        Logger.Debug(LogType.Inspection, message);
                    }

                    tabInspResult.AlignResult.RightY = algorithmTool.RunMainRightAlignY(inspTab.MergeCogImage, tab, fpcRightOffset, panelRightOffset, judgementY);
                    if (tabInspResult.AlignResult.RightY?.Judgement != Judgement.OK)
                    {
                        var rightAlignY = tabInspResult.AlignResult.RightY;
                        message = string.Format("Right AlignY Insp NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index + 1, rightAlignY.Fpc.Judgement, rightAlignY.Panel.Judgement);
                        WriteLog(message, true);
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

                #region Center Align
                #endregion

                sw.Stop();
                string resultMessage = string.Format("Tab {0} Align Insp Completed.({1}ms)", (inspTab.TabScanBuffer.TabNo + 1), sw.ElapsedMilliseconds);
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
                //AkkonProcessTask.Wait();
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
                //AlignProcessTask.Wait();
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
            roiList = roiList.OrderBy(x => x.LeftTopX).ToList();
            int id = 0;
            foreach (var roi in roiList)
            {
                PointF leftTop = roi.GetLeftTopPoint();
                PointF rightTop = roi.GetRightTopPoint();
                PointF leftBottom = roi.GetLeftBottomPoint();
                PointF rightBottom = roi.GetRightBottomPoint();

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

            DateTime currentTime = AppsInspResult.Instance().StartInspTime;
            string timeStamp = currentTime.ToString("yyyyMMddHHmmss");
            string month = currentTime.ToString("MM");
            string day = currentTime.ToString("dd");
            string folderPath = AppsInspResult.Instance().Cell_ID + "_" + timeStamp;

            string path = Path.Combine(ConfigSet.Instance().Path.Result, inspModel.Name, month, day, folderPath, "Akkon");
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
            var leftMainMarkParam = tab.MarkParamter.GetPanelMark(MarkDirection.Left, MarkName.Main, false);
            var leftMainMarkOrigin = leftMainMarkParam.InspParam.GetOrigin();
            PointF leftMainMarkOriginPoint = new PointF(Convert.ToSingle(leftMainMarkOrigin.TranslationX), Convert.ToSingle(leftMainMarkOrigin.TranslationY));
            PointF searchedLeftPoint = tabInspResult.MarkResult.PanelMark.FoundedMark.Left.MaxMatchPos.FoundPos;

            var rightMainMarkParam = tab.MarkParamter.GetPanelMark(MarkDirection.Right, MarkName.Main, false);
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

        //private PointF GetFpcLeftOffset(TabMarkResult markResult)
        //{
        //    var searchedLeftPoint = markResult.FpcMark.FoundedMark.Left.MaxMatchPos.FoundPos;
        //    var teachingLeftPoint = markResult.FpcMark.FoundedMark.Left.MaxMatchPos.ReferencePos;

        //    return MathHelper.GetOffset(searchedLeftPoint, teachingLeftPoint);
        //}
        #endregion
    }

    public class VirtualData
    {
        public int TabNo { get; set; }

        public string FilePath { get; set; }
    }
}
