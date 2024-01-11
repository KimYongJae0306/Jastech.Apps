using Emgu.CV.ML.MlEnum;
using Jastech.Apps.Structure;
using Jastech.Apps.Winform.Service;
using Jastech.Framework.Winform.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class ResultChartControl : UserControl
    {
        #region 필드
        private int _selectedTabNo { get; set; } = 0;

        private AlignResultType _selectedAlignResult { get; set; } = AlignResultType.All;

        private AkkonResultType _selectedAkkonResult { get; set; } = AkkonResultType.All;
        #endregion

        public enum InspChartType
        {
            Akkon,
            Align,
        }

        #region 속성
        public InspChartType ChartType { get; set; } = InspChartType.Akkon;

        public bool IsDailyInfo { get; set; } = true;

        public Series AkkonSeriesCount { get; private set; } = null;

        public Series AkkonSeriesLength { get; private set; } = null;

        private List<Series> AkkonSeriesList { get; set; } = new List<Series>();

        public Series AlignSeriesLx { get; private set; } = null;

        public Series AlignSeriesLy { get; private set; } = null;

        public Series AlignSeriesRx { get; private set; } = null;

        public Series AlignSeriesRy { get; private set; } = null;

        public Series AlignSeriesCx { get; private set; } = null;

        private List<Series> AlignSeriesList { get; set; } = new List<Series>();

        public bool EnableLegend { get; set; } = true;
        #endregion

        #region 델리게이트
        private delegate void UpdateAlignChartDelegate(int tabNo);

        private delegate void UpdateAkkonChartDelegate(int tabNo);

        private delegate void ClearChartDelegate();

        private delegate void ReUpdateChartDelegate(InspChartType inspChartType);
        #endregion

        #region 생성자
        public ResultChartControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void ResultChartControl_Load(object sender, EventArgs e)
        {
            Initialize();
        }

        private void Initialize()
        {
            if (EnableLegend == false)
                chtData.Legends.Clear();

            if (ChartType == InspChartType.Akkon)
            {
                AkkonSeriesCount = new Series();
                AkkonSeriesCount = chtData.Series.Add("Avg Count");
                AkkonSeriesCount.ChartType = SeriesChartType.Line;
                AkkonSeriesCount.Color = Color.Blue;
                AkkonSeriesCount.Name = "Count";

                AkkonSeriesLength = new Series();
                AkkonSeriesLength = chtData.Series.Add("Avg Length");
                AkkonSeriesLength.ChartType = SeriesChartType.Line;
                AkkonSeriesLength.Color = Color.FromArgb(142, 89, 159);
                AkkonSeriesLength.Name = "Length";

                AkkonSeriesList.Add(AkkonSeriesCount);
                AkkonSeriesList.Add(AkkonSeriesLength);
            }
            else
            {
                AlignSeriesLx = new Series();
                AlignSeriesLx = chtData.Series.Add("Lx");
                AlignSeriesLx.ChartType = SeriesChartType.Line;
                AlignSeriesLx.Color = Color.Blue;
                AlignSeriesLx.Name = "Lx";

                AlignSeriesLy = new Series();
                AlignSeriesLy = chtData.Series.Add("Ly");
                AlignSeriesLy.ChartType = SeriesChartType.Line;
                AlignSeriesLy.Color = Color.Orange;
                AlignSeriesLy.Name = "Ly";

                AlignSeriesRx = new Series();
                AlignSeriesRx = chtData.Series.Add("Rx");
                AlignSeriesRx.ChartType = SeriesChartType.Line;
                AlignSeriesRx.Color = Color.Green;
                AlignSeriesRx.Name = "Rx";

                AlignSeriesRy = new Series();
                AlignSeriesRy = chtData.Series.Add("Ry");
                AlignSeriesRy.ChartType = SeriesChartType.Line;
                AlignSeriesRy.Color = Color.FromArgb(142, 89, 159);
                AlignSeriesRy.Name = "Ry";

                AlignSeriesCx = new Series();
                AlignSeriesCx = chtData.Series.Add("Cx");
                AlignSeriesCx.ChartType = SeriesChartType.Line;
                AlignSeriesCx.Color = Color.Yellow;
                AlignSeriesCx.Name = "Cx";

                AlignSeriesList.Add(AlignSeriesLx);
                AlignSeriesList.Add(AlignSeriesLy);
                AlignSeriesList.Add(AlignSeriesCx);
                AlignSeriesList.Add(AlignSeriesRx);
                AlignSeriesList.Add(AlignSeriesRy);
            }
        }

        public void SetInspChartType(InspChartType chartType)
        {
            ChartType = chartType;
        }

        public void UpdateAlignDaily(int tabNo)
        {
            if (this.InvokeRequired)
            {
                UpdateAlignChartDelegate callback = UpdateAlignDaily;
                BeginInvoke(callback, tabNo);
                return;
            }

            _selectedTabNo = tabNo;

            UpdateAlignChart(tabNo, _selectedAlignResult);
        }

        public void ReUpdate(InspChartType inspChartType)
        {
            if (this.InvokeRequired)
            {
                ReUpdateChartDelegate callback = ReUpdate;
                BeginInvoke(callback, inspChartType);
                return;
            }

            if (inspChartType == InspChartType.Align)
                UpdateAlignChart(_selectedTabNo, _selectedAlignResult);
            else if (inspChartType == InspChartType.Akkon)
                UpdateAkkonChart(_selectedTabNo, _selectedAkkonResult);
        }

        private void InitializeAlignChart()
        {
            chtData.ChartAreas[0].AxisX.Interval = 10;
            chtData.ChartAreas[0].AxisX.Title = "ea";
            chtData.ChartAreas[0].AxisX.TextOrientation = TextOrientation.Auto;
            chtData.ChartAreas[0].AxisX.TitleForeColor = Color.White;
            chtData.ChartAreas[0].AxisX.TitleAlignment = StringAlignment.Far;
            chtData.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chtData.ChartAreas[0].AxisY.Title = "um";
            chtData.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Auto;
            chtData.ChartAreas[0].AxisY.TitleForeColor = Color.White;
            chtData.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Far;
            chtData.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
        }

        private void InitializeAkkonChart(AkkonResultType akkonResultType)
        {
            chtData.ChartAreas[0].AxisX.Interval = 10;
            chtData.ChartAreas[0].AxisX.Title = "ea";
            chtData.ChartAreas[0].AxisX.TextOrientation = TextOrientation.Auto;
            chtData.ChartAreas[0].AxisX.TitleForeColor = Color.White;
            chtData.ChartAreas[0].AxisX.TitleAlignment = StringAlignment.Far;
            chtData.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            if (akkonResultType == AkkonResultType.Length)
                chtData.ChartAreas[0].AxisY.Title = "um";
            else if (akkonResultType == AkkonResultType.Count)
                chtData.ChartAreas[0].AxisY.Title = "ea";
            else
                chtData.ChartAreas[0].AxisY.Title = "";

            chtData.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Auto;
            chtData.ChartAreas[0].AxisY.TitleForeColor = Color.White;
            chtData.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Far;
            chtData.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
        }

        private void UpdateAlignChart(int tabNo, AlignResultType alignResultType = AlignResultType.All)
        {
            ClearChartData();
            InitializeAlignChart();
            //chtData.AlignDataPointsByAxisLabel();

            var dailyInfo = DailyInfoService.GetDailyInfo();

            foreach (var item in dailyInfo.DailyDataList)
            {
                if (item.AlignDailyInfoList.Count > 0)
                {
                    var tabData = item.AlignDailyInfoList.Where(x => x.TabNo == tabNo).FirstOrDefault();

                    if (tabData == null)
                        continue;

                    AddSeriesPoint(tabData, alignResultType);
                }
            }
        }

        private void AddSeriesPoint(AlignDailyInfo tabData, AlignResultType alignResultType)
        {
            switch (alignResultType)
            {
                case AlignResultType.All:
                    if (double.TryParse(tabData.LX, out double lx1))
                        AlignSeriesLx.Points.Add(lx1);
                    else
                        AlignSeriesLx.Points.Add(0);

                    if (double.TryParse(tabData.LY, out double ly1))
                        AlignSeriesLy.Points.Add(ly1);
                    else
                        AlignSeriesLy.Points.Add(0);

                    if (double.TryParse(tabData.RX, out double rx1))
                        AlignSeriesCx.Points.Add(rx1);
                    else
                        AlignSeriesCx.Points.Add(0);

                    if (double.TryParse(tabData.RY, out double ry1))
                        AlignSeriesRy.Points.Add(ry1);
                    else
                        AlignSeriesRy.Points.Add(0);

                    if (double.TryParse(tabData.CX, out double cx1))
                        AlignSeriesRx.Points.Add(cx1);
                    else
                        AlignSeriesRx.Points.Add(0);
                    break;

                case AlignResultType.Lx:
                    if (double.TryParse(tabData.LX, out double lx2))
                        AlignSeriesLx.Points.Add(lx2);
                    else
                        AlignSeriesLx.Points.Add(0);
                    break;

                case AlignResultType.Ly:
                    if (double.TryParse(tabData.LY, out double ly2))
                        AlignSeriesLy.Points.Add(ly2);
                    else
                        AlignSeriesLy.Points.Add(0);
                    break;

                case AlignResultType.Cx:
                    if (double.TryParse(tabData.CX, out double cx2))
                        AlignSeriesCx.Points.Add(cx2);
                    else
                        AlignSeriesCx.Points.Add(0);
                    break;

                case AlignResultType.Rx:
                    if (double.TryParse(tabData.RX, out double rx2))
                        AlignSeriesRx.Points.Add(rx2);
                    else
                        AlignSeriesRx.Points.Add(0);
                    break;

                case AlignResultType.Ry:
                    if (double.TryParse(tabData.RY, out double ry2))
                        AlignSeriesRy.Points.Add(ry2);
                    else
                        AlignSeriesRy.Points.Add(0);
                    break;

                default:
                    break;
            }
        }

        public void UpdateAkkonDaily(int tabNo)
        {
            if (this.InvokeRequired)
            {
                UpdateAkkonChartDelegate callback = UpdateAkkonDaily;
                BeginInvoke(callback, tabNo);
                return;
            }

            _selectedTabNo = tabNo;

            UpdateAkkonChart(tabNo, _selectedAkkonResult);
        }

        private void UpdateAkkonChart(int tabNo, AkkonResultType akkonResultType = AkkonResultType.All)
        {
            ClearChartData();
            InitializeAkkonChart(akkonResultType);

            var dailyInfo = DailyInfoService.GetDailyInfo();

            foreach (var item in dailyInfo.DailyDataList)
            {
                if (item.AkkonDailyInfoList.Count > 0)
                {
                    var tabData = item.AkkonDailyInfoList.Where(x => x.TabNo == tabNo).FirstOrDefault();

                    if (tabData == null)
                        continue;

                    AddSeriesPoint(tabData, akkonResultType);
                }
            }
        }

        private void AddSeriesPoint(AkkonDailyInfo tabData, AkkonResultType akkonResultType)
        {
            switch (akkonResultType)
            {
                case AkkonResultType.All:
                    if (double.TryParse(tabData.MinBlobCount.ToString(), out double minBlobCount1))
                        AkkonSeriesCount.Points.Add(minBlobCount1);
                    else
                        AkkonSeriesCount.Points.Add(0);

                    if (double.TryParse(tabData.MinLength.ToString(), out double minLength1))
                        AkkonSeriesLength.Points.Add(minLength1);
                    else
                        AkkonSeriesLength.Points.Add(0);

                    break;

                case AkkonResultType.Count:
                    if (double.TryParse(tabData.MinBlobCount.ToString(), out double minBlobCount2))
                        AkkonSeriesCount.Points.Add(minBlobCount2);
                    else
                        AkkonSeriesCount.Points.Add(0);
                    break;

                case AkkonResultType.Length:
                    if (double.TryParse(tabData.MinLength.ToString(), out double minLength2))
                        AkkonSeriesLength.Points.Add(minLength2);
                    else
                        AkkonSeriesLength.Points.Add(0);
                    break;

                case AkkonResultType.Strength:
                    break;

                case AkkonResultType.STD:
                    break;

                default:
                    break;
            }
        }

        public void ClearChart()
        {
            if (this.InvokeRequired)
            {
                ClearChartDelegate callback = ClearChart;
                BeginInvoke(callback);
                return;
            }

            ClearChartData();
        }

        private void ClearChartData()
        {
            if (ChartType == InspChartType.Align)
            {
                foreach (var alignSeries in AlignSeriesList)
                    alignSeries.Points.Clear();
            }
            else
            {
                foreach (var akkonSeries in AkkonSeriesList)
                    akkonSeries.Points.Clear();
            }
        }

        public void UpdateAlignChart(List<TrendResult> alignTrendResults, TabType tabType, AlignResultType alignResultType)
        {
            if (alignTrendResults == null || alignTrendResults.Count <= 0)
                return;

            _selectedTabNo = (int)tabType;
            ClearChartData();

            if (alignResultType == AlignResultType.All)
            {
                foreach (TrendResult trendResult in alignTrendResults)
                {
                    AlignSeriesLx.Points.Add(trendResult.GetAlignDatas(tabType, AlignResultType.Lx));
                    AlignSeriesLy.Points.Add(trendResult.GetAlignDatas(tabType, AlignResultType.Ly));
                    AlignSeriesCx.Points.Add(trendResult.GetAlignDatas(tabType, AlignResultType.Cx));
                    AlignSeriesRx.Points.Add(trendResult.GetAlignDatas(tabType, AlignResultType.Rx));
                    AlignSeriesRy.Points.Add(trendResult.GetAlignDatas(tabType, AlignResultType.Ry));
                }
            }
            else
            {
                var alignSeries = AlignSeriesList.First(x => x.Name == alignResultType.ToString());
                foreach (TrendResult trendResult in alignTrendResults)
                    alignSeries?.Points.Add(trendResult.GetAlignDatas(tabType, alignResultType));
            }
        }

        public void UpdateAlignChart(List<TrendResult> alignTrendResultList, AlignResultType alignResultType, int tabNo, int dataCount)
        {
            if (alignTrendResultList == null || alignTrendResultList.Count <= 0)
                return;

            ClearChartData();

            var alignSeries = AlignSeriesList.First(x => x.Name == alignResultType.ToString());

            List<TrendResult> reverseList = Enumerable.Reverse(alignTrendResultList).ToList();
            List<TrendResult> viewDataList = new List<TrendResult>();

            for (int index = 0; index < dataCount; index++)
                viewDataList.Add(reverseList[index]);

            foreach (TrendResult trendResult in Enumerable.Reverse(viewDataList).ToList())
                alignSeries?.Points.Add(trendResult.GetAlignDatas((TabType)Enum.Parse(typeof(TabType), tabNo.ToString()), alignResultType));
        }

        public void UpdateAkkonChart(List<TrendResult> akkonTrendResults, TabType tabType, AkkonResultType akkonResultType)
        {
            if (akkonTrendResults == null)
                return;

            _selectedTabNo = (int)tabType;
            ClearChartData();

            if (akkonResultType == AkkonResultType.All)
            {
                foreach (TrendResult trendResult in akkonTrendResults)
                {
                    AkkonSeriesCount.Points.Add(trendResult.GetAkkonDatas(tabType, AkkonResultType.Count));
                    AkkonSeriesLength.Points.Add(trendResult.GetAkkonDatas(tabType, AkkonResultType.Length));
                }
            }
            else
            {
                var akkonSeries = AkkonSeriesList.First(x => x.Name == akkonResultType.ToString());
                foreach (TrendResult trendResult in akkonTrendResults)
                    akkonSeries?.Points.Add(trendResult.GetAkkonDatas(tabType, akkonResultType));
            }
        }

        private DataTable ParseData(DataTable dataTable, int tabNo)
        {
            DataTable newDataTable = new DataTable();

            newDataTable = dataTable.Clone();

            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
            {
                int index = (rowIndex * inspModel.TabCount) + tabNo;

                var rowData = dataTable.Rows[rowIndex];

                newDataTable.Rows.Add(rowData.ItemArray);
            }

            return newDataTable;
        }

        private void chtData_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsDailyInfo == false)
                return;

            switch (ChartType)
            {
                case InspChartType.Akkon:
                    ShowSelectedAkkonLegend(e.X, e.Y);
                    break;

                case InspChartType.Align:
                    ShowSelectedAlignLegend(e.X, e.Y);
                    break;

                default:
                    break;
            }
        }

        private void ShowSelectedAlignLegend(int x, int y)
        {
            HitTestResult result = chtData.HitTest(x, y);

            if (result != null && result.Object != null)
            {
                if (result.Object is LegendItem)
                {
                    LegendItem legendItem = (LegendItem)result.Object;

                    _selectedAlignResult = (AlignResultType)Enum.Parse(typeof(AlignResultType), legendItem.SeriesName);
                    UpdateAlignChart(_selectedTabNo, _selectedAlignResult);
                }
            }
            else
            {
                _selectedAlignResult = AlignResultType.All;
                UpdateAlignChart(_selectedTabNo, _selectedAlignResult);
            }
        }

        private void ShowSelectedAkkonLegend(int x, int y)
        {
            HitTestResult result = chtData.HitTest(x, y);

            if (result != null && result.Object != null)
            {
                if (result.Object is LegendItem)
                {
                    LegendItem legendItem = (LegendItem)result.Object;
                    _selectedAkkonResult = (AkkonResultType)Enum.Parse(typeof(AkkonResultType), legendItem.SeriesName);
                    UpdateAkkonChart(_selectedTabNo, _selectedAkkonResult);
                }
            }
            else
            {
                _selectedAkkonResult = AkkonResultType.All;
                UpdateAkkonChart(_selectedTabNo, _selectedAkkonResult);
            }
        }

        private void chtData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            HitTestResult result = chtData.HitTest(e.X, e.Y);

            if (result != null && result.Object != null)
            {
                if (result.PointIndex >= 0)
                {
                    double selectedValue = chtData.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);
                    //chtData.Series[result.PointIndex].ToolTip = selectedValue.ToString("F4");
                    //chtData.Text = selectedValue.ToString("F4");

                    //var tlbvkf = chtData.ChartAreas[0].AxisY.PositionToValue(e.Y);

                    var t1 = chtData.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);
                    //var t2 = chtData.ChartAreas[0].AxisY.PixelPositionToValue(e.X);
                    //var t3 = chtData.ChartAreas[0].AxisY.PositionToValue(e.Y);
                    //var t4 = chtData.ChartAreas[0].AxisY.PositionToValue(e.X);
                    //var t5 = chtData.ChartAreas[0].AxisY.
                    int gg = 0;

                    var alignSeries = AlignSeriesList.First(x => x.Name == AlignResultType.Lx.ToString());

                    var tlqkf = alignSeries.Points[result.PointIndex].YValues;
                    //var tlvkf = chtData.ChartAreas[0].AxisY.
                }
            }
        }
        #endregion
    }
}
