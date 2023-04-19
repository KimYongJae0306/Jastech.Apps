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

        private JogSpeedMode _jogSpeedMode = JogSpeedMode.Slow;

        private JogMode _jogMode = JogMode.Jog;

        private Color _selectedColor;

        private Color _noneSelectedColor;


        private TeachingPositionListControl TeachingPositionListControl { get; set; } = new TeachingPositionListControl();

        //private MotionStatusControl MotionStatusControl { get; set; } = new MotionStatusControl() { Dock = DockStyle.Fill };
        //private AutoFocusStatusControl AutoFocusStatusControl { get; set; } = new AutoFocusStatusControl() { Dock = DockStyle.Fill};
        
        //private JogControl JogControl { get; set; } = new JogControl() { Dock = DockStyle.Fill };

        private List<MotionParameterCommonControl> MotionParameterCommonControlList { get; set; } = new List<MotionParameterCommonControl>();

        private List<MotionParameterVariableControl> MotionParameterVariableControlList { get; set; } = new List<MotionParameterVariableControl>();
        #endregion

        #region 속성
        public string UnitName { get; set; } = string.Empty;
        private TeachingPosition TeachingPositionInfo { get; set; } = null;
        private AxisHandler AxisHandler { get; set; } = null;
        private LAFCtrl LAFCtrl { get; set; } = null;

        private MotionJogControl MotionJogControl { get; set; } = new MotionJogControl() { Dock = DockStyle.Fill };
        private LAFJogControl LAFJogControl { get; set;} = new LAFJogControl() { Dock = DockStyle.Fill };
        MotionParameterCommonControl XCommonControl = new MotionParameterCommonControl();

        MotionParameterCommonControl YCommonControl = new MotionParameterCommonControl();

        MotionParameterCommonControl ZCommonControl = new MotionParameterCommonControl();

        MotionParameterVariableControl XVariableControl = new MotionParameterVariableControl();

        MotionParameterVariableControl YVariableControl = new MotionParameterVariableControl();

        MotionParameterVariableControl ZVariableControl = new MotionParameterVariableControl();

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
            SetParams();
            InitializeUI();
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _noneSelectedColor = Color.FromArgb(52, 52, 52);

            rdoJogSlowMode.Checked = true;
            rdoJogMode.Checked = true;
        }

        private void AddControl()
        {
            AddTeachingPositionListControl();

            pnlMotionJog.Controls.Add(MotionJogControl);
            MotionJogControl.SetAxisHanlder(AxisHandler);

            pnlLAFJog.Controls.Add(LAFJogControl);
            LAFJogControl.SetSelectedLafCtrl(LAFCtrl);

            AddCommonControl();
            AddVariableControl();
        }

        private void AddTeachingPositionListControl()
        {
            TeachingPositionListControl.Dock = DockStyle.Fill;
            TeachingPositionListControl.UnitName = "";
            TeachingPositionListControl.SendEventHandler += new TeachingPositionListControl.SetTeachingPositionListDelegate(ReceiveTeachingPosition);
            pnlTeachingPositionList.Controls.Add(TeachingPositionListControl);
        }

        private void ReceiveTeachingPosition(TeachingPositionType teachingPositionType)
        {
            Console.WriteLine(teachingPositionType.ToString());
            TeachingPositionType = teachingPositionType;

            SetParams(TeachingPositionType);
        }

        private void AddCommandControl()
        {
            //var axisHandler = AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Unit0);

            //XCommandControl.Dock = DockStyle.Fill;
            //XCommandControl.SetAxis(axisHandler.GetAxis(AxisName.X));

            //YCommandControl.Dock = DockStyle.Fill;
            //YCommandControl.SetAxis(axisHandler.GetAxis(AxisName.Y));

            var lafctrl = DeviceManager.Instance().LAFCtrlHandler.Where(x => x.Name == LAFName.Akkon.ToString()).First();
            //AutoFocusStatusControl.SetLAFCtrl(lafctrl);

            //tlpStatus.Controls.Add(XCommandControl);
            //tlpStatus.Controls.Add(YCommandControl);
            //tlpStatus.Controls.Add(ZCommandControl);
            //tlpStatus.Controls.Add(AutoFocusStatusControl);
        }

        private void AddCommonControl()
        {
            var axisHandler = AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Unit0);

            XCommonControl.Dock = DockStyle.Fill;
            XCommonControl.SetAxis(axisHandler.GetAxis(AxisName.X));

            YCommonControl.Dock = DockStyle.Fill;
            YCommonControl.SetAxis(axisHandler.GetAxis(AxisName.Y));

            ZCommonControl.Dock = DockStyle.Fill;
            ZCommonControl.SetAxis(axisHandler.GetAxis(AxisName.Z));

            tlpCommonParameter.Controls.Add(XCommonControl);
            tlpCommonParameter.Controls.Add(YCommonControl);
            tlpCommonParameter.Controls.Add(ZCommonControl);
        }

        private void AddVariableControl()
        {
            var axisHandler = AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Unit0);

            XVariableControl.Dock = DockStyle.Fill;
            XVariableControl.SetAxis(axisHandler.GetAxis(AxisName.X));

            YVariableControl.Dock = DockStyle.Fill;
            YVariableControl.SetAxis(axisHandler.GetAxis(AxisName.Y));

            ZVariableControl.Dock = DockStyle.Fill;
            ZVariableControl.SetAxis(axisHandler.GetAxis(AxisName.Z));

            tlpVariableParameter.Controls.Add(XVariableControl);
            tlpVariableParameter.Controls.Add(YVariableControl);
            tlpVariableParameter.Controls.Add(ZVariableControl);
        }

        private void StartTimer()
        {
            _formTimer = new System.Threading.Timer(UpdateStatus, null, 1000, 1000);
        }

        //public void UpdateData(AxisHandler axisHandler, TeachingPosition teacingPosition, LAFCtrl lafCtrl)
        //{
        //    SetTeachingPosition(teacingPosition);
        //    SetAxisHandler(axisHandler);
        //    SetLAFCtrl(lafCtrl);
        //}

        private void UpdateData()
        {
            var axisHandler = AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Unit0);
            SetAxisHandler(axisHandler);

            var posData = SystemManager.Instance().GetTeachingData().GetUnit(UnitName).TeachingPositions[(int)TeachingPositionType];
            SetTeachingPosition(posData);

            var lafCtrl = AppsLAFManager.Instance().GetLAFCtrl(LAFName.Akkon);
            SetLAFCtrl(lafCtrl);
        }

        private void SetAxisHandler(AxisHandler axisHandler)
        {
            AxisHandler = axisHandler;
        }

        private void SetTeachingPosition(TeachingPosition teacingPosition)
        {
            TeachingPositionInfo = teacingPosition.DeepCopy();
        }

        private void SetLAFCtrl(LAFCtrl lafCtrl)
        {
            LAFCtrl = lafCtrl;
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
            //var axis = AxisHandler.AxisList[(int)AxisName.Z];
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

        public void SetParams(TeachingPositionType teachingPositionType = TeachingPositionType.Standby)
        {
            SetCommonParams();
            SetVariableParams();
        }

        private void SetCommonParams()
        {
            XCommonControl.UpdateData(AxisHandler.GetAxis(AxisName.X).AxisCommonParams.DeepCopy());
            YCommonControl.UpdateData(AxisHandler.GetAxis(AxisName.Y).AxisCommonParams.DeepCopy());
            ZCommonControl.UpdateData(AxisHandler.GetAxis(AxisName.Z).AxisCommonParams.DeepCopy());
        }

        private void SetVariableParams()
        {
            lblTargetPositionX.Text = TeachingPositionInfo.GetTargetPosition(AxisName.X).ToString();
            lblOffsetX.Text = TeachingPositionInfo.GetOffset(AxisName.X).ToString();
            XVariableControl.UpdateData(TeachingPositionInfo.GetMovingParams(AxisName.X));

            lblTargetPositionY.Text = TeachingPositionInfo.GetTargetPosition(AxisName.Y).ToString();
            lblOffsetY.Text = TeachingPositionInfo.GetOffset(AxisName.Y).ToString();
            YVariableControl.UpdateData(TeachingPositionInfo.GetMovingParams(AxisName.Y));

            lblTargetPositionZ.Text = TeachingPositionInfo.GetTargetPosition(AxisName.Z).ToString();
            lblTeachedCenterOfGravityZ.Text = TeachingPositionInfo.GetCenterOfGravity(AxisName.Z).ToString();
            ZVariableControl.UpdateData(TeachingPositionInfo.GetMovingParams(AxisName.Z));
        }

        public void UpdateCurrentData()
        {
            GetCurrentCommonParams();
            GetCurrentVariableParams();
        }

        private void GetCurrentCommonParams()
        {
            AxisHandler.GetAxis(AxisName.X).AxisCommonParams.SetCommonParams(XCommonControl.GetCurrentData());
            AxisHandler.GetAxis(AxisName.Y).AxisCommonParams.SetCommonParams(YCommonControl.GetCurrentData());
            AxisHandler.GetAxis(AxisName.Z).AxisCommonParams.SetCommonParams(ZCommonControl.GetCurrentData());
        }

        private void GetCurrentVariableParams()
        {
            TeachingPositionInfo.SetTargetPosition(AxisName.X, Convert.ToDouble(lblTargetPositionX.Text));
            TeachingPositionInfo.SetOffset(AxisName.X, Convert.ToDouble(lblOffsetX.Text));
            TeachingPositionInfo.SetMovingParams(AxisName.X, XVariableControl.GetCurrentData());

            int gg = 0;
            TeachingPositionInfo.SetTargetPosition(AxisName.Y, Convert.ToDouble(lblTargetPositionY.Text));
            TeachingPositionInfo.SetOffset(AxisName.Y, Convert.ToDouble(lblOffsetY.Text));
            TeachingPositionInfo.SetMovingParams(AxisName.Y, YVariableControl.GetCurrentData());

            TeachingPositionInfo.SetTargetPosition(AxisName.Z, Convert.ToDouble(lblTargetPositionZ.Text));
            TeachingPositionInfo.SetCenterOfGravity(AxisName.Z, Convert.ToInt32(lblTeachedCenterOfGravityZ.Text));
            TeachingPositionInfo.SetMovingParams(AxisName.Z, ZVariableControl.GetCurrentData());
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

            var tlqkf = SystemManager.Instance().GetTeachingData().UnitList;
            string fileName = System.IO.Path.Combine(AppConfig.Instance().Path.Model, model.Name, InspModel.FileName);
            SystemManager.Instance().SaveModel(fileName, model);
        }
        #endregion

        private void lblTargetPositionX_Click(object sender, EventArgs e)
        {
            double targetPosition = SetLabelDoubleData(sender);
            TeachingPositionInfo.SetTargetPosition(AxisName.X, targetPosition);
        }

        private void lblOffsetX_Click(object sender, EventArgs e)
        {
            double offset = SetLabelDoubleData(sender);
            TeachingPositionInfo.SetOffset(AxisName.X, offset);
        }

        private void lblCurrentToTargetX_Click(object sender, EventArgs e)
        {
            double currentPosition = Convert.ToDouble(lblCurrentPositionX.Text);
            lblTargetPositionX.Text = currentPosition.ToString();

            TeachingPositionInfo.SetTargetPosition(AxisName.X, currentPosition);
        }

        private void lblMoveToTargetX_Click(object sender, EventArgs e)
        {
            double targetPosition = TeachingPositionInfo.GetTargetPosition(AxisName.X);
            var movingParam = TeachingPositionInfo.GetMovingParams(AxisName.X);

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
            TeachingPositionInfo.SetTargetPosition(AxisName.Y, targetPosition);
        }

        private void lblOffsetY_Click(object sender, EventArgs e)
        {
            double offset = SetLabelDoubleData(sender);
            TeachingPositionInfo.SetOffset(AxisName.Y, offset);
        }

        private void lblCurrentToTargetY_Click(object sender, EventArgs e)
        {
            double currentPosition = Convert.ToDouble(lblCurrentPositionY.Text);
            lblTargetPositionY.Text = currentPosition.ToString();

            TeachingPositionInfo.SetTargetPosition(AxisName.Y, currentPosition);
        }

        private void lblMoveToTargetY_Click(object sender, EventArgs e)
        {
            double targetPosition = TeachingPositionInfo.GetTargetPosition(AxisName.Y);
            var movingParam = TeachingPositionInfo.GetMovingParams(AxisName.Y);

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
            TeachingPositionInfo.SetTargetPosition(AxisName.Z, targetPosition);
        }

        private void lblCurrentToTargetZ_Click(object sender, EventArgs e)
        {
            double currentPosition = Convert.ToDouble(lblCurrentPositionZ.Text);
            lblTargetPositionZ.Text = currentPosition.ToString();

            TeachingPositionInfo.SetTargetPosition(AxisName.Z, currentPosition);
        }

        private void lblTeachedCenterOfGravityZ_Click(object sender, EventArgs e)
        {
            int targetCenterOfGravity = SetLabelIntegerData(sender);
            TeachingPositionInfo.SetCenterOfGravity(AxisName.Z, targetCenterOfGravity);
        }

        private void lblCurrentToTargetenterOfGravityZ_Click(object sender, EventArgs e)
        {
            int currentCenterOfGravity = Convert.ToInt32(lblCurrentCenterOfGravityZ.Text);
            lblCurrentToTargetenterOfGravityZ.Text = currentCenterOfGravity.ToString();

            TeachingPositionInfo.SetTargetPosition(AxisName.Z, currentCenterOfGravity);
        }

        private void lblMoveToTargetZ_Click(object sender, EventArgs e)
        {
            double targetPosition = TeachingPositionInfo.GetTargetPosition(AxisName.Z);
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

        private void rdoIncreaseMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoIncreaseMode.Checked)
            {
                SetSelectJogMode(JogMode.Increase);
                rdoIncreaseMode.BackColor = _selectedColor;
            }
            else
                rdoIncreaseMode.BackColor = _noneSelectedColor;
        }

        private void rdoJogMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoJogMode.Checked)
            {
                SetSelectJogMode(JogMode.Jog);
                rdoJogMode.BackColor = _selectedColor;
            }
            else
                rdoJogMode.BackColor = _noneSelectedColor;
        }

        private void SetSelectJogMode(JogMode jogMode)
        {
            MotionJogControl.JogMode = jogMode;
            LAFJogControl.JogMode = jogMode;
        }

        private void rdoJogFastMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoJogFastMode.Checked)
            {
                SetSelectJogSpeedMode(JogSpeedMode.Fast);
                rdoJogFastMode.BackColor = _selectedColor;
            }
            else
                rdoJogFastMode.BackColor = _noneSelectedColor;
        }

        private void rdoJogSlowMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoJogSlowMode.Checked)
            {
                SetSelectJogSpeedMode(JogSpeedMode.Slow);
                rdoJogSlowMode.BackColor = _selectedColor;
            }
            else
                rdoJogSlowMode.BackColor = _noneSelectedColor;
        }

        private void SetSelectJogSpeedMode(JogSpeedMode jogSpeedMode)
        {
            MotionJogControl.JogSpeedMode = jogSpeedMode;
            LAFJogControl.JogSpeedMode = jogSpeedMode;
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
