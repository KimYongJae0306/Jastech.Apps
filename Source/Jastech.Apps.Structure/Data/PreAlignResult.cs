using Cognex.VisionPro;
using Emgu.CV;
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
        #region 속성
        public ICogImage CogImage { get; set; } = null;

        public ICogImage OverlayImage { get; set; } = null;

        public VisionProPatternMatchingResult MatchResult { get; set; } = null;
        #endregion

        #region 메서드
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
        #endregion
    }
}
