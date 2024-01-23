using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Service.Plc;
using Jastech.Apps.Winform.Service.Plc.Maps;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Algorithms.Akkon.Results;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Device.Plcs;
using Jastech.Framework.Device.Plcs.Melsec;
using Jastech.Framework.Device.Plcs.Melsec.Parsers;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform
{
    public partial class PlcControlManager
    {
        #region 필드
        private bool _pcHeartBit { get; set; } = false;

        private static PlcControlManager _instance = null;

        private int _loopCount { get; set; } = 0;
        #endregion

        #region 속성
        public MachineStatus MachineStatus { get; set; } = MachineStatus.STOP;

        public bool IsDoorOpened { get; set; } = false;

        private ParserType ParserType { get; set; } = ParserType.Binary;

        private PlcAddressService PlcAddressService { get; set; } = new PlcAddressService();

        public Task PlcActionTask { get; set; }

        public CancellationTokenSource CancelPlcActionTask { get; set; }

        public bool EnableSendPeriodically { get; set; } = true;
        #endregion

        #region 이벤트
        public event PlcAxisNegativeLimitEventHandler AxisNegativeLimitEventHandler;

        public event PlcAxisPositiveLimitEventHandler AxisPositiveLimitEventHandler;
        #endregion

        #region 델리게이트
        public delegate bool PlcAxisNegativeLimitEventHandler(AxisName axisName);

        public delegate bool PlcAxisPositiveLimitEventHandler(AxisName axisName);
        #endregion

        #region 메서드
        public static PlcControlManager Instance()
        {
            if (_instance == null)
                _instance = new PlcControlManager();

            return _instance;
        }

        public void Initialize()
        {
            PlcAddressService.CreateMap();
            PlcAddressService.Initialize();

            if (DeviceManager.Instance().PlcHandler.Count > 0)
            {
                var plc = DeviceManager.Instance().PlcHandler.First();

                if (plc is MelsecPlc melsecPlc)
                    ParserType = melsecPlc.MelsecParser.ParserType;

                plc.PlcReceived += Plc_PlcReceived;
                StartReadTask();
            }
        }

        private void Plc_PlcReceived(byte[] data)
        {
            if (data == null)
                return;

            if (data.Length > 0)
            {
                lock (PlcAddressService.AddressMapList)
                {
                    PlcAddressService.OrgData = data;

                    int minAddressNum = PlcAddressService.MinAddressNumber;

                    foreach (var map in PlcAddressService.AddressMapList)
                    {
                        byte[] buffer = SplitData(data, map.AddressNum, map.WordSize, minAddressNum);

                        if (ParserType == ParserType.Binary)
                            map.Value = ConvertBinary(buffer, map.WordType);
                        else
                            map.Value = ConvertToAscii(buffer, map.WordType);
                    }
                }

                if (GetAddressMap(PlcCommonMap.PC_Status_Common).Value == "0")
                {
                    int command = Convert.ToInt32(GetAddressMap(PlcCommonMap.PLC_Command_Common).Value);
                    PlcScenarioManager.Instance().AddCommonCommand(command);
                }

                if (GetAddressMap(PlcCommonMap.PC_Status).Value == "0")
                {
                    var commandMap = GetAddressMap(PlcCommonMap.PLC_Command);
                    int command = Convert.ToInt32(commandMap.Value);
                    PlcScenarioManager.Instance().AddCommand(command);
                }
            }
        }

        private void OnPlcCommandReceived(int command)
        {
            PlcCommand plcCommand = (PlcCommand)command;
        }

        public void Release()
        {
            if (DeviceManager.Instance().PlcHandler.Count > 0)
            {
                var plc = DeviceManager.Instance().PlcHandler.First();
                plc.PlcReceived -= Plc_PlcReceived;
                StopReadTask();
            }
        }

        public void StartReadTask()
        {
            if (PlcActionTask != null)
                return;

            CancelPlcActionTask = new CancellationTokenSource();
            PlcActionTask = new Task(PlcAction, CancelPlcActionTask.Token);
            PlcActionTask.Start();
        }

        public void StopReadTask()
        {
            if (PlcActionTask == null)
                return;

            CancelPlcActionTask.Cancel();
            //PlcReadTask.Wait();
            PlcActionTask = null;
        }

        private void PlcAction()
        {
            while (true)
            {
                if (CancelPlcActionTask.IsCancellationRequested)
                    break;

                if(EnableSendPeriodically)
                {
                    if (DeviceManager.Instance().PlcHandler.Count > 0)
                    {
                        if (_loopCount % 2 == 0)
                            ReadCommand();
                        else if (_loopCount % 3 == 0)
                            WritePcStatusPeriodically(UnitName.Unit0);
                    }
                }

                if (_loopCount >= int.MaxValue)
                    _loopCount = 0;
                else
                    _loopCount++;

                Thread.Sleep(50);
            }
        }

        public string GetValue(PlcCommonMap map)
        {
            string value = "";

            lock (PlcAddressService.AddressMapList)
            {
                if (PlcAddressService.AddressMapList.Count() > 0)
                    value = PlcAddressService.AddressMapList.Where(x => x.Name == map.ToString()).First().Value;
            }

            if (value == "")
                value = "0";

            return value;
        }

        public PlcAddressMap GetAddressMap(PlcCommonMap map)
        {
            lock (PlcAddressService.AddressMapList)
                return PlcAddressService.AddressMapList.Where(x => x.Name == map.ToString()).FirstOrDefault();
        }

        public PlcAddressMap GetResultMap(PlcResultMap map)
        {
            lock (PlcAddressService.AddressMapList)
                return PlcAddressService.ResultMapList.Where(x => x.Name == map.ToString()).FirstOrDefault();
        }

        private void ReadCommand()
        {
            if (DeviceManager.Instance().PlcHandler.Count > 0)
            {
                var plc = DeviceManager.Instance().PlcHandler.First();
                plc.Read("D" + PlcAddressService.MinAddressNumber, PlcAddressService.AddressLength);
            }
        }

        public void ClearAddress(PlcCommonMap commonMap, int length = 1)
        {
            var map = PlcControlManager.Instance().GetAddressMap(commonMap);
            if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;

                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                {
                    for (int i = 0; i < length; i++)
                        stream.AddSwap16BitData(0);
                }
                else
                {
                    for (int i = 0; i < length; i++)
                        stream.Add16BitData(0);
                }

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        public void ClearPlcCommonCommand()
        {
            var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PLC_Command_Common);

            if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;

                PlcDataStream stream = new PlcDataStream();

                short value = Convert.ToInt16(0);

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                    stream.AddSwap16BitData(value);
                else
                    stream.Add16BitData(value);

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        private void WritePcStatusPeriodically(UnitName unitName)
        {
            if (DeviceManager.Instance().PlcHandler.Count > 0)
            {
                _pcHeartBit = !_pcHeartBit;
          
                bool isMoving = MotionManager.Instance().IsMoving(AxisHandlerName.Handler0, AxisName.X);
                int currentPosX = GetPlcPosXData(unitName);
                bool isServoOn = MotionManager.Instance().IsEnable(AxisHandlerName.Handler0, AxisName.X);

                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;
                var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_Alive);
                int machineStatus = PlcControlManager.Instance().MachineStatus == MachineStatus.RUN ? 9000 : 0;

                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                {
                    stream.AddSwap16BitData(Convert.ToInt16(_pcHeartBit));
                    stream.AddSwap16BitData(Convert.ToInt16(isMoving));
                    stream.AddSwap16BitData(Convert.ToInt16(currentPosX));
                    stream.AddSwap16BitData(Convert.ToInt16(isServoOn));
                    stream.AddSwap16BitData(Convert.ToInt16(machineStatus));
                }
                else
                {
                    stream.Add16BitData(Convert.ToInt16(_pcHeartBit));
                    stream.Add16BitData(Convert.ToInt16(isMoving));
                    stream.Add16BitData(Convert.ToInt16(currentPosX));
                    stream.Add16BitData(Convert.ToInt16(isServoOn));
                    stream.Add16BitData(Convert.ToInt16(machineStatus));
                }

                plc.Write("D" + map.AddressNum, stream.Data);
            }

            WriteAxisLimitStatus();
        }

        private void WriteAxisLimitStatus()
        {
            if (DeviceManager.Instance().PlcHandler.Count > 0)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;
                var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_X_NegativeLimit);

                if (AxisNegativeLimitEventHandler == null || AxisPositiveLimitEventHandler == null)
                    return;

                bool isNegativeLimitX = AxisNegativeLimitEventHandler.Invoke(AxisName.X);
                bool isPositiveLimitX = AxisPositiveLimitEventHandler.Invoke(AxisName.X);

                bool isNegativeLimitZ0 = AxisNegativeLimitEventHandler.Invoke(AxisName.Z0);
                bool isPositiveLimitZ0 = AxisPositiveLimitEventHandler.Invoke(AxisName.Z0);

                bool isNegativeLimitZ1 = AxisNegativeLimitEventHandler.Invoke(AxisName.Z1);
                bool isPositiveLimitZ1 = AxisPositiveLimitEventHandler.Invoke(AxisName.Z1);

                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                {
                    stream.AddSwap16BitData(Convert.ToInt16(isNegativeLimitX));
                    stream.AddSwap16BitData(Convert.ToInt16(isPositiveLimitX));
                    stream.AddSwap16BitData(Convert.ToInt16(0));
                    stream.AddSwap16BitData(Convert.ToInt16(0));
                    stream.AddSwap16BitData(Convert.ToInt16(isNegativeLimitZ0));
                    stream.AddSwap16BitData(Convert.ToInt16(isPositiveLimitZ0));
                    stream.AddSwap16BitData(Convert.ToInt16(isNegativeLimitZ1));
                    stream.AddSwap16BitData(Convert.ToInt16(isPositiveLimitZ1));
                }
                else
                {
                    stream.Add16BitData(Convert.ToInt16(isNegativeLimitX));
                    stream.Add16BitData(Convert.ToInt16(isPositiveLimitX));
                    stream.Add16BitData(Convert.ToInt16(0));
                    stream.Add16BitData(Convert.ToInt16(0));
                    stream.Add16BitData(Convert.ToInt16(isNegativeLimitZ0));
                    stream.Add16BitData(Convert.ToInt16(isPositiveLimitZ0));
                    stream.Add16BitData(Convert.ToInt16(isNegativeLimitZ1));
                    stream.Add16BitData(Convert.ToInt16(isPositiveLimitZ1));
                }

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        public void WritePcVisionStatus(MachineStatus status)
        {
            int value = status == MachineStatus.RUN ? 9000 : 0;
            PlcDataStream stream = new PlcDataStream();
            var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_Ready);

            if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;

                if (plc?.MelsecParser.ParserType == ParserType.Binary)
                    stream.AddSwap16BitData(Convert.ToInt16(value));
                else
                    stream.Add16BitData(Convert.ToInt16(value));

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        public void WriteGrabDone()
        {
            if (DeviceManager.Instance().PlcHandler.Count > 0)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;
                var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_GrabDone);

                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                    stream.AddSwap16BitData(Convert.ToInt16(true));
                else
                    stream.Add16BitData(Convert.ToInt16(true));

                plc.Write("D" + map.AddressNum, stream.Data);
                Logger.Write(LogType.Comm, $"Wrote PC_GrabDone to D{map.AddressNum}");
            }
        }

        private int GetPlcPosXData(UnitName unitName)
        {
            if (ModelManager.Instance().CurrentModel is AppsInspModel inspModel)
            {
                var manager = MotionManager.Instance();
                var axis = manager.GetAxis(AxisHandlerName.Handler0, AxisName.X);

                foreach (TeachingPosType posType in Enum.GetValues(typeof(TeachingPosType)))
                {
                    if (inspModel.GetUnit(unitName) is Unit unit)
                    {
                        var teachingInfo = unit.GetTeachingInfo(posType);
                        if (teachingInfo != null)
                        {
                            bool inPosition = manager.IsAxisInPosition(unitName, posType, axis);

                            if (inPosition)
                                return (int)ConvertToPlcCommand(posType);
                        }
                    }
                }
            }
            return 0;
        }

        private PlcCommand ConvertToPlcCommand(TeachingPosType posType)
        {
            if (posType == TeachingPosType.Standby)
                return PlcCommand.Move_StandbyPos;
            else if (posType == TeachingPosType.Stage1_PreAlign_Left)
                return PlcCommand.Move_Left_AlignPos;
            else if (posType == TeachingPosType.Stage1_PreAlign_Right)
                return PlcCommand.Move_Right_AlignPos;
            else if (posType == TeachingPosType.Stage1_Scan_Start)
                return PlcCommand.Move_ScanStartPos;
            else if (posType == TeachingPosType.Stage1_Scan_End)
                return PlcCommand.Move_ScanEndPos;
            else
                return PlcCommand.None;
        }

        public void WritePcReady(MachineStatus machineStatus)
        {
            int value = machineStatus == MachineStatus.RUN ? 9000 : 0;
            var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_Ready);

            if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;

                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                    stream.AddSwap16BitData(Convert.ToInt16(value));
                else
                    stream.Add16BitData(Convert.ToInt16(value));

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        public short WritePcStatusCommon(PlcCommonCommand status, bool isFailed = false)
        {
            var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_Status_Common);

            if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;

                PlcDataStream stream = new PlcDataStream();

                short value = Convert.ToInt16(status);
                if (isFailed)
                    value *= -1;

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                    stream.AddSwap16BitData(value);
                else
                    stream.Add16BitData(value);

                plc.Write("D" + map.AddressNum, stream.Data);

                return value;
            }
            return 0;
        }

        public void WritePcErrorCode()
        {
            var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_ErrorCode);

            if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;
                int errorCode = 0; // error 기준 성립 후 함수화 진행해야함

                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                    stream.AddSwap16BitData(Convert.ToInt16(errorCode));
                else
                    stream.Add16BitData(Convert.ToInt16(errorCode));

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        public void WritePcCommand(PcCommand command)
        {
            var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_Command);

            if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;

                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                    stream.AddSwap16BitData(Convert.ToInt16(command));
                else
                    stream.Add16BitData(Convert.ToInt16(command));

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        public short WritePcStatus(PlcCommand command, bool isFailed = false)
        {
            var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_Status);
            short value = -1;

            if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;

                PlcDataStream stream = new PlcDataStream();

                value = Convert.ToInt16(command);
                if (isFailed)
                    value *= -1;

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                    stream.AddSwap16BitData(value);
                else
                    stream.Add16BitData(value);

                plc.Write("D" + map.AddressNum, stream.Data);
            }

            return value;
        }

        public void WriteMoveRequest(bool isOff = false)
        {
            var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_Move_REQ);
            if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;

                int value;

                if (isOff)
                    value = 0;
                else
                    value = 5;

                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                    stream.AddSwap16BitData(Convert.ToInt16(value));
                else
                    stream.Add16BitData(Convert.ToInt16(value));

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        public void ClearAlignData()
        {
            var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_AlignDataX);

            if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;
                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                {
                    stream.AddSwap16BitData(0);
                    stream.AddSwap16BitData(0);
                    stream.AddSwap16BitData(0);
                    stream.AddSwap16BitData(0);
                    stream.AddSwap16BitData(0);
                    stream.AddSwap16BitData(0);
                }
                else
                {
                    stream.Add16BitData(0);
                    stream.Add16BitData(0);
                    stream.Add16BitData(0);
                    stream.Add16BitData(0);
                    stream.Add16BitData(0);
                    stream.Add16BitData(0);
                }

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        public void WriteAlignData(double alignDataX_mm, double alignDataY_mm, double alignDataT_mm)
        {
            int convertAlignX = ConvertDoubleWordData(alignDataX_mm);
            int convertAlignY = ConvertDoubleWordData(alignDataY_mm);
            int convertAlignT = ConvertDoubleWordData(alignDataT_mm);

            var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_AlignDataX);

            if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;
                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                {
                    stream.AddSwap32BitData(convertAlignX);
                    stream.AddSwap32BitData(convertAlignY);
                    stream.AddSwap32BitData(convertAlignT);
                }
                else
                {
                    stream.Add32BitData(convertAlignX);
                    stream.Add32BitData(convertAlignY);
                    stream.Add32BitData(convertAlignT);
                }

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        public void WriteVersion()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            var map = PlcControlManager.Instance().GetResultMap(PlcResultMap.Version_Major);

            if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;
                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                {
                    stream.AddSwap16BitData(Convert.ToInt16(version.Major));
                    stream.AddSwap16BitData(Convert.ToInt16(version.Minor));
                    stream.AddSwap16BitData(Convert.ToInt16(version.Build));
                    stream.AddSwap16BitData(Convert.ToInt16(version.Revision));
                }
                else
                {
                    stream.Add16BitData(Convert.ToInt16(version.Major));
                    stream.Add16BitData(Convert.ToInt16(version.Minor));
                    stream.Add16BitData(Convert.ToInt16(version.Build));
                    stream.Add16BitData(Convert.ToInt16(version.Revision));
                }

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        public void WriteCurrentModelName(string modelName)
        {
            modelName = modelName.PadRight(40, Convert.ToChar("\0"));
            byte[] modelNameByte = Encoding.Default.GetBytes(modelName);
            var map = PlcControlManager.Instance().GetResultMap(PlcResultMap.Current_ModelName);

            if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;
                PlcDataStream stream = new PlcDataStream();
                if (plc.MelsecParser.ParserType == ParserType.Binary)
                {
                    for (int i = 0; i < map.WordSize; i++)
                    {
                        int num = i * 2;
                        int value = BitConverter.ToUInt16(modelNameByte, num);
                        stream.AddSwap16BitData(Convert.ToInt16(value));
                    }
                }
                else
                {
                    for (int i = 0; i < map.WordSize; i++)
                    {
                        int num = i * 2;
                        int value = BitConverter.ToUInt16(modelNameByte, num);
                        stream.Add16BitData(Convert.ToInt16(value));
                    }
                }

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        public void WriteTabResult(TabInspResult tabInspResult, double resolution)
        {
            string tabJudgementName = string.Format("Tab{0}_Judgement", tabInspResult.TabNo);
            PlcResultMap plcResultMap = (PlcResultMap)Enum.Parse(typeof(PlcResultMap), tabJudgementName);

            var map = PlcControlManager.Instance().GetResultMap(plcResultMap);

            if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;
                PlcDataStream stream = new PlcDataStream();

                int tabJudgement = 0;
                if (tabInspResult.IsManualOK)
                    tabJudgement = (int)TabJudgement.Manual_OK;
                else
                    tabJudgement = (int)tabInspResult.Judgement;

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                    stream.AddSwap16BitData(Convert.ToInt16(tabJudgement));
                else
                    stream.Add16BitData(Convert.ToInt16(tabJudgement));

                AddAlignResult(plc.MelsecParser.ParserType, tabInspResult.AlignResult, resolution, ref stream);
                AddAkkonResult(plc.MelsecParser.ParserType, tabInspResult.AkkonResult, ref stream);
                AddMarkResult(plc.MelsecParser.ParserType, tabInspResult.MarkResult, ref stream);

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        public void WriteTabResult(int tabNo, TabJudgement judgement, TabAlignResult alignResult, AkkonResult akkonResult, TabMarkResult markResult, double resolution)
        {
            WriteFirstResultStream(tabNo, judgement, alignResult, akkonResult, markResult, resolution);

            Thread.Sleep(10);

            WriteSecondResultStream(tabNo, judgement, alignResult, akkonResult, markResult, resolution);
        }

        private void WriteFirstResultStream(int tabNo, TabJudgement judgement, TabAlignResult alignResult, AkkonResult akkonResult, TabMarkResult markResult, double resolution)
        {
            string tabJudgementName = string.Format("Tab{0}_Judgement", tabNo);
            PlcResultMap plcResultMap = (PlcResultMap)Enum.Parse(typeof(PlcResultMap), tabJudgementName);

            var map = PlcControlManager.Instance().GetResultMap(plcResultMap);
            if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;
                PlcDataStream stream = new PlcDataStream();

                int tabJudgement = (int)judgement;

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                    stream.AddSwap16BitData(Convert.ToInt16(tabJudgement));
                else
                    stream.Add16BitData(Convert.ToInt16(tabJudgement));

                AddAlignResult(plc.MelsecParser.ParserType, alignResult, resolution, ref stream);
                AddAkkonResult(plc.MelsecParser.ParserType, akkonResult, ref stream);
                AddMarkResult(plc.MelsecParser.ParserType, markResult, ref stream);

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        private void WriteSecondResultStream(int tabNo, TabJudgement judgement, TabAlignResult alignResult, AkkonResult akkonResult, TabMarkResult markResult, double resolution)
        {
            string tabCxName = string.Format("Tab{0}_Align_Cx", tabNo);
            PlcResultMap plcCxResultMap = (PlcResultMap)Enum.Parse(typeof(PlcResultMap), tabCxName);

            var map = PlcControlManager.Instance().GetResultMap(plcCxResultMap);
            if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;
                PlcDataStream secondStream = new PlcDataStream();

                AddSecondAlignResult(plc.MelsecParser.ParserType, alignResult, resolution, ref secondStream);

                plc.Write("D" + map.AddressNum, secondStream.Data);
            }
        }

        private void AddAlignResult(ParserType parserType, TabAlignResult alignResult, double resolution, ref PlcDataStream stream)
        {
            if (alignResult == null)
            {
                if (parserType == ParserType.Binary)
                {
                    stream.AddSwap16BitData(0); // Align OK/NG
                    stream.AddSwap32BitData(0); // Left AlignX
                    stream.AddSwap32BitData(0); // Left AlignY
                    stream.AddSwap32BitData(0); // Right AlignX
                    stream.AddSwap32BitData(0); // Right AlignY
                }
                else
                {
                    stream.Add16BitData(0); // Align OK/NG
                    stream.Add32BitData(0); // Left AlignX
                    stream.Add32BitData(0); // Left AlignY
                    stream.Add32BitData(0); // Right AlignX
                    stream.Add32BitData(0); // Right AlignY
                }
            }
            else
            {
                int alignJudgement = alignResult.Judgement == Judgement.OK ? 1 : 2;

                if (AppsConfig.Instance().EnableAlignByPass)
                    alignJudgement = 1;

                double leftX_um = alignResult.GetDoubleLx_um();
                double leftY_um = alignResult.GetDoubleLy_um();
                double rightX_um = alignResult.GetDoubleRx_um();
                double rightY_um = alignResult.GetDoubleRy_um();

                int leftX = ConvertDoubleWordData(leftX_um);
                int leftY = ConvertDoubleWordData(leftY_um);
                int rightX = ConvertDoubleWordData(rightX_um);
                int rightY = ConvertDoubleWordData(rightY_um);

                if (parserType == ParserType.Binary)
                {
                    stream.AddSwap16BitData(Convert.ToInt16(alignJudgement));   // Align OK/NG
                    stream.AddSwap32BitData(leftX);                          // Left AlignX
                    stream.AddSwap32BitData(leftY);                          // Left AlignY
                    stream.AddSwap32BitData(rightX);                         // Right AlignX
                    stream.AddSwap32BitData(rightY);                         // Right AlignY
                }
                else
                {
                    stream.Add16BitData(Convert.ToInt16(alignJudgement));   // Align OK/NG
                    stream.Add32BitData(leftX);                          // Left AlignX
                    stream.Add32BitData(leftY);                          // Left AlignY
                    stream.Add32BitData(rightX);                         // Right AlignX
                    stream.Add32BitData(rightY);                         // Right AlignY
                }
            }
        }

        private void AddSecondAlignResult(ParserType parserType, TabAlignResult alignResult, double resolution, ref PlcDataStream stream)
        {
            if (alignResult == null)
            {
                if (parserType == ParserType.Binary)
                    stream.AddSwap16BitData(0); // CX
                else
                    stream.Add16BitData(0); // Cx
            }
            else
            {
                double cx_um = alignResult.GetDoubleCx_um();
                int cx = ConvertDoubleWordData(cx_um);

                if (parserType == ParserType.Binary)
                    stream.AddSwap32BitData(cx);   // Align Cx
                else
                    stream.Add32BitData(cx);       // Align Cx
            }
        }

        private void AddAkkonResult(ParserType parserType, AkkonResult akkonResult, ref PlcDataStream stream)
        {
            if (akkonResult == null)
            {
                if (parserType == ParserType.Binary)
                {
                    stream.AddSwap16BitData(0); // Akkon OK/NG

                    stream.AddSwap16BitData(0); // Empty

                    stream.AddSwap16BitData(0); // Left Avg Count
                    stream.AddSwap16BitData(0); // Left Min Count
                    stream.AddSwap16BitData(0); // Left Max Count

                    stream.AddSwap16BitData(0); // Right Avg Count
                    stream.AddSwap16BitData(0); // Right Min Count
                    stream.AddSwap16BitData(0); // Right Max Count

                    stream.AddSwap32BitData(0); // Left Avg Length
                    stream.AddSwap32BitData(0); // Left Min Length
                    stream.AddSwap32BitData(0); // Left Max Length
                    stream.AddSwap32BitData(0); // Right Avg Length
                    stream.AddSwap32BitData(0); // Right Min Length
                    stream.AddSwap32BitData(0); // Right Max Length
                }
                else
                {
                    stream.Add16BitData(0); // Akkon OK/NG

                    stream.Add16BitData(0); // Empty

                    stream.Add16BitData(0); // Left Avg Count
                    stream.Add16BitData(0); // Left Min Count
                    stream.Add16BitData(0); // Left Max Count

                    stream.Add16BitData(0); // Right Avg Count
                    stream.Add16BitData(0); // Right Min Count
                    stream.Add16BitData(0); // Right Max Count

                    stream.Add32BitData(0); // Left Avg Length
                    stream.Add32BitData(0); // Left Min Length
                    stream.Add32BitData(0); // Left Max Length
                    stream.Add32BitData(0); // Right Avg Length
                    stream.Add32BitData(0); // Right Min Length
                    stream.Add32BitData(0); // Right Max Length
                }
            }
            else
            {
                int akkonJudgement = akkonResult.Judgement == Judgement.OK ? 1 : 2;

                if (AppsConfig.Instance().EnableAkkonByPass)
                    akkonJudgement = 1;

                int leftLengthAvg = ConvertDoubleWordData(akkonResult.Length_Left_Avg_um);
                int leftLengthMin = ConvertDoubleWordData(akkonResult.Length_Left_Min_um);
                int leftLengthMax = ConvertDoubleWordData(akkonResult.Length_Left_Max_um);
                int rightLengthAvg = ConvertDoubleWordData(akkonResult.Length_Right_Avg_um);
                int rightLengthMin = ConvertDoubleWordData(akkonResult.Length_Right_Min_um);
                int rightLengthMax = ConvertDoubleWordData(akkonResult.Length_Right_Max_um);

                if (parserType == ParserType.Binary)
                {
                    stream.AddSwap16BitData(Convert.ToInt16(akkonJudgement)); // Akkon OK/NG

                    stream.AddSwap16BitData(0); // Empty

                    stream.AddSwap16BitData(Convert.ToInt16(akkonResult.LeftCount_Avg)); // Left Avg Count
                    stream.AddSwap16BitData(Convert.ToInt16(akkonResult.LeftCount_Min)); // Left Min Count
                    stream.AddSwap16BitData(Convert.ToInt16(akkonResult.LeftCount_Max)); // Left Max Count

                    stream.AddSwap16BitData(Convert.ToInt16(akkonResult.RightCount_Avg)); // Right Avg Count
                    stream.AddSwap16BitData(Convert.ToInt16(akkonResult.RightCount_Min)); // Right Min Count
                    stream.AddSwap16BitData(Convert.ToInt16(akkonResult.RightCount_Max)); // Right Max Count

                    stream.AddSwap32BitData(leftLengthAvg); // Left Avg Length
                    stream.AddSwap32BitData(leftLengthMin); // Left Min Length
                    stream.AddSwap32BitData(leftLengthMax); // Left Max Length
                    stream.AddSwap32BitData(rightLengthAvg); // Right Avg Length
                    stream.AddSwap32BitData(rightLengthMin); // Right Min Length
                    stream.AddSwap32BitData(rightLengthMax); // Right Max Length
                }
                else
                {
                    stream.Add16BitData(Convert.ToInt16(akkonJudgement)); // Akkon OK/NG

                    stream.Add16BitData(0); // Empty

                    stream.Add16BitData(Convert.ToInt16(akkonResult.LeftCount_Avg)); // Left Avg Count
                    stream.Add16BitData(Convert.ToInt16(akkonResult.LeftCount_Min)); // Left Min Count
                    stream.Add16BitData(Convert.ToInt16(akkonResult.LeftCount_Max)); // Left Max Count

                    stream.Add16BitData(Convert.ToInt16(akkonResult.RightCount_Avg)); // Right Avg Count
                    stream.Add16BitData(Convert.ToInt16(akkonResult.RightCount_Min)); // Right Min Count
                    stream.Add16BitData(Convert.ToInt16(akkonResult.RightCount_Max)); // Right Max Count

                    stream.Add32BitData(leftLengthAvg); // Left Avg Length
                    stream.Add32BitData(leftLengthMin); // Left Min Length
                    stream.Add32BitData(leftLengthMax); // Left Max Length
                    stream.Add32BitData(rightLengthAvg); // Right Avg Length
                    stream.Add32BitData(rightLengthMin); // Right Min Length
                    stream.Add32BitData(rightLengthMax); // Right Max Length
                }
            }
        }

        private void AddMarkResult(ParserType parserType, TabMarkResult markResult, ref PlcDataStream stream)
        {
            if (markResult == null)
            {
                if (parserType == ParserType.Binary)
                {
                    stream.AddSwap32BitData(0); // Panel Left Mark Sccore
                    stream.AddSwap32BitData(0); // Panel Right Mark Score
                    stream.AddSwap32BitData(0); // COF Left Mark Score
                    stream.AddSwap32BitData(0); // COF Right Mark Score
                }
                else
                {
                    stream.Add32BitData(0); // Panel Left Mark Sccore
                    stream.Add32BitData(0); // Panel Right Mark Score
                    stream.Add32BitData(0); // COF Left Mark Score
                    stream.Add32BitData(0); // COF Right Mark Score
                }
            }
            else
            {
                var panel = markResult.PanelMark;
                int panelLeft = 0;
                int panelRight = 0;

                if (panel.FoundedMark != null)
                {
                    var left = panel.FoundedMark.Left;
                    if (left != null)
                    {
                        if (left.MatchPosList.Count > 0)
                            panelLeft = ConvertDoubleWordData(left.MaxScore);
                    }

                    var right = panel.FoundedMark.Right;
                    if (right != null)
                    {
                        if (right.MatchPosList.Count > 0)
                            panelRight = ConvertDoubleWordData(right.MaxScore);
                    }
                }

                var fpc = markResult.FpcMark;
                int cofLeft = 0;
                int cofRight = 0;

                if (fpc.FoundedMark != null)
                {
                    var left = fpc.FoundedMark.Left;
                    if (left != null)
                    {
                        if (left.MatchPosList.Count > 0)
                            cofLeft = ConvertDoubleWordData(left.MaxScore);
                    }

                    var right = fpc.FoundedMark.Right;
                    if (right != null)
                    {
                        if (right.MatchPosList.Count > 0)
                            cofRight = ConvertDoubleWordData(right.MaxScore);
                    }
                }

                if (parserType == ParserType.Binary)
                {
                    stream.AddSwap32BitData(panelLeft);     // Panel Left Mark Sccore
                    stream.AddSwap32BitData(panelRight);    // Panel Right Mark Score 
                    stream.AddSwap32BitData(cofLeft);       // COF Left Mark Score
                    stream.AddSwap32BitData(cofRight);      // COF Right Mark Score
                }
                else
                {
                    stream.Add32BitData(panelLeft);         // Panel Left Mark Score
                    stream.Add32BitData(panelRight);        // Panel Right Mark Score
                    stream.Add32BitData(cofLeft);           // COF Left Mark Score
                    stream.Add32BitData(cofRight);          // COF Right Mark Score
                }
            }
        }

        public void WriteTabAlignResult(int tabNo, int judgement, double leftAlignX_mm, double leftAlignY_mm, double rightAlignX_mm, double rightAlignY_mm)
        {
            string alignJudegmentName = string.Format("Tab{0}_Align_Judgement", tabNo);
            PlcResultMap plcResultMap = (PlcResultMap)Enum.Parse(typeof(PlcResultMap), alignJudegmentName);
            var map = PlcControlManager.Instance().GetResultMap(plcResultMap);

            if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;
                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                {
                    stream.AddSwap16BitData(Convert.ToInt16(judgement));
                    stream.AddSwap16BitData(Convert.ToInt16((int)leftAlignX_mm));
                    stream.AddSwap16BitData(Convert.ToInt16((int)leftAlignY_mm));
                    stream.AddSwap16BitData(Convert.ToInt16((int)rightAlignX_mm));
                    stream.AddSwap16BitData(Convert.ToInt16((int)rightAlignY_mm));
                }
                else
                {
                    stream.Add16BitData(Convert.ToInt16(judgement));
                    stream.Add16BitData(Convert.ToInt16((int)leftAlignX_mm));
                    stream.Add16BitData(Convert.ToInt16((int)leftAlignY_mm));
                    stream.Add16BitData(Convert.ToInt16((int)rightAlignX_mm));
                    stream.Add16BitData(Convert.ToInt16((int)rightAlignY_mm));
                }

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        public double GetReadPosition(AxisName axisName)
        {
            double position = 0.0;

            lock (PlcAddressService.AddressMapList)
            {
                if (axisName == AxisName.Y)
                {
                    var value = ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Position_AxisY);
                    position = Convert.ToDouble(value);
                }
                else if (axisName == AxisName.T)
                {
                    var value = ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Position_AxisT);
                    position = Convert.ToDouble(value);
                }
            }

            return position;
        }

        public void WriteManualJudge(bool manualFlag)
        {
            var map = GetAddressMap(PlcCommonMap.PLC_ManualMatch);
            if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;

                PlcDataStream stream = new PlcDataStream();

                if(manualFlag == true)
                {
                    if (plc.MelsecParser.ParserType == ParserType.Binary)
                        stream.AddSwap16BitData(5);
                    else
                        stream.Add16BitData(5);
                }
                else
                {
                    if (plc.MelsecParser.ParserType == ParserType.Binary)
                        stream.AddSwap16BitData(0);
                    else
                        stream.Add16BitData(0);
                }

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        public void WriteModelData(AppsInspModel inspModel)
        {
            if (inspModel == null)
                return;

            int tabCount = inspModel.TabCount;

            for (int tabNo = 0; tabNo < tabCount; tabNo++)
            {
                var tab = inspModel.GetUnit(UnitName.Unit0).GetTab(tabNo);
                WriteModelParameter(tab, tabNo);
                Thread.Sleep(50);
            }

            WritePcCommand(PcCommand.Set_InspParameter);
        }

        private void WriteModelParameter(Tab tab, int tabNo)
        {
            string leftFpcX = string.Format("Tab{0}_Left_FPC_X_Threshold", tabNo);
            PlcResultMap plcResultMap = (PlcResultMap)Enum.Parse(typeof(PlcResultMap), leftFpcX);
            var map = PlcControlManager.Instance().GetResultMap(plcResultMap);

            if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
            {
                int leftFpcX_param = (int)tab.GetAlignParam(ATTTabAlignName.LeftFPCX).CaliperParams.GetContrastThreshold();
                int leftFpcY_param = (int)tab.GetAlignParam(ATTTabAlignName.LeftFPCY).CaliperParams.GetContrastThreshold();
                int leftPanelX_param = (int)tab.GetAlignParam(ATTTabAlignName.LeftPanelX).CaliperParams.GetContrastThreshold();
                int leftPanelY_param = (int)tab.GetAlignParam(ATTTabAlignName.LeftPanelY).CaliperParams.GetContrastThreshold();

                int rightFpcX_param = (int)tab.GetAlignParam(ATTTabAlignName.RightFPCX).CaliperParams.GetContrastThreshold();
                int rightFpcY_param = (int)tab.GetAlignParam(ATTTabAlignName.RightFPCY).CaliperParams.GetContrastThreshold();
                int rightPanelX_param = (int)tab.GetAlignParam(ATTTabAlignName.RightPanelX).CaliperParams.GetContrastThreshold();
                int rightPanelY_param = (int)tab.GetAlignParam(ATTTabAlignName.RightPanelY).CaliperParams.GetContrastThreshold();

                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;
                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                {
                    stream.AddSwap16BitData(Convert.ToInt16(leftFpcX_param));
                    stream.AddSwap16BitData(Convert.ToInt16(leftFpcY_param));
                    stream.AddSwap16BitData(Convert.ToInt16(leftPanelX_param));
                    stream.AddSwap16BitData(Convert.ToInt16(leftPanelY_param));
                    
                    stream.AddSwap16BitData(Convert.ToInt16(rightFpcX_param));
                    stream.AddSwap16BitData(Convert.ToInt16(rightFpcY_param));
                    stream.AddSwap16BitData(Convert.ToInt16(rightPanelX_param));
                    stream.AddSwap16BitData(Convert.ToInt16(rightPanelY_param));
                }
                else
                {
                    stream.Add16BitData(Convert.ToInt16(leftFpcX_param));
                    stream.Add16BitData(Convert.ToInt16(leftFpcY_param));
                    stream.Add16BitData(Convert.ToInt16(leftPanelX_param));
                    stream.Add16BitData(Convert.ToInt16(leftPanelY_param));

                    stream.Add16BitData(Convert.ToInt16(rightFpcX_param));
                    stream.Add16BitData(Convert.ToInt16(rightFpcY_param));
                    stream.Add16BitData(Convert.ToInt16(rightPanelX_param));
                    stream.Add16BitData(Convert.ToInt16(rightPanelY_param));
                }

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }
        #endregion
    }

    public partial class PlcControlManager
    {
        #region 메서드
        public int ConvertDoubleWordData(double value_mm)
        {
            double noneDotValue = MathHelper.GetFloorDecimal(value_mm, 4) * 10000;

            int returnValue = Convert.ToInt32(noneDotValue);

            return returnValue;
        }

        public void SplitDouble(double value, out int high, out int low)
        {
            high = 0;
            low = 0;
            string valueString = value.ToString("0.0000");
            int index = valueString.IndexOf('.');

            if (index > 0)
            {
                string highString = valueString.Substring(0, index);
                string lowString = valueString.Substring(index + 1, valueString.Length - index - 1);
                high = Convert.ToInt32(highString);
                low = Convert.ToInt32(lowString);
            }
            else
            {
                high = Convert.ToInt32(valueString);
                low = 0;
            }
        }

        private byte[] SplitData(byte[] dataBuffer, int addressNumber, int wordSize, int minAddressNumber)
        {
            byte[] resultByte = null;

            try
            {
                int startAddress = (addressNumber - minAddressNumber) * 2;
                int addresslength = ParserType == ParserType.Ascii ? wordSize * 4 : wordSize * 2;
                resultByte = new byte[addresslength];

                Array.Copy(dataBuffer, startAddress, resultByte, 0, addresslength);
            }
            catch (Exception)
            {
                return resultByte;
            }

            return resultByte;
        }

        private string ConvertBinary(byte[] hexBuffer, WordType wordType)
        {
            string resultStr = "";
            byte[] dataArray;
            int value;

            switch (wordType)
            {
                case WordType.DoubleWord:
                    dataArray = new byte[4];
                    Array.Copy(hexBuffer, 0, dataArray, 0, 2); // low
                    Array.Copy(hexBuffer, 2, dataArray, 2, 2); // high

                    value = BitConverter.ToInt32(dataArray, 0);
                    resultStr = value.ToString();
                    break;

                case WordType.DEC:
                    for (int i = 0; i < hexBuffer.Length; i += 2)
                    {
                        dataArray = new byte[2];
                        Array.Copy(hexBuffer, i, dataArray, 0, 2);
                        value = BitConverter.ToInt16(dataArray, 0);

                        resultStr += value.ToString();
                    }
                    break;

                case WordType.HEX:

                    string strValue = Encoding.Default.GetString(hexBuffer, 0, hexBuffer.Length);
                    resultStr = strValue.Replace("\0", "");
                    break;
                default:
                    break;
            }

            return resultStr;
        }

        private string ConvertToAscii(byte[] hexBuffer, WordType wordType)
        {
            string asciiHexBuffer = BitConverter.ToString(hexBuffer).Replace("-", string.Empty);
            string resultStr = "";

            switch (wordType)
            {
                case WordType.DEC:
                    for (int i = 0; i < hexBuffer.Length; i += 4)
                    {
                        string code = asciiHexBuffer.Substring(i, 4);
                        int value = Convert.ToInt16(code, 16);

                        resultStr += value.ToString();
                    }

                    break;

                case WordType.HEX:
                    for (int i = 0; i < hexBuffer.Length; i += 4)
                    {
                        string code = asciiHexBuffer.Substring(i, 4);

                        //plc가 반대로 들어와 first, second 바꿈
                        string first = code.Substring(2, 2);
                        string second = code.Substring(0, 2);

                        byte[] resultByte = { Convert.ToByte(first, 16), Convert.ToByte(second, 16) };

                        if (resultByte[0] == 0)
                            resultByte[0] = 32;

                        if (resultByte[1] == 0)
                            resultByte[1] = 32;

                        resultStr += Encoding.ASCII.GetString(resultByte).Trim();
                    }

                    break;

                default:
                    break;
            }

            return resultStr;
        }

        public string ConvertDoubleWordStringFormat_mm(PlcCommonMap plcCommonMap)
        {
			// 영진 숙제
            try
            {
                int inputValue = Convert.ToInt32(GetValue(plcCommonMap));
                string outputValue = (inputValue / 10000.0).ToString("F4");

                return outputValue;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public double ConvertDoubleWordDoubleFormat_mm(PlcCommonMap plcCommonMap)
        {
            return Convert.ToDouble(ConvertDoubleWordStringFormat_mm(plcCommonMap));
        }

        public string GetPreHeadData(int tabNo)
        {
            PlcCommonMap plcCommonMap = PlcCommonMap.PLC_PreBond_Tab0;

            if (tabNo == 0)
                plcCommonMap = PlcCommonMap.PLC_PreBond_Tab0;
            else if (tabNo == 1)
                plcCommonMap = PlcCommonMap.PLC_PreBond_Tab1;
            else if (tabNo == 2)
                plcCommonMap = PlcCommonMap.PLC_PreBond_Tab2;
            else if (tabNo == 3)
                plcCommonMap = PlcCommonMap.PLC_PreBond_Tab3;
            else if (tabNo == 4)
                plcCommonMap = PlcCommonMap.PLC_PreBond_Tab4;
            else if (tabNo == 5)
                plcCommonMap = PlcCommonMap.PLC_PreBond_Tab5;
            else if (tabNo == 6)
                plcCommonMap = PlcCommonMap.PLC_PreBond_Tab6;
            else if (tabNo == 7)
                plcCommonMap = PlcCommonMap.PLC_PreBond_Tab7;
            else if (tabNo == 8)
                plcCommonMap = PlcCommonMap.PLC_PreBond_Tab8;
            else if (tabNo == 9)
                plcCommonMap = PlcCommonMap.PLC_PreBond_Tab9;
            else { }

            string outputValue = string.Empty;

            string sendData = GetValue(plcCommonMap);
            if (sendData == null || sendData == string.Empty || sendData == "")
                outputValue = "-";
            else if (sendData.Length > 2)
                outputValue = sendData.Substring(0, 1) + "#" + sendData.Substring(2, 1);

            return outputValue;
        }
        #endregion
    }
}
