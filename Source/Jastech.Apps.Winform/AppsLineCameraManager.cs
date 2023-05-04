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

        //public bool IsGrabbing { get; private set; } = false;

        public TDIOperationMode CurrentOperationMode = TDIOperationMode.Area;
        #endregion

        #region 이벤트
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
            InitGrabSettings(name);

            if (GetCamera(name) is Camera camera)
            {
                if (camera.IsGrabbing())
                    StopGrab(name);

                camera.GrabMulti(AppsConfig.Instance().GrabCount);
            }
        }

        public void StartGrab(CameraName name, float scanLength_mm)
        {
            if (GetCamera(name) is Camera camera)
            {
                if (camera.IsGrabbing())
                    StopGrab(name);

                ClearTabScanImage();
            
                float resolution_mm = (float)(camera.PixelResolution_um / camera.LensScale) / 1000;
                int totalScanSubImageCount = (int)Math.Ceiling(scanLength_mm / resolution_mm / camera.ImageHeight);

                AppsConfig.Instance().GrabCount = totalScanSubImageCount;

                TabScanImage scanImage = new TabScanImage(0, 0, totalScanSubImageCount);
                TabScanImageList.Add(scanImage);

                // LineScan Page에서 Line 모드 GrabStart 할 때 Height Set 해줘야함
                camera.GrabMulti(AppsConfig.Instance().GrabCount);
            }
        }

        public void StartGrabContinous(CameraName name)
        {
            //ClearScanImage();

            if (GetCamera(name) is CameraMil camera)
            {
                if (camera.IsGrabbing())
                    StopGrab(name);

                camera.SetOperationMode(TDIOperationMode.Area);
                camera.GrabMulti(AppsConfig.Instance().GrabCount);
            }
        }

        public void StopGrab(CameraName name)
        {
            if (GetCamera(name) is Camera camera)
                camera.Stop();
        }

        public void StopGrab()
        {
            var cameraHandler = DeviceManager.Instance().CameraHandler;
           
            foreach (var camera in cameraHandler)
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
            var camera = GetCamera(name);

            if (camera is CameraMil milCamera)
            {
                if (operationMode == TDIOperationMode.TDI)
                {
                    milCamera.SetTriggerMode(TriggerMode.Hardware);
                }
                else
                {
                    milCamera.SetTriggerMode(TriggerMode.Software);
                }
            }

            camera.SetOperationMode(operationMode);
        }

        public void ClearTabScanImage()
        {
            lock(TabScanImageList)
            {
                foreach (var scanImage in TabScanImageList)
                    scanImage.Dispose();

                TabScanImageList.Clear();
            }
            _grabCount = 0;
            StackTabNo = 0;
        }

        public void InitGrabSettings(CameraName cameraName)
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            MaterialInfo materialInfo = inspModel.MaterialInfo;

            int tabCount = inspModel.TabCount;
            if (inspModel == null)
                return;

            ClearTabScanImage();

            Camera camera = GetCamera(cameraName);
            float resolution_mm = (float)(camera.PixelResolution_um / camera.LensScale) / 1000; // ex) 3.5 um / 5 / 1000 = 0.0007mm
            int totalScanSubImageCount = (int)Math.Ceiling(materialInfo.PanelXSize_mm / resolution_mm / camera.ImageHeight); // ex) 500mm / 0.0007mm / 1024 pixel

            AppsConfig.Instance().GrabCount = totalScanSubImageCount;
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
                    Mat grabImage = MatHelper.ByteArrayToMat(data, camera.ImageWidth, camera.ImageHeight, 1);

                    if (CurrentOperationMode == TDIOperationMode.TDI)
                    {
                        //kyj : 최적화 생각해봐야함...
                        Mat rotatedMat = MatHelper.Transpose(grabImage);

                        if (TabScanImageList.Count > 0 && TabScanImageList.Count < StackTabNo)
                        {
                            if (GetTabScanImage(StackTabNo) is TabScanImage scanImage)
                            {
                                if (scanImage.StartIndex <= _grabCount && _grabCount <= scanImage.EndIndex)
                                    scanImage.AddImage(rotatedMat);

                                if (scanImage.IsAddImageDone())
                                {
                                    TabImageGrabCompletedEventHandler?.Invoke(scanImage);
                                    StackTabNo++;
                                }
                            }
                        }

                        _grabCount++;

                        if (_grabCount == AppsConfig.Instance().GrabCount)
                        {
                            camera.Stop();
                            GrabDoneEventHanlder?.Invoke(true);
                        }
                    }
                    else
                    {
                        if (camera is CameraMil cameraMil)
                            TeachingLiveImageGrabbed?.Invoke(grabImage);
                    }

                    grabImage.Dispose();
                }
            }
        }
       
        public bool IsGrabbing(CameraName name)
        {
            return GetCamera(name).IsGrabbing() == false;
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
