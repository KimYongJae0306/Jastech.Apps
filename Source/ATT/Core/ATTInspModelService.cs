using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.Parameters;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Device.LightCtrls;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Framework.Macron.Akkon.Parameters;
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
            return new AppsInspModel();
        }

        public override void AddModelData(InspModel inspModel)
        {
            AppsInspModel appInspModel = inspModel as AppsInspModel;

            for (int i = 0; i < appInspModel.UnitCount; i++)
            {
                Unit unit = new Unit();

                unit.Name = i.ToString(); // 임시 -> Apps에서 변경

                // LineScan 조명 Parameter 생성
                unit.LightParams.AddRange(CreateLightParameter());

                for (int k = 0; k < appInspModel.TabCount; k++)
                {
                    Tab tab = new Tab();
                    tab.Name = k.ToString();
                    tab.Index = k;

                    // Tab Fpc Mark 등록
                    foreach (MarkName type in Enum.GetValues(typeof(MarkName)))
                    {
                        MarkParam leftMark = new MarkParam();
                        leftMark.Name = type;
                        leftMark.InspParam.Name = MarkDirecton.Left.ToString() + type.ToString();
                        leftMark.Direction = MarkDirecton.Left;

                        MarkParam RightMark = new MarkParam();
                        RightMark.Name = type;
                        RightMark.InspParam.Name = MarkDirecton.Right.ToString() + type.ToString();
                        RightMark.Direction = MarkDirecton.Right;

                        tab.FpcMarkParamList.Add(leftMark);
                        tab.FpcMarkParamList.Add(RightMark);
                    }
                    // Tab Panel Mark 등록
                    foreach (MarkName type in Enum.GetValues(typeof(MarkName)))
                    {
                        MarkParam leftMark = new MarkParam();
                        leftMark.Name = type;
                        leftMark.InspParam.Name = MarkDirecton.Left.ToString() + type.ToString();
                        leftMark.Direction = MarkDirecton.Left;

                        MarkParam RightMark = new MarkParam();
                        RightMark.Name = type;
                        RightMark.InspParam.Name = MarkDirecton.Right.ToString() + type.ToString();
                        RightMark.Direction = MarkDirecton.Right;

                        tab.PanelMarkParamList.Add(leftMark);
                        tab.PanelMarkParamList.Add(RightMark);
                    }

                    // Tab Align 등록
                    foreach (ATTTabAlignName type in Enum.GetValues(typeof(ATTTabAlignName)))
                    {
                        AlignParam align = new AlignParam();
                        align.Name = type.ToString();
                        tab.AlignParamList.Add(align);
                    }

                    // Tab Akkon 등록
                    AkkonParam akkon = new AkkonParam();
                    akkon.Name = tab.Name;
                    tab.AkkonParam = akkon.DeepCopy();

                    unit.AddTab(tab);
                }

                AddTeachingPosition(unit);
                appInspModel.AddUnit(unit);
            }
        }


        private void AddTeachingPosition(Unit unit)
        {
            var currentAxisHandler = AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Unit0);

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

        // PreAlign 검사 시
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
                if(light.Name == "LvsLight12V")
                {
                    LightParameter lightParameter = new LightParameter(light.Name);
                    LightValue lightValue = new LightValue(light.TotalChannelCount);
                    lightValue.LightLevels[light.ChannelNameMap["Ch.Blue"]] = 100;
                    lightValue.LightLevels[light.ChannelNameMap["Ch.RedSpot"]] = 100;

                    lightParameter.Add(light, lightValue);

                    lightParameterList.Add(lightParameter);
                }
                else if(light.Name == "LvsLight24V")
                {
                    LightParameter lightParameter = new LightParameter(light.Name);
                    LightValue lightValue = new LightValue(light.TotalChannelCount);
                    lightValue.LightLevels[light.ChannelNameMap["Ch.RedRing"]] = 100;

                    lightParameter.Add(light, lightValue);

                    lightParameterList.Add(lightParameter);
                }
            }

            return lightParameterList;
        }

        public override InspModel Load(string filePath)
        {  
            var model = new AppsInspModel();

            JsonConvertHelper.LoadToExistingTarget<AppsInspModel>(filePath, model);

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

                    //Tab FPC Mark 열기
                    string tabFpcMarkDir = tabDir + @"\FPC_Mark";
                    foreach (var alignParam in tab.FpcMarkParamList)
                        alignParam.InspParam.LoadTool(tabFpcMarkDir);

                    //Tab Panel Mark 열기
                    string tabPanelMarkDir = tabDir + @"\Panel_Mark";
                    foreach (var alignParam in tab.PanelMarkParamList)
                        alignParam.InspParam.LoadTool(tabPanelMarkDir);

                    //Tab Align 열기
                    string tabAlignDir = tabDir + @"\Align";
                    foreach (var alignParam in tab.AlignParamList)
                        alignParam.CaliperParams.LoadTool(tabAlignDir, alignParam.Name);
                }

            }
            return model;
        }

        public override void Save(string filePath, InspModel model)
        {
            AppsInspModel attInspModel = model as AppsInspModel;

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

                    //Tab FPC Mark 저장
                    string tabFpcMarkDir = tabDir + @"\FPC_Mark";
                    foreach (var alignParam in tab.FpcMarkParamList)
                        alignParam.InspParam.SaveTool(tabFpcMarkDir);

                    //Tab Panel Mark 저장
                    string tabPanelMarkDir = tabDir + @"\Panel_Mark";
                    foreach (var alignParam in tab.PanelMarkParamList)
                        alignParam.InspParam.SaveTool(tabPanelMarkDir);

                    //TabAlign 저장
                    string tabAlignDir = tabDir + @"\Align";
                    foreach (var alignParam in tab.AlignParamList)
                        alignParam.CaliperParams.SaveTool(tabAlignDir, alignParam.Name);
                }
            }
        }

        public void SaveExceptVpp(string filePath, InspModel model)
        {
            AppsInspModel attInspModel = model as AppsInspModel;

            JsonConvertHelper.Save(filePath, attInspModel);
        }
    }
}
