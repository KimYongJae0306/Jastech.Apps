using Cognex.VisionPro;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Framework.Comm;
using Jastech.Framework.Comm.Protocol;
using Jastech.Framework.Config;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.LAF;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Device.LightCtrls;
using Jastech.Framework.Device.LightCtrls.Lvs;
using Jastech.Framework.Device.LightCtrls.Lvs.Parser;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Imaging;
using Jastech.Framework.Users;
using Jastech.Framework.Util.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.Settings
{
    public class AppsConfig// : ConfigSet
    {
        #region 필드
        private static AppsConfig _instance = null;
        #endregion

        #region 속성
        [JsonProperty]
        public int TabMaxCount { get; set; } = 10;

        [JsonProperty]
        public PlcAddressInfo PlcAddressInfo { get; set; } = new PlcAddressInfo();

        [JsonProperty]
        public float DistanceFromPreAlignToLineScanX { get; set; } = 1.0F;  //um

        [JsonProperty]
        public float DistanceFromPreAlignToLineScanY { get; set; } = 1.0F;  //um

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
        public int AlignResultCount { get; set; } = 100;

        [JsonProperty]
        public int AkkonResultCount { get; set; } = 100;

        [JsonProperty]
        public bool UseNGDisplay { get; set; } = true;

        [JsonProperty]
        public int NGSendingCycle { get; set; } = 1;
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
        public int ResultStart_PreAlign { get; set; }

        [JsonProperty]
        public int ResultTabToTabInterval { get; set; }
    }

    public enum AkkonTeachingMode
    {
        Manual,
        Auto,
    }
}
