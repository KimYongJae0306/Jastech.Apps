using Emgu.CV;
using Emgu.CV.CvEnum;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Framework.Config;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Device.LightCtrls;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Structure;
using Jastech.Framework.Structure.Service;
using Jastech.Framework.Winform;
using Jastech.Framework.Winform.Forms;
using Jastech.Framework.Winform.Helper;
using Jastech.Framework.Winform.VisionPro.Controls;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Forms
{
    public partial class PreAlignTeachingForm : Form
    {
        #region 필드
        private Color _selectedColor = new Color();

        private Color _nonSelectedColor = new Color();

        private DisplayType _displayType { get; set; } = DisplayType.PreAlign;

        private MarkDirection _curMark { get; set; }
        #endregion

        #region 속성
        public InspModelService InspModelService { get; set; } = null;

        public Unit CurrentUnit { get; private set; } = null;

        public UnitName UnitName { get; set; } = UnitName.Unit0;

        public string TitleCameraName { get; set; } = "";

        private CogTeachingDisplayControl Display { get; set; } = null;

        private PreAlignControl PreAlignControl { get; set; } = null;

        private VisionCalibrationControl VisionCalibrationControl { get; set; } = null;

        private LightControl LightControl { get; set; } = null;

        public string TeachingImagePath { get; set; }

        public AxisHandler AxisHandler { get; set; } = null;

        public AreaCamera AreaCamera { get; set; } = null;

        public LAFCtrl LAFCtrl { get; set; } = null;

        public LightParameter CalibLightParameter { get; set; } = null;
        #endregion

        #region 이벤트
        public MotionPopupDelegate OpenMotionPopupEventHandler;

        public MotionPopupDelegate CloseMotionPopupEventHandler;
        #endregion

        #region 델리게이트
        public delegate void MotionPopupDelegate(UnitName unitName);
        #endregion

        #region 생성자
        public PreAlignTeachingForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void PreAlignTeachingForm_Load(object sender, EventArgs e)
        {
            TeachingData.Instance().UpdateTeachingData();
            CurrentUnit = TeachingData.Instance().GetUnit(UnitName.ToString());

            AddControl();
            InitializeUI();

            // TeachingUIManager 참조
            TeachingUIManager.Instance().TeachingDisplayControl = Display;
            AreaCamera.OnImageGrabbed += AreaCamera_OnImageGrabbed;
          
            SelectPage(DisplayType.PreAlign);
        }

        private void AddControl()
        {
            Display = new CogTeachingDisplayControl();
            if (AreaCamera != null)
            {
                var camera = AreaCamera.Camera;
                Display.PixelResolution = camera.PixelResolution_um / camera.LensScale;
            }

            Display.Dock = DockStyle.Fill;
            Display.DeleteEventHandler += Display_DeleteEventHandler;
            pnlDisplay.Controls.Add(Display);

            LightControl = new LightControl();
            LightControl.Dock = DockStyle.Fill;
            LightControl.SetParam(DeviceManager.Instance().LightCtrlHandler, CurrentUnit.PreAlign.LeftLightParam);
            pnlLight.Controls.Add(LightControl);
         
            PreAlignControl = new PreAlignControl();
            PreAlignControl.Dock = DockStyle.Fill;
            PreAlignControl.MarkDirectionChanged += MarkDirectionChangedEvent;
            PreAlignControl.AreaCameraGrabEventHandler += AreaCameraGrabEvent;
            pnlTeach.Controls.Add(PreAlignControl);

            VisionCalibrationControl = new VisionCalibrationControl();
            VisionCalibrationControl.AreaCameraGrabEventHandler += AreaCameraGrabEvent;
            VisionCalibrationControl.Dock = DockStyle.Fill;
            pnlTeach.Controls.Add(VisionCalibrationControl);
        }

        private void AreaCamera_OnImageGrabbed(Camera camera)
        {
            byte[] byteData = camera.GetGrabbedImage();
            if (byteData == null)
                return;

            var image = VisionProImageHelper.ConvertImage(byteData, camera.ImageWidth, camera.ImageHeight, camera.ColorFormat);

            Display.SetImage(image);
        }

        private void Display_DeleteEventHandler(object sender, EventArgs e)
        {
            if (pnlTeach.Controls.Count > 0)
            {
                if (pnlTeach.Controls[0] as MarkControl != null)
                    PreAlignControl.DrawROI();
            }
        }

        private void SelectPage(DisplayType type)
        {
            if (ModelManager.Instance().CurrentModel == null || UnitName.ToString() == "")
                return;

            ClearSelectedButton();

            _displayType = type;

            pnlTeach.Controls.Clear();

            switch (type)
            {
                case DisplayType.PreAlign:
                    btnPreAlign.BackColor = _selectedColor;
                    PreAlignControl.SetParams(CurrentUnit);
                    
                    pnlTeach.Controls.Add(PreAlignControl);
                    break;

                case DisplayType.Calibration:
                    btnCalibration.BackColor = _selectedColor;
                    VisionCalibrationControl.SetParams(CurrentUnit);
                    VisionCalibrationControl.SetAxisHandler(AxisHandler);
                    VisionCalibrationControl.SetAreaCamera(AreaCamera);
                    pnlTeach.Controls.Add(VisionCalibrationControl);
                    break;
            }

            UpdateLightParam();
        }

        private void AreaCameraGrabEvent(bool isGrabStart)
        {
            if (isGrabStart)
                AreaCamera.StartGrabContinous();
            else
                AreaCamera.StopGrab();
        }

        private void MarkDirectionChangedEvent(MarkDirection direction)
        {
            if (_curMark == direction)
                return;

            _curMark = direction;
            UpdateLightParam();
        }

        private void UpdateLightParam()
        {
            if (btnPreAlign.BackColor == _selectedColor)
            {
                if (_curMark == MarkDirection.Left)
                    LightControl.UpdateSetParam(CurrentUnit.PreAlign.LeftLightParam);
                else if (_curMark == MarkDirection.Right)
                    LightControl.UpdateSetParam(CurrentUnit.PreAlign.RightLightParam);
            }

            if (btnCalibration.BackColor == _selectedColor)
                LightControl.UpdateSetParam(CalibLightParameter);
        }

        private void ClearSelectedButton()
        {
            foreach (Control control in tlpTeachingItems.Controls)
            {
                if (control is Button)
                    control.BackColor = _nonSelectedColor;
            }
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(34, 34, 34);

            lblStageCam.Text = $"STAGE : {UnitName} / CAM : {TitleCameraName}";
        }

        private void btnMotionPopup_Click(object sender, EventArgs e)
        {
            OpenMotionPopupEventHandler?.Invoke(UnitName);
        }

        private void btnGrabStart_Click(object sender, EventArgs e)
        {
            AreaCamera.StartGrabContinous();
        }

        private void btnGrabStop_Click(object sender, EventArgs e)
        {
            AreaCamera.StopGrab();
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            var camera = AreaCamera.Camera;
            if (camera.IsGrabbing())
                camera.Stop();

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ReadOnlyChecked = true;
            dlg.Filter = "BMP Files (*.bmp)|*.bmp";
            dlg.ShowDialog();

            if (dlg.FileName != "")
            {
                Mat image = new Mat(dlg.FileName, ImreadModes.Grayscale);

                int size = image.Width * image.Height * image.NumberOfChannels;
                byte[] dataArray = new byte[size];
                Marshal.Copy(image.DataPointer, dataArray, 0, size);

                ColorFormat format = image.NumberOfChannels == 1 ? ColorFormat.Gray : ColorFormat.RGB24;

                var cogImage = VisionProImageHelper.ConvertImage(dataArray, image.Width, image.Height, format);

                TeachingUIManager.Instance().SetOrginCogImageBuffer(cogImage);
                TeachingUIManager.Instance().SetOriginMatImageBuffer(new Mat(dlg.FileName, ImreadModes.Grayscale));
                Display.SetImage(TeachingUIManager.Instance().GetOriginCogImageBuffer(false));
            }
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
            model.SetUnitList(TeachingData.Instance().UnitList);

            string fileName = Path.Combine(ConfigSet.Instance().Path.Model, model.Name, InspModel.FileName);
            InspModelService?.Save(fileName, model);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PreAlignTeachingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AreaCamera.StopGrab();
            AreaCamera.OnImageGrabbed -= AreaCamera_OnImageGrabbed;
            PreAlignControl.AreaCameraGrabEventHandler -= AreaCameraGrabEvent;
            VisionCalibrationControl.AreaCameraGrabEventHandler -= AreaCameraGrabEvent;

            Display.DisposeImage();
            PreAlignControl.DisposeImage();
        }

        private void lblCameraExposureValue_Click(object sender, EventArgs e)
        {
            int exposureTime = 0;

            exposureTime = KeyPadHelper.SetLabelIntegerData((Label)sender);

            var camera = AreaCamera.Camera;
            camera.SetExposureTime(exposureTime);
        }

        private void lblCameraGainValue_Click(object sender, EventArgs e)
        {
            int gain = KeyPadHelper.SetLabelIntegerData((Label)sender);

            var camera = AreaCamera.Camera;

            // 하이크는 둘 중 무엇
            //camera.SetAnalogGain(gain);
            //camera.SetDigitalGain(gain);
        }

        private void btnPreAlign_Click(object sender, EventArgs e)
        {
            SelectPage(DisplayType.PreAlign);
        }

        private void btnCalibration_Click(object sender, EventArgs e)
        {
            SelectPage(DisplayType.Calibration);
        }
        #endregion
    }
}
