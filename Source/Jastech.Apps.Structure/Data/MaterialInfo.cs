using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.Data
{
    public class MaterialInfo
    {
        [JsonProperty]
        public double PanelXSize_mm { get; set; }

        [JsonProperty]
        public double MarkToMark_mm { get; set; }

        [JsonProperty]
        public double PanelEdgeToFirst_mm { get; set; }

        [JsonProperty]
        public double TabWidth_mm { get; set; }

        [JsonProperty]
        public double TabToTabDistance0_mm { get; set; }

        [JsonProperty]
        public double TabToTabDistance1_mm { get; set; }

        [JsonProperty]
        public double TabToTabDistance2_mm { get; set; }

        [JsonProperty]
        public double TabToTabDistance3_mm { get; set; }

        [JsonProperty]
        public double TabToTabDistance4_mm { get; set; }

        [JsonProperty]
        public double TabToTabDistance5_mm { get; set; }

        [JsonProperty]
        public double TabToTabDistance6_mm { get; set; }

        [JsonProperty]
        public double TabToTabDistance7_mm { get; set; }
    }
}
