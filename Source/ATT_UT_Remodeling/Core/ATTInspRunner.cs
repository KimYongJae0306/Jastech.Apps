using Cognex.VisionPro;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Core.Calibrations;
using Jastech.Apps.Winform.Service;
using Jastech.Apps.Winform.Service.Plc.Maps;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Algorithms.Akkon;
using Jastech.Framework.Algorithms.Akkon.Parameters;
using Jastech.Framework.Algorithms.Akkon.Results;
using Jastech.Framework.Config;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using Jastech.Framework.Util;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using static Jastech.Framework.Modeller.Controls.ModelControl;

namespace ATT_UT_Remodeling.Core
{
    public partial class ATTInspRunner
    {
        #region 필드
        private Axis _axis { get; set; } = null;

        private object _akkonLock = new object();

        private object _inspLock = new object();
        #endregion

        #region 속성
        private Task SeqTask { get; set; }

        private CancellationTokenSource SeqTaskCancellationTokenSource { get; set; }

        private SeqStep SeqStep { get; set; } = SeqStep.SEQ_IDLE;

        public bool IsPanelIn { get; set; } = false;

        private bool IsGrabDone { get; set; } = false;

        private AppsInspResult AppsInspResult { get; set; } = null;

        private Stopwatch LastInspSW { get; set; } = new Stopwatch();

        public Task InspTask { get; set; }

        public CancellationTokenSource CancelInspTask { get; set; }

        public Queue<AkkonThreadParam> InspQueue = new Queue<AkkonThreadParam>();

        public Queue<ATTInspTab> InspTabQueue = new Queue<ATTInspTab>();

        public AkkonAlgorithm AkkonAlgorithm { get; set; } = new AkkonAlgorithm();

        public List<ATTInspTab> InspTabList { get; set; } = new List<ATTInspTab>();

       
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public ATTInspRunner()
        {
            //if(AppsConfig.Instance().AkkonAlgorithmType == AkkonAlgorithmType.Macron)
            //    MacronAkkonAlgorithmTool = new MacronAkkonAlgorithmTool();
            //else
            //    AkkonAlgorithm = new AkkonAlgorithm();
        }
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

            if (inspResult.MarkResult.IsGood() == false)
            {
                // 검사 실패
                string message = string.Format("Mark Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, inspResult.MarkResult.FpcMark.Judgement, inspResult.MarkResult.PanelMark.Judgement);
                Logger.Debug(LogType.Inspection, message);
            }
            else
            {
                // Set Coordinate Params
                SetCoordinateData(fpcCoordinate, inspResult);
                SetCoordinateData(panelCoordinate, inspResult);

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
                    if (inspResult.AlignResult.IsLeftXGood() == false)
                    {
                        var leftAlignX = inspResult.AlignResult.LeftX;
                        string message = string.Format("Left AlignX Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, leftAlignX.Fpc.Judgement, leftAlignX.Panel.Judgement);
                        Logger.Debug(LogType.Inspection, message);
                    }

                    inspResult.AlignResult.LeftY = algorithmTool.RunMainLeftAlignY(inspTab.MergeCogImage, tab, fpcCoordinate, panelCoordinate, judgementY);
                    if (inspResult.AlignResult.IsLeftYGood() == false)
                    {
                        var leftAlignY = inspResult.AlignResult.LeftY;
                        string message = string.Format("Left AlignY Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, leftAlignY.Fpc.Judgement, leftAlignY.Panel.Judgement);
                        Logger.Debug(LogType.Inspection, message);
                    }

                    inspResult.AlignResult.RightX = algorithmTool.RunMainRightAlignX(inspTab.MergeCogImage, tab, fpcCoordinate, panelCoordinate, judgementX);
                    if (inspResult.AlignResult.IsRightXGood() == false)
                    {
                        var rightAlignX = inspResult.AlignResult.RightX;
                        string message = string.Format("Right AlignX Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, rightAlignX.Fpc.Judgement, rightAlignX.Panel.Judgement);
                        Logger.Debug(LogType.Inspection, message);
                    }

                    inspResult.AlignResult.RightY = algorithmTool.RunMainRightAlignY(inspTab.MergeCogImage, tab, fpcCoordinate, panelCoordinate, judgementY);
                    if (inspResult.AlignResult.IsRightYGood() == false)
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
                }
                else
                    inspResult.AkkonResult = new AkkonResult();

                AppsInspResult.TabResultList.Add(inspResult);

                sw.Stop();
                string resultMessage = string.Format("Tab {0} Inspection Completed.({1}ms)", inspTab.TabScanBuffer.TabNo, sw.ElapsedMilliseconds);
                Console.WriteLine(resultMessage);
                WriteLog(resultMessage, true);
            }
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
                    leftCountNG |= leadResult.Judgement == Judgment.NG ? true : false;
                    leftCountList.Add(leadResult.AkkonCount);

                    leftLengthNG |= leadResult.Judgement == Judgment.NG ? true : false;
                    leftLengthList.Add(leadResult.LengthY_um);
                }
                else
                {
                    rightCountNG |= leadResult.Judgement == Judgment.NG ? true : false;
                    rightCountList.Add(leadResult.AkkonCount);

                    rightLengthNG |= leadResult.Judgement == Judgment.NG ? true : false;
                    rightLengthList.Add(leadResult.LengthY_um);
                }
            }

            akkonResult.AkkonCountJudgement = (leftCountNG || rightCountNG) == true ? AkkonJudgement.NG_Akkon : AkkonJudgement.OK;
            akkonResult.LeftCount_Avg = (int)leftCountList.Average();
            akkonResult.LeftCount_Min = (int)leftCountList.Min();
            akkonResult.LeftCount_Max = (int)leftCountList.Max();
            akkonResult.RightCount_Avg = (int)rightCountList.Average();
            akkonResult.RightCount_Min = (int)rightCountList.Min();
            akkonResult.RightCount_Max = (int)rightCountList.Max();

            akkonResult.LengthJudgement = (leftLengthNG || rightLengthNG) == true ? Judgment.NG : Judgment.OK;
            akkonResult.Length_Left_Avg_um = (float)leftLengthList.Average();
            akkonResult.Length_Left_Min_um = (float)leftLengthList.Min();
            akkonResult.Length_Left_Max_um = (float)leftLengthList.Max();
            akkonResult.Length_Right_Avg_um = (float)rightLengthList.Average();
            akkonResult.Length_Right_Min_um = (float)rightLengthList.Min();
            akkonResult.Length_Right_Max_um = (float)rightLengthList.Max();

            akkonResult.LeadResultList = leadResultList;

            return akkonResult;
        }

