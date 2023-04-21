using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Framework.Winform.Controls;
using Jastech.Framework.Device.Motions;
using Jastech.Apps.Winform;
using Jastech.Apps.Structure;
using System.Reflection;
using ATT.UI.Controls;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Structure;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Framework.Winform;
using static Jastech.Framework.Device.Motions.AxisMovingParam;
using Jastech.Framework.Winform.Forms;
using Jastech.Framework.Device.LAFCtrl;

namespace ATT.UI.Forms
{
    public partial class MotionSettingsForm : Form
    {
        #region 필드
        private System.Threading.Timer _formTimer = null;

        private Color _selectedColor;

        private Color _nonSelectedColor;
        #endregion

        #region 속성
        private TeachingPositionListControl TeachingPositionListControl { get; set; } = new TeachingPositionListControl();

        public string UnitName { get; set; } = string.Empty;

        private List<TeachingPosition> TeachingPositionList { get; set; } = null;

        private AxisHandler AxisHandler { get; set; } = null;

        private LAFCtrl LAFCtrl { get; set; } = null;

        private MotionJogControl MotionJogControl { get; set; } = new MotionJogControl() { Dock = DockStyle.Fill };

        private LAFJogControl LAFJogControl { get; set; } = new LAFJogControl() { Dock = DockStyle.Fill };

        private MotionParameterCommonControl XCommonControl = new MotionParameterCommonControl();

        private MotionParameterCommonControl YCommonControl = new MotionParameterCommonControl();

        private MotionParameterCommonControl ZCommonControl = new MotionParameterCommonControl();

        private MotionParameterVariableControl XVariableControl = new MotionParameterVariableControl();

        private MotionParameterVariableControl YVariableControl = new MotionParameterVariableControl();

        private MotionParameterVariableControl ZVariableControl = new MotionParameterVariableControl();

        public TeachingPositionType TeachingPositionType = TeachingPositionType.Standby;

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
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public MotionSettingsForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void MotionSettingsForm_Load(object sender, EventArgs e)
        {
            SystemManager.Instance().UpdateTeachingData();
            UpdateData();
            AddControl();
            StartTimer();
            InitializeUI();
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            rdoJogSlowMode.Checked = true;
            rdoJogMode.Checked = true;
        }

        private void AddControl()
        {
            AddTeachingPositionListControl();
            AddJogControl();
            AddCommonControl();
            AddVariableControl();
        }

        private void AddTeachingPositionListControl()
        {
            TeachingPositionListControl.Dock = DockStyle.Fill;
            TeachingPositionListControl.UnitName = "0";
            TeachingPositionListControl.SendEventHandler += new TeachingPositionListControl.SetTeachingPositionListDelegate(ReceiveTeachingPosition);
            pnlTeachingPositionList.Controls.Add(TeachingPositionListControl);
        }

        private void ReceiveTeachingPosition(TeachingPositionType teachingPositionType)
        {
            Console.WriteLine(teachingPositionType.ToString());
            TeachingPositionType = teachingPositionType;

            UpdateVariableParam(teachingPositionType);
        }

        private void UpdateData(TeachingPositionType teachingPositionType = TeachingPositionType.Standby)
        {
            var axisHandler = AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Unit0);
            SetAxisHandler(axisHandler);

            string unitName = "0";// TeachingPositionListControl.UnitName;
            var posData = SystemManager.Instance().GetTeachingData().GetUnit(unitName).TeachingPositions;
            SetTeachingPosition(posData);

            var lafCtrl = AppsLAFManager.Instance().GetLAFCtrl(LAFName.Akkon);
            SetLAFCtrl(lafCtrl);

            UpdateCommonParam();
            UpdateVariableParam();
        }

