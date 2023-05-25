using Jastech.Framework.Winform.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATT.UI.Controls
{
    public partial class TrendViewControl : UserControl
    {
        private List<TrendControl> TrendConrolList { get; set; } = new List<TrendControl>();
        public TrendViewControl()
        {
            InitializeComponent();
        }

        private void TrendControl_Load(object sender, EventArgs e)
        {
            AddControl();
        }

        private void AddControl()
        {
            TrendControl t1 = new TrendControl();
            this.tlpTrend.Controls.Add(t1, 0, 0);
        }
    }
}
