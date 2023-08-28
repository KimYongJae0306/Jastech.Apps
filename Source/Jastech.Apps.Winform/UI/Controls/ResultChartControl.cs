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
        private int _selectedTabNo { get; set; } = -1;

        private AlignResultType _selectedAlignResult { get; set; } = AlignResultType.All;
        #endregion

        public enum InspChartType
        {
            Akkon,
            Align,
        }

        #region 속성
        public InspChartType ChartType { get; set; } = InspChartType.Akkon;

        public Series AkkonSeriesCount { get; private set; } = null;

        public Series AkkonSeriesLength { get; private set; } = null;

        private List<Series> AkkonSeriesList { get; set; } = new List<Series>();

        public Series AlignSeriesLx { get; private set; } = null;

        public Series AlignSeriesLy { get; private set; } = null;

        public Series AlignSeriesRx { get; private set; } = null;

        public Series AlignSeriesRy { get; private set; } = null;

        public Series AlignSeriesCx { get; private set; } = null;

        private List<Series> AlignSeriesList { get; set; } = new List<Series>();
        #endregion

        #region 델리게이트
        private delegate void UpdateAlignChartDelegate(int tabNo);

        private delegate void UpdateAkkonChartDelegate(int tabNo);

        private delegate void ClearChartDelegate();
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

                
                //AlignSeriesLx.XValueMember = "ea";
                //AlignSeriesLx.YValueMembers = "um";

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
                AlignSeriesRy.Color = Color.Yellow;
                AlignSeriesRy.Name = "Ry";

                AlignSeriesCx = new Series();
                AlignSeriesCx = chtData.Series.Add("Cx");
                AlignSeriesCx.ChartType = SeriesChartType.Line;
                AlignSeriesCx.Color = Color.FromArgb(142, 89, 159);
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

        private void InitializeAlignChart()
        {
            //chtData.Titles[0].Position.Auto = false;

            //chtData.ChartAreas[0].Position.Auto = false;

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
                    AlignSeriesLx.Points.Add(tabData.LX);
                    AlignSeriesLy.Points.Add(tabData.LY);
                    AlignSeriesRx.Points.Add(tabData.RX);
                    AlignSeriesRy.Points.Add(tabData.RY);
                    AlignSeriesCx.Points.Add(tabData.CX);
                    break;

                case AlignResultType.Lx:
                    AlignSeriesLx.Points.Add(tabData.LX);
                    break;

                case AlignResultType.Ly:
                    AlignSeriesLy.Points.Add(tabData.LY);
                    break;

                case AlignResultType.Cx:
                    AlignSeriesCx.Points.Add(tabData.CX);
                    break;

                case AlignResultType.Rx:
                    AlignSeriesRx.Points.Add(tabData.RX);
                    break;

                case AlignResultType.Ry:
                    AlignSeriesRy.Points.Add(tabData.RY);
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

            UpdateAkkonChart(tabNo);
        }

        private void UpdateAkkonChart(int tabNo)
        {
            ClearChartData();

            var dailyInfo = DailyInfoService.GetDailyInfo();

            foreach (var item in dailyInfo.DailyDataList)
            {
                if (item.AkkonDailyInfoList.Count > 0)
                {
                    var tabData = item.AkkonDailyInfoList.Where(x => x.TabNo == tabNo).FirstOrDefault();

                    if(tabData != null)
                    {
                        AkkonSeriesCount.Points.Add(tabData.MinBlobCount);
                        AkkonSeriesLength.Points.Add(tabData.MinLength);
                    }
                }
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

        public void UpdateAlignChart(DataTable dataTable, TabType tabType, AlignResultType alignResultType)
        {
            if (dataTable == null)
                return;

            ClearChartData();

            var dt = ParseData(dataTable, (int)tabType);

            if (alignResultType == AlignResultType.All)
            {
                AlignSeriesLx.Points.DataBind(dt.AsEnumerable(), "Inspection Time", "Lx", "");
                AlignSeriesLy.Points.DataBind(dt.AsEnumerable(), "Inspection Time", "Ly", "");
                AlignSeriesCx.Points.DataBind(dt.AsEnumerable(), "Inspection Time", "Cx", "");
                AlignSeriesRx.Points.DataBind(dt.AsEnumerable(), "Inspection Time", "Rx", "");
                AlignSeriesRy.Points.DataBind(dt.AsEnumerable(), "Inspection Time", "Ry", "");
            }
            else
            {
                var alignSeries = AlignSeriesList.Where(x => x.Name == alignResultType.ToString()).First();
                alignSeries.Points.DataBind(dt.AsEnumerable(), "Inspection Time", alignResultType.ToString(), "");
            }
        }

        public void UpdateAkkonChart(DataTable dataTable, TabType tabType, AkkonResultType akkonResultType)
        {
            if (dataTable == null)
                return;

            ClearChartData();

            var dt = ParseData(dataTable, (int)tabType);

            if (akkonResultType == AkkonResultType.All)
            {
                AkkonSeriesCount.Points.DataBind(dt.AsEnumerable(), "Inspection Time", "Avg Count", "");
                AkkonSeriesLength.Points.DataBind(dt.AsEnumerable(), "Inspection Time", "Avg Length", "");
            }
            else
            {
                var akkonSeries = AkkonSeriesList.Where(x => x.Name == akkonResultType.ToString()).First();
                akkonSeries.Points.DataBind(dt.AsEnumerable(), "Inspection Time", "Avg " + akkonResultType.ToString(), "");
            }
        }

        private DataTable ParseData(DataTable dataTable, int tabNo)
        {
            DataTable newDataTable = new DataTable();

            newDataTable = dataTable.Clone();

            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            for (int rowIndex = 0; rowIndex < dataTable.Rows.Count / inspModel.TabCount; rowIndex++)
            {
                int index = (rowIndex * inspModel.TabCount) + tabNo;

                var rowData = dataTable.Rows[index];

                newDataTable.Rows.Add(rowData.ItemArray);
            }

            return newDataTable;
        }
        #endregion

        private void chtData_MouseDown(object sender, MouseEventArgs e)
        {
            if (ChartType == InspChartType.Akkon)
                return;

            HitTestResult result = chtData.HitTest(e.X, e.Y);

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
    }
}
