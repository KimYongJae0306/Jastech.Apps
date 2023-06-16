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
using ATT_UT_Remodeling.Core;
using ATT_UT_Remodeling.UI.Forms;
using Jastech.Apps.Winform.UI.Forms;

namespace ATT_UT_Remodeling.UI.Pages
{
    public partial class TeachingPage : UserControl
    {
        private ATTInspModelService ATTInspModelService { get; set; } = null;

        public TeachingPage()
        {
            InitializeComponent();
        }

        private void btnPreAlign_Click(object sender, EventArgs e)
        {
            PreAlignSetting();
        }

        private void PreAlignSetting()
        {
            PreAlignTeachingForm form = new PreAlignTeachingForm();
            form.AreaCamera = AreaCameraManager.Instance().GetAppsCamera("PreAlign");
            //form.LAFCtrl = LAFManager.Instance().GetLAFCtrl("Laf");
            form.UnitName = UnitName.Unit0;
            form.AxisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
            form.TitleCameraName = "PreAlign";
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.ShowDialog();
        }

        private void btnAlgorithm_Click(object sender, EventArgs e)
        {
            InspectionTeachingForm form = new InspectionTeachingForm();
            form.UnitName = UnitName.Unit0;
            form.TitleCameraName = "LineCamera";
            form.LineCamera = LineCameraManager.Instance().GetAppsCamera("LineCamera");
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

        private void btnLinescanSetting_Click(object sender, EventArgs e)
        {
            OpticTeachingForm form = new OpticTeachingForm();
            form.LineCamera = LineCameraManager.Instance().GetAppsCamera("LineCamera");
            form.LAFCtrl = LAFManager.Instance().GetLAFCtrl("Laf");
            form.UnitName = UnitName.Unit0;
            form.AxisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.ShowDialog();
        }
    }
}
