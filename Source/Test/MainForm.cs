using Cognex.VisionPro;
using Cognex.VisionPro.PMAlign;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms;
using Jastech.Framework.Winform.Forms;
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

namespace Test
{
    public partial class MainForm : Form
    {
        CogDisplayControl display;
        CogPMAlignTool CogPMAlignTool;
        ICogImage CogImage;
        ModelForm form;
        CogPatternMatchingParamControl CogPatternMatchingParamControl = new CogPatternMatchingParamControl();
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
            display = new CogDisplayControl();
            display.Dock = DockStyle.Fill;
            panel1.Controls.Add(display);


            CogPatternMatchingParamControl.Dock = DockStyle.Fill;
            pnlTest.Controls.Add(CogPatternMatchingParamControl);

            Settings.Instance().Initialize();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string fileName = @"D:\back\06_35_19_0_0\06_35_19_0_0.bmp";
            CogImage = CogImageHelper.Load(fileName);
            display.SetImage(CogImage);
        }
        CogPatternMatching cogPMAlign = new CogPatternMatching();
        private void button1_Click(object sender, EventArgs e)
        {
            display.ClearGraphic();
       
            ICogImage cogImage = display.GetImage();

            double centerX = cogImage.Width / 2;
            double centerY = cogImage.Height / 2;

            CogRectangle rect2 = CogImageHelper.CreateRectangle(centerX - display.GetPan().X, centerY - display.GetPan().Y, 100, 100);
            CogRectangle rect = CogImageHelper.CreateRectangle(centerX , centerY, cogImage.Width, cogImage.Height);

            cogPMAlign.SetTrainRegion(rect2);

            display.AddGraphics("tool", cogPMAlign.GetTrainRegion());

            cogPMAlign.SetSearchRegion(rect);

            display.AddGraphics("tool", cogPMAlign.GetSearchRegion());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ModelForm form = new ModelForm();
            form.ModelPath = @"D:\1.Programs\Jastech.Apps\Jastech.Apps\Runtime\Model";
            form.Show();
        }
    }
}
