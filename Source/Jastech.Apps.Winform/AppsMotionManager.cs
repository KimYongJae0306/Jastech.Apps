using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Structure;
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

            string unit0FileName = string.Format("AxisHanlder_{0}.json", AxisHandlerName.Handler0);
            string unit0FilePath = Path.Combine(dir, unit0FileName);
            if (File.Exists(unit0FilePath) == false)
            {
                AxisHandler handler0 = new AxisHandler(AxisHandlerName.Handler0.ToString());

                handler0.AddAxis(AxisName.X, motion, 0, 2);
                handler0.AddAxis(AxisName.Y, motion, 8, 1);
                handler0.AddAxis(AxisName.Z, motion, 0, 2);

                AxisHandlerList.Add(handler0);

                JsonConvertHelper.Save(unit0FilePath, handler0);
            }
            else
            {
                AxisHandler unit0 = new AxisHandler();
                JsonConvertHelper.LoadToExistingTarget<AxisHandler>(unit0FilePath, unit0);

                foreach (var axis in unit0.AxisList)
                {
                    axis.SetMotion(motion);
                }
                AxisHandlerList.Add(unit0);
            }
            return true;
        }

        public void Save(AxisHandler axishandler)
        {
            string dir = Path.Combine(AppConfig.Instance().Path.Config, "AxisHanlder");
            string unit0FileName = string.Format("AxisHanlder_{0}.json", axishandler.Name);
            string unit0FilePath = Path.Combine(dir, unit0FileName);

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            JsonConvertHelper.Save(unit0FilePath, axishandler);
        }

        public Axis GetAxis(AxisHandlerName axisHandlerName, AxisName axisName)
        {
            foreach (var axisHandler in AxisHandlerList)
            {
                if (axisHandler.Name == axisHandlerName.ToString())
                {
                    foreach (var axis in axisHandler.AxisList)
                    {
                        if (axis.Name == axisName.ToString())
                            return axis;
                    }
                }
            }
            return null;
        }

        //public Axis GetAxis(string axisHandlerName, AxisName axisName)
        //{
        //    foreach (var axisHandler in AxisHandlerList)
        //    {
        //        if (axisHandler.Name == axisHandlerName)
        //        {
        //            foreach (var axis in axisHandler.AxisList)
        //            {
        //                if (axis.Name == axisName.ToString())
        //                    return axis;
        //            }
        //        }
        //    }
        //    return null;
        //}

        public AxisHandler GetAxisHandler(AxisHandlerName axisHandlerName)
         {
            return AxisHandlerList.Where(x => x.Name == axisHandlerName.ToString()).First();
        }

        public void MoveTo(UnitName unitName, AxisHandlerName handlerName, AxisName axisName, TeachingPositionType teachingPosition)
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            var unit = inspModel.GetUnit(unitName);
            var axis = GetAxis(handlerName, axisName);

            var posData = unit.TeachingPositions[(int)teachingPosition];
            var targetPosition = posData.GetTargetPosition(axisName);
            var movingParam = posData.GetMovingParams(axisName);

            axis.MoveTo(targetPosition, movingParam);
        }

        public bool IsMovingCompleted(UnitName unitName, AxisHandlerName handlerName, AxisName axisName, TeachingPositionType teachingPosition)
        {
            var axis = GetAxis(handlerName, axisName);
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            var posData = inspModel.GetUnit(unitName).TeachingPositions[(int)teachingPosition];
            var targetPosition = posData.GetTargetPosition(axisName);
            var actualPosition = axis.GetActualPosition();

            if (Math.Abs(targetPosition - actualPosition) <= 0.1)
                return true;

            return false;
        }
        #endregion
    }

    public enum AxisHandlerName
    {
        Handler0,
        Handler1,
        Handler2,
    }
}
