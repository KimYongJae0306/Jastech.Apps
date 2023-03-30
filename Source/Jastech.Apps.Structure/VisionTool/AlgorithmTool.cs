using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms;
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
        public CogPatternMatching PatternAlgorithm = new CogPatternMatching();
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
