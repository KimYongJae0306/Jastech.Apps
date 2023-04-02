using Cognex.VisionPro;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Winform.VisionPro.Controls;
using Cognex.VisionPro.Caliper;
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
        private CogImage8Grey CogOriginImage;
        private CogImage8Grey CogRegionImage;
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

                CogOriginImage = (CogImage8Grey)CogImage;
            }
        }

        private void btnShowROI_Click(object sender, EventArgs e)
        {
            ShowROI();
        }

        private void ShowROI()      
        {
            CogDisplay.ClearGraphic();
            double centerX = CogOriginImage.Width / 2;
            double centerY = CogOriginImage.Height / 2;

            CogRect = CogImageHelper.CreateRectangle(centerX - CogDisplay.GetPan().X, centerY - CogDisplay.GetPan().Y, 100, 100);
            CogRect.Interactive = true;
            CogRect.GraphicDOFEnable = CogRectangleDOFConstants.All;
            CogRect.Color = CogColorConstants.Green;
            CogDisplay.AddGraphics("ROI", CogRect);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            CogDisplay.ClearGraphic();
            ImageConversion();
            AutoLeadROI();
        }

        private void ImageConversion()
        {
            if (CogImage == null)
                return;

            RegionImage = CogImageHelper.CropImage(CogImage, CogRect);
            CogRegionImage = CogImageHelper.Threshold((CogImage8Grey)RegionImage, 10, 120, 255, true);
            CogDisplay.SetImage(CogRegionImage);
        }

        private void AutoLeadROI()
        {
            CogRectangleAffine cogRectAffine = new CogRectangleAffine();
            CogCaliperTool cogCaliper = new CogCaliperTool();
            cogCaliper.InputImage = CogRegionImage;
        
            cogRectAffine.SetCenterLengthsRotationSkew((CogRect.Width / 2), CogRect.Y + 20, CogRect.Width, 50, 0, 0);
            cogRectAffine.GraphicDOFEnable = CogRectangleAffineDOFConstants.Position | CogRectangleAffineDOFConstants.Size | CogRectangleAffineDOFConstants.Skew | CogRectangleAffineDOFConstants.Rotation;
            cogRectAffine.Rotation = 0; //좌->우 방향 고정
            cogRectAffine.Interactive = false;
            CogDisplay.AddGraphics("ROI", cogRectAffine);


            //맨 우측까지 Search 완료 시 탈출
            //Caliper Params 적용
            cogCaliper.Region = cogRectAffine;
            cogCaliper.RunParams.Edge0Polarity = CogCaliperPolarityConstants.LightToDark;
            cogCaliper.RunParams.EdgeMode = CogCaliperEdgeModeConstants.SingleEdge;
            cogCaliper.RunParams.ContrastThreshold = 10;
            cogCaliper.RunParams.FilterHalfSizeInPixels = 5;

            //Caliper Search
            cogCaliper.Run();
            
            //Search OK
            if (cogCaliper.Results != null && cogCaliper.Results.Count > 0)
            {
                for (int i = 0; i < cogCaliper.Results.Count; i++)
                {
                    CogDisplay.AddGraphics("Search", cogCaliper.Results[i].CreateResultGraphics(CogCaliperResultGraphicConstants.Edges));                    
                }
            }

            //임의로 10pixel 정도 이동
            cogRectAffine.SetCenterLengthsRotationSkew(cogRectAffine.CenterX + 10, CogRect.Y + 20, cogRectAffine.SideXLength - 10, 50, 0, 0);
            cogRectAffine.Color = CogColorConstants.Red;
            CogDisplay.AddGraphics("ROI", cogRectAffine);

        }
    }
}
