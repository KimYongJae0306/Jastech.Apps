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
    public partial class AkkonTrendControl : UserControl
    {
        #region 필드
        private Color _selectedColor;

        private Color _nonSelectedColor;

        private List<Label> _tabLabelList = new List<Label>();

        private TabType _tabType { get; set; } = TabType.Tab1;

        private AkkonResultType _akkonResultType { get; set; } = AkkonResultType.All;

        private ResultChartControl ChartControl = new ResultChartControl() { Dock = DockStyle.Fill };
        #endregion

        #region 속성
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
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
            ChartControl.ChartType = ResultChartControl.InspChartType.Akkon;
            pnlChart.Controls.Add(ChartControl);
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);
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

            UpdateChart(_tabType, _akkonResultType);
        }

        private void ClearSelectedTabLabel()
        {
            foreach (Control control in pnlTabs.Controls)
            {
                if (control is Label)
                    control.BackColor = _nonSelectedColor;
            }
        }

        private void lblAkkon_Click(object sender, EventArgs e)
        {
            SetAkkonResultType(AkkonResultType.All);
            UpdateChart(_tabType, _akkonResultType);
        }

        private void lblCount_Click(object sender, EventArgs e)
        {
            SetAkkonResultType(AkkonResultType.Count);
            UpdateChart(_tabType, _akkonResultType);
        }

        private void lblLength_Click(object sender, EventArgs e)
        {
            SetAkkonResultType(AkkonResultType.Length);
            UpdateChart(_tabType, _akkonResultType);
        }

        private void lblStrength_Click(object sender, EventArgs e)
        {
            SetAkkonResultType(AkkonResultType.Strength);
            UpdateChart(_tabType, _akkonResultType);
        }

        private void lblStd_Click(object sender, EventArgs e)
        {
            SetAkkonResultType(AkkonResultType.STD);
            UpdateChart(_tabType, _akkonResultType);
        }

        public void SetAkkonResultType(AkkonResultType akkonResultType)
        {
            _akkonResultType = akkonResultType;
            UpdateSelectedAkkonResultType(akkonResultType);
        }

        private void UpdateSelectedAkkonResultType(AkkonResultType akkonResultType)
        {
            ClearSelectedAkkonTypeLabel();

            switch (akkonResultType)
            {
                case AkkonResultType.All:
                    lblAkkon.BackColor = _selectedColor;
                    break;

                case AkkonResultType.Count:
                    lblCount.BackColor = _selectedColor;
                    break;

                case AkkonResultType.Length:
                    lblLength.BackColor = _selectedColor;
                    break;

                case AkkonResultType.Strength:
                    lblStrength.BackColor = _selectedColor;
                    break;

                case AkkonResultType.STD:
                    lblStd.BackColor = _selectedColor;
                    break;

                default:
                    break;
            }
        }

        private void ClearSelectedAkkonTypeLabel()
        {
            foreach (Control control in pnlAkkonType.Controls)
            {
                if (control is Label)
                    control.BackColor = _nonSelectedColor;
            }
        }

        private void UpdateChart(TabType tabType, AkkonResultType akkonResultType)
        {
            if (_bindingDataTable == null)
                return;

            ChartControl.UpdateAkkonChart(_bindingDataTable, tabType, akkonResultType);
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
                table.Columns.Add("Count");
                table.Columns.Add("Length");
                table.Columns.Add("Strength");
                table.Columns.Add("STD");

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
                            dataArray[4], dataArray[5], dataArray[6], dataArray[7]);
                    }

                    readLine++;
                }

                sr.Close();

                dgvAkkonTrendData.DataSource = table;

                SetDataTable(table.Copy());
            }
        }
        #endregion
    }

    public enum AkkonResultType
    {
        All,
        Count,
        Length,
        Strength,
        STD,
    }


}
