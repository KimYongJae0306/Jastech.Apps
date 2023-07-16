using ATT_UT_IPAD.UI.Controls;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform;
using Jastech.Framework.Config;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Structure;
using Jastech.Framework.Structure.Service;
using Jastech.Framework.Winform.Controls;
using Jastech.Framework.Winform.Forms;
using Jastech.Framework.Winform.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static Jastech.Framework.Device.Motions.AxisMovingParam;

namespace ATT_UT_IPAD.UI.Forms
{
    public partial class MotionPopupForm : Form
    {
        #region 필드
        private System.Threading.Timer _formTimer = null;

        private Color _selectedColor;

        private Color _nonSelectedColor;

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        #endregion

        #region 속성
        public InspModelService InspModelService { get; set; } = null;

        private TeachingPositionListControl TeachingPositionListControl { get; set; } = new TeachingPositionListControl();

        private List<TeachingInfo> TeachingPositionList { get; set; } = null;

        private AxisHandler AxisHandler { get; set; } = null;

        private LAFCtrl AlignLafCtrl { get; set; } = null;

        private LAFCtrl AkkonnLafCtrl { get; set; } = null;

        //private LAFCtrl 
        private MotionJogXControl MotionJogXControl { get; set; } = new MotionJogXControl() { Dock = DockStyle.Fill };

        private LAFJogControl LAFJogZ1Control { get; set; } = new LAFJogControl() { Dock = DockStyle.Fill };

        private LAFJogControl LAFJogZ2Control { get; set; } = new LAFJogControl() { Dock = DockStyle.Fill };

        private MotionParameterVariableControl XVariableControl = new MotionParameterVariableControl();

        private MotionParameterVariableControl Z1VariableControl = new MotionParameterVariableControl();

        private MotionParameterVariableControl Z2VariableControl = new MotionParameterVariableControl();

        public TeachingPosType TeachingPositionType = TeachingPosType.Standby;

        public UnitName UnitName { get; set; } = UnitName.Unit0;
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        public Action CloseEventDelegate;
        #endregion

        #region 생성자
        public MotionPopupForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void MotionPopupForm_Load(object sender, EventArgs e)
        {
            UpdateData();
            AddControl();
            StartTimer();
            InitializeUI();
            SetDefaultValue();
        }

        private void SetDefaultValue()
        {
            if (MotionJogXControl != null)
            {
                MotionJogXControl.JogMode = JogMode.Jog;
                MotionJogXControl.JogSpeedMode = JogSpeedMode.Slow;
                MotionJogXControl.JogPitch = Convert.ToDouble(lblPitchXYValue.Text);
            }

            if (LAFJogZ1Control != null)
            {
                LAFJogZ1Control.JogMode = JogMode.Jog;
                LAFJogZ1Control.JogSpeedMode = JogSpeedMode.Slow;
                LAFJogZ1Control.MoveAmount = Convert.ToDouble(lblPitchZValue.Text);
            }

            if (LAFJogZ2Control != null)
            {
                LAFJogZ2Control.JogMode = JogMode.Jog;
                LAFJogZ2Control.JogSpeedMode = JogSpeedMode.Slow;
                LAFJogZ2Control.MoveAmount = Convert.ToDouble(lblPitchZValue.Text);
            }
        }

        private void InitializeUI()
        {
            this.Location = new Point(1000, 40);
            this.Size = new Size(800, 1000);

            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            tlpMotionFunction.Dock = DockStyle.Fill;
            tlpVariableParameters.Dock = DockStyle.Fill;

            rdoJogSlowMode.Checked = true;
            rdoJogMode.Checked = true;
            ShowCommandPage();
        }

        private void AddControl()
        {
            AddTeachingPositionListControl();
            AddJogControl();
            AddVariableControl();
        }

        private void AddTeachingPositionListControl()
        {
            TeachingPositionListControl.Dock = DockStyle.Fill;
            TeachingPositionListControl.UnitName = UnitName;
            TeachingPositionListControl.SendEventHandler += new TeachingPositionListControl.SetTeachingPositionListDelegate(ReceiveTeachingPosition);
            pnlTeachingPositionList.Controls.Add(TeachingPositionListControl);
        }

        private void ReceiveTeachingPosition(TeachingPosType teachingPositionType)
        {
            Console.WriteLine(teachingPositionType.ToString());
            TeachingPositionType = teachingPositionType;

            UpdateParam(teachingPositionType);
        }

