using Emgu.CV;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Framework.Config;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Structure;
using Jastech.Framework.Structure.Service;
using Jastech.Framework.Winform.Controls;
using Jastech.Framework.Winform.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using static Jastech.Framework.Device.Motions.AxisMovingParam;

namespace Jastech.Framework.Winform.Forms
{
    public partial class OpticTeachingForm : Form
    {
        #region 필드
        private Color _selectedColor = new Color();

        private Color _nonSelectedColor = new Color();

        private int _curRepeat { get; set; } = 0;

        private float _curLength { get; set; } = 0;

        private Direction _direction = Direction.CW;

        public double _prevAnalogGain { get; set; } = 0;

        public double _prevDigitalGain { get; set; } = 0;

        private Thread _repeatThread = null;

        private bool _isRepeat = false;

        private bool _isInfinite = false;

        private int _remainCount = 0;
        #endregion

        #region 속성
        public UnitName UnitName { get; set; } = UnitName.Unit0;

        public AxisName AxisNameZ { get; set; } = AxisName.Z0;

        public InspModelService InspModelService { get; set; } = null;

        private DrawBoxControl DrawBoxControl { get; set; } = null;

        private PixelValueGraphControl PixelValueGraphControl { get; set; } = null;

        private AutoFocusControl AutoFocusControl { get; set; } = null;

        //private MotionJogXYControl MotionJogXYControl { get; set; } = null;

        private MotionJogXControl MotionJogXControl { get; set; } = null;

        private LAFJogControl LAFJogControl { get; set; } = null;

        private LightControl LightControl { get; set; } = null;

        public AxisHandler AxisHandler { get; set; } = null;

        public string TitleCameraName { get; set; } = "";
        
        public LAFCtrl LAFCtrl { get; set; } = null;

        public LineCamera LineCamera { get; set; } = null;

        public string LineCameraDataName { get; set; } = "";

        private Axis SelectedAxis { get; set; } = null;
        #endregion

        #region 이벤트
        public OpenMotionPopupDelegate OpenMotionPopupEventHandler;
        #endregion

        #region 델리게이트
        private delegate void UpdateUIDelegate();

        public delegate void OpenMotionPopupDelegate(UnitName unitName);
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
            TeachingData.Instance().UpdateTeachingData();
            AddControl();
            InitializeUI();
            InitializeData();

            LineCamera.TeachingLiveImageGrabbed += LiveDisplay;
            LineCamera.GrabOnceEventHandler += OpticTeachingForm_GrabOnceEventHandler;
            SelectedAxis = AxisHandler.GetAxis(AxisName.X);

            lblStageCam.Text = $"STAGE : {UnitName} / CAM : {TitleCameraName}";

            StatusTimer.Start();
        }

        private void RollbackPrevData()
        {
            var camera = LineCamera.Camera;

            camera.SetDigitalGain(_prevDigitalGain);

            Thread.Sleep(50);

            camera.SetAnalogGain((int)_prevAnalogGain);
        }

        private void TeachingEventFunction(ATTInspTab inspTab)
        {
            if (inspTab.MergeMatImage != null)
            {
                DrawBoxControl.SetImage(inspTab.MergeMatImage.ToBitmap());
            }
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            SetOperationMode(TDIOperationMode.TDI);
            rdoJogSlowMode.Checked = true;
            rdoJogMode.Checked = true;

            tlpExposure.Dock = DockStyle.Fill;
            tlpDigitalGain.Dock = DockStyle.Fill;
        }

        private void AddControl()
        {
            DrawBoxControl = new DrawBoxControl();
            DrawBoxControl.Dock = DockStyle.Fill;
            DrawBoxControl.FigureDataDelegateEventHandler += DrawBoxControl_FigureDataDelegateEventHandler;
            pnlDisplay.Controls.Add(DrawBoxControl);

            PixelValueGraphControl = new PixelValueGraphControl();
            PixelValueGraphControl.Dock = DockStyle.Fill;
            pnlHistogram.Controls.Add(PixelValueGraphControl);

            AutoFocusControl = new AutoFocusControl();
            AutoFocusControl.Dock = DockStyle.Fill;
            pnlAutoFocus.Controls.Add(AutoFocusControl);

            MotionJogXControl = new MotionJogXControl();
            MotionJogXControl.Dock = DockStyle.Fill;
            pnlMotionJog.Controls.Add(MotionJogXControl);
            MotionJogXControl.SetAxisHandler(AxisHandler);

            LAFJogControl = new LAFJogControl();
            LAFJogControl.Dock = DockStyle.Fill;
            pnlLAFJog.Controls.Add(LAFJogControl);

            LightControl = new LightControl();
            LightControl.Dock = DockStyle.Fill;

            pnlLight.Controls.Add(LightControl);
        }

