﻿using ATT.UI.Controls;
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

namespace ATT.UI.Forms
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

        private LAFCtrl LAFCtrl { get; set; } = null;

        private MotionJogXYControl MotionJogXYControl { get; set; } = new MotionJogXYControl() { Dock = DockStyle.Fill };

        private LAFJogControl LAFJogControl { get; set; } = new LAFJogControl() { Dock = DockStyle.Fill };

        private MotionParameterVariableControl XVariableControl = new MotionParameterVariableControl();

        private MotionParameterVariableControl YVariableControl = new MotionParameterVariableControl();

        private MotionParameterVariableControl ZVariableControl = new MotionParameterVariableControl();

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
            if (MotionJogXYControl != null)
            {
                MotionJogXYControl.JogMode = JogMode.Jog;
                MotionJogXYControl.JogSpeedMode = JogSpeedMode.Slow;
                MotionJogXYControl.JogPitch = Convert.ToDouble(lblPitchXYValue.Text);
            }

            if (LAFJogControl != null)
            {
                LAFJogControl.JogMode = JogMode.Jog;
                LAFJogControl.JogSpeedMode = JogSpeedMode.Slow;
                LAFJogControl.MoveAmount = Convert.ToDouble(lblPitchZValue.Text);
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
            var posData = TeachingData.Instance().GetUnit(unitName).TeachingInfoList;
            SetTeachingPosition(posData);

            var lafCtrl = LAFManager.Instance().GetLAFCtrl("Akkon");
            SetLAFCtrl(lafCtrl);

            UpdateParam();
        }

        private void UpdateParam(TeachingPosType teachingPositionType = TeachingPosType.Standby)
        {
            var param = TeachingPositionList.Where(x => x.Name == teachingPositionType.ToString()).FirstOrDefault();
            if (param == null)
                return;

            lblTargetPositionX.Text = param.GetTargetPosition(AxisName.X).ToString();
            lblOffsetX.Text = param.GetOffset(AxisName.X).ToString();
            XVariableControl.UpdateData(param.GetMovingParams(AxisName.X));

            lblTargetPositionY.Text = param.GetTargetPosition(AxisName.Y).ToString();
            lblOffsetY.Text = param.GetOffset(AxisName.Y).ToString();
            YVariableControl.UpdateData(param.GetMovingParams(AxisName.Y));

            lblTargetPositionZ.Text = param.GetTargetPosition(AxisName.Z).ToString();
            lblTeachedCenterOfGravityZ.Text = param.GetCenterOfGravity(AxisName.Z).ToString();
            ZVariableControl.UpdateData(param.GetMovingParams(AxisName.Z));
        }

        public void SetAxisHandler(AxisHandler axisHandler)
        {
            AxisHandler = axisHandler;
        }

        public void SetTeachingPosition(List<TeachingInfo> teacingPositionList)
        {
            TeachingPositionList = teacingPositionList;
        }

        public void SetLAFCtrl(LAFCtrl lafCtrl)
        {
            LAFCtrl = lafCtrl;
        }

        private void AddVariableControl()
        {
            XVariableControl.Dock = DockStyle.Fill;
            XVariableControl.SetAxis(AxisHandler.GetAxis(AxisName.X));

            YVariableControl.Dock = DockStyle.Fill;
            YVariableControl.SetAxis(AxisHandler.GetAxis(AxisName.Y));

            ZVariableControl.Dock = DockStyle.Fill;
            ZVariableControl.SetAxis(AxisHandler.GetAxis(AxisName.Z));

            tlpVariableParameter.Controls.Add(XVariableControl);
            tlpVariableParameter.Controls.Add(YVariableControl);
            tlpVariableParameter.Controls.Add(ZVariableControl);
        }

        private void AddJogControl()
        {
            pnlMotionJog.Controls.Add(MotionJogXYControl);
            MotionJogXYControl.SetAxisHanlder(AxisHandler);

            pnlLAFJog.Controls.Add(LAFJogControl);
            LAFJogControl.SetSelectedLafCtrl(LAFCtrl);
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
            UpdateStatusMotionY();
            UpdateStatusAutoFocusZ();
        }

        private void UpdateStatusMotionX()
        {
            var axis = AxisHandler.AxisList[(int)AxisName.X];

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

        private void UpdateStatusMotionY()
        {
            var axis = AxisHandler.AxisList[(int)AxisName.Y];

            if (axis == null || !axis.IsConnected())
                return;

            lblCurrentPositionY.Text = axis.GetActualPosition().ToString("F3");

            if (axis.IsNegativeLimit())
                lblSensorY.Text = "-";
            else if (axis.IsPositiveLimit())
                lblSensorY.Text = "+";
            else
                lblSensorY.Text = "";

            lblAxisStatusY.Text = axis.GetCurrentMotionStatus().ToString();

            if (axis.IsEnable())
                lblServoOnOffY.Text = "On";
            else
                lblServoOnOffY.Text = "Off";
        }

        private void UpdateStatusAutoFocusZ()
        {
            var status = LAFCtrl.Status;

            if (status == null)
                return;

            double mPos_um = 0.0;
            if (LAFCtrl is NuriOneLAFCtrl nuriOne)
                mPos_um = status.MPosPulse / nuriOne.ResolutionAxisZ;
            else
                mPos_um = status.MPosPulse;

            lblCurrentPositionZ.Text = mPos_um.ToString("F3");
            lblCurrentCenterOfGravityZ.Text = status.CenterofGravity.ToString();

            if (status.IsNegativeLimit)
                lblSensorZ.Text = "-";
            else if (status.IsPositiveLimit)
                lblSensorZ.Text = "+";
            else
                lblSensorZ.Text = "";
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
            var posData = TeachingData.Instance().GetUnit(UnitName.ToString()).TeachingInfoList[(int)TeachingPositionType];

            posData.SetMovingParams(AxisName.X, XVariableControl.GetCurrentData());
            posData.SetMovingParams(AxisName.Y, YVariableControl.GetCurrentData());
            posData.SetMovingParams(AxisName.Z, ZVariableControl.GetCurrentData());
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

        private void lblTargetPositionY_Click(object sender, EventArgs e)
        {
            double targetPosition = KeyPadHelper.SetLabelDoubleData((Label)sender);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetTargetPosition(AxisName.Y, targetPosition);
        }

        private void lblOffsetY_Click(object sender, EventArgs e)
        {
            double offset = KeyPadHelper.SetLabelDoubleData((Label)sender);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetOffset(AxisName.Y, offset);
        }

        private void lblTargetPositionZ_Click(object sender, EventArgs e)
        {
            double targetPosition = KeyPadHelper.SetLabelDoubleData((Label)sender);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetTargetPosition(AxisName.Z, targetPosition);
        }

        private void lblTeachedCenterOfGravityZ_Click(object sender, EventArgs e)
        {
            int centerOfGravity = KeyPadHelper.SetLabelIntegerData((Label)sender);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetCenterOfGravity(AxisName.Z, centerOfGravity);
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
            MotionJogXYControl.JogSpeedMode = jogSpeedMode;
            LAFJogControl.JogSpeedMode = jogSpeedMode;
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
            MotionJogXYControl.JogMode = jogMode;
            LAFJogControl.JogMode = jogMode;
        }

        private void lblPitchXYValue_Click(object sender, EventArgs e)
        {
            double pitchXY = KeyPadHelper.SetLabelDoubleData((Label)sender);
            MotionJogXYControl.JogPitch = pitchXY;
        }

        private void lblPitchZValue_Click(object sender, EventArgs e)
        {
            double pitchZ = KeyPadHelper.SetLabelDoubleData((Label)sender);
            LAFJogControl.MoveAmount = pitchZ;
        }

        private void lblOriginZ_Click(object sender, EventArgs e)
        {
            LAFManager.Instance().StartHomeThread("Akkon");
        }

        private void lblCurrentToTargetX_Click(object sender, EventArgs e)
        {
            double currentPosition = Convert.ToDouble(lblCurrentPositionX.Text);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetTargetPosition(AxisName.X, currentPosition);

            lblTargetPositionX.Text = currentPosition.ToString("F3");
        }

        private void lblCurrentToTargetY_Click(object sender, EventArgs e)
        {
            double currentPosition = Convert.ToDouble(lblCurrentPositionY.Text);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetTargetPosition(AxisName.Y, currentPosition);

            lblTargetPositionY.Text = currentPosition.ToString("F3");
        }

        private void lblCurrentToTargetZ_Click(object sender, EventArgs e)
        {
            double currentPosition = Convert.ToDouble(lblCurrentPositionZ.Text);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetTargetPosition(AxisName.Z, currentPosition);

            lblTargetPositionZ.Text = currentPosition.ToString("F3");
        }

        private void lblCurrentToTargetCenterOfGravityZ_Click(object sender, EventArgs e)
        {
            int targetCenterOfGravity = Convert.ToInt32(lblCurrentCenterOfGravityZ.Text);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetCenterOfGravity(AxisName.Z, targetCenterOfGravity);

            lblTeachedCenterOfGravityZ.Text = targetCenterOfGravity.ToString();
        }

        private void lblMoveToTargetX_Click(object sender, EventArgs e)
        {
            double targetPosition = TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().GetTargetPosition(AxisName.X);
            var movingParam = TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().GetMovingParams(AxisName.X);

            AxisHandler.AxisList[(int)AxisName.X].StartAbsoluteMove(targetPosition, movingParam);
        }

        private void lblMoveToTargetY_Click(object sender, EventArgs e)
        {
            double targetPosition = TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().GetTargetPosition(AxisName.Y);
            var movingParam = TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().GetMovingParams(AxisName.Y);

            AxisHandler.AxisList[(int)AxisName.Y].StartAbsoluteMove(targetPosition, movingParam);
        }

        private void lblMoveToTargetZ_Click(object sender, EventArgs e)
        {
            double targetPosition = TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().GetTargetPosition(AxisName.Z);

            double mPos_um = 0.0;
            if (LAFCtrl is NuriOneLAFCtrl nuriOne)
                mPos_um = LAFCtrl.Status.MPosPulse / nuriOne.ResolutionAxisZ;
            else
                mPos_um = LAFCtrl.Status.MPosPulse;

            double currentPosition = mPos_um;

            Direction direction = Direction.CCW;
            double moveAmount = targetPosition - currentPosition;
            if (moveAmount < 0)
                direction = Direction.CW;
            else
                direction = Direction.CCW;

            LAFCtrl.SetMotionRelativeMove(direction, Math.Abs(moveAmount));
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
        #endregion
    }
}
