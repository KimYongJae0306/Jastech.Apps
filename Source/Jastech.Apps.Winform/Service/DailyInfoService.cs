namespace Jastech.Apps.Winform.Service
{
    public static class DailyInfoService
    {
        private static DailyInfo DailyInfo { get; set; } = new DailyInfo();

        public static void Save(string modelName)
        {
            DailyInfo.Save(modelName);
        }

        public static void Load(string modelName) 
        {
            DailyInfo.Load(modelName);
        }

        public static DailyInfo GetDailyInfo()
        {
            return DailyInfo;
        }

        public static void Reset()
        {
            if (DailyInfo != null)
                DailyInfo = null;

            DailyInfo = new DailyInfo();
        }
    }
}