        private void UpdateData()
        {
            var axisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
            SetAxisHandler(axisHandler);

            string unitName = UnitName.Unit0.ToString();  // 나중에 변수로...
            var posData = TeachingData.Instance().GetUnit(unitName).GetTeachingInfoList();
            SetTeachingPosition(posData);

            var alignLafCtrl = LAFManager.Instance().GetLAFCtrl("Align");
            SetAlignLAFCtrl(alignLafCtrl);

            var akkonLafCtrl = LAFManager.Instance().GetLAFCtrl("Akkon");
            SetAkkonLafCtrl(akkonLafCtrl);

            UpdateParam();
        }

        private void UpdateParam(TeachingPosType teachingPositionType = TeachingPosType.Standby)
        {
            var param = TeachingPositionList.Where(x => x.Name == teachingPositionType.ToString()).First();
            if (param == null)
                return;

            lblTargetPositionX.Text = param.GetTargetPosition(AxisName.X).ToString();
            lblOffsetX.Text = param.GetOffset(AxisName.X).ToString();
            XVariableControl.UpdateData(param.GetMovingParams(AxisName.X));

            lblTargetPositionZ1.Text = param.GetTargetPosition(AxisName.Z1).ToString();
            lblTeachedCenterOfGravityZ1.Text = param.GetCenterOfGravity(AxisName.Z1).ToString();
            Z1VariableControl.UpdateData(param.GetMovingParams(AxisName.Z1));

            lblTargetPositionZ2.Text = param.GetTargetPosition(AxisName.Z2).ToString();
            lblTeachedCenterOfGravityZ2.Text = param.GetCenterOfGravity(AxisName.Z2).ToString();
            Z2VariableControl.UpdateData(param.GetMovingParams(AxisName.Z2));
        }

        public void SetAxisHandler(AxisHandler axisHandler)
        {
            AxisHandler = axisHandler;
        }

        public void SetTeachingPosition(List<TeachingInfo> teacingPositionList)
        {
            TeachingPositionList = teacingPositionList;
        }

        public void SetAlignLAFCtrl(LAFCtrl alignLafCtrl)
        {
            AlignLafCtrl = alignLafCtrl;
        }

        public void SetAkkonLafCtrl(LAFCtrl akkonLafCtrl)
        {
            AkkonnLafCtrl = akkonLafCtrl;
        }

        private void AddVariableControl()
        {
            XVariableControl.Dock = DockStyle.Fill;
            XVariableControl.SetAxis(AxisHandler.GetAxis(AxisName.X));

            Z1VariableControl.Dock = DockStyle.Fill;
            Z1VariableControl.SetAxis(AxisHandler.GetAxis(AxisName.Z1));

            Z2VariableControl.Dock = DockStyle.Fill;
            Z2VariableControl.SetAxis(AxisHandler.GetAxis(AxisName.Z2));

            tlpVariableParameter.Controls.Add(XVariableControl);
            tlpVariableParameter.Controls.Add(Z1VariableControl);
            tlpVariableParameter.Controls.Add(Z2VariableControl);
        }

        private void AddJogControl()
        {
            pnlMotionJog.Controls.Add(MotionJogXControl);
            MotionJogXControl.SetAxisHanlder(AxisHandler);

            pnlLAFZ1Jog.Controls.Add(LAFJogZ1Control);
            LAFJogZ1Control.SetSelectedLafCtrl(AlignLafCtrl);

            pnlLAFZ2Jog.Controls.Add(LAFJogZ2Control);
            LAFJogZ2Control.SetSelectedLafCtrl(AkkonnLafCtrl);
        }

        private void StartTimer()
        {
            _formTimer = new System.Threading.Timer(UpdateStatus, null, 1000, 1000);
        }

        private delegate void UpdateStatusDelegate(object obj);
        private void UpdateStatus(object obj)
        {
            if (this.InvokeRequired)
            {
                UpdateStatusDelegate callback = UpdateStatus;
                BeginInvoke(callback, obj);
                return;
            }

            UpdateStatusMotionX();
            UpdateStatusAutoFocusZ1();
            UpdateStatusAutoFocusZ2();
        }

