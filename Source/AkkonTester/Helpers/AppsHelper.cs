using Cognex.VisionPro;
using Emgu.CV;
using Jastech.Framework.Algorithms.Akkon.Parameters;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.VisionPro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace AkkonTester.Helpers
{
    public static class AppsHelper
    {
        public static ICogImage ConvertCogGrayImage(Mat mat)
        {
            if (mat == null)
                return null;

            int size = mat.Width * mat.Height * mat.NumberOfChannels;
            ColorFormat format = mat.NumberOfChannels == 1 ? ColorFormat.Gray : ColorFormat.RGB24;
            var cogImage = VisionProImageHelper.CovertImage(mat.DataPointer, mat.Width, mat.Height, format);
            return cogImage;
        }

        public static ICogImage ConvertCogColorImage(Mat mat)
        {
            Mat matR = MatHelper.ColorChannelSprate(mat, MatHelper.ColorChannel.R);
            Mat matG = MatHelper.ColorChannelSprate(mat, MatHelper.ColorChannel.G);
            Mat matB = MatHelper.ColorChannelSprate(mat, MatHelper.ColorChannel.B);

            byte[] dataR = new byte[matR.Width * matR.Height];
            Marshal.Copy(matR.DataPointer, dataR, 0, matR.Width * matR.Height);

            byte[] dataG = new byte[matG.Width * matG.Height];
            Marshal.Copy(matG.DataPointer, dataG, 0, matG.Width * matG.Height);

            byte[] dataB = new byte[matB.Width * matB.Height];
            Marshal.Copy(matB.DataPointer, dataB, 0, matB.Width * matB.Height);

            var cogImage = VisionProImageHelper.CovertImage(dataR, dataG, dataB, matB.Width, matB.Height);

            matR.Dispose();
            matG.Dispose();
            matB.Dispose();

            return cogImage;
        }

        public static List<AkkonROI> ReadROI(string path, int offsetX = 0, int offsetY = 0)
        {
            List<AkkonROI> roiList = new List<AkkonROI>();

            string str = File.ReadAllText(path);
            string[] lines = str.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                AkkonROI roi = new AkkonROI();

                string[] values = line.Split(' ');

                roi.LeftTopX = Convert.ToDouble(values[0]) + offsetX;
                roi.LeftTopY = Convert.ToDouble(values[1]) + offsetY;

                roi.RightTopX = Convert.ToDouble(values[2]) + offsetX;
                roi.RightTopY = Convert.ToDouble(values[3]) + offsetY;

                roi.RightBottomX = Convert.ToDouble(values[4]) + offsetX;
                roi.RightBottomY = Convert.ToDouble(values[5]) + offsetY;

                roi.LeftBottomX = Convert.ToDouble(values[6]) + offsetX;
                roi.LeftBottomY = Convert.ToDouble(values[7]) + offsetY;

                roiList.Add(roi);
            }
            return roiList;
        }

    }
}
