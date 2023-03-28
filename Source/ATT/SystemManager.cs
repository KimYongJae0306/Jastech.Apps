using ATT.Core;
using Jastech.Apps.Structure;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Device.Cameras;
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
            
            LogHelper.Write(LogType.SYSTEM, "Init SplashForm");

            SplashForm form = new SplashForm();

            form.Title = "ATT Inspection";
            form.Version = AppConfig.Instance().Operation.SystemVersion;
            form.SetupActionEventHandler = SplashSetupAction;

            form.ShowDialog();

            return true;
        }

        private bool SplashSetupAction(IReportProgress reportProgress)
        {
            LogHelper.Write(LogType.SYSTEM, "Initialize Device");

            int percent = 0;
            DoReportProgress(reportProgress, percent, "Initialize Device");
            DeviceManager.Instance().Initialized += SystemManager_Initialized;
            DeviceManager.Instance().Initialize(AppConfig.Instance());

            percent += 30;

            if (AppConfig.Instance().Operation.LastModelName != "")
            {
                string filePath = Path.Combine(AppConfig.Instance().Path.Model,
                                    AppConfig.Instance().Operation.LastModelName,
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
            else
            {

            }
            MessageYesNoForm form = new MessageYesNoForm();
            form.Message = message;
            form.ShowDialog();
        }

        private void DoReportProgress(IReportProgress reportProgress, int percentage, string message)
        {
            LogHelper.Write(LogType.SYSTEM, message);

            reportProgress?.ReportProgress(percentage, message);
        }

        public void SaveModel(string filePath, ATTInspModel inspModel)
        {
            _mainForm.ATTInspModelService.Save(filePath, inspModel);
        }
        #endregion
    }
}