        private void UpdateStatusMotionX()
        {
            var axis = AxisHandler.GetAxis(AxisName.X);

            if (axis == null || !axis.IsConnected())
                return;

            lblCurrentPositionX.Text = axis.GetActualPosition().ToString("F3");

            if (axis.IsNegativeLimit())
                lblSensorX.Text = "-";
            else if (axis.IsPositiveLimit())
                lblSensorX.Text = "+";
            else
                lblSensorX.Text = "";

            lblAxisStatusX.Text = axis.GetCurrentMotionStatus().ToString();

            if (axis.IsEnable())
                lblServoOnOffX.Text = "On";
            else
                lblServoOnOffX.Text = "Off";
        }

        private void UpdateStatusAutoFocusZ1()
        {
            var status = AlignLafCtrl.Status;

            if (status == null)
                return;

            double mPos_um = 0.0;
            if (AlignLafCtrl is NuriOneLAFCtrl nuriOne)
                mPos_um = status.MPosPulse / nuriOne.ResolutionAxisZ;
            else
                mPos_um = status.MPosPulse;

            lblCurrentPositionZ1.Text = mPos_um.ToString("F3");
            lblCurrentCenterOfGravityZ1.Text = status.CenterofGravity.ToString();

            if (status.IsNegativeLimit)
                lblSensorZ1.Text = "-";
            else if (status.IsPositiveLimit)
                lblSensorZ1.Text = "+";
            else
                lblSensorZ1.Text = "Done";

            if (status.IsAutoFocusOn)
            {
                lblAutoFocusOnZ1.BackColor = _selectedColor;
                lblAutoFocusOffZ1.BackColor = _nonSelectedColor;
            }
            else
            {
                lblAutoFocusOnZ1.BackColor = _nonSelectedColor;
                lblAutoFocusOffZ1.BackColor = _selectedColor;
            }
        }

