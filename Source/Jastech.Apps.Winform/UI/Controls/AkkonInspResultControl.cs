using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Service;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AkkonInspResultControl : UserControl
    {

        #region 필드
        #endregion

        #region 속성
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        private delegate void UpdateAkkonResultDelegate(DailyData dailyData);
        #endregion

        #region 생성자
        public AkkonInspResultControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        public void UpdateAkkonDaily(DailyData dailyData)
        {
            if (this.InvokeRequired)
            {
                UpdateAkkonResultDelegate callback = UpdateAkkonDaily;
                BeginInvoke(callback, dailyData);
                return;
            }

            UpdateDataGridView(dailyData);
        }

        private void UpdateDataGridView(DailyData dailyData)
        {
            foreach (var item in dailyData.AkkonDailyInfoList)
            {
                string inspectionTime = item.InspectionTime.ToString();
                string panelID = item.PanelID.ToString();
                string tabNumber = item.TabNo.ToString();
                string judge = item.Judgement.ToString();
                string count = item.AvgBlobCount.ToString();
                string length = item.AvgLength.ToString("F2");

                string[] row = { inspectionTime, panelID, tabNumber, judge, count, length };
                dgvAkkonHistory.Rows.Add(row);
            }
        }
        #endregion
    }
}
