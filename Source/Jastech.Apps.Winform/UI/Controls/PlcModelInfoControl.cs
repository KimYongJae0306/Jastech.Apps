using Jastech.Apps.Winform.Service.Plc.Maps;
using System;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class PlcModelInfoControl : UserControl
    {
        public PlcModelInfoControl()
        {
            InitializeComponent();
        }

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
            lblPanelXSize.Text = manager.GetValue(PlcCommonMap.PLC_PanelX_Size_H) + "." + manager.GetValue(PlcCommonMap.PLC_PanelX_Size_L);
            lblMarkToMark.Text = manager.GetValue(PlcCommonMap.PLC_MarkToMarkDistance_H) + "." + manager.GetValue(PlcCommonMap.PLC_MarkToMarkDistance_L);
            lblEdgeDistance.Text = manager.GetValue(PlcCommonMap.PLC_PanelLeftEdgeToTab1LeftEdgeDistance_H) + "." + manager.GetValue(PlcCommonMap.PLC_PanelLeftEdgeToTab1LeftEdgeDistance_L);
            lblAxisXSpeed.Text = manager.GetValue(PlcCommonMap.PLC_Axis_X_Speed);

            // Recipe Data
            lblTabWidth_0.Text = manager.GetValue(PlcCommonMap.PLC_Tab0_Width);
            lblTabWidth_1.Text = manager.GetValue(PlcCommonMap.PLC_Tab1_Width);
            lblTabWidth_2.Text = manager.GetValue(PlcCommonMap.PLC_Tab2_Width);
            lblTabWidth_3.Text = manager.GetValue(PlcCommonMap.PLC_Tab3_Width);
            lblTabWidth_4.Text = manager.GetValue(PlcCommonMap.PLC_Tab4_Width);
            lblTabWidth_5.Text = manager.GetValue(PlcCommonMap.PLC_Tab5_Width);
            lblTabWidth_6.Text = manager.GetValue(PlcCommonMap.PLC_Tab6_Width);
            lblTabWidth_7.Text = manager.GetValue(PlcCommonMap.PLC_Tab7_Width);
            lblTabWidth_8.Text = manager.GetValue(PlcCommonMap.PLC_Tab8_Width);
            lblTabWidth_9.Text = manager.GetValue(PlcCommonMap.PLC_Tab9_Width);

            lblLeftOffset_0.Text = manager.GetValue(PlcCommonMap.PLC_Tab0_Offset_Left);
            lblLeftOffset_1.Text = manager.GetValue(PlcCommonMap.PLC_Tab1_Offset_Left);
            lblLeftOffset_2.Text = manager.GetValue(PlcCommonMap.PLC_Tab2_Offset_Left);
            lblLeftOffset_3.Text = manager.GetValue(PlcCommonMap.PLC_Tab3_Offset_Left);
            lblLeftOffset_4.Text = manager.GetValue(PlcCommonMap.PLC_Tab4_Offset_Left);
            lblLeftOffset_5.Text = manager.GetValue(PlcCommonMap.PLC_Tab5_Offset_Left);
            lblLeftOffset_6.Text = manager.GetValue(PlcCommonMap.PLC_Tab6_Offset_Left);
            lblLeftOffset_7.Text = manager.GetValue(PlcCommonMap.PLC_Tab7_Offset_Left);
            lblLeftOffset_8.Text = manager.GetValue(PlcCommonMap.PLC_Tab8_Offset_Left);
            lblLeftOffset_9.Text = manager.GetValue(PlcCommonMap.PLC_Tab9_Offset_Left);

            lblRightOffset_0.Text = manager.GetValue(PlcCommonMap.PLC_Tab0_Offset_Right);
            lblRightOffset_1.Text = manager.GetValue(PlcCommonMap.PLC_Tab1_Offset_Right);
            lblRightOffset_2.Text = manager.GetValue(PlcCommonMap.PLC_Tab2_Offset_Right);
            lblRightOffset_3.Text = manager.GetValue(PlcCommonMap.PLC_Tab3_Offset_Right);
            lblRightOffset_4.Text = manager.GetValue(PlcCommonMap.PLC_Tab4_Offset_Right);
            lblRightOffset_5.Text = manager.GetValue(PlcCommonMap.PLC_Tab5_Offset_Right);
            lblRightOffset_6.Text = manager.GetValue(PlcCommonMap.PLC_Tab6_Offset_Right);
            lblRightOffset_7.Text = manager.GetValue(PlcCommonMap.PLC_Tab7_Offset_Right);
            lblRightOffset_8.Text = manager.GetValue(PlcCommonMap.PLC_Tab8_Offset_Right);
            lblRightOffset_9.Text = manager.GetValue(PlcCommonMap.PLC_Tab9_Offset_Right);

            lblEdgeToEdgeDistance_0.Text = manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance0_H) + "." + manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance0_L);
            lblEdgeToEdgeDistance_1.Text = manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance1_H) + "." + manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance1_L);
            lblEdgeToEdgeDistance_2.Text = manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance2_H) + "." + manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance2_L);
            lblEdgeToEdgeDistance_3.Text = manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance3_H) + "." + manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance3_L);
            lblEdgeToEdgeDistance_4.Text = manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance4_H) + "." + manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance4_L);
            lblEdgeToEdgeDistance_5.Text = manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance5_H) + "." + manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance5_L);
            lblEdgeToEdgeDistance_6.Text = manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance6_H) + "." + manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance6_L);
            lblEdgeToEdgeDistance_7.Text = manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance7_H) + "." + manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance7_L);
            lblEdgeToEdgeDistance_8.Text = manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance8_H) + "." + manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance8_L);
        }
    }
}
