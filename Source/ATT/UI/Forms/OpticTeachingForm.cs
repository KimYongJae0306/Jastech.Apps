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
using System.Threading.Tasks;

namespace ATT.UI.Forms
{
    public partial class OpticTeachingForm : Form
    {
        #region 필드
        private Color _selectedColor = new Color();

        private Color _nonSelectedColor = new Color();
        #endregion

        #region 속성
        public UnitName UnitName { get; set; } = UnitName.Unit0;

        private DrawBoxControl DrawBoxControl { get; set; } = new DrawBoxControl() { Dock = DockStyle.Fill };

        private PixelValueGraphControl PixelValueGraphControl { get; set; } = new PixelValueGraphControl() { Dock = DockStyle.Fill };

        private AutoFocusControl AutoFocusControl { get; set; } = new AutoFocusControl() { Dock = DockStyle.Fill };

        private MotionRepeatControl MotionRepeatControl { get; set; } = new MotionRepeatControl() { Dock = DockStyle.Fill };

        private MotionJogControl MotionJogControl { get; set; } = new MotionJogControl() { Dock = DockStyle.Fill };

        private LAFJogControl LAFJogControl { get; set; } = new LAFJogControl() { Dock = DockStyle.Fill };

        private AxisHandler AxisHandler { get; set; } = null;

        private LAFCtrl LAFCtrl { get; set; } = null;

        private TeachingInfo TeachingPositionInfo { get; set; } = null;

        public CameraName CameraName { get; set; }
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        private delegate void UpdateUIDelegate();
        #endregion

        #region 생성자
        public OpticTeachingForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void LinescanControl_Load(object sender, EventArgs e)
        {
            SystemManager.Instance().UpdateTeachingData();

            AppsLineCamera appsLineCamera = AppsLineCameraManager.Instance().GetLineCamera(CameraName);
            appsLineCamera.TeachingLiveImageGrabbed += LiveDisplay;
            appsLineCamera.TabImageGrabCompletedEventHandler += OpticTeachingForm_TabImageGrabCompletedEventHandler;
            appsLineCamera.StartMainGrabTask();

            UpdateData();
            AddControl();
            InitializeUI();
            StatusTimer.Start();
        }

        private void OpticTeachingForm_TabImageGrabCompletedEventHandler(string camearaName, TabScanImage image)
        {
            if(image.GetMergeImage() is Mat mat)
            {
                DrawBoxControl.SetImage(mat.ToBitmap());
                mat.Dispose();
            }
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            SetOperationMode(TDIOperationMode.TDI);
            rdoJogSlowMode.Checked = true;
            rdoJogMode.Checked = true;
        }

        private void AddControl()
        {
            DrawBoxControl.FigureDataDelegateEventHanlder += DrawBoxControl_FigureDataDelegateEventHanlder;
            pnlDisplay.Controls.Add(DrawBoxControl);
            pnlHistogram.Controls.Add(PixelValueGraphControl);

            string unitName = UnitName.Unit0.ToString();
            var posData = SystemManager.Instance().GetTeachingData().GetUnit(unitName).TeachingInfoList;
            AxisHandler axisHandler = AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
            var lafCtrl = AppsLAFManager.Instance().GetLAFCtrl(LAFName.Akkon);

            pnlAutoFocus.Controls.Add(AutoFocusControl);
            AutoFocusControl.UpdateData(posData[(int)TeachingPosType.Stage1_Scan_Start].AxisInfoList[(int)AxisName.Z]);
            AutoFocusControl.SetAxisHanlder(axisHandler);
            AutoFocusControl.SetLAFCtrl(lafCtrl);

            MotionRepeatControl.SetAxisHanlder(axisHandler);
            pnlMotionRepeat.Controls.Add(MotionRepeatControl);

            pnlMotionJog.Controls.Add(MotionJogControl);
            MotionJogControl.SetAxisHanlder(AxisHandler);

            pnlLAFJog.Controls.Add(LAFJogControl);
            LAFJogControl.SetSelectedLafCtrl(LAFCtrl);
        }

        private void DrawBoxControl_FigureDataDelegateEventHanlder(byte[] data)
        {
            PixelValueGraphControl.SetData(data);
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
            var camera = AppsLineCameraManager.Instance().GetLineCamera(CameraName).Camera;
            //camera.Stop();

            if (camera is ICameraTDIavailable tdiCamera)
            {
                if (operationMode == TDIOperationMode.TDI)
                {
                    AppsLineCameraManager.Instance().GetLineCamera(CameraName).IsLive = false;
                    AppsLineCameraManager.Instance().GetLineCamera(CameraName).StopLiveTask();
                    AppsLineCameraManager.Instance().GetLineCamera(CameraName).StartMergeTask();
                    tdiCamera.SetTDIOperationMode(TDIOperationMode.TDI);
                }
                else
                {
                    AppsLineCameraManager.Instance().GetLineCamera(CameraName).IsLive = true;
                    AppsLineCameraManager.Instance().GetLineCamera(CameraName).StartLiveTask();
                    AppsLineCameraManager.Instance().GetLineCamera(CameraName).StopMergeTask();
                    tdiCamera.SetTDIOperationMode(TDIOperationMode.Area);
                }
            }

            switch (operationMode)
            {
                case TDIOperationMode.TDI:
                    lblLineMode.BackColor = _selectedColor;
                    lblAreaMode.BackColor = _nonSelectedColor;
                    lblCameraExposure.Text = "D GAIN (0 ~ 8[dB])";
                  
                    break;

                case TDIOperationMode.Area:
                    lblLineMode.BackColor = _nonSelectedColor;
                    lblAreaMode.BackColor = _selectedColor;
                    lblCameraExposure.Text = "EXPOSURE [us]";
                 
                    break;

                default:
                    break;
            }
        }

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

