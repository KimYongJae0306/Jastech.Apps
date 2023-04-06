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
using Jastech.Apps.Structure;
using Jastech.Framework.Winform.Forms;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.UI.Forms;
using ATT.UI.Forms;
using Cognex.VisionPro;
using Jastech.Framework.Imaging.VisionPro;
using ATT.Core;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Structure;

namespace ATT.UI.Pages
{
    public partial class LineTeachingPage : UserControl
    {
        #region 필드
        #endregion

        #region 속성
        public string UnitName { get; set; } = "";

        private CogThumbnailDisplayControl Display { get; set; } = new CogThumbnailDisplayControl();

        private AlignControl AlignControl { get; set; } = new AlignControl() { Dock = DockStyle.Fill };

        private AkkonControl AkkonControl { get; set; } = new AkkonControl() { Dock = DockStyle.Fill };

        private List<UserControl> TeachControlList = null;
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public LineTeachingPage()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void LineTeachingPage_Load(object sender, EventArgs e)
        {
            AddControl();
            SelectAlign();
        }

        private void AddControl()
        {
            // Display Control
            Display = new CogThumbnailDisplayControl();
            Display.Dock = DockStyle.Fill;
            //Event 연결
            Display.DeleteEventHandler += Display_DeleteEventHandler;
            pnlDisplay.Controls.Add(Display);

            // TeachingUIManager 참조
            AppsTeachingUIManager.Instance().TeachingDisplay = Display.GetDisplay();

            // Teach Control List
            TeachControlList = new List<UserControl>();
            TeachControlList.Add(AlignControl);
        }

        private void Display_DeleteEventHandler(object sender, EventArgs e)
        {
            AlignControl.DrawROI();
        }

        private void btnAlign_Click(object sender, EventArgs e)
        {
            SelectAlign();
        }

        private void btnAkkon_Click(object sender, EventArgs e)
        {
            SelectAkkon();
        }

        public void UpdateSelectPage()
        {
            SelectAlign();
        }

        private void ClearSelectedButton()
        {
            btnAlign.ForeColor = Color.Black;
            btnAkkon.ForeColor = Color.Black;
            pnlTeach.Controls.Clear();
        }

        private void SelectAlign()
        {
            if (ModelManager.Instance().CurrentModel == null || UnitName == "")
                return;

            ClearSelectedButton();

            btnAlign.ForeColor = Color.Blue;
            pnlTeach.Controls.Add(AlignControl);

            var alignParam = SystemManager.Instance().GetTeachingData().GetAlignParameters(UnitName);
            AlignControl.SetParams(alignParam);
        }

        private void SelectAkkon()
        {
            if (ModelManager.Instance().CurrentModel == null || UnitName == "")
                return;

            ClearSelectedButton();

            btnAkkon.ForeColor = Color.Blue;
            pnlTeach.Controls.Add(AkkonControl);

            var akkonParam = SystemManager.Instance().GetTeachingData().GetAkkonParameters(UnitName);
            AkkonControl.SetParams(akkonParam);
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
                AppsTeachingUIManager.Instance().TeachingDisplay.SetImage(cogImage);
                AlignControl.DrawROI();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ATTInspModel model = ModelManager.Instance().CurrentModel as ATTInspModel;

            if (model == null)
                return;

            SaveModelData(model);
        }

        private void SaveModelData(ATTInspModel model)
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


        #endregion

        private void btnPattern_Click(object sender, EventArgs e)
        {

        }
    }
}
