using Jastech.Apps.Winform;
using Jastech.Framework.Device.Motions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATT.UI.Forms
{
    public partial class DataChartingForm : Form
    {
        public class TempDataTest
        {
            public DateTime time = DateTime.MinValue;
            public double data = 0;
        }

        CancellationTokenSource Cancellation { get; set; } = new CancellationTokenSource();

        public DataChartingForm()
        {
            InitializeComponent();
        }

        private void DataChartingForm_Load(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                var motion = MotionManager.Instance().GetAxis(AxisHandlerName.Handler0, Jastech.Framework.Device.Motions.AxisName.Z0).Motion as ACSMotion;
                var points = chart1.Series.First().Points;
                while (Cancellation.IsCancellationRequested == false)
                {
                    BeginInvoke(new Action(() =>
                    {
                        points.AddY(motion.ReadRealVariable(nameof(ACSGlobalVariable.AF_DIFF)));

                        if (points.Count > 200)
                            points.RemoveAt(0);
                    }));
                    Thread.Sleep(20);
                }
            }, Cancellation.Token);
        }

        private void DataChartingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cancellation.Cancel();
        }
    }
}
