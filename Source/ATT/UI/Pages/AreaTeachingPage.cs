using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Framework.Winform.VisionPro.Controls;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Framework.Structure;
using Jastech.Framework.Imaging.VisionPro;
using Cognex.VisionPro;
using Jastech.Apps.Winform;
using Jastech.Apps.Structure;
using Jastech.Framework.Util.Helper;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Winform.Forms;
using Jastech.Apps.Winform.UI.Forms;
using ATT.UI.Forms;
using Jastech.Apps.Winform.Core;

namespace ATT.UI.Pages
{
    public partial class AreaTeachingPage : UserControl
    {
        #region 필드
        private string _prevName { get; set; } = "";
        #endregion

        #region 속성
        public string UnitName { get; set; } = "";
        // Display
        private CogTeachingDisplayControl Display { get; set; } = new CogTeachingDisplayControl();
       
        // Teach Controls
        private PreAlignControl PreAlignControl { get; set; } = new PreAlignControl();

        // Control List
        private List<UserControl> TeachControlList = null;
        #endregion

        public AreaTeachingPage()
        {
            InitializeComponent();
        }

        private void TeachingPage_Load(object sender, EventArgs e)
        {
            AddControl();
            SelectPreAlign();
        }

        private void AddControl()
        {
            // Display Control
            Display = new CogTeachingDisplayControl();
            Display.Dock = DockStyle.Fill;

            //Event 연결
            Display.DeleteEventHandler += Display_DeleteEventHandler;
            pnlDisplay.Controls.Add(Display);

            // TeachingUIManager 참조
            AppsTeachingUIManager.Instance().SetDisplay(Display.GetDisplay());

            // Teach Control List
            TeachControlList = new List<UserControl>();
            TeachControlList.Add(PreAlignControl);
        }

        private void Display_DeleteEventHandler(object sender, EventArgs e)
        {
            PreAlignControl.DrawROI();
        }

        private void btnLinescan_Click(object sender, EventArgs e)
        {
        }

        private void btnPreAlign_Click(object sender, EventArgs e)
        {
            SelectPreAlign();
        }

        public void UpdateSelectPage()
        {
            SelectPreAlign();
        }

        private void SelectPreAlign()
        {
            if (ModelManager.Instance().CurrentModel == null || UnitName == "")
                return;

            btnPreAlign.ForeColor = Color.Blue;
            pnlTeach.Controls.Add(PreAlignControl);

            var preAlignParams = SystemManager.Instance().GetTeachingData().GetPreAlign(UnitName);
            PreAlignControl.SetParams(preAlignParams);
        }

        private void SetSelectTeachPage(UserControl selectedControl)
        {
            foreach (UserControl control in TeachControlList)
                control.Visible = false;

            selectedControl.Visible = true;
            selectedControl.Dock = DockStyle.Fill;
            pnlTeach.Controls.Add(selectedControl);
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ReadOnlyChecked = true;
            dlg.Filter = "Bmp File | *.bmp";
            dlg.ShowDialog();

            if (dlg.FileName != "")
            {
                ICogImage cogImage = CogImageHelper.Load(dlg.FileName);
                Display.SetImage(cogImage);
                AppsTeachingUIManager.Instance().SetImage(cogImage);
                PreAlignControl.DrawROI();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AppsInspModel model = ModelManager.Instance().CurrentModel as AppsInspModel;

            if (model == null)
                return;

            SaveModelData(model);
        }

        private void SaveModelData(AppsInspModel model)
        {
            model.SetUnitList(SystemManager.Instance().GetTeachingData().UnitList);

            string fileName = System.IO.Path.Combine(AppConfig.Instance().Path.Model, model.Name, InspModel.FileName);
            SystemManager.Instance().SaveModel(fileName, model);
        }

        private void btnMotionPopup_Click(object sender, EventArgs e)
        {
            MotionPopupForm motionPopupForm = new MotionPopupForm();
            motionPopupForm.SetAxisHandler(AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Unit0));
            motionPopupForm.ShowDialog();
        }
    }
}
