using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class UPHControl : UserControl
    {
        #region 필드
        private Color _selectedColor;

        private Color _nonSelectedColor;
        #endregion

        #region 속성
        private UPHData UPHData { get; set; } = new UPHData();
        #endregion

        #region 생성자
        public UPHControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void UPHControl_Load(object sender, EventArgs e)
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            dgvUPHData.RowHeadersVisible = false;
            dgvUPHData.AllowUserToAddRows = false;

            dgvUPHData.ColumnCount = UPHData.ColumnHeader.Count;

            for (int index = 0; index < dgvUPHData.ColumnCount; index++)
            {
                dgvUPHData.Columns[index].HeaderText = UPHData.ColumnHeader[index].ToString();
                dgvUPHData.Columns[index].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            for (int index = 0; index < UPHData.RowHeader.Count; index++)
                dgvUPHData.Rows.Add(UPHData.RowHeader[index]);

            dgvUPHData.CurrentCell = null;
            dgvUPHData.ClearSelection();
        }

        private void ClearUI()
        {
            ClearLabel();
            ClearDataGridView();
            ClearChart();
        }

        private void ClearLabel()
        {
            lblTotal.Text = "TOTAL : 0";
            lblTotalOK.Text = "OK : 0";
            lblTotalNG.Text = "NG : 0";
            lblTotalFail.Text = "FAIL : 0";
        }

        private void ClearDataGridView()
        {
            ResetData();

            dgvUPHData.Columns.Clear();
            dgvUPHData.Rows.Clear();

            InitializeDataGridView();
        }

        private void ResetData()
        {
            UPHData.OkCount = Enumerable.Repeat(0, 24).ToArray<int>();
            UPHData.NGCount = Enumerable.Repeat(0, 24).ToArray<int>();
            UPHData.FailCount = Enumerable.Repeat(0, 24).ToArray<int>();

            UPHData.MarkNG = Enumerable.Repeat(0, 24).ToArray<int>();
            UPHData.CountNG = Enumerable.Repeat(0, 24).ToArray<int>();
            UPHData.LengthNG = Enumerable.Repeat(0, 24).ToArray<int>();
        }

        public void ClearChart()
        {
            chtPie.Series.Clear();
            chtBar.Series.Clear();
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {
            UpdateBarChart(BarChartContentsType.TotalCount);
        }

        private void lblTotalOK_Click(object sender, EventArgs e)
        {
            UpdateBarChart(BarChartContentsType.OkCount);
        }

        private void lblTotalNG_Click(object sender, EventArgs e)
        {
            UpdateBarChart(BarChartContentsType.NgCount);
        }

        private void lblTotalFail_Click(object sender, EventArgs e)
        {
            UpdateBarChart(BarChartContentsType.FailCount);
        }

        private void ClearLabelColor()
        {
            foreach (Label lbl in pnlBasicFunction.Controls)
                lbl.BackColor = _nonSelectedColor;
        }

        private void lblExport_Click(object sender, EventArgs e)
        {
            ExportFile();
        }

        private void ExportFile()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.OverwritePrompt = true;
            sfd.InitialDirectory = @"D:\";
            sfd.FileName = DateTime.Now.ToString("yyyyMMdd") + "_Cell Data.csv";
            sfd.Filter = "csv파일(*.csv)|*.csv|모든파일(*.*)|*.*";
        }

        public void ReadDataFromCSVFile(string path)
        {
            ClearUI();

            Tuple<string[], List<string[]>> readData = CSVHelper.ReadData(path);

            if (ParseData(readData.Item1, readData.Item2))
            {
                UpdateLabelData();
                UpdateDataGridView();
                UpdatePieChart();
                UpdateBarChart(BarChartContentsType.TotalCount);
            }
        }

        private bool ParseData(string[] headers, List<string[]> datas)
        {
            try
            {
                for (int index = 0; index < datas.Count; index += 5)
                {
                    bool isFail = false;
                    bool isNg = false;

                    for (int innerIndex = 0; innerIndex < 5; innerIndex++)
                    {
                        // "[" 인덱스 찾기
                        int hourStringIndex = datas[index + innerIndex][0].IndexOf("[");

                        // "[" 부터 2글자가 시간
                        int hour = Convert.ToInt32(datas[index +innerIndex][0].Substring(hourStringIndex + 1, 2));

                        if (datas[index + innerIndex][15].ToLower().Contains("fail") || datas[index + innerIndex][16].ToLower().Contains("fail"))
                            isFail = true;

                        if (datas[index + innerIndex][15].ToLower().Contains("ng") || datas[index + innerIndex][16].ToLower().Contains("ng"))
                            isNg = true;

                        SortJudge(hour, isFail, isNg);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        private void SortJudge(int hour, bool isFail, bool isNg)
        {
            if (isFail)
                UPHData.FailCount[hour]++;
            else
            {
                if (isNg)
                    UPHData.NGCount[hour]++;
                else
                    UPHData.OkCount[hour]++;
            }
        }


        private void UpdateLabelData()
        {
            lblTotal.Text = "Total : " + (UPHData.OkCount.Sum() + UPHData.NGCount.Sum() + UPHData.FailCount.Sum()).ToString();
            lblTotalOK.Text = "OK : " + UPHData.OkCount.Sum().ToString();
            lblTotalNG.Text = "NG : " + UPHData.NGCount.Sum().ToString();
            lblTotalFail.Text = "FAIL : " + UPHData.FailCount.Sum().ToString();
        }

        private void UpdateDataGridView()
        {
            for (int hour = 0; hour < UPHData.HOUR; hour++)
            {
                dgvUPHData.Rows[hour].Cells[1].Value = UPHData.OkCount[hour];
                dgvUPHData.Rows[hour].Cells[2].Value = UPHData.NGCount[hour];
                dgvUPHData.Rows[hour].Cells[3].Value = UPHData.FailCount[hour];
                dgvUPHData.Rows[hour].Cells[4].Value = UPHData.OkCount[hour] + UPHData.NGCount[hour] + UPHData.FailCount[hour];

                dgvUPHData.Rows[hour].Cells[5].Value = UPHData.MarkNG[hour];
                dgvUPHData.Rows[hour].Cells[6].Value = UPHData.CountNG[hour];
                dgvUPHData.Rows[hour].Cells[7].Value = UPHData.LengthNG[hour];
                dgvUPHData.Rows[hour].Cells[8].Value = UPHData.MarkNG[hour] + UPHData.CountNG[hour] + UPHData.LengthNG[hour];
            }

            dgvUPHData.Rows[UPHData.HOUR].Cells[1].Value = UPHData.OkCount.Sum();
            dgvUPHData.Rows[UPHData.HOUR].Cells[2].Value = UPHData.NGCount.Sum();
            dgvUPHData.Rows[UPHData.HOUR].Cells[3].Value = UPHData.FailCount.Sum();
            dgvUPHData.Rows[UPHData.HOUR].Cells[4].Value = UPHData.OkCount.Sum() + UPHData.NGCount.Sum() + UPHData.FailCount.Sum();

            dgvUPHData.Rows[UPHData.HOUR].Cells[5].Value = UPHData.MarkNG.Sum();
            dgvUPHData.Rows[UPHData.HOUR].Cells[6].Value = UPHData.CountNG.Sum();
            dgvUPHData.Rows[UPHData.HOUR].Cells[7].Value = UPHData.LengthNG.Sum();
            dgvUPHData.Rows[UPHData.HOUR].Cells[8].Value = UPHData.MarkNG.Sum() + UPHData.CountNG.Sum() + UPHData.LengthNG.Sum();
        }

        private void UpdatePieChart()
        {
            chtPie.Series.Clear();
            Series series = chtPie.Series.Add("Count");
            series.IsValueShownAsLabel = true;
            chtPie.Series[0].ChartType = SeriesChartType.Pie;

            int total = UPHData.OkCount.Sum() + UPHData.NGCount.Sum() + UPHData.FailCount.Sum();
            double okRatio = UPHData.OkCount.Sum() / (double)total;
            double ngRatio = UPHData.NGCount.Sum() / (double)total;
            double failRatio = UPHData.FailCount.Sum() / (double)total;

            series.Points.AddXY("OK", $"{okRatio:F2}");
            series.Points.AddXY("NG", $"{ngRatio:F2}");
            series.Points.AddXY("FAIL", $"{failRatio:F2}");

            series.Points[0].Color = Color.Lime;
            series.Points[1].Color = Color.LightCoral;
            series.Points[2].Color = Color.FromArgb(104, 104, 104);

            series.Points[0].Label = okRatio > 0 ? $"OK\r\n{okRatio * 100:F2}%" : "";
            series.Points[1].Label = ngRatio > 0 ? $"NG\r\n{ngRatio * 100:F2}%" : "";
            series.Points[2].Label = failRatio > 0 ? $"FAIL\r\n{failRatio * 100:F2}%" : "";

            series.Points[1].LegendText = $"NG : {ngRatio * 100:F2}%";
            series.Points[2].LegendText = $"FAIL : {failRatio * 100:F2}%";
            series.Points[0].LegendText = $"OK : {okRatio * 100:F2}%";
        }

        private void UpdateBarChart(BarChartContentsType type)
        {
            ClearLabelColor();

            chtBar.Series.Clear();
            chtBar.ChartAreas[0].AxisX.Interval = 1;
            chtBar.ChartAreas[0].AxisX.IntervalOffset = 0;
            chtBar.ChartAreas[0].AxisX.MajorGrid.Interval = 1;

            Series series = chtBar.Series.Add("Count");
            series.IsValueShownAsLabel = true;
            chtBar.Series[0].ChartType = SeriesChartType.Column;
            chtBar.Series[0].Color = Color.FromArgb(104, 104, 104);
            chtBar.Series[0].LabelForeColor = Color.White;

            string time = string.Empty;

            switch (type)
            {
                case BarChartContentsType.TotalCount:
                    lblTotal.BackColor = _selectedColor;
                    for (int hour = 0; hour < UPHData.HOUR; hour++)
                    {
                        time = string.Format("{0:00}H ~ {1:00}H", hour, (hour + 1 != UPHData.HOUR) ? hour + 1 : 0);
                        series.Points.AddXY(time, UPHData.OkCount[hour] + UPHData.NGCount[hour] + UPHData.FailCount[hour]);
                    }
                    break;
                case BarChartContentsType.OkCount:
                    lblTotalOK.BackColor = _selectedColor;
                    for (int hour = 0; hour < UPHData.HOUR; hour++)
                    {
                        time = string.Format("{0:00}H ~ {1:00}H", hour, (hour + 1 != UPHData.HOUR) ? hour + 1 : 0);
                        series.Points.AddXY(time, UPHData.OkCount[hour]);
                    }
                    break;
                case BarChartContentsType.NgCount:
                    lblTotalNG.BackColor = _selectedColor;
                    for (int hour = 0; hour < UPHData.HOUR; hour++)
                    {
                        time = string.Format("{0:00}H ~ {1:00}H", hour, (hour + 1 != UPHData.HOUR) ? hour + 1 : 0);
                        series.Points.AddXY(time, UPHData.NGCount[hour]);
                    }
                    break;
                case BarChartContentsType.FailCount:
                    lblTotalFail.BackColor = _selectedColor;
                    for (int hour = 0; hour < UPHData.HOUR; hour++)
                    {
                        time = string.Format("{0:00}H ~ {1:00}H", hour, (hour + 1 != UPHData.HOUR) ? hour + 1 : 0);
                        series.Points.AddXY(time, UPHData.FailCount[hour]);
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion
    }

    public enum BarChartContentsType
    {
        TotalCount,
        OkCount,
        NgCount,
        FailCount
    }

    public class UPHData
    {
        public List<string> ColumnHeader = new List<string> { "Time", "OK", "NG", "FAIL", "Total",
                                                        "NG (Mark)", "NG (Count)", "NG (Length)", "Total Tab NG" };

        public List<string> RowHeader = new List<string>{"00:00 ~ 01:00", "01:00 ~ 02:00", "02:00 ~ 03:00", "03:00 ~ 04:00",
                                                    "04:00 ~ 05:00", "05:00 ~ 06:00", "06:00 ~ 07:00", "07:00 ~ 08:00",
                                                    "08:00 ~ 09:00", "09:00 ~ 10:00", "10:00 ~ 11:00", "11:00 ~ 12:00",
                                                    "12:00 ~ 13:00", "13:00 ~ 14:00", "14:00 ~ 15:00", "15:00 ~ 16:00",
                                                    "16:00 ~ 17:00", "17:00 ~ 18:00", "18:00 ~ 19:00", "19:00 ~ 20:00",
                                                    "20:00 ~ 21:00", "21:00 ~ 22:00", "22:00 ~ 23:00", "23:00 ~ 24:00",
                                                    "ALL TIME"};

        public const int HOUR = 24;

        public int[] OkCount { get; set; } = new int[HOUR];

        public int[] NGCount { get; set; } = new int[HOUR];

        public int[] FailCount { get; set; } = new int[HOUR];

        public int[] MarkNG { get; set; } = new int[HOUR];
                  
        public int[] CountNG { get; set; } = new int[HOUR];

        public int[] LengthNG { get; set; } = new int[HOUR];

        public int[] TotalTabNG { get; set; } = new int[HOUR];
    }
}
