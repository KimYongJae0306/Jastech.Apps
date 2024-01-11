using ATT_UT_IPAD.Core.Data;
using ATT_UT_IPAD.UI.Controls;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ATT_UT_IPAD.UI.Pages
{
    public partial class MainPage : UserControl
    {
        #region 필드
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        #endregion

        #region 속성
        public AkkonViewerControl AkkonViewerControl { get; set; } = null;

        public AlignViewerControl AlignViewerControl { get; set; } = null;

        public AlignMonitoringControl AlignMonitoringControl { get; set; } = null;

        public DailyInfoViewerControl DailyInfoViewerControl { get; set; } = null;

        public SystemLogControl SystemLogControl { get; set; } = null;
        #endregion

        #region 생성자
        public MainPage()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void MainPage_Load(object sender, EventArgs e)
        {
            AddControls();
            UpdateView();
        }

        private void AddControls()
        {
            DailyInfoViewerControl = new DailyInfoViewerControl();
            DailyInfoViewerControl.Dock = DockStyle.Fill;
            pnlDailyInfo.Controls.Add(DailyInfoViewerControl);

            AkkonViewerControl = new AkkonViewerControl();
            AkkonViewerControl.Dock = DockStyle.Fill;
            AkkonViewerControl.SetTabEventHandler += AkkonViewerControl_SetTabEventHandler;
            pnlAkkon.Controls.Add(AkkonViewerControl);

            AlignViewerControl = new AlignViewerControl();
            AlignViewerControl.Dock = DockStyle.Fill;
            AlignViewerControl.SetTabEventHandler += AlignViewerControl_SetTabEventHandler;
            pnlAlign.Controls.Add(AlignViewerControl);

            AlignMonitoringControl = new AlignMonitoringControl();
            AlignMonitoringControl.Dock = DockStyle.Fill;
            AlignMonitoringControl.SetTabEventHandler += AlignViewerControl_SetTabEventHandler;
            AlignMonitoringControl.GetTabInspResultEvent += AlignMonitoringControl_GetTabInspResultEvent;
            pnlAlignMonitoringView.Controls.Add(AlignMonitoringControl);

            SystemLogControl = new SystemLogControl();
            SystemLogControl.Dock = DockStyle.Fill;
            pnlSystemLog.Controls.Add(SystemLogControl);

            pnlMainView.Dock = DockStyle.Fill;
            pnlAlignMonitoringView.Dock = DockStyle.Fill;
        }

        private TabInspResult AlignMonitoringControl_GetTabInspResultEvent(int tabNo)
        {
            return AppsInspResult.Instance().GetAlign(tabNo);
        }

        private void lblStart_Click(object sender, EventArgs e)
        {
            if (ModelManager.Instance().CurrentModel == null)
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "Current Model is null.";
                form.ShowDialog();
                return;
            }

            SystemManager.Instance().StartRun();

            Logger.Write(LogType.GUI, "Clicked Start Button");
        }

        private void lblStop_Click(object sender, EventArgs e)
        {
            SystemManager.Instance().StopRun();

            Logger.Write(LogType.GUI, "Clicked Stop Button");
        }

        public void UpdateButton()
        {
            if (PlcControlManager.Instance().MachineStatus == MachineStatus.RUN)
            {
                lblStartText.ForeColor = Color.LawnGreen;
                lblStopText.ForeColor = Color.White;
                lblStart.Image = Properties.Resources.Start_Green;
                lblStop.Image = Properties.Resources.Stop_White;
            }
            else
            {
                lblStartText.ForeColor = Color.White;
                lblStopText.ForeColor = Color.Red;
                lblStart.Image = Properties.Resources.Start_White;
                lblStop.Image = Properties.Resources.Stop_Red;
            }

            if(AppsStatus.Instance().IsAlignMonitoringView)
            {
                lblAlignMonitoringText.ForeColor = Color.LawnGreen;
                lblAlignMonitoring.Image = Properties.Resources.AlignView_Green;
            }
            else
            {
                lblAlignMonitoringText.ForeColor = Color.White;
                lblAlignMonitoring.Image = Properties.Resources.AlignView_White;
            }
        }

        private void AkkonViewerControl_SetTabEventHandler(int tabNo)
        {
            DailyInfoViewerControl.UpdateAkkonResult(tabNo);
        }

        private void AlignViewerControl_SetTabEventHandler(int tabNo)
        {
            DailyInfoViewerControl.UpdateAlignResult(tabNo);
        }

        public void UpdateTabCount(int tabCount)
        {
            AkkonViewerControl.UpdateTabCount(tabCount);
            AlignViewerControl.UpdateTabCount(tabCount);
            AlignMonitoringControl.UpdateRefreshControl(tabCount);
            DailyInfoViewerControl.ReUpdate();
        }

        public void UpdateMainAkkonResultData(int tabNo)
        {
            AkkonViewerControl.UpdateMainResult(tabNo);
            DailyInfoViewerControl.UpdateAkkonResult(tabNo);
        }

        public void UpdateMainAlignResult(int tabNo)
        {
            AlignViewerControl.UpdateMainResult(tabNo);
            AlignMonitoringControl.UpdateMainResult(tabNo);
            DailyInfoViewerControl.UpdateAlignResult(tabNo);
        }

        public void UpdateAkkonResultTabButton(int tabNo)
        {
            AkkonViewerControl.UpdateResultTabButton(tabNo);
        }

        public void UpdateAlignResultTabButton(int tabNo)
        {
            AlignViewerControl.UpdateResultTabButton(tabNo);
            AlignMonitoringControl.UpdateResultTabButton(tabNo);
        }

        public void TabButtonResetColor()
        {
            AkkonViewerControl.TabButtonResetColor();
            AlignViewerControl.TabButtonResetColor();
            AlignMonitoringControl.TabButtonResetColor();
        }

        public void AddSystemLogMessage(string logMessage)
        {
            SystemLogControl.AddLogMessage(logMessage);
        }

        public void Enable(bool isEnable)
        {
            AkkonViewerControl.Enable(isEnable);
            AlignViewerControl.Enable(isEnable);
            DailyInfoViewerControl.Enable(isEnable);
            AlignMonitoringControl.Enable(isEnable);
        }

        private void lblAlignMonitoring_Click(object sender, EventArgs e)
        {
            AppsStatus.Instance().IsAlignMonitoringView = !AppsStatus.Instance().IsAlignMonitoringView;

            UpdateView();
        }

        private void UpdateView()
        {
            if (AppsStatus.Instance().IsAlignMonitoringView)
            {
                pnlMainView.Visible = false;
                pnlAlignMonitoringView.Visible = true;
                pnlAlignMonitoringView.Dock = DockStyle.Fill;
            }
            else
            {
                pnlAlignMonitoringView.Visible = false;
                pnlMainView.Visible = true;
                pnlMainView.Dock = DockStyle.Fill;
            }
        }
        #endregion
    }
}
