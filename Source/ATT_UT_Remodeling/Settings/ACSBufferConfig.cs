using Jastech.Framework.Config;
using Jastech.Framework.Util.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATT_UT_Remodeling.Settings
{
    public class ACSBufferConfig
    {
        #region 필드
        private static ACSBufferConfig _instance = null;
        #endregion

        #region 속성
        [JsonProperty]
        public int CameraTrigger { get; set; }
        #endregion

        #region 이벤트
        public NewAcsBufferSettingDelegate NewAcsBufferSettingEventHandler;
        #endregion

        #region 델리게이트
        public delegate void NewAcsBufferSettingDelegate();
        #endregion

        #region 메서드
        public static ACSBufferConfig Instance()
        {
            if (_instance == null)
            {
                _instance = new ACSBufferConfig();
            }

            return _instance;
        }

        public void Initialize()
        {
            string dirPath = ConfigSet.Instance().Path.Config;
            string fullPath = Path.Combine(ConfigSet.Instance().Path.Config, "ACSBufferConfig.cfg");

            if (!File.Exists(fullPath))
            {
                NewAcsBufferSettingEventHandler?.Invoke();
                Save();
                return;
            }
            Load();
        }

        public void Save()
        {
            string dirPath = ConfigSet.Instance().Path.Config;
            string fullPath = Path.Combine(ConfigSet.Instance().Path.Config, "ACSBufferConfig.cfg");

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            JsonConvertHelper.Save(fullPath, this);
        }

        public void Load()
        {
            string dirPath = ConfigSet.Instance().Path.Config;
            string fullPath = Path.Combine(ConfigSet.Instance().Path.Config, "ACSBufferConfig.cfg");

            if (!File.Exists(fullPath))
            {
                Save();
                return;
            }

            JsonConvertHelper.LoadToExistingTarget<ACSBufferConfig>(fullPath, this);
        }
        #endregion
    }
}
