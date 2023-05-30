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
using System.IO;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AlignInspResultControl : UserControl
    {
        #region 필드
        #endregion

        #region 속성
        public List<AppsInspResult> resultList = new List<AppsInspResult>();
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        private delegate void UpdateAlignResultDelegate(DailyData dailyData);
        #endregion

        #region 생성자
        #endregion

        #region 메서드
        #endregion

        public AlignInspResultControl()
        {
            InitializeComponent();
        }

        public void UpdateAlignDaily(DailyData dailyData)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    UpdateAlignResultDelegate callback = UpdateAlignDaily;
                    BeginInvoke(callback, dailyData);
                    return;
                }

                UpdateDataGridView(dailyData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " : " + ex.Message);
            }
        }

        private void UpdateDataGridView(DailyData dailyData)
        {
            foreach (var item in dailyData.AlignDailyInfoList)
            {
                string inspectionTime = item.InspectionTime;
                string panelID = item.PanelID;
                string tabNumber = item.TabNo.ToString();
                string judge = item.Judgement.ToString();
                string leftAlignX = item.LX.ToString("F2");
                string leftAlignY = item.LX.ToString("F2");
                string rightAlignX = item.RX.ToString("F2");
                string rightAlignY = item.RY.ToString("F2");
                string centerAlignX = item.CX.ToString("F2");

                string[] row = { inspectionTime, panelID, tabNumber, judge, leftAlignX, leftAlignY, rightAlignX, rightAlignY, centerAlignX };
                dgvAlignHistory.Rows.Add(row);
            }
        }
    }
}
