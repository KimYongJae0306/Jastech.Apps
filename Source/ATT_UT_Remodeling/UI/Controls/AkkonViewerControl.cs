using ATT_UT_IPAD.UI.Controls;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.UI.Controls;
using System;
using System.Windows.Forms;
using static Jastech.Apps.Winform.UI.Controls.ResultChartControl;

namespace ATT_UT_Remodeling.UI.Controls
{
    public partial class AkkonViewerControl : UserControl
    {
        #region 속성
        public AkkonResultDisplayControl AkkonResultDisplayControl { get; set; } = null;

        public AkkonResultDataControl AkkonResultDataControl { get; set; } = null;

        public ResultChartControl AkkonResultChartControl { get; set; } = null;
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
            AkkonResultChartControl = new ResultChartControl();
            AkkonResultChartControl.Dock = DockStyle.Fill;
            AkkonResultChartControl.SetInspChartType(InspChartType.Akkon);
            pnlResultChart.Controls.Add(AkkonResultChartControl);

            AkkonResultDataControl = new AkkonResultDataControl();
            AkkonResultDataControl.Dock = DockStyle.Fill;
            AkkonResultDataControl.UpdateAkkonDaily();
            pnlResultData.Controls.Add(AkkonResultDataControl);

            AkkonResultDisplayControl = new AkkonResultDisplayControl();
            AkkonResultDisplayControl.Dock = DockStyle.Fill;
            AkkonResultDisplayControl.SendTabNumber += UpdateResultChart;
            pnlResultDisplay.Controls.Add(AkkonResultDisplayControl);
        }

        public void UpdateTabCount(int tabCount)
        {
            AkkonResultDisplayControl.UpdateTabCount(tabCount);
            AkkonResultDataControl.ClearData();
            AkkonResultChartControl.ClearChart();
        }

        public void UpdateMainResult(int tabNo)
        {
            AkkonResultDisplayControl.UpdateResultDisplay(tabNo);
            AkkonResultDataControl.UpdateAkkonDaily();
            UpdateResultChart(tabNo);
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
            AkkonResultChartControl.UpdateAkkonDaily(tabNo);
        }
        #endregion
    }
}