        private void UpdateVariableParam(TeachingPositionType teachingPositionType = TeachingPositionType.Standby)
        {
            var param = TeachingPositionList.Where(x => x.Name == teachingPositionType.ToString()).First();
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

        private void UpdateCommonParam()
        {
            XCommonControl.UpdateData(AxisHandler.GetAxis(AxisName.X).AxisCommonParams.DeepCopy());
            YCommonControl.UpdateData(AxisHandler.GetAxis(AxisName.Y).AxisCommonParams.DeepCopy());
            ZCommonControl.UpdateData(AxisHandler.GetAxis(AxisName.Z).AxisCommonParams.DeepCopy());
        }

        private void SetAxisHandler(AxisHandler axisHandler)
        {
            AxisHandler = axisHandler;
        }

        private void SetTeachingPosition(List<TeachingPosition> teacingPositionList)
        {
            TeachingPositionList = teacingPositionList;
        }

        private void SetLAFCtrl(LAFCtrl lafCtrl)
        {
            LAFCtrl = lafCtrl;
        }

        private void AddJogControl()
        {
            pnlMotionJog.Controls.Add(MotionJogControl);
            MotionJogControl.SetAxisHanlder(AxisHandler);

            pnlLAFJog.Controls.Add(LAFJogControl);
            LAFJogControl.SetSelectedLafCtrl(LAFCtrl);
        }

        private void AddCommonControl()
        {
            XCommonControl.Dock = DockStyle.Fill;
            XCommonControl.SetAxis(AxisHandler.GetAxis(AxisName.X));

            YCommonControl.Dock = DockStyle.Fill;
            YCommonControl.SetAxis(AxisHandler.GetAxis(AxisName.Y));

            ZCommonControl.Dock = DockStyle.Fill;
            ZCommonControl.SetAxis(AxisHandler.GetAxis(AxisName.Z));

            tlpCommonParameter.Controls.Add(XCommonControl);
            tlpCommonParameter.Controls.Add(YCommonControl);
            tlpCommonParameter.Controls.Add(ZCommonControl);
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

            lblCurrentPositionX.Text = axis.GetActualPosition().ToString();

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

            lblCurrentPositionY.Text = axis.GetActualPosition().ToString();

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

            lblCurrentPositionZ.Text = status.MPos.ToString("F3");
            lblCurrentCenterOfGravityZ.Text = status.CenterofGravity.ToString();

            if (status.IsNegativeLimit)
                lblSensorZ.Text = "-";
            else if (status.IsPositiveLimit)
                lblSensorZ.Text = "+";
            else
                lblSensorZ.Text = "";
        }

        public void UpdateCurrentData()
        {
            GetCurrentCommonParams();
            GetCurrentVariableParams();
        }

        private void GetCurrentCommonParams()
        {
            var axisHandler = AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Unit0);

            axisHandler.GetAxis(AxisName.X).AxisCommonParams.SetCommonParams(XCommonControl.GetCurrentData());
            axisHandler.GetAxis(AxisName.Y).AxisCommonParams.SetCommonParams(YCommonControl.GetCurrentData());
            axisHandler.GetAxis(AxisName.Z).AxisCommonParams.SetCommonParams(ZCommonControl.GetCurrentData());
        }

        private void GetCurrentVariableParams()
        {
            string unitName = TeachingPositionListControl.UnitName;
            var posData = SystemManager.Instance().GetTeachingData().GetUnit(unitName).TeachingPositions[(int)TeachingPositionType];

            posData.SetMovingParams(AxisName.X, XVariableControl.GetCurrentData());
            posData.SetMovingParams(AxisName.Y, YVariableControl.GetCurrentData());
            posData.SetMovingParams(AxisName.Z, ZVariableControl.GetCurrentData());
        }

        private void btnMoveToTeachingPosition_Click(object sender, EventArgs e)
        {
            Console.WriteLine(TeachingPositionType.ToString());
        }

        private void lblSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void lblCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Save()
        {
            UpdateCurrentData();

            // Save AxisHandler
            var axisHandler = AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Unit0);
            AppsMotionManager.Instance().Save(axisHandler);

            // Save Model
            var model = ModelManager.Instance().CurrentModel as AppsInspModel;
            model.SetUnitList(SystemManager.Instance().GetTeachingData().UnitList);

