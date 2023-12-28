using Jastech.Apps.Structure.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATT_UT_IPAD.Core.Data
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

        public string FinalHead { get; set; } = "";

        private Dictionary<int, TabInspResult> InspAkkonResultDic { get; set; } = new Dictionary<int, TabInspResult>();

        private Dictionary<int, TabInspResult> InspAlignResultDic { get; set; } = new Dictionary<int, TabInspResult>();
        #endregion

        #region 메서드
        public static AppsInspResult Instance()
        {
            if (_instance == null)
                _instance = new AppsInspResult();

            return _instance;
        }

        public void ClearResult()
        {
            Stopwatch sw = new Stopwatch();
            sw.Restart();

            foreach (var inspAkkonResult in InspAkkonResultDic)
                inspAkkonResult.Value.Dispose();

            foreach (var inspAlignResult in InspAlignResultDic)
                inspAlignResult.Value.Dispose();

            sw.Stop();
        }

        public void Dispose()
        {
            foreach (var inspAkkonResult in InspAkkonResultDic)
                inspAkkonResult.Value.Dispose();

            InspAkkonResultDic.Clear();

            foreach (var inspAlignResult in InspAlignResultDic)
                inspAlignResult.Value.Dispose();

            InspAlignResultDic.Clear();
        }

        public void AddAkkon(TabInspResult tabInspResult)
        {
            lock (InspAkkonResultDic)
            {
                int tabNo = tabInspResult.TabNo;

                if (InspAkkonResultDic.ContainsKey(tabNo))
                {
                    InspAkkonResultDic[tabNo].Dispose();
                    InspAkkonResultDic.Remove(tabNo);
                }

                InspAkkonResultDic.Add(tabNo, tabInspResult);
            }
        }

        public TabInspResult GetAkkon(int tabNo)
        {
            lock (InspAkkonResultDic)
            {
                if (InspAkkonResultDic.ContainsKey(tabNo))
                    return InspAkkonResultDic[tabNo];
            }
            return null;
        }

        public void AddAlign(TabInspResult tabInspResult)
        {
            lock (InspAlignResultDic)
            {
                int tabNo = tabInspResult.TabNo;
                if (InspAlignResultDic.ContainsKey(tabNo))
                {
                    InspAlignResultDic[tabNo].Dispose();
                    InspAlignResultDic.Remove(tabNo);
                }
                InspAlignResultDic.Add(tabNo, tabInspResult);
            }
        }

        public TabInspResult GetAlign(int tabNo)
        {
            lock (InspAlignResultDic)
            {
                if (InspAlignResultDic.ContainsKey(tabNo))
                    return InspAlignResultDic[tabNo];
            }
            return null;
        }
        #endregion
    }
}
