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

        //private void btnAlgorithmPage_Click(object sender, EventArgs e)
        //{
        //    LineTeachingForm form = new LineTeachingForm();
        //    form.UnitName = UnitName.Unit0;
        //    form.TitleCameraName = "LineScan";
        //    form.AppsLineCamera = AppsLineCameraManager.Instance().GetAppsCamera("AkkonCamera");
        //    form.InspModelService = ATTInspModelService;
        //    form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
        //    form.ShowDialog();
        //}

        private void btnAlignPage_Click(object sender, EventArgs e)
        {
            UpdateAlignPage();
        }

        private void UpdateAlignPage()
        {
            LineTeachingForm form = new LineTeachingForm();
            form.UnitName = UnitName.Unit0;
            form.TitleCameraName = "LineScan";
            form.LineCamera = LineCameraManager.Instance().GetAppsCamera("AlignCamera");
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.ShowDialog();
        }

        private void btnAkkonPage_Click(object sender, EventArgs e)
        {
            UpdateAkkonPage();
        }

        private void UpdateAkkonPage()
        {
            LineTeachingForm form = new LineTeachingForm();
            form.UnitName = UnitName.Unit0;
            form.TitleCameraName = "LineScan";
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
