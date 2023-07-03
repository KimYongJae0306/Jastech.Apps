using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.UI.Controls;
using ATT_UT_IPAD.UI.Controls;

namespace ATT_UT_IPAD.UI.Pages
{
    public partial class MainPage : UserControl
    {
        //public AkkonViewerControl AkkonResultViewer { get; set; } = new AkkonViewerControl() { Dock = DockStyle.Fill };

        //public AlignViewerControl AlignResultViewer { get; set; } = new AlignViewerControl() { Dock = DockStyle.Fill };

        public SystemLogControl SystemLogControl { get; set; } = new SystemLogControl() { Dock = DockStyle.Fill };

        public MainPage()
        {
            InitializeComponent();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            AddControls();
        }

        private void AddControls()
        {
            //pnlAkkon.Controls.Add(AkkonResultViewer);
            //pnlAlign.Controls.Add(AlignResultViewer);
        }

        public void UpdateTabCount(int tabCount)
        {
            //AkkonResultViewer.UpdateTabCount(tabCount);
            //AlignResultViewer.UpdateTabCount(tabCount);
        }

        public void UpdateMainResult(AppsInspResult result)
        {
            //AkkonResultViewer.UpdateMainResult(result);
            //AlignResultViewer.UpdateMainResult(result);
        }

        public void AddSystemLogMessage(string logMessage)
        {
            SystemLogControl.AddLogMessage(logMessage);
        }
    }

    public enum PageType
    {
        Result,
        Log,
    }
}
