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

        public CogCaliper CaliperAlgorithm { get; set; } = new CogCaliper();

        public CogPatternMatchingResult RunPreAlign(ICogImage image, CogPatternMatchingParam param)
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
