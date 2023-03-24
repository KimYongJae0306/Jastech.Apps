using Jastech.Apps.Structure.VisionTool;
using Jastech.Framework.Structure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure
{
    public class AppsInspModel : InspModel
    {
        [JsonProperty]
        public AlgorithmTool AlgorithmTool { get; set; } = new AlgorithmTool();
    }
}
