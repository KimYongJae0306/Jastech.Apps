using Cognex.VisionPro;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.Parameters;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro;
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
    public class MainAlgorithmTool : AlgorithmTool
    {
        public TabInspResult MainRunInspect(Tab tab, Mat matImage, ICogImage cogImage, float judgementX, float judgementY)
        {
            TabInspResult inspResult = new TabInspResult();
            inspResult.TabNo = tab.Index;

            #region Mark 검사
            inspResult.Image = matImage;
            inspResult.CogImage = cogImage;

            RunFpcMark(cogImage, tab, ref inspResult);
            if (inspResult.FpcMark.Judgement == Judgement.NG)
            {
                inspResult.MarkJudgement = Judgement.NG;
                return inspResult;    // 검사 실패
            }

            RunPanelMark(cogImage, tab, ref inspResult);
            if (inspResult.PanelMark.Judgement == Judgement.NG)
            {
                inspResult.MarkJudgement = Judgement.NG;
                return inspResult;    // 검사 실패
            }
            #endregion


            //PointF tlqkf = MathHelper.ThetaCoordinate(inspResult.FpcMark.FailMarks[0].Left.MaxMatchPos.FoundPos,
            //                                            inspResult.FpcMark.FailMarks[0].Right.MaxMatchPos.FoundPos,
            //                                            new Point());
            //// 마크 결과값으로 포인트에 적용

            inspResult.LeftAlignX = RunLeftAlignX(cogImage, tab, judgementX);
            if (inspResult.LeftAlignX.Judgement != Judgement.OK)
            {
                inspResult.AlignJudgement = Judgement.NG;
                return inspResult;
            }

            inspResult.LeftAlignY = RunLeftAlignY(cogImage, tab, judgementY);
            if (inspResult.LeftAlignY.Judgement != Judgement.OK)
            {
                inspResult.AlignJudgement = Judgement.NG;
                return inspResult;
            }

            inspResult.RightAlignX = RunRightAlignX(cogImage, tab, judgementX);
            if (inspResult.RightAlignX.Judgement != Judgement.OK)
            {
                inspResult.AlignJudgement = Judgement.NG;
                return inspResult;
            }

            inspResult.RightAlignY = RunRightAlignY(cogImage, tab, judgementY);
            if (inspResult.RightAlignY.Judgement != Judgement.OK)
            {
                inspResult.AlignJudgement = Judgement.NG;
                return inspResult;
            }

            // 압흔검사
            //var akkonParam = tab.AkkonParam;
            //var akkonResult = RunAkkon(matImage, akkonParam, tab.StageIndex, tab.Index);

            //if(akkonResult.Count() > 0)
            //{
            //    inspResult.AkkonResultList.AddRange(akkonResult);
            //    inspResult.AkkonResultImage = GetResultImage(matImage, tab, akkonParam);
            //}

            return inspResult;
        }

        private AlignResult RunLeftAlignX(ICogImage cogImage, Tab tab, float judgementX)
        {
            var result = RunMainAlignX(cogImage, tab, ATTTabAlignName.LeftPanelX, ATTTabAlignName.LeftFPCX);
            
            float lx = Math.Abs(result.X);
            if (lx > judgementX)
            {
                result.Judgement = Judgement.NG;
                string message = string.Format("Main Alignment Lx NG : {0} / {1}", lx, judgementX);
                Logger.Debug(LogType.Inspection, message);
            }
            return result;
        }

        private AlignResult RunLeftAlignY(ICogImage cogImage, Tab tab, float judgementY)
        {
            var result = RunMainAlignY(cogImage, tab, ATTTabAlignName.LeftPanelY, ATTTabAlignName.LeftFPCY);

            float ly = Math.Abs(result.Y);
            if (ly > judgementY)
            {
                result.Judgement = Judgement.NG;
                string message = string.Format("Main Alignment Ly NG : {0} / {1}", ly, judgementY);
                Logger.Debug(LogType.Inspection, message);
            }
            return result;
        }

        private AlignResult RunRightAlignX(ICogImage cogImage, Tab tab, float judgementX)
        {
            var result = RunMainAlignX(cogImage, tab, ATTTabAlignName.RightPanelX, ATTTabAlignName.RightFPCX);

            float rx = Math.Abs(result.X);
            if (rx > judgementX)
            {
                result.Judgement = Judgement.NG;
                string message = string.Format("Main Alignment Rx NG : {0} / {1}", rx, judgementX);
                Logger.Debug(LogType.Inspection, message);
            }

            return result;
        }

        private AlignResult RunRightAlignY(ICogImage cogImage, Tab tab, float judgementY)
        {
            var result = RunMainAlignY(cogImage, tab, ATTTabAlignName.RightPanelY, ATTTabAlignName.RightFPCY);

            float ry = Math.Abs(result.Y);
            if (ry > judgementY)
            {
                result.Judgement = Judgement.NG;
                string message = string.Format("Main Alignment Ry NG : {0} / {1}", ry, judgementY);
                Logger.Debug(LogType.Inspection, message);
            }
            return result;
        }

        private AlignResult RunMainAlignX(ICogImage cogImage, Tab tab, ATTTabAlignName panelAlign, ATTTabAlignName fpcAlign)
        {
            AlignResult result = new AlignResult();

            var panelParam = tab.GetAlignParam(panelAlign);
            var fpcParam = tab.GetAlignParam(fpcAlign);

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

                result.Judgement = Judgement.OK;
                result.X = temp / count;
            }
            else
            {
                string message = string.Format(" Main CaliperX Search Fail. Panel({0}), FPC({1})", panelAlign.ToString(), fpcAlign.ToString());
                Logger.Debug(LogType.Inspection, message);

                result.Judgement = Judgement.Fail;
            }

            return result;
        }

        private AlignResult RunMainAlignY(ICogImage cogImage, Tab tab, ATTTabAlignName panelAlign, ATTTabAlignName fpcAlign)
        {
            AlignResult result = new AlignResult();

            var panelParam = tab.GetAlignParam(panelAlign);
            var fpcParam = tab.GetAlignParam(fpcAlign);

            result.Panel = RunAlignY(cogImage, panelParam.CaliperParams);
            result.Fpc = RunAlignY(cogImage, fpcParam.CaliperParams);

            if (result.Panel.Judgement == Judgement.OK && result.Fpc.Judgement == Judgement.OK)
            {
                float panelY = result.Panel.CogAlignResult[0].MaxCaliperMatch.FoundPos.Y;
                float fpcY = result.Fpc.CogAlignResult[0].MaxCaliperMatch.FoundPos.Y;

                float newY = panelY - fpcY;

                result.Judgement = Judgement.OK;
                result.Y = newY;
            }
            else
            {
                string message = string.Format("Main CaliperY Search Fail. Panel({0}), FPC({1})", panelAlign.ToString(), fpcAlign.ToString());
                Logger.Debug(LogType.Inspection, message);

                result.Judgement = Judgement.Fail;
            }

            return result;
        }

        public void RunFpcMark(ICogImage cogImage, Tab tab, ref TabInspResult inspResult)
        {
            var result = inspResult.FpcMark;

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

                    return;
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
        }

        public void RunPanelMark(ICogImage cogImage, Tab tab, ref TabInspResult inspResult)
        {
            var result = inspResult.PanelMark;

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
                    return;
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
        }

        private ICogImage GetResultImage(Mat mat, Tab tab, AkkonParam akkonParam)
        {
            float resize = akkonParam.MacronAkkonParam.InspOption.InspResizeRatio;
            double width = Math.Truncate(mat.Width * resize);
            double height = Math.Truncate(mat.Height * resize);

            Mat testMat = new Mat((int)height, (int)width, DepthType.Cv8U, 1);
            Mat resultMatImage = LastAkkonResultImage(testMat, akkonParam, tab.StageIndex, tab.Index);

            Mat matR = MatHelper.ColorChannelSprate(resultMatImage, MatHelper.ColorChannel.R);
            Mat matG = MatHelper.ColorChannelSprate(resultMatImage, MatHelper.ColorChannel.G);
            Mat matB = MatHelper.ColorChannelSprate(resultMatImage, MatHelper.ColorChannel.B);

            byte[] dataR = new byte[matR.Width * matR.Height];
            Marshal.Copy(matR.DataPointer, dataR, 0, matR.Width * matR.Height);

            byte[] dataG = new byte[matG.Width * matG.Height];
            Marshal.Copy(matG.DataPointer, dataG, 0, matG.Width * matG.Height);

            byte[] dataB = new byte[matB.Width * matB.Height];
            Marshal.Copy(matB.DataPointer, dataB, 0, matB.Width * matB.Height);

            var cogImage = CogImageHelper.CovertImage(dataR, dataG, dataB, matB.Width, matB.Height);

            return cogImage;
        }
    }
}
