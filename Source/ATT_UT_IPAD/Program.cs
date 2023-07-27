using Emgu.CV.Ocl;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Settings;
using Jastech.Apps.Winform.UI.Forms;
using Jastech.Framework.Comm;
using Jastech.Framework.Config;
using Jastech.Framework.Device;
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
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;

namespace ATT_UT_IPAD
{
    internal static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        private static void Main()
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
                var alignCamera = new CameraVirtual("AkkonCamera", 3072, 1024, ColorFormat.Gray, SensorType.Line);
                alignCamera.PixelResolution_um = 3.5F;
                alignCamera.LensScale = 10F;
                config.Add(alignCamera);

                // Align LineScanCamera
                var akkonCamera = new CameraVirtual("AlignCamera", 3072, 1024, ColorFormat.Gray, SensorType.Line);
                akkonCamera.PixelResolution_um = 3.5F;
                akkonCamera.LensScale = 10F;
                config.Add(akkonCamera);

                // Motion
                var motion = new VirtualMotion("VirtualMotion", 2);
                config.Add(motion);

                // Akkon LAF
                var akkonLaf = new VirtualLAFCtrl("AkkonLaf");
                config.Add(akkonLaf);

                // Akkon LAF
                var alignLaf = new VirtualLAFCtrl("AlignLaf");
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
                // Initialize Config by Program Types
                string[] typeList = Enum.GetNames(typeof(ProgramType));

                ProgramSelectForm form = new ProgramSelectForm();
                form.SetList(typeList);
                form.ShowDialog();

                AppsConfig.Instance().ProgramType = form.SelectedProgramType;

