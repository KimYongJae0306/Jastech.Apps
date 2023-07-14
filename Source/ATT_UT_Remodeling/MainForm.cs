using ATT_UT_Remodeling.Core;
using ATT_UT_Remodeling.UI.Pages;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Service.Plc;
using Jastech.Apps.Winform.Service.Plc.Maps;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Config;
using Jastech.Framework.Device.Grabbers;
using Jastech.Framework.Matrox;
using Jastech.Framework.Structure;
using Jastech.Framework.Users;
using Jastech.Framework.Winform;
using Jastech.Framework.Winform.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ATT_UT_Remodeling
{
    public partial class MainForm : Form
    {
        #region 속성
        private MainPage MainPageControl { get; set; } = null;

        private DataPage DataPageControl { get; set; } = null;

        private LogPage LogPageControl { get; set; } = null;

        private TeachingPage TeachingPageControl { get; set; } = null;

        private List<UserControl> PageControlList = null;

        private List<Label> PageLabelList = null;

        public ATTInspModelService ATTInspModelService { get; set; } = new ATTInspModelService();
        #endregion

        #region 생성자
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void MainForm_Load(object sender, EventArgs e)
        {
            ModelManager.Instance().CurrentModelChangedEvent += MainForm_CurrentModelChangedEvent;
            PlcScenarioManager.Instance().Initialize(ATTInspModelService);
            PlcScenarioManager.Instance().InspRunnerHandler += MainForm_InspRunnerHandler;
            PlcScenarioManager.Instance().PreAlignRunnerHandler += MainForm_PreAlignRunnerHandler;
            if (ModelManager.Instance().CurrentModel != null)
            {
                lblCurrentModel.Text = ModelManager.Instance().CurrentModel.Name;
                ModelManager.Instance().ApplyChangedEvent();
            }

            AddControls();
            SelectMainPage();

            tmrMainForm.Start();
        }

        private void MainForm_PreAlignRunnerHandler(bool tt)
        {
            AppsStatus.Instance().IsPreAlignRunnerFlagFromPlc = tt;
        }

        private void MainForm_InspRunnerHandler(bool tt)
        {
            AppsStatus.Instance().IsInspRunnerFlagFromPlc = tt;
        }

        private void AddControls()
        {
            // Page Control List
            PageControlList = new List<UserControl>();

            MainPageControl = new MainPage();
            MainPageControl.Dock = DockStyle.Fill;
            PageControlList.Add(MainPageControl);

            DataPageControl = new DataPage();
            DataPageControl.Dock = DockStyle.Fill;
            PageControlList.Add(DataPageControl);

            LogPageControl = new LogPage();
            LogPageControl.Dock = DockStyle.Fill;
            PageControlList.Add(LogPageControl);

            TeachingPageControl = new TeachingPage();
            TeachingPageControl.Dock = DockStyle.Fill;
            PageControlList.Add(TeachingPageControl);

            TeachingPageControl.SetInspModelService(ATTInspModelService);

            DataPageControl.SetInspModelService(ATTInspModelService);
            DataPageControl.ApplyModelEventHandler += ModelPageControl_ApplyModelEventHandler;

            // Button List
            PageLabelList = new List<Label>();
            PageLabelList.Add(lblMainPage);
            PageLabelList.Add(lblTeachingPage);
            PageLabelList.Add(lblDataPage);
            PageLabelList.Add(lblLogPage);
        }

        private void ModelPageControl_ApplyModelEventHandler(string modelName)
        {
            string modelDir = ConfigSet.Instance().Path.Model;
            string filePath = Path.Combine(modelDir, modelName, InspModel.FileName);

            ModelManager.Instance().CurrentModel = ATTInspModelService.Load(filePath);
            SelectMainPage();

            lblCurrentModel.Text = modelName;

            ConfigSet.Instance().Operation.LastModelName = modelName;
            ConfigSet.Instance().Operation.Save(ConfigSet.Instance().Path.Config);
        }

        private void SelectMainPage()
        {
            SetSelectLabel(lblMainPage);
            SetSelectPage(MainPageControl);
        }

        private void SetSelectLabel(object sender)
        {
            foreach (Label label in PageLabelList)
            {
                label.ForeColor = Color.White;
            }

            Label currentLabel = sender as Label;
            currentLabel.ForeColor = Color.Blue;
        }

        private void SetSelectPage(UserControl selectedControl)
        {
            foreach (UserControl control in PageControlList)
                control.Visible = false;

            selectedControl.Visible = true;
            selectedControl.Dock = DockStyle.Fill;
            pnlPage.Controls.Add(selectedControl);
        }

        private void MainForm_CurrentModelChangedEvent(InspModel inspModel)
        {
            AppsInspModel model = inspModel as AppsInspModel;

            MainPageControl.UpdateTabCount(model.TabCount);

            lblCurrentModel.Text = model.Name;

            ConfigSet.Instance().Operation.LastModelName = model.Name;
            ConfigSet.Instance().Operation.Save(ConfigSet.Instance().Path.Config);
        }

        private void lblMainPage_Click(object sender, EventArgs e)
        {
            SetSelectLabel(sender);
            SetSelectPage(MainPageControl);
        }

        private void lblTeachingPage_Click(object sender, EventArgs e)
        {
            if (ModelManager.Instance().CurrentModel == null)
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "Current Model is null.";
                form.ShowDialog();
                return;
            }

            SetSelectLabel(sender);
            SetSelectPage(TeachingPageControl);
        }

        private void lblDataPage_Click(object sender, EventArgs e)
        {
            SetSelectLabel(sender);
            SetSelectPage(DataPageControl);
        }

        private void lblLogPage_Click(object sender, EventArgs e)
        {
            LogForm logForm = new LogForm();

            string logPath = ConfigSet.Instance().Path.Log;
            string resultPath = ConfigSet.Instance().Path.Result;
            string modelName = ConfigSet.Instance().Operation.LastModelName;

            logForm.SetLogViewPath(logPath, resultPath, modelName);
            logForm.ShowDialog();
        }

        private void tmrMainForm_Tick(object sender, EventArgs e)
        {
            if (AppsConfig.Instance().EnablePlcTime)
            {
                // Model Info
                var manager = PlcControlManager.Instance();

                string yyyy = manager.GetValue(PlcCommonMap.PLC_Time_Year);
                string MM = manager.GetValue(PlcCommonMap.PLC_Time_Month);
                string dd = manager.GetValue(PlcCommonMap.PLC_Time_Day);
                string hh = manager.GetValue(PlcCommonMap.PLC_Time_Hour);
                string mm = manager.GetValue(PlcCommonMap.PLC_Time_Minute);
                string ss = manager.GetValue(PlcCommonMap.PLC_Time_Second);
                lblCurrentTime.Text = string.Format("{0}-{1}-{2} {3}:{4}:{5}", yyyy, MM, dd, hh, mm, ss);
            }
            else
            {
                DateTime now = DateTime.Now;
                lblCurrentTime.Text = now.ToString("yyyy-MM-dd HH:mm:ss");
            }

            if (lblCurrentTime.Text != "")
                AppsStatus.Instance().CurrentTime = Convert.ToDateTime(lblCurrentTime.Text);

            var user = UserManager.Instance().CurrentUser;

            if (user.Type == AuthorityType.None)
            {
                lblCurrentUser.Text = "Operator";
                lblTeachingPage.Enabled = false;
                lblTeachingPageImage.Enabled = false;
            }
            else
            {
                lblCurrentUser.Text = user.Id.ToString();
                lblTeachingPage.Enabled = true;
                lblTeachingPageImage.Enabled = true;
            }

            if (MainPageControl.Visible)
                MainPageControl.UpdateButton();
        }

        public void UpdateMainResult(AppsInspResult result)
        {
            MainPageControl.UpdateMainResult(result);
        }

        public void AddSystemLogMessage(string logMessage)
        {
            MainPageControl.AddSystemLogMessage(logMessage);
        }

        private void lblCurrentUser_Click(object sender, EventArgs e)
        {
            LoginForm form = new LoginForm();
            form.CurrentUser = UserManager.Instance().CurrentUser;
            form.UserHandler = UserManager.Instance().UserHanlder;
            form.StopProgramEvent += StopProgramEventFunction;
            form.ShowDialog();

            UserManager.Instance().SetCurrentUser(form.CurrentUser.Id);
        }

        private void StopProgramEventFunction()
        {
            this.Close();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            DeviceManager.Instance().Release();
            GrabberMil.Release();
            MilHelper.FreeApplication();
            Application.ExitThread();
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmrMainForm.Stop();

            LAFManager.Instance().Release();
            PlcControlManager.Instance().Release();
        }
        #endregion
    }
}
