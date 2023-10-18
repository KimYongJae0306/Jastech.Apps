using Jastech.Apps.Structure.Parameters;
using Jastech.Apps.Winform.Core.Calibrations;
using Jastech.Framework.Device.LightCtrls;
using Jastech.Framework.Structure;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Jastech.Apps.Structure.Data
{
    public class Unit
    {
        #region 속성
        [JsonProperty]
        public string Name { get; set; } = "";

        [JsonProperty]
        public PreAlignData PreAlign { get; set; } = null;

        [JsonProperty]
        public LineCameraData CameraData { get; set; } = null;

        [JsonProperty]
        public LineCameraData AlignCamCameraData { get; set; } = null;

        [JsonProperty]
        public LightParameter LightParam { get; set; } = null;   // LineScan 용 조명 파라메터

        [JsonProperty]
        private List<Tab> TabList { get; set; } = new List<Tab>();

        [JsonProperty]
        private List<TeachingInfo> TeachingInfoList { get; set; } = new List<TeachingInfo>();
        #endregion

        #region 메서드
        public Unit DeepCopy()
        {
            // Cognex Tool 때문에 개별 DeepCopy 호출 해줘야함(Json DeepCopy 안됨)
            Unit unit = new Unit();
            unit.Name = Name;
            unit.PreAlign = PreAlign?.DeepCopy();
            unit.CameraData = CameraData?.DeepCopy();
            unit.AlignCamCameraData = AlignCamCameraData?.DeepCopy();
            unit.LightParam = LightParam?.DeepCopy();
            unit.TabList = TabList.Select(x => x.DeepCopy()).ToList();
            unit.TeachingInfoList = TeachingInfoList.Select(x => x.DeepCopy()).ToList();
            return unit;
        }

        public void Dispose()
        {
            PreAlign?.Dispose();
            foreach (var tab in TabList)
                tab.Dispose();
            TabList.Clear();
        }

        public TeachingInfo GetTeachingInfo(TeachingPosType type)
        {
            return TeachingInfoList.Where(x => x.Name == type.ToString()).FirstOrDefault();
        }

        public void AddTeachingInfo(TeachingInfo position)
        {
            TeachingInfoList.Add(position);
        }

        public List<TeachingInfo> GetTeachingInfoList()
        {
            return TeachingInfoList;
        }

        public void SetTeachingInfoList(List<TeachingInfo> teachingInfoList)
        {
            TeachingInfoList.Clear();
            TeachingInfoList.AddRange(teachingInfoList);
        }

        public Tab GetTab(int index)
        {
            return TabList.Where(x => x.Index == index).FirstOrDefault();
        }

        public Tab GetTab(string tabName)
        {
            return TabList.Where(x => x.Name == tabName).FirstOrDefault();
        }

        public void AddTab(Tab tab)
        {
            TabList.Add(tab);
        }

        public List<Tab> GetTabList()
        {
            return TabList;
        }

        public void SetTab(Tab tab)
        {
            var removeTab = GetTab(tab.Index);
            removeTab.Dispose();
            TabList.Remove(removeTab);
            TabList.Add(tab);
        }

        public void AddPreAlignParam(PreAlignParam preAlignParam)
        {
            if (PreAlign == null)
                PreAlign = new PreAlignData();

            PreAlign.AlignParamList.Add(preAlignParam);
        }

        public PreAlignParam GetPreAlignMark(MarkDirection direction, MarkName markName)
        {
            return PreAlign.AlignParamList.Where(x => x.Direction == direction && x.Name == markName).First();
        }

        public void SetCalibraionPram(CalibrationParam calibrationParam)
        {
            if (PreAlign == null)
                PreAlign = new PreAlignData();

            PreAlign.CalibrationParam = calibrationParam;
        }

        public CalibrationParam GetCalibrationMark()
        {
            return PreAlign.CalibrationParam;
        }

        public LineCameraData GetLineCameraData(string name)
        {
            if (CameraData != null)
            {
                if (CameraData.Name == name)
                    return CameraData;
            }

            if (AlignCamCameraData != null)
            {
                if (AlignCamCameraData.Name == name)
                    return AlignCamCameraData;
            }

            return null;
        }
        #endregion
    }
}