        private void InitializeData()
        {
            var unit = TeachingData.Instance().GetUnit(UnitName.ToString());
            var posData = unit.GetTeachingInfo(TeachingPosType.Stage1_Scan_Start);

            AutoFocusControl.SetAxisHandler(AxisHandler);
            AutoFocusControl.SetLAFCtrl(LAFCtrl);
            AutoFocusControl.UpdateData(posData.GetAxisInfo(AxisNameZ));

            //if (MotionJogXYControl != null)
            //{
            //    MotionJogXYControl.SetAxisHanlder(AxisHandler);
            //    MotionJogXYControl.JogMode = JogMode.Jog;
            //    MotionJogXYControl.JogSpeedMode = JogSpeedMode.Slow;
            //    MotionJogXYControl.JogPitch = Convert.ToDouble(lblPitchXYValue.Text);
            //}
            if (MotionJogXControl != null)
            {
                MotionJogXControl.JogMode = JogMode.Jog;
                MotionJogXControl.JogSpeedMode = JogSpeedMode.Slow;
                MotionJogXControl.JogPitch = Convert.ToDouble(lblPitchXYValue.Text);
            }

            LAFJogControl.SetSelectedLafCtrl(LAFCtrl);
            LAFJogControl.JogMode = JogMode.Jog;
            LAFJogControl.JogSpeedMode = JogSpeedMode.Slow;
            LAFJogControl.MoveAmount = Convert.ToDouble(lblPitchZValue.Text);

            var lineCameraData = unit.GetLineCameraData(LineCameraDataName);
            LightControl.SetParam(DeviceManager.Instance().LightCtrlHandler, lineCameraData.LightParam);
            LightControl.InitializeData();

            var camera = LineCamera.Camera;
            camera.SetAnalogGain(lineCameraData.AnalogGain);
            camera.SetDigitalGain(lineCameraData.DigitalGain);

            lbExposureValue.Text = camera.GetExposureTime().ToString("F4");
            lblDigitalGainValue.Text = lineCameraData.DigitalGain.ToString("F4");
            lblAnalogGainValue.Text = lineCameraData.AnalogGain.ToString();

            _prevAnalogGain = lineCameraData.AnalogGain;
            _prevDigitalGain = lineCameraData.DigitalGain;
        }

        private void DrawBoxControl_FigureDataDelegateEventHandler(byte[] data)
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
            var camera = LineCamera.Camera;

            if (camera is ICameraTDIavailable tdiCamera)
            {
                if (operationMode == TDIOperationMode.TDI)
                    tdiCamera.SetTDIOperationMode(TDIOperationMode.TDI);
                else
                    tdiCamera.SetTDIOperationMode(TDIOperationMode.Area);
            }

            switch (operationMode)
            {
                case TDIOperationMode.TDI:
                    lblLineMode.BackColor = _selectedColor;
                    lblAreaMode.BackColor = _nonSelectedColor;
                    tlpExposure.Visible = false;
                    tlpDigitalGain.Visible = true;
                    break;

                case TDIOperationMode.Area:
                    lblLineMode.BackColor = _nonSelectedColor;
                    lblAreaMode.BackColor = _selectedColor;
                    tlpExposure.Visible = true;
                    tlpDigitalGain.Visible = false;
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
            UpdateRepeatCount();
            AutoFocusControl.UpdateAxisStatus();
        }

        private void UpdateMotionStatus()
        {
            UpdateStatusMotionX();
            UpdateStatusMotionY();
            UpdateStatusMotionZ();
        }

        private void UpdateStatusMotionX()
        {
            var axis = AxisHandler.GetAxis(AxisName.X);

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
            var axis = AxisHandler.GetAxis(AxisName.Y);

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

            if (status.IsPositiveLimit)
                lblPositiveLimitZ.BackColor = Color.Red;
            else
                lblPositiveLimitZ.BackColor = _nonSelectedColor;

        }

