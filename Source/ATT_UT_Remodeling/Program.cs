using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Comm;
using Jastech.Framework.Config;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Device.LightCtrls;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Device.Plcs.Melsec.Parsers;
using Jastech.Framework.Device.Plcs.Melsec;
using Jastech.Framework.Imaging;
using Jastech.Framework.Matrox;
using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.ThreadException += Application_ThreadException;
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

                MilHelper.InitApplication();
                CameraMil.BufferPoolCount = 200;
                SystemHelper.StartChecker(@"D:\ATT_Memory_Test.txt");

                ConfigSet.Instance().PathConfigCreated += ConfigSet_PathConfigCreated;
                ConfigSet.Instance().OperationConfigCreated += ConfigSet_OperationConfigCreated;
                ConfigSet.Instance().MachineConfigCreated += ConfigSet_MachineConfigCreated;
                ConfigSet.Instance().Initialize();
                AppsConfig.Instance().Initialize();

                UserManager.Instance().Initialize();

                Logger.Initialize(ConfigSet.Instance().Path.Log);

                var mainForm = new MainForm();

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

                var areaCamera = new CameraVirtual("PreAlign", 1280, 1024, ColorFormat.Gray, SensorType.Area);
                config.Add(areaCamera);

                var lineCamera = new CameraVirtual("LineCamera", 4640, 1024, ColorFormat.Gray, SensorType.Line);
                config.Add(lineCamera);

                var motion = new VirtualMotion("VirtualMotion", 2);
                config.Add(motion);

                var inpectionLight = new VirtualLightCtrl("LvsLight24V", 6);
                inpectionLight.ChannelNameMap["Ch.RedRing"] = 0;
                config.Add(inpectionLight);

                var laf = new VirtualLAFCtrl("Laf");
                config.Add(laf);

                //Test 코드
                AppsConfig.Instance().PlcAddressInfo.CommonStart = 1000;
                var plc = new MelsecPlc("PLC", new SocketComm("192.168.125.1", 9011, SocketCommType.Udp), new MelsecBinaryParser());
                config.Add(plc);
            }
            else
            {
               
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
