using Cognex.VisionPro;
using Emgu.CV;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.VisionPro;
using System;
using System.Collections.Generic;

namespace Jastech.Apps.Winform.Core
{
    public class TabScanBuffer
    {
        #region 필드
        private object _objLock = new object();
        #endregion

        #region 속성
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
        #endregion

        #region 생성자
        public TabScanBuffer(int tabNo, int startIndex, int endIndex, int subImageWidth, int subImageHeight)
        {
            TabNo = tabNo;
            StartIndex = startIndex;
            EndIndex = endIndex;
            SubImageWidth = subImageWidth;
            SubImageHeight = subImageHeight;
        }

        #endregion

        #region 메서드
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
            lock(_objLock)
                DataQueue.Clear();
        }

        private CogImage8Grey ConvertCogGrayImage(Mat mat)
        {
            if (mat == null)
                return null;

            int size = mat.Width * mat.Height * mat.NumberOfChannels;
            var cogImage = VisionProImageHelper.CovertImage(mat.DataPointer, mat.Width, mat.Height, mat.Step, ColorFormat.Gray) as CogImage8Grey;
            return cogImage;
        }
        #endregion
    }
}
