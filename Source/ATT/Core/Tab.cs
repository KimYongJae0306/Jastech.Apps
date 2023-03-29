using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATT.Core
{
    public class Tab
    {
        public string Name { get; set; } = "";

        public int TabNo { get; set; } = -1;

        public List<CogCaliperParam> FPCCaliperAlignXList = new List<CogCaliperParam>();
        public List<CogCaliperParam> FPCCaliperAlignYList = new List<CogCaliperParam>();

        public List<CogCaliperParam> PanelCaliperAlignXList = new List<CogCaliperParam>();
        public List<CogCaliperParam> PanelCaliperAlignYList = new List<CogCaliperParam>();
    }
}
