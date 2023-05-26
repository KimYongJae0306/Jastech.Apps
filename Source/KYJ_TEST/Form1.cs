using Cognex.VisionPro;
using Cognex.VisionPro.Blob;
using Cognex.VisionPro.Caliper;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Jastech.Framework.Algorithms.Akkon;
using Jastech.Framework.Algorithms.Akkon.Parameters;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.Ipp;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform.VisionPro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KYJ_TEST
{
    public partial class Form1 : Form
    {
        CogDisplayControl CogDisplayControl = new CogDisplayControl();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //VisionProHelper.InitMemory();
            //ICogImage cogImage = new CogImage8Grey(new Bitmap(10, 10));
            //CogBlobTool tool = new CogBlobTool();

            //tool.InputImage = cogImage;
            //tool.Run();

            //CogDisplayControl.Dock = DockStyle.Fill;
            //panel1.Controls.Add(CogDisplayControl);
            //Test();
            //Test2();
            TestAkkon();
          
        }
        private void Test2()
        {
            string path = string.Format(@"D:\Test\AkkonTestProgram\Test_Blob{0}.bmp", 0);
            Mat matImage = new Mat(@"D:\Test\AkkonTestProgram\0\Test_Blob.bmp", ImreadModes.Grayscale);

            ICogImage cogImage = VisionProImageHelper.Load(path);
            VisionProBlob t = new VisionProBlob();
             var result = t.Run(cogImage, new VisionProBlobParam());
            //Jastech.Framework.Imaging.VisionAlgorithms.OpencvContour cont = new Jastech.Framework.Imaging.VisionAlgorithms.OpencvContour();
            //cont.Run(matImage);
            Mat colorMat = new Mat();
            CvInvoke.CvtColor(matImage, colorMat, ColorConversion.Gray2Bgr);

            for (int i = 0; i < result.BlobList.Count(); i++)
            {
                var blob = result.BlobList[i];
                CvInvoke.Rectangle(colorMat, blob.BoundingRect, new MCvScalar(0, 0,255));
                //CvInvoke.Polylines(colorMat, result.BlobList[i].Points.ToArray(), true, new MCvScalar(255));
            }

            colorMat.Save(@"D:\123.bmp");
        }
        private void Test()
        {
            //Mat matImage = new Mat(@"D:\Test\AkkonTestProgram\0\Test_Blob.bmp");

            //ICogImage cogImage1 = new CogImage8Grey(new Bitmap(matImage.Width, matImage.Height, PixelFormat.Format8bppIndexed));
            ////CogDisplayControl.SetImage(cogImage);
            //CogBlobTool tool1 = new CogBlobTool();
            //tool1.InputImage = cogImage1;
            //tool1.Run();
            //for (int i = 0; i < 11; i++)
            //{
            string path = string.Format(@"D:\Test\AkkonTestProgram\Test_Blob{0}.bmp", 0);
            ICogImage cogImage = VisionProImageHelper.Load(path);
            CogDisplayControl.SetImage(cogImage);
            CogBlobTool tool = new CogBlobTool();

            Stopwatch sw = new Stopwatch();
            sw.Restart();
            tool.InputImage = cogImage;

            tool.LastRunRecordDiagEnable = CogBlobLastRunRecordDiagConstants.None;
            tool.RunParams.RegionMode = CogRegionModeConstants.PixelAlignedBoundingBoxAdjustMask;
            tool.RunParams.ConnectivityMode = CogBlobConnectivityModeConstants.GreyScale;
            tool.RunParams.ConnectivityCleanup = CogBlobConnectivityCleanupConstants.Fill;
            tool.RunParams.SegmentationParams.Mode = CogBlobSegmentationModeConstants.HardFixedThreshold;
            tool.RunParams.ConnectivityMinPixels = 1;
            tool.Run();

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds.ToString());
            //}
            
            var blobResult = tool.Results.GetBlobs();


            CogGraphicInteractiveCollection collect = new CogGraphicInteractiveCollection();

            for (int i = 0; i < blobResult.Count; i++)
            {
                var cogBlob = blobResult[i] as CogBlobResult;
                //var g = cogBlob.GetBoundary();
                //var aaa1 = cogBlob.GetMeasure(CogBlobMeasureConstants.BoundingBoxPrincipalAxisWidth);
                //g.GetVertices();

                //var area = cogBlob.Area;
                //var centerX = cogBlob.CenterOfMassX;
                //var centerY = cogBlob.CenterOfMassY;

                var cogRect = cogBlob.GetBoundingBox(CogBlobAxisConstants.Principal);
                cogRect.Color = CogColorConstants.Yellow;
                collect.Add(cogRect);
                //int x = (int)cogRect.CornerXX;
                //int y = (int)cogRect.CornerXY;

                //Rectangle rect = new Rectangle(cogr)
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds.ToString());
            CogDisplayControl.SetInteractiveGraphics("Test", collect);

        }

        private void TestAkkon()
        {
            AkkonAlgorithm AkkonAlgorithm = new AkkonAlgorithm();

            //string maskFile = @"D:\Test\[A3CF1D2307PAA055]_00h47m50s_A1P1M1_Stage1_Tab1.txt";
            //Mat mat = new Mat(@"D:\Test\[A3CF1D2307PAA055]_00h47m50s_A1P1M1_Stage1_Tab1.bmp", ImreadModes.Grayscale);
            //var roiList = ReadROI(maskFile);

            string maskFile = @"D:\Test\MFC 압흔 결과\ROI_Data_2.txt";
            Mat mat = new Mat(@"D:\Test\MFC 압흔 결과\[A3DV1D3216CBE065]_23h55m39s_A1P1M1_Stage1_Tab3.bmp", ImreadModes.Grayscale);

            var roiList = ReadROI(maskFile);
            var param = new AkkonAlgoritmParam();
            param.ThresParam.Weight = 1;
            param.ResizeRatio = 0.5;
            Stopwatch sw = new Stopwatch();

            for (int i = 0; i < 1000; i++)
            {
                //sw.Restart();

                var resultList = AkkonAlgorithm.Run(mat, roiList, param);

                //sw.Stop();
                //Console.WriteLine(sw.ElapsedMilliseconds.ToString());

                Thread.Sleep(50);
            }
            int g1 = 7;

            //Mat resizeMat = new Mat();
            //Size newSize = new Size((int)(mat.Width * param.ResizeRatio), (int)(mat.Height * param.ResizeRatio));
            //CvInvoke.Resize(mat, resizeMat, newSize);
            //Mat colorMat = new Mat();
            //CvInvoke.CvtColor(resizeMat, colorMat, ColorConversion.Gray2Bgr);
            //resizeMat.Dispose();

            //foreach (var result in resultList)
            //{
            //    var lead = result.Lead;
            //    var startPoint = new Point((int)result.OffsetToWorldX, (int)result.OffsetToWorldY);

            //    Point leftTop = new Point(lead.LeftTop.X + startPoint.X, lead.LeftTop.Y + startPoint.Y);
            //    Point leftBottom = new Point(lead.LeftBottom.X + startPoint.X, lead.LeftBottom.Y + startPoint.Y);
            //    Point rightTop = new Point(lead.RightTop.X + startPoint.X, lead.RightTop.Y + startPoint.Y);
            //    Point rightBottom = new Point(lead.RightBottom.X + startPoint.X, lead.RightBottom.Y + startPoint.Y);

            //    CvInvoke.Line(colorMat, leftTop, leftBottom, new MCvScalar(50, 230, 50, 255), 1);
            //    CvInvoke.Line(colorMat, leftTop, rightTop, new MCvScalar(50, 230, 50, 255), 1);
            //    CvInvoke.Line(colorMat, rightTop, rightBottom, new MCvScalar(50, 230, 50, 255), 1);
            //    CvInvoke.Line(colorMat, rightBottom, leftBottom, new MCvScalar(50, 230, 50, 255), 1);

            //    int blobCount = 0;
            //    foreach (var blob in result.BlobList)
            //    {
            //        Rectangle rectRect = new Rectangle();
            //        rectRect.X = (int)(blob.BoundingRect.X + result.OffsetToWorldX + result.LeadOffsetX);
            //        rectRect.Y = (int)(blob.BoundingRect.Y + result.OffsetToWorldY + result.LeadOffsetY);
            //        rectRect.Width = blob.BoundingRect.Width;
            //        rectRect.Height = blob.BoundingRect.Height;

            //        //if (rectRect.Width > 2 && rectRect.Height > 2)
            //        //if (rectRect.Height != 1)
            //        {

            //            var areaPercent = (double)blob.Area / (rectRect.Width * rectRect.Height) * 100;
            //            //Console.WriteLine(g.ToString());
            //            //if ((rectRect.Width + rectRect.Height) / 2.0 >= 1.5) // 2 : minSize
            //            //if(rectRect.Width != 1 && rectRect.Height != 1)
            //            //if (blob.Area > 3)
            //            if (areaPercent >= 20)
            //            {
            //                //if((rectRect.Width + rectRect.Height) / 2.0 <= 10)

            //                blobCount++;
            //                Point center = new Point(rectRect.X + (rectRect.Width / 2), rectRect.Y + (rectRect.Height / 2));
            //                int radius = rectRect.Width > rectRect.Height ? rectRect.Width : rectRect.Height;
            //                //CvInvoke.Rectangle(colorMat, rectRect, new MCvScalar(50, 230, 50, 255), 1);
            //                CvInvoke.Circle(colorMat, center, radius / 2, new MCvScalar(255), 1);
            //            }
            //            else
            //            {
            //                //blobCount++;

            //                double g = 0;
            //                if (rectRect.Width > rectRect.Height)
            //                {
            //                    g = ((double)rectRect.Height / rectRect.Width) * 100.0;
            //                }
            //                else
            //                {
            //                    g = ((double)rectRect.Width / rectRect.Height) * 100.0;
            //                }

            //                // Console.WriteLine(g);

            //                if (g <= 50)
            //                {
            //                    blobCount++;
            //                    Point center = new Point(rectRect.X + (rectRect.Width / 2), rectRect.Y + (rectRect.Height / 2));
            //                    int radius = rectRect.Width > rectRect.Height ? rectRect.Width : rectRect.Height;
            //                    //CvInvoke.Rectangle(colorMat, rectRect, new MCvScalar(50, 230, 50, 255), 1);
            //                    CvInvoke.Circle(colorMat, center, radius / 2, new MCvScalar(255), 1);
            //                }
            //                else
            //                {
            //                    Point center = new Point(rectRect.X + (rectRect.Width / 2), rectRect.Y + (rectRect.Height / 2));
            //                    int radius = rectRect.Width > rectRect.Height ? rectRect.Width : rectRect.Height;
            //                    //CvInvoke.Rectangle(colorMat, rectRect, new MCvScalar(50, 230, 50, 255), 1);
            //                    CvInvoke.Circle(colorMat, center, radius / 2, new MCvScalar(0), 1);
            //                }

            //            }
            //        }
            //    }
            //    string leadIndexString = result.LeadIndex.ToString();
            //    string blobCountString = string.Format("[{0}]", blobCount);

            //    Point centerPt = new Point((int)((leftBottom.X + rightBottom.X) / 2.0), leftBottom.Y);

            //    int baseLine = 0;
            //    Size textSize = CvInvoke.GetTextSize(leadIndexString, FontFace.HersheyComplex, 0.3, 1, ref baseLine);
            //    int textX = centerPt.X - (textSize.Width / 2);
            //    int textY = centerPt.Y + (baseLine / 2);
            //    CvInvoke.PutText(colorMat, leadIndexString, new Point(textX, textY + 30), FontFace.HersheyComplex, 0.3, new MCvScalar(50, 230, 50, 255));

            //    textSize = CvInvoke.GetTextSize(blobCountString, FontFace.HersheyComplex, 0.3, 1, ref baseLine);
            //    textX = centerPt.X - (textSize.Width / 2);
            //    textY = centerPt.Y + (baseLine / 2);
            //    CvInvoke.PutText(colorMat, blobCountString, new Point(textX, textY + 60), FontFace.HersheyComplex, 0.3, new MCvScalar(50, 230, 50, 255));



            //    //colorMat.Save(@"D:\123.bmp");

            //}
            //colorMat.Save(@"D:\123.bmp");
        }

        private List<AkkonROI> ReadROI(string path, int offsetX = 0, int offsetY = 0)
        {
            List<AkkonROI> roiList = new List<AkkonROI>();

            string str = File.ReadAllText(path);
            string[] lines = str.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                AkkonROI roi = new AkkonROI();

                string[] values = line.Split(' ');

                roi.LeftTopX = Convert.ToDouble(values[0]) + offsetX;
                roi.LeftTopY = Convert.ToDouble(values[1]) + offsetY;

                roi.RightTopX = Convert.ToDouble(values[2]) + offsetX;
                roi.RightTopY = Convert.ToDouble(values[3]) + offsetY;

                roi.RightBottomX = Convert.ToDouble(values[4]) + offsetX;
                roi.RightBottomY = Convert.ToDouble(values[5]) + offsetY;

                roi.LeftBottomX = Convert.ToDouble(values[6]) + offsetX;
                roi.LeftBottomY = Convert.ToDouble(values[7]) + offsetY;

                roiList.Add(roi);
            }
            //roiList.OrderBy(roi => roi.LeftTop.X).FirstOrDefault();
            return roiList;
        }
    }
}
