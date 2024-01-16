using ATT_UT_IPAD.UI.Controls;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.UI.Controls;
using System;
using System.Windows.Forms;
using static Jastech.Apps.Winform.UI.Controls.ResultChartControl;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AlignViewerControl : UserControl
    {
        #region 속성
        public AlignResultDisplayControl AlignResultDisplayControl { get; set; } = null;
        #endregion

        #region 이벤트
        public event SetTabDelegate SetTabEventHandler;

        public event GetTabInspResultDelegate GetTabInspResultEvent;
        #endregion

        #region 델리게이트
        public delegate void SetTabDelegate(int tabNo);

        public delegate TabInspResult GetTabInspResultDelegate(int tabNo);
        #endregion

        #region 생성자
        public AlignViewerControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void AlignViewerControl_Load(object sender, EventArgs e)
        {
            AddControls();
        }

        private void AddControls()
        {
            AlignResultDisplayControl = new AlignResultDisplayControl();
            AlignResultDisplayControl.Dock = DockStyle.Fill;
            AlignResultDisplayControl.SendTabNumberEvent += UpdateResultChart;
            AlignResultDisplayControl.GetTabInspResultEvent += GetTabInspResult;
            pnlResultDisplay.Controls.Add(AlignResultDisplayControl);
        }

        private TabInspResult GetTabInspResult(int tabNo)
        {
            return GetTabInspResultEvent?.Invoke(tabNo);
        }

        public void CreateTabButton(int tabCount)
        {
            AlignResultDisplayControl.CreateTabButton(tabCount);
        }

        public void UpdateMainResult(int tabNo)
        {
            AlignResultDisplayControl.UpdateResultDisplay(tabNo);
        }

        public void UpdateResultTabButton(int tabNo)
        {
            AlignResultDisplayControl.UpdateResultTabButton(tabNo);
        }

        public void TabButtonResetColor()
        {
            AlignResultDisplayControl.TabButtonResetColor();
        }

        private void UpdateResultChart(int tabNo)
        {
            SetTabEventHandler?.Invoke(tabNo);
        }

        public void Enable(bool isEnable)
        {
            AlignResultDisplayControl.Enable(isEnable);
        }
        #endregion
    }
}
