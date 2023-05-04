using Cognex.VisionPro;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.Parameters;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using Jastech.Framework.Macron.Akkon;
using Jastech.Framework.Macron.Akkon.Parameters;
using Jastech.Framework.Macron.Akkon.Results;
using Jastech.Framework.Util.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.CogCaliper;

namespace Jastech.Apps.Structure.VisionTool
{
    public class AlgorithmTool
    {
        private MacronAkkon AkkonAlgorithm { get; set; } = new MacronAkkon();

        private CogPatternMatching PatternAlgorithm { get; set; } = new CogPatternMatching();

        public CogAlignCaliper AlignAlgorithm { get; set; } = new CogAlignCaliper();

        //public AppsPatternMatchingResult RunPatternMatching(Tab tab, ICogImage cogImage)
        //{
        //    var paramCount = tab.FpcMarkParamList.Count() / 2;

        //    Logger.Debug(LogType.Inspection, "Run Pattern Matching.");

        //    for (int i = 0; i < paramCount; i++)
        //    {
        //        var fpcParam = tab.FpcMarkParamList[i];
        //        var panelParam = tab.PanelMarkParamList[i];

        //        var fpcMarkResult = RunPatternMatch(cogImage, fpcParam.InspParam);
        //        var panelMarkResult = RunPatternMatch(cogImage, fpcParam.InspParam);

        //        if (fpcMarkResult == null || panelMarkResult == null)
        //        {
        //            string message = string.Format("RunPatternMatch is null. Index : {0}", i.ToString());
        //            Logger.Debug(LogType.Inspection, message);
        //            continue;
        //        }

        //        if (fpcMarkResult.Result == Result.OK && panelMarkResult.Result == Result.OK)
        //        {
        //            string message = string.Format("RunPatternMatch Result : FPC Result_{0}, Panel_Result_{1}"
        //                                    , fpcMarkResult.Result.ToString(), panelMarkResult.Result.ToString());
        //            Logger.Debug(LogType.Inspection, message);

        //            AppsPatternMatchingResult result = new AppsPatternMatchingResult();

        //            result.FpcResult = fpcMarkResult;
        //            result.PanelResult = panelMarkResult;

        //            return result;
        //        }
        //        else
        //        {
        //            string message = string.Format("RunPatternMatch Result : FPC Result_{0}, Panel Result_{1}"
        //                                    , fpcMarkResult.Result.ToString(), panelMarkResult.Result.ToString());
        //            Logger.Debug(LogType.Inspection, message);
        //        }
        //    }

        //    return null;

        //}

        public bool Test(Tab tab, Mat matImage)
        {
            AppsInspResult inspResult = new AppsInspResult();
            inspResult.TabNo = tab.Index;

            #region Mark 검사

            ICogImage cogImage = ConvertCogImage(matImage);

            RunFpcMark(cogImage, tab, ref inspResult);
            if (inspResult.FpcMark.Judement == Judgement.NG)
                return false;    // 검사 실패

            RunPanelMark(cogImage, tab, ref inspResult);
            if (inspResult.PanelMark.Judement == Judgement.NG)
                return false;    // 검사 실패

            #endregion


            // 마크 결과값으로 포인트에 적용

            RunLeftAlign(cogImage, tab, ref inspResult);
            if (inspResult.LeftAlignX.Judgement != Judgement.OK || inspResult.LeftAlignY.Judgement != Judgement.OK)
                return false;

            RunRightAlign(cogImage, tab, ref inspResult);
            if (inspResult.RightAlignX.Judgement != Judgement.OK || inspResult.RightAlignY.Judgement != Judgement.OK)
                return false;


            // 압흔검사

            return true;
        }

        private void RunLeftAlign(ICogImage cogImage, Tab tab, ref AppsInspResult inspResult)
        {
            string ngReason = "";

            inspResult.LeftAlignY = RunLeftAlignY(cogImage, tab, 100.0f, out ngReason); // 100.0f 컨피그로 빼야함
            if (ngReason != "")
                Logger.Debug(LogType.Inspection, ngReason);
        }

        private void RunRightAlign(ICogImage cogImage, Tab tab, ref AppsInspResult inspResult)
        {
            string ngReason = "";

            inspResult.RightAlignY = RunRightAlignY(cogImage, tab, 100.0f, out ngReason); // 100.0f 컨피그로 빼야함
            if (ngReason != "")
                Logger.Debug(LogType.Inspection, ngReason);
        }

