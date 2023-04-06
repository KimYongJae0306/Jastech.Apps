using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Framework.Winform.Controls;
using Jastech.Apps.Winform.Settings;
using ATT.UI.Forms;

namespace ATT.UI.Pages
{
    public partial class DataPage : UserControl
    {
        public DataPage()
        {
            InitializeComponent();
        }

        private void btnModelPage_Click(object sender, EventArgs e)
        {
            ModelPageForm form = new ModelPageForm();
            form.ShowDialog();
        }

        private void btnMotionData_Click(object sender, EventArgs e)
        {
            MotionSettingsForm form = new MotionSettingsForm();
            form.ShowDialog();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            OperationSettingsForm form = new OperationSettingsForm();
            form.ShowDialog();
        }
    }
}
