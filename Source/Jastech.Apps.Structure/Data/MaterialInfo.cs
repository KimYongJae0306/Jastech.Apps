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
        public double Tab0Width_mm { get; set; }

        [JsonProperty]
        public double Tab1Width_mm { get; set; }

        [JsonProperty]
        public double Tab2Width_mm { get; set; }

        [JsonProperty]
        public double Tab3Width_mm { get; set; }

        [JsonProperty]
        public double Tab4Width_mm { get; set; }

        [JsonProperty]
        public double Tab5Width_mm { get; set; }

        [JsonProperty]
        public double Tab6Width_mm { get; set; }

        [JsonProperty]
        public double Tab7Width_mm { get; set; }

        [JsonProperty]
        public double Tab8Width_mm { get; set; }

        [JsonProperty]
        public double Tab9Width_mm { get; set; }

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

        [JsonProperty]
        public double TabToTabDistance8_mm { get; set; }

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

        public double GetTabWidth(int tabNo)
        {
            if (tabNo == 0)
                return Tab0Width_mm;
            else if (tabNo == 1)
                return Tab1Width_mm;
            else if (tabNo == 2)
                return Tab2Width_mm;
            else if (tabNo == 3)
                return Tab3Width_mm;
            else if (tabNo == 4)
                return Tab4Width_mm;
            else if (tabNo == 5)
                return Tab5Width_mm;
            else if (tabNo == 6)
                return Tab6Width_mm;
            else if (tabNo == 7)
                return Tab7Width_mm;
            else
                return 0;
        }
    }
}
