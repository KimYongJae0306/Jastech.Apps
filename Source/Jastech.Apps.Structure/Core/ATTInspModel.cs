using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.Core
{
    public class ATTInspModel : Jastech.Apps.Structure.AppsInspModel
    {
    }

    public enum ATTPreAlignName
    {
        MainLeft,
        MainRight,
        SubLeft1,
        SubRight1,
        SubLeft2,
        SubRight2,
        SubLeft3,
        SubRight3,
        SubLeft4,
        SubRight4,
    }

    public enum ATTTabAlignName
    {
        LeftFPCX,
        LeftPFCY,

        RightFPCX,
        RightPFCY,

        LeftPanelX,
        LeftPanelY,

        RightPanelX,
        RightPanelY,
    }
}
