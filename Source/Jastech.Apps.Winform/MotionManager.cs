﻿using Emgu.CV.Flann;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Framework.Config;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform;
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
            string dir = Path.Combine(ConfigSet.Instance().Path.Config, "AxisHandler");
            string unit0FileName = string.Format("AxisHandler_{0}.json", axishandler.Name);
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

        public Axis GetAxis(AxisHandlerName axisHandlerName, string axisName)
        {
            foreach (var axisHandler in AxisHandlerList)
            {
                if (axisHandler.Name == axisHandlerName.ToString())
                {
                    foreach (var axis in axisHandler.GetAxisList())
                    {
                        if (axis.Name == axisName)
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

        public void StartAbsoluteMove(UnitName unitName, TeachingPosType teachingPosType, Axis axis, double offset = 0)
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            var unit = inspModel.GetUnit(unitName);
            var posData = unit.GetTeachingInfo(teachingPosType);

            var targetPosition = posData.GetTargetPosition(axis.Name) + offset;
            var movingParam = posData.GetMovingParam(axis.Name);

            axis.StartAbsoluteMove(targetPosition, movingParam);
        }

        public bool IsAxisInPosition(UnitName unitName, TeachingPosType teachingPosition, Axis axis, double offset = 0)
        {
            if (axis.Name.Contains("Z"))
                return true;

            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            var posData = inspModel.GetUnit(unitName).GetTeachingInfo(teachingPosition);
            var targetPosition = posData.GetTargetPosition(axis.Name) + offset;
            var actualPosition = axis.GetActualPosition();

            if (Math.Abs(targetPosition - actualPosition) <= /*double.Epsilon*/0.0002)
                return true;

            return false;
        }

        public bool MoveAxisX(AxisHandlerName axisHandlerName, UnitName unitName, TeachingPosType teachingPos,double offset = 0)
        {
            if (ConfigSet.Instance().Operation.VirtualMode)
                return true;

            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            var teachingInfo = inspModel.GetUnit(unitName).GetTeachingInfo(teachingPos);
            var movingParamX = teachingInfo.GetMovingParam(AxisName.X.ToString());

            Axis axisX = MotionManager.Instance().GetAxis(axisHandlerName, AxisName.X);
            if (axisX.IsEnable() == false)
            {
                string error = string.Format("AxisX Servo Off.");
                Logger.Write(LogType.Seq, error);
                return false;
            }
            
            if (MoveAxis(unitName, teachingPos, axisX, movingParamX, offset) == false)
            {
                string error = string.Format("Move To Axis X TimeOut!({0})", movingParamX.MovingTimeOut.ToString());
                Logger.Write(LogType.Seq, error);
                return false;
            }

            string message = string.Format("Move Completed.(Teaching Pos : {0})", teachingPos.ToString());
            Logger.Write(LogType.Seq, message);

            return true;
        }

        public bool MoveAxisX(Axis axisX, UnitName unitName, TeachingPosType teachingPos, double offset = 0)
        {
            if (ConfigSet.Instance().Operation.VirtualMode)
                return true;

            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            var teachingInfo = inspModel.GetUnit(unitName).GetTeachingInfo(teachingPos);
            var movingParamX = teachingInfo.GetMovingParam(AxisName.X.ToString());

            if (axisX == null)
                return false;

            if (axisX.IsEnable() == false)
            {
                string error = string.Format("AxisX Servo Off.");
                Logger.Write(LogType.Seq, error);
                return false;
            }

            if (MoveAxis(unitName, teachingPos, axisX, movingParamX, offset) == false)
            {
                string error = string.Format("Move To Axis X TimeOut!({0})", movingParamX.MovingTimeOut.ToString());
                Logger.Write(LogType.Seq, error);
                return false;
            }

            string message = string.Format("Move Completed.(Teaching Pos : {0})", teachingPos.ToString());
            Logger.Write(LogType.Seq, message);

            return true;
        }

        private bool MoveAxis(UnitName unitName, TeachingPosType teachingPos, Axis axis, AxisMovingParam movingParam, double offset = 0)
        {
            if (IsAxisInPosition(unitName, teachingPos, axis, offset) == false)
            {
                Stopwatch sw = new Stopwatch();
                sw.Restart();

                StartAbsoluteMove(unitName, teachingPos, axis, offset);

                while (IsAxisInPosition(unitName, teachingPos, axis, offset) == false)
                {
                    if (sw.ElapsedMilliseconds >= movingParam.MovingTimeOut)
                        return false;

                    Thread.Sleep(10);
                }
            }

            return true;
        }

        public bool IsEnable(AxisHandlerName axisHandlerName, AxisName axisName)
        {
            if (DeviceManager.Instance().MotionHandler.Count > 0)
            {
                var motion = DeviceManager.Instance().MotionHandler.First() as ACSMotion;

                if (motion != null)
                {
                    var axis = GetAxis(axisHandlerName, axisName);
                    return motion.IsEnable(axis.AxisNo);
                }

                return false;
            }

            return false;
        }

        public bool IsMoving(AxisHandlerName axisHandlerName, AxisName axisName)
        {
            if (DeviceManager.Instance().MotionHandler.Count > 0)
            {
                var motion = DeviceManager.Instance().MotionHandler.First() as ACSMotion;

                if (motion != null)
                {
                    var axis = GetAxis(axisHandlerName, axisName);
                    return motion.IsMoving(axis.AxisNo);
                }

                return false;
            }

            return false;
        }

        public bool MoveAxisZ(UnitName unitName, TeachingPosType teachingPos, LAFCtrl lafCtrl, AxisName axisNameZ)
        {
            if (ConfigSet.Instance().Operation.VirtualMode)
                return true;

            if (IsAxisZInPosition(unitName, teachingPos, lafCtrl, axisNameZ) == false)
            {
                Stopwatch sw = new Stopwatch();
                sw.Restart();

                var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

                var posData = inspModel.GetUnit(unitName).GetTeachingInfo(teachingPos);
                var targetPosition = posData.GetTargetPosition(axisNameZ);
                var AlignMovingParamZ = posData.GetMovingParam(axisNameZ.ToString());

                lafCtrl.SetMotionAbsoluteMove(targetPosition);
                Console.WriteLine(string.Format("Dove Done.{0}", axisNameZ.ToString()));
            }

            return true;
        }

        public bool IsAxisZInPosition(UnitName unitName, TeachingPosType teachingPosition, LAFCtrl lafCtrl, AxisName axisNameZ)
        {
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            var posData = inspModel.GetUnit(unitName).GetTeachingInfo(teachingPosition);
            var targetPosition = posData.GetTargetPosition(axisNameZ);
            var curPos = lafCtrl.Status.MPosPulse / lafCtrl.ResolutionAxisZ;

            if (Math.Abs(targetPosition - curPos) <= 0.001)
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

    public enum ACSGlobalVariable
    {
        AF_DIFF,        // Test#7 AF Difference
        AF_SW,          // Test#7 AF On/Off Switch
        AF_OFFSET,      // Test#7 AF Difference Offset
        AF_FACTOR,      // Test#7 AF Axis Gain Factor
        JUDGE_RANGE,    // Test#7 Judge Range (MSG dB Ret와 동일 기능)

        LASER_ENABLE_POS_0,
        LASER_DISABLE_POS_0,
        LASER_ENABLE_POS_1,
        LASER_DISABLE_POS_1,
        LASER_ENABLE_POS_2,
        LASER_DISABLE_POS_2,
        LASER_ENABLE_POS_3,
        LASER_DISABLE_POS_3,
        LASER_ENABLE_POS_4,
        LASER_DISABLE_POS_4,
        LASER_ENABLE_POS_5,
        LASER_DISABLE_POS_5,
        LASER_ENABLE_POS_6,
        LASER_DISABLE_POS_6,
        LASER_ENABLE_POS_7,
        LASER_DISABLE_POS_7,
        LASER_ENABLE_POS_8,
        LASER_DISABLE_POS_8,
        LASER_ENABLE_POS_9,
        LASER_DISABLE_POS_9,

        LASER_ENABLE_POS,
        LASER_DISABLE_POS
    }

    //public enum ACSBufferNumber
    //{
    //    Homing_Unit1 = 0,
    //    Homing_Unit2 = 1,
    //    Buffer2 = 2,
    //    Buffer3 = 3,
    //    CameraTrigger_Unit1 = 4,
    //    CameraTrigger_Unit2 = 5,
    //    LAFTrigger_Unit1 = 6,
    //    LAFTrigger_Unit2 = 7,
    //}
}
