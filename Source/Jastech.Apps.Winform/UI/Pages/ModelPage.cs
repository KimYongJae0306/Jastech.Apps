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
using Jastech.Framework.Config;

namespace Jastech.Apps.Winform.UI.Pages
{
    public partial class ModelPage : UserControl
    {
        #region 속성
        private ModelControl ModelControl { get; set; } = new ModelControl();

        #endregion

        public ModelPage()
        {
            InitializeComponent();
        }

        private void ModelPage_Load(object sender, EventArgs e)
        {
            AddControl();
        }

        private void AddControl()
        {
            ModelControl.Dock = DockStyle.Fill;
            pnlModelPage.Controls.Add(ModelControl);

            // TEST용
            ConfigSet tt = new ConfigSet();
            ModelControl.ModelPath = tt.Path.Model;
        }
    }
}
