using Cognex.VisionPro;
using Cognex.VisionPro.PMAlign;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform.Forms;
using Jastech.Framework.Winform.VisionPro.Controls;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        CogThumbnailControl thumbnailDisplay;
        ICogImage CogImage;
        ModelForm form;
        CogPatternMatchingParamControl CogPatternMatchingParamControl = new CogPatternMatchingParamControl();
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LogHelper.Initialize(@"D:\0.Works\1.Programs\ATT\Jastech.Apps\Runtime\Log");
            LogHelper.Error(ErrorType.Grabber, "Test");
            display = new CogDisplayControl();
            display.Dock = DockStyle.Fill;
         
            pnlDisplay.Controls.Add(display);


            //CogPatternMatchingParamControl.Dock = DockStyle.Fill;
            thumbnailDisplay = new CogThumbnailControl();
            thumbnailDisplay.Dock = DockStyle.Fill;
            pnlThumbnail.Controls.Add(thumbnailDisplay);

            display.DrawViewRectEventHandler += thumbnailDisplay.DrawViewRect;
            //Settings.Instance().Initialize();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //string fileName = @"D:\back\06_35_19_0_0\06_35_19_0_0.bmp";
            string fileName = @"D:\Image.png";
            CogImage = CogImageHelper.Load(fileName);
            display.SetImage(CogImage);
            thumbnailDisplay.SetThumbnailImage(CogImage);
        }
        CogPatternMatching cogPMAlign = new CogPatternMatching();
        private void button1_Click(object sender, EventArgs e)
        {
            //display.
            //display.ClearGraphic();
       
            //ICogImage cogImage = display.GetImage();

            //double centerX = cogImage.Width / 2;
            //double centerY = cogImage.Height / 2;

            //CogRectangle rect2 = CogImageHelper.CreateRectangle(centerX - display.GetPan().X, centerY - display.GetPan().Y, 100, 100);
            //CogRectangle rect = CogImageHelper.CreateRectangle(centerX , centerY, cogImage.Width, cogImage.Height);

            //cogPMAlign.SetTrainRegion(rect2);

            //display.AddGraphics("tool", cogPMAlign.GetTrainRegion());

            //cogPMAlign.SetSearchRegion(rect);

            //display.AddGraphics("tool", cogPMAlign.GetSearchRegion());
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //thumbnailDisplay.Test();

            //string filePath = @"D:\06_35_19_0_0.bmp";

            //ICogImage image = CogImageHelper.Load(filePath);

            //display.SetImage(image);
            //thumbnailDisplay.SetThumbnailImage(image);
                
            //CogImageHelper.Save(g, @"d:\1234.bmp");
            //ModelForm form = new ModelForm();
            //form.ModelPath = @"D:\1.Programs\Jastech.Apps\Jastech.Apps\Runtime\Model";
            //form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            Mat image1 = new Mat(@"D:\0.Works\3.테스트이미지\06_35_19_0_0.bmp");

            //sw.Restart();
            //Mat mat1 = Transpose(image1);
            //sw.Stop();
            //Debug.WriteLine(sw.ElapsedMilliseconds.ToString());

            //mat1.SaveImage(@"D:\trans.bmp");

            //sw.Reset();
            //sw.Restart();
            //Mat mat2 = CvHelper.Rotate(image1, -90);
            //sw.Stop();
            //Debug.WriteLine(sw.ElapsedMilliseconds.ToString());

            //mat1.SaveImage(@"D:\rotate.bmp");

        }
    }
}
