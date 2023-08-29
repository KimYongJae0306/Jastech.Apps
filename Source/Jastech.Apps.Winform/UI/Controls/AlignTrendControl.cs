using Jastech.Apps.Structure;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Imaging.Result;
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
            ChartControl.UpdateAlignChart(_alignTrendResults, tabType, alignResultType);
        }

        private List<TrendResult> _alignTrendResults { get; set; } = new List<TrendResult>();

        public void UpdateDataGridView()
        {
            for (int rowIndex = 0; rowIndex < _alignTrendResults.Count; rowIndex++)
            {
                if (rowIndex == 0)
                {
                    List<string> header = new List<string>
                    {
                        "Inspection Time",
                        "Panel ID",
                        "Stage No",
                        "Final Head",
                    };
                    for (int index = 0; index < AppsConfig.Instance().TabMaxCount; index++)
                    {
                        header.Add($"Tab");
                        header.Add($"Judge");
                        header.Add($"Pre Head");
                        header.Add($"Lx");
                        header.Add($"Ly");
                        header.Add($"Cx");
                        header.Add($"Rx");
                        header.Add($"Ry");
                    }
                    var columns = header.Select(text => new DataGridViewTextBoxColumn { Name = text });
                    dgvAlignTrendData.Columns.AddRange(columns.ToArray());
                }

                foreach (var datas in _alignTrendResults[rowIndex].GetStringDatas())
                    dgvAlignTrendData.Rows.Add(datas);
            }
        }

        public void SetAlignResultData(string path)
        {
            List<string[]> texts = new List<string[]>();
            foreach (string textLine in File.ReadAllLines(path))
                texts.Add(textLine.Split(','));

            int tabCount = AppsConfig.Instance().TabMaxCount;
            for (int rowIndex = 0; rowIndex < texts.Count; rowIndex++)
            {
                _alignTrendResults[rowIndex].InspectionTime = texts[rowIndex][0];
                _alignTrendResults[rowIndex].PanelID = texts[rowIndex][1];
                _alignTrendResults[rowIndex].StageNo = Convert.ToInt32(texts[rowIndex][2]);
                _alignTrendResults[rowIndex].FinalHead = texts[rowIndex][3];

                for (int colIndex = 0; colIndex < tabCount; colIndex++)
                {
                    int skipIndex = colIndex * tabCount;
                    _alignTrendResults[rowIndex].TabAlignResults[rowIndex] = new TabAlignTrendResult
                    {
                        Tab = Convert.ToInt32(texts[rowIndex][skipIndex + 4]),
                        Judge = texts[rowIndex][skipIndex + 5],
                        PreHead = texts[rowIndex][skipIndex + 6],
                        Lx = Convert.ToDouble(texts[rowIndex][skipIndex + 7]),
                        Ly = Convert.ToDouble(texts[rowIndex][skipIndex + 8]),
                        Cx = Convert.ToDouble(texts[rowIndex][skipIndex + 9]),
                        Rx = Convert.ToDouble(texts[rowIndex][skipIndex + 10]),
                        Ry = Convert.ToDouble(texts[rowIndex][skipIndex + 11]),
                    };
                }
            }
        }
        #endregion
    }
}