            var lafCtrl = AppsLAFManager.Instance().GetLAFCtrl(LAFName.Akkon);
            SetLAFCtrl(lafCtrl);
        }
    
        private void SetAxisHandler(AxisHandler axisHandler)
        {
            AxisHandler = axisHandler;
        }
      
        private void SetTeachingPosition(TeachingInfo teacingPosition)
        {
            TeachingPositionInfo = teacingPosition.DeepCopy();
        }
     
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

            double mPos_um = 0.0;
            if (LAFCtrl is NuriOneLAFCtrl nuriOne)
                mPos_um = status.MPosPulse / nuriOne.ResolutionAxisZ;
            else
                mPos_um = status.MPosPulse;

            lblCurrentPositionZ.Text = mPos_um.ToString("F3");

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

            var appsLineCamera = AppsLineCameraManager.Instance().GetLineCamera(CameraName).Camera as CameraMil;

            if (appsLineCamera.TDIOperationMode == TDIOperationMode.Area)
            {
                exposureTime = KeyPadHelper.SetLabelIntegerData((Label)sender);
            }
            else
            {
                digitalGain = KeyPadHelper.SetLabelIntegerData((Label)sender);
                appsLineCamera.SetDigitalGain(digitalGain);
            }
        }

        private void lblCameraGainValue_Click(object sender, EventArgs e)
        {
            int analogGain = KeyPadHelper.SetLabelIntegerData((Label)sender);

            var appsLineCamera = AppsLineCameraManager.Instance().GetLineCamera(CameraName);

            if(appsLineCamera != null)
            {
                appsLineCamera.Camera.SetAnalogGain(analogGain);
            }
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
            string unitName = UnitName.Unit0.ToString();
            var unit = SystemManager.Instance().GetTeachingData().GetUnit(unitName);
            var posData = unit.TeachingInfoList[(int)TeachingPosType.Stage1_Scan_Start];

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

            string fileName = System.IO.Path.Combine(AppsConfig.Instance().Path.Model, model.Name, InspModel.FileName);
            SystemManager.Instance().SaveModel(fileName, model);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMotionPopup_Click(object sender, EventArgs e)
        {
            MotionPopupForm motionPopupForm = new MotionPopupForm();
            motionPopupForm.UnitName = UnitName;
            motionPopupForm.Show();
        }

        private void LiveDisplay(string cameraName, Mat image)
        {
            if (image == null)
                return;

            var camera = AppsLineCameraManager.Instance().GetLineCamera(CameraName).Camera;

            if(camera is ICameraTDIavailable tdiCamera)
            {
                if (tdiCamera.TDIOperationMode == TDIOperationMode.Area)
                {
                    int tdiStage = tdiCamera.TDIStages;
                    tdiStage = 256;
                    Mat cropImage = MatHelper.CropRoi(image, new Rectangle(0, 0, camera.ImageWidth, tdiStage));

                    Bitmap bmp = cropImage.ToBitmap();
                    DrawBoxControl.SetImage(bmp);

                    cropImage.Dispose();
                }
                else
                {
                    DrawBoxControl.SetImage(image.ToBitmap());
                }
            }
            image.Dispose();
        }

        private void btnGrabStart_Click(object sender, EventArgs e)
        {
            var appsLineCamera = AppsLineCameraManager.Instance().GetLineCamera(CameraName);
            //if (LAFCtrl is NuriOneLAFCtrl nuriOne)
            //    nuriOne.SetAutoFocusOnOFF(true);

            if (appsLineCamera.Camera is ICameraTDIavailable tdiCamera)
            {
                if (tdiCamera.TDIOperationMode == TDIOperationMode.TDI)
                {
                    AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
                    double length = MotionRepeatControl.GetScanLength(); // repeat Length

                    // Motion 이동 추가
                    appsLineCamera.StartGrab((float)length);
                }
                else
                {
                    appsLineCamera.StartGrabContinous();
                }
            }
        }

        private void btnGrabStop_Click(object sender, EventArgs e)
        {
            AppsLineCamera camera = AppsLineCameraManager.Instance().GetLineCamera(CameraName);
            camera.StopGrab();
        }

        private void OpticTeachingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AppsLineCamera appsLineCamera = AppsLineCameraManager.Instance().GetLineCamera(CameraName);
            appsLineCamera.StopLiveTask();
            appsLineCamera.StopMainGrabTask();
            appsLineCamera.TeachingLiveImageGrabbed -= LiveDisplay;
            appsLineCamera.TabImageGrabCompletedEventHandler -= OpticTeachingForm_TabImageGrabCompletedEventHandler;
            appsLineCamera.StopGrab();

            appsLineCamera.SetOperationMode(TDIOperationMode.TDI);
            StatusTimer.Stop();
        }
        #endregion

        private void StatusTimer_Tick(object sender, EventArgs e)
        {
            UpdateUI();
        }
    }
}
