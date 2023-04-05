using Jastech.Apps.Structure.VisionTool;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Framework.Structure;
using Jastech.Framework.Util.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure
{
    public enum CameraName
    {
        LeftArea,
        RightArea,
        LinscanMIL0,
        LinscanVT0,
    }

    public class AppsInspModel : InspModel
    {
        [JsonProperty]
        public int UnitCount { get; set; } = 1;

        [JsonProperty]
        public int TabCount { get; set; } = 5;

        [JsonProperty]
        public List<Unit> UnitList { get; private set; } = new List<Unit>();

        public Unit GetUnit(string name)
        {
            return UnitList.Select(x => x.Name == name) as Unit;
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

            UnitList.AddRange(newUnitList);
        }
    }
}
