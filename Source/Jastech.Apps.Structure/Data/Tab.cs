using Jastech.Apps.Structure.Parameters;
using Jastech.Framework.Algorithms.Akkon.Parameters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Jastech.Apps.Structure.Data
{
    public class Tab
    {
        [JsonProperty]
        public string Name { get; set; } = "";

        [JsonProperty]
        public int Index { get; set; }

        [JsonProperty]
        public int StageIndex { get; set; }

        [JsonProperty]
        public AlignSpec AlignSpec { get; set; } = new AlignSpec();

        [JsonProperty]
        public List<AlignParam> AlignParamList { get; set; } = new List<AlignParam>();

        [JsonProperty]
        public List<MarkParam> FpcMarkParamList { get; set; } = new List<MarkParam>();

        [JsonProperty]
        public List<MarkParam> PanelMarkParamList { get; set; } = new List<MarkParam>();

        [JsonProperty]
        public AkkonParam AkkonParam { get; set; } = new AkkonParam();

        public Tab DeepCopy()
        {
            Tab tab = new Tab();
            tab.Name = Name;
            tab.Index = Index;
            tab.StageIndex = StageIndex;

            tab.FpcMarkParamList = FpcMarkParamList.Select(x => x.DeepCopy()).ToList();
            tab.PanelMarkParamList = PanelMarkParamList.Select(x => x.DeepCopy()).ToList();
            tab.AlignParamList = AlignParamList.Select(x => x.DeepCopy()).ToList();
            tab.AkkonParam = AkkonParam.DeepCopy();

            return tab;
        }

        public void Dispose()
        {
            foreach (var fpc in FpcMarkParamList)
                fpc.Dispose();

            foreach (var panel in PanelMarkParamList)
                panel.Dispose();

            foreach (var align in AlignParamList)
                align.Dispose();

            FpcMarkParamList.Clear();
            PanelMarkParamList.Clear();
            AlignParamList.Clear();
            AkkonParam.Dispose();
        }

        public AlignParam GetAlignParam(ATTTabAlignName alignName)
        {
            return AlignParamList.Where(x => x.Name == alignName.ToString()).FirstOrDefault();
        }

        public void SetAlignParam(ATTTabAlignName alignName, AlignParam alignParam)
        {
            if (alignParam == null)
                return;

            AlignParamList.Where(x => x.Name == alignName.ToString()).First().LeadCount = alignParam.LeadCount;
            AlignParamList.Where(x => x.Name == alignName.ToString()).First().CaliperParams = alignParam.CaliperParams.DeepCopy();
        }

        public MarkParam GetFPCMark(MarkDirection direction, MarkName name)
        {
            //if (FpcMarkParamList.Count <= 0)
            //    return null;
            
            return FpcMarkParamList.Where(x => x.Name == name && x.Direction == direction).FirstOrDefault();
        }

        public MarkParam GetPanelMark(MarkDirection direction, MarkName name)
        {
            return PanelMarkParamList.Where(x => x.Name == name && x.Direction == direction).FirstOrDefault();
        }

        public AkkonGroup GetAkkonGroup(int index)
        {
            return AkkonParam.GetAkkonGroup(index);
        }

        public void SetAkkonGroup(int index, AkkonGroup newGroup)
        {
            AkkonParam.SetAkkonGroup(index, newGroup);
        }

        public void AdjustAkkonGroup(int newGroupCount)
        {
            AkkonParam.AdjustGroupCount(newGroupCount);
        }
    }

    public class AlignSpec
    {
        [JsonProperty]
        public double LeftSpecX_um { get; set; } = 0.5;

        [JsonProperty]
        public double LeftSpecY_um { get; set; } = 1;

        [JsonProperty]
        public double RightSpecX_um { get; set; } = 0.5;

        [JsonProperty]
        public double RightSpecY_um { get; set; } = 1;
    }
}
