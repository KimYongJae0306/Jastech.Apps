using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Config;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Winform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform
{
    public class ACSBufferManager
    {
        #region 필드
        private static ACSBufferManager _instance = null;

        private bool _activeLafTrigger { get; set; } = false;
        #endregion

        #region 속성
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        #endregion

        #region 메서드
        public static ACSBufferManager Instance()
        {
            if (_instance == null)
            {
                _instance = new ACSBufferManager();
            }

            return _instance;
        }

        public void Initialize()
        {
            if (DeviceManager.Instance().MotionHandler.Count > 0)
            {
                var motion = DeviceManager.Instance().MotionHandler.First() as ACSMotion;
                int index = ACSBufferConfig.Instance().CameraTrigger;
                motion?.RunBuffer(index);

                if(AppsConfig.Instance().UseLafTrigger)
                {
                    _activeLafTrigger = true;

                    ConstructBuffer();
                    SetStopMode();

                    foreach (var trigger in ACSBufferConfig.Instance().LafTriggerBufferList)
                        motion?.RunBuffer(trigger.BufferNumber);
                }
            }
        }

        private void ConstructBuffer()
        {
            if (AppsConfig.Instance().UseLafTrigger == false)
                return;

            if (DeviceManager.Instance().MotionHandler.Count > 0)
            {
                var motion = DeviceManager.Instance().MotionHandler.First() as ACSMotion;
                var config = ACSBufferConfig.Instance();
                int tabMaxCount = AppsConfig.Instance().TabMaxCount;

                foreach (var buffer in ACSBufferConfig.Instance().LafTriggerBufferList)
                {
                    motion?.WriteRealVariable(config.IoEnableModeName, (int)IoEnableMode.Off, buffer.LafIndex, buffer.LafIndex);

                    for (int tabNum = 0; tabNum < tabMaxCount; tabNum++)
                    {
                        motion?.WriteRealVariable(config.IoPositionUsagesName, 0, buffer.LafIndex, buffer.LafIndex, tabNum, tabNum);
                        motion?.WriteRealVariable(config.LaserStartPositionsName, 0, buffer.LafIndex, buffer.LafIndex, tabNum, tabNum);
                        motion?.WriteRealVariable(config.LaserEndPositionsName, 0, buffer.LafIndex, buffer.LafIndex, tabNum, tabNum);
                    }
                }
            }
        }

        public void Release()
        {
            if (DeviceManager.Instance().MotionHandler.Count > 0)
            {
                var motion = DeviceManager.Instance().MotionHandler.First() as ACSMotion;
                int index = ACSBufferConfig.Instance().CameraTrigger;
                motion?.StopBuffer(index);

                if(_activeLafTrigger)
                {
                    foreach (var trigger in ACSBufferConfig.Instance().LafTriggerBufferList)
                        motion?.StopBuffer(trigger.BufferNumber);
                }
            }
        }

        public void SetAutoMode()
        {
            if (AppsConfig.Instance().UseLafTrigger == false)
                return;

            if (DeviceManager.Instance().MotionHandler.Count > 0)
            {
                var motion = DeviceManager.Instance().MotionHandler.First() as ACSMotion;
                var config = ACSBufferConfig.Instance();

                foreach (var buffer in ACSBufferConfig.Instance().LafTriggerBufferList)
                    motion?.WriteRealVariable(config.IoEnableModeName, (int)IoEnableMode.Auto, buffer.LafIndex, buffer.LafIndex);
            }
        }

        public void SetStopMode()
        {
            if (AppsConfig.Instance().UseLafTrigger == false)
                return;

            if (DeviceManager.Instance().MotionHandler.Count > 0)
            {
                var motion = DeviceManager.Instance().MotionHandler.First() as ACSMotion;
                var config = ACSBufferConfig.Instance();

                foreach (var buffer in ACSBufferConfig.Instance().LafTriggerBufferList)
                    motion?.WriteRealVariable(config.IoEnableModeName, (int)IoEnableMode.Off, buffer.LafIndex, buffer.LafIndex);
            }
        }

        public void SetManualMode(string lafName)
        {
            if (AppsConfig.Instance().UseLafTrigger == false)
                return;

            SetStopMode();

            var triggerBuffer = ACSBufferConfig.Instance().GetTriggerBuffer(lafName);

            if (DeviceManager.Instance().MotionHandler.Count > 0)
            {
                var motion = DeviceManager.Instance().MotionHandler.First() as ACSMotion;
                var config = ACSBufferConfig.Instance();

                motion?.WriteRealVariable(config.IoEnableModeName, (int)IoEnableMode.On, triggerBuffer.LafIndex, triggerBuffer.LafIndex);
            }
        }

        public void EnableTabCount(string lafName, int tabCount)
        {
            if (AppsConfig.Instance().UseLafTrigger == false)
                return;

            if (DeviceManager.Instance().MotionHandler.Count > 0)
            {
                var triggerBuffer = ACSBufferConfig.Instance().GetTriggerBuffer(lafName);
                var motion = DeviceManager.Instance().MotionHandler.First() as ACSMotion;
                var config = ACSBufferConfig.Instance();

                int tabMaxCount = AppsConfig.Instance().TabMaxCount;

                for (int tabNum = 0; tabNum < tabMaxCount; tabNum++)
                {
                    if (tabNum < tabCount)
                        motion?.WriteRealVariable(config.IoPositionUsagesName, 1, triggerBuffer.LafIndex, triggerBuffer.LafIndex, tabNum, tabNum);
                    else
                        motion?.WriteRealVariable(config.IoPositionUsagesName, 0, triggerBuffer.LafIndex, triggerBuffer.LafIndex, tabNum, tabNum);
                }
            }
        }

        private void SetTriggerPosition(string lafName, List<IoPositionData> positionList)
        {
            if (AppsConfig.Instance().UseLafTrigger == false)
                return;

            EnableTabCount(lafName, positionList.Count);

            if (DeviceManager.Instance().MotionHandler.Count > 0)
            {
                var triggerBuffer = ACSBufferConfig.Instance().GetTriggerBuffer(lafName);
                var motion = DeviceManager.Instance().MotionHandler.First() as ACSMotion;
                var config = ACSBufferConfig.Instance();

                int tabMaxCount = AppsConfig.Instance().TabMaxCount;
                int lafIndex = triggerBuffer.LafIndex;
                for (int tabNum = 0; tabNum < positionList.Count; tabNum++)
                {
                    motion?.WriteRealVariable(config.LaserStartPositionsName, positionList[tabNum].Start, lafIndex, lafIndex, tabNum, tabNum);
                    motion?.WriteRealVariable(config.LaserEndPositionsName, positionList[tabNum].End, lafIndex, lafIndex, tabNum, tabNum);
                }
            }
        }

        public void SetLafTriggerPosition(string lafName, List<TabScanBuffer> tabScanBufferList, double offset = 0)
        {
            if (ConfigSet.Instance().Operation.VirtualMode || AppsConfig.Instance().UseLafTrigger == false)
                return;

            var camera = DeviceManager.Instance().CameraHandler.First();
            double resolution_um = camera.PixelResolution_um / camera.LensScale;

            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            var posData = inspModel.GetUnit(UnitName.Unit0).GetTeachingInfo(TeachingPosType.Stage1_Scan_Start);
            double teachingStartPos = posData.GetTargetPosition(AxisName.X) + offset;

            double subImageSize = resolution_um * camera.ImageHeight;

            List<IoPositionData> dataList = new List<IoPositionData>();

            foreach (var scanBuffer in tabScanBufferList)
            {
                double tempStart = teachingStartPos + (scanBuffer.StartIndex * subImageSize);
                double tempEnd = teachingStartPos + ((scanBuffer.EndIndex + 1) * subImageSize);

                IoPositionData data = new IoPositionData
                {
                    Start = tempStart,
                    End = tempEnd,
                };

                dataList.Add(data);
            }
            dataList.Sort((x, y) => x.Start.CompareTo(y.Start));

            SetTriggerPosition(lafName, dataList);
        }
        #endregion
    }
}
