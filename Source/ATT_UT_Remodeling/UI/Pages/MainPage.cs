using ATT_UT_Remodeling.UI.Controls;
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

namespace ATT_UT_Remodeling.UI.Pages
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

        public PreAlignDisplayControl PreAlignDisplayControl { get; set; } = null;

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

            PreAlignDisplayControl = new PreAlignDisplayControl();
            PreAlignDisplayControl.Dock = DockStyle.Fill;
            pnlPreAlign.Controls.Add(PreAlignDisplayControl);

            SystemLogControl = new SystemLogControl();
            SystemLogControl.Dock = DockStyle.Fill;
            pnlSystemLog.Controls.Add(SystemLogControl);
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
        }

        public void UpdateMainResult(int tabNo)
        {
            AkkonViewerControl.UpdateMainResult(tabNo);
            DailyInfoViewerControl.UpdateAkkonResult(tabNo);

            AlignViewerControl.UpdateMainResult(tabNo);
            DailyInfoViewerControl.UpdateAlignResult(tabNo);
        }

        public void UpdateResultTabButton(int tabNo)
        {
            AkkonViewerControl.UpdateResultTabButton(tabNo);
            AlignViewerControl.UpdateResultTabButton(tabNo);
        }

        public void TabButtonResetColor()
        {
            AkkonViewerControl.TabButtonResetColor();
            AlignViewerControl.TabButtonResetColor();
        }

        public void UpdateLeftPreAlignResult(AppsPreAlignResult result)
        {
            PreAlignDisplayControl.UpdateLeftDisplay(result.Left);
            PreAlignDisplayControl.UpdateLeftResult(result.Left);
        }

        public void UpdateRightPreAlignResult(AppsPreAlignResult result)
        {
            PreAlignDisplayControl.UpdateRightDisplay(result.Right);
            PreAlignDisplayControl.UpdateRightResult(result.Right);
        }

        public void UpdatePreAlignResult(AppsPreAlignResult result)
        {
            PreAlignDisplayControl.UpdatePreAlignResult(result);
        }

        public void ClearPreAlignResult()
        {
            PreAlignDisplayControl.ClearPreAlignResult();
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
        }
        #endregion
    }
}
