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
        //[JsonProperty]
        //public List<PatternMachingAlgorithmTool> PreAlign { get; set; } = new List<PatternMachingAlgorithmTool>();

        [JsonProperty]
        public List<IVisionTool> Align { get; set; } = new List<IVisionTool>();
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
