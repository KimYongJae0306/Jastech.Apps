using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.Parameters;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Algorithms.Akkon.Parameters;
using Jastech.Framework.Config;
using Jastech.Framework.Device.LightCtrls;
using Jastech.Framework.Structure;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform;
using System;
using System.Collections.Generic;
using System.IO;

namespace ATT_UT_IPAD.Core
{
    public class ATTInspModelService : Jastech.Framework.Structure.Service.InspModelService
    {
        private object _lock { get; set; } = new object();

        public override InspModel New()
        {
            return new AppsInspModel();
        }

        public override void AddModelData(InspModel inspModel)
        {
            AppsInspModel appInspModel = inspModel as AppsInspModel;

            int count = 0;
            foreach (UnitName unitName in Enum.GetValues(typeof(UnitName)))
            {
                if (count >= AppsConfig.Instance().UnitCount)
                    break;

                Unit unit = new Unit();

                unit.Name = unitName.ToString(); // 임시 -> Apps에서 변경
                unit.LightParam = CreateLightParameter();

                // LineScan 조명 Parameter 생성
                unit.CameraData = new LineCameraData();
                unit.CameraData.Name = "AkkonCamera";

                unit.AlignCamCameraData = new LineCameraData();
                unit.AlignCamCameraData.Name = "AlignCamera";

                for (int tabIndex = 0; tabIndex < appInspModel.TabCount; tabIndex++)
                {
                    Tab tab = new Tab();
                    tab.Name = tabIndex.ToString(); // 임시
                    tab.Index = tabIndex;
                    tab.StageIndex = (int)unitName;
                    tab.AlignCamMark = new MarkParamter();

                    // Akkon 카메라 Mark 등록
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

                        tab.Mark.PanelMarkList.Add(leftMark);
                        tab.Mark.PanelMarkList.Add(RightMark);
                    }

                    // Align 카메라 FPC Mark 등록
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

                        tab.AlignCamMark.FpcMarkList.Add(leftMark);
                        tab.AlignCamMark.FpcMarkList.Add(RightMark);
                    }

                    // Align 카메라 Panel Mark 등록
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

                        tab.AlignCamMark.PanelMarkList.Add(leftMark);
                        tab.AlignCamMark.PanelMarkList.Add(RightMark);
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

            TeachingInfo standbyPosition = new TeachingInfo();
            standbyPosition.CreateTeachingInfo(TeachingPosType.Standby.ToString(), "Standby", currentAxisHandler);
            unit.AddTeachingInfo(standbyPosition);

            TeachingInfo scanStartPosition = new TeachingInfo();
            scanStartPosition.CreateTeachingInfo(TeachingPosType.Stage1_Scan_Start.ToString(), "Stage#1 ScanStart", currentAxisHandler);
            unit.AddTeachingInfo(scanStartPosition);

            TeachingInfo scanEndPotisition = new TeachingInfo();
            scanEndPotisition.CreateTeachingInfo(TeachingPosType.Stage1_Scan_End.ToString(), "Stage#1 ScanEnd", currentAxisHandler);
            unit.AddTeachingInfo(scanEndPotisition);
        }

        // PreAlign 검사 시
        private List<LightParameter> CreatePreAlignLightParameter()
        {
            // PreAlign 사용할 경우 작성
            List<LightParameter> lightParamList = new List<LightParameter>();

            return lightParamList;
        }

