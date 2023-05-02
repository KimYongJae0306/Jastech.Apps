using System;
using System.Drawing;
using System.Windows.Forms;
using Jastech.Framework.Device.Motions;
using AxisName = Jastech.Framework.Device.Motions.AxisName;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Winform.Forms;
using static Jastech.Framework.Device.Motions.AxisMovingParam;
using Jastech.Framework.Winform.Controls;
using Jastech.Apps.Structure.Data;
using Jastech.Framework.Structure;
using Jastech.Framework.Winform;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Winform.VisionPro.Controls;
using Jastech.Apps.Structure;
using Jastech.Apps.Winform.Settings;
using System.Collections.Generic;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Apps.Winform;
using Cognex.VisionPro;
using Jastech.Framework.Imaging;
using System.Runtime.InteropServices;
using Jastech.Framework.Winform.Helper;
using Emgu.CV;
using Jastech.Framework.Imaging.VisionPro;
using System.Threading;
using Jastech.Framework.Imaging.Helper;
using Emgu.CV.CvEnum;
using Jastech.Apps.Structure.Parameters;
using static Emgu.CV.XImgproc.SupperpixelSLIC;
using Emgu.CV.XFeatures2D;

namespace ATT.UI.Forms
{
    public partial class OpticTeachingForm : Form
    {
        private Color _selectedColor = new Color();
        private Color _nonSelectedColor = new Color();

        private CogDisplayControl CogDisplayControl { get; set; } = new CogDisplayControl() { Dock = DockStyle.Fill };

        private AutoFocusControl AutoFocusControl { get; set; } = new AutoFocusControl() { Dock = DockStyle.Fill };

        private MotionRepeatControl MotionRepeatControl { get; set; } = new MotionRepeatControl() { Dock = DockStyle.Fill };

        private MotionJogControl MotionJogControl { get; set; } = new MotionJogControl() { Dock = DockStyle.Fill };

        private LAFJogControl LAFJogControl { get; set; } = new LAFJogControl() { Dock = DockStyle.Fill };

        public OpticTeachingForm()
        {
            InitializeComponent();
        }

        private void LinescanControl_Load(object sender, EventArgs e)
        {
            SystemManager.Instance().UpdateTeachingData();
            AppsLineCameraManager.Instance().TeachingLiveImageGrabbed += LiveDisplay;
            UpdateData();
            AddControl();
            InitializeUI();
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            SetOperationMode(TDIOperationMode.Area);
            rdoJogSlowMode.Checked = true;
            rdoJogMode.Checked = true;
        }

        private void AddControl()
        {
            pnlDisplay.Controls.Add(CogDisplayControl);

            string unitName = "0";// TeachingPositionListControl.UnitName;
            var posData = SystemManager.Instance().GetTeachingData().GetUnit(unitName).TeachingPositions;
            AxisHandler axisHandler = AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
            var lafCtrl = AppsLAFManager.Instance().GetLAFCtrl(LAFName.Akkon);

            pnlAutoFocus.Controls.Add(AutoFocusControl);
            AutoFocusControl.UpdateData(posData[(int)TeachingPositionType.Stage1_Scan_Start].AxisInfoList[(int)AxisName.Z]);
            AutoFocusControl.SetAxisHanlder(axisHandler);
            AutoFocusControl.SetLAFCtrl(lafCtrl);

            MotionRepeatControl.SetAxisHanlder(axisHandler);
            pnlMotionRepeat.Controls.Add(MotionRepeatControl);

            pnlMotionJog.Controls.Add(MotionJogControl);
            MotionJogControl.SetAxisHanlder(AxisHandler);

            pnlLAFJog.Controls.Add(LAFJogControl);
            LAFJogControl.SetSelectedLafCtrl(LAFCtrl);
        }

        private void lblAreaMode_Click(object sender, EventArgs e)
        {
            SetOperationMode(TDIOperationMode.Area);
        }

        
        private void lblLineMode_Click(object sender, EventArgs e)
        {
            SetOperationMode(TDIOperationMode.TDI);
        }

        private void SetOperationMode(TDIOperationMode operationMode)
        {
            if (AppsLineCameraManager.Instance().IsGrabbing)
                AppsLineCameraManager.Instance().StopGrab(CameraName.LinscanMIL0);

            switch (operationMode)
            {
                case TDIOperationMode.TDI:
                    lblLineMode.BackColor = _selectedColor;
                    lblAreaMode.BackColor = _nonSelectedColor;
                    lblCameraExposure.Text = "D GAIN (0 ~ 8[dB])";
                    AppsLineCameraManager.Instance().SetOperationMode(CameraName.LinscanMIL0, TDIOperationMode.TDI);
                    break;

                case TDIOperationMode.Area:
                    lblLineMode.BackColor = _nonSelectedColor;
                    lblAreaMode.BackColor = _selectedColor;
                    lblCameraExposure.Text = "EXPOSURE [us]";
                    AppsLineCameraManager.Instance().SetOperationMode(CameraName.LinscanMIL0, TDIOperationMode.Area);
                    break;

                default:
                    break;
            }
            AppsLineCameraManager.Instance().CurrentOperationMode = operationMode;
        }

