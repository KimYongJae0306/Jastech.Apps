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
using Jastech.Framework.Winform.Forms;

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
                    string preHead = item.PreHead;
                    string finalHead = item.FinalHead;
                    string leftAlignX = GetValue(item.LX);
                    string leftAlignY = GetValue(item.LY);
                    string centerAlignX = GetValue(item.CX);
                    string rightAlignX = GetValue(item.RX);
                    string rightAlignY = GetValue(item.RY);

                    string[] row = { inspectionTime, panelID, tabNumber, judge, preHead, finalHead, leftAlignX, leftAlignY, centerAlignX, rightAlignX, rightAlignY };
                    dgvAlignHistory.Rows.Add(row);
                }
            }
        }

        private string GetValue(string value)
        {
            if(double.TryParse(value, out double temp))
            {
                return MathHelper.GetFloorDecimal(temp, 2).ToString();
            }
            else
                return "-";
        }

        public void ClearData()
        {
            dgvAlignHistory.Rows.Clear();
        }

        private void dgvAlignHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //string basePath = ConfigSet.Instance().Path.Result;

            //var model = ModelManager.Instance().CurrentModel as InspModel;
            //string modelName = model.Name;

            //string fullPath = Path.Combine(basePath, modelName);

            //var directoryList = Directory.EnumerateDirectories(fullPath, "*", SearchOption.AllDirectories);

            //string cellID = dgvAlignHistory.Rows[e.RowIndex].Cells[1].Value.ToString();
            //string tabNo = dgvAlignHistory.Rows[e.RowIndex].Cells[2].Value.ToString();
            //string selectedPath = string.Empty;

            //foreach (var directory in directoryList)
            //{
            //    if (directory.Contains(cellID) && directory.Contains("Align"))
            //    {
            //        selectedPath = directory;
            //        break;
            //    }
            //}

            //if (selectedPath == string.Empty)
            //{
            //    MessageConfirmForm confirmForm = new MessageConfirmForm();
            //    confirmForm.Message = "The selected cell id does not have an image file.";
            //    confirmForm.ShowDialog();
            //    return;
            //}

            //var imageFiles = Directory.GetFiles(selectedPath, "*.bmp");

            //string selectedImageFilePath = string.Empty; 
            //foreach (var file in imageFiles)
            //{
            //    if (file.ToUpper().Contains($"TAB_{tabNo}"))
            //    {
            //        selectedImageFilePath = file;
            //        break;
            //    }
            //}

            //if (selectedImageFilePath == string.Empty)
            //{
            //    MessageConfirmForm confirmForm = new MessageConfirmForm();
            //    confirmForm.Message = "The selected cell id does not have an image file.";
            //    confirmForm.ShowDialog();
            //}
            //else
            //    Process.Start(selectedImageFilePath);
        }

        private void dgvAlignHistory_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var dailyInfo = DailyInfoService.GetDailyInfo();

            string time = dgvAlignHistory.Rows[e.RowIndex].Cells[0].Value.ToString();
            var alignDailyInfo = dailyInfo.GetAlignDailyInfo(time);

            if (alignDailyInfo != null)
            {
                string path = Path.Combine(alignDailyInfo.ResultPath, "Inspection", "AlignResult");

                if (Directory.Exists(path))
                {
                    Process.Start(path);
                    return;
                }
                else
                {
                    path = Path.Combine(alignDailyInfo.ResultPath, "AlignResult");

                    if (Directory.Exists(path))
                    {
                        Process.Start(path);
                        return;
                    }
                }
            }

            MessageConfirmForm form = new MessageConfirmForm();
            form.Message = "The data does not exist.";
            form.ShowDialog();
            return;
        }
        #endregion
    }
}
