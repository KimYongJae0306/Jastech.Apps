using Jastech.Apps.Structure.Data;
using Jastech.Framework.Winform.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class ProcessCapabilityControl : UserControl
    {
        #region 필드
        private Color _selectedColor;

        private Color _nonSelectedColor;

        private double _upperSpecLimit { get; set; } = 0.0;

        public double _lowerSpecLimit { get; set; } = 0.0;

        private TabType _tabType { get; set; } = TabType.Tab1;

        private AlignResultType _alignResultType { get; set; } = AlignResultType.All;

        public DateTime StartDate { get; set; } = DateTime.Now;

        private ResultChartControl ChartControl = new ResultChartControl() { Dock = DockStyle.Fill };

        private List<Label> _tabLabelList = new List<Label>();
        #endregion

        #region 속성
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        private DataTable _dataTable = new DataTable();

        private ProcessCapability ProcessCapability { get; set; } = new ProcessCapability();

        #region 생성자
        public ProcessCapabilityControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void ProcessCapabilityControl_Load(object sender, EventArgs e)
        {
            AddControl();
            InitializeUI();
        }

        private void AddControl()
        {
            ChartControl.ChartType = ResultChartControl.InspChartType.Align;
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            InitializeResultDataTable();
        }

        public void MakeTabListControl(int tabCount)
        {
            int controlWidth = 120;
            int controlHeight = 60;
            Point point = new Point(160, 0);
            int interval = 20;

            for (int tabIndex = 0; tabIndex < tabCount; tabIndex++)
            {
                Label lbl = new Label();

                lbl.BorderStyle = BorderStyle.FixedSingle;
                lbl.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Text = "Tab" + (tabIndex + 1);
                lbl.Margin = new Padding(0);
                lbl.Size = new Size(controlWidth, controlHeight);
                lbl.MouseClick += LabelControl_SetTabEventHandler;
                lbl.Location = point;

                tlpTab.Controls.Add(lbl);
                point.X += controlWidth + interval;

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
            _dataTable.Columns.Add("No");
            _dataTable.Columns.Add("Cp");
            _dataTable.Columns.Add("Cpk");
            _dataTable.Columns.Add("Pp");
            _dataTable.Columns.Add("Ppk");

            int dataCount = GetDataCount();
            SetDataCount(dataCount);

            InitializeDataGridView(_dataTable.Copy());
        }

        private void InitializeDataGridView(DataTable dataTable)
        {
            dgvPCResult.DataSource = dataTable;
        }

        private void lblDayCount_Click(object sender, EventArgs e)
        {
            int dayCount = SetLabelIntegerData(sender);
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
            int dataCount = SetLabelIntegerData(sender);
        }

        private int SetLabelIntegerData(object sender)
        {
            Label lbl = sender as Label;
            int prevData = Convert.ToInt32(lbl.Text);

            KeyPadForm keyPadForm = new KeyPadForm();
            keyPadForm.PreviousValue = (double)prevData;
            keyPadForm.ShowDialog();

            int inputData = Convert.ToInt16(keyPadForm.PadValue);

            Label label = (Label)sender;
            label.Text = inputData.ToString();

            return inputData;
        }

        private void lblUpperSpecLimit_Click(object sender, EventArgs e)
        {
            double usl = SetLabelDoubleData(sender);
            SetUpperSpecLimit(usl);
        }

        private void SetUpperSpecLimit(double  upperSpecLimit)
        {
            _upperSpecLimit = upperSpecLimit;
        }

        private void lblLowerSpecLimit_Click(object sender, EventArgs e)
        {
            double lsl = SetLabelDoubleData(sender);
            SetLowerSpecLimit(lsl);
        }

        private void SetLowerSpecLimit(double lowerSpecLimit)
        {
            _lowerSpecLimit = lowerSpecLimit;
        }

        private double SetLabelDoubleData(object sender)
        {
            Label lbl = sender as Label;
            double prevData = Convert.ToDouble(lbl.Text);

            KeyPadForm keyPadForm = new KeyPadForm();
            keyPadForm.PreviousValue = prevData;
            keyPadForm.ShowDialog();

            double inputData = keyPadForm.PadValue;

            Label label = (Label)sender;
            label.Text = inputData.ToString();

            return inputData;
        }
        #endregion

        private void dgvCPK_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void UpdateParameterDataGridView(string path)
        {
            //using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            //{
            //    int readLine = 0;
            //    int headerLine = 0;

            //    DataTable table = new DataTable();

            //    table.Columns.Add("Time");
            //    table.Columns.Add("Panel ID");
            //    table.Columns.Add("Tab");
            //    table.Columns.Add("Judge");
            //    table.Columns.Add("Lx");
            //    table.Columns.Add("Ly");
            //    table.Columns.Add("Cx");
            //    table.Columns.Add("Rx");
            //    table.Columns.Add("Ry");

            //    StreamReader sr = new StreamReader(fs);

            //    while (!sr.EndOfStream)
            //    {
            //        if (readLine == headerLine)
            //        {
            //            string header = sr.ReadLine();
            //        }
            //        else
            //        {
            //            string contents = sr.ReadLine();

            //            string[] dataArray = contents.Split(',');

            //            table.Rows.Add(dataArray[0], dataArray[1], dataArray[2], dataArray[3],
            //                dataArray[4], dataArray[5], dataArray[6], dataArray[7], dataArray[8]);
            //        }

            //        readLine++;
            //    }

            //    sr.Close();

            //    dgvAlignTrendData.DataSource = table;

            //    SetDataTable(table.Copy());
            //}
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
            //UpdateChart()
        }

        public void SetAlignResultType(AlignResultType alignResultType)
        {
            _alignResultType = alignResultType;
            UpdateSelectedAlignResultType(alignResultType);
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

        private void UpdateChart(TabType tabType, AlignResultType alignResultType)
        {
            if (_bindingDataTable == null)
                return;

            ChartControl.UpdateAlignChart(_bindingDataTable, tabType, alignResultType);
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

        public void UpdateDataGridView(string path)
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

                //dgvAlignTrendData.DataSource = table;

                SetDataTable(table.Copy());
            }
        }
    }
}
