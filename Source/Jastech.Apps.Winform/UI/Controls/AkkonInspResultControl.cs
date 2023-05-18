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
using Jastech.Framework.Macron.Akkon.Results;

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
        private delegate void UpdateAkkonResultDelegate(AppsInspResult result);
        #endregion

        #region 생성자
        public AkkonInspResultControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        public void UpdateAkkonResult(AppsInspResult result)
        {
            if (this.InvokeRequired)
            {
                UpdateAkkonResultDelegate callback = UpdateAkkonResult;
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
                string tabNumber = item.AkkonResult.TabNo.ToString();
                string judge = item.AkkonResult.Judgement.ToString();
                string count = item.AkkonResult.AvgBlobCount.ToString();
                string length = item.AkkonResult.AvgLength.ToString("F2");

                string[] row = { inspectionTime, panelID, tabNumber, judge, count, length };
                dgvAkkonHistory.Rows.Add(row);
            }
        }
        #endregion
    }
}
