using Cognex.VisionPro;
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

namespace YSHTest
{
    public partial class Form1 : Form
    {
        private CogDisplayControl CogDisplay;
        private ICogImage CogImage;
        private ICogImage RegionImage;
        private ICogImage RegionConversionImage;
        private CogImage8Grey CogGreyImage;
        private CogRectangle CogRect = new CogRectangle();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AddControl();
        }

        private void AddControl()
        {
            CogDisplay = new CogDisplayControl();
            CogDisplay.Dock = DockStyle.Fill;
            pnlPage.Controls.Add(CogDisplay);
        }
        private void btnImageLoad_Click(object sender, EventArgs e)
        {
            ImageLoad();
        }
        private void ImageLoad()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.ReadOnlyChecked = true;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                CogImage = CogImageHelper.Load(openFile.FileName);
                CogDisplay.SetImage(CogImage);

                CogGreyImage = (CogImage8Grey)CogImage;
            }
        }

        private void btnShowROI_Click(object sender, EventArgs e)
        {
            ShowROI();
        }

        private void ShowROI()      
        {
            CogDisplay.ClearGraphic();
            double centerX = CogGreyImage.Width / 2;
            double centerY = CogGreyImage.Height / 2;

            CogRect = CogImageHelper.CreateRectangle(centerX - CogDisplay.GetPan().X, centerY - CogDisplay.GetPan().Y, 100, 100);
            CogRect.Interactive = true;
            CogRect.GraphicDOFEnable = CogRectangleDOFConstants.All;
            CogRect.Color = CogColorConstants.Green;
            CogDisplay.AddGraphics("ROI", CogRect);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            ImageConversion();
            AutoLeadROI();
        }

        private void ImageConversion()
        {
            if (CogImage == null)
                return;

            //RegionImage = CogImageHelper.CogCopyRegionTool2(CogImage, CogRect);
        }

        private void AutoLeadROI()
        {
            CogImage8Grey TempRegionImage = (CogImage8Grey)RegionImage;
            ICogImage image = CogImageHelper.Load(@"D:\123.bmp");
            //CogImageHelper.Save((ICogImage)TempRegionImage, @"D:\123.bmp");
            CogImageHelper.Save(TempRegionImage, @"D:\123.bmp");

            //CogImage8Grey ConversionImage;
            //ConversionImage = CogImageHelper.Threadhold(TempRegionImage, 50, 255, true);
            //CogImageHelper.Save(ConversionImage, "D:\\Test_Region.bmp");
            //CogImageHelper.Save(TempRegionImage, @"D:\1234.bmp");
            //CogImageHelper.Save()
            /////////////////////////////////////////////////////////////

            //CogImage8Grey TempRegionImage;
            //string fileName = @"D:\Conversion.bmp";
            //RegionConversionImage = CogImageHelper.Load(fileName);
            //TempRegionImage = (CogImage8Grey)RegionConversionImage;

            //CogRectangleAffine cogRectAffine = new CogRectangleAffine();
            //cogRectAffine.SetCenterLengthsRotationSkew((TempRegionImage.Width / 2), (TempRegionImage.Height / 2), TempRegionImage.Width, 50, 0, 0);
            //cogRectAffine.GraphicDOFEnable = CogRectangleAffineDOFConstants.Position | CogRectangleAffineDOFConstants.Size | CogRectangleAffineDOFConstants.Skew | CogRectangleAffineDOFConstants.Rotation;
            //cogRectAffine.Interactive = false;
            //CogDisplay.AddGraphics("ROI", cogRectAffine);

        }
    }
}
