using Cognex.VisionPro;
using Emgu.CV;
using Jastech.Framework.Algorithms.Akkon.Parameters;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.VisionPro;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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
            var cogImage = CogImageHelper.CovertImage(mat.DataPointer, mat.Width, mat.Height, format);
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

            var cogImage = CogImageHelper.CovertImage(dataR, dataG, dataB, matB.Width, matB.Height);

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

                roi.LeftTop = new Point(Convert.ToInt32(values[0]) + offsetX, Convert.ToInt32(values[1]) + offsetY);
                roi.RightTop = new Point(Convert.ToInt32(values[2]) + offsetX, Convert.ToInt32(values[3]) + offsetY);
                roi.RightBottom = new Point(Convert.ToInt32(values[4]) + offsetX, Convert.ToInt32(values[5]) + offsetY);
                roi.LeftBottom = new Point(Convert.ToInt32(values[6]) + offsetX, Convert.ToInt32(values[7]) + offsetY);

                roiList.Add(roi);
            }
            return roiList;
        }

    }
}
