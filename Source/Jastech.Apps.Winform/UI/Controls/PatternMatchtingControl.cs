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

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class PatternMatchtingControl : UserControl
    {
        private CogPatternMatchingParamControl CogPatternMatchingParamControl { get; set; } = new CogPatternMatchingParamControl();

        public PatternMatchtingControl()
        {
            InitializeComponent();
        }

        private void PatternMatchtingControl_Load(object sender, EventArgs e)
        {
            CogPatternMatchingParamControl.Dock = DockStyle.Fill;
            pnlParam.Controls.Add(CogPatternMatchingParamControl);
        }
    }
}