        private delegate void UpdateUIDelegate();
        public void UpdateUI()
        {
            if (this.InvokeRequired)
            {
                UpdateUIDelegate callback = UpdateUI;
                BeginInvoke(callback);
                return;
            }

            UpdateMotionStatus();
            MotionRepeatControl.UpdateRepeatCount();
            AutoFocusControl.UpdateAxisStatus();
        }

        public void SetParams()
        {

        }

        private void UpdateData()
        {
            var axisHandler = AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
            SetAxisHandler(axisHandler);

            //var posData = SystemManager.Instance().GetTeachingData().GetUnit(UnitName).TeachingPositions[(int)TeachingPositionType];
            //var posData = GetUnit(UnitName).TeachingPositions[(int)TeachingPositionType];
            //SetTeachingPosition(posData);

            var lafCtrl = AppsLAFManager.Instance().GetLAFCtrl(LAFName.Akkon);
            SetLAFCtrl(lafCtrl);
        }

        private AxisHandler AxisHandler { get; set; } = null;
        private void SetAxisHandler(AxisHandler axisHandler)
        {
            AxisHandler = axisHandler;
        }

        private TeachingPosition TeachingPositionInfo { get; set; } = null;
        private void SetTeachingPosition(TeachingPosition teacingPosition)
        {
            TeachingPositionInfo = teacingPosition.DeepCopy();
        }

        private LAFCtrl LAFCtrl { get; set; } = null;
        private void SetLAFCtrl(LAFCtrl lafCtrl)
        {
            LAFCtrl = lafCtrl;
        }

        private void UpdateMotionStatus()
        {
            UpdateStatusMotionX();
            UpdateStatusMotionY();
            UpdateStatusMotionZ();
        }

        private void UpdateStatusMotionX()
        {
            var axis = AxisHandler.AxisList[(int)AxisName.X];

            if (axis == null || !axis.IsConnected())
                return;

            lblCurrentPositionX.Text = axis.GetActualPosition().ToString("F3");

            if (axis.IsNegativeLimit())
                lblNegativeLimitX.BackColor = Color.Red;
            else
                lblNegativeLimitX.BackColor = _nonSelectedColor;

            if (axis.IsPositiveLimit())
                lblPositiveLimitX.BackColor = _nonSelectedColor;
            else
                lblPositiveLimitX.BackColor = _nonSelectedColor;
        }

        private void UpdateStatusMotionY()
        {
            var axis = AxisHandler.AxisList[(int)AxisName.Y];

            if (axis == null || !axis.IsConnected())
                return;

            lblCurrentPositionY.Text = axis.GetActualPosition().ToString("F3");

            if (axis.IsNegativeLimit())
                lblNegativeLimitY.BackColor = Color.Red;
            else
                lblNegativeLimitY.BackColor = _nonSelectedColor;

            if (axis.IsPositiveLimit())
                lblPositiveLimitY.BackColor = Color.Red;
            else
                lblPositiveLimitY.BackColor = _nonSelectedColor;
        }

        private void UpdateStatusMotionZ()
        {
            var status = LAFCtrl.Status;

            if (status == null)
                return;

            lblCurrentPositionZ.Text = status.MPos.ToString("F3");

            if (status.IsNegativeLimit)
                lblNegativeLimitZ.BackColor = Color.Red;
            else
                lblNegativeLimitZ.BackColor = _nonSelectedColor;

            if (status.IsNegativeLimit)
                lblPositiveLimitZ.BackColor = Color.Red;
            else
                lblPositiveLimitZ.BackColor = _nonSelectedColor;

        }

        private void lblCameraExposureValue_Click(object sender, EventArgs e)
        {
            int exposureTime = 0;
            int digitalGain = 0;
            if (AppsLineCameraManager.Instance().CurrentOperationMode == TDIOperationMode.Area)
            {
                exposureTime = KeyPadHelper.SetLabelIntegerData((Label)sender);
            }
            else
            {
                digitalGain = KeyPadHelper.SetLabelIntegerData((Label)sender);
            }
        }

        private void lblCameraGainValue_Click(object sender, EventArgs e)
        {
            int analogGain = KeyPadHelper.SetLabelIntegerData((Label)sender);
        }

        private void trbDimmingLevelValue_Scroll(object sender, EventArgs e)
        {
            int level = trbDimmingLevelValue.Value;
            nudLightDimmingLevel.Text = level.ToString();
        }

        private void nudLightDimmingLevel_ValueChanged(object sender, EventArgs e)
        {
            int level = Convert.ToInt32(nudLightDimmingLevel.Text);
            trbDimmingLevelValue.Value = level;
        }

