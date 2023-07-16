using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Comm;
using Jastech.Framework.Config;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Device.LightCtrls;
using Jastech.Framework.Device.LightCtrls.Lvs;
using Jastech.Framework.Device.LightCtrls.Lvs.Parser;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Device.Plcs.Melsec;
using Jastech.Framework.Device.Plcs.Melsec.Parsers;
using Jastech.Framework.Imaging;
using Jastech.Framework.Matrox;
using Jastech.Framework.Util.Helper;
using System;
using System.Threading;
using System.Windows.Forms;

namespace ATT_UT_IPAD
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool isRunning = false;
            Mutex mutex = new Mutex(true, "ATT", out isRunning);

            if(isRunning)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.ThreadException += Application_ThreadException;
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

                Logger.Initialize(ConfigSet.Instance().Path.Log);

                MilHelper.InitApplication();
                CameraMil.BufferPoolCount = 200;
                SystemHelper.StartChecker(@"D:\ATT_Memory_Test.txt");

                ConfigSet.Instance().PathConfigCreated += ConfigSet_PathConfigCreated;
                ConfigSet.Instance().OperationConfigCreated += ConfigSet_OperationConfigCreated;
                ConfigSet.Instance().MachineConfigCreated += ConfigSet_MachineConfigCreated;
                ConfigSet.Instance().Initialize();
                AppsConfig.Instance().Initialize();

                UserManager.Instance().Initialize();

                var mainForm = new MainForm();
                SystemManager.Instance().Initialize(mainForm);
                Application.Run(mainForm);
            }
            else
            {
                MessageBox.Show("The program already started.");
                Application.Exit();
            }
        }

        private static void ConfigSet_MachineConfigCreated(MachineConfig config)
        {
            if (ConfigSet.Instance().Operation.VirtualMode)
            {
                var alignCamera = new CameraVirtual("AlignCamera", 4640, 1024, ColorFormat.Gray, SensorType.Line);
                config.Add(alignCamera);

                var akkonCamera = new CameraVirtual("AkkonCamera", 4640, 1024, ColorFormat.Gray, SensorType.Line);
                config.Add(akkonCamera);

                var motion = new VirtualMotion("VirtualMotion", 2);
                config.Add(motion);

                var spotLight = new VirtualLightCtrl("Spot12V", 6);
                //spotLight.ChannelNameMap["Ch.Blue"] = 0;
                //spotLight.ChannelNameMap["Ch.RedSpot"] = 1;
                //spotLight.ChannelNameMap["Ch.White"] = 2;
                config.Add(spotLight);

                var rightLight = new VirtualLightCtrl("Ring24V", 6);
                //rightLight.ChannelNameMap["Ch.RedRing"] = 0;
                config.Add(rightLight);

                var alignLaf = new VirtualLAFCtrl("Align");
                config.Add(alignLaf);

                var akkonLaf = new VirtualLAFCtrl("Akkon");
                config.Add(akkonLaf);

                //Test 코드
                AppsConfig.Instance().PlcAddressInfo.CommonStart = 1000;
                var plc = new MelsecPlc("PLC", new SocketComm("192.168.125.1", 9011, SocketCommType.Udp), new MelsecBinaryParser());
                config.Add(plc);
            }
            else
            {
                // Motion
                var motion = new VirtualMotion("VirtualMotion", 2);
                config.Add(motion);


                // Light1
                var spotLight = new LvsLightCtrl("Spot", 6, new SerialPortComm("COM5", 19200), new LvsSerialParser()); // 12V
                spotLight.ChannelNameMap["Ch.White"] = 1; // channel 지정
                spotLight.ChannelNameMap["Ch.RedSpot"] = 2; // channel 지정
                spotLight.ChannelNameMap["Ch.Blue"] = 3; // channel 지정
                config.Add(spotLight);

                // Light2
                var ringLight = new LvsLightCtrl("Ring", 6, new SerialPortComm("COM4", 19200), new LvsSerialParser());  // 24V
                ringLight.ChannelNameMap["Ch.RedRing1"] = 1; // channel 지정
                ringLight.ChannelNameMap["Ch.RedRing2"] = 2; // channel 지정
                config.Add(ringLight);
            }
        }

        private static void ConfigSet_OperationConfigCreated(OperationConfig config)
        {
            if (MessageBox.Show("Do you want to Virtual Mode?", "Setup", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                config.VirtualMode = true;
            }
            else
            {
                config.VirtualMode = false;
            }
        }

        private static void ConfigSet_PathConfigCreated(PathConfig config)
        {
            config.CreateDirectory();
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            string message = "Application_ThreadException " + e.Exception.Message;
            Logger.Error(ErrorType.Apps, message, AppsStatus.Instance().CurrentTime);
            System.Diagnostics.Trace.WriteLine(message);
            MessageBox.Show(message);
            Application.Exit(new System.ComponentModel.CancelEventArgs(false));
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = (Exception)e.ExceptionObject;
            string message = "CurrentDomain_UnhandledException " + exception.Message + " Source: " + exception.Source.ToString() + "StackTrack :" + exception.StackTrace.ToString();
            Logger.Error(ErrorType.Apps, message, AppsStatus.Instance().CurrentTime);

            System.Diagnostics.Trace.WriteLine(message);
            MessageBox.Show(message);
        }
    }
}
