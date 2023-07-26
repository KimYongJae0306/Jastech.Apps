using Cognex.VisionPro;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.Data
{
    public class AppsPreAlignResult
    {
        public DateTime StartInspTime { get; set; }

        public DateTime EndInspTime { get; set; }

        public string Cell_ID { get; set; } = "";

        public Judgment Judgement { get; set; } = Judgment.OK;

        public PreAlignResult Left { get; set; } = new PreAlignResult();

        public PreAlignResult Right { get; set; } = new PreAlignResult();

        public double OffsetX { get; private set; } = 0.0;

        public double OffsetY { get; private set; } = 0.0;

        public double OffsetT { get; private set; } = 0.0;

        public void SetPreAlignResult(double offsetX, double offsetY, double offsetT)
        {
            OffsetX = offsetX;
            OffsetY = offsetY;
            OffsetT = offsetT;
        }

        public void Dispose()
        {
            Left?.Dispose();
            Right?.Dispose();
        }
    }

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
