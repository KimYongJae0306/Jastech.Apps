using Emgu.CV;
using Emgu.CV.Reg;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Imaging.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Jastech.Framework.Device.Cameras.Camera;

namespace Jastech.Apps.Winform
{
    public class AppsLineCamera
    {

        #region 필드
        private int _curGrabCount { get; set; } = 0;

        private int _stackTabNo { get; set; } = 0;

        private object _lock = new object();
        #endregion

        #region 속성
        public Camera Camera { get; private set; } = null;

        public bool IsLive { get; set; } = false;

        public int GrabCount { get; private set; } = 0;

        public List<TabScanImage> TabScanImageList { get; private set; } = new List<TabScanImage>();

        public Queue<Mat> LiveMatQueue = new Queue<Mat>();

        public Thread LiveThread = null;

        public bool IsStopLiveUpdate = false;

        public Thread MergeThread = null;

        public bool isMergeThreadEnable = false;
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

        public void InitGrabSettings()
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

                tempPos += Math.Ceiling((materialInfo.GetTabToTabDistance(i) - materialInfo.Tab0Width_mm));

                int endIndex = (int)(tempPos / resolution_mm / Camera.ImageHeight);

                tempPos += materialInfo.Tab0Width_mm;

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
            int totalScanSubImageCount = (int)Math.Ceiling(scanLength_mm / resolution_mm / Camera.ImageHeight) + 2;
            totalScanSubImageCount = 55;
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

            lock(_lock)
            {
                foreach (var mat in LiveMatQueue)
                    mat.Dispose();
                LiveMatQueue.Clear();
            }

            Camera.Stop();
        }

        public void AddImage(Mat mat, int grabCount)
        {
            if (IsLive)
            {
                lock (_lock)
                {
                    LiveMatQueue.Enqueue(mat.Clone());
                }
            }
            else
            {
                Mat rotatedMat = MatHelper.Transpose(mat);

                if (TabScanImageList.Count > 0/* && TabScanImageList.Count < _stackTabNo*/)
                {
                    if (GetTabScanImage(_stackTabNo) is TabScanImage scanImage)
                    {
                        if (scanImage.StartIndex <= _curGrabCount && _curGrabCount <= scanImage.EndIndex)
                            scanImage.AddImage(rotatedMat, grabCount);

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

        public void UpdateMergeThread()
        {
            while (isMergeThreadEnable)
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
            MergeThread = null;
        }
        public void StartMergeTread()
        {
            isMergeThreadEnable = true;

            if(MergeThread == null)
            {
                MergeThread = new Thread(() => UpdateMergeThread());
                MergeThread.Start();
            }
         
        }
       
        public void StopMergeTread()
        {
            isMergeThreadEnable = false;
            Thread.Sleep(100);
        }

        public void StartUpdateLive()
        {
            IsStopLiveUpdate = false;
            if(LiveThread == null)
            {
                LiveThread = new Thread(() => UpdateLiveImage());
                LiveThread.Start();
            }
        }

        public void StopUpdateLive()
        {
            IsStopLiveUpdate = true;
            Thread.Sleep(100);
        }

        public void UpdateLiveImage()
        {
            while(IsStopLiveUpdate == false)
            {
                Mat mat = null;
                lock (_lock)
                {
                    if (LiveMatQueue.Count() > 0)
                        mat = LiveMatQueue.Dequeue();
                    if (mat != null)
                    {
                        TeachingLiveImageGrabbed?.Invoke(Camera.Name, mat);
                    }
                }
            }
            LiveThread = null;
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
