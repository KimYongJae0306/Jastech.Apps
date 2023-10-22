using Cognex.VisionPro;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Core.Calibrations;
using Jastech.Apps.Winform.Service.Plc.Maps;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Config;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Structure;
using Jastech.Framework.Structure.Helper;
using Jastech.Framework.Structure.Service;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform;
using Jastech.Framework.Winform.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform.Service.Plc
{
    public class PlcScenarioManager
    {
        #region 필드

        private static PlcScenarioManager _instance = null;

        private int _prevCommonCommand { get; set; } = 0;

        private int _prevCommand { get; set; } = 0;
        #endregion

        #region 속성
        public InspModelService InspModelService = null;

        public VisionXCalibration VisionXCalibration { get; set; } = new VisionXCalibration();

        private bool EnableActive { get; set; } = false;

        private Queue<int> PlcCommonCommandQueue { get; set; } = new Queue<int>();

        private Queue<int> PlcCommandQueue { get; set; } = new Queue<int>();

        private Task CommandTask { get; set; }

        private CancellationTokenSource CommandTaskCancellationTokenSource { get; set; }
        #endregion

        #region 이벤트
        public event InspRunnerEventHandler InspRunnerHandler;

        public event PreAlignRunnerEventHandler PreAlignRunnerHandler;

        public event CalibrationRunnerEventHandler CalibrationRunnerHandler;

        public event PlcScenarioMoveEventHandler MoveEventHandler;

        public event OriginAllDelegate OriginAllEvent;
        #endregion

        #region 델리게이트
        public delegate void InspRunnerEventHandler(bool isStart);

        public delegate void PreAlignRunnerEventHandler(bool isStart);

        public delegate void CalibrationRunnerEventHandler(bool isStart);

        public delegate bool PlcScenarioMoveEventHandler(PlcCommand plcCommand, TeachingPosType teachingPosType, out string alarmMessage);

        public delegate bool OriginAllDelegate();
        #endregion

        #region 생성자
        #endregion

        #region 메서드
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
            StartScenarioTask();
        }

        private void StartScenarioTask()
        {
            if (CommandTask != null)
                return;
            CommandTaskCancellationTokenSource = new CancellationTokenSource();
            CommandTask = new Task(ScenarioTask, CommandTaskCancellationTokenSource.Token);
            CommandTask.Start();
        }

        public void Release()
        {
            StopScenarioTask();
            PlcCommonCommandQueue.Clear();
            PlcCommandQueue.Clear();
        }

        private void StopScenarioTask()
        {
            if (CommandTask == null)
                return;
            CommandTaskCancellationTokenSource.Cancel();
            //CommandTask.Wait();
            CommandTask = null;
        }

        public void AddCommonCommand(int command)
        {
            if (_prevCommonCommand == command)
                return;

            if (command != 0 && EnableActive == false)
            {
                lock (PlcCommonCommandQueue)
                    PlcCommonCommandQueue.Enqueue(command);
						
				_prevCommonCommand = command;
            }
        }

        public void AddCommand(int command)
        {
            if (_prevCommand == command)
                return;

            if (command != 0 && EnableActive == false)
            {
                lock (PlcCommandQueue)
                    PlcCommandQueue.Enqueue(command);
					
				_prevCommand = command;
            }           
        }

        public int GetCommonCommand()
        {
            lock (PlcCommonCommandQueue)
            {
                if (PlcCommonCommandQueue.Count() > 0)
                    return PlcCommonCommandQueue.Dequeue();
                else
                    return 0;
            }
        }

        public int GetCommand()
        {
            lock (PlcCommandQueue)
            {
                if (PlcCommandQueue.Count() > 0)
                    return PlcCommandQueue.Dequeue();
                else
                    return 0;
            }
        }

        private void ScenarioTask()
        {
            while (true)
            {
                if (CommandTaskCancellationTokenSource.IsCancellationRequested)
                    break;

                if (GetCommonCommand() is int commonCommand)
                {
                    if (commonCommand != 0)
                    {
                        EnableActive = true;
                        PlcControlManager.Instance().ClearAddress(PlcCommonMap.PLC_Command_Common);
                        CommonCommandReceived((PlcCommonCommand)commonCommand);
                        _prevCommonCommand = 0;
                        EnableActive = false;
                    }
                }

                if (GetCommand() is int command)
                {
                    if (command != 0)
                    {
                        EnableActive = true;
                        PlcControlManager.Instance().ClearAddress(PlcCommonMap.PLC_Command);
                        PlcCommandReceived((PlcCommand)command);
                        _prevCommand = 0;
                        EnableActive = false;
                    }
                }

                Thread.Sleep(100);
            }
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
                    CreateModelData();
                    break;
                case PlcCommonCommand.Model_Edit:
                    EditModelData();
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

            string modelName = manager.GetValue(PlcCommonMap.PLC_PPID_ModelName).TrimEnd();
            string modelDir = ConfigSet.Instance().Path.Model;
            string filePath = Path.Combine(modelDir, modelName, InspModel.FileName);
            short command = -1;

            if (File.Exists(modelName) || AppsStatus.Instance().IsRunning)
            {
                // 모델이 존재O, 검사 중일 때
                command = manager.WritePcStatusCommon(PlcCommonCommand.Model_Create, true);
                Logger.Debug(LogType.Device, $"Write Fail CreateModelData.[{command}]");
                return;
            }

            AppsInspModel inspModel = InspModelService.New() as AppsInspModel;

            inspModel.Name = modelName;
            inspModel.CreateDate = DateTime.Now;
            inspModel.ModifiedDate = inspModel.CreateDate;
            inspModel.TabCount = Convert.ToInt32(manager.GetValue(PlcCommonMap.PLC_TabCount));
            inspModel.AxisSpeed = Convert.ToInt32(manager.GetValue(PlcCommonMap.PLC_Axis_X_Speed));
            inspModel.MaterialInfo = GetModelMaterialInfo();

            InspModelService.AddModelData(inspModel);

            ModelFileHelper.Save(ConfigSet.Instance().Path.Model, inspModel);

            command = manager.WritePcStatusCommon(PlcCommonCommand.Model_Create);
            Logger.Debug(LogType.Device, $"Write CreateModelData.[{command}]");
        }

        private void ChangeModelData()
        {
            var manager = PlcControlManager.Instance();
            string modelName = manager.GetValue(PlcCommonMap.PLC_PPID_ModelName);

            string modelDir = ConfigSet.Instance().Path.Model;
            string filePath = Path.Combine(modelDir, modelName, InspModel.FileName);
            short command = -1;

            if (File.Exists(filePath) == false || AppsStatus.Instance().IsRunning)
            {
                // 모델 존재X, 검사 중일 때
                command = manager.WritePcStatusCommon(PlcCommonCommand.Model_Change, true);
                Logger.Debug(LogType.Device, $"Write Fail ChangeModelData.[{command}]");
                return;
            }

            ModelManager.Instance().CurrentModel = InspModelService.Load(filePath);

            command = manager.WritePcStatusCommon(PlcCommonCommand.Model_Change);
            Logger.Debug(LogType.Device, $"Write ChangeModelData.[{command}]");
        }

        private void EditModelData()
        {
            var currentModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            var manager = PlcControlManager.Instance();

            string modelName = manager.GetValue(PlcCommonMap.PLC_PPID_ModelName);
            string modelDir = ConfigSet.Instance().Path.Model;
            string filePath = Path.Combine(modelDir, modelName, InspModel.FileName);
            short command = -1;

            if (File.Exists(filePath) == false || AppsStatus.Instance().IsRunning || currentModel == null)
            {
                // 모델 존재 X, 검사 중, 현재 모델이 없을 때
                command = manager.WritePcStatusCommon(PlcCommonCommand.Model_Edit, true);
                Logger.Debug(LogType.Device, $"Write Fail EditModelData.[{command}]");
                return;
            }

            if (currentModel.TabCount != Convert.ToInt32(manager.GetValue(PlcCommonMap.PLC_TabCount)))
            {
                // Tab Count는 변경 불가
                command = manager.WritePcStatusCommon(PlcCommonCommand.Model_Edit, true);
                Logger.Debug(LogType.Device, $"Write Fail EditModelData[{command}] : Tab Count not Changed.");
                return;
            }
            currentModel.Name = manager.GetValue(PlcCommonMap.PLC_PPID_ModelName);
            currentModel.ModifiedDate = DateTime.Now;
            currentModel.AxisSpeed = Convert.ToInt32(manager.GetValue(PlcCommonMap.PLC_Axis_X_Speed));
            currentModel.MaterialInfo = GetModelMaterialInfo();

            ModelFileHelper.Save(ConfigSet.Instance().Path.Model, currentModel);

            command = manager.WritePcStatusCommon(PlcCommonCommand.Model_Edit);
            Logger.Debug(LogType.Device, $"Write EditModelData.[{command}]");
        }

        public MaterialInfo GetModelMaterialInfo()
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
            var manager = PlcControlManager.Instance();

            string yyyy = manager.GetValue(PlcCommonMap.PLC_Time_Year);
            string MM = manager.GetValue(PlcCommonMap.PLC_Time_Month);
            string dd = manager.GetValue(PlcCommonMap.PLC_Time_Day);
            string hh = manager.GetValue(PlcCommonMap.PLC_Time_Hour);
            string mm = manager.GetValue(PlcCommonMap.PLC_Time_Minute);
            string ss = manager.GetValue(PlcCommonMap.PLC_Time_Second);
            string time = string.Format("{0}-{1}-{2} {3}:{4}:{5}", yyyy, MM, dd, hh, mm, ss);

            try
            {
                DateTime changeTime = Convert.ToDateTime(time);
                SystemHelper.SetSystemTime(changeTime);

                short command = PlcControlManager.Instance().WritePcStatusCommon(PlcCommonCommand.Time_Change);
                Logger.Debug(LogType.Device, $"Write TimeChanged.[{command}]");

            }
            catch (Exception err)
            {
                PlcControlManager.Instance().WritePcStatusCommon(PlcCommonCommand.Time_Change, true);
                Logger.Debug(LogType.Device, $"Fail Write TimeChanged.[{err}]");
            }

        }

        private void ReceivedCommandClear()
        {
            var startAddress = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_Alive);
            var endAddress = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_AlignDataT);
            int length = endAddress.AddressNum + endAddress.WordSize - startAddress.AddressNum;

            PlcControlManager.Instance().ClearAddress(PlcCommonMap.PC_Alive, length);
            Thread.Sleep(50);

            short command = PlcControlManager.Instance().WritePcStatusCommon(PlcCommonCommand.Command_Clear);
            Logger.Debug(LogType.Device, $"Write ClearCommand.[{command}]");
        }

        private void ReceivedLightOff()
        {
            var lightHandler = DeviceManager.Instance().LightCtrlHandler;
            foreach (var light in lightHandler)
                light.TurnOff();

            short command = PlcControlManager.Instance().WritePcStatusCommon(PlcCommonCommand.Light_Off);
            Logger.Debug(LogType.Device, $"Write LightOff.[{command}]");
        }

        public void PlcCommandReceived(PlcCommand command)
        {
            switch (command)
            {
                case PlcCommand.StartInspection:
                    StartInspection();
                    break;

                case PlcCommand.StartPreAlign:
                    StartPreAlign();
                    break;

                case PlcCommand.Calibration:
                    if (AppsStatus.Instance().IsCalibrationing == false)
                    {
                        if(StartMovePosition(PlcCommand.Move_Left_AlignPos, TeachingPosType.Stage1_PreAlign_Left, false))
                        {
                            Calibration(CalibrationMode.XYT);
                            AppsStatus.Instance().IsCalibrationing = true;
                        }
                        else
                        {
                            short retCommand = PlcControlManager.Instance().WritePcStatus(command, true);
                            Logger.Debug(LogType.Device, $"Write Fail Calibration[{retCommand}].");
                        }
                    }
                    break;

                case PlcCommand.Origin_All:
                    StartOriginAll();
                    break;

                case PlcCommand.Move_StandbyPos:
                    StartMovePosition(PlcCommand.Move_StandbyPos, TeachingPosType.Standby, true);
                    break;

                case PlcCommand.Move_Left_AlignPos:
                    StartMovePosition(PlcCommand.Move_Left_AlignPos, TeachingPosType.Stage1_PreAlign_Left, true);
                    break;

                case PlcCommand.Move_Right_AlignPos:
                    StartMovePosition(PlcCommand.Move_Right_AlignPos, TeachingPosType.Stage1_PreAlign_Right, true);
                    break;

                case PlcCommand.Move_ScanStartPos:
                    StartMovePosition(PlcCommand.Move_ScanStartPos, TeachingPosType.Stage1_Scan_Start, true);
                    break;

                default:
                    break;
            }
        }

        private void StartInspection()
        {
            if (ModelManager.Instance().CurrentModel == null)
            {
                short command = PlcControlManager.Instance().WritePcStatus(PlcCommand.StartInspection, true);
                Logger.Debug(LogType.Device, $"Write Fail Start Inspection(Current Model is null).[{command}]");
                return;
            }
            // 검사 시작 InspRunner
            InspRunnerHandler?.Invoke(true);
        }

        private void Calibration(CalibrationMode calibrationMode)
        {
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            if (inspModel == null)
            {
                short command = PlcControlManager.Instance().WritePcStatus(PlcCommand.Calibration, true);
                Logger.Debug(LogType.Device, $"Write Fail StartPreAlign[{command}] : Current Model is null.");
                return;
            }

            var unit = inspModel.GetUnit(UnitName.Unit0);
            if (unit == null)
            {
                short command = PlcControlManager.Instance().WritePcStatus(PlcCommand.Calibration, true);
                Logger.Debug(LogType.Device, $"Write Fail StartPreAlign[{command}] : Unit is null.");
                return;
            }

            var param = unit.PreAlign.CalibrationParam.InspParam;
            if (param == null)
            {
                short command = PlcControlManager.Instance().WritePcStatus(PlcCommand.Calibration, true);
                Logger.Debug(LogType.Device, $"Write Fail StartPreAlign[{command}] : Calibration Param is null.");
                return;
            }

            VisionXCalibration.UnitName = UnitName.Unit0;
            VisionXCalibration.SetInterval(intervalX: 1.5, intervalY: 1.5);
            VisionXCalibration.SetParam(param);
            VisionXCalibration.SetCalibrationMode(calibrationMode);
            VisionXCalibration.StartCalSeqRun();
        }

        private void StartPreAlign()
        {
            if (ModelManager.Instance().CurrentModel == null)
            {
                short command = PlcControlManager.Instance().WritePcStatus(PlcCommand.StartPreAlign, true);
                Logger.Debug(LogType.Device, $"Write Fail StartPreAlign[{command}] : Current Model is null.");
                return;
            }

            // prealign 검사 시작 InspRunner
            PreAlignRunnerHandler?.Invoke(true);
        }

        private bool StartMovePosition(PlcCommand plcCommand, TeachingPosType teachingPosType, bool isWritePlc)
        {
            short command = 0;
            if (ModelManager.Instance().CurrentModel == null)
            {
                if (isWritePlc)
                    command = PlcControlManager.Instance().WritePcStatus(plcCommand, true);

                Logger.Debug(LogType.Device, $"Write Fail Move Position(Current Model is null).[{command}]");
                return false;
            }

            if(MoveEventHandler != null)
            {
                string alarmMessage = "";
                bool isSuccess = MoveEventHandler.Invoke(plcCommand, teachingPosType, out alarmMessage);

                if (isWritePlc)
                    command = PlcControlManager.Instance().WritePcStatus(plcCommand, isSuccess);

                Logger.Write(LogType.Device, $"Send to PLC {plcCommand.ToString()}.[{command}]");

                if(isSuccess == false)
                {
                    MessageConfirmForm form = new MessageConfirmForm();
                    form.Message = alarmMessage;
                    form.ShowDialog();
                    return false;
                }
            }
            else
            {
                if (isWritePlc)
                    command = PlcControlManager.Instance().WritePcStatus(plcCommand, false);

                Logger.Write(LogType.Device, $"Move Event is null {plcCommand.ToString()}.[{command}]");
                return false;
            }
            return true;
        }
    
        private void StartOriginAll()
        {
            if (ModelManager.Instance().CurrentModel == null)
            {
                short command = PlcControlManager.Instance().WritePcStatus(PlcCommand.Origin_All, true);
                Logger.Debug(LogType.Device, $"Send to PLC Orgin All [{command}] : Current Model is null.");
                return;
            }

            if (OriginAllEvent != null)
            {
                bool isSuccess = OriginAllEvent.Invoke();
                short command = PlcControlManager.Instance().WritePcStatus(PlcCommand.Origin_All, isSuccess);
                Logger.Write(LogType.Device, $"Send to PLC Origin All {PlcCommand.Origin_All.ToString()}.[{command}]");
            }
            else
            {
                short command = PlcControlManager.Instance().WritePcStatus(PlcCommand.Origin_All, true);
                Logger.Write(LogType.Device, $"OriginAllEvent is null {PlcCommand.Origin_All.ToString()}.[{command}]");
            }
        }
        #endregion
    }
}
