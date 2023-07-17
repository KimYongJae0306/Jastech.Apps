﻿using ATT_UT_IPAD.UI.Controls;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.UI.Controls;
using System;
using System.Windows.Forms;
using static Jastech.Apps.Winform.UI.Controls.ResultChartControl;

namespace ATT_UT_Remodeling.UI.Controls
{
    public partial class AkkonViewerControl : UserControl
    {
        #region 필드
        #endregion

        #region 속성
        public AkkonResultDisplayControl AkkonResultDisplayControl { get; set; } = null;// new AkkonResultDisplayControl() { Dock = DockStyle.Fill };

        public AkkonResultDataControl AkkonResultDataControl { get; set; } = null;// new AkkonResultDataControl() { Dock = DockStyle.Fill };

        public ResultChartControl AkkonResultChartControl { get; set; } = null; // new ResultChartControl() { Dock = DockStyle.Fill };
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public AkkonViewerControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void AkkonViewerControl_Load(object sender, EventArgs e)
        {
            AddControls();
        }

        private void AddControls()
        {
            AkkonResultChartControl = new ResultChartControl();
            AkkonResultChartControl.Dock = DockStyle.Fill;
            AkkonResultChartControl.SetInspChartType(InspChartType.Akkon);
            pnlResultChart.Controls.Add(AkkonResultChartControl);

            AkkonResultDataControl = new AkkonResultDataControl();
            AkkonResultDataControl.Dock = DockStyle.Fill;
            AkkonResultDataControl.UpdateAkkonDaily();
            pnlResultData.Controls.Add(AkkonResultDataControl);

            AkkonResultDisplayControl = new AkkonResultDisplayControl();
            AkkonResultDisplayControl.Dock = DockStyle.Fill;
            AkkonResultDisplayControl.SendTabNumber += UpdateResultChart;
            pnlResultDisplay.Controls.Add(AkkonResultDisplayControl);
        }

        public void UpdateTabCount(int tabCount)
        {
            AkkonResultDisplayControl.UpdateTabCount(tabCount);
        }

        public void UpdateMainResult(AppsInspResult result)
        {
            UpdateResultDisplay(result);
            UpdateResultData();
            UpdateResultChart(0);
        }

        private void UpdateResultDisplay(AppsInspResult result)
        {
            AkkonResultDisplayControl.UpdateResultDisplay(result);
        }

        private void UpdateResultData()
        {
            AkkonResultDataControl.UpdateAkkonDaily();
        }

        private void UpdateResultChart(int tabNumber)
        {
            AkkonResultChartControl.UpdateAkkonDaily(tabNumber);
        }
        #endregion
    }
}
