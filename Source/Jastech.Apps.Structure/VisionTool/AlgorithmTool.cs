using Cognex.VisionPro;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.CogCaliper;

namespace Jastech.Apps.Structure.VisionTool
{
    public class AlgorithmTool
    {
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

        public void RunAkkon()
        {

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
