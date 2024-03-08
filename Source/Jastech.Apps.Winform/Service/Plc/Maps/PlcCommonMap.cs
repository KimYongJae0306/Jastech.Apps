namespace Jastech.Apps.Winform.Service.Plc.Maps
{
    public enum PlcCommonMap
    {
        PC_Alive,
        PC_AxisX_Busy,
        PC_AxisX_CurPos,
        PC_AxisX_ServoOn,
        PLC_Alive,
        PLC_RunMode,
        PLC_DoorStatus,
        PC_AkkonByPass,

        PC_Ready,
        PLC_Ready,

        PC_Status_Common,
        PLC_Command_Common,

        PC_X_NegativeLimit,
        PC_X_PositiveLimit,
        PC_Z0_NegativeLimit,
        PC_Z0_PositiveLimit,
        PC_Z1_NegativeLimit,
        PC_Z1_PositiveLimit,

        PLC_PPID_ModelName,

        PLC_Time_Year,
        PLC_Time_Month,
        PLC_Time_Day,
        PLC_Time_Hour,
        PLC_Time_Minute,
        PLC_Time_Second,

        PC_ErrorCode,

        #region RecipeData

        //PLC_PanelX_Size,
        //PLC_MarkToMarkDistance,
        //PLC_PanelLeftEdgeToTab1LeftEdgeDistance,
        //PLC_TabCount, 
        //PLC_Axis_X_Speed,
  
        //PLC_Akkon_Count,
        //PLC_Akkon_Length,
        //PLC_Akkon_Strength,
        //PLC_Akkon_Min_Size,
        //PLC_Akkon_Max_Size,

        PLC_Tab0_Offset_Left,
        PLC_Tab1_Offset_Left,
        PLC_Tab2_Offset_Left,
        PLC_Tab3_Offset_Left,
        PLC_Tab4_Offset_Left,
        PLC_Tab5_Offset_Left,
        PLC_Tab6_Offset_Left,
        PLC_Tab7_Offset_Left,
        PLC_Tab8_Offset_Left,
        PLC_Tab9_Offset_Left,

        PLC_Tab0_Offset_Right,
        PLC_Tab1_Offset_Right,
        PLC_Tab2_Offset_Right,
        PLC_Tab3_Offset_Right,
        PLC_Tab4_Offset_Right,
        PLC_Tab5_Offset_Right,
        PLC_Tab6_Offset_Right,
        PLC_Tab7_Offset_Right,
        PLC_Tab8_Offset_Right,
        PLC_Tab9_Offset_Right,

        PLC_Tab0_Width,
        PLC_Tab1_Width,
        PLC_Tab2_Width,
        PLC_Tab3_Width,
        PLC_Tab4_Width,
        PLC_Tab5_Width,
        PLC_Tab6_Width,
        PLC_Tab7_Width,
        PLC_Tab8_Width,
        PLC_Tab9_Width,
 
        PLC_TabtoTab_Distance0,
        PLC_TabtoTab_Distance1,
        PLC_TabtoTab_Distance2,
        PLC_TabtoTab_Distance3,
        PLC_TabtoTab_Distance4,
        PLC_TabtoTab_Distance5,
        PLC_TabtoTab_Distance6,
        PLC_TabtoTab_Distance7,
        PLC_TabtoTab_Distance8,

        PLC_PanelX_Size,
        PLC_MarkToMarkDistance,
        PLC_PanelLeftEdgeToTab1LeftEdgeDistance,
        PLC_TabCount,
        PLC_Axis_X_Speed,

        PLC_Akkon_Count,
        PLC_Akkon_Length,
        PLC_Akkon_Strength,
        PLC_Akkon_Min_Size,
        PLC_Akkon_Max_Size,
        #endregion

        PC_GrabDone,
        PC_Command,
        PC_Status,
        PC_Move_REQ,

        PLC_AlignZ_ServoOnOff,
        PLC_AlignZ_Alarm,

        PLC_Status,
        PLC_Command,
        PLC_Move_END,

        PLC_FinalBond,
        PLC_ManualMatch,

        PLC_Cell_Id,

        PLC_PreBond_Tab0,
        PLC_PreBond_Tab1,
        PLC_PreBond_Tab2,
        PLC_PreBond_Tab3,
        PLC_PreBond_Tab4,
        PLC_PreBond_Tab5,
        PLC_PreBond_Tab6,
        PLC_PreBond_Tab7,
        PLC_PreBond_Tab8,
        PLC_PreBond_Tab9,

        PLC_AkkonZ_ServoOnOff,
        PLC_AkkonZ_Alarm,

        PC_AlignDataX,
        PC_AlignDataY,
        PC_AlignDataT,

        PLC_Position_AxisY,
        PLC_Position_AxisT,

        PLC_AlignDataX,

        PLC_OffsetDataX,
        PLC_OffsetDataY,
        PLC_OffsetDataT,
    }

    public enum PlcCommonCommand
    {
        None = -1,
        Time_Change = 6000,
        Model_Change = 8000,
        Model_Create = 8200,
        Model_Edit = 8400,
        Command_Clear = 9000,
        Light_Off = 9100,
    }

    public enum PcCommand
    {
        ServoOn_1 = 1000,               // Z축#1 Servo On
        ServoOff_1 = 1001,            // Z축#1 Servo Off
        ServoReset_1 = 1002,            // Z축#1 Servo Reset
        ServoOn_2 = 1010,               // Z축#2 Servo On
        ServoOff_2 = 1011,            // Z축#2 Servo Off
        ServoReset_2 = 1012,            // Z축#2 Servo Reset
        Required_Origin = 1050,     // Homming 요청
        Set_InspParameter = 1100,       // Align Edge Threshold 값 변경 알림
    }

    public enum PlcCommand
    {
        None = 0,
        StartInspection = 1100,         // 검사 시작 신호(Align+Akkon)
        StartPreAlign = 1200,   // PreAlign 검사 시작 신호
        Calibration = 1300,
        Origin_All = 1400,              // 모든 축 Homming
        Move_StandbyPos = 1500,         // X축을 Standby 위치로 이동
        Move_Left_AlignPos = 1501,      // X축을 Left Align 위치로 이동
        Move_Right_AlignPos = 1502,     // X축을 Right Align 위치로 이동
        Move_ScanStartPos = 1503,       // X축을 Scan Start 위치로 이동
        Move_ScanEndPos = 1504,         // X축을 Scan End 위치로 이동
    }
}
