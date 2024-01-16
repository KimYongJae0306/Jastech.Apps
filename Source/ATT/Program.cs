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
using Jastech.Framework.Device.Plcs.Melsec.Parsers;
using Jastech.Framework.Device.Plcs.Melsec;
using Jastech.Framework.Imaging;
using Jastech.Framework.Matrox;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform.Forms;
using System;
using System.Threading;
using System.Windows.Forms;

namespace ATT
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

            if(isRunning)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.ThreadException += Application_ThreadException;
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

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
                ACSBufferConfig.Instance().NewAcsBufferSettingEventHandler += NewAcsBufferSettingEventHandler;
                ACSBufferConfig.Instance().Initialize();

                UserManager.Instance().Initialize();

                var mainForm = new MainForm();
                SystemManager.Instance().SetMainForm(mainForm);
                Application.Run(mainForm);
            }
            else
            {
                MessageBox.Show("The program already started.");
                Application.Exit();
            }
        }

        private static void NewAcsBufferSettingEventHandler()
        {
            var buffer = ACSBufferConfig.Instance();

            if (AppsConfig.Instance().ProgramType == ProgramType.ProgramType_1.ToString())
            {
                buffer.CameraTrigger = 7;

                LafTriggerBuffer lafTriggerBuffer = new LafTriggerBuffer
                {
                    LafName = "Laf",
                    LafArrayIndex = 0,
                    OutputBit = 1,
                    BufferNumber = 25,
                };

                buffer.LafTriggerBufferList.Add(lafTriggerBuffer);

            }
        }

        private static void ConfigSet_MachineConfigCreated(MachineConfig config)
        {
            if (ConfigSet.Instance().Operation.VirtualMode)
            {
                // Initialize Config by Program Types
                AppsConfig.Instance().ProgramType = ProgramType.ProgramType_1.ToString();
                AppsConfig.Instance().MachineName = "Test Equipment #7 (Virtual)";

                var lineCamera = new CameraVirtual("LineCamera", 6560, 1024, ColorFormat.Gray, SensorType.Line);
                config.Add(lineCamera);

                var motion = new VirtualMotion("VirtualMotion", 3);
                config.Add(motion);

                var light1 = new VirtualLightCtrl("LvsLight12V", 6);
                light1.ChannelNameMap["Ch.Blue"] = 0;
                light1.ChannelNameMap["Ch.RedSpot"] = 1;
                config.Add(light1);

                var light2 = new VirtualLightCtrl("LvsLight24V", 6);
                light2.ChannelNameMap["Ch.RedRing"] = 0;
                config.Add(light2);

                var laf = new VirtualLAFCtrl("Laf");
                config.Add(laf);
            }
            else
            {
                // Initialize Config by Program Types
                AppsConfig.Instance().ProgramType = ProgramType.ProgramType_1.ToString();
                AppsConfig.Instance().MachineName = "Test Equipment #7";

                int cameraWidth = 3072;
                int cameraOffsetX = 1536;
                if (CheckCameraProperty(ref cameraWidth, ref cameraOffsetX, 6560) == true)
                {
                    var lineCamera = new CameraMil("LineCamera", cameraWidth, 1024, ColorFormat.Gray, SensorType.Line);
                    lineCamera.OffsetX = cameraOffsetX;
                    lineCamera.EnableReverseX = true;
                    lineCamera.MilSystemType = MilSystemType.Rapixo;
                    lineCamera.TriggerMode = TriggerMode.Hardware;
                    lineCamera.TriggerSource = (int)MilCxpTriggerSource.Cxp;
                    lineCamera.TriggerSignalType = MilTriggerSignalType.TL_Trigger;
                    lineCamera.TriggerIoSourceType = MILIoSourceType.AUX_IO0;
                    lineCamera.PixelResolution_um = 3.5F;
                    lineCamera.LensScale = 10F;
                    lineCamera.DigitizerNum = 0;
                    lineCamera.TDIDirection = TDIDirectionType.Forward;

                    lineCamera.DcfFile = CameraMil.GetDcfFile(CameraType.VT_4K5X_H200);
                    config.Add(lineCamera);
                }

                // Motion
                var motion = new ACSMotion("Motion", 2, ACSConnectType.Ethernet);
                motion.IpAddress = "10.0.0.100";
                config.Add(motion);

                //var light1 = new LvsLightCtrl("LvsLight12V", 6, new SerialPortComm("COM2", 9600), new LvsSerialParser());
                //light1.ChannelNameMap["Ch.Blue"] = 0;
                //light1.ChannelNameMap["Ch.RedSpot"] = 1;
                //config.Add(light1);

                //var light2 = new LvsLightCtrl("LvsLight24V", 6, new SerialPortComm("COM3", 9600), new LvsSerialParser());
                //light2.ChannelNameMap["Ch.RedRing"] = 0;
                //config.Add(light2);

                var laf = new NuriOneLAFCtrl("Laf");
                laf.SerialPortComm = new SerialPortComm("COM1", 9600);
                laf.AxisName = AxisName.Z0.ToString();
                laf.HomePosition_mm = 0.02;
                laf.ResolutionAxisZ = 10000.0;
                laf.MaxSppedAxisZ = 20;
                laf.AccDec = 30;
                config.Add(laf);

                //var laf = new VirtualLAFCtrl("Laf");
                //config.Add(laf);

                // PLC ATT Tester
                AppsConfig.Instance().PlcAddressInfo.CommonStart = 20000;
                AppsConfig.Instance().PlcAddressInfo.ResultStart = 21000;
                AppsConfig.Instance().PlcAddressInfo.ResultStart_Align = 21220;
                AppsConfig.Instance().PlcAddressInfo.ResultTabToTabInterval = 200;
                AppsConfig.Instance().PlcAddressInfo.ResultStart_Akkon = 21230;

                var plc = new MelsecPlc("PLC", new SocketComm("127.0.0.1", 9021, SocketCommType.Udp, 9031), new MelsecBinaryParser());
                config.Add(plc);
            }
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
