using ATT.Core;
using ATT.UI.Forms;
using Jastech.Apps.Structure;
using Jastech.Apps.Winform;
using Jastech.Framework.Config;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Winform.Forms;
using System;
using System.Windows.Forms;

namespace ATT.UI.Pages
{
    public partial class TeachingPage : UserControl
    {
        #region 필드
        #endregion

        #region 속성
        private ATTInspModelService ATTInspModelService { get; set; } = null;

        private MotionPopupForm MotionPopupForm { get; set; } = null;
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
            OpticTeachingForm form = new OpticTeachingForm();
            form.LineCamera = LineCameraManager.Instance().GetLineCamera("AlignCamera");
            form.LAFCtrl = LAFManager.Instance().GetLAF("AlignLaf").LafCtrl;
            form.UnitName = UnitName.Unit0;
            form.AxisNameZ = Jastech.Framework.Device.Motions.AxisName.Z1;
            form.AxisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.CloseMotionPopupEventHandler += CloseMotionPopupEventHandler;
            form.ShowDialog();
        }

        private void CloseMotionPopupEventHandler(UnitName unitName)
        {
            MotionPopupForm?.Close();
        }

        private void btnAkkonCameraSetting_Click(object sender, EventArgs e)
        {
            OpticTeachingForm form = new OpticTeachingForm();
            form.LineCamera = LineCameraManager.Instance().GetLineCamera("AkkonCamera");
            form.LAFCtrl = LAFManager.Instance().GetLAF("AkkonLaf").LafCtrl;
            form.UnitName = UnitName.Unit0;
            form.AxisNameZ = Jastech.Framework.Device.Motions.AxisName.Z0;
            form.AxisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.CloseMotionPopupEventHandler += CloseMotionPopupEventHandler;
            form.ShowDialog();
        }

        private void btnAlignInspectionPage_Click(object sender, EventArgs e)
        {
            InspectionTeachingForm form = new InspectionTeachingForm();
            form.UnitName = UnitName.Unit0;
            form.LineCamera = LineCameraManager.Instance().GetLineCamera("AlignCamera");
            form.UseDelayStart = true;
            form.LAFCtrl = LAFManager.Instance().GetLAF("AlignLaf").LafCtrl;
            form.UseAlignMark = true;
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.ShowDialog();
        }

        private void btnAkkonInspectionPage_Click(object sender, EventArgs e)
        {
            InspectionTeachingForm form = new InspectionTeachingForm();
            form.UnitName = UnitName.Unit0;
            form.LineCamera = LineCameraManager.Instance().GetLineCamera("AkkonCamera");
            form.LAFCtrl = LAFManager.Instance().GetLAF("AkkonLaf").LafCtrl;
            form.UseAlignMark = false;
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
                MotionPopupForm.LafCtrl = LAFManager.Instance().GetLAF("AkkonLaf").LafCtrl;
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
