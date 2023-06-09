using Jastech.Framework.Device.Plcs;
using Jastech.Framework.Winform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform
{
    public class AppsPlcService
    {
        public List<AppsPlcAddressMap> AddressMapList { get; private set; } = new List<AppsPlcAddressMap>();

        private Plc Plc { get; set; } = null;

        private int MinAddressNumber { get; set; }

        private int MaxAddressNumber { get; set; }

        private int AddressLength { get; set; }

        public bool Initialize()
        {
            if (DeviceManager.Instance().PlcHandler == null)
                return false;

            Plc = DeviceManager.Instance().PlcHandler.First();

            lock(AddressMapList)
            {
                AddressMapList.Add(new AppsPlcAddressMap(AddressMapType))
            }

            return true;
        }

        private void CalcSettingAddress()
        {
            int min = int.MaxValue;
            int max = int.MinValue;
            foreach (var addressMap in AddressMapList)
            {
                int addressNum = (int)addressMap.AddressNum;

                if (min > addressNum)
                    min = addressNum;

                if (max < addressNum)
                    max = addressNum + addressMap.WordSize;
            }

            MaxAddressNumber = max;
            MinAddressNumber = min;
            AddressLength = Math.Abs(min - max);
        }
    }
}
