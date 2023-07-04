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

        private AxisHandler AxisHandler { get; set; } = null;

        private LAFCtrl LAFCtrl { get; set; } = null;

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

            if (status.IsAutoFocusOn)
            {
                lblAFOn.BackColor = _selectedColor;
                lblAFOff.BackColor = _nonSelectedColor;
            }
            else
            {
                lblAFOn.BackColor = _nonSelectedColor;
                lblAFOff.BackColor = _selectedColor;
            }
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
            double targetPosition = KeyPadHelper.SetLabelDoubleData((Label)sender);
            //TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetTargetPosition(AxisName.Z, targetPosition);
            AxisInfo.TargetPosition = targetPosition;
        }

        private void lblSetCurrentToTarget_Click(object sender, EventArgs e)
        {
            lblTargetPositionValue.Text = lblCuttentPositionValue.Text;
        }

        private void lblTeachCogValue_Click(object sender, EventArgs e)
        {
            int centerOfGravity = KeyPadHelper.SetLabelIntegerData((Label)sender);
            //TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetCenterOfGravity(AxisName.Z, centerOfGravity);
            AxisInfo.CenterOfGravity = centerOfGravity;
        }

        private void lblCurrentToTeach_Click(object sender, EventArgs e)
        {
            int cog = Convert.ToInt32(lblCurrentCogValue.Text);
            LAFManager.Instance().SetCenterOfGravity(LAFCtrl.Name, cog);
            lblTeachCogValue.Text = cog.ToString();
        }

        private void lblAFOn_Click(object sender, EventArgs e)
        {
            LAFManager.Instance().AutoFocusOnOff(LAFCtrl.Name, true);
        }


        private void lblAFOff_Click(object sender, EventArgs e)
        {
            LAFManager.Instance().AutoFocusOnOff(LAFCtrl.Name, false);
        }

        public TeachingAxisInfo GetCurrentData()
        {
            TeachingAxisInfo param = new TeachingAxisInfo();

            param.TargetPosition = AxisInfo.TargetPosition;
            param.CenterOfGravity = AxisInfo.CenterOfGravity;

            return param.DeepCopy();
        }
        #endregion
    }
}
