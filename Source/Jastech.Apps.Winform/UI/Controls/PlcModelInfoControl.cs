using Jastech.Apps.Winform.Service.Plc.Maps;
using System;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class PlcModelInfoControl : UserControl
    {
        #region 생성자
        public PlcModelInfoControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void PlcModelInfoControl_Load(object sender, EventArgs e)
        {
        }

        public void UpdateData()
        {
            var manager = PlcControlManager.Instance();

            // Model Info
            string yyyy = manager.GetValue(PlcCommonMap.PLC_Time_Year);
            string MM = manager.GetValue(PlcCommonMap.PLC_Time_Month);
            string dd = manager.GetValue(PlcCommonMap.PLC_Time_Day);
            string hh = manager.GetValue(PlcCommonMap.PLC_Time_Hour);
            string mm = manager.GetValue(PlcCommonMap.PLC_Time_Minute);
            string ss = manager.GetValue(PlcCommonMap.PLC_Time_Second);
            lblTime.Text = string.Format("{0}/{1}/{2} {3}:{4}:{5}", yyyy, MM, dd, hh, mm, ss);
            lblModelName.Text = manager.GetValue(PlcCommonMap.PLC_PPID_ModelName);
            lblTabCount.Text = manager.GetValue(PlcCommonMap.PLC_TabCount);

            lblPanelXSize.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_PanelX_Size);
            lblMarkToMark.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_MarkToMarkDistance);
            lblEdgeDistance.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_PanelLeftEdgeToTab1LeftEdgeDistance);
            lblAxisXSpeed.Text = manager.GetValue(PlcCommonMap.PLC_Axis_X_Speed);

            // Recipe Data
            lblTabWidth_0.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab0_Width);
            lblTabWidth_1.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab1_Width);
            lblTabWidth_2.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab2_Width);
            lblTabWidth_3.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab3_Width);
            lblTabWidth_4.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab4_Width);
            lblTabWidth_5.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab5_Width);
            lblTabWidth_6.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab6_Width);
            lblTabWidth_7.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab7_Width);
            lblTabWidth_8.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab8_Width);
            lblTabWidth_9.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab9_Width);

            lblLeftOffset_0.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab0_Offset_Left);
            lblLeftOffset_1.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab1_Offset_Left);
            lblLeftOffset_2.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab2_Offset_Left);
            lblLeftOffset_3.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab3_Offset_Left);
            lblLeftOffset_4.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab4_Offset_Left);
            lblLeftOffset_5.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab5_Offset_Left);
            lblLeftOffset_6.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab6_Offset_Left);
            lblLeftOffset_7.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab7_Offset_Left);
            lblLeftOffset_8.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab8_Offset_Left);
            lblLeftOffset_9.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab9_Offset_Left);

            lblRightOffset_0.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab0_Offset_Right);
            lblRightOffset_1.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab1_Offset_Right);
            lblRightOffset_2.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab2_Offset_Right);
            lblRightOffset_3.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab3_Offset_Right);
            lblRightOffset_4.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab4_Offset_Right);
            lblRightOffset_5.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab5_Offset_Right);
            lblRightOffset_6.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab6_Offset_Right);
            lblRightOffset_7.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab7_Offset_Right);
            lblRightOffset_8.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab8_Offset_Right);
            lblRightOffset_9.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Tab9_Offset_Right);

            lblEdgeToEdgeDistance_0.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_TabtoTab_Distance0);
            lblEdgeToEdgeDistance_1.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_TabtoTab_Distance1);
            lblEdgeToEdgeDistance_2.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_TabtoTab_Distance2);
            lblEdgeToEdgeDistance_3.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_TabtoTab_Distance3);
            lblEdgeToEdgeDistance_4.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_TabtoTab_Distance4);
            lblEdgeToEdgeDistance_5.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_TabtoTab_Distance5);
            lblEdgeToEdgeDistance_6.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_TabtoTab_Distance6);
            lblEdgeToEdgeDistance_7.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_TabtoTab_Distance7);
            lblEdgeToEdgeDistance_8.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_TabtoTab_Distance8);
        }
        #endregion
    }
}
