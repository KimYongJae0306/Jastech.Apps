using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.VisionTool
{
    public class PatternMachingAlgorithmTool : IVisionTool
    {
        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public int TabNo { get; set; }

        [JsonProperty]
        public double Score { get; set; } = 70;

        [JsonProperty]
        public double MaxAngle { get; set; } = 1;
    }
}
