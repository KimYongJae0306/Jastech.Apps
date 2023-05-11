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
using Jastech.Apps.Structure.Data;

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

            double mPos_um = 0.0;
            if (LAFCtrl is NuriOneLAFCtrl nuriOne)
                mPos_um = status.MPosPulse / nuriOne.ResolutionAxisZ;
            else
                mPos_um = status.MPosPulse;

            lblCuttentPositionValue.Text = mPos_um.ToString();
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

        private List<TeachingInfo> TeachingPositionList { get; set; } = null;
        public TeachingPosType TeachingPositionType = TeachingPosType.Stage1_Scan_Start;

        private void lblTargetPositionZValue_Click(object sender, EventArgs e)
        {
            double targetPosition = SetLabelDoubleData(sender);
            //TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetTargetPosition(AxisName.Z, targetPosition);
            AxisInfo.TargetPosition = targetPosition;
        }

         private void btnSetCurrentToTarget_Click(object sender, EventArgs e)
        {
            lblTargetPositionValue.Text = lblCuttentPositionValue.Text;
        }

        private void lblTeachCogValue_Click(object sender, EventArgs e)
        {
            int centerOfGravity = SetLabelIntegerData(sender);
            //TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetCenterOfGravity(AxisName.Z, centerOfGravity);
            AxisInfo.CenterOfGravity = centerOfGravity;
        }

        private void btnCurrentToTeach_Click(object sender, EventArgs e)
        {
            int cog = Convert.ToInt32(lblCurrentCogValue.Text);
            AppsLAFManager.Instance().SetCenterOfGravity(LAFName.Akkon.ToString(), cog);
            lblTeachCogValue.Text = cog.ToString();
        }

        private void bnAFOn_Click(object sender, EventArgs e)
        {
            AppsLAFManager.Instance().AutoFocusOnOff(LAFName.Akkon.ToString(), true);
        }

        private void btnAFOff_Click(object sender, EventArgs e)
        {
            AppsLAFManager.Instance().AutoFocusOnOff(LAFName.Akkon.ToString(), false);
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

        private double SetLabelDoubleData(object sender)
        {
            Label lbl = sender as Label;
            double prevData = Convert.ToDouble(lbl.Text);

            KeyPadForm keyPadForm = new KeyPadForm();
            keyPadForm.PreviousValue = prevData;
            keyPadForm.ShowDialog();

            double inputData = keyPadForm.PadValue;

            Label label = (Label)sender;
            label.Text = inputData.ToString();

            return inputData;
        }

        private int SetLabelIntegerData(object sender)
        {
            Label lbl = sender as Label;
            int prevData = Convert.ToInt32(lbl.Text);

            KeyPadForm keyPadForm = new KeyPadForm();
            keyPadForm.PreviousValue = (double)prevData;
            keyPadForm.ShowDialog();

            int inputData = Convert.ToInt16(keyPadForm.PadValue);

            Label label = (Label)sender;
            label.Text = inputData.ToString();

            return inputData;
        }

        public TeachingAxisInfo GetCurrentData()
        {
            TeachingAxisInfo param = new TeachingAxisInfo();

            param.TargetPosition = AxisInfo.TargetPosition;
            param.CenterOfGravity = AxisInfo.CenterOfGravity;

            return param.DeepCopy();
        }
    }
}
