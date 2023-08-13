using Cognex.VisionPro;
using Emgu.CV;
using Jastech.Framework.Algorithms.Akkon.Results;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.Data
{
    public class TabInspResult
    {
        public int TabNo { get; set; } = -1;

        public bool IsResultProcessDone { get; set; } = false;

        public TabJudgement Judgement
        {
            get
            {
                if (IsManualOK)
                {
                    return TabJudgement.Manual_OK;
                }
                else
                {
                    if (MarkResult.Judgement != Framework.Imaging.Result.Judgement.OK)
                        return TabJudgement.Mark_NG;

                    if (AlignResult.Judgement != Framework.Imaging.Result.Judgement.OK)
                        return TabJudgement.NG;

                    if (AkkonResult == null)
                        return TabJudgement.NG;

                    if (AkkonResult.Judgement != Framework.Imaging.Result.Judgement.OK)
                        return TabJudgement.NG;

                    return TabJudgement.OK;
                }
            }
        }

        public bool IsManualOK { get; set; } = false;

        public Mat Image { get; set; } = null;

        public ICogImage CogImage { get; set; } = null;

        public Mat AkkonInspMatImage { get; set; } = null;

        public ICogImage AkkonInspCogImage { get; set; } = null;

        public ICogImage AkkonResultCogImage { get; set; } = null;

        public TabMarkResult MarkResult { get; set; } = new TabMarkResult();

        public TabAlignResult AlignResult { get; set; } = new TabAlignResult();

        public AkkonResult AkkonResult { get; set; } = null;

        public void Dispose()
        {
            IsResultProcessDone = false;
            if (Image != null)
            {
                Image.Dispose();
                Image = null;
            }
            if(AkkonInspMatImage != null)
            {
                AkkonInspMatImage.Dispose();
                AkkonInspMatImage = null;
            }
            if (CogImage is CogImage8Grey orgGrey)
            {
                orgGrey.Dispose();
                orgGrey = null;
            }
            if (AkkonInspCogImage is CogImage8Grey inspGrey)
            {
                inspGrey.Dispose();
                inspGrey = null;
            }
            if (AkkonResultCogImage is CogImage24PlanarColor color)
            {
                color.Dispose();
                color = null;
            }

            MarkResult?.Dispose();
            AlignResult?.Dispose();
        }
    }

    public class TabMarkResult
    {
        #region 속성
        public MarkResult FpcMark { get; set; } = null;

        public MarkResult PanelMark { get; set; } = null;

        public double OffsetX { get; set; } = 0;

        public double OffsetY { get; set; } = 0;

        public Judgement Judgement
        {
            get
            {
                if (FpcMark == null || PanelMark == null)
                    return Judgement.FAIL;

                if (FpcMark.Judgement == Judgement.OK && PanelMark.Judgement == Judgement.OK)
                    return Judgement.OK;
                else
                    return Judgement.NG;
            }
        }
        #endregion

        #region 메서드
        public void Dispose()
        {
            FpcMark?.Dispose();
            PanelMark?.Dispose();
        }

        public TabMarkResult DeepCopy()
        {
            TabMarkResult result = new TabMarkResult();
            result.FpcMark = FpcMark?.DeepCopy();
            result.PanelMark = PanelMark?.DeepCopy();

            return result;
        }
        #endregion
    }

    public class MarkResult
    {
        #region 속성
        public Judgement Judgement { get; set; } = Judgement.FAIL;

        public MarkMatchingResult FoundedMark { get; set; } = null;

        public List<MarkMatchingResult> FailMarks { get; set; } = new List<MarkMatchingResult>();
        #endregion

        #region 메서드
        public void Dispose()
        {
            FoundedMark?.Dispose();
            FailMarks.ForEach(x => x.Dispose());
            FailMarks.Clear();
        }

        public MarkResult DeepCopy()
        {
            MarkResult result = new MarkResult();
            result.Judgement = Judgement;
            result.FoundedMark = FoundedMark?.DeepCopy();
            result.FailMarks = FailMarks?.Select(x => x.DeepCopy()).ToList();

            return result;
        }
        #endregion
    }

    public class MarkMatchingResult
    {
        #region 속성
        public VisionProPatternMatchingResult Left { get; set; } = null;

        public VisionProPatternMatchingResult Right { get; set; } = null;
        #endregion

        #region 메서드
        public void Dispose()
        {
            Left?.Dispose();
            Right?.Dispose();
        }

        public MarkMatchingResult DeepCopy()
        {
            MarkMatchingResult result = new MarkMatchingResult();

            result.Left = Left?.DeepCopy();
            result.Right = Right?.DeepCopy();

            return result;
        }
        #endregion
    }

    public class TabAlignResult
    {
        public Judgement Judgement
        {
            get
            {
                if (LeftX == null || LeftY == null || RightX == null || RightY == null)
                    return Judgement.FAIL;

                if (LeftX.Judgement == Judgement.OK && LeftY.Judgement == Judgement.OK && RightX.Judgement == Judgement.OK && RightY.Judgement == Judgement.OK)
                    return Judgement.OK;
                else
                    return Judgement.NG;
            }
        }

        public AlignResult LeftX { get; set; } = null;

        public AlignResult LeftY { get; set; } = null;

        public AlignResult RightX { get; set; } = null;

        public AlignResult RightY { get; set; } = null;

        public float CenterX { get; set; }

        public TabAlignResult DeepCopy()
        {
            TabAlignResult result = new TabAlignResult();
            result.LeftX = LeftX?.DeepCopy();
            result.LeftY = LeftY?.DeepCopy();
            result.RightX = RightX?.DeepCopy();
            result.RightY = RightY?.DeepCopy();
            result.CenterX = CenterX;

            return result;
        }

        public void Dispose()
        {
            LeftX?.Dispose();
            LeftY?.Dispose();
            RightX?.Dispose();
            RightY?.Dispose();
        }
    }

    public class AlignResult
    {
        #region 속성
        public Judgement Judgement
        {
            get
            {
                if (Fpc.Judgement == Judgement.OK && Panel.Judgement == Judgement.OK)
                {
                    if (Math.Abs(ResultValue_pixel) <= JudegementValue_pixel)
                        return Judgement.OK;
                    else
                        return Judgement.NG;
                }
                else
                    return Judgement.NG;
            }
        }

        public float ResultValue_pixel { get; set; } = 0.0f;

        public double JudegementValue_pixel { get; set; }

        public float AvgCenterX { get; set; }

        public float AvgCenterY { get; set; }

        public VisionProAlignCaliperResult Panel { get; set; } = new VisionProAlignCaliperResult();

        public VisionProAlignCaliperResult Fpc { get; set; } = new VisionProAlignCaliperResult();
        #endregion

        #region 메서드
        public bool IsGood()
        {
            if (Panel == null || Fpc == null)
                return false;

            bool isGood = true;
            if (Panel.Judgement != Judgement.OK)
                isGood = false;

            if (Fpc.Judgement != Judgement.OK)
                isGood = false;

            return isGood;
        }

        public void Dispose()
        {
            Panel?.Dispose();
            Fpc?.Dispose();
        }

        public AlignResult DeepCopy()
        {
            AlignResult result = new AlignResult();
            result.ResultValue_pixel = ResultValue_pixel;
            result.Panel = Panel?.DeepCopy();
            result.Fpc = Fpc?.DeepCopy();

            return result;
        }
        #endregion
    }

    public enum TabJudgement
    {
        None = 0,
        OK = 1,     // Mark, Align, Akkon 전부 OK
        NG = 2,     // Align, Akkon 둘 중 한개가 NG
        Mark_NG = 3, // Mark NG
        Manual_OK = 4, // Align, Akkon Manual OK
    }
}