        private void UpdateStatusAutoFocusZ2()
        {
            var status = AkkonnLafCtrl.Status;

            if (status == null)
                return;

            double mPos_um = 0.0;
            if (AkkonnLafCtrl is NuriOneLAFCtrl nuriOne)
                mPos_um = status.MPosPulse / nuriOne.ResolutionAxisZ;
            else
                mPos_um = status.MPosPulse;

            lblCurrentPositionZ2.Text = mPos_um.ToString("F3");
            lblCurrentCenterOfGravityZ2.Text = status.CenterofGravity.ToString();

            if (status.IsNegativeLimit)
                lblSensorZ2.Text = "-";
            else if (status.IsPositiveLimit)
                lblSensorZ2.Text = "+";
            else
                lblSensorZ2.Text = "Done";

            if (status.IsAutoFocusOn)
            {
                lblAutoFocusOnZ2.BackColor = _selectedColor;
                lblAutoFocusOffZ2.BackColor = _nonSelectedColor;
            }
            else
            {
                lblAutoFocusOnZ2.BackColor = _nonSelectedColor;
                lblAutoFocusOffZ2.BackColor = _selectedColor;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCommand_Click(object sender, EventArgs e)
        {
            ShowCommandPage();
        }

        private void ShowCommandPage()
        {
            tlpMotionFunction.Visible = true;
            tlpVariableParameters.Visible = false;
        }

        private void btnParameter_Click(object sender, EventArgs e)
        {
            ShowParameterPage();
        }

        private void ShowParameterPage()
        {
            tlpMotionFunction.Visible = false;
            tlpVariableParameters.Visible = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void UpdateCurrentData()
        {
            GetCurrentVariableParams();
        }

        private void GetCurrentVariableParams()
        {
            var posData = TeachingData.Instance().GetUnit(UnitName.ToString()).GetTeachingInfo(TeachingPositionType);

            posData.SetMovingParams(AxisName.X, XVariableControl.GetCurrentData());
            posData.SetMovingParams(AxisName.Z1, Z1VariableControl.GetCurrentData());
            posData.SetMovingParams(AxisName.Z2, Z2VariableControl.GetCurrentData());
        }

        private void Save()
        {
            UpdateCurrentData();

            // Save AxisHandler
            var axisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
            MotionManager.Instance().Save(axisHandler);

            // Save Model
            var model = ModelManager.Instance().CurrentModel as AppsInspModel;
            model.SetUnitList(TeachingData.Instance().UnitList);

            string fileName = System.IO.Path.Combine(ConfigSet.Instance().Path.Model, model.Name, InspModel.FileName);

            InspModelService?.Save(fileName, model);

            MessageConfirmForm form = new MessageConfirmForm();
            form.Message = "Save Motion Data Completed.";
            form.ShowDialog();
        }

        private void lblTargetPositionX_Click(object sender, EventArgs e)
        {
            double targetPosition = KeyPadHelper.SetLabelDoubleData((Label)sender);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetTargetPosition(AxisName.X, targetPosition);
        }

        private void lblOffsetX_Click(object sender, EventArgs e)
        {
            double offset = KeyPadHelper.SetLabelDoubleData((Label)sender);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetOffset(AxisName.X, offset);
        }

        private void lblCurrentToTargetX_Click(object sender, EventArgs e)
        {
            double currentPosition = Convert.ToDouble(lblCurrentPositionX.Text);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetTargetPosition(AxisName.X, currentPosition);

            lblTargetPositionX.Text = currentPosition.ToString("F3");
        }

        private void lblMoveToTargetX_Click(object sender, EventArgs e)
        {
            double targetPosition = TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().GetTargetPosition(AxisName.X);
            var movingParam = TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().GetMovingParams(AxisName.X);

            AxisHandler.GetAxis(AxisName.X).StartAbsoluteMove(targetPosition, movingParam);
        }

        private void lblTargetPositionZ1_Click(object sender, EventArgs e)
        {
            double targetPosition = KeyPadHelper.SetLabelDoubleData((Label)sender);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetTargetPosition(AxisName.Z1, targetPosition);
        }


        private void lblCurrentToTargetZ1_Click(object sender, EventArgs e)
        {
            double currentPosition = Convert.ToDouble(lblCurrentPositionZ1.Text);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetTargetPosition(AxisName.Z1, currentPosition);

            lblTargetPositionZ1.Text = currentPosition.ToString("F3");
        }

        private void lblTeachedCenterOfGravityZ1_Click(object sender, EventArgs e)
        {
            int centerOfGravity = KeyPadHelper.SetLabelIntegerData((Label)sender);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetCenterOfGravity(AxisName.Z1, centerOfGravity);
        }

        private void lblTargetPositionZ2_Click(object sender, EventArgs e)
        {
            double targetPosition = KeyPadHelper.SetLabelDoubleData((Label)sender);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetTargetPosition(AxisName.Z2, targetPosition);
        }
        private void lblCurrentToTargetZ2_Click(object sender, EventArgs e)
        {
            double currentPosition = Convert.ToDouble(lblCurrentPositionZ1.Text);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetTargetPosition(AxisName.Z2, currentPosition);

            lblTargetPositionZ2.Text = currentPosition.ToString("F3");
        }

        private void lblCurrentToTargetCenterOfGravityZ1_Click(object sender, EventArgs e)
        {
            int targetCenterOfGravity = Convert.ToInt32(lblCurrentCenterOfGravityZ1.Text);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetCenterOfGravity(AxisName.Z1, targetCenterOfGravity);

            lblTeachedCenterOfGravityZ1.Text = targetCenterOfGravity.ToString();
        }

        private void lblMoveToTargetZ1_Click(object sender, EventArgs e)
        {
            double targetPosition = TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().GetTargetPosition(AxisName.Z1);

            double mPos_um = 0.0;
            if (AlignLafCtrl is NuriOneLAFCtrl nuriOne)
                mPos_um = AlignLafCtrl.Status.MPosPulse / nuriOne.ResolutionAxisZ;
            else
                mPos_um = AlignLafCtrl.Status.MPosPulse;

            double currentPosition = mPos_um;

            Direction direction = Direction.CCW;
            double moveAmount = targetPosition - currentPosition;
            if (moveAmount < 0)
                direction = Direction.CW;
            else
                direction = Direction.CCW;

            AlignLafCtrl.SetMotionRelativeMove(direction, Math.Abs(moveAmount));
        }

        private void lblOriginZ1_Click(object sender, EventArgs e)
        {
            LAFManager.Instance().StartHomeThread("Align");
        }

        private void lblAutoFocusOnZ1_Click(object sender, EventArgs e)
        {
            var status = AlignLafCtrl.Status;

            if (!status.IsAutoFocusOn)
                LAFManager.Instance().AutoFocusOnOff("Align", true);
        }

        private void lblAutoFocusOffZ1_Click(object sender, EventArgs e)
        {
            var status = AlignLafCtrl.Status;

            if (status.IsAutoFocusOn)
                LAFManager.Instance().AutoFocusOnOff("Align", false);
        }

        private void lblTeachedCenterOfGravityZ2_Click(object sender, EventArgs e)
        {
            int centerOfGravity = KeyPadHelper.SetLabelIntegerData((Label)sender);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetCenterOfGravity(AxisName.Z2, centerOfGravity);
        }

        private void lblCurrentToTargetCenterOfGravityZ2_Click(object sender, EventArgs e)
        {
            int targetCenterOfGravity = Convert.ToInt32(lblCurrentCenterOfGravityZ2.Text);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetCenterOfGravity(AxisName.Z2, targetCenterOfGravity);

            lblTeachedCenterOfGravityZ2.Text = targetCenterOfGravity.ToString();
        }

        private void lblMoveToTargetZ2_Click(object sender, EventArgs e)
        {
            double targetPosition = TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().GetTargetPosition(AxisName.Z2);

            double mPos_um = 0.0;
            if (AkkonnLafCtrl is NuriOneLAFCtrl nuriOne)
                mPos_um = AkkonnLafCtrl.Status.MPosPulse / nuriOne.ResolutionAxisZ;
            else
                mPos_um = AkkonnLafCtrl.Status.MPosPulse;

            double currentPosition = mPos_um;

            Direction direction = Direction.CCW;
            double moveAmount = targetPosition - currentPosition;
            if (moveAmount < 0)
                direction = Direction.CW;
            else
                direction = Direction.CCW;

            AkkonnLafCtrl.SetMotionRelativeMove(direction, Math.Abs(moveAmount));
        }

        private void lblOriginZ2_Click(object sender, EventArgs e)
        {
            LAFManager.Instance().StartHomeThread("Akkon");
        }

        private void lblAutoFocusOnZ2_Click(object sender, EventArgs e)
        {
            var status = AkkonnLafCtrl.Status;

            if (!status.IsAutoFocusOn)
                LAFManager.Instance().AutoFocusOnOff("Akkon", true);
        }

        private void lblAutoFocusOffZ2_Click(object sender, EventArgs e)
        {
            var status = AkkonnLafCtrl.Status;

            if (status.IsAutoFocusOn)
                LAFManager.Instance().AutoFocusOnOff("Akkon", false);
        }

        private void rdoJogSlowMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoJogSlowMode.Checked)
            {
                SetSelectJogSpeedMode(JogSpeedMode.Slow);
                rdoJogSlowMode.BackColor = _selectedColor;
            }
            else
                rdoJogSlowMode.BackColor = _nonSelectedColor;
        }

        private void rdoJogFastMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoJogFastMode.Checked)
            {
                SetSelectJogSpeedMode(JogSpeedMode.Fast);
                rdoJogFastMode.BackColor = _selectedColor;
            }
            else
                rdoJogFastMode.BackColor = _nonSelectedColor;
        }

