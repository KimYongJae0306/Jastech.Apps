using Cognex.VisionPro;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.Parameters;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro;
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
using System.Runtime.InteropServices;
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

        //public AppsPatternMatchingResult RunPatternMatching(Tab tab, ICogImage cogImage)
        //{
        //    var paramCount = tab.FpcMarkParamList.Count() / 2;

        //    Logger.Debug(LogType.Inspection, "Run Pattern Matching.");

        //    for (int i = 0; i < paramCount; i++)
        //    {
        //        var fpcParam = tab.FpcMarkParamList[i];
        //        var panelParam = tab.PanelMarkParamList[i];

        //        var fpcMarkResult = RunPatternMatch(cogImage, fpcParam.InspParam);
        //        var panelMarkResult = RunPatternMatch(cogImage, fpcParam.InspParam);

        //        if (fpcMarkResult == null || panelMarkResult == null)
        //        {
        //            string message = string.Format("RunPatternMatch is null. Index : {0}", i.ToString());
        //            Logger.Debug(LogType.Inspection, message);
        //            continue;
        //        }

        //        if (fpcMarkResult.Result == Result.OK && panelMarkResult.Result == Result.OK)
        //        {
        //            string message = string.Format("RunPatternMatch Result : FPC Result_{0}, Panel_Result_{1}"
        //                                    , fpcMarkResult.Result.ToString(), panelMarkResult.Result.ToString());
        //            Logger.Debug(LogType.Inspection, message);

        //            AppsPatternMatchingResult result = new AppsPatternMatchingResult();

        //            result.FpcResult = fpcMarkResult;
        //            result.PanelResult = panelMarkResult;

        //            return result;
        //        }
        //        else
        //        {
        //            string message = string.Format("RunPatternMatch Result : FPC Result_{0}, Panel Result_{1}"
        //                                    , fpcMarkResult.Result.ToString(), panelMarkResult.Result.ToString());
        //            Logger.Debug(LogType.Inspection, message);
        //        }
        //    }

        //    return null;

        //}

        public ICogImage ConvertCogImage(Mat image)
        {
            if (image == null)
                return null;

            int size = image.Width * image.Height * image.NumberOfChannels;
            byte[] dataArray = new byte[size];
            Marshal.Copy(image.DataPointer, dataArray, 0, size);

            ColorFormat format = image.NumberOfChannels == 1 ? ColorFormat.Gray : ColorFormat.RGB24;

            var cogImage = CogImageHelper.CovertImage(dataArray, image.Width, image.Height, format);

            return cogImage;
        }

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

            alignResult.Judgement = isFounded ? Judgement.OK : Judgement.Fail;

            if(alignResult.Judgement == Judgement.OK)
            {
                if (leadCount != alignResult.CogAlignResult.Count() / 2)
                    alignResult.Judgement = Judgement.NG;
            }
                
            return alignResult;
        }

        public CogAlignCaliperResult RunAlignY(ICogImage image, VisionProCaliperParam param)
        {
            CogAlignCaliperResult alignResult = new CogAlignCaliperResult();
            var result = AlignAlgorithm.RunAlignY(image, param);
            alignResult.AddAlignResult(result);

            bool isFounded = false;
            foreach (var item in alignResult.CogAlignResult)
            {
                if (item == null)
                    continue;

                isFounded |= item.Found;
            }

            alignResult.Judgement = isFounded ? Judgement.OK : Judgement.Fail;

            return alignResult;
        }

        public CogPatternMatchingResult RunPatternMatch(ICogImage image, VisionProPatternMatchingParam param)
        {
            if (image == null || param == null)
                return null;

            CogPatternMatchingResult matchingResult = PatternAlgorithm.Run(image, param);

            if (matchingResult == null)
                return null;

            if (matchingResult.MatchPosList.Count <= 0)
                matchingResult.Judgement = Judgement.Fail;
            else
            {
                if ((matchingResult.MaxScore * 100) >= param.Score)
                    matchingResult.Judgement = Judgement.OK;
                else
                    matchingResult.Judgement = Judgement.NG;
            }

            return matchingResult;
        }

        public List<AkkonResult> RunAkkon(Mat mat, AkkonParam akkonParam, int stageNo, int tabNo)
        {
            if (mat == null)
                return null;

            var marcon = akkonParam.MacronAkkonParam;
            var akkonRoiList = akkonParam.GetAkkonROIList();

            if(akkonRoiList.Count<=0)
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
