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
        private delegate void UpdateAlignResultDelegate(AppsInspResult result);
        #endregion

        #region 생성자
        #endregion

        #region 메서드
        #endregion

        public AlignInspResultControl()
        {
            InitializeComponent();
        }

        public void UpdateAlignResult(AppsInspResult result)
        {
            if (this.InvokeRequired)
            {
                UpdateAlignResultDelegate callback = UpdateAlignResult;
                BeginInvoke(callback);
                return;
            }

            UpdateDataGridView(result);
        }

        private void UpdateDataGridView(AppsInspResult result)
        {
            foreach (var item in result.TabResultList)
            {
                string inspectionTime = result.LastInspTime;
                string panelID = result.Cell_ID;
                string tabNumber = item.TabNo.ToString();
                string judge = item.IsAlignGood().ToString();
                string leftAlignX = item.LeftAlignX.ResultValue.ToString("F2");
                string leftAlignY = item.LeftAlignY.ResultValue.ToString("F2");
                string rightAlignX = item.RightAlignX.ResultValue.ToString("F2");
                string rightAlignY = item.RightAlignY.ResultValue.ToString("F2");
                string centerAlignX = Math.Abs((item.LeftAlignX.ResultValue - item.RightAlignX.ResultValue) / 2).ToString("F2");

                string[] row = { inspectionTime, panelID, tabNumber, judge, leftAlignX, leftAlignY, rightAlignX, rightAlignY, centerAlignX };
                dgvAlignHistory.Rows.Add(row);
            }
        }
    }
}
