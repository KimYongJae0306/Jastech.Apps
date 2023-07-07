using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Comm;
using Jastech.Framework.Config;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Device.LightCtrls;
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
                PlcControlManager.Instance().Initialize();

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
                spotLight.ChannelNameMap["Ch.Blue"] = 0;
                spotLight.ChannelNameMap["Ch.RedSpot"] = 1;
                spotLight.ChannelNameMap["Ch.White"] = 2;
                config.Add(spotLight);

                var rightLight = new VirtualLightCtrl("Ring24V", 6);
                rightLight.ChannelNameMap["Ch.RedRing"] = 0;
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
                //var camera1 = new CameraMil(CameraName.LinscanMIL0.ToString(), 4640, 1024, ColorFormat.Gray, SensorType.Line);
                //camera1.MilSystemType = MilSystemType.Rapixo;
                //camera1.TriggerMode = TriggerMode.Hardware;
                //camera1.TriggerSource = (int)MilCxpTriggerSource.Cxp;
                //camera1.TriggerSignalType = MilTriggerSignalType.TL_Trigger;
                //camera1.TriggerIoSourceType = MILIoSourceType.AUX_IO0;
                //camera1.DigitizerNum = 0;

                //camera1.DcfFile = CameraMil.GetDcfFile(CameraType.VT_6k35c_trigger);
                //config.Add(camera1);

                //var motion = new ACSMotion("Motion", 2, ACSConnectType.Ethernet);
                //motion.IpAddress = "10.0.0.100";
                //config.Add(motion);

                ////var light1 = new LvsLightCtrl("LvsLight12V", 6, new SerialPortComm("COM2", 9600), new LvsSerialParser());
                ////light1.ChannelNameMap["Ch.Blue"] = 0;
                ////light1.ChannelNameMap["Ch.RedSpot"] = 1;
                ////config.Add(light1);

                ////var light2 = new LvsLightCtrl("LvsLight24V", 6, new SerialPortComm("COM3", 9600), new LvsSerialParser());
                ////light2.ChannelNameMap["Ch.RedRing"] = 0;
                ////config.Add(light2);

                //var laf1 = new NuriOneLAFCtrl(LAFName.Akkon.ToString());
                //laf1.SerialPortComm = new SerialPortComm
                //{
                //    PortName = "COM4",
                //    BaudRate = 9600,
                //};
                //config.Add(laf1);

                ////var laf2 = new NuriOneLAFCtrl(LAFName.Akkon.ToString());
                ////laf2.SerialPortComm = new SerialPortComm
                ////{
                ////    PortName = "COM3",
                ////    BaudRate = 9600,
                ////};
                ////config.Add(laf2);
                ///
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
