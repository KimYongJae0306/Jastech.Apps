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
using Emgu.CV.Structure;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Config;
using Jastech.Apps.Structure;
using Jastech.Framework.Structure;
using Jastech.Framework.Util.Helper;
using System.Diagnostics;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AlignResultDataControl : UserControl
    {
        #region 속성
        #endregion

        #region 이벤트
        //public event SelectAlignResultTypeDelegate SelectAlignResultTypeEventHandler;
        #endregion

        #region 델리게이트
        private delegate void UpdateAlignResultDelegate();

        //public delegate void SelectAlignResultTypeDelegate(AlignResult alignResultType);
        #endregion

        #region 생성자
        public AlignResultDataControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        public void UpdateAlignDaily()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    UpdateAlignResultDelegate callback = UpdateAlignDaily;
                    BeginInvoke(callback);
                    return;
                }

                UpdateDataGridView();
            }
            catch (Exception ex)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " : " + ex.Message);
            }
        }

        private void UpdateDataGridView()
        {
            dgvAlignHistory.Rows.Clear();

            var dailyInfo = DailyInfoService.GetDailyInfo();

            List<DailyData> reverseList = Enumerable.Reverse(dailyInfo.DailyDataList).ToList();

            foreach (var dailyDataList in reverseList)
            {
                foreach (var item in dailyDataList.AlignDailyInfoList)
                {
                    string inspectionTime = item.InspectionTime;
                    string panelID = item.PanelID;
                    string tabNumber = (item.TabNo + 1).ToString();
                    string judge = item.Judgement.ToString();
                    string leftAlignX = item.LX.ToString("F2");
                    string leftAlignY = item.LY.ToString("F2");
                    string rightAlignX = item.RX.ToString("F2");
                    string rightAlignY = item.RY.ToString("F2");
                    string centerAlignX = item.CX.ToString("F2");

                    string[] row = { inspectionTime, panelID, tabNumber, judge, leftAlignX, leftAlignY, rightAlignX, rightAlignY, centerAlignX };
                    dgvAlignHistory.Rows.Add(row);
                }
            }
        }

        public void ClearData()
        {
            dgvAlignHistory.Rows.Clear();
        }
        #endregion

        private void dgvAlignHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string basePath = ConfigSet.Instance().Path.Result;

            var model = ModelManager.Instance().CurrentModel as InspModel;
            string modelName = model.Name;
            
            string fullPath = Path.Combine(basePath, modelName);

            var directoryList = Directory.EnumerateDirectories(fullPath, "*", SearchOption.AllDirectories);

            string cellID = dgvAlignHistory.Rows[e.RowIndex].Cells[1].Value.ToString();
            string tabNo = dgvAlignHistory.Rows[e.RowIndex].Cells[2].Value.ToString();
            string selectedPath = string.Empty;

            foreach (var directory in directoryList)
            {
                if (directory.Contains(cellID) && directory.Contains("Align"))
                {
                    selectedPath = directory;
                    break;
                }
            }

            var imageFiles = Directory.GetFiles(selectedPath, "*.bmp");

            string selectedImageFilePath = string.Empty;
            foreach (var file in imageFiles)
            {
                if (file.ToUpper().Contains($"TAB_{tabNo}"))
                {
                    selectedImageFilePath = file;
                    break;
                }
            }

            Process.Start(selectedImageFilePath);
            return;
        }
    }
}
