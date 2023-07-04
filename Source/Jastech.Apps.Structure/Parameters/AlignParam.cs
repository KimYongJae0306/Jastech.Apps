using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Newtonsoft.Json;

namespace Jastech.Apps.Structure.Parameters
{
    public class AlignParam
    {
        [JsonProperty]
        public string Name { get; set; } = string.Empty;

        [JsonProperty]
        public int LeadCount { get; set; } = 5;

        [JsonProperty]
        public VisionProCaliperParam CaliperParams { get; set; } = new VisionProCaliperParam();

        public void Dispose()
        {
            CaliperParams.Dispose();
        }

        public AlignParam DeepCopy()
        {
            AlignParam align = new AlignParam();
            align.Name = Name;
            align.LeadCount = LeadCount;
            align.CaliperParams = CaliperParams.DeepCopy();

            return align;
        }
    }
}
