using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Structure;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform.Forms;
using Jastech.Framework.Winform.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class ProcessCapabilityIndexControl : UserControl
    {
        #region 필드
        private Color _selectedColor;

        private Color _nonSelectedColor;

        private TabType _tabType { get; set; } = TabType.Tab1;

        private AlignResultType _alignResultType { get; set; } = AlignResultType.All;

        private List<Label> _tabLabelList = new List<Label>();

        private List<Label> _alignTypeLabelList = new List<Label>();

        private List<TrendResult> _alignTrendResults = new List<TrendResult>();

        private readonly ProcessCapabilityIndex _processCapabilityIndex = new ProcessCapabilityIndex();
        #endregion

        #region 속성
        private ResultChartControl ChartControl = null;

        #endregion

        #region 생성자
        public ProcessCapabilityIndexControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void ProcessCapabilityIndexControl_Load(object sender, EventArgs e)
        {
            AddControl();
            InitializeUI();

            var config = AppsConfig.Instance();
            lblCapabilityUSL.DataBindings.Add("Text", config, "CapabilityUSL", false, DataSourceUpdateMode.OnPropertyChanged);
            lblCapabilityLSL.DataBindings.Add("Text", config, "CapabilityLSL", false, DataSourceUpdateMode.OnPropertyChanged);
            lblPerformanceUSL_Center.DataBindings.Add("Text", config, "PerformanceUSL_Center", false, DataSourceUpdateMode.OnPropertyChanged);
            lblPerformanceLSL_Center.DataBindings.Add("Text", config, "PerformanceLSL_Center", false, DataSourceUpdateMode.OnPropertyChanged);
            lblPerformanceUSL_Side.DataBindings.Add("Text", config, "PerformanceUSL_Side", false, DataSourceUpdateMode.OnPropertyChanged);
            lblPerformanceLSL_Side.DataBindings.Add("Text", config, "PerformanceLSL_Side", false, DataSourceUpdateMode.OnPropertyChanged);
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

        public void MakeTabListControl(int tabCount)
        {
            Size size = new Size(60, 56);
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
            ClearSelectedTabLabel(pnlTabs);
            _tabType = tabType;
            _tabLabelList[(int)tabType].BackColor = _selectedColor;

            UpdateChart(_tabType, _alignResultType);
            UpdatePcInfoDataGridView(_tabType);
        }

        private void ClearSelectedTabLabel(Panel panel)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is Label label)
                    label.BackColor = _nonSelectedColor;
            }
        }

        private void InitializeDataGridView()
        {
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            dgvPCResult.Columns.Clear();
            dgvPCResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            List<string> pcHeader = new List<string>
            {
                "Type",
                "Cp",
                "Cpk",
                "Pp",
                "Ppk",
            };
            var pcColumns = pcHeader.Select(text => new DataGridViewTextBoxColumn { Name = text, SortMode = DataGridViewColumnSortMode.NotSortable});
            dgvPCResult.Columns.AddRange(pcColumns.ToArray());

            dgvAlignData.Columns.Clear();
            List<string> alignHeader = new List<string>
            {
                "Time",
                "ID",
                "Stage",
                "F",
            };
            for (int index = 0; index < inspModel.TabCount; index++)
            {
                alignHeader.Add($"Tab");
                alignHeader.Add($"Judge");
                alignHeader.Add($"P");
                alignHeader.Add($"Cx");
                alignHeader.Add($"Lx");
                alignHeader.Add($"Rx");
                alignHeader.Add($"Ly");
                alignHeader.Add($"Ry");
            }
            var alignColumns = alignHeader.Select(text => new DataGridViewTextBoxColumn { Name = text, SortMode = DataGridViewColumnSortMode.NotSortable });
            dgvAlignData.Columns.AddRange(alignColumns.ToArray());
        }

        private void lblAllData_Click(object sender, EventArgs e) => SetAlignResultType(AlignResultType.All);

        private void lblLx_Click(object sender, EventArgs e) => SetAlignResultType(AlignResultType.Lx);

        private void lblLy_Click(object sender, EventArgs e) => SetAlignResultType(AlignResultType.Ly);

        private void lblCx_Click(object sender, EventArgs e) => SetAlignResultType(AlignResultType.Cx);

        private void lblRx_Click(object sender, EventArgs e) => SetAlignResultType(AlignResultType.Rx);

        private void lblRy_Click(object sender, EventArgs e) => SetAlignResultType(AlignResultType.Ry);

        private void lblSpecLimit_Click(object sender, EventArgs e)
        {
            if (sender is Label label)
            {
                double oldSpec = Convert.ToDouble(label.Text);
                double newSpec = KeyPadHelper.SetLabelDoubleData(label);

                ParamTrackingLogger.AddChangeHistory("6Sigma", label.Text, oldSpec, newSpec);
            }
        }

        public void SetAlignResultType(AlignResultType alignResultType)
        {
            ClearSelectedTabLabel(pnlChartTypes);
            _alignResultType = alignResultType;

            _alignTypeLabelList[(int)alignResultType].BackColor = _selectedColor;
            UpdateChart(_tabType, _alignResultType);
        }

        private void UpdateChart(TabType tabType, AlignResultType alignResultType) => ChartControl.UpdateAlignChart(_alignTrendResults, tabType, alignResultType);

        public void UpdateAlignDataGridView()
        {
            dgvAlignData.Rows.Clear();
            for (int rowIndex = 0; rowIndex < _alignTrendResults.Count; rowIndex++)
                dgvAlignData.Rows.Add(_alignTrendResults[rowIndex].GetAlignStringDatas().ToArray());
        }

        private void UpdatePcInfoDataGridView(TabType tabType)
        {
            List<PcResult> listCapabilityResults = new List<PcResult>();
            var config = AppsConfig.Instance();

            var lxData = GetPcData(tabType, AlignResultType.Lx);
            var lyData = GetPcData(tabType, AlignResultType.Ly);
            var cxData = GetPcData(tabType, AlignResultType.Cx);
            var rxData = GetPcData(tabType, AlignResultType.Rx);
            var ryData = GetPcData(tabType, AlignResultType.Ry);

            _processCapabilityIndex.CapabilityUSL = config.CapabilityUSL;
            _processCapabilityIndex.CapabilityLSL = config.CapabilityLSL;
            _processCapabilityIndex.PerformanceUSL_Center = config.PerformanceUSL_Center;
            _processCapabilityIndex.PerformanceLSL_Center = config.PerformanceLSL_Center;
            _processCapabilityIndex.PerformanceUSL_Side = config.PerformanceUSL_Side;
            _processCapabilityIndex.PerformanceLSL_Side = config.PerformanceLSL_Side;

            listCapabilityResults.Add(_processCapabilityIndex.GetResult($"{AlignResultType.Lx}", lxData));
            listCapabilityResults.Add(_processCapabilityIndex.GetResult($"{AlignResultType.Ly}", lyData));
            listCapabilityResults.Add(_processCapabilityIndex.GetResult($"{AlignResultType.Cx}", cxData));
            listCapabilityResults.Add(_processCapabilityIndex.GetResult($"{AlignResultType.Rx}", rxData));
            listCapabilityResults.Add(_processCapabilityIndex.GetResult($"{AlignResultType.Ry}", ryData));

            dgvPCResult.Rows.Clear();
            foreach (var pcResult in listCapabilityResults)
            {
                string[] row = new string[]
                {
                    $"{pcResult.Type}",
                    $"{pcResult.Cp}",
                    $"{pcResult.Cpk}",
                    $"{pcResult.Pp}",
                    $"{pcResult.Ppk}",
                };
                dgvPCResult.Rows.Add(row);
            }
        }

        private List<double> GetPcData(TabType tabType, AlignResultType alignResultType)
        {
            return _alignTrendResults.Select(trendResult => trendResult
                                                        .GetAlignDatas(tabType, alignResultType))
                                                        .SelectMany(value => value).ToList();
        }

        public void SetAlignResultData(string path)
        {
            _alignTrendResults.Clear();
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

        private void dgvPCResult_DataSourceChanged(object sender, EventArgs e)
        {
            for (int index = 1; index < dgvPCResult.ColumnCount; index++)
            {
                dgvPCResult.Columns[index].MinimumWidth = tlpPCResult.Width / 4 - 20;
                dgvPCResult.Columns[index].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        #endregion
    }
}
