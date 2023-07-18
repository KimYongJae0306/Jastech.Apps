using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform
{
    public class AppsStatus
    {
        public bool IsRunning { get; set; } = false;

        private static AppsStatus _instance = null;

        public bool IsInspRunnerFlagFromPlc { get; set; } = false;

        public bool IsPreAlignRunnerFlagFromPlc { get; set; } = false;

        public static AppsStatus Instance()
        {
            if (_instance == null)
            {
                _instance = new AppsStatus();
            }

            return _instance;
        }
    }
}
