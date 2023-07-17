using ATT.Core;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Service;
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
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ATT
{
    public class SystemManager
    {
        #region 필드
        private static SystemManager _instance = null;

        private MainForm _mainForm = null;

        private ATTInspRunner _inspRunner = new ATTInspRunner();
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

            //AppsMotionManager.Instance().CreateAxisHanlder();
            CreateAxisHanlder();

            LAFManager.Instance().Initialize();
            LineCameraManager.Instance().Initialize();

            percent += 30;

            if (ConfigSet.Instance().Operation.LastModelName != "")
            {
                string filePath = Path.Combine(ConfigSet.Instance().Path.Model,
                                    ConfigSet.Instance().Operation.LastModelName,
                                    InspModel.FileName);
                if(File.Exists(filePath))
                {
                    DoReportProgress(reportProgress, percent, "Model Loading");
                    ModelManager.Instance().CurrentModel =  _mainForm.ATTInspModelService.Load(filePath);
                }
            }

            return true;
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

        private void DoReportProgress(IReportProgress reportProgress, int percentage, string message)
        {
            Logger.Write(LogType.System, message);

            reportProgress?.ReportProgress(percentage, message);
        }

        public void StartRun()
        {
            if(ModelManager.Instance().CurrentModel == null)
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "Current Model is null.";
                return;
            }

            if(SystemManager.Instance().MachineStatus != MachineStatus.RUN)
            {
                MessageYesNoForm form = new MessageYesNoForm();
                form.Message = "Do you want to Start Auto Mode?";
                if(form.ShowDialog() == DialogResult.Yes)
                {
                    _inspRunner.SeqRun();
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

            if (SystemManager.Instance().MachineStatus != MachineStatus.STOP)
            {
                MessageYesNoForm form = new MessageYesNoForm();
                form.Message = "Do you want to Stop Auto Mode?";
                if (form.ShowDialog() == DialogResult.Yes)
                {
                    _inspRunner.SeqStop();
                }
            }
        }

        public void UpdateMainResult(AppsInspResult result)
        {
            _mainForm.UpdateMainResult(result);
        }

        //public void InitializeResult(int tabCount)
        //{
        //    _mainForm.InitializeResult(tabCount);
        //}

        public void InitalizeInspTab(List<TabScanBuffer> bufferList)
        {
            _inspRunner.InitalizeInspTab(bufferList);
        }

        public bool CreateAxisHanlder()
        {
            var motion = DeviceManager.Instance().MotionHandler.FirstOrDefault();
            if (motion == null)
                return false;

            string dir = Path.Combine(ConfigSet.Instance().Path.Config, "AxisHanlder");

            if (Directory.Exists(dir) == false)
            {
                Directory.CreateDirectory(dir);
            }

            string unit0FileName = string.Format("AxisHanlder_{0}.json", AxisHandlerName.Handler0);
            string unit0FilePath = Path.Combine(dir, unit0FileName);
            if (File.Exists(unit0FilePath) == false)
            {
                AxisHandler handler0 = new AxisHandler(AxisHandlerName.Handler0.ToString());

                handler0.AddAxis(AxisName.X, motion, 0, 2);
                handler0.AddAxis(AxisName.Y, motion, 8, 1);
                handler0.AddAxis(AxisName.Z0, motion, 0, 2);

                //AxisHandlerList.Add(handler0);
                MotionManager.Instance().AxisHandlerList.Add(handler0);

                JsonConvertHelper.Save(unit0FilePath, handler0);
            }
            else
            {
                AxisHandler unit0 = new AxisHandler();
                JsonConvertHelper.LoadToExistingTarget<AxisHandler>(unit0FilePath, unit0);

                foreach (var axis in unit0.GetAxisList())
                {
                    axis.SetMotion(motion);
                }

                //AxisHandlerList.Add(unit0);
                MotionManager.Instance().AxisHandlerList.Add(unit0);
            }

            return true;
        }
        #endregion
    }
}
