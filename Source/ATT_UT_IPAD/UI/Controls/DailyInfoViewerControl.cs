using Jastech.Apps.Winform.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Jastech.Apps.Winform.UI.Controls.ResultChartControl;

namespace ATT_UT_IPAD.UI.Controls
{
    public partial class DailyInfoViewerControl : UserControl
    {
        #region 필드
        private Color _selectedColor = new Color();

        private Color _nonSelectedColor = new Color();
        #endregion

        #region 속성
        public AkkonResultDataControl AkkonResultDataControl { get; set; } = null;

        public ResultChartControl AkkonResultChartControl { get; set; } = null;

        public AlignResultDataControl AlignResultDataControl { get; set; } = null;

        public ResultChartControl AlignResultChartControl { get; set; } = null;
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public DailyInfoViewerControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void DailyInfoViewerControl_Load(object sender, EventArgs e)
        {
            AddControls();
            InitializeUI();
        }
        
        private void AddControls()
        {
            AkkonResultDataControl = new AkkonResultDataControl() { Dock = DockStyle.Fill};
            AkkonResultDataControl.UpdateAkkonDaily();
            pnlDailyResult.Controls.Add(AkkonResultDataControl);

            AkkonResultChartControl = new ResultChartControl() { Dock = DockStyle.Fill };
            AkkonResultChartControl.SetInspChartType(InspChartType.Akkon);
            pnlDailyChart.Controls.Add(AkkonResultChartControl);

            AlignResultDataControl = new AlignResultDataControl() { Dock = DockStyle.Fill };
            AlignResultDataControl.UpdateAlignDaily();
            pnlDailyResult.Controls.Add(AlignResultDataControl);

            AlignResultChartControl = new ResultChartControl() { Dock = DockStyle.Fill };
            AlignResultChartControl.SetInspChartType(InspChartType.Align);
            pnlDailyChart.Controls.Add(AlignResultChartControl);
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            ShowAlignDailyInfo();
        }

        private void ShowAkkonDailyInfo()
        {
            lblAkkon.BackColor = _selectedColor;
            lblAlign.BackColor = _nonSelectedColor;

            pnlDailyResult.Controls.Clear();
            pnlDailyResult.Controls.Add(AkkonResultDataControl);

            pnlDailyChart.Controls.Clear();
            pnlDailyChart.Controls.Add(AkkonResultChartControl);
        }

        private void ShowAlignDailyInfo()
        {
            lblAkkon.BackColor = _nonSelectedColor;
            lblAlign.BackColor = _selectedColor;

            pnlDailyResult.Controls.Clear();
            pnlDailyResult.Controls.Add(AlignResultDataControl);

            pnlDailyChart.Controls.Clear();
            pnlDailyChart.Controls.Add(AlignResultChartControl);
        }

        private void lblAkkon_Click(object sender, EventArgs e)
        {
            ShowAkkonDailyInfo();
        }

        private void lblAlign_Click(object sender, EventArgs e)
        {
            ShowAlignDailyInfo();
        }

        public void UpdateAkkonResult(int tabNo)
        {
            AkkonResultDataControl.UpdateAkkonDaily();
            UpdateAkkonChart(tabNo);
        }

        public void UpdateAlignResult(int tabNo)
        {
            AlignResultDataControl.UpdateAlignDaily();
            UpdateAlignChart(tabNo);
        }

        private void UpdateAkkonChart(int tabNo)
        {
            AkkonResultChartControl.UpdateAkkonDaily(tabNo);
        }

        private void UpdateAlignChart(int tabNo)
        {
            AlignResultChartControl.UpdateAlignDaily(tabNo);
        }

        public void ReUpdate()
        {
            AkkonResultDataControl.UpdateAkkonDaily();
            AkkonResultChartControl.ReUpdate(InspChartType.Akkon);

            AlignResultDataControl.UpdateAlignDaily();
            AlignResultChartControl.ReUpdate(InspChartType.Align);
        }

        public void ClearAkkonData()
        {
            AkkonResultDataControl.ClearData();
            AkkonResultChartControl.ClearChart();
        }

        public void ClearAlignData()
        {
            AlignResultDataControl.ClearData();
            AlignResultChartControl.ClearChart();
        }

        public void Enable(bool isEnable)
        {
            BeginInvoke(new Action(() =>
            {
                AkkonResultDataControl.Enabled = isEnable;
                AlignResultDataControl.Enabled = isEnable;
            }));
        }
        #endregion
    }
}
