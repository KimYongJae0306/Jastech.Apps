using Jastech.Apps.Structure;
using Jastech.Framework.Config;
using Jastech.Framework.Device.Cameras;
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
        //public OperationSettings Operation { get; set; } = new OperationSettings();

        #endregion
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
            if(MessageBox.Show("Do you want to Virtual Mode?", "Setup", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
            }
            else
            {

            }
        }

        private void AppConfig_PathConfigCreated(PathConfig config)
        {
            config.CreateDirectory();
        }

        public override void Save()
        {
            base.Save();
            //Operation.Save();
        }

        public override void Load()
        {
            base.Load();
           // Operation.Load();
        }
    }
}
