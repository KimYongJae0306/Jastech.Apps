using Cognex.VisionPro;
using Emgu.CV;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Winform;
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

        private int StackTabNo { get; set; } = 0;
        #endregion

        #region 속성
        public List<TabScanImage> TabScanImageList { get; private set; } = new List<TabScanImage>();

        public bool IsGrabbing { get; private set; } = false;

        public TDIOperationMode CurrentOperationMode = TDIOperationMode.Area;
        #endregion

        #region 이벤트
        public event TeachingImageGrabbedDelegate TeachingImageGrabbed;

        public event TeachingImageGrabbedDelegate TeachingLiveImageGrabbed;

        public event TabImageGrabCompletedDelegate TabImageGrabCompletedEventHandler;

        public event GrabDoneDelegate GrabDoneEventHanlder;
        #endregion

        #region 델리게이트
        public delegate void TeachingImageGrabbedDelegate(Mat image);

        public delegate void GrabDoneDelegate(bool isGrabDone);

        public delegate void TabImageGrabCompletedDelegate(TabScanImage image);
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
            // Program 시작 후 처음 Mat을 할당하면 로딩 시간이 필요
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
            InitGrabSettings();

            if (GetCamera(name) is Camera camera)
            {
                if (IsGrabbing)
                    StopGrab(name);

                IsGrabbing = true;

                camera.GrabMulti(AppConfig.Instance().GrabCount);
            }
        }

        public void StartGrabContinous(CameraName name)
        {
            //ClearScanImage();

            if (GetCamera(name) is CameraMil camera)
            {
                if (IsGrabbing)
                    StopGrab(name);

                IsGrabbing = true;

                camera.SetOperationMode(TDIOperationMode.Area);
                camera.GrabMulti(AppConfig.Instance().GrabCount);
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
            Camera camera = cameraHandler.Where(x => x.Name == name.ToString()).First();

            return camera;
        }
        
        public void SetOperationMode(CameraName name, TDIOperationMode operationMode)
        {
            //var camera = GetCamera(name);

            //if(camera is CameraMil milCamera)
            //{
            //    if (operationMode == TDIOperationMode.TDI)
            //    {
            //        milCamera.SetTriggerMode(TriggerMode.Hardware);
            //    }
            //    else
            //    {
            //        milCamera.SetTriggerMode(TriggerMode.Software);
            //    }
            //}
          
            //camera.SetOperationMode(operationMode);
        }

        public void ClearTabScanImage()
        {
            lock(TabScanImageList)
            {
                foreach (var scanImage in TabScanImageList)
                    scanImage.Dispose();

                TabScanImageList.Clear();
            }
            //_grabCount = 0;
            //lock (_objLock)
            //{
            //    lock(ScanImageList)
            //    {
            //        for (int i = 0; i < ScanImageList.Count; i++)
            //        {
            //            ScanImageList[i]?.Dispose();
            //            ScanImageList[i] = null;
            //        }
            //        ScanImageList.Clear();
            //    }
            //}
        }

        public void InitGrabSettings()
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            MaterialInfo materialInfo = inspModel.MaterialInfo;

            int tabCount = inspModel.TabCount;
            if (inspModel == null)
                return;

            ClearTabScanImage();

            Camera camera = GetCamera(CameraName.LinscanMIL0);
            float resolution_mm = (float)(camera.PixelResolution_um / camera.LensScale) / 1000; // ex) 3.5 um / 5 / 1000 = 0.0007mm
            int totalScanSubImageCount = (int)Math.Ceiling(materialInfo.PanelXSize_mm / resolution_mm / camera.ImageHeight); // ex) 500mm / 0.0007mm / 1024 pixel

            AppConfig.Instance().GrabCount = totalScanSubImageCount;
            _grabCount = 0;
            StackTabNo = 0;

            double tempPos = 0.0;
            for (int i = 0; i < tabCount; i++)
            {
                if(i == 0)
                    tempPos += inspModel.MaterialInfo.PanelEdgeToFirst_mm;

                int startIndex = (int)(tempPos / resolution_mm / camera.ImageHeight);

                tempPos += Math.Ceiling((materialInfo.GetTabToTabDistance(i) - materialInfo.TabWidth_mm));

                int endIndex = (int)(tempPos / resolution_mm / camera.ImageHeight);

                tempPos += materialInfo.TabWidth_mm;

                TabScanImage scanImage = new TabScanImage(i, startIndex, endIndex);
                TabScanImageList.Add(scanImage);
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
                    if(CurrentOperationMode == TDIOperationMode.TDI)
                    {
                        //kyj : 최적화 생각해봐야함...
                        Mat grabImage = MatHelper.ByteArrayToMat(data, camera.ImageWidth, camera.ImageHeight, 1);
                        Mat rotatedMat = MatHelper.Transpose(grabImage);

                        grabImage.Dispose();

                        if(TabScanImageList.Count > 0)
                        {
                            if(GetTabScanImage(StackTabNo) is TabScanImage scanImage)
                            {
                                if(scanImage.StartIndex <= _grabCount && _grabCount <= scanImage.EndIndex)
                                    scanImage.AddImage(rotatedMat);

                                if (scanImage.IsAddImageDone())
                                {
                                    TabImageGrabCompletedEventHandler?.Invoke(scanImage);
                                    StackTabNo++;
                                }
                            }
                        }

                        _grabCount++;

                        if(_grabCount == AppConfig.Instance().GrabCount)
                        {
                            camera.Stop();
                            IsGrabbing = false;

                            GrabDoneEventHanlder?.Invoke(true);
                        }
                    }
                    else
                    {
                        if(camera is CameraMil cameraMil)
                        {
                            Mat grabImage = MatHelper.ByteArrayToMat(data, camera.ImageWidth, cameraMil.TDIStages, 1);  // 256 : TDI CAM AREA SIZE
                            TeachingLiveImageGrabbed?.Invoke(grabImage);
                        }
                    }
                }
            }
        }
       
        public bool IsGrabCompleted()
        {
            return true;
        }

        private TabScanImage GetTabScanImage(int tabNo)
        {
            if(tabNo < TabScanImageList.Count)
            {
                return TabScanImageList[tabNo];
            }
            return null;
        }
        #endregion
    }
}
