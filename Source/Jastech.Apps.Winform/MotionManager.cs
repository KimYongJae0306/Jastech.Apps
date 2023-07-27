using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Framework.Config;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Jastech.Apps.Winform
{
    public class MotionManager
    {
        #region 필드
        private static MotionManager _instance = null;
        #endregion

        #region 속성
        public List<AxisHandler> AxisHandlerList { get; set; } = new List<AxisHandler>();
        #endregion

        #region 메서드
        public static MotionManager Instance()
        {
            if (_instance == null)
                _instance = new MotionManager();

            return _instance;
        }

        public void Save(AxisHandler axishandler)
        {
            string dir = Path.Combine(ConfigSet.Instance().Path.Config, "AxisHanlder");
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
                    foreach (var axis in axisHandler.GetAxisList())
                    {
                        if (axis.Name == axisName.ToString())
                            return axis;
                    }
                }
            }
            return null;
        }

        public AxisHandler GetAxisHandler(AxisHandlerName axisHandlerName)
         {
            return AxisHandlerList.Where(x => x.Name == axisHandlerName.ToString()).FirstOrDefault();
        }

        public void StartAbsoluteMove(UnitName unitName, TeachingPosType teachingPosType, Axis axis)
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            var unit = inspModel.GetUnit(unitName);
            var posData = unit.GetTeachingInfo(teachingPosType);

            var targetPosition = posData.GetTargetPosition(axis.Name);
            var movingParam = posData.GetMovingParam(axis.Name);

            axis.StartAbsoluteMove(targetPosition, movingParam);
        }

        public bool IsAxisInPosition(UnitName unitName, TeachingPosType teachingPosition, Axis axis)
        {
            if (axis.Name == AxisName.Z0.ToString())
                return true;

            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            var posData = inspModel.GetUnit(unitName).GetTeachingInfo(teachingPosition);
            var targetPosition = posData.GetTargetPosition(axis.Name);
            var actualPosition = axis.GetActualPosition();

            if (Math.Abs(targetPosition - actualPosition) <= double.Epsilon)
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
