﻿using Jastech.Apps.Structure;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Structure;
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
    public partial class AkkonTrendControl : UserControl
    {
        #region 필드
        private Color _selectedColor;

        private Color _nonSelectedColor;

        private List<Label> _tabLabelList = new List<Label>();

        private TabType _tabType { get; set; } = TabType.Tab1;

        private AkkonResultType _akkonResultType { get; set; } = AkkonResultType.All;
        #endregion

        #region 속성
        private ResultChartControl ChartControl = null;
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
            ChartControl = new ResultChartControl();
            ChartControl.Dock = DockStyle.Fill;
            ChartControl.ChartType = ResultChartControl.InspChartType.Akkon;
            pnlChart.Controls.Add(ChartControl);
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

            dgvAkkonTrendData.Columns.Clear();
            List<string> header = new List<string>
            {
                "Inspection Time",
                "Panel ID",
                "Stage No",
            };
            for (int index = 0; index < inspModel.TabCount; index++)
            {
                header.Add($"Tab_{index + 1}");
                header.Add($"Judge_{index + 1}");
                header.Add($"Avg Count_{index + 1}");
                header.Add($"Avg Length_{index + 1}");
            }
            var columns = header.Select(text => new DataGridViewTextBoxColumn { Name = text });
            dgvAkkonTrendData.Columns.AddRange(columns.ToArray());
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
                lbl.BackColor = Color.FromArgb(52, 52, 52);
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

        private void lblAllData_Click(object sender, EventArgs e)
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

                default:
                    break;
            }
        }

        private void ClearSelectedAkkonTypeLabel()
        {
            foreach (Control control in pnlChartTypes.Controls)
            {
                if (control is Label)
                    control.BackColor = _nonSelectedColor;
            }
        }

        private void UpdateChart(TabType tabType, AkkonResultType akkonResultType)
        {
            if (_akkonTrendResults == null)
                return;

            ChartControl.UpdateAkkonChart(_akkonTrendResults, tabType, akkonResultType);
        }

        public void UpdateDataGridView()
        {
            dgvAkkonTrendData.Rows.Clear();
            for (int rowIndex = 1; rowIndex < _akkonTrendResults.Count; rowIndex++)
                dgvAkkonTrendData.Rows.Add(_akkonTrendResults[rowIndex].GetAkkonStringDatas().ToArray());
        }

        private List<TrendResult> _akkonTrendResults = new List<TrendResult>();
        public void SetAkkonResultData(string path)
        {
            List<string[]> texts = new List<string[]>();
            foreach (string textLine in File.ReadAllLines(path))
                texts.Add(textLine.Split(','));

            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            _akkonTrendResults.Clear();
            for (int rowIndex = 1; rowIndex < texts.Count; rowIndex++)
            {
                TrendResult trendResult = new TrendResult();
                trendResult.InspectionTime = texts[rowIndex][0];
                trendResult.PanelID = texts[rowIndex][1];
                trendResult.StageNo = Convert.ToInt32(texts[rowIndex][2]);

                for (int colIndex = 0, dataCount = 4; colIndex < inspModel.TabCount; colIndex++)
                {
                    int skipIndex = colIndex * dataCount;
                    var akkonResult = new TabAkkonTrendResult
                    {
                        Tab = Convert.ToInt32(texts[rowIndex][skipIndex + 3]),
                        Judge = texts[rowIndex][skipIndex + 4],
                        AvgCount = Convert.ToInt32(texts[rowIndex][skipIndex + 5]),
                        AvgLength = Convert.ToDouble(texts[rowIndex][skipIndex + 6]),
                    };
                    trendResult.TabAkkonResults.Add(akkonResult);
                }
                _akkonTrendResults.Add(trendResult);
            }
        }
        #endregion
    }
}
