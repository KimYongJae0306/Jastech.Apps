using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.Data
{
    public class SpecInfo
    {
        [JsonProperty]
        public double AlignToleranceX_um { get; set; } = 1;

        [JsonProperty]
        public double AlignToleranceY_um { get; set; } = 1;

        [JsonProperty]
        public double AlignToleranceCx_um { get; set; } = 1;

        [JsonProperty]
        public double AlignStandard_um { get; set; }
    }
}
