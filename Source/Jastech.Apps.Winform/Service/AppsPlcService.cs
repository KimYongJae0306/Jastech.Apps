using Jastech.Framework.Device.Plcs;
using Jastech.Framework.Winform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform.Service
{
    public class AppsPlcService
    {
        public List<AppsPlcAddressMap> CommonMapList { get; private set; } = new List<AppsPlcAddressMap>();
        private Plc Plc { get; set; } = null;

        private int MinAddressNumber { get; set; }

        private int MaxAddressNumber { get; set; }

        private int AddressLength { get; set; }

        public void CalcSettingAddress()
        {
            int min = int.MaxValue;
            int max = int.MinValue;
            foreach (var map in CommonMapList)
            {
                int addressNum = (int)map.AddressNum;

                if (min > addressNum)
                    min = addressNum;

                if (max < addressNum)
                    max = addressNum + map.WordSize;
            }

            MaxAddressNumber = max;
            MinAddressNumber = min;
            AddressLength = Math.Abs(min - max);
        }
    }
}
