using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform
{
    public class AppsStatus
    {
        #region 필드
        private static AppsStatus _instance = null;
        #endregion

        #region 속성
        public bool IsRunning { get; set; } = false;

        public bool IsInspRunnerFlagFromPlc { get; set; } = false;

        public bool IsPreAlignRunnerFlagFromPlc { get; set; } = false;
        #endregion

        #region 메서드
        public static AppsStatus Instance()
        {
            if (_instance == null)
            {
                _instance = new AppsStatus();
            }

            return _instance;
        }
        #endregion
    }
}
