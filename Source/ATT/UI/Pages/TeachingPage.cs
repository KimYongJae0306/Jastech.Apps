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
using Jastech.Apps.Structure;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Apps.Structure.Data;

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
            form.UnitName = UnitName.Unit0;
            form.TitleCameraName = "LineScan";
            form.CameraName = CameraName.LinscanMIL0;
            form.ShowDialog();
            GC.Collect();
        }

        private void btnLinescanSetting_Click(object sender, EventArgs e)
        {
            OpticTeachingForm form = new OpticTeachingForm();
            form.CameraName = CameraName.LinscanMIL0;
            form.ShowDialog();
        }
    }
}
