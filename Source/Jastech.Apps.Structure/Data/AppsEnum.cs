namespace Jastech.Apps.Structure.Data
{
    public enum Material
    {
        Fpc,
        Panel,
    }

    public enum TeachingPosType
    {
        Standby,

        Stage1_PreAlign_Left,
        Stage1_PreAlign_Right,
        Stage1_Scan_Start,
        Stage1_Scan_End
    }

    public enum MarkName
    {
        Main,
        Sub1,
        Sub2,
        Sub3,
        Sub4,
    }

    public enum MarkDirection
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

        Center,
    }
}
