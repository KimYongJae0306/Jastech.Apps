using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using Emgu.CV;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.Grabbers;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Matrox;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform;
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
        public DoubleBufferPictureBox pbxImageCam0 { get; set; }

        public DoubleBufferPictureBox pbxImageCam1 { get; set; }

        public bool StopThread { get; set; } = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MilHelper.InitApplication();

            pbxImageCam0 = new DoubleBufferPictureBox();
            pbxImageCam0.Dock = DockStyle.Fill;
            pbxImageCam0.SizeMode = PictureBoxSizeMode.Zoom;
            pnlCam0Display.Controls.Add(pbxImageCam0);

            pbxImageCam1 = new DoubleBufferPictureBox();
            pbxImageCam1.Dock = DockStyle.Fill;
            pbxImageCam1.SizeMode = PictureBoxSizeMode.Zoom;
            pnlCam1Display.Controls.Add(pbxImageCam1);

            Logger.Initialize(TestAppConfig.Instance().Path.Log);
            TestAppConfig.Instance().Initialize();
            DeviceManager.Instance().Initialize(TestAppConfig.Instance());

            TestAppCameraManager.Instance().Initialize();
            TestAppCameraManager.Instance().TeachingImageGrabbed += MainForm_TeachingImageGrabbed;

            Thread th = new Thread(Cam0LiveUpdate);
            th.Start();

            Thread th2 = new Thread(Cam1LiveUpdate);
            th2.Start();
        }

        private void Cam0LiveUpdate()
        {
            while(StopThread == false)
            {
                lock(_cam0Lock)
                {
                    if(liveImageCam0List.Count > 0)
                    {
                        var image = liveImageCam0List.Dequeue();
                        UpdatePictureBox(image, pbxImageCam0);
                    }
                }
                Thread.Sleep(50);
            }
        }

        public delegate void UpdatePictureBoxDele(Bitmap bmp, PictureBox pictureBox);
        public void UpdatePictureBox(Bitmap bmp, PictureBox pictureBox)
        {
            if(this.InvokeRequired)
            {
                UpdatePictureBoxDele callback = UpdatePictureBox;
                BeginInvoke(callback, bmp, pictureBox);
                return;
            }

            if (pictureBox.Image != null)
            {
                pictureBox.Image.Dispose();
                pictureBox.Image = null;
            }
            pictureBox.Image = bmp;
        }

        private void Cam1LiveUpdate()
        {
            while (StopThread == false)
            {
                lock (_cam1Lock)
                {
                    if (liveImageCam1List.Count > 0)
                    {
                        var image = liveImageCam1List.Dequeue();
                        UpdatePictureBox(image, pbxImageCam1);
                    }

                }
                Thread.Sleep(50);
            }
        }

        static int ccCount = 0;
        Stopwatch sw1 = new Stopwatch();
        public object _cam0Lock = new object();
        public object _cam1Lock = new object();
        Queue<Bitmap> liveImageCam0List = new Queue<Bitmap>();
        Queue<Bitmap> liveImageCam1List = new Queue<Bitmap>();

        private void MainForm_TeachingImageGrabbed(string name, Mat image)
        {
            if (image == null)
                return;

            //if(ccCount % 50 != 0)
            //{
            //    ccCount++;
            //    return;
            //}
            ccCount = 1;
            Console.WriteLine("Update");
            //if (isGrabbing == false)
            //    return;

            if (name == CameraName.LinscanMIL0.ToString())
            {
                lock (_cam0Lock)
                    liveImageCam0List.Enqueue(image.ToBitmap());
            }
            else if(name == CameraName.LinscanMIL1.ToString())
            {
                lock (_cam1Lock)
                    liveImageCam1List.Enqueue(image.ToBitmap());
            }
        }

        private void btnGrabStartCam0_Click(object sender, EventArgs e)
        {
            TestAppCameraManager.Instance().StartGrab(CameraName.LinscanMIL0);
        }

        private void btnGrabStopCam0_Click(object sender, EventArgs e)
        {
            TestAppCameraManager.Instance().StopGrab(CameraName.LinscanMIL0);
            lock (_cam0Lock)
            {
                for (int i = 0; i < liveImageCam0List.Count; i++)
                {
                    var image = liveImageCam0List.Dequeue();
                    image.Dispose();
                }
            }
        }

        private void btnGrabStartCam1_Click(object sender, EventArgs e)
        {
            TestAppCameraManager.Instance().StartGrab(CameraName.LinscanMIL1);
        }

        private void btnGrabStopCam1_Click(object sender, EventArgs e)
        {
            TestAppCameraManager.Instance().StopGrab(CameraName.LinscanMIL1);
            lock (_cam1Lock)
            {
                for (int i = 0; i < liveImageCam1List.Count; i++)
                {
                    var image = liveImageCam1List.Dequeue();
                    image.Dispose();
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TestAppCameraManager.Instance().StopGrab(CameraName.LinscanMIL0);
            TestAppCameraManager.Instance().StopGrab(CameraName.LinscanMIL1);
            StopThread = true;
            Thread.Sleep(1000);
            DeviceManager.Instance().Release();
            GrabberMil.Release();
            MilHelper.FreeApplication();
        }

        bool isGrabbing = false;
        private void GrabTimer_Tick(object sender, EventArgs e)
        {
            //var mat = TestAppCameraManager.Instance().Test();
            //UpdatePictureBox(mat.ToBitmap(), pbxImageCam0);
            isGrabbing = false;
            lock (_cam0Lock)
            {
                for (int i = 0; i < liveImageCam0List.Count; i++)
                {
                    var image = liveImageCam0List.Dequeue();
                    image.Dispose();
                }
            }
            lock (_cam1Lock)
            {
                for (int i = 0; i < liveImageCam1List.Count; i++)
                {
                    var image = liveImageCam1List.Dequeue();
                    image.Dispose();
                }
            }

            isGrabbing = true;
            TestAppCameraManager.Instance().StartGrab(CameraName.LinscanMIL0);
            //TestAppCameraManager.Instance().StartGrab(CameraName.LinscanMIL1);
        }

        private void btnTimerStart_Click(object sender, EventArgs e)
        {
            isGrabbing = true;
            TestAppCameraManager.Instance().StartGrab(CameraName.LinscanMIL0);
            //TestAppCameraManager.Instance().StartGrab(CameraName.LinscanMIL1);

            GrabTimer.Start();
        }

        private void btnTimerStop_Click(object sender, EventArgs e)
        {
            GrabTimer.Stop();
            TestAppCameraManager.Instance().StopGrab(CameraName.LinscanMIL0);
            TestAppCameraManager.Instance().StopGrab(CameraName.LinscanMIL1);
            lock (_cam0Lock)
            {
                for (int i = 0; i < liveImageCam0List.Count; i++)
                {
                    var image = liveImageCam0List.Dequeue();
                    image.Dispose();
                }
            }
            lock (_cam1Lock)
            {
                for (int i = 0; i < liveImageCam1List.Count; i++)
                {
                    var image = liveImageCam1List.Dequeue();
                    image.Dispose();
                }
            }
        }
    }
}
