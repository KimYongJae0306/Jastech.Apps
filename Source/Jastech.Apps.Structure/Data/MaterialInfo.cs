using Newtonsoft.Json;

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
        public TabWidth TabWidth_mm { get; set; } = new TabWidth();

        public TabToTabDistance TabToTabDistance_mm { get; set; } = new TabToTabDistance();

        public TabOffset LeftOffset { get; set; } = new TabOffset();

        public TabOffset RightOffset { get; set; } = new TabOffset();

        public double GetTabToTabDistance(int startTabNo)
        {
            if (startTabNo == 0)
                return TabToTabDistance_mm.Tab0ToTab1;
            else if (startTabNo == 1)
                return TabToTabDistance_mm.Tab1ToTab2;
            else if (startTabNo == 2)
                return TabToTabDistance_mm.Tab2ToTab3;
            else if (startTabNo == 3)
                return TabToTabDistance_mm.Tab3ToTab4;
            else if (startTabNo == 4)
                return TabToTabDistance_mm.Tab4ToTab5;
            else if (startTabNo == 5)
                return TabToTabDistance_mm.Tab5ToTab6;
            else if (startTabNo == 6)
                return TabToTabDistance_mm.Tab6ToTab7;
            else if (startTabNo == 7)
                return TabToTabDistance_mm.Tab7ToTab8;
            else if (startTabNo == 8)
                return TabToTabDistance_mm.Tab8ToTab9;
            else
                return 0;
        }

        public double GetTabWidth(int tabNo)
        {
            if (tabNo == 0)
                return TabWidth_mm.Tab0;
            else if (tabNo == 1)
                return TabWidth_mm.Tab1;
            else if (tabNo == 2)
                return TabWidth_mm.Tab2;
            else if (tabNo == 3)
                return TabWidth_mm.Tab3;
            else if (tabNo == 4)
                return TabWidth_mm.Tab4;
            else if (tabNo == 5)
                return TabWidth_mm.Tab5;
            else if (tabNo == 6)
                return TabWidth_mm.Tab6;
            else if (tabNo == 7)
                return TabWidth_mm.Tab7;
            else if (tabNo == 8)
                return TabWidth_mm.Tab8;
            else if (tabNo == 9)
                return TabWidth_mm.Tab9;
            else
                return 0;
        }

        public double GetLeftOffset(int tabNo)
        {
            if (tabNo == 0)
                return LeftOffset.Tab0;
            else if (tabNo == 1)
                return LeftOffset.Tab1;
            else if (tabNo == 2)
                return LeftOffset.Tab2;
            else if (tabNo == 3)
                return LeftOffset.Tab3;
            else if (tabNo == 4)
                return LeftOffset.Tab4;
            else if (tabNo == 5)
                return LeftOffset.Tab5;
            else if (tabNo == 6)
                return LeftOffset.Tab6;
            else if (tabNo == 7)
                return LeftOffset.Tab7;
            else if (tabNo == 8)
                return LeftOffset.Tab8;
            else if (tabNo == 9)
                return LeftOffset.Tab9;
            else
                return 0;
        }

        public double GetRightOffset(int tabNo)
        {
            if (tabNo == 0)
                return RightOffset.Tab0;
            else if (tabNo == 1)
                return RightOffset.Tab1;
            else if (tabNo == 2)
                return RightOffset.Tab2;
            else if (tabNo == 3)
                return RightOffset.Tab3;
            else if (tabNo == 4)
                return RightOffset.Tab4;
            else if (tabNo == 5)
                return RightOffset.Tab5;
            else if (tabNo == 6)
                return RightOffset.Tab6;
            else if (tabNo == 7)
                return RightOffset.Tab7;
            else if (tabNo == 8)
                return RightOffset.Tab8;
            else if (tabNo == 9)
                return RightOffset.Tab9;
            else
                return 0;
        }
    }

    public class TabWidth
    {
        [JsonProperty]
        public double Tab0 { get; set; }

        [JsonProperty]
        public double Tab1 { get; set; }

        [JsonProperty]
        public double Tab2 { get; set; }

        [JsonProperty]
        public double Tab3 { get; set; }

        [JsonProperty]
        public double Tab4 { get; set; }

        [JsonProperty]
        public double Tab5 { get; set; }

        [JsonProperty]
        public double Tab6 { get; set; }

        [JsonProperty]
        public double Tab7 { get; set; }

        [JsonProperty]
        public double Tab8 { get; set; }

        [JsonProperty]
        public double Tab9 { get; set; }
    }

    public class TabToTabDistance
    {
        [JsonProperty]
        public double Tab0ToTab1 { get; set; }

        [JsonProperty]
        public double Tab1ToTab2 { get; set; }

        [JsonProperty]
        public double Tab2ToTab3 { get; set; }

        [JsonProperty]
        public double Tab3ToTab4 { get; set; }

        [JsonProperty]
        public double Tab4ToTab5 { get; set; }

        [JsonProperty]
        public double Tab5ToTab6 { get; set; }

        [JsonProperty]
        public double Tab6ToTab7 { get; set; }

        [JsonProperty]
        public double Tab7ToTab8 { get; set; }

        [JsonProperty]
        public double Tab8ToTab9 { get; set; }
    }

    public class TabOffset
    {
        [JsonProperty]
        public double Tab0 { get; set; }

        [JsonProperty]
        public double Tab1 { get; set; }

        [JsonProperty]
        public double Tab2 { get; set; }

        [JsonProperty]
        public double Tab3 { get; set; }

        [JsonProperty]
        public double Tab4 { get; set; }

        [JsonProperty]
        public double Tab5 { get; set; }

        [JsonProperty]
        public double Tab6 { get; set; }

        [JsonProperty]
        public double Tab7 { get; set; }

        [JsonProperty]
        public double Tab8 { get; set; }

        [JsonProperty]
        public double Tab9 { get; set; }
    }
}
