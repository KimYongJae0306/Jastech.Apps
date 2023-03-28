using Jastech.Apps.Structure.VisionTool;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Framework.Structure;
using Jastech.Framework.Util.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure
{
    public enum CameraName
    {
        LeftArea,
        RightArea,
        Linscan0,
    }

    public class AppsInspModel : InspModel
    {
        [JsonProperty]
        public List<CogPatternMatchingParam> PreAlignParams { get; set; } = new List<CogPatternMatchingParam>();

        public void SetPreAlignParams(List<CogPatternMatchingParam> matchingParam)
        {
            foreach (var prevParam in PreAlignParams)
                prevParam.Dispose();

            PreAlignParams = matchingParam.Select(x => x.DeepCopy()).ToList();
        }
    }
}
