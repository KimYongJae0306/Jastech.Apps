using ATT_UT_Remodeling.Core;
using ATT_UT_Remodeling.UI.Pages;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform;
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
        #region 필드
        private MainPage MainPageControl { get; set; } = new MainPage();

        private DataPage DataPageControl { get; set; } = new DataPage();

        private LogPage LogPageControl { get; set; } = new LogPage();

        private TeachingPage TeachingPageControl { get; set; } = new TeachingPage();

        private List<UserControl> PageControlList = null;

        private List<Label> PageLabelList = null;

        public ATTInspModelService ATTInspModelService { get; set; } = new ATTInspModelService();
        #endregion

        #region 속성
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        #endregion

        #region 메서드
        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            AddControls();
            SelectMainPage();

            ModelManager.Instance().CurrentModelChangedEvent += MainForm_CurrentModelChangedEvent;

            if (ModelManager.Instance().CurrentModel != null)
            {
                lblCurrentModel.Text = ModelManager.Instance().CurrentModel.Name;
                ModelManager.Instance().ApplyChangedEvent();
            }

            tmrMainForm.Start();
        }

        private void AddControls()
        {
            // Page Control List
            PageControlList = new List<UserControl>();
            PageControlList.Add(MainPageControl);
            PageControlList.Add(DataPageControl);
            PageControlList.Add(LogPageControl);
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
            DateTime now = DateTime.Now;
            lblCurrentTime.Text = now.ToString("yyyy-MM-dd HH:mm:ss");

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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmrMainForm.Stop();

            LAFManager.Instance().Release();
            DeviceManager.Instance().Release();
            GrabberMil.Release();
            MilHelper.FreeApplication();
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
    }
}
