using ATT_UT_IPAD.Core;
using ATT_UT_IPAD.Core.Data;
using ATT_UT_IPAD.Properties;
using ATT_UT_IPAD.UI.Pages;
using Cognex.VisionPro;
using Jastech.Apps.Structure;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Service.Plc;
using Jastech.Apps.Winform.Service.Plc.Maps;
using Jastech.Apps.Winform.Settings;
using Jastech.Apps.Winform.UI.Forms;
using Jastech.Framework.Config;
using Jastech.Framework.Device.Grabbers;
using Jastech.Framework.Matrox;
using Jastech.Framework.Structure;
using Jastech.Framework.Users;
using Jastech.Framework.Winform;
using Jastech.Framework.Winform.Forms;
using Jastech.Framework.Winform.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATT_UT_IPAD
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

        private Queue<ICogImage> AlignLastScanImageQueue = new Queue<ICogImage>();
        private Queue<ICogImage> AkkonLastScanImageQueue = new Queue<ICogImage>();
        public ATTInspModelService ATTInspModelService { get; set; } = new ATTInspModelService();

        public ManualJudgeForm ManualJudgeForm { get; set; } = null;
        #endregion

        #region 델리게이트
        private delegate void UpdateLabelDelegate(string modelname);
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
            if (UserManager.Instance().CurrentUser.Type == AuthorityType.Maker)
                this.Text = " ";
            
            lblMachineName.Text = AppsConfig.Instance().MachineName;

            AddControls();
            SelectMainPage();

            TeachingPageControl.SetInspModelService(ATTInspModelService);
            DataPageControl.SetInspModelService(ATTInspModelService);
            DataPageControl.ApplyModelEventHandler += ModelPageControl_ApplyModelEventHandler;

            ModelManager.Instance().CurrentModelChangedEvent += MainForm_CurrentModelChangedEvent;
            PlcScenarioManager.Instance().Initialize(ATTInspModelService);
            PlcScenarioManager.Instance().InspRunnerHandler += MainForm_InspRunnerHandler;

            PlcControlManager.Instance().WritePcCommand(PcCommand.ServoOn_1);

            if (ModelManager.Instance().CurrentModel != null)
            {
                lblCurrentModel.Text = ModelManager.Instance().CurrentModel.Name;
                ModelManager.Instance().ApplyChangedEvent();
            }

            tmrMainForm.Start();
            tmrUpdateStates.Start();
            StartVirtualInspTask();
            SystemManager.Instance().InitializeInspRunner();
            SystemManager.Instance().AddSystemLogMessage("Start Program.");

            PlcControlManager.Instance().WriteVersion();

            ManualJudgeForm = new ManualJudgeForm();
            ManualJudgeForm.Show();
        }

        private void MainForm_InspRunnerHandler(bool isStart)
        {
            AppsStatus.Instance().IsInspRunnerFlagFromPlc = isStart;
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
            UpdateLabel(modelName);            

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
                label.ForeColor = Color.White;

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

            AppsInspResult.Instance().Dispose();

            MainPageControl.MainViewControl?.UpdateTabCount(model.TabCount);

            UpdateLabel(model.Name);
            ConfigSet.Instance().Operation.LastModelName = model.Name;
            ConfigSet.Instance().Operation.Save(ConfigSet.Instance().Path.Config);
        }
     
        private void UpdateLabel(string modelname)
        {
            if(this.InvokeRequired)
            {
                UpdateLabelDelegate callback = UpdateLabel;
                BeginInvoke(callback, modelname);
                return;
            }
            lblCurrentModel.Text = modelname;
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
                //lblTeachingPage.Enabled = false;
                //lblTeachingPageImage.Enabled = false;
            }
            else
            {
                lblCurrentUser.Text = user.Id.ToString();
                //lblTeachingPage.Enabled = true;
                //lblTeachingPageImage.Enabled = true;
            }

            if (MainPageControl.Visible)
                MainPageControl.UpdateButton();

            if (PlcControlManager.Instance().MachineStatus == MachineStatus.RUN)
            {
                lblTeachingPageImage.Enabled = false;
                lblTeachingPage.Enabled = false;
                lblDataPageImage.Enabled = false;
                lblDataPage.Enabled = false;
                lblLogPage.Enabled = false;
            }
            else
            {
                if(user.Type == AuthorityType.None)
                {
                    lblTeachingPage.Enabled = false;
                    lblTeachingPageImage.Enabled = false;
                }
                else
                {
                    lblTeachingPage.Enabled = true;
                    lblTeachingPageImage.Enabled = true;
                }

                lblDataPageImage.Enabled = true;
                lblDataPage.Enabled = true;
                lblLogPage.Enabled = true;
            }
        }

        private void tmrUpdateStates_Tick(object sender, EventArgs e)
        {
            var plc = DeviceManager.Instance().PlcHandler;
            bool isPlcConnected = plc.Count > 0 && plc.All(h => h.IsConnected());
            ControlDisplayHelper.DisposeDisplay(lblPLCState);
            lblPLCState.Image = GetStateImage(isPlcConnected);

            var motion = DeviceManager.Instance().MotionHandler;
            bool isMotionConnected = motion.Count > 0 && motion.All(h => h.IsConnected());
            ControlDisplayHelper.DisposeDisplay(lblMotionState);
            lblMotionState.Image = GetStateImage(isMotionConnected);

            //bool isCognexLicenseNormal = Cognex.VisionPro.CogLicense.GetLicensedFeatures(false, false).Count != 0;
            //ControlDisplayHelper.DisposeDisplay(lblLicenseState);
            //lblLicenseState.Image = GetStateImage(isCognexLicenseNormal);

            var laf = DeviceManager.Instance().LAFCtrlHandler;
            bool isLafConnected = laf.Count > 0 && laf.All(h => h.IsConnected());
            ControlDisplayHelper.DisposeDisplay(lblLafState);
            lblLafState.Image = GetStateImage(isLafConnected);
            
            var light = DeviceManager.Instance().LightCtrlHandler;
            bool isLightConnected = light.Count > 0 && light.All(h => h.IsConnected());
            ControlDisplayHelper.DisposeDisplay(lblLightState);
            lblLightState.Image = GetStateImage(isLightConnected);
        }

        public void UpdateMainAkkonResult(int tabNo)
        {
            MainPageControl.MainViewControl.UpdateMainAkkonResultData(tabNo);
        }

        public void UpdateMainAlignResult(int tabNo)
        {
            MainPageControl.MainViewControl.UpdateMainAlignResult(tabNo);
        }

        public void UpdateAkkonResultTabButton(int tabNo)
        {
            MainPageControl.MainViewControl.UpdateAkkonResultTabButton(tabNo);
        }

        public void UpdateAlignResultTabButton(int tabNo)
        {
            MainPageControl.MainViewControl.UpdateAlignResultTabButton(tabNo);
        }

        public void TabButtonResetColor()
        {
            MainPageControl.MainViewControl.TabButtonResetColor();
        }

        public void AddSystemLogMessage(string logMessage)
        {
            MainPageControl.MainViewControl.AddSystemLogMessage(logMessage);
        }

        private void lblCurrentUser_Click(object sender, EventArgs e)
        {
            LoginForm form = new LoginForm();
            form.CurrentUser = UserManager.Instance().CurrentUser;
            form.UserHandler = UserManager.Instance().UserHandler;
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
            tmrUpdateStates.Stop();
            StopVirtualInspTask();

            SystemManager.Instance().ReleaseInspRunner();
            SystemManager.Instance().StopRun();

            LAFManager.Instance().Release();
            PlcControlManager.Instance().Release();
            PlcScenarioManager.Instance().Release();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys key = keyData & ~(Keys.Shift | Keys.Control);

            if (ConfigSet.Instance().Operation.VirtualMode)
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
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected void LoadTabImage()
        {
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            if (inspModel == null)
                return;

            if (PlcControlManager.Instance().MachineStatus != MachineStatus.RUN)
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

        private void LoadImage(string[] fileNames)
        {
            foreach (var fileName in fileNames)
                AddVirtualImagePath(fileName);
        }

        private void AddVirtualImagePath(string filePath)
        {
            lock (VirtualImagePathQueue)
                VirtualImagePathQueue.Enqueue(filePath);
        }

        public void SetAkkonLastScanImage(ICogImage cogImage)
        {
            lock (AkkonLastScanImageQueue)
                AkkonLastScanImageQueue.Enqueue(cogImage);
        }

        public ICogImage GetAkkonLastScanImage()
        {
            lock (AkkonLastScanImageQueue)
            {
                if (AkkonLastScanImageQueue.Count > 0)
                    return AkkonLastScanImageQueue.Dequeue();
                else
                    return null;
            }
        }

        public void ReleaseAkkonLastScanImage()
        {
            AkkonLastScanImageQueue.Clear();
        }

        public void SetAlignLastScanImage(ICogImage cogImage)
        {
            lock (AlignLastScanImageQueue)
                AlignLastScanImageQueue.Enqueue(cogImage);
        }

        public ICogImage GetAlignLastScanImage()
        {
            lock (AlignLastScanImageQueue)
            {
                if (AlignLastScanImageQueue.Count > 0)
                    return AlignLastScanImageQueue.Dequeue();
                else
                    return null;
            }
        }

        public void ReleaseAlignLastScanImage()
        {
            AlignLastScanImageQueue.Clear();
        }

        private void StartVirtualInspTask()
        {
            if (ConfigSet.Instance().Operation.VirtualMode)
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
            while (true)
            {
                if (CancelVirtualInspTask.IsCancellationRequested)
                    break;

                if (GetVirtualImagePath() is string filePath)
                {
                    string text = "TAB_";
                    string fileName = Path.GetFileNameWithoutExtension(filePath).ToUpper();
                    fileName = fileName.Replace("_OK", "");
                    fileName = fileName.Replace("_NG", "");
                    int index = fileName.IndexOf(text);
                    if (index < 0)
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
                    if (_virtualImageCount == inspModel.TabCount)
                    {
                        AppsStatus.Instance().IsInspRunnerFlagFromPlc = true;
                    }
                }

                Thread.Sleep(50);
            }
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

        private Image GetStateImage(bool isNormalState)
        {
            return isNormalState ? Resources.Circle_Green : Resources.Circle_Red;
        }

        private void picLogo_Click(object sender, EventArgs e)
        {
            if (PlcControlManager.Instance().MachineStatus != MachineStatus.RUN)
                return;

            if(AppsStatus.Instance().IsInspRunnerFlagFromPlc == false)
            {
                MessageYesNoForm form = new MessageYesNoForm();
                form.Message = "Would you like to do a test run?";
                if (form.ShowDialog() == DialogResult.Yes)
                {
                    AppsStatus.Instance().IsInspRunnerFlagFromPlc = true;
                }
            }
        }

        public void Enable(bool isEnable)
        {
            MainPageControl?.Enable(isEnable);
        }

        public void ShowManualJudgeForm(string message)
        {
            ManualJudgeForm.SetMessage(message);
            ManualJudgeForm.Show();
        }

        #endregion

        private void lblMachineName_Click(object sender, EventArgs e)
        {
            Process.Start(ConfigSet.Instance().Path.Result);
        }
    }
}
