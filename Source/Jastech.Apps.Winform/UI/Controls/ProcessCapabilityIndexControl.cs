using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Framework.Winform.Forms;
using Jastech.Framework.Winform.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
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

        public DateTime StartDate { get; set; } = DateTime.Now;

        private ResultChartControl ChartControl = new ResultChartControl() { Dock = DockStyle.Fill };

        private List<Label> _tabLabelList = new List<Label>();

        private DataTable _resultDataTable = new DataTable();
        #endregion

        #region 속성
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
            InitializeResultDataTable();
        }

        private void AddControl()
        {
            ChartControl.ChartType = ResultChartControl.InspChartType.Align;
            pnlChart.Controls.Add(ChartControl);
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);
        }

        public void MakeTabListControl(int tabCount)
        {
            //int controlWidth = 120;
            //int controlHeight = 60;
            //Point point = new Point(160, 0);
            //int interval = 20;

            for (int tabIndex = 0; tabIndex < tabCount; tabIndex++)
            {
                Label lbl = new Label();

                lbl.BorderStyle = BorderStyle.FixedSingle;
                lbl.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Text = "Tab" + (tabIndex + 1);
                lbl.Margin = new Padding(0);
                //lbl.Size = new Size(controlWidth, controlHeight);
                lbl.MouseClick += LabelControl_SetTabEventHandler;
                //lbl.Location = point;
                lbl.Dock = DockStyle.Fill;

                tlpTab.Controls.Add(lbl);
                //point.X += controlWidth + interval;

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
            ClearSelectedTabLabel();
            _tabType = tabType;

            _tabLabelList[(int)tabType].BackColor = _selectedColor;

            UpdateChart(_tabType, _alignResultType);
            UpdateDataGridView(_tabType, _alignResultType);
        }

        private void ClearSelectedTabLabel()
        {
            foreach (Control control in tlpTab.Controls)
            {
                if (control is Label)
                    control.BackColor = _nonSelectedColor;
            }
        }

        private int GetDataCount()
        {
            int dataCount = Convert.ToInt32(lblDataCount.Text);

            return dataCount;
        }

        private void SetDataCount(int dataCount)
        {
            int temp = dataCount;
        }

        private void InitializeResultDataTable()
        {
            _resultDataTable.Columns.Add("Type");
            _resultDataTable.Columns.Add("Cp");
            _resultDataTable.Columns.Add("Cpk");
            _resultDataTable.Columns.Add("Pp");
            _resultDataTable.Columns.Add("Ppk");

            int dataCount = GetDataCount();
            SetDataCount(dataCount);

            InitializeDataGridView(_resultDataTable.Copy());
        }

        private void InitializeDataGridView(DataTable dataTable)
        {
            dgvPCResult.DataSource = dataTable;
        }

        private void lblDayCount_Click(object sender, EventArgs e)
        {
            int dayCount = KeyPadHelper.SetLabelIntegerData((Label)sender);
        }

        public void SetSelectionStartDate(DateTime date)
        {
            StartDate = date;
        }

        private DateTime GetSelectionStartDate()
        {
            return StartDate;
        }

        private void SetData(int dayCount)
        {
            DateTime date = GetSelectionStartDate();

            for (int i = 0; i < dayCount; i++)
            {
                DateTime ndate = new DateTime(date.Year, date.Month, date.Day - i);              // Pick 날짜 기준으로 -1일씩 돌리기
                //string FileCheck = Main.DEFINE.SYS_DATADIR + Main.DEFINE.LOG_DATADIR + ndate.ToString("yyyyMMdd") + LV_TreeView01.SelectedNode.FullPath.Substring(ndate.ToShortDateString().Length - 2);       // 여기 좀 확인 필요
            }
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

        private void dgvCPK_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        private void lblLx_Click(object sender, EventArgs e)
        {
            SetAlignResultType(AlignResultType.Lx);
        }

        private void lblLy_Click(object sender, EventArgs e)
        {
            SetAlignResultType(AlignResultType.Ly);
        }

        private void lblCx_Click(object sender, EventArgs e)
        {
            SetAlignResultType(AlignResultType.Cx);
        }

        private void lblRx_Click(object sender, EventArgs e)
        {
            SetAlignResultType(AlignResultType.Rx);
        }

        private void lblRy_Click(object sender, EventArgs e)
        {
            SetAlignResultType(AlignResultType.Ry);
        }

        public void SetAlignResultType(AlignResultType alignResultType)
        {
            _alignResultType = alignResultType;
            UpdateSelectedAlignResultType(alignResultType);

            UpdateChart(_tabType, _alignResultType);
            UpdateDataGridView(_tabType, _alignResultType);
        }

        private void UpdateSelectedAlignResultType(AlignResultType alignResultType)
        {
            ClearSelectedAlignTypeLabel();

            switch (alignResultType)
            {
                case AlignResultType.All:
                    lblAlign.BackColor = _selectedColor;
                    break;

                case AlignResultType.Lx:
                    lblLx.BackColor = _selectedColor;
                    break;

                case AlignResultType.Ly:
                    lblLy.BackColor = _selectedColor;
                    break;

                case AlignResultType.Cx:
                    lblCx.BackColor = _selectedColor;
                    break;

                case AlignResultType.Rx:
                    lblRx.BackColor = _selectedColor;
                    break;

                case AlignResultType.Ry:
                    lblRy.BackColor = _selectedColor;
                    break;

                default:
                    break;
            }
        }

        private void ClearSelectedAlignTypeLabel()
        {
            foreach (Control control in tlpAlignTypes.Controls)
            {
                if (control is Label)
                    control.BackColor = _nonSelectedColor;
            }
        }

        private DataTable _bindingDataTable = new DataTable();
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

            string queryTab = SelectQuery(tabType);
            DataRow[] dataRowArray = dataTable.Select(queryTab);

            int columnIndex = dataTable.Columns.IndexOf(alignResultType.ToString());

            List<double> valueList = new List<double>();

            foreach (var item in dataRowArray)
            {
                var value = Convert.ToDouble(item.ItemArray[columnIndex]);
                valueList.Add(value);
            }

            double upperSpecLimit = Convert.ToDouble(lblUpperSpecLimit.Text);
            double lowerSpecLimit = Convert.ToDouble(lblLowerSpecLimit.Text);
            var result = ProcessCapabilityIndex.GetResult(valueList, upperSpecLimit, lowerSpecLimit);

            List<string> dataList = new List<string>();
            dataList.Add(alignResultType.ToString());
            dataList.Add(result.Cp.ToString());
            dataList.Add(result.Cpk.ToString());
            dataList.Add(result.Pp.ToString());
            dataList.Add(result.Ppk.ToString());

            DataTable newDataTable = new DataTable();
            newDataTable = _resultDataTable.Clone();


            //string[] dataArray = { alignResultType.ToString(), result.Cp.ToString(), result.Cpk.ToString(), result.Pp.ToString(), result.Ppk.ToString() };

            DataRow dr = newDataTable.NewRow();
            dr.ItemArray = dataList.ToArray();

            newDataTable.Rows.Add(dr);

            dgvPCResult.DataSource = newDataTable;
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
    }
}
