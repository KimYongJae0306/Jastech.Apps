using Jastech.Apps.Structure;
using Jastech.Apps.Winform.Service;
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
    public partial class AkkonResultControl : UserControl
    {
        //public AkkonInspResultControl AkkonInspResultControl { get; set; } = new AkkonInspResultControl() { Dock = DockStyle.Fill };
        public AkkonResultDataControl AkkonResultDataControl { get; set; } = new AkkonResultDataControl() { Dock = DockStyle.Fill };

        public AkkonResultChartControl AkkonResultChartControl { get; set; } = new AkkonResultChartControl() { Dock = DockStyle.Fill };


        public AkkonResultControl()
        {
            InitializeComponent();
        }

        private void AkkonResultControl_Load(object sender, EventArgs e)
        {
            AddControls();
        }

        private void AddControls()
        {
            pnlAkkonResultData.Controls.Add(AkkonResultDataControl);
            pnlAkkonResultChart.Controls.Add(AkkonResultChartControl);
        }

        public void UpdateData()
        {
            //AkkonInspResultControl.UpdateAkkonDaily(dailyInfo);
            //AkkonInspResultControl.UpdateAkkonDaily();
            AkkonResultDataControl.UpdateResultData();
        }

        public void UpdateChart(int tabNo)
        {
            AkkonResultChartControl.UpdateUI(tabNo);
        }
    }
}
