using Jastech.Framework.Device.LightCtrls;
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
        public List<PreAlign> PreAligns { get; set; } = new List<PreAlign>();

        [JsonProperty]
        public List<LightParameter> LightParams { get; set; } = new List<LightParameter>();   // LineScan 용 조명 파라메터

        [JsonProperty]
        private List<Tab> TabList { get; set; } = new List<Tab>();

        [JsonProperty]
        public List<TeachingPosition> TeachingPositions { get; set; } = new List<TeachingPosition>();

        public void AddTeachingPosition(TeachingPosition position)
        {
            TeachingPositions.Add(position);
        }

        public Unit DeepCopy()
        {
            Unit unit = new Unit();
            unit.Name = Name;
            unit.PreAligns = PreAligns.Select(x => x.DeepCopy()).ToList();
            unit.LightParams = LightParams.Select(x => x.DeepCopy()).ToList();
            unit.TabList = TabList.Select(x => x.DeepCopy()).ToList();
            unit.TeachingPositions = TeachingPositions.Select(x => x.DeepCopy()).ToList();

            return unit;
        }

        public void Dispose()
        {
            foreach (var item in PreAligns)
                item.Dispose();

            foreach (var item in TabList)
                item.Dispose();

            PreAligns.Clear();
            TabList.Clear();
        }

        public Tab GetTab(int index)
        {
            return TabList.Where(x => x.Index == index).First();
        }

        public void AddTab(Tab tab)
        {
            TabList.Add(tab);
        }

        public List<Tab> GetTabList()
        {
            return TabList;
        }
    }
}
