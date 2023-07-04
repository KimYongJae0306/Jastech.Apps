using Jastech.Apps.Winform.Service.Plc.Maps;
using Jastech.Apps.Winform.Settings;
using System;
using System.Collections.Generic;

namespace Jastech.Apps.Winform.Service.Plc
{
    public partial class PlcAddressService
    {
        public byte[] OrgData { get; set; }

        public List<PlcAddressMap> ResultMapList { get; set; } = new List<PlcAddressMap>();

        public List<PlcAddressMap> AddressMapList { get; set; } = new List<PlcAddressMap>();

        public int MinAddressNumber { get; set; } = 0;

        public int MaxAddressNumber { get; set; } = 800;

        public int AddressLength { get; set; } = 800;
    }

    public partial class PlcAddressService
    {
        public void CreateMap()
        {
            CreateAddressMap();
            CreateResultMap();
        }

        public void Initialize()
        {
            int min = int.MaxValue;
            int max = int.MinValue;
            foreach (var addressMap in AddressMapList)
            {
                int addressNum = (int)addressMap.AddressNum;

                if (min > addressNum)
                    min = addressNum;

                if (max < addressNum)
                    max = addressNum + addressMap.WordSize;
            }

            MaxAddressNumber = max;
            MinAddressNumber = min;
            AddressLength = Math.Abs(min - max);
        }

        private void CreateResultMap()
        {
            // Current Model Name
            ResultMapList.Add(new PlcAddressMap(PlcResultMap.Current_ModelName, WordType.HEX, AppsConfig.Instance().PlcAddressInfo.ResultStart, 10));

            int tabTotabInterval = AppsConfig.Instance().PlcAddressInfo.ResultTabToTabInterval; // AddressMap 참고 

            // Align Results
            CreateAlignResult(AppsConfig.Instance().PlcAddressInfo.ResultStart_Align, tabTotabInterval);

            // Akkon Results
            CreateAkkonResult(AppsConfig.Instance().PlcAddressInfo.ResultStart_Akkon, tabTotabInterval);

            CreatePreAlign(AppsConfig.Instance().PlcAddressInfo.ResultStart_PreAlign);
        }

        private void CreateAlignResult(int alignStartIndex, int tabTotabInterval)
        {
            int maxCount = AppsConfig.Instance().TabMaxCount;
            int addressNum = alignStartIndex;

            for (int i = 0; i < maxCount; i++)
            {
                string alignJudement = string.Format("Tab{0}_Align_Judgement", i);
                string alignLeftX = string.Format("Tab{0}_Align_Left_X", i);
                string alignLeftY = string.Format("Tab{0}_Align_Left_Y", i);
                string alignRightX = string.Format("Tab{0}_Align_Right_X", i);
                string alignRightY = string.Format("Tab{0}_Align_Right_Y", i);

                int addressIndex = addressNum + (tabTotabInterval * i);
                ResultMapList.Add(new PlcAddressMap((PlcResultMap)Enum.Parse(typeof(PlcResultMap), alignJudement), WordType.DEC, addressIndex, 1));
                ResultMapList.Add(new PlcAddressMap((PlcResultMap)Enum.Parse(typeof(PlcResultMap), alignLeftX), WordType.DEC, addressIndex + 1, 1));
                ResultMapList.Add(new PlcAddressMap((PlcResultMap)Enum.Parse(typeof(PlcResultMap), alignLeftY), WordType.DEC, addressIndex + 2, 1));
                ResultMapList.Add(new PlcAddressMap((PlcResultMap)Enum.Parse(typeof(PlcResultMap), alignRightX), WordType.DEC, addressIndex + 3, 1));
                ResultMapList.Add(new PlcAddressMap((PlcResultMap)Enum.Parse(typeof(PlcResultMap), alignRightY), WordType.DEC, addressIndex + 4, 1));
            }
        }

