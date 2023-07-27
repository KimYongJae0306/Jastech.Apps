using Jastech.Apps.Structure.Data;
using Jastech.Framework.Imaging.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform
{
    public class AppsPreAlignResult
    {

        #region 필드
        private static AppsPreAlignResult _instance = null;
        #endregion

        #region 속성
        public DateTime StartInspTime { get; set; }

        public DateTime EndInspTime { get; set; }

        public string Cell_ID { get; set; } = "";

        public Judgement Judgement { get; set; } = Judgement.FAIL;

        public PreAlignResult Left { get; set; } = new PreAlignResult();

        public PreAlignResult Right { get; set; } = new PreAlignResult();

        public double OffsetX { get; private set; } = 0.0;

        public double OffsetY { get; private set; } = 0.0;

        public double OffsetT { get; private set; } = 0.0;
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        #endregion

        #region 메서드
        public static AppsPreAlignResult Instance()
        {
            if (_instance == null)
            {
                _instance = new AppsPreAlignResult();
            }

            return _instance;
        }

        public void SetPreAlignResult(double offsetX, double offsetY, double offsetT)
        {
            OffsetX = offsetX;
            OffsetY = offsetY;
            OffsetT = offsetT;
        }

        public void ClearResult()
        {
            Left?.Dispose();
            Right?.Dispose();
        }
        #endregion
    }
}
