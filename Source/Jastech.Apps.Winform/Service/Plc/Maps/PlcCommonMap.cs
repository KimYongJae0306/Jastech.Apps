using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform.Service.Plc.Maps
{
    public enum PlcCommonMap
    {
        PC_Alive,
        PLC_Alive,

        PC_Ready,
        PLC_Ready,

        PC_Status_Common,
        PLC_Command_Common,

        PLC_PPID_ModelName,

        PLC_Time_Year,
        PLC_Time_Month,
        PLC_Time_Day,
        PLC_Time_Hour,
        PLC_Time_Minute,
        PLC_Time_Second,

        PC_ErrorCode,

        #region RecipeData

        PLC_PanelX_Size_L,
        PLC_PanelX_Size_H,
        PLC_MarkToMarkDistance_L,
        PLC_MarkToMarkDistance_H,
        PLC_PanelLeftEdgeToTab1LeftEdgeDistance_L,
        PLC_PanelLeftEdgeToTab1LeftEdgeDistance_H,
        PLC_TabCount, 
        PLC_Axis_X_Speed,
  
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
 
        PLC_TabtoTab_Distance0_L,
        PLC_TabtoTab_Distance0_H,
        PLC_TabtoTab_Distance1_L,
        PLC_TabtoTab_Distance1_H,
        PLC_TabtoTab_Distance2_L,
        PLC_TabtoTab_Distance2_H,
        PLC_TabtoTab_Distance3_L,
        PLC_TabtoTab_Distance3_H,
        PLC_TabtoTab_Distance4_L,
        PLC_TabtoTab_Distance4_H,
        PLC_TabtoTab_Distance5_L,
        PLC_TabtoTab_Distance5_H,
        PLC_TabtoTab_Distance6_L,
        PLC_TabtoTab_Distance6_H,
        PLC_TabtoTab_Distance7_L,
        PLC_TabtoTab_Distance7_H,
        PLC_TabtoTab_Distance8_L,
        PLC_TabtoTab_Distance8_H,
        PLC_TabtoTab_Distance9_L,
        PLC_TabtoTab_Distance9_H,
        #endregion

        PC_Command,
        PC_Status,
        PC_Move_REQ,

        PLC_AlignZ_ServoOnOff,
        PLC_AlignZ_Status,

        PLC_Status,
        PLC_Command,
        PLC_Move_END,

        PLC_ManualMatch,

        PLC_Cell_Id,

        PLC_AkkonZ_ServoOnOff,
        PLC_AkkonZ_Status,

        PC_AlignDataX_L,
        PC_AlignDataX_H,
        PC_AlignDataY_L,
        PC_AlignDataY_H,
        PC_AlignDataT_L,
        PC_AlignDataT_H,

        PLC_Position_AxisY_L,
        PLC_Position_AxisY_H,
        PLC_Position_AxisT_L,
        PLC_Position_AxisT_H,

        PLC_AlignDataX_L,
        PLC_AlignDataX_H,

        PLC_OffsetDataX,
        PLC_OffsetDataY,
        PLC_OffsetDataT,
    }

    public enum StatusCommon
    {
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
        ServoReset_1 = 1001,            // Z축#1 Servo Reset
        ServoOn_2 = 1010,               // Z축#1 Servo On
        ServoReset_2 = 1011,            // Z축#1 Servo Reset
    }

    public enum PlcCommand
    {
        StartInspection = 1100,         // 검사 시작 신호(Align+Akkon)
        StartPreAlign_AutoRun = 1200,   // PreAlign 검사 시작 신호(AutoRun)
        StartPreAlign_Manual = 1201,    // PreAlign 검사 시작 신호(Manual) 
        Origin_All = 1400,              // 모든 축 Homming
        Origin_X = 1401,                // X 축 Homming
        Origin_Z1 = 1402,               // Z1 축 Homming
        Origin_Z2 = 1403,               // Z2 축 Homming
        Move_StandbyPos = 1500,         // X축을 Standby 위치로 이동
        Move_Left_AlignPos = 1501,      // X축을 Left Align 위치로 이동
        Move_Right_AlignPos = 1502,     // X축을 Right Align 위치로 이동
        Move_ScanStartPos = 1503,       // X축을 ScanStart 위치로 이동
        Move_AlignDataX = 1504,         // X축 Align 보정량 이동
    }
}
