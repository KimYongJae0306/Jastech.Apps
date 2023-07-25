using Jastech.Apps.Structure.Parameters;
using Jastech.Framework.Algorithms.Akkon.Parameters;
using Jastech.Framework.Util.Helper;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Jastech.Apps.Structure.Data
{
    public class MarkParamter
    {
        [JsonProperty]
        public List<MarkParam> MainFpcMarkParamList { get; set; } = new List<MarkParam>(); 

        [JsonProperty]
        public List<MarkParam> MainPanelMarkParamList { get; set; } = new List<MarkParam>();

        [JsonProperty]
        public List<MarkParam> AlignFpcMarkParamList { get; set; } = new List<MarkParam>();

        [JsonProperty]
        public List<MarkParam> AlignPanelMarkParamList { get; set; } = new List<MarkParam>();

        public MarkParam GetFPCMark(MarkDirection markDirection, MarkName markName, bool isAlignParam)
        {
            if (isAlignParam == false)
                return GetMainFPCMark(markDirection, markName);
            else
                return GetAlignFPCMark(markDirection, markName);
        }

        public MarkParam GetPanelMark(MarkDirection markDirection, MarkName markName, bool isAlignParam)
        {
            if (isAlignParam == false)
                return GetMainPanelMark(markDirection, markName);
            else
                return GetAlignPanelMark(markDirection, markName);
        }

        private MarkParam GetMainFPCMark(MarkDirection direction, MarkName name)
        {
            return MainFpcMarkParamList.Where(x => x.Name == name && x.Direction == direction).FirstOrDefault();
        }

        private MarkParam GetMainPanelMark(MarkDirection direction, MarkName name)
        {
            return MainPanelMarkParamList.Where(x => x.Name == name && x.Direction == direction).FirstOrDefault();
        }

        private MarkParam GetAlignFPCMark(MarkDirection direction, MarkName name)
        {
            return AlignFpcMarkParamList.Where(x => x.Name == name && x.Direction == direction).FirstOrDefault();
        }

        private MarkParam GetAlignPanelMark(MarkDirection direction, MarkName name)
        {
            return AlignPanelMarkParamList.Where(x => x.Name == name && x.Direction == direction).FirstOrDefault();
        }

        public MarkParamter DeepCopy()
        {
            MarkParamter param = new MarkParamter();
            param.MainFpcMarkParamList = MainFpcMarkParamList.Select(x => x.DeepCopy()).ToList();
            param.MainPanelMarkParamList = MainPanelMarkParamList.Select(x => x.DeepCopy()).ToList();
            param.AlignFpcMarkParamList = AlignFpcMarkParamList.Select(x => x.DeepCopy()).ToList();
            param.AlignPanelMarkParamList = AlignPanelMarkParamList.Select(x => x.DeepCopy()).ToList();

            return param;
        }

        public void Dispose()
        {
            foreach (var fpc in MainFpcMarkParamList)
                fpc.Dispose();

            foreach (var panel in MainPanelMarkParamList)
                panel.Dispose();

            foreach (var fpc in AlignFpcMarkParamList)
                fpc.Dispose();

            foreach (var panel in AlignPanelMarkParamList)
                panel.Dispose();

            MainFpcMarkParamList.Clear();
            MainPanelMarkParamList.Clear();
            AlignFpcMarkParamList.Clear();
            AlignPanelMarkParamList.Clear();
        }
    }

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
        public MarkParamter MarkParamter = new MarkParamter();

        [JsonProperty]
        public List<AlignParam> AlignParamList { get; set; } = new List<AlignParam>();

        [JsonProperty]
        public AkkonParam AkkonParam { get; set; } = new AkkonParam();

        public Tab DeepCopy()
        {
            Tab tab = new Tab();
            tab.Name = Name;
            tab.Index = Index;
            tab.StageIndex = StageIndex;
            tab.AlignSpec = JsonConvertHelper.DeepCopy(AlignSpec) as AlignSpec;
            tab.MarkParamter = MarkParamter.DeepCopy();
            tab.AlignParamList = AlignParamList.Select(x => x.DeepCopy()).ToList();
            tab.AkkonParam = AkkonParam.DeepCopy();

            return tab;
        }

        public void Dispose()
        {
            foreach (var align in AlignParamList)
                align.Dispose();

            MarkParamter.Dispose();
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
        public float LeftSpecX_um { get; set; } = 0.5F;

        [JsonProperty]
        public float LeftSpecY_um { get; set; } = 1.0F;

        [JsonProperty]
        public float RightSpecX_um { get; set; } = 0.5F;

        [JsonProperty]
        public float RightSpecY_um { get; set; } = 1.0F;
    }
}
