using ATT_UT_IPAD.Core.Data;
using ATT_UT_IPAD.UI.Controls;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.UI.Controls;
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

        public DailyInfoViewerControl DailyInfoViewerControl { get; set; } = null;

        public SystemLogControl SystemLogControl { get; set; } = null;
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
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

            SystemLogControl = new SystemLogControl();
            SystemLogControl.Dock = DockStyle.Fill;
            pnlSystemLog.Controls.Add(SystemLogControl);
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
            //DailyInfoViewerControl.ClearAkkonData();

            AlignViewerControl.UpdateTabCount(tabCount);
            //DailyInfoViewerControl.ClearAlignData();
        }

        public void UpdateMainAkkonResultDisplay(int tabNo)
        {
            AkkonViewerControl.UpdateMainResult(tabNo);
        }

        public void UpdateMainAkkonResultData(int tabNo)
        {
            DailyInfoViewerControl.UpdateAkkonResult(tabNo);
        }

        public void UpdateMainAlignResult(int tabNo)
        {
            AlignViewerControl.UpdateMainResult(tabNo);
            DailyInfoViewerControl.UpdateAlignResult(tabNo);
        }

        public void UpdateAkkonResultTabButton(int tabNo)
        {
            AkkonViewerControl.UpdateResultTabButton(tabNo);
        }

        public void UpdateAlignResultTabButton(int tabNo)
        {
            AlignViewerControl.UpdateResultTabButton(tabNo);
        }


        public void TabButtonResetColor()
        {
            AkkonViewerControl.TabButtonResetColor();
            AlignViewerControl.TabButtonResetColor();
        }

        public void AddSystemLogMessage(string logMessage)
        {
            SystemLogControl.AddLogMessage(logMessage);
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
        }

        private void lblStop_Click(object sender, EventArgs e)
        {
            SystemManager.Instance().StopRun();
        }

        public void UpdateButton()
        {
            if (SystemManager.Instance().MachineStatus == MachineStatus.RUN)
            {
                lblStartText.ForeColor = Color.Blue;
                lblStopText.ForeColor = Color.White;
            }
            else
            {
                lblStartText.ForeColor = Color.White;
                lblStopText.ForeColor = Color.Blue;
            }
        }
        #endregion
    }
}