        private AlignResult RunLeftAlignY(ICogImage cogImage, Tab tab, float judgementValue, out string ngReason)
        {
            AlignResult result = new AlignResult();
            ngReason = "";

            var panelParam = tab.GetAlignParam(ATTTabAlignName.LeftPanelY);
            var fpcParam = tab.GetAlignParam(ATTTabAlignName.LeftFPCY);

            result.Panel = RunAlignY(cogImage, panelParam.CaliperParams);
            result.Fpc = RunAlignY(cogImage, fpcParam.CaliperParams);

            if (result.Panel.Judgement == Judgement.OK && result.Fpc.Judgement == Judgement.OK)
            {
                float panelY = result.Panel.CogAlignResult[0].MaxCaliperMatch.FoundPos.Y;
                float fpcY = result.Fpc.CogAlignResult[0].MaxCaliperMatch.FoundPos.Y;

                if (Math.Abs(panelY - fpcY) <= judgementValue)
                    result.Judgement = Judgement.OK;
                else
                {
                    ngReason = "Alignment NG";
                    result.Judgement = Judgement.NG;
                }
            }
            else
            {
                ngReason = "LeftAlignY Caliper Search Fail.";
                result.Judgement = Judgement.Fail;
            }

            return result;
        }

        private AlignResult RunRightAlignY(ICogImage cogImage, Tab tab, float judgementValue, out string ngReason)
        {
            AlignResult result = new AlignResult();
            ngReason = "";

            var panelParam = tab.GetAlignParam(ATTTabAlignName.RightPanelY);
            var fpcParam = tab.GetAlignParam(ATTTabAlignName.RightFPCY);

            result.Panel = RunAlignY(cogImage, panelParam.CaliperParams);
            result.Fpc = RunAlignY(cogImage, fpcParam.CaliperParams);

            if (result.Panel.Judgement == Judgement.OK && result.Fpc.Judgement == Judgement.OK)
            {
                float panelY = result.Panel.CogAlignResult[0].MaxCaliperMatch.FoundPos.Y;
                float fpcY = result.Fpc.CogAlignResult[0].MaxCaliperMatch.FoundPos.Y;

                if (Math.Abs(panelY - fpcY) <= judgementValue)
                    result.Judgement = Judgement.OK;
                else
                {
                    ngReason = "Alignment NG";
                    result.Judgement = Judgement.NG;
                }
            }
            else
            {
                ngReason = "LeftAlignY Caliper Search Fail.";
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
                if(leftResult == null)
                    continue;

                var rightResult = RunPatternMatch(cogImage, rightParam.InspParam);
                if(rightResult == null)
                    continue;

                if (leftResult.Judgement == Judgement.OK && rightResult.Judgement == Judgement.OK)
                {
                    result.Judement = Judgement.OK;
                    result.FoundedMark.Left = leftResult;
                    result.FoundedMark.Right = rightResult;

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
                    result.Judement = Judgement.OK;
                    result.FoundedMark.Left = leftResult;
                    result.FoundedMark.Right = rightResult;
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

        public ICogImage ConvertCogImage(Mat image)
        {
            if (image == null)
                return null;

            int size = image.Width * image.Height * image.NumberOfChannels;
            byte[] dataArray = new byte[size];
            Marshal.Copy(image.DataPointer, dataArray, 0, size);

            ColorFormat format = image.NumberOfChannels == 1 ? ColorFormat.Gray : ColorFormat.RGB24;

            var cogImage = CogImageHelper.CovertImage(dataArray, image.Width, image.Height, format);

            return cogImage;
        }

        public CogAlignCaliperResult RunAlignX(ICogImage image, VisionProCaliperParam param, int leadCount)
        {
            if (image == null || param == null)
                return null;

            CogAlignCaliperResult alignResult = new CogAlignCaliperResult();
            alignResult.AddAlignResult(AlignAlgorithm.RunAlignX(image, param, leadCount));

            bool isFounded = false;
            foreach (var item in alignResult.CogAlignResult)
            {
                isFounded |= item.Found;
            }

            alignResult.Judgement = isFounded ? Judgement.OK : Judgement.Fail;

            if(alignResult.Judgement == Judgement.OK)
            {
                if (leadCount != alignResult.CogAlignResult.Count() / 2)
                    alignResult.Judgement = Judgement.NG;
            }
            
                
            return alignResult;
        }

        public CogAlignCaliperResult RunAlignY(ICogImage image, VisionProCaliperParam param)
        {
            CogAlignCaliperResult alignResult = new CogAlignCaliperResult();
            alignResult.AddAlignResult(AlignAlgorithm.RunAlignY(image, param));

            bool isFounded = false;
            foreach (var item in alignResult.CogAlignResult)
            {
                if (item == null)
                    continue;

                isFounded |= item.Found;
            }

            alignResult.Judgement = isFounded ? Judgement.OK : Judgement.Fail;

            return alignResult;
        }

        public CogPatternMatchingResult RunPatternMatch(ICogImage image, VisionProPatternMatchingParam param)
        {
            if (image == null || param == null)
                return null;

            CogPatternMatchingResult matchingResult = PatternAlgorithm.Run(image, param);

            if (matchingResult == null)
                return null;

            if (matchingResult.MatchPosList.Count <= 0)
                matchingResult.Judgement = Judgement.Fail;
            else
            {
                if ((matchingResult.MaxScore * 100) >= param.Score)
                    matchingResult.Judgement = Judgement.OK;
                else
                    matchingResult.Judgement = Judgement.NG;
            }

            return matchingResult;
        }

        public List<AkkonResult> RunAkkon(Mat mat, AkkonParam akkonParam, int stageNo, int tabNo)
        {
            if (mat == null)
                return null;

            var marcon = akkonParam.MacronAkkonParam;

            marcon.SliceHeight = mat.Height;

            if (AkkonAlgorithm.CreateDllBuffer(marcon))
            {
                AkkonAlgorithm.CreateImageBuffer(stageNo, tabNo, mat.Width, mat.Height, marcon.InspOption.InspResizeRatio);

                var akkonRoiList = akkonParam.GetAkkonROIList();

                AkkonAlgorithm.SetConvertROIData(akkonRoiList, 0, tabNo, new PointF(mat.Width / 2, mat.Height / 2), new PointF(0, 0), 0, akkonParam.MacronAkkonParam.InspOption.InspResizeRatio);

                AkkonAlgorithm.InitPrepareInspect();
                int overlapCount = AkkonAlgorithm.PrepareInspect(0, tabNo);
                marcon.InspOption.Overlap = overlapCount;
                
                AkkonAlgorithm.SetAkkonParam(0, tabNo, ref marcon);
                AkkonAlgorithm.EnableInspFlag();

                var results = AkkonAlgorithm.Inspect(0, tabNo, mat);
              
                return results;
            }
            else
            {
                Logger.Debug(LogType.Inspection, "ATT is not Initalized");
                return new List<AkkonResult>();
            }
        }

        public List<AkkonResult> RunCropAkkon(Mat mat, PointF cropOffset, AkkonParam akkonParam, int tabNo)
        {
            var marcon = akkonParam.MacronAkkonParam;
            if (AkkonAlgorithm.CreateDllBuffer(marcon))
            {
                AkkonAlgorithm.CreateImageBuffer(0, 0, mat.Width, mat.Height, marcon.InspOption.InspResizeRatio);

                var calcROIList = AkkonAlgorithm.GetCalcROI(cropOffset, akkonParam.GetAkkonROIList());

                AkkonAlgorithm.SetConvertROIData(calcROIList, 0, tabNo, new PointF(mat.Width / 2, mat.Height / 2), new PointF(0, 0), 0);

                AkkonAlgorithm.InitPrepareInspect();
                int overlapCount = AkkonAlgorithm.PrepareInspect(0, tabNo);
                marcon.InspOption.Overlap = overlapCount;

                AkkonAlgorithm.SetAkkonParam(0, tabNo, ref marcon);
                AkkonAlgorithm.EnableInspFlag();

                var results = AkkonAlgorithm.Inspect(0, tabNo, mat);

                return results;
            }
            return null;
        }

        public Mat LastAkkonResultImage(Mat mat, AkkonParam akkonParam, int stageNo, int tabNo)
        {
            var marcon = akkonParam.MacronAkkonParam;
            marcon.DrawOption.Contour = true;
            marcon.DrawOption.Center = false;
            return AkkonAlgorithm.GetDrawResultImage(mat, stageNo, tabNo, ref marcon);
        }
    }

    public enum InspectionType
    {
        PreAlign,
        Align,
        Akkon,
    }

    public enum AlignName
    {
        Tab1,
    }
}
