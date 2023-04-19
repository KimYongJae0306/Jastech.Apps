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
using Jastech.Apps.Structure.Parameters;
//using Jastech.Framework.Winform.Controls;
using Jastech.Framework.Device.Motions;
using Jastech.Apps.Winform;

namespace ATT.UI.Controls
{
    public partial class LinescanControl : UserControl
    {
        private Color _selectedColor = new Color();
        private Color _nonSelectedColor = new Color();

        private AutoFocusControl AutoFocusControl { get; set; } = new AutoFocusControl() { Dock = DockStyle.Fill };
        private MotionRepeatControl MotionRepeatControl { get; set; } = new MotionRepeatControl() { Dock = DockStyle.Fill };

        public LinescanControl()
        {
            InitializeComponent();
        }

        private void LinescanControl_Load(object sender, EventArgs e)
        {
            AddControl();
            InitializeUI();
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);
        }

        private void AddControl()
        {
            pnlAutoFocus.Controls.Add(AutoFocusControl);

            AxisHandler axisHandler = AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Unit0);
            MotionRepeatControl.SetAxisHanlder(axisHandler);
            pnlMotionRepeat.Controls.Add(MotionRepeatControl);
        }
    }
}
