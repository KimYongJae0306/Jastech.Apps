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

       public ICogImage ConvertCogImage(Mat image)
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

            alignResult.Judgement = isFounded ? Judgment.OK : Judgment.FAIL;

            if(alignResult.Judgement == Judgment.OK)
            {
                if (leadCount != alignResult.CogAlignResult.Count() / 2)
                    alignResult.Judgement = Judgment.NG;
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

            alignResult.Judgement = isFounded ? Judgment.OK : Judgment.FAIL;

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
                matchingResult.Judgement = Judgment.FAIL;
            else
            {
                if ((matchingResult.MaxScore * 100) >= param.Score)
                    matchingResult.Judgement = Judgment.OK;
                else
                    matchingResult.Judgement = Judgment.NG;
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
