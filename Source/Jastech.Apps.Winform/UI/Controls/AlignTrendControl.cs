using Jastech.Framework.Winform.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private ResultChartControl ChartControl = new ResultChartControl() { Dock = DockStyle.Fill };
        #endregion

        #region 속성
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        #endregion

        #region 메서드
        #endregion

        public AlignTrendControl()
        {
            InitializeComponent();
        }

        private void AlignTrendControl_Load(object sender, EventArgs e)
        {
            AddControl();
            InitializeUI();
        }

        private void AddControl()
        {
            ChartControl.ChartType = ResultChartControl.InspChartType.Align;
            tlpData.Controls.Add(ChartControl, 0, 0);
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);
            //SetTab((int)TabType.All);
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
                lbl.Size = new Size(controlWidth, controlHeight);
                lbl.MouseClick += LabelControl_SetTabEventHandler;
                lbl.Location = point;

                pnlTabs.Controls.Add(lbl);
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
            foreach (Control control in pnlTabs.Controls)
            {
                if (control is Label)
                    control.BackColor = _nonSelectedColor;
            }
        }

        private void lblAlign_Click(object sender, EventArgs e)
        {
            SetAlignResultType(AlignResultType.All);
            UpdateChart(_tabType, _alignResultType);
        }

        private void lblLx_Click(object sender, EventArgs e)
        {
            SetAlignResultType(AlignResultType.Lx);
            UpdateChart(_tabType, _alignResultType);
        }

        private void lblLy_Click(object sender, EventArgs e)
        {
            SetAlignResultType(AlignResultType.Ly);
            UpdateChart(_tabType, _alignResultType);
        }

        private void lblCx_Click(object sender, EventArgs e)
        {
            SetAlignResultType(AlignResultType.Cx);
            UpdateChart(_tabType, _alignResultType);
        }

        private void lblRx_Click(object sender, EventArgs e)
        {
            SetAlignResultType(AlignResultType.Rx);
            UpdateChart(_tabType, _alignResultType);
        }

        private void lblRy_Click(object sender, EventArgs e)
        {
            SetAlignResultType(AlignResultType.Ry);
            UpdateChart(_tabType, _alignResultType);
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
            foreach (Control control in pnlAlignType.Controls)
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

                dgvAlignTrendData.DataSource = table;

                SetDataTable(table.Copy());
            }
        }
    }

    public enum AlignResultType
    {
        All,
        Lx,
        Ly,
        Cx,
        Rx,
        Ry,
    }
}
