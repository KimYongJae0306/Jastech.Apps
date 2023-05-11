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

        public int SubImageWidth { get; set; }

        public int SubImageHeight { get; set; }
      
        public int StartIndex { get; set; }

        public int EndIndex { get; set; }

        public bool ExcuteMerge { get; set; }

        public bool IsInspection { get; set; }

        public int TotalGrabCount { get => Math.Abs(EndIndex - StartIndex); }

        private List<Mat> SubImageList { get; set; } = new List<Mat>();

        public TabScanImage(int tabNo, int startIndex, int endIndex, int subImageWidth, int subImageHeight)
        {
            TabNo = tabNo;
            StartIndex = startIndex;
            EndIndex = endIndex;
            SubImageWidth = subImageWidth;
            SubImageHeight = subImageHeight;

            Dispose();
        }

        public bool IsAddImageDone()
        {
            bool isDone = false;

            lock (_objLock)
                isDone = TotalGrabCount == SubImageList.Count() ? true : false;

            return isDone;
        }

        public void Dispose()
        {
            lock (_objLock)
            {
                for (int i = 0; i < SubImageList.Count(); i++)
                {
                    SubImageList[i].Dispose();
                    SubImageList[i] = null;
                }
                SubImageList.Clear();
            }
        }

        public void AddSubImage(Mat mat)
        {
             lock (_objLock)
                SubImageList.Add(mat);
        }

        public int GetImageCount()
        {
            return SubImageList.Count();
        }

        public Mat GetMergeImage()
        {
            Mat mergeImage = null;
            lock (_objLock)
            {
                if (SubImageList.Count > 0)
                {
                    mergeImage = new Mat();

                    CvInvoke.HConcat(SubImageList.ToArray(), mergeImage);
                }
            }
            return mergeImage;
        }
    }
}
