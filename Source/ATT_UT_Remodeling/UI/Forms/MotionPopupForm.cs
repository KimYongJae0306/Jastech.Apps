using ATT_UT_Remodeling.UI.Controls;
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
using System.Linq;
using System.Windows.Forms;
using static Jastech.Framework.Device.Motions.AxisMovingParam;

namespace ATT_UT_Remodeling.UI.Forms
{
    public partial class MotionPopupForm : Form
    {
        #region 필드
        private System.Threading.Timer _formTimer = null;

        private Color _selectedColor;

        private Color _nonSelectedColor;

        private Point _mousePoint;
        #endregion

        #region 속성
        public InspModelService InspModelService { get; set; } = null;

        private TeachingPositionListControl TeachingPositionListControl { get; set; } = null;

        private List<TeachingInfo> TeachingPositionList { get; set; } = null;

        public AxisHandler AxisHandler { get; set; } = null;

        public LAFCtrl LafCtrl { get; set; } = null;

        private MotionJogXControl MotionJogXControl { get; set; } = null;

        private LAFJogControl LAFJogZControl { get; set; } = null;

        private MotionParameterVariableControl XVariableControl = null;

        //private MotionParameterVariableControl ZVariableControl = null;

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

            if (LAFJogZControl != null)
            {
                LAFJogZControl.JogMode = JogMode.Jog;
                LAFJogZControl.JogSpeedMode = JogSpeedMode.Slow;
                LAFJogZControl.MoveAmount = Convert.ToDouble(lblPitchZValue.Text);
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
            var param = TeachingPositionList.Where(x => x.Name == teachingPositionType.ToString()).FirstOrDefault();
            if (param == null)
                return;

            lblTargetPositionX.Text = param.GetTargetPosition(AxisName.X).ToString();
            lblOffsetX.Text = param.GetOffset(AxisName.X).ToString();
            XVariableControl.UpdateData(param.GetMovingParams(AxisName.X));

            lblTargetPositionZ.Text = param.GetTargetPosition(AxisName.Z0).ToString();
            lblTeachedCenterOfGravityZ.Text = param.GetCenterOfGravity(AxisName.Z0).ToString();
            //ZVariableControl.UpdateData(param.GetMovingParams(AxisName.Z0));
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

            //ZVariableControl = new MotionParameterVariableControl();
            //ZVariableControl.Dock = DockStyle.Fill;
            //ZVariableControl.SetAxis(AxisHandler.GetAxis(AxisName.Z0));
            //tlpVariableParameter.Controls.Add(ZVariableControl);
        }

        private void AddJogControl()
        {
            MotionJogXControl = new MotionJogXControl();
            MotionJogXControl.Dock = DockStyle.Fill;
            pnlMotionJog.Controls.Add(MotionJogXControl);
            MotionJogXControl.SetAxisHandler(AxisHandler);

            LAFJogZControl = new LAFJogControl();
            LAFJogZControl.Dock = DockStyle.Fill;
            pnlLAFZ1Jog.Controls.Add(LAFJogZControl);
            LAFJogZControl.SetSelectedLafCtrl(LafCtrl);
        }

        //private void UpdateStatus(object obj)
        //{
        //    if (this.InvokeRequired)
        //    {
        //        UpdateStatusDelegate callback = UpdateStatus;
        //        BeginInvoke(callback, obj);
        //        return;
        //    }

        //    UpdateStatusMotionX();
        //    UpdateStatusAutoFocusZ();
        //}

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

