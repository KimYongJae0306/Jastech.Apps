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
        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public int AnalogGain { get; set; } = 4;         // 공통

        [JsonProperty]
        public double DigitalGain { get; set; } = 8.0;        // TDI Mode

        [JsonProperty]
        public LightParameter LightParam { get; set; } = null;   // LineScan 용 조명 파라메터

        public LineCameraData DeepCopy()
        {
            LineCameraData lineCameraData = new LineCameraData();

            lineCameraData.Name = Name;
            lineCameraData.AnalogGain = AnalogGain;
            lineCameraData.DigitalGain = DigitalGain;
            lineCameraData.LightParam = LightParam?.DeepCopy();

            return lineCameraData;
        }
    }
}