        private LightParameter CreateLightParameter()
        {
            LightParameter lightParameter = new LightParameter("Light");

            var lightCtrlHandler = DeviceManager.Instance().LightCtrlHandler;
            var spotLightCtrl = lightCtrlHandler.Get("Spot");
            var ringLightCtrl = lightCtrlHandler.Get("Ring");

            lightParameter.Add(spotLightCtrl, new LightValue(spotLightCtrl.TotalChannelCount));
            lightParameter.Add(ringLightCtrl, new LightValue(ringLightCtrl.TotalChannelCount));

            return lightParameter;
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

                foreach (var tab in unit.GetTabList())
                {
                    string tabDir = unitDir + @"\" + "Tab_" + tab.Name;

                    string akkonDir = tabDir + @"\Akkon";
                    tab.LoadAkkonParam(akkonDir);

                    foreach (var group in tab.AkkonParam.GroupList)
                    {
                        var akkonRoi = group.AkkonROIList;
                        akkonRoi.Sort((x, y) => x.LeftTopX.CompareTo(y.LeftTopX));
                    }

                    //Tab Akkon Camera FPC Mark 열기
                    string tabMainFpcMarkDir = tabDir + @"\Mark\AkkonCamera\FPC_Mark";
                    foreach (var alignParam in tab.Mark.FpcMarkList)
                        alignParam.InspParam.LoadTool(tabMainFpcMarkDir);

                    //Tab Akkon Camera Panel Mark 열기
                    string tabMainPanelMarkDir = tabDir + @"\Mark\AkkonCamera\Panel_Mark";
                    foreach (var alignParam in tab.Mark.PanelMarkList)
                        alignParam.InspParam.LoadTool(tabMainPanelMarkDir);

                    //Tab Align Camera FPC Mark 열기
                    string tabAlignFpcMarkDir = tabDir + @"\Mark\AlignCamera\FPC_Mark";
                    foreach (var alignParam in tab.AlignCamMark.FpcMarkList)
                        alignParam.InspParam.LoadTool(tabAlignFpcMarkDir);

                    //Tab Align Camera Panel Mark 열기
                    string tabAlignPanelMarkDir = tabDir + @"\Mark\AlignCamera\Panel_Mark";
                    foreach (var alignParam in tab.AlignCamMark.PanelMarkList)
                        alignParam.InspParam.LoadTool(tabAlignPanelMarkDir);

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
            SaveAkkonROI(filePath, attInspModel);

            // Vpp 저장
            foreach (var unit in attInspModel.GetUnitList())
            {
                string unitDir = Path.GetDirectoryName(filePath) + @"\Unit_" + unit.Name;

                foreach (var tab in unit.GetTabList())
                {
                    string tabDir = unitDir + @"\" + "Tab_" + tab.Name;

                    string akkonDir = tabDir + @"\Akkon";
                    tab.SaveAkkonParam(akkonDir);

                    //Tab Akkon Camera FPC Mark 저장
                    string tabAkkonFpcMarkDir = tabDir + @"\Mark\AkkonCamera\FPC_Mark";
                    foreach (var alignParam in tab.Mark.FpcMarkList)
                        alignParam.InspParam.SaveTool(tabAkkonFpcMarkDir);

                    //Tab Akkon Camera Panel Mark 저장
                    string tabAkkonPanelMarkDir = tabDir + @"\Mark\AkkonCamera\Panel_Mark";
                    foreach (var alignParam in tab.Mark.PanelMarkList)
                        alignParam.InspParam.SaveTool(tabAkkonPanelMarkDir);

                    //Tab Align Camera FPC Mark 저장
                    string tabAlignFpcMarkDir = tabDir + @"\Mark\AlignCamera\FPC_Mark";
                    foreach (var alignParam in tab.AlignCamMark.FpcMarkList)
                        alignParam.InspParam.SaveTool(tabAlignFpcMarkDir);

                    //Tab Align Camera Panel Mark 저장
                    string tabAlignPanelMarkDir = tabDir + @"\Mark\AlignCamera\Panel_Mark";
                    foreach (var alignParam in tab.AlignCamMark.PanelMarkList)
                        alignParam.InspParam.SaveTool(tabAlignPanelMarkDir);

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
            SaveAkkonROI(filePath, attInspModel);
        }

        private void SaveAkkonROI(string filePath, AppsInspModel inspModel)
        {
            string path = Path.GetDirectoryName(filePath);
            string modelName = inspModel.Name;

            lock (_lock)
            {
                for (int unitIndex = 0; unitIndex < inspModel.GetUnitList().Count; unitIndex++)
                {
                    Unit unit = inspModel.UnitList[unitIndex];
                    for (int tabNo = 0; tabNo < unit.GetTabList().Count; tabNo++)
                    {
                        string fileName = string.Format("{0}_Unit{1}_Tab{2}_AkkonROI.txt", modelName, unitIndex, tabNo);
                        string savePath = Path.Combine(path, fileName);

                        Tab tab = unit.GetTab(tabNo);
                        var akkonROIList = tab.AkkonParam.GetAkkonROIList();

                        using (StreamWriter streamWriter = new StreamWriter(savePath, false))
                        {
                            foreach (var roi in akkonROIList)
                            {
                                string message = string.Format("{0} {1} {2} {3} {4} {5} {6} {7}",
                                                            (int)roi.LeftTopX, (int)roi.LeftTopY,
                                                            (int)roi.RightTopX, (int)roi.RightTopY,
                                                            (int)roi.RightBottomX, (int)roi.RightBottomY,
                                                            (int)roi.LeftBottomX, (int)roi.LeftBottomY);

                                streamWriter.WriteLine(message);
                            }
                        }
                    }
                }
            }
        }
    }
}
