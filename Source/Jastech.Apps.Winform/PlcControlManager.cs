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
    public class PlcControlManager
    {
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
            var plcHandler =  DeviceManager.Instance().PlcHandler;

            if (plcHandler == null)
                return;

            PlcAddressService.CreateMap();
            PlcAddressService.Initialize();
            StartReadTask();
        }

        public void Release()
        {
            StopReadTask();
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
            while(true)
            {
                if(CancelPlcReadTask.IsCancellationRequested)
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

        private void ReadCommand()
        {
            if(DeviceManager.Instance().PlcHandler.Count > 0)
            {
                var plc = DeviceManager.Instance().PlcHandler.First();
                plc.Read("D" + PlcAddressService.MinAddressNumber, PlcAddressService.AddressLength);
            }
        }

        private void WriteAlive()
        {
            if(DeviceManager.Instance().PlcHandler.Count > 0)
            {
                var plc = DeviceManager.Instance().PlcHandler.First() as MelsecPlc;
                PlcDataStream stream = new PlcDataStream();

                if(plc.MelsecParser.ParserType == ParserType.Binary)
                {
                    //Convert.ToInt16
                }
                else
                {

                }
            }
           // plc.Write("D" + PlcCommonMap.PC_Alive, );
        }
    }
}
