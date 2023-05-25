using AkkonTester.UI.Pages;
using Cognex.VisionPro;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Winform.VisionPro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AkkonTester
{
    public partial class MainForm : Form
    {
        public OriginalPage OriginalPage = null;

        public SlicePage SlicePage = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializePage();
            SystemManager.Instance().Initialize(this);

            SelectOriginalPage();
        }

        private void InitializePage()
        {
            OriginalPage = new OriginalPage();
            SlicePage = new SlicePage();
        }

        private void lblOriginal_Click(object sender, EventArgs e)
        {
            SelectOriginalPage();
        }

        private void lblSlicePage_Click(object sender, EventArgs e)
        {
            SelectSlicePage();
        }

        private void SelectOriginalPage()
        {
            lblOriginal.ForeColor = Color.Blue;
            lblSlicePage.ForeColor = Color.White;

            OriginalPage.Dock = DockStyle.Fill;

            pnlPage.Controls.Clear();
            pnlPage.Controls.Add(OriginalPage);
        }

        private void SelectSlicePage()
        {
            lblOriginal.ForeColor = Color.White;
            lblSlicePage.ForeColor = Color.Blue;

            SlicePage.Dock = DockStyle.Fill;

            pnlPage.Controls.Clear();
            pnlPage.Controls.Add(SlicePage);
        }


    }
}
