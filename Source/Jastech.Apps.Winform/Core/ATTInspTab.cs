using Cognex.VisionPro;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.VisionPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform.Core
{
    public class ATTInspTab
    {
        private object _lock = new object();

        public string CameraName { get; set; }

        public Queue<byte[]> DataQueue = new Queue<byte[]>();

        public Task InspTask { get; set; }

        public CancellationTokenSource CancelInspTask { get; set; }

        public Task TeachingGrabTask { get; set; }

        public CancellationTokenSource CancelTeachingGrabTask { get; set; }

        public TabScanBuffer TabScanBuffer { get; set; } = null;

        public List<Mat> SubImageList { get; set; } = new List<Mat>();

        public Mat MergeMatImage { get; set; } = null;

        public CogImage8Grey MergeCogImage { get; set; } = null;

        public bool IsAddStart { get; set; }

        public SomeThingDelegate InspectEvent;

        public SomeThingDelegate TeachingEvent;

        public delegate void SomeThingDelegate(ATTInspTab inspTab);

        public void Dispose()
        {
            StopInspTask();
            StopTeachingTask();

            for (int i = 0; i < SubImageList.Count(); i++)
            {
                SubImageList[i].Dispose();
                SubImageList[i] = null;
            }
            SubImageList.Clear();

            MergeMatImage?.Dispose();
            MergeMatImage = null;

            MergeCogImage?.Dispose();
            MergeCogImage = null;

            DataQueue.Clear();
            TabScanBuffer?.Dispose();
        }

        public void AddData(byte[] data)
        {
            lock (_lock)
                DataQueue.Enqueue(data);
        }

        public int GetDataCount()
        {
            lock (_lock)
                return DataQueue.Count();
        }

        public byte[] GetData()
        {
            lock (_lock)
            {
                if (DataQueue.Count > 0)
                    return DataQueue.Dequeue();
                else
                    return null;
            }
        }

        public void StartInspTask()
        {
            if (TabScanBuffer == null)
                return;

            if (InspTask != null)
                return;

            CancelInspTask = new CancellationTokenSource();
            InspTask = new Task(InspectionTask, CancelInspTask.Token);
            InspTask.Start();
        }

        public void StopInspTask()
        {
            if (InspTask == null)
                return;

            CancelInspTask.Cancel();
            //InspTask.Wait();
            InspTask = null;
        }

        private void InspectionTask()
        {
            while (true)
            {
                if (CancelInspTask.IsCancellationRequested)
                {
                    break;
                }
                if (TabScanBuffer.InspectionDone)
                    Thread.Sleep(1000); // CPU 점유율 낮추려고...
                else
                {
                    AddImage();
                    if (SubImageList.Count() == TabScanBuffer.TotalGrabCount)
                    {
                        MakeMergeImage();
                        InspectEvent?.Invoke(this);
                        TabScanBuffer.InspectionDone = true;

                        //if (CameraName.Contains("Align"))
                        //{

                        //    string name = string.Format(@"D:\Test\{0}.bmp", TabScanBuffer.TabNo);

                        //    MergeMatImage.Save(name);
                        //}

                    }

                    if (IsAddStart)
                        Thread.Sleep(0);
                    else
                        Thread.Sleep(50);
                }
            }
        }

        private void TeachingGrabMergeTask()
        {
            while (true)
            {
                if (CancelTeachingGrabTask.IsCancellationRequested)
                {
                    break;
                }
                if (TabScanBuffer.TeachingGrabDone)
                    Thread.Sleep(1000); // CPU 점유율 낮추려고...
                else
                {
                    AddImage();

                    if (SubImageList.Count() == TabScanBuffer.TotalGrabCount)
                    {
                        MakeMergeImage();
                        TeachingEvent?.Invoke(this);
                        TabScanBuffer.TeachingGrabDone = true;
                    }

                    if (IsAddStart)
                        Thread.Sleep(0);
                    else
                        Thread.Sleep(50);
                }
            }
        }

        public void StartTeacingTask()
        {
            if (TabScanBuffer == null)
                return;

            if (TeachingGrabTask != null)
                return;

            CancelTeachingGrabTask = new CancellationTokenSource();
            TeachingGrabTask = new Task(TeachingGrabMergeTask, CancelTeachingGrabTask.Token);
            TeachingGrabTask.Start();

            while(TeachingGrabTask.Status != TaskStatus.Running)
            {
                Thread.Sleep(10);
            }
        }

        public void StopTeachingTask()
        {
            if (TeachingGrabTask == null)
                return;

            CancelTeachingGrabTask.Cancel();
            TeachingGrabTask.Wait();
            TeachingGrabTask = null;
        }

        private void MakeMergeImage()
        {
            MakeMergeMatImage();
            MakeMergeCogImage();
        }

        private void AddImage()
        {
            if (TabScanBuffer.GetData() is byte[] data)
            {
                IsAddStart = true;

                Mat mat = MatHelper.ByteArrayToMat(data, TabScanBuffer.SubImageWidth, TabScanBuffer.SubImageHeight, 1);
                Mat rotatedMat = MatHelper.Transpose(mat);
                SubImageList.Add(rotatedMat);
                mat.Dispose();
            }

            if (GetDataCount() > 0)
                AddImage();
        }

        private void MakeMergeCogImage()
        {
            if (MergeCogImage == null)
            {
                MakeMergeMatImage();
                if (MergeMatImage != null)
                    MergeCogImage = ConvertCogGrayImage(MergeMatImage);
            }
        }

        private void MakeMergeMatImage()
        {
            if (MergeMatImage == null)
            {
                if (SubImageList.Count > 0)
                {
                    MergeMatImage = new Mat();
                    CvInvoke.HConcat(SubImageList.ToArray(), MergeMatImage);
                }
            }
        }

        private CogImage8Grey ConvertCogGrayImage(Mat mat)
        {
            if (mat == null)
                return null;

            int size = mat.Width * mat.Height * mat.NumberOfChannels;
            var cogImage = VisionProImageHelper.CovertImage(mat.DataPointer, mat.Width, mat.Height, mat.Step, ColorFormat.Gray) as CogImage8Grey;
            return cogImage;
        }

        public void SetVirtualImage(string fileName)
        {
            MergeMatImage = new Mat(fileName, ImreadModes.Grayscale);
            MergeCogImage = VisionProImageHelper.Load(fileName) as CogImage8Grey;
            InspectEvent?.Invoke(this);
            TabScanBuffer.TeachingGrabDone = true;
        }
    }
}
