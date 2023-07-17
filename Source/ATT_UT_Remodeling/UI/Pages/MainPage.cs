using ATT_UT_Remodeling.UI.Controls;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.UI.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ATT_UT_Remodeling.UI.Pages
{
    public partial class MainPage : UserControl
    {
        #region 필드
        #endregion

        #region 속성
        public AkkonViewerControl AkkonViewerControl { get; set; } = new AkkonViewerControl() { Dock = DockStyle.Fill };

        public AlignViewerControl AlignViewerControl { get; set; } = new AlignViewerControl() { Dock = DockStyle.Fill };

        public PreAlignDisplayControl PreAlignDisplayControl { get; set; } = new PreAlignDisplayControl() { Dock = DockStyle.Fill };

        public SystemLogControl SystemLogControl { get; set; } = new SystemLogControl() { Dock = DockStyle.Fill };

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
            pnlAkkon.Controls.Add(AkkonViewerControl);
            pnlAlign.Controls.Add(AlignViewerControl);
            pnlPreAlign.Controls.Add(PreAlignDisplayControl);
            pnlSystemLog.Controls.Add(SystemLogControl);
        }

        public void UpdateTabCount(int tabCount)
        {
            AkkonViewerControl.UpdateTabCount(tabCount);
            AlignViewerControl.UpdateTabCount(tabCount);
        }

        public void UpdateMainResult(AppsInspResult result)
        {
            AkkonViewerControl.UpdateMainResult(result);
            AlignViewerControl.UpdateMainResult(result);
        }

        public void AddSystemLogMessage(string logMessage)
        {
            SystemLogControl.AddLogMessage(logMessage);
        }
        #endregion

        private void lblStart_Click(object sender, EventArgs e)
        {
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
    }
}
