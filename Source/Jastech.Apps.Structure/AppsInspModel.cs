using Jastech.Apps.Structure.Data;
using Jastech.Framework.Structure;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Jastech.Apps.Structure
{
    public class AppsInspModel : InspModel
    {
        [JsonProperty]
        public int UnitCount { get; set; } = 1;

        [JsonProperty]
        public int TabCount { get; set; } = 5;

        [JsonProperty]
        public MaterialInfo MaterialInfo { get; set; } = new MaterialInfo();

        [JsonProperty]
        public int AxisSpeed { get; set; } = 48;

        [JsonProperty]
        public List<Unit> UnitList { get; set; } = new List<Unit>();

        public Unit GetUnit(string name)
        {
            return UnitList.Where(x => x.Name == name).FirstOrDefault();
        }

        public Unit GetUnit(UnitName name)
        {
            return UnitList.Where(x => x.Name == name.ToString()).FirstOrDefault();
        }

        public void AddUnit(Unit unit)
        {
            UnitList.Add(unit);
        }

        public List<Unit> GetUnitList()
        {
            return UnitList;
        }

        public void SetUnitList(List<Unit> newUnitList)
        {
            foreach (var unit in UnitList)
                unit.Dispose();

            UnitList.Clear();

            UnitList.AddRange(newUnitList.Select(x => x.DeepCopy()).ToList());
        }

        public void SetTeachingList(List<TeachingInfo> teachingInfoList)
        {
            foreach (var unit in UnitList)
                unit.SetTeachingInfoList(teachingInfoList.ToList());
        }
    }
}
