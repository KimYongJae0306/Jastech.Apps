using ATT_UT_IPAD.UI.Controls;
using ATT_UT_IPAD.Core.Data;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.UI.Controls;
using System;
using System.Windows.Forms;
using static Jastech.Apps.Winform.UI.Controls.ResultChartControl;

namespace ATT_UT_IPAD.UI.Controls
{
    public partial class AkkonViewerControl : UserControl
    {
        #region 속성
        public AkkonResultDisplayControl AkkonResultDisplayControl { get; set; } = null;
        #endregion

        #region 이벤트
        public event SetTabDelegate SetTabEventHandler;
        #endregion

        #region 델리게이트
        public delegate void SetTabDelegate(int tabNo);
        #endregion

        #region 생성자
        public AkkonViewerControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void AkkonViewerControl_Load(object sender, EventArgs e)
        {
            AddControls();
        }

        private void AddControls()
        {
            AkkonResultDisplayControl = new AkkonResultDisplayControl();
            AkkonResultDisplayControl.Dock = DockStyle.Fill;
            AkkonResultDisplayControl.SendTabNumberEvent += UpdateResultChart;
            AkkonResultDisplayControl.GetTabInspResultEvent += GetTabInspResult;
            pnlResultDisplay.Controls.Add(AkkonResultDisplayControl);
        }

        private TabInspResult GetTabInspResult(int tabNo)
        {
            return AppsInspResult.Instance().GetAkkon(tabNo);
        }

        public void UpdateTabCount(int tabCount)
        {
            AkkonResultDisplayControl.UpdateTabButtons(tabCount);
        }

        public void UpdateMainResult(int tabNo)
        {
            AkkonResultDisplayControl.UpdateResultDisplay(tabNo);
        }

        public void UpdateResultTabButton(int tabNo)
        {
            AkkonResultDisplayControl.UpdateResultTabButton(tabNo);
        }

        public void TabButtonResetColor()
        {
            AkkonResultDisplayControl.TabButtonResetColor();
        }

        private void UpdateResultChart(int tabNo)
        {
            SetTabEventHandler?.Invoke(tabNo);
        }

        public void Enable(bool isEnable)
        {
            AkkonResultDisplayControl.Enable(isEnable);
        }
        #endregion
    }
}