        private void CreateAkkonResult(int akkonStartIndex, int tabTotabInterval)
        {
            int maxCount = AppsConfig.Instance().TabMaxCount;
            int addressNum = akkonStartIndex;

            for (int i = 0; i < maxCount; i++)
            {
                string akkonCountJudgement = string.Format("Tab{0}_Akkon_Judgement", i);
                string akkonCountLeftAvg = string.Format("Tab{0}_Akkon_Count_Left_Avg", i);
                string akkonCountLeftMin = string.Format("Tab{0}_Akkon_Count_Left_Min", i);
                string akkonCountLeftMax = string.Format("Tab{0}_Akkon_Count_Left_Max", i);
                string akkonCountRightAvg = string.Format("Tab{0}_Akkon_Count_Right_Avg", i);
                string akkonCountRightMin = string.Format("Tab{0}_Akkon_Count_Right_Min", i);
                string akkonCountRightMax = string.Format("Tab{0}_Akkon_Count_Right_Max", i);

                int addressIndex = addressNum + (tabTotabInterval * i);
                ResultMapList.Add(new PlcAddressMap((PlcResultMap)Enum.Parse(typeof(PlcResultMap), akkonCountJudgement), WordType.DEC, addressIndex, 1));
                ResultMapList.Add(new PlcAddressMap((PlcResultMap)Enum.Parse(typeof(PlcResultMap), akkonCountLeftAvg), WordType.DEC, addressIndex + 1, 1));
                ResultMapList.Add(new PlcAddressMap((PlcResultMap)Enum.Parse(typeof(PlcResultMap), akkonCountLeftMin), WordType.DEC, addressIndex + 2, 1));
                ResultMapList.Add(new PlcAddressMap((PlcResultMap)Enum.Parse(typeof(PlcResultMap), akkonCountLeftMax), WordType.DEC, addressIndex + 3, 1));
                ResultMapList.Add(new PlcAddressMap((PlcResultMap)Enum.Parse(typeof(PlcResultMap), akkonCountRightAvg), WordType.DEC, addressIndex + 4, 1));
                ResultMapList.Add(new PlcAddressMap((PlcResultMap)Enum.Parse(typeof(PlcResultMap), akkonCountRightMin), WordType.DEC, addressIndex + 5, 1));
                ResultMapList.Add(new PlcAddressMap((PlcResultMap)Enum.Parse(typeof(PlcResultMap), akkonCountRightMax), WordType.DEC, addressIndex + 6, 1));


                string akkonLengthJudgement = string.Format("Tab{0}_Akkon_Length_Judgement", i);
                string akkonLengthLeftAvg = string.Format("Tab{0}_Akkon_Length_Left_Avg", i);
                string akkonLengthLeftMin = string.Format("Tab{0}_Akkon_Length_Left_Min", i);
                string akkonLengthLeftMax = string.Format("Tab{0}_Akkon_Length_Left_Max", i);
                string akkonLengthRightAvg = string.Format("Tab{0}_Akkon_Length_Right_Avg", i);
                string akkonLengthRightMin = string.Format("Tab{0}_Akkon_Length_Right_Min", i);
                string akkonLengthRightMax = string.Format("Tab{0}_Akkon_Length_Right_Max", i);

                addressIndex = addressNum + 10 + (tabTotabInterval * i);
                ResultMapList.Add(new PlcAddressMap((PlcResultMap)Enum.Parse(typeof(PlcResultMap), akkonLengthJudgement), WordType.DEC, addressIndex, 1));
                ResultMapList.Add(new PlcAddressMap((PlcResultMap)Enum.Parse(typeof(PlcResultMap), akkonLengthLeftAvg), WordType.DEC, addressIndex + 1, 1));
                ResultMapList.Add(new PlcAddressMap((PlcResultMap)Enum.Parse(typeof(PlcResultMap), akkonLengthLeftMin), WordType.DEC, addressIndex + 2, 1));
                ResultMapList.Add(new PlcAddressMap((PlcResultMap)Enum.Parse(typeof(PlcResultMap), akkonLengthLeftMax), WordType.DEC, addressIndex + 3, 1));
                ResultMapList.Add(new PlcAddressMap((PlcResultMap)Enum.Parse(typeof(PlcResultMap), akkonLengthRightAvg), WordType.DEC, addressIndex + 4, 1));
                ResultMapList.Add(new PlcAddressMap((PlcResultMap)Enum.Parse(typeof(PlcResultMap), akkonLengthRightMin), WordType.DEC, addressIndex + 5, 1));
                ResultMapList.Add(new PlcAddressMap((PlcResultMap)Enum.Parse(typeof(PlcResultMap), akkonLengthRightMax), WordType.DEC, addressIndex + 6, 1));
            }
        }