        private void SetSelectJogSpeedMode(JogSpeedMode jogSpeedMode)
        {
            MotionJogXControl.JogSpeedMode = jogSpeedMode;
            LAFJogZ1Control.JogSpeedMode = jogSpeedMode;
        }

        private void rdoJogMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoJogMode.Checked)
            {
                SetSelectJogMode(JogMode.Jog);
                rdoJogMode.BackColor = _selectedColor;
            }
            else
                rdoJogMode.BackColor = _nonSelectedColor;
        }

        private void rdoIncreaseMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoIncreaseMode.Checked)
            {
                SetSelectJogMode(JogMode.Increase);
                rdoIncreaseMode.BackColor = _selectedColor;
            }
            else
                rdoIncreaseMode.BackColor = _nonSelectedColor;
        }

        private void SetSelectJogMode(JogMode jogMode)
        {
            MotionJogXControl.JogMode = jogMode;
            LAFJogZ1Control.JogMode = jogMode;
        }

        private void lblPitchXYValue_Click(object sender, EventArgs e)
        {
            double pitchXY = KeyPadHelper.SetLabelDoubleData((Label)sender);
            MotionJogXControl.JogPitch = pitchXY;
        }

        private void lblPitchZValue_Click(object sender, EventArgs e)
        {
            double pitchZ = KeyPadHelper.SetLabelDoubleData((Label)sender);
            LAFJogZ1Control.MoveAmount = pitchZ;
        }

        private void MotionPopupForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _formTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

            if (_formTimer != null)
            {
                _formTimer.Dispose();
                _formTimer = null;
            }
        }

        private void MotionPopupForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CloseEventDelegate != null)
                CloseEventDelegate();
        }
    }
    #endregion
}
