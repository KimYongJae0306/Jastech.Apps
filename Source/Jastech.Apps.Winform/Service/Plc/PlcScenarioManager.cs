using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Service.Plc.Maps;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Config;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Structure;
using Jastech.Framework.Structure.Helper;
using Jastech.Framework.Structure.Service;
using Jastech.Framework.Winform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform.Service.Plc
{
    public class PlcScenarioManager
    {
        private static PlcScenarioManager _instance = null;

        public OriginAllDelegate OriginAllEvent;

        public delegate bool OriginAllDelegate();
        public InspModelService InspModelService = null;

        public static PlcScenarioManager Instance()
        {
            if (_instance == null)
            {
                _instance = new PlcScenarioManager();
            }

            return _instance;
        }

        public void Initialize(InspModelService inspModelService)
        {
            InspModelService = inspModelService;
        }

        public void CommonCommandReceived(PlcCommonCommand command)
        {
            switch (command)
            {
                case PlcCommonCommand.Time_Change:
                    ReceivedTimeChange();
                    break;
                case PlcCommonCommand.Model_Change:
                    ChangeModelData();
                    break;
                case PlcCommonCommand.Model_Create:
                    CreateModelData(); // 질문 : Cretae 명령어 내려주면 자동으로 모델 변경해야하나? 아님 Change 명령어 더 보내주나?
                    break;
                case PlcCommonCommand.Model_Edit:
                    EditModelData(); // 질문 : 현재 선택된 모델만 수정???
                    break;
                case PlcCommonCommand.Command_Clear:
                    ReceivedCommandClear();
                    break;
                case PlcCommonCommand.Light_Off:
                    ReceivedLightOff();
                    break;
                default:
                    break;
            }
        }

        private void CreateModelData()
        {
            var manager = PlcControlManager.Instance();
            if (AppsStatus.Instance().IsRunning)
            {
                manager.WritePcStatusCommon(PlcCommonCommand.Model_Create, true);
                return;
            }
        
            AppsInspModel inspModel = InspModelService.New() as AppsInspModel;

            inspModel.Name = manager.GetValue(PlcCommonMap.PLC_PPID_ModelName);
            inspModel.CreateDate = AppsStatus.Instance().CurrentTime;
            inspModel.ModifiedDate = inspModel.CreateDate;
            inspModel.TabCount = Convert.ToInt32(manager.GetValue(PlcCommonMap.PLC_TabCount));
            inspModel.AxisSpeed = Convert.ToInt32(manager.GetValue(PlcCommonMap.PLC_Axis_X_Speed));
            inspModel.MaterialInfo = GetModelMaterialInfo();

            InspModelService.AddModelData(inspModel);

            ModelFileHelper.Save(ConfigSet.Instance().Path.Model, inspModel);

            manager.WritePcStatusCommon(PlcCommonCommand.Model_Create);
        }

        private void ChangeModelData()
        {
            var manager = PlcControlManager.Instance();
            string modelName = manager.GetValue(PlcCommonMap.PLC_PPID_ModelName);

            string modelDir = ConfigSet.Instance().Path.Model;
            string filePath = Path.Combine(modelDir, modelName, InspModel.FileName);

            if(File.Exists(filePath) == false || AppsStatus.Instance().IsRunning)
            {
                manager.WritePcStatusCommon(PlcCommonCommand.Model_Change, true);
                return;
            }

            ModelManager.Instance().CurrentModel = InspModelService.Load(filePath);

            manager.WritePcStatusCommon(PlcCommonCommand.Model_Change);
        }

        private void EditModelData()
        {
            var currentModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            var manager = PlcControlManager.Instance();

            string modelName = manager.GetValue(PlcCommonMap.PLC_PPID_ModelName);
            string modelDir = ConfigSet.Instance().Path.Model;
            string filePath = Path.Combine(modelDir, modelName, InspModel.FileName);

            if (File.Exists(filePath) == false || AppsStatus.Instance().IsRunning || currentModel == null)
            {
                manager.WritePcStatusCommon(PlcCommonCommand.Model_Edit, true);
                return;
            }

            if(currentModel.TabCount != Convert.ToInt32(manager.GetValue(PlcCommonMap.PLC_TabCount)))
            {
                // Tab Count는 변경 불가
                manager.WritePcStatusCommon(PlcCommonCommand.Model_Edit, true);
                return;
            }
            currentModel.Name = manager.GetValue(PlcCommonMap.PLC_PPID_ModelName);
            currentModel.ModifiedDate = AppsStatus.Instance().CurrentTime;
            currentModel.AxisSpeed = Convert.ToInt32(manager.GetValue(PlcCommonMap.PLC_Axis_X_Speed));
            currentModel.MaterialInfo = GetModelMaterialInfo();

            ModelFileHelper.Save(ConfigSet.Instance().Path.Model, currentModel);

            manager.WritePcStatusCommon(PlcCommonCommand.Model_Edit);
        }

        private MaterialInfo GetModelMaterialInfo()
        {
            MaterialInfo meterialInfo = new MaterialInfo();

            var manager = PlcControlManager.Instance();
            meterialInfo.PanelXSize_mm = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_PanelX_Size);
            meterialInfo.MarkToMark_mm = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_MarkToMarkDistance);
            meterialInfo.PanelEdgeToFirst_mm = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_PanelLeftEdgeToTab1LeftEdgeDistance);
            meterialInfo.MarkToMark_mm = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_MarkToMarkDistance);

            #region LeftOffset
            meterialInfo.LeftOffset.Tab0 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab0_Offset_Left);
            meterialInfo.LeftOffset.Tab1 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab1_Offset_Left);
            meterialInfo.LeftOffset.Tab2 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab2_Offset_Left);
            meterialInfo.LeftOffset.Tab3 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab3_Offset_Left);
            meterialInfo.LeftOffset.Tab4 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab4_Offset_Left);
            meterialInfo.LeftOffset.Tab5 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab5_Offset_Left);
            meterialInfo.LeftOffset.Tab6 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab6_Offset_Left);
            meterialInfo.LeftOffset.Tab7 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab7_Offset_Left);
            meterialInfo.LeftOffset.Tab8 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab8_Offset_Left);
            meterialInfo.LeftOffset.Tab9 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab9_Offset_Left);
            #endregion

            #region RrightOffset
            meterialInfo.RightOffset.Tab0 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab0_Offset_Right);
            meterialInfo.RightOffset.Tab1 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab1_Offset_Right);
            meterialInfo.RightOffset.Tab2 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab2_Offset_Right);
            meterialInfo.RightOffset.Tab3 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab3_Offset_Right);
            meterialInfo.RightOffset.Tab4 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab4_Offset_Right);
            meterialInfo.RightOffset.Tab5 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab5_Offset_Right);
            meterialInfo.RightOffset.Tab6 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab6_Offset_Right);
            meterialInfo.RightOffset.Tab7 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab7_Offset_Right);
            meterialInfo.RightOffset.Tab8 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab8_Offset_Right);
            meterialInfo.RightOffset.Tab9 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab9_Offset_Right);
            #endregion

            #region Tab To Tab Distacne
            meterialInfo.TabToTabDistance_mm.Tab0ToTab1 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_TabtoTab_Distance0);
            meterialInfo.TabToTabDistance_mm.Tab1ToTab2 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_TabtoTab_Distance1);
            meterialInfo.TabToTabDistance_mm.Tab2ToTab3 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_TabtoTab_Distance2);
            meterialInfo.TabToTabDistance_mm.Tab3ToTab4 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_TabtoTab_Distance3);
            meterialInfo.TabToTabDistance_mm.Tab4ToTab5 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_TabtoTab_Distance4);
            meterialInfo.TabToTabDistance_mm.Tab5ToTab6 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_TabtoTab_Distance5);
            meterialInfo.TabToTabDistance_mm.Tab6ToTab7 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_TabtoTab_Distance6);
            meterialInfo.TabToTabDistance_mm.Tab7ToTab8 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_TabtoTab_Distance7);
            meterialInfo.TabToTabDistance_mm.Tab8ToTab9 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_TabtoTab_Distance8);
            #endregion

            #region Tab Width
            meterialInfo.TabWidth_mm.Tab0 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab0_Width);
            meterialInfo.TabWidth_mm.Tab1 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab1_Width);
            meterialInfo.TabWidth_mm.Tab2 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab2_Width);
            meterialInfo.TabWidth_mm.Tab3 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab3_Width);
            meterialInfo.TabWidth_mm.Tab4 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab4_Width);
            meterialInfo.TabWidth_mm.Tab5 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab5_Width);
            meterialInfo.TabWidth_mm.Tab6 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab6_Width);
            meterialInfo.TabWidth_mm.Tab7 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab7_Width);
            meterialInfo.TabWidth_mm.Tab8 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab8_Width);
            meterialInfo.TabWidth_mm.Tab9 = manager.ConvertDoubleWordDoubleFormat_mm(PlcCommonMap.PLC_Tab9_Width);
            #endregion

            return meterialInfo;
        }

        private void ReceivedTimeChange()
        {
            AppsConfig.Instance().EnablePlcTime = true;
            AppsConfig.Instance().Save();

            PlcControlManager.Instance().WritePcStatusCommon(PlcCommonCommand.Time_Change);
        }

        private void ReceivedCommandClear()
        {
            var startAddress = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_Alive);
            var endAddress = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_AlignDataT);
            int length = endAddress.AddressNum + endAddress.WordSize - startAddress.AddressNum;

            PlcControlManager.Instance().ClearAddress(PlcCommonMap.PC_Alive, length);

            Thread.Sleep(50);

            PlcControlManager.Instance().WritePcStatusCommon(PlcCommonCommand.Command_Clear);
        }

        private void ReceivedLightOff()
        {
            var lightHanlder = DeviceManager.Instance().LightCtrlHandler;
            foreach (var light in lightHanlder)
                light.TurnOff();

            PlcControlManager.Instance().WritePcStatusCommon(PlcCommonCommand.Light_Off);
        }

        public void PlcCommandReceived(PlcCommand command)
        {
            switch (command)
            {
                case PlcCommand.StartInspection:
                    StartInspection();
                    break;
                case PlcCommand.StartPreAlign_AutoRun:
                    break;
                case PlcCommand.StartPreAlign_Manual:
                    break;
                case PlcCommand.Origin_All:
                    break;
                case PlcCommand.Move_StandbyPos:
                    break;
                case PlcCommand.Move_Left_AlignPos:
                    break;
                case PlcCommand.Move_Right_AlignPos:
                    break;
                case PlcCommand.Move_ScanStartPos:
                    break;
                case PlcCommand.Move_AlignDataX:
                    break;
                default:
                    break;
            }
        }

        private void StartInspection()
        {
            if(ModelManager.Instance().CurrentModel == null)
            {
                PlcControlManager.Instance().WritePcStatus(PlcCommand.StartInspection, true);
                return;
            }

            // 검사 시작 InspRunner
        }

        private void StartOriginAll()
        {
            AxisHandler allAxisHanlder = new AxisHandler("All");

            foreach (var axisHandler in MotionManager.Instance().AxisHandlerList)
            {
                allAxisHanlder.AddAxis(axisHandler.AxisList);
            }
            allAxisHanlder.StopMove();
            Thread.Sleep(100);

            var lafCtrlHandler = DeviceManager.Instance().LAFCtrlHandler;
            PlcControlManager.Instance().WritePcCommand(PcCommand.ServoOn_1);

            if (lafCtrlHandler.Count > 1)
                PlcControlManager.Instance().WritePcCommand(PcCommand.ServoOn_2);
            
            allAxisHanlder.TurnOnServo(true);

            foreach (var laf in lafCtrlHandler)
            {
                // LAF Homming
            }
            allAxisHanlder.StartHomeMove();
        }
    }
}
