namespace Jastech.Apps.Winform.Service
{
    public static class DailyInfoService
    {
        private static DailyInfo DailyInfo { get; set; } = new DailyInfo();

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
