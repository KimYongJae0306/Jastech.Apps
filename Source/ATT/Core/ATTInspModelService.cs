using Jastech.Apps.Structure;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Apps.Winform;
using Jastech.Framework.Device.Motions;
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

            for (int i = 0; i < newInspModel.UnitCount; i++)
            {
                Unit unit = new Unit();

                unit.Name = i.ToString(); // 임시 -> Apps에서 변경

                // Prev Align 등록
                foreach (ATTAlignName type in Enum.GetValues(typeof(ATTAlignName)))
                {
                    CogPatternMatchingParam preAlign = new CogPatternMatchingParam();
                    preAlign.Name = type.ToString();
                    unit.PreAlignParams.Add(preAlign);
                }

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

                newInspModel.AddUnit(unit);
                AddTeachingPosition(unit);
            }

            

            return newInspModel;
        }

        private void AddTeachingPosition(Unit unit)
        {
            var currentAxisHandler = AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Unit0);
            string unitName = "0";

            TeachingPosition t1 = new TeachingPosition();
            t1.CreateTeachingPosition(TeachingPositionType.Standby.ToString(), "Standby", currentAxisHandler);
            unit.AddTeachingPosition(t1);

            TeachingPosition t2 = new TeachingPosition();
            t2.CreateTeachingPosition(TeachingPositionType.Stage1_PreAlign_Left.ToString(), "Stage#1 PreAlign Left Position", currentAxisHandler);
            unit.AddTeachingPosition(t2);

            TeachingPosition t3 = new TeachingPosition();
            t3.CreateTeachingPosition(TeachingPositionType.Stage1_PreAlign_Right.ToString(), "Stage#1 PreAlign Right Position", currentAxisHandler);
            unit.AddTeachingPosition(t3);

            TeachingPosition t4 = new TeachingPosition();
            t4.CreateTeachingPosition(TeachingPositionType.Stage1_Scan_Start.ToString(), "Stage#1 ScanStart", currentAxisHandler);
            unit.AddTeachingPosition(t4);

            TeachingPosition t5 = new TeachingPosition();
            t5.CreateTeachingPosition(TeachingPositionType.Stage1_Scan_End.ToString(), "Stage#1 ScanEnd", currentAxisHandler);
            unit.AddTeachingPosition(t5);
        }

        public override InspModel Load(string filePath)
        {  
            var model = new ATTInspModel();

            JsonConvertHelper.LoadToExistingTarget<ATTInspModel>(filePath, model);

            string rootDir = Path.GetDirectoryName(filePath);

            foreach (var unit in model.GetUnitList())
            {
                string unitDir = rootDir + @"\Unit_" + unit.Name;

                string preAlignPath = unitDir + @"\PreAlign";

                //PreAlign Load
                foreach (var item in unit.PreAlignParams)
                    item.LoadTool(preAlignPath);

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

            foreach (var unit in attInspModel.GetUnitList())
            {
                string unitDir = Path.GetDirectoryName(filePath) + @"\Unit_" + unit.Name;
                string preAlignPath = unitDir + @"\PreAlign";

                //PreAlign 저장
                foreach (var item in unit.PreAlignParams)
                    item.SaveTool(preAlignPath);

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


    public enum ATTAlignName
    {
        MainLeft,
        MainRight,
        SubLeft1,
        SubRight1,
        SubLeft2,
        SubRight2,
        SubLeft3,
        SubRight3,
        SubLeft4,
        SubRight4,
    }

    public enum ATTTabAlignName
    {
        LeftFPCX,
        LeftPFCY,

        RightFPCX,
        RightPFCY,

        LeftPanelX,
        LeftPanelY,

        RightPanelX,
        RightPanelY,
    }
}
