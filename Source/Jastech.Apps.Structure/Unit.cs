using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Framework.Structure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure
{
    public class Unit
    {
        [JsonProperty]
        public string Name { get; set; } = "";

        [JsonProperty]
        public List<CogPatternMatchingParam> PreAlignParams { get; set; } = new List<CogPatternMatchingParam>();

        [JsonProperty]
        private List<Tab> TabList { get; set; } = new List<Tab>();

        [JsonProperty]
        public List<TeachingPosition> PositionList { get; set; } = new List<TeachingPosition>();

        public void AddTeachingPosition(TeachingPosition position)
        {
            PositionList.Add(position);
        }

        public Unit DeepCopy()
        {
            Unit unit = new Unit();
            unit.Name = Name;
            unit.PreAlignParams = PreAlignParams.Select(x => x.DeepCopy()).ToList();
            unit.TabList = TabList.Select(x => x.DeepCopy()).ToList();
            unit.PositionList = PositionList.Select(x => x.DeepCopy()).ToList();

            return unit;
        }

        public void Dispose()
        {
            foreach (var item in PreAlignParams)
                item.Dispose();

            foreach (var item in TabList)
                item.Dispose();

            PreAlignParams.Clear();
            TabList.Clear();
        }

        public Tab GetTab(int index)
        {
            return TabList.Select(x => x.Index == index) as Tab;
        }

        public void AddTab(Tab tab)
        {
            TabList.Add(tab);
        }

        public List<Tab> GetTabList()
        {
            return TabList;
        }

        public void SetPreAlignParams(List<CogPatternMatchingParam> matchingParam)
        {
            if (matchingParam == null)
                return;

            foreach (var prevParam in PreAlignParams)
                prevParam.Dispose();

            PreAlignParams = matchingParam.Select(x => x.DeepCopy()).ToList();
        }
    }
}
