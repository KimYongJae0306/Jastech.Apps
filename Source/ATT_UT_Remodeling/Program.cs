using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Core.Calibrations;
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

namespace ATT_UT_Remodeling
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

            if (isRunning)
            {
                Application.ThreadException += Application_ThreadException;
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Logger.Initialize(ConfigSet.Instance().Path.Log);

                MilHelper.InitApplication();
                CameraMil.BufferPoolCount = 200;
                SystemHelper.StartChecker(@"D:\ATT_Memory_Test.txt");

                ConfigSet.Instance().PathConfigCreated += ConfigSet_PathConfigCreated;
                ConfigSet.Instance().OperationConfigCreated += ConfigSet_OperationConfigCreated;
                ConfigSet.Instance().MachineConfigCreated += ConfigSet_MachineConfigCreated;
                ConfigSet.Instance().Initialize();
                AppsConfig.Instance().Initialize();
                CalibrationData.Instance().LoadCalibrationData();


                var mainForm = new MainForm();
                UserManager.Instance().Initialize();
                SystemManager.Instance().Initialize(mainForm);
                PlcControlManager.Instance().Initialize();

                SystemManager.Instance().AddSystemLogMessage("Start program.");
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
                //var areaScan = new CameraHIK("PreAlign", 1280, 1024, ColorFormat.Gray, SensorType.Area);
                //areaScan.SerialNo = "02750170835";
                //config.Add(areaScan);

                //var areaCamera = new CameraVirtual("PreAlign", 1280, 1024, ColorFormat.Gray, SensorType.Area);
                //config.Add(areaCamera);

                //var lineCamera = new CameraVirtual("LineCamera", 4640, 1024, ColorFormat.Gray, SensorType.Line);
                //config.Add(lineCamera);

                //var motion = new VirtualMotion("VirtualMotion", 3);
                //config.Add(motion);

                //var spotLight = new VirtualLightCtrl("Spot", 6); // 12V
                //spotLight.ChannelNameMap["Ch.White"] = 1; // channel 지정
                //spotLight.ChannelNameMap["Ch.RedSpot"] = 2; // channel 지정
                //spotLight.ChannelNameMap["Ch.Blue"] = 3; // channel 지정
                //config.Add(spotLight);

                //var ringLight = new VirtualLightCtrl("Ring", 6); // 24V
                //ringLight.ChannelNameMap["Ch.RedRing1"] = 1; // channel 지정
                //ringLight.ChannelNameMap["Ch.RedRing2"] = 2; // channel 지정
                //config.Add(ringLight);

                //var laf = new VirtualLAFCtrl("Laf");
                //config.Add(laf);
                // Light1
                var spotLight = new LvsLightCtrl("Spot", 6, new SerialPortComm("COM2", 19200), new LvsSerialParser()); // 12V
                spotLight.ChannelNameMap["Ch.White"] = 1; // channel 지정
                spotLight.ChannelNameMap["Ch.RedSpot"] = 2; // channel 지정
                spotLight.ChannelNameMap["Ch.Blue"] = 3; // channel 지정
                config.Add(spotLight);

                // Light2
                var ringLight = new LvsLightCtrl("Ring", 6, new SerialPortComm("COM3", 19200), new LvsSerialParser());  // 24V
                ringLight.ChannelNameMap["Ch.RedRing1"] = 1; // channel 지정
                ringLight.ChannelNameMap["Ch.RedRing2"] = 2; // channel 지정
                config.Add(ringLight);
            }
            else
            {
                var areaScan = new CameraHIK("PreAlign", 2592, 1944, ColorFormat.Gray, SensorType.Area);
                areaScan.SerialNo = "DA0228166";
                config.Add(areaScan);

                var lineCamera = new CameraMil("LineCamera", 6560, 1024, ColorFormat.Gray, SensorType.Line);
                lineCamera.MilSystemType = MilSystemType.Rapixo;
                lineCamera.TriggerMode = TriggerMode.Hardware;
                lineCamera.TriggerSource = (int)MilCxpTriggerSource.Cxp;
                lineCamera.TriggerSignalType = MilTriggerSignalType.TL_Trigger;
                lineCamera.TriggerIoSourceType = MILIoSourceType.AUX_IO0;
                lineCamera.DigitizerNum = 0;

                lineCamera.DcfFile = CameraMil.GetDcfFile(CameraType.VT_6k35c_trigger);
                config.Add(lineCamera);

                var motion = new ACSMotion("Motion", 2, ACSConnectType.Ethernet);
                motion.IpAddress = "10.0.0.100";
                config.Add(motion);


                var laf = new VirtualLAFCtrl("Laf");
                config.Add(laf);
                // Light1
                var spotLight = new LvsLightCtrl("Spot", 6, new SerialPortComm("COM2", 19200), new LvsSerialParser()); // 12V
                spotLight.ChannelNameMap["Ch.White"] = 1; // channel 지정
                spotLight.ChannelNameMap["Ch.RedSpot"] = 2; // channel 지정
                spotLight.ChannelNameMap["Ch.Blue"] = 3; // channel 지정
                config.Add(spotLight);

                // Light2
                var ringLight = new LvsLightCtrl("Ring", 6, new SerialPortComm("COM3", 19200), new LvsSerialParser());  // 24V
                ringLight.ChannelNameMap["Ch.RedRing1"] = 1; // channel 지정
                ringLight.ChannelNameMap["Ch.RedRing2"] = 2; // channel 지정
                config.Add(ringLight);

                AppsConfig.Instance().PlcAddressInfo.CommonStart = 104000;
                AppsConfig.Instance().PlcAddressInfo.ResultStart = 105000;
                AppsConfig.Instance().PlcAddressInfo.ResultStart_Align = 105220;
                AppsConfig.Instance().PlcAddressInfo.ResultTabToTabInterval = 200;
                AppsConfig.Instance().PlcAddressInfo.ResultStart_Akkon = 105230;
                AppsConfig.Instance().PlcAddressInfo.ResultStart_PreAlign = 105250;

                var plc = new MelsecPlc("PLC", new SocketComm("192.168.130.2", 9021, SocketCommType.Udp, 9031), new MelsecBinaryParser());
                config.Add(plc);
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
