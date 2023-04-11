using Jastech.Apps.Structure;
using Jastech.Framework.Comm;
using Jastech.Framework.Config;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.LightCtrls;
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
        public int GrabCount { get; set; } = 10;
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
        }

        private void AppConfig_MachineConfigCreated(MachineConfig config)
        {
            if (Operation.VirtualMode)
            {
                var camera0 = new CameraVirtual(CameraName.LeftArea.ToString(), 4096, 1024, ColorFormat.Gray, SensorType.Area);
                config.Add(camera0);

                var camera1 = new CameraVirtual(CameraName.RightArea.ToString(), 4096, 1024, ColorFormat.Gray, SensorType.Area);
                config.Add(camera1);

                var motion = new VirtualMotion("VirtualMotion", 3);
                config.Add(motion);

                var light1 = new VirtualLightCtrl("LvsLight12V", 6);
                light1.ChannelNameMap["Ch.Blue"] = 0;
                light1.ChannelNameMap["Ch.RedSpot"] = 1;
                config.Add(light1);

                var light2 = new VirtualLightCtrl("LvsLight24V", 6);
                light2.ChannelNameMap["Ch.RedRing"] = 0;
                config.Add(light2);
            }
            else
            {
                var camera0 = new CameraVieworksVT(CameraName.LinscanVT0.ToString(), 4096, 1024, ColorFormat.Gray, SensorType.Line);
                config.Add(camera0);

                var camera1 = new CameraMil(CameraName.LinscanMIL0.ToString(), 4096, 1024, ColorFormat.Gray, SensorType.Line);
                config.Add(camera1);

                var motion = new ACSMotion("Motion", 3);
                config.Add(motion);

                var light1 = new LvsLightCtrl("LvsLight12V", 6);
                light1.SerialPortProp = new SerialPortInfo
                {
                    BaudRate = 9600,
                    PortName = "COM2",
                };
                light1.ChannelNameMap["Ch.Blue"] = 0;
                light1.ChannelNameMap["Ch.RedSpot"] = 1;
                config.Add(light1);

                var light2 = new LvsLightCtrl("LvsLight24V", 6);
                light2.SerialPortProp = new SerialPortInfo
                {
                    BaudRate = 9600,
                    PortName = "COM3",
                };
                light2.ChannelNameMap["Ch.RedRing"] = 0;
                config.Add(light2);
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
}