                if (Enum.TryParse(AppsConfig.Instance().ProgramType, true, out ProgramType type))
                {
                    switch (type)
                    {
                        case ProgramType.ProgramType_1:
                            CreateDeviceConfigType1(config);
                            break;
                        case ProgramType.ProgramType_2:
                            CreateDeviceConfigType2(config);
                            break;
                    }
                }
                else
                    Console.WriteLine($"ConfigSet_MachineConfigCreated: Failed to parse program type {AppsConfig.Instance().ProgramType}");
            }
        }

        private static void CreateDeviceConfigType1(MachineConfig config)
        {
            // Akkon LineScanCamera
            var akkonCamera = new CameraMil("AkkonCamera", 3072, 1024, ColorFormat.Gray, SensorType.Line);
            akkonCamera.MilSystemType = MilSystemType.Rapixo;
            akkonCamera.TriggerMode = TriggerMode.Hardware;
            akkonCamera.TriggerSource = (int)MilCxpTriggerSource.Cxp;
            akkonCamera.TriggerSignalType = MilTriggerSignalType.TL_Trigger;
            akkonCamera.TriggerIoSourceType = MILIoSourceType.AUX_IO0;
            akkonCamera.PixelResolution_um = 3.5F;
            akkonCamera.LensScale = 10F;
            akkonCamera.DigitizerNum = 0;

            akkonCamera.DcfFile = CameraMil.GetDcfFile(CameraType.VT_6K3_5X_H160);
            config.Add(akkonCamera);

            // Align LineScanCamera
            var alignCamera = new CameraMil("AlignCamera", 3072, 1024, ColorFormat.Gray, SensorType.Line);
            alignCamera.MilSystemType = MilSystemType.Rapixo;
            alignCamera.TriggerMode = TriggerMode.Hardware;
            alignCamera.TriggerSource = (int)MilCxpTriggerSource.Cxp;
            alignCamera.TriggerSignalType = MilTriggerSignalType.TL_Trigger;
            alignCamera.TriggerIoSourceType = MILIoSourceType.AUX_IO0;
            alignCamera.PixelResolution_um = 3.5F;
            alignCamera.LensScale = 10F;
            alignCamera.DigitizerNum = 2;

            alignCamera.DcfFile = CameraMil.GetDcfFile(CameraType.VT_6K3_5X_H160);
            config.Add(alignCamera);

            // Motion
            var motion = new ACSMotion("Motion", 2, ACSConnectType.Ethernet);
            motion.IpAddress = "10.0.0.100";
            config.Add(motion);

            // Akkon LAF
            var akkonLaf = new NuriOneLAFCtrl("AkkonLaf");
            akkonLaf.SerialPortComm = new SerialPortComm("COM2", 9600);
            config.Add(akkonLaf);

            // Align LAF
            var alignLaf = new NuriOneLAFCtrl("AlignLaf");
            alignLaf.SerialPortComm = new SerialPortComm("COM3", 9600);
            config.Add(alignLaf);

            // Light1
            var spotLight = new LvsLightCtrl("Spot", 6, new SerialPortComm("COM4", 19200), new LvsSerialParser()); // 12V
            spotLight.ChannelNameMap["Ch.AlignBlue"] = 1; // channel 지정
            spotLight.ChannelNameMap["Ch.AkkonRed"] = 2; // channel 지정
            spotLight.ChannelNameMap["Ch.AkkonBlue"] = 3; // channel 지정
            config.Add(spotLight);

            // Light2
            var ringLight = new LvsLightCtrl("Ring", 6, new SerialPortComm("COM5", 19200), new LvsSerialParser());  // 24V
            ringLight.ChannelNameMap["Ch.RedRing1"] = 1; // channel 지정
            ringLight.ChannelNameMap["Ch.RedRing2"] = 2; // channel 지정
            config.Add(ringLight);

            // PLC UT IPAD
            AppsConfig.Instance().PlcAddressInfo.CommonStart = 120000;
            AppsConfig.Instance().PlcAddressInfo.ResultStart = 121000;
            AppsConfig.Instance().PlcAddressInfo.ResultStart_Align = 121220;
            AppsConfig.Instance().PlcAddressInfo.ResultTabToTabInterval = 200;
            AppsConfig.Instance().PlcAddressInfo.ResultStart_Akkon = 121230;

            var plc = new MelsecPlc("PLC", new SocketComm("192.168.130.2", 9021, SocketCommType.Udp, 9031), new MelsecBinaryParser());
            config.Add(plc);
        }

        private static void CreateDeviceConfigType2(MachineConfig config)
        {
            // Akkon LineScanCamera
            var akkonCamera = new CameraMil("AkkonCamera", 3072, 1024, ColorFormat.Gray, SensorType.Line);
            akkonCamera.MilSystemType = MilSystemType.Rapixo;
            akkonCamera.TriggerMode = TriggerMode.Hardware;
            akkonCamera.TriggerSource = (int)MilCxpTriggerSource.Cxp;
            akkonCamera.TriggerSignalType = MilTriggerSignalType.TL_Trigger;
            akkonCamera.TriggerIoSourceType = MILIoSourceType.AUX_IO0;
            akkonCamera.PixelResolution_um = 3.5F;
            akkonCamera.LensScale = 10F;
            akkonCamera.DigitizerNum = 0;

            akkonCamera.DcfFile = CameraMil.GetDcfFile(CameraType.VT_6K3_5X_H160);
            config.Add(akkonCamera);

            // Align LineScanCamera
            var alignCamera = new CameraMil("AlignCamera", 3072, 1024, ColorFormat.Gray, SensorType.Line);
            alignCamera.MilSystemType = MilSystemType.Rapixo;
            alignCamera.TriggerMode = TriggerMode.Hardware;
            alignCamera.TriggerSource = (int)MilCxpTriggerSource.Cxp;
            alignCamera.TriggerSignalType = MilTriggerSignalType.TL_Trigger;
            alignCamera.TriggerIoSourceType = MILIoSourceType.AUX_IO0;
            alignCamera.PixelResolution_um = 3.5F;
            alignCamera.LensScale = 10F;
            alignCamera.DigitizerNum = 2;

            alignCamera.DcfFile = CameraMil.GetDcfFile(CameraType.VT_6K3_5X_H160);
            config.Add(alignCamera);

            // Motion
            var motion = new ACSMotion("Motion", 2, ACSConnectType.Ethernet);
            motion.IpAddress = "10.0.0.100";
            config.Add(motion);

            // Akkon LAF
            var akkonLaf = new NuriOneLAFCtrl("AkkonLaf");
            akkonLaf.SerialPortComm = new SerialPortComm("COM2", 9600);
            config.Add(akkonLaf);

            // Align LAF
            var alignLaf = new NuriOneLAFCtrl("AlignLaf");
            alignLaf.SerialPortComm = new SerialPortComm("COM3", 9600);
            config.Add(alignLaf);

            // Light1
            var spotLight = new LvsLightCtrl("Spot", 6, new SerialPortComm("COM4", 19200), new LvsSerialParser()); // 12V
            spotLight.ChannelNameMap["Ch.AlignBlue"] = 1; // channel 지정
            spotLight.ChannelNameMap["Ch.AkkonRed"] = 2; // channel 지정
            spotLight.ChannelNameMap["Ch.AkkonBlue"] = 3; // channel 지정
            config.Add(spotLight);

            // Light2
            var ringLight = new LvsLightCtrl("Ring", 6, new SerialPortComm("COM5", 19200), new LvsSerialParser());  // 24V
            ringLight.ChannelNameMap["Ch.RedRing1"] = 1; // channel 지정
            ringLight.ChannelNameMap["Ch.RedRing2"] = 2; // channel 지정
            config.Add(ringLight);

            // PLC UT IPAD
            AppsConfig.Instance().PlcAddressInfo.CommonStart = 120000;
            AppsConfig.Instance().PlcAddressInfo.ResultStart = 121000;
            AppsConfig.Instance().PlcAddressInfo.ResultStart_Align = 121220;
            AppsConfig.Instance().PlcAddressInfo.ResultTabToTabInterval = 200;
            AppsConfig.Instance().PlcAddressInfo.ResultStart_Akkon = 121230;

            var plc = new MelsecPlc("PLC", new SocketComm("192.168.130.2", 9021, SocketCommType.Udp, 9031), new MelsecBinaryParser());
            config.Add(plc);
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