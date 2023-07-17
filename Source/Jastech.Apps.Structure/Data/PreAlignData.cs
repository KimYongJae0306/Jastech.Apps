using Jastech.Apps.Structure.Parameters;
using Jastech.Apps.Winform.Core.Calibrations;
using Jastech.Framework.Device.LightCtrls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.Data
{
    public class PreAlignData
    {
        [JsonProperty]
        public List<PreAlignParam> AlignParamList { get; set; } = new List<PreAlignParam>();

        [JsonProperty]
        public CalibrationParam CalibrationParam { get; set; } = new CalibrationParam();

        [JsonProperty]
        public LightParameter LeftLightParam { get; set; } = null;

        [JsonProperty]
        public LightParameter RightLightParam { get; set; } = null;

        public PreAlignData DeepCopy()
        {
            PreAlignData preAlign = new PreAlignData();
            preAlign.AlignParamList = AlignParamList.Select(x => x.DeepCopy()).ToList();
            preAlign.CalibrationParam = CalibrationParam.DeepCopy();
            preAlign.LeftLightParam = LeftLightParam?.DeepCopy();
            preAlign.RightLightParam = RightLightParam?.DeepCopy();

            return preAlign;
        }

        public void Dispose()
        {
            foreach (var preAlignParam in AlignParamList)
                preAlignParam.Dispose();

            AlignParamList.Clear();
            CalibrationParam.Dispose();
        }
    }
}
