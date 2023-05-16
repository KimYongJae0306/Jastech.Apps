using Cognex.VisionPro;
using Emgu.CV;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using Jastech.Framework.Macron.Akkon.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.Data
{
    public class AppsInspResult
    {
        public DateTime StartInspTime { get; set; }

        public DateTime EndInspTime { get; set; }

        public string LastInspTime { get; set; }

        public string Cell_ID { get; set; } = "";

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

        public Judgement Judgement { get; set; }

        public Mat Image { get; set; } = null;

        public ICogImage CogImage { get; set; } = null;

        public ICogImage AkkonResultImage { get; set; } = null;
      
        public MarkResult FpcMark { get; set; }

        public MarkResult PanelMark { get; set; }

        public AlignResult LeftAlignX { get; set; } = null;

        public AlignResult LeftAlignY { get; set; } = null;

        public AlignResult RightAlignX { get; set; } = null;

        public AlignResult RightAlignY { get; set; } = null;

        public float CenterX { get; set; }

        public AkkonResult AkkonResult { get; set; } = null;

        public bool IsMarkGood()
        {
            if (FpcMark == null || PanelMark == null)
                return false;

            bool isGood = true;
            if (FpcMark.Judgement != Judgement.OK)
                isGood = false;

            if (PanelMark.Judgement != Judgement.OK)
                isGood = false;

            return isGood;
        }

        public bool IsLeftAlignXGood()
        {
            if (LeftAlignX.Fpc == null || LeftAlignX.Panel == null)
                return false;

            bool isGood = true;
            if (LeftAlignX.Fpc.Judgement != Judgement.OK)
                isGood = false;

            if (LeftAlignX.Panel.Judgement != Judgement.OK)
                isGood = false;

            return isGood;
        }

        public bool IsLeftAlignYGood()
        {
            if (LeftAlignY.Fpc == null || LeftAlignY.Panel == null)
                return false;

            bool isGood = true;
            if (LeftAlignY.Fpc.Judgement != Judgement.OK)
                isGood = false;

            if (LeftAlignY.Panel.Judgement != Judgement.OK)
                isGood = false;

            return isGood;
        }

        public bool IsRightAlignXGood()
        {
            if (RightAlignX.Fpc == null || RightAlignX.Panel == null)
                return false;

            bool isGood = true;
            if (RightAlignX.Fpc.Judgement != Judgement.OK)
                isGood = false;

            if (RightAlignX.Panel.Judgement != Judgement.OK)
                isGood = false;

            return isGood;
        }

        public bool IsRightAlignYGood()
        {
            if (RightAlignY.Fpc == null || RightAlignY.Panel == null)
                return false;

            bool isGood = true;
            if (RightAlignY.Fpc.Judgement != Judgement.OK)
                isGood = false;

            if (RightAlignY.Panel.Judgement != Judgement.OK)
                isGood = false;

            return isGood;
        }

        public Judgement IsAlignGood()
        {
            Judgement = Judgement.OK;

            if (IsLeftAlignXGood() && IsLeftAlignYGood() && IsRightAlignXGood() && IsRightAlignYGood())
                Judgement = Judgement.OK;
            else
                Judgement = Judgement.NG;

            return Judgement;
        }

        public void Dispose()
        {
            if (Image != null)
            {
                Image.Dispose();
                Image = null;
            }
            CogImage = null;
            AkkonResultImage = null;

            FpcMark?.Dispose();
            PanelMark?.Dispose();
            LeftAlignX?.Dispose();
            LeftAlignY?.Dispose();
            RightAlignX?.Dispose();
            RightAlignY?.Dispose();
            AkkonResult.Dispose();
        }

        public TabInspResult DeepCopy()
        {
            TabInspResult result = new TabInspResult();

            result.TabNo = TabNo;
            result.Judgement = Judgement;
            result.Image = Image?.Clone();
            result.CogImage = CogImage?.CopyBase(CogImageCopyModeConstants.CopyPixels);
            result.AkkonResultImage = AkkonResultImage?.CopyBase(CogImageCopyModeConstants.CopyPixels);
            result.FpcMark = FpcMark?.DeepCopy();
            result.PanelMark = PanelMark?.DeepCopy();
            result.LeftAlignX = LeftAlignX?.DeepCopy();
            result.LeftAlignY = LeftAlignY?.DeepCopy();
            result.RightAlignX = RightAlignX?.DeepCopy();
            result.RightAlignY = RightAlignY?.DeepCopy();
            result.CenterX = CenterX;
            result.AkkonResult = AkkonResult?.DeepCopy();

            return result;

        }
    }
    public class MarkResult
    {
        #region 속성
        public Judgement Judgement { get; set; } = Judgement.OK;

        public double TranslateX { get; set; } = 0;

        public double TranslateY { get; set; } = 0;

        public double TranslateRotion { get; set; } = 0;

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
            result.TranslateX = TranslateX;
            result.TranslateY = TranslateY;
            result.TranslateRotion = TranslateRotion;
            result.FoundedMark = FoundedMark?.DeepCopy();
            result.FailMarks = FailMarks?.Select(x => x.DeepCopy()).ToList();

            return result;
        }
        #endregion
    }

    public class MarkMatchingResult
    {
        #region 속성
        public CogPatternMatchingResult Left { get; set; } = null;

        public CogPatternMatchingResult Right { get; set; } = null;
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

    public class AlignResult
    {
        #region 속성
        public Judgement Judgement { get; set; }

        public float ResultValue { get; set; } = 0.0f;

        public CogAlignCaliperResult Panel { get; set; } = null;

        public CogAlignCaliperResult Fpc { get; set; } = null;
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
            result.ResultValue = ResultValue;
            result.Panel = Panel.DeepCopy();
            result.Fpc = Fpc.DeepCopy();

            return result;
        }
        #endregion
    }
}
