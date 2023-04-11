using Jastech.Apps.Structure.Core;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure
{
    public class Tab
    {
        [JsonProperty]
        public string Name { get; set; } = "";

        [JsonProperty]
        public int Index { get; set; }

        [JsonProperty]
        public List<AlignParam> AlignParamList { get; set; } = new List<AlignParam>();

        public Tab DeepCopy()
        {
            Tab tab = new Tab();
            tab.Name = Name;
            tab.Index = Index;
            tab.AlignParamList = AlignParamList.Select(x => x.DeepCopy()).ToList();

            return tab;
        }

        public void Dispose()
        {
            foreach (var item in AlignParamList)
            {
                item.Dispose();
            }
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
    }
}
