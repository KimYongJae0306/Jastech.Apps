using Cognex.VisionPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform.Core
{
    public class ScanImage
    {
        public string UnitName { get; set; }

        public int TabIndex { get; set; }

        public ICogImage Image { get; set; } = null;
    }
}
