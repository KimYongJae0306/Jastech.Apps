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
            // 210~219
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_PanelX_Size, WordType.DoubleWord, index + 210, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_MarkToMarkDistance, WordType.DoubleWord, index + 212, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_PanelLeftEdgeToTab1LeftEdgeDistance, WordType.DoubleWord, index + 214, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabCount, WordType.DEC, index + 216, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Axis_X_Speed, WordType.DEC, index + 217, 1));

            // 310~319
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Akkon_Count, WordType.DEC, index + 310, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Akkon_Length, WordType.DEC, index + 311, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Akkon_Strength, WordType.DEC, index + 312, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Akkon_Min_Size, WordType.DEC, index + 313, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Akkon_Max_Size, WordType.DEC, index + 314, 1));

            // 400~419
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab0_Offset_Left, WordType.DoubleWord, index + 400, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab1_Offset_Left, WordType.DoubleWord, index + 402, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab2_Offset_Left, WordType.DoubleWord, index + 404, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab3_Offset_Left, WordType.DoubleWord, index + 406, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab4_Offset_Left, WordType.DoubleWord, index + 408, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab5_Offset_Left, WordType.DoubleWord, index + 410, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab6_Offset_Left, WordType.DoubleWord, index + 412, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab7_Offset_Left, WordType.DoubleWord, index + 414, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab8_Offset_Left, WordType.DoubleWord, index + 416, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab9_Offset_Left, WordType.DoubleWord, index + 418, 2));

            // 500~519
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab0_Offset_Right, WordType.DoubleWord, index + 500, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab1_Offset_Right, WordType.DoubleWord, index + 502, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab2_Offset_Right, WordType.DoubleWord, index + 504, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab3_Offset_Right, WordType.DoubleWord, index + 506, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab4_Offset_Right, WordType.DoubleWord, index + 508, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab5_Offset_Right, WordType.DoubleWord, index + 510, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab6_Offset_Right, WordType.DoubleWord, index + 512, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab7_Offset_Right, WordType.DoubleWord, index + 514, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab8_Offset_Right, WordType.DoubleWord, index + 516, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab9_Offset_Right, WordType.DoubleWord, index + 518, 2));

            // 610~619
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab0_Width, WordType.DoubleWord, index + 600, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab1_Width, WordType.DoubleWord, index + 602, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab2_Width, WordType.DoubleWord, index + 604, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab3_Width, WordType.DoubleWord, index + 606, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab4_Width, WordType.DoubleWord, index + 608, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab5_Width, WordType.DoubleWord, index + 610, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab6_Width, WordType.DoubleWord, index + 612, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab7_Width, WordType.DoubleWord, index + 614, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab8_Width, WordType.DoubleWord, index + 616, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Tab9_Width, WordType.DoubleWord, index + 618, 2));

            // 710~719
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance0, WordType.DoubleWord, index + 700, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance1, WordType.DoubleWord, index + 702, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance2, WordType.DoubleWord, index + 704, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance3, WordType.DoubleWord, index + 706, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance4, WordType.DoubleWord, index + 708, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance5, WordType.DoubleWord, index + 710, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance6, WordType.DoubleWord, index + 712, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance7, WordType.DoubleWord, index + 714, 2));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_TabtoTab_Distance8, WordType.DoubleWord, index + 716, 2));
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
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PC_AlignDataX, WordType.DoubleWord, index + 50, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PC_AlignDataY, WordType.DoubleWord, index + 52, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PC_AlignDataT, WordType.DoubleWord, index + 54, 1));

            // 150~159
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Position_AxisY, WordType.DoubleWord, index + 152, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_Position_AxisT, WordType.DoubleWord, index + 154, 1));

            // 250~259
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_AlignDataX, WordType.DoubleWord, index + 250, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_OffsetDataX, WordType.DEC, index + 256, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_OffsetDataY, WordType.DEC, index + 257, 1));
            AddressMapList.Add(new PlcAddressMap(PlcCommonMap.PLC_OffsetDataT, WordType.DEC, index + 258, 1));
        }
    }
}
