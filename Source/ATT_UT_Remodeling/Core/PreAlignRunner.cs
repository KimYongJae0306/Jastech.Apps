using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure;
using Jastech.Framework.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging;
using Jastech.Framework.Util.Helper;
using Cognex.VisionPro;
using Jastech.Apps.Winform;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Imaging.VisionPro;
using System.Diagnostics;
using System.Threading;
using Jastech.Framework.Device.LightCtrls;
using Jastech.Apps.Winform.Service.Plc.Maps;
using Jastech.Apps.Winform.Settings;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Apps.Winform.Core.Calibrations;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using System.Drawing;
using Jastech.Framework.Winform;
using Jastech.Apps.Winform.Core;

namespace ATT_UT_Remodeling
{
    public partial class PreAlignRunner
    {
        #region 필드
        private Task SeqTask { get; set; }

        private CancellationTokenSource SeqTaskCancellationTokenSource { get; set; }

        private SeqStep SeqStep { get; set; } = SeqStep.SEQ_IDLE;

        private AppsInspResult AppsInspResult { get; set; } = null;
        #endregion

        #region 속성
        private MainAlgorithmTool AlgorithmTool = new MainAlgorithmTool();

        private CogImage8Grey VirtualLeftImage = null;

        private CogImage8Grey VirtualRightImage = null;
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        #endregion

        #region 메서드
        #endregion

        public void SeqRun()
        {
            if (ModelManager.Instance().CurrentModel == null)
                return;

            SystemManager.Instance().MachineStatus = MachineStatus.RUN;

            if (SeqTask != null)
            {
                SeqStep = SeqStep.SEQ_READY;
                return;
            }

            SeqTaskCancellationTokenSource = new CancellationTokenSource();
            SeqTask = new Task(SeqTaskAction, SeqTaskCancellationTokenSource.Token);
            SeqTask.Start();
        }

        public void SeqStop()
        {
            SystemManager.Instance().MachineStatus = MachineStatus.STOP;

            var areaCamera = AreaCameraManager.Instance().GetAppsCamera("PreAlign");
            areaCamera.StopGrab();
            AreaCameraManager.Instance().GetAreaCamera("PreAlign").StopGrab();

            // 조명 off
            LAFManager.Instance().TrackingOnOff("Laf", false);
            WriteLog("AutoFocus Off.");

            if (SeqTask == null)
                return;

            SeqTaskCancellationTokenSource.Cancel();
            SeqTask.Wait();
            SeqTask = null;

            WriteLog("Stop Sequence.");
        }

