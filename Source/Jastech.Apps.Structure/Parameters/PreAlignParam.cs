using Jastech.Apps.Structure.Data;
using Jastech.Framework.Device.LightCtrls;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.Parameters
{
    public class PreAlignParam
    {
        [JsonProperty]
        public MarkDirection Direction { get; set; } = MarkDirection.Left;

        [JsonProperty]
        public MarkName Name { get; set; } = MarkName.Main;

        [JsonProperty]
        public VisionProPatternMatchingParam InspParam { get; set; } = new VisionProPatternMatchingParam();

        [JsonProperty]
        public List<LightParameter> LightParams { get; set; } = null;

        [JsonProperty]
        public int ExposureTime_us { get; set; } = 5000;

        [JsonProperty]
        public double AnalogGain_dB { get; set; } = 0;

        public double MotionX { get; set; } = 0.0;

        public double MotionY { get; set; } = 0.0;

        public double MotionT { get; set; } = 0.0;

        public double OffsetX { get; set; } = 0.0;

        public double OffsetY { get; set; } = 0.0;

        public double OffsetT { get; set; } = 0.0;

        public PreAlignParam DeepCopy()
        {
            PreAlignParam preAlign = new PreAlignParam();

            preAlign.Name = Name;
            preAlign.Direction = Direction;

            if (InspParam != null)
                preAlign.InspParam = InspParam.DeepCopy();

            if (preAlign.LightParams != null)
                preAlign.LightParams = LightParams.Select(x => x.DeepCopy()).ToList();

            preAlign.ExposureTime_us = ExposureTime_us;
            preAlign.AnalogGain_dB = AnalogGain_dB;

            return preAlign;
        }

        public void Dispose()
        {
            InspParam.Dispose();
        }

        public void SetMotionData(double motionX, double motionY, double motionT)
        {
            MotionX = motionX;
            MotionY = motionY;
            MotionT = motionT;
        }

        public double GetMotionData(AxisName axisName)
        {
            double position = 0.0;

            switch (axisName)
            {
                case AxisName.X:
                    position = MotionX;
                    break;
                case AxisName.Y:
                    position = MotionY;
                    break;
                case AxisName.Z:
                    break;
                case AxisName.Z1:
                    break;
                case AxisName.Z2:
                    break;
                case AxisName.T:
                    position = MotionT;
                    break;
                default:
                    break;
            }

            return position;
        }

        public void SetPreAlignOffset(double offsetX, double offsetY, double offsetT)
        {
            OffsetX = offsetX;
            OffsetY = offsetY;
            OffsetT = offsetT;
        }

        public double GetPreAlignOffset(AxisName axisName)
        {
            double offset = 0.0;

            switch (axisName)
            {
                case AxisName.X:
                    offset = OffsetX;
                    break;
                case AxisName.Y:
                    offset = OffsetY;
                    break;
                case AxisName.Z:
                    break;
                case AxisName.Z1:
                    break;
                case AxisName.Z2:
                    break;
                case AxisName.T:
                    offset = OffsetT;
                    break;
                default:
                    break;
            }

            return offset;
        }
    }
}
