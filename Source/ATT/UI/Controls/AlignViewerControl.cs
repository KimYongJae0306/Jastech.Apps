using ATT_UT_IPAD.UI.Controls;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.UI.Controls;
using System;
using System.Windows.Forms;
using static Jastech.Apps.Winform.UI.Controls.ResultChartControl;

namespace ATT.UI.Controls
{
    public partial class AlignViewerControl : UserControl
    {
        #region 필드
        #endregion

        #region 속성
        public AlignResultDisplayControl AlignResultDisplayControl { get; set; } = new AlignResultDisplayControl() { Dock = DockStyle.Fill };

        public AlignResultDataControl AlignResultDataControl { get; set; } = new AlignResultDataControl() { Dock = DockStyle.Fill };

        public ResultChartControl AlignResultChartControl { get; set; } = new ResultChartControl() { Dock = DockStyle.Fill };
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
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
            AlignResultChartControl.SetInspChartType(InspChartType.Align);
            pnlResultChart.Controls.Add(AlignResultChartControl);

            AlignResultDataControl.UpdateAlignDaily();
            pnlResultData.Controls.Add(AlignResultDataControl);

            AlignResultDisplayControl.SendTabNumber += UpdateResultChart;
            pnlResultDisplay.Controls.Add(AlignResultDisplayControl);
        }

        public void UpdateTabCount(int tabCount)
        {
            AlignResultDisplayControl.UpdateTabCount(tabCount);
        }

        public void UpdateMainResult(AppsInspResult result)
        {
            UpdateResultDisplay(result);
            UpdateResultData();
            UpdateResultChart(0);
        }

        private void UpdateResultDisplay(AppsInspResult result)
        {
            AlignResultDisplayControl.UpdateResultDisplay(result);
        }

        private void UpdateResultData()
        {
            AlignResultDataControl.UpdateAlignDaily();
        }

        private void UpdateResultChart(int tabNumber)
        {
            AlignResultChartControl.UpdateAlignDaily(tabNumber);
        }
        #endregion
    }
}
