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
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.VisionTool
{
    public class MainAlgorithmTool : AlgorithmTool
    {
        public AppsInspResult MainRunInspect(Tab tab, Mat matImage, float judgementX, float judgementY)
        {
            AppsInspResult inspResult = new AppsInspResult();
            inspResult.TabNo = tab.Index;

            #region Mark 검사
            ICogImage cogImage = ConvertCogImage(matImage);
            inspResult.CogImage = cogImage;

            RunFpcMark(cogImage, tab, ref inspResult);
            if (inspResult.FpcMark.Judement == Judgement.NG)
                return inspResult;    // 검사 실패

            RunPanelMark(cogImage, tab, ref inspResult);
            if (inspResult.PanelMark.Judement == Judgement.NG)
                return inspResult;    // 검사 실패
            #endregion

            // 마크 결과값으로 포인트에 적용
            RunLeftAlign(cogImage, tab, ref inspResult, judgementX, judgementY);
            if (inspResult.LeftAlignX.Judgement != Judgement.OK || inspResult.LeftAlignY.Judgement != Judgement.OK)
                return inspResult;

            RunRightAlign(cogImage, tab, ref inspResult, judgementX, judgementY);
            if (inspResult.RightAlignX.Judgement != Judgement.OK || inspResult.RightAlignY.Judgement != Judgement.OK)
                return inspResult;

            // 압흔검사
            var akkonParam = tab.AkkonParam;
            var akkonResult = RunAkkon(matImage, akkonParam, tab.StageIndex, tab.Index);

            if(akkonResult.Count() > 0)
            {
                inspResult.AkkonResultList.AddRange(akkonResult);
                inspResult.AkkonResultImage = GetResultImage(matImage, tab, akkonParam);
            }

            return inspResult;
        }

        private void RunLeftAlign(ICogImage cogImage, Tab tab, ref AppsInspResult inspResult, float judgementX, float judgementY)
        {
            string ngReason = "";

            inspResult.LeftAlignX = RunMainAlignX(cogImage, tab, ATTTabAlignName.LeftPanelX, ATTTabAlignName.LeftFPCX, judgementX, out ngReason); 
            if (ngReason != "")
                Logger.Debug(LogType.Inspection, ngReason);

            inspResult.LeftAlignY = RunMainAlignY(cogImage, tab, ATTTabAlignName.LeftPanelY, ATTTabAlignName.LeftFPCY, judgementY, out ngReason);
            if (ngReason != "")
                Logger.Debug(LogType.Inspection, ngReason);


        }

        private void RunRightAlign(ICogImage cogImage, Tab tab, ref AppsInspResult inspResult, float judgementX, float judgementY)
        {
            string ngReason = "";

            inspResult.RightAlignX = RunMainAlignX(cogImage, tab, ATTTabAlignName.RightPanelX, ATTTabAlignName.RightFPCX, judgementX, out ngReason);
            if (ngReason != "")
                Logger.Debug(LogType.Inspection, ngReason);

            inspResult.RightAlignY = RunMainAlignY(cogImage, tab, ATTTabAlignName.RightPanelY, ATTTabAlignName.RightFPCY, judgementY, out ngReason); // 100.0f 컨피그로 빼야함
            if (ngReason != "")
                Logger.Debug(LogType.Inspection, ngReason);
        }

        private AlignResult RunMainAlignX(ICogImage cogImage, Tab tab, ATTTabAlignName panelAlign, ATTTabAlignName fpcAlign, float judgementValue, out string ngReason)
        {
            AlignResult result = new AlignResult();
            ngReason = "";

            var panelParam = tab.GetAlignParam(panelAlign);
            var fpcParam = tab.GetAlignParam(fpcAlign);

            result.Panel = RunAlignX(cogImage, panelParam.CaliperParams, panelParam.LeadCount);
            result.Fpc = RunAlignX(cogImage, fpcParam.CaliperParams, fpcParam.LeadCount);

            if (result.Panel.Judgement == Judgement.OK && result.Fpc.Judgement == Judgement.OK)
            {
                List<float> intervalValueX = new List<float>();

                for (int i = 0; i < panelParam.LeadCount * 2; i+= 2)
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

                Judgement judgement = Judgement.OK;

                foreach (var avg in intervalValueX)
                {
                    if (avg == max || avg == min)
                        continue;

                    if (avg >= judgementValue)
                    {
                        ngReason = string.Format("Main AlignmentX NG : Panel({0}), FPC({1})", panelAlign.ToString(), fpcAlign.ToString());
                        judgement = Judgement.NG;
                        break;
                    }
                }
                result.Judgement = judgement;
            }
            else
            {
                ngReason = string.Format(" Main CaliperX Search Fail. Panel({0}), FPC({1})", panelAlign.ToString(), fpcAlign.ToString());
                result.Judgement = Judgement.Fail;
            }

            return result;
        }

        private AlignResult RunMainAlignY(ICogImage cogImage, Tab tab, ATTTabAlignName panelAlign, ATTTabAlignName fpcAlign, float judgementValue, out string ngReason)
        {
            AlignResult result = new AlignResult();
            ngReason = "";

            var panelParam = tab.GetAlignParam(panelAlign);
            var fpcParam = tab.GetAlignParam(fpcAlign);

            result.Panel = RunAlignY(cogImage, panelParam.CaliperParams);
            result.Fpc = RunAlignY(cogImage, fpcParam.CaliperParams);

            if (result.Panel.Judgement == Judgement.OK && result.Fpc.Judgement == Judgement.OK)
            {
                float panelY = result.Panel.CogAlignResult[0].MaxCaliperMatch.FoundPos.Y;
                float fpcY = result.Fpc.CogAlignResult[0].MaxCaliperMatch.FoundPos.Y;

                float newY = panelY - fpcY;

                if (Math.Abs(newY) <= judgementValue)
                    result.Judgement = Judgement.OK;
                else
                {
                    ngReason = string.Format("Main AlignmentY NG : Panel({0}), FPC({1})", panelAlign.ToString(), fpcAlign.ToString());
                    result.Judgement = Judgement.NG;
                }
            }
            else
            {
                ngReason = string.Format(" Main CaliperY Search Fail. Panel({0}), FPC({1})", panelAlign.ToString(), fpcAlign.ToString());
                result.Judgement = Judgement.Fail;
            }

            return result;
        }

        public void RunFpcMark(ICogImage cogImage, Tab tab, ref AppsInspResult inspResult)
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
                    result.Judement = Judgement.OK;

                    return;
                }
                else
                {
                    MarkMatchingResult matchingResult = new MarkMatchingResult();
                    matchingResult.Left = leftResult;
                    matchingResult.Right = rightResult;

                    result.FailMarks.Add(matchingResult);
                    result.Judement = Judgement.NG;
                }
            }
        }

        public void RunPanelMark(ICogImage cogImage, Tab tab, ref AppsInspResult inspResult)
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
                    result.Judement = Judgement.OK;
                    return;
                }
                else
                {
                    MarkMatchingResult matchingResult = new MarkMatchingResult();
                    matchingResult.Left = leftResult;
                    matchingResult.Right = rightResult;

                    result.FailMarks.Add(matchingResult);
                    result.Judement = Judgement.NG;
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
