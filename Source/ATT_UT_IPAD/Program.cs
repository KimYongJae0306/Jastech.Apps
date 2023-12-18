using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Settings;
using Jastech.Apps.Winform.UI.Forms;
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
using Jastech.Framework.Winform.Forms;
using System;
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
            isRunning = true;
            if (isRunning)
            {
                Application.ThreadException += Application_ThreadException;
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Logger.Initialize(ConfigSet.Instance().Path.Log);

                MilHelper.InitApplication();
                CameraMil.BufferPoolCount = 400;
                //SystemHelper.StartChecker(@"D:\ATT_Memory_Test.txt");
                AppsConfig.Instance().UnitCount = 1;

                if (Cognex.VisionPro.CogLicense.GetLicensedFeatures(false, false).Count == 0)
                {
                    MessageConfirmForm cognexAlart = new MessageConfirmForm { Message = "Cognex license not detected.\r\nPlease check the key." };
                    cognexAlart.ShowDialog();
                }

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
                AppsConfig.Instance().MachineName = "UT COF ATT (Virtual)";

                // Initialize Config by Program Types
                string[] typeList = Enum.GetNames(typeof(ProgramType));
                ProgramSelectForm form = new ProgramSelectForm();
                form.SetList(typeList);
                form.ShowDialog();
                AppsConfig.Instance().ProgramType = form.SelectedProgramType;

                // Akkon LineScanCamera
                var alignCamera = new CameraVirtual("AkkonCamera", 6560, 1024, ColorFormat.Gray, SensorType.Line);
                alignCamera.OffsetX = 0;
                alignCamera.PixelResolution_um = 3.5F;
                alignCamera.LensScale = 10F;
                config.Add(alignCamera);

                // Align LineScanCamera
                var akkonCamera = new CameraVirtual("AlignCamera", 6560, 1024, ColorFormat.Gray, SensorType.Line);
                akkonCamera.OffsetX = 0;
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

                var programType = StringHelper.StringToEnum<ProgramType>(AppsConfig.Instance().ProgramType);
                switch (programType)
                {
                    case ProgramType.ProgramType_1:
                        AppsConfig.Instance().MachineName = "UT COF ATT #1";
                        CreateDeviceConfigType1(config);
                        break;
                    case ProgramType.ProgramType_2:
                        AppsConfig.Instance().MachineName = "UT COF ATT #2";
                        CreateDeviceConfigType2(config);
                        break;
                }
            }
        }

        private static void CreateDeviceConfigType1(MachineConfig config)
        {
            // Akkon LineScanCamera
            int akkonCameraWidth = 3072;
            int akkonCameraOffsetX = 1536;
            if (CheckCameraProperty(ref akkonCameraWidth, ref akkonCameraOffsetX, 6560) == true)
            {
                var akkonCamera = new CameraMil("AkkonCamera", akkonCameraWidth, 1024, ColorFormat.Gray, SensorType.Line);
                akkonCamera.OffsetX = akkonCameraOffsetX;
                akkonCamera.EnableReverseX = true;
                akkonCamera.MilSystemType = MilSystemType.Rapixo;
                akkonCamera.TriggerMode = TriggerMode.Hardware;
                akkonCamera.TriggerSource = (int)MilCxpTriggerSource.Cxp;
                akkonCamera.TriggerSignalType = MilTriggerSignalType.TL_Trigger;
                akkonCamera.TriggerIoSourceType = MILIoSourceType.AUX_IO0;
                akkonCamera.PixelResolution_um = 3.5F;
                akkonCamera.LensScale = 10F;
                akkonCamera.DigitizerNum = 0;
                akkonCamera.TDIDirection = TDIDirectionType.Reverse;

                akkonCamera.DcfFile = CameraMil.GetDcfFile(CameraType.VT_6K3_5X_H160);
                config.Add(akkonCamera);
            }

            // Align LineScanCamera
            int alignCameraWidth = 3072;
            int alignCameraOffsetX = 1536;
            if (CheckCameraProperty(ref alignCameraWidth, ref alignCameraOffsetX, 6560) == true)
            {
                var alignCamera = new CameraMil("AlignCamera", alignCameraWidth, 1024, ColorFormat.Gray, SensorType.Line);
                alignCamera.OffsetX = alignCameraOffsetX;
                alignCamera.EnableReverseX = true;
                alignCamera.MilSystemType = MilSystemType.Rapixo;
                alignCamera.TriggerMode = TriggerMode.Hardware;
                alignCamera.TriggerSource = (int)MilCxpTriggerSource.Cxp;
                alignCamera.TriggerSignalType = MilTriggerSignalType.TL_Trigger;
                alignCamera.TriggerIoSourceType = MILIoSourceType.AUX_IO0;
                alignCamera.PixelResolution_um = 3.5F;
                alignCamera.LensScale = 10F;
                alignCamera.DigitizerNum = 2;
                alignCamera.TDIDirection = TDIDirectionType.Reverse;

                alignCamera.DcfFile = CameraMil.GetDcfFile(CameraType.VT_6K3_5X_H160);
                config.Add(alignCamera);
            }

            // Motion
            var motion = new ACSMotion("Motion", 2, ACSConnectType.Ethernet);
            motion.IpAddress = "10.0.0.100";
            motion.TriggerBuffer = ACSBufferNumber.CameraTrigger_Unit1;
            config.Add(motion);

            ////Akkon LAF
            //var akkonLaf = new VirtualLAFCtrl("AkkonLaf");
            //config.Add(akkonLaf);

            ////Akkon LAF
            //var alignLaf = new VirtualLAFCtrl("AlignLaf");
            //config.Add(alignLaf);

            //Akkon LAF
            var akkonLaf = new NuriOneLAFCtrl("AkkonLaf");
            akkonLaf.SerialPortComm = new SerialPortComm("COM2", 9600);
            akkonLaf.AxisName = AxisName.Z0.ToString();
            akkonLaf.HomePosition_mm = 0.02;
            akkonLaf.ResolutionAxisZ = 10000.0;
            akkonLaf.MaxSppedAxisZ = 20;
            akkonLaf.AccDec = 70;
            //akkonLaf.MinimumReturn_db = -1.0;
            //akkonLaf.MaximumReturn_db = -1.0;
            config.Add(akkonLaf);

            // Align LAF
            var alignLaf = new NuriOneLAFCtrl("AlignLaf");
            alignLaf.SerialPortComm = new SerialPortComm("COM3", 9600);
            alignLaf.AxisName = AxisName.Z1.ToString();
            alignLaf.HomePosition_mm = 0.02;
            alignLaf.ResolutionAxisZ = 10000.0;
            alignLaf.MaxSppedAxisZ = 20;
            alignLaf.AccDec = 40;
            //alignLaf.MinimumReturn_db = -1.0;
            //alignLaf.MaximumReturn_db = -1.0;
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
            int akkonCameraWidth = 3072;
            int akkonCameraOffsetX = 768;
            if (CheckCameraProperty(ref akkonCameraWidth, ref akkonCameraOffsetX, 6560) == true)
            {
                var akkonCamera = new CameraMil("AkkonCamera", akkonCameraWidth, 1024, ColorFormat.Gray, SensorType.Line);
                akkonCamera.OffsetX = akkonCameraOffsetX;
                akkonCamera.EnableReverseX = true;
                akkonCamera.MilSystemType = MilSystemType.Rapixo;
                akkonCamera.TriggerMode = TriggerMode.Hardware;
                akkonCamera.TriggerSource = (int)MilCxpTriggerSource.Cxp;
                akkonCamera.TriggerSignalType = MilTriggerSignalType.TL_Trigger;
                akkonCamera.TriggerIoSourceType = MILIoSourceType.AUX_IO0;
                akkonCamera.PixelResolution_um = 3.5F;
                akkonCamera.LensScale = 10F;
                akkonCamera.DigitizerNum = 0;
                akkonCamera.TDIDirection = TDIDirectionType.Reverse;

                akkonCamera.DcfFile = CameraMil.GetDcfFile(CameraType.VT_6K3_5X_H160);
                config.Add(akkonCamera);
            }

            // Align LineScanCamera
            int alignCameraWidth = 3072;
            int alignCameraOffsetX = 512;
            if (CheckCameraProperty(ref alignCameraWidth, ref alignCameraOffsetX, 6560) == true)
            {
                var alignCamera = new CameraMil("AlignCamera", alignCameraWidth, 1024, ColorFormat.Gray, SensorType.Line);
                alignCamera.OffsetX = alignCameraOffsetX;
                alignCamera.EnableReverseX = true;
                alignCamera.MilSystemType = MilSystemType.Rapixo;
                alignCamera.TriggerMode = TriggerMode.Hardware;
                alignCamera.TriggerSource = (int)MilCxpTriggerSource.Cxp;
                alignCamera.TriggerSignalType = MilTriggerSignalType.TL_Trigger;
                alignCamera.TriggerIoSourceType = MILIoSourceType.AUX_IO0;
                alignCamera.PixelResolution_um = 3.5F;
                alignCamera.LensScale = 10F;
                alignCamera.DigitizerNum = 2;
                alignCamera.TDIDirection = TDIDirectionType.Reverse;

                alignCamera.DcfFile = CameraMil.GetDcfFile(CameraType.VT_6K3_5X_H160);
                config.Add(alignCamera);
            }

            // Motion
            var motion = new ACSMotion("Motion", 2, ACSConnectType.Ethernet);
            motion.IpAddress = "10.0.0.100";
            motion.TriggerBuffer = ACSBufferNumber.CameraTrigger_Unit2;
            config.Add(motion);

            ////Akkon LAF
            //var akkonLaf = new VirtualLAFCtrl("AkkonLaf");
            //config.Add(akkonLaf);

            ////Akkon LAF
            //var alignLaf = new VirtualLAFCtrl("AlignLaf");
            //config.Add(alignLaf);

            //// Akkon LAF
            var akkonLaf = new NuriOneLAFCtrl("AkkonLaf");
            akkonLaf.SerialPortComm = new SerialPortComm("COM2", 9600);
            akkonLaf.AxisName = AxisName.Z0.ToString();
            akkonLaf.HomePosition_mm = 0.025;
            akkonLaf.ResolutionAxisZ = 10000.0;
            akkonLaf.MaxSppedAxisZ = 20;
            akkonLaf.AccDec = 70;
            //akkonLaf.MinimumReturn_db = -1.0;
            //akkonLaf.MaximumReturn_db = -1.0;
            config.Add(akkonLaf);

            //// Align LAF
            var alignLaf = new NuriOneLAFCtrl("AlignLaf");
            alignLaf.SerialPortComm = new SerialPortComm("COM3", 9600);
            alignLaf.AxisName = AxisName.Z1.ToString();
            alignLaf.HomePosition_mm = 0.02;
            alignLaf.ResolutionAxisZ = 10000.0;
            alignLaf.MaxSppedAxisZ = 20;
            alignLaf.AccDec = 40;
            //alignLaf.MinimumReturn_db = -1.0;
            //alignLaf.MaximumReturn_db = -1.0;
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
            AppsConfig.Instance().PlcAddressInfo.CommonStart = 124000;
            AppsConfig.Instance().PlcAddressInfo.ResultStart = 125000;
            AppsConfig.Instance().PlcAddressInfo.ResultStart_Align = 125220;
            AppsConfig.Instance().PlcAddressInfo.ResultTabToTabInterval = 200;
            AppsConfig.Instance().PlcAddressInfo.ResultStart_Akkon = 125230;

            var plc = new MelsecPlc("PLC", new SocketComm("192.168.130.2", 9023, SocketCommType.Udp, 9033), new MelsecBinaryParser());
            config.Add(plc);
        }

        private static bool CheckCameraProperty(ref int width, ref int offsetX, int fullPixelSize)
        {
            if (width % 16 != 0 || offsetX % 16 != 0)
            {
                string errorMessage = string.Format("Set parameter to a multiple of 16\r\n Width : {0}, Offset : {1}", width, offsetX);
                Logger.Debug(LogType.Device, errorMessage);

                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = errorMessage;

                return false;
            }

            if (width > fullPixelSize) 
                width = fullPixelSize;

            if (width + offsetX > fullPixelSize)
            {
                string errorMessage = "Width + Offset <= " + fullPixelSize.ToString();
                Logger.Debug(LogType.Device, errorMessage);

                int newOffsetX = fullPixelSize - width;
                errorMessage = string.Format("{0}\r\n\r\nInput Width : {1},Offset : {2}\r\n\r\nDo you want to Change Parameter?\r\n\r\nSet Width : {3},Offset : {4}",
                                            errorMessage, width, offsetX, width, newOffsetX);

                MessageYesNoForm form = new MessageYesNoForm();
                form.Message = errorMessage;
                if (form.ShowDialog() == DialogResult.Yes)
                {
                    offsetX = newOffsetX;
                    return true;
                }
                else
                    return false;
            }
            else
                return true;
        }

        private static void ConfigSet_OperationConfigCreated(OperationConfig config)
        {
            if (MessageBox.Show("Do you want to Virtual Mode?", "Setup", MessageBoxButtons.YesNo) == DialogResult.Yes)
                config.VirtualMode = true;
            else
                config.VirtualMode = false;
        }

        private static void ConfigSet_PathConfigCreated(PathConfig config)
        {
            config.CreateDirectory();
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            PlcControlManager.Instance().WritePcVisionStatus(MachineStatus.STOP);
            string message = "Application_ThreadException " + e.Exception.Message;
            Logger.Error(ErrorType.Apps, message);
            System.Diagnostics.Trace.WriteLine(message);
            MessageBox.Show(message);
            Application.Exit(new System.ComponentModel.CancelEventArgs(false));
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            PlcControlManager.Instance().WritePcVisionStatus(MachineStatus.STOP);
            var exception = (Exception)e.ExceptionObject;
            string message = "CurrentDomain_UnhandledException " + exception.Message + " Source: " + exception.Source.ToString() + "StackTrack :" + exception.StackTrace.ToString();
            Logger.Error(ErrorType.Apps, message);

            System.Diagnostics.Trace.WriteLine(message);
            MessageBox.Show(message);
        }
    }
}