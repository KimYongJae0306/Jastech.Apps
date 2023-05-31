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
        //private delegate void UpdateAlignResultDelegate(DailyData dailyData);
        private delegate void UpdateAlignResultDelegate(DailyInfo dailyInfo);
        #endregion

        #region 생성자
        #endregion

        #region 메서드
        #endregion

        public AlignInspResultControl()
        {
            InitializeComponent();
        }

        //public void UpdateAlignDaily(DailyData dailyData)
        public void UpdateAlignDaily(DailyInfo dailyInfo)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    UpdateAlignResultDelegate callback = UpdateAlignDaily;
                    BeginInvoke(callback, dailyInfo);
                    return;
                }

                UpdateDataGridView(dailyInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " : " + ex.Message);
            }
        }

        private void UpdateDataGridView(DailyInfo dailyInfo)
        {
            dgvAlignHistory.Rows.Clear();

            List<DailyData> reverseList = new List<DailyData>();
            reverseList = Enumerable.Reverse(dailyInfo.DailyDataList).ToList();

            foreach (var dailyDataList in reverseList)
            {
                foreach (var item in dailyDataList.AlignDailyInfoList)
                {
                    string inspectionTime = item.InspectionTime;
                    string panelID = item.PanelID;
                    string tabNumber = item.TabNo.ToString();
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

        //private void UpdateDataGridView(DailyData dailyData)
        //{
        //    foreach (var item in dailyData.AlignDailyInfoList)
        //    {
        //        string inspectionTime = item.InspectionTime;
        //        string panelID = item.PanelID;
        //        string tabNumber = item.TabNo.ToString();
        //        string judge = item.Judgement.ToString();
        //        string leftAlignX = item.LX.ToString("F2");
        //        string leftAlignY = item.LY.ToString("F2");
        //        string rightAlignX = item.RX.ToString("F2");
        //        string rightAlignY = item.RY.ToString("F2");
        //        string centerAlignX = item.CX.ToString("F2");

        //        string[] row = { inspectionTime, panelID, tabNumber, judge, leftAlignX, leftAlignY, rightAlignX, rightAlignY, centerAlignX };
        //        dgvAlignHistory.Rows.Add(row);
        //    }
        //}

        //private List<AlignDailyInfo> SortData(List<AlignDailyInfo> alignDailyInfoList)
        //{
        //    List<AlignDailyInfo> outputList = new List<AlignDailyInfo>();

        //    //List<AlignDailyInfo> tabList = new List<AlignDailyInfo>();

        //    int interval = 0;
        //    for (int index = 0; index < alignDailyInfoList.Count / 4; index++)
        //    {
        //        //Range range = new Range(interval, interval + 4);
        //        //var tt = alignDailyInfo.OrderBy(x => x).Take(4).ToList();

        //        var tt = alignDailyInfoList.OrderBy(x => x).Skip(interval).Take(4).ToList();
        //        interval += 4;

        //        List<AlignDailyInfo> tabList = Enumerable.Reverse(tt).ToList();

        //        outputList.AddRange(tabList);

        //        int gg = 0;
        //    }

        //    return outputList;
        //}
    }
}
