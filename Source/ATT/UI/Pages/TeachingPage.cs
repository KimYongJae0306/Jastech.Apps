using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATT.UI.Forms;

namespace ATT.UI.Pages
{
    public partial class TeachingPage : UserControl
    {
        public TeachingPage()
        {
            InitializeComponent();
        }

        private void btnModelPage_Click(object sender, EventArgs e)
        {
            LineTeachingForm form = new LineTeachingForm();
            form.UnitName = "0";
            form.ShowDialog();
        }
    }
}
