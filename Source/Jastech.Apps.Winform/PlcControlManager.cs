using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Service.Plc;
using Jastech.Apps.Winform.Service.Plc.Maps;
using Jastech.Framework.Algorithms.Akkon.Results;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Device.Plcs;
using Jastech.Framework.Device.Plcs.Melsec;
using Jastech.Framework.Device.Plcs.Melsec.Parsers;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Winform;
using Newtonsoft.Json.Linq;
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
        private ParserType ParserType { get; set; } = ParserType.Binary;

        private PlcAddressService PlcAddressService { get; set; } = new PlcAddressService();

        public Task PlcActionTask { get; set; }

        public CancellationTokenSource CancelPlcActionTask { get; set; }
        #endregion

        #region 메서드
        public static PlcControlManager Instance()
        {
            if (_instance == null)
            {
                _instance = new PlcControlManager();
            }

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
                            map.Value = ConvertAscll(buffer, map.WordType);
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
                {
                    break;
                }

                if (DeviceManager.Instance().PlcHandler.Count > 0)
                {
                    if (_loopCount % 2 == 0)
                        ReadCommand();
                    else if (_loopCount % 3 == 0)
                        WritePcStatusPeriodically(UnitName.Unit0);
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

            return value;
        }

        public PlcAddressMap GetAddressMap(PlcCommonMap map)
        {
            lock (PlcAddressService.AddressMapList)
            {
                return PlcAddressService.AddressMapList.Where(x => x.Name == map.ToString()).FirstOrDefault();
            }
        }

        public PlcAddressMap GetResultMap(PlcResultMap map)
        {
            lock (PlcAddressService.AddressMapList)
            {
                return PlcAddressService.ResultMapList.Where(x => x.Name == map.ToString()).FirstOrDefault();
            }
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
                    {
                        stream.AddSwap16BitData(0);
                    }
                }
                else
                {
                    for (int i = 0; i < length; i++)
                    {
                        stream.Add16BitData(0);
                    }
                }

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        private void WritePcStatusPeriodically(UnitName unitName)
        {
            if (DeviceManager.Instance().PlcHandler.Count > 0)
            {
                _pcHeartBit = !_pcHeartBit;
                bool isMovingAxis = MotionManager.Instance().IsMovingAxis(AxisHandlerName.Handler0, AxisName.X);
                int currentPosX = GetPlcPosXData(unitName);

                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;
                var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_Alive);

                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                {
                    stream.AddSwap16BitData(Convert.ToInt16(_pcHeartBit));
                    stream.AddSwap16BitData(Convert.ToInt16(isMovingAxis));
                    stream.AddSwap16BitData(Convert.ToInt16(currentPosX));
                }
                else
                {
                    stream.Add16BitData(Convert.ToInt16(_pcHeartBit));
                    stream.Add16BitData(Convert.ToInt16(isMovingAxis));
                    stream.Add16BitData(Convert.ToInt16(currentPosX));
                }
                plc.Write("D" + map.AddressNum, stream.Data);
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

        public void WritePcCommand(PlcCommand command)
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
                    plc.Write("D" + map.AddressNum, stream.Data);
                }
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
            modelName = modelName.PadRight(20, Convert.ToChar("\0"));
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
                        stream.Add16BitData(Convert.ToInt16(value));
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
                {
                    stream.AddSwap16BitData(Convert.ToInt16(tabJudgement));
                }
                else
                {
                    stream.Add16BitData(Convert.ToInt16(tabJudgement));
                }
                AddAlignResult(plc.MelsecParser.ParserType, tabInspResult.AlignResult, resolution, ref stream);
                AddAkkonResult(plc.MelsecParser.ParserType, tabInspResult.AkkonResult, ref stream);

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        public void WriteTabResult(int tabNo, TabJudgement judgement, TabAlignResult alignResult, AkkonResult akkonResult, double resolution)
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
                {
                    stream.AddSwap16BitData(Convert.ToInt16(tabJudgement));
                }
                else
                {
                    stream.Add16BitData(Convert.ToInt16(tabJudgement));
                }

                AddAlignResult(plc.MelsecParser.ParserType, alignResult, resolution, ref stream);
                AddAkkonResult(plc.MelsecParser.ParserType, akkonResult, ref stream);

                plc.Write("D" + map.AddressNum, stream.Data);
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
                
                //double leftX_mm = alignResult.LeftX.ResultValue_pixel * resolution / 1000.0;
                //double leftY_mm = alignResult.LeftY.ResultValue_pixel * resolution / 1000.0;
                //double rightX_mm = alignResult.RightX.ResultValue_pixel * resolution / 1000.0;
                //double rightY_mm = alignResult.RightY.ResultValue_pixel * resolution / 1000.0;

                //int leftX_um = ConvertDoubleWordData(leftX_mm);
                //int leftY_um = ConvertDoubleWordData(leftY_mm);
                //int rightX_um = ConvertDoubleWordData(rightX_mm);
                //int rightY_um = ConvertDoubleWordData(rightY_mm);

                //if (parserType == ParserType.Binary)
                //{
                //    stream.AddSwap16BitData(Convert.ToInt16(alignJudgement));   // Align OK/NG
                //    stream.AddSwap32BitData(leftX_um);                          // Left AlignX
                //    stream.AddSwap32BitData(leftY_um);                          // Left AlignY
                //    stream.AddSwap32BitData(rightX_um);                         // Right AlignX
                //    stream.AddSwap32BitData(rightY_um);                         // Right AlignY
                //}
                //else
                //{
                //    stream.Add16BitData(Convert.ToInt16(alignJudgement));   // Align OK/NG
                //    stream.Add32BitData(leftX_um);                          // Left AlignX
                //    stream.Add32BitData(leftY_um);                          // Left AlignY
                //    stream.Add32BitData(rightX_um);                         // Right AlignX
                //    stream.Add32BitData(rightY_um);                         // Right AlignY
                //}
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

                double leftLengthAvg_mm = akkonResult.Length_Left_Avg_um / 1000.0;
                double leftLengthMin_mm = akkonResult.Length_Left_Min_um / 1000.0;
                double leftLengthMax_mm = akkonResult.Length_Left_Max_um / 1000.0;
                double rightLengthAvg_mm = akkonResult.Length_Right_Avg_um / 1000.0;
                double rightLengthMin_mm = akkonResult.Length_Right_Min_um / 1000.0;
                double rightLengthMax_mm = akkonResult.Length_Right_Max_um / 1000.0;

                int leftLengthAvg_um = ConvertDoubleWordData(leftLengthAvg_mm);
                int leftLengthMin_um = ConvertDoubleWordData(leftLengthMin_mm);
                int leftLengthMax_um = ConvertDoubleWordData(leftLengthMax_mm);
                int rightLengthAvg_um = ConvertDoubleWordData(rightLengthAvg_mm);
                int rightLengthMin_um = ConvertDoubleWordData(rightLengthMin_mm);
                int rightLengthMax_um = ConvertDoubleWordData(rightLengthMax_mm);
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

                    stream.AddSwap32BitData(leftLengthAvg_um); // Left Avg Length
                    stream.AddSwap32BitData(leftLengthMin_um); // Left Min Length
                    stream.AddSwap32BitData(leftLengthMax_um); // Left Max Length
                    stream.AddSwap32BitData(rightLengthAvg_um); // Right Avg Length
                    stream.AddSwap32BitData(rightLengthMin_um); // Right Min Length
                    stream.AddSwap32BitData(rightLengthMax_um); // Right Max Length
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

                    stream.Add32BitData(leftLengthAvg_um); // Left Avg Length
                    stream.Add32BitData(leftLengthMin_um); // Left Min Length
                    stream.Add32BitData(leftLengthMax_um); // Left Max Length
                    stream.Add32BitData(rightLengthAvg_um); // Right Avg Length
                    stream.Add32BitData(rightLengthMin_um); // Right Min Length
                    stream.Add32BitData(rightLengthMax_um); // Right Max Length
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
        #endregion
    }

    public partial class PlcControlManager
    {
        #region 메서드
        public int ConvertDoubleWordData(double value_mm)
        {
            double noneDotValue = Convert.ToDouble(value_mm.ToString("F4")) * 10000;

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

        private string ConvertAscll(byte[] hexBuffer, WordType wordType)
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
            int inputValue = Convert.ToInt32(GetValue(plcCommonMap));
            string outputValue = (inputValue / 10000.0).ToString("F4");

            return outputValue;
        }

        public double ConvertDoubleWordDoubleFormat_mm(PlcCommonMap plcCommonMap)
        {
            return Convert.ToDouble(ConvertDoubleWordStringFormat_mm(plcCommonMap));
        }
        #endregion
    }
}
