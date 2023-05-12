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

        public Judgement MarkJudgement { get; set; } = Judgement.OK;

        public Judgement AlignJudgement { get; set; } = Judgement.OK;

        public Judgement AkkonJudgement { get; set; } = Judgement.OK;

        public Mat Image { get; set; } = null;

        public ICogImage CogImage { get; set; } = null;

        public ICogImage AkkonResultImage { get; set; } = null;
      
        public MarkResult FpcMark { get; set; } = new MarkResult();

        public MarkResult PanelMark { get; set; } = new MarkResult();
   
        public AlignResult LeftAlignX { get; set; } = null;

        public AlignResult LeftAlignY { get; set; } = null;

        public AlignResult RightAlignX { get; set; } = null;

        public AlignResult RightAlignY { get; set; } = null;

        public AkkonResult AkkonResult { get; set; } = new AkkonResult();

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
            AkkonResult?.Dispose();
        }

        public TabInspResult DeepCopy()
        {
            TabInspResult result = new TabInspResult();

            result.TabNo = TabNo;
            result.MarkJudgement = MarkJudgement;
            result.AlignJudgement = AlignJudgement;
            result.AkkonJudgement = AkkonJudgement;
            result.Image = Image?.Clone();
            result.CogImage = CogImage?.CopyBase(CogImageCopyModeConstants.CopyPixels);
            result.AkkonResultImage = AkkonResultImage?.CopyBase(CogImageCopyModeConstants.CopyPixels);
            result.FpcMark = FpcMark.DeepCopy();
            result.PanelMark = PanelMark.DeepCopy();
            result.LeftAlignX = LeftAlignX.DeepCopy();
            result.LeftAlignY = LeftAlignY.DeepCopy();
            result.RightAlignX = RightAlignX.DeepCopy();
            result.RightAlignY = RightAlignY.DeepCopy();
            result.AkkonResult = AkkonResult.DeepCopy();

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
            result.FoundedMark = FoundedMark.DeepCopy();
            result.FailMarks = FailMarks.Select(x => x.DeepCopy()).ToList();

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
        public Judgement Judgement { get; set; } = Judgement.OK;

        public float X { get; set; } = 0.0f;

        public float Y { get; set; } = 0.0f;

        public CogAlignCaliperResult Panel { get; set; } = null;

        public CogAlignCaliperResult Fpc { get; set; } = null;
        #endregion

        #region 메서드
        public void Dispose()
        {
            Panel?.Dispose();
            Fpc?.Dispose();
        }

        public AlignResult DeepCopy()
        {
            AlignResult result = new AlignResult();
            result.Judgement = Judgement;
            result.X = X;
            result.Y = Y;
            result.Panel = Panel.DeepCopy();
            result.Fpc = Fpc.DeepCopy();

            return result;
        }
        #endregion
    }
}
