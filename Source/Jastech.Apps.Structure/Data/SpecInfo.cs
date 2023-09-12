using Newtonsoft.Json;

namespace Jastech.Apps.Structure.Data
{
    public class SpecInfo
    {
        [JsonProperty]
        public double AlignToleranceX_um { get; set; } = 6.5;

        [JsonProperty]
        public double AlignToleranceY_um { get; set; } = 150;

        [JsonProperty]
        public double AlignToleranceCx_um { get; set; } = 4;

        [JsonProperty]
        public double AlignStandard_um { get; set; }
    }
}
