using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using Jastech.Framework.Util.Helper;

namespace Jastech.Framework.Winform.Controls
{
    public partial class UPHControl_old : UserControl
    {
        private string _filePath { get; set; } = string.Empty;

        private enum eGraphType
        {
            TOTAL_GRAPH,
            TOTAL_OK,
            TOTAL_NG,
            TOTAL_FAIL
        }

        private const int OneWeek = 7;
        private const int Hour = 24;
        private int[] _okCount = new int[Hour];
        private int[] _ngCount = new int[Hour];
        private int[] _failCount = new int[Hour];
        private int[] _ngMarkCount = new int[Hour];
        private int[] _ngCountCount = new int[Hour];
        private int[] _ngLenghtCount = new int[Hour];
        private Tuple<string[], List<string[]>> _readData = null;
        private string _isOpenFilePath = string.Empty;

        public UPHControl_old()
        {
            InitializeComponent();
        }

        private void UPHControl_Load(object sender, EventArgs e)
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            InitializeLabels();
            InitializeDataGridView();
        }

        public void SetFilePath(string filePath)
        {
            _filePath = filePath.Trim();
        }

        private void InitializeLabels()
        {
            lblTotalProduction.ForeColor = Color.Blue;
            lblTotalOK.ForeColor = Color.Green;
            lblTotalNG.ForeColor = Color.Red;
            lblTotalFail.ForeColor = Color.Black;
            lblDownload.ForeColor = Color.Black;
        }

        private void InitializeDataGridView()
        {
            dgvUPHData.Font = new Font("맑은 고딕", 9F, FontStyle.Bold);
            dgvUPHData.RowHeadersVisible = false;
            dgvUPHData.AllowUserToAddRows = false;

            dgvUPHData.ColumnCount = UPHData.ListHeader.Count;

            for (int index = 0; index < dgvUPHData.ColumnCount; index++)
            {
                dgvUPHData.Columns[index].HeaderText = UPHData.ListHeader[index].ToString();
                dgvUPHData.Columns[index].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            for (int index = 0; index < UPHData.ListRow.Count; index++)
                dgvUPHData.Rows.Add(UPHData.ListRow[index]);

            dgvUPHData.CurrentCell = null;
            dgvUPHData.ClearSelection();
        }

        private delegate void DayChangedDelegate(object sender);
        public void DayChanged(object sender)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    DayChangedDelegate callback = DayChanged;
                    BeginInvoke(callback, sender);
                    return;
                }

