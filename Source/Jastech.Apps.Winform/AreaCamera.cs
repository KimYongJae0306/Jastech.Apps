﻿using Jastech.Framework.Device.Cameras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform
{
    public class AreaCamera
    {
        #region 필드
        #endregion

        #region 속성
        public Camera Camera { get; private set; } = null;
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public AreaCamera(Camera camera)
        {
            Camera = camera;
        }
        #endregion

        #region 메서드
        public void StartGrabContinous()
        {
            if (Camera == null)
                return;

            if (Camera.IsGrabbing())
                Camera.Stop();

            Camera.GrabContinous();
        }

        public void StopGrab()
        {
            if (Camera == null)
                return;

            if (Camera.IsGrabbing())
                Camera.Stop();
        }
        #endregion
    }
}