using Cognex.VisionPro;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Jastech.Apps.Structure.Parameters;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using Jastech.Framework.Macron.Akkon;
using Jastech.Framework.Macron.Akkon.Parameters;
using Jastech.Framework.Macron.Akkon.Results;
using Jastech.Framework.Util.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.CogCaliper;

namespace Jastech.Apps.Structure.VisionTool
{
    public class AlgorithmTool
    {
        private MacronAkkon AkkonAlgorithm { get; set; } = new MacronAkkon();

        private CogPatternMatching PatternAlgorithm { get; set; } = new CogPatternMatching();

        public CogAlignCaliper AlignAlgorithm { get; set; } = new CogAlignCaliper();

        public CogAlignCaliperResult RunAlignX(ICogImage image, VisionProCaliperParam param, int leadCount)
        {
            if (image == null || param == null)
                return null;

            CogAlignCaliperResult alignResult = new CogAlignCaliperResult();
            alignResult.AddAlignResult(AlignAlgorithm.RunAlignX(image, param, leadCount));

            bool isFounded = false;
            foreach (var item in alignResult.CogAlignResult)
            {
                isFounded |= item.Found;
            }

            alignResult.Result = isFounded ? Result.OK : Result.Fail;

            if(alignResult.Result == Result.OK)
            {
                if (leadCount != alignResult.CogAlignResult.Count() / 2)
                    alignResult.Result = Result.NG;
            }
            
                
            return alignResult;
        }

        public CogAlignCaliperResult RunAlignY(ICogImage image, VisionProCaliperParam param)
        {
            if (image == null || param == null)
                return null;

            CogAlignCaliperResult alignResult = new CogAlignCaliperResult();
            alignResult.AddAlignResult(AlignAlgorithm.RunAlignY(image, param));

            bool isFounded = false;
            foreach (var item in alignResult.CogAlignResult)
            {
                if (item == null)
                    continue;

                isFounded |= item.Found;
            }

            alignResult.Result = isFounded ? Result.OK : Result.Fail;

            return alignResult;
        }

        public CogPatternMatchingResult RunPreAlign(ICogImage image, VisionProPatternMatchingParam param)
        {
            if (image == null || param == null)
                return null;
            CogPatternMatchingResult matchingResult = PatternAlgorithm.Run(image, param);

            if (matchingResult.MatchPosList.Count <= 0)
                matchingResult.Result = Result.Fail;
            else
            {
                if ((matchingResult.MaxScore * 100) >= param.Score)
                    matchingResult.Result = Result.OK;
                else
                    matchingResult.Result = Result.NG;
            }

            return matchingResult;
        }

        public List<AkkonResult> RunAkkon(Mat mat, AkkonParam akkonParam, int stageNo, int tabNo)
        {

            var marcon = akkonParam.MacronAkkonParam;

            marcon.SliceHeight = mat.Height;

            if (AkkonAlgorithm.CreateDllBuffer(marcon))
            {
                AkkonAlgorithm.CreateImageBuffer(stageNo, tabNo, mat.Width, mat.Height, marcon.InspOption.InspResizeRatio);

                var akkonRoiList = akkonParam.GetAkkonROIList();

                AkkonAlgorithm.SetConvertROIData(akkonRoiList, 0, tabNo, new PointF(mat.Width / 2, mat.Height / 2), new PointF(0, 0), 0, akkonParam.MacronAkkonParam.InspOption.InspResizeRatio);

                AkkonAlgorithm.InitPrepareInspect();
                int overlapCount = AkkonAlgorithm.PrepareInspect(0, tabNo);
                marcon.InspOption.Overlap = overlapCount;
                
                AkkonAlgorithm.SetAkkonParam(0, tabNo, ref marcon);
                AkkonAlgorithm.EnableInspFlag();

                var results = AkkonAlgorithm.Inspect(0, tabNo, mat);
              
                return results;
            }
            else
            {
                Logger.Debug(LogType.Inspection, "ATT is not Initalized");
                return new List<AkkonResult>();
            }
        }

        public List<AkkonResult> RunCropAkkon(Mat mat, PointF cropOffset, AkkonParam akkonParam, int stageNo, int tabNo)
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
    }

    public enum InspectionType
    {
        PreAlign,
        Align,
        Akkon,
    }

    public enum AlignName
    {
        Tab1,
    }
}
