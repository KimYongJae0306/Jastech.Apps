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
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using Jastech.Framework.Algorithms.Akkon.Results;

namespace Jastech.Apps.Structure.VisionTool
{
    public partial class MainAlgorithmTool : AlgorithmTool
    {
        public AlignmentResult ExecuteAlignment(Unit unit, List<PointF> realCoordinateList, PointF calibrationStartPosition)
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
            AlignmentResult alignment = new AlignmentResult
            {
                OffsetX = offsetX,
                OffsetY = offsetY,
                OffsetT = offsetT,
            };

            return alignment;
        }

        public void MainMarkInspect(ICogImage cogImage, Tab tab, ref TabInspResult tabInspResult, bool useAlignMark)
        {
            tabInspResult.MarkResult.FpcMark = RunFpcMark(cogImage, tab, useAlignMark);
            tabInspResult.MarkResult.PanelMark = RunPanelMark(cogImage, tab, useAlignMark);
        }

        public AlignResult RunMainLeftAlignX(ICogImage cogImage, Tab tab, CoordinateTransform fpcCoordinate, CoordinateTransform panelCoordinate, double judgementX_pixel)
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

            var result = RunMainAlignX(cogImage, fpcParam, panelParam, judgementX_pixel);
            return result;
        }

        public AlignResult RunMainLeftAlignY(ICogImage cogImage, Tab tab, CoordinateTransform fpcCoordinate, CoordinateTransform panelCoordinate, double judgementY_pixel)
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

            var result = RunMainAlignY(cogImage, fpcParam, panelParam, judgementY_pixel);
            return result;
        }

        public AlignResult RunMainRightAlignX(ICogImage cogImage, Tab tab, CoordinateTransform fpcCoordinate, CoordinateTransform panelCoordinate, double judgementX_pixel)
        {
            var fpcParam = tab.GetAlignParam(ATTTabAlignName.RightFPCX).DeepCopy();
            var panelParam = tab.GetAlignParam(ATTTabAlignName.RightPanelX).DeepCopy();

            if (fpcCoordinate != null && panelCoordinate != null)
            {
                var calcFpcRegion = CoordinateRectangle(fpcParam.CaliperParams.GetRegion() as CogRectangleAffine, fpcCoordinate);
                var calcPanelRegion = CoordinateRectangle(panelParam.CaliperParams.GetRegion() as CogRectangleAffine, panelCoordinate);

                fpcParam.CaliperParams.SetRegion(calcFpcRegion);
                panelParam.CaliperParams.SetRegion(calcPanelRegion);
            }

            var result = RunMainAlignX(cogImage, fpcParam, panelParam, judgementX_pixel);

            return result;
        }

        public AlignResult RunMainRightAlignY(ICogImage cogImage, Tab tab, CoordinateTransform fpcCoordinate, CoordinateTransform panelCoordinate, double judgementY_pixel)
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

            var result = RunMainAlignY(cogImage, fpcParam, panelParam, judgementY_pixel);
            return result;
        }

        public AlignResult RunMainAlignX(ICogImage cogImage, AlignParam fpcParam, AlignParam panelParam, double judegementX_pixel)
        {
            AlignResult result = new AlignResult();

            result.Panel = RunAlignX(cogImage, panelParam.CaliperParams, panelParam.LeadCount);
            result.Fpc = RunAlignX(cogImage, fpcParam.CaliperParams, fpcParam.LeadCount);

            List<double> alignValueList = new List<double>();

            // 정환
            for (int i = 0; i < panelParam.LeadCount * 2; i += 2)
            {
                var panelResult1 = result.Panel.CogAlignResult[i];
                var panelResult2 = result.Panel.CogAlignResult[i + 1];

                var fpcResult1 = result.Fpc.CogAlignResult[i];
                var fpcResult2 = result.Fpc.CogAlignResult[i + 1];

                if (panelResult1.Found == false || panelResult2.Found == false || fpcResult1.Found == false || fpcResult2.Found == false)
                    continue;

                float panelIntervalX = Math.Abs(panelResult1.CaliperMatchList[0].FoundPos.X - panelResult2.CaliperMatchList[0].FoundPos.X);
                float panelIntervalY = Math.Abs(panelResult1.CaliperMatchList[0].FoundPos.Y - panelResult2.CaliperMatchList[0].FoundPos.Y);
                float panelCenterX = panelResult1.CaliperMatchList[0].FoundPos.X + (panelIntervalX / 2.0f);
                float panelCenterY = panelResult1.CaliperMatchList[0].FoundPos.Y + (panelIntervalY / 2.0f);

                float fpcIntervalX = Math.Abs(fpcResult1.CaliperMatchList[0].FoundPos.X - fpcResult2.CaliperMatchList[0].FoundPos.X);
                float fpcIntervalY = Math.Abs(fpcResult1.CaliperMatchList[0].FoundPos.Y - fpcResult2.CaliperMatchList[0].FoundPos.Y);
                float fpcCenterX = fpcResult1.CaliperMatchList[0].FoundPos.X + (fpcIntervalX / 2.0f);
                float fpcCenterY = fpcResult1.CaliperMatchList[0].FoundPos.Y + (fpcIntervalY / 2.0f);

                var centerY = (panelCenterY + fpcCenterY) / 2.0f;
                var panelSkew = panelResult1.CaliperMatchList[0].ReferenceSkew;
                var fpcSkew = fpcResult1.CaliperMatchList[0].ReferenceSkew;

                var deltaPanelY = centerY - panelCenterY;
                var deltaPanelX = panelSkew * deltaPanelY;
                var panelX = panelCenterX - deltaPanelX;

                var deltaFpcY = fpcCenterY - centerY;
                var deltaFpcX = fpcSkew * deltaFpcY;

                var fpcX = fpcCenterX + deltaFpcX;
                var res = panelX - fpcX;

                alignValueList.Add(res);
            }

            if(alignValueList.Count > 0)
            {
                double max = alignValueList.Max();
                double min = alignValueList.Min();

                double temp = 0.0f;
                int count = 0;
                foreach (var value in alignValueList)
                {
                    if (value == max || value == min)
                        continue;

                    temp += value;
                    count++;
                }

                result.ResultValue_pixel = Convert.ToSingle(temp / count);
                result.JudegementValue_pixel = judegementX_pixel;
            }
           
            return result;
        }

        private AlignResult RunMainAlignY(ICogImage cogImage, AlignParam fpcParam, AlignParam panelParam, double judgementY_pixel)
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
                result.JudegementValue_pixel = judgementY_pixel;
            }
            else
            {
                string message = string.Format("Main CaliperY Search Fail. Panel({0}), FPC({1})", panelParam.Name, fpcParam.Name);
                Logger.Debug(LogType.Inspection, message);
            }

            return result;
        }

        public MarkResult RunFpcMark(ICogImage cogImage, Tab tab, bool isAlignMark)
        {
            MarkResult result = new MarkResult();
            MarkMatchingResult matchingResult = new MarkMatchingResult();
            foreach (MarkName markName in Enum.GetValues(typeof(MarkName)))
            {
                var leftParam = tab.MarkParamter.GetFPCMark(MarkDirection.Left, markName, isAlignMark);

                var leftResult = RunPatternMatch(cogImage, leftParam.InspParam);
                if (leftResult == null)
                    continue;

                leftResult.Name = markName.ToString();

                if (leftResult.Judgement == Judgement.OK)
                {
                    matchingResult.Left = leftResult;
                    result.FoundedMark = matchingResult;
                    result.Judgement = Judgement.OK;
                    break;
                }
                else
                {
                    matchingResult.Left = leftResult;
                    result.FailMarks.Add(matchingResult);
                    result.Judgement = Judgement.NG;
                }
            }

            foreach (MarkName markName in Enum.GetValues(typeof(MarkName)))
            {
                var rightParam = tab.MarkParamter.GetFPCMark(MarkDirection.Right, markName, isAlignMark);
                var rightResult = RunPatternMatch(cogImage, rightParam.InspParam);
                if (rightResult == null)
                    continue;

                rightResult.Name = markName.ToString();

                if (rightResult.Judgement == Judgement.OK)
                {
                    matchingResult.Right = rightResult;
                    result.FoundedMark = matchingResult;
                    result.Judgement = Judgement.OK;

                    break;
                }
                else
                {
                    matchingResult.Right = rightResult;
                    result.FailMarks.Add(matchingResult);
                    result.Judgement = Judgement.NG;
                }
            }

            return result;
        }

        public MarkResult RunPanelMark(ICogImage cogImage, Tab tab, bool useAlignMark)
        {
            MarkResult result = new MarkResult();
            MarkMatchingResult matchingResult = new MarkMatchingResult();
            foreach (MarkName markName in Enum.GetValues(typeof(MarkName)))
            {
                var leftParam = tab.MarkParamter.GetPanelMark(MarkDirection.Left, markName, useAlignMark);

                var leftResult = RunPatternMatch(cogImage, leftParam.InspParam);
                if (leftResult == null)
                    continue;

                leftResult.Name = markName.ToString();

                if (leftResult.Judgement == Judgement.OK)
                {
                    matchingResult.Left = leftResult;

                    result.FoundedMark = matchingResult;
                    result.Judgement = Judgement.OK;
                    break;
                }
                else
                {
                    matchingResult.Left = leftResult;
                    result.FailMarks.Add(matchingResult);
                    result.Judgement = Judgement.NG;
                }
            }

            foreach (MarkName markName in Enum.GetValues(typeof(MarkName)))
            {
                var rightParam = tab.MarkParamter.GetPanelMark(MarkDirection.Right, markName, useAlignMark);

                var rightResult = RunPatternMatch(cogImage, rightParam.InspParam);
                if (rightResult == null)
                    continue;

                rightResult.Name = markName.ToString();

                if (rightResult.Judgement == Judgement.OK)
                {
                    matchingResult.Right = rightResult;
                    result.FoundedMark = matchingResult;
                    result.Judgement = Judgement.OK;
                    break;
                }
                else
                {
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

        public void GetAlignFpcLeftOffset(Tab tab, TabInspResult tabInspResult, out double offsetX, out double offsetY)
        {
            double fpcRefX = tabInspResult.MarkResult.FpcMark.FoundedMark.Left.MaxMatchPos.ReferencePos.X;
            double fpcRefY = tabInspResult.MarkResult.FpcMark.FoundedMark.Left.MaxMatchPos.ReferencePos.Y;

            var mainFPCMark = tab.MarkParamter.GetFPCMark(MarkDirection.Left, MarkName.Main, true);
            var mainOrigin = mainFPCMark.InspParam.GetOrigin();

            offsetX = mainOrigin.TranslationX - fpcRefX;
            offsetY = mainOrigin.TranslationY - fpcRefY;
        }

        public void GetAlignFpcRightOffset(Tab tab, TabInspResult tabInspResult, out double offsetX, out double offsetY)
        {
            double fpcRefX = tabInspResult.MarkResult.FpcMark.FoundedMark.Right.MaxMatchPos.ReferencePos.X;
            double fpcRefY = tabInspResult.MarkResult.FpcMark.FoundedMark.Right.MaxMatchPos.ReferencePos.Y;

            var mainFPCMark = tab.MarkParamter.GetFPCMark(MarkDirection.Right, MarkName.Main, true);
            var mainOrigin = mainFPCMark.InspParam.GetOrigin();

            offsetX = mainOrigin.TranslationX - fpcRefX;
            offsetY = mainOrigin.TranslationY - fpcRefY;
        }

        public void GetAlignPanelLeftOffset(Tab tab, TabInspResult tabInspResult, out double offsetX, out double offsetY)
        {
            double panelRefX = tabInspResult.MarkResult.PanelMark.FoundedMark.Left.MaxMatchPos.ReferencePos.X;
            double panelRefY = tabInspResult.MarkResult.PanelMark.FoundedMark.Left.MaxMatchPos.ReferencePos.Y;

            var mainPanelMark = tab.MarkParamter.GetPanelMark(MarkDirection.Left, MarkName.Main, true);
            var mainOrigin = mainPanelMark.InspParam.GetOrigin();

            offsetX = mainOrigin.TranslationX - panelRefX;
            offsetY = mainOrigin.TranslationY - panelRefY;
        }

        public void GetAlignPanelRightOffset(Tab tab, TabInspResult tabInspResult, out double offsetX, out double offsetY)
        {
            double panelRefX = tabInspResult.MarkResult.PanelMark.FoundedMark.Right.MaxMatchPos.ReferencePos.X;
            double panelRefY = tabInspResult.MarkResult.PanelMark.FoundedMark.Right.MaxMatchPos.ReferencePos.Y;

            var mainPanelMark = tab.MarkParamter.GetPanelMark(MarkDirection.Right, MarkName.Main, true);
            var mainOrigin = mainPanelMark.InspParam.GetOrigin();

            offsetX = mainOrigin.TranslationX - panelRefX;
            offsetY = mainOrigin.TranslationY - panelRefY;
        }
    }

    public class AlignmentResult
    {
        #region 속성
        public double OffsetX { get; set; } = 0.0;

        public double OffsetY { get; set; } = 0.0;

        public double OffsetT { get; set; } = 0.0;
        #endregion
    }
}
