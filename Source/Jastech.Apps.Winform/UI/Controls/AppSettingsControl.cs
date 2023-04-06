using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Apps.Winform.Settings;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AppSettingsControl : UserControl
    {
        public AppSettingsControl()
        {
            InitializeComponent();
        }

        private void AppSettingsControl_Load(object sender, EventArgs e)
        {

        }

        private void Load()
        {
            var operation = AppConfig.Instance().Operation;

            txtDistanceX.Text = operation.DistanceFromPreAlignToLineScanX.ToString();
            txtDistanceY.Text = operation.DistanceFromPreAlignToLineScanY.ToString();

            txtPreAlignToleranceX.Text = operation.PreAlignToleranceX.ToString();
            txtPreAlignToleranceY.Text = operation.PreAlignToleranceY.ToString();
            txtPreAlignToleranceTheta.Text = operation.PreAlignToleranceTheta.ToString();

            //mtgEnablePreAlign.te
        }
    }
}
