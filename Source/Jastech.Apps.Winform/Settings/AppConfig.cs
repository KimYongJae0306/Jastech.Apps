using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Framework.Comm;
using Jastech.Framework.Comm.Protocol;
using Jastech.Framework.Config;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.LAF;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Device.LightCtrls;
using Jastech.Framework.Device.LightCtrls.Lvs;
using Jastech.Framework.Device.LightCtrls.Lvs.Parser;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Imaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.Settings
{
    public class AppConfig : ConfigSet
    {
        #region 필드
        private static AppConfig _instance = null;
        #endregion

        #region 속성
        [JsonProperty]
        public bool UseMaterialInfo { get; set; } = false;

        [JsonProperty]
        public int GrabCount { get; set; } = 10;

        [JsonProperty]
        public int AkkonThreadCount { get; set; } = 8;

        #endregion

        #region 메서드
        public static AppConfig Instance()
        {
            if (_instance == null)
            {
                _instance = new AppConfig();
            }

            return _instance;
        }

        public override void Initialize()
        {
            base.PathConfigCreated += AppConfig_PathConfigCreated;
            base.OperationConfigCreated += AppConfig_OperationConfigCreated;
            base.MachineConfigCreated += AppConfig_MachineConfigCreated;
            base.Initialize();
        }

        private void AppConfig_OperationConfigCreated(OperationConfig config)
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

        private void AppConfig_MachineConfigCreated(MachineConfig config)
        {
            if (Operation.VirtualMode)
            {
                var camera0 = new CameraVirtual(CameraName.LinscanMIL0.ToString(), 4640, 1024, ColorFormat.Gray, SensorType.Area);
                config.Add(camera0);

                //var camera1 = new CameraVirtual(CameraName.LinscanMIL1.ToString(), 4096, 1024, ColorFormat.Gray, SensorType.Area);
                //config.Add(camera1);

                var motion = new VirtualMotion("VirtualMotion", 3);
                config.Add(motion);

                var light1 = new VirtualLightCtrl("LvsLight12V", 6);
                light1.ChannelNameMap["Ch.Blue"] = 0;
                light1.ChannelNameMap["Ch.RedSpot"] = 1;
                config.Add(light1);

                var light2 = new VirtualLightCtrl("LvsLight24V", 6);
                light2.ChannelNameMap["Ch.RedRing"] = 0;
                config.Add(light2);

                var laf = new VirtualLAFCtrl(LAFName.Akkon.ToString());
                config.Add(laf);
            }
            else
            {
                var camera1 = new CameraMil(CameraName.LinscanMIL0.ToString(), 4640, 1024, ColorFormat.Gray, SensorType.Line);
                camera1.MilSystemType = Framework.Device.Grabbers.MilSystemType.Rapixo;
                camera1.TriggerMode = TriggerMode.Software;
                //camera1.TriggerSource = (int)MilCxpTriggerSource.Cxp;
                //camera1.TriggerSignalType = MilTriggerSignalType.TL_Trigger;
                //camera1.TriggerIoSourceType = MILIoSourceType.AUX_IO0;
                config.Add(camera1);

                //var camera0 = new CameraVirtual(CameraName.LeftArea.ToString(), 4096, 1024, ColorFormat.Gray, SensorType.Area);
                //config.Add(camera0);

                // Motion OK
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

                // LAF OK
                var laf1 = new NuriOneLAFCtrl(LAFName.Akkon.ToString());
                laf1.SerialPortComm = new SerialPortComm
                {
                    PortName = "COM4",
                    BaudRate = 9600,
                };
                config.Add(laf1);

                //var laf2 = new NuriOneLAFCtrl(LAFName.Akkon.ToString());
                //laf2.SerialPortComm = new SerialPortComm
                //{
                //    PortName = "COM3",
                //    BaudRate = 9600,
                //};
                //config.Add(laf2);
            }
        }

        private void AppConfig_PathConfigCreated(PathConfig config)
        {
            config.CreateDirectory();
        }

        public override void Save()
        {
            base.Save();
        }

        public override void Load()
        {
            base.Load();
        }
        #endregion
    }

    public enum TeachingMode
    {
        Manual,
        Auto,
    }
}