        private void lblCameraExposureValue_Click(object sender, EventArgs e)
        {
            double exposureTime = 0;
            bool isOutOfRange = false;

            var tdiCamera = LineCamera.Camera as CameraMil;
            if (tdiCamera != null)
            {
                exposureTime = KeyPadHelper.SetLabelIntegerData((Label)sender);

                if (exposureTime > 200000.0)
                {
                    isOutOfRange = true;
                    exposureTime = 200000.0;
                }

                if (exposureTime < 1.0)
                {
                    isOutOfRange = true;
                    exposureTime = 1.0;
                }

                if (isOutOfRange)
                    ShowConfirmDataRange(minValue: "1.0", maxValue: "200000.0");

                lbExposureValue.Text = exposureTime.ToString();
                tdiCamera.SetExposureTime(exposureTime);
       
            }
        }

        private void lblCameraDigitalGainValue_Click(object sender, EventArgs e)
        {
            double digitalGain = 0;
            bool isOutOfRange = false;

            digitalGain = KeyPadHelper.SetLabelIntegerData((Label)sender);

            if (digitalGain > 8.0)
            {
                isOutOfRange = true;
                digitalGain = 8.0;
            }

            if (digitalGain < 0.5)
            {
                isOutOfRange = true;
                digitalGain = 0.5;
            }

            if (isOutOfRange)
                ShowConfirmDataRange(minValue: "0.5", maxValue: "8.0");

            lblDigitalGainValue.Text = digitalGain.ToString();

            var tdiCamera = LineCamera.Camera as CameraMil;
            if (tdiCamera != null)
                tdiCamera.SetDigitalGain(digitalGain);
        }

        private void ShowConfirmDataRange(string minValue, string maxValue)
        {
            MessageConfirmForm form = new MessageConfirmForm();
            form.Message = "Out of Range. Min : " + minValue + " ~ Max : " + maxValue;
            form.ShowDialog();
        }

        private void lblCameraGainValue_Click(object sender, EventArgs e)
        {
            int analogGain = KeyPadHelper.SetLabelIntegerData((Label)sender);

            bool isOutOfRange = false;

            if (analogGain > 4.0)
            {
                isOutOfRange = true;
                analogGain = 4;
            }

            if (analogGain < 1)
            {
                isOutOfRange = true;
                analogGain = 1;
            }

            if (isOutOfRange)
                ShowConfirmDataRange(minValue: "1", maxValue: "4");

            lblAnalogGainValue.Text = analogGain.ToString();
            LineCamera?.Camera.SetAnalogGain(analogGain);
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
            MotionJogXControl.JogMode = jogMode;
            LAFJogControl.JogMode = jogMode;
        }

        private void lblPitchXYValue_Click(object sender, EventArgs e)
        {
            double pitchXY = KeyPadHelper.SetLabelDoubleData((Label)sender);
            MotionJogXControl.JogPitch = pitchXY;
        }

        private void lblPitchZValue_Click(object sender, EventArgs e)
        {
            double pitchZ = KeyPadHelper.SetLabelDoubleData((Label)sender);
            LAFJogControl.MoveAmount = pitchZ;
        }

        private void UpdateCurrentdata()
        {
            var unit = TeachingData.Instance().GetUnit(UnitName.ToString());
            var posData = unit.GetTeachingInfo(TeachingPosType.Stage1_Scan_Start);

            posData.GetAxisInfo(AxisNameZ).TargetPosition = AutoFocusControl.GetCurrentData().TargetPosition;
            posData.GetAxisInfo(AxisNameZ).CenterOfGravity = AutoFocusControl.GetCurrentData().CenterOfGravity;

            var lineCameraData = unit.GetLineCameraData(LineCameraDataName);
            lineCameraData.DigitalGain = Convert.ToDouble(lblDigitalGainValue.Text);
            lineCameraData.AnalogGain = Convert.ToInt16(lblAnalogGainValue.Text);
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
            model.SetUnitList(TeachingData.Instance().UnitList);

            string fileName = System.IO.Path.Combine(ConfigSet.Instance().Path.Model, model.Name, InspModel.FileName);
            InspModelService?.Save(fileName, model);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            RollbackPrevData();
            this.Close();
        }

        private void btnMotionPopup_Click(object sender, EventArgs e)
        {
            OpenMotionPopupEventHandler?.Invoke(UnitName);
        }

