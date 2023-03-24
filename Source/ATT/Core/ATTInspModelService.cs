using Jastech.Apps.Structure.VisionTool;
using Jastech.Framework.Structure;
using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATT.Core
{
    public class ATTInspModelService : Jastech.Framework.Structure.Service.InspModelService
    {
        public override InspModel New()
        {
            var newInspModel = new ATTInpModel();

            foreach (PreAlignName type in Enum.GetValues(typeof(PreAlignName)))
            {
                PatternMachingAlgorithmTool preAlign = new PatternMachingAlgorithmTool();
                preAlign.Name = type.ToString();
                newInspModel.AlgorithmTool.Align.Add(preAlign);
            }
            return newInspModel;
        }

        public override InspModel Load(string filePath)
        {
            var model = new ATTInpModel();

            JsonConvertHelper.LoadToExistingTarget<ATTInpModel>(filePath, model);

            return model;
        }
    }
}
