using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using Jastech.Apps.Structure.Data;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Matrox;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraTeseter
{
    public partial class MainForm : Form
    {
        //public CogDisplay Cam0Display { get; set; }

        //public CogDisplay Cam1Display { get; set; }
        public DoubleBufferPictureBox pbxImageCam0 { get; set; }
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            pbxImageCam0 = new DoubleBufferPictureBox();
            pbxImageCam0.Dock = DockStyle.Fill;
            pnlCam0Display.Controls.Add(pbxImageCam0);

            //Cam1Display = new CogDisplay();
            //Cam1Display.Dock = DockStyle.Fill;
            //pnlCam1Display.Controls.Add(Cam1Display);
            Logger.Initialize(TestAppConfig.Instance().Path.Log);
            TestAppConfig.Instance().Initialize();
            DeviceManager.Instance().Initialize(TestAppConfig.Instance());

            TestAppCameraManager.Instance().Initialize();
            TestAppCameraManager.Instance().TeachingImageGrabbed += MainForm_TeachingImageGrabbed;

            Thread th = new Thread(Test);
            th.Start();
        }

        private void Test()
        {
            while(true)
            {
                lock(_lock)
                {
                    if(liveImageList.Count>0)
                    {
                        var image = liveImageList.Dequeue();
                        if (pbxImageCam0.Image != null)
                        {
                            pbxImageCam0.Image.Dispose();
                            pbxImageCam0.Image = null;
                        }
                        pbxImageCam0.Image = image;
                    }
                    
                }
                Thread.Sleep(100);
            }
        }

        static int ccCount = 0;
        Stopwatch sw1 = new Stopwatch();
        public object _lock = new object();
        Queue<Bitmap> liveImageList = new Queue<Bitmap>();
        private void MainForm_TeachingImageGrabbed(string name, OpenCvSharp.Mat image)
        {
            Console.WriteLine("main com");
            if (image == null)
                return;

            int size = image.Width * image.Height * image.Channels();
            byte[] dataArray = new byte[size];
            Marshal.Copy(image.Data, dataArray, 0, size);

            ColorFormat format = image.Channels() == 1 ? ColorFormat.Gray : ColorFormat.RGB24;

            var cogImage = CogImageHelper.CovertImage(dataArray, image.Width, image.Height, format) as CogImage8Grey;
            CogImageHelper.Save(cogImage, @"D:\123.bmp");
            //Console.WriteLine("convert");
            lock(_lock)
                liveImageList.Enqueue(OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image));

            if (name == CameraName.LinscanMIL0.ToString())
            {
                //if (pictureBox1.Image != null)
                //{
                //    pictureBox1.Image.Dispose();
                //    pictureBox1.Image = null;
                //}
                //pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image);
                //cogDisplay1.Image = null;
                //if(sw1.ElapsedMilliseconds >1000)
                //{
                //cogDisplay1.Image = cogImage;
                //Thread.Sleep(500);
                //    while (cogDisplay1.Image != cogImage)
                //    {
                //        Thread.Sleep(10);
                //    }

                //    sw1.Restart();
                //}

                Console.WriteLine("ccCount : " + ccCount);
                ccCount++;
            }
            else if(name == CameraName.LinscanMIL1.ToString())
            {
                //cogDisplay2.Image = cogImage;
            }
        }

        private void btnGrabStartCam0_Click(object sender, EventArgs e)
        {
            sw1.Restart();
            TestAppCameraManager.Instance().StartGrab(CameraName.LinscanMIL0);
        }

        private void btnGrabStopCam0_Click(object sender, EventArgs e)
        {
            TestAppCameraManager.Instance().StopGrab(CameraName.LinscanMIL0);
        }

        private void btnGrabStartCam1_Click(object sender, EventArgs e)
        {
            TestAppCameraManager.Instance().StartGrab(CameraName.LinscanMIL1);
        }

        private void btnGrabStopCam1_Click(object sender, EventArgs e)
        {
            TestAppCameraManager.Instance().StopGrab(CameraName.LinscanMIL1);
        }
    }
}
