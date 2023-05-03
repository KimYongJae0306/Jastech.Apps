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

        public double GetTabToTabDistance(int tabNo)
        {
            if (tabNo == 0)
                return TabToTabDistance0_mm;
            else if (tabNo == 1)
                return TabToTabDistance1_mm;
            else if (tabNo == 2)
                return TabToTabDistance2_mm;
            else if (tabNo == 3)
                return TabToTabDistance3_mm;
            else if (tabNo == 4)
                return TabToTabDistance4_mm;
            else if (tabNo == 5)
                return TabToTabDistance5_mm;
            else if (tabNo == 6)
                return TabToTabDistance6_mm;
            else if (tabNo == 7)
                return TabToTabDistance7_mm;
            else
                return 0;
        }
    }
}
