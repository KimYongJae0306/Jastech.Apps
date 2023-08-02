using ATT_UT_Remodeling.Core.AppTask;
using ATT_UT_Remodeling.Core.Data;
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
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Device.LightCtrls;
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
        private const int SAVE_IMAGE_MAX_WIDTH = 65000;

        private Axis _axis { get; set; } = null;

        private object _akkonLock = new object();

        private object _inspLock = new object();

        private Thread _deleteThread { get; set; } = null;
        #endregion

        #region 속성
        private LineCamera LineCamera { get; set; } = null;

        private AreaCamera AreaCamera { get; set; } = null;

        private LAFCtrl LAFCtrl { get; set; } = null;

        private LightCtrlHandler LightCtrlHandler { get; set; } = null;

        private Task SeqTask { get; set; }

        private CancellationTokenSource SeqTaskCancellationTokenSource { get; set; }

        private SeqStep SeqStep { get; set; } = SeqStep.SEQ_IDLE;

        private bool IsGrabDone { get; set; } = false;

        private Stopwatch LastInspSW { get; set; } = new Stopwatch();

        public InspProcessTask InspProcessTask { get; set; } = new InspProcessTask();
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public ATTInspRunner()
        {
        }
        #endregion

        #region 메서드
        public void SetVirtualmage(int tabNo, string fileName)
        {
            InspProcessTask.VirtualQueue.Enqueue(new VirtualData
                {
                TabNo = tabNo,
                FilePath = fileName,
            });
        }

        public void StartVirtualMode()
        {
            InspProcessTask.StartVirtual();
        }


        private void ATTSeqRunner_GrabDoneEventHandler(string cameraName, bool isGrabDone)
        {
            IsGrabDone = isGrabDone;

            if(IsGrabDone)
            {
                LineCamera.StopGrab();
                LAFCtrl.SetTrackingOnOFF(false);

                WriteLog("Received Camera Grab Done Event.");
            }
        }

        public void StartSeqTask()
        {
            if (SeqTask != null)
                return;

            SeqTaskCancellationTokenSource = new CancellationTokenSource();
            SeqTask = new Task(SeqTaskAction, SeqTaskCancellationTokenSource.Token);
            SeqTask.Start();
        }

        public void StopSeqTask()
        {
            if (SeqTask != null)
            {
                SeqTaskCancellationTokenSource.Cancel();
                SeqTask.Wait();
                SeqTask = null;
            }
        }
      
        public bool IsInspectionDone()
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            if (InspProcessTask.InspCount == inspModel.TabCount)
                return true;

            return false;
        }
        
        public void Initialize()
        {
            LineCamera = LineCameraManager.Instance().GetLineCamera("LineCamera");
            LineCamera.GrabDoneEventHandler += ATTSeqRunner_GrabDoneEventHandler;

            LAFCtrl = LAFManager.Instance().GetLAFCtrl("Laf");
            LightCtrlHandler = DeviceManager.Instance().LightCtrlHandler;
            AreaCamera = AreaCameraManager.Instance().GetAppsCamera("PreAlign");

            InspProcessTask.StartTask();
            StartSeqTask();
        }

        public void Release()
        {
            StopDevice();

            InspProcessTask.StopTask();
            StopSeqTask();
        }

        private void StopDevice()
        {
            LightCtrlHandler.TurnOff();

            LAFCtrl.SetTrackingOnOFF(false);

            WriteLog("AutoFocus Off.");

            LineCamera.StopGrab();
            LineCamera.GrabDoneEventHandler -= ATTSeqRunner_GrabDoneEventHandler;
            LineCamera.StopGrab();
            WriteLog("LinceCamera Stop Grab.");

            AreaCamera.StopGrab();
            WriteLog("PreAlignCamera Stop Grab.");
        }

        public void SeqRun()
        {
            if (ModelManager.Instance().CurrentModel == null)
                return;

            SystemManager.Instance().MachineStatus = MachineStatus.RUN;
            SeqStep = SeqStep.SEQ_INIT;

            WriteLog("Start Sequence.");
        }

        public void SeqStop()
        {
            SystemManager.Instance().MachineStatus = MachineStatus.STOP;
            SeqStep = SeqStep.SEQ_IDLE;

            WriteLog("Stop Sequence.");
        }

        private void SeqTaskAction()
        {
            var cancellationToken = SeqTaskCancellationTokenSource.Token;
            cancellationToken.ThrowIfCancellationRequested();
            SeqStep = SeqStep.SEQ_IDLE;

            while (true)
            {
                // 작업 취소
                if (cancellationToken.IsCancellationRequested)
                {
                    StopDevice();
                    InspProcessTask.DisposeInspTabList();

                    SeqStep = SeqStep.SEQ_IDLE;
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

            string systemLogMessage = string.Empty;
            string errorMessage = string.Empty;

            switch (SeqStep)
            {
                case SeqStep.SEQ_IDLE:
                    break;

                case SeqStep.SEQ_INIT:
                    LineCamera.IsLive = false;
                    LineCamera.StopGrab();
                    WriteLog("Stop Grab.");

                    LightCtrlHandler.TurnOff();
                    WriteLog("Light Off.");

                    LAFCtrl.SetTrackingOnOFF(false);
                    LAFCtrl.SetMotionAbsoluteMove(0);
                    WriteLog("Laf Off.");

                    SeqStep = SeqStep.SEQ_WAITING;
                    break;

                case SeqStep.SEQ_WAITING:
                    if (AppsStatus.Instance().IsInspRunnerFlagFromPlc == false)
                        break;

                    WriteLog("Receive Inspection Start Signal From PLC.", true);

                    AppsInspResult.Instance().ClearResult();
                    WriteLog("Clear Result.");

                    SystemManager.Instance().TabButtonResetColor();

                    InitializeBuffer();
                    WriteLog("Initialize Buffer.");

                    AppsInspResult.Instance().StartInspTime = DateTime.Now;
                    AppsInspResult.Instance().Cell_ID = GetCellID();

                    WriteLog("Cell ID : " + AppsInspResult.Instance().Cell_ID, true);

                    SeqStep = SeqStep.SEQ_MOVE_START_POS;
                    break;

                case SeqStep.SEQ_MOVE_START_POS:
                    if (MoveTo(TeachingPosType.Stage1_Scan_Start, out errorMessage) == false)
                        break;

                    SeqStep = SeqStep.SEQ_SCAN_START;
                    break;

                case SeqStep.SEQ_SCAN_START:
                    IsGrabDone = false;

                    LAFCtrl.SetTrackingOnOFF(true);
                    WriteLog("Laser Auto Focus On.");

                    LightCtrlHandler.TurnOn(unit.GetLineCameraData("Akkon").LightParam);
                    Thread.Sleep(100);
                    WriteLog("Light On.");

                    LineCamera.SetOperationMode(TDIOperationMode.TDI);
                    LineCamera.StartGrab();
                    WriteLog("Start LineScanner Grab.", true);

                    if (ConfigSet.Instance().Operation.VirtualMode)
                    {
                        InspProcessTask.StartVirtual();
                        IsGrabDone = true;
                    }

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

                    LastInspSW.Restart();

                    SeqStep = SeqStep.SEQ_WAITING_INSPECTION_DONE;
                    break;

                case SeqStep.SEQ_WAITING_INSPECTION_DONE:
                    if (IsInspectionDone() == false)
                        break;

                    LastInspSW.Stop();
                    AppsInspResult.Instance().EndInspTime = DateTime.Now;
                    AppsInspResult.Instance().LastInspTime = LastInspSW.ElapsedMilliseconds.ToString();

                    string message = $"Grab End to Insp Completed Time.({LastInspSW.ElapsedMilliseconds.ToString()}ms)";
                    WriteLog(message, true);

                    SeqStep = SeqStep.SEQ_MANUAL_CHECK;
                    break;

                case SeqStep.SEQ_MANUAL_CHECK:

                    SeqStep = SeqStep.SEQ_SEND_RESULT;
                    break;

                case SeqStep.SEQ_SEND_RESULT:
                    // Align 결과, Akkon 결과
                    // Ok 이면 + Ng -
                    SendResultData();
                    WriteLog("Completed Send Plc Tab Result Data", true);

                    SeqStep = SeqStep.SEQ_WAIT_UI_RESULT_UPDATE;
                    break;

                case SeqStep.SEQ_WAIT_UI_RESULT_UPDATE:
                    GetAkkonResultImage();
                    UpdateDailyInfo();

                    SystemManager.Instance().UpdateMainResult();
                    WriteLog("Update Inspectinon Result.", true);

                    SeqStep = SeqStep.SEQ_SAVE_RESULT_DATA;
                    break;

                case SeqStep.SEQ_SAVE_RESULT_DATA:
                    DailyInfoService.Save(inspModel.Name);
                    SaveInspResultCSV();
                    WriteLog("Save inspection result.");

                    SeqStep = SeqStep.SEQ_SAVE_IMAGE;
                    break;

                case SeqStep.SEQ_SAVE_IMAGE:

                    SaveImage();
                    WriteLog("Save inspection images.");

                    SeqStep = SeqStep.SEQ_DELETE_DATA;
                    break;

                case SeqStep.SEQ_DELETE_DATA:

                    StartDeleteData();
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

        private void GetAkkonResultImage()
        {
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            for (int tabNo = 0; tabNo < inspModel.TabCount; tabNo++)
            {
                var tabResult = AppsInspResult.Instance().Get(tabNo);

                if (tabResult != null)
                {
                    if (tabResult.MarkResult.Judgement == Judgement.OK)
                    {
                        Stopwatch sw = new Stopwatch();
                        sw.Restart();

                        var tab = inspModel.GetUnit(UnitName.Unit0).GetTab(tabNo);

                        // Overlay Image
                        Mat resultMat = GetResultImage(tabResult.AkkonInspMatImage, tabResult.AkkonResult.LeadResultList, tab.AkkonParam.AkkonAlgoritmParam);
                        ICogImage cogImage = ConvertCogColorImage(resultMat);
                        tabResult.AkkonResultCogImage = cogImage;
                        resultMat.Dispose();
                        
                        // AkkonInspCogImage
                        tabResult.AkkonInspCogImage = ConvertCogGrayImage(tabResult.AkkonInspMatImage);

                        sw.Stop();
                        Console.WriteLine(string.Format("Get Akkon Result Image_Tab{0} : {1}ms", tabNo, sw.ElapsedMilliseconds.ToString()));
                    }
                }
                else
                {
                    Console.WriteLine(string.Format("Get Akkon Result Image_Tab{0} Fail.", tabNo));
                }
            }
        }

        private void SendResultData()
        {
            bool isManualOK = false;

            double resolution = LineCamera.Camera.PixelResolution_um / LineCamera.Camera.LensScale;

            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            for (int tabNo = 0; tabNo < inspModel.TabCount; tabNo++)
            {
                var tabInspResult = AppsInspResult.Instance().Get(tabNo);
                PlcControlManager.Instance().WriteTabResult(tabInspResult, resolution, isManualOK);

                Thread.Sleep(10);
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

        private void InitializeBuffer()
        {
            string cameraName = "LineCamera";
            var lineCamera = LineCameraManager.Instance().GetLineCamera(cameraName);
            lineCamera.InitGrabSettings();
            InspProcessTask.InitalizeInspBuffer(cameraName, lineCamera.TabScanBufferList);
        }

        public void RunVirtual()
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            Tab tab = inspModel.GetUnit(UnitName.Unit0).GetTab(0);
        }

        public void StartDeleteData()
        {
            if (_deleteThread == null)
            {
                _deleteThread = new Thread(DeleteData);
                _deleteThread.Start();
            }
        }
        #endregion
    }

    public partial class ATTInspRunner
    {
        #region 메서드
        private void UpdateDailyInfo()
        {
            var dailyInfo = DailyInfoService.GetDailyInfo();
            if (dailyInfo == null)
                return;

            var dailyData = new DailyData();

            UpdateAlignDailyInfo(ref dailyData);
            UpdateAkkonDailyInfo(ref dailyData);

            dailyInfo.AddDailyDataList(dailyData);
        }

        private void UpdateAlignDailyInfo(ref DailyData dailyData)
        {
            int tabCount = (ModelManager.Instance().CurrentModel as AppsInspModel).TabCount;

            for (int tabNo = 0; tabNo < tabCount; tabNo++)
            {
                AlignDailyInfo alignInfo = new AlignDailyInfo();

                var tabInspResult = AppsInspResult.Instance().Get(tabNo);

                alignInfo.InspectionTime = AppsInspResult.Instance().EndInspTime.ToString("HH:mm:ss");
                alignInfo.PanelID = AppsInspResult.Instance().Cell_ID;
                alignInfo.TabNo = tabInspResult.TabNo;
                alignInfo.Judgement = tabInspResult.Judgement;
                alignInfo.LX = GetResultAlignResultValue(tabInspResult.AlignResult.LeftX);
                alignInfo.LY = GetResultAlignResultValue(tabInspResult.AlignResult.LeftY);
                alignInfo.RX = GetResultAlignResultValue(tabInspResult.AlignResult.RightX);
                alignInfo.RY = GetResultAlignResultValue(tabInspResult.AlignResult.RightY);
                alignInfo.CX = tabInspResult.AlignResult.CenterX;

                dailyData.AddAlignInfo(alignInfo);
            }
        }

        private float GetResultAlignResultValue(AlignResult alignResult)
        {
            if (alignResult == null)
                return 0.0F;
            else
                return alignResult.ResultValue_pixel;
        }

        private void UpdateAkkonDailyInfo(ref DailyData dailyData)
        {
            int tabCount = (ModelManager.Instance().CurrentModel as AppsInspModel).TabCount;

            for (int tabNo = 0; tabNo < tabCount; tabNo++)
            {
                AkkonDailyInfo akkonInfo = new AkkonDailyInfo();

                var tabInspResult = AppsInspResult.Instance().Get(tabNo);
                var akkonResult = tabInspResult.AkkonResult;

                akkonInfo.InspectionTime = AppsInspResult.Instance().EndInspTime.ToString("HH:mm:ss");
                akkonInfo.PanelID = AppsInspResult.Instance().Cell_ID;
                akkonInfo.TabNo = tabInspResult.TabNo;

                int minCount = 0;
                float minLength = 0.0F;
                if (akkonResult != null)
                {
                    akkonInfo.Judgement = akkonResult.Judgement;
                    minCount = akkonResult.LeftCount_Avg > akkonResult.RightCount_Min ? akkonResult.RightCount_Min : akkonResult.LeftCount_Avg;
                    minLength = akkonResult.Length_Left_Min_um > akkonResult.Length_Right_Min_um ? akkonResult.Length_Right_Min_um : akkonResult.Length_Left_Min_um;
                }

                akkonInfo.MinBlobCount = minCount;
                akkonInfo.MinLength = minLength;

                dailyData.AddAkkonInfo(akkonInfo);
            }
        }

        private void SaveInspResultCSV()
        {
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            DateTime currentTime = AppsInspResult.Instance().StartInspTime;

            string month = currentTime.ToString("MM");
            string day = currentTime.ToString("dd");
            string folderPath = AppsInspResult.Instance().Cell_ID;

            string path = Path.Combine(ConfigSet.Instance().Path.Result, inspModel.Name, month, day);

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            SaveAlignResult(path, inspModel.TabCount);
            SaveAkkonResult(path, inspModel.TabCount);
            SaveUPHResult(path, inspModel.TabCount);
        }

        private void SaveAlignResult(string resultPath, int tabCount)
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
            for (int tabNo = 0; tabNo < tabCount; tabNo++)
            {
                var tabInspResult = AppsInspResult.Instance().Get(tabNo);
                var alignResult = tabInspResult.AlignResult;
                Judgement judgement = alignResult.Judgement;

                List<string> tabData = new List<string>
                {
                    AppsInspResult.Instance().EndInspTime.ToString("HH:mm:ss"),                                    // Insp Time
                    AppsInspResult.Instance().Cell_ID,                                                             // Panel ID
                    (tabInspResult.TabNo + 1).ToString(),                                                               // Tab
                    judgement.ToString(),                       // Judge
                    CheckResultValue(alignResult.LeftX).ToString("F4"),          // Left Align X
                    CheckResultValue(alignResult.LeftY).ToString("F4"),          // Left Align Y
                    alignResult.CenterX.ToString("F4"),                         // Center Align X
                    CheckResultValue(alignResult.RightX).ToString("F4"),         // Right Align X
                    CheckResultValue(alignResult.RightY).ToString("F4"),         // Right Align Y     // Right Align Y
                };

                dataList.Add(tabData);
            }

            CSVHelper.WriteData(csvFile, dataList);
        }

        private void SaveAkkonResult(string resultPath, int tabCount)
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
                    "Avg Count",
                    "Avg Length",
                };

                CSVHelper.WriteHeader(csvFile, header);
            }

            List<List<string>> dataList = new List<List<string>>();
            for (int tabNo = 0; tabNo < tabCount; tabNo++)
            {
                var tabInspResult = AppsInspResult.Instance().Get(tabNo);
                var akkonResult = tabInspResult.AkkonResult;

                Judgement judgement = akkonResult.Judgement;
                int avgCount = (akkonResult.LeftCount_Avg + akkonResult.RightCount_Avg) / 2;
                float avgLength = (akkonResult.Length_Left_Avg_um + akkonResult.Length_Right_Avg_um) / 2.0F;

                List<string> tabData = new List<string>
                {
                    AppsInspResult.Instance().EndInspTime.ToString("HH:mm:ss"),
                    AppsInspResult.Instance().Cell_ID,
                    (tabInspResult.TabNo + 1).ToString(),
                    judgement.ToString(),
                    avgCount.ToString(),
                    avgLength.ToString("F4"),
                };

                dataList.Add(tabData);

            }
            CSVHelper.WriteData(csvFile, dataList);
        }

        private void SaveUPHResult(string resultPath, int tabCount)
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
            for (int tabNo = 0; tabNo < tabCount; tabNo++)
            {
                var tabInspResult = AppsInspResult.Instance().Get(tabNo);
                var alignResult = tabInspResult.AlignResult;

                List<string> tabData = new List<string>
                {
                    AppsInspResult.Instance().EndInspTime.ToString("HH:mm:ss"),                                    // Insp Time
                    AppsInspResult.Instance().Cell_ID,                                                             // Panel ID
                    1.ToString(),                                                                   // Stage
                    (tabInspResult.TabNo + 1).ToString(),                                                               // Tab

                    //inspResult.TabResultList[tabNo].MacronAkkonResult.AvgBlobCount.ToString(),
                    //inspResult.TabResultList[tabNo].MacronAkkonResult.AvgLength.ToString("F3"),
                    //inspResult.TabResultList[tabNo].MacronAkkonResult.AvgStrength.ToString("F3"),
                    //inspResult.TabResultList[tabNo].MacronAkkonResult.AvgStd.ToString("F3"),
                    (tabNo + 1).ToString(),                                                         // Count Min
                    (tabNo + 2).ToString("F4"),                                                     // Count Avg
                    (tabNo + 3).ToString(),                                                         // Length Min
                    (tabNo + 4).ToString("F4"),                                                     // Length Avg
                    (tabNo + 5).ToString(),                                                         // Strength Min
                    (tabNo + 6).ToString("F4"),                                                     // Strength Avg

                    CheckResultValue(alignResult.LeftX).ToString("F4"),    // Left Align X
                    CheckResultValue(alignResult.LeftY).ToString("F4"),    // Left Align Y
                    alignResult.CenterX.ToString("F4"),                         // Center Align X
                    CheckResultValue(alignResult.RightX).ToString("F4"),   // Right Align X
                    CheckResultValue(alignResult.RightY).ToString("F4"),   // Right Align Y

                    (tabNo + 7).ToString(),                                                         // ACF Head
                    (tabNo + 8).ToString(),                                                         // Pre Head
                    (tabNo + 9).ToString(),                                                         // Main Head

                    tabInspResult.Judgement.ToString(),                           // Judge
                    "Count",                                                                        // Cause
                    "OP_OK"                                                                         // OP Judge
                };

                dataList.Add(tabData);
            }
            CSVHelper.WriteData(csvFile, dataList);
        }

        private float CheckResultValue(AlignResult alignResult)
        {
            if (alignResult == null)
                return 0.0F;
            else
                return alignResult.ResultValue_pixel;
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

        public Mat GetResultImage(Mat resizeMat, List<AkkonLeadResult> leadResultList, AkkonAlgoritmParam AkkonParameters)
        {
            if (resizeMat == null)
                return null;

            Mat colorMat = new Mat();
            CvInvoke.CvtColor(resizeMat, colorMat, ColorConversion.Gray2Bgr);

            MCvScalar redColor = new MCvScalar(50, 50, 230, 255);
            MCvScalar greenColor = new MCvScalar(50, 230, 50, 255);

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
                if (result.Judgement == Judgement.NG)
                {
                    CvInvoke.Line(colorMat, leftTop, leftBottom, redColor, 3);
                    CvInvoke.Line(colorMat, leftTop, rightTop, redColor, 3);
                    CvInvoke.Line(colorMat, rightTop, rightBottom, redColor, 3);
                    CvInvoke.Line(colorMat, rightBottom, leftBottom, redColor, 3);
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

                    if (blob.IsAkkonShape)
                    {
                        blobCount++;
                        CvInvoke.Circle(colorMat, center, radius / 2, new MCvScalar(255), 1);
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

        public ICogImage ConvertCogColorImage(Mat mat)
        {
            Mat matR = MatHelper.ColorChannelSeperate(mat, MatHelper.ColorChannel.R);
            Mat matG = MatHelper.ColorChannelSeperate(mat, MatHelper.ColorChannel.G);
            Mat matB = MatHelper.ColorChannelSeperate(mat, MatHelper.ColorChannel.B);

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

        private CogImage8Grey ConvertCogGrayImage(Mat mat)
        {
            if (mat == null)
                return null;

            int size = mat.Width * mat.Height * mat.NumberOfChannels;
            var cogImage = VisionProImageHelper.CovertImage(mat.DataPointer, mat.Width, mat.Height, mat.Step, ColorFormat.Gray) as CogImage8Grey;
            return cogImage;
        }

        private void SaveImage()
        {
            if (ConfigSet.Instance().Operation.VirtualMode)
                return;

            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            Stopwatch sw = new Stopwatch();
            sw.Restart();

            for (int tabNo = 0; tabNo < inspModel.TabCount; tabNo++)
            {
                DateTime currentTime = AppsInspResult.Instance().StartInspTime;

                string month = currentTime.ToString("MM");
                string day = currentTime.ToString("dd");
                string folderPath = AppsInspResult.Instance().Cell_ID;

                string path = Path.Combine(ConfigSet.Instance().Path.Result, inspModel.Name, month, day, folderPath);

                SaveResultImage(path, tabNo);
            }

            sw.Stop();
            Console.WriteLine("Save Image : " + sw.ElapsedMilliseconds.ToString() + "ms");
        }

        private void SaveResultImage(string resultPath, int tabNo)
        {
            var tabInspResult = AppsInspResult.Instance().Get(tabNo);
            var operation = ConfigSet.Instance().Operation;

            if (Directory.Exists(resultPath) == false)
                Directory.CreateDirectory(resultPath);

            string okExtension = operation.GetExtensionOKImage();
            string ngExtension = operation.GetExtensionNGImage();

            if (tabInspResult.Judgement == Judgement.OK)
            {
                if (ConfigSet.Instance().Operation.SaveImageOK)
                {
                    string imageName = "Tab_" + tabInspResult.TabNo.ToString();
                    string filePath = Path.Combine(resultPath, imageName);
                    
                    if (operation.ExtensionOKImage == ImageExtension.Bmp)
                    {
                        SaveImage(tabInspResult.Image, filePath, Judgement.OK, ImageExtension.Bmp, false);
                    }
                    else if(operation.ExtensionOKImage == ImageExtension.Jpg)
                    {
                        if (tabInspResult.Image.Width > SAVE_IMAGE_MAX_WIDTH)
                            SaveImage(tabInspResult.Image, filePath, Judgement.OK, ImageExtension.Jpg, true);
                        else
                            SaveImage(tabInspResult.Image, filePath, Judgement.OK, ImageExtension.Jpg, false);
                    }
                }
            }
            else
            {
                if (ConfigSet.Instance().Operation.SaveImageNG)
                {
                    string imageName = "Tab_" + tabInspResult.TabNo.ToString();
                    string filePath = Path.Combine(resultPath, imageName);
                    if (operation.ExtensionNGImage == ImageExtension.Bmp)
                    {
                        SaveImage(tabInspResult.Image, filePath, Judgement.NG, ImageExtension.Bmp, false);
                    }
                    else if (operation.ExtensionNGImage == ImageExtension.Jpg)
                    {
                        if (tabInspResult.Image.Width > SAVE_IMAGE_MAX_WIDTH)
                            SaveImage(tabInspResult.Image, filePath, Judgement.NG, ImageExtension.Jpg, true);
                        else
                            SaveImage(tabInspResult.Image, filePath, Judgement.NG, ImageExtension.Jpg, false);
                    }
                }
            }
        }

        private void SaveImage(Mat image, string filePath, Judgement judgement, ImageExtension extension, bool isHalfSave)
        {
            if(extension == ImageExtension.Bmp)
            {
                filePath += string.Format("_{0}.bmp", judgement.ToString());
                image.Save(filePath);
            }
            else if(extension == ImageExtension.Jpg)
            {
                if(isHalfSave)
                {
                    string leftPath = filePath + string.Format("_{0}_Left.jpg", judgement.ToString());
                    string rightPath = filePath + string.Format("_{0}_Right.jpg", judgement.ToString());

                    int half = image.Width / 2;
                    Rectangle leftRect = new Rectangle(0, 0, half, image.Height);
                    Rectangle rightRect = new Rectangle(half, 0, image.Width - half, image.Height);

                    Mat leftMat = new Mat(image, leftRect);
                    Mat rightMat = new Mat(image, rightRect);

                    leftMat.Save(leftPath);
                    rightMat.Save(rightPath);

                    leftMat.Dispose();
                    rightMat.Dispose();
                }
                else
                {
                    filePath += string.Format("_{0}.jpg", judgement.ToString());
                    image.Save(filePath);
                }
            }
        }

        private void DeleteData()
        {
            try
            {
                var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
                if (inspModel == null)
                {
                    _deleteThread = null;
                    return;
                }

                string resultPath = ConfigSet.Instance().Path.Result;
                string logPath = ConfigSet.Instance().Path.Log;

                int duration = ConfigSet.Instance().Operation.DataStoringDuration;
                FileHelper.DeleteFilesInDirectory(resultPath, ".*", duration);
                FileHelper.DeleteFilesInDirectory(logPath, ".*", duration);

                _deleteThread = null;
            }
            catch (Exception err)
            {
                Logger.Error(ErrorType.Etc, "Delete Data Error : " + err.Message);
                _deleteThread = null;
            }
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
        SEQ_INIT,
        SEQ_WAITING,
        SEQ_MOVE_START_POS,
        SEQ_SCAN_START,
        SEQ_MOVE_END_POS,
        SEQ_WAITING_SCAN_COMPLETED,
        SEQ_WAITING_INSPECTION_DONE,
        SEQ_MANUAL_CHECK,
        SEQ_SEND_RESULT,
        SEQ_WAIT_UI_RESULT_UPDATE,
        SEQ_SAVE_RESULT_DATA,
        SEQ_SAVE_IMAGE,
        SEQ_DELETE_DATA,
        SEQ_CHECK_STANDBY,
        SEQ_ERROR,
    }
}
