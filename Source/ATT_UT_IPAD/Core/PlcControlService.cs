using ATT_UT_IPAD.Core.Plc;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATT_UT_IPAD.Core
{
    public class PlcControlService : AppsPlcService
    {
        public void CreateMap()
        {
            AddCommonMap();
        }

        private void AddCommonMap()
        {
            CommonMapList.Add(new AppsPlcAddressMap(PlcCommonMap.tab0_)
        }
    }
}
