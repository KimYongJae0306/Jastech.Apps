﻿using ATT_UT_Remodeling.Core;
using ATT_UT_Remodeling.UI.Pages;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Core.Calibrations;
using Jastech.Apps.Winform.Service.Plc;
using Jastech.Apps.Winform.Service.Plc.Maps;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Config;
using Jastech.Framework.Device.Cameras;
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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATT_UT_Remodeling
{
    public partial class MainForm : Form
    {
        #region 필드
        private int _virtualImageCount { get; set; } = 0;
        #endregion

        #region 속성
        private MainPage MainPageControl { get; set; } = null;

        private DataPage DataPageControl { get; set; } = null;

        private TeachingPage TeachingPageControl { get; set; } = null;

        private List<UserControl> PageControlList = null;

        private List<Label> PageLabelList = null;

        private Task VirtualInspTask { get; set; }

        private CancellationTokenSource CancelVirtualInspTask { get; set; }

        private Queue<string> VirtualImagePathQueue = new Queue<string>();

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
            AddControls();
            SelectMainPage();
            TeachingPageControl.SetInspModelService(ATTInspModelService);
            DataPageControl.SetInspModelService(ATTInspModelService);
            DataPageControl.ApplyModelEventHandler += ModelPageControl_ApplyModelEventHandler;

            ModelManager.Instance().CurrentModelChangedEvent += MainForm_CurrentModelChangedEvent;
            PlcScenarioManager.Instance().Initialize(ATTInspModelService);
            PlcScenarioManager.Instance().InspRunnerHandler += MainForm_InspRunnerHandler;
            PlcScenarioManager.Instance().PreAlignRunnerHandler += MainForm_PreAlignRunnerHandler;

            PlcControlManager.Instance().WritePcCommand(PcCommand.ServoOn_1);

            if (ModelManager.Instance().CurrentModel != null)
            {
                lblCurrentModel.Text = ModelManager.Instance().CurrentModel.Name;
                ModelManager.Instance().ApplyChangedEvent();
            }

            tmrMainForm.Start();
            StartVirtualInspTask();
            SystemManager.Instance().AddSystemLogMessage("Start Program.");
        }

        private void MainForm_PreAlignRunnerHandler(bool isStart)
        {
            AppsStatus.Instance().IsPreAlignRunnerFlagFromPlc = isStart;
        }

        private void MainForm_InspRunnerHandler(bool isStart)
        {
            AppsStatus.Instance().IsInspRunnerFlagFromPlc = isStart;
        }

        private void AddControls()
        {
            //// Page Control List
            PageControlList = new List<UserControl>();

            MainPageControl = new MainPage();
            MainPageControl.Dock = DockStyle.Fill;
            PageControlList.Add(MainPageControl);

            DataPageControl = new DataPage();
            DataPageControl.Dock = DockStyle.Fill;
            PageControlList.Add(DataPageControl);

            TeachingPageControl = new TeachingPage();
            TeachingPageControl.Dock = DockStyle.Fill;
            PageControlList.Add(TeachingPageControl);

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
            //Application.ExitThread();
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmrMainForm.Stop();
            StopVirtualInspTask();
            SystemManager.Instance().StopRun();

            LAFManager.Instance().Release();
            PlcControlManager.Instance().Release();
            PlcScenarioManager.Instance().Release();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys key = keyData & ~(Keys.Shift | Keys.Control);

            if(ConfigSet.Instance().Operation.VirtualMode)
            {
                switch (key)
                {
                    case Keys.D:
                        if ((keyData & Keys.Control) != 0)
                        {
                            LoadTabImage();
                            return true;
                        }
                        break;
                    case Keys.S:
                        if ((keyData & Keys.Control) != 0)
                        {
                            LoadPreAlignImage();
                            return true;
                        }
                        break;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected void LoadTabImage()
        {
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            if (inspModel == null)
                return;

            if(SystemManager.Instance().MachineStatus != MachineStatus.RUN)
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "Change Auto Run.";
                form.ShowDialog();
                return;
            }
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _virtualImageCount = 0;
                string[] fileNames = dialog.FileNames;

                if (inspModel.TabCount != fileNames.Count())
                {
                    MessageConfirmForm form = new MessageConfirmForm();
                    form.Message = "The number of Tabs is Different.";
                    form.ShowDialog();
                    return;
                }
                LoadImage(fileNames);
                AppsStatus.Instance().IsInspRunnerFlagFromPlc = true;
            }
        }

        private void LoadPreAlignImage()
        {
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            if (inspModel == null)
                return;

            if (SystemManager.Instance().MachineStatus != MachineStatus.RUN)
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "Change Auto Run.";
                form.ShowDialog();
                return;
            }
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] fileNames = dialog.FileNames;
                if (fileNames.Count() != 2)
                {
                    MessageConfirmForm form = new MessageConfirmForm();
                    form.Message = "PreAlign Image is 2.";
                    form.ShowDialog();
                    return;
                }

                foreach (var fileName in fileNames)
                {
                    if (fileName.Contains("_Left"))
                        SystemManager.Instance().SetLeftPreAlignImage(fileName);
                    if (fileName.Contains("_Right"))
                        SystemManager.Instance().SetRightPreAlignImage(fileName);
                }
                AppsStatus.Instance().IsPreAlignRunnerFlagFromPlc = true;
            }
        }

        private void LoadImage(string[] fileNames)
        { 
            foreach (var fileName in fileNames)
            {
                AddVirtualImagePath(fileName);
            }
        }

        private void StartVirtualInspTask()
        {
            if(ConfigSet.Instance().Operation.VirtualMode)
            {
                CancelVirtualInspTask = new CancellationTokenSource();
                VirtualInspTask = new Task(VirtualInspTaskLoop, CancelVirtualInspTask.Token);
                VirtualInspTask.Start();
            }
        }

        private void StopVirtualInspTask()
        {
            if (ConfigSet.Instance().Operation.VirtualMode)
            {
                CancelVirtualInspTask.Cancel();
                VirtualInspTask.Wait();
                VirtualInspTask = null;
            }
        }
        public void VirtualInspTaskLoop()
        {
            while(true)
            {
                if (CancelVirtualInspTask.IsCancellationRequested)
                    break;

                if (GetVirtualImagePath() is string filePath)
                {
                    string text = "Tab_";
                    string fileName = Path.GetFileNameWithoutExtension(filePath);

                    int index = fileName.IndexOf(text);
                    if(index < 0)
                    {
                        MessageConfirmForm form = new MessageConfirmForm();
                        form.Message = "The format of the file name is not correct.";
                        form.ShowDialog();
                    }
                    else
                    {
                        string tabNoString = fileName.Substring(index + text.Length);
                        SystemManager.Instance().SetVirtualImage(Convert.ToInt32(tabNoString), filePath);
                    }
                    _virtualImageCount++;

                    var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
                    if(_virtualImageCount == inspModel.TabCount)
                        SystemManager.Instance().VirtualGrabDone();
                }

                Thread.Sleep(50);
            }
        }

        private void AddVirtualImagePath(string filePath)
        {
            lock (VirtualImagePathQueue)
                VirtualImagePathQueue.Enqueue(filePath);
        }

        private string GetVirtualImagePath()
        {
            lock (VirtualImagePathQueue)
            {
                if (VirtualImagePathQueue.Count > 0)
                    return VirtualImagePathQueue.Dequeue();
                else
                    return null;
            }
        }

        #endregion
    }
}
