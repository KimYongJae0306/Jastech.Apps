using Cognex.VisionPro;
using Emgu.CV;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Core;
using Jastech.Framework.Config;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform
{
    public class LineCamera
    {
        #region 필드
        private int _curGrabCount { get; set; } = 0;

        private int _stackTabNo { get; set; } = 0;

        private object _lock = new object();

        private object _dataLock = new object();

        private Thread _trackingOnThread { get; set; } = null;

        private bool _isStopTrackingOn { get; set; } = false;
        #endregion

        #region 속성
        public Camera Camera { get; private set; } = null;

        public bool IsLive { get; set; } = false;

        public int GrabCount { get; private set; } = 0;

        public int DelayGrabIndex { get; private set; } = -1;

        public double LAFTrackingPos_mm { get; private set; } = -1;

        public List<TabScanBuffer> TabScanBufferList { get; private set; } = new List<TabScanBuffer>();

        public Queue<byte[]> LiveDataQueue = new Queue<byte[]>();

        public Queue<byte[]> DataQueue = new Queue<byte[]>();

        public Task LiveTask { get; set; }

        public CancellationTokenSource CancelLiveTask { get; set; }

        public int CameraGab { get; set; } = -1;

    
        #endregion

        #region 이벤트
        public event TeachingImageGrabbedDelegate TeachingLiveImageGrabbed;

        public event GrabDoneDelegate GrabDoneEventHandler;

        public event GrabOnceDelegate GrabOnceEventHandler;

        public event GrabDelayStartDelegate GrabDelayStartEventHandler;

        public event LAFTrackingOnOffDelegate LAFTrackingOnOffHandler;
        #endregion

        #region 델리게이트
        public delegate void TeachingImageGrabbedDelegate(string cameraName, Mat image);

        public delegate void GrabDoneDelegate(string cameraName, bool isGrabDone);

        public delegate void GrabOnceDelegate(TabScanBuffer tabScanBuffer);

        public delegate void GrabDelayStartDelegate(string cameraName);

        public delegate void LAFTrackingOnOffDelegate(bool isOn);
        #endregion

        #region 생성자
        public LineCamera(Camera camera)
        {
            Camera = camera;
        }
        #endregion

        #region 메서드
        public void ClearTabScanBuffer()
        {
            lock (TabScanBufferList)
            {
                foreach (var buffer in TabScanBufferList)
                    buffer.Dispose();

                TabScanBufferList.Clear();
            }
            _curGrabCount = 0;
            _stackTabNo = 0;
            DelayGrabIndex = -1;
            LAFTrackingPos_mm = -1;
        }

        public void InitGrabSettings()
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            MaterialInfo materialInfo = inspModel.MaterialInfo;

            int tabCount = inspModel.TabCount;
            if (inspModel == null)
                return;

            ClearTabScanBuffer();

            float resolution_mm = (float)(Camera.PixelResolution_um / Camera.LensScale) / 1000; // ex) 3.5 um / 5 / 1000 = 0.0007mm
            int totalScanSubImageCount = (int)Math.Ceiling(materialInfo.PanelXSize_mm / resolution_mm / Camera.ImageHeight); // ex) 500mm / 0.0007mm / 1024 pixel

            GrabCount = totalScanSubImageCount;
            _curGrabCount = 0;
            _stackTabNo = 0;

            double tempPos = 0.0;
            int maxEndIndex = 0;
            for (int i = 0; i < tabCount; i++)
            {
                if (i == 0)
                {
                    tempPos += inspModel.MaterialInfo.PanelEdgeToFirst_mm;
                    LAFTrackingPos_mm = tempPos - ((inspModel.MaterialInfo.PanelEdgeToFirst_mm / 2.0));
                    //LAFTrackingPos_mm = 1;
                    //LAFTrackingPos_mm = tempPos;
                }

                int startIndex = (int)(tempPos / resolution_mm / Camera.ImageHeight);

                
                double tabWidth = materialInfo.GetTabWidth(i);
                double tabLeftOffset = materialInfo.GetLeftOffset(i);
                double tabRightOffset = materialInfo.GetRightOffset(i);

                tempPos += tabWidth;

                double calcPos = tempPos;
                calcPos += tabLeftOffset;
                calcPos += tabRightOffset;

                int endIndex = (int)(calcPos / resolution_mm / Camera.ImageHeight);
                if(maxEndIndex <= endIndex)
                    maxEndIndex = endIndex;

                var temp = endIndex - startIndex;

                tempPos += materialInfo.GetTabToTabDistance(i, tabCount);

                TabScanBuffer scanImage = new TabScanBuffer(i, startIndex, endIndex, Camera.ImageWidth, Camera.ImageHeight);
                lock(TabScanBufferList)
                    TabScanBufferList.Add(scanImage);
            }

            GrabCount = maxEndIndex;
        }

        public void InitGrabSettings(float delayStart_um)
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            MaterialInfo materialInfo = inspModel.MaterialInfo;

            int tabCount = inspModel.TabCount;
            if (inspModel == null)
                return;

            ClearTabScanBuffer();

            float resolution_mm = (float)(Camera.PixelResolution_um / Camera.LensScale) / 1000; // ex) 3.5 um / 5 / 1000 = 0.0007mm
            float delayStart_mm = delayStart_um / 1000.0F;
            int totalScanSubImageCount = (int)Math.Ceiling(materialInfo.PanelXSize_mm / resolution_mm / Camera.ImageHeight); // ex) 500mm / 0.0007mm / 1024 pixel

            GrabCount = totalScanSubImageCount;

            _curGrabCount = 0;
            _stackTabNo = 0;

            double tempPos = 0.0;
            int maxEndIndex = 0;
            for (int i = 0; i < tabCount; i++)
            {
                if (i == 0)
                {
                    tempPos += delayStart_mm;
                    tempPos += inspModel.MaterialInfo.PanelEdgeToFirst_mm;

                    DelayGrabIndex = (int)(tempPos / resolution_mm / Camera.ImageHeight);
                    LAFTrackingPos_mm = tempPos - (inspModel.MaterialInfo.PanelEdgeToFirst_mm / 2.0);
                }

                int startIndex = (int)(tempPos / resolution_mm / Camera.ImageHeight);

                double tabWidth = materialInfo.GetTabWidth(i);
                double tabLeftOffset = materialInfo.GetLeftOffset(i);
                double tabRightOffset = materialInfo.GetRightOffset(i);

                tempPos += tabWidth;

                double calcPos = tempPos;
                calcPos += tabLeftOffset;
                calcPos += tabRightOffset;

                int endIndex = (int)(calcPos / resolution_mm / Camera.ImageHeight);
                if (maxEndIndex <= endIndex)
                    maxEndIndex = endIndex;

                var temp = endIndex - startIndex;

                tempPos += materialInfo.GetTabToTabDistance(i, tabCount);

                TabScanBuffer scanImage = new TabScanBuffer(i, startIndex, endIndex, Camera.ImageWidth, Camera.ImageHeight);
                lock (TabScanBufferList)
                    TabScanBufferList.Add(scanImage);
            }
            GrabCount = maxEndIndex;
        }

        public void StartLAFTrackingOnThread()
        {
            if (ModelManager.Instance().CurrentModel != null)
            {
                if (_trackingOnThread == null)
                {
                    _isStopTrackingOn = false;
                    _trackingOnThread = new Thread(LAFTrackingOn);
                    _trackingOnThread.Start();
                }
            }
        }

        public void StopLAFTrackingOnThread()
        {
            if (_trackingOnThread != null)
            {
                _isStopTrackingOn = true;
            }
        }

        private void LAFTrackingOn()
        {
            while(!_isStopTrackingOn)
            {
                if (LAFTrackingPos_mm != -1 /*&& appsDeviceMonitor.AxisStatus.IsMovingAxisX*/)
                {
                    var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
                    var scanStartPosX = inspModel.GetUnit(UnitName.Unit0).GetTeachingInfo(TeachingPosType.Stage1_Scan_Start).GetTargetPosition(AxisName.X);

                    var curPosition = GetCurrentAxisXPosition();
                    var lafOnPos = scanStartPosX + LAFTrackingPos_mm;

                    var value = (int)curPosition - (int)lafOnPos;
                    Debug.WriteLine("CurPosition : " + curPosition +" Laf : " + lafOnPos + "  " + value.ToString());
                    if (Math.Abs(value) < 1)
                    //if(Math.Abs(curPosition - 80) <= 10)
                    {
                        //var axis = MotionManager.Instance().GetAxis(AxisHandlerName.Handler0, AxisName.X);
                        //axis.StopMove();
                        LAFTrackingOnOffHandler?.Invoke(true);
                        break;
                    }
                }

                Thread.Sleep(1);
            }
            _trackingOnThread = null;
        }

        private double GetCurrentAxisXPosition()
        {
            Axis axisX = MotionManager.Instance().GetAxis(AxisHandlerName.Handler0, AxisName.X);
            return axisX.GetActualPosition();
        }

        public void StartGrab()
        {
            if (Camera == null)
                return;

            Camera.Stop();
            Thread.Sleep(100);

            Camera.GrabMulti(GrabCount);
            Thread.Sleep(100);
        }

        public void StartGrab(float scanLength_mm)
        {
            if (Camera == null)
                return;

            if (Camera.IsGrabbing())
                Camera.Stop();

            ClearTabScanBuffer();

            float resolution_mm = (float)(Camera.PixelResolution_um / Camera.LensScale) / 1000;
            int totalScanSubImageCount = (int)Math.Ceiling(scanLength_mm / resolution_mm / Camera.ImageHeight);

            TabScanBuffer buffer = new TabScanBuffer(0, 0, totalScanSubImageCount, Camera.ImageWidth, Camera.ImageHeight);
            lock(TabScanBufferList)
                TabScanBufferList.Add(buffer);

            GrabCount = totalScanSubImageCount;
            // LineScan Page에서 Line 모드 GrabStart 할 때 Height Set 해줘야함
            Console.WriteLine("Length Grab Count :" + GrabCount);
            Camera.GrabMulti(GrabCount);
        }

        public void StartGrabContinous()
        {
            if (Camera == null)
                return;

            if (Camera.IsGrabbing())
                Camera.Stop();

            //if(Camera is ICameraTDIavailable tdiCamera)
            //    tdiCamera.SetTDIOperationMode(TDIOperationMode.Area);

            Camera.GrabContinous();
        }

        public void StopGrab()
        {
            if (Camera == null)
                return;

            lock(_lock)
                LiveDataQueue.Clear();

            Camera.Stop();
            StopLAFTrackingOnThread();
        }

        public void AddSubImage(byte[] data, int grabCount)
        {
            if (IsLive)
            {
                lock (_lock)
                    LiveDataQueue.Enqueue(data);
            }
            else
            {
                TabScanBuffer tabScanBuffer = GetTabScanBuffer(_stackTabNo);
                if (tabScanBuffer == null)
                    return;

                if(DelayGrabIndex != -1 && DelayGrabIndex == _curGrabCount)
                    GrabDelayStartEventHandler?.Invoke(Camera.Name);

                if (tabScanBuffer.StartIndex <= _curGrabCount && _curGrabCount <= tabScanBuffer.EndIndex)
                {
                    tabScanBuffer.AddData(data);
                }

                if (tabScanBuffer.IsAddDataDone())
                {
                    _stackTabNo++;
                }

                if (_curGrabCount == GrabCount - 1)
                {
                    Camera.Stop();
                    LAFTrackingOnOffHandler?.Invoke(false);
                    GrabDoneEventHandler?.Invoke(Camera.Name, true);
                    GrabOnceEventHandler?.Invoke(tabScanBuffer);
                }

                _curGrabCount++;
            }   
        }

        private TabScanBuffer GetTabScanBuffer(int tabNo)
        {
            if(tabNo < TabScanBufferList.Count)
                return TabScanBufferList[tabNo];

            return null;
        }

        public void StartLiveTask()
        {
            if (LiveTask != null)
                return;

            CancelLiveTask = new CancellationTokenSource();
            LiveTask = new Task(UpdateLiveImage, CancelLiveTask.Token);
            LiveTask.Start();
        }

        public void StopLiveTask()
        {
            if (LiveTask == null)
                return;

            CancelLiveTask.Cancel();
            LiveTask.Wait();
            LiveTask = null;
        }

        public void UpdateLiveImage()
        {
            while (true)
            {
                if (CancelLiveTask.IsCancellationRequested)
                {
                    ClearTabScanBuffer();
                    break;
                }

                lock (_lock)
                {
                    if (LiveDataQueue.Count() > 0)
                    {
                       byte[] data = LiveDataQueue.Dequeue();
                        Mat mat = MatHelper.ByteArrayToMat(data, Camera.ImageWidth, Camera.ImageHeight, 1);

                        Mat rotatedMat = MatHelper.Transpose(mat);
  
                        if (mat != null)
                            TeachingLiveImageGrabbed?.Invoke(Camera.Name, rotatedMat);

                        mat.Dispose();
                    }
                }

                Thread.Sleep(0);
            }
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

        public ICogImage ConvertCogGrayImage(Mat mat)
        {
            if (mat == null)
                return null;

            int size = mat.Width * mat.Height * mat.NumberOfChannels;
            ColorFormat format = mat.NumberOfChannels == 1 ? ColorFormat.Gray : ColorFormat.RGB24;
            var cogImage = VisionProImageHelper.CovertImage(mat.DataPointer, mat.Width, mat.Height, mat.Step, format);
            return cogImage;
        }
        #endregion
    }
}
