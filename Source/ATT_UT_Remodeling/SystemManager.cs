using ATT_UT_Remodeling.Core;
using ATT_UT_Remodeling.Core.Data;
using Cognex.VisionPro;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Core.Calibrations;
using Jastech.Apps.Winform.Service;
using Jastech.Apps.Winform.Service.Plc;
using Jastech.Apps.Winform.Service.Plc.Maps;
using Jastech.Apps.Winform.UI.Forms;
using Jastech.Framework.Config;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Device.LightCtrls;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Device.Plcs;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Structure;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform;
using Jastech.Framework.Winform.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace ATT_UT_Remodeling
{
    public partial class SystemManager
    {
        #region 필드
        private static SystemManager _instance = null;

        private MainForm _mainForm = null;

        private ATTInspRunner _inspRunner = new ATTInspRunner();

        private PreAlignRunner _preAlignRunner = new PreAlignRunner();
        #endregion

        #region 속성
        public VisionXCalibration VisionXCalibration { get; set; } = new VisionXCalibration();
        #endregion

        #region 메서드
        public static SystemManager Instance()
        {
            if (_instance == null)
                _instance = new SystemManager();

            return _instance;
        }

        public bool Initialize(MainForm mainForm)
        {
            _mainForm = mainForm;

            Logger.Write(LogType.System, "Init SplashForm");

            SplashForm form = new SplashForm();

            form.Title = "ATT Inspection";
            form.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            form.SetupActionEventHandler = SplashSetupAction;

            form.ShowDialog();

            var recentModelName = ConfigSet.Instance().Operation.LastModelName;
            DailyInfoService.Load(recentModelName);

            return true;
        }

        private bool SplashSetupAction(IReportProgress reportProgress)
        {
            Logger.Write(LogType.System, "Initialize Device");

            int percent = 0;
            DoReportProgress(reportProgress, percent, "Initialize Device");

            DeviceManager.Instance().Initialized += SystemManager_Initialized;
            DeviceManager.Instance().Initialize(ConfigSet.Instance());
            PlcControlManager.Instance().Initialize();

            percent = 50;
            DoReportProgress(reportProgress, percent, "Create Axis Info");

            CreateAxisHandler();

            percent = 80;
            DoReportProgress(reportProgress, percent, "Initialize Manager.");
            LAFManager.Instance().Initialize();
            LineCameraManager.Instance().Initialize();
            AreaCameraManager.Instance().Initialize();

            var areaCamera = AreaCameraManager.Instance().GetAppsCamera("PreAlign");
            if (areaCamera != null)
                VisionXCalibration.SetCamera(areaCamera);

            var axisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
            if (axisHandler != null)
                VisionXCalibration.SetAxisHandler(axisHandler);

            if (ConfigSet.Instance().Operation.LastModelName != "")
            {
                percent = 90;
                DoReportProgress(reportProgress, percent, "Open Last Model.");

                string filePath = Path.Combine(ConfigSet.Instance().Path.Model,
                                    ConfigSet.Instance().Operation.LastModelName,
                                    InspModel.FileName);
                if (File.Exists(filePath))
                {
                    DoReportProgress(reportProgress, percent, "Model Loading");
                    ModelManager.Instance().CurrentModel = _mainForm.ATTInspModelService.Load(filePath);
                }
            }

            percent = 100;
            DoReportProgress(reportProgress, percent, "Initialize Completed.");
            return true;
        }

        private void DoReportProgress(IReportProgress reportProgress, int percentage, string message)
        {
            Logger.Write(LogType.System, message);

            reportProgress?.ReportProgress(percentage, message);
        }

        private void SystemManager_Initialized(Type deviceType, bool success)
        {
            if (success)
                return;

            string message = "";
            if (deviceType == typeof(Camera))
            {
                message = "Camera Initialize Fail";
            }
            else if (deviceType == typeof(Motion))
            {
                message = "Motion Initialize Fail";
            }
            else if (deviceType == typeof(LightCtrl))
            {
                message = "LightCtrl Initialize Fail";
            }
            else if (deviceType == typeof(LAFCtrl))
            {
                message = "LAF Initialize Fail";
            }
            else if (deviceType == typeof(Plc))
            {
                message = "Plc Initialize Fail";
            }

            if (message != "")
            {
                MessageYesNoForm form = new MessageYesNoForm();
                form.Message = message;
                form.ShowDialog();
            }
        }

        public bool CreateAxisHandler()
        {
            var motion = DeviceManager.Instance().MotionHandler.First();
            if (motion == null)
                return false;

            string dir = Path.Combine(ConfigSet.Instance().Path.Config, "AxisHanlder");

            if (Directory.Exists(dir) == false)
                Directory.CreateDirectory(dir);

            string unit0FileName = string.Format("AxisHanlder_{0}.json", AxisHandlerName.Handler0);
            string unit0FilePath = Path.Combine(dir, unit0FileName);
            if (File.Exists(unit0FilePath) == false)
            {
                AxisHandler handler0 = new AxisHandler(AxisHandlerName.Handler0.ToString());

                handler0.AddAxis(AxisName.X, motion, axisNo: 0, homeOrder: 3);
                handler0.AddAxis(AxisName.Z0, motion, axisNo: 1, homeOrder: 1);

                //AxisHandlerList.Add(handler0);
                MotionManager.Instance().AxisHandlerList.Add(handler0);

                JsonConvertHelper.Save(unit0FilePath, handler0);
            }
            else
            {
                AxisHandler unit0 = new AxisHandler();
                JsonConvertHelper.LoadToExistingTarget<AxisHandler>(unit0FilePath, unit0);

                foreach (var axis in unit0.GetAxisList())
                    axis.SetMotion(motion);

                //AxisHandlerList.Add(unit0);
                MotionManager.Instance().AxisHandlerList.Add(unit0);
            }

            return true;
        }

        public void UpdateMainResult(int tabNo)
        {
            _mainForm.UpdateMainResult(tabNo);
        }

        public void UpdateResultTabButton(int tabNo)
        {
            _mainForm.UpdateResultTabButton(tabNo);
        }

        public void EnableMainView(bool isEnable)
        {
            _mainForm.Enable(isEnable);
        }

        public void TabButtonResetColor()
        {
            _mainForm.TabButtonResetColor();
        }

        public void UpdateMainResult()
        {
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            for (int tabNo = 0; tabNo < inspModel.TabCount; tabNo++)
                _mainForm.UpdateMainResult(tabNo);
        }

        public void AddSystemLogMessage(string logMessage)
        {
            _mainForm.AddSystemLogMessage(logMessage);
        }

        public void StartRun()
        {
            if (PlcControlManager.Instance().MachineStatus != MachineStatus.RUN)
            {
                MessageYesNoForm form = new MessageYesNoForm();
                form.Message = "Do you want to Start Auto Mode?";

                if (form.ShowDialog() == DialogResult.Yes)
                {
                    _preAlignRunner.SeqRun();
                    _inspRunner.SeqRun();
                    AddSystemLogMessage("Start Auto mode.");

                    PlcControlManager.Instance().MachineStatus = MachineStatus.RUN;
                }
            }
        }

        public void StopRun()
        {
            if (ModelManager.Instance().CurrentModel == null)
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "Current Model is null.";
                return;
            }

            if (PlcControlManager.Instance().MachineStatus != MachineStatus.STOP)
            {
                MessageYesNoForm form = new MessageYesNoForm();
                form.Message = "Do you want to Stop Auto Mode?";

                if (form.ShowDialog() == DialogResult.Yes)
                {
                    _preAlignRunner.SeqStop();
                    _inspRunner.SeqStop();
                    AddSystemLogMessage("Stop Auto Mode.");

                    PlcControlManager.Instance().MachineStatus = MachineStatus.STOP;
                }
            }
        }

        public void StartCalibration(UnitName unitName, CalibrationMode calibrationMode)
        {
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            var unit = inspModel.GetUnit(unitName);
            var param = unit.PreAlign.CalibrationParam.InspParam;

            VisionXCalibration.UnitName = UnitName.Unit0;
            VisionXCalibration.SetInterval(intervalX: 1.5, intervalY: 1.5);
            VisionXCalibration.SetParam(param);
            VisionXCalibration.SetCalibrationMode(calibrationMode);
            VisionXCalibration.StartCalSeqRun();
        }

        public void SetVirtualImage(int tabNo, string fileName)
        {
            _inspRunner.SetVirtualmage(tabNo, fileName);
        }

        public void InitializeInspRunner()
        {
            _inspRunner.Initialize();
        }

        public void ReleaseInspRunner()
        {
            _inspRunner.Release();
        }

        public void SetLeftPreAlignImage(string filePath)
        {
            ICogImage cogImage = VisionProImageHelper.Load(filePath);
             _preAlignRunner.SetPreAlignLeftImage(cogImage as CogImage8Grey);
        }

        public void SetRightPreAlignImage(string filePath)
        {
            ICogImage cogImage = VisionProImageHelper.Load(filePath);
            _preAlignRunner.SetPreAlignRightImage(cogImage as CogImage8Grey);
        }

        public void UpdateLeftPreAlignResult(AppsPreAlignResult result)
        {
            _mainForm.UpdateLeftPreAlignResult(result);
        }

        public void UpdateRightPreAlignResult(AppsPreAlignResult result)
        {
            _mainForm.UpdateRightPreAlignResult(result);
        }

        public void UpdatePreAlignResult(AppsPreAlignResult result)
        {
            _mainForm.UpdatePreAlignResult(result);
        }

        public void ClearPreAlignResult()
        {
            _mainForm.ClearPreAlignResult();
        }

        public void SetManualJudgeData(List<ManualJudge> manualJudgeList)
        {
            _mainForm.SetManualJudgeData(manualJudgeList);
        }

        public void ShowManualJugdeForm()
        {
            _mainForm.ShowManualJudgeForm();
        }

        public bool PlcScenarioMoveTo(PlcCommand plcCommand, TeachingPosType teachingPosType, out string alarmMessage)
        {
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            alarmMessage = "";

            if (inspModel == null)
            {
                alarmMessage = "Current Model is null.";
                Logger.Write(LogType.Device, alarmMessage);
                return false;
            }

            var teachingInfo = inspModel.GetUnit(UnitName.Unit0).GetTeachingInfo(teachingPosType);

            #region AxisX
            Axis axisX = MotionManager.Instance().GetAxis(AxisHandlerName.Handler0, AxisName.X);
            if (axisX.IsEnable() == false)
            {
                alarmMessage = "AxisX Servo Off. Please Servo On.";
                Logger.Write(LogType.Device, alarmMessage);
                return false;
            }

            if (MotionManager.Instance().MoveAxisX(AxisHandlerName.Handler0, UnitName.Unit0, teachingPosType) == false)
            {
                alarmMessage = "AxisX Moving Error.";
                Logger.Write(LogType.Device, alarmMessage);
                return false;
            }
            #endregion

            #region AxisZ0
            Axis axisZ0 = MotionManager.Instance().GetAxis(AxisHandlerName.Handler0, AxisName.Z0);

            if (PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PLC_AlignZ_ServoOnOff).Value != "1")
            {
                alarmMessage = "Axis Z Servo Off.";
                Logger.Write(LogType.Device, alarmMessage);
                return false;
            }
            var lafCtrl = LAFManager.Instance().GetLAF("Laf").LafCtrl;
            if (MotionManager.Instance().MoveAxisZ(UnitName.Unit0, teachingPosType, lafCtrl, AxisName.Z0) == false)
            {
                alarmMessage = "Axis Z Moving Error.";
                Logger.Write(LogType.Device, alarmMessage);
                return false;
            }
            #endregion

            return true;
        }
        #endregion
    }

    public enum ProgramType
    {
        ProgramType_1 = 0, // UT Remodeling PC #1 (출하일 : 2023.08 )
    }

    // All Axes Homing
    public partial class SystemManager
    {
        #region 필드
        private bool _isAxisHoming { get; set; }

        private Axis _currentHomingAxis { get; set; }
        #endregion

        #region 메소드
        public bool AxisHoming(AxisName axisName)
        {
            int timeOutSec = 40;
            _currentHomingAxis = MotionManager.Instance().GetAxis(AxisHandlerName.Handler0, axisName);
            _currentHomingAxis.StartHome();
            _isAxisHoming = true;

            Stopwatch stopwatch = Stopwatch.StartNew();
            while (_isAxisHoming == true && _currentHomingAxis.IsHomeFound == false && stopwatch.Elapsed.Seconds < timeOutSec)
            {
                if (_currentHomingAxis.IsMoving() == false)
                    _currentHomingAxis.IsHomeFound = true;
                else
                    Thread.Sleep(50);
            }

            StopAxisHoming();
            return _currentHomingAxis.IsHomeFound;
        }

        public void StopAxisHoming()
        {
            _isAxisHoming = false;
            _currentHomingAxis?.StopMove();
        }
        #endregion
    }

}
