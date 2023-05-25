using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Service;
using Jastech.Apps.Structure;
using Jastech.Framework.Winform.Forms;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class ResultChartControl : UserControl
    {
        public enum InspChartType
        {
            Akkon,
            Align,
        }

        #region 필드
        #endregion

        #region 속성
        public InspChartType ChartType { get; set; } = InspChartType.Akkon;

        public Series AkkonSeriesCount { get; private set; } = null;

        public Series AkkonSeriesLength { get; private set; } = null;

        public Series AlignSeriesLx { get; private set; } = null;

        public Series AlignSeriesLy { get; private set; } = null;

        public Series AlignSeriesRx { get; private set; } = null;

        public Series AlignSeriesRy { get; private set; } = null;

        public Series AlignSeriesCx { get; private set; } = null;

        private List<Series> AlignSeriesList { get; set; } = new List<Series>();
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        private delegate void UpdateAlignChartDelegate(AlignDailyInfo align);
        private delegate void UpdateAkkonChartDelegate(AkkonDailyInfo akkon);
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
            if(ChartType == InspChartType.Akkon)
            {
                AkkonSeriesCount = new Series();
                AkkonSeriesCount = chtData.Series.Add("Count");
                AkkonSeriesCount.ChartType = SeriesChartType.Line;
                AkkonSeriesCount.Color = Color.Blue;
                    
                AkkonSeriesLength = new Series();
                AkkonSeriesLength = chtData.Series.Add("Length");
                AkkonSeriesLength.ChartType = SeriesChartType.Line;
                //AkkonSeriesLength.Color = Color.Purple;
                AkkonSeriesLength.Color = Color.FromArgb(142, 89, 159);
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

        public void UpdateAlignDaily(AlignDailyInfo align)
        {
            if (this.InvokeRequired)
            {
                UpdateAlignChartDelegate callback = UpdateAlignDaily;
                BeginInvoke(callback);
                return;
            }

            UpdateAlignChart(align);
        }

        private void UpdateAlignChart(AlignDailyInfo align)
        {
            ClearChartData();

            AlignSeriesLx.Points.Add(align.LX);
            AlignSeriesLy.Points.Add(align.LY);
            AlignSeriesRx.Points.Add(align.RX);
            AlignSeriesRy.Points.Add(align.RY);
            AlignSeriesCx.Points.Add(align.CX);
        }

        public void UpdateAkkonDaily(AkkonDailyInfo akkon)
        {
            if (this.InvokeRequired)
            {
                UpdateAkkonChartDelegate callback = UpdateAkkonDaily;
                BeginInvoke(callback);
                return;
            }

            UpdateAkkonChart(akkon);
        }

        private void UpdateAkkonChart(AkkonDailyInfo akkon)
        {
            ClearChartData();

            AkkonSeriesCount.Points.Add(akkon.AvgBlobCount);
            AkkonSeriesLength.Points.Add(akkon.AvgLength);
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
                AlignSeriesLx.Points.Clear();
                AlignSeriesLy.Points.Clear();
                AlignSeriesRx.Points.Clear();
                AlignSeriesRy.Points.Clear();
                AlignSeriesCx.Points.Clear();
            }
            else
            {
                AkkonSeriesCount.Points.Clear();
                AkkonSeriesLength.Points.Clear();
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
                AlignSeriesLx.Points.DataBind(dt.AsEnumerable(), "Time", "Lx", "");
                AlignSeriesLy.Points.DataBind(dt.AsEnumerable(), "Time", "Ly", "");
                AlignSeriesCx.Points.DataBind(dt.AsEnumerable(), "Time", "Cx", "");
                AlignSeriesRx.Points.DataBind(dt.AsEnumerable(), "Time", "Rx", "");
                AlignSeriesRy.Points.DataBind(dt.AsEnumerable(), "Time", "Ry", "");
            }
            else
            {
                var alignSeries = AlignSeriesList.Where(x => x.Name == alignResultType.ToString()).First();
                alignSeries.Points.DataBind(dt.AsEnumerable(), "Time", alignResultType.ToString(), "");
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
                AlignSeriesLx.Points.DataBind(dt.AsEnumerable(), "Time", "Lx", "");
                AlignSeriesLy.Points.DataBind(dt.AsEnumerable(), "Time", "Ly", "");
                AlignSeriesCx.Points.DataBind(dt.AsEnumerable(), "Time", "Cx", "");
                AlignSeriesRx.Points.DataBind(dt.AsEnumerable(), "Time", "Rx", "");
                AlignSeriesRy.Points.DataBind(dt.AsEnumerable(), "Time", "Ry", "");
            }
            else
            {
                var alignSeries = AlignSeriesList.Where(x => x.Name == akkonResultType.ToString()).First();
                alignSeries.Points.DataBind(dt.AsEnumerable(), "Time", akkonResultType.ToString(), "");
            }
        }

        private DataTable ParseData(DataTable dataTable, int tabNo)
        {
            DataTable newDataTable = new DataTable();

            newDataTable = dataTable.Clone();
            int gg = 0;

            //foreach (var item in dt.Rows)
            //{
            //    int tlqkf = 0;
            //}
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            for (int rowIndex = 0; rowIndex < dataTable.Rows.Count / inspModel.TabCount; rowIndex++)
            {
                int index = (rowIndex * inspModel.TabCount) + tabNo;

                var rowData = dataTable.Rows[index];

                newDataTable.Rows.Add(rowData.ItemArray);
            }
            int tt = 0;

            return newDataTable;
        }
        #endregion
    }
}