        private void UpdateStatusAutoFocusZ()
        {
            var status = LafCtrl.Status;

            if (status == null)
                return;

            double mPos_um = 0.0;
            if (LafCtrl is NuriOneLAFCtrl nuriOne)
                mPos_um = status.MPosPulse / nuriOne.ResolutionAxisZ;
            else
                mPos_um = status.MPosPulse;

            lblCurrentPositionZ.Text = mPos_um.ToString("F3");
            lblCurrentCenterOfGravityZ.Text = status.CenterofGravity.ToString();

            if (status.IsNegativeLimit)
            {
                lblSensorZ.BackColor = Color.Red;
                lblSensorZ.Text = "-";
            }
            else if (status.IsPositiveLimit)
            {
                lblSensorZ.BackColor = Color.Red;
                lblSensorZ.Text = "+";
            }
            else
            {
                lblSensorZ.BackColor = _nonSelectedColor;
                lblSensorZ.Text = "Done";
            }

            if (status.IsLaserOn)
            {
                lblLaserOnZ.BackColor = _selectedColor;
                lblLaserOffZ.BackColor = _nonSelectedColor;
            }
            else
            {
                lblLaserOnZ.BackColor = _nonSelectedColor;
                lblLaserOffZ.BackColor = _selectedColor;
            }

            if (status.IsTrackingOn)
            {
                lblTrackingOnZ.BackColor = _selectedColor;
                lblTrackingOffZ.BackColor = _nonSelectedColor;
            }
            else
            {
                lblTrackingOnZ.BackColor = _nonSelectedColor;
                lblTrackingOffZ.BackColor = _selectedColor;
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
            //posData.SetMovingParams(AxisName.Z0, ZVariableControl.GetCurrentData());
        }

        private void SetCenterOfGravity()
        {
            int Cog = TeachingPositionList.First(x => x.Name == TeachingPositionType.ToString()).GetCenterOfGravity(AxisName.Z0);

            if (LAFManager.Instance().GetLAF(LafCtrl.Name) is LAF alignLAF)
                alignLAF.SetCenterOfGravity(Cog);
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

                string fileName = System.IO.Path.Combine(ConfigSet.Instance().Path.Model, model.Name, InspModel.FileName);

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
            //double targetPosition = KeyPadHelper.SetLabelDoubleData((Label)sender);
            //TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetTargetPosition(AxisName.X, targetPosition);

            Label label = sender as Label;
            double oldPosition = Convert.ToDouble(label.Text);
            double newPosition = KeyPadHelper.SetLabelDoubleData(label);

            TeachingPositionList.First(x => x.Name == TeachingPositionType.ToString()).SetTargetPosition(AxisName.X, newPosition);

            ParamTrackingLogger.AddChangeHistory($"{AxisName.X}", "TargetPos", oldPosition, newPosition);
        }

        private void lblOffsetX_Click(object sender, EventArgs e)
        {
            //double offset = KeyPadHelper.SetLabelDoubleData((Label)sender);
            //TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetOffset(AxisName.X, offset);

            Label label = sender as Label;
            double oldOffset = Convert.ToDouble(label.Text);
            double newOffset = KeyPadHelper.SetLabelDoubleData(label);

            TeachingPositionList.First(x => x.Name == TeachingPositionType.ToString()).SetOffset(AxisName.X, newOffset);

            ParamTrackingLogger.AddChangeHistory($"{AxisName.X}", "Offset", oldOffset, newOffset);
        }

        private void lblCurrentToTargetX_Click(object sender, EventArgs e)
        {
            //double currentPosition = Convert.ToDouble(lblCurrentPositionX.Text);
            //TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetTargetPosition(AxisName.X, currentPosition);

            //lblTargetPositionX.Text = currentPosition.ToString("F3");

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

        private void lblTargetPositionZ_Click(object sender, EventArgs e)
        {
            //double targetPosition = KeyPadHelper.SetLabelDoubleData((Label)sender);
            //TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetTargetPosition(AxisName.Z0, targetPosition);

            Label label = sender as Label;
            double oldPosition = Convert.ToDouble(label.Text);
            double newPosition = KeyPadHelper.SetLabelDoubleData(label);

            TeachingPositionList.First(x => x.Name == TeachingPositionType.ToString()).SetTargetPosition(AxisName.Z0, newPosition);

            ParamTrackingLogger.AddChangeHistory($"{AxisName.Z0}", "TargetPos", oldPosition, newPosition);
        }

        private void lblCurrentToTargetZ_Click(object sender, EventArgs e)
        {
            //double currentPosition = Convert.ToDouble(lblCurrentPositionZ.Text);
            //TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetTargetPosition(AxisName.Z0, currentPosition);

            //lblTargetPositionZ.Text = currentPosition.ToString("F3");

            double oldPosition = Convert.ToDouble(lblTargetPositionZ.Text);
            double newPosition = Convert.ToDouble(lblCurrentPositionZ.Text);

            TeachingPositionList.First(x => x.Name == TeachingPositionType.ToString()).SetTargetPosition(AxisName.Z0, newPosition);

            lblTargetPositionZ.Text = newPosition.ToString("F3");
            ParamTrackingLogger.AddChangeHistory($"{AxisName.Z0}", "TargetPos", oldPosition, newPosition);
        }

        private void lblTeachedCenterOfGravityZ_Click(object sender, EventArgs e)
        {
            //int centerOfGravity = KeyPadHelper.SetLabelIntegerData((Label)sender);
            //TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetCenterOfGravity(AxisName.Z0, centerOfGravity);

            Label label = sender as Label;
            int oldCenterOfGravity = Convert.ToInt32(label.Text);
            int newCenterOfGravity = KeyPadHelper.SetLabelIntegerData(label);

            TeachingPositionList.First(x => x.Name == TeachingPositionType.ToString()).SetCenterOfGravity(AxisName.Z0, newCenterOfGravity);

            if (LAFManager.Instance().GetLAF(LafCtrl.Name) is LAF alignLAF)
                alignLAF.SetCenterOfGravity(newCenterOfGravity);

            ParamTrackingLogger.AddChangeHistory($"{AxisName.Z0}", "Cog", oldCenterOfGravity, newCenterOfGravity);
        }

        private void lblCurrentToTargetCenterOfGravityZ_Click(object sender, EventArgs e)
        {
            //int targetCenterOfGravity = Convert.ToInt32(lblCurrentCenterOfGravityZ.Text);
            //TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetCenterOfGravity(AxisName.Z0, targetCenterOfGravity);

            //lblTeachedCenterOfGravityZ.Text = targetCenterOfGravity.ToString();

            int oldCenterOfGravity = Convert.ToInt32(lblTeachedCenterOfGravityZ.Text);
            int newCenterOfGravity = Convert.ToInt32(lblCurrentCenterOfGravityZ.Text);

            TeachingPositionList.First(x => x.Name == TeachingPositionType.ToString()).SetCenterOfGravity(AxisName.Z0, newCenterOfGravity);

            if (LAFManager.Instance().GetLAF(LafCtrl.Name) is LAF alignLAF)
                alignLAF.SetCenterOfGravity(newCenterOfGravity);

            lblTeachedCenterOfGravityZ.Text = newCenterOfGravity.ToString();
            ParamTrackingLogger.AddChangeHistory($"{AxisName.Z0}", "CoG", oldCenterOfGravity, newCenterOfGravity);
        }

        private void lblMoveToTargetZ_Click(object sender, EventArgs e)
        {
            double targetPosition = TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().GetTargetPosition(AxisName.Z0);

            double mPos_um = 0.0;
            if (LafCtrl is NuriOneLAFCtrl nuriOne)
                mPos_um = LafCtrl.Status.MPosPulse / nuriOne.ResolutionAxisZ;
            else
                mPos_um = LafCtrl.Status.MPosPulse;

            double currentPosition = mPos_um;

            Direction direction = Direction.CCW;
            double moveAmount = targetPosition - currentPosition;
            if (moveAmount < 0)
                direction = Direction.CW;
            else
                direction = Direction.CCW;

            LafCtrl.SetMotionRelativeMove(direction, Math.Abs(moveAmount));
        }

        private void lblOriginZ_Click(object sender, EventArgs e)
        {
            LAFManager.Instance().GetLAF(LafCtrl.Name).StartHomeThread();
        }

        private void lblLaserOnZ_Click(object sender, EventArgs e)
        {
            LAFManager.Instance().GetLAF(LafCtrl.Name).LaserOnOff(true);
        }

        private void lblLaserOffZ_Click(object sender, EventArgs e)
        {
            LAFManager.Instance().GetLAF(LafCtrl.Name).LaserOnOff(false);
        }

        private void lblTrackingOnZ_Click(object sender, EventArgs e)
        {
            LAFManager.Instance().GetLAF(LafCtrl.Name).TrackingOnOff(true);
        }

        private void lblTrackingOffZ_Click(object sender, EventArgs e)
        {
            LAFManager.Instance().GetLAF(LafCtrl.Name).TrackingOnOff(false);
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
            LAFJogZControl.JogSpeedMode = jogSpeedMode;
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
            LAFJogZControl.JogMode = jogMode;
        }

        private void lblPitchXYValue_Click(object sender, EventArgs e)
        {
            double pitchXY = KeyPadHelper.SetLabelDoubleData((Label)sender);
            MotionJogXControl.JogPitch = pitchXY;
        }

        private void lblPitchZValue_Click(object sender, EventArgs e)
        {
            double pitchZ = KeyPadHelper.SetLabelDoubleData((Label)sender);
            LAFJogZControl.MoveAmount = pitchZ;
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
            UpdateStatusAutoFocusZ();
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