        private void SeqTaskAction()
        {
            var cancellationToken = SeqTaskCancellationTokenSource.Token;
            cancellationToken.ThrowIfCancellationRequested();
            SeqStep = SeqStep.SEQ_WAITING;

            while (true)
            {
                // 작업 취소
                if (cancellationToken.IsCancellationRequested)
                {
                    SeqStep = SeqStep.SEQ_IDLE;

                    // Light off
                    DeviceManager.Instance().LightCtrlHandler.TurnOff();

                    // Camera stop
                    LineCameraManager.Instance().Stop("LineCamera");
                    AreaCameraManager.Instance().Stop("PreAlign");
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

            var areaCamera = AreaCameraManager.Instance().GetAreaCamera("PreAlign");
            if (areaCamera == null)
                return;

            var laf = LAFManager.Instance().GetLAFCtrl("Laf");
            if (laf == null)
                return;

            var light = DeviceManager.Instance().LightCtrlHandler;
            
            string systemLogMessage = string.Empty;
            string errorMessage = string.Empty;

            switch (SeqStep)
            {
                case SeqStep.SEQ_IDLE:
                    break;

                case SeqStep.SEQ_WAITING:

                    if (AppsStatus.Instance().IsPreAlignRunnerFlagFromPlc == false)
                        break;

                    WriteLog("Receive PreAlign Start Signal From PLC.", true);
                    SeqStep = SeqStep.SEQ_READY;
                    break;

                case SeqStep.SEQ_READY:

                    light.TurnOff();
                    WriteLog("Light Off.");

                    PlcControlManager.Instance().ClearAlignData();
                    WriteLog("Clear PLC Data.");

                    LAFManager.Instance().TrackingOnOff("Laf", false);
                    laf.SetMotionAbsoluteMove(0);
                    WriteLog("LAF Off.");

                    var camera = areaCamera.Camera;
                    camera.Stop();
                    WriteLog($"Set Camera Property. Expose : {camera.Exposure}, AnalogGain : {camera.AnalogGain}");

                    // CELL ID 넣는곳이 없음
                    SeqStep = SeqStep.SEQ_START;
                    break;
           
                case SeqStep.SEQ_START:

                    SeqStep = SeqStep.SEQ_PREALIGN_R;
                    break;

                case SeqStep.SEQ_PREALIGN_R:

                    if (MoveTo(TeachingPosType.Stage1_PreAlign_Right, out errorMessage) == false)
                        SeqStep = SeqStep.SEQ_ERROR;
                    else
                    {
                        // Light On
                        light.TurnOn(unit.PreAlign.RightLightParam);
                        Thread.Sleep(100);
                        WriteLog("Left Prealign Light On.");

                        // Grab
                        CogImage8Grey leftImage = null;

                        if(ConfigSet.Instance().Operation.VirtualMode == false)
                        {
                            var preAlignRightImage = GetAreaCameraImage(areaCamera.Camera) as CogImage8Grey;
                            AppsInspResult.PreAlignResult.PreAlignMark.FoundedMark.Right = RunPreAlignMark(unit, preAlignRightImage, MarkDirection.Right);
                        }
                        else
                        {
                            AppsInspResult.PreAlignResult.PreAlignMark.FoundedMark.Right = RunPreAlignMark(unit, VirtualRightImage, MarkDirection.Right);
                        }
                  
                        WriteLog("Complete PreAlign Right Mark Search.", true);

                        // Set prealign motion position
                        SetMarkMotionPosition(unit, MarkDirection.Right);
                        WriteLog("Set Axis Information For PreAlign Right Mark Position.");

                        SeqStep = SeqStep.SEQ_PREALIGN_L;
                    }
                    break;

                case SeqStep.SEQ_PREALIGN_L:

                    if (MoveTo(TeachingPosType.Stage1_PreAlign_Left, out errorMessage) == false)
                        SeqStep = SeqStep.SEQ_ERROR;
                    else
                    {
                        // Light On
                        light.TurnOn(unit.PreAlign.LeftLightParam);
                        Thread.Sleep(100);
                        WriteLog("Left PreAlign Light On.", true);

                        // Grab start
                        if (ConfigSet.Instance().Operation.VirtualMode == false)
                        {
                            var preAlignLeftImage = GetAreaCameraImage(areaCamera.Camera);
                            AppsInspResult.PreAlignResult.PreAlignMark.FoundedMark.Left = RunPreAlignMark(unit, preAlignLeftImage, MarkDirection.Left);
                        }
                        else
                        {
                            AppsInspResult.PreAlignResult.PreAlignMark.FoundedMark.Left = RunPreAlignMark(unit, VirtualLeftImage, MarkDirection.Left);
                        }
                        WriteLog("Complete PreAlign Left Mark Search.", true);

                        // Set prealign motion position
                        SetMarkMotionPosition(unit, MarkDirection.Left);
                        WriteLog("Set Axis Information For Prealign Left Mark Position.");

                        SeqStep = SeqStep.SEQ_SEND_PREALIGN_DATA;
                    }
                    break;

                case SeqStep.SEQ_SEND_PREALIGN_DATA:

                    light.TurnOff();
                    WriteLog("PreAlign Light Off.",true);

                    if (RunPreAlign(AppsInspResult))
                    {
                        var offsetX = AppsInspResult.PreAlignResult.OffsetX;
                        var offsetY = AppsInspResult.PreAlignResult.OffsetY;
                        var offsetT = AppsInspResult.PreAlignResult.OffsetT;

                        PlcControlManager.Instance().WriteAlignData(offsetX, offsetY, offsetT);
                        WriteLog($"Write PreAlign Data.(OffsetX : {offsetX.ToString("F4")}, OffsetY : {offsetY.ToString("F4")}, OffsetT : {offsetT.ToString("F4")})", true);

                        // Check Tolerance
                        if (Math.Abs(offsetX) <= AppsConfig.Instance().PreAlignToleranceX
                            || Math.Abs(offsetY) <= AppsConfig.Instance().PreAlignToleranceY
                            || Math.Abs(offsetT) <= AppsConfig.Instance().PreAlignToleranceTheta)
                        {
                            PlcControlManager.Instance().WritePcStatus(PlcCommand.StartPreAlign);
                            WriteLog($"Send PreAlign OK Complete Signal.(SpecIn) {(int)PlcCommand.StartPreAlign}", true);
                        }
                        else
                        {
                            PlcControlManager.Instance().WritePcStatus(PlcCommand.StartPreAlign, true);
                            WriteLog($"Send PreAlign NG Complete Signal.(SpecOut) {(int)PlcCommand.StartPreAlign * -1}", true);
                        }
                    }
                    else
                    {
                        PlcControlManager.Instance().WritePcStatus(PlcCommand.StartPreAlign, true);
                        WriteLog($"Send PreAlign NG Complete Signal.(Mark Fail) {(int)PlcCommand.StartPreAlign * -1}", true);
                    }
                   
                    SeqStep = SeqStep.SEQ_UI_RESULT_UPDATE;
                    break;
                case SeqStep.SEQ_UI_RESULT_UPDATE:

                    SeqStep = SeqStep.SEQ_SAVE_RESULT_DATA;

                    break;
                case SeqStep.SEQ_SAVE_RESULT_DATA:

                    SeqStep = SeqStep.SEQ_SAVE_IMAGE;

                    break;
                case SeqStep.SEQ_SAVE_IMAGE:

                    SeqStep = SeqStep.SEQ_DELETE_DATA;

                    break;
                case SeqStep.SEQ_DELETE_DATA:
                    // 이거 필요한지 고민좀..
                    // 메인 검사 시퀀스에서 하니깐 필요 없을듯????
                    SeqStep = SeqStep.SEQ_END;

                    break;
                case SeqStep.SEQ_END:
                    if (ConfigSet.Instance().Operation.VirtualMode)
                        AppsStatus.Instance().IsPreAlignRunnerFlagFromPlc = false;

                    SeqStep = SeqStep.SEQ_READY;
                    break;

                case SeqStep.SEQ_ERROR:

                    WriteLog("SEQ_ERROR.", true);

                    // Light off
                    DeviceManager.Instance().LightCtrlHandler.TurnOff();
                    // Camera stop
                    AreaCameraManager.Instance().Stop("PreAlign");

                    PlcControlManager.Instance().WritePcStatus(PlcCommand.StartPreAlign, false);
                    WriteLog($"Send PreAlign NG Complete Signal.{(int)PlcCommand.StartPreAlign * -1}", true);

                    Thread.Sleep(300);

                    SeqStep = SeqStep.SEQ_IDLE;
                    break;
                default:
                    break;
            }
        }

        private bool RunPreAlign(AppsInspResult inspResult)
        {
            if (inspResult.PreAlignResult.PreAlignMark.FoundedMark.Left.Judgement == Judgment.OK && inspResult.PreAlignResult.PreAlignMark.FoundedMark.Right.Judgement == Judgment.OK)
            {
                PointF leftVisionCoordinates = inspResult.PreAlignResult.PreAlignMark.FoundedMark.Left.MaxMatchPos.FoundPos;
                PointF rightVisionCoordinates = inspResult.PreAlignResult.PreAlignMark.FoundedMark.Right.MaxMatchPos.FoundPos;

                List<PointF> realCoordinateList = new List<PointF>();
                PointF leftRealCoordinates = CalibrationData.Instance().ConvertVisionToReal(leftVisionCoordinates);
                PointF righttRealCoordinates = CalibrationData.Instance().ConvertVisionToReal(rightVisionCoordinates);

                realCoordinateList.Add(leftRealCoordinates);
                realCoordinateList.Add(righttRealCoordinates);

                var unit = TeachingData.Instance().GetUnit("Unit0");
                PointF calibrationStartPosition = CalibrationData.Instance().GetCalibrationStartPosition();

                inspResult.PreAlignResult = AlgorithmTool.ExecuteAlignment(unit, realCoordinateList, calibrationStartPosition);

                Judgment leftJudgement = AppsInspResult.PreAlignResult.PreAlignMark.FoundedMark.Left.Judgement;
                Judgment rightJudgement = AppsInspResult.PreAlignResult.PreAlignMark.FoundedMark.Right.Judgement;
                var leftScore = AppsInspResult.PreAlignResult.PreAlignMark.FoundedMark.Left.MaxMatchPos.Score;
                var rightScore = AppsInspResult.PreAlignResult.PreAlignMark.FoundedMark.Right.MaxMatchPos.Score;

                WriteLog($"OK Mark Search For PreAlign. (Left : {leftJudgement.ToString()}, Right : {rightJudgement.ToString()})", true);
                WriteLog($"FoundedMark Score. (Left : {leftScore.ToString("F4")}, Right : {rightScore.ToString("F4")})", true);
                return true;
            }
            else
            {
                Judgment leftJudgement = AppsInspResult.PreAlignResult.PreAlignMark.FoundedMark.Left.Judgement;
                Judgment rightJudgement = AppsInspResult.PreAlignResult.PreAlignMark.FoundedMark.Right.Judgement;
                var leftScore = AppsInspResult.PreAlignResult.PreAlignMark.FoundedMark.Left.MaxMatchPos.Score;
                var rightScore = AppsInspResult.PreAlignResult.PreAlignMark.FoundedMark.Right.MaxMatchPos.Score;

                WriteLog($"NG Mark Search For PreAlign. (Left : {leftJudgement.ToString()}, Right : {rightJudgement.ToString()})", true);
                WriteLog($"FoundedMark Score. (Left : {leftScore.ToString("F4")}, Right : {rightScore.ToString("F4")})", true);
                return false;
            } 
        }

        private VisionProPatternMatchingResult RunPreAlignMark(Unit unit, ICogImage cogImage, MarkDirection markDirection)
        {
            var preAlignParam = unit.PreAlign.AlignParamList.Where(x => x.Direction == markDirection).FirstOrDefault();

            VisionProPatternMatchingResult result = AlgorithmTool.RunPatternMatch(cogImage, preAlignParam.InspParam);

            return result;
        }
    }

    public partial class PreAlignRunner
    {
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

            if (ConfigSet.Instance().Operation.ExtensionOKImage == ImageExtension.Bmp)
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
                    if (ConfigSet.Instance().Operation.SaveImageOK)
                    {
                        string imageName = "Tab_" + result.TabNo.ToString() + "_OK_" + okExtension;
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

        private void SavePreAlignResult(string resultPath, AppsInspResult inspResult)
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

            // Right PreAlign Pattern Matching
            var cogImage = VisionProImageHelper.ConvertImage(dataArrayRight, camera.ImageWidth, camera.ImageHeight, camera.ColorFormat);

            return cogImage;
        }

        public void SetPreAlignLeftImage(CogImage8Grey cogImage)
        {
            if (VirtualLeftImage != null)
                (VirtualLeftImage as CogImage8Grey).Dispose();

            VirtualLeftImage = null;
            VirtualLeftImage = cogImage;
        }

        public void SetPreAlignRightImage(CogImage8Grey cogImage)
        {
            if (VirtualRightImage != null)
                (VirtualRightImage as CogImage8Grey).Dispose();

            VirtualRightImage = null;
            VirtualRightImage = cogImage;
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
    }

    public enum SeqStep
    {
        SEQ_IDLE,
        SEQ_READY,
        SEQ_WAITING,
        SEQ_START,
        SEQ_PREALIGN_R,
        SEQ_PREALIGN_L,
        SEQ_SEND_PREALIGN_DATA,
        SEQ_UI_RESULT_UPDATE,
        SEQ_SAVE_RESULT_DATA,
        SEQ_SAVE_IMAGE,
        SEQ_DELETE_DATA,
        SEQ_END,
        SEQ_ERROR,
    }
}
