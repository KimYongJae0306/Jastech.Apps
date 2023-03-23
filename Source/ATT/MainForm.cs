using Jastech.Apps.Winform.UI.Pages;
using Jastech.Framework.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        private ConfigPage ConfigPageControl { get; set; } = new ConfigPage();

        private List<UserControl> PageControlList = null;
        private List<Label> PageLabelList = null;
        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            AddControls();
            InitializeUI();
            tmrMainForm.Start();
        }

        private void AddControls()
        {
            // Page Control List
            PageControlList = new List<UserControl>();
            PageControlList.Add(AutoPageControl);
            PageControlList.Add(TeachingPageControl);
            PageControlList.Add(ModelPageControl);
            PageControlList.Add(RecipePageControl);
            PageControlList.Add(LogPageControl);
            PageControlList.Add(ConfigPageControl);

            // Button List
            PageLabelList = new List<Label>();
            PageLabelList.Add(lblModelPage);
            PageLabelList.Add(lblInspectionPage);
            PageLabelList.Add(lblTeachingPage);
            PageLabelList.Add(lblLogPage);
            PageLabelList.Add(lblSettingPage);
        }

        private void btnAutoPage_Click(object sender, EventArgs e)
        {
            
        }

        private void btnTeachPage_Click(object sender, EventArgs e)
        {
            
        }

        private void btnModelPage_Click(object sender, EventArgs e)
        {
            
        }

        private void btnRecipePage_Click(object sender, EventArgs e)
        {
            SetSelectLabel(sender);
            SetSelectPage(selectedControl: RecipePageControl);
        }

        private void btnLogPage_Click(object sender, EventArgs e)
        {
            
        }

        private void btnConfigPage_Click(object sender, EventArgs e)
        {
            
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

        private void InitializeUI()
        {
            SetSelectLabel(lblInspectionPage);
            SetSelectPage(selectedControl: AutoPageControl);
        }

        private void tmrMainForm_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            lblCurrentTime.Text = now.ToString("yyyy-MM-dd HH:mm:ss");
            //lblDate.Text = DateTime.Now.ToShortDateString();
            //lblTime.Text = DateTime.Now.ToLongTimeString();
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
            SetSelectPage(selectedControl: ConfigPageControl);
        }
    }
}
