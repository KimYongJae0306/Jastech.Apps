using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Jastech.Framework.Algorithms.Akkon;
using Jastech.Framework.Algorithms.Akkon.Parameters;
using Jastech.Framework.Imaging.Result;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace AkkonTester
{
    public class SystemManager
    {
        private MainForm _mainForm = null;

        private AkkonAlgorithm AkkonAlgorithm { get; set; } = new AkkonAlgorithm();

        public AkkonAlgoritmParam AkkonParameters { get; set; } = new AkkonAlgoritmParam();

        public float Resolution_um { get; set; } = 1.0F;

        public Mat OrginalImage { get; set; } = null;

        public List<AkkonROI> CurrentAkkonROI = new List<AkkonROI>();

        public List<AkkonSlice> SliceList { get; set; } = new List<AkkonSlice>();

        public List<AkkonLeadResult> CurrentLeadResult { get; set; } = new List<AkkonLeadResult>();

        private static SystemManager _instance = null;

        public static SystemManager Instance()
        {
            if (_instance == null)
            {
                _instance = new SystemManager();
            }
            return _instance;
        }

        public bool Initialize(MainForm mainForm)
        {
            _mainForm = mainForm;

            if(AkkonParameters.ImageFilterParam.Filters.Count == 0)
            {
                AkkonParameters.Initalize();
                AkkonParameters.ImageFilterParam.AddMacronFilter();
            }

            return true;
        }

        public void CalcSliceImage()
        {
            if (OrginalImage == null || CurrentAkkonROI.Count() == 0)
                return;

            SliceList.ForEach(x => x.Dispose());
            SliceList.Clear();

            double resizeRatio = AkkonParameters.ImageFilterParam.ResizeRatio;
            List<AkkonSlice> slices = AkkonAlgorithm.PrepareInspect(OrginalImage, CurrentAkkonROI, 2048, resizeRatio);

            SliceList.AddRange(slices);
        }

        public void UpdateSliceData()
        {
            _mainForm.SlicePage.UpdateSliceInfo();
        }

        public void RunForDebug(int sliceIndex)
        {
            var slice = SliceList[sliceIndex];

            slice.EnhanceMat?.Dispose();
            slice.ProcessingMat?.Dispose();
            slice.MaskingMat?.Dispose();

            Stopwatch sw = new Stopwatch();
            sw.Restart();

            List<AkkonLeadResult> result = AkkonAlgorithm.RunForDebug(ref slice, AkkonParameters, Resolution_um);

            sw.Stop();
            Console.WriteLine(" RunForDebug TactTime : " + sw.ElapsedMilliseconds);

            CurrentLeadResult.Clear();
            CurrentLeadResult.AddRange(result);
        }

        public List<AkkonLeadResult> Run()
        {
            if (OrginalImage == null)
                return null;
            
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            Judgement tabJudgement = Judgement.NG;
            List<AkkonLeadResult> result = AkkonAlgorithm.Run(OrginalImage, CurrentAkkonROI, AkkonParameters, Resolution_um, ref tabJudgement);

            sw.Stop();
            Console.WriteLine("Run : " + sw.ElapsedMilliseconds);
            return result;
        }

        public Mat GetResultImage(List<AkkonLeadResult> leadResultList)
        {
            if (OrginalImage == null)
                return null;

            Mat resizeMat = new Mat();
            Size newSize = new Size((int)(OrginalImage.Width * AkkonParameters.ImageFilterParam.ResizeRatio), (int)(OrginalImage.Height * AkkonParameters.ImageFilterParam.ResizeRatio));
            CvInvoke.Resize(OrginalImage, resizeMat, newSize);
            Mat colorMat = new Mat();
            CvInvoke.CvtColor(resizeMat, colorMat, ColorConversion.Gray2Bgr);
            resizeMat.Dispose();

            MCvScalar redColor = new MCvScalar(50, 50, 230, 255);
            MCvScalar greenColor = new MCvScalar(50, 230, 50, 255);

            foreach (var result in leadResultList)
            {
                var lead = result.Roi;
                var startPoint = new Point((int)result.Offset.ToWorldX, (int)result.Offset.ToWorldY);
                    
                Point leftTop = new Point((int)lead.LeftTopX + startPoint.X, (int)lead.LeftTopY + startPoint.Y);
                Point leftBottom = new Point((int)lead.LeftBottomX + startPoint.X, (int)lead.LeftBottomY + startPoint.Y);
                Point rightTop = new Point((int)lead.RightTopX + startPoint.X, (int)lead.RightTopY + startPoint.Y);
                Point rightBottom = new Point((int)lead.RightBottomX + startPoint.X, (int)lead.RightBottomY + startPoint.Y);

                if (AkkonParameters.DrawOption.ContainLeadROI)
                {
                    CvInvoke.Line(colorMat, leftTop, leftBottom, greenColor, 1);
                    CvInvoke.Line(colorMat, leftTop, rightTop, greenColor, 1);
                    CvInvoke.Line(colorMat, rightTop, rightBottom, greenColor, 1);
                    CvInvoke.Line(colorMat, rightBottom, leftBottom, greenColor, 1);
                }

                foreach (var blob in result.BlobList)
                {
                    Rectangle rectRect = new Rectangle();
                    rectRect.X = (int)(blob.BoundingRect.X + result.Offset.ToWorldX + result.Offset.X);
                    rectRect.Y = (int)(blob.BoundingRect.Y + result.Offset.ToWorldY + result.Offset.Y);
                    rectRect.Width = blob.BoundingRect.Width;
                    rectRect.Height = blob.BoundingRect.Height;

                    Point center = new Point(rectRect.X + (rectRect.Width / 2), rectRect.Y + (rectRect.Height / 2));
                    int radius = rectRect.Width > rectRect.Height ? rectRect.Width : rectRect.Height;

                    int size = blob.BoundingRect.Width * blob.BoundingRect.Height;
                    double calcMinArea = AkkonParameters.ShapeFilterParam.MinArea_um * Resolution_um;
                    double calcMaxArea = AkkonParameters.ShapeFilterParam.MaxArea_um * Resolution_um;

                    if (blob.IsAkkonShape)
                        CvInvoke.Circle(colorMat, center, radius / 2, greenColor, 1);
                    else
                    {
                        if(AkkonParameters.DrawOption.ContainNG)
                            CvInvoke.Circle(colorMat, center, radius / 2, redColor, 1);
                    }
                }

                if(AkkonParameters.DrawOption.ContainLeadCount)
                {
                    string leadIndexString = result.Roi.Index.ToString();
                    string akkonCountString = string.Format("[{0}]", result.AkkonCount);

                    Point centerPt = new Point((int)((leftBottom.X + rightBottom.X) / 2.0), leftBottom.Y);

                    int baseLine = 0;
                    Size textSize = CvInvoke.GetTextSize(leadIndexString, FontFace.HersheyComplex, 0.3, 1, ref baseLine);
                    int textX = centerPt.X - (textSize.Width / 2);
                    int textY = centerPt.Y + (baseLine / 2);
                    CvInvoke.PutText(colorMat, leadIndexString, new Point(textX, textY + 30), FontFace.HersheyComplex, 0.3, new MCvScalar(50, 230, 50, 255));

                    textSize = CvInvoke.GetTextSize(akkonCountString, FontFace.HersheyComplex, 0.3, 1, ref baseLine);
                    textX = centerPt.X - (textSize.Width / 2);
                    textY = centerPt.Y + (baseLine / 2);
                    CvInvoke.PutText(colorMat, akkonCountString, new Point(textX, textY + 60), FontFace.HersheyComplex, 0.3, new MCvScalar(50, 230, 50, 255));
                }
            }
            return colorMat;
        }

        public void UpdateParam()
        {
            _mainForm.SlicePage.UpdateData();
        }
    }
}
