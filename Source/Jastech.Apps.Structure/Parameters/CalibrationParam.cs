using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.Parameters;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Jastech.Framework.Device.Motions.AxisMovingParam;
using System.Xml.Linq;

namespace Jastech.Apps.Winform.Core.Calibrations
{
    public class CalibrationParam
    {
        [JsonProperty]
        public CalibrationMarkName MarkName { get; set; } = CalibrationMarkName.Calibraion_1;

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
