using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Newtonsoft.Json;

namespace Jastech.Apps.Winform.Core.Calibrations
{
    public class CalibrationParam
    {
        [JsonProperty]
        public string MarkName { get; set; } = "Calibration";

        [JsonProperty]
        public VisionProPatternMatchingParam InspParam { get; set; } = new VisionProPatternMatchingParam();

        public CalibrationParam DeepCopy()
        {
            CalibrationParam calibration = new CalibrationParam();

            calibration.MarkName = MarkName;

            if (InspParam != null)
                calibration.InspParam = InspParam.DeepCopy();

            return calibration;
        }

        public void Dispose()
        {
            InspParam.Dispose();
        }
    }
}
