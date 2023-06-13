using Jastech.Apps.Structure;
using Jastech.Apps.Winform.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATT_UT_IPAD.UI.Controls
{
    public partial class AlignResultControl : UserControl
    {
        //public AlignInspResultControl AlignInspResultControl { get; set; } = new AlignInspResultControl() { Dock = DockStyle.Fill };

        public AlignResultDataControl AlignResultDataControl { get; set; } = new AlignResultDataControl() { Dock = DockStyle.Fill };

        public AlignResultChartControl AlignResultChartControl { get; set; } = new AlignResultChartControl() { Dock = DockStyle.Fill };

        public AlignResultControl()
        {
            InitializeComponent();
        }

        private void AlignResultControl_Load(object sender, EventArgs e)
        {
            AddControls();
        }

        private void AddControls()
        {
            pnlAlignResultData.Controls.Add(AlignResultDataControl);
            pnlAlignResultChart.Controls.Add(AlignResultChartControl);
        }

        public void UpdateData()
        {
            //AlignInspResultControl.UpdateAkkonDaily(dailyInfo);
            //AlignInspResultControl.UpdateAlignDaily();

            AlignResultDataControl.UpdateResultData();
        }

        public void UpdateChart(int tabNo)
        {
            AlignResultChartControl.UpdateUI(tabNo);
        }

        public void ClearResult()
        {

        }
    }
}
