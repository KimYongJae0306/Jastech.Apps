using Cognex.VisionPro;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.VisionTool
{
    public class AlgorithmTool
    {
        private CogPatternMatching PatternAlgorithm { get; set; } = new CogPatternMatching();

        public CogPatternMatchingResult RunPreAlign(ICogImage image, CogPatternMatchingParam param)
        {
            if (image == null)
                return null;

            CogPatternMatchingResult matchingResult = PatternAlgorithm.Run(image, param);

            if (matchingResult.MatchPosList.Count <= 0)
                matchingResult.Result = Result.Fail;
            else
            {
                if (matchingResult.MaxScore >= param.Score)
                    matchingResult.Result = Result.Good;
                else
                    matchingResult.Result = Result.NG;
            }

            return matchingResult;
        }
        public CogCaliper CaliperAlgorithm = new CogCaliper();
    }

    public enum InspectionType
    {
        PreAlign,
        Align,
        Akkon,
    }

    public enum PreAlignName
    {
        MainLeft,
        MainRight,
        SubLeft1,
        SubRight1,
        SubLeft2,
        SubRight2,
        SubLeft3,
        SubRight3,
        SubLeft4,
        SubRight4,
    }

    public enum AlignName
    {
        Tab1,
    }
}
