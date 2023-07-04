using ATT.Core;
using ATT.UI.Forms;
using Jastech.Apps.Structure;
using Jastech.Apps.Winform;
using Jastech.Framework.Config;
using Jastech.Framework.Winform.Forms;
using System;
using System.Windows.Forms;

namespace ATT.UI.Pages
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
        private void btnModelPage_Click(object sender, EventArgs e)
        {
            InspectionTeachingForm form = new InspectionTeachingForm();
            form.UnitName = UnitName.Unit0;
            form.TitleCameraName = "LineScan";
            form.LineCamera = LineCameraManager.Instance().GetAppsCamera("Camera0");
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.ShowDialog();

            //GC.Collect();
        }

        private void btnLinescanSetting_Click(object sender, EventArgs e)
        {
            OpticTeachingForm form = new OpticTeachingForm();

            if(ConfigSet.Instance().Operation.VirtualMode)
            {
                form.LineCamera = LineCameraManager.Instance().GetAppsCamera("Camera0");
                form.LAFCtrl = LAFManager.Instance().GetLAFCtrl("Akkon");
            }
            else
            {
                form.LineCamera = LineCameraManager.Instance().GetAppsCamera("Camera0");
                form.LAFCtrl = LAFManager.Instance().GetLAFCtrl("Akkon");
            }
            form.UnitName = UnitName.Unit0;
            form.AxisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
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
        #endregion
    }
}
