using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Service.Plc.Maps;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Winform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform
{
    public class AppsDeviceMonitor
    {
        public AxisStatus AxisStatus { get; set; } = new AxisStatus();

        public Task MonitoringTask { get; set; } = null;

        public CancellationTokenSource CancelMonitoringTaskToken { get; set; }

        private static AppsDeviceMonitor _instance = null;

        public static AppsDeviceMonitor Instance()
        {
            if (_instance == null)
                _instance = new AppsDeviceMonitor();

            return _instance;
        }

        public void Initialize()
        {
            StartMachineStatus();
        }

        public void Release()
        {
            StopMachineStatus();
        }

        private void StartMachineStatus()
        {
            if (MonitoringTask != null)
                return;
            CancelMonitoringTaskToken = new CancellationTokenSource();
            MonitoringTask = new Task(MachineTaskAction, CancelMonitoringTaskToken.Token);
            MonitoringTask.Start();
        }

        private void StopMachineStatus()
        {
            CancelMonitoringTaskToken.Cancel();
            MonitoringTask = null;
        }

        private void MachineTaskAction()
        {
            while (true)
            {
                if (CancelMonitoringTaskToken.IsCancellationRequested)
                    return;

                lock(AxisStatus)
                {
                    AxisStatus.IsMovingAxisX = IsMovingAxisX();
                    AxisStatus.CurrentAxisXPosition = GetCurrentAxisXPosition();
                    AxisStatus.PlcAxisXData = GetPlcPosXData(UnitName.Unit0);
                }

                Thread.Sleep(50);
            }
        }

        private bool IsMovingAxisX()
        {
            if (DeviceManager.Instance().MotionHandler.Count > 0)
            {
                var motion = DeviceManager.Instance().MotionHandler.First() as ACSMotion;

                if(motion != null)
                {
                    var axis = MotionManager.Instance().GetAxis(AxisHandlerName.Handler0, AxisName.X);
                    return motion.IsMoving(axis.AxisNo);
                }
                return false;
            }

            return false;
        }

        private int GetPlcPosXData(UnitName unitName)
        {
            if (ModelManager.Instance().CurrentModel is AppsInspModel inspModel)
            {
                var manager = MotionManager.Instance();
                var axis = manager.GetAxis(AxisHandlerName.Handler0, AxisName.X);

                foreach (TeachingPosType posType in Enum.GetValues(typeof(TeachingPosType)))
                {
                    if(inspModel.GetUnit(unitName) is Unit unit)
                    {
                        var teachingInfo = unit.GetTeachingInfo(posType);
                        if (teachingInfo != null)
                        {
                            bool inPosition = manager.IsAxisInPosition(unitName, posType, axis);
                            if (inPosition)
                                return (int)ConvertToPlcCommand(posType);
                        }
                    }
                   
                }
            }
            return 0;
        }

        private double GetCurrentAxisXPosition()
        {
            Axis axisX = MotionManager.Instance().GetAxis(AxisHandlerName.Handler0, AxisName.X);
            return axisX.GetActualPosition();
        }

        private PlcCommand ConvertToPlcCommand(TeachingPosType posType)
        {
            if (posType == TeachingPosType.Standby)
                return PlcCommand.Move_StandbyPos;
            else if (posType == TeachingPosType.Stage1_PreAlign_Left)
                return PlcCommand.Move_Left_AlignPos;
            else if (posType == TeachingPosType.Stage1_PreAlign_Right)
                return PlcCommand.Move_Right_AlignPos;
            else if (posType == TeachingPosType.Stage1_Scan_Start)
                return PlcCommand.Move_ScanStartPos;
            else if (posType == TeachingPosType.Stage1_Scan_End)
                return PlcCommand.Move_ScanEndPos;
            else
                return PlcCommand.None;
        }
    }

    public class AxisStatus
    {
        public bool IsMovingAxisX { get; set; } = false;

        public double CurrentAxisXPosition { get; set; } = 0.0;

        public int PlcAxisXData { get; set; } = 0;
    }
}
