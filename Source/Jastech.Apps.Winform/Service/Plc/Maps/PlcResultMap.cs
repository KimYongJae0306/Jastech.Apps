using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform.Service.Plc.Maps
{
    // Tab 결과 관련 이름 변경 시 다른 부분에서 에러나므로 바꾸지 마세요. (CreateResultMap 와 결과 전송할 때 연관 있음)
    public enum PlcResultMap
    {
        Current_ModelName,

        #region Tab0
        Tab0_Align_Judgement,
        Tab0_Align_Left_X,
        Tab0_Align_Left_Y,
        Tab0_Align_Right_X,
        Tab0_Align_Right_Y,

        Tab0_Akkon_Count_Judgement,
        Tab0_Akkon_Count_Left_Avg,
        Tab0_Akkon_Count_Left_Min,
        Tab0_Akkon_Count_Left_Max,
        Tab0_Akkon_Count_Right_Avg,
        Tab0_Akkon_Count_Right_Min,
        Tab0_Akkon_Count_Right_Max,

        Tab0_Akkon_Length_Judement,
        Tab0_Akkon_Length_Left_Avg,
        Tab0_Akkon_Length_Left_Min,
        Tab0_Akkon_Length_Left_Max,

        Tab0_Akkon_Length_Right_Avg,
        Tab0_Akkon_Length_Right_Min,
        Tab0_Akkon_Length_Right_Max,
        #endregion

        #region Tab1
        Tab1_Align_Judgement,
        Tab1_Align_Left_X,
        Tab1_Align_Left_Y,
        Tab1_Align_Right_X,
        Tab1_Align_Right_Y,

        Tab1_Akkon_Judgement,
        Tab1_Akkon_Count_Left_Avg,
        Tab1_Akkon_Count_Left_Min,
        Tab1_Akkon_Count_Left_Max,
        Tab1_Akkon_Count_Right_Avg,
        Tab1_Akkon_Count_Right_Min,
        Tab1_Akkon_Count_Right_Max,

        Tab1_Akkon_Length_Judement,
        Tab1_Akkon_Length_Left_Avg,
        Tab1_Akkon_Length_Left_Min,
        Tab1_Akkon_Length_Left_Max,

        Tab1_Akkon_Length_Right_Avg,
        Tab1_Akkon_Length_Right_Min,
        Tab1_Akkon_Length_Right_Max,
        #endregion

        #region Tab2
        Tab2_Align_Judgement,
        Tab2_Align_Left_X,
        Tab2_Align_Left_Y,
        Tab2_Align_Right_X,
        Tab2_Align_Right_Y,

        Tab2_Akkon_Judgement,
        Tab2_Akkon_Count_Left_Avg,
        Tab2_Akkon_Count_Left_Min,
        Tab2_Akkon_Count_Left_Max,
        Tab2_Akkon_Count_Right_Avg,
        Tab2_Akkon_Count_Right_Min,
        Tab2_Akkon_Count_Right_Max,

        Tab2_Akkon_Length_Judement,
        Tab2_Akkon_Length_Left_Avg,
        Tab2_Akkon_Length_Left_Min,
        Tab2_Akkon_Length_Left_Max,

        Tab2_Akkon_Length_Right_Avg,
        Tab2_Akkon_Length_Right_Min,
        Tab2_Akkon_Length_Right_Max,
        #endregion

        #region Tab3
        Tab3_Align_Judgement,
        Tab3_Align_Left_X,
        Tab3_Align_Left_Y,
        Tab3_Align_Right_X,
        Tab3_Align_Right_Y,

        Tab3_Akkon_Judgement,
        Tab3_Akkon_Count_Left_Avg,
        Tab3_Akkon_Count_Left_Min,
        Tab3_Akkon_Count_Left_Max,
        Tab3_Akkon_Count_Right_Avg,
        Tab3_Akkon_Count_Right_Min,
        Tab3_Akkon_Count_Right_Max,

        Tab3_Akkon_Length_Judement,
        Tab3_Akkon_Length_Left_Avg,
        Tab3_Akkon_Length_Left_Min,
        Tab3_Akkon_Length_Left_Max,

        Tab3_Akkon_Length_Right_Avg,
        Tab3_Akkon_Length_Right_Min,
        Tab3_Akkon_Length_Right_Max,
        #endregion

        #region Tab4
        Tab4_Align_Judgement,
        Tab4_Align_Left_X,
        Tab4_Align_Left_Y,
        Tab4_Align_Right_X,
        Tab4_Align_Right_Y,

        Tab4_Akkon_Judgement,
        Tab4_Akkon_Count_Left_Avg,
        Tab4_Akkon_Count_Left_Min,
        Tab4_Akkon_Count_Left_Max,
        Tab4_Akkon_Count_Right_Avg,
        Tab4_Akkon_Count_Right_Min,
        Tab4_Akkon_Count_Right_Max,

        Tab4_Akkon_Length_Judement,
        Tab4_Akkon_Length_Left_Avg,
        Tab4_Akkon_Length_Left_Min,
        Tab4_Akkon_Length_Left_Max,

        Tab4_Akkon_Length_Right_Avg,
        Tab4_Akkon_Length_Right_Min,
        Tab4_Akkon_Length_Right_Max,
        #endregion

        #region Tab5
        Tab5_Align_Judgement,
        Tab5_Align_Left_X,
        Tab5_Align_Left_Y,
        Tab5_Align_Right_X,
        Tab5_Align_Right_Y,

        Tab5_Akkon_Judgement,
        Tab5_Akkon_Count_Left_Avg,
        Tab5_Akkon_Count_Left_Min,
        Tab5_Akkon_Count_Left_Max,
        Tab5_Akkon_Count_Right_Avg,
        Tab5_Akkon_Count_Right_Min,
        Tab5_Akkon_Count_Right_Max,

        Tab5_Akkon_Length_Judement,
        Tab5_Akkon_Length_Left_Avg,
        Tab5_Akkon_Length_Left_Min,
        Tab5_Akkon_Length_Left_Max,

        Tab5_Akkon_Length_Right_Avg,
        Tab5_Akkon_Length_Right_Min,
        Tab5_Akkon_Length_Right_Max,
        #endregion

        #region Tab6
        Tab6_Align_Judgement,
        Tab6_Align_Left_X,
        Tab6_Align_Left_Y,
        Tab6_Align_Right_X,
        Tab6_Align_Right_Y,

        Tab6_Akkon_Judgement,
        Tab6_Akkon_Count_Left_Avg,
        Tab6_Akkon_Count_Left_Min,
        Tab6_Akkon_Count_Left_Max,
        Tab6_Akkon_Count_Right_Avg,
        Tab6_Akkon_Count_Right_Min,
        Tab6_Akkon_Count_Right_Max,

        Tab6_Akkon_Length_Judement,
        Tab6_Akkon_Length_Left_Avg,
        Tab6_Akkon_Length_Left_Min,
        Tab6_Akkon_Length_Left_Max,

        Tab6_Akkon_Length_Right_Avg,
        Tab6_Akkon_Length_Right_Min,
        Tab6_Akkon_Length_Right_Max,
        #endregion

        #region Tab7
        Tab7_Align_Judgement,
        Tab7_Align_Left_X,
        Tab7_Align_Left_Y,
        Tab7_Align_Right_X,
        Tab7_Align_Right_Y,

        Tab7_Akkon_Judgement,
        Tab7_Akkon_Count_Left_Avg,
        Tab7_Akkon_Count_Left_Min,
        Tab7_Akkon_Count_Left_Max,
        Tab7_Akkon_Count_Right_Avg,
        Tab7_Akkon_Count_Right_Min,
        Tab7_Akkon_Count_Right_Max,

        Tab7_Akkon_Length_Judement,
        Tab7_Akkon_Length_Left_Avg,
        Tab7_Akkon_Length_Left_Min,
        Tab7_Akkon_Length_Left_Max,

        Tab7_Akkon_Length_Right_Avg,
        Tab7_Akkon_Length_Right_Min,
        Tab7_Akkon_Length_Right_Max,
        #endregion

        #region Tab8
        Tab8_Align_Judgement,
        Tab8_Align_Left_X,
        Tab8_Align_Left_Y,
        Tab8_Align_Right_X,
        Tab8_Align_Right_Y,

        Tab8_Akkon_Judgement,
        Tab8_Akkon_Count_Left_Avg,
        Tab8_Akkon_Count_Left_Min,
        Tab8_Akkon_Count_Left_Max,
        Tab8_Akkon_Count_Right_Avg,
        Tab8_Akkon_Count_Right_Min,
        Tab8_Akkon_Count_Right_Max,

        Tab8_Akkon_Length_Judement,
        Tab8_Akkon_Length_Left_Avg,
        Tab8_Akkon_Length_Left_Min,
        Tab8_Akkon_Length_Left_Max,

        Tab8_Akkon_Length_Right_Avg,
        Tab8_Akkon_Length_Right_Min,
        Tab8_Akkon_Length_Right_Max,
        #endregion

        #region Tab9
        Tab9_Align_Judgement,
        Tab9_Align_Left_X,
        Tab9_Align_Left_Y,
        Tab9_Align_Right_X,
        Tab9_Align_Right_Y,

        Tab9_Akkon_Judgement,
        Tab9_Akkon_Count_Left_Avg,
        Tab9_Akkon_Count_Left_Min,
        Tab9_Akkon_Count_Left_Max,
        Tab9_Akkon_Count_Right_Avg,
        Tab9_Akkon_Count_Right_Min,
        Tab9_Akkon_Count_Right_Max,

        Tab9_Akkon_Length_Judement,
        Tab9_Akkon_Length_Left_Avg,
        Tab9_Akkon_Length_Left_Min,
        Tab9_Akkon_Length_Left_Max,

        Tab9_Akkon_Length_Right_Avg,
        Tab9_Akkon_Length_Right_Min,
        Tab9_Akkon_Length_Right_Max,
        #endregion
    }
}
