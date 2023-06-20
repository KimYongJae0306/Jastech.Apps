using Cognex.VisionPro;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.Parameters;
using Jastech.Framework.Algorithms.Akkon.Results;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
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
        public void ExecuteAlignment(ref AppsInspResult inspResult)
        {
            PointF leftMark = inspResult.PreAlignResult.FoundedMark.Left.MaxMatchPos.FoundPos;
            PointF rightMark = inspResult.PreAlignResult.FoundedMark.Right.MaxMatchPos.FoundPos;

            //double dx = 
        }

        public void RunPreAlign(ref AppsInspResult inspResult)
        {
            if (inspResult.PreAlignResult.FoundedMark.Left.Judgement == Judgement.OK && inspResult.PreAlignResult.FoundedMark.Right.Judgement == Judgement.OK)
                inspResult.PreAlignResult.Judgement = Judgement.OK;
            else
                inspResult.PreAlignResult.Judgement = Judgement.NG;
        }

        public void MainMarkInspect(ICogImage cogImage, Tab tab, ref TabInspResult tabInspResult)
        {
            tabInspResult.FpcMark = RunFpcMark(cogImage, tab);
            tabInspResult.PanelMark = RunPanelMark(cogImage, tab);
        }

        public AlignResult RunMainLeftAlignX(ICogImage cogImage, Tab tab, Coordinate fpcCoordinate, Coordinate panelCoordinate, double judgementX)
        {
            var fpcParam = tab.GetAlignParam(ATTTabAlignName.LeftFPCX).DeepCopy();
            var panelParam = tab.GetAlignParam(ATTTabAlignName.LeftPanelX).DeepCopy();

            if (fpcCoordinate != null && panelCoordinate != null)
            {
                var calcFpcRegion = CoordinateRectangle(fpcParam.CaliperParams.GetRegion() as CogRectangleAffine, fpcCoordinate);
                var calcPanelRegion = CoordinateRectangle(panelParam.CaliperParams.GetRegion() as CogRectangleAffine, panelCoordinate);

                fpcParam.CaliperParams.SetRegion(calcFpcRegion);
                panelParam.CaliperParams.SetRegion(calcPanelRegion);
            }

            var result = RunMainAlignX(cogImage, fpcParam, panelParam, judgementX);
            return result;
        }

        public AlignResult RunMainLeftAlignY(ICogImage cogImage, Tab tab, Coordinate fpcCoordinate, Coordinate panelCoordinate, double judgementY)
        {
            var fpcParam = tab.GetAlignParam(ATTTabAlignName.LeftFPCY).DeepCopy();
            var panelParam = tab.GetAlignParam(ATTTabAlignName.LeftPanelY).DeepCopy();

            if (fpcCoordinate != null && panelCoordinate != null)
            {
                var calcFpcRegion = CoordinateRectangle(fpcParam.CaliperParams.GetRegion() as CogRectangleAffine, fpcCoordinate);
                var calcPanelRegion = CoordinateRectangle(panelParam.CaliperParams.GetRegion() as CogRectangleAffine, panelCoordinate);

                fpcParam.CaliperParams.SetRegion(calcFpcRegion);
                panelParam.CaliperParams.SetRegion(calcPanelRegion);
            }

            var result = RunMainAlignY(cogImage, fpcParam, panelParam, judgementY);
            return result;
        }

        public AlignResult RunMainRightAlignX(ICogImage cogImage, Tab tab, Coordinate fpcCoordinate, Coordinate panelCoordinate, double judgementX)
        {
            var fpcParam = tab.GetAlignParam(ATTTabAlignName.RightFPCX);
            var panelParam = tab.GetAlignParam(ATTTabAlignName.RightPanelX);

            if (fpcCoordinate != null && panelCoordinate != null)
            {
                var calcFpcRegion = CoordinateRectangle(fpcParam.CaliperParams.GetRegion() as CogRectangleAffine, fpcCoordinate);
                var calcPanelRegion = CoordinateRectangle(panelParam.CaliperParams.GetRegion() as CogRectangleAffine, panelCoordinate);

                fpcParam.CaliperParams.SetRegion(calcFpcRegion);
                panelParam.CaliperParams.SetRegion(calcPanelRegion);
            }

            var result = RunMainAlignX(cogImage, fpcParam, panelParam, judgementX);

            return result;
        }

        public AlignResult RunMainRightAlignY(ICogImage cogImage, Tab tab, Coordinate fpcCoordinate, Coordinate panelCoordinate, double judgementY)
        {
            var fpcParam = tab.GetAlignParam(ATTTabAlignName.RightFPCY).DeepCopy();
            var panelParam = tab.GetAlignParam(ATTTabAlignName.RightPanelY).DeepCopy();

            if (fpcCoordinate != null && panelCoordinate != null)
            {
                var calcFpcRegion = CoordinateRectangle(fpcParam.CaliperParams.GetRegion() as CogRectangleAffine, fpcCoordinate);
                var calcPanelRegion = CoordinateRectangle(panelParam.CaliperParams.GetRegion() as CogRectangleAffine, panelCoordinate);

                fpcParam.CaliperParams.SetRegion(calcFpcRegion);
                panelParam.CaliperParams.SetRegion(calcPanelRegion);
            }

            var result = RunMainAlignY(cogImage, fpcParam, panelParam, judgementY);
            return result;
        }

        public AlignResult RunMainAlignX(ICogImage cogImage, AlignParam fpcParam, AlignParam panelParam, double judegementX)
        {
            AlignResult result = new AlignResult();

            result.Panel = RunAlignX(cogImage, panelParam.CaliperParams, panelParam.LeadCount);
            result.Fpc = RunAlignX(cogImage, fpcParam.CaliperParams, fpcParam.LeadCount);

            List<float> panelCenterXList = new List<float>();
            List<float> fpcCenterXList = new List<float>();

            if (result.Panel.Judgement == Judgement.OK && result.Fpc.Judgement == Judgement.OK)
            {
                List<float> intervalValueX = new List<float>();

                for (int i = 0; i < panelParam.LeadCount * 2; i += 2)
                {
                    if(i >= result.Panel.CogAlignResult.Count || i >= result.Fpc.CogAlignResult.Count)
                        continue;

                    var panelResult1 = result.Panel.CogAlignResult[i];
                    var panelResult2 = result.Panel.CogAlignResult[i + 1];

                    float panelInterval = Math.Abs(panelResult1.CaliperMatchList[0].FoundPos.X - panelResult2.CaliperMatchList[0].FoundPos.X);
                    float panelCenterX = panelResult1.CaliperMatchList[0].FoundPos.X + (panelInterval / 2.0f);
                    panelCenterXList.Add(panelCenterX);

                    var fpcResult1 = result.Fpc.CogAlignResult[i];
                    var fpcResult2 = result.Fpc.CogAlignResult[i + 1];

                    float fpcInterval = Math.Abs(fpcResult1.CaliperMatchList[0].FoundPos.X - fpcResult2.CaliperMatchList[0].FoundPos.X);
                    float fpcCenterX = fpcResult1.CaliperMatchList[0].FoundPos.X + (fpcInterval / 2.0f);
                    fpcCenterXList.Add(fpcCenterX);

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

                result.ResultValue_pixel = temp / count;
                result.AvgCenterX = GetCenterX(panelCenterXList);

                if (Math.Abs(result.ResultValue_pixel) <= judegementX)
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

        private float GetCenterX(List<float> dataList)
        {
            float max = dataList.Max();
            float min = dataList.Min();

            float temp = 0.0f;
            int count = 0;
            foreach (var value in dataList)
            {
                if (value == max || value == min)
                    continue;

                temp += value;
                count++;
            }

            return temp / count;
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

                result.ResultValue_pixel = newY;

                if (Math.Abs(result.ResultValue_pixel) <= judgementY)
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
        public Coordinate Coordinate { get; set; } = new Coordinate();

        public CogRectangleAffine CoordinateRectangle(CogRectangleAffine originRegion, Coordinate coordinate)
        {
            CogRectangleAffine roi = new CogRectangleAffine(originRegion);

            PointF inputPoint = new PointF();
            inputPoint.X = (float)roi.CenterX;
            inputPoint.Y = (float)roi.CenterY;

            var newPoint = coordinate.GetCoordinate(inputPoint);

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

    public class Coordinate
    {
        private PointF _teachedCenterPoint { get; set; } = new PointF();

        private PointF _searchedCenterPoint { get; set; } = new PointF();

        private double _diffRadian { get; set; } = 0.0;

        private PointF _offsetPoint { get; set; } = new PointF();

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
            _teachedCenterPoint = MathHelper.GetCenterPoint(teachedLeftPoint, teachedRightPoint);
        }

        private PointF GetTeachedCenterPoint()
        {
            return _teachedCenterPoint;
        }

        private void SetSearchedCenterPoint(PointF searchedLeftPoint, PointF searchedRightPoint)
        {
            _searchedCenterPoint = MathHelper.GetCenterPoint(searchedLeftPoint, searchedRightPoint);
        }

        private PointF GetSearchedCenterPoint()
        {
            return _searchedCenterPoint;
        }

        private void SetDiffAngle(PointF teachedLeftPoint, PointF teachedRightPoint, PointF searchedLeftPoint, PointF searchedRightPoint)
        {
            double teachedRadian = MathHelper.GetRadian(teachedLeftPoint, teachedRightPoint);
            if (teachedRadian > 180.0)
                teachedRadian -= 360.0;

            double searchedRadian = MathHelper.GetRadian(searchedLeftPoint, searchedRightPoint);
            if (searchedRadian > 180.0)
                searchedRadian -= 360.0;

            _diffRadian = searchedRadian - teachedRadian;
        }

        private double GetDiffRadian()
        {
            return _diffRadian;
        }

        private void SetOffsetPoint(PointF teachedCenterPoint, PointF searchedCenterPoint)
        {
            _offsetPoint = MathHelper.GetOffset(teachedCenterPoint, searchedCenterPoint);
        }

        private PointF GetOffsetPoint()
        {
            return _offsetPoint;
        }

        public PointF GetCoordinate(PointF inputPoint)
        {
            PointF searchedCenterPoint = GetSearchedCenterPoint();
            double diffRadian = GetDiffRadian();
            PointF offsetPoint = GetOffsetPoint();

            return MathHelper.GetCoordinate(searchedCenterPoint, diffRadian, offsetPoint, inputPoint);
        }
    }
}
