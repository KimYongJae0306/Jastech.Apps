using ATT_UT_Remodeling.Core;
using ATT_UT_Remodeling.UI.Forms;
using Jastech.Apps.Structure;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.UI.Forms;
using Jastech.Framework.Device.LightCtrls;
using Jastech.Framework.Winform;
using Jastech.Framework.Winform.Forms;
using System;
using System.Windows.Forms;

namespace ATT_UT_Remodeling.UI.Pages
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
        private void btnPreAlign_Click(object sender, EventArgs e)
        {
            PreAlignSetting();
        }

        private void PreAlignSetting()
        {
            PreAlignTeachingForm form = new PreAlignTeachingForm();
            form.LAFCtrl = LAFManager.Instance().GetLAFCtrl("Laf");
            form.AreaCamera = AreaCameraManager.Instance().GetAppsCamera("PreAlign");
            form.UnitName = UnitName.Unit0;
            form.AxisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
            form.TitleCameraName = "PreAlign";
            form.CalibLightParameter = CreateCalibrationLightParam();
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.ShowDialog();
        }

        private LightParameter CreateCalibrationLightParam()
        {
            // PreAlign 사용할 경우 작성
            LightParameter calibLightParameter = new LightParameter("Calibration");

            var lightCtrlHandler = DeviceManager.Instance().LightCtrlHandler;
            var spotLightCtrl = lightCtrlHandler.Get("Spot");

            calibLightParameter.Add(spotLightCtrl, new LightValue(spotLightCtrl.TotalChannelCount));

            return calibLightParameter;
        }

        private void btnAlgorithm_Click(object sender, EventArgs e)
        {
            InspectionTeachingForm form = new InspectionTeachingForm();
            form.UnitName = UnitName.Unit0;
            form.TitleCameraName = "LineCamera";
            form.LineCamera = LineCameraManager.Instance().GetAppsCamera("LineCamera");
            form.LAFCtrl = LAFManager.Instance().GetLAFCtrl("Laf");
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.ShowDialog();
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

        private void btnLinescanSetting_Click(object sender, EventArgs e)
        {
            if (ModelManager.Instance().CurrentModel == null)
                return;

            OpticTeachingForm form = new OpticTeachingForm();
            form.LineCamera = LineCameraManager.Instance().GetAppsCamera("LineCamera");
            form.LAFCtrl = LAFManager.Instance().GetLAFCtrl("Laf");
            form.UnitName = UnitName.Unit0;
            form.AxisNameZ = Jastech.Framework.Device.Motions.AxisName.Z0;
            form.AxisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
            form.LineCameraDataName = "Akkon";
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.ShowDialog();
        }
        #endregion
    }
}
