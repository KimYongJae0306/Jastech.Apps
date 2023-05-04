using Emgu.CV.Flann;
using Jastech.Apps.Structure.Parameters;
using Jastech.Framework.Device.LightCtrls;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Framework.Macron.Akkon.Parameters;
using Jastech.Framework.Structure;
using Jastech.Framework.Util.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.Data
{
    public class Unit
    {
        [JsonProperty]
        public string Name { get; set; } = "";

        [JsonProperty]
        public List<PreAlignParam> PreAligns { get; set; } = new List<PreAlignParam>();

        [JsonProperty]
        public List<LightParameter> LightParams { get; set; } = new List<LightParameter>();   // LineScan 용 조명 파라메터

        [JsonProperty]
        private List<Tab> TabList { get; set; } = new List<Tab>();

        [JsonProperty]
        public List<TeachingInfo> TeachingInfoList { get; set; } = new List<TeachingInfo>();

        public void AddTeachingInfo(TeachingInfo position)
        {
            TeachingInfoList.Add(position);
        }

        public TeachingInfo GetTeachingInfo(TeachingPosType type)
        {
            return TeachingInfoList.Where(x => x.Name == type.ToString()).First();
        }

        public Unit DeepCopy()
        {
            // Cognex Tool 때문에 개별 DeepCopy 호출 해줘야함(Json DeepCopy 안됨)
            Unit unit = new Unit();
            unit.Name = Name;
            unit.PreAligns = PreAligns.Select(x => x.DeepCopy()).ToList();
            unit.LightParams = LightParams.Select(x => x.DeepCopy()).ToList();
            unit.TabList = TabList.Select(x => x.DeepCopy()).ToList();
            unit.TeachingInfoList = TeachingInfoList.Select(x => x.DeepCopy()).ToList();
            return unit;
        }

        public void Dispose()
        {
            foreach (var preAlign in PreAligns)
                preAlign.Dispose();

            foreach (var tap in TabList)
                tap.Dispose();

            PreAligns.Clear();
            TabList.Clear();
        }

        public Tab GetTab(int index)
        {
            return TabList.Where(x => x.Index == index).First();
        }

        public Tab GetTab(string tabName)
        {
            return TabList.Where(x => x.Name == tabName).First();
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