        private void lblLightOn_Click(object sender, EventArgs e)
        {
            LightOnOff(true);
        }

        private void lblLightOff_Click(object sender, EventArgs e)
        {
            LightOnOff(false);
        }

        private void LightOnOff(bool isOn)
        {
            // 조명 추가
            if (isOn)
            {

            }
            else
            { 

            }
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
            double pitchXY = KeyPadHelper.SetLabelDoubleData((Label)sender);
            MotionJogControl.JogPitch = pitchXY;
        }

        private void lblPitchZValue_Click(object sender, EventArgs e)
        {
            double pitchZ = KeyPadHelper.SetLabelDoubleData((Label)sender);
            LAFJogControl.MoveAmount = pitchZ;
        }

        private void UpdateCurrentdata()
        {
            string unitName = "0";
            var unit = SystemManager.Instance().GetTeachingData().GetUnit(unitName);
            var posData = unit.TeachingPositions[(int)TeachingPositionType.Stage1_Scan_Start];

            posData.AxisInfoList[(int)AxisName.Z].TargetPosition = AutoFocusControl.GetCurrentData().TargetPosition;
            posData.AxisInfoList[(int)AxisName.Z].CenterOfGravity = AutoFocusControl.GetCurrentData().CenterOfGravity;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AppsInspModel model = ModelManager.Instance().CurrentModel as AppsInspModel;

            if (model == null)
                return;

            SaveModelData(model);

            MessageConfirmForm form = new MessageConfirmForm();
            form.Message = "Save Model Completed.";
            form.ShowDialog();
        }

        private void SaveModelData(AppsInspModel model)
        {
            UpdateCurrentdata();
            model.SetUnitList(SystemManager.Instance().GetTeachingData().UnitList);

            string fileName = System.IO.Path.Combine(AppConfig.Instance().Path.Model, model.Name, InspModel.FileName);
            SystemManager.Instance().SaveModel(fileName, model);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMotionPopup_Click(object sender, EventArgs e)
        {
            MotionPopupForm motionPopupForm = new MotionPopupForm();
            motionPopupForm.ShowDialog();
        }
        private void LiveDisplay(Mat image)
        {
            if (image == null)
                return;

            int size = image.Width * image.Height * image.NumberOfChannels;
            byte[] dataArray = new byte[size];
            Marshal.Copy(image.DataPointer, dataArray, 0, size);

            ColorFormat format = image.NumberOfChannels == 1 ? ColorFormat.Gray : ColorFormat.RGB24;

            var cogImage = CogImageHelper.CovertImage(dataArray, image.Width, image.Height, format);
            CogDisplayControl.SetImage(cogImage);

            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            pictureBox1.Image = image.ToBitmap();
 
        }

        private void btnGrabStart_Click(object sender, EventArgs e)
        {
            if (AppsLineCameraManager.Instance().CurrentOperationMode == TDIOperationMode.TDI)
                AppsLineCameraManager.Instance().StartGrab(CameraName.LinscanMIL0);
            else
            {
                //AreaTimer.Start();
                AppsLineCameraManager.Instance().StartGrabContinous(CameraName.LinscanMIL0);
            }
        }

        private void btnGrabStop_Click(object sender, EventArgs e)
        {
            if (AppsLineCameraManager.Instance().CurrentOperationMode == TDIOperationMode.TDI)
                AppsLineCameraManager.Instance().StopGrab(CameraName.LinscanMIL0);
            else
                AreaTimer.Stop();
        }

        private void OpticTeachingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AppsLineCameraManager.Instance().TeachingLiveImageGrabbed -= LiveDisplay;
            AppsLineCameraManager.Instance().StopGrab(CameraName.LinscanMIL0);
            AppsLineCameraManager.Instance().SetOperationMode(CameraName.LinscanMIL0, TDIOperationMode.TDI);
        }

        private void AreaTimer_Tick(object sender, EventArgs e)
        {
            //byte[] dataArray = AppsLineCameraManager.Instance().OnceGrab(CameraName.LinscanMIL0);
            //Mat grabImage = MatHelper.ByteArrayToMat(dataArray, 4640,1024, 1);
            //if(pictureBox1.Image != null)
            //{
            //    pictureBox1.Image.Dispose();
            //    pictureBox1.Image = null;
            //}
            //pictureBox1.Image = grabImage.ToBitmap();
            ////grabImage.Save(@"D:\grab.bmp");
            //var cogImage = CogImageHelper.CovertImage(dataArray, 4640, 1024, ColorFormat.Gray);
            //CogDisplayControl.SetImage(cogImage);
        }
        static int aaaa = 100;
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            aaaa += 1;
            e.Graphics.DrawRectangle(new Pen(Color.Yellow), new Rectangle(100, 100, aaaa, aaaa));

            if (aaaa > 2000)
                aaaa = 100;

        }
    }
}
