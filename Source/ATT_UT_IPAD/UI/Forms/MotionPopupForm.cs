﻿using ATT_UT_IPAD.UI.Controls;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform;
using Jastech.Framework.Config;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Structure;
using Jastech.Framework.Structure.Service;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform.Controls;
using Jastech.Framework.Winform.Forms;
using Jastech.Framework.Winform.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static Jastech.Framework.Device.Motions.AxisMovingParam;

namespace ATT_UT_IPAD.UI.Forms
{
    public partial class MotionPopupForm : Form
    {
        #region 필드
        private Color _selectedColor;

        private Color _nonSelectedColor;

        private Point _mousePoint;
        #endregion

        #region 속성
        public InspModelService InspModelService { get; set; } = null;

        private TeachingPositionListControl TeachingPositionListControl { get; set; } = null;

        private List<TeachingInfo> TeachingPositionList { get; set; } = null;

        public AxisHandler AxisHandler { get; set; } = null;

        public LAFCtrl AkkonLafCtrl { get; set; } = null;

        public LAFCtrl AlignLafCtrl { get; set; } = null;

        private MotionJogXControl MotionJogXControl { get; set; } = null;

        private LAFJogControl LAFJogZ0Control { get; set; } = null;

        private LAFJogControl LAFJogZ1Control { get; set; } = null;

        private MotionParameterVariableControl XVariableControl = null;

        //private MotionParameterVariableControl Z0VariableControl = null;

        //private MotionParameterVariableControl Z1VariableControl = null;

        public TeachingPosType TeachingPositionType = TeachingPosType.Standby;

        public UnitName UnitName { get; set; } = UnitName.Unit0;

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

        #region 이벤트
        public Action CloseEventDelegate;
        #endregion

        #region 델리게이트
        private delegate void UpdateStatusDelegate();
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
            AddControl();
            UpdateData();
            InitializeUI();
            SetDefaultValue();

            StatusTimer.Start();
        }

