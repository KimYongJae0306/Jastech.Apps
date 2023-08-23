using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
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

        private AlignResultType _alignResultType { get; set; } = AlignResultType.Lx;

        private DateTime _startDate { get; set; } = DateTime.Now;

        private List<Label> _tabLabelList = new List<Label>();

        private DataTable _bindingDataTable = new DataTable();
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
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            InitializeDataGridView();
        }

        public void MakeTabListControl(int tabCount)
        {
            Size size = new Size(60, 34);
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
            ClearSelectedTabLabel();
            _tabType = tabType;

            _tabLabelList[(int)tabType].BackColor = _selectedColor;

            UpdateChart(_tabType, _alignResultType);
            UpdateDataGridView(_tabType, _alignResultType);
        }

        private void ClearSelectedTabLabel()
        {
            foreach (Control control in pnlTabs.Controls)
            {
                if (control is Label)
                    control.BackColor = _nonSelectedColor;
            }
        }

        private void InitializeDataGridView()
        {
            dgvPCResult.DataSource = new PcResult();
        }

        private void lblDayCount_Click(object sender, EventArgs e)
        {
            int dayCount = KeyPadHelper.SetLabelIntegerData((Label)sender);
        }

        public void SetSelectionStartDate(DateTime date)
        {
            _startDate = date;
        }

        private DateTime GetSelectionStartDate()
        {
            return _startDate;
        }

        private void lblDataCount_Click(object sender, EventArgs e)
        {
            int dataCount = KeyPadHelper.SetLabelIntegerData((Label)sender);
        }

        private void lblUpperSpecLimit_Click(object sender, EventArgs e)
        {
            double usl = KeyPadHelper.SetLabelDoubleData((Label)sender);
            SetUpperSpecLimit(usl);
        }

        private void SetUpperSpecLimit(double  upperSpecLimit)
        {
            _upperSpecLimit = upperSpecLimit;
        }

        private void lblLowerSpecLimit_Click(object sender, EventArgs e)
        {
            double lsl = KeyPadHelper.SetLabelDoubleData((Label)sender);
            SetLowerSpecLimit(lsl);
        }

        private void SetLowerSpecLimit(double lowerSpecLimit)
        {
            _lowerSpecLimit = lowerSpecLimit;
        }

        public void UpdateAlignDataGridView(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                int readLine = 0;
                int headerLine = 0;

                DataTable table = new DataTable();

                table.Columns.Add("Time");
                table.Columns.Add("Panel ID");
                table.Columns.Add("Tab");
                table.Columns.Add("Judge");
                table.Columns.Add("Lx");
                table.Columns.Add("Ly");
                table.Columns.Add("Cx");
                table.Columns.Add("Rx");
                table.Columns.Add("Ry");

                StreamReader sr = new StreamReader(fs);

                while (!sr.EndOfStream)
                {
                    if (readLine == headerLine)
                    {
                        string header = sr.ReadLine();
                    }
                    else
                    {
                        string contents = sr.ReadLine();

                        string[] dataArray = contents.Split(',');

                        table.Rows.Add(dataArray[0], dataArray[1], dataArray[2], dataArray[3],
                            dataArray[4], dataArray[5], dataArray[6], dataArray[7], dataArray[8]);
                    }

                    readLine++;
                }

                sr.Close();

                dgvAlignData.DataSource = table;

                SetDataTable(table.Copy());
            }
        }

        public void SetAlignResultType(AlignResultType alignResultType)
        {
            _alignResultType = alignResultType;

            UpdateChart(_tabType, _alignResultType);
            UpdateDataGridView(_tabType, _alignResultType);
        }

        private void SetDataTable(DataTable dt)
        {
            _bindingDataTable = dt.Copy();
        }

        private DataTable GetDataTable()
        {
            return _bindingDataTable;
        }

        private void UpdateChart(TabType tabType, AlignResultType alignResultType)
        {
            if (GetDataTable() == null)
                return;

            ChartControl.UpdateAlignChart(GetDataTable(), tabType, alignResultType);
        }

        private DataTable ParseData(DataTable dataTable, int tabNo)
        {
            DataTable newDataTable = new DataTable();

            newDataTable = dataTable.Clone();

            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            for (int rowIndex = 0; rowIndex < dataTable.Rows.Count / inspModel.TabCount; rowIndex++)
            {
                int index = (rowIndex * inspModel.TabCount) + tabNo;

                var rowData = dataTable.Rows[index];

                newDataTable.Rows.Add(rowData.ItemArray);
            }

            return newDataTable;
        }

        private void UpdateDataGridView(TabType tabType, AlignResultType alignResultType)
        {
            var dataTable = GetDataTable();

            if (dataTable.Rows.Count == 0)
                return;

            string queryTab = SelectQuery(tabType);
            DataRow[] dataRowArray = dataTable.Select(queryTab);

            double upperSpecLimit = Convert.ToDouble(lblUpperSpecLimit.Text);
            double lowerSpecLimit = Convert.ToDouble(lblLowerSpecLimit.Text);

            var listCapabilityResults = new List<PcResult>();
            var resultTypes = Enum.GetNames(typeof(AlignResultType)).Where(name => name.ToUpper() != "ALL").ToList();
            for (int index = 0; index < resultTypes.Count; index++)
            {
                int columnIndex = dataTable.Columns.IndexOf(resultTypes[index]);

                var values = dataRowArray.Select(row => Convert.ToDouble(row[columnIndex])).ToList();
                var result = ProcessCapabilityIndex.GetResult(values, upperSpecLimit, lowerSpecLimit);
                result.Type = resultTypes[index];

                listCapabilityResults.Add(result);
            }
            dgvPCResult.DataSource = listCapabilityResults;
        }

        private string SelectQuery(TabType tabType)
        {
            string query = string.Empty;

            switch (tabType)
            {
                case TabType.Tab1:
                    query = "Tab = '0'";
                    break;

                case TabType.Tab2:
                    query = "Tab = '1'";
                    break;

                case TabType.Tab3:
                    query = "Tab = '2'";
                    break;
                case TabType.Tab4:
                    query = "Tab = '3'";
                    break;
                case TabType.Tab5:
                    query = "Tab = '4'";
                    break;
                default:
                    break;
            }

            return query;
        }
        #endregion

        private void dgvPCResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            AlignResultType alignType = (AlignResultType)(e.RowIndex + 1);
            SetAlignResultType(alignType);
        }
    }
}
