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
        public List<MarkParam> Marks { get; set; } = new List<MarkParam>();

        [JsonProperty]
        public List<PreAlignParam> PreAligns { get; set; } = new List<PreAlignParam>();

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
            return JsonConvertHelper.DeepCopy(this) as Unit;
        }

        public void Dispose()
        {
            foreach (var mark in Marks)
                mark.Dispose();

            foreach (var preAlign in PreAligns)
                preAlign.Dispose();

            foreach (var tap in TabList)
                tap.Dispose();

            Marks.Clear();
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