        private void SetDefaultValue()
        {
            if (MotionJogXControl != null)
            {
                MotionJogXControl.JogMode = JogMode.Jog;
                MotionJogXControl.JogSpeedMode = JogSpeedMode.Slow;
                MotionJogXControl.JogPitch = Convert.ToDouble(lblPitchXYValue.Text);
            }

            if (LAFJogZ0Control != null)
            {
                LAFJogZ0Control.JogMode = JogMode.Jog;
                LAFJogZ0Control.JogSpeedMode = JogSpeedMode.Slow;
                LAFJogZ0Control.MoveAmount = Convert.ToDouble(lblPitchZValue.Text);
            }

            if (LAFJogZ1Control != null)
            {
                LAFJogZ1Control.JogMode = JogMode.Jog;
                LAFJogZ1Control.JogSpeedMode = JogSpeedMode.Slow;
                LAFJogZ1Control.MoveAmount = Convert.ToDouble(lblPitchZValue.Text);
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
            TeachingPositionListControl = new TeachingPositionListControl();
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
            var posData = TeachingData.Instance().GetUnit(UnitName.ToString()).GetTeachingInfoList();
            SetTeachingPosition(posData);

            UpdateParam();
        }

        private void UpdateParam(TeachingPosType teachingPositionType = TeachingPosType.Standby)
        {
            var param = TeachingPositionList.Where(x => x.Name == teachingPositionType.ToString()).First();
            if (param == null)
                return;

            lblTargetPositionX.Text = param.GetTargetPosition(AxisName.X).ToString("F3");
            lblOffsetX.Text = param.GetOffset(AxisName.X).ToString();
            XVariableControl.UpdateData(param.GetMovingParams(AxisName.X));

            lblTargetPositionZ0.Text = param.GetTargetPosition(AxisName.Z0).ToString("F3");
            lblTeachedCenterOfGravityZ0.Text = param.GetCenterOfGravity(AxisName.Z0).ToString();
            //Z0VariableControl.UpdateData(param.GetMovingParams(AxisName.Z0));

            lblTargetPositionZ1.Text = param.GetTargetPosition(AxisName.Z1).ToString("F3");
            lblTeachedCenterOfGravityZ1.Text = param.GetCenterOfGravity(AxisName.Z1).ToString();
            //Z1VariableControl.UpdateData(param.GetMovingParams(AxisName.Z1));
        }

        public void SetTeachingPosition(List<TeachingInfo> teacingPositionList)
        {
            TeachingPositionList = teacingPositionList;
        }

        private void AddVariableControl()
        {
            XVariableControl = new MotionParameterVariableControl();
            XVariableControl.Dock = DockStyle.Fill;
            XVariableControl.SetAxis(AxisHandler.GetAxis(AxisName.X));
            tlpVariableParameter.Controls.Add(XVariableControl);

            //Z0VariableControl = new MotionParameterVariableControl();
            //Z0VariableControl.Dock = DockStyle.Fill;
            //Z0VariableControl.SetAxis(AxisHandler.GetAxis(AxisName.Z0));
            //tlpVariableParameter.Controls.Add(Z0VariableControl);

            //Z1VariableControl = new MotionParameterVariableControl();
            //Z1VariableControl.Dock = DockStyle.Fill;
            //Z1VariableControl.SetAxis(AxisHandler.GetAxis(AxisName.Z1));
            //tlpVariableParameter.Controls.Add(Z1VariableControl);
        }

        private void AddJogControl()
        {
            MotionJogXControl = new MotionJogXControl();
            MotionJogXControl.Dock = DockStyle.Fill;
            pnlMotionJog.Controls.Add(MotionJogXControl);
            MotionJogXControl.SetAxisHandler(AxisHandler);

            LAFJogZ0Control = new LAFJogControl();
            LAFJogZ0Control.Dock = DockStyle.Fill;
            pnlLAFZ1Jog.Controls.Add(LAFJogZ0Control);
            LAFJogZ0Control.SetSelectedLafCtrl(AkkonLafCtrl);

            LAFJogZ1Control = new LAFJogControl();
            LAFJogZ1Control.Dock = DockStyle.Fill;
            pnlLAFZ2Jog.Controls.Add(LAFJogZ1Control);
            LAFJogZ1Control.SetSelectedLafCtrl(AlignLafCtrl);
        }

        private void UpdateStatusMotionX()
        {
            var axis = AxisHandler.GetAxis(AxisName.X);

            if (axis == null || !axis.IsConnected())
                return;

            lblCurrentPositionX.Text = axis.GetActualPosition().ToString("F3");

            if (axis.IsNegativeLimit())
            {
                lblSensorX.BackColor = Color.Red;
                lblSensorX.Text = "-";
            }
            else if (axis.IsPositiveLimit())
            {
                lblSensorX.BackColor = Color.Red;
                lblSensorX.Text = "+";
            }
            else
            {
                lblSensorX.BackColor = _nonSelectedColor;
                lblSensorX.Text = "Done";
            }

            lblAxisStatusX.Text = axis.GetCurrentMotionStatus().ToString();

            if (axis.IsEnable())
            {
                lblServoOnX.BackColor = _selectedColor;
                lblServoOffX.BackColor = _nonSelectedColor;
            }
            else
            {
                lblServoOnX.BackColor = _nonSelectedColor;
                lblServoOffX.BackColor = _selectedColor;
            }
        }

        private void UpdateStatusAutoFocusZ0()
        {
            var status = AkkonLafCtrl.Status;

            if (status == null)
                return;

            double mPos_um = 0.0;
            if (AkkonLafCtrl is NuriOneLAFCtrl nuriOne)
                mPos_um = status.MPosPulse / nuriOne.ResolutionAxisZ;
            else
                mPos_um = status.MPosPulse;

            lblCurrentPositionZ0.Text = mPos_um.ToString("F3");
            lblCurrentCenterOfGravityZ0.Text = status.CenterofGravity.ToString();

            if (status.IsNegativeLimit)
            {
                lblSensorZ0.BackColor = Color.Red;
                lblSensorZ0.Text = "-";
            }
            else if (status.IsPositiveLimit)
            {
                lblSensorZ0.BackColor = Color.Red;
                lblSensorZ0.Text = "+";
            }
            else
            {
                lblSensorZ0.BackColor = _nonSelectedColor;
                lblSensorZ0.Text = "Done";
            }

            if (status.IsLaserOn)
            {
                lblLaserOnZ0.BackColor = _selectedColor;
                lblLaserOffZ0.BackColor = _nonSelectedColor;
            }
            else
            {
                lblLaserOnZ0.BackColor = _nonSelectedColor;
                lblLaserOffZ0.BackColor = _selectedColor;
            }

            if (status.IsTrackingOn)
            {
                lblTrackingOnZ0.BackColor = _selectedColor;
                lblTrackingOffZ0.BackColor = _nonSelectedColor;
            }
            else
            {
                lblTrackingOnZ0.BackColor = _nonSelectedColor;
                lblTrackingOffZ0.BackColor = _selectedColor;
            }
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
            {
                lblSensorZ1.BackColor = Color.Red;
                lblSensorZ1.Text = "-";
            }
            else if (status.IsPositiveLimit)
            {
                lblSensorZ1.BackColor = Color.Red;
                lblSensorZ1.Text = "+";
            }
            else
            {
                lblSensorZ1.BackColor = _nonSelectedColor;
                lblSensorZ1.Text = "Done";
            }

            if (status.IsLaserOn)
            {
                lblLaserOnZ1.BackColor = _selectedColor;
                lblLaserOffZ1.BackColor = _nonSelectedColor;
            }
            else
            {
                lblLaserOnZ1.BackColor = _nonSelectedColor;
                lblLaserOffZ1.BackColor = _selectedColor;
            }

            if (status.IsTrackingOn)
            {
                lblTrackingOnZ1.BackColor = _selectedColor;
                lblTrackingOffZ1.BackColor = _nonSelectedColor;
            }
            else
            {
                lblTrackingOnZ1.BackColor = _nonSelectedColor;
                lblTrackingOffZ1.BackColor = _selectedColor;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Logger.Write(LogType.GUI, "Clicked MotionPopupForm Close Button");
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
            Logger.Write(LogType.GUI, "Clicked MotionPopupForm Save Button");
        }

        private void UpdateCurrentData()
        {
            GetCurrentVariableParams();
        }

        private void SetCenterOfGravity()
        {
            int akkonCog = TeachingPositionList.Where(x => x.Name == TeachingPosType.Stage1_Scan_Start.ToString()).First().GetCenterOfGravity(AxisName.Z0);
            if (LAFManager.Instance().GetLAF(AkkonLafCtrl.Name) is LAF akkonLAF)
                akkonLAF.SetCenterOfGravity(akkonCog);

            int alignCog = TeachingPositionList.Where(x => x.Name == TeachingPosType.Stage1_Scan_Start.ToString()).First().GetCenterOfGravity(AxisName.Z1);
            if (LAFManager.Instance().GetLAF(AlignLafCtrl.Name) is LAF alignLAF)
                alignLAF.SetCenterOfGravity(alignCog);
        }

        private void GetCurrentVariableParams()
        {
            var posData = TeachingData.Instance().GetUnit(UnitName.ToString()).GetTeachingInfo(TeachingPositionType);

            posData.SetMovingParams(AxisName.X, XVariableControl.GetCurrentData());
            //posData.SetMovingParams(AxisName.Z0, Z0VariableControl.GetCurrentData());
            //posData.SetMovingParams(AxisName.Z1, Z1VariableControl.GetCurrentData());
        }

        private void Save()
        {
            MessageYesNoForm yesNoForm = new MessageYesNoForm();
            yesNoForm.Message = "Teaching data will change.\nDo you agree?";

            if (yesNoForm.ShowDialog() == DialogResult.Yes)
            {
                UpdateCurrentData();
                SetCenterOfGravity();

                // Save AxisHandler
                var axisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
                MotionManager.Instance().Save(axisHandler);

                // Save Model
                var model = ModelManager.Instance().CurrentModel as AppsInspModel;
                //model.SetUnitList(TeachingData.Instance().UnitList);
                model.SetTeachingList(TeachingPositionList);

                string fileName = Path.Combine(ConfigSet.Instance().Path.Model, model.Name, InspModel.FileName);

                InspModelService?.Save(fileName, model);

                XVariableControl?.WriteChangeLog();

                if (ParamTrackingLogger.IsEmpty == false)
                {
                    ParamTrackingLogger.AddLog("Motion Parameter saved.");
                    ParamTrackingLogger.WriteLogToFile();
                }

                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "Save Motion Data Completed.";
                form.ShowDialog();
            }
        }

        private void lblTargetPositionX_Click(object sender, EventArgs e)
        {
            Label label = sender as Label;
            double oldPosition = Convert.ToDouble(label.Text);
            double newPosition = KeyPadHelper.SetLabelDoubleData(label);

            TeachingPositionList.First(x => x.Name == TeachingPositionType.ToString()).SetTargetPosition(AxisName.X, newPosition);

            ParamTrackingLogger.AddChangeHistory($"{AxisName.X}", "TargetPos", oldPosition, newPosition);
        }

        private void lblOffsetX_Click(object sender, EventArgs e)
        {
            Label label = sender as Label;
            double oldOffset = Convert.ToDouble(label.Text);
            double newOffset = KeyPadHelper.SetLabelDoubleData(label);

            TeachingPositionList.First(x => x.Name == TeachingPositionType.ToString()).SetOffset(AxisName.X, newOffset);

            ParamTrackingLogger.AddChangeHistory($"{AxisName.X}", "Offset", oldOffset, newOffset);
        }

        private void lblCurrentToTargetX_Click(object sender, EventArgs e)
        {
            double oldPosition = Convert.ToDouble(lblTargetPositionX.Text);
            double newPosition = Convert.ToDouble(lblCurrentPositionX.Text);

            TeachingPositionList.First(x => x.Name == TeachingPositionType.ToString()).SetTargetPosition(AxisName.X, newPosition);

            lblTargetPositionX.Text = newPosition.ToString("F3");
            ParamTrackingLogger.AddChangeHistory($"{AxisName.X}", "TargetPos", oldPosition, newPosition);
        }

        private void lblMoveToTargetX_Click(object sender, EventArgs e)
        {
            double targetPosition = TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().GetTargetPosition(AxisName.X);
            var movingParam = TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().GetMovingParams(AxisName.X);

            AxisHandler.GetAxis(AxisName.X).StartAbsoluteMove(targetPosition, movingParam);
        }

        private void lblOriginX_Click(object sender, EventArgs e)
        {
            AxisHandler.GetAxis(AxisName.X).StartHome();
        }

        private void lblServoOnX_Click(object sender, EventArgs e)
        {
            AxisHandler.GetAxis(AxisName.X).TurnOnServo();
        }

        private void lblServoOffX_Click(object sender, EventArgs e)
        {
            AxisHandler.GetAxis(AxisName.X).TurnOffServo();
        }

        private void lblTargetPositionZ0_Click(object sender, EventArgs e)
        {
            Label label = sender as Label;
            double oldPosition = Convert.ToDouble(label.Text);
            double newPosition = KeyPadHelper.SetLabelDoubleData(label);

            TeachingPositionList.First(x => x.Name == TeachingPositionType.ToString()).SetTargetPosition(AxisName.Z0, newPosition);

            ParamTrackingLogger.AddChangeHistory($"{AxisName.Z0}", "TargetPos", oldPosition, newPosition);
        }

        private void lblCurrentToTargetZ0_Click(object sender, EventArgs e)
        {
            double oldPosition = Convert.ToDouble(lblTargetPositionZ0.Text);
            double newPosition = Convert.ToDouble(lblCurrentPositionZ0.Text);

            TeachingPositionList.First(x => x.Name == TeachingPositionType.ToString()).SetTargetPosition(AxisName.Z0, newPosition);

            lblTargetPositionZ0.Text = newPosition.ToString("F3");
            ParamTrackingLogger.AddChangeHistory($"{AxisName.Z0}", "TargetPos", oldPosition, newPosition);
        }

        private void lblTeachedCenterOfGravityZ0_Click(object sender, EventArgs e)
        {
            Label label = sender as Label;
            int oldCenterOfGravity = Convert.ToInt32(label.Text);
            int newCenterOfGravity = KeyPadHelper.SetLabelIntegerData(label);

            TeachingPositionList.First(x => x.Name == TeachingPositionType.ToString()).SetCenterOfGravity(AxisName.Z0, newCenterOfGravity);

            if (LAFManager.Instance().GetLAF(AkkonLafCtrl.Name) is LAF akkonLAF)
                akkonLAF.SetCenterOfGravity(newCenterOfGravity);

            ParamTrackingLogger.AddChangeHistory($"{AxisName.Z0}", "CoG", oldCenterOfGravity, newCenterOfGravity);
        }

        private void lblCurrentToTargetCenterOfGravityZ0_Click(object sender, EventArgs e)
        {
            int oldCenterOfGravity = Convert.ToInt32(lblTeachedCenterOfGravityZ0.Text);
            int newCenterOfGravity = Convert.ToInt32(lblCurrentCenterOfGravityZ0.Text);

            TeachingPositionList.First(x => x.Name == TeachingPositionType.ToString()).SetCenterOfGravity(AxisName.Z0, newCenterOfGravity);

            if (LAFManager.Instance().GetLAF(AkkonLafCtrl.Name) is LAF akkonLAF)
                akkonLAF.SetCenterOfGravity(newCenterOfGravity);

            lblTeachedCenterOfGravityZ0.Text = newCenterOfGravity.ToString();
            ParamTrackingLogger.AddChangeHistory($"{AxisName.Z0}", "CoG", oldCenterOfGravity, newCenterOfGravity);
        }

        private void lblMoveToTargetZ0_Click(object sender, EventArgs e)
        {
            double targetPosition = TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().GetTargetPosition(AxisName.Z0);

            double mPos_um = 0.0;
            if (AkkonLafCtrl is NuriOneLAFCtrl nuriOne)
                mPos_um = AkkonLafCtrl.Status.MPosPulse / nuriOne.ResolutionAxisZ;
            else
                mPos_um = AkkonLafCtrl.Status.MPosPulse;

            double currentPosition = mPos_um;

            Direction direction = Direction.CCW;
            double moveAmount = targetPosition - currentPosition;
            if (moveAmount < 0)
                direction = Direction.CW;
            else
                direction = Direction.CCW;
            
            AkkonLafCtrl.SetMotionRelativeMove(direction, Math.Abs(moveAmount));
        }

        private void lblOriginZ0_Click(object sender, EventArgs e)
        {
            var unit = TeachingData.Instance().GetUnit(UnitName.ToString());
            if (unit != null)
            {
                if (LAFManager.Instance().GetLAF(AkkonLafCtrl.Name) is LAF akkonLAF)
                {
                    double standbyPosition = unit.GetTeachingInfo(TeachingPosType.Stage1_Scan_Start).GetTargetPosition(AkkonLafCtrl.AxisName);

                    ProgressForm progressForm = new ProgressForm();
                    akkonLAF.SetHomeStandbyPosition(standbyPosition);
                    progressForm.Add($"Axis Z0 ({AkkonLafCtrl.Name}) homing", akkonLAF.HomeSequenceAction, akkonLAF.StopHomeSequence);
                    progressForm.ShowDialog();
                }
            }
        }

        private void lblLaserOnZ0_Click(object sender, EventArgs e)
        {
            if (LAFManager.Instance().GetLAF(AkkonLafCtrl.Name) is LAF akkonLAF)
                akkonLAF.LaserOnOff(true);
        }

        private void lblLaserOffZ0_Click(object sender, EventArgs e)
        {
            if (LAFManager.Instance().GetLAF(AkkonLafCtrl.Name) is LAF akkonLAF)
                akkonLAF.LaserOnOff(false);
        }

        private void lblTrackingOnZ0_Click(object sender, EventArgs e)
        {
            if (LAFManager.Instance().GetLAF(AkkonLafCtrl.Name) is LAF akkonLAF)
                akkonLAF.TrackingOnOff(true);
        }

        private void lblTrackingOffZ0_Click(object sender, EventArgs e)
        {
            if (LAFManager.Instance().GetLAF(AkkonLafCtrl.Name) is LAF akkonLAF)
                akkonLAF.TrackingOnOff(false);
        }

        private void lblTargetPositionZ1_Click(object sender, EventArgs e)
        {
            Label label = sender as Label;
            double oldPosition = Convert.ToDouble(label.Text);
            double newPosition = KeyPadHelper.SetLabelDoubleData(label);

            TeachingPositionList.First(x => x.Name == TeachingPositionType.ToString()).SetTargetPosition(AxisName.Z1, newPosition);

            ParamTrackingLogger.AddChangeHistory($"{AxisName.Z1}", "TargetPos", oldPosition, newPosition);
        }

        private void lblCurrentToTargetZ1_Click(object sender, EventArgs e)
        {
            double oldPosition = Convert.ToDouble(lblTargetPositionZ1.Text);
            double newPosition = Convert.ToDouble(lblCurrentPositionZ1.Text);

            TeachingPositionList.First(x => x.Name == TeachingPositionType.ToString()).SetTargetPosition(AxisName.Z1, newPosition);

            lblTargetPositionZ1.Text = newPosition.ToString("F3");
            ParamTrackingLogger.AddChangeHistory($"{AxisName.Z1}", "TargetPos", oldPosition, newPosition);
        }

        private void lblTeachedCenterOfGravityZ1_Click(object sender, EventArgs e)
        {
            Label label = sender as Label;
            int oldCenterOfGravity = Convert.ToInt32(label.Text);
            int newCenterOfGravity = KeyPadHelper.SetLabelIntegerData(label);

            TeachingPositionList.First(x => x.Name == TeachingPositionType.ToString()).SetCenterOfGravity(AxisName.Z1, newCenterOfGravity);

            if (LAFManager.Instance().GetLAF(AlignLafCtrl.Name) is LAF alignLAF)
                alignLAF.SetCenterOfGravity(newCenterOfGravity);

            ParamTrackingLogger.AddChangeHistory($"{AxisName.Z1}", "Cog", oldCenterOfGravity, newCenterOfGravity);
        }

        private void lblCurrentToTargetCenterOfGravityZ1_Click(object sender, EventArgs e)
        {
            int oldCenterOfGravity = Convert.ToInt32(lblTeachedCenterOfGravityZ1.Text);
            int newCenterOfGravity = Convert.ToInt32(lblCurrentCenterOfGravityZ1.Text);

            TeachingPositionList.First(x => x.Name == TeachingPositionType.ToString()).SetCenterOfGravity(AxisName.Z1, newCenterOfGravity);

            if (LAFManager.Instance().GetLAF(AlignLafCtrl.Name) is LAF alignLAF)
                alignLAF.SetCenterOfGravity(newCenterOfGravity);

            lblTeachedCenterOfGravityZ1.Text = newCenterOfGravity.ToString();
            ParamTrackingLogger.AddChangeHistory($"{AxisName.Z1}", "CoG", oldCenterOfGravity, newCenterOfGravity);
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
            var unit = TeachingData.Instance().GetUnit(UnitName.ToString());
            if (unit != null)
            {
                if (LAFManager.Instance().GetLAF(AlignLafCtrl.Name) is LAF alignLAF)
                {
                    double standbyPosition = unit.GetTeachingInfo(TeachingPosType.Stage1_Scan_Start).GetTargetPosition(AlignLafCtrl.AxisName);

                    ProgressForm progressForm = new ProgressForm();
                    alignLAF.SetHomeStandbyPosition(standbyPosition);
                    progressForm.Add($"Axis Z1 ({AlignLafCtrl.Name}) homing", alignLAF.HomeSequenceAction, alignLAF.StopHomeSequence);
                    progressForm.ShowDialog();
                }
            }
        }

        private void lblLaserOnZ1_Click(object sender, EventArgs e)
        {
            if (LAFManager.Instance().GetLAF(AlignLafCtrl.Name) is LAF alignLAF)
                alignLAF.LaserOnOff(true);
        }

        private void lblLaserOffZ1_Click(object sender, EventArgs e)
        {
            if (LAFManager.Instance().GetLAF(AlignLafCtrl.Name) is LAF alignLAF)
                alignLAF.LaserOnOff(false);
        }

        private void lblTrackingOnZ1_Click(object sender, EventArgs e)
        {
            if (LAFManager.Instance().GetLAF(AlignLafCtrl.Name) is LAF alignLAF)
                alignLAF.TrackingOnOff(true);
        }

        private void lblTrackingOffZ1_Click(object sender, EventArgs e)
        {
            if (LAFManager.Instance().GetLAF(AlignLafCtrl.Name) is LAF alignLAF)
                alignLAF.TrackingOnOff(false);
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
            LAFJogZ0Control.JogSpeedMode = jogSpeedMode;
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
            LAFJogZ0Control.JogMode = jogMode;
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
            LAFJogZ0Control.MoveAmount = pitchZ;
            LAFJogZ1Control.MoveAmount = pitchZ;
        }

        private void MotionPopupForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StatusTimer.Stop();
        }

        private void MotionPopupForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ParamTrackingLogger.ClearChangedLog();

            if (CloseEventDelegate != null)
                CloseEventDelegate();
        }

        private void StatusTimer_Tick(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            if (this.InvokeRequired)
            {
                UpdateStatusDelegate callback = UpdateStatus;
                BeginInvoke(callback);
                return;
            }

            UpdateStatusMotionX();
            UpdateStatusAutoFocusZ0();
            UpdateStatusAutoFocusZ1();
        }

        private void pnlTop_MouseDown(object sender, MouseEventArgs e)
        {
            _mousePoint = new Point(e.X, e.Y);
        }

        private void pnlTop_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                Location = new Point(this.Left - (_mousePoint.X - e.X), this.Top - (_mousePoint.Y - e.Y));
        }
        #endregion
    }
}
