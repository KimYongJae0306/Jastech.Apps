using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Framework.Winform.Forms;
using Jastech.Framework.Structure;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Device.LAFCtrl;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AutoFocusControl : UserControl
    {
        private Axis SelectedAxis { get; set; } = null;

        public TeachingAxisInfo AxisInfo { get; set; } = null;

        private AxisHandler AxisHandler { get; set; } = null;

        private delegate void UpdateMotionStatusDelegate();

        public AutoFocusControl()
        {
            InitializeComponent();
        }

        public void UpdateData(TeachingAxisInfo axisInfo)
        {
            AxisInfo = axisInfo.DeepCopy();
            UpdateUI();
        }

        private void UpdateUI()
        {
            lblTargetPositionValue.Text = AxisInfo.TargetPosition.ToString();
            lblTeachCogValue.Text = AxisInfo.CenterOfGravity.ToString();
        }

        public void UpdateAxisStatus()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    UpdateMotionStatusDelegate callback = UpdateAxisStatus;
                    BeginInvoke(callback);
                    return;
                }

                UpdateStatus();
            }
            catch (Exception ex)
            {
                Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() + " : " + ex.Message);
            }
        }

        private LAFCtrl LAFCtrl { get; set; } = null;
        private void UpdateStatus()
        {
            var status = LAFCtrl.Status;

            if (status == null)
                return;

            lblCuttentPositionValue.Text = status.MPos.ToString();
            lblCurrentCogValue.Text = status.CenterofGravity.ToString();
        }

        public void SetAxisHanlder(AxisHandler axisHandler)
        {
            AxisHandler = axisHandler;
            SetAxis(AxisHandler);
        }

        private void SetAxis(AxisHandler axisHandler)
        {
            SelectedAxis = axisHandler.GetAxis(AxisName.Z);
        }

        public void SetLAFCtrl(LAFCtrl lafCtrl)
        {
            LAFCtrl = lafCtrl;
        }

        private void lblTargetPositionZValue_Click(object sender, EventArgs e)
        {
            SetLabelDoubleData(sender);
        }

        private double SetLabelDoubleData(object sender)
        {
            KeyPadForm keyPadForm = new KeyPadForm();
            keyPadForm.ShowDialog();

            double inputData = keyPadForm.PadValue;

            Label label = (Label)sender;
            label.Text = inputData.ToString();

            return inputData;
        }

        private void btnSetCurrentToTarget_Click(object sender, EventArgs e)
        {
            lblTargetPositionValue.Text = lblCuttentPositionValue.Text;
        }

        private void lblTeachCogValue_Click(object sender, EventArgs e)
        {
            SetLabelIntegerData(sender);
        }

        private int SetLabelIntegerData(object sender)
        {
            KeyPadForm keyPadForm = new KeyPadForm();
            keyPadForm.ShowDialog();

            int inputData = Convert.ToInt32(keyPadForm.PadValue);

            Label label = (Label)sender;
            label.Text = inputData.ToString();

            return inputData;
        }

        private void btnCurrentToTeach_Click(object sender, EventArgs e)
        {
            int cog = Convert.ToInt32(lblCurrentCogValue.Text);
            //Main.LAF.SetFocusPosition(cog);
            lblTeachCogValue.Text = cog.ToString();
        }

        private void bnAFOn_Click(object sender, EventArgs e)
        {

        }

        private void btnAFOff_Click(object sender, EventArgs e)
        {

        }

        private void AutoFocusOnOff(bool isOn)
        {
            //if (isOn)
            //{
            //    int negative = Convert.ToInt32(Main.TeachingPositionList[(int)_teachingPosition].UnitPositionList[(int)eAxis.Axis_Z].TargetPosition - (Main.TeachingPositionList[(int)_teachingPosition].UnitPositionList[(int)eAxis.Axis_Z].MotionProperty.NegativeSWLimit * Main.LAF.GetPulseResolution()));
            //    int positive = Convert.ToInt32(Main.TeachingPositionList[(int)_teachingPosition].UnitPositionList[(int)eAxis.Axis_Z].TargetPosition + (Main.TeachingPositionList[(int)_teachingPosition].UnitPositionList[(int)eAxis.Axis_Z].MotionProperty.PositiveSWLimit * Main.LAF.GetPulseResolution()));

            //    Main.LAF.SetMotionNegativeLimit(negative);
            //    Main.LAF.SetMotionPositiveLimit(positive);
            //}
            //else
            //{
            //    Main.LAF.SetMotionNegativeLimit(0);
            //    Main.LAF.SetMotionPositiveLimit(0);
            //}

            //Main.LAF.SetAutoFocusOnOFF(isOn);
        }

        public TeachingAxisInfo GetCurrentData()
        {
            TeachingAxisInfo param = new TeachingAxisInfo();

            param.TargetPosition = 0;
            param.CenterOfGravity = 0;

            AxisInfo = param.DeepCopy();

            return AxisInfo;
        }
    }
}
