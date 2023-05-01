using Cognex.VisionPro;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Util.Helper;
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

namespace KYJ_TEST
{
    public partial class Form1 : Form
    {
        DrawBoxControl DrawBoxControl;
        LiveViewPanel LiveViewPanel;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DrawBoxControl = new DrawBoxControl();
            DrawBoxControl.Dock = DockStyle.Fill;

            panel1.Controls.Add(DrawBoxControl);

            //LiveViewPanel = new LiveViewPanel();
            //LiveViewPanel.Dock = DockStyle.Fill;
            //panel1.Controls.Add(LiveViewPanel);
        }
    }
}
