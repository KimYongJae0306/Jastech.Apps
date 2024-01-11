using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AlignTrendXControl : UserControl
    {
        #region 필드
        #endregion

        #region 속성
        public int TabNo { get; private set; } = 0;

        public ResultChartControl LxChartControl { get; set; } = null;

        public ResultChartControl CxChartControl { get; set; } = null;

        public ResultChartControl RxChartControl { get; set; } = null;
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public AlignTrendXControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void AlignTrendXControl_Load(object sender, EventArgs e)
        {
            InitializeUI();
            AddControl();
        }

        public void SetTabNumber(int tabNo)
        {
            TabNo = tabNo;
        }

        private void InitializeUI()
        {
            lblTabName.Text = "Tab " + (TabNo + 1);
        }

        private void AddControl()
        {
            LxChartControl = new ResultChartControl();
            LxChartControl.Dock = DockStyle.Fill;
            LxChartControl.IsDailyInfo = false;
            LxChartControl.ChartType = ResultChartControl.InspChartType.Align;
            LxChartControl.EnableLegend = false;
            pnlLxChart.Controls.Add(LxChartControl);

            CxChartControl = new ResultChartControl();
            CxChartControl.Dock = DockStyle.Fill;
            CxChartControl.IsDailyInfo = false;
            CxChartControl.ChartType = ResultChartControl.InspChartType.Align;
            CxChartControl.EnableLegend = false;
            pnlCxChart.Controls.Add(CxChartControl);

            RxChartControl = new ResultChartControl();
            RxChartControl.Dock = DockStyle.Fill;
            RxChartControl.IsDailyInfo = false;
            RxChartControl.ChartType = ResultChartControl.InspChartType.Align;
            RxChartControl.EnableLegend = false;
            pnlRxChart.Controls.Add(RxChartControl);
        }
        #endregion
    }
}
