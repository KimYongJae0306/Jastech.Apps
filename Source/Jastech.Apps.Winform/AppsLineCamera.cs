using Cognex.VisionPro;
using Emgu.CV;
using Emgu.CV.Reg;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
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

        private object _dataLock = new object();
        #endregion

        #region 속성
        public Camera Camera { get; private set; } = null;

        public bool IsLive { get; set; } = false;

        public int GrabCount { get; private set; } = 0;

        //public List<TabScanImage> TabScanImageList { get; private set; } = new List<TabScanImage>();
        public List<TabScanBuffer> TabScanBufferList { get; private set; } = new List<TabScanBuffer>();

        public Queue<byte[]> LiveDataQueue = new Queue<byte[]>();

        public Queue<byte[]> DataQueue = new Queue<byte[]>();

        public Task MergeTask { get; set; }

        public Task LiveTask { get; set; }

        public Task MainGrabTask { get; set; }

        public CancellationTokenSource CancelMergeTask { get; set; }

        public CancellationTokenSource CancelLiveTask { get; set; }

        public CancellationTokenSource CancelMainGrabTask { get; set; }
        #endregion

        #region 이벤트
        public event TeachingImageGrabbedDelegate TeachingLiveImageGrabbed;

        public event GrabDoneDelegate GrabDoneEventHanlder;

        public event GrabOnceDelegate GrabOnceEventHandler;
        #endregion

        #region 델리게이트
        public delegate void TeachingImageGrabbedDelegate(string cameraName, Mat image);

        public delegate void GrabDoneDelegate(string cameraName, bool isGrabDone);

        public delegate void GrabOnceDelegate(TabScanBuffer tabScanBuffer);
        #endregion

        #region 생성자
        public AppsLineCamera(Camera camera)
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
            //materialInfo.PanelXSize_mm = 270;
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
                    //tempPos += 10;
                }

                int startIndex = (int)(tempPos / resolution_mm / Camera.ImageHeight);

                double tabWidth = materialInfo.GetTabWidth(i);
                tempPos += tabWidth;
                int endIndex = (int)(tempPos / resolution_mm / Camera.ImageHeight);
                if(maxEndIndex <= endIndex)
                    maxEndIndex = endIndex;

                var temp = endIndex - startIndex;

                tempPos += materialInfo.GetTabToTabDistance(i);

                TabScanBuffer scanImage = new TabScanBuffer(i, startIndex, endIndex, Camera.ImageWidth, Camera.ImageHeight);
                lock(TabScanBufferList)
                    TabScanBufferList.Add(scanImage);
            }
            GrabCount = maxEndIndex;
        }

        public void StartGrab()
        {
            if (Camera == null)
                return;

            Camera.Stop();

            string error = "";
            bool ret = MoveTo(TeachingPosType.Stage1_Scan_Start, out error);
            Thread.Sleep(1000);

            Camera.GrabMulti(GrabCount);
     
            MoveTo(TeachingPosType.Stage1_Scan_End, out error);
        }

        public bool MoveTo(TeachingPosType teachingPos, out string error)
        {
            error = "";

            if (AppsConfig.Instance().Operation.VirtualMode)
                return true;

            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            var teachingInfo = inspModel.GetUnit(UnitName.Unit0).GetTeachingInfo(teachingPos);

            Axis axisX = AppsMotionManager.Instance().GetAxis(AxisHandlerName.Handler0, AxisName.X);

            var movingParamX = teachingInfo.GetMovingParam(AxisName.X.ToString());

            if (MoveAxis(teachingPos, axisX, movingParamX) == false)
            {
                error = string.Format("Move To Axis X TimeOut!({0})", movingParamX.MovingTimeOut.ToString());
                Logger.Write(LogType.Seq, error);
                return false;
            }
         
            string message = string.Format("Move Completed.(Teaching Pos : {0})", teachingPos.ToString());
            Logger.Write(LogType.Seq, message);

            return true;
        }

        private bool MoveAxis(TeachingPosType teachingPos, Axis axis, AxisMovingParam movingParam)
        {
            AppsMotionManager manager = AppsMotionManager.Instance();
            if (manager.IsAxisInPosition(UnitName.Unit0, teachingPos, axis) == false)
            {
                Stopwatch sw = new Stopwatch();
                sw.Restart();

                manager.MoveTo(UnitName.Unit0, teachingPos, axis);

                while (manager.IsAxisInPosition(UnitName.Unit0, teachingPos, axis) == false)
                {
                    if (sw.ElapsedMilliseconds >= movingParam.MovingTimeOut)
                    {
                        return false;
                    }
                    Thread.Sleep(10);
                }
            }
            Console.WriteLine("Dove Done.");
            return true;
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
                    GrabDoneEventHanlder?.Invoke(Camera.Name, true);
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

        //private TabScanImage GetTabScanImage(int tabNo)
        //{
        //    if (tabNo <= TabScanImageList.Count)
        //    {
        //        return TabScanImageList[tabNo];
        //    }
        //    return null;
        //}

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
            var cogImage = VisionProImageHelper.CovertImage(mat.DataPointer, mat.Width, mat.Height, format);
            return cogImage;
        }
        #endregion
    }
}
