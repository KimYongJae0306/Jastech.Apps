using Jastech.Apps.Winform.UI.Pages;
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
        private AutoPage AutoPageControl { get; set; } = new AutoPage();
        private TeachingPage TeachingPageControl { get; set; } = new TeachingPage();
        private RecipePage RecipePageControl { get; set; } = new RecipePage();
        private LogPage LogPageControl { get; set; } = new LogPage();
        private ConfigPage ConfigPageControl { get; set; } = new ConfigPage();

        private List<UserControl> PageControlList = null;
        private List<Button> PageButtonList = null;
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
            PageControlList.Add(RecipePageControl);
            PageControlList.Add(LogPageControl);
            PageControlList.Add(ConfigPageControl);

            // Button List
            PageButtonList = new List<Button>();
            PageButtonList.Add(btnAutoPage);
            PageButtonList.Add(btnTeachPage);
            PageButtonList.Add(btnRecipePage);
            PageButtonList.Add(btnLogPage);
            PageButtonList.Add(btnConfigPage);
        }

        private void btnAutoPage_Click(object sender, EventArgs e)
        {
            SetSelectButton(sender);
            SetSelectPage(selectedControl: AutoPageControl);
        }

        private void btnTeachPage_Click(object sender, EventArgs e)
        {
            SetSelectButton(sender);
            SetSelectPage(selectedControl: TeachingPageControl);
        }

        private void btnRecipePage_Click(object sender, EventArgs e)
        {
            SetSelectButton(sender);
            SetSelectPage(selectedControl: RecipePageControl);
        }

        private void btnLogPage_Click(object sender, EventArgs e)
        {
            SetSelectButton(sender);
            SetSelectPage(selectedControl: LogPageControl);
        }

        private void btnConfigPage_Click(object sender, EventArgs e)
        {
            SetSelectButton(sender);
            SetSelectPage(selectedControl: ConfigPageControl);
        }

        private void SetSelectButton(object sender)
        {
            foreach (Button button in PageButtonList)
                button.ForeColor = Color.Black;

            Button btn = sender as Button;
            btn.ForeColor = Color.Blue;
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
            //picLogo.Image = 

            SetSelectButton(btnAutoPage);
            SetSelectPage(selectedControl: AutoPageControl);
        }

        private void tmrMainForm_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToShortDateString();
            lblTime.Text = DateTime.Now.ToLongTimeString();
        }
    }
}
