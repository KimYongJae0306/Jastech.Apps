using Jastech.Apps.Structure.Data;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Structure;
using Jastech.Framework.Winform.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AutoFocusControl : UserControl
    {
        #region 필드
        private Color _selectedColor;

        private Color _nonSelectedColor;
        #endregion

        #region 속성
        private Axis SelectedAxis { get; set; } = null;

        public TeachingAxisInfo AxisInfo { get; set; } = null;

        public AxisCommonParams AxisCommonParams { get; set; } = null;

        private AxisHandler AxisHandler { get; set; } = null;

        private LAFCtrl LAFCtrl { get; set; } = null;

        private LAFData LAFData { get; set; } = null;

        private List<TeachingInfo> TeachingPositionList { get; set; } = null;
        
        public TeachingPosType TeachingPositionType = TeachingPosType.Stage1_Scan_Start;
        #endregion

        #region 델리게이트  
        private delegate void UpdateMotionStatusDelegate();
        #endregion

        #region 생성자
        public AutoFocusControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void AutoFocusControl_Load(object sender, EventArgs e)
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);
        }

        public void UpdateData(TeachingAxisInfo axisInfo)
        {
            AxisInfo = axisInfo.DeepCopy();
            AxisCommonParams = SelectedAxis.AxisCommonParams.DeepCopy();

            UpdateUI();
        }

        private void UpdateUI()
        {
            lblTargetPositionValue.Text = AxisInfo.TargetPosition.ToString();
            lblTeachCogValue.Text = AxisInfo.CenterOfGravity.ToString();
            
            lblNegativeLimit.Text = SelectedAxis.AxisCommonParams.NegativeLimit.ToString("F3");
            lblPositiveLimit.Text = SelectedAxis.AxisCommonParams.PositiveLimit.ToString("F3");

            lblLowerReturndB.Text = "0.0";
            lblUpperReturndB.Text = "0.0";
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

            lblCurrentReturndB.Text = status.ReturndB.ToString();

            if (status.IsTrackingOn)
            {
                lblTrackingOn.BackColor = _selectedColor;
                lblTrackingOff.BackColor = _nonSelectedColor;
            }
            else
            {
                lblTrackingOn.BackColor = _nonSelectedColor;
                lblTrackingOff.BackColor = _selectedColor;
            }
        }

        public void SetAxis(Axis axis)
        {
            SelectedAxis = axis;
        }

        public void SetLAFCtrl(LAFCtrl lafCtrl, LAFData lafData)
        {
            LAFCtrl = lafCtrl;
        }

        private void lblTargetPositionZValue_Click(object sender, EventArgs e)
        {
            double targetPosition = KeyPadHelper.SetLabelDoubleData((Label)sender);
            AxisInfo.TargetPosition = targetPosition;
        }

        private void lblSetCurrentToTarget_Click(object sender, EventArgs e)
        {
            lblTargetPositionValue.Text = lblCuttentPositionValue.Text;
        }

        private void lblTeachCogValue_Click(object sender, EventArgs e)
        {
            int centerOfGravity = KeyPadHelper.SetLabelIntegerData((Label)sender);
            AxisInfo.CenterOfGravity = centerOfGravity;
        }

        private void lblNegativeLimit_Click(object sender, EventArgs e)
        {
            double swNegativeLimit = KeyPadHelper.SetLabelDoubleData((Label)sender);
            AxisCommonParams.NegativeLimit = swNegativeLimit;
        }

        private void lblPositiveLimit_Click(object sender, EventArgs e)
        {
            double swPositiveLimit = KeyPadHelper.SetLabelDoubleData((Label)sender);
            AxisCommonParams.PositiveLimit = swPositiveLimit;
        }

        private void lblCurrentToTeach_Click(object sender, EventArgs e)
        {
            int cog = Convert.ToInt32(lblCurrentCogValue.Text);
            LAFCtrl?.SetCenterOfGravity(cog);
            lblTeachCogValue.Text = cog.ToString();
        }

        private void lblTrackingOn_Click(object sender, EventArgs e)
        {
            LAFCtrl?.SetTrackingOnOFF(true);
        }

        private void lblTrackingOff_Click(object sender, EventArgs e)
        {
            LAFCtrl?.SetTrackingOnOFF(false);
        }

        public TeachingAxisInfo GetCurrentTeachingData()
        {
            TeachingAxisInfo param = new TeachingAxisInfo();

            param.TargetPosition = AxisInfo.TargetPosition;
            param.CenterOfGravity = AxisInfo.CenterOfGravity;

            param.MovingParam = new AxisMovingParam();
            param.MovingParam = AxisInfo.MovingParam.DeepCopy();

            return param;
        }

        public AxisCommonParams GetAxisCommonParams()
        {
            AxisCommonParams param = new AxisCommonParams();

            param = AxisCommonParams.DeepCopy();

            //param.JogLowSpeed = AxisCommonParams.JogLowSpeed;
            //param.JogHighSpeed = AxisCommonParams.JogHighSpeed;
            //param.MoveTolerance = AxisCommonParams.MoveTolerance;
            //param.NegativeLimit = AxisCommonParams.NegativeLimit;
            //param.PositiveLimit = AxisCommonParams.PositiveLimit;
            //param.HommingTimeOut = AxisCommonParams.HommingTimeOut;

            return param;
        }

        #endregion

        private void lblLowerReturndB_Click(object sender, EventArgs e)
        {
            double lowerReturndB = KeyPadHelper.SetLabelDoubleData((Label)sender);
            LAFData.LowerReturndB = lowerReturndB;
        }

        private void lblUpperReturndB_Click(object sender, EventArgs e)
        {
            double upperReturndB = KeyPadHelper.SetLabelDoubleData((Label)sender);
            LAFData.UpperReturndB = upperReturndB;
        }

        private void cbxEnableRetrundB_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxEnableRetrundB.Checked)
            {
                LAFCtrl?.SetUpperReturndB(LAFData.LowerReturndB);
                LAFCtrl?.SetUpperReturndB(LAFData.UpperReturndB);
            }
            else
            {
                LAFCtrl?.SetUpperReturndB(0);
                LAFCtrl?.SetUpperReturndB(0);
            }
        }
    }
}
