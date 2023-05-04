using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.Data
{
    public class TabScanImage
    {
        private object _objLock = new object();

        public int TabNo { get; set; }

        public int StartIndex { get; set; }

        public int EndIndex { get; set; }

        public int TotalGrabCount { get => Math.Abs(EndIndex - StartIndex); }

        private List<Mat> ScanImageList { get; set; } = new List<Mat>();

        public TabScanImage(int tabNo, int startIndex, int endIndex)
        {
            TabNo = tabNo;
            StartIndex = startIndex;
            EndIndex = endIndex;

            Dispose();
        }

        public bool IsAddImageDone()
        {
            bool isDone = false;

            lock (_objLock)
                isDone = TotalGrabCount == ScanImageList.Count() ? true : false;

            return isDone;
        }

        public void Dispose()
        {
            lock (_objLock)
            {
                for (int i = 0; i < ScanImageList.Count(); i++)
                {
                    ScanImageList[i].Dispose();
                    ScanImageList[i] = null;
                }
                ScanImageList.Clear();
            }
        }

        public void AddImage(Mat mat)
        {
            lock (_objLock)
                ScanImageList.Add(mat);
        }

        public Mat GetMergeImage()
        {
            Mat mergeImage = null;
            lock (_objLock)
            {
                if (ScanImageList.Count > 0)
                {
                    mergeImage = new Mat();

                    CvInvoke.HConcat(ScanImageList.ToArray(), mergeImage);
                }
            }
            return mergeImage;
        }
    }
}
