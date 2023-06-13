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
using static Jastech.Apps.Winform.UI.Controls.ResultChartControl;

namespace ATT_UT_IPAD.UI.Controls
{
    public partial class AkkonResultChartControl : UserControl
    {
        private delegate void UpdateUIDelegate(int tabNo);

        public ResultChartControl ResultChartControl { get; private set; } = new ResultChartControl();

        public AkkonResultChartControl()
        {
            InitializeComponent();
        }

        private void AkkonResultChartControl_Load(object sender, EventArgs e)
        {
            AddControl();
            UpdateDailyChart(tabNo: 0);
        }

        private void AddControl()
        {
            ResultChartControl.Dock = DockStyle.Fill;
            ResultChartControl.SetInspChartType(InspChartType.Akkon);
            pnlAkkonResultChart.Controls.Add(ResultChartControl);
        }

        public void UpdateUI(int tabNo)
        {
            if (this.InvokeRequired)
            {
                UpdateUIDelegate callback = UpdateUI;
                BeginInvoke(callback);
                return;
            }

            UpdateDailyChart(tabNo);
        }

        private void UpdateDailyChart(int tabNo)
        {
            var dailyInfo = DailyInfoService.GetDailyInfo();

            if (dailyInfo.DailyDataList.Count > 0)
                ResultChartControl.UpdateAkkonDaily(dailyInfo, tabNo);
        }
    }
}
