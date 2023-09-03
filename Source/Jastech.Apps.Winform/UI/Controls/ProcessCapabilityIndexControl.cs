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
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class ProcessCapabilityIndexControl : UserControl
    {
        #region 필드
        private Color _selectedColor;

        private Color _nonSelectedColor;

        private double _upperSpecLimit { get; set; } = 0.0;

        public double _lowerSpecLimit { get; set; } = 0.0;

        private TabType _tabType { get; set; } = TabType.Tab1;

        private AlignResultType _alignResultType { get; set; } = AlignResultType.All;

        private List<Label> _tabLabelList = new List<Label>();

        private List<Label> _alignTypeLabelList = new List<Label>();

        private List<TrendResult> _alignTrendResults = new List<TrendResult>();
        #endregion

        #region 속성
        private ResultChartControl ChartControl = null;

        private ProcessCapabilityIndex ProcessCapabilityIndex { get; set; } = new ProcessCapabilityIndex();
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
        }

        private void AddControl()
        {
            ChartControl = new ResultChartControl();
            ChartControl.Dock = DockStyle.Fill;
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
            var pcColumns = pcHeader.Select(text => new DataGridViewTextBoxColumn { Name = text});
            dgvPCResult.Columns.AddRange(pcColumns.ToArray());

            dgvAlignData.Columns.Clear();
            List<string> alignHeader = new List<string>
            {
                "Inspection Time",
                "Panel ID",
                "Stage No",
                "Final Head",
            };
            for (int index = 0; index < inspModel.TabCount; index++)
            {
                alignHeader.Add($"Tab");
                alignHeader.Add($"Judge");
                alignHeader.Add($"Pre Head");
                alignHeader.Add($"Lx");
                alignHeader.Add($"Ly");
                alignHeader.Add($"Cx");
                alignHeader.Add($"Rx");
                alignHeader.Add($"Ry");
            }
            var alignColumns = alignHeader.Select(text => new DataGridViewTextBoxColumn { Name = text });
            dgvAlignData.Columns.AddRange(alignColumns.ToArray());
        }

        private void lblAllData_Click(object sender, EventArgs e) => SetAlignResultType(AlignResultType.All);

        private void lblLx_Click(object sender, EventArgs e) => SetAlignResultType(AlignResultType.Lx);

        private void lblLy_Click(object sender, EventArgs e) => SetAlignResultType(AlignResultType.Ly);

        private void lblCx_Click(object sender, EventArgs e) => SetAlignResultType(AlignResultType.Cx);

        private void lblRx_Click(object sender, EventArgs e) => SetAlignResultType(AlignResultType.Rx);

        private void lblRy_Click(object sender, EventArgs e) => SetAlignResultType(AlignResultType.Ry);

        private void lblUpperSpecLimit_Click(object sender, EventArgs e)
        {
            double usl = KeyPadHelper.SetLabelDoubleData((Label)sender);
            SetUpperSpecLimit(usl);
        }
        private void lblLowerSpecLimit_Click(object sender, EventArgs e)
        {
            double lsl = KeyPadHelper.SetLabelDoubleData((Label)sender);
            SetLowerSpecLimit(lsl);
        }

        private void SetUpperSpecLimit(double upperSpecLimit) => _upperSpecLimit = upperSpecLimit;

        private void SetLowerSpecLimit(double lowerSpecLimit) => _lowerSpecLimit = lowerSpecLimit;

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
            for (int rowIndex = 1; rowIndex < _alignTrendResults.Count; rowIndex++)
                dgvAlignData.Rows.Add(_alignTrendResults[rowIndex].GetAlignStringDatas().ToArray());
        }

        private void UpdatePcInfoDataGridView(TabType tabType)
        {
            List<PcResult> listCapabilityResults = new List<PcResult>();

            var lxData = GetPcData(tabType, AlignResultType.Lx);
            var lyData = GetPcData(tabType, AlignResultType.Ly);
            var cxData = GetPcData(tabType, AlignResultType.Cx);
            var rxData = GetPcData(tabType, AlignResultType.Rx);
            var ryData = GetPcData(tabType, AlignResultType.Ry);

            listCapabilityResults.Add(ProcessCapabilityIndex.GetResult(AlignResultType.Lx.ToString(), lxData, _upperSpecLimit, _lowerSpecLimit));
            listCapabilityResults.Add(ProcessCapabilityIndex.GetResult(AlignResultType.Ly.ToString(), lyData, _upperSpecLimit, _lowerSpecLimit));
            listCapabilityResults.Add(ProcessCapabilityIndex.GetResult(AlignResultType.Cx.ToString(), cxData, _upperSpecLimit, _lowerSpecLimit));
            listCapabilityResults.Add(ProcessCapabilityIndex.GetResult(AlignResultType.Rx.ToString(), rxData, _upperSpecLimit, _lowerSpecLimit));
            listCapabilityResults.Add(ProcessCapabilityIndex.GetResult(AlignResultType.Ry.ToString(), ryData, _upperSpecLimit, _lowerSpecLimit));

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
                        Lx = Convert.ToDouble(datas[columnSkip + 7]),
                        Ly = Convert.ToDouble(datas[columnSkip + 8]),
                        Cx = Convert.ToDouble(datas[columnSkip + 9]),
                        Rx = Convert.ToDouble(datas[columnSkip + 10]),
                        Ry = Convert.ToDouble(datas[columnSkip + 11])
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
