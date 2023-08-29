using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Config;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Util.Helper;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Jastech.Apps.Winform.Service
{
    public class DailyInfo
    {
        public string FileName { get; private set; } = "DailyInfo.cfg";

        [JsonProperty]
        public List<DailyData> DailyDataList { get; set; } = new List<DailyData>();

        public void AddDailyDataList(DailyData dailyData)
        {
            if (DailyDataList.Count >= AppsConfig.Instance().AlignResultDailyCount)
                DailyDataList.RemoveAt(0);

            DailyDataList.Add(dailyData);
        }

        public void Save(string modelName)
        {
            string dirPath = Path.Combine(ConfigSet.Instance().Path.Temp, modelName);

            if (Directory.Exists(dirPath) == false)
                Directory.CreateDirectory(dirPath);

            string filePath = Path.Combine(dirPath, FileName);
            JsonConvertHelper.Save(filePath, this);
        }

        public void Load(string modelName)
        {
            string filePath = Path.Combine(ConfigSet.Instance().Path.Temp, modelName, FileName);
            JsonConvertHelper.LoadToExistingTarget<DailyInfo>(filePath, this);
        }

        //public void Dispose()
        //{
        //    if (DailyDataList.Count > 0)
        //        DailyDataList.Clear();
        //}
    }

    public class DailyData
    {
        [JsonProperty]
        public List<AlignDailyInfo> AlignDailyInfoList { get; set; } = new List<AlignDailyInfo>();

        [JsonProperty]
        public List<AkkonDailyInfo> AkkonDailyInfoList { get; set; } = new List<AkkonDailyInfo>();

        public void AddAlignInfo(AlignDailyInfo alignDailyInfo)
        {
            AlignDailyInfoList.Add(alignDailyInfo);
        }

        public void ClearAlignInfo()
        {
            AlignDailyInfoList.Clear();
        }

        public void AddAkkonInfo(AkkonDailyInfo akkonDailyInfo)
        {
            AkkonDailyInfoList.Add(akkonDailyInfo);
        }

        public void ClearAkkonInfo()
        {
            AkkonDailyInfoList.Clear();
        }
    }

    public class AlignDailyInfo
    {
        [JsonProperty]
        public string Name { get; set; } =  "Align";

        [JsonProperty]
        public string InspectionTime { get; set; } = string.Empty;

        [JsonProperty]
        public string PanelID { get; set; } = string.Empty;

        [JsonProperty]
        public int TabNo { get; set; } = 0;

        [JsonProperty]
        public Judgement Judgement { get; set; } = Judgement.FAIL;

        [JsonProperty]
        public string AcfHead { get; set; } = "acf";//string.Empty;

        [JsonProperty]
        public string PreHead { get; set; } = "pre";//string.Empty;

        [JsonProperty]
        public string FinalHead { get; set; } = "final";//string.Empty;

        [JsonProperty]
        public float LX { get; set; } = 0.0f;

        [JsonProperty]
        public float LY { get; set; } = 0.0f;

        [JsonProperty]
        public float RX { get; set; } = 0.0f;

        [JsonProperty]
        public float RY { get; set; } = 0.0f;

        [JsonProperty]
        public float CX { get; set; } = 0.0f;
    }

    public class AkkonDailyInfo
    {
        [JsonProperty]
        public string Name { get; set; } = "Akkon";

        [JsonProperty]
        public string InspectionTime { get; set; } = string.Empty;

        [JsonProperty]
        public string PanelID { get; set; } = string.Empty;

        [JsonProperty]
        public int TabNo { get; set; } = 0;

        [JsonProperty]
        public Judgement Judgement { get; set; } = Judgement.FAIL;

        [JsonProperty]
        public int MinBlobCount { get; set; } = 0;

        [JsonProperty]
        public float MinLength { get; set; } = 0.0f;
    }
}
