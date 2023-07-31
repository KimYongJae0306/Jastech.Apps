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
    public partial class AlignViewerControl : UserControl
    {
        #region 속성
        public AlignResultDisplayControl AlignResultDisplayControl { get; set; } = null;

        public AlignResultDataControl AlignResultDataControl { get; set; } = null;

        public ResultChartControl AlignResultChartControl { get; set; } = null;
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
            AlignResultChartControl = new ResultChartControl();
            AlignResultChartControl.Dock = DockStyle.Fill;
            AlignResultChartControl.SetInspChartType(InspChartType.Align);
            pnlResultChart.Controls.Add(AlignResultChartControl);

            AlignResultDataControl = new AlignResultDataControl();
            AlignResultDataControl.Dock = DockStyle.Fill;
            AlignResultDataControl.UpdateAlignDaily();
            pnlResultData.Controls.Add(AlignResultDataControl);

            AlignResultDisplayControl = new AlignResultDisplayControl();
            AlignResultDisplayControl.Dock = DockStyle.Fill;
            AlignResultDisplayControl.SendTabNumberEvent += UpdateResultChart;
            AlignResultDisplayControl.GetTabInspResultEvent += GetTabInspResult;
            pnlResultDisplay.Controls.Add(AlignResultDisplayControl);
        }

        private TabInspResult GetTabInspResult(int tabNo)
        {
            return AppsInspResult.Instance().GetAlign(tabNo);
        }

        public void UpdateTabCount(int tabCount)
        {
            AlignResultDisplayControl.UpdateTabCount(tabCount);
            AlignResultDataControl.ClearData();
            AlignResultChartControl.ClearChart();
        }

        public void UpdateMainResult(int tabNo)
        {
            AlignResultDisplayControl.UpdateResultDisplay(tabNo);
            AlignResultDataControl.UpdateAlignDaily();
            UpdateResultChart(tabNo);
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
            AlignResultChartControl.UpdateAlignDaily(tabNo);
        }

        #endregion
    }
}
