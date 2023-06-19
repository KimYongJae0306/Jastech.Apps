using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Service.Plc.Maps;
using Jastech.Apps.Winform.UI.Controls;
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
    public partial class PlcStatusForm : Form
    {
        private bool _isLoading { get; set; } = false;

        public PlcCommandControl PlcCommandControl { get; set; } = null;

        public PlcModelInfoControl PlcModelInfoControl { get; set; } = null;

        public Action CloseEventDelegate;

        public PlcStatusForm()
        {
            InitializeComponent();
        }
        
        private void PlcStatusForm_Load(object sender, EventArgs e)
        {
            _isLoading = true;
            AddControls();

            UpdateTimer.Start();

            _isLoading = false;
        }

        private void AddControls()
        {
            PlcCommandControl = new PlcCommandControl();
            PlcCommandControl.Dock = DockStyle.Fill;

            PlcModelInfoControl = new PlcModelInfoControl();
            PlcModelInfoControl.Dock = DockStyle.Fill;

            SelectDataPage();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            UpdateTimer.Stop();
            this.Close();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if(PlcCommandControl != null)
            {
                if (PlcCommandControl.Visible)
                    PlcCommandControl?.UpdateData();
            }

            if (PlcModelInfoControl != null)
            {
                if (PlcModelInfoControl.Visible)
                    PlcModelInfoControl?.UpdateData();
            }
        }

        private void btnDataPage_Click(object sender, EventArgs e)
        {
            SelectDataPage();
        }

        private void SelectDataPage()
        {
            PlcCommandControl.Visible = true;
            PlcModelInfoControl.Visible = false;

            pnlDisplay.Controls.Clear();
            pnlDisplay.Controls.Add(PlcCommandControl);
        }

        private void SelectModelPage()
        {
            PlcCommandControl.Visible = false;
            PlcModelInfoControl.Visible = true;

            pnlDisplay.Controls.Clear();
            pnlDisplay.Controls.Add(PlcModelInfoControl);
        }

        private void btnModelPage_Click(object sender, EventArgs e)
        {
            SelectModelPage();
        }
    }
}
