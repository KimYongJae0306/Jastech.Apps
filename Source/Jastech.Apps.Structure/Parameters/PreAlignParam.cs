using Jastech.Apps.Structure.Data;
using Jastech.Framework.Device.LightCtrls;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.Parameters
{
    public class PreAlignParam
    {
        [JsonProperty]
        public MarkDirection Direction { get; set; } = MarkDirection.Left;

        [JsonProperty]
        public MarkName Name { get; set; } = MarkName.Main;

        [JsonProperty]
        public VisionProPatternMatchingParam InspParam { get; set; } = new VisionProPatternMatchingParam();

        [JsonProperty]
        public List<LightParameter> LightParams { get; set; } = null;

        public PreAlignParam DeepCopy()
        {
            PreAlignParam preAlign = new PreAlignParam();

            preAlign.Name = Name;
            preAlign.Direction = Direction;

            if (InspParam != null)
                preAlign.InspParam = InspParam.DeepCopy();

            if (preAlign.LightParams != null)
                preAlign.LightParams = LightParams.Select(x => x.DeepCopy()).ToList();

            return preAlign;
        }

        public void Dispose()
        {
            InspParam.Dispose();
        }
    }
}
