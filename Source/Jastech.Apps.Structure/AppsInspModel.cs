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
        public SpecInfo SpecInfo { get; set; } = new SpecInfo();

        [JsonProperty]
        public MaterialInfo MaterialInfo { get; set; } = new MaterialInfo();

        [JsonProperty]
        public int AxisSpeed { get; set; } = 48;

        [JsonProperty]
        public List<Unit> UnitList { get; private set; } = new List<Unit>();

        [JsonProperty]
        public double AnalogGain { get; set; } = 0;         // 공통

        [JsonProperty]
        public double DigitalGain { get; set; } = 0;        // TDI Mode

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
    }
}
