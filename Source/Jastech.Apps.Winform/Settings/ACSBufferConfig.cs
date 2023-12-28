using Jastech.Framework.Config;
using Jastech.Framework.Util.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform.Settings
{
    public class ACSBufferConfig
    {
        #region 필드
        private static ACSBufferConfig _instance = null;
        #endregion

        #region 속성
        [JsonProperty]
        public int CameraTrigger { get; set; }

        [JsonProperty]
        public List<LafTriggerBuffer> LafTriggerBufferList { get; set; } = new List<LafTriggerBuffer>();

        public string IoEnableModeName { get; set; } = "IoEnableMode";                      // IoEnableMode(4)

        public String IoAddrName { get; set; } = "IoAddr";                                  // IoAddr(4)

        public string IoPositionUsagesName { get; set; } = "IoPositionUsages";              // IoPositionUsages(4)(10)

        public string LaserStartPositionsName { get; set; } = "LaserStartPositions";        // LaserStartPositions(4)(10)

        public string LaserEndPositionsName { get; set; } = "LaserEndPositions";            // LaserEndPositions(4)(10)
        #endregion

        #region 이벤트
        [JsonIgnore]
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

        public LafTriggerBuffer GetTriggerBuffer(string lafName)
        {
            return LafTriggerBufferList.Where(x => x.LafName == lafName).FirstOrDefault();
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

    public class LafTriggerBuffer
    {
        #region 속성
        public string LafName { get; set; }

        public int LafArrayIndex { get; set; }

        public int OutputBit { get; set; }

        public int BufferNumber { get; set; }
        #endregion
    }

    public enum IoEnableMode
    {
        Auto = 0,
        On = 1,
        Off = 2,
    }

    public class IoPositionData
    {
        #region 속성
        public double Start { get; set; }

        public double End { get; set; }
        #endregion
    }
}
