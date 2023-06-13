using ATT_UT_IPAD.Core.Plc;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATT_UT_IPAD
{
    public class PlcManager
    {
        public AppsPlcService PlcService = new AppsPlcService();
        public void CreateAddressMap()
        {
            PlcService.CommonMapList.Add(new AppsPlcAddressMap(PlcMap.Tab0_Align_Judgement, WordType.))
        }
    }
}
