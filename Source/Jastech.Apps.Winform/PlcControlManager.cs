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
using System;
using System.Collections.Generic;
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

        public CommandEventDelegate OnPlcCommonCommandReceived;

        public CommandEventDelegate OnPlcCommandReceived;

        public delegate void CommandEventDelegate(int numbber);

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

                        if (map.Name == PlcCommonMap.PLC_Command_Common.ToString())
                        {
                            int command = Convert.ToInt32(map.Value);
                            OnPlcCommonCommandReceived?.Invoke(command);
                        }
                        else if (map.Name == PlcCommonMap.PLC_Command.ToString())
                        {
                            int command = Convert.ToInt32(map.Value);
                            OnPlcCommandReceived?.Invoke(command);
                        }
                    }
                }
            }
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
            PlcReadTask.Wait();
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

        public void WritePcStatusCommon(StatusCommon status)
        {
            var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_Status_Common);
            if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;

                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                    stream.AddSwap16BitData(Convert.ToInt16(status));
                else
                    stream.Add16BitData(Convert.ToInt16(status));

                plc.Write("D" + map.AddressNum, stream.Data);
            }
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

        public void WritePcStatus(PlcCommand command)
        {
            var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_Status);
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

        public void WriteMoveRequest()
        {
            var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_Move_REQ);
            if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;

                int value = 5;
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
            var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_AlignDataX_L);
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
            ClearAlignData();

            SplitDouble(alignDataX_mm, out int alignDataX_H, out int alignDataX_L);
            SplitDouble(alignDataY_mm, out int alignDataY_H, out int alignDataY_L);
            SplitDouble(alignDataT_mm, out int alignDataT_H, out int alignDataT_L);

            var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_AlignDataX_L);
            if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;
                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                {
                    stream.AddSwap16BitData(Convert.ToInt16(alignDataX_L));
                    stream.AddSwap16BitData(Convert.ToInt16(alignDataX_H));
                    stream.AddSwap16BitData(Convert.ToInt16(alignDataY_L));
                    stream.AddSwap16BitData(Convert.ToInt16(alignDataY_H));
                    stream.AddSwap16BitData(Convert.ToInt16(alignDataT_L));
                    stream.AddSwap16BitData(Convert.ToInt16(alignDataT_H));
                }
                else
                {
                    stream.Add16BitData(Convert.ToInt16(alignDataX_L));
                    stream.Add16BitData(Convert.ToInt16(alignDataX_H));
                    stream.Add16BitData(Convert.ToInt16(alignDataY_L));
                    stream.Add16BitData(Convert.ToInt16(alignDataY_H));
                    stream.Add16BitData(Convert.ToInt16(alignDataT_L));
                    stream.Add16BitData(Convert.ToInt16(alignDataT_H));
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
            if (leftAlignResultX.Judgement != Judgement.OK || leftAlignResultY.Judgement != Judgement.OK
                || rightAlignResultX.Judgement != Judgement.OK || rightAlignResultY.Judgement != Judgement.OK)
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

        public void WriteTabAkkonResult(int tabNo, AkkonJudgement judgement, AkkonResult akkonResult)
        {
            string akkonJudegmentName = string.Format("Tab{0}_Akkon_Count_Judgement", tabNo);
            PlcResultMap plcResultMap = (PlcResultMap)Enum.Parse(typeof(PlcResultMap), akkonJudegmentName);

            var map = PlcControlManager.Instance().GetResultMap(plcResultMap);

            if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;
                PlcDataStream stream = new PlcDataStream();
                if (plc.MelsecParser.ParserType == ParserType.Binary)
                {
                    stream.AddSwap16BitData(Convert.ToInt16((int)judgement));
                    stream.AddSwap16BitData(Convert.ToInt16(akkonResult.LeftCount_Avg));
                    stream.AddSwap16BitData(Convert.ToInt16(akkonResult.LeftCount_Min));
                    stream.AddSwap16BitData(Convert.ToInt16(akkonResult.LeftCount_Max));
                    stream.AddSwap16BitData(Convert.ToInt16(akkonResult.RightCount_Avg));
                    stream.AddSwap16BitData(Convert.ToInt16(akkonResult.RightCount_Min));
                    stream.AddSwap16BitData(Convert.ToInt16(akkonResult.RightCount_Max));

                    // Empty 넣어주기(237~239)
                    stream.AddSwap16BitData(0);
                    stream.AddSwap16BitData(0);
                    stream.AddSwap16BitData(0);

                    stream.AddSwap16BitData(Convert.ToInt16(akkonResult.LengthJudgement == Judgement.OK ? 1 : 2));
                    stream.AddSwap16BitData(Convert.ToInt16((int)akkonResult.Length_Left_Avg_um));
                    stream.AddSwap16BitData(Convert.ToInt16((int)akkonResult.Length_Left_Min_um));
                    stream.AddSwap16BitData(Convert.ToInt16((int)akkonResult.Length_Left_Max_um));
                    stream.AddSwap16BitData(Convert.ToInt16((int)akkonResult.Length_Right_Avg_um));
                    stream.AddSwap16BitData(Convert.ToInt16((int)akkonResult.Length_Right_Min_um));
                    stream.AddSwap16BitData(Convert.ToInt16((int)akkonResult.Length_Right_Max_um));

                }
                else
                {
                    stream.Add16BitData(Convert.ToInt16((int)judgement));
                    stream.Add16BitData(Convert.ToInt16(akkonResult.LeftCount_Avg));
                    stream.Add16BitData(Convert.ToInt16(akkonResult.LeftCount_Min));
                    stream.Add16BitData(Convert.ToInt16(akkonResult.LeftCount_Max));
                    stream.Add16BitData(Convert.ToInt16(akkonResult.RightCount_Avg));
                    stream.Add16BitData(Convert.ToInt16(akkonResult.RightCount_Min));
                    stream.Add16BitData(Convert.ToInt16(akkonResult.RightCount_Max));

                    // Empty 넣어주기(237~239)
                    stream.Add16BitData(0);
                    stream.Add16BitData(0);
                    stream.Add16BitData(0);

                    stream.Add16BitData(Convert.ToInt16(akkonResult.LengthJudgement == Judgement.OK ? 1 : 2));
                    stream.Add16BitData(Convert.ToInt16((int)akkonResult.Length_Left_Avg_um));
                    stream.Add16BitData(Convert.ToInt16((int)akkonResult.Length_Left_Min_um));
                    stream.Add16BitData(Convert.ToInt16((int)akkonResult.Length_Left_Max_um));
                    stream.Add16BitData(Convert.ToInt16((int)akkonResult.Length_Right_Avg_um));
                    stream.Add16BitData(Convert.ToInt16((int)akkonResult.Length_Right_Min_um));
                    stream.Add16BitData(Convert.ToInt16((int)akkonResult.Length_Right_Max_um));
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
                    string high = GetAddressMap(PlcCommonMap.PLC_Position_AxisY_H).Value;
                    string low = GetAddressMap(PlcCommonMap.PLC_Position_AxisY_L).Value;

                    position = Convert.ToDouble(high + "." + low);
                }
                else if (axisName == AxisName.T)
                {
                    string high = GetAddressMap(PlcCommonMap.PLC_Position_AxisT_H).Value;
                    string low = GetAddressMap(PlcCommonMap.PLC_Position_AxisT_L).Value;

                    position = Convert.ToDouble(high + "." + low);
                }
            }

            return position;
        }

        public void WritePreAlignResult(double leftScore, double rightScore)
        {
            var map = PlcControlManager.Instance().GetResultMap(PlcResultMap.PreAlign0_Left_L);
            if (DeviceManager.Instance().PlcHandler.Count > 0 && map != null)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;

                SplitDouble(leftScore, out int leftHight, out int leftLow);
                SplitDouble(rightScore, out int rightHight, out int rightLow);

                PlcDataStream stream = new PlcDataStream();
                if (plc.MelsecParser.ParserType == ParserType.Binary)
                {
                    stream.AddSwap16BitData(Convert.ToInt16(leftLow));
                    stream.AddSwap16BitData(Convert.ToInt16(leftHight));
                    stream.AddSwap16BitData(Convert.ToInt16(rightLow));
                    stream.AddSwap16BitData(Convert.ToInt16(rightHight));
                }
                else
                {
                    stream.Add16BitData(Convert.ToInt16(leftLow));
                    stream.Add16BitData(Convert.ToInt16(leftHight));
                    stream.Add16BitData(Convert.ToInt16(rightLow));
                    stream.Add16BitData(Convert.ToInt16(rightHight));
                }
                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }
    }

    public partial class PlcControlManager
    {
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

            switch (wordType)
            {
                case WordType.DEC:
                    for (int i = 0; i < hexBuffer.Length; i += 2)
                    {
                        byte[] dataArray = new byte[2];
                        Array.Copy(hexBuffer, i, dataArray, 0, 2);
                        int value = BitConverter.ToInt16(dataArray, 0);

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
    }
}
