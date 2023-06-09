﻿using Jastech.Apps.Winform.Service;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AkkonResultDataControl : UserControl
    {
        #region 필드
        #endregion

        #region 속성
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        //private delegate void UpdateAkkonResultDelegate(DailyInfo dailyInfo);
        private delegate void UpdateAkkonResultDelegate();
        #endregion

        #region 생성자
        public AkkonResultDataControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        //public void UpdateAkkonDaily(DailyInfo dailyInfo)
        public void UpdateAkkonDaily()
        {
            if (this.InvokeRequired)
            {
                UpdateAkkonResultDelegate callback = UpdateAkkonDaily;
                //BeginInvoke(callback, dailyInfo);
                BeginInvoke(callback);
                return;
            }

            //UpdateDataGridView(dailyInfo);
            UpdateDataGridView();
        }

        private void UpdateDataGridView()
        {
            dgvAkkonHistory.Rows.Clear();

            var dailyInfo = DailyInfoService.GetDailyInfo();

            List<DailyData> reverseList = new List<DailyData>();
            reverseList = Enumerable.Reverse(dailyInfo.DailyDataList).ToList();

            foreach (var dailyDataList in reverseList)
            {
                foreach (var item in dailyDataList.AkkonDailyInfoList)
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
        }

        //private void UpdateDataGridView(DailyInfo dailyInfo)
        //{
        //    dgvAkkonHistory.Rows.Clear();

        //    List<DailyData> reverseList = new List<DailyData>();
        //    reverseList = Enumerable.Reverse(dailyInfo.DailyDataList).ToList();

        //    foreach (var dailyDataList in reverseList)
        //    {
        //        foreach (var item in dailyDataList.AkkonDailyInfoList)
        //        {
        //            string inspectionTime = item.InspectionTime.ToString();
        //            string panelID = item.PanelID.ToString();
        //            string tabNumber = item.TabNo.ToString();
        //            string judge = item.Judgement.ToString();
        //            string count = item.AvgBlobCount.ToString();
        //            string length = item.AvgLength.ToString("F2");

        //            string[] row = { inspectionTime, panelID, tabNumber, judge, count, length };
        //            dgvAkkonHistory.Rows.Add(row);
        //        }
        //    }
        //}
        #endregion
    }
}
