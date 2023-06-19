using Emgu.CV.CvEnum;
using Emgu.CV;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Framework.Config;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Structure;
using Jastech.Framework.Structure.Service;
using Jastech.Framework.Winform.Forms;
using Jastech.Framework.Winform.VisionPro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Framework.Imaging;
using Cognex.VisionPro;
using Jastech.Framework.Winform.Helper;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.Motions;

namespace Jastech.Apps.Winform.UI.Forms
{
    public partial class PreAlignTeachingForm : Form
    {
        #region 필드
        private Color _selectedColor = new Color();

        private Color _nonSelectedColor = new Color();

        private DisplayType _displayType { get; set; } = DisplayType.PreAlign;
        #endregion

        #region 속성
        public InspModelService InspModelService { get; set; } = null;

        public Unit CurrentUnit { get; private set; } = null;

        public UnitName UnitName { get; set; } = UnitName.Unit0;

        public string TitleCameraName { get; set; } = "";

        private CogTeachingDisplayControl Display { get; set; } = new CogTeachingDisplayControl();

        private PreAlignControl PreAlignControl { get; set; } = new PreAlignControl() { Dock = DockStyle.Fill };

        private VisionCalibrationControl VisionCalibrationControl { get; set; } = new VisionCalibrationControl() { Dock = DockStyle.Fill };

        public string TeachingImagePath { get; set; }

        public AxisHandler AxisHandler { get; set; } = null;

        public AreaCamera AreaCamera { get; set; } = null;
        #endregion

        #region 이벤트
        public OpenMotionPopupDelegate OpenMotionPopupEventHandler;
        #endregion

        #region 델리게이트
        public delegate void OpenMotionPopupDelegate(UnitName unitName);
        #endregion

        #region 생성자
        public PreAlignTeachingForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        #endregion

        private void PreAlignTeachingForm_Load(object sender, EventArgs e)
        {
            TeachingData.Instance().UpdateTeachingData();

            CurrentUnit = TeachingData.Instance().GetUnit(UnitName.ToString());

            AddControl();
            InitializeUI();
            SelectPage(DisplayType.PreAlign);
        }

        private void AddControl()
        {
            // Display Control
            Display = new CogTeachingDisplayControl();
            Display.Dock = DockStyle.Fill;

            //Event 연결
            Display.DeleteEventHandler += Display_DeleteEventHandler;
            pnlDisplay.Controls.Add(Display);

            // TeachingUIManager 참조
            TeachingUIManager.Instance().SetDisplay(Display.GetDisplay());

            if (AreaCamera != null)
                AreaCamera.OnImageGrabbed += AreaCamera_OnImageGrabbed;
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
        }

        private void ClearSelectedButton()
        {
            foreach (Control control in tlpTeachingItems.Controls)
            {
                if (control is Button)
                    control.BackColor = _nonSelectedColor;
            }
        }

        public void UpdateParam()
        {
            var camera = AreaCamera.Camera;
            if (camera == null)
                return;

            lblCameraExposureValue.Text = camera.GetExposureTime().ToString();
            lblCameraGainValue.Text = camera.GetAnalogGain().ToString();
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

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
            Display.DisposeImage();
            PreAlignControl.DisposeImage();
            AreaCamera.OnImageGrabbed -= AreaCamera_OnImageGrabbed;
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

        private void trbDimmingLevelValue_Scroll(object sender, EventArgs e)
        {
            SetLightDimmingLevel(sender);
        }

        private void nudLightDimmingLevel_ValueChanged(object sender, EventArgs e)
        {
            SetLightDimmingLevel(sender);
        }

        private void SetLightDimmingLevel(object sender)
        {
            Control control = sender as Control;

            if (control.GetType().Name.ToLower() == "trackbar")
                nudLightDimmingLevel.Text = trbDimmingLevelValue.Value.ToString();
            else if (control.GetType().Name.ToLower() == "numericupdown")
                trbDimmingLevelValue.Value = Convert.ToInt32(nudLightDimmingLevel.Text);

            // 조명값 넣기
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

        private void btnPreAlign_Click(object sender, EventArgs e)
        {
            SelectPage(DisplayType.PreAlign);
        }

        private void btnCalibration_Click(object sender, EventArgs e)
        {
            SelectPage(DisplayType.Calibration);
        }

        //private void UpdateUI()
        //{
        //    //UpdateLightStatus();
        //    //UpdateCameraStatus();
        //}

        //private void UpdateLightStatus()
        //{
        //    if (true) // 조명 온 상태
        //    {
        //        lblLightOn.BackColor = _selectedColor;
        //        lblLightOff.BackColor = _nonSelectedColor;
        //    }
        //    else
        //    {
        //        lblLightOn.BackColor = _nonSelectedColor;
        //        lblLightOff.BackColor = _selectedColor;
        //    }
        //}

        //private void UpdateCameraStatus()
        //{
        //    var camera = AreaCamera.Camera;

        //    int gg = 0;
        //    lblCameraExposureValue.Text = camera.GetExposureTime().ToString();
        //    lblCameraGainValue.Text = camera.GetAnalogGain().ToString();
        //    //lblCameraGainValue.Text = camera.GetDigitalGain().ToString();
        //}
    }
}
