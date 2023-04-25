using Jastech.Apps.Structure.Data;
using Jastech.Framework.Config;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.Grabbers;
using Jastech.Framework.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraTeseter
{
    public class TestAppConfig : ConfigSet
    {
        #region 필드
        private static TestAppConfig _instance = null;
        #endregion

        #region 속성
        #endregion

        #region 메서드
        public static TestAppConfig Instance()
        {
            if (_instance == null)
            {
                _instance = new TestAppConfig();
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
                var camera0 = new CameraVirtual(CameraName.LeftArea.ToString(), 4096, 1024, ColorFormat.Gray, SensorType.Area);
                config.Add(camera0);

                var camera1 = new CameraVirtual(CameraName.RightArea.ToString(), 4096, 1024, ColorFormat.Gray, SensorType.Area);
                config.Add(camera1);
            }
            else
            {
                var camera1 = new CameraMil(CameraName.LinscanMIL0.ToString(), 4640, 1024, ColorFormat.Gray, SensorType.Line);
                camera1.MilSystemType = Jastech.Framework.Device.Grabbers.MilSystemType.Rapixo;
                camera1.SystemNum = 0;
                camera1.DigitizerNum = 0;
                camera1.TriggerMode = TriggerMode.Software;
                config.Add(camera1);

                //var camera2 = new CameraMil(CameraName.LinscanMIL1.ToString(), 4640, 1024, ColorFormat.Gray, SensorType.Line);
                //camera2.MilSystemType = Jastech.Framework.Device.Grabbers.MilSystemType.Rapixo;
                //camera2.SystemNum = 0;
                //camera2.DigitizerNum = 2;
                //camera2.TriggerMode = TriggerMode.Software;
                //config.Add(camera2);
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
