using Jastech.Apps.Structure.Data;
using Jastech.Framework.Structure;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Jastech.Apps.Structure
{
    public class AppsInspModel : InspModel
    {
        private object _lock = new object();

        [JsonProperty]
        public int UnitCount { get; set; } = 1;

        [JsonProperty]
        public int TabCount { get; set; } = 5;

        [JsonProperty]
        public MaterialInfo MaterialInfo { get; set; } = new MaterialInfo();

        [JsonProperty]
        public double AxisSpeed { get; set; } = 48.0;

        [JsonProperty]
        public List<Unit> UnitList { get; set; } = new List<Unit>();

        public Unit GetUnit(string name)
        {
            Unit unit = null;
            lock(UnitList)
                unit = UnitList.Where(x => x.Name == name).FirstOrDefault();

            return unit;
        }

        public Unit GetUnit(UnitName name)
        {
            Unit unit = null;
            lock(UnitList)
                unit = UnitList.Where(x => x.Name == name.ToString()).FirstOrDefault();

            return unit;
        }

        public void AddUnit(Unit unit)
        {
            lock (UnitList)
                UnitList.Add(unit);
        }

        public List<Unit> GetUnitList()
        {
            lock (UnitList)
                return UnitList;
        }

        public void SetUnitList(List<Unit> newUnitList)
        {
            lock (UnitList)
            {
                foreach (var unit in UnitList)
                    unit.Dispose();

                UnitList.Clear();

                UnitList.AddRange(newUnitList.Select(x => x.DeepCopy()).ToList());
            }
        }

        public void SetTeachingList(List<TeachingInfo> teachingInfoList)
        {
            lock (UnitList)
            {
                foreach (var unit in UnitList)
                    unit.SetTeachingInfoList(teachingInfoList.ToList());
            }
        }
    }
}
