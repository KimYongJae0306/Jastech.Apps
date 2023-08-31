using Jastech.Apps.Structure;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Structure;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AkkonTrendControl : UserControl
    {
        #region 필드
        private Color _selectedColor;

        private Color _nonSelectedColor;

        private List<Label> _tabLabelList = new List<Label>();

        private List<Label> _akkonTypeLabelList = new List<Label>();

        private List<TrendResult> _akkonTrendResults = new List<TrendResult>();

        private TabType _tabType { get; set; } = TabType.Tab1;

        private AkkonResultType _akkonResultType { get; set; } = AkkonResultType.All;
        #endregion

        #region 속성
        private ResultChartControl ChartControl = null;
        #endregion

        #region 생성자
        public AkkonTrendControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void AkkonTrendControl_Load(object sender, EventArgs e)
        {
            AddControl();
            InitializeUI();
        }

        private void AddControl()
        {
            ChartControl = new ResultChartControl();
            ChartControl.Dock = DockStyle.Fill;
            ChartControl.ChartType = ResultChartControl.InspChartType.Akkon;
            pnlChart.Controls.Add(ChartControl);
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            dgvAkkonTrendData.Columns.Clear();
            List<string> header = new List<string>
            {
                "Inspection Time",
                "Panel ID",
                "Stage No",
            };
            for (int index = 0; index < inspModel.TabCount; index++)
            {
                header.Add($"Tab_{index + 1}");
                header.Add($"Judge_{index + 1}");
                header.Add($"Avg Count_{index + 1}");
                header.Add($"Avg Length_{index + 1}");
            }
            var columns = header.Select(text => new DataGridViewTextBoxColumn { Name = text });
            dgvAkkonTrendData.Columns.AddRange(columns.ToArray());

            _akkonTypeLabelList.AddRange(new Label[] { lblAllData, lblCount, lblLength });
        }

        public void MakeTabListControl(int tabCount)
        {
            Size size = new Size(100, 54);
            Point location = new Point(20, 0);
            int margin = 10;

            for (int tabIndex = 0; tabIndex < tabCount; tabIndex++)
            {
                Label lbl = new Label();

                lbl.BorderStyle = BorderStyle.Fixed3D;
                lbl.BackColor = Color.FromArgb(52, 52, 52);
                lbl.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Text = "Tab" + (tabIndex + 1);
                lbl.Size = size;
                lbl.Location = location;
                lbl.MouseClick += LabelControl_SetTabEventHandler;

                pnlTabs.Controls.Add(lbl);
                _tabLabelList.Add(lbl);

                location.X += size.Width + margin;
            }
        }

        private void LabelControl_SetTabEventHandler(object sender, MouseEventArgs e)
        {
            Label lbl = (Label)sender;
            string tabNo = lbl.Text.Substring(3, 1);
            int tabIndex = Convert.ToInt32(tabNo) - 1;

            SetTabType((TabType)tabIndex);
        }

        public void SetTabType(TabType tabType)
        {
            ClearSelectedLabel(pnlTabs);
            _tabType = tabType;

            _tabLabelList[(int)tabType].BackColor = _selectedColor;
            UpdateChart(_tabType, _akkonResultType);
        }

        public void SetAkkonResultType(AkkonResultType akkonResultType)
        {
            ClearSelectedLabel(pnlChartTypes);
            _akkonResultType = akkonResultType;

            _akkonTypeLabelList[(int)_tabType].BackColor = _selectedColor;
            UpdateChart(_tabType, _akkonResultType);
        }

        private void ClearSelectedLabel(Panel panel)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is Label label)
                    label.BackColor = _nonSelectedColor;
            }
        }

        private void lblAllData_Click(object sender, EventArgs e) => SetAkkonResultType(AkkonResultType.All);

        private void lblCount_Click(object sender, EventArgs e) => SetAkkonResultType(AkkonResultType.Count);

        private void lblLength_Click(object sender, EventArgs e) => SetAkkonResultType(AkkonResultType.Length);

        private void UpdateChart(TabType tabType, AkkonResultType akkonResultType) => ChartControl.UpdateAkkonChart(_akkonTrendResults, tabType, akkonResultType);

        public void UpdateDataGridView()
        {
            dgvAkkonTrendData.Rows.Clear();
            for (int Index = 0; Index < _akkonTrendResults.Count; Index++)
                dgvAkkonTrendData.Rows.Add(_akkonTrendResults[Index].GetAkkonStringDatas().ToArray());
        }

        public void SetAkkonResultData(string path)
        {
            List<string[]> readTexts = new List<string[]>();
            foreach (string textLine in File.ReadAllLines(path))
                readTexts.Add(textLine.Split(','));

            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            _akkonTrendResults.Clear();
            for (int rowIndex = 1; rowIndex < readTexts.Count; rowIndex++)
            {
                TrendResult trendResult = new TrendResult();
                string[] datas = readTexts[rowIndex];

                trendResult.InspectionTime = datas[0];
                trendResult.PanelID = datas[1];
                trendResult.StageNo = Convert.ToInt32(datas[2]);

                for (int colIndex = 0, dataCount = 4; colIndex < inspModel.TabCount; colIndex++)
                {
                    int skipIndex = colIndex * dataCount;
                    var akkonResult = new TabAkkonTrendResult
                    {
                        Tab = Convert.ToInt32(datas[skipIndex + 3]),
                        Judge = datas[skipIndex + 4],
                        AvgCount = Convert.ToInt32(datas[skipIndex + 5]),
                        AvgLength = Convert.ToDouble(datas[skipIndex + 6]),
                    };
                    trendResult.TabAkkonResults.Add(akkonResult);
                }
                _akkonTrendResults.Add(trendResult);
            }
        }
        #endregion
    }
}
