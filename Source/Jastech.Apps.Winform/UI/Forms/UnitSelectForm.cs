using Jastech.Framework.Device.Cameras;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Forms
{
    public partial class UnitSelectForm : Form
    {
        public UnitSelectForm()
        {
            InitializeComponent();
        }

        public string UnitName { get; set; } = "";

        public SensorType SensorType { get; set; } = SensorType.Area;
        private void lblOK_Click(object sender, EventArgs e)
        {
            UnitName = "0";
            SensorType = SensorType.Area;

            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
