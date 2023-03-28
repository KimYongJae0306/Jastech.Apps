using Cognex.VisionPro;
using Jastech.Framework.Winform.VisionPro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform
{
    public class TeachingUIManager
    {
        #region 필드
        private static TeachingUIManager _instance = null;
        #endregion

        #region 속성
        public CogDisplayControl TeachingDisplay { get; set; } = null;
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        #endregion

        #region 메서드
        public static TeachingUIManager Instance()
        {
            if (_instance == null)
            {
                _instance = new TeachingUIManager();
            }

            return _instance;
        }
        #endregion

    }
}
