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
using static OpenCvSharp.Stitcher;
using Jastech.Framework.Structure;
using static Jastech.Apps.Winform.UI.Controls.LinescanControl;
using System.Windows.Forms.DataVisualization.Charting;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class LinescanControl : UserControl
    {
        private Color _selectedColor = new Color();
        private Color _nonSelectedColor = new Color();

        private AutoFocusControl AutoFocusControl { get; set; } = new AutoFocusControl() { Dock = DockStyle.Fill };
        private MotionRepeatControl MotionRepeatControl { get; set; } = new MotionRepeatControl() { Dock = DockStyle.Fill };

        private GrabMode _grabMode = GrabMode.AreaMode;
        public enum GrabMode
        {
            AreaMode,
            LineMode,
        }

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

        private void rdoGrabType_CheckedChanged(object sender, EventArgs e)
        {
            SetSelecteGrabType(sender);
        }

        private void SetSelecteGrabType(object sender)
        {
            RadioButton btn = sender as RadioButton;

            if (btn.Checked)
            {
                if (btn.Text.ToLower().Contains("area"))
                    ShowUpdateUI(GrabMode.AreaMode);
                else
                    ShowUpdateUI(GrabMode.LineMode);

                btn.BackColor = _selectedColor;
            }
            else
                btn.BackColor = _nonSelectedColor;

            UpdateData();
        }

        private void ShowUpdateUI(GrabMode grabMode)
        {
            switch (grabMode)
            {
                case GrabMode.AreaMode:
                    ShowAreaMode();
                    break;

                case GrabMode.LineMode:
                    ShowLineMode();
                    break;

                default:
                    break;
            }
        }

        private void ShowAreaMode()
        {
            _grabMode = GrabMode.AreaMode;
            lblCameraExposure.Text = "EXPOSURE [us]";
        }

        private void ShowLineMode()
        {
            _grabMode = GrabMode.LineMode;
            lblCameraExposure.Text = "D GAIN (0 ~ 8[dB])";
        }

        private void UpdateData()
        {
            if (GrabMode.AreaMode == _grabMode)
                lblCameraExposureValue.Text = "";
            else
                lblCameraExposureValue.Text = "";

            lblCameraGainValue.Text = "";
            nudLightDimmingLevel.Text = "";
        }

        private delegate void UpdateUIDelegate(object obj);
        public void UpdateUI(object obj)
        {
            if (this.InvokeRequired)
            {
                UpdateUIDelegate callback = UpdateUI;
                BeginInvoke(callback, obj);
                return;
            }

            UpdateStatus();
        }

        private void UpdateStatus()
        {
            UpdateMotionStatus();
            UpdateRepeatCount();
        }

        private void UpdateMotionStatus()
        {
        }

        private void UpdateRepeatCount()
        {
        }
    }
}
