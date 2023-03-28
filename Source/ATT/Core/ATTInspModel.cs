using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATT.Core
{
    public class ATTInspModel : Jastech.Apps.Structure.AppsInspModel
    {
        [JsonProperty]
        public int TabCount { get; set; } = 5;
    }
}
