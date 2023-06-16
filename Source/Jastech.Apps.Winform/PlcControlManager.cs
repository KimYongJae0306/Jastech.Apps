using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Service.Plc;
using Jastech.Apps.Winform.Service.Plc.Maps;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Device.Plcs;
using Jastech.Framework.Device.Plcs.Melsec;
using Jastech.Framework.Device.Plcs.Melsec.Parsers;
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
                lock(PlcAddressService.AddressMapList)
                {
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
            PlcReadTask = new Task(PlcReadFunction, CancelPlcReadTask.Token);
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

        private void PlcReadFunction()
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
                if(PlcAddressService.AddressMapList.Count() > 0)
                    value = PlcAddressService.AddressMapList.Where(x => x.Name == map.ToString()).First().Value;
            }

            return value;
        }

        public PlcAddressMap GetAddressMap(PlcCommonMap map)
        {
            lock (PlcAddressService.AddressMapList)
            {
                return PlcAddressService.AddressMapList.Where(x => x.Name == map.ToString()).First();
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
            if (DeviceManager.Instance().PlcHandler.Count > 0)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;

                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                {
                    for (int i = 0; i < length; i++)
                    {
                        stream.AddSwap32BitData(0);
                    }
                }
                else
                {
                    for (int i = 0; i < length; i++)
                    {
                        stream.Add32BitData(0);
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

            if (DeviceManager.Instance().PlcHandler.Count > 0)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;

                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                    stream.AddSwap32BitData(value);
                else
                    stream.Add32BitData(value);

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        public void WritePcStatusCommon(StatusCommon status)
        {
            var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_Status_Common);
            if (DeviceManager.Instance().PlcHandler.Count > 0)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;

                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                    stream.AddSwap32BitData((int)status);
                else
                    stream.Add32BitData((int)status);

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        public void WritePcErrorCode()
        {
            var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_ErrorCode);
            if (DeviceManager.Instance().PlcHandler.Count > 0)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;
                int errorCode = 0; // error 기준 성립 후 함수화 진행해야함

                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                    stream.AddSwap32BitData(errorCode);
                else
                    stream.Add32BitData(errorCode);

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        public void WritePcCommand(PcCommand command)
        {
            var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_Command);
            if (DeviceManager.Instance().PlcHandler.Count > 0)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;

                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                    stream.AddSwap32BitData((int)command);
                else
                    stream.Add32BitData((int)command);

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        public void WritePcCommand(PlcCommand command)
        {
            var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_Command);
            if (DeviceManager.Instance().PlcHandler.Count > 0)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;

                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                    stream.AddSwap32BitData((int)command);
                else
                    stream.Add32BitData((int)command);

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        public void WritePcStatus(PlcCommand command)
        {
            var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_Status);
            if (DeviceManager.Instance().PlcHandler.Count > 0)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;

                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                    stream.AddSwap32BitData((int)command);
                else
                    stream.Add32BitData((int)command);

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        public void WriteMoveRequest()
        {
            var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_Move_REQ);
            if (DeviceManager.Instance().PlcHandler.Count > 0)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;

                int value = 5;
                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                    stream.AddSwap32BitData((int)value);
                else
                    stream.Add32BitData((int)value);

                plc.Write("D" + map.AddressNum, stream.Data);
            }
        }

        public void WriteAlignData(double alignDataX, double alignDataY, double alignDataT)
        {
            SplitDoulbe(alignDataX, out int alignDataX_H, out int alignDataX_L);
            SplitDoulbe(alignDataY, out int alignDataY_H, out int alignDataY_L);
            SplitDoulbe(alignDataT, out int alignDataT_H, out int alignDataT_L);

            var map = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_AlignDataX_L);
            if (DeviceManager.Instance().PlcHandler.Count > 0)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;
                PlcDataStream stream = new PlcDataStream();

                if (plc.MelsecParser.ParserType == ParserType.Binary)
                {
                    stream.AddSwap32BitData(alignDataX_L);
                    stream.AddSwap32BitData(alignDataX_H);
                    stream.AddSwap32BitData(alignDataY_L);
                    stream.AddSwap32BitData(alignDataY_H);
                    stream.AddSwap32BitData(alignDataT_L);
                    stream.AddSwap32BitData(alignDataT_H);
                }
                else
                {
                    stream.Add32BitData(alignDataX_L);
                    stream.Add32BitData(alignDataX_H);
                    stream.Add32BitData(alignDataY_L);
                    stream.Add32BitData(alignDataY_H);
                    stream.Add32BitData(alignDataT_L);
                    stream.Add32BitData(alignDataT_H);
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
    }

    public partial class PlcControlManager
    {
        public void SplitDoulbe(double value, out int high, out int low)
        {
            high = 0;
            low = 0;
            string valueString = value.ToString("0.000");
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
