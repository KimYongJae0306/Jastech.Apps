using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.Parameters;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.Data
{
    public class TeachingData
    {
        public List<Unit> UnitList { get; set; } = new List<Unit>();

        public void Initialize(AppsInspModel inspModel)
        {
            Dispose();

            foreach (var unit in inspModel.GetUnitList())
                UnitList.Add(unit.DeepCopy());
        }

        public void Dispose()
        {
            foreach (var unit in UnitList)
                unit.Dispose();

            UnitList.Clear();
        }

        public Unit GetUnit(string name)
        {
            return UnitList.Where(x => x.Name == name).First();
        }

        public List<PreAlignParam> GetPreAlign(string unitName)
        {
            Unit unit = GetUnit(unitName);

            return unit.PreAligns;
        }

        public VisionProPatternMatchingParam GetPreAlignParameters(string unitName, string preAlignName)
        {
            Unit unit = GetUnit(unitName);
            return unit.PreAligns.Where(x => x.Name == preAlignName).First().InspParam as VisionProPatternMatchingParam;
        }

        public List<Tab> GetAlignParameters(string unitName)
        {
            List<Tab> alignParam = new List<Tab>();

            Unit unit = GetUnit(unitName);

            for (int i = 0; i < unit.GetTabList().Count; i++)
            {
                var tab = unit.GetTab(i);
                alignParam.Add(tab);
            }

            return alignParam;
        }

        public List<Tab> GetAkkonParameters(string unitName)
        {
            List<Tab> akkonParam = new List<Tab>();

            Unit unit = GetUnit(unitName);

            for (int i = 0; i < UnitList.Count; i++)
            {
                var tab = unit.GetTab(i);
                akkonParam.Add(tab);
            }

            return akkonParam;
        }
    }
}
