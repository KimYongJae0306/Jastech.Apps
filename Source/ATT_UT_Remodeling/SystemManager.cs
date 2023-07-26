using ATT_UT_Remodeling.Core;
using Cognex.VisionPro;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Service;
using Jastech.Apps.Winform.Service.Plc;
using Jastech.Apps.Winform.Service.Plc.Maps;
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
using System.IO;
using System.Windows.Forms;

namespace ATT_UT_Remodeling
{
    public class SystemManager
    {
        #region 필드
        private static SystemManager _instance = null;

        private MainForm _mainForm = null;

        private ATTInspRunner _inspRunner = new ATTInspRunner();

        private PreAlignRunner _preAlignRunner = new PreAlignRunner();
        #endregion

        #region 속성
        public MachineStatus MachineStatus { get; set; } = MachineStatus.STOP;
        #endregion

        #region 메서드
        public static SystemManager Instance()
        {
            if (_instance == null)
            {
                _instance = new SystemManager();
            }

            return _instance;
        }

        public bool Initialize(MainForm mainForm)
        {
            _mainForm = mainForm;

            Logger.Write(LogType.System, "Init SplashForm");

            SplashForm form = new SplashForm();

            form.Title = "ATT Inspection";
            form.Version = ConfigSet.Instance().Operation.SystemVersion;
            form.SetupActionEventHandler = SplashSetupAction;

            form.ShowDialog();

            DailyInfoService.Load();

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

            CreateAxisHanlder();

            percent = 80;
            DoReportProgress(reportProgress, percent, "Initialize Manager.");
            LAFManager.Instance().Initialize();
            LineCameraManager.Instance().Initialize();
            AreaCameraManager.Instance().Initialize();
           
            var areaCamera = AreaCameraManager.Instance().GetAppsCamera("PreAlign");
            if (areaCamera != null)
                PlcScenarioManager.Instance().VisionXCalibration.SetCamera(areaCamera);

            var axisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
            if (axisHandler != null)
                PlcScenarioManager.Instance().VisionXCalibration.SetAxisHandler(axisHandler);

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

            if(message != "")
            {
                MessageYesNoForm form = new MessageYesNoForm();
                form.Message = message;
                form.ShowDialog();
            }
        }

        public bool CreateAxisHanlder()
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

        public void UpdateMainResult(AppsInspResult result)
        {
            _mainForm.UpdateMainResult(result);
        }

        public void AddSystemLogMessage(string logMessage)
        {
            _mainForm.AddSystemLogMessage(logMessage);
        }

        public void StartRun()
        {
            if (MachineStatus != MachineStatus.RUN)
            {
                MessageYesNoForm form = new MessageYesNoForm();
                form.Message = "Do you want to Start Auto Mode?";
                if (form.ShowDialog() == DialogResult.Yes)
                {
                    _preAlignRunner.SeqRun();
                    _inspRunner.SeqRun();
                    AddSystemLogMessage("Start Auto mode.");
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

            if (MachineStatus != MachineStatus.STOP)
            {
                MessageYesNoForm form = new MessageYesNoForm();
                form.Message = "Do you want to Stop Auto Mode?";

                if (form.ShowDialog() == DialogResult.Yes)
                {
                    _preAlignRunner.SeqStop();
                    _inspRunner.SeqStop();
                    AddSystemLogMessage("Stop Auto Mode.");
                }
            }
        }

        public void SetVirtualImage(int tabNo, string fileName)
        {
            _inspRunner.SetVirtualmage(tabNo, fileName);
        }

        public void VirtualGrabDone()
        {
            _inspRunner.VirtualGrabDone();
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
        #endregion
    }

    public enum ProgramType
    {
        ProgramType_1 = 0, // UT Remodeling PC #1 (출하일 : 2023.08 )
    }
}
