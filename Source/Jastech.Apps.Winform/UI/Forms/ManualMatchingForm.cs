using Jastech.Apps.Winform.UI.Controls;
using Jastech.Framework.Winform.VisionPro.Controls;
using Jastech.Framework.Winform;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Winform.Controls;
using Jastech.Framework.Winform.Helper;
using Cognex.VisionPro;
using static Jastech.Framework.Device.Motions.AxisMovingParam;
using Jastech.Framework.Winform.VisionPro.Helper;
using Cognex.VisionPro.Display;

namespace Jastech.Apps.Winform.UI.Forms
{
    public partial class ManualMatchingForm : Form
    {
        #region 필드
        private int _pitch = 0;
        #endregion

        #region 속성
        private CogTeachingDisplayControl Display { get; set; } = null;

        private LightControl LightControl { get; set; } = null;

        public AreaCamera AreaCamera { get; set; } = null;

        public LAFCtrl LAFCtrl { get; set; } = null;

        public Unit CurrentUnit { get; private set; } = null;

        public UnitName UnitName { get; set; } = UnitName.Unit0;

        public MarkDirection MarkDirection { get; set; } = MarkDirection.Left;

        public MarkName MarkName { get; set; } = MarkName.Main;
        #endregion

        #region 이벤트
        public GetOriginImageDelegate GetOriginImageHandler;
        #endregion

        #region 델리게이트
        public delegate ICogImage GetOriginImageDelegate();
        #endregion

        #region 생성자
        public ManualMatchingForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void ManualMatchingForm_Load(object sender, EventArgs e)
        {
            TeachingData.Instance().UpdateTeachingData();
            CurrentUnit = TeachingData.Instance().GetUnit(UnitName.ToString());

            SetPitch(Convert.ToInt16(lblPitch.Text));
            AddControls();

            // TeachingUIManager 참조
            TeachingUIManager.Instance().TeachingDisplayControl = Display;
            AreaCamera.OnImageGrabbed += AreaCamera_OnImageGrabbed;

            LoadPatternImage();
        }

        private void SetPitch(int pitch)
        {
            _pitch = pitch;
        }

        private void AddControls()
        {
            Display = new CogTeachingDisplayControl();
            Display.Dock = DockStyle.Fill;
            Display.DeleteEventHandler += Display_DeleteEventHandler;
            pnlDisplay.Controls.Add(Display);

            LightControl = new LightControl();
            LightControl.Dock = DockStyle.Fill;
            LightControl.SetParam(DeviceManager.Instance().LightCtrlHandler, CurrentUnit.PreAlign.LeftLightParam);
            pnlLight.Controls.Add(LightControl);
        }

        private void AreaCamera_OnImageGrabbed(Camera camera)
        {
            byte[] byteData = camera.GetGrabbedImage();
            if (byteData == null)
                return;

            var image = VisionProImageHelper.ConvertImage(byteData, camera.ImageWidth, camera.ImageHeight, camera.ColorFormat);

            Display.SetImage(image);
        }

        private void LoadPatternImage()
        {
            if (GetOriginImage() != null)
            {
                CogDisplayHelper.DisposeDisplay(cogPatternDisplay);

                ICogImage originImage = GetOriginImageHandler();

                var preAlignMarkParam = CurrentUnit.PreAlign.AlignParamList.Where(x => x.Direction == MarkDirection && x.Name == MarkName).FirstOrDefault().InspParam;
                if (preAlignMarkParam.Train(originImage))
                {
                    DisposePatternDisplay();
                    cogPatternDisplay.Image = preAlignMarkParam.GetTrainedPatternImage();
                    cogPatternDisplay.StaticGraphics.Add(preAlignMarkParam.GetOrigin() as ICogGraphic, "Origin");
                }
            }
        }

        private ICogImage GetOriginImage()
        {
            if (GetOriginImageHandler != null)
            {
                ICogImage originImage = GetOriginImageHandler();
                return originImage;
            }

            return null;
        }

        private void DisposePatternDisplay()
        {
            CogDisplayHelper.DisposeDisplay(cogPatternDisplay);
            cogPatternDisplay.Image = null;
        }

        private void Display_DeleteEventHandler(object sender, EventArgs e)
        {

        }

        private void lblPitch_Click(object sender, EventArgs e)
        {
            int pitch = KeyPadHelper.SetLabelIntegerData((Label)sender);
            SetPitch(pitch);
        }

        private void lblMoveUp_Click(object sender, EventArgs e)
        {

        }

        private void lblMoveLeft_Click(object sender, EventArgs e)
        {

        }

        private void lblMoveRight_Click(object sender, EventArgs e)
        {

        }

        private void lblMoveDown_Click(object sender, EventArgs e)
        {

        }

        private void lblApply_Click(object sender, EventArgs e)
        {

        }

        private void lblCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
