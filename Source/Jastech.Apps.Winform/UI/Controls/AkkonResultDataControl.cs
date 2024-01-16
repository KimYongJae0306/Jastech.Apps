using Jastech.Apps.Winform.Service;
using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AkkonResultDataControl : UserControl
    {
        #region 속성
        public ResultChartControl ResultChartControl { get; private set; } = null;
        #endregion

        #region 델리게이트
        private delegate void UpdateAkkonResultDelegate();
        #endregion

        #region 생성자
        public AkkonResultDataControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void AkkonResultDataControl_Load(object sender, EventArgs e)
        {
            AddControls();
        }

        private void AddControls()
        {
            ResultChartControl = new ResultChartControl();
            ResultChartControl.Dock = DockStyle.Fill;
            ResultChartControl.SetInspChartType(ResultChartControl.InspChartType.Akkon);
            pnlChart.Controls.Add(ResultChartControl);
        }

        public void RefreshData()
        {
            if (this.InvokeRequired)
            {
                UpdateAkkonResultDelegate callback = RefreshData;
                BeginInvoke(callback);
                return;
            }

            UpdateDataGridView();
            UpdateChart();
        }

        public void UpdateAkkonDaily(int tabNo)
        {
            ResultChartControl.UpdateAkkonDaily(tabNo);
        }

        public void UpdateDataGridView()
        {
            if (this.InvokeRequired)
            {
                UpdateAkkonResultDelegate callback = UpdateDataGridView;
                BeginInvoke(callback);
                return;
            }

            dgvAkkonHistory.Rows.Clear();

            var dailyInfo = DailyInfoService.GetDailyInfo();
            
            List<DailyData> reverseList = Enumerable.Reverse(dailyInfo.DailyDataList).ToList();
            foreach (var dailyDataList in reverseList)
            {
                foreach (var item in dailyDataList.AkkonDailyInfoList)
                {
                    string inspectionTime = item.InspectionTime.ToString();
                    string panelID = item.PanelID.ToString();
                    string tabNumber = (item.TabNo + 1).ToString();
                    string judge = item.Judgement.ToString();
                    string count = item.MinBlobCount.ToString();
                    string length = MathHelper.GetFloorDecimal(item.MinLength, 4).ToString();

                    string[] row = { inspectionTime, panelID, tabNumber, judge, count, length };
                    dgvAkkonHistory.Rows.Add(row);
                }
            }
        }

        public void ClearData()
        {
            dgvAkkonHistory.Rows.Clear();
        }

        public void UpdateChart()
        {
            ResultChartControl.UpdateChart();
        }
        #endregion
    }
}
