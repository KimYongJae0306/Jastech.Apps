using Cognex.VisionPro;
using Emgu.CV;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.VisionPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform.Core
{
    public class TabScanBuffer
    {
        private object _objLock = new object();

        public int TabNo { get; set; }

        public int SubImageWidth { get; set; }

        public int SubImageHeight { get; set; }

        public int StartIndex { get; set; }

        public int EndIndex { get; set; }

        public int TotalGrabCount { get => Math.Abs(EndIndex - StartIndex); }

        public bool InspectionDone { get; set; }

        public bool TeachingGrabDone { get; set; }

        public int AddCount { get; set; } = 0;

        public Queue<byte[]> DataQueue = new Queue<byte[]>();

        public TabScanBuffer(int tabNo, int startIndex, int endIndex, int subImageWidth, int subImageHeight)
        {
            TabNo = tabNo;
            StartIndex = startIndex;
            EndIndex = endIndex;
            SubImageWidth = subImageWidth;
            SubImageHeight = subImageHeight;
        }

        public void AddData(byte[] data)
        {
            lock (_objLock)
                DataQueue.Enqueue(data);

            AddCount++;
        }

        public byte[] GetData()
        {
            lock (_objLock)
            {
                if (DataQueue.Count > 0)
                    return DataQueue.Dequeue();
                else
                    return null;
            }
        }

        public bool IsAddDataDone()
        {
            bool isDone = false;

            lock (_objLock)
                isDone = TotalGrabCount == AddCount ? true : false;

            return isDone;
        }

        public void Dispose()
        {
            //lock (_objLock)
            //{
            //    for (int i = 0; i < SubImageList.Count(); i++)
            //    {
            //        SubImageList[i].Dispose();
            //        SubImageList[i] = null;
            //    }
            //    SubImageList.Clear();
            //}
            //MergeMatImage?.Dispose();
            //MergeMatImage = null;

            //MergeCogImage?.Dispose();
            //MergeCogImage = null;
        }

        public void MakeMergeImage()
        {
            MakeMergeMatImage();
            MakeMergeCogImage();

           // ExcuteMerge = true;
        }

        public Mat GetMergeMatImage()
        {
            MakeMergeMatImage();
            //return MergeMatImage;
            return null;
        }

        private void MakeMergeCogImage()
        {
            //if(MergeCogImage == null)
            //{
            //    MakeMergeMatImage();
            //    if(MergeMatImage != null)
            //        MergeCogImage = ConvertCogGrayImage(MergeMatImage);
            //}
        }

        private void MakeMergeMatImage()
        {
            //if (MergeMatImage == null)
            //{
            //    lock (_objLock)
            //    {
            //        if (SubImageList.Count > 0)
            //        {
            //            MergeMatImage = new Mat();
            //            CvInvoke.HConcat(SubImageList.ToArray(), MergeMatImage);
            //        }
            //    }
            //}
        }

        private CogImage8Grey ConvertCogGrayImage(Mat mat)
        {
            if (mat == null)
                return null;

            int size = mat.Width * mat.Height * mat.NumberOfChannels;
            var cogImage = VisionProImageHelper.CovertImage(mat.DataPointer, mat.Width, mat.Height, ColorFormat.Gray) as CogImage8Grey;
            return cogImage;
        }
    }
}
