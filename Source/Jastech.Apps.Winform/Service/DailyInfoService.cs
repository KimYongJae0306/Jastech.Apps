using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform.Service
{
    public static class DailyInfoService
    {
        private static DailyInfo DailyInfo { get; set; } = new DailyInfo();

        private static DailyData DailyData { get; set; } = new DailyData();

        public static void Save()
        {
            DailyInfo.Save();
        }

        public static void Load() 
        {
            DailyInfo.Load();
        }

        public static DailyInfo GetDailyInfo()
        {
            return DailyInfo;
        }
    }
}