        public void InitalizeInspTab(string cameraName, List<TabScanBuffer> bufferList)
        {
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

        private void AddInspectEventFuction(ATTInspTab inspTab)
        {
            lock (_inspLock)
                InspTabQueue.Enqueue(inspTab);
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

        public void SetVirtualmage(int tabNo, string fileName)
        {
            InspTabList[tabNo].SetVirtualImage(fileName);
        }

        private void ATTSeqRunner_GrabDoneEventHanlder(string cameraName, bool isGrabDone)
        {
            IsGrabDone = isGrabDone;
        }

        private void Inspection()
        {
            while (true)
            {
                if (CancelInspTask.IsCancellationRequested)
                {
                    break;
                }

                if (GetInspTab() is ATTInspTab inspTab)
                {
                    Run(inspTab);
                }

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

        private AkkonThreadParam GetAkkonThreadParam()
        {
            lock (_akkonLock)
            {
                if (InspQueue.Count > 0)
                    return InspQueue.Dequeue();
                else
                    return null;
            }
        }

        public void StartAkkonInspTask()
        {
            if (InspTask != null)
                return;

            CancelInspTask = new CancellationTokenSource();
            InspTask = new Task(Inspection, CancelInspTask.Token);
            InspTask.Start();
        }

        public void StopAkkonInspTask()
        {
            if (InspTask == null)
                return;

            while (InspTabQueue.Count > 0)
            {
                var data = InspTabQueue.Dequeue();
                data.Dispose();
            }

            CancelInspTask.Cancel();
            InspTask.Wait();
            InspTask = null;
        }

        public void ClearResult()
        {
            if (AppsInspResult == null)
                AppsInspResult = new AppsInspResult();

            if (AppsInspResult != null)
                AppsInspResult.Dispose();

            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
        }

        public bool IsInspectionDone()
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            
            lock (AppsInspResult)
            {
                if (AppsInspResult.TabResultList.Count() == inspModel.TabCount)
                    return true;
            }
            return false;
        }

        public void SeqRun()
        {
            if (ModelManager.Instance().CurrentModel == null)
                return;

            SystemManager.Instance().MachineStatus = MachineStatus.RUN;

            var lineCamera = LineCameraManager.Instance().GetAppsCamera("LineCamera");

            lineCamera.GrabDoneEventHanlder += ATTSeqRunner_GrabDoneEventHanlder;
            StartAkkonInspTask();

            if (SeqTask != null)
            {
                SeqStep = SeqStep.SEQ_START;
                return;
            }

            SeqTaskCancellationTokenSource = new CancellationTokenSource();
            SeqTask = new Task(SeqTaskAction, SeqTaskCancellationTokenSource.Token);
            SeqTask.Start();
        }

        public void SeqStop()
        {
            SystemManager.Instance().MachineStatus = MachineStatus.STOP;

            var lineCamera = LineCameraManager.Instance().GetAppsCamera("LineCamera");
            lineCamera.StopGrab();
            lineCamera.GrabDoneEventHanlder -= ATTSeqRunner_GrabDoneEventHanlder;
            LineCameraManager.Instance().GetLineCamera("LineCamera").StopGrab();

            var areaCamera = AreaCameraManager.Instance().GetAppsCamera("PreAlign");
            areaCamera.StopGrab();
            AreaCameraManager.Instance().GetAreaCamera("PreAlign").StopGrab();

            StopAkkonInspTask();

            // 조명 off
            LAFManager.Instance().TrackingOnOff("Laf", false);
            WriteLog("AutoFocus Off.");

            LineCameraManager.Instance().Stop("LineCamera");
            WriteLog("Stop Grab.");

            if (SeqTask == null)
                return;

            //foreach (var item in AppsInspResult.TabResultList)
            //{
            //    item.Dispose();
            //}
            //AppsInspResult.TabResultList.Clear();
            SeqTaskCancellationTokenSource.Cancel();
            SeqTask.Wait();
            SeqTask = null;

            WriteLog("Stop Sequence.");
        }

        private void SeqTaskAction()
        {
            var cancellationToken = SeqTaskCancellationTokenSource.Token;
            cancellationToken.ThrowIfCancellationRequested();
            SeqStep = SeqStep.SEQ_INIT;

            while (true)
            {
                // 작업 취소
                if (cancellationToken.IsCancellationRequested)
                {
                    SeqStep = SeqStep.SEQ_IDLE;
                    //조명 Off
                    LineCameraManager.Instance().Stop("LineCamera");
                    DisposeInspTabList();
                    break;
                }

                SeqTaskLoop();
                Thread.Sleep(50);
            }
        }

        private void SeqTaskLoop()
        {
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            if (inspModel == null)
                return;

            var unit = inspModel.GetUnit(UnitName.Unit0);
            if (unit == null)
                return;

            var tab = unit.GetTab(0);
            if (tab == null)
                return;

            var lineCamera = LineCameraManager.Instance().GetLineCamera("LineCamera");
            if (lineCamera == null)
                return;

            var laf = LAFManager.Instance().GetLAFCtrl("Laf");
            if (laf == null)
                return;
            var light = DeviceManager.Instance().LightCtrlHandler;

            if (light == null)
                return;
            string systemLogMessage = string.Empty;
            string errorMessage = string.Empty;

            switch (SeqStep)
            {
                case SeqStep.SEQ_IDLE:
                    break;
                case SeqStep.SEQ_INIT:
                    lineCamera.IsLive = false;
                    lineCamera.StopGrab();
                    WriteLog("Stop Grab.");

                    light.TurnOff();
                    WriteLog("Light Off.");

                    LAFManager.Instance().TrackingOnOff("Laf", false);
                    laf.SetMotionAbsoluteMove(0);
                    WriteLog("Laf Off.");

                    ClearResult();
                    WriteLog("Clear Result.");

                    InitializeBuffer();
                    WriteLog("Initialize Buffer.");

              
                    SeqStep = SeqStep.SEQ_WAITING;
                    break;
                case SeqStep.SEQ_WAITING:

                    if (AppsStatus.Instance().IsInspRunnerFlagFromPlc == false)
                        break;

                    WriteLog("Receive Inspection Start Signal From PLC.", true);

                    AppsInspResult.StartInspTime = DateTime.Now;
                    AppsInspResult.Cell_ID = GetCellID();

                    WriteLog("Cell ID : " + AppsInspResult.Cell_ID, true);
                    SeqStep = SeqStep.SEQ_MOVE_START_POS;
                    break;
                case SeqStep.SEQ_MOVE_START_POS:
                    if (MoveTo(TeachingPosType.Stage1_Scan_Start, out errorMessage) == false)
                        break;

                    SeqStep = SeqStep.SEQ_SCAN_START;
                    break;

                case SeqStep.SEQ_SCAN_START:

                    IsGrabDone = false;

                    LAFManager.Instance().TrackingOnOff("Laf", true);
                    WriteLog("Laser Auto Focus On.");

                    light.TurnOn(unit.GetLineCameraData("Akkon").LightParam);
                    Thread.Sleep(100);
                    WriteLog("Left Prealign Light On.");

                    lineCamera.SetOperationMode(TDIOperationMode.TDI);
                    lineCamera.StartGrab();
                    WriteLog("Start LineScanner Grab.", true);

                    SeqStep = SeqStep.SEQ_MOVE_END_POS;

                    break;
                case SeqStep.SEQ_MOVE_END_POS:
                    
                    if (MoveTo(TeachingPosType.Stage1_Scan_End, out errorMessage) == false)
                        SeqStep = SeqStep.SEQ_ERROR;
                    else
                        SeqStep = SeqStep.SEQ_WAITING_SCAN_COMPLETED;
                    break;

                case SeqStep.SEQ_WAITING_SCAN_COMPLETED:
                    if (IsGrabDone == false)
                        break;

                    WriteLog("Complete LineScanner Grab.", true);
                    
                    LineCameraManager.Instance().Stop("LineCamera");
                    WriteLog("Stop Grab.");

                    LAFManager.Instance().TrackingOnOff("Laf", false);
                    WriteLog("Laf Off.");

                    LastInspSW.Restart();

                    SeqStep = SeqStep.SEQ_WAITING_INSPECTION_DONE;
                    break;

                case SeqStep.SEQ_WAITING_INSPECTION_DONE:
                    
                    if (IsInspectionDone() == false)
                        break;

                    LastInspSW.Stop();
                    AppsInspResult.EndInspTime = DateTime.Now;
                    AppsInspResult.LastInspTime = LastInspSW.ElapsedMilliseconds.ToString();

                    string message = $"Grab End to Insp Completed Time.({LastInspSW.ElapsedMilliseconds.ToString()}ms)";
                    WriteLog(message, true);

                    SeqStep = SeqStep.SEQ_SEND_RESULT;
                    break;
                case SeqStep.SEQ_SEND_RESULT:
                    // Align 결과, Akkon 결과
                    // Ok 이면 + Ng -
                    
                    SeqStep = SeqStep.SEQ_UI_RESULT_UPDATE;
                    break;

                case SeqStep.SEQ_UI_RESULT_UPDATE:

                    GetAkkonResultImage();
                    UpdateDailyInfo(AppsInspResult);
                    WriteLog("Update Inspectinon Result.", true);
                    
                    SystemManager.Instance().UpdateMainResult(AppsInspResult);

                    SeqStep = SeqStep.SEQ_SAVE_RESULT_DATA;
                    break;

                case SeqStep.SEQ_SAVE_RESULT_DATA:

                    DailyInfoService.Save();

                    SaveInspectionResult(AppsInspResult);
                    WriteLog("Save inspection result.");

                    SeqStep = SeqStep.SEQ_SAVE_IMAGE;
                    break;

                case SeqStep.SEQ_SAVE_IMAGE:
                    // Save reuslt images
                    SaveImage(AppsInspResult);
                    WriteLog("Save inspection images.");

                    SeqStep = SeqStep.SEQ_DELETE_DATA;
                    break;

                case SeqStep.SEQ_DELETE_DATA:
                    // Delete result datas
                    WriteLog("Delete the old data");

                    SeqStep = SeqStep.SEQ_CHECK_STANDBY;
                    break;

                case SeqStep.SEQ_CHECK_STANDBY:

                    //if (!AppsMotionManager.Instance().IsMotionInPosition(UnitName.Unit0, AxisHandlerName.Handler0, AxisName.X, TeachingPosType.Standby))
                    //    break;
                    if (ConfigSet.Instance().Operation.VirtualMode)
                        AppsStatus.Instance().IsInspRunnerFlagFromPlc = false;

                    SeqStep = SeqStep.SEQ_INIT;
                    break;

                default:
                    break;
            }
        }

        private string GetCellID()
        {
            string cellId = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PLC_Cell_Id).Value;

            if (cellId == "0" || cellId == null)
                return DateTime.Now.ToString("yyyyMMddHHmmss");
            else
                return cellId;
        }

        private void GetAkkonResultImage()
        {
            Stopwatch sw = new Stopwatch();
            sw.Restart();

            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            var unit = inspModel.GetUnit(UnitName.Unit0);

            for (int i = 0; i < AppsInspResult.TabResultList.Count(); i++)
            {
                var tabResult = AppsInspResult.TabResultList[i];
                Tab tab = unit.GetTab(tabResult.TabNo);

                // Overlay Image
                Mat resultMat = GetResultImage(tabResult.Image, tabResult.AkkonResult.LeadResultList, tab.AkkonParam.AkkonAlgoritmParam);
                ICogImage cogImage = ConvertCogColorImage(resultMat);
                tabResult.AkkonResultImage = cogImage;
                resultMat.Dispose();

                // Resize Image
                Mat resizeMat = MatHelper.Resize(tabResult.Image, tab.AkkonParam.AkkonAlgoritmParam.ImageFilterParam.ResizeRatio);
                tabResult.AkkonInspImage = ConvertCogGrayImage(resizeMat);
                resizeMat.Dispose();
            }

            sw.Stop();
            Console.WriteLine("Get Akkon Result Image : " + sw.ElapsedMilliseconds.ToString() + "ms");
        }

        private CogImage8Grey ConvertCogGrayImage(Mat mat)
        {
            if (mat == null)
                return null;

            int size = mat.Width * mat.Height * mat.NumberOfChannels;
            var cogImage = VisionProImageHelper.CovertImage(mat.DataPointer, mat.Width, mat.Height, mat.Step, ColorFormat.Gray) as CogImage8Grey;
            return cogImage;
        }

        public ICogImage ConvertCogColorImage(Mat mat)
        {
            Mat matR = MatHelper.ColorChannelSprate(mat, MatHelper.ColorChannel.R);
            Mat matG = MatHelper.ColorChannelSprate(mat, MatHelper.ColorChannel.G);
            Mat matB = MatHelper.ColorChannelSprate(mat, MatHelper.ColorChannel.B);

            byte[] dataR = new byte[matR.Width * matR.Height];
            Marshal.Copy(matR.DataPointer, dataR, 0, matR.Width * matR.Height);

            byte[] dataG = new byte[matG.Width * matG.Height];
            Marshal.Copy(matG.DataPointer, dataG, 0, matG.Width * matG.Height);

            byte[] dataB = new byte[matB.Width * matB.Height];
            Marshal.Copy(matB.DataPointer, dataB, 0, matB.Width * matB.Height);

            var cogImage = VisionProImageHelper.CovertImage(dataR, dataG, dataB, matB.Width, matB.Height);

            matR.Dispose();
            matG.Dispose();
            matB.Dispose();

            return cogImage;
        }

        public Mat GetResultImage(Mat mat, List<AkkonLeadResult> leadResultList, AkkonAlgoritmParam AkkonParameters)
        {
            if (mat == null)
                return null;

            Mat resizeMat = new Mat();
            Size newSize = new Size((int)(mat.Width * AkkonParameters.ImageFilterParam.ResizeRatio), (int)(mat.Height * AkkonParameters.ImageFilterParam.ResizeRatio));
            CvInvoke.Resize(mat, resizeMat, newSize);
            Mat colorMat = new Mat();
            CvInvoke.CvtColor(resizeMat, colorMat, ColorConversion.Gray2Bgr);
            resizeMat.Dispose();

            DrawParam autoDrawParam = new DrawParam();
            autoDrawParam.ContainLeadCount = true;

            foreach (var result in leadResultList)
            {
                var lead = result.Roi;
                var startPoint = new Point((int)result.Offset.ToWorldX, (int)result.Offset.ToWorldY);

                Point leftTop = new Point((int)lead.LeftTopX + startPoint.X, (int)lead.LeftTopY + startPoint.Y);
                Point leftBottom = new Point((int)lead.LeftBottomX + startPoint.X, (int)lead.LeftBottomY + startPoint.Y);
                Point rightTop = new Point((int)lead.RightTopX + startPoint.X, (int)lead.RightTopY + startPoint.Y);
                Point rightBottom = new Point((int)lead.RightBottomX + startPoint.X, (int)lead.RightBottomY + startPoint.Y);

                // 향 후 Main 페이지 ROI 보여 달라고 하면 ContainLeadROI = true로 속성 변경
                if (autoDrawParam.ContainLeadROI)
                {
                    CvInvoke.Line(colorMat, leftTop, leftBottom, new MCvScalar(50, 230, 50, 255), 1);
                    CvInvoke.Line(colorMat, leftTop, rightTop, new MCvScalar(50, 230, 50, 255), 1);
                    CvInvoke.Line(colorMat, rightTop, rightBottom, new MCvScalar(50, 230, 50, 255), 1);
                    CvInvoke.Line(colorMat, rightBottom, leftBottom, new MCvScalar(50, 230, 50, 255), 1);
                }

                int blobCount = 0;
                foreach (var blob in result.BlobList)
                {
                    Rectangle rectRect = new Rectangle();
                    rectRect.X = (int)(blob.BoundingRect.X + result.Offset.ToWorldX + result.Offset.X);
                    rectRect.Y = (int)(blob.BoundingRect.Y + result.Offset.ToWorldY + result.Offset.Y);
                    rectRect.Width = blob.BoundingRect.Width;
                    rectRect.Height = blob.BoundingRect.Height;

                    Point center = new Point(rectRect.X + (rectRect.Width / 2), rectRect.Y + (rectRect.Height / 2));
                    int radius = rectRect.Width > rectRect.Height ? rectRect.Width : rectRect.Height;

                    int size = blob.BoundingRect.Width * blob.BoundingRect.Height;
                    //double calcMinArea = AkkonParameters.ResultFilterParam.MinArea_um * AkkonParameters.ResultFilterParam.Resolution_um;
                    //double calcMaxArea = AkkonParameters.ResultFilterParam.MaxArea_um * AkkonParameters.ResultFilterParam.Resolution_um;

                    //if (calcMinArea <= size && size <= calcMaxArea)
                    if(blob.IsAkkonShape)
                    {
                        blobCount++;
                        CvInvoke.Circle(colorMat, center, radius / 2, new MCvScalar(255), 1);
                    }
                    else
                    {
                        //if (AkkonParameters.DrawOption.ContainNG)
                        //    CvInvoke.Circle(colorMat, center, radius / 2, new MCvScalar(0), 1);
                    }
                }

                if (autoDrawParam.ContainLeadCount)
                {
                    string leadIndexString = result.Roi.Index.ToString();
                    string blobCountString = string.Format("[{0}]", blobCount);

                    Point centerPoint = new Point((int)((leftBottom.X + rightBottom.X) / 2.0), leftBottom.Y);

                    int baseLine = 0;
                    Size textSize = CvInvoke.GetTextSize(leadIndexString, FontFace.HersheyComplex, 0.3, 1, ref baseLine);
                    int textX = centerPoint.X - (textSize.Width / 2);
                    int textY = centerPoint.Y + (baseLine / 2);
                    CvInvoke.PutText(colorMat, leadIndexString, new Point(textX, textY + 30), FontFace.HersheyComplex, 0.3, new MCvScalar(50, 230, 50, 255));

                    textSize = CvInvoke.GetTextSize(blobCountString, FontFace.HersheyComplex, 0.3, 1, ref baseLine);
                    textX = centerPoint.X - (textSize.Width / 2);
                    textY = centerPoint.Y + (baseLine / 2);
                    CvInvoke.PutText(colorMat, blobCountString, new Point(textX, textY + 60), FontFace.HersheyComplex, 0.3, new MCvScalar(50, 230, 50, 255));
                }
            }

            return colorMat;
        }

        private void InitializeBuffer()
        {
            string cameraName = "LineCamera";
            var lineCamera = LineCameraManager.Instance().GetLineCamera(cameraName);
            lineCamera.InitGrabSettings();
            InitalizeInspTab(cameraName, lineCamera.TabScanBufferList);
        }

        public void RunVirtual()
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            Tab tab = inspModel.GetUnit(UnitName.Unit0).GetTab(0);

            //Mat tabMatImage = new Mat(@"D:\Tab1.bmp", Emgu.CV.CvEnum.ImreadModes.Grayscale);

            // ICogImage tabCogImage = ConvertCogImage(tabMatImage);
            // MainAlgorithmTool tool = new MainAlgorithmTool();

            //var result = tool.MainRunInspect(tab, tabMatImage, 30.0f, 80.0f);

            // AppsInspResult.TabResultList.Add(result);
        }

        private void SetCoordinateData(CoordinateTransform coordinate, TabInspResult tabInspResult)
        {
            PointF teachedLeftPoint = tabInspResult.MarkResult.FpcMark.FoundedMark.Left.MaxMatchPos.ReferencePos;
            PointF teachedRightPoint = tabInspResult.MarkResult.FpcMark.FoundedMark.Right.MaxMatchPos.ReferencePos;
            PointF searchedLeftPoint = tabInspResult.MarkResult.FpcMark.FoundedMark.Left.MaxMatchPos.FoundPos;
            PointF searchedRightPoint = tabInspResult.MarkResult.FpcMark.FoundedMark.Right.MaxMatchPos.FoundPos;
            coordinate.SetReferenceData(teachedLeftPoint, teachedRightPoint);
            coordinate.SetTargetData(searchedLeftPoint, searchedRightPoint);
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
                akkonRoi.SetLeftTopPoint(newRightTop);
                akkonRoi.SetLeftTopPoint(newLeftBottom);
                akkonRoi.SetLeftTopPoint(newRightBottom);

                newList.Add(akkonRoi);
            }

            return newList;
        }
        #endregion
    }

