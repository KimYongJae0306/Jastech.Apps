using Jastech.Apps.Structure;
using Jastech.Framework.Winform.Forms;
using Jastech.Framework.Winform.Helper;
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
    public partial class AlignTrendPreviewControl : UserControl
    {
        #region 필드
        private int _dataCount { get; set; } = 10;

        private List<TrendResult> _alignTrendResults { get; set; } = new List<TrendResult>();

        private List<AlignTrendXControl> _alignTrendXControlList = null;
        #endregion

        #region 속성
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public AlignTrendPreviewControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void AlignTrendControl_Load(object sender, EventArgs e)
        {
            InitializeUI();
            AddControl();
        }

        private void AddControl()
        {
            var appsInspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            if (appsInspModel == null)
                return;

            var tabCount = appsInspModel.TabCount;

            _alignTrendXControlList = new List<AlignTrendXControl>();

            for (int tabNo = 0; tabNo < tabCount; tabNo++)
            {
                AlignTrendXControl alignXControl = new AlignTrendXControl() { Dock = DockStyle.Fill };
                alignXControl.SetTabNumber(tabNo);
                _alignTrendXControlList.Add(alignXControl);

                if (tabNo >= 5)
                    tlpChart.Controls.Add(alignXControl, 1, tabNo - 5);
                else
                    tlpChart.Controls.Add(alignXControl, 0, tabNo);
            }
        }

        private void InitializeUI()
        {
            InitializeChart();
            InitializeDataGridView();
        }

        private void InitializeChart()
        {
            var appsInspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            if (appsInspModel == null)
                return;

            var tabCount = appsInspModel.TabCount;

            tlpChart.RowStyles.Clear();
            tlpChart.ColumnStyles.Clear();

            if (tabCount > 5)
            {
                tlpChart.ColumnCount = 2;
                tlpChart.RowCount = (int)(Math.Ceiling((double)tabCount / (double)tlpChart.ColumnCount));
            }
            else
            {
                tlpChart.ColumnCount = 1;
                tlpChart.RowCount = tabCount;
            }

            for (int rowIndex = 0; rowIndex < tlpChart.RowCount; rowIndex++)
                tlpChart.RowStyles.Add(new RowStyle(SizeType.Percent, (float)(100 / tlpChart.RowCount)));

            for (int columnIndex = 0; columnIndex < tlpChart.ColumnCount; columnIndex++)
                tlpChart.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, (float)(100 / tlpChart.ColumnCount)));
        }

        private void InitializeDataGridView()
        {
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            List<string> header = new List<string>
                    {
                        "Time",
                        "ID",
                        "Stage",
                        "F",
                    };

            for (int index = 0; index < inspModel.TabCount; index++)
            {
                header.Add($"Tab");
                header.Add($"Judge");
                header.Add($"P");
                header.Add($"Lx");
                header.Add($"Ly");
                header.Add($"Cx");
                header.Add($"Rx");
                header.Add($"Ry");
            }

            var columns = header.Select(text => new DataGridViewTextBoxColumn { Name = text });
            dgvAlignTrendData.Columns.Clear();
            dgvAlignTrendData.Columns.AddRange(columns.ToArray());
        }

        private void lblDataCount_Click(object sender, EventArgs e)
        {
            int dataCount = KeyPadHelper.SetLabelIntegerData((Label)sender);
            SetDataCount(dataCount);
        }

        private void SetDataCount(int dataCount)
        {
            _dataCount = dataCount;
        }

        public void SetAlignResultData(string path)
        {
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
                        Lx = datas[columnSkip + 7] == "-" ? 0 : Convert.ToDouble(datas[columnSkip + 7]),
                        Ly = datas[columnSkip + 8] == "-" ? 0 : Convert.ToDouble(datas[columnSkip + 8]),
                        Cx = datas[columnSkip + 9] == "-" ? 0 : Convert.ToDouble(datas[columnSkip + 9]),
                        Rx = datas[columnSkip + 10] == "-" ? 0 : Convert.ToDouble(datas[columnSkip + 10]),
                        Ry = datas[columnSkip + 11] == "-" ? 0 : Convert.ToDouble(datas[columnSkip + 11])
                    };
                    trendResult.TabAlignResults.Add(tabAlignResult);
                }
                _alignTrendResults.Add(trendResult);
            }
        }

        public void UpdateDataGridView()
        {
            dgvAlignTrendData.Rows.Clear();
            for (int rowIndex = 0; rowIndex < _alignTrendResults.Count; rowIndex++)
                dgvAlignTrendData.Rows.Add(_alignTrendResults[rowIndex].GetAlignStringDatas().ToArray());
        }

        private void UpdateChart(int dataCount)
        {
            int tabNo = 0;
            foreach (var alignTrendXControl in _alignTrendXControlList)
            {
                alignTrendXControl.LxChartControl.UpdateAlignChart(_alignTrendResults, AlignResultType.Lx, tabNo, dataCount);
                alignTrendXControl.CxChartControl.UpdateAlignChart(_alignTrendResults, AlignResultType.Cx, tabNo, dataCount);
                alignTrendXControl.RxChartControl.UpdateAlignChart(_alignTrendResults, AlignResultType.Rx, tabNo, dataCount);

                tabNo++;
            }
        }

        private void lblSearch_Click(object sender, EventArgs e)
        {
            if (_dataCount <= 0)
                return;

            UpdateChart(_dataCount);
        }
        #endregion
    }
}
