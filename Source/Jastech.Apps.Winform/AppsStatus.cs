using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform
{
    public class AppsStatus
    {
        public DateTime CurrentTime { get; set; } = new DateTime();

        public bool IsRunning { get; set; } = false;

        private static AppsStatus _instance = null;

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