                SetDayChanged(sender);
            }
            catch (Exception ex)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + ex.Message);
            }
        }

        private void SetDayChanged(object sender)
        {
            MonthCalendar mcd = sender as MonthCalendar;
            string seletedDateTime = mcd.SelectionStart.ToString("yyyyMMdd");

            if (seletedDateTime == string.Empty)
                return;

            ClearLabel();
            ClearChart();
            ClearDataGridView();

            if (ParseDataList(LoadDataFromCSV(seletedDateTime)))
            {
                DrawCircleGraph();
                DrawBarGraph(eGraphType.TOTAL_GRAPH);
                UpdateDataGridView();
            }
        }

        private void ClearLabel()
        {
            lblTotalProduction.Text = "TOTAL\r\nPRODUCTION : 0";
            lblTotalOK.Text = "TOTAL\r\nOK : 0";
            lblTotalNG.Text = "TOTAL\r\nNG : 0";
            lblTotalFail.Text = "TOTAL\r\nFAIL : 0";
        }

        public void ClearChart()
        {
            chartPie.Series.Clear();
            chartBar.Series.Clear();
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
            _okCount = Enumerable.Repeat(0, Hour).ToArray<int>();
            _ngCount = Enumerable.Repeat(0, Hour).ToArray<int>();
            _failCount = Enumerable.Repeat(0, Hour).ToArray<int>();

            _ngMarkCount = Enumerable.Repeat(0, Hour).ToArray<int>();
            _ngCountCount = Enumerable.Repeat(0, Hour).ToArray<int>();
            _ngLenghtCount = Enumerable.Repeat(0, Hour).ToArray<int>();
        }

        private bool ParseDataList(Tuple<string[], List<string[]>> inputData)
        {
            try
            {
                if (inputData == null)
                {
                    Console.WriteLine("inputData is null.");
                    return false;
                }

                _readData = inputData;

                int tabCount = 4;
                if (inputData.Item2.Count % tabCount != 0)
                {
                    Console.WriteLine("입력 데이터 형식 불일치");
                    return false;
                }

                int hour = 0;
                bool isNG = false;

                List<string> outputData = new List<string>();

                for (int index = 0; index < inputData.Item2.Count; index += tabCount)
                {
                    isNG = false;

                    for (int innerIndex = 0; innerIndex < tabCount; innerIndex++)
                    {
                        int startIndex = inputData.Item2[index + innerIndex][0].IndexOf("[");
                        hour = Convert.ToInt16(inputData.Item2[index + innerIndex][0].Substring(startIndex + 1, 2));

                        Console.WriteLine("총합 : " + (index + innerIndex).ToString());
                        Console.WriteLine("해당 결과 : " + inputData.Item2[index + innerIndex][18]);

                        if (inputData.Item2[index + innerIndex][18].Contains("NG"))
                            isNG = true;
                    }

                    if (isNG)
                        _ngCount[hour]++;
                    else
                        _okCount[hour]++;
                }

                UpdateLabelData();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + ex.Message);
                return false;
            }
        }

        private void UpdateLabelData()
        {
            lblTotalProduction.Text = "TOTAL\r\nPRODUCTION : " + (_okCount.Sum() + _ngCount.Sum() + _failCount.Sum()).ToString();
            lblTotalOK.Text = "TOTAL\r\nOK : " + _okCount.Sum().ToString();
            lblTotalNG.Text = "TOTAL\r\nNG : " + _ngCount.Sum().ToString();
            lblTotalFail.Text = "TOTAL\r\nFAIL : " + _failCount.Sum().ToString();
        }

        private Tuple<string[], List<string[]>> LoadDataFromCSV(string dateTime, string filePath = "")
        {
            try
            {
                FileInfo fileInfo = new FileInfo(_filePath);
                if (fileInfo.Exists)
                {
                    return CSVHelper.ReadData(_filePath);
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + ex.Message);
                return null;
            }
        }

        private void DrawCircleGraph()
        {
            chartPie.Series.Clear();
            System.Windows.Forms.DataVisualization.Charting.Series series = chartPie.Series.Add("Count");
            series.IsValueShownAsLabel = true;
            chartPie.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;

            int total = _okCount.Sum() + _ngCount.Sum() + _failCount.Sum();
            double okRatio = _okCount.Sum() / (double)total;
            double ngRatio = _ngCount.Sum() / (double)total;
            double failRatio = _failCount.Sum() / (double)total;

            series.Points.AddXY("OK", okRatio.ToString("F2"));
            series.Points.AddXY("NG", ngRatio.ToString("F2"));
            series.Points.AddXY("FAIL", failRatio.ToString("F2"));

            series.Points[0].Label = okRatio.ToString("F2") + "%";
            series.Points[1].Label = ngRatio.ToString("F2") + "%";
            series.Points[2].Label = failRatio.ToString("F2") + "%";

            series.Points[0].LegendText = "OK : " + okRatio.ToString("F2") + "%";
            series.Points[1].LegendText = "NG : " + ngRatio.ToString("F2") + "%";
            series.Points[2].LegendText = "FAIL : " + failRatio.ToString("F2") + "%";
        }

        private void DrawBarGraph(eGraphType graphType)
        {
            chartBar.Series.Clear();
            chartBar.ChartAreas[0].AxisX.Interval = 1;
            chartBar.ChartAreas[0].AxisX.IntervalOffset = 0;
            chartBar.ChartAreas[0].AxisX.MajorGrid.Interval = 1;
            System.Windows.Forms.DataVisualization.Charting.Series series = chartBar.Series.Add("Count");
            series.IsValueShownAsLabel = true;
            chartBar.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            string time = string.Empty;

            switch (graphType)
            {
                case eGraphType.TOTAL_GRAPH:
                    for (int hour = 0; hour < Hour; hour++)
                    {
                        time = string.Format("{0:00}H ~ {1:00}H", hour, (hour + 1 != Hour) ? hour + 1 : 0);
                        series.Points.AddXY(time, _okCount[hour] + _ngCount[hour] + _failCount[hour]);
                    }
                    break;
                case eGraphType.TOTAL_OK:
                    for (int hour = 0; hour < Hour; hour++)
                    {
                        time = string.Format("{0:00}H ~ {1:00}H", hour, (hour + 1 != Hour) ? hour + 1 : 0);
                        series.Points.AddXY(time, _okCount[hour]);
                    }
                    break;
                case eGraphType.TOTAL_NG:
                    for (int hour = 0; hour < Hour; hour++)
                    {
                        time = string.Format("{0:00}H ~ {1:00}H", hour, (hour + 1 != Hour) ? hour + 1 : 0);
                        series.Points.AddXY(time, _ngCount[hour]);
                    }
                    break;
                case eGraphType.TOTAL_FAIL:
                    for (int hour = 0; hour < Hour; hour++)
                    {
                        time = string.Format("{0:00}H ~ {1:00}H", hour, (hour + 1 != Hour) ? hour + 1 : 0);
                        series.Points.AddXY(time, _failCount[hour]);
                    }
                    break;
                default:
                    break;
            }
        }

        private void UpdateDataGridView()
        {
            for (int hour = 0; hour < Hour; hour++)
            {
                dgvUPHData.Rows[hour].Cells[1].Value = _okCount[hour];
                dgvUPHData.Rows[hour].Cells[2].Value = _ngCount[hour];
                dgvUPHData.Rows[hour].Cells[3].Value = _failCount[hour];
                dgvUPHData.Rows[hour].Cells[4].Value = _okCount[hour] + _ngCount[hour] + _failCount[hour];
                dgvUPHData.Rows[hour].Cells[5].Value = _ngMarkCount[hour];
                dgvUPHData.Rows[hour].Cells[6].Value = _ngCountCount[hour];
                dgvUPHData.Rows[hour].Cells[7].Value = _ngLenghtCount[hour];
                dgvUPHData.Rows[hour].Cells[8].Value = _ngMarkCount[hour] + _ngCountCount[hour] + _ngLenghtCount[hour];
            }

            dgvUPHData.Rows[Hour].Cells[1].Value = _okCount.Sum();
            dgvUPHData.Rows[Hour].Cells[2].Value = _ngCount.Sum();
            dgvUPHData.Rows[Hour].Cells[3].Value = _failCount.Sum();
            dgvUPHData.Rows[Hour].Cells[4].Value = _okCount.Sum() + _ngCount.Sum() + _failCount.Sum();
            dgvUPHData.Rows[Hour].Cells[5].Value = _ngMarkCount.Sum();
            dgvUPHData.Rows[Hour].Cells[6].Value = _ngCountCount.Sum();
            dgvUPHData.Rows[Hour].Cells[7].Value = _ngLenghtCount.Sum();
            dgvUPHData.Rows[Hour].Cells[8].Value = _ngMarkCount.Sum() + _ngCountCount.Sum() + _ngLenghtCount.Sum();
        }

        internal class UPHData
        {
            public static List<string> ListHeader = new List<string> { "Time", "OK", "NG", "FAIL", "Total", "NG (Mark)", "NG (Count)", "NG (Length)", "Total Tab NG" };
            public static List<string> ListRow = new List<string>{"00:00 ~ 01:00", "01:00 ~ 02:00", "02:00 ~ 03:00", "03:00 ~ 04:00",
                                                    "04:00 ~ 05:00", "05:00 ~ 06:00", "06:00 ~ 07:00", "07:00 ~ 08:00",
                                                    "08:00 ~ 09:00", "09:00 ~ 10:00", "10:00 ~ 11:00", "11:00 ~ 12:00",
                                                    "12:00 ~ 13:00", "13:00 ~ 14:00", "14:00 ~ 15:00", "15:00 ~ 16:00",
                                                    "16:00 ~ 17:00", "17:00 ~ 18:00", "18:00 ~ 19:00", "19:00 ~ 20:00",
                                                    "20:00 ~ 21:00", "21:00 ~ 22:00", "22:00 ~ 23:00", "23:00 ~ 24:00", "ALL TIME"};

            //private int _ok = 0;
            //public int OK
            //{
            //    get { return _ok; }
            //    set { _ok = value; }
            //}

            //private int _ng = 0;
            //public int NG
            //{
            //    get { return _ng; }
            //    set { _ng = value; }
            //}

            //private int _fail = 0;
            //public int FAIL
            //{
            //    get { return _fail; }
            //    set { _fail = value; }
            //}

            //private int _total = 0;
            //public int Total
            //{
            //    get { return _total; }
            //    set { _total = value; }
            //}

            //private int _ng_Mark = 0;
            //public int NG_Mark
            //{
            //    get { return _ng_Mark; }
            //    set { _ng_Mark = value; }
            //}

            //private int _ng_Count = 0;
            //public int NG_Count
            //{
            //    get { return _ng_Count; }
            //    set { _ng_Count = value; }
            //}

            //private int _ng_Lenght = 0;
            //public int NG_Length
            //{
            //    get { return _ng_Lenght; }
            //    set { _ng_Lenght = value; }
            //}

            //private int _total_Tab_NG = 0;
            //public int Total_Tab_NG
            //{
            //    get { return _total_Tab_NG; }
            //    set { _total_Tab_NG = value; }
            //}

            //public string CellID = string.Empty;
            //public int StageNo;
            //public int TabNo;
            //public int CountMin;
            //public int CountAvg;
            //public int LengthMin;
            //public int LengthAvg;
            //public int StrengthMin;
            //public int StrengthAvg;
            //public double LeftAlignX;
            //public double LeftAlignY;
            //public double RightAlignX;
            //public double RightAlignY;
            //public double CenterAlignX;
            //public int ACFHead;
            //public int PreHead;
            //public int MainHead;
            //public string Judge = string.Empty;
            //public string Cause;
            //public string OpJudge;
        }
    }
}
