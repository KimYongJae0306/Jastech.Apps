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
        public List<CogCaliperParam> AlignParams { get; set; } = new List<CogCaliperParam>();

        public Tab DeepCopy()
        {
            Tab tab = new Tab();
            tab.Name = Name;
            tab.Index = Index;
            tab.AlignParams = AlignParams.Select(x => x.DeepCopy()).ToList();

            return tab;
        }

        public void Dispose()
        {
            foreach (var item in AlignParams)
            {
                item.Dispose();
            }
            AlignParams.Clear();
        }

        public void SetAlignParams(List<CogCaliperParam> caliperParam)
        {
            if (caliperParam == null)
                return;

            foreach (var prevParam in AlignParams)
                prevParam.Dispose();

            AlignParams = caliperParam.Select(x => x.DeepCopy()).ToList();
        }
    }
}
