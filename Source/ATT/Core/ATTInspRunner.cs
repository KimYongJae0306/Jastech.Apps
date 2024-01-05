using ATT.Core.AppTask;
using ATT.Core.Data;
using Cognex.VisionPro;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Service;
using Jastech.Apps.Winform.Service.Plc.Maps;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Algorithms.Akkon.Parameters;
using Jastech.Framework.Config;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Device.LightCtrls;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro;
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

namespace ATT.Core
{
    public partial class ATTInspRunner
    {
        #region 필드
        private const int SAVE_IMAGE_MAX_WIDTH = 65000;

        private Axis _axis { get; set; } = null;

        private object _akkonLock = new object();

        private object _inspLock = new object();

        private Thread _deleteThread { get; set; } = null;

        private Thread _ClearBufferThread { get; set; } = null;

        private Thread _saveThread { get; set; } = null;

        private Thread _updateThread { get; set; } = null;
        #endregion

        #region 속성
        private LineCamera LineCamera { get; set; } = null;

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
             if(LineCamera.Camera.Name == cameraName)
            {
                IsGrabDone = isGrabDone;

                if(IsGrabDone == false)
                {
                    LineCamera.StopGrab();
                    LAFCtrl.SetTrackingOnOFF(false);

                    WriteLog("Received Akkon Camera Grab Done Event.");
                }
                    
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

        public void StartUpdateThread()
        {
            if(_updateThread == null)
            {
                _updateThread = new Thread(UpdateUI);
                _updateThread.Start();
            }
        }

        private void UpdateUI()
        {
            StartSaveThread();

            GetAkkonResultImage();
            WriteLog("Make Akkon ResultImage.", true);

            //StartSaveThread();        // 상위로.. 택 테스트
            UpdateDailyInfo();

            SystemManager.Instance().UpdateMainAkkonResult();
            SystemManager.Instance().UpdateMainAlignResult();

            AppsStatus.Instance().IsInspRunnerFlagFromPlc = false;
            SystemManager.Instance().EnableMainView(true);

            _updateThread = null;

            WriteLog("Update UI Inspection Result.", true);
        }

        public bool IsInspAkkonDone()
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            if (InspProcessTask.InspAkkonCount == inspModel.TabCount)
                return true;

            return false;
        }

        public bool IsInspAlignDone()
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            if (InspProcessTask.InspAlignCount == inspModel.TabCount)
                return true;

            return false;
        }

        public void Initialize()
        {
            LineCamera = LineCameraManager.Instance().GetLineCamera("LineCamera");
            LineCamera.GrabDoneEventHandler += ATTSeqRunner_GrabDoneEventHandler;

            LAFCtrl = LAFManager.Instance().GetLAF("Laf").LafCtrl;
            LightCtrlHandler = DeviceManager.Instance().LightCtrlHandler;

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

            LineCamera.GrabDoneEventHandler -= ATTSeqRunner_GrabDoneEventHandler;
            LineCamera.StopGrab();
            WriteLog("AkkonCamera Stop Grab.");
        }

        public void SeqRun()
        {
            if (ModelManager.Instance().CurrentModel == null)
                return;

            PlcControlManager.Instance().MachineStatus = MachineStatus.RUN;
            SeqStep = SeqStep.SEQ_INIT;

            WriteLog("Start Sequence.");
        }

