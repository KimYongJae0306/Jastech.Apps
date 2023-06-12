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

        private void btnAlgorithmPage_Click(object sender, EventArgs e)
        {
            LineTeachingForm form = new LineTeachingForm();
            form.UnitName = UnitName.Unit0;
            form.TitleCameraName = "LineScan";
            form.CameraName = CameraName.LinscanMIL0;
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.ShowDialog();
        }

        private void btnAreascanSetting_Click(object sender, EventArgs e)
        {

        }

        private void btnLinescanSetting_Click(object sender, EventArgs e)
        {
            OpticTeachingForm form = new OpticTeachingForm();
            form.CameraName = CameraName.LinscanMIL0;
            form.LafName = LAFName.Akkon;
            form.UnitName = UnitName.Unit0;
            form.AxisHandlerName = AxisHandlerName.Handler0;
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
