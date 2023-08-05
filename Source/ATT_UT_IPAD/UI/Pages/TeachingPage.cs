using ATT_UT_IPAD.Core;
using ATT_UT_IPAD.UI.Forms;
using Jastech.Apps.Structure;
using Jastech.Apps.Winform;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Winform.Forms;
using System;
using System.Windows.Forms;

namespace ATT_UT_IPAD.UI.Pages
{
    public partial class TeachingPage : UserControl
    {
        #region 속성
        private ATTInspModelService ATTInspModelService { get; set; } = null;

        private MotionPopupForm MotionPopupForm { get; set; } = null;
        #endregion

        #region 생성자
        public TeachingPage()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void btnAlignCameraSetting_Click(object sender, EventArgs e)
        {
            if (LAFManager.Instance().GetLAFCtrl("AkkonLaf") is LAFCtrl lafCtrl)
                lafCtrl.SetTrackingOnOFF(false);

            OpticTeachingForm form = new OpticTeachingForm();
            form.LineCamera = LineCameraManager.Instance().GetLineCamera("AlignCamera");
            form.LAFCtrl = LAFManager.Instance().GetLAFCtrl("AlignLaf");
            form.UnitName = UnitName.Unit0;
            form.AxisNameZ = Jastech.Framework.Device.Motions.AxisName.Z0;
            form.AxisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.ShowDialog();
        }

        private void btnAkkonCameraSetting_Click(object sender, EventArgs e)
        {
            if (LAFManager.Instance().GetLAFCtrl("AlignLaf") is LAFCtrl lafCtrl)
                lafCtrl.SetTrackingOnOFF(false);

            OpticTeachingForm form = new OpticTeachingForm();
            form.LineCamera = LineCameraManager.Instance().GetLineCamera("AkkonCamera");
            form.LAFCtrl = LAFManager.Instance().GetLAFCtrl("AkkonLaf");
            form.UnitName = UnitName.Unit0;
            form.AxisNameZ = Jastech.Framework.Device.Motions.AxisName.Z1;
            form.AxisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.ShowDialog();
        }

        private void btnAlignInspectionPage_Click(object sender, EventArgs e)
        {
            InspectionTeachingForm form = new InspectionTeachingForm();
            form.UnitName = UnitName.Unit0;
            form.TitleCameraName = "AlignCamera";
            form.LineCamera = LineCameraManager.Instance().GetLineCamera("AlignCamera");
            form.UseDelayStart = true;
            form.LAFCtrl = LAFManager.Instance().GetLAFCtrl("AlignLaf");
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.ShowDialog();
        }

        private void btnAkkonInspectionPage_Click(object sender, EventArgs e)
        {
            InspectionTeachingForm form = new InspectionTeachingForm();
            form.UnitName = UnitName.Unit0;
            form.TitleCameraName = "AkkonCamera";
            form.LineCamera = LineCameraManager.Instance().GetLineCamera("AkkonCamera");
            form.LAFCtrl = LAFManager.Instance().GetLAFCtrl("AkkonLaf");
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.ShowDialog();
        }

        private void OpenMotionPopupEventHandler(UnitName unitName)
        {
            if (MotionPopupForm == null)
            {
                MotionPopupForm = new MotionPopupForm();
                MotionPopupForm.UnitName = unitName;
                MotionPopupForm.AxisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
                MotionPopupForm.AkkonLafCtrl = LAFManager.Instance().GetLAFCtrl("AkkonLaf");
                MotionPopupForm.AlignLafCtrl = LAFManager.Instance().GetLAFCtrl("AlignLaf");
                MotionPopupForm.InspModelService = ATTInspModelService;
                MotionPopupForm.CloseEventDelegate = () => MotionPopupForm = null;
                MotionPopupForm.Show();
            }
            else
                MotionPopupForm.Focus();
        }

        internal void SetInspModelService(ATTInspModelService inspModelService)
        {
            ATTInspModelService = inspModelService;
        }
        #endregion
    }
}