    public partial class ATTInspRunner
    {
        #region 메서드
        private void SaveImage(AppsInspResult inspResult)
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            DateTime currentTime = inspResult.StartInspTime;

            string month = currentTime.ToString("MM");
            string day = currentTime.ToString("dd");
            string folderPath = inspResult.Cell_ID;

            string path = Path.Combine(ConfigSet.Instance().Path.Result, inspModel.Name, month, day, folderPath);

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            SaveResultImage(path, inspResult.TabResultList);
        }

        private void SaveResultImage(string resultPath, List<TabInspResult> insTabResultList)
        {
            if (ConfigSet.Instance().Operation.VirtualMode)
                return;

            string path = Path.Combine(resultPath, "Orgin");
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            string okExtension = ".bmp";

            if(ConfigSet.Instance().Operation.ExtensionOKImage == ImageExtension.Bmp)
                okExtension = ".bmp";
            else if (ConfigSet.Instance().Operation.ExtensionOKImage == ImageExtension.Jpg)
                okExtension = ".jpg";
            else if (ConfigSet.Instance().Operation.ExtensionOKImage == ImageExtension.Png)
                okExtension = ".png";

            string ngExtension = ".bmp";

            if (ConfigSet.Instance().Operation.ExtensionNGImage == ImageExtension.Bmp)
                ngExtension = ".bmp";
            else if (ConfigSet.Instance().Operation.ExtensionNGImage == ImageExtension.Jpg)
                ngExtension = ".jpg";
            else if (ConfigSet.Instance().Operation.ExtensionNGImage == ImageExtension.Png)
                ngExtension = ".png";


            foreach (var result in insTabResultList)
            {
                if (result.Judgement == Judgment.OK)
                {
                    if(ConfigSet.Instance().Operation.SaveImageOK)
                    {
                        string imageName = "Tab_" + result.TabNo.ToString() +"_OK_" + okExtension;
                        string imagePath = Path.Combine(path, imageName);
                        result.Image.Save(imagePath);
                    }
                }
                else
                {
                    if (ConfigSet.Instance().Operation.SaveImageNG)
                    {
                        string imageName = "Tab_" + result.TabNo.ToString() + "_NG_" + ngExtension;
                        string imagePath = Path.Combine(path, imageName);
                        result.Image.Save(imagePath);
                    }
                }
            }
        }

