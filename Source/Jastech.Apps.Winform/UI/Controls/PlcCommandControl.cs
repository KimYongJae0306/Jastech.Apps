using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Service.Plc.Maps;
using Jastech.Framework.Algorithms.Akkon.Results;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Winform.Helper;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class PlcCommandControl : UserControl
    {
        #region 필드
        private bool _isLoading { get; set; } = false;
        #endregion

        #region 속성
        public double Resolution_um { get; set; } = 1;
        #endregion
        #region 생성자
        public PlcCommandControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void PlcCommandControl_Load(object sender, EventArgs e)
        {
            _isLoading = true;
            InitializeControls();
            _isLoading = false;
        }

        private void InitializeControls()
        {
            foreach (PlcCommonCommand type in Enum.GetValues(typeof(PlcCommonCommand)))
                cbxPcStatusCommon.Items.Add(type.ToString());
            cbxPcStatusCommon.SelectedIndex = 0;

            foreach (PcCommand type in Enum.GetValues(typeof(PcCommand)))
                cbxPCCommand.Items.Add(type.ToString());
            cbxPCCommand.SelectedIndex = 0;

            foreach (PlcCommand type in Enum.GetValues(typeof(PlcCommand)))
                cbxPCStatus.Items.Add(type.ToString());
            cbxPCStatus.SelectedIndex = 0;

            foreach (Judgement type in Enum.GetValues(typeof(Judgement)))
            {
                if (type != Judgement.FAIL)
                    cbxWriteAlignJudgement.Items.Add(type.ToString());
            }
            cbxWriteAlignJudgement.SelectedIndex = 0;

            foreach (Judgement type in Enum.GetValues(typeof(Judgement)))
            {
                if(type != Judgement.FAIL)
                    cbxWriteAkkonJudgement.Items.Add(type.ToString());
            }
            cbxWriteAkkonJudgement.SelectedIndex = 0;
        }

        public void UpdateData()
        {
            var manager = PlcControlManager.Instance();

            // PC
            lblPcAlive.Text = manager.GetValue(PlcCommonMap.PC_Alive);
            lblPcAxisXBusy.Text = manager.GetValue(PlcCommonMap.PC_AxisX_Busy);
            lblPcAxisXCurPos.Text = manager.GetValue(PlcCommonMap.PC_AxisX_CurPos);
            lblPcReady.Text = manager.GetValue(PlcCommonMap.PC_Ready);
            lblPcStatusCommon.Text = manager.GetValue(PlcCommonMap.PC_Status_Common);
            lblPcErrorCode.Text = manager.GetValue(PlcCommonMap.PC_ErrorCode);
            lblPcCommand.Text = manager.GetValue(PlcCommonMap.PC_Command);
            lblPcStatus.Text = manager.GetValue(PlcCommonMap.PC_Status);
            lblPcMoveReq.Text = manager.GetValue(PlcCommonMap.PC_Move_REQ);

            //lblPcAlignDataX.Text = manager.GetValue(PlcCommonMap.PC_AlignDataX_H) + "." + manager.GetValue(PlcCommonMap.PC_AlignDataX);
            lblPcAlignDataX.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PC_AlignDataX);

            //lblPcAlignDataY.Text = manager.GetValue(PlcCommonMap.PC_AlignDataY_H) + "." + manager.GetValue(PlcCommonMap.PC_AlignDataY);
            lblPcAlignDataY.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PC_AlignDataY);

            //lblPcAlignDataT.Text = manager.GetValue(PlcCommonMap.PC_AlignDataT_H) + "." + manager.GetValue(PlcCommonMap.PC_AlignDataT);
            lblPcAlignDataT.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PC_AlignDataT);

            // PLC
            lblPlcAlive.Text = manager.GetValue(PlcCommonMap.PLC_Alive);
            lblRunStatus.Text = manager.GetValue(PlcCommonMap.PLC_Run_Mode) == "2" ? "IDLE" : "AUTO";

            lblPlcReady.Text = manager.GetValue(PlcCommonMap.PLC_Ready);
            lblPlcCommandCommon.Text = manager.GetValue(PlcCommonMap.PLC_Command_Common);
            lblPlcAlignAxisZServoOnOff.Text = manager.GetValue(PlcCommonMap.PLC_AlignZ_ServoOnOff);
            lblPlcAlignAxisAlarm.Text = manager.GetValue(PlcCommonMap.PLC_AlignZ_Alarm);
            lblPlcStatus.Text = manager.GetValue(PlcCommonMap.PLC_Status);
            lblPlcCommand.Text = manager.GetValue(PlcCommonMap.PLC_Command);
            lblPlcMoveEnd.Text = manager.GetValue(PlcCommonMap.PLC_Move_END);
            lblPlcAkkonAxisZServoOnOff.Text = manager.GetValue(PlcCommonMap.PLC_AkkonZ_ServoOnOff);
            lblPlcAkkonAxisZAlarm.Text = manager.GetValue(PlcCommonMap.PLC_AkkonZ_Alarm);
            lblPlcManualMatch.Text = manager.GetValue(PlcCommonMap.PLC_ManualMatch);
            lblPlcCurAxisY.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Position_AxisY);
            lblPlcCurAxisT.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_Position_AxisT);
            lblPlcAlignDataX.Text = manager.ConvertDoubleWordStringFormat_mm(PlcCommonMap.PLC_AlignDataX);
            lblPlcOffsetDataX.Text = manager.GetValue(PlcCommonMap.PLC_OffsetDataX);
            lblPlcOffsetDataY.Text = manager.GetValue(PlcCommonMap.PLC_OffsetDataY);
            lblPlcOffsetDataT.Text = manager.GetValue(PlcCommonMap.PLC_OffsetDataT);
        }

        private void btnSetVision_Click(object sender, EventArgs e)
        {
            if (lblPcReady.Text == "9000")
                PlcControlManager.Instance().MachineStatus = MachineStatus.STOP;
            else
                PlcControlManager.Instance().MachineStatus = MachineStatus.RUN;
        }

        private void DrawComboboxCenterAlign(object sender, DrawItemEventArgs e)
        {
            try
            {
                ComboBox cmb = sender as ComboBox;

                if (cmb != null)
                {
                    e.DrawBackground();

                    if (cmb.Name.ToString().ToLower().Contains("group"))
                        cmb.ItemHeight = lblPcAlignDataT.Height - 6;
                    else
                        cmb.ItemHeight = lblPcAlignDataT.Height - 6;

                    if (e.Index >= 0)
                    {
                        StringFormat sf = new StringFormat();
                        sf.LineAlignment = StringAlignment.Center;
                        sf.Alignment = StringAlignment.Center;

                        Brush brush = new SolidBrush(cmb.ForeColor);

                        if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                            brush = SystemBrushes.HighlightText;

                        e.Graphics.DrawString(cmb.Items[e.Index].ToString(), cmb.Font, brush, e.Bounds, sf);
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
                throw;
            }
        }

        private void DrawCombobox(object sender, DrawItemEventArgs e)
        {
            DrawComboboxCenterAlign(sender, e);
        }

        private void cbxPcStatusCommon_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlcCommonCommand status = (PlcCommonCommand)Enum.Parse(typeof(PlcCommonCommand), cbxPcStatusCommon.SelectedItem as string);
            lblPcWriteStatusCommon.Text = ((int)status).ToString();
        }

        private void cbxPCCommand_SelectedIndexChanged(object sender, EventArgs e)
        {
            PcCommand command = (PcCommand)Enum.Parse(typeof(PcCommand), cbxPCCommand.SelectedItem as string);
            lblPcWriteCommand.Text = ((int)command).ToString();
        }

        private void cbxPCStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlcCommand command = (PlcCommand)Enum.Parse(typeof(PlcCommand), cbxPCStatus.SelectedItem as string);
            lblPcWriteStatus.Text = ((int)command).ToString();
        }

        private void btnPcMoveReq_Click(object sender, EventArgs e)
        {
            PlcControlManager.Instance().WriteMoveRequest();
        }

        private void btnStatusCommon_Click(object sender, EventArgs e)
        {
            PlcCommonCommand status = (PlcCommonCommand)Enum.Parse(typeof(PlcCommonCommand), cbxPcStatusCommon.SelectedItem as string);
            PlcControlManager.Instance().WritePcStatusCommon(status);
        }

        private void btnPcCommand_Click(object sender, EventArgs e)
        {
            PcCommand command = (PcCommand)Enum.Parse(typeof(PcCommand), cbxPCCommand.SelectedItem as string);
            PlcControlManager.Instance().WritePcCommand(command);
        }

        private void btnPcStatus_Click(object sender, EventArgs e)
        {
            PlcCommand command = (PlcCommand)Enum.Parse(typeof(PlcCommand), cbxPCStatus.SelectedItem as string);
            PlcControlManager.Instance().WritePcStatus(command);
        }

        private void lblWriteAlignDataX_Click(object sender, EventArgs e)
        {
           double value = KeyPadHelper.SetLabelDoubleData(lblWriteAlignDataX);
            lblWriteAlignDataX.Text = value.ToString();
        }

        private void lblWriteAlignDataY_Click(object sender, EventArgs e)
        {
            double value = KeyPadHelper.SetLabelDoubleData(lblWriteAlignDataY);
            lblWriteAlignDataY.Text = value.ToString();
        }

        private void lblWriteAlignDataT_Click(object sender, EventArgs e)
        {
            double value = KeyPadHelper.SetLabelDoubleData(lblWriteAlignDataT);
            lblWriteAlignDataT.Text = value.ToString();
        }

        private void btnSetPCAlignData_Click(object sender, EventArgs e)
        {
            double alignDataX = 0.0;
            double alignDataY = 0.0;
            double alignDataT = 0.0;

            if (lblWriteAlignDataX.Text != "")
                alignDataX = Convert.ToDouble(lblWriteAlignDataX.Text);

            if (lblWriteAlignDataY.Text != "")
                alignDataY = Convert.ToDouble(lblWriteAlignDataY.Text);

            if (lblWriteAlignDataT.Text != "")
                alignDataT = Convert.ToDouble(lblWriteAlignDataT.Text);

            PlcControlManager.Instance().WriteAlignData(alignDataX, alignDataY, alignDataT);
        }

        private void lblWriteInspLeftAlignX_Click(object sender, EventArgs e)
        {
            double value = KeyPadHelper.SetLabelDoubleData(lblWriteInspLeftAlignX);
            lblWriteInspLeftAlignX.Text = value.ToString();
        }

        private void lblWriteInspLeftAlignY_Click(object sender, EventArgs e)
        {
            double value = KeyPadHelper.SetLabelDoubleData(lblWriteInspLeftAlignY);
            lblWriteInspLeftAlignY.Text = value.ToString();
        }

        private void lblWriteInspRightAlignX_Click(object sender, EventArgs e)
        {
            double value = KeyPadHelper.SetLabelDoubleData(lblWriteInspRightAlignX);
            lblWriteInspRightAlignX.Text = value.ToString();
        }
       
        private void lblWriteInspRightAlignY_Click(object sender, EventArgs e)
        {
            double value = KeyPadHelper.SetLabelDoubleData(lblWriteInspRightAlignY);
            lblWriteInspRightAlignY.Text = value.ToString();
        }

        private void lblLeftAkkonCountAvg_Click(object sender, EventArgs e)
        {
            int value = KeyPadHelper.SetLabelIntegerData(lblLeftAkkonCountAvg);
            lblLeftAkkonCountAvg.Text = value.ToString();
        }

        private void lblLeftAkkonCountMin_Click(object sender, EventArgs e)
        {
            int value = KeyPadHelper.SetLabelIntegerData(lblLeftAkkonCountMin);
            lblLeftAkkonCountMin.Text = value.ToString();
        }

        private void lblLeftAkkonCountMax_Click(object sender, EventArgs e)
        {
            int value = KeyPadHelper.SetLabelIntegerData(lblLeftAkkonCountMax);
            lblLeftAkkonCountMax.Text = value.ToString();
        }

        private void lblRightAkkonCountAvg_Click(object sender, EventArgs e)
        {
            int value = KeyPadHelper.SetLabelIntegerData(lblRightAkkonCountAvg);
            lblRightAkkonCountAvg.Text = value.ToString();
        }

        private void lblRightAkkonCountMin_Click(object sender, EventArgs e)
        {
            int value = KeyPadHelper.SetLabelIntegerData(lblRightAkkonCountMin);
            lblRightAkkonCountMin.Text = value.ToString();
        }

        private void lblRightAkkonCountMax_Click(object sender, EventArgs e)
        {
            int value = KeyPadHelper.SetLabelIntegerData(lblRightAkkonCountMax);
            lblRightAkkonCountMax.Text = value.ToString();
        }

        private void lblLeftLengthAvg_Click(object sender, EventArgs e)
        {
            int value = KeyPadHelper.SetLabelIntegerData(lblLeftLengthAvg);
            lblLeftLengthAvg.Text = value.ToString();
        }

        private void lblLeftLengthMin_Click(object sender, EventArgs e)
        {
            int value = KeyPadHelper.SetLabelIntegerData(lblLeftLengthMin);
            lblLeftLengthMin.Text = value.ToString();
        }

        private void lblLeftLengthMax_Click(object sender, EventArgs e)
        {
            int value = KeyPadHelper.SetLabelIntegerData(lblLeftLengthMax);
            lblLeftLengthMax.Text = value.ToString();
        }

        private void lblRightLengthAvg_Click(object sender, EventArgs e)
        {
            int value = KeyPadHelper.SetLabelIntegerData(lblRightLengthAvg);
            lblRightLengthAvg.Text = value.ToString();
        }

        private void lblRightLengthMin_Click(object sender, EventArgs e)
        {
            int value = KeyPadHelper.SetLabelIntegerData(lblRightLengthMin);
            lblRightLengthMin.Text = value.ToString();
        }

        private void lblRightLengthMax_Click(object sender, EventArgs e)
        {
            int value = KeyPadHelper.SetLabelIntegerData(lblRightLengthMax);
            lblRightLengthMax.Text = value.ToString();
        }

        private void btnCommand_Common_Click(object sender, EventArgs e)
        {
            PlcControlManager.Instance().ClearAddress(PlcCommonMap.PLC_Command_Common);
        }

        private void btnClearPlcStatus_Click(object sender, EventArgs e)
        {
            PlcControlManager.Instance().ClearAddress(PlcCommonMap.PLC_Status);
        }

        private void btnClearPlcCommand_Click(object sender, EventArgs e)
        {
            PlcControlManager.Instance().ClearAddress(PlcCommonMap.PLC_Command);
        }

        private void btnClearPlcMoveEnd_Click(object sender, EventArgs e)
        {
            PlcControlManager.Instance().ClearAddress(PlcCommonMap.PLC_Move_END);
        }

        private void btnClearPcData_Click(object sender, EventArgs e)
        {
            var startAddress = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_Alive);
            var endAddress = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PC_AlignDataT);
            int length = endAddress.AddressNum + endAddress.WordSize - startAddress.AddressNum;

            PlcControlManager.Instance().ClearAddress(PlcCommonMap.PC_Alive, length);
        }

        private void btnWriteCurrentModel_Click(object sender, EventArgs e)
        {
            PlcControlManager.Instance().WriteCurrentModelName(lblCurrentModel.Text);
        }

        private void btnWriteInspResult_Click(object sender, EventArgs e)
        {
            CheckTabResultValue();

            TabInspResult inspResult = new TabInspResult();
            inspResult.TabNo = Convert.ToInt32(lblWriteTabNo.Text);
            inspResult.IsManualOK = Convert.ToInt32(lblManualOK.Text) == 0 ? false : true;

            CreateAlignResult(ref inspResult);
            CreateAkkonResult(ref inspResult);

            PlcControlManager.Instance().WriteTabResult(inspResult, Resolution_um);
        }

        private void CreateAlignResult(ref TabInspResult tabInspResult)
        {
            var result = tabInspResult.AlignResult;
            if (result == null)
                result = new TabAlignResult();

            result.LeftX = new AlignResult();
            result.LeftY = new AlignResult();
            result.RightX = new AlignResult();
            result.RightY = new AlignResult();

            // Default 값이 OK 이므로 한개의 Judgement만 바꾸면 전체 결과가 바뀐다.
            result.LeftX.Fpc.Judgement = (Judgement)Enum.Parse(typeof(Judgement), cbxWriteAlignJudgement.SelectedItem as string);
            result.LeftX.ResultValue_pixel = GetStringToFloat(lblWriteInspLeftAlignX.Text) * 1000.0F / (float)Resolution_um;
            result.LeftY.ResultValue_pixel = GetStringToFloat(lblWriteInspLeftAlignY.Text) * 1000.0F / (float)Resolution_um;

            result.RightX.ResultValue_pixel = GetStringToFloat(lblWriteInspRightAlignX.Text) * 1000.0F / (float)Resolution_um;
            result.RightY.ResultValue_pixel = GetStringToFloat(lblWriteInspRightAlignY.Text) * 1000.0F / (float)Resolution_um;
        }

        private void CreateAkkonResult(ref TabInspResult tabInspResult)
        {
            var result = tabInspResult.AkkonResult;
            if (result == null)
                result = new AkkonResult();

            result.Judgement = (Judgement)Enum.Parse(typeof(Judgement), cbxWriteAkkonJudgement.SelectedItem as string);
            result.LeftCount_Avg = Convert.ToInt32(lblLeftAkkonCountAvg.Text);
            result.LeftCount_Min = Convert.ToInt32(lblLeftAkkonCountMin.Text);
            result.LeftCount_Max = Convert.ToInt32(lblLeftAkkonCountMax.Text);
            result.RightCount_Avg = Convert.ToInt32(lblRightAkkonCountAvg.Text);
            result.RightCount_Min = Convert.ToInt32(lblRightAkkonCountMin.Text);
            result.RightCount_Max = Convert.ToInt32(lblRightAkkonCountMax.Text);

            result.Length_Left_Avg_um = GetStringToFloat(lblLeftLengthAvg.Text) * 1000.0F / (float)Resolution_um;
            result.Length_Left_Min_um = GetStringToFloat(lblLeftLengthMin.Text) * 1000.0F / (float)Resolution_um;
            result.Length_Left_Max_um = GetStringToFloat(lblLeftLengthMax.Text) * 1000.0F / (float)Resolution_um;
            result.Length_Right_Avg_um = GetStringToFloat(lblRightLengthAvg.Text) * 1000.0F / (float)Resolution_um;
            result.Length_Right_Min_um = GetStringToFloat(lblRightLengthMin.Text) * 1000.0F / (float)Resolution_um;
            result.Length_Right_Max_um = GetStringToFloat(lblRightLengthMax.Text) * 1000.0F / (float)Resolution_um;
        }

        private float GetStringToFloat(string value)
        {
            return Convert.ToSingle(value);
        }

        private void CheckTabResultValue()
        {
            lblWriteTabNo.Text = lblWriteTabNo.Text == "" ? "0" : lblWriteTabNo.Text;
            lblManualOK.Text = lblManualOK.Text == "" ? "0" : lblManualOK.Text;

            lblWriteInspLeftAlignX.Text = lblWriteInspLeftAlignX.Text == "" ? "0" : lblWriteInspLeftAlignX.Text;
            lblWriteInspLeftAlignY.Text = lblWriteInspLeftAlignY.Text == "" ? "0" : lblWriteInspLeftAlignY.Text;
            lblWriteInspRightAlignX.Text = lblWriteInspRightAlignX.Text == "" ? "0" : lblWriteInspRightAlignX.Text;
            lblWriteInspRightAlignY.Text = lblWriteInspRightAlignY.Text == "" ? "0" : lblWriteInspRightAlignY.Text;

            lblLeftAkkonCountAvg.Text = lblLeftAkkonCountAvg.Text == "" ? "0" : lblLeftAkkonCountAvg.Text;
            lblLeftAkkonCountMin.Text = lblLeftAkkonCountMin.Text == "" ? "0" : lblLeftAkkonCountMin.Text;
            lblLeftAkkonCountMax.Text = lblLeftAkkonCountMax.Text == "" ? "0" : lblLeftAkkonCountMax.Text;
            lblRightAkkonCountAvg.Text = lblRightAkkonCountAvg.Text == "" ? "0" : lblRightAkkonCountAvg.Text;
            lblRightAkkonCountMin.Text = lblRightAkkonCountMin.Text == "" ? "0" : lblRightAkkonCountMin.Text;
            lblRightAkkonCountMax.Text = lblRightAkkonCountMax.Text == "" ? "0" : lblRightAkkonCountMax.Text;

            lblLeftLengthAvg.Text = lblLeftLengthAvg.Text == "" ? "0" : lblLeftLengthAvg.Text;
            lblLeftLengthMin.Text = lblLeftLengthMin.Text == "" ? "0" : lblLeftLengthMin.Text;
            lblLeftLengthMax.Text = lblLeftLengthMax.Text == "" ? "0" : lblLeftLengthMax.Text;
            lblRightLengthAvg.Text = lblRightLengthAvg.Text == "" ? "0" : lblRightLengthAvg.Text;
            lblRightLengthMin.Text = lblRightLengthMin.Text == "" ? "0" : lblRightLengthMin.Text;
            lblRightLengthMax.Text = lblRightLengthMax.Text == "" ? "0" : lblRightLengthMax.Text;

        }
        #endregion
    }
}
