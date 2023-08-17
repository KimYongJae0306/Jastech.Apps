using Emgu.CV;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Winform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Jastech.Apps.Winform
{
    public class LineCameraManager
    {
        #region 필드
        private static LineCameraManager _instance = null;

        private object _objLock { get; set; } = new object();
        #endregion

        #region 속성
        private List<LineCamera> CameraList = new List<LineCamera>();

        public bool IsLive { get; set; } = false;
        #endregion

        #region 이벤트
      
        #endregion

        #region 델리게이트
     
        #endregion

        #region 생성자
        public static LineCameraManager Instance()
        {
            if (_instance == null)
            {
                _instance = new LineCameraManager();
            }

            return _instance;
        }
        #endregion

        #region 메서드
        public void Initialize()
        {
            // Program 시작 후 처음 Mat을 할당하면 로딩 시간이 필요
            Mat initialImage = new Mat();
            initialImage.Dispose();

            var cameraCtrlHandler = DeviceManager.Instance().CameraHandler;

            if (cameraCtrlHandler == null)
                return;

            foreach (var camera in cameraCtrlHandler)
            {
                camera.ImageGrabbed += LinescanImageGrabbed;
                CameraList.Add(new LineCamera(camera));
            }
        }

        public void Release()
        {
        }

        public void Stop(string name)
        {
            GetLineCamera(name).StopGrab();
        }

        private Camera GetCamera(string name)
        {
            var cameraHandler = DeviceManager.Instance().CameraHandler;
            Camera camera = cameraHandler.Where(x => x.Name == name).FirstOrDefault();

            return camera;
        }
      
        public void LinescanImageGrabbed(Camera camera)
        {
            if (camera is CameraVirtual)
                return;

            lock (_objLock)
            {
                byte[] data = camera.GetGrabbedImage();
                if (data != null)
                {
                    if (GetLineCamera(camera.Name) is LineCamera lineCamera)
                        GetLineCamera(camera.Name).AddSubImage(data, camera.GrabCount);
                }
            }
            Thread.Sleep(0);
        }
       
        public bool IsGrabbing(string name)
        {
            return GetCamera(name).IsGrabbing() == false;
        }

        public LineCamera GetLineCamera(string cameraName)
        {
            return CameraList.Where(x => x.Camera.Name == cameraName.ToString()).FirstOrDefault();
        }

        public void Dispose()
        {
            var cameraCtrlHandler = DeviceManager.Instance().CameraHandler;

            if (cameraCtrlHandler == null)
                return;

            foreach (var camera in cameraCtrlHandler)
            {
                camera.ImageGrabbed -= LinescanImageGrabbed;
            }
        }
        #endregion
    }
}
