using Jastech.Framework.Winform.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AlignTrendControl : UserControl
    {
        #region 필드
        private Color _selectedColor;

        private Color _nonSelectedColor;

        private List<Label> _tabLabelList = new List<Label>();

        private TabType _tabType { get; set; } = TabType.Tab1;

        private AlignResultType _alignResultType { get; set; } = AlignResultType.All;
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
            ClearSelectedTabLabel();
            _tabType = tabType;

            _tabLabelList[(int)tabType].BackColor = _selectedColor;
            
            UpdateChart(_tabType, _alignResultType);
        }

        private void ClearSelectedTabLabel()
        {
            foreach (Control control in pnlTabs.Controls)
            {
                if (control is Label)
                    control.BackColor = _nonSelectedColor;
            }
        }

        private void lblAllData_Click(object sender, EventArgs e)
        {
            SetAlignResultType(AlignResultType.All);
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
        }

        private void UpdateSelectedAlignResultType(AlignResultType alignResultType)
        {
            ClearSelectedAlignTypeLabel();

            switch (alignResultType)
            {
                case AlignResultType.All:
                    lblAllData.BackColor = _selectedColor;
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
            foreach (Control control in pnlChartTypes.Controls)
            {
                if (control is Label)
                    control.BackColor = _nonSelectedColor;
            }
        }
        
        private void UpdateChart(TabType tabType, AlignResultType alignResultType)
        {
            if (GetDataTable() == null)
                return;

            ChartControl.UpdateAlignChart(GetDataTable(), tabType, alignResultType);
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

                dgvAlignTrendData.DataSource = table;

                SetDataTable(table.Copy());
            }
        }
        #endregion
    }
}
