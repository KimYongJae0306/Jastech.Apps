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

        public void SetAlignParams(List<AlignParam> alignParamList)
        {
            if (alignParamList == null)
                return;

            foreach (var prevParam in AlignParamList)
                prevParam.Dispose();

            AlignParamList = AlignParamList.Select(x => x.DeepCopy()).ToList();
        }
    }
}
