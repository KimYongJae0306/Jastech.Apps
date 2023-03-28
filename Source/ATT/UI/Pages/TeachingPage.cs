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
using ATT.Core;
using Jastech.Framework.Imaging.VisionPro;
using Cognex.VisionPro;
using Jastech.Apps.Winform;
using Jastech.Apps.Structure;
using Jastech.Framework.Util.Helper;
using Jastech.Apps.Winform.Settings;

namespace ATT.UI.Pages
{
    public partial class TeachingPage : UserControl
    {
        #region 속성
        // Display
        private CogDisplayControl TeachDisplay = null;

        // Teach Controls
        private PreAlignControl PreAlignControl { get; set; } = new PreAlignControl();

        // Control List
        private List<UserControl> TeachControlList = null;
        private List<Button> TeachButtonList = null;
        #endregion

        public TeachingPage()
        {
            InitializeComponent();
        }

        private void TeachingPage_Load(object sender, EventArgs e)
        {
            AddControl();
        }

        private void AddControl()
        {
            // Display Control
            TeachDisplay = new CogDisplayControl();
            TeachDisplay.Dock = DockStyle.Fill;
            pnlDisplay.Controls.Add(TeachDisplay);

            // TeachingUIManager 참조
            TeachingUIManager.Instance().TeachingDisplay = TeachDisplay;

            // Teach Control List
            TeachControlList = new List<UserControl>();
            TeachControlList.Add(PreAlignControl);

            // Button List
            TeachButtonList = new List<Button>();
            TeachButtonList.Add(btnLinescan);
            TeachButtonList.Add(btnPreAlign);
            TeachButtonList.Add(btnAlign);
            TeachButtonList.Add(btnAkkon);

        }

        private void btnLinescan_Click(object sender, EventArgs e)
        {
            SetSelectButton(sender);
        }

        private void btnPreAlign_Click(object sender, EventArgs e)
        {
            SetSelectButton(sender);
            SetSelectTeachPage(PreAlignControl);
        }

        private void btnAlign_Click(object sender, EventArgs e)
        {
            SetSelectButton(sender);
        }

        private void btnAkkon_Click(object sender, EventArgs e)
        {
            SetSelectButton(sender);
        }

        private void SetSelectButton(object sender)
        {
            foreach (Button button in TeachButtonList)
                button.ForeColor = Color.Black;

            Button btn = sender as Button;
            btn.ForeColor = Color.Blue;
        }

        private void SetSelectTeachPage(UserControl selectedControl)
        {
            foreach (UserControl control in TeachControlList)
                control.Visible = false;

            selectedControl.Visible = true;
            selectedControl.Dock = DockStyle.Fill;
            pnlTeach.Controls.Add(selectedControl);
        }

        public void UpdateModel(InspModel inspModel)
        {
            ATTInspModel model = inspModel as ATTInspModel;

            PreAlignControl.SetParams(model.PreAlignParams.Select(x => x.DeepCopy()).ToList());
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
                TeachDisplay.SetImage(cogImage);
                TeachingUIManager.Instance().TeachingDisplay.SetImage(cogImage);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ATTInspModel model = ModelManager.Instance().CurrentModel as ATTInspModel;

            if (model == null)
                return;

            model.SetPreAlignParams(PreAlignControl.GetTeachingData());

            SaveModelData(model);
        }

        private void SaveModelData(ATTInspModel model)
        {
            string fileName = System.IO.Path.Combine(AppConfig.Instance().Path.Model, model.Name, InspModel.FileName);
            SystemManager.Instance().SaveModel(fileName, model);
        }
    }
}