        public void SeqStop()
        {
            PlcControlManager.Instance().MachineStatus = MachineStatus.STOP;
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
                    InspProcessTask.DisposeInspAkkonTabList();
                    InspProcessTask.DisposeInspAlignTabList();

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

            Stopwatch sw = new Stopwatch();
            switch (SeqStep)
            {
                case SeqStep.SEQ_IDLE:
                    AppsStatus.Instance().IsInspRunnerFlagFromPlc = false;
                    PlcControlManager.Instance().EnableSendPeriodically = true;
                    break;

                case SeqStep.SEQ_INIT:
                    ClearBufferThread();

                    SeqStep = SeqStep.SEQ_MOVE_START_POS;
                    break;

                case SeqStep.SEQ_MOVE_START_POS:
               
                    MotionManager.Instance().MoveAxisZ(UnitName.Unit0, TeachingPosType.Stage1_Scan_Start, LAFCtrl, AxisName.Z0);

                    if (_ClearBufferThread != null || _updateThread != null)
                        break;

                    if (MoveTo(TeachingPosType.Stage1_Scan_Start, out errorMessage) == false)
                        break;

                    PlcControlManager.Instance().WritePcStatus(PlcCommand.Move_ScanStartPos);
                    WriteLog("Send Scan Start Position", true);

                    WriteLog("Wait Inspection Start Signal From PLC.", true);
                    SeqStep = SeqStep.SEQ_WAITING;
                    break;

                case SeqStep.SEQ_WAITING:
                    if (AppsStatus.Instance().IsInspRunnerFlagFromPlc == false)
                        break;
                    SystemManager.Instance().EnableMainView(false);
                    SystemManager.Instance().TabButtonResetColor();

                    WriteLog("Receive Inspection Start Signal From PLC.", true);

                    LAFCtrl.SetTrackingOnOFF(true);
                    WriteLog("LAF Tracking ON.");

                    WriteLog("Align LAF Tracking ON.");

                    Thread.Sleep(300);

                    SeqStep = SeqStep.SEQ_BUFFER_INIT;
                    break;

                case SeqStep.SEQ_BUFFER_INIT:
                    InitializeBuffer();
                    WriteLog("Initialize Buffer.");

                    AppsInspResult.Instance().ClearResult();
                    WriteLog("Clear Result.");

                    AppsInspResult.Instance().StartInspTime = DateTime.Now;
                    AppsInspResult.Instance().Cell_ID = GetCellID();

                    WriteLog("Cell ID : " + AppsInspResult.Instance().Cell_ID, true);
                    SeqStep = SeqStep.SEQ_SCAN_START;
                    break;

                case SeqStep.SEQ_SCAN_START:
                    IsGrabDone = false;


                    if(unit.LightParam != null)
                    {
                        LightCtrlHandler?.TurnOn(unit.LightParam);
                        WriteLog("Light Turn On.", true);
                    }

                    LineCamera.StartGrab();
                   
                    WriteLog("Start Akkon LineScanner Grab.", true);

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
                        SeqStep = SeqStep.SEQ_WAITING_AKKON_SCAN_COMPLETED;
                    break;

                case SeqStep.SEQ_WAITING_AKKON_SCAN_COMPLETED:
                    if (IsGrabDone == false)
                        break;

                    WriteLog("Complete Akkon LineScanner Grab.", true);
                    SeqStep = SeqStep.SEQ_WAITING_ALIGN_SCAN_COMPLETED;
                    break;

                case SeqStep.SEQ_WAITING_ALIGN_SCAN_COMPLETED:
                    if (IsGrabDone == false)
                        break;
					WriteLog("Complete Align LineScanner Grab.", true);

                    PlcControlManager.Instance().WriteGrabDone();
                    WriteLog("Send to Plc Grab Done", true);

                    LightCtrlHandler.TurnOff();
                    WriteLog("Light Off.", false);
                    LastInspSW.Restart();

                    SeqStep = SeqStep.SEQ_WAITING_INSPECTION_DONE;
                    break;

                case SeqStep.SEQ_WAITING_INSPECTION_DONE:
                    if (IsInspAkkonDone() == false)
                        break;
              
                    if (IsInspAlignDone() == false)
                        break;

                    LastInspSW.Stop();
                    AppsInspResult.Instance().EndInspTime = DateTime.Now;
                    AppsInspResult.Instance().LastInspTime = LastInspSW.ElapsedMilliseconds.ToString();

                    string message = $"Grab End to Insp Completed Time.({LastInspSW.ElapsedMilliseconds.ToString()}ms)";
                    WriteLog(message, true);

                    PlcControlManager.Instance().EnableSendPeriodically = false;

                    SeqStep = SeqStep.SEQ_MANUAL_CHECK;
                    break;

                case SeqStep.SEQ_MANUAL_CHECK:

                    SeqStep = SeqStep.SEQ_SEND_RESULT;
                    break;

                case SeqStep.SEQ_SEND_RESULT:
                    SendResultData();
                    WriteLog("Completed Send Plc Tab Result Data", true);

                    PlcControlManager.Instance().EnableSendPeriodically = true;
                    SeqStep = SeqStep.SEQ_WAIT_UI_RESULT_UPDATE;
                    break;

                case SeqStep.SEQ_WAIT_UI_RESULT_UPDATE:

                    MoveTo(TeachingPosType.Stage1_Scan_Start, out errorMessage);

                    StartUpdateThread();

                    SeqStep = SeqStep.SEQ_SAVE_RESULT_DATA;
                    break;
               
                case SeqStep.SEQ_SAVE_RESULT_DATA:
                    DailyInfoService.Save(inspModel.Name);
                    SaveInspResultCSV();

                    SeqStep = SeqStep.SEQ_DELETE_DATA;
                    break;

                case SeqStep.SEQ_DELETE_DATA:
                    StartDeleteData();
                    WriteLog("Delete the old data");

                    SeqStep = SeqStep.SEQ_CHECK_STANDBY;
                    break;

                case SeqStep.SEQ_CHECK_STANDBY:
                    //AppsStatus.Instance().IsInspRunnerFlagFromPlc = false;
                    ClearBufferThread();
                    SeqStep = SeqStep.SEQ_INIT;
                    break;

                case SeqStep.SEQ_ERROR:
                    short command = PlcControlManager.Instance().WritePcStatus(PlcCommand.StartInspection, true);
                    Logger.Debug(LogType.Device, $"Sequence Error StartInspection.[{command}]");
                    // 추가 필요
                    IsGrabDone = false;
                    AppsStatus.Instance().IsInspRunnerFlagFromPlc = false;
                    SystemManager.Instance().EnableMainView(true);
                    PlcControlManager.Instance().EnableSendPeriodically = true;
                    WriteLog("Sequnce Error.", true);
                    ClearBuffer();

                    SeqStep = SeqStep.SEQ_IDLE;
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
                var tabResult = AppsInspResult.Instance().GetAkkon(tabNo);

                if (tabResult != null)
                {
                    if (tabResult.MarkResult.Judgement == Judgement.OK)
                    {
                        Stopwatch sw = new Stopwatch();
                        sw.Restart();

                        var tab = inspModel.GetUnit(UnitName.Unit0).GetTab(tabNo);

                        // Overlay Image
                        if(tabResult.AkkonInspMatImage != null)
                        {
                            Mat resultMat = GetResultImage(tabResult.AkkonInspMatImage, tabResult.AkkonResult.LeadResultList, tab.AkkonParam.AkkonAlgoritmParam, ref tabResult.AkkonNGAffineList);
                            ICogImage cogImage = ConvertCogColorImage(resultMat);
                            tabResult.AkkonResultCogImage = cogImage;

                            resultMat.Dispose();

                            // AkkonInspCogImage
                            tabResult.AkkonInspCogImage = ConvertCogGrayImage(tabResult.AkkonInspMatImage);
                        }

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

        private string GetCellID()
        {
            string cellId = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PLC_Cell_Id).Value;
            
            if (cellId == "0" || cellId == null || cellId == "")
                return DateTime.Now.ToString("yyyyMMddHHmmss");
            else
            {
                cellId = cellId.Replace(" ", string.Empty);
                return cellId;
            }
        }

        private void SendResultData()
        {
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            double resolution = LineCamera.Camera.PixelResolution_um / LineCamera.Camera.LensScale;

            for (int tabNo = 0; tabNo < inspModel.TabCount; tabNo++)
            {
                var akkonTabInspResult = AppsInspResult.Instance().GetAkkon(tabNo);
                var alignTabInspResult = AppsInspResult.Instance().GetAlign(tabNo);
                TabJudgement judgement = GetJudgemnet(akkonTabInspResult, alignTabInspResult);
                PlcControlManager.Instance().WriteTabResult(tabNo, judgement, alignTabInspResult.AlignResult, akkonTabInspResult.AkkonResult, alignTabInspResult.MarkResult, resolution);

                Thread.Sleep(10);
            }

            PlcControlManager.Instance().WritePcStatus(PlcCommand.StartInspection);
        }

        private TabJudgement GetJudgemnet(TabInspResult akkonInspResult, TabInspResult alignInspResult)
        {
            if (akkonInspResult.IsManualOK || alignInspResult.IsManualOK)
            {
                return TabJudgement.Manual_OK;
            }
            else
            {
                if (akkonInspResult.MarkResult.Judgement != Judgement.OK)
                    return TabJudgement.Mark_NG;

                if (alignInspResult.MarkResult.Judgement != Judgement.OK)
                    return TabJudgement.Mark_NG;

                if (alignInspResult.AlignResult.Judgement != Judgement.OK)
                    return TabJudgement.NG;

                if (akkonInspResult.AkkonResult == null)
                    return TabJudgement.NG;

                if (akkonInspResult.AkkonResult.Judgement != Judgement.OK)
                    return TabJudgement.NG;

                return TabJudgement.OK;
            }
        }

        public void StartSaveThread()
        {
            if(_saveThread == null)
            {
                _saveThread = new Thread(SaveImage);
                _saveThread.Start();
            }
        }

        private void InitializeBuffer()
        {
        	LineCamera.InitGrabSettings();
            InspProcessTask.InitalizeInspAkkonBuffer(LineCamera.Camera.Name, LineCamera.TabScanBufferList);
            InspProcessTask.InitalizeInspAlignBuffer(LineCamera.Camera.Name, LineCamera.TabScanBufferList);

            ACSBufferManager.Instance().SetLafTriggerPosition(UnitName.Unit0, LAFCtrl.Name, LineCamera.TabScanBufferList, 0);
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

                var tabInspResult = AppsInspResult.Instance().GetAlign(tabNo);

                alignInfo.InspectionTime = AppsInspResult.Instance().EndInspTime.ToString("HH:mm:ss");
                alignInfo.PanelID = AppsInspResult.Instance().Cell_ID;
                alignInfo.TabNo = tabInspResult.TabNo;
                alignInfo.Judgement = tabInspResult.AlignResult.Judgement;
                alignInfo.PreHead = tabInspResult.AlignResult.PreHead;
                alignInfo.FinalHead = AppsInspResult.Instance().FinalHead;
                alignInfo.LX = GetResultAlignResultValue(tabInspResult.AlignResult.LeftX);
                alignInfo.LY = GetResultAlignResultValue(tabInspResult.AlignResult.LeftY);
                alignInfo.RX = GetResultAlignResultValue(tabInspResult.AlignResult.RightX);
                alignInfo.RY = GetResultAlignResultValue(tabInspResult.AlignResult.RightY);

                if (double.TryParse(alignInfo.LX, out double lx) && double.TryParse(alignInfo.RX, out double rx))
                    alignInfo.CX = ((lx + rx) / 2.0F).ToString();
                else
                    alignInfo.CX = "-";

                dailyData.AddAlignInfo(alignInfo);
            }
        }

        private string GetResultAlignResultValue(AlignResult alignResult)
        {
            if (alignResult == null)
                return "-";

            if (alignResult.AlignMissing)
                return "-";

            double resolution = LineCamera.Camera.PixelResolution_um / LineCamera.Camera.LensScale;
            double value = MathHelper.GetFloorDecimal(alignResult.ResultValue_pixel * (float)resolution, 4);
            return value.ToString();
        }

        private void UpdateAkkonDailyInfo(ref DailyData dailyData)
        {
            int tabCount = (ModelManager.Instance().CurrentModel as AppsInspModel).TabCount;

            for (int tabNo = 0; tabNo < tabCount; tabNo++)
            {
                AkkonDailyInfo akkonInfo = new AkkonDailyInfo();

                var tabInspResult = AppsInspResult.Instance().GetAkkon(tabNo);
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
            string csvFile = Path.Combine(resultPath, "Align.csv");
            if (File.Exists(csvFile) == false)
            {
                List<string> header = new List<string>
                {
                    "Inspection Time",
                    "Panel ID",
                    "Stage No",
                };
                for (int index = 0; index < tabCount; index++)
                {
                    header.Add($"Tab_{index + 1}");
                    header.Add($"Judge{index + 1}");
                    header.Add($"Lx_{index + 1}");
                    header.Add($"Ly_{index + 1}");
                    header.Add($"Cx_{index + 1}");
                    header.Add($"Rx_{index + 1}");
                    header.Add($"Ry_{index + 1}");
                }

                CSVHelper.WriteHeader(csvFile, header);
            }

            List<string> body = new List<string>();

            var programType = StringHelper.StringToEnum<ProgramType>(AppsConfig.Instance().ProgramType);
            body.Add($"{AppsInspResult.Instance().EndInspTime:HH:mm:ss}");                  // Insp Time
            body.Add($"{AppsInspResult.Instance().Cell_ID}");                               // Panel ID
            body.Add($"{(int)programType + 1}");                                            // Stage No

            for (int tabNo = 0; tabNo < tabCount; tabNo++)
            {
                var tabInspResult = AppsInspResult.Instance().GetAlign(tabNo);
                var alignResult = tabInspResult.AlignResult;

                double lx = CheckResultValue(alignResult.LeftX);
                double ly = CheckResultValue(alignResult.LeftY);
                double rx = CheckResultValue(alignResult.RightX);
                double ry = CheckResultValue(alignResult.RightY);
                double cx = (lx + rx) / 2.0F;

                body.Add($"{tabInspResult.TabNo + 1}");                                     // Tab No
                body.Add($"{tabInspResult.Judgement}");                                     // Judge
                body.Add($"{lx:F3}");                                                       // Align Lx
                body.Add($"{ly:F3}");                                                       // Align Ly
                body.Add($"{cx:F3}");                                                       // Align Cx
                body.Add($"{rx:F3}");                                                       // Align Rx
                body.Add($"{ry:F3}");                                                       // Align Ry
            }

            CSVHelper.WriteData(csvFile, body);
        }

        private void SaveAkkonResult(string resultPath, int tabCount)
        {
            string csvFile = Path.Combine(resultPath, "Akkon.csv");
            if (File.Exists(csvFile) == false)
            {
                List<string> header = new List<string>
                {
                    "Inspection Time",
                    "Panel ID",
                    "Stage No",
                };
                for (int index = 0; index < tabCount; index++)
                {
                    header.Add($"Tab_{index + 1}");
                    header.Add($"Judge_{index + 1}");
                    header.Add($"Min Count_{index + 1}");
                    header.Add($"Max Count_{index + 1}");
                    header.Add($"Avg Count_{index + 1}");
                    header.Add($"Avg Length_{index + 1}");
                }

                CSVHelper.WriteHeader(csvFile, header);
            }

            List<string> body = new List<string>();

            var programType = StringHelper.StringToEnum<ProgramType>(AppsConfig.Instance().ProgramType);
            body.Add($"{AppsInspResult.Instance().EndInspTime:HH:mm:ss}");                  // Insp Time
            body.Add($"{AppsInspResult.Instance().Cell_ID}");                               // Panel ID
            body.Add($"{(int)programType + 1}");                                            // Stage No
            for (int tabNo = 0; tabNo < tabCount; tabNo++)
            {
                var tabInspResult = AppsInspResult.Instance().GetAkkon(tabNo);
                var akkonResult = tabInspResult.AkkonResult;

                int minCount = Math.Min(akkonResult.LeftCount_Min, akkonResult.RightCount_Min);
                int maxCount = Math.Max(akkonResult.LeftCount_Max, akkonResult.RightCount_Max);

                int avgCount = (akkonResult.LeftCount_Avg + akkonResult.RightCount_Avg) / 2;
                float avgLength = (akkonResult.Length_Left_Avg_um + akkonResult.Length_Right_Avg_um) / 2.0F;

                body.Add($"{tabInspResult.TabNo + 1}");                                     // Tab No
                body.Add($"{tabInspResult.Judgement}");                                     // Judge
                body.Add($"{minCount}");                                                    // Min Count
                body.Add($"{maxCount}");                                                    // Max Count
                body.Add($"{avgCount}");                                                    // Average Count
                body.Add($"{avgLength:F3}");                                                // Average Length

            }
            CSVHelper.WriteData(csvFile, body);
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
                    "Stage No",
                    "Tab No.",

                    "Count Min",
                    "Count Avg",
                    "Length Min",
                    "Length Avg",

                    "Left Align X",
                    "Left Align Y",
                    "Center Align X",
                    "Right Align X",
                    "Right Align Y",

                    "Pre Head",
                    "Main Head",

                    "AkkonJudge",
                    "AlignJudge",
                };

                CSVHelper.WriteHeader(csvFile, header);
            }

            List<List<string>> body = new List<List<string>>();
            for (int tabNo = 0; tabNo < tabCount; tabNo++)
            {
                var akkonResult = AppsInspResult.Instance().GetAkkon(tabNo);
                int countMin = Math.Min(akkonResult.AkkonResult.LeftCount_Min, akkonResult.AkkonResult.RightCount_Min);
                float countAvg = (akkonResult.AkkonResult.LeftCount_Avg + akkonResult.AkkonResult.RightCount_Avg) / 2.0F;
                float lengthMin = Math.Min(akkonResult.AkkonResult.Length_Left_Min_um, akkonResult.AkkonResult.Length_Right_Min_um);
                float lengthAvg = (akkonResult.AkkonResult.Length_Left_Avg_um + akkonResult.AkkonResult.Length_Right_Avg_um) / 2.0F;

                var alignResult = AppsInspResult.Instance().GetAlign(tabNo);
                double lx = CheckResultValue(alignResult.AlignResult.LeftX);
                double ly = CheckResultValue(alignResult.AlignResult.LeftY);
                double rx = CheckResultValue(alignResult.AlignResult.RightX);
                double ry = CheckResultValue(alignResult.AlignResult.RightY);
                double cx = (lx + rx) / 2.0F;

                var programType = StringHelper.StringToEnum<ProgramType>(AppsConfig.Instance().ProgramType);
                List<string> tabData = new List<string>
                {
                    $"{AppsInspResult.Instance().EndInspTime:HH:mm:ss}",                   // Insp Time
                    $"{AppsInspResult.Instance().Cell_ID}",                                // Panel ID
                    $"{(int)programType + 1}",                                             // Unit No
                    $"{akkonResult.TabNo + 1}",                                            // Tab

                    $"{countMin}",                                                         // Count Min
                    $"{countAvg:F3}",                                                      // Count Avg
                    $"{lengthMin:F3}",                                                     // Length Min
                    $"{lengthAvg:F3}",                                                     // Length Avg

                    $"{lx:F3}",                                                            // Left Align X
                    $"{ly:F3}",                                                            // Left Align Y
                    $"{cx:F3}",                                                            // Center Align X
                    $"{rx:F3}",                                                            // Right Align X
                    $"{ry:F3}",                                                            // Right Align Y

                    "-1",                                                         // ACF Head
                    "-1",                                                         // Pre Head
                    "-1",                                                         // Main Head

                    akkonResult.Judgement.ToString(),                           // Akkon Judge
                    alignResult.Judgement.ToString(),                           // Align Judge
                };

                body.Add(tabData);
            }
            CSVHelper.WriteData(csvFile, body);
        }

        private double CheckResultValue(AlignResult alignResult)
        {
            float resolution = LineCamera.Camera.PixelResolution_um / LineCamera.Camera.LensScale;
            if (alignResult == null)
                return 0.0F;
            else
                return alignResult.ResultValue_pixel * resolution;
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
            Axis axisY = GetAxis(AxisHandlerName.Handler0, AxisName.Y);
            //Axis axisZ = GetAxis(AxisHandlerName.Handler0, AxisName.Z);

            var movingParamX = teachingInfo.GetMovingParam(AxisName.X.ToString());
            var movingParamY = teachingInfo.GetMovingParam(AxisName.Y.ToString());
            var movingParamZ = teachingInfo.GetMovingParam(AxisName.Z0.ToString());

            if (MoveAxis(teachingPos, axisX, movingParamX) == false)
            {
                errorMessage = string.Format("Move To Axis X TimeOut!({0})", movingParamX.MovingTimeOut.ToString());
                WriteLog(errorMessage);
                return false;
            }
						
            if (MoveAxis(teachingPos, axisY, movingParamY) == false)
            {
                errorMessage = string.Format("Move To Axis Y TimeOut!({0})", movingParamY.MovingTimeOut.ToString());
                WriteLog(errorMessage);
                return false;
            }
			
            //if (MoveAxis(teachingPos, axisZ, movingParamZ) == false)
            //{
            //    error = string.Format("Move To Axis Z TimeOut!({0})", movingParamZ.MovingTimeOut.ToString());
            //    WriteLog(LogType.Seq, error);
            //    return false;
			//}

            string message = string.Format("Move Completed.(Teaching Pos : {0})", teachingPos.ToString());
            WriteLog(message);

            return true;
        }

        private bool MoveAxis(TeachingPosType teachingPos, Axis axis, AxisMovingParam movingParam)
        {
            MotionManager manager = MotionManager.Instance();
            double cameraGap = 0;
            if (teachingPos == TeachingPosType.Stage1_Scan_End)
                cameraGap = AppsConfig.Instance().CameraGap_mm;
                
            if (manager.IsAxisInPosition(UnitName.Unit0, teachingPos, axis, cameraGap) == false)
            {
                Stopwatch sw = new Stopwatch();
                sw.Restart();

                manager.StartAbsoluteMove(UnitName.Unit0, teachingPos, axis, cameraGap);

                while (manager.IsAxisInPosition(UnitName.Unit0, teachingPos, axis, cameraGap) == false)
                {
                    if (sw.ElapsedMilliseconds >= movingParam.MovingTimeOut)
                        return false;

                    Thread.Sleep(10);
                }
            }

            return true;
        }

        private bool MoveLAF(TeachingPosType teachingPos, Axis axis, AxisMovingParam movingParam)
        {
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            var posData = inspModel.GetUnit(UnitName.Unit0).GetTeachingInfo(teachingPos);
            var targetPosition = posData.GetTargetPosition(axis.Name);

            LAFCtrl.SetMotionAbsoluteMove(targetPosition);

            Thread.Sleep(5000);
            var position = LAFCtrl.Status.MPosPulse;
            Console.WriteLine("Target Laf MPos : " + position.ToString());
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

        public Mat GetResultImage(Mat resizeMat, List<AkkonLeadResult> leadResultList, AkkonAlgoritmParam akkonParameters, ref List<CogRectangleAffine> akkonNGAffineList)
        {
            if (resizeMat == null)
                return null;

            Mat colorMat = new Mat();
            CvInvoke.CvtColor(resizeMat, colorMat, ColorConversion.Gray2Bgr);

            MCvScalar redColor = new MCvScalar(50, 50, 230, 255);
            MCvScalar greenColor = new MCvScalar(50, 230, 50, 255);
            MCvScalar orangeColor = new MCvScalar(0, 165, 255);

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
                    CvInvoke.Line(colorMat, leftTop, leftBottom, redColor, 1);
                    CvInvoke.Line(colorMat, leftTop, rightTop, redColor, 1);
                    CvInvoke.Line(colorMat, rightTop, rightBottom, redColor, 1);
                    CvInvoke.Line(colorMat, rightBottom, leftBottom, redColor, 1);

                    var rect = VisionProShapeHelper.ConvertToCogRectAffine(leftTop, rightTop, leftBottom);
                    akkonNGAffineList.Add(rect);
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
                        CvInvoke.Circle(colorMat, center, radius / 2, greenColor, 1);
                    }
                    else
                    {
                        double strengthValue = Math.Abs(blob.Strength - akkonParameters.ShapeFilterParam.MinAkkonStrength);
                        if (strengthValue <= 1)
                        {
                            int temp = (int)(radius / 2.0);
                            Point pt = new Point(center.X + temp, center.Y - temp);
                            string strength = blob.Strength.ToString("F1");

                            CvInvoke.Circle(colorMat, center, radius / 2, orangeColor, 1);
                            CvInvoke.PutText(colorMat, strength, pt, FontFace.HersheySimplex, 0.3, orangeColor);
                        }
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
            {
                _saveThread = null;
                return;
            }

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

                SaveResultImage(Path.Combine(path, "Akkon"), tabNo, true);
                SaveResultImage(Path.Combine(path, "Align"), tabNo, false);
            }

            sw.Stop();
            Console.WriteLine("Save Image : " + sw.ElapsedMilliseconds.ToString() + "ms");
            WriteLog("Save Image : " + sw.ElapsedMilliseconds.ToString() + "ms");
            _saveThread = null;
        }

        private void SaveResultImage(string resultPath, int tabNo, bool isAkkonResult)
        {
            TabInspResult tabInspResult = null;
            if (isAkkonResult)
                tabInspResult = AppsInspResult.Instance().GetAkkon(tabNo);
            else
                tabInspResult = AppsInspResult.Instance().GetAlign(tabNo);

            var operation = ConfigSet.Instance().Operation;

            if (Directory.Exists(resultPath) == false)
                Directory.CreateDirectory(resultPath);

            string okExtension = operation.GetExtensionOKImage();
            string ngExtension = operation.GetExtensionNGImage();

            if (tabInspResult.Judgement == TabJudgement.OK || tabInspResult.Judgement == TabJudgement.Manual_OK)
            {
                if (ConfigSet.Instance().Operation.SaveImageOK)
                {
                    string imageName = AppsInspResult.Instance().Cell_ID + "_Tab_" + tabInspResult.TabNo.ToString();
                    string filePath = Path.Combine(resultPath, imageName);

                    if (operation.ExtensionOKImage == ImageExtension.Bmp)
                    {
                        SaveImage(tabInspResult.Image, filePath, Judgement.OK, ImageExtension.Bmp, false);
                    }
                    else if (operation.ExtensionOKImage == ImageExtension.Jpg)
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
                    string imageName = AppsInspResult.Instance().Cell_ID + "_Tab_" + tabInspResult.TabNo.ToString();
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
            if (extension == ImageExtension.Bmp)
            {
                filePath += $"_{judgement}.bmp";
                image.Save(filePath);
            }
            else if (extension == ImageExtension.Jpg)
            {
                if (isHalfSave)
                {

                    string leftPath = $"{filePath}_{judgement}_Left.jpg";
                    string rightPath = $"{filePath}_{judgement}_Right.jpg";

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
                    filePath += $"_{judgement}.jpg";
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
        public bool ClearBufferThread()
        {
            if (_ClearBufferThread == null)
            {
                _ClearBufferThread = new Thread(ClearBuffer);
                _ClearBufferThread.Start();
                return true;
            }
            return false;
        }

        private void ClearBuffer()
        {
            Console.WriteLine("ClearBuffer");
            LineCamera.IsLive = false;

            LineCamera.SetOperationMode(TDIOperationMode.TDI);

            LightCtrlHandler?.TurnOff();

            LAFCtrl?.SetTrackingOnOFF(false);
            LAFCtrl?.SetDefaultParameter();
            LineCamera.ClearTabScanBuffer();

            MotionManager.Instance().MoveAxisZ(UnitName.Unit0, TeachingPosType.Stage1_Scan_Start, LAFCtrl, AxisName.Z0);

            _ClearBufferThread = null;
            WriteLog("Clear Buffer.");
        }
        #endregion
    }

    public enum SeqStep
    {
        SEQ_IDLE,
        SEQ_INIT,
        SEQ_WAITING,
        SEQ_BUFFER_INIT,
        SEQ_MOVE_START_POS,
        SEQ_SCAN_START,
        SEQ_MOVE_END_POS,
        SEQ_WAITING_AKKON_SCAN_COMPLETED,
        SEQ_WAITING_ALIGN_SCAN_COMPLETED,
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
