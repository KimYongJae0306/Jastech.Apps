using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Newtonsoft.Json;
using static Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters.VisionProCaliperParam;

namespace Jastech.Apps.Structure.Parameters
{
    public class AlignParam
    {
        #region 속성
        [JsonProperty]
        public string Name { get; set; } = string.Empty;

        [JsonProperty]
        public int LeadCount { get; set; } = 5;

        [JsonProperty]
        public double PanelToFpcOffset { get; set; } = 130.0;

        [JsonProperty]
        public CaliperSearchDirection SearchDirection { get; set; } = CaliperSearchDirection.InsideToOutside;

        [JsonProperty]
        public VisionProCaliperParam CaliperParams { get; set; } = new VisionProCaliperParam();
        #endregion

        #region 메서드
        public void Dispose()
        {
            CaliperParams.Dispose();
        }

        public AlignParam DeepCopy()
        {
            AlignParam align = new AlignParam();
            align.Name = Name;
            align.LeadCount = LeadCount;
            align.PanelToFpcOffset = PanelToFpcOffset;
            align.SearchDirection = SearchDirection;
            align.CaliperParams = CaliperParams.DeepCopy();

            return align;
        }
        #endregion
    }
}