        private void UpdateDailyInfo(AppsInspResult inspResult)
        {
            var dailyData = new DailyData();
            UpdateAlignDailyInfo(inspResult, ref dailyData);
            UpdateAkkonDailyInfo(inspResult, ref dailyData);

            AddDailyInfo(dailyData);
        }

        private void UpdateAlignDailyInfo(AppsInspResult inspResult, ref DailyData dailyData)
        {
            foreach (var item in inspResult.TabResultList)
            {
                AlignDailyInfo alignInfo = new AlignDailyInfo();

                alignInfo.InspectionTime = inspResult.EndInspTime.ToString("HH:mm:ss");
                alignInfo.PanelID = inspResult.Cell_ID;
                alignInfo.TabNo = item.TabNo;
                alignInfo.Judgement = item.Judgement;
                alignInfo.LX = item.AlignResult.LeftX.ResultValue_pixel;
                alignInfo.LY = item.AlignResult.LeftY.ResultValue_pixel;
                alignInfo.RX = item.AlignResult.RightX.ResultValue_pixel;
                alignInfo.RY = item.AlignResult.RightY.ResultValue_pixel;
                alignInfo.CX = item.AlignResult.CenterX;

                dailyData.AddAlignInfo(alignInfo);
            }
        }

        private void UpdateAkkonDailyInfo(AppsInspResult inspResult, ref DailyData dailyData)
        {
            foreach (var item in inspResult.TabResultList)
            {
                AkkonDailyInfo akkonInfo = new AkkonDailyInfo();

                akkonInfo.InspectionTime = inspResult.EndInspTime.ToString("HH:mm:ss");
                akkonInfo.PanelID = inspResult.Cell_ID;
                akkonInfo.TabNo = item.TabNo;
                akkonInfo.Judgement = item.Judgement;
                //akkonInfo.AvgBlobCount = item.MacronAkkonResult.AvgBlobCount;
                //akkonInfo.AvgLength = item.MacronAkkonResult.AvgLength;
                //akkonInfo.AvgStrength = item.MacronAkkonResult.AvgStrength;
                //akkonInfo.AvgSTD = item.MacronAkkonResult.AvgStd;

                akkonInfo.AvgBlobCount = 10;
                akkonInfo.AvgLength = 10;
                akkonInfo.AvgStrength = 10;
                akkonInfo.AvgSTD = 10;

                dailyData.AddAkkonInfo(akkonInfo);
            }
        }

