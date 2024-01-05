using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Config;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Winform;
using System.Collections.Generic;

namespace Jastech.Apps.Winform
{
    public class ACSBufferManager
    {
        #region 필드
        private static ACSBufferManager _instance = null;

        private bool _activeLafTrigger { get; set; } = false;
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

                if(AppsConfig.Instance().EnableLafTrigger)
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
            if (AppsConfig.Instance().EnableLafTrigger == false)
                return;

            if (DeviceManager.Instance().MotionHandler.Count > 0)
            {
                var motion = DeviceManager.Instance().MotionHandler.First() as ACSMotion;
                var config = ACSBufferConfig.Instance();
                int tabMaxCount = AppsConfig.Instance().TabMaxCount;

                foreach (var buffer in ACSBufferConfig.Instance().LafTriggerBufferList)
                {
                    motion?.WriteRealVariable(config.IoEnableModeName, (int)IoEnableMode.Off, buffer.LafArrayIndex, buffer.LafArrayIndex);
                    motion?.WriteRealVariable(config.IoAddrName, buffer.OutputBit, buffer.LafArrayIndex, buffer.LafArrayIndex);

                    for (int tabNum = 0; tabNum < tabMaxCount; tabNum++)
                    {
                        motion?.WriteRealVariable(config.IoPositionUsagesName, 0, buffer.LafArrayIndex, buffer.LafArrayIndex, tabNum, tabNum);
                        motion?.WriteRealVariable(config.LaserStartPositionsName, 0, buffer.LafArrayIndex, buffer.LafArrayIndex, tabNum, tabNum);
                        motion?.WriteRealVariable(config.LaserEndPositionsName, 0, buffer.LafArrayIndex, buffer.LafArrayIndex, tabNum, tabNum);
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

        public void SetAutoMode(string targetAFName = "")
        {
            if (AppsConfig.Instance().EnableLafTrigger == false)
                return;

            if (DeviceManager.Instance().MotionHandler.Count > 0)
            {
                var motion = DeviceManager.Instance().MotionHandler.First() as ACSMotion;
                var config = ACSBufferConfig.Instance();

                foreach (var buffer in ACSBufferConfig.Instance().LafTriggerBufferList)
                {
                    if(targetAFName == "")
                    {
                        if (AppsConfig.Instance().EnableLafTriggerAutoMode)
                            motion?.WriteRealVariable(config.IoEnableModeName, (int)IoEnableMode.Auto, buffer.LafArrayIndex, buffer.LafArrayIndex);
                        else
                            motion?.WriteRealVariable(config.IoEnableModeName, (int)IoEnableMode.On, buffer.LafArrayIndex, buffer.LafArrayIndex);
                    }
                    else
                    {
                        if(buffer.LafName == targetAFName)
                            motion?.WriteRealVariable(config.IoEnableModeName, (int)IoEnableMode.Auto, buffer.LafArrayIndex, buffer.LafArrayIndex);
                        else
                            motion?.WriteRealVariable(config.IoEnableModeName, (int)IoEnableMode.Off, buffer.LafArrayIndex, buffer.LafArrayIndex);
                    }
                }
            }
        }

        public void SetStopMode()
        {
            if (AppsConfig.Instance().EnableLafTrigger == false)
                return;

            if (DeviceManager.Instance().MotionHandler.Count > 0)
            {
                var motion = DeviceManager.Instance().MotionHandler.First() as ACSMotion;
                var config = ACSBufferConfig.Instance();

                foreach (var buffer in ACSBufferConfig.Instance().LafTriggerBufferList)
                    motion?.WriteRealVariable(config.IoEnableModeName, (int)IoEnableMode.Off, buffer.LafArrayIndex, buffer.LafArrayIndex);
            }
        }

        public void SetManualMode(string lafName)
        {
            if (AppsConfig.Instance().EnableLafTrigger == false)
                return;

            SetStopMode();

            var triggerBuffer = ACSBufferConfig.Instance().GetTriggerBuffer(lafName);

            if (DeviceManager.Instance().MotionHandler.Count > 0)
            {
                var motion = DeviceManager.Instance().MotionHandler.First() as ACSMotion;
                var config = ACSBufferConfig.Instance();

                motion?.WriteRealVariable(config.IoEnableModeName, (int)IoEnableMode.On, triggerBuffer.LafArrayIndex, triggerBuffer.LafArrayIndex);
            }
        }

        public void EnableTabCount(string lafName, int tabCount)
        {
            if (AppsConfig.Instance().EnableLafTrigger == false)
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
                        motion?.WriteRealVariable(config.IoPositionUsagesName, 1, triggerBuffer.LafArrayIndex, triggerBuffer.LafArrayIndex, tabNum, tabNum);
                    else
                        motion?.WriteRealVariable(config.IoPositionUsagesName, 0, triggerBuffer.LafArrayIndex, triggerBuffer.LafArrayIndex, tabNum, tabNum);
                }
            }
        }

        private void SetTriggerPosition(string lafName, List<IoPositionData> positionList)
        {
            if (AppsConfig.Instance().EnableLafTrigger == false)
                return;

            EnableTabCount(lafName, positionList.Count);

            if (DeviceManager.Instance().MotionHandler.Count > 0)
            {
                var triggerBuffer = ACSBufferConfig.Instance().GetTriggerBuffer(lafName);
                var motion = DeviceManager.Instance().MotionHandler.First() as ACSMotion;
                var config = ACSBufferConfig.Instance();

                int lafIndex = triggerBuffer.LafArrayIndex;
                for (int tabNum = 0; tabNum < positionList.Count; tabNum++)
                {
                    motion?.WriteRealVariable(config.LaserStartPositionsName, positionList[tabNum].Start, lafIndex, lafIndex, tabNum, tabNum);
                    motion?.WriteRealVariable(config.LaserEndPositionsName, positionList[tabNum].End, lafIndex, lafIndex, tabNum, tabNum);
                }
            }
        }

        public void SetLafTriggerPosition(UnitName unitName, string lafName, List<TabScanBuffer> tabScanBufferList, bool useAlignCam, double offset = 0)
        {
            if (ConfigSet.Instance().Operation.VirtualMode || AppsConfig.Instance().EnableLafTrigger == false)
                return;

            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            var unit = inspModel.GetUnit(unitName);

            SetLafTriggerPosition(unit, lafName, tabScanBufferList, useAlignCam, offset);
        }

        public void SetLafTriggerPosition(Unit unit, string lafName, List<TabScanBuffer> tabScanBufferList, bool useAlignCam, double offset = 0)
        {
            if (ConfigSet.Instance().Operation.VirtualMode || AppsConfig.Instance().EnableLafTrigger == false)
                return;

            var camera = DeviceManager.Instance().CameraHandler.First();
            float resolution_um = camera.PixelResolution_um / camera.LensScale;

            var posData = unit.GetTeachingInfo(TeachingPosType.Stage1_Scan_Start);
            double teachingStartPos = posData.GetTargetPosition(AxisName.X) + offset;

            float subImageSize = (resolution_um * camera.ImageHeight) / 1000.0F;

            List<IoPositionData> dataList = new List<IoPositionData>();

            foreach (var scanBuffer in tabScanBufferList)
            {
                double tempStart = teachingStartPos + (scanBuffer.StartIndex * subImageSize);
                double tempEnd = teachingStartPos + ((scanBuffer.EndIndex + 1) * subImageSize);

                var triggerOffset = unit.GetTab(scanBuffer.TabNo).GetTriggerOffsetParameter(useAlignCam);
                float afLeftOffset = triggerOffset.Left;
                float afRightOffset = triggerOffset.Right;

                IoPositionData data = new IoPositionData
                {
                    Start = tempStart + afLeftOffset,
                    End = tempEnd + afRightOffset,
                };

                dataList.Add(data);
            }
            dataList.Sort((x, y) => x.Start.CompareTo(y.Start));

            SetTriggerPosition(lafName, dataList);
            //System.Threading.Thread.Sleep(100);
        }
        #endregion
    }
}
