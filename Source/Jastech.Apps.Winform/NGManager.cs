using Jastech.Apps.Structure.Data;
using Jastech.Framework.Imaging.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform
{
    public class NGManager
    {
        #region 필드
        private static NGManager _instance = null;
        #endregion

        #region 속성
        private List<TabJudgement> _judgementList = new List<TabJudgement>();

        private List<TabInspResult> _tabInspResult = new List<TabInspResult>();
        #endregion

        #region 메서드
        public static NGManager Instance()
        {
            if (_instance == null)
                _instance = new NGManager();

            return _instance;
        }

        public void Clear()
        {
            _judgementList.Clear();
        }

        public bool IsExistNG()
        {
            lock (_judgementList)
            {
                foreach (var item in _judgementList)
                {
                    if (item == TabJudgement.NG || item == TabJudgement.Mark_NG)
                        return true;
                }
            }

            return false;
        }

        public List<TabJudgement> GetNGList()
        {
            List<TabJudgement> ngList = new List<TabJudgement>();

            lock (_judgementList)
            {
                foreach (var item in _judgementList)
                {
                    if (item == TabJudgement.NG)
                        ngList.Add(item);
                }

                return ngList;
            }
        }

        public void AddJudgementList(TabJudgement judgement)
        {
            _judgementList.Add(judgement);
        }

        public void SetJudgementList(List<TabJudgement> judgementList)
        {
            if (_judgementList.Count >= 0)
                _judgementList.Clear();

            _judgementList.AddRange(judgementList);
        }

        public void AddTabInspResult(TabInspResult tabInspResult)
        {
            _tabInspResult.Add(tabInspResult);
        }

        public void ClearTabInspResult()
        {
            _tabInspResult.Clear();
        }
        #endregion
    }
}
