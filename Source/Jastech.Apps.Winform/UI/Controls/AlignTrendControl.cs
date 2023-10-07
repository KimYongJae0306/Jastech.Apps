using Jastech.Apps.Structure;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Structure;
using Jastech.Framework.Winform.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AlignTrendControl : UserControl
    {
        #region 필드
        private Color _selectedColor;

        private Color _nonSelectedColor;

        private List<Label> _tabLabelList = new List<Label>();

        private List<Label> _alignTypeLabelList = new List<Label>();

        private TabType _tabType { get; set; } = TabType.Tab1;

        private AlignResultType _alignResultType { get; set; } = AlignResultType.All;

        private List<TrendResult> _alignTrendResults { get; set; } = new List<TrendResult>();
        #endregion

        #region 속성
        private ResultChartControl ChartControl = null;
        #endregion

        #region 생성자
        public AlignTrendControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void AlignTrendControl_Load(object sender, EventArgs e)
        {
            AddControl();
            InitializeUI();
        }

        private void AddControl()
        {
            ChartControl = new ResultChartControl();
            ChartControl.Dock = DockStyle.Fill;
            ChartControl.IsDailyInfo = false;
            ChartControl.ChartType = ResultChartControl.InspChartType.Align;
            pnlChart.Controls.Add(ChartControl);
            _alignTypeLabelList.AddRange(new Label[] { lblAllData, lblLx, lblLy, lblCx, lblRx, lblRy });
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
            List<string> header = new List<string>
                    {
                        "Time",
                        "ID",
                        "Stage",
                        "F",
                    };
            for (int index = 0; index < inspModel.TabCount; index++)
            {
                header.Add($"Tab");
                header.Add($"Judge");
                header.Add($"P");
                header.Add($"Lx");
                header.Add($"Ly");
                header.Add($"Cx");
                header.Add($"Rx");
                header.Add($"Ry");
            }
            var columns = header.Select(text => new DataGridViewTextBoxColumn { Name = text });
            dgvAlignTrendData.Columns.Clear();
            dgvAlignTrendData.Columns.AddRange(columns.ToArray());
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
                lbl.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Text = "Tab" + (tabIndex + 1);
                lbl.Size = size;
                lbl.Location = location;
                lbl.MouseClick += LabelControl_SetTabEventHandler;

                pnlTabs.Controls.Add(lbl);
                location.X += size.Width + margin;

                _tabLabelList.Add(lbl);
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
            
            UpdateChart(_tabType, _alignResultType);
        }

        public void SetAlignResultType(AlignResultType alignResultType)
        {
            ClearSelectedLabel(pnlChartTypes);
            _alignResultType = alignResultType;

            _alignTypeLabelList[(int)alignResultType].BackColor = _selectedColor;
            UpdateAlignResults(_alignResultType);
            UpdateChart(_tabType, alignResultType);
        }

        private void ClearSelectedLabel(Panel panel)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is Label label)
                    label.BackColor = _nonSelectedColor;
            }
        }

        private void lblAllData_Click(object sender, EventArgs e) => SetAlignResultType(AlignResultType.All);

        private void lblLx_Click(object sender, EventArgs e) => SetAlignResultType(AlignResultType.Lx);

        private void lblLy_Click(object sender, EventArgs e) => SetAlignResultType(AlignResultType.Ly);

        private void lblCx_Click(object sender, EventArgs e) => SetAlignResultType(AlignResultType.Cx);

        private void lblRx_Click(object sender, EventArgs e) => SetAlignResultType(AlignResultType.Rx);

        private void lblRy_Click(object sender, EventArgs e) => SetAlignResultType(AlignResultType.Ry);

        private void UpdateAlignResults(AlignResultType alignResultType)
        {
            foreach (var column in dgvAlignTrendData.Columns.Cast<DataGridViewColumn>())
                column.Visible = true;

            if (alignResultType != AlignResultType.All)
            {
                var alignTypes = Enum.GetNames(typeof(AlignResultType));
                string[] filters = alignTypes.Where(type => type != $"{AlignResultType.All}" && type != $"{alignResultType}").ToArray();
                foreach (var column in dgvAlignTrendData.Columns.Cast<DataGridViewColumn>().Where(column => filters.Any(filter => filter == column.HeaderText)))
                    column.Visible = false;
            }
        }

        private void UpdateChart(TabType tabType, AlignResultType alignResultType) => ChartControl.UpdateAlignChart(_alignTrendResults, tabType, alignResultType);

        public void UpdateDataGridView()
        {
            dgvAlignTrendData.Rows.Clear();
            for (int rowIndex = 0; rowIndex < _alignTrendResults.Count; rowIndex++)
                dgvAlignTrendData.Rows.Add(_alignTrendResults[rowIndex].GetAlignStringDatas().ToArray());
        }

        public void SetAlignResultData(string path)
        {
            List<string[]> readTexts = new List<string[]>();
            foreach (string textLine in File.ReadAllLines(path))
                readTexts.Add(textLine.Split(','));

            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            _alignTrendResults.Clear();
            for (int rowIndex = 1; rowIndex < readTexts.Count; rowIndex++)
            {
                var trendResult = new TrendResult();
                string[] datas = readTexts[rowIndex];

                trendResult.InspectionTime = datas[0];
                trendResult.PanelID = datas[1];
                trendResult.StageNo = Convert.ToInt32(datas[2]);
                trendResult.FinalHead = datas[3];

                for (int colIndex = 0, dataCount = 8; colIndex < inspModel.TabCount; colIndex++)
                {
                    int columnSkip = colIndex * dataCount;
                    var tabAlignResult = new TabAlignTrendResult
                    {
                        Tab = Convert.ToInt32(datas[columnSkip + 4]),
                        Judge = datas[columnSkip + 5],
                        PreHead = datas[columnSkip + 6],
                        Cx = datas[columnSkip + 7] == "-" ? 0 : Convert.ToDouble(datas[columnSkip + 7]),
                        Lx = datas[columnSkip + 8] == "-" ? 0 : Convert.ToDouble(datas[columnSkip + 8]),
                        Rx = datas[columnSkip + 9] == "-" ? 0 : Convert.ToDouble(datas[columnSkip + 9]),
                        Ly = datas[columnSkip + 10] == "-" ? 0 : Convert.ToDouble(datas[columnSkip + 10]),
                        Ry = datas[columnSkip + 11] == "-" ? 0 : Convert.ToDouble(datas[columnSkip + 11])
                    };
                    trendResult.TabAlignResults.Add(tabAlignResult);
                }
                _alignTrendResults.Add(trendResult);
            }
        }
        #endregion
    }
}
