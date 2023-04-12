using Jastech.Apps.Structure.Parameters;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Framework.Macron.Akkon.Parameters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.Data
{
    public class Tab
    {
        [JsonProperty]
        public string Name { get; set; } = "";

        [JsonProperty]
        public int Index { get; set; }

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
        }

        public AlignParam GetAlignParam(ATTTabAlignName alignName)
        {
            return AlignParamList.Where(x => x.Name == alignName.ToString()).First();
        }

        public void SetAlignParam(ATTTabAlignName alignName, AlignParam alignParam)
        {
            if (alignParam == null)
                return;

            AlignParamList.Where(x => x.Name == alignName.ToString()).First().LeadCount = alignParam.LeadCount;
            AlignParamList.Where(x => x.Name == alignName.ToString()).First().CaliperParams = alignParam.CaliperParams.DeepCopy();
        }

        public MarkParam GetFPCMark(MarkDirecton direction, MarkName name)
        {
            return FpcMarkParamList.Where(x => x.Name == name && x.Direction == direction).First();
        }

        public MarkParam GetPanelMark(MarkDirecton direction, MarkName name)
        {
            return PanelMarkParamList.Where(x => x.Name == name && x.Direction == direction).First();
        }

        public AkkonParam GetAkkonParam()
        {
            return AkkonParam;
        }
    }
}
