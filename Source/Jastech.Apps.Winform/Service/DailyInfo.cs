using Emgu.CV.Dnn;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Util.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform.Service
{
    public class DailyInfo
    {
        public string AlignFileName { get; private set; } = "Align.cfg";

        public string AkkonFileName { get; private set; } = "Akkon.cfg";

        [JsonProperty]
        public List<DailyData> DailyDataList { get; set; } = new List<DailyData>();

        public void AddDailyData(DailyData dailyData)
        {
            if (DailyDataList.Count >= AppsConfig.Instance().Operation.AlignResultCount)
                DailyDataList.RemoveAt(0);

            DailyDataList.Add(dailyData);
        }

        public void Save()
        {
            string filePath = Path.Combine(AppsConfig.Instance().Path.Temp, AlignFileName);
            JsonConvertHelper.Save(filePath, this);
        }

        public void Load()
        {
            string filePath = Path.Combine(AppsConfig.Instance().Path.Temp, AlignFileName);
            JsonConvertHelper.LoadToExistingTarget<DailyInfo>(filePath, this);
        }
    }

    public class DailyData
    {
        [JsonProperty]
        public List<AlignDailyInfo> AlignDailyInfoList { get; set; } = new List<AlignDailyInfo>();

        [JsonProperty]
        public List<AkkonDailyInfo> AkkonDailyInfoList { get; set; } = new List<AkkonDailyInfo>();

        public void AddAlignInfo(AlignDailyInfo alignDailyInfo)
        {
            if (AlignDailyInfoList.Count >= AppsConfig.Instance().Operation.AlignResultCount)
                AlignDailyInfoList.RemoveAt(0);

            AlignDailyInfoList.Add(alignDailyInfo);

            //AlignDailyInfoList.Reverse();
        }

        public void AddAkkonInfo(AkkonDailyInfo akkonDailyInfo)
        {
            if (AkkonDailyInfoList.Count >= AppsConfig.Instance().Operation.AkkonResultCount)
                AkkonDailyInfoList.RemoveAt(0);

            AkkonDailyInfoList.Add(akkonDailyInfo);

            //AkkonDailyInfoList.Reverse();
        }
    }

    public class AlignDailyInfo
    {
        [JsonProperty]
        public string InspectionTime { get; set; } = string.Empty;

        [JsonProperty]
        public string PanelID { get; set; } = string.Empty;

        [JsonProperty]
        public int TabNo { get; set; } = 0;

        [JsonProperty]
        public Judgement Judgement { get; set; } = Judgement.OK;

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
        public string InspectionTime { get; set; } = string.Empty;

        [JsonProperty]
        public string PanelID { get; set; } = string.Empty;

        [JsonProperty]
        public int TabNo { get; set; } = 0;

        [JsonProperty]
        public Judgement Judgement { get; set; } = Judgement.OK;

        [JsonProperty]
        public int AvgBlobCount { get; set; } = 0;

        [JsonProperty]
        public float AvgLength { get; set; } = 0.0f;

        [JsonProperty]
        public float AvgStrength { get; set; } = 0.0f;

        [JsonProperty]
        public float AvgSTD { get; set; } = 0.0f;
    }
}
