using ATT_UT_IPAD.Core;
using ATT_UT_IPAD.UI.Forms;
using Jastech.Apps.Structure;
using Jastech.Apps.Winform;
using Jastech.Framework.Winform.Forms;
using System;
using System.Windows.Forms;

namespace ATT_UT_IPAD.UI.Pages
{
    public partial class TeachingPage : UserControl
    {
        #region 필드
        private ATTInspModelService ATTInspModelService { get; set; } = null;

        private MotionPopupForm _motionPopupForm { get; set; } = null;
        #endregion

        #region 속성
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
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
            AlignCameraSetting();
        }

        private void AlignCameraSetting()
        {
            OpticTeachingForm form = new OpticTeachingForm();
            form.LineCamera = LineCameraManager.Instance().GetAppsCamera("AlignCamera");
            form.LAFCtrl = LAFManager.Instance().GetLAFCtrl("Align");
            form.UnitName = UnitName.Unit0;
            form.AxisNameZ = Jastech.Framework.Device.Motions.AxisName.Z0;
            form.AxisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
            form.LineCameraDataName = "Align";
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.ShowDialog();
        }

        private void btnAkkonCameraSetting_Click(object sender, EventArgs e)
        {
            AkkonCameraSetting();
        }

        private void AkkonCameraSetting()
        {
            OpticTeachingForm form = new OpticTeachingForm();
            form.LineCamera = LineCameraManager.Instance().GetAppsCamera("AkkonCamera");
            form.LAFCtrl = LAFManager.Instance().GetLAFCtrl("Akkon");
            form.UnitName = UnitName.Unit0;
            form.AxisNameZ = Jastech.Framework.Device.Motions.AxisName.Z1;
            form.AxisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
            form.LineCameraDataName = "Akkon";
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.ShowDialog();
        }

        private void btnAlignInspectionPage_Click(object sender, EventArgs e)
        {
            ShowAlignInspectionSettingPage();
        }

        private void ShowAlignInspectionSettingPage()
        {
            InspectionTeachingForm form = new InspectionTeachingForm();
            form.UnitName = UnitName.Unit0;
            form.TitleCameraName = "AlignCamera";
            form.LineCamera = LineCameraManager.Instance().GetAppsCamera("AlignCamera");
            form.LAFCtrl = LAFManager.Instance().GetLAFCtrl("Align");
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.ShowDialog();
        }

        private void btnAkkonInspectionPage_Click(object sender, EventArgs e)
        {
            ShowAkkonInspectionSettingPage();
        }

        private void ShowAkkonInspectionSettingPage()
        {
            InspectionTeachingForm form = new InspectionTeachingForm();
            form.UnitName = UnitName.Unit0;
            form.TitleCameraName = "AkkonCamera";
            form.LineCamera = LineCameraManager.Instance().GetAppsCamera("AkkonCamera");
            form.LAFCtrl = LAFManager.Instance().GetLAFCtrl("Akkon");
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.ShowDialog();
        }

        private void btnLinescanSetting_Click(object sender, EventArgs e)
        {
            
        }
        

        private void OpenMotionPopupEventHandler(UnitName unitName)
        {
            //MotionPopupForm motionPopupForm = new MotionPopupForm();
            //motionPopupForm.UnitName = unitName;
            //motionPopupForm.InspModelService = ATTInspModelService;
            //motionPopupForm.Show();
            if (_motionPopupForm == null)
            {
                _motionPopupForm = new MotionPopupForm();
                _motionPopupForm.UnitName = unitName;
                _motionPopupForm.InspModelService = ATTInspModelService;
                _motionPopupForm.CloseEventDelegate = () => _motionPopupForm = null;
                _motionPopupForm.Show();
            }
            else
                _motionPopupForm.Focus();
        }

        internal void SetInspModelService(ATTInspModelService inspModelService)
        {
            ATTInspModelService = inspModelService;
        }
        #endregion
    }
}
