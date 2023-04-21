using Cognex.VisionPro;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Winform;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform
{
    public class AppsLineCameraManager
    {
        #region 필드
        private static AppsLineCameraManager _instance = null;

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
        public delegate void TeachingImageGrabbedDelete(Mat image);
        #endregion

        #region 생성자
        public static AppsLineCameraManager Instance()
        {
            if (_instance == null)
            {
                _instance = new AppsLineCameraManager();
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

            if (GetCamera(name) is Camera camera)
            {
                if (IsGrabbing)
                    StopGrab(name);

                IsGrabbing = true;

                camera.GrabMuti(AppConfig.Instance().GrabCount);
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
                lock(ScanImageList)
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

                    Mat grabImage = new Mat(camera.ImageHeight, camera.ImageWidth, MatType.CV_8UC1, data);
                    Mat rotatedMat = MatHelper.Transpose(grabImage);

                    ScanImageList.Add(rotatedMat);
                    grabImage.Dispose();

                    sw.Stop();
                    Console.WriteLine("Covert : " + sw.ElapsedMilliseconds.ToString() + "ms");

                    if (ScanImageList.Count == AppConfig.Instance().GrabCount)
                    {
                        camera.Stop();
                        IsGrabbing = false;
                        TeachingImageGrabbed?.Invoke(GetMergeImage());
                    }
                }
            }
        }

        
        public Mat GetMergeImage()
        {
            lock (_objLock)
            {
                if (ScanImageList.Count > 0)
                {
                    Mat mergeImage = new Mat();

                    Cv2.HConcat(ScanImageList, mergeImage);

                    return mergeImage;
                }
            }
            return null;
        }
        #endregion
    }
}
