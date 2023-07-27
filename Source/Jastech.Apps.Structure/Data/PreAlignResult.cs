using Cognex.VisionPro;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.Data
{
    public class PreAlignResult
    {
        public ICogImage CogImage { get; set; } = null;

        public VisionProPatternMatchingResult MatchResult { get; set; } = null;

        public PreAlignResult DeepCopy()
        {
            PreAlignResult result = new PreAlignResult();

            result.CogImage = CogImage?.CopyBase(CogImageCopyModeConstants.CopyPixels);
            result.MatchResult = MatchResult?.DeepCopy();
            return result;
        }

        public void Dispose()
        {
            if (CogImage is CogImage8Grey grey)
            {
                grey.Dispose();
                grey = null;
            }

            MatchResult?.Dispose();
        }
    }
}
