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
        [JsonProperty]
        public string Name { get; set; } = "";

        [JsonProperty]
        public List<PreAlignParam> PreAlignParamList { get; set; } = new List<PreAlignParam>();

        [JsonProperty]
        public CalibrationParam CalibrationParam { get; set; } = new CalibrationParam();

        [JsonProperty]
        public List<LightParameter> LightParams { get; set; } = new List<LightParameter>();   // LineScan 용 조명 파라메터

        [JsonProperty]
        private List<Tab> TabList { get; set; } = new List<Tab>();

        [JsonProperty]
        public List<TeachingInfo> TeachingInfoList { get; set; } = new List<TeachingInfo>();

        public Unit DeepCopy()
        {
            // Cognex Tool 때문에 개별 DeepCopy 호출 해줘야함(Json DeepCopy 안됨)
            Unit unit = new Unit();
            unit.Name = Name;
            unit.PreAlignParamList = PreAlignParamList.Select(x => x.DeepCopy()).ToList();
            unit.CalibrationParam = CalibrationParam.DeepCopy();
            unit.LightParams = LightParams.Select(x => x.DeepCopy()).ToList();
            unit.TabList = TabList.Select(x => x.DeepCopy()).ToList();
            unit.TeachingInfoList = TeachingInfoList.Select(x => x.DeepCopy()).ToList();
            return unit;
        }

        public void Dispose()
        {
            foreach (var preAlignParam in PreAlignParamList)
                preAlignParam.Dispose();

            foreach (var tab in TabList)
                tab.Dispose();

            PreAlignParamList.Clear();
            CalibrationParam.Dispose();
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

        public void AddPreAlignParam(PreAlignParam preAlignParam)
        {
            PreAlignParamList.Add(preAlignParam);
        }

        public PreAlignParam GetPreAlignMark(MarkDirection direction, MarkName markName)
        {
            return PreAlignParamList.Where(x => x.Direction == direction && x.Name == markName).First();
        }

        public void SetCalibraionPram(CalibrationParam calibrationParam)
        {
            if (calibrationParam != null)
                CalibrationParam = calibrationParam;
        }

        public CalibrationParam GetCalibrationMark()
        {
            return CalibrationParam;
        }
    }
}
