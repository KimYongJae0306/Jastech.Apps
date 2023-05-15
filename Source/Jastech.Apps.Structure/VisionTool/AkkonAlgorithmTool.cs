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
        private MacronAkkon AkkonAlgorithm { get; set; } = new MacronAkkon();

        public List<AkkonResult> RunAkkon(Mat mat, AkkonParam akkonParam, int stageNo, int tabNo)
        {
            if (mat == null)
                return null;

            var marcon = akkonParam.MacronAkkonParam;
            var akkonRoiList = akkonParam.GetAkkonROIList();

            if (akkonRoiList.Count <= 0)
            {
                Logger.Debug(LogType.Inspection, "Akkon Roi is nothing.");
                return new List<AkkonResult>();
            }

            float resizeRatio = 1.0f;
            if (marcon.InspParam.PanelInfo == (int)TargetType.COG)
                resizeRatio = 1.0f;
            else if (marcon.InspParam.PanelInfo == (int)TargetType.COF)
                resizeRatio = 0.5f;
            else if (marcon.InspParam.PanelInfo == (int)TargetType.FOG)
                resizeRatio = 0.6f;

            akkonParam.MacronAkkonParam.InspOption.InspResizeRatio = resizeRatio;
            akkonParam.MacronAkkonParam.DrawOption.DrawResizeRatio = resizeRatio;

            marcon.SliceHeight = mat.Height;

            if (AkkonAlgorithm.CreateDllBuffer(marcon))
            {
                AkkonAlgorithm.CreateImageBuffer(stageNo, tabNo, mat.Width, mat.Height, marcon.InspOption.InspResizeRatio);
                AkkonAlgorithm.SetConvertROIData(akkonRoiList, stageNo, tabNo, new PointF(mat.Width / 2, mat.Height / 2), new PointF(0, 0), 0, akkonParam.MacronAkkonParam.InspOption.InspResizeRatio);

                AkkonAlgorithm.InitPrepareInspect();
                int overlapCount = AkkonAlgorithm.PrepareInspect(stageNo, tabNo);
                marcon.InspOption.Overlap = overlapCount;

                AkkonAlgorithm.SetAkkonParam(stageNo, tabNo, ref marcon);
                AkkonAlgorithm.EnableInspFlag();
                var results = AkkonAlgorithm.Inspect(stageNo, tabNo, mat);

                return results;
            }
            else
            {
                Logger.Debug(LogType.Inspection, "ATT is not Initalized");
                return new List<AkkonResult>();
            }
        }

        public List<AkkonResult> RunCropAkkon(Mat mat, PointF cropOffset, AkkonParam akkonParam, int tabNo)
        {
            var marcon = akkonParam.MacronAkkonParam;
            if (AkkonAlgorithm.CreateDllBuffer(marcon))
            {
                AkkonAlgorithm.CreateImageBuffer(0, 0, mat.Width, mat.Height, marcon.InspOption.InspResizeRatio);

                var calcROIList = AkkonAlgorithm.GetCalcROI(cropOffset, akkonParam.GetAkkonROIList());

                AkkonAlgorithm.SetConvertROIData(calcROIList, 0, tabNo, new PointF(mat.Width / 2, mat.Height / 2), new PointF(0, 0), 0);

                AkkonAlgorithm.InitPrepareInspect();
                int overlapCount = AkkonAlgorithm.PrepareInspect(0, tabNo);
                marcon.InspOption.Overlap = overlapCount;

                AkkonAlgorithm.SetAkkonParam(0, tabNo, ref marcon);
                AkkonAlgorithm.EnableInspFlag();

                var results = AkkonAlgorithm.Inspect(0, tabNo, mat);

                return results;
            }
            return null;
        }

        public Mat LastAkkonResultImage(Mat mat, AkkonParam akkonParam, int stageNo, int tabNo)
        {
            var marcon = akkonParam.MacronAkkonParam;
            marcon.DrawOption.Contour = true;
            marcon.DrawOption.Center = false;
            return AkkonAlgorithm.GetDrawResultImage(mat, stageNo, tabNo, ref marcon);
        }

        public ICogImage GetResultImage(Mat mat, Tab tab, int tabIndex)
        {
            AkkonParam akkonParam = tab.AkkonParam;
            float resize = akkonParam.MacronAkkonParam.InspOption.InspResizeRatio;
            double width = Math.Truncate(mat.Width * resize);
            double height = Math.Truncate(mat.Height * resize);

            Mat testMat = new Mat((int)height, (int)width, DepthType.Cv8U, 1);
            Mat resultMatImage = LastAkkonResultImage(testMat, akkonParam, tab.StageIndex, tabIndex);

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

            return cogImage;
        }
    }
}
