using ATT_UT_IPAD.Core;
using ATT_UT_IPAD.Core.Data;
using ATT_UT_IPAD.UI.Controls;
using Cognex.VisionPro;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Service;
using Jastech.Apps.Winform.Service.Plc.Maps;
using Jastech.Apps.Winform.Settings;
using Jastech.Apps.Winform.UI.Forms;
using Jastech.Framework.Config;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Device.LightCtrls;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Device.Plcs;
using Jastech.Framework.Structure;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform;
using Jastech.Framework.Winform.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;

namespace ATT_UT_IPAD
{
    public partial class SystemManager
    {
        #region 필드
        private static SystemManager _instance = null;

        private MainForm _mainForm = null;

        private ATTInspRunner _inspRunner = new ATTInspRunner();
        #endregion

        #region 속성
      
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
                message = "Camera Initialize Fail";
            else if (deviceType == typeof(Motion))
                message = "Motion Initialize Fail";
            else if (deviceType == typeof(LightCtrl))
                message = "LightCtrl Initialize Fail";
            else if (deviceType == typeof(LAFCtrl))
                message = "LAF Initialize Fail";
            else if (deviceType == typeof(Plc))
                message = "Plc Initialize Fail";

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

            string dir = Path.Combine(ConfigSet.Instance().Path.Config, "AxisHandler");

            if (Directory.Exists(dir) == false)
                Directory.CreateDirectory(dir);

            string unit0FileName = string.Format("AxisHandler_{0}.json", AxisHandlerName.Handler0);
            string unit0FilePath = Path.Combine(dir, unit0FileName);
            if (File.Exists(unit0FilePath) == false)
            {
                AxisHandler handler = new AxisHandler();

                var programType = StringHelper.StringToEnum<ProgramType>(AppsConfig.Instance().ProgramType);
                switch (programType)
                {
                    case ProgramType.ProgramType_1:
                        AddAxisHandlerType1(motion, out handler);
                        break;
                    case ProgramType.ProgramType_2:
                        AddAxisHandlerType2(motion, out handler);
                        break;
                }

                MotionManager.Instance().AxisHandlerList.Add(handler);
                JsonConvertHelper.Save(unit0FilePath, handler);
            }
            else
            {
                AxisHandler unit0 = new AxisHandler();
                JsonConvertHelper.LoadToExistingTarget<AxisHandler>(unit0FilePath, unit0);

                foreach (var axis in unit0.GetAxisList())
                    axis.SetMotion(motion);

                MotionManager.Instance().AxisHandlerList.Add(unit0);
            }
            return true;
        }

        private void AddAxisHandlerType1(Motion motion, out AxisHandler handler)
        {
            handler = new AxisHandler(AxisHandlerName.Handler0.ToString());

            handler.AddAxis(AxisName.X, motion, axisNo: 0, homeOrder: 2);
            handler.AddAxis(AxisName.Z0, motion, axisNo: -1, homeOrder: 1);
            handler.AddAxis(AxisName.Z1, motion, axisNo: -1, homeOrder: 1);
        }
        private void AddAxisHandlerType2(Motion motion, out AxisHandler handler)
        {
            handler = new AxisHandler(AxisHandlerName.Handler0.ToString());

            handler.AddAxis(AxisName.X, motion, axisNo: 1, homeOrder: 2);
            handler.AddAxis(AxisName.Z0, motion, axisNo: -1, homeOrder: 1);
            handler.AddAxis(AxisName.Z1, motion, axisNo: -1, homeOrder: 1);
        }

        public void UpdateAkkonResultTabButton(int tabNo)
        {
            _mainForm.UpdateAkkonResultTabButton(tabNo);
        }

        public void UpdateAlignResultTabButton(int tabNo)
        {
            _mainForm.UpdateAlignResultTabButton(tabNo);
        }


        //public void UpdateMainResult()
        //{
        //    var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

        //    for (int tabNo = 0; tabNo < inspModel.TabCount; tabNo++)
        //    {
        //        _mainForm.UpdateMainAkkonResult(tabNo);
        //        _mainForm.UpdateMainAlignResult(tabNo);
        //    }
        //}

        public void UpdateMainAkkonResult()
        {
            Stopwatch sw = new Stopwatch();
            sw.Restart();

            // 탭별로 안들어올 수도 있을텐데....
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            for (int tabNo = 0; tabNo < inspModel.TabCount; tabNo++)
                _mainForm.UpdateMainAkkonResult(tabNo);

            sw.Stop();
        }

        public void EnableMainView(bool isEnable)
        {
            _mainForm.Enable(isEnable);
        }

        public void UpdateMainAlignResult()
        {
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            for (int tabNo = 0; tabNo < inspModel.TabCount; tabNo++)
                _mainForm.UpdateMainAlignResult(tabNo);
        }

        public void TabButtonResetColor()
        {
            _mainForm.TabButtonResetColor();
        }

        public void AddSystemLogMessage(string logMessage)
        {
            _mainForm.AddSystemLogMessage(logMessage);
        }