        private void LiveDisplay(string cameraName, Mat image)
        {
            if (image == null)
                return;

            var camera = LineCamera.Camera;

            if(camera is ICameraTDIavailable tdiCamera)
            {
                if (tdiCamera.TDIOperationMode == TDIOperationMode.Area)
                {
                    int tdiStage = tdiCamera.TDIStages;
                    tdiStage = 256;
                    // Orginal Data를 Transpose해 Queue에 쌓아 Crop 시 Width, Height가 반대임
                    Mat cropImage = MatHelper.CropRoi(image, new Rectangle(0, 0, tdiStage, camera.ImageWidth));
           
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
            LineCamera.IsLive = true;
            LineCamera.StartLiveTask();

            StartGrab(false);
        }

        private void StartGrab(bool isRepeat)
        {
            StopGrab();

            //if (LAFCtrl is NuriOneLAFCtrl nuriOne)
            //    nuriOne.SetAutoFocusOnOFF(true);

            if(isRepeat)
            {
                AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
                double length = Convert.ToDouble(lblScanXLength.Text);

                // Motion 이동 추가
                LineCamera.StartGrab((float)length);
            }
            else
            {
                LineCamera.StartGrabContinous();
            }
           
        }

        private void btnGrabStop_Click(object sender, EventArgs e)
        {
            LineCamera.IsLive = false;
            StopGrab();
        }

        private void StopGrab()
        {
            LineCamera.StopGrab();
        }

        private void OpticTeachingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopGrab();

            DrawBoxControl.DisposeImage();

            LineCamera.IsLive = false;
            LineCamera.StopLiveTask();
            LineCamera.TeachingLiveImageGrabbed -= LiveDisplay;
            LineCamera.GrabOnceEventHandler -= OpticTeachingForm_GrabOnceEventHandler;
            LineCamera.StopGrab();

            LineCamera.SetOperationMode(TDIOperationMode.TDI);
            StatusTimer.Stop();
        }

        private void StatusTimer_Tick(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void lblRepeatVelocityValue_Click(object sender, EventArgs e)
        {
            KeyPadHelper.SetLabelDoubleData((Label)sender);
        }

        private void lblRepeatAccelerationValue_Click(object sender, EventArgs e)
        {
            KeyPadHelper.SetLabelDoubleData((Label)sender);
        }

        private void lblDwellTimeValue_Click(object sender, EventArgs e)
        {
            KeyPadHelper.SetLabelIntegerData((Label)sender);
        }

        private void lblForward_Click(object sender, EventArgs e)
        {
            SetScanDirection(Direction.CW);
        }

        private void lblBackward_Click(object sender, EventArgs e)
        {
            SetScanDirection(Direction.CCW);
        }

        private void SetScanDirection(Direction direction)
        {
            if (direction == Direction.CW)
            {
                lblForward.BackColor = _selectedColor;
                lblBackward.BackColor = _nonSelectedColor;
            }
            else
            {
                lblForward.BackColor = _nonSelectedColor;
                lblBackward.BackColor = _selectedColor;
            }

            _direction = direction;
        }

        private void lblScanXLength_Click(object sender, EventArgs e)
        {
            KeyPadHelper.SetLabelDoubleData((Label)sender);
        }

        private void lblRepeatCount_Click(object sender, EventArgs e)
        {
            KeyPadHelper.SetLabelIntegerData((Label)sender);
        }

        private void lblStart_Click(object sender, EventArgs e)
        {
            SetOperationMode(TDIOperationMode.TDI);
        }

        public double GetScanLength()
        {
            return Convert.ToDouble(lblScanXLength.Text);
        }

        private void MoveRepeat(bool isRepeat)
        {
            if (isRepeat)
            {
                LineCamera.IsLive = false;
                LineCamera.StopGrab();

                double currentPosition = SelectedAxis.GetActualPosition();
                double distance = Convert.ToDouble(lblScanXLength.Text);
                double targetPosition = 0.0;

                if (_direction == Direction.CW)
                    targetPosition = currentPosition + distance;
                else
                    targetPosition = currentPosition - distance;

                RepeatParam param = new RepeatParam();

                param.Velocity = Convert.ToDouble(lblRepeatVelocityValue.Text);
                param.AccDec = Convert.ToDouble(lblRepeatAccelerationValue.Text);
                param.DwellTime = Convert.ToInt16(lblDwellTimeValue.Text);
                param.StartPosition = currentPosition;
                param.EndPosition = targetPosition;
                param.RepeatCount = Convert.ToInt32(lblRepeatCount.Text);

                if (param.RepeatCount == 0)
                    _isInfinite = true;

                _isRepeat = true;
                _repeatThread = new Thread(new ParameterizedThreadStart(this.MoveRepeatThread));
                _repeatThread.Start(param);
            }
            else
            {
                _isRepeat = false;
                lblStartRepeat.Text = "Start";
            }
        }

        private void MoveRepeatThread(object param)
        {
            var repeatParam = param as RepeatParam;

            AxisMovingParam movingParam = new AxisMovingParam();
            movingParam.Velocity = repeatParam.Velocity;
            movingParam.Acceleration = repeatParam.AccDec;
            movingParam.AfterWaitTime = repeatParam.DwellTime;

            int repeatCount = repeatParam.RepeatCount;
            int count = 0;

            if (repeatCount == 0)
            { }
            else
                _remainCount = repeatCount;

            while (_isRepeat)
            {

                StartGrab(true);

                SelectedAxis.StartAbsoluteMove(repeatParam.EndPosition, movingParam);
                while (!SelectedAxis.WaitForDone())
                    Thread.Sleep(Convert.ToInt16(movingParam.AfterWaitTime));

                SelectedAxis.StartAbsoluteMove(repeatParam.StartPosition, movingParam);
                while (!SelectedAxis.WaitForDone())
                    Thread.Sleep(Convert.ToInt16(movingParam.AfterWaitTime));

                count++;

                if (repeatCount == count)
                    _isRepeat = false;

                if (_isInfinite)
                    _isRepeat = true;

                _remainCount = repeatCount - count;

                StopGrab();

                Console.WriteLine("Set Repeat Count : " + repeatCount.ToString() + " / Complete Count : " + count.ToString() + " / Remain Count : " + _remainCount.ToString());
            }
            lblStartRepeat.BeginInvoke(new Action(() => lblStartRepeat.Text = "Start"));
        }

        public void UpdateRepeatCount()
        {
            //if (!_isRepeat)
            //    return;

            lblRepeatRemain.Text = _remainCount + " / " + lblRepeatCount.Text;
        }

        private void lblStartRepeat_Click(object sender, EventArgs e)
        {
            if (_isRepeat == false)
            {
                lblStartRepeat.Text = "Stop";
                MoveRepeat(true);
            }
            else
            {
                lblStartRepeat.Text = "Start";
                MoveRepeat(false);
            }
        }

        private void OpticTeachingForm_GrabOnceEventHandler(TabScanBuffer tabScanBuffer)
        {
            if (_isRepeat == false)
                return;

            List<Mat> subImageList = new List<Mat>();

            int count = 0;
            while (tabScanBuffer.DataQueue.Count > 0)
            {
                byte[] data = tabScanBuffer.GetData();
                if(count ==0)
                {
                    //TDI 특성상 첫장 버림
                    count++;
                    continue;
                }
                Mat mat = MatHelper.ByteArrayToMat(data, tabScanBuffer.SubImageWidth, tabScanBuffer.SubImageHeight, 1);
                Mat rotatedMat = MatHelper.Transpose(mat);
                subImageList.Add(rotatedMat);
            }

            if (subImageList.Count > 0)
            {
                Mat mergeMatImage = new Mat();
                CvInvoke.HConcat(subImageList.ToArray(), mergeMatImage);

                for (int i = 0; i < subImageList.Count; i++)
                {
                    subImageList[i].Dispose();
                    subImageList[i] = null;
                }
                subImageList.Clear();

                Bitmap bmp = mergeMatImage.ToBitmap();
                DrawBoxControl.SetImage(bmp);
                mergeMatImage.Dispose();
                mergeMatImage = null;
            }
            Console.WriteLine("Update Repeat Display");
        }
        #endregion
    }

    public class RepeatParam
    {
        #region 속성
        public double Velocity { get; set; } = 0.0;

        public double AccDec { get; set; } = 0.0;

        public int DwellTime { get; set; } = 0;

        public double StartPosition { get; set; } = 0.0;

        public double EndPosition { get; set; } = 0.0;

        public int RepeatCount { get; set; } = 0;
        #endregion
    }
}
