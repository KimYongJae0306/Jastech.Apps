using ATT_UT_Remodeling.Core;
using ATT_UT_Remodeling.UI.Forms;
using Cognex.VisionPro;
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
        #endregion

        #region 속성
        private ATTInspModelService ATTInspModelService { get; set; } = null;

        private MotionPopupForm MotionPopupForm { get; set; } = null;

        public ICogImage ScanImage { get; set; } = null;
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
            form.LAFCtrl = LAFManager.Instance().GetLAF("Laf").LafCtrl;
            form.AreaCamera = AreaCameraManager.Instance().GetAppsCamera("PreAlign");
            form.UnitName = UnitName.Unit0;
            form.AxisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
            form.TitleCameraName = "PreAlign";
            form.CalibLightParameter = CreateCalibrationLightParam();
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.CloseMotionPopupEventHandler += CloseMotionPopupEventHandler;
            form.ShowDialog();
        }

        private void CloseMotionPopupEventHandler(UnitName unitName)
        {
            MotionPopupForm?.Close();
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

        private void btnInspection_Click(object sender, EventArgs e)
        {
            InspectionTeachingForm form = new InspectionTeachingForm();
            form.ScanImage = ScanImage?.CopyBase(CogImageCopyModeConstants.CopyPixels);
            form.UnitName = UnitName.Unit0;
            form.LineCamera = LineCameraManager.Instance().GetLineCamera("LineCamera");
            form.LAFCtrl = LAFManager.Instance().GetLAF("Laf").LafCtrl;
            form.UseAlignCamMark = false;
            form.UseAlignTeaching = true;
            form.UseAkkonTeaching = true;
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.CloseMotionPopupEventHandler += CloseMotionPopupEventHandler;
            form.ShowDialog();
        }

        private void OpenMotionPopupEventHandler(UnitName unitName)
        {
            if (MotionPopupForm == null)
            {
                MotionPopupForm = new MotionPopupForm();
                MotionPopupForm.UnitName = unitName;
                MotionPopupForm.AxisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
                MotionPopupForm.LafCtrl = LAFManager.Instance().GetLAF("Laf").LafCtrl;
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

        private void btnLinescanSetting_Click(object sender, EventArgs e)
        {
            if (LAFManager.Instance().GetLAF("Laf") is LAF laf)
                laf.LafCtrl.SetTrackingOnOFF(false);

            OpticTeachingForm form = new OpticTeachingForm();
            form.LineCamera = LineCameraManager.Instance().GetLineCamera("LineCamera");
            form.LAFCtrl = LAFManager.Instance().GetLAF("Laf").LafCtrl;
            form.UnitName = UnitName.Unit0;
            form.AxisNameZ = Jastech.Framework.Device.Motions.AxisName.Z0;
            form.AxisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.CloseMotionPopupEventHandler += CloseMotionPopupEventHandler;
            form.ShowDialog();
        }
        #endregion
    }
}
