using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Service.Plc.Maps;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Forms
{
    public partial class PlcStatusForm : Form
    {
        public PlcStatusForm()
        {
            InitializeComponent();
        }
        
        private void PlcStatusForm_Load(object sender, EventArgs e)
        {
            UpdateTimer.Start();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            UpdateTimer.Stop();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            var manager = PlcControlManager.Instance();

            // PC
            lblPcAlive.Text = manager.GetValue(PlcCommonMap.PC_Alive);
            lblPcReady.Text = manager.GetValue(PlcCommonMap.PC_Ready);
            lblPcStatusCommon.Text = manager.GetValue(PlcCommonMap.PC_Status_Common);
            lblPcErrorCode.Text = manager.GetValue(PlcCommonMap.PC_ErrorCode);
            lblPcCommand.Text = manager.GetValue(PlcCommonMap.PC_Command);
            lblPcStatus.Text = manager.GetValue(PlcCommonMap.PC_Status);
            lblPcMoveReq.Text = manager.GetValue(PlcCommonMap.PC_Move_REQ);
            lblPcAlignDataX.Text = manager.GetValue(PlcCommonMap.PC_AlignDataX_H) + "." + manager.GetValue(PlcCommonMap.PC_AlignDataX_L);
            lblPcAlignDataY.Text = manager.GetValue(PlcCommonMap.PC_AlignDataY_H) + "." + manager.GetValue(PlcCommonMap.PC_AlignDataY_L);
            lblPcAlignDataT.Text = manager.GetValue(PlcCommonMap.PC_AlignDataT_H) + "." + manager.GetValue(PlcCommonMap.PC_AlignDataT_L);

            // PLC
            lblPlcAlive.Text = manager.GetValue(PlcCommonMap.PLC_Alive);
            lblPlcReady.Text = manager.GetValue(PlcCommonMap.PLC_Ready);
            lblPlcCommandCommon.Text = manager.GetValue(PlcCommonMap.PLC_Command_Common);
            lblPlcStatus.Text = manager.GetValue(PlcCommonMap.PLC_Status);
            lblPlcCommand.Text = manager.GetValue(PlcCommonMap.PLC_Command);
            lblPlcMoveEnd.Text = manager.GetValue(PlcCommonMap.PLC_Move_END);
            lblPlcCurAxisY.Text = manager.GetValue(PlcCommonMap.PLC_Position_AxisY_H) + "." + manager.GetValue(PlcCommonMap.PLC_Position_AxisY_L);
            lblPlcCurAxisT.Text = manager.GetValue(PlcCommonMap.PLC_Position_AxisT_H) + "." + manager.GetValue(PlcCommonMap.PLC_Position_AxisT_L);
            lblPlcAlignDataX.Text = manager.GetValue(PlcCommonMap.PLC_AlignDataX_H) + "." + manager.GetValue(PlcCommonMap.PLC_AlignDataX_L);
            lblPlcOffsetDataX.Text = manager.GetValue(PlcCommonMap.PLC_OffsetDataX);
            lblPlcOffsetDataY.Text = manager.GetValue(PlcCommonMap.PLC_OffsetDataY);
            lblPlcOffsetDataT.Text = manager.GetValue(PlcCommonMap.PLC_OffsetDataT);

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
            lblEdgeToEdgeDistance_1.Text = manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance1_H) + "." + manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance0_L);
            lblEdgeToEdgeDistance_2.Text = manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance2_H) + "." + manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance0_L);
            lblEdgeToEdgeDistance_3.Text = manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance3_H) + "." + manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance0_L);
            lblEdgeToEdgeDistance_4.Text = manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance4_H) + "." + manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance0_L);
            lblEdgeToEdgeDistance_5.Text = manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance5_H) + "." + manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance0_L);
            lblEdgeToEdgeDistance_6.Text = manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance6_H) + "." + manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance0_L);
            lblEdgeToEdgeDistance_7.Text = manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance7_H) + "." + manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance0_L);
            lblEdgeToEdgeDistance_8.Text = manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance8_H) + "." + manager.GetValue(PlcCommonMap.PLC_TabtoTab_Distance0_L);
        }

        private void btnVisionReady_Click(object sender, EventArgs e)
        {
            //MachineStatus
            if(lblPcReady.Text == "9000")
                PlcControlManager.Instance().WritePcReady(MachineStatus.STOP);
            else
                PlcControlManager.Instance().WritePcReady(MachineStatus.RUN);
        }

        private void btnStatusCommon_Click(object sender, EventArgs e)
        {
            PlcControlManager.Instance().ClearAddress(PlcCommonMap.PLC_Command_Common);
        }
    }
}