        private void CreatePreAlign(int preAlignStartIndex)
        {
            ResultMapList.Add(new PlcAddressMap(PlcResultMap.PreAlign0_Left_L, WordType.DEC, preAlignStartIndex, 1));
            ResultMapList.Add(new PlcAddressMap(PlcResultMap.PreAlign0_Left_H, WordType.DEC, preAlignStartIndex, 1));
            ResultMapList.Add(new PlcAddressMap(PlcResultMap.PreAlign0_Right_L, WordType.DEC, preAlignStartIndex, 1));
            ResultMapList.Add(new PlcAddressMap(PlcResultMap.PreAlign0_Right_H, WordType.DEC, preAlignStartIndex, 1));
        }

        private void CreateAddressMap()
        {
            int index = AppsConfig.Instance().PlcAddressInfo.CommonStart;

            // 0~9
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PC_Alive, WordType.DEC, index, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PC_Ready, WordType.DEC, index + 4, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PC_Status_Common, WordType.DEC, index + 6, 1));

            // 100~109
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Alive, WordType.DEC, index + 100, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Ready, WordType.DEC, index + 104, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Command_Common, WordType.DEC, index + 106, 1));

            // 200~209
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_PPID_ModelName, WordType.HEX, index + 200, 10));

            // 300~309
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Time_Year, WordType.DEC, index + 300, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Time_Month, WordType.DEC, index + 301, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Time_Day, WordType.DEC, index + 302, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Time_Hour, WordType.DEC, index + 303, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Time_Minute, WordType.DEC, index + 304, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Time_Second, WordType.DEC, index + 305, 1));

            // 10~19
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PC_ErrorCode, WordType.DEC, index + 19, 1));

            #region Model 정보
            // 110~19
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_PanelX_Size_L, WordType.HEX, index + 110, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_PanelX_Size_H, WordType.HEX, index + 111, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_MarkToMarkDistance_L, WordType.HEX, index + 112, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_MarkToMarkDistance_H, WordType.HEX, index + 113, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_PanelLeftEdgeToTab1LeftEdgeDistance_L, WordType.HEX, index + 114, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_PanelLeftEdgeToTab1LeftEdgeDistance_H, WordType.HEX, index + 115, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabCount, WordType.HEX, index + 116, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Axis_X_Speed, WordType.HEX, index + 117, 1));

            // 210~219
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab0_Offset_Left, WordType.HEX, index + 210, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab1_Offset_Left, WordType.HEX, index + 211, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab2_Offset_Left, WordType.HEX, index + 212, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab3_Offset_Left, WordType.HEX, index + 213, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab4_Offset_Left, WordType.HEX, index + 214, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab5_Offset_Left, WordType.HEX, index + 215, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab6_Offset_Left, WordType.HEX, index + 216, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab7_Offset_Left, WordType.HEX, index + 217, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab8_Offset_Left, WordType.HEX, index + 218, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab9_Offset_Left, WordType.HEX, index + 219, 1));

            // 310~319
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab0_Offset_Right, WordType.HEX, index + 310, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab1_Offset_Right, WordType.HEX, index + 311, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab2_Offset_Right, WordType.HEX, index + 312, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab3_Offset_Right, WordType.HEX, index + 313, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab4_Offset_Right, WordType.HEX, index + 314, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab5_Offset_Right, WordType.HEX, index + 315, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab6_Offset_Right, WordType.HEX, index + 316, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab7_Offset_Right, WordType.HEX, index + 317, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab8_Offset_Right, WordType.HEX, index + 318, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab9_Offset_Right, WordType.HEX, index + 319, 1));

            // 410~419
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab0_Width, WordType.HEX, index + 410, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab1_Width, WordType.HEX, index + 411, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab2_Width, WordType.HEX, index + 412, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab3_Width, WordType.HEX, index + 413, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab4_Width, WordType.HEX, index + 414, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab5_Width, WordType.HEX, index + 415, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab6_Width, WordType.HEX, index + 416, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab7_Width, WordType.HEX, index + 417, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab8_Width, WordType.HEX, index + 418, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab9_Width, WordType.HEX, index + 419, 1));

            // 510~519
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance0_L, WordType.HEX, index + 510, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance0_H, WordType.HEX, index + 511, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance1_L, WordType.HEX, index + 512, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance1_H, WordType.HEX, index + 513, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance2_L, WordType.HEX, index + 514, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance2_H, WordType.HEX, index + 515, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance3_L, WordType.HEX, index + 516, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance3_H, WordType.HEX, index + 517, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance4_L, WordType.HEX, index + 518, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance4_H, WordType.HEX, index + 519, 1));

            // 610~619
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance5_L, WordType.HEX, index + 610, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance5_H, WordType.HEX, index + 611, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance6_L, WordType.HEX, index + 612, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance6_H, WordType.HEX, index + 613, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance7_L, WordType.HEX, index + 614, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance7_H, WordType.HEX, index + 615, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance8_L, WordType.HEX, index + 616, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance8_H, WordType.HEX, index + 617, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance9_L, WordType.HEX, index + 618, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance9_H, WordType.HEX, index + 619, 1));
            #endregion

            // 20~29
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PC_Command, WordType.DEC, index + 26, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PC_Status, WordType.DEC, index + 27, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PC_Move_REQ, WordType.DEC, index + 28, 1));

            // 120~129
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_AlignZ_ServoOnOff, WordType.DEC, index + 120, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_AlignZ_Status, WordType.DEC, index + 121, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Status, WordType.DEC, index + 126, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Command, WordType.DEC, index + 127, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Move_END, WordType.DEC, index + 128, 1));

            // 220~229
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_ManualMatch, WordType.DEC, index + 229, 1));

            // 320~339
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Cell_Id, WordType.HEX, index + 320, 10));

            // 130~139
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_AkkonZ_ServoOnOff, WordType.DEC, index + 130, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_AkkonZ_Status, WordType.DEC, index + 131, 1));

            // 50~59
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PC_AlignDataX_L, WordType.DEC, index + 50, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PC_AlignDataX_H, WordType.DEC, index + 51, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PC_AlignDataY_L, WordType.DEC, index + 52, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PC_AlignDataY_H, WordType.DEC, index + 53, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PC_AlignDataT_L, WordType.DEC, index + 54, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PC_AlignDataT_H, WordType.DEC, index + 55, 1));

            // 150~159
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Position_AxisY_L, WordType.DEC, index + 152, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Position_AxisY_H, WordType.DEC, index + 153, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Position_AxisT_L, WordType.DEC, index + 154, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Position_AxisT_H, WordType.DEC, index + 155, 1));

            // 250~259
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_AlignDataX_L, WordType.DEC, index + 250, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_AlignDataX_H, WordType.DEC, index + 251, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_OffsetDataX, WordType.DEC, index + 256, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_OffsetDataY, WordType.DEC, index + 257, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_OffsetDataT, WordType.DEC, index + 258, 1));
        }
    }
}
