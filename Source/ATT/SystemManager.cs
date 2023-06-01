using ATT.Core;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Service;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Device.LightCtrls;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Structure;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform;
using Jastech.Framework.Winform.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATT
{
    public class SystemManager
    {
        #region 필드
        private static SystemManager _instance = null;

        private MainForm _mainForm = null;

        private ATTInspRunner _inspRunner = new ATTInspRunner();
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
            form.Version = AppsConfig.Instance().Operation.SystemVersion;
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
            DeviceManager.Instance().Initialize(AppsConfig.Instance());

            AppsMotionManager.Instance().CreateAxisHanlder();
            AppsLAFManager.Instance().Initialize();
            AppsLineCameraManager.Instance().Initialize();

            percent += 30;

            if (AppsConfig.Instance().Operation.LastModelName != "")
            {
                string filePath = Path.Combine(AppsConfig.Instance().Path.Model,
                                    AppsConfig.Instance().Operation.LastModelName,
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
            else if(deviceType == typeof(LightCtrl))
            {
                message = "LightCtrl Initialize Fail";
            }

            MessageYesNoForm form = new MessageYesNoForm();
            form.Message = message;
            form.ShowDialog();
        }

        private void DoReportProgress(IReportProgress reportProgress, int percentage, string message)
        {
            Logger.Write(LogType.System, message);

            reportProgress?.ReportProgress(percentage, message);
        }

        public void SaveModel(string filePath, AppsInspModel inspModel, bool isSaveVpp = true)
        {
            if (isSaveVpp)
                _mainForm.ATTInspModelService.Save(filePath, inspModel);
            else
                _mainForm.ATTInspModelService.SaveExceptVpp(filePath, inspModel);
        }

        public TeachingData GetTeachingData()
        {
            return _mainForm.TeachingData;
        }

        public void UpdateTeachingData()
        {
            _mainForm.UpdateTeachingData();
        }

        public void StartRun()
        {
            _inspRunner.SeqRun();
        }

        public void StopRun()
        {
            _inspRunner.SeqStop();
        }

        public void UpdateMainResult(AppsInspResult result)
        {
            _mainForm.UpdateMainResult(result);
        }

        public void InitializeResult(int tabCount)
        {
            _mainForm.InitializeResult(tabCount);
        }
        #endregion
    }
}
