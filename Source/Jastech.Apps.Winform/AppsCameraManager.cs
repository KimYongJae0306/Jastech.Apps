using Cognex.VisionPro;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.VisionPro;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform
{
    public class AppsCameraManager
    {
        #region 필드
        private static AppsCameraManager _instance = null;

        private object _objLock { get; set; } = new object();
        #endregion

        #region 속성
        public List<Mat> ScanImageList { get; private set; } = new List<Mat>();
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public static AppsCameraManager Instance()
        {
            if (_instance == null)
            {
                _instance = new AppsCameraManager();
            }

            return _instance;
        }

        #endregion

        #region 메서드
        public void StartGrab()
        {
            ClearScanImage();
        }

        public void ClearScanImage()
        {
            lock(_objLock)
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
                    Mat grabImage = new Mat(camera.ImageWidth, camera.ImageHeight, MatType.CV_8UC1, data);
                    Mat rotatedMat = MatHelper.Rotate(grabImage, -90);

                    ScanImageList.Add(grabImage);
                    grabImage.Dispose();
                }
            }
        }

        public Mat GetMergeImage()
        {
            Mat mergeImage = new Mat();

            Cv2.HConcat(ScanImageList, mergeImage);

            return mergeImage;
        }
        #endregion
    }
}
