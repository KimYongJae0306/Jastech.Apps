using Cognex.VisionPro;
using Emgu.CV;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using System.Linq;
using System.Runtime.InteropServices;

namespace Jastech.Apps.Structure.VisionTool
{
    public class AlgorithmTool
    {
       
        private VisionProPatternMatching PatternAlgorithm { get; set; } = new VisionProPatternMatching();

        public CogAlignCaliper AlignAlgorithm { get; set; } = new CogAlignCaliper();

        public static ICogImage ConvertCogImage(Mat image)
        {
            if (image == null)
                return null;

            int size = image.Width * image.Height * image.NumberOfChannels;
            byte[] dataArray = new byte[size];
            Marshal.Copy(image.DataPointer, dataArray, 0, size);
            ColorFormat format = image.NumberOfChannels == 1 ? ColorFormat.Gray : ColorFormat.RGB24;
            var cogImage = VisionProImageHelper.ConvertImage(dataArray, image.Width, image.Height, format);

            return cogImage;
        }

        public VisionProAlignCaliperResult RunAlignX(ICogImage image, VisionProCaliperParam param, int leadCount)
        {
            if (image == null || param == null)
                return null;

            VisionProAlignCaliperResult alignResult = new VisionProAlignCaliperResult();
            var result = AlignAlgorithm.RunAlignX(image, param, leadCount);
            alignResult.AddAlignResult(result);

            bool isFounded = false;
            foreach (var item in alignResult.CogAlignResult)
            {
                isFounded |= item.Found;
            }

            alignResult.Judgement = isFounded ? Judgement.OK : Judgement.FAIL;

            if(alignResult.Judgement == Judgement.OK)
            {
                if (leadCount != alignResult.CogAlignResult.Count() / 2)
                    alignResult.Judgement = Judgement.NG;
            }
                
            return alignResult;
        }

        public VisionProAlignCaliperResult RunAlignY(ICogImage image, VisionProCaliperParam param)
        {
            VisionProAlignCaliperResult alignResult = new VisionProAlignCaliperResult();
            var result = AlignAlgorithm.RunAlignY(image, param);
            alignResult.AddAlignResult(result);

            bool isFounded = false;
            foreach (var item in alignResult.CogAlignResult)
            {
                if (item == null)
                    continue;

                isFounded |= item.Found;
            }

            alignResult.Judgement = isFounded ? Judgement.OK : Judgement.FAIL;

            return alignResult;
        }

        public VisionProPatternMatchingResult RunPatternMatch(ICogImage image, VisionProPatternMatchingParam param)
        {
            if (image == null || param == null)
                return null;
            VisionProPatternMatchingResult matchingResult = PatternAlgorithm.Run(image, param);
            if (matchingResult == null)
                return null;

            if (matchingResult.MatchPosList.Count <= 0)
                matchingResult.Judgement = Judgement.FAIL;
            else
            {
                if ((matchingResult.MaxScore * 100) >= param.Score)
                    matchingResult.Judgement = Judgement.OK;
                else
                {
                    //VisionProSearchMaxTool searchMaxTool = new VisionProSearchMaxTool();
                    //VisionProSearchMaxToolParam searchMaxParam = new VisionProSearchMaxToolParam();

                    //searchMaxParam.CreateTool();
                    //searchMaxParam.SetOrigin(param.GetOrigin());
                    //searchMaxParam.SetSearchRegion(param.GetSearchRegion() as CogRectangle);
                    //searchMaxParam.SetTrainRegion(param.GetTrainRegion() as CogRectangle);
                    //searchMaxParam.SetTrainImage(param.GetTrainImage());
                    //searchMaxParam.TrainImageMask(param.GetTrainImageMask());
                    //searchMaxParam.Train();
                    //var searchMaxResults = searchMaxTool.Run(image, searchMaxParam);
                    //if(searchMaxResults != null)
                    //{
                    //    if(searchMaxResults.MatchPosList.Count > 0)
                    //    {
                    //        if(searchMaxResults.MaxScore * 100 >= param.Score)
                    //        {
                    //            matchingResult.MatchPosList.Clear();
                    //            matchingResult.MatchPosList = searchMaxResults.MatchPosList;
                    //            matchingResult.Judgement = Judgement.OK;
                    //            return matchingResult;
                    //        }
                    //        else
                    //        {
                    //            matchingResult.MatchPosList.Clear();
                    //            matchingResult.MatchPosList = searchMaxResults.MatchPosList;
                    //        }
                    //    }
                    //}
                    matchingResult.Judgement = Judgement.NG;
                }
            }

            return matchingResult;
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
