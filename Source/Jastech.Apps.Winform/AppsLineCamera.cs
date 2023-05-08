using Emgu.CV;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Imaging.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform
{
    public class AppsLineCamera
    {

        #region 필드
        private int _curGrabCount { get; set; } = 0;

        private int _stackTabNo { get; set; } = 0;
        #endregion

        #region 속성
        public Camera Camera { get; private set; } = null;

        public bool IsLive { get; set; } = false;

        public int GrabCount { get; private set; } = 0;

        public List<TabScanImage> TabScanImageList { get; private set; } = new List<TabScanImage>();
        #endregion

        #region 이벤트
        public event TeachingImageGrabbedDelegate TeachingLiveImageGrabbed;

        public event TabImageGrabCompletedDelegate TabImageGrabCompletedEventHandler;

        public event GrabDoneDelegate GrabDoneEventHanlder;
        #endregion

        #region 델리게이트
        public delegate void TeachingImageGrabbedDelegate(string cameraName, Mat image);

        public delegate void TabImageGrabCompletedDelegate(string cameraName, TabScanImage image);

        public delegate void GrabDoneDelegate(string cameraName, bool isGrabDone);
        #endregion

        #region 생성자
        public AppsLineCamera(Camera camera)
        {
            Camera = camera;
        }
        #endregion

        #region 메서드
        public void ClearTabScanImage()
        {
            lock (TabScanImageList)
            {
                foreach (var scanImage in TabScanImageList)
                    scanImage.Dispose();

                TabScanImageList.Clear();
            }
            _curGrabCount = 0;
            _stackTabNo = 0;
        }

        public void InitGrabSettings(CameraName cameraName)
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            MaterialInfo materialInfo = inspModel.MaterialInfo;

            int tabCount = inspModel.TabCount;
            if (inspModel == null)
                return;

            ClearTabScanImage();

            float resolution_mm = (float)(Camera.PixelResolution_um / Camera.LensScale) / 1000; // ex) 3.5 um / 5 / 1000 = 0.0007mm
            int totalScanSubImageCount = (int)Math.Ceiling(materialInfo.PanelXSize_mm / resolution_mm / Camera.ImageHeight); // ex) 500mm / 0.0007mm / 1024 pixel

            GrabCount = totalScanSubImageCount;
            _curGrabCount = 0;
            _stackTabNo = 0;

            double tempPos = 0.0;
            for (int i = 0; i < tabCount; i++)
            {
                if (i == 0)
                    tempPos += inspModel.MaterialInfo.PanelEdgeToFirst_mm;

                int startIndex = (int)(tempPos / resolution_mm / Camera.ImageHeight);

                tempPos += Math.Ceiling((materialInfo.GetTabToTabDistance(i) - materialInfo.TabWidth_mm));

                int endIndex = (int)(tempPos / resolution_mm / Camera.ImageHeight);

                tempPos += materialInfo.TabWidth_mm;

                TabScanImage scanImage = new TabScanImage(i, startIndex, endIndex);
                TabScanImageList.Add(scanImage);
            }
        }

        public void StartGrab(float scanLength_mm)
        {
            if (Camera == null)
                return;

            if (Camera.IsGrabbing())
                Camera.Stop();

            ClearTabScanImage();

            float resolution_mm = (float)(Camera.PixelResolution_um / Camera.LensScale) / 1000;
            int totalScanSubImageCount = (int)Math.Ceiling(scanLength_mm / resolution_mm / Camera.ImageHeight);

            TabScanImage scanImage = new TabScanImage(0, 0, totalScanSubImageCount);
            TabScanImageList.Add(scanImage);

            GrabCount = totalScanSubImageCount;
            // LineScan Page에서 Line 모드 GrabStart 할 때 Height Set 해줘야함
            Camera.GrabMulti(GrabCount);
        }

        public void StartGrabContinous()
        {
            if (Camera == null)
                return;

            if (Camera.IsGrabbing())
                Camera.Stop();

            if(Camera is ICameraTDIavailable tdiCamera)
                tdiCamera.SetTDIOperationMode(TDIOperationMode.Area);

            Camera.GrabMulti(-1); // -1 이거 디버깅 확인해봐야함
        }

        public void StopGrab()
        {
            if (Camera == null)
                return;
            Camera.Stop();
        }

        public void AddImage(Mat mat)
        {
            if (IsLive)
            {
                TeachingLiveImageGrabbed?.Invoke(Camera.Name, mat);
            }
            else
            {
                Mat rotatedMat = MatHelper.Transpose(mat);

                if (TabScanImageList.Count > 0 && TabScanImageList.Count < _stackTabNo)
                {
                    if (GetTabScanImage(_stackTabNo) is TabScanImage scanImage)
                    {
                        if (scanImage.StartIndex <= _curGrabCount && _curGrabCount <= scanImage.EndIndex)
                            scanImage.AddImage(mat);

                        if (scanImage.IsAddImageDone())
                        {
                            _stackTabNo++;
                            scanImage.ExcuteMerge = true;
                        }
                        _curGrabCount++;

                        if (_curGrabCount == GrabCount)
                        {
                            Camera.Stop();
                            GrabDoneEventHanlder?.Invoke(Camera.Name, true);
                        }
                    }
                }
            }
        }

        public void MergeThread()
        {
            while (true)
            {
                foreach (var scanImage in TabScanImageList)
                {
                    if (scanImage.ExcuteMerge)
                    {
                        TabImageGrabCompletedEventHandler?.Invoke(Camera.Name, scanImage);
                        scanImage.ExcuteMerge = false;
                    }
                }
                Thread.Sleep(50);
            }
        }

        private TabScanImage GetTabScanImage(int tabNo)
        {
            if (tabNo < TabScanImageList.Count)
            {
                return TabScanImageList[tabNo];
            }
            return null;
        }

        public void SetOperationMode(TDIOperationMode operationMode)
        {
            if (Camera == null)
                return;

            if (Camera is CameraMil milCamera)
            {
                if (operationMode == TDIOperationMode.TDI)
                    milCamera.SetTriggerMode(TriggerMode.Hardware);
                else
                    milCamera.SetTriggerMode(TriggerMode.Software);

                milCamera.SetTDIOperationMode(operationMode);
            }
        }

        public TDIOperationMode GetTDIOperationMode()
        {
            if (Camera is CameraMil milCamera)
            {
                return milCamera.TDIOperationMode;
            }
            return TDIOperationMode.TDI;
        }
        #endregion
    }
}
