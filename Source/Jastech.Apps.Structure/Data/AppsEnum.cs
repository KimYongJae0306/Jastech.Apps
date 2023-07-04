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

    //public enum CalibrationMarkName
    //{
    //    Calibraion_1,
    //    Calibraion_2,
    //    Calibraion_3,
    //    Calibraion_4,
    //    Calibraion_5,
    //    Calibraion_6,
    //    Calibraion_7,
    //    Calibraion_8,
    //    Calibraion_9,
    //}

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
