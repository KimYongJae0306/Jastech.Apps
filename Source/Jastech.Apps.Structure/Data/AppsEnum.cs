using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.Data
{
    public enum Material
    {
        Fpc,
        Panel,
    }

    public enum TeachingPositionType
    {
        Standby,

        Stage1_PreAlign_Left,
        Stage1_PreAlign_Right,
        Stage1_Scan_Start,
        Stage1_Scan_End
    }

    public enum CameraName
    {
        Area,
        LeftArea,
        RightArea,
        LinscanMIL0,
        LinscanMIL1,
        LinscanVT0,
    }

    public enum LAFName
    {
        Align,
        Akkon,
    }

    public enum MarkName
    {
        Main,
        Sub1,
        Sub2,
        Sub3,
        Sub4,
    }

    public enum MarkDirecton
    {
        Left,
        Right,
    }

    public enum ATTTabAlignName
    {
        LeftFPCX,
        LeftFPCY,

        RightFPCX,
        RightFPCY,

        LeftPanelX,
        LeftPanelY,

        RightPanelX,
        RightPanelY,
    }
}
