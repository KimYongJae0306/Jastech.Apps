using Cognex.VisionPro;
using Jastech.Framework.Winform.VisionPro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform
{
    public class AppsTeachingUIManager
    {
        #region 필드
        private static AppsTeachingUIManager _instance = null;
        #endregion

        #region 속성
        private CogDisplayControl TeachingDisplay { get; set; } = null;

        public ICogImage PrevImage { get; set; } = null;
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        #endregion

        #region 메서드
        public static AppsTeachingUIManager Instance()
        {
            if (_instance == null)
            {
                _instance = new AppsTeachingUIManager();
            }

            return _instance;
        }

        public CogDisplayControl GetDisplay()
        {
            return TeachingDisplay;
        }

        public void SetDisplay(CogDisplayControl display)
        {
            TeachingDisplay = display;
        }

        public void SetImage(ICogImage image)
        {
            PrevImage = image;
            TeachingDisplay?.SetImage(image);
        }

        public ICogImage GetPrevImage()
        {
            return PrevImage;
        }
        #endregion

    }
}
