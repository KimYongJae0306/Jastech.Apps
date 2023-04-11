using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure
{
    public class AlignParam
    {
        [JsonProperty]
        public string Name { get; set; } = "";

        [JsonProperty]
        public int LeadCount { get; set; } = 1;

        [JsonProperty]
        public CogCaliperParam CaliperParams { get; set; } = new CogCaliperParam();

        public void Dispose()
        {
            CaliperParams.Dispose();
        }

        public AlignParam DeepCopy()
        {
            AlignParam align = new AlignParam();
            align.LeadCount = LeadCount;
            align.CaliperParams = CaliperParams.DeepCopy();

            return align;
        }
    }
}
