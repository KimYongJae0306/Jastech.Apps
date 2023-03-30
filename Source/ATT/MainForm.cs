using ATT.Core;
using ATT.UI.Pages;
using Jastech.Apps.Structure;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Config;
using Jastech.Framework.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATT
{
    public partial class MainForm : Form
    {
        #region 속성
        // Page Control
        private AutoPage AutoPageControl { get; set; } = new AutoPage();

        private TeachingPage TeachingPageControl { get; set; } = new TeachingPage();

        private ModelPage ModelPageControl { get; set; } = new ModelPage();

        private RecipePage RecipePageControl { get; set; } = new RecipePage();

        private LogPage LogPageControl { get; set; } = new LogPage();

        private SettingPage SettingPageControl { get; set; } = new SettingPage();

        private List<UserControl> PageControlList = null;

        private List<Label> PageLabelList = null;

        public ATTInspModelService ATTInspModelService { get; set; } = new ATTInspModelService();
        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            AddControls();
            SelectInspectionPage();

            ModelManager.Instance().CurrentModelChangedEvent += MainForm_CurrentModelChangedEvent;

            if (ModelManager.Instance().CurrentModel != null)
            {
                lblCurrentModel.Text = ModelManager.Instance().CurrentModel.Name;
                ModelManager.Instance().ApplyChangedEvent();
            }

            tmrMainForm.Start();
        }

        private void tmrMainForm_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            lblCurrentTime.Text = now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void MainForm_CurrentModelChangedEvent(InspModel inspModel)
        {
            TeachingPageControl.UpdateModel(inspModel);
        }

        private void AddControls()
        {
            // Page Control List
            PageControlList = new List<UserControl>();
            PageControlList.Add(AutoPageControl);
            PageControlList.Add(TeachingPageControl);

            ModelPageControl.InspModelService = ATTInspModelService;
            ModelPageControl.ApplyModelEventHandler += ModelPageControl_ApplyModelEventHandler;
            PageControlList.Add(ModelPageControl);
            PageControlList.Add(RecipePageControl);
            PageControlList.Add(LogPageControl);
            PageControlList.Add(SettingPageControl);

            // Button List
            PageLabelList = new List<Label>();
            PageLabelList.Add(lblModelPage);
            PageLabelList.Add(lblInspectionPage);
            PageLabelList.Add(lblTeachingPage);
            PageLabelList.Add(lblLogPage);
            PageLabelList.Add(lblSettingPage);
            PageLabelList.Add(lblModelPage);
        }

        private void ModelPageControl_ApplyModelEventHandler(string modelName)
        {
            string modelDir = AppConfig.Instance().Path.Model;
            string filePath = Path.Combine(modelDir, modelName, InspModel.FileName);

            ModelManager.Instance().CurrentModel = ATTInspModelService.Load(filePath);
            SelectInspectionPage();

            lblCurrentModel.Text = modelName;

            AppConfig.Instance().Operation.LastModelName = modelName;

            AppConfig.Instance().Operation.Save(AppConfig.Instance().Path.Config);
        }

        private void SetSelectLabel(object sender)
        {
            foreach (Label label in PageLabelList)
                label.ForeColor = Color.Black;

            Label currentLabel = sender as Label;
            currentLabel.ForeColor = Color.Blue;
        }

        private void SetSelectPage(UserControl selectedControl)
        {
            foreach (UserControl control in PageControlList)
                control.Visible = false;

            selectedControl.Visible = true;
            selectedControl.Dock = DockStyle.Fill;
            pnlPage.Controls.Add(selectedControl);
        }

        private void SelectInspectionPage()
        {
            SetSelectLabel(lblInspectionPage);
            SetSelectPage(selectedControl: AutoPageControl);
        }

        
        private void ModelPage_Click(object sender, EventArgs e)
        {
            SetSelectLabel(sender);
            SetSelectPage(selectedControl: ModelPageControl);
        }

        private void InspectionPage_Click(object sender, EventArgs e)
        {
            SetSelectLabel(sender);
            SetSelectPage(selectedControl: AutoPageControl);
        }

        private void TeachPage_Click(object sender, EventArgs e)
        {
            SetSelectLabel(sender);
            SetSelectPage(selectedControl: TeachingPageControl);
        }

        private void LogPage_Click(object sender, EventArgs e)
        {
            SetSelectLabel(sender);
            SetSelectPage(selectedControl: LogPageControl);
        }

        private void SettingPage_Click(object sender, EventArgs e)
        {
            SetSelectLabel(sender);
            SetSelectPage(selectedControl: SettingPageControl);
        }
    }
}
