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
        private List<Judgement> _judgementList = new List<Judgement>();

        private List<TabInspResult> _tabInspResult = new List<TabInspResult>();
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
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
                    if (item == Judgement.NG)
                        return true;
                }
            }

            return false;
        }

        public List<Judgement> GetNGList()
        {
            List<Judgement> ngList = new List<Judgement>();

            lock (_judgementList)
            {
                foreach (var item in _judgementList)
                {
                    if (item == Judgement.NG)
                        ngList.Add(item);
                }

                return ngList;
            }
        }

        public void AddJudgementList(Judgement judgement)
        {
            _judgementList.Add(judgement);
        }

        public void SetJudgementList(List<Judgement> judgementList)
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
