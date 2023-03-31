using Jastech.Apps.Structure.VisionTool;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Framework.Structure;
using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATT.Core
{
    public class ATTInspModelService : Jastech.Framework.Structure.Service.InspModelService
    {
        public override InspModel New()
        {
            var newInspModel = new ATTInspModel();

            foreach (PreAlignName type in Enum.GetValues(typeof(PreAlignName)))
            {
                CogPatternMatchingParam preAlign = new CogPatternMatchingParam();
                preAlign.Name = type.ToString();
                newInspModel.PreAlignParams.Add(preAlign);
            }

            //for (int i = 0; i < newInspModel.TabCount; i++)
            //{
            //    Tab tab = new Tab();
            //    // 필요한 Tool 추가 
            //    //
            //    tab.FPCCaliperAlignXList.Add(new CogCaliperParam());
            //    tab.FPCCaliperAlignYList.Add(new CogCaliperParam());

            //    tab.PanelCaliperAlignXList.Add(new CogCaliperParam());
            //    tab.PanelCaliperAlignYList.Add(new CogCaliperParam());

            //    newInspModel.TabList.Add(tab);
            //}
            //foreach (AlignName type in Enum.GetValues(typeof(AlignName)))
            //{
            //    CogCaliperParam align = new CogCaliperParam();
            //    align.Name = type.ToString();
            //    newInspModel.AlignParams.Add(align);
            //}

            return newInspModel;
        }

        public override InspModel Load(string filePath)
        {  
            var model = new ATTInspModel();

            JsonConvertHelper.LoadToExistingTarget<ATTInspModel>(filePath, model);

            string preAlignPath = Path.GetDirectoryName(filePath) + @"\PreAlign";
            foreach (var item in model.PreAlignParams)
                item.LoadTool(preAlignPath);

            string alignPath = Path.GetDirectoryName(filePath) + @"\Align";
            foreach (var item in model.AlignParams)
                item.LoadTool(alignPath);

            return model;
        }

        public override void Save(string filePath, InspModel model)
        {
            ATTInspModel attInspModel = model as ATTInspModel;

            JsonConvertHelper.Save(filePath, attInspModel);

            string preAlignPath = Path.GetDirectoryName(filePath) + @"\PreAlign";
            foreach (var item in attInspModel.PreAlignParams)
                item.SaveTool(preAlignPath);

            string alignPath = Path.GetDirectoryName(filePath) + @"\Align";
            foreach (var item in attInspModel.AlignParams)
                item.SaveTool(alignPath);
        }
    }
}
