using Jastech.Apps.Structure.Data;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Imaging.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jastech.Framework.Winform;
using Jastech.Apps.Winform.Settings;
using Cognex.VisionPro;
using Emgu.CV;
using Jastech.Framework.Util.Helper;
using System.Runtime.InteropServices.WindowsRuntime;

namespace CameraTeseter
{
    public class TestAppCameraManager
    {
        #region 필드
        private static TestAppCameraManager _instance = null;

        private object _objLock { get; set; } = new object();

        private int _grabCount { get; set; } = 0;
        #endregion

        #region 속성
        public List<Mat> ScanImageList { get; private set; } = new List<Mat>();

        public bool IsGrabbing { get; private set; } = false;
        #endregion

        #region 이벤트
        public event TeachingImageGrabbedDelete TeachingImageGrabbed;
        #endregion

        #region 델리게이트
        public delegate void TeachingImageGrabbedDelete(string name, Mat image);
        #endregion

        #region 생성자
        public static TestAppCameraManager Instance()
        {
            if (_instance == null)
            {
                _instance = new TestAppCameraManager();
            }

            return _instance;
        }

        #endregion

        #region 메서드
        public void Initialize()
        {
            Mat initialImage = new Mat();
            initialImage.Dispose();
            var cameraCtrlHandler = DeviceManager.Instance().CameraHandler;

            if (cameraCtrlHandler == null)
                return;

            foreach (var camera in cameraCtrlHandler)
            {
                if (camera is CameraMil)
                    camera.ImageGrabbed += LinscanImageGrabbed;
            }
        }

        public void StartGrab(CameraName name)
        {
            ClearScanImage();

            if (GetCamera(name) is CameraMil camera)
            {
                if (IsGrabbing)
                    StopGrab(name);

                IsGrabbing = true;
                camera.SetOperationMode(TDIOperationMode.Area);
                camera.SetTriggerMode(TriggerMode.Software);
                camera.GrabMulti(AppsConfig.Instance().GrabCount);
            }
        }

        public void StopGrab(CameraName name)
        {
            IsGrabbing = false;

            if (GetCamera(name) is Camera camera)
                camera.Stop();
        }

        private Camera GetCamera(CameraName name)
        {
            var cameraHandler = DeviceManager.Instance().CameraHandler;
            Camera camera = cameraHandler.Get(name.ToString());

            return camera;
        }

        public void ClearScanImage()
        {
            _grabCount = 0;
            lock (_objLock)
            {
                lock (ScanImageList)
                {
                    for (int i = 0; i < ScanImageList.Count; i++)
                    {
                        ScanImageList[i]?.Dispose();
                        ScanImageList[i] = null;
                    }
                    ScanImageList.Clear();
                }
            }
        }
        static int count = 0;
        public void LinscanImageGrabbed(Camera camera)
        {
            if (camera is CameraVirtual)
                return;

            lock (_objLock)
            {
                byte[] data = camera.GetGrabbedImage();

                if (data != null)
                {
                    //kyj : 최적화 생각해봐야함...
                    //kyj : Grabber 단에서 돌리면 더 빠를수도???
                    Stopwatch sw = new Stopwatch();
                    sw.Restart();

                    Mat grabImage = MatHelper.ByteArrayToMat(data, camera.ImageWidth, camera.ImageHeight, 1);
                    Mat rotatedMat = MatHelper.Transpose(grabImage);
                    grabImage.Dispose();

                    sw.Stop();

                    TeachingImageGrabbed?.Invoke(camera.Name, rotatedMat);
                }
            }
        }

        public Mat Test()
        {
            var camera = GetCamera(CameraName.LinscanMIL0);

            lock (_objLock)
            {
                byte[] data = camera.GetGrabbedImage();

                if (data != null)
                {
                    //kyj : 최적화 생각해봐야함...
                    //kyj : Grabber 단에서 돌리면 더 빠를수도???
                    Mat grabImage = MatHelper.ByteArrayToMat(data, camera.ImageWidth, camera.ImageHeight, 1);

                    return grabImage;
                }
            }
            return null;
        }

        public Mat GetMergeImage()
        {
            lock (_objLock)
            {
                if (ScanImageList.Count > 0)
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Restart();

                    Mat mergeImage = new Mat();
                    
                    CvInvoke.HConcat(ScanImageList.ToArray(), mergeImage);

                    sw.Stop();

                    Logger.Debug(LogType.Imaging, "MerImage Image TactTime : " + sw.ElapsedMilliseconds.ToString() + "ms");

                    return mergeImage;
                }
            }
            return null;
        }
        #endregion
    }
}
