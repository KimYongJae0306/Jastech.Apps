using ATT.UI.Controls;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Structure;
using Jastech.Framework.Winform.Controls;
using Jastech.Framework.Winform.Forms;
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
        #endregion

        #region 속성
        private TeachingPositionListControl TeachingPositionListControl { get; set; } = new TeachingPositionListControl();

        public string UnitName { get; set; } = string.Empty;

        private List<TeachingInfo> TeachingPositionList { get; set; } = null;

        private AxisHandler AxisHandler { get; set; } = null;

        private LAFCtrl LAFCtrl { get; set; } = null;

        private MotionJogControl MotionJogControl { get; set; } = new MotionJogControl() { Dock = DockStyle.Fill };

        private LAFJogControl LAFJogControl { get; set; } = new LAFJogControl() { Dock = DockStyle.Fill };

        private MotionParameterVariableControl XVariableControl = new MotionParameterVariableControl();

        private MotionParameterVariableControl YVariableControl = new MotionParameterVariableControl();

        private MotionParameterVariableControl ZVariableControl = new MotionParameterVariableControl();

        public TeachingPosType TeachingPositionType = TeachingPosType.Standby;
        #endregion

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        #endregion

        #region 메서드
        public MotionPopupForm()
        {
            InitializeComponent();
        }

        private void MotionPopupForm_Load(object sender, EventArgs e)
        {
            SystemManager.Instance().UpdateTeachingData();
            UpdateData();
            AddControl();
            StartTimer();
            InitializeUI();
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
            TeachingPositionListControl.UnitName = "0";
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
            var axisHandler = AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
            SetAxisHandler(axisHandler);

            string unitName = "0";// TeachingPositionListControl.UnitName;
            var posData = SystemManager.Instance().GetTeachingData().GetUnit(unitName).TeachingInfoList;
            SetTeachingPosition(posData);

            var lafCtrl = AppsLAFManager.Instance().GetLAFCtrl(LAFName.Akkon);
            SetLAFCtrl(lafCtrl);

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
            pnlMotionJog.Controls.Add(MotionJogControl);
            MotionJogControl.SetAxisHanlder(AxisHandler);

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
            string unitName = TeachingPositionListControl.UnitName;
            var posData = SystemManager.Instance().GetTeachingData().GetUnit(unitName).TeachingInfoList[(int)TeachingPositionType];

            posData.SetMovingParams(AxisName.X, XVariableControl.GetCurrentData());
            posData.SetMovingParams(AxisName.Y, YVariableControl.GetCurrentData());
            posData.SetMovingParams(AxisName.Z, ZVariableControl.GetCurrentData());
        }

        private void Save()
        {
            UpdateCurrentData();

            // Save AxisHandler
            var axisHandler = AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
            AppsMotionManager.Instance().Save(axisHandler);

            // Save Model
            var model = ModelManager.Instance().CurrentModel as AppsInspModel;
            model.SetUnitList(SystemManager.Instance().GetTeachingData().UnitList);

            string fileName = System.IO.Path.Combine(AppsConfig.Instance().Path.Model, model.Name, InspModel.FileName);
            SystemManager.Instance().SaveModel(fileName, model);

            MessageConfirmForm form = new MessageConfirmForm();
            form.Message = "Save Motion Data Completed.";
            form.ShowDialog();
        }

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

        private void lblTargetPositionZ_Click(object sender, EventArgs e)
        {
            double targetPosition = SetLabelDoubleData(sender);
            TeachingPositionList.Where(x => x.Name == TeachingPositionType.ToString()).First().SetTargetPosition(AxisName.Z, targetPosition);
        }

        private void lblTeachedCenterOfGravityZ_Click(object sender, EventArgs e)
        {
            int centerOfGravity = SetLabelIntegerData(sender);
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

        private void lblOriginZ_Click(object sender, EventArgs e)
        {
            AppsLAFManager.Instance().StartHomeThread(LAFName.Akkon);
        }
    }
    #endregion
}
