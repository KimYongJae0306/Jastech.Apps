using Jastech.Apps.Structure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATT_UT_Remodeling.Core.Data
{
    public class AppsInspResult
    {
        #region 필드
        private static AppsInspResult _instance = null;
        #endregion

        #region 속성
        public DateTime StartInspTime { get; set; }

        public DateTime EndInspTime { get; set; }

        public string LastInspTime { get; set; }

        public string Cell_ID { get; set; } = "";

        public string FinalHead { get; set; } = string.Empty;

        private Dictionary<int, TabInspResult> InspResultDic { get; set; } = new Dictionary<int, TabInspResult>();
        #endregion

        #region 메서드
        public static AppsInspResult Instance()
        {
            if (_instance == null)
                _instance = new AppsInspResult();

            return _instance;
        }

        public void Dispose()
        {
            foreach (var inspResult in InspResultDic)
                inspResult.Value.Dispose();

            InspResultDic.Clear();
        }

        public void Add(TabInspResult tabInspResult)
        {
            lock (InspResultDic)
            {
                int tabNo = tabInspResult.TabNo;

                if (InspResultDic.ContainsKey(tabNo))
                {
                    InspResultDic[tabNo].Dispose();
                    InspResultDic.Remove(tabNo);
                }

                InspResultDic.Add(tabNo, tabInspResult);
            }
        }

        public TabInspResult Get(int tabNo)
        {
            lock (InspResultDic)
            {
                if (InspResultDic.ContainsKey(tabNo))
                    return InspResultDic[tabNo];
            }

            return null;
        }
        #endregion
    }
}
