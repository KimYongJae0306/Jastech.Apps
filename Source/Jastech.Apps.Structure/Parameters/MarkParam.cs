using Jastech.Apps.Structure.Data;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.Parameters
{
    public class MarkParam
    {
        [JsonProperty]
        public MarkName Name { get; set; } = MarkName.Main;

        [JsonProperty]
        public MarkDirection Direction { get; set; } = MarkDirection.Left;

        [JsonProperty]
        public VisionProPatternMatchingParam InspParam { get; set; } = new VisionProPatternMatchingParam();

        public MarkParam DeepCopy()
        {
            MarkParam markParam = new MarkParam();

            markParam.Name = Name;
            markParam.Direction = Direction;

            if (InspParam != null)
                markParam.InspParam = InspParam.DeepCopy();

            return markParam;
        }

        public void Dispose()
        {
            InspParam.Dispose();
        }
    }
}
