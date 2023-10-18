using Jastech.Framework.Device.LightCtrls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.Data
{
    public class LineCameraData
    {
        #region 속성
        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public int AnalogGain { get; set; } = 4;         // 공통

        [JsonProperty]
        public double DigitalGain { get; set; } = 8.0;        // TDI Mode
        #endregion

        #region 메서드
        public LineCameraData DeepCopy()
        {
            LineCameraData lineCameraData = new LineCameraData();

            lineCameraData.Name = Name;
            lineCameraData.AnalogGain = AnalogGain;
            lineCameraData.DigitalGain = DigitalGain;

            return lineCameraData;
        }
        #endregion
    }
}
