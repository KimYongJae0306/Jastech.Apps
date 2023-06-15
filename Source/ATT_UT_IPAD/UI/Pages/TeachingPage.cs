using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure;
using Jastech.Framework.Winform.Forms;
using Jastech.Apps.Winform;
using ATT_UT_IPAD.UI.Forms;
using ATT_UT_IPAD.Core;

namespace ATT_UT_IPAD.UI.Pages
{
    public partial class TeachingPage : UserControl
    {
        private ATTInspModelService ATTInspModelService { get; set; } = null;

        public TeachingPage()
        {
            InitializeComponent();
        }

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
            form.AxisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
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
            form.AxisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
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
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.ShowDialog();
        }

        private void btnLinescanSetting_Click(object sender, EventArgs e)
        {
            
        }

        

        private void OpenMotionPopupEventHandler(UnitName unitName)
        {
            MotionPopupForm motionPopupForm = new MotionPopupForm();
            motionPopupForm.UnitName = unitName;
            motionPopupForm.InspModelService = ATTInspModelService;
            motionPopupForm.Show();
        }

        internal void SetInspModelService(ATTInspModelService inspModelService)
        {
            ATTInspModelService = inspModelService;
        }

        
    }
}