        private void AddDailyInfo(DailyData dailyData)
        {
            var dailyInfo = DailyInfoService.GetDailyInfo();

            if (dailyInfo == null)
                return;

            dailyInfo.AddDailyDataList(dailyData);
        }

        private void SaveInspectionResult(AppsInspResult inspResult)
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            DateTime currentTime = inspResult.StartInspTime;

            string month = currentTime.ToString("MM");
            string day = currentTime.ToString("dd");
            string folderPath = inspResult.Cell_ID;

            string path = Path.Combine(ConfigSet.Instance().Path.Result, inspModel.Name, month, day);

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            SaveAlignResult(path, inspResult);
            SaveAkkonResult(path, inspResult);
            SaveUPHResult(path, inspResult);
        }

        private void SaveAlignResult(string resultPath, AppsInspResult inspResult)
        {
            string filename = string.Format("Align.csv");
            string csvFile = Path.Combine(resultPath, filename);
            if (File.Exists(csvFile) == false)
            {
                List<string> header = new List<string>
                {
                    "Inspection Time",
                    "Panel ID",
                    "Tab",
                    "Judge",
                    "Lx",
                    "Ly",
                    "Cx",
                    "Rx",
                    "Ry"
                };

                CSVHelper.WriteHeader(csvFile, header);
            }

            List<List<string>> dataList = new List<List<string>>();
            for (int tabNo = 0; tabNo < inspResult.TabResultList.Count; tabNo++)
            {
                List<string> tabData = new List<string>
                {
                    inspResult.EndInspTime.ToString("HH:mm:ss"),                                    // Insp Time
                    inspResult.Cell_ID,                                                             // Panel ID
                    tabNo.ToString(),                                                               // Tab
                    inspResult.TabResultList[tabNo].AlignResult.Judgment.ToString(),                       // Judge
                    inspResult.TabResultList[tabNo].AlignResult.LeftX.ResultValue_pixel.ToString("F3"),          // Left Align X
                    inspResult.TabResultList[tabNo].AlignResult.LeftY.ResultValue_pixel.ToString("F3"),          // Left Align Y
                    inspResult.TabResultList[tabNo].AlignResult.CenterX.ToString("F3"),                         // Center Align X
                    inspResult.TabResultList[tabNo].AlignResult.RightX.ResultValue_pixel.ToString("F3"),         // Right Align X
                    inspResult.TabResultList[tabNo].AlignResult.RightY.ResultValue_pixel.ToString("F3"),         // Right Align Y
                };

                dataList.Add(tabData);
            }

            CSVHelper.WriteData(csvFile, dataList);
        }

        //private void SaveAlignResult(string resultPath, AppsInspResult inspResult)
        //{
        //    string filename = string.Format("Align.csv");
        //    string csvFile = Path.Combine(resultPath, filename);
        //    if (File.Exists(csvFile) == false)
        //    {
        //        List<string> header = new List<string>
        //        {
        //            "Inspection Time",
        //            "Panel ID",
        //        };

        //        for (int tabNo = 0; tabNo < inspResult.TabResultList.Count; tabNo++)
        //        {
        //            header.Add("Tab");
        //            header.Add("Judge");
        //            header.Add("Lx");
        //            header.Add("Ly");
        //            header.Add("Cx");
        //            header.Add("Rx");
        //            header.Add("Ry");
        //        }

        //        CSVHelper.WriteHeader(csvFile, header);
        //    }

        //    List<string> dataList = new List<string>
        //    {
        //        inspResult.EndInspTime.ToString("HH:mm:ss"),
        //        inspResult.Cell_ID.ToString()
        //    };

        //    foreach (var tabResult in inspResult.TabResultList)
        //    {
        //        int tabNo = tabResult.TabNo;
        //        var judge = tabResult.AlignJudgment;
        //        float lx = tabResult.LeftAlignX.ResultValue;
        //        float ly = tabResult.LeftAlignY.ResultValue;
        //        float rx = tabResult.RightAlignX.ResultValue;
        //        float ry = tabResult.RightAlignY.ResultValue;
        //        float cx = (lx + rx) / 2.0f;

        //        dataList.Add(tabNo.ToString());
        //        dataList.Add(judge.ToString());
        //        dataList.Add(lx.ToString("F3"));
        //        dataList.Add(ly.ToString("F3"));
        //        dataList.Add(cx.ToString("F3"));
        //        dataList.Add(rx.ToString("F3"));
        //        dataList.Add(ry.ToString("F3"));
        //    }

        //    CSVHelper.WriteData(csvFile, dataList);
        //}

        private void SaveAkkonResult(string resultPath, AppsInspResult inspResult)
        {
            string filename = string.Format("Akkon.csv");
            string csvFile = Path.Combine(resultPath, filename);
            if (File.Exists(csvFile) == false)
            {
                List<string> header = new List<string>
                {
                    "Inspection Time",
                    "Panel ID",
                    "Tab",
                    "Judge",
                    "Count",
                    "Length",
                    "Strength",
                    "STD"
                };

                CSVHelper.WriteHeader(csvFile, header);
            }

            List<List<string>> dataList = new List<List<string>>();
            for (int tabNo = 0; tabNo < inspResult.TabResultList.Count; tabNo++)
            {
                List<string> tabData = new List<string>
                {
                    inspResult.EndInspTime.ToString("HH:mm:ss"),                                    // Insp Time
                    inspResult.Cell_ID,                                                             // Panel ID
                    tabNo.ToString(),                                                               // Tab
                    inspResult.TabResultList[tabNo].AlignResult.Judgment.ToString(),                       // Judge
                    inspResult.TabResultList[tabNo].AlignResult.LeftX.ResultValue_pixel.ToString("F3"),          // Left Align X
                    inspResult.TabResultList[tabNo].AlignResult.LeftY.ResultValue_pixel.ToString("F3"),          // Left Align Y
                    inspResult.TabResultList[tabNo].AlignResult.CenterX.ToString("F3"),                         // Center Align X
                    inspResult.TabResultList[tabNo].AlignResult.RightX.ResultValue_pixel.ToString("F3"),         // Right Align X
                    inspResult.TabResultList[tabNo].AlignResult.RightY.ResultValue_pixel.ToString("F3"),         // Right Align Y
                };

                dataList.Add(tabData);
            }

            for (int tabNo = 0; tabNo < inspResult.TabResultList.Count; tabNo++)
            {
                List<string> tabData = new List<string>
                {
                    inspResult.EndInspTime.ToString("HH:mm:ss"),
                    inspResult.Cell_ID,
                    tabNo.ToString(),
                    
                    //inspResult.TabResultList[tabNo].MacronAkkonResult.Judgement
                    //inspResult.TabResultList[tabNo].MacronAkkonResult.AvgBlobCount.ToString(),
                    //inspResult.TabResultList[tabNo].MacronAkkonResult.AvgLength.ToString("F3"),
                    //inspResult.TabResultList[tabNo].MacronAkkonResult.AvgStrength.ToString("F3"),

                    "OK",
                    (1 + tabNo).ToString(),             // Count
                    (2.2 + tabNo).ToString("F3"),       // Length
                    (4.4 + tabNo).ToString("F3"),       // Strength
                };

                dataList.Add(tabData);
            }

            CSVHelper.WriteData(csvFile, dataList);
        }

        //private void SaveAkkonResult(string resultPath, AppsInspResult inspResult)
        //{
        //    string filename = string.Format("Akkon.csv");
        //    string csvFile = Path.Combine(resultPath, filename);
        //    if (File.Exists(csvFile) == false)
        //    {
        //        List<string> header = new List<string>
        //        {
        //            "Inspection Time",
        //            "Panel ID",
        //        };

        //        for (int tabNo = 0; tabNo < inspResult.TabResultList.Count; tabNo++)
        //        {
        //            header.Add("Tab");
        //            header.Add("Judge");
        //            header.Add("Count");
        //            header.Add("Length");
        //            header.Add("Strength");
        //            header.Add("STD");
        //        }

        //        CSVHelper.WriteHeader(csvFile, header);
        //    }

        //    List<string> dataList = new List<string>
        //    {
        //        inspResult.EndInspTime.ToString("HH:mm:ss"),
        //        inspResult.Cell_ID.ToString()
        //    };

        //    foreach (var tabResult in inspResult.TabResultList)
        //    {
        //        if(AppsConfig.Instance().AkkonAlgorithmType == AkkonAlgorithmType.Macron)
        //        {
        //            int tabNo = tabResult.TabNo;
        //            var judge = tabResult.MacronAkkonResult.Judgement;
        //            int count = tabResult.MacronAkkonResult.AvgBlobCount;
        //            float length = tabResult.MacronAkkonResult.AvgLength;
        //            float strength = tabResult.MacronAkkonResult.AvgStrength;
        //            float std = tabResult.MacronAkkonResult.AvgStd;
        //            dataList.Add(tabNo.ToString());
        //            dataList.Add(judge.ToString());
        //            dataList.Add(count.ToString());
        //            dataList.Add(length.ToString("F3"));
        //            dataList.Add(strength.ToString("F3"));
        //            dataList.Add(std.ToString("F3"));
        //        }
        //        else
        //        {
        //            int tabNo = tabResult.TabNo;
        //            var judge = "OK";
        //            int count = 10;
        //            float length = 0.0f;
        //            float strength = 0.0f;
        //            float std = 0.0f;

        //            dataList.Add(tabNo.ToString());
        //            dataList.Add(judge.ToString());
        //            dataList.Add(count.ToString());
        //            dataList.Add(length.ToString("F3"));
        //            dataList.Add(strength.ToString("F3"));
        //            dataList.Add(std.ToString("F3"));
        //        }
        //    }

        //    CSVHelper.WriteData(csvFile, dataList);
        //}

        private void SaveUPHResult(string resultPath, AppsInspResult inspResult)
        {
            string filename = string.Format("UPH.csv");
            string csvFile = Path.Combine(resultPath, filename);
            if (File.Exists(csvFile) == false)
            {
                List<string> header = new List<string>
                {
                    "Inspection Time",
                    "Panel ID",
                    "Stage No.",
                    "Tab No.",

                    "Count Min",
                    "Count Avg",
                    "Length Min",
                    "Length Avg",
                    "Strength Min",
                    "Strength Avg",

                    "Left Align X",
                    "Left Align Y",
                    "Center Align X",
                    "Right Align X",
                    "Right Align Y",

                    "ACF Head",
                    "Pre Head",
                    "Main Head",

                    "Judge",
                    "Cause",
                    "Op Judge"
                };

                CSVHelper.WriteHeader(csvFile, header);
            }

            List<List<string>> dataList = new List<List<string>>();
            for (int tabNo = 0; tabNo < inspResult.TabResultList.Count; tabNo++)
            {
                List<string> tabData = new List<string>
                {
                    inspResult.EndInspTime.ToString("HH:mm:ss"),                                    // Insp Time
                    inspResult.Cell_ID,                                                             // Panel ID
                    1.ToString(),                                                                   // Stage
                    tabNo.ToString(),                                                               // Tab

                    //inspResult.TabResultList[tabNo].MacronAkkonResult.AvgBlobCount.ToString(),
                    //inspResult.TabResultList[tabNo].MacronAkkonResult.AvgLength.ToString("F3"),
                    //inspResult.TabResultList[tabNo].MacronAkkonResult.AvgStrength.ToString("F3"),
                    //inspResult.TabResultList[tabNo].MacronAkkonResult.AvgStd.ToString("F3"),
                    (tabNo + 1).ToString(),                                                         // Count Min
                    (tabNo + 2).ToString("F3"),                                                     // Count Avg
                    (tabNo + 3).ToString(),                                                         // Length Min
                    (tabNo + 4).ToString("F3"),                                                     // Length Avg
                    (tabNo + 5).ToString(),                                                         // Strength Min
                    (tabNo + 6).ToString("F3"),                                                     // Strength Avg

                    inspResult.TabResultList[tabNo].AlignResult.LeftX.ResultValue_pixel.ToString("F3"),    // Left Align X
                    inspResult.TabResultList[tabNo].AlignResult.LeftY.ResultValue_pixel.ToString("F3"),    // Left Align Y
                    inspResult.TabResultList[tabNo].AlignResult.CenterX.ToString("F3"),                         // Center Align X
                    inspResult.TabResultList[tabNo].AlignResult.RightX.ResultValue_pixel.ToString("F3"),   // Right Align X
                    inspResult.TabResultList[tabNo].AlignResult.RightY.ResultValue_pixel.ToString("F3"),   // Right Align Y

                    (tabNo + 7).ToString(),                                                         // ACF Head
                    (tabNo + 8).ToString(),                                                         // Pre Head
                    (tabNo + 9).ToString(),                                                         // Main Head

                    inspResult.TabResultList[tabNo].Judgement.ToString(),                           // Judge
                    "Count",                                                                        // Cause
                    "OP_OK"                                                                         // OP Judge
                };

                dataList.Add(tabData);
            }

            CSVHelper.WriteData(csvFile, dataList);
        }

        private string GetExtensionOKImage()
        {
            return "." + ConfigSet.Instance().Operation.ExtensionOKImage;
        }

        private string GetExtensionNGImage()
        {
            return "." + ConfigSet.Instance().Operation.ExtensionNGImage;
        }

        private Axis GetAxis(AxisHandlerName axisHandlerName, AxisName axisName)
        {
            return MotionManager.Instance().GetAxis(axisHandlerName, axisName);
        }

        public bool IsAxisInPosition(UnitName unitName, TeachingPosType teachingPos, Axis axis)
        {
            return MotionManager.Instance().IsAxisInPosition(unitName, teachingPos, axis);
        }

        public bool MoveTo(TeachingPosType teachingPos, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (ConfigSet.Instance().Operation.VirtualMode)
                return true;

            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            var teachingInfo = inspModel.GetUnit(UnitName.Unit0).GetTeachingInfo(teachingPos);

            Axis axisX = GetAxis(AxisHandlerName.Handler0, AxisName.X);
            Axis axisZ = GetAxis(AxisHandlerName.Handler0, AxisName.Z0);

            var movingParamX = teachingInfo.GetMovingParam(AxisName.X.ToString());
            var movingParamZ = teachingInfo.GetMovingParam(AxisName.Z0.ToString());
            
            if (MoveAxis(teachingPos, axisX, movingParamX) == false)
            {
                errorMessage = string.Format("Move To Axis X TimeOut!({0})", movingParamX.MovingTimeOut.ToString());
                WriteLog(errorMessage);
                return false;
            }

            if (MoveAxis(teachingPos, axisZ, movingParamZ) == false)
            {
                errorMessage = string.Format("Move To Axis Z TimeOut!({0})", movingParamZ.MovingTimeOut.ToString());
                Logger.Write(LogType.Seq, errorMessage);
                return false;
            }

            string message = string.Format("Move Completed.(Teaching Pos : {0})", teachingPos.ToString());
            WriteLog(message);

            return true;
        }

        private bool MoveAxis(TeachingPosType teachingPos, Axis axis, AxisMovingParam movingParam)
        {
            MotionManager manager = MotionManager.Instance();
            if (manager.IsAxisInPosition(UnitName.Unit0, teachingPos, axis) == false)
            {
                Stopwatch sw = new Stopwatch();
                sw.Restart();

                manager.StartAbsoluteMove(UnitName.Unit0, teachingPos, axis);

                while (manager.IsAxisInPosition(UnitName.Unit0, teachingPos, axis) == false)
                {
                    if (sw.ElapsedMilliseconds >= movingParam.MovingTimeOut)
                        return false;

                    Thread.Sleep(10);
                }
            }

            return true;
        }

        private ICogImage GetAreaCameraImage(Camera camera)
        {
            camera.GrabOnce();
            byte[] dataArrayRight = camera.GetGrabbedImage();
            Thread.Sleep(50);

            // Right PreAlign Pattern Matching
            var cogImage = VisionProImageHelper.ConvertImage(dataArrayRight, camera.ImageWidth, camera.ImageHeight, camera.ColorFormat);

            return cogImage;
        }

        private void SetMarkMotionPosition(Unit unit, MarkDirection markDirection)
        {
            var preAlignParam = unit.PreAlign.AlignParamList.Where(x => x.Direction == markDirection).FirstOrDefault();

            var motionX = MotionManager.Instance().GetAxis(AxisHandlerName.Handler0, AxisName.X).GetActualPosition();
            var motionY = PlcControlManager.Instance().GetReadPosition(AxisName.Y) / 1000;
            var motionT = PlcControlManager.Instance().GetReadPosition(AxisName.T) / 1000;

            preAlignParam.SetMotionData(motionX, motionY, motionT);
        }

        private void WriteLog(string logMessage, bool isSystemLog = false)
        {
            if (isSystemLog)
                SystemManager.Instance().AddSystemLogMessage(logMessage);

            Logger.Write(LogType.Seq, logMessage);
        }

        public void VirtualGrabDone()
        {
            IsGrabDone = true;
        }
        #endregion
    }

    public enum SeqStep
    {
        SEQ_IDLE,
        SEQ_START,
        SEQ_INIT,
        SEQ_WAITING,
        SEQ_MOVE_START_POS,
        SEQ_VIRTUAL,
        SEQ_SCAN_READY,
        SEQ_SCAN_START,
        SEQ_MOVE_END_POS,
        SEQ_WAITING_SCAN_COMPLETED,
        SEQ_WAITING_INSPECTION_DONE,
        SEQ_SEND_RESULT,
        SEQ_PATTERN_MATCH,
        SEQ_ALIGN_INSPECTION,
        SEQ_ALIGN_INSPECTION_COMPLETED,
        SEQ_AKKON_INSPECTION,
        SEQ_AKKON_INSPECTION_COMPLETED,
        SEQ_UI_RESULT_UPDATE,
        SEQ_SAVE_RESULT_DATA,
        SEQ_SAVE_IMAGE,
        SEQ_DELETE_DATA,
        SEQ_CHECK_STANDBY,
        SEQ_ERROR,
    }

    public class AkkonThreadParam
    {
        public TabInspResult TabInspResult { get; set; } = null;

        public Tab Tab { get; set; } = null;
    }
}
