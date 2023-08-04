using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Framework.Config;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

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
            if (axis.Name.Contains("Z"))
                return true;

            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            var posData = inspModel.GetUnit(unitName).GetTeachingInfo(teachingPosition);
            var targetPosition = posData.GetTargetPosition(axis.Name);
            var actualPosition = axis.GetActualPosition();

            if (Math.Abs(targetPosition - actualPosition) <= /*double.Epsilon*/0.0001)
                return true;

            return false;
        }
        public bool MoveTo(TeachingPosType teachingPos, out string error)
        {
            error = "";

            if (ConfigSet.Instance().Operation.VirtualMode)
                return true;

            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            var teachingInfo = inspModel.GetUnit(UnitName.Unit0).GetTeachingInfo(teachingPos);

            Axis axisX = MotionManager.Instance().GetAxis(AxisHandlerName.Handler0, AxisName.X);

            var movingParamX = teachingInfo.GetMovingParam(AxisName.X.ToString());

            if (MoveAxis(teachingPos, axisX, movingParamX) == false)
            {
                error = string.Format("Move To Axis X TimeOut!({0})", movingParamX.MovingTimeOut.ToString());
                Logger.Write(LogType.Seq, error);
                return false;
            }

            string message = string.Format("Move Completed.(Teaching Pos : {0})", teachingPos.ToString());
            Logger.Write(LogType.Seq, message);

            return true;
        }

        private bool MoveAxis(TeachingPosType teachingPos, Axis axis, AxisMovingParam movingParam)
        {
            if (IsAxisInPosition(UnitName.Unit0, teachingPos, axis) == false)
            {
                Stopwatch sw = new Stopwatch();
                sw.Restart();

                StartAbsoluteMove(UnitName.Unit0, teachingPos, axis);

                while (IsAxisInPosition(UnitName.Unit0, teachingPos, axis) == false)
                {
                    if (sw.ElapsedMilliseconds >= movingParam.MovingTimeOut)
                    {
                        return false;
                    }
                    Thread.Sleep(10);
                }
            }
            Console.WriteLine("Dove Done.");
            return true;
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
