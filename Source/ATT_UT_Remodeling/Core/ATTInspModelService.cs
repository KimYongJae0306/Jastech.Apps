using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Core;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Device.LightCtrls;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Framework.Structure;
using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATT_UT_Remodeling.Core
{
    public class ATTInspModelService : Jastech.Framework.Structure.Service.InspModelService
    {
        public override InspModel New()
        {
            var newInspModel = new ATTInspModel();

            for (int i = 0; i < newInspModel.UnitCount; i++)
            {
                Unit unit = new Unit();

                unit.Name = i.ToString(); // 임시 -> Apps에서 변경

                // Prev Align 등록
                foreach (ATTPreAlignName type in Enum.GetValues(typeof(ATTPreAlignName)))
                {
                    PreAlign preAlign = new PreAlign();
                    preAlign.Name = type.ToString();
                    preAlign.InspParam = new CogPatternMatchingParam();
                    preAlign.LightParams = new List<LightParameter>();
                    preAlign.LightParams.AddRange(CreatePreAlignLightParameter());

                    unit.PreAligns.Add(preAlign);
                }

                // LineScan 조명 Parameter 생성
                unit.LightParams.AddRange(CreateLightParameter());

                for (int k = 0; k < newInspModel.TabCount; k++)
                {
                    Tab tab = new Tab();
                    tab.Name = k.ToString();
                    tab.Index = k;

                    // Tab Align 등록
                    foreach (ATTTabAlignName type in Enum.GetValues(typeof(ATTTabAlignName)))
                    {
                        CogCaliperParam align = new CogCaliperParam();
                        align.Name = type.ToString();
                        tab.AlignParams.Add(align);
                    }

                    unit.AddTab(tab);
                }

                AddTeachingPosition(unit);
                newInspModel.AddUnit(unit);
            }

            return newInspModel;
        }

        private void AddTeachingPosition(Unit unit)
        {
            var currentAxisHandler = AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Unit0);

            // ATT 프로젝트 보고 작성
        }

    
        private List<LightParameter> CreatePreAlignLightParameter()
        {
            // PreAlign 사용할 경우 작성
            List<LightParameter> lightParamList = new List<LightParameter>();

            return lightParamList;
        }

        private List<LightParameter> CreateLightParameter()
        {
            List<LightParameter> lightParameterList = new List<LightParameter>();

            var lightCtrls = AppConfig.Instance().Machine.GetDevices<LightCtrl>();
            if (lightCtrls == null)
                return lightParameterList;

            foreach (var light in lightCtrls)
            {
                // ATT 프로젝트 보고 작성
            }

            return lightParameterList;
        }

        public override InspModel Load(string filePath)
        {
            var model = new ATTInspModel();

            JsonConvertHelper.LoadToExistingTarget<ATTInspModel>(filePath, model);

            string rootDir = Path.GetDirectoryName(filePath);

            // Vpp Load
            foreach (var unit in model.GetUnitList())
            {
                string unitDir = rootDir + @"\Unit_" + unit.Name;

                string preAlignPath = unitDir + @"\PreAlign";

                //PreAlign Load
                foreach (var item in unit.PreAligns)
                    item.InspParam.LoadTool(preAlignPath);

                foreach (var tab in unit.GetTabList())
                {
                    string tabDir = unitDir + @"\" + "Tab_" + tab.Name;

                    //TabAlign 저장
                    string tabAlignDir = tabDir + @"\Align";
                    foreach (var item in tab.AlignParams)
                        item.LoadTool(tabAlignDir);
                }

            }
            return model;
        }

        public override void Save(string filePath, InspModel model)
        {
            ATTInspModel attInspModel = model as ATTInspModel;

            JsonConvertHelper.Save(filePath, attInspModel);

            // Vpp 저장
            foreach (var unit in attInspModel.GetUnitList())
            {
                string unitDir = Path.GetDirectoryName(filePath) + @"\Unit_" + unit.Name;
                string preAlignPath = unitDir + @"\PreAlign";

                //PreAlign 저장
                foreach (var item in unit.PreAligns)
                    item.InspParam.SaveTool(preAlignPath);

                foreach (var tab in unit.GetTabList())
                {
                    string tabDir = unitDir + @"\" + "Tab_" + tab.Name;

                    //TabAlign 저장
                    string tabAlignDir = tabDir + @"\Align";
                    foreach (var item in tab.AlignParams)
                        item.SaveTool(tabAlignDir);
                }
            }
        }

        public void SaveExceptVpp(string filePath, InspModel model)
        {
            ATTInspModel attInspModel = model as ATTInspModel;

            JsonConvertHelper.Save(filePath, attInspModel);
        }
    }
}
