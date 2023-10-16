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
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
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
