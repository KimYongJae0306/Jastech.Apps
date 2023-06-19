using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.Parameters;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Core.Calibrations;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Algorithms.Akkon.Parameters;
using Jastech.Framework.Config;
using Jastech.Framework.Device.LightCtrls;
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

namespace ATT_UT_Remodeling.Core
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

            foreach (UnitName unitName in Enum.GetValues(typeof(UnitName)))
            {
                Unit unit = new Unit();

                unit.Name = unitName.ToString(); // 임시 -> Apps에서 변경

                // Calibration Mark 등록
                CalibrationParam calibrationParam = new CalibrationParam();
                calibrationParam.MarkName = "Calibration";
                calibrationParam.InspParam.Name = "Calibration";
                unit.SetCalibraionPram(calibrationParam);

                // LineScan 조명 Parameter 생성
                unit.LightParams.AddRange(CreateLightParameter());

                // PreAlign Mark 등록
                foreach (MarkName type in Enum.GetValues(typeof(MarkName)))
                {
                    PreAlignParam preAlignLeftMark = new PreAlignParam();
                    preAlignLeftMark.Name = type;
                    preAlignLeftMark.InspParam.Name = MarkDirection.Left.ToString() + type.ToString();
                    preAlignLeftMark.Direction = MarkDirection.Left;

                    PreAlignParam preAlignRightMark = new PreAlignParam();
                    preAlignRightMark.Name = type;
                    preAlignRightMark.InspParam.Name = MarkDirection.Right.ToString() + type.ToString();
                    preAlignRightMark.Direction = MarkDirection.Right;

                    unit.AddPreAlignParam(preAlignLeftMark);
                    unit.AddPreAlignParam(preAlignRightMark);
                }

                for (int tabIndex = 0; tabIndex < appInspModel.TabCount; tabIndex++)
                {
                    Tab tab = new Tab();
                    tab.Name = tabIndex.ToString(); // 임시
                    tab.Index = tabIndex;
                    tab.StageIndex = (int)unitName;

                    // Tab Fpc Mark 등록
                    foreach (MarkName type in Enum.GetValues(typeof(MarkName)))
                    {
                        MarkParam leftMark = new MarkParam();
                        leftMark.Name = type;
                        leftMark.InspParam.Name = MarkDirection.Left.ToString() + type.ToString();
                        leftMark.Direction = MarkDirection.Left;

                        MarkParam RightMark = new MarkParam();
                        RightMark.Name = type;
                        RightMark.InspParam.Name = MarkDirection.Right.ToString() + type.ToString();
                        RightMark.Direction = MarkDirection.Right;

                        tab.FpcMarkParamList.Add(leftMark);
                        tab.FpcMarkParamList.Add(RightMark);
                    }

                    // Tab Panel Mark 등록
                    foreach (MarkName type in Enum.GetValues(typeof(MarkName)))
                    {
                        MarkParam leftMark = new MarkParam();
                        leftMark.Name = type;
                        leftMark.InspParam.Name = MarkDirection.Left.ToString() + type.ToString();
                        leftMark.Direction = MarkDirection.Left;

                        MarkParam RightMark = new MarkParam();
                        RightMark.Name = type;
                        RightMark.InspParam.Name = MarkDirection.Right.ToString() + type.ToString();
                        RightMark.Direction = MarkDirection.Right;

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

                    tab.AkkonParam = new AkkonParam();
                    tab.AkkonParam.AkkonAlgoritmParam.Initalize();
                    tab.AkkonParam.AkkonAlgoritmParam.ImageFilterParam.AddMacronFilter();

                    int cnt = 0;
                    foreach (var item in tab.AkkonParam.GroupList)
                    {
                        AkkonGroup group = new AkkonGroup();
                        group.Index = cnt;
                        tab.AkkonParam.SetAkkonGroup(group.Index, group);
                    }

                    unit.AddTab(tab);
                }

                AddTeachingPosition(unit);
                appInspModel.AddUnit(unit);
            }
        }

        private void AddTeachingPosition(Unit unit)
        {
            var currentAxisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);

            TeachingInfo t1 = new TeachingInfo();
            t1.CreateTeachingInfo(TeachingPosType.Standby.ToString(), "Standby", currentAxisHandler);
            unit.AddTeachingInfo(t1);

            TeachingInfo t2 = new TeachingInfo();
            t2.CreateTeachingInfo(TeachingPosType.Stage1_PreAlign_Left.ToString(), "Stage#1 PreAlign Left Position", currentAxisHandler);
            unit.AddTeachingInfo(t2);

            TeachingInfo t3 = new TeachingInfo();
            t3.CreateTeachingInfo(TeachingPosType.Stage1_PreAlign_Right.ToString(), "Stage#1 PreAlign Right Position", currentAxisHandler);
            unit.AddTeachingInfo(t3);

            TeachingInfo t4 = new TeachingInfo();
            t4.CreateTeachingInfo(TeachingPosType.Stage1_Scan_Start.ToString(), "Stage#1 ScanStart", currentAxisHandler);
            unit.AddTeachingInfo(t4);

            TeachingInfo t5 = new TeachingInfo();
            t5.CreateTeachingInfo(TeachingPosType.Stage1_Scan_End.ToString(), "Stage#1 ScanEnd", currentAxisHandler);
            unit.AddTeachingInfo(t5);
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

            var lightCtrls = ConfigSet.Instance().Machine.GetDevices<LightCtrl>();
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

                // Calibration Mark Load
                string calibrationPath = unitDir + @"\Calibration";
                unit.CalibrationParam.InspParam.LoadTool(calibrationPath);

                string preAlignPath = unitDir + @"\PreAlign";

                //PreAlign Load
                foreach (var item in unit.PreAlignParamList)
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

                // Calibration Mark 저장
                string calibrationPath = unitDir + @"\Calibration";
                unit.CalibrationParam.InspParam.SaveTool(calibrationPath);

                //PreAlign 저장
                string preAlignPath = unitDir + @"\PreAlign";
                foreach (var item in unit.PreAlignParamList)
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

        public override void SaveExceptVpp(string filePath, InspModel model)
        {
            AppsInspModel attInspModel = model as AppsInspModel;

            JsonConvertHelper.Save(filePath, attInspModel);
        }
    }
}
