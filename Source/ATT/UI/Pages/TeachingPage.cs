using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATT.UI.Forms;
using Jastech.Apps.Structure;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Apps.Structure.Data;
using Jastech.Framework.Winform.Forms;
using ATT.Core;
using Jastech.Apps.Winform;

namespace ATT.UI.Pages
{
    public partial class TeachingPage : UserControl
    {
        private ATTInspModelService ATTInspModelService { get; set; } = null;

        public TeachingPage()
        {
            InitializeComponent();
        }

        private void btnModelPage_Click(object sender, EventArgs e)
        {
            LineTeachingForm form = new LineTeachingForm();
            form.UnitName = UnitName.Unit0;
            form.TitleCameraName = "LineScan";
            form.AppsLineCamera = AppsLineCameraManager.Instance().GetAppsCamera("Camera0");
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.ShowDialog();

            //GC.Collect();
        }

        private void btnLinescanSetting_Click(object sender, EventArgs e)
        {
            OpticTeachingForm form = new OpticTeachingForm();
            form.AppsLineCamera = AppsLineCameraManager.Instance().GetAppsCamera("Camera0");
            form.LAFCtrl = AppsLAFManager.Instance().GetLAFCtrl("Akkon");
            form.UnitName = UnitName.Unit0;
            form.AxisHandler = AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.ShowDialog();
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
