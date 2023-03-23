using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Framework.Winform.VisionPro.Controls;

namespace Jastech.Apps.Winform.UI.Pages
{
    public partial class TeachingPage : UserControl
    {
        private CogDisplayControl TeachDisplay = null;

        public TeachingPage()
        {
            InitializeComponent();
        }

        private void TeachingPage_Load(object sender, EventArgs e)
        {
            AddControl();
        }

        private void AddControl()
        {
            TeachDisplay = new CogDisplayControl();
            TeachDisplay.Dock = DockStyle.Fill;
            pnlDisplay.Controls.Add(TeachDisplay);
        }
    }
}
