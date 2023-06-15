using Jastech.Apps.Winform.Service.Plc;
using Jastech.Apps.Winform.Service.Plc.Maps;
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
        private PlcAddressStatus Status { get; set; } = new PlcAddressStatus();

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
                value = PlcAddressService.AddressMapList.Where(x => x.Name == map.ToString()).First().Value;

            return value;
        }

        private void ReadCommand()
        {
            if (DeviceManager.Instance().PlcHandler.Count > 0)
            {
                var plc = DeviceManager.Instance().PlcHandler.First();
                plc.Read("D" + PlcAddressService.MinAddressNumber, PlcAddressService.AddressLength);
            }
        }

        private void WriteAlive()
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
    }

    public partial class PlcControlManager
    {
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
