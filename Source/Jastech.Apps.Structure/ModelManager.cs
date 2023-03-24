using Jastech.Framework.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure
{
    public class ModelManager
    {

        #region 필드  
        private static ModelManager _instance = null;
        #endregion

        #region 속성
        public InspModel CurrentModel = null;
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public static ModelManager Instance()
        {
            if (_instance == null)
            {
                _instance = new ModelManager();
            }

            return _instance;
        }
        #endregion

        #region 메서드
        #endregion
    }
}
