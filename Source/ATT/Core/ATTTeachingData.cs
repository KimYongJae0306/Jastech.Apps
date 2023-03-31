using Jastech.Apps.Structure;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATT.Core
{
    public class ATTTeachingData
    {
        public List<Unit> UnitList { get; set; } = new List<Unit>();

        public void Initialize(ATTInspModel inspModel)
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

        public List<CogPatternMatchingParam> GetPreAlignParameters(string unitName)
        {
            Unit unit = GetUnit(unitName);

            return unit.PreAlignParams;
        }
    }
}
