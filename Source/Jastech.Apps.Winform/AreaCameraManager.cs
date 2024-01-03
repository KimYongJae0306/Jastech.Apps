using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Winform;
using System.Collections.Generic;
using System.Linq;

namespace Jastech.Apps.Winform
{
    public class AreaCameraManager
    {
        #region 필드
        private static AreaCameraManager _instance = null;
        #endregion

        #region 속성
        public List<AreaCamera> CameraList = new List<AreaCamera>();
        #endregion

        #region 생성자
        public static AreaCameraManager Instance()
        {
            if (_instance == null)
            {
                _instance = new AreaCameraManager();
            }

            return _instance;
        }
        #endregion

        #region 메서드
        public void Initialize()
        {
            var cameraCtrlHandler = DeviceManager.Instance().CameraHandler;

            if (cameraCtrlHandler == null)
                return;

            foreach (var camera in cameraCtrlHandler)
                CameraList.Add(new AreaCamera(camera));
        }

        public void Release()
        {
        }

        public AreaCamera GetAppsCamera(string name)
        {
            return CameraList.Where(x => x.Camera.Name == name).FirstOrDefault();
        }

        public void Stop(string name)
        {
            GetAppsCamera(name).StopGrab();
        }

        private Camera GetCamera(string name)
        {
            var cameraHandler = DeviceManager.Instance().CameraHandler;
            Camera camera = cameraHandler.Where(x => x.Name == name).FirstOrDefault();

            return camera;
        }

        public bool IsGrabbing(string name)
        {
            return GetCamera(name).IsGrabbing() == false;
        }

        public AreaCamera GetAreaCamera(string cameraName)
        {
            return CameraList.Where(x => x.Camera.Name == cameraName).FirstOrDefault();
        }
        #endregion
    }
}
