using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Comm;
using Jastech.Framework.Config;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.Grabbers;
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
                AppsConfig.Instance().UnitCount = 1;

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
                // Akkon LineScanCamera
                var alignCamera = new CameraVirtual("AkkonCamera", 4640, 1024, ColorFormat.Gray, SensorType.Line);
                config.Add(alignCamera);

                // Align LineScanCamera
                var akkonCamera = new CameraVirtual("AlignCamera", 4640, 1024, ColorFormat.Gray, SensorType.Line);
                config.Add(akkonCamera);

                // Motion
                var motion = new VirtualMotion("VirtualMotion", 2);
                config.Add(motion);

                // Akkon LAF
                var akkonLaf = new VirtualLAFCtrl("Akkon");
                config.Add(akkonLaf);

                // Akkon LAF
                var alignLaf = new VirtualLAFCtrl("Align");
                config.Add(alignLaf);

                // Light1
                var spotLight = new VirtualLightCtrl("Spot", 6); // 12V
                spotLight.ChannelNameMap["Ch.White"] = 1; // channel 지정
                spotLight.ChannelNameMap["Ch.RedSpot"] = 2; // channel 지정
                spotLight.ChannelNameMap["Ch.Blue"] = 3; // channel 지정
                config.Add(spotLight);

                // Light2
                var ringLight = new VirtualLightCtrl("Ring", 6);  // 24V
                ringLight.ChannelNameMap["Ch.RedRing1"] = 1; // channel 지정
                ringLight.ChannelNameMap["Ch.RedRing2"] = 2; // channel 지정
                config.Add(ringLight);
            }
            else
            {
                // Akkon LineScanCamera
                var akkonCamera = new CameraMil("AkkonCamera", 4640, 1024, ColorFormat.Gray, SensorType.Line);
                akkonCamera.MilSystemType = MilSystemType.Rapixo;
                akkonCamera.TriggerMode = TriggerMode.Hardware;
                akkonCamera.TriggerSource = (int)MilCxpTriggerSource.Cxp;
                akkonCamera.TriggerSignalType = MilTriggerSignalType.TL_Trigger;
                akkonCamera.TriggerIoSourceType = MILIoSourceType.AUX_IO0;
                akkonCamera.DigitizerNum = 0;

                akkonCamera.DcfFile = CameraMil.GetDcfFile(CameraType.VT_6K3_5X_H160);
                config.Add(akkonCamera);

                // Align LineScanCamera
                var alignCamera = new CameraMil("AlignCamera", 4640, 1024, ColorFormat.Gray, SensorType.Line);
                alignCamera.MilSystemType = MilSystemType.Rapixo;
                alignCamera.TriggerMode = TriggerMode.Hardware;
                alignCamera.TriggerSource = (int)MilCxpTriggerSource.Cxp;
                alignCamera.TriggerSignalType = MilTriggerSignalType.TL_Trigger;
                alignCamera.TriggerIoSourceType = MILIoSourceType.AUX_IO0;
                alignCamera.DigitizerNum = 2;

                alignCamera.DcfFile = CameraMil.GetDcfFile(CameraType.VT_6K3_5X_H160);
                config.Add(alignCamera);

                // Motion
                var motion = new ACSMotion("Motion", 2, ACSConnectType.Ethernet);
                motion.IpAddress = "10.0.0.100";
                config.Add(motion);

                // Akkon LAF
                var akkonLaf = new NuriOneLAFCtrl("AkkonLaf");
                akkonLaf.SerialPortComm = new SerialPortComm("COM1", 9600);
                config.Add(akkonLaf);

                // Align LAF
                var alignLaf = new NuriOneLAFCtrl("AlignLaf");
                alignLaf.SerialPortComm = new SerialPortComm("COM2", 9600);
                config.Add(alignLaf);

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
            Logger.Error(ErrorType.Apps, message);
            System.Diagnostics.Trace.WriteLine(message);
            MessageBox.Show(message);
            Application.Exit(new System.ComponentModel.CancelEventArgs(false));
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = (Exception)e.ExceptionObject;
            string message = "CurrentDomain_UnhandledException " + exception.Message + " Source: " + exception.Source.ToString() + "StackTrack :" + exception.StackTrace.ToString();
            Logger.Error(ErrorType.Apps, message);

            System.Diagnostics.Trace.WriteLine(message);
            MessageBox.Show(message);
        }
    }
}
