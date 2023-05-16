using Cognex.VisionPro;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.Parameters;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Macron.Akkon;
using Jastech.Framework.Macron.Akkon.Results;
using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.VisionTool
{
    public class AkkonAlgorithmTool
    {
        public int SliceWidth { get; set; } = 2048;

        private List<List<int>> TotalSliceOverlap = new List<List<int>>();

        private List<List<int>> TotalSliceCnt = new List<List<int>>();

        private MacronAkkon AkkonAlgorithm { get; set; } = new MacronAkkon();

        public AkkonResult RunAkkonForTeachingData(Mat mat, Tab tab, int stageCount, int tabCount, float resizeRatio)
        {
            if (mat == null)
                return null;

            var marcon = tab.AkkonParam.MacronAkkonParam;
            var akkonRoiList = tab.AkkonParam.GetAkkonROIList();

            if (akkonRoiList.Count <= 0)
            {
                Logger.Debug(LogType.Inspection, "Akkon Roi is nothing.");
                return new AkkonResult();
            }

            marcon.DrawOption.DrawResizeRatio = resizeRatio;
            
            AkkonAlgorithm.CreateDllBuffer(stageCount, tabCount , SliceWidth, mat.Height, resizeRatio);

            TotalSliceOverlap.Clear();
            TotalSliceCnt.Clear();

            for (int stageNo = 0; stageNo < stageCount; stageNo++)
            {
                List<int> stageSliceOverLap = new List<int>();
                List<int> stageTotalSliceCnt = new List<int>();

                stageSliceOverLap.Clear();
                stageTotalSliceCnt.Clear();

                for (int tabNo = 0; tabNo < tabCount; tabNo++)
                {
                    if (tabNo == tab.Index)
                    {
                        AkkonAlgorithm.CreateImageBuffer(tab.StageIndex, tabNo, mat.Width, mat.Height, resizeRatio);

                        PointF centerPoint = new PointF(mat.Width / 2, mat.Height / 2); // ?? 검사 결과 넣기?
                        AkkonAlgorithm.SetConvertROIData(akkonRoiList, tab.StageIndex, tabNo, centerPoint, new PointF(0, 0), 0, resizeRatio);

                        int overlap = AkkonAlgorithm.GetCalcSliceOverlap(tab.StageIndex, tabNo);
                        int total = AkkonAlgorithm.GetCalcTotalSliceCnt(tab.StageIndex, tabNo, overlap, mat.Width, mat.Height);

                        stageSliceOverLap.Add(overlap);
                        stageTotalSliceCnt.Add(total);
                    }
                    else
                    {
                        stageSliceOverLap.Add(0);
                        stageTotalSliceCnt.Add(0);
                    }
                }
                TotalSliceOverlap.Add(stageSliceOverLap);
                TotalSliceCnt.Add(stageTotalSliceCnt);
            }

            int[][] intSliceCnt = TotalSliceCnt.Select(list => list.ToArray()).ToArray();
            AkkonAlgorithm.EnableInspFlag(intSliceCnt); //검사 FLag 할당
            AkkonAlgorithm.SetAkkonParam(tab.StageIndex, tab.Index, ref marcon);
            var result = AkkonAlgorithm.Inspect(tab.StageIndex, tab.Index, mat);

            return result;
        }
        public AkkonResult RunMultiAkkon(Mat mat, int stageNo, int tabNo)
        {
            var result = AkkonAlgorithm.Inspect(stageNo, tabNo, mat);

            return result;
        }

        public void PrepareMultiInspection(AppsInspModel inspModel, List<TabScanImage> tabscanImageList, float resizeRatio)
        {
            if (tabscanImageList.Count < 0)
                return;

            int stageCount = inspModel.UnitCount;
            int tabCount = inspModel.TabCount;

            int sliceHeight = tabscanImageList[0].SubImageWidth;
            
            AkkonAlgorithm.CreateDllBuffer(stageCount, tabCount, SliceWidth, sliceHeight, resizeRatio);

            TotalSliceOverlap.Clear();
            TotalSliceCnt.Clear();

            for (int stageNo = 0; stageNo < stageCount; stageNo++)
            {
                List<int> stageSliceOverLap = new List<int>();
                List<int> stageTotalSliceCnt = new List<int>();

                stageSliceOverLap.Clear();
                stageTotalSliceCnt.Clear();

                for (int tabNo = 0; tabNo < tabCount; tabNo++)
                {
                    Tab tab = inspModel.GetUnit(UnitName.Unit0).GetTab(tabNo);

                    var tabScanImageBuffer = GetTabScanImage(tabscanImageList, tabNo);
                    int width = tabScanImageBuffer.TotalGrabCount * tabScanImageBuffer.SubImageHeight;
                    int height = tabScanImageBuffer.SubImageWidth;

                    AkkonAlgorithm.CreateImageBuffer(stageNo, tabNo, width, height, resizeRatio);

                    var akkonRoiList = tab.AkkonParam.GetAkkonROIList();
                    PointF centerPoint = new PointF(width / 2, height / 2); // ?? 검사 결과 넣기?
                    AkkonAlgorithm.SetConvertROIData(akkonRoiList, tab.StageIndex, tabNo, centerPoint, new PointF(0, 0), 0, resizeRatio);

                    int overlap = AkkonAlgorithm.GetCalcSliceOverlap(tab.StageIndex, tabNo);
                    int total = AkkonAlgorithm.GetCalcTotalSliceCnt(tab.StageIndex, tabNo, overlap, width, height);

                    stageSliceOverLap.Add(overlap);
                    stageTotalSliceCnt.Add(total);

                    var marcon = tab.AkkonParam.MacronAkkonParam;
                    marcon.InspOption.Overlap = overlap;
                    marcon.DrawOption.DrawResizeRatio = resizeRatio;

                    AkkonAlgorithm.SetAkkonParam(tab.StageIndex, tab.Index, ref marcon);
                }

                TotalSliceOverlap.Add(stageSliceOverLap);
                TotalSliceCnt.Add(stageTotalSliceCnt);
            }

            int[][] intSliceCnt = TotalSliceCnt.Select(list => list.ToArray()).ToArray();
            AkkonAlgorithm.EnableInspFlag(intSliceCnt); //검사 FLag 할당
        }

        public List<AkkonResult> RunCropAkkon(Mat mat, PointF cropOffset, AkkonParam akkonParam, int tabNo)
        {
            //var marcon = akkonParam.MacronAkkonParam;
            //if (AkkonAlgorithm.CreateDllBuffer(marcon))
            //{
            //    AkkonAlgorithm.CreateImageBuffer(0, 0, mat.Width, mat.Height, marcon.InspOption.InspResizeRatio);

            //    var calcROIList = AkkonAlgorithm.GetCalcROI(cropOffset, akkonParam.GetAkkonROIList());

            //    AkkonAlgorithm.SetConvertROIData(calcROIList, 0, tabNo, new PointF(mat.Width / 2, mat.Height / 2), new PointF(0, 0), 0);

            //    AkkonAlgorithm.InitPrepareInspect();
            //    int overlapCount = AkkonAlgorithm.PrepareInspect(0, tabNo);
            //    marcon.InspOption.Overlap = overlapCount;

            //    AkkonAlgorithm.SetAkkonParam(0, tabNo, ref marcon);
            //    AkkonAlgorithm.EnableInspFlag();

            //    var results = AkkonAlgorithm.Inspect(0, tabNo, mat);

            //    return results;
            //}
            return null;
        }

        public Mat LastAkkonResultImage(Mat mat, AkkonParam akkonParam, int stageNo, int tabNo)
        {
            var marcon = akkonParam.MacronAkkonParam;
            marcon.DrawOption.Contour = true;
            marcon.DrawOption.Center = false;
            return AkkonAlgorithm.GetDrawResultImage(mat, stageNo, tabNo, ref marcon);
        }

        public ICogImage GetResultImage(int orgWidth, int orgHeight, Tab tab, float resizeRatio)
        {
            AkkonParam akkonParam = tab.AkkonParam;
            double width = Math.Truncate(orgWidth * resizeRatio);
            double height = Math.Truncate(orgHeight * resizeRatio);

            //double width = orgWidth;
            //double height = orgHeight;

            Mat testMat = new Mat((int)height, (int)width, DepthType.Cv8U, 1);
            Mat resultMatImage = LastAkkonResultImage(testMat, akkonParam, tab.StageIndex, tab.Index);

            Mat matR = MatHelper.ColorChannelSprate(resultMatImage, MatHelper.ColorChannel.R);
            Mat matG = MatHelper.ColorChannelSprate(resultMatImage, MatHelper.ColorChannel.G);
            Mat matB = MatHelper.ColorChannelSprate(resultMatImage, MatHelper.ColorChannel.B);

            byte[] dataR = new byte[matR.Width * matR.Height];
            Marshal.Copy(matR.DataPointer, dataR, 0, matR.Width * matR.Height);

            byte[] dataG = new byte[matG.Width * matG.Height];
            Marshal.Copy(matG.DataPointer, dataG, 0, matG.Width * matG.Height);

            byte[] dataB = new byte[matB.Width * matB.Height];
            Marshal.Copy(matB.DataPointer, dataB, 0, matB.Width * matB.Height);

            var cogImage = CogImageHelper.CovertImage(dataR, dataG, dataB, matB.Width, matB.Height);

            resultMatImage.Dispose();
            testMat.Dispose();
            matR.Dispose();
            matG.Dispose();
            matB.Dispose();

            return cogImage;
        }

        private TabScanImage GetTabScanImage(List<TabScanImage> tabscanImageList, int tabNo)
        {
            return tabscanImageList.Where(x => x.TabNo == tabNo).First();
        }
    }
}
