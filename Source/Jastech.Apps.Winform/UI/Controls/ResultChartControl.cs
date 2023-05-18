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
        public InspChartType ChartType { get; private set; } = InspChartType.Akkon;

        public Series AkkonSeriesCount { get; private set; } = null;

        public Series AkkonSeriesLength { get; private set; } = null;

        public Series AlignSeriesLx { get; private set; } = null;

        public Series AlignSeriesLy { get; private set; } = null;

        public Series AlignSeriesRx { get; private set; } = null;

        public Series AlignSeriesRy { get; private set; } = null;

        public Series AlignSeriesCx { get; private set; } = null;
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        private delegate void UpdateAlignChartDelegate(TabInspResult result);
        private delegate void UpdateAkkonChartDelegate(TabInspResult result);
        private delegate void ClearAlignChartDelegate();
        private delegate void ClearAkkonChartDelegate();
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

            //Test

            //if (ChartType == InspChartType.Akkon)
            //{
            //    for (int i = 0; i < 100; i++)
            //    {
            //        AkkonSeriesCount.Points.AddXY(i, i);
            //        AkkonSeriesLength.Points.AddXY(i, 100 - i);
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < 100; i++)
            //    {
            //        AlignSeriesLx.Points.AddXY(i, i);
            //        AlignSeriesLy.Points.AddXY(i, 100 - i);

            //        AlignSeriesRx.Points.AddXY(50, i);
            //        AlignSeriesRy.Points.AddXY(100, 100 - i);

            //        AlignSeriesCx.Points.AddXY(25, 100-i);

            //    }
            //}
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

                AlignSeriesLy = new Series();
                AlignSeriesLy = chtData.Series.Add("Ly");
                AlignSeriesLy.ChartType = SeriesChartType.Line;
                AlignSeriesLy.Color = Color.Orange;

                AlignSeriesRx = new Series();
                AlignSeriesRx = chtData.Series.Add("Rx");
                AlignSeriesRx.ChartType = SeriesChartType.Line;
                AlignSeriesRx.Color = Color.Green;

                AlignSeriesRy = new Series();
                AlignSeriesRy = chtData.Series.Add("Ry");
                AlignSeriesRy.ChartType = SeriesChartType.Line;
                AlignSeriesRy.Color = Color.Yellow;

                AlignSeriesCx = new Series();
                AlignSeriesCx = chtData.Series.Add("Cx");
                AlignSeriesCx.ChartType = SeriesChartType.Line;
                AlignSeriesCx.Color = Color.FromArgb(142, 89, 159);
            }
        }

        public void SetInspChartType(InspChartType chartType)
        {
            ChartType = chartType;
        }

        public void UpdateAlignChart(TabInspResult result)
        {
            if (this.InvokeRequired)
            {
                UpdateAlignChartDelegate callback = UpdateAlignChart;
                BeginInvoke(callback);
                return;
            }

            UpdateChart(result, InspChartType.Align);
        }

        public void UpdateAkkonChart(TabInspResult result)
        {
            if (this.InvokeRequired)
            {
                UpdateAlignChartDelegate callback = UpdateAkkonChart;
                BeginInvoke(callback);
                return;
            }

            UpdateChart(result, InspChartType.Akkon);
        }

        private void UpdateChart(TabInspResult tabInspResult, InspChartType inspType)
        {
            if (ChartType == InspChartType.Align)
            {
                AlignSeriesLx.Points.Add(tabInspResult.LeftAlignX.ResultValue);
                AlignSeriesLy.Points.Add(tabInspResult.LeftAlignX.ResultValue);
                AlignSeriesRx.Points.Add(tabInspResult.RightAlignX.ResultValue);
                AlignSeriesRy.Points.Add(tabInspResult.RightAlignY.ResultValue);
                AlignSeriesCx.Points.Add(tabInspResult.CenterX);
            }
            else
            {
                AkkonSeriesCount.Points.Add(tabInspResult.AkkonResult.AvgBlobCount);
                AkkonSeriesLength.Points.Add(tabInspResult.AkkonResult.AvgLength);
            }
        }

        public void ClearAlignChart()
        {
            if (this.InvokeRequired)
            {
                ClearAlignChartDelegate callback = ClearAlignChart;
                BeginInvoke(callback);
                return;
            }

            ClearChartData();
        }

        public void ClearAkkonChart()
        {
            if (this.InvokeRequired)
            {
                ClearAkkonChartDelegate callback = ClearAkkonChart;
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
        #endregion
    }
}