        public void StartRun()
        {
            if(ConfigSet.Instance().Operation.VirtualMode == false)
            {
                if (PlcControlManager.Instance().IsDoorOpened == true)
                {
                    MessageConfirmForm alert = new MessageConfirmForm();
                    alert.Message = "Safety Doorlock is opened.\r\nPlease check the doorlock state";
                    alert.ShowDialog();
                    return;
                }
            
                bool isServoOnAxisX = MotionManager.Instance().IsEnable(AxisHandlerName.Handler0, AxisName.X);
                if(isServoOnAxisX == false)
                {
                    MessageConfirmForm alert = new MessageConfirmForm();
                    alert.Message = "AxisX Servo Off. Please Servo On.";
                    alert.ShowDialog();
                    return;
                }
                else
                {
                    //Check : Command Position 만들고 주석 해제
                    Axis axisX = MotionManager.Instance().GetAxis(AxisHandlerName.Handler0, AxisName.X);

                    //if(Math.Abs(axisX.GetActualPosition() - axisX.GetCommandPosition()) <= 1)
                    //{
                    //    MessageConfirmForm confirmForm = new MessageConfirmForm();
                    //    confirmForm.Message = "AxisX Position is incorrect. Please Run Homming Sequence.";
                    //    confirmForm.ShowDialog();
                    //    return;
                    //}
                }

                bool isServoOnAxisZ0 = MotionManager.Instance().IsEnable(AxisHandlerName.Handler0, AxisName.Z0);
                if (isServoOnAxisZ0 == false)
                {
                    MessageConfirmForm alert = new MessageConfirmForm();
                    alert.Message = "AxisZ0 Servo Off. Please Servo On.";
                    alert.ShowDialog();
                    return;
                }

                Axis axisZ0 = MotionManager.Instance().GetAxis(AxisHandlerName.Handler0, AxisName.Z0);
            }
            
            if (PlcControlManager.Instance().MachineStatus != MachineStatus.RUN)
            {
                MessageYesNoForm form = new MessageYesNoForm();
                form.Message = "Do you want to Start Auto Mode?";

                if (form.ShowDialog() == DialogResult.Yes)
                    SetRunMode();
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
                    SetStopMode();
            }
        }

        public void SetRunMode()
        {
            _inspRunner.SeqRun();
            AddSystemLogMessage("Start Auto mode.");
        }

        public void SetStopMode()
        {
            _inspRunner.SeqStop();
            AddSystemLogMessage("Stop Auto Mode.");
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

        // 정환
        public void SetAkkonLastScanImage(ICogImage cogImage)
        {
            _mainForm.SetAkkonLastScanImage(cogImage);
        }

        public ICogImage GetAkkonLastScanImage()
        {
            return _mainForm.GetAkkonLastScanImage();
        }

        public void ReleaseAkkonLastScanImage()
        {
            _mainForm.ReleaseAkkonLastScanImage();
        }

        public void SetAlignLastScanImage(ICogImage cogImage)
        {
            _mainForm.SetAlignLastScanImage(cogImage);
        }

        public ICogImage GetAlignLastScanImage()
        {
            return _mainForm.GetAlignLastScanImage();
        }

        public void ReleaseAlignLastScanImage()
        {
            _mainForm.ReleaseAlignLastScanImage();
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

            if(MotionManager.Instance().MoveAxisX(AxisHandlerName.Handler0, UnitName.Unit0, teachingPosType) == false)
            {
                alarmMessage = "AxisX Moving Error.";
                Logger.Write(LogType.Device, alarmMessage);
                return false;
            }
            #endregion

            #region AxisZ0(Akkon)
            Axis axisZ0 = MotionManager.Instance().GetAxis(AxisHandlerName.Handler0, AxisName.Z0);

            if(PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PLC_AkkonZ_ServoOnOff).Value != "1")
            {
                alarmMessage = "Akkon AxisZ Servo Off.";
                Logger.Write(LogType.Device, alarmMessage);
                return false;
            }
            var akkonLAFCtrl = LAFManager.Instance().GetLAF("AkkonLaf").LafCtrl;
            if(MotionManager.Instance().MoveAxisZ(UnitName.Unit0, teachingPosType, akkonLAFCtrl, AxisName.Z0) == false)
            {
                alarmMessage = "Akkon AxisZ Moving Error.";
                Logger.Write(LogType.Device, alarmMessage);
                return false;
            }
            #endregion

            #region AxisZ1(Align)
            Axis axisZ1 = MotionManager.Instance().GetAxis(AxisHandlerName.Handler0, AxisName.Z1);

            if (PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PLC_AlignZ_ServoOnOff).Value != "1")
            {
                alarmMessage = "Align AxisZ Servo Off.";
                Logger.Write(LogType.Device, alarmMessage);
                return false;
            }
            var alignLAFCtrl = LAFManager.Instance().GetLAF("AlignLaf").LafCtrl;
            if (MotionManager.Instance().MoveAxisZ(UnitName.Unit0, teachingPosType, alignLAFCtrl, AxisName.Z1) == false)
            {
                alarmMessage = "Align AxisZ Moving Error.";
                Logger.Write(LogType.Device, alarmMessage);
                return false;
            }
            #endregion

            return true;
        }

        public void MessageConfirm(string message)
        {
            _mainForm.MessageConfirm(message);
        }
        #endregion
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

    public enum ProgramType
    {
        ProgramType_1, // UT IPAD PC #1 (출하일 : 2023.08 )
        ProgramType_2, // UT IPAD PC #2 (출하일 : 2023.08 )
    }
}
