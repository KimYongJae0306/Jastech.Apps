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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform
{
    public partial class PlcControlManager
    {
        private ParserType ParserType { get; set; } = ParserType.Binary;

        private PlcAddressService PlcAddressService { get; set; } = new PlcAddressService();

        private static PlcControlManager _instance = null;

        public Task PlcReadTask { get; set; }

        public CancellationTokenSource CancelPlcReadTask { get; set; }

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
            if (PlcReadTask != null)
                return;

            CancelPlcReadTask = new CancellationTokenSource();
            PlcReadTask = new Task(PlcReadAction, CancelPlcReadTask.Token);
            PlcReadTask.Start();
        }

        public void StopReadTask()
        {
            if (PlcReadTask == null)
                return;

            CancelPlcReadTask.Cancel();
            //PlcReadTask.Wait();
            PlcReadTask = null;
        }

        private void PlcReadAction()
        {
            while (true)
            {
                if (CancelPlcReadTask.IsCancellationRequested)
                {
                    break;
                }

                if (DeviceManager.Instance().PlcHandler.Count > 0)
                {
                    ReadCommand();
                }

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

        private void WritePcAlive()
        {
            if (DeviceManager.Instance().PlcHandler.Count > 0)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;
                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                {
                    //stream.AddSwapData(Convert.ToInt16());
                    //Convert.ToInt16
                }
                else
                {

                }
            }
            // plc.Write("D" + PlcCommonMap.PC_Alive, );
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
            int convertAlignX = ConvertPlcAlignData(alignDataX_mm);
            int convertAlignY = ConvertPlcAlignData(alignDataY_mm);
            int convertAlignT = ConvertPlcAlignData(alignDataT_mm);

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

        public void WriteTabAlignResult(int tabNo, AlignResult leftAlignResultX, AlignResult leftAlignResultY, AlignResult rightAlignResultX, AlignResult rightAlignResultY, double resolution)
        {
            int judgement = 1; // 1 : OK, 2: NG
            if (leftAlignResultX.Judgement != Judgment.OK || leftAlignResultY.Judgement != Judgment.OK
                || rightAlignResultX.Judgement != Judgment.OK || rightAlignResultY.Judgement != Judgment.OK)
                judgement = 2;

            double calcLeftAlignX_mm = (leftAlignResultX.ResultValue_pixel * resolution) * 1000;
            double calcLeftAlignY_mm = (leftAlignResultY.ResultValue_pixel * resolution) * 1000;
            double calcRightAlignX_mm = (rightAlignResultX.ResultValue_pixel * resolution) * 1000;
            double calcRightAlignY_mm = (rightAlignResultY.ResultValue_pixel * resolution) * 1000;

            WriteTabAlignResult(tabNo, judgement, calcLeftAlignX_mm, calcLeftAlignY_mm, calcRightAlignX_mm, calcRightAlignY_mm);
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

        public void WriteTabAkkonResult(int tabNo, AkkonResult akkonResult)
        {
            string alignJudegmentName = string.Format("Tab{0}_Akkon_Judgement", tabNo);
            PlcResultMap plcResultMap = (PlcResultMap)Enum.Parse(typeof(PlcResultMap), alignJudegmentName);
            var map = PlcControlManager.Instance().GetResultMap(plcResultMap);

            if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
            {
                int countJudgement = 1; // 1 : OK, 2: NG
                if (akkonResult.AkkonCountJudgement == AkkonJudgement.OK)
                    countJudgement = 1;
                else
                    countJudgement = 2;

                int lengthJudgement = 1;
                if (akkonResult.LengthJudgement == Judgment.OK)
                    lengthJudgement = 1;
                else
                    lengthJudgement = 2;

                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;
                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                {
                    stream.AddSwap16BitData(Convert.ToInt16(countJudgement));
                    stream.AddSwap16BitData(Convert.ToInt16(akkonResult.LeftCount_Avg));
                    stream.AddSwap16BitData(Convert.ToInt16(akkonResult.LeftCount_Min));
                    stream.AddSwap16BitData(Convert.ToInt16(akkonResult.LeftCount_Max));
                    stream.AddSwap16BitData(Convert.ToInt16(akkonResult.RightCount_Avg));
                    stream.AddSwap16BitData(Convert.ToInt16(akkonResult.RightCount_Min));
                    stream.AddSwap16BitData(Convert.ToInt16(akkonResult.RightCount_Max));
                    // Empty 넣어주기
                    stream.AddSwap16BitData(0);
                    stream.AddSwap16BitData(0);
                    stream.AddSwap16BitData(0);
                    stream.AddSwap16BitData(0);

                    stream.AddSwap16BitData(Convert.ToInt16(lengthJudgement));

                }
                else
                {
                    stream.Add16BitData(Convert.ToInt16(countJudgement));
                    stream.Add16BitData(Convert.ToInt16(akkonResult.LeftCount_Avg));
                    stream.Add16BitData(Convert.ToInt16(akkonResult.LeftCount_Min));
                    stream.Add16BitData(Convert.ToInt16(akkonResult.LeftCount_Max));
                    stream.Add16BitData(Convert.ToInt16(akkonResult.RightCount_Avg));
                    stream.Add16BitData(Convert.ToInt16(akkonResult.RightCount_Min));
                    stream.Add16BitData(Convert.ToInt16(akkonResult.RightCount_Max));
                }
                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        //public void WriteTabAkkonResult(int tabNo, AkkonJudgement judgement, AkkonResult akkonResult)
        //{
        //    string akkonJudegmentName = string.Format("Tab{0}_Akkon_Count_Judgement", tabNo);
        //    PlcResultMap plcResultMap = (PlcResultMap)Enum.Parse(typeof(PlcResultMap), akkonJudegmentName);

        //    var map = PlcControlManager.Instance().GetResultMap(plcResultMap);

        //    if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
        //    {
        //        var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;
        //        PlcDataStream stream = new PlcDataStream();
        //        if (plc.MelsecParser.ParserType == ParserType.Binary)
        //        {
        //            stream.AddSwap16BitData(Convert.ToInt16((int)judgement));
        //            stream.AddSwap16BitData(Convert.ToInt16(akkonResult.LeftCount_Avg));
        //            stream.AddSwap16BitData(Convert.ToInt16(akkonResult.LeftCount_Min));
        //            stream.AddSwap16BitData(Convert.ToInt16(akkonResult.LeftCount_Max));
        //            stream.AddSwap16BitData(Convert.ToInt16(akkonResult.RightCount_Avg));
        //            stream.AddSwap16BitData(Convert.ToInt16(akkonResult.RightCount_Min));
        //            stream.AddSwap16BitData(Convert.ToInt16(akkonResult.RightCount_Max));

        //            // Empty 넣어주기(237~239)
        //            stream.AddSwap16BitData(0);
        //            stream.AddSwap16BitData(0);
        //            stream.AddSwap16BitData(0);

        //            stream.AddSwap16BitData(Convert.ToInt16(akkonResult.LengthJudgement == Judgement.OK ? 1 : 2));
        //            stream.AddSwap16BitData(Convert.ToInt16((int)akkonResult.Length_Left_Avg_um));
        //            stream.AddSwap16BitData(Convert.ToInt16((int)akkonResult.Length_Left_Min_um));
        //            stream.AddSwap16BitData(Convert.ToInt16((int)akkonResult.Length_Left_Max_um));
        //            stream.AddSwap16BitData(Convert.ToInt16((int)akkonResult.Length_Right_Avg_um));
        //            stream.AddSwap16BitData(Convert.ToInt16((int)akkonResult.Length_Right_Min_um));
        //            stream.AddSwap16BitData(Convert.ToInt16((int)akkonResult.Length_Right_Max_um));

        //        }
        //        else
        //        {
        //            stream.Add16BitData(Convert.ToInt16((int)judgement));
        //            stream.Add16BitData(Convert.ToInt16(akkonResult.LeftCount_Avg));
        //            stream.Add16BitData(Convert.ToInt16(akkonResult.LeftCount_Min));
        //            stream.Add16BitData(Convert.ToInt16(akkonResult.LeftCount_Max));
        //            stream.Add16BitData(Convert.ToInt16(akkonResult.RightCount_Avg));
        //            stream.Add16BitData(Convert.ToInt16(akkonResult.RightCount_Min));
        //            stream.Add16BitData(Convert.ToInt16(akkonResult.RightCount_Max));

        //            // Empty 넣어주기(237~239)
        //            stream.Add16BitData(0);
        //            stream.Add16BitData(0);
        //            stream.Add16BitData(0);

        //            stream.Add16BitData(Convert.ToInt16(akkonResult.LengthJudgement == Judgement.OK ? 1 : 2));
        //            stream.Add16BitData(Convert.ToInt16((int)akkonResult.Length_Left_Avg_um));
        //            stream.Add16BitData(Convert.ToInt16((int)akkonResult.Length_Left_Min_um));
        //            stream.Add16BitData(Convert.ToInt16((int)akkonResult.Length_Left_Max_um));
        //            stream.Add16BitData(Convert.ToInt16((int)akkonResult.Length_Right_Avg_um));
        //            stream.Add16BitData(Convert.ToInt16((int)akkonResult.Length_Right_Min_um));
        //            stream.Add16BitData(Convert.ToInt16((int)akkonResult.Length_Right_Max_um));
        //        }
        //        plc.Write("D" + map.AddressNum, stream.Data);
        //    }
        //}

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
    }

    public partial class PlcControlManager
    {
        public int ConvertPlcAlignData(double value_mm)
        {
            double noneDotValue = Convert.ToDouble(value_mm.ToString("F4")) * 10000;

            int returnValue = Convert.ToInt32(noneDotValue);
            return returnValue;
        }

        public int ConvertPlcScoreData(double score)
        {
            double noneDotValue = Convert.ToDouble(score.ToString("F2")) * 100;

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
    }
}
