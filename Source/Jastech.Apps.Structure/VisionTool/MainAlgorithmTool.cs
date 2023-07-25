using Cognex.VisionPro;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.Parameters;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Jastech.Framework.Util;

namespace Jastech.Apps.Structure.VisionTool
{
    public partial class MainAlgorithmTool : AlgorithmTool
    {
        //public void ExecuteAlignment(Unit unit, List<PointF> realCoordinateList, PointF calibrationStartPosition, ref AppsInspResult inspResult)
        public PreAlignResult ExecuteAlignment(Unit unit, List<PointF> realCoordinateList, PointF calibrationStartPosition)
        {
            double t1, t2, dt = 0.0;
            double cX = 0, cY = 0, pX = 0, pY = 0;
            double imagePosX = 0, imagePosY = 0;

            double leftRealMarkX = realCoordinateList[0].X;
            double leftRealMarkY = realCoordinateList[0].Y;

            double rightRealMarkX = realCoordinateList[1].X;
            double rightRealMarkY = realCoordinateList[1].Y;

            var preAlignLeftData = unit.PreAlign.AlignParamList.Where(x => x.Direction == MarkDirection.Left).FirstOrDefault();
            double leftMotionX = preAlignLeftData.GetMotionData(AxisName.X);
            double leftMotionY = preAlignLeftData.GetMotionData(AxisName.Y);
            double leftMotionT = preAlignLeftData.GetMotionData(AxisName.T);

            var preAlignRightData = unit.PreAlign.AlignParamList.Where(x => x.Direction == MarkDirection.Left).FirstOrDefault();
            double rightMotionX = preAlignRightData.GetMotionData(AxisName.X);
            double rightMotionY = preAlignRightData.GetMotionData(AxisName.Y);
            double rightMotionT = preAlignRightData.GetMotionData(AxisName.T);

            double centerOffsetX = 0, centerOffsetY = 0;

            double alignX = 0, alignY = 0;

            // 1. 보정각 Theta 구하기
            double dX = (rightMotionX + rightRealMarkX) - (leftMotionX + leftRealMarkX) + 0;    // (m_dMotionPosX[ALIGN_OBJ_RIGHT] + dMarkRightX) - (m_dMotionPosX[ALIGN_OBJ_LEFT] + dMarkLeftX) + m_dLRDistCamera;
            double dY = (rightMotionY + rightRealMarkY) - (leftMotionY + leftRealMarkY);        // (m_dMotionPosY[ALIGN_OBJ_RIGHT] + dMarkRightY) - (m_dMotionPosY[ALIGN_OBJ_LEFT] + dMarkLeftY);

            if (dX != 0)
                t1 = Math.Atan2(dY, dX);
            else
                t1 = 0.0;

            t2 = 0;

            dt = t2 - t1;

            // 2. 회전 대상 지정 ( Left 기준 )
            pX = leftMotionX; // m_dMotionPosX[ALIGN_OBJ_LEFT]
            pY = leftMotionY; // m_dMotionPosY[ALIGN_OBJ_LEFT]
            imagePosX = leftRealMarkX;
            imagePosY = leftRealMarkY;

            // 3. 회전 중심 보정
            centerOffsetX = (leftMotionX - calibrationStartPosition.X); // m_dMotionPosX[ALIGN_OBJ_LEFT] - g_DataCalibration.GetCalStartPosX(m_nCurrentCamIndex, nStageNo);
            centerOffsetY = (leftMotionY - calibrationStartPosition.Y); // m_dMotionPosY[ALIGN_OBJ_LEFT] - g_DataCalibration.GetCalStartPosY(m_nCurrentCamIndex, nStageNo);

            cX = centerOffsetX; // g_DataCalibration.GetRotCenterX(m_nCurrentCamIndex, nStageNo) + dCenterOffsetX;
            cY = centerOffsetY; // g_DataCalibration.GetRotCenterY(m_nCurrentCamIndex, nStageNo) + dCenterOffsetY;

            // 4. 회전 후 보정 위치 계산
            alignX = (pX - cX) * Math.Cos(dt) - (pY - cY) * Math.Sin(dt) + cX;
            alignY = (pY - cY) * Math.Cos(dt) - (pX - cX) * Math.Sin(dt) + cY;

            // 5. 현재 위치 기준 보정 offset 계산
            double offsetX = leftMotionX - alignX;
            double offsetY = leftMotionY - alignY;
            //refAlignOffset.s_fOffsetX = (m_dAxisX1 - dAlignX);
            //refAlignOffset.s_fOffsetY = (m_dAxisY1 - dAlignY);


            // 6. mark를 화면 중심으로 보내기 위한 mark offset 적용
            //refAlignOffset.s_fOffsetX -= dImagePosX;
            //refAlignOffset.s_fOffsetY -= dImagePosY;
            //refAlignOffset.s_fOffsetT = (dt * 180.0 / M_PI);

            offsetX -= imagePosX;
            offsetY -= imagePosY;
            double offsetT = (dt * 180.0 / Math.PI);

            //return false;
            //inspResult.PreAlignResult.SetPreAlignResult(offsetX, offsetY, offsetT);

            PreAlignResult result = new PreAlignResult();
            result.SetPreAlignResult(offsetX, offsetY, offsetT);
            return result;
        }

