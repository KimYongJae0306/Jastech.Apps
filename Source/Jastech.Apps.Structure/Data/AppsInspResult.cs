using Cognex.VisionPro;
using Emgu.CV;
using Jastech.Framework.Algorithms.Akkon.Results;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jastech.Apps.Structure.Data
{
    public class AppsInspResult
    {
        public DateTime StartInspTime { get; set; }

        public DateTime EndInspTime { get; set; }

        public string LastInspTime { get; set; }

        public string Cell_ID { get; set; } = "";

        public PreAlignResult PreAlignResult { get; set; } = new PreAlignResult();

        public List<TabInspResult> TabResultList { get; set; } = new List<TabInspResult>();

        public void Dispose()
        {
            for (int i = 0; i < TabResultList.Count(); i++)
            {
                TabResultList[i].Dispose();
                TabResultList[i] = null;
            }

            TabResultList.Clear();
        }

        public AppsInspResult DeepCopy()
        {
            AppsInspResult result = new AppsInspResult();
            result.StartInspTime = StartInspTime;
            result.EndInspTime = EndInspTime;
            result.LastInspTime = LastInspTime;
            result.Cell_ID = Cell_ID;
            result.TabResultList = TabResultList.Select(x => x.DeepCopy()).ToList();

            return result;
        }
    }

    public class TabInspResult
    {
        public int TabNo { get; set; } = -1;

        public Judgment Judgement { get; set; }

        public Mat Image { get; set; } = null;

        public ICogImage CogImage { get; set; } = null;

        public ICogImage AkkonInspImage { get; set; } = null;

        public ICogImage AkkonResultImage { get; set; } = null;

        public TabMarkResult MarkResult { get; set; } = new TabMarkResult();

        public TabAlignResult AlignResult { get; set; } = new TabAlignResult();

        public AkkonResult AkkonResult { get; set; } = null;

        public void Dispose()
        {
            if (Image != null)
            {
                Image.Dispose();
                Image = null;
            }
            if (CogImage is CogImage8Grey orgGrey)
            {
                orgGrey.Dispose();
                orgGrey = null;
            }
            if (AkkonInspImage is CogImage8Grey inspGrey)
            {
                inspGrey.Dispose();
                inspGrey = null;
            }
            if (AkkonResultImage is CogImage24PlanarColor color)
            {
                color.Dispose();
                color = null;
            }

            MarkResult?.Dispose();
            AlignResult?.Dispose();
        }

        public TabInspResult DeepCopy()
        {
            TabInspResult result = new TabInspResult();

            result.TabNo = TabNo;
            result.Judgement = Judgement;
            result.Image = Image?.Clone();
            result.CogImage = CogImage?.CopyBase(CogImageCopyModeConstants.CopyPixels);
            result.AkkonInspImage = AkkonInspImage?.CopyBase(CogImageCopyModeConstants.CopyPixels);
            result.AkkonResultImage = AkkonResultImage?.CopyBase(CogImageCopyModeConstants.CopyPixels);
            result.MarkResult = MarkResult?.DeepCopy();
            result.AlignResult = AlignResult?.DeepCopy();

            return result;
        }
    }

    public class PreAlignResult
    {
        public MarkResult PreAlignMark { get; set; } = new MarkResult();

        public ICogImage CogImage { get; set; } = null;

        public double OffsetX { get; private set; } = 0.0;

        public double OffsetY { get; private set; } = 0.0;

        public double OffsetT { get; private set; } = 0.0;

        public Judgment Judgement { get; set; } = Judgment.OK;

        public void SetPreAlignResult(double offsetX, double offsetY, double offsetT)
        {
            OffsetX = offsetX;
            OffsetY = offsetY;
            OffsetT = offsetT;
        }
    }

    public class TabMarkResult
    {
        public MarkResult FpcMark { get; set; } = null;

        public MarkResult PanelMark { get; set; } = null;

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
        public bool IsGood()
        {
            if (FpcMark == null || PanelMark == null)
                return false;

            bool isGood = true;
            if (FpcMark.Judgement != Judgment.OK)
                isGood = false;

            if (PanelMark.Judgement != Judgment.OK)
                isGood = false;

            return isGood;
        }

    }

    public class MarkResult
    {
        #region 속성
        public Judgment Judgement { get; set; } = Judgment.OK;

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
        public Judgment Judgment { get; set; }

        public AlignResult LeftX { get; set; } = null;

        public AlignResult LeftY { get; set; } = null;

        public AlignResult RightX { get; set; } = null;

        public AlignResult RightY { get; set; } = null;

        public float CenterX { get; set; }

        public TabAlignResult DeepCopy()
        {
            TabAlignResult result = new TabAlignResult();
            result.Judgment = Judgment;
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

        public Judgment IsAlignGood()
        {
            Judgment = Judgment.OK;

            if (IsLeftXGood() && IsLeftYGood() && IsRightXGood() && IsRightYGood())
                Judgment = Judgment.OK;
            else
                Judgment = Judgment.NG;

            return Judgment;
        }

        public bool IsLeftXGood()
        {
            if (LeftX.Fpc == null || LeftX.Panel == null)
                return false;

            bool isGood = true;
            if (LeftX.Fpc.Judgement != Judgment.OK)
                isGood = false;

            if (LeftX.Panel.Judgement != Judgment.OK)
                isGood = false;

            return isGood;
        }

        public bool IsLeftYGood()
        {
            if (LeftY.Fpc == null || LeftY.Panel == null)
                return false;

            bool isGood = true;
            if (LeftY.Fpc.Judgement != Judgment.OK)
                isGood = false;

            if (LeftY.Panel.Judgement != Judgment.OK)
                isGood = false;

            return isGood;
        }

        public bool IsRightXGood()
        {
            if (RightX.Fpc == null || RightX.Panel == null)
                return false;

            bool isGood = true;
            if (RightX.Fpc.Judgement != Judgment.OK)
                isGood = false;

            if (RightX.Panel.Judgement != Judgment.OK)
                isGood = false;

            return isGood;
        }

        public bool IsRightYGood()
        {
            if (RightY.Fpc == null || RightY.Panel == null)
                return false;

            bool isGood = true;
            if (RightY.Fpc.Judgement != Judgment.OK)
                isGood = false;

            if (RightY.Panel.Judgement != Judgment.OK)
                isGood = false;

            return isGood;
        }

    }

    public class AlignResult
    {
        #region 속성
        public Judgment Judgement { get; set; }

        public float ResultValue_pixel { get; set; } = 0.0f;

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
            if (Panel.Judgement != Judgment.OK)
                isGood = false;

            if (Fpc.Judgement != Judgment.OK)
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
}
