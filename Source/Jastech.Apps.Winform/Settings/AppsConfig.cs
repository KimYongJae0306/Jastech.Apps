using Jastech.Framework.Config;
using Jastech.Framework.Util.Helper;
using Newtonsoft.Json;
using System.IO;

namespace Jastech.Apps.Winform.Settings
{
    public class AppsConfig// : ConfigSet
    {
        #region 필드
        private static AppsConfig _instance = null;
        #endregion

        #region 속성
        [JsonProperty]
        public string ProgramType { get; set; } = "";

        [JsonProperty]
        public string MachineName { get; set; } = "";

        [JsonProperty]
        public int UnitCount { get; set; } = 1;

        [JsonProperty]
        public int TabMaxCount { get; set; } = 10;

        [JsonProperty]
        public PlcAddressInfo PlcAddressInfo { get; set; } = new PlcAddressInfo();

        [JsonProperty]
        public float CameraGap_mm { get; set; } = 1.0F;  //um

        [JsonProperty]
        public float PreAlignToleranceX { get; set; } = 0.0F;  //um

        [JsonProperty]
        public float PreAlignToleranceY { get; set; } = 0.0F;  //um

        [JsonProperty]
        public float PreAlignToleranceTheta { get; set; } = 0.0F;  //degree

        [JsonProperty]
        public bool EnablePreAlign { get; set; } = true;

        [JsonProperty]
        public bool EnableAlign { get; set; } = true;

        [JsonProperty]
        public bool EnableAkkon { get; set; } = true;

        [JsonProperty]
        public bool UseMaterialInfo { get; set; } = true;

        [JsonProperty]
        public int AkkonThreadCount { get; set; } = 8;

        [JsonProperty]
        public int AlignResultDailyCount { get; set; } = 100;

        [JsonProperty]
        public int AkkonResultDailyCount { get; set; } = 100;

        [JsonProperty]
        public double CapabilityUSL { get; set; } = 4;

        [JsonProperty]
        public double CapabilityLSL { get; set; } = -4;

        [JsonProperty]
        public double PerformanceUSL_Center { get; set; } = 4;

        [JsonProperty]
        public double PerformanceLSL_Center { get; set; } = -4;

        [JsonProperty]
        public double PerformanceUSL_Side { get; set; } = 9;

        [JsonProperty]
        public double PerformanceLSL_Side { get; set; } = -9;

        [JsonProperty]
        public bool EnableTest1 { get; set; } = false;

        [JsonProperty]
        public bool EnableTest2 { get; set; } = false;

        [JsonProperty]
        public bool EnableAkkonLeadResultLog{ get; set; } = false;
        #endregion

        #region 메서드
        public static AppsConfig Instance()
        {
            if (_instance == null)
            {
                _instance = new AppsConfig();
            }

            return _instance;
        }

        public void Initialize()
        {
            string dirPath = ConfigSet.Instance().Path.Config;
            string fullPath = Path.Combine(ConfigSet.Instance().Path.Config, "AppsConfig.cfg");

            if(!File.Exists(fullPath))
            {
                Save();
                return;
            }
            Load();
        }

        public void Save()
        {
            string dirPath = ConfigSet.Instance().Path.Config;
            string fullPath = Path.Combine(ConfigSet.Instance().Path.Config, "AppsConfig.cfg");

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            JsonConvertHelper.Save(fullPath, this);
        }

        public void Load()
        {
            string dirPath = ConfigSet.Instance().Path.Config;
            string fullPath = Path.Combine(ConfigSet.Instance().Path.Config, "AppsConfig.cfg");

            if (!File.Exists(fullPath))
            {
                Save();
                return;
            }

            JsonConvertHelper.LoadToExistingTarget<AppsConfig>(fullPath, this);
        }
        #endregion
    }

    public class PlcAddressInfo
    {
        [JsonProperty]
        public int CommonStart { get; set; }

        [JsonProperty]
        public int ResultStart { get; set; }

        [JsonProperty]
        public int ResultStart_Align { get; set; }

        [JsonProperty]
        public int ResultStart_Akkon { get; set; }

        [JsonProperty]
        public int ResultTabToTabInterval { get; set; }
    }

    public enum AkkonTeachingMode
    {
        Manual,
        Auto,
    }
}