        //public void RunPreAlign(ref AppsInspResult inspResult)
        //{
        //    if (inspResult.PreAlignResult.FoundedMark.Left.Judgement == Judgement.OK && inspResult.PreAlignResult.FoundedMark.Right.Judgement == Judgement.OK)
        //        inspResult.PreAlignResult.Judgement = Judgement.OK;
        //    else
        //        inspResult.PreAlignResult.Judgement = Judgement.NG;
        //}

        public void MainMarkInspect(ICogImage cogImage, Tab tab, ref TabInspResult tabInspResult)
        {
            tabInspResult.MarkResult.FpcMark = RunFpcMark(cogImage, tab);
            tabInspResult.MarkResult.PanelMark = RunPanelMark(cogImage, tab);
        }

        public AlignResult RunMainLeftAlignX(ICogImage cogImage, Tab tab, CoordinateTransform fpcCoordinate, CoordinateTransform panelCoordinate, double judgementX)
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

        public AlignResult RunMainLeftAlignY(ICogImage cogImage, Tab tab, CoordinateTransform fpcCoordinate, CoordinateTransform panelCoordinate, double judgementY)
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

        public AlignResult RunMainRightAlignX(ICogImage cogImage, Tab tab, CoordinateTransform fpcCoordinate, CoordinateTransform panelCoordinate, double judgementX)
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

        public AlignResult RunMainRightAlignY(ICogImage cogImage, Tab tab, CoordinateTransform fpcCoordinate, CoordinateTransform panelCoordinate, double judgementY)
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

            if (result.Panel.Judgement == Judgment.OK && result.Fpc.Judgement == Judgment.OK)
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
                    result.Judgement = Judgment.OK;
                else
                    result.Judgement = Judgment.NG;
            }
            else
            {
                result.Judgement = Judgment.FAIL;
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

            if (result.Panel.Judgement == Judgment.OK && result.Fpc.Judgement == Judgment.OK)
            {
                float panelY = result.Panel.CogAlignResult[0].MaxCaliperMatch.FoundPos.Y;
                float fpcY = result.Fpc.CogAlignResult[0].MaxCaliperMatch.FoundPos.Y;

                float newY = panelY - fpcY;

                result.ResultValue_pixel = newY;

                if (Math.Abs(result.ResultValue_pixel) <= judgementY)
                    result.Judgement = Judgment.OK;
                else
                    result.Judgement = Judgment.NG;
            }
            else
            {
                string message = string.Format("Main CaliperY Search Fail. Panel({0}), FPC({1})", panelParam.Name, fpcParam.Name);
                Logger.Debug(LogType.Inspection, message);
                result.Judgement = Judgment.FAIL;
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

                if (leftResult.Judgement == Judgment.OK && rightResult.Judgement == Judgment.OK)
                {
                    MarkMatchingResult matchingResult = new MarkMatchingResult();
                    matchingResult.Left = leftResult;
                    matchingResult.Right = rightResult;

                    result.FoundedMark = matchingResult;
                    result.Judgement = Judgment.OK;

                    return result;
                }
                else
                {
                    MarkMatchingResult matchingResult = new MarkMatchingResult();
                    matchingResult.Left = leftResult;
                    matchingResult.Right = rightResult;

                    result.FailMarks.Add(matchingResult);
                    result.Judgement = Judgment.NG;
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

                if (leftResult.Judgement == Judgment.OK && rightResult.Judgement == Judgment.OK)
                {
                    MarkMatchingResult matchingResult = new MarkMatchingResult();
                    matchingResult.Left = leftResult;
                    matchingResult.Right = rightResult;

                    result.FoundedMark = matchingResult;
                    result.Judgement = Judgment.OK;
                    return result;
                }
                else
                {
                    MarkMatchingResult matchingResult = new MarkMatchingResult();
                    matchingResult.Left = leftResult;
                    matchingResult.Right = rightResult;

                    result.FailMarks.Add(matchingResult);
                    result.Judgement = Judgment.NG;
                }
            }
            return result;
        }
    }

    public partial class MainAlgorithmTool : AlgorithmTool
    {
        public CoordinateTransform Coordinate = null;

        public CogRectangleAffine CoordinateRectangle(CogRectangleAffine originRegion, CoordinateTransform coordinate)
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
}
