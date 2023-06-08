using Cognex.VisionPro;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.Parameters;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.VisionTool
{
    public partial class MainAlgorithmTool : AlgorithmTool
    {
        public void MainMarkInspect(ICogImage cogImage, Tab tab, ref TabInspResult tabInspResult)
        {
            tabInspResult.FpcMark = RunFpcMark(cogImage, tab);
            tabInspResult.PanelMark = RunPanelMark(cogImage, tab);
        }

        public AlignResult RunMainLeftAlignX(ICogImage cogImage, Tab tab, double fpcTheta, double panelTheta, double judgementX)
        {
            var fpcParam = tab.GetAlignParam(ATTTabAlignName.LeftFPCX).DeepCopy();
            var panelParam = tab.GetAlignParam(ATTTabAlignName.LeftPanelX).DeepCopy();

            //var calcFpcRegion = CoordinateRectangle(fpcParam.CaliperParams.GetRegion() as CogRectangleAffine, fpcTheta);
            //var calcPanelRegion = CoordinateRectangle(panelParam.CaliperParams.GetRegion() as CogRectangleAffine, panelTheta);

            //fpcParam.CaliperParams.SetRegion(calcFpcRegion);
            //panelParam.CaliperParams.SetRegion(calcPanelRegion);

            var result = RunMainAlignX(cogImage, fpcParam, panelParam, judgementX);
            return result;
        }

        public AlignResult RunMainLeftAlignY(ICogImage cogImage, Tab tab, double fpcTheta, double panelTheta, double judgementY)
        {
            var fpcParam = tab.GetAlignParam(ATTTabAlignName.LeftFPCY).DeepCopy();
            var panelParam = tab.GetAlignParam(ATTTabAlignName.LeftPanelY).DeepCopy();

            //var calcFpcRegion = CoordinateRectangle(fpcParam.CaliperParams.GetRegion() as CogRectangleAffine, fpcTheta);
            //var calcPanelRegion = CoordinateRectangle(panelParam.CaliperParams.GetRegion() as CogRectangleAffine, panelTheta);

            //fpcParam.CaliperParams.SetRegion(calcFpcRegion);
            //panelParam.CaliperParams.SetRegion(calcPanelRegion);

            var result = RunMainAlignY(cogImage, fpcParam, panelParam, judgementY);
            return result;
        }

        public AlignResult RunMainRightAlignX(ICogImage cogImage, Tab tab, double fpcTheta, double panelTheta, double judgementX)
        {
            var fpcParam = tab.GetAlignParam(ATTTabAlignName.RightFPCX);
            var panelParam = tab.GetAlignParam(ATTTabAlignName.RightPanelX);

            //var calcFpcRegion = CoordinateRectangle(fpcParam.CaliperParams.GetRegion() as CogRectangleAffine, fpcTheta);
            //var calcPanelRegion = CoordinateRectangle(panelParam.CaliperParams.GetRegion() as CogRectangleAffine, panelTheta);

            //fpcParam.CaliperParams.SetRegion(calcFpcRegion);
            //panelParam.CaliperParams.SetRegion(calcPanelRegion);

            var result = RunMainAlignX(cogImage, fpcParam, panelParam, judgementX);

            return result;
        }

        public AlignResult RunMainRightAlignY(ICogImage cogImage, Tab tab, double fpcTheta, double panelTheta, double judgementY)
        {
            var fpcParam = tab.GetAlignParam(ATTTabAlignName.RightFPCY).DeepCopy();
            var panelParam = tab.GetAlignParam(ATTTabAlignName.RightPanelY).DeepCopy();

            //var calcFpcRegion = CoordinateRectangle(fpcParam.CaliperParams.GetRegion() as CogRectangleAffine, fpcTheta);
            //var calcPanelRegion = CoordinateRectangle(panelParam.CaliperParams.GetRegion() as CogRectangleAffine, panelTheta);

            //fpcParam.CaliperParams.SetRegion(calcFpcRegion);
            //panelParam.CaliperParams.SetRegion(calcPanelRegion);

            var result = RunMainAlignY(cogImage, fpcParam, panelParam, judgementY);
            return result;
        }

        public AlignResult RunMainAlignX(ICogImage cogImage, AlignParam fpcParam, AlignParam panelParam, double judegementX)
        {
            AlignResult result = new AlignResult();

            result.Panel = RunAlignX(cogImage, panelParam.CaliperParams, panelParam.LeadCount);
            result.Fpc = RunAlignX(cogImage, fpcParam.CaliperParams, fpcParam.LeadCount);

            if (result.Panel.Judgement == Judgement.OK && result.Fpc.Judgement == Judgement.OK)
            {
                List<float> intervalValueX = new List<float>();

                for (int i = 0; i < panelParam.LeadCount * 2; i += 2)
                {
                    var panelResult1 = result.Panel.CogAlignResult[i];
                    var panelResult2 = result.Panel.CogAlignResult[i + 1];

                    float panelCenterX = panelResult1.CaliperMatchList[0].FoundPos.X - panelResult2.CaliperMatchList[0].FoundPos.X;

                    var fpcResult1 = result.Fpc.CogAlignResult[i];
                    var fpcResult2 = result.Fpc.CogAlignResult[i + 1];

                    float fpcCenterX = fpcResult1.CaliperMatchList[0].FoundPos.X - fpcResult2.CaliperMatchList[0].FoundPos.X;

                    float interval = Math.Abs(panelCenterX - fpcCenterX);
                    intervalValueX.Add(Math.Abs(interval));
                }

                float max = intervalValueX.Max();
                float min = intervalValueX.Min();

                float temp = 0.0f;
                int count = 0;
                foreach (var value in intervalValueX)
                {
                    if (value == max || value == min)
                        continue;

                    temp += value;
                    count++;
                }

                result.ResultValue = temp / count;

                if (Math.Abs(result.ResultValue) <= judegementX)
                    result.Judgement = Judgement.OK;
                else
                    result.Judgement = Judgement.NG;
            }
            else
            {
                result.Judgement = Judgement.FAIL;
                string message = string.Format(" Main CaliperX Search Fail. Panel({0}), FPC({1})", panelParam.Name, fpcParam.Name);
                Logger.Debug(LogType.Inspection, message);
            }

            return result;
        }

        private AlignResult RunMainAlignY(ICogImage cogImage, AlignParam fpcParam, AlignParam panelParam, double judgementY)
        {
            AlignResult result = new AlignResult();

            result.Panel = RunAlignY(cogImage, panelParam.CaliperParams);
            result.Fpc = RunAlignY(cogImage, fpcParam.CaliperParams);

            if (result.Panel.Judgement == Judgement.OK && result.Fpc.Judgement == Judgement.OK)
            {
                float panelY = result.Panel.CogAlignResult[0].MaxCaliperMatch.FoundPos.Y;
                float fpcY = result.Fpc.CogAlignResult[0].MaxCaliperMatch.FoundPos.Y;

                float newY = panelY - fpcY;

                result.ResultValue = newY;

                if (Math.Abs(result.ResultValue) <= judgementY)
                    result.Judgement = Judgement.OK;
                else
                    result.Judgement = Judgement.NG;
            }
            else
            {
                string message = string.Format("Main CaliperY Search Fail. Panel({0}), FPC({1})", panelParam.Name, fpcParam.Name);
                Logger.Debug(LogType.Inspection, message);
                result.Judgement = Judgement.FAIL;
            }

            return result;
        }

        public MarkResult RunFpcMark(ICogImage cogImage, Tab tab)
        {
            MarkResult result = new MarkResult();

            foreach (MarkName markName in Enum.GetValues(typeof(MarkName)))
            {
                var leftParam = tab.GetFPCMark(MarkDirection.Left, markName);
                var rightParam = tab.GetFPCMark(MarkDirection.Right, markName);

                var leftResult = RunPatternMatch(cogImage, leftParam.InspParam);
                if (leftResult == null)
                    continue;

                var rightResult = RunPatternMatch(cogImage, rightParam.InspParam);
                if (rightResult == null)
                    continue;

                if (leftResult.Judgement == Judgement.OK && rightResult.Judgement == Judgement.OK)
                {
                    MarkMatchingResult matchingResult = new MarkMatchingResult();
                    matchingResult.Left = leftResult;
                    matchingResult.Right = rightResult;

                    result.FoundedMark = matchingResult;
                    result.Judgement = Judgement.OK;

                    return result;
                }
                else
                {
                    MarkMatchingResult matchingResult = new MarkMatchingResult();
                    matchingResult.Left = leftResult;
                    matchingResult.Right = rightResult;

                    result.FailMarks.Add(matchingResult);
                    result.Judgement = Judgement.NG;
                }
            }
            return result;
        }

        public MarkResult RunPanelMark(ICogImage cogImage, Tab tab)
        {
            MarkResult result = new MarkResult();

            foreach (MarkName markName in Enum.GetValues(typeof(MarkName)))
            {
                var leftParam = tab.GetPanelMark(MarkDirection.Left, markName);
                var rightParam = tab.GetPanelMark(MarkDirection.Right, markName);

                var leftResult = RunPatternMatch(cogImage, leftParam.InspParam);
                if (leftResult == null)
                    continue;

                var rightResult = RunPatternMatch(cogImage, rightParam.InspParam);
                if (rightResult == null)
                    continue;

                if (leftResult.Judgement == Judgement.OK && rightResult.Judgement == Judgement.OK)
                {
                    MarkMatchingResult matchingResult = new MarkMatchingResult();
                    matchingResult.Left = leftResult;
                    matchingResult.Right = rightResult;

                    result.FoundedMark = matchingResult;
                    result.Judgement = Judgement.OK;
                    return result;
                }
                else
                {
                    MarkMatchingResult matchingResult = new MarkMatchingResult();
                    matchingResult.Left = leftResult;
                    matchingResult.Right = rightResult;

                    result.FailMarks.Add(matchingResult);
                    result.Judgement = Judgement.NG;
                }
            }
            return result;
        }

    }

    public partial class MainAlgorithmTool : AlgorithmTool
    {
        public class Coordinate
        {
            public PointF TeachedCenterPoint { get; private set; } = new PointF();

            public PointF SearchedCenterPoint { get; private set; } = new PointF();

            public double DiffRadian { get; private set; } = 0.0;

            public PointF OffsetPoint { get; private set; } = new PointF();

            public void SetCoordinateParam(PointF teachedLeftPoint, PointF teachedRightPoint, PointF searchedLeftPoint, PointF searchedRightPoint)
            {
                SetTeachedCenterPoint(teachedLeftPoint, teachedRightPoint);

                SetSearchedCenterPoint(searchedLeftPoint, searchedRightPoint);

                SetDiffAngle(teachedLeftPoint, teachedRightPoint, searchedLeftPoint, searchedRightPoint);

                PointF teachedCenterPoint = GetTeachedCenterPoint();
                PointF searchedCenterPoint = GetSearchedCenterPoint();

                SetOffsetPoint(teachedCenterPoint, searchedCenterPoint);
            }

            private void SetTeachedCenterPoint(PointF teachedLeftPoint, PointF teachedRightPoint)
            {
                TeachedCenterPoint = MathHelper.GetCenterPoint(teachedLeftPoint, teachedRightPoint);
            }

            public PointF GetTeachedCenterPoint()
            {
                return TeachedCenterPoint;
            }

            private void SetSearchedCenterPoint(PointF searchedLeftPoint, PointF searchedRightPoint)
            {
                SearchedCenterPoint = MathHelper.GetCenterPoint(searchedLeftPoint, searchedRightPoint);
            }

            public PointF GetSearchedCenterPoint()
            {
                return SearchedCenterPoint;
            }

            private void SetDiffAngle(PointF teachedLeftPoint, PointF teachedRightPoint, PointF searchedLeftPoint, PointF searchedRightPoint)
            {
                double teachedRadian = MathHelper.GetRadian(teachedLeftPoint, teachedRightPoint);
                if (teachedRadian > 180.0)
                    teachedRadian -= 360.0;

                double searchedRadian = MathHelper.GetRadian(searchedLeftPoint, searchedRightPoint);
                if (searchedRadian > 180.0)
                    searchedRadian -= 360.0;

                DiffRadian = searchedRadian - teachedRadian;
            }

            public double GetDiffRadian()
            {
                return DiffRadian;
            }

            private void SetOffsetPoint(PointF teachedCenterPoint, PointF searchedCenterPoint)
            {
                OffsetPoint = MathHelper.GetOffset(teachedCenterPoint, searchedCenterPoint);
            }

            public PointF GetOffsetPoint()
            {
                return OffsetPoint;
            }

            public PointF GetCoordinate(PointF inputPoint)
            {
                PointF searchedCenterPoint = GetSearchedCenterPoint();
                double diffRadian = GetDiffRadian();
                PointF offsetPoint = GetOffsetPoint();

                return MathHelper.GetCoordinate(searchedCenterPoint, diffRadian, offsetPoint, inputPoint);
            }
        }

        public CogRectangleAffine CoordinateRectangle(CogRectangleAffine originRegion, PointF referenceLeft, PointF referenceRight, PointF searchedLeft, PointF searchedRight)
        {
            CogRectangleAffine roi = new CogRectangleAffine(originRegion);

            PointF inputPoint = new PointF();
            inputPoint.X = (float)roi.CenterX;
            inputPoint.Y = (float)roi.CenterY;

            var newPoint = MathHelper.GetCoordinate(referenceLeft, referenceRight, searchedLeft, searchedRight, inputPoint);

            roi.CenterX = newPoint.X;
            roi.CenterY = newPoint.Y;

            return roi;
        }

        public CogRectangleAffine CalcTheta(CogRectangleAffine orginRegion, double theta)
        {
            PointF orginPoint = new PointF((float)orginRegion.CornerOriginX, (float)orginRegion.CornerOriginY);
            PointF corner1Point = new PointF((float)orginRegion.CornerXX, (float)orginRegion.CornerXX);
            PointF corner2Point = new PointF((float)orginRegion.CornerYX, (float)orginRegion.CornerYY);

            CogRectangleAffine roi = new CogRectangleAffine();

            double originX = orginPoint.X;
            double originY = orginPoint.Y;
            double cornerXX = corner1Point.X;
            double cornerXY = corner1Point.Y;
            double cornerYX = corner2Point.X;
            double cornerYY = corner2Point.Y;

            roi.SetOriginCornerXCornerY(originX, originY, cornerXX, cornerXY, cornerYX, cornerYY);

            return roi;
        }
    }
}