            string fileName = System.IO.Path.Combine(AppConfig.Instance().Path.Model, model.Name, InspModel.FileName);
            SystemManager.Instance().SaveModel(fileName, model);
        }
        #endregion

        private void lblTargetPositionX_Click(object sender, EventArgs e)
        {
            double targetPosition = SetLabelDoubleData(sender);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetTargetPosition(AxisName.X, targetPosition);
        }

        private void lblOffsetX_Click(object sender, EventArgs e)
        {
            double offset = SetLabelDoubleData(sender);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetOffset(AxisName.X, offset);
        }

        private void lblCurrentToTargetX_Click(object sender, EventArgs e)
        {
            double currentPosition = Convert.ToDouble(lblCurrentPositionX.Text);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetTargetPosition(AxisName.X, currentPosition);

            lblTargetPositionX.Text = currentPosition.ToString();
        }

        private void lblMoveToTargetX_Click(object sender, EventArgs e)
        {
            double targetPosition = TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().GetTargetPosition(AxisName.X);
            var movingParam = TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().GetMovingParams(AxisName.X);

            AxisHandler.AxisList[(int)AxisName.X].MoveTo(targetPosition, movingParam);
        }

        private void lblServoOnOffX_Click(object sender, EventArgs e)
        {
            if (AxisHandler.AxisList[(int)AxisName.X].IsEnable())
                AxisHandler.AxisList[(int)AxisName.X].TurnOffServo();
            else
                AxisHandler.AxisList[(int)AxisName.X].TurnOnServo();
        }

        private void lblTargetPositionY_Click(object sender, EventArgs e)
        {
            double targetPosition = SetLabelDoubleData(sender);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetTargetPosition(AxisName.Y, targetPosition);
        }

        private void lblOffsetY_Click(object sender, EventArgs e)
        {
            double offset = SetLabelDoubleData(sender);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetOffset(AxisName.Y, offset);
        }

        private void lblCurrentToTargetY_Click(object sender, EventArgs e)
        {
            double currentPosition = Convert.ToDouble(lblCurrentPositionX.Text);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetTargetPosition(AxisName.Y, currentPosition);

            lblTargetPositionY.Text = currentPosition.ToString();
        }

        private void lblMoveToTargetY_Click(object sender, EventArgs e)
        {
            double targetPosition = TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().GetTargetPosition(AxisName.Y);
            var movingParam = TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().GetMovingParams(AxisName.Y);

            AxisHandler.AxisList[(int)AxisName.Y].MoveTo(targetPosition, movingParam);
        }

        private void lblServoOnOffY_Click(object sender, EventArgs e)
        {
            if (AxisHandler.AxisList[(int)AxisName.Y].IsEnable())
                AxisHandler.AxisList[(int)AxisName.Y].TurnOffServo();
            else
                AxisHandler.AxisList[(int)AxisName.Y].TurnOnServo();
        }

        private void lblTargetPositionZ_Click(object sender, EventArgs e)
        {
            double targetPosition = SetLabelDoubleData(sender);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetTargetPosition(AxisName.Z, targetPosition);
        }

        private void lblCurrentToTargetZ_Click(object sender, EventArgs e)
        {
            double currentPosition = Convert.ToDouble(lblCurrentPositionX.Text);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetTargetPosition(AxisName.Z, currentPosition);

            lblTargetPositionZ.Text = currentPosition.ToString();
        }

        private void lblTeachedCenterOfGravityZ_Click(object sender, EventArgs e)
        {
            int targetCenterOfGravity = SetLabelIntegerData(sender);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetCenterOfGravity(AxisName.Z, targetCenterOfGravity);
        }

        private void lblCurrentToTargetenterOfGravityZ_Click(object sender, EventArgs e)
        {
            int targetCenterOfGravity = Convert.ToInt32(lblCurrentCenterOfGravityZ.Text);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetCenterOfGravity(AxisName.Z, targetCenterOfGravity);

            lblTeachedCenterOfGravityZ.Text = targetCenterOfGravity.ToString();
        }

        private void lblMoveToTargetZ_Click(object sender, EventArgs e)
        {
            double targetPosition = TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().GetTargetPosition(AxisName.Z);
            double currentPosition = LAFCtrl.Status.MPos;

            Direction direction = Direction.CCW;
            double moveAmount = targetPosition - currentPosition;
            if (moveAmount < 0)
                direction = Direction.CW;
            else 
                direction = Direction.CCW;

            LAFCtrl.SetMotionRelativeMove(direction, Math.Abs(moveAmount));
        }

        private void lblAutoFocusOnOffZ_Click(object sender, EventArgs e)
        {
            //LAFCtrl.IsLaserOn
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
            MotionJogControl.JogSpeedMode = jogSpeedMode;
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
            MotionJogControl.JogMode = jogMode;
            LAFJogControl.JogMode = jogMode;
        }

        private void lblPitchXYValue_Click(object sender, EventArgs e)
        {
            double pitchXY = SetLabelDoubleData(sender);
            MotionJogControl.JogPitch = pitchXY;
        }

        private void lblPitchZValue_Click(object sender, EventArgs e)
        {
            double pitchZ = SetLabelDoubleData(sender);
            LAFJogControl.MoveAmount = pitchZ;
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
    }
}
