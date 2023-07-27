using Jastech.Apps.Structure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform
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

        private Dictionary<int, TabInspResult> InspResultDic { get; set; } = new Dictionary<int, TabInspResult>();
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        #endregion

        #region 메서드
        public static AppsInspResult Instance()
        {
            if (_instance == null)
            {
                _instance = new AppsInspResult();
            }

            return _instance;
        }

        public void ClearResult()
        {
            foreach (var inspResult in InspResultDic)
            {
                inspResult.Value.Dispose();
            }
        }

        public void Disopose()
        {
            foreach (var inspResult in InspResultDic)
            {
                inspResult.Value.Dispose();
            }
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

        //public AppsInspResult DeepCopy()
        //{
        //    AppsInspResult result = new AppsInspResult();
        //    result.StartInspTime = StartInspTime;
        //    result.EndInspTime = EndInspTime;
        //    result.LastInspTime = LastInspTime;
        //    result.Cell_ID = Cell_ID;
        //    result.TabResultList = TabResultList.Select(x => x.DeepCopy()).ToList();

        //    return result;
        //}
        #endregion
    }
}
