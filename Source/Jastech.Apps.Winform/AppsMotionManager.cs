using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform
{
    public class AppsMotionManager
    {
        #region 필드
        private static AppsMotionManager _instance = null;
        #endregion

        #region 속성
        private List<AxisHandler> AxisHandlerList { get; set; } = new List<AxisHandler>();
        #endregion

        #region 메서드
        public static AppsMotionManager Instance()
        {
            if (_instance == null)
            {
                _instance = new AppsMotionManager();
            }

            return _instance;
        }

        public bool CreateAxisHanlder()
        {
            var motion = DeviceManager.Instance().MotionHandler.First();
            if (motion == null)
                return false;

            string dir = Path.Combine(AppConfig.Instance().Path.Config, "AxisHanlder");

            if (Directory.Exists(dir) == false)
            {
                Directory.CreateDirectory(dir);
            }

            string unit0FileName = string.Format("AxisHanlder_{0}.json", AxisHandlerName.Unit0);
            string unit0FilePath = Path.Combine(dir, unit0FileName);
            if (File.Exists(unit0FilePath) == false)
            {
                AxisHandler unit0 = new AxisHandler(AxisHandlerName.Unit0.ToString());

                unit0.AddAxis(AxisName.X, motion, 0, 1);
                unit0.AddAxis(AxisName.Y, motion, 0, 1);
                unit0.AddAxis(AxisName.Z, motion, 0, 0);

                AxisHandlerList.Add(unit0);

                JsonConvertHelper.Save(unit0FilePath, unit0);
            }
            else
            {
                AxisHandler unit0 = new AxisHandler();
                JsonConvertHelper.LoadToExistingTarget<AxisHandler>(unit0FilePath, unit0);

                AxisHandlerList.Add(unit0);
            }
            return true;
        }

        public Axis GetAxis(AxisHandlerName axisHandlerName, AxisName axisName)
        {
            foreach (var axisHandler in AxisHandlerList)
            {
                foreach (var axis in axisHandler.AxisList)
                {
                    if (axis.Name == axisName.ToString())
                        return axis;
                }
            }
            return null;
        }

        public AxisHandler GetAxisHandler(AxisHandlerName axisHandlerName)
        {
            return AxisHandlerList.Where(x => x.Name == axisHandlerName.ToString()).First();
        }
        #endregion
    }

    public enum AxisHandlerName
    {
        Unit0,
        Unit1,
        Unit2,
    }
}
