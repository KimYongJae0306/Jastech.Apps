using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATT_UT_IPAD.Core.Plc
{
    public enum PlcCommonMap
    {
        Vision_Ready,
        Plc_Ready,
        Common_Status,

        #region RecipeData
        PanelX_Size_L,
        PanelX_Size_H,
        MarkToMarkDistance_L,
        MarkToMarkDistance_H,
        PanelLeftEdgeToTab1LeftEdgeDistance,

        Tab0_Offset_Left,
        Tab1_Offset_Left,
        Tab2_Offset_Left,
        Tab3_Offset_Left,
        Tab4_Offset_Left,
        Tab5_Offset_Left,
        Tab6_Offset_Left,
        Tab7_Offset_Left,
        Tab8_Offset_Left,
        Tab9_Offset_Left,

        Tab0_Offset_Right,
        Tab1_Offset_Right,
        Tab2_Offset_Right,
        Tab3_Offset_Right,
        Tab4_Offset_Right,
        Tab5_Offset_Right,
        Tab6_Offset_Right,
        Tab7_Offset_Right,
        Tab8_Offset_Right,
        Tab9_Offset_Right,

        Tab0_Width,
        Tab1_Width,
        Tab2_Width,
        Tab3_Width,
        Tab4_Width,
        Tab5_Width,
        Tab6_Width,
        Tab7_Width,
        Tab8_Width,
        Tab9_Width,

        TabtoTab_Distance0_L,
        TabtoTab_Distance0_H,
        TabtoTab_Distance1_L,
        TabtoTab_Distance1_H,
        TabtoTab_Distance2_L,
        TabtoTab_Distance2_H,
        TabtoTab_Distance3_L,
        TabtoTab_Distance3_H,
        TabtoTab_Distance4_L,
        TabtoTab_Distance4_H,
        TabtoTab_Distance5_L,
        TabtoTab_Distance5_H,
        TabtoTab_Distance6_L,
        TabtoTab_Distance6_H,
        TabtoTab_Distance7_L,
        TabtoTab_Distance7_H,
        TabtoTab_Distance8_L,
        TabtoTab_Distance8_H,
        TabtoTab_Distance9_L,
        TabtoTab_Distance9_H,
        #endregion

        #region PreAlign

        PreAlign_CellId_0,
        PreAlign_CellId_1,
        PreAlign_CellId_2,
        PreAlign_CellId_3,

        #endregion

        #region 검사

        Insp_0_CellId_0,
        Insp_0_CellId_1,
        Insp_0_CellId_2,
        Insp_0_CellId_3,

        #endregion
    }
}
