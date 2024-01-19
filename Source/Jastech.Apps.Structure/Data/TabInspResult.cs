using Cognex.VisionPro;
using Emgu.CV;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Framework.Algorithms.Akkon.Parameters;
using Jastech.Framework.Algorithms.Akkon.Results;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.Data
{
    public class TabInspResult
    {
        #region 필드
        private static CogColorConstants _fpcColor = CogColorConstants.Purple;

        private static CogColorConstants _panelColor = CogColorConstants.Orange;
        #endregion

        #region 속성
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

                    if (AlignResult != null)
                    {
                        if (AlignResult.Judgement != Framework.Imaging.Result.Judgement.OK)
                            return TabJudgement.NG;
                    }

                    if (AkkonResult != null)
                    {
                        if (AkkonResult.Judgement != Framework.Imaging.Result.Judgement.OK)
                            return TabJudgement.NG;
                    }

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

        public TabAlignResult AlignResult { get; set; } = null; // new TabAlignResult();

        public float Resolution_um { get; set; } = 0.0F;

        public AkkonResult AkkonResult { get; set; } = null;

        public List<CogRectangleAffine> AkkonNGAffineList = new List<CogRectangleAffine>();

        public int ResultSamplingCount => 5;
        #endregion

        #region 메서드
        public void Dispose()
        {
            IsResultProcessDone = false;

            if (Image != null)
            {
                Image.Dispose();
                Image = null;
            }
            if (AkkonInspMatImage != null)
            {
                AkkonInspMatImage.Dispose();
                AkkonInspMatImage = null;
            }
            if (CogImage is CogImage8Grey orgGrey)
            {
                orgGrey.Dispose();
                CogImage = null;
            }
            if (AkkonInspCogImage is CogImage8Grey inspGrey)
            {
                inspGrey.Dispose();
                AkkonInspCogImage = null;
            }
            if (AkkonResultCogImage is CogImage24PlanarColor color)
            {
                color.Dispose();
                AkkonResultCogImage = null;
            }

            MarkResult?.Dispose();
            AlignResult?.Dispose();
            AkkonResult?.Dispose();

            AkkonNGAffineList.ForEach(x => x.Dispose());
            AkkonNGAffineList.Clear();
        }

        public IEnumerable<int> GetAkkonCounts(string position)
        {
            var leadResults = AkkonResult.LeadResultList;

            int offsetLeft = 0;
            int offsetCenter = leadResults.Count / 2;
            int offsetRight = leadResults.Count - ResultSamplingCount;

            IEnumerable<int> samplingResult = null;
            if (position.ToUpper() == "Left".ToUpper())
                samplingResult = leadResults.Select((result) => result.AkkonCount).Skip(offsetLeft).Take(ResultSamplingCount);
            else if (position.ToUpper() == "Right".ToUpper())
                samplingResult = leadResults.Select((result) => result.AkkonCount).Skip(offsetRight).Take(ResultSamplingCount);
            else if (position.ToUpper() == "Center".ToUpper())
                samplingResult = leadResults.Select((result) => result.AkkonCount).Skip(offsetCenter).Take(ResultSamplingCount);
            else
                samplingResult = leadResults.Select((result) => result.AkkonCount).Skip(0).Take(ResultSamplingCount);

            return samplingResult;
        }

        public List<CogLineSegment> GetCenterLineByAlignLeftResult()
        {
            List<CogLineSegment> cogLineSegmentList = new List<CogLineSegment>();

            if (AlignResult.LeftX == null)
                return cogLineSegmentList;

            var fpcCenterLineList = GetFpcCenterLine(AlignResult.LeftX);
            if (fpcCenterLineList.Count > 0)
                cogLineSegmentList.AddRange(fpcCenterLineList);

            var panelCenterLineList = GetPanelCenterLine(AlignResult.LeftX);
            if (panelCenterLineList.Count > 0)
                cogLineSegmentList.AddRange(panelCenterLineList);

            return cogLineSegmentList;
        }

        public List<CogLineSegment> GetCenterLineByAlignRightResult()
        {
            List<CogLineSegment> cogLineSegmentList = new List<CogLineSegment>();

            if (AlignResult.RightX == null)
                return cogLineSegmentList;

            var fpcCenterLineList = GetFpcCenterLine(AlignResult.RightX);
            if (fpcCenterLineList.Count > 0)
                cogLineSegmentList.AddRange(fpcCenterLineList);

            var panelCenterLineList = GetPanelCenterLine(AlignResult.RightX);
            if (panelCenterLineList.Count > 0)
                cogLineSegmentList.AddRange(panelCenterLineList);

            return cogLineSegmentList;
        }

        private List<CogLineSegment> GetFpcCenterLine(AlignResult alignResult)
        {
            List<CogLineSegment> lineList = new List<CogLineSegment>();

            if (alignResult == null)
                return lineList;

            foreach (var result in alignResult.AlignResultList)
            {
                PointF fpcCenter = new PointF((float)(result.FpcCenterX), (float)(result.FpcCenterY));
                PointF panelCenter = new PointF((float)(result.PanelCenterX), (float)(result.PanelCenterY));

                double length = MathHelper.GetDistance(fpcCenter, panelCenter);

                var fpcSkew = result.FpcSkew;

                CogLineSegment fpcLine = new CogLineSegment();
                fpcLine.Color = _fpcColor;
                fpcLine.SetStartLengthRotation(fpcCenter.X, fpcCenter.Y, length, fpcSkew + MathHelper.DegToRad(90));

                lineList.Add(fpcLine);
            }

            return lineList;
        }

        private List<CogLineSegment> GetPanelCenterLine(AlignResult alignResult)
        {
            List<CogLineSegment> lineList = new List<CogLineSegment>();

            if (alignResult == null)
                return lineList;

            foreach (var result in alignResult.AlignResultList)
            {
                PointF fpcCenter = new PointF((float)(result.FpcCenterX), (float)(result.FpcCenterY));
                PointF panelCenter = new PointF((float)(result.PanelCenterX), (float)(result.PanelCenterY));

                double length = MathHelper.GetDistance(fpcCenter, panelCenter);

                var panelSkew = result.PanelSkew;

                CogLineSegment panelLine = new CogLineSegment();
                panelLine.Color = _panelColor;
                panelLine.SetStartLengthRotation(panelCenter.X, panelCenter.Y, length, panelSkew + MathHelper.DegToRad(270));
                lineList.Add(panelLine);
            }
            return lineList;
        }

        public List<AlignGraphicPosition> GetLeftAlignShapeList()
        {
            List<AlignGraphicPosition> leftResultList = new List<AlignGraphicPosition>();

            var leftAlignX = AlignResult.LeftX;
            if (leftAlignX != null)
            {
                if (leftAlignX.Fpc.CogAlignResult.Count > 0)
                {
                    foreach (var fpc in leftAlignX.Fpc.CogAlignResult)
                    {
                        var leftFpcX = fpc?.MaxCaliperMatch.ResultGraphics;
                        var fpcGraphicList = GetAlignGraphicPosition(leftFpcX, true);
                        if (fpcGraphicList.Count > 0)
                            leftResultList.AddRange(fpcGraphicList);
                    }
                }

                if (leftAlignX.Panel.CogAlignResult.Count() > 0)
                {
                    foreach (var panel in leftAlignX.Panel.CogAlignResult)
                    {
                        var leftPanelX = panel?.MaxCaliperMatch.ResultGraphics;
                        var panelGraphicList = GetAlignGraphicPosition(leftPanelX, false);
                        if (panelGraphicList.Count > 0)
                            leftResultList.AddRange(panelGraphicList);
                    }
                }
            }

            var leftAlignY = AlignResult.LeftY;
            if (leftAlignY != null)
            {
                if (leftAlignY.Fpc.CogAlignResult.Count > 0)
                {
                    if (leftAlignY.Fpc.CogAlignResult[0].MaxCaliperMatch != null)
                    {
                        var leftFpcY = leftAlignY.Fpc.CogAlignResult[0].MaxCaliperMatch.ResultGraphics;
                        var fpcGraphicList = GetAlignGraphicPosition(leftFpcY, true);
                        if (fpcGraphicList.Count > 0)
                            leftResultList.AddRange(fpcGraphicList);
                    }
                }

                if (leftAlignY.Panel.CogAlignResult.Count > 0)
                {
                    if (leftAlignY.Panel.CogAlignResult[0].MaxCaliperMatch != null)
                    {
                        var leftPanelY = leftAlignY.Panel.CogAlignResult[0].MaxCaliperMatch.ResultGraphics;
                        var panelGraphicList = GetAlignGraphicPosition(leftPanelY, false);
                        if (panelGraphicList.Count > 0)
                            leftResultList.AddRange(panelGraphicList);
                    }
                }
            }

            var fpcLeftCenterLineList = GetFpcCenterLine(AlignResult.LeftX);
            foreach (var fpc in fpcLeftCenterLineList)
            {
                AlignGraphicPosition position = new AlignGraphicPosition
                {
                    IsFpc = true,
                    StartX = fpc.StartX,
                    StartY = fpc.StartY,
                    EndX = fpc.EndX,
                    EndY = fpc.EndY,
                };
                leftResultList.Add(position);
            }

            var panelLeftCenterLineList = GetPanelCenterLine(AlignResult.LeftX);
            foreach (var panel in panelLeftCenterLineList)
            {
                AlignGraphicPosition position = new AlignGraphicPosition
                {
                    IsFpc = false,
                    StartX = panel.StartX,
                    StartY = panel.StartY,
                    EndX = panel.EndX,
                    EndY = panel.EndY,
                };
                leftResultList.Add(position);
            }

            return leftResultList;
        }

        public List<AlignGraphicPosition> GetRightAlignShapeList()
        {
            List<AlignGraphicPosition> rightResultList = new List<AlignGraphicPosition>();

            var rightAlignX = AlignResult.RightX;
            if (rightAlignX != null)
            {
                if (rightAlignX.Fpc.CogAlignResult.Count > 0)
                {
                    foreach (var fpc in rightAlignX.Fpc.CogAlignResult)
                    {
                        var rightFpcX = fpc?.MaxCaliperMatch.ResultGraphics;
                        var fpcGraphicList = GetAlignGraphicPosition(rightFpcX, true);
                        if (fpcGraphicList.Count > 0)
                            rightResultList.AddRange(fpcGraphicList);
                    }
                }
                if (rightAlignX.Panel.CogAlignResult.Count() > 0)
                {
                    foreach (var panel in rightAlignX.Panel.CogAlignResult)
                    {
                        if (panel != null)
                        {
                            var rightPanelX = panel.MaxCaliperMatch.ResultGraphics;
                            var panelGraphicList = GetAlignGraphicPosition(rightPanelX, false);
                            if (panelGraphicList.Count > 0)
                                rightResultList.AddRange(panelGraphicList);
                        }
                    }
                }
            }

            var rightAlignY = AlignResult.RightY;
            if (rightAlignY != null)
            {
                if (rightAlignY.Fpc.CogAlignResult.Count > 0)
                {
                    if (rightAlignY.Fpc.CogAlignResult[0].MaxCaliperMatch != null)
                    {
                        var rightFpcY = rightAlignY.Fpc.CogAlignResult[0].MaxCaliperMatch.ResultGraphics;
                        var fpcGraphicList = GetAlignGraphicPosition(rightFpcY, true);
                        if (fpcGraphicList.Count > 0)
                            rightResultList.AddRange(fpcGraphicList);
                    }
                }

                if (rightAlignY.Panel.CogAlignResult.Count > 0)
                {
                    if (rightAlignY.Panel.CogAlignResult[0].MaxCaliperMatch != null)
                    {
                        var rightPanelY = rightAlignY.Panel.CogAlignResult[0].MaxCaliperMatch.ResultGraphics;
                        var panelGraphicList = GetAlignGraphicPosition(rightPanelY, true);
                        if (panelGraphicList.Count > 0)
                            rightResultList.AddRange(panelGraphicList);
                    }
                }
            }

            var fpcRightCenterLineList = GetFpcCenterLine(AlignResult.RightX);
            foreach (var fpc in fpcRightCenterLineList)
            {
                AlignGraphicPosition position = new AlignGraphicPosition
                {
                    IsFpc = true,
                    StartX = fpc.StartX,
                    StartY = fpc.StartY,
                    EndX = fpc.EndX,
                    EndY = fpc.EndY,
                };
                rightResultList.Add(position);
            }

            var panelRightCenterLineList = GetPanelCenterLine(AlignResult.RightY);
            foreach (var panel in panelRightCenterLineList)
            {
                AlignGraphicPosition position = new AlignGraphicPosition
                {
                    IsFpc = false,
                    StartX = panel.StartX,
                    StartY = panel.StartY,
                    EndX = panel.EndX,
                    EndY = panel.EndY,
                };
                rightResultList.Add(position);
            }

            return rightResultList;
        }

        private List<AlignGraphicPosition> GetAlignGraphicPosition(CogCompositeShape cogCompositeShape, bool isFpc)
        {
            List<AlignGraphicPosition> positionList = new List<AlignGraphicPosition>();
            if (cogCompositeShape == null)
                return positionList;

            foreach (var item in cogCompositeShape.Shapes)
            {
                if (item is CogLineSegment lineSegment)
                {
                    AlignGraphicPosition alignGraphic = new AlignGraphicPosition
                    {
                        IsFpc = isFpc,
                        StartX = lineSegment.StartX,
                        StartY = lineSegment.StartY,
                        EndX = lineSegment.EndX,
                        EndY = lineSegment.EndY,
                    };

                    positionList.Add(alignGraphic);
                }
            }

            return positionList;
        }
        #endregion
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
                {
                    var cx = GetDoubleCx_um();

                    if (Math.Abs(cx) <= Math.Abs(CxJudegementValue_pixel * Resolution_um))
                        return Judgement.OK;
                    else
                        return Judgement.NG;
                }
                else
                    return Judgement.NG;
            }
        }

        public string PreHead { get; set; } = "-";

        public double CxJudegementValue_pixel { get; set; }

        public AlignResult LeftX { get; set; } = null;

        public AlignResult LeftY { get; set; } = null;

        public AlignResult RightX { get; set; } = null;

        public AlignResult RightY { get; set; } = null;

        public float Resolution_um { get; set; }

        public ICogImage CenterImage { get; set; } = null;

        public TabAlignResult DeepCopy()
        {
            TabAlignResult result = new TabAlignResult();
            result.PreHead = PreHead;
            result.LeftX = LeftX?.DeepCopy();
            result.LeftY = LeftY?.DeepCopy();
            result.RightX = RightX?.DeepCopy();
            result.RightY = RightY?.DeepCopy();
            result.Resolution_um = Resolution_um;
            result.CxJudegementValue_pixel = CxJudegementValue_pixel;
            result.CenterImage = CenterImage?.CopyBase(CogImageCopyModeConstants.CopyPixels);

            return result;
        }

        public string GetStringLx_um()
        {
            if (LeftX == null)
                return "-";

            if (LeftX.AlignMissing)
                return "-";

            double lx = MathHelper.GetFloorDecimal(LeftX.ResultValue_pixel * Resolution_um, 4);

            return MathHelper.GetFloorDecimal(lx, 4).ToString();
        }

        public string GetStringRx_um()
        {
            if (RightX == null)
                return "-";

            if (RightX.AlignMissing)
                return "-";

            double rx = MathHelper.GetFloorDecimal(RightX.ResultValue_pixel * Resolution_um, 4);

            return MathHelper.GetFloorDecimal(rx, 4).ToString();
        }


        public string GetStringLy_um()
        {
            if (LeftY == null)
                return "-";

            if (LeftY.AlignMissing)
                return "-";

            double ly = MathHelper.GetFloorDecimal(LeftY.ResultValue_pixel * Resolution_um, 4);

            return MathHelper.GetFloorDecimal(ly, 4).ToString();
        }

        public string GetStringRy_um()
        {
            if (RightY == null)
                return "-";

            if (RightY.AlignMissing)
                return "-";

            double ry = MathHelper.GetFloorDecimal(RightY.ResultValue_pixel * Resolution_um, 4);

            return MathHelper.GetFloorDecimal(ry, 4).ToString();
        }

        public string GetStringCx_um()
        {
            var lxString = GetStringLx_um();
            var rxString = GetStringRx_um();

            if (lxString == "-" || rxString == "-")
                return "-";

            double cx = (Convert.ToDouble(lxString) + Convert.ToDouble(rxString)) / 2.0;

            return MathHelper.GetFloorDecimal(cx, 4).ToString();
        }
        //
        public double GetDoubleLx_um()
        {
            var lxString = GetStringLx_um();
            if (lxString == "-")
                return 0.0;

            return Convert.ToDouble(lxString);
        }

        public double GetDoubleRx_um()
        {
            var rxString = GetStringRx_um();
            if (rxString == "-")
                return 0.0;

            return Convert.ToDouble(rxString);
        }

        public double GetDoubleLy_um()
        {
            var lyString = GetStringLy_um();
            if (lyString == "-")
                return 0.0;

            return Convert.ToDouble(lyString);
        }

        public double GetDoubleRy_um()
        {
            var ryString = GetStringRy_um();
            if (ryString == "-")
                return 0.0;

            return Convert.ToDouble(ryString);
        }

        public double GetDoubleCx_um()
        {
            var cxString = GetStringCx_um();
            if (cxString == "-")
                return 0.0;

            return Convert.ToDouble(cxString);
        }

        public void Dispose()
        {
            LeftX?.Dispose();
            LeftY?.Dispose();
            RightX?.Dispose();
            RightY?.Dispose();

            if (CenterImage is CogImage8Grey grey)
            {
                grey.Dispose();
                grey = null;
            }
        }
    }

    public class AlignResult
    {
        #region 속성
        public Judgement Judgement
        {
            get
            {
                if(AlignMissing)
                {
                    return Judgement.FAIL;
                }
                else
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
        }

        public bool AlignMissing { get; set; } = false;

        public double ResultValue_pixel { get; set; } = 0.0;

        public double JudegementValue_pixel { get; set; }

        public List<LeadAlignResult> AlignResultList { get; set; } = new List<LeadAlignResult>();

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
            //result.ResultValue_um = ResultValue_um;
            result.Panel = Panel?.DeepCopy();
            result.Fpc = Fpc?.DeepCopy();
            return result;
        }
        #endregion
    }

    public class AlignGraphicPosition
    {
        #region 속성
        public bool IsFpc { get; set; } = false;

        public double StartX { get; set; } = 0;

        public double StartY { get; set; } = 0;

        public double EndX { get; set; } = 0;

        public double EndY { get; set; } = 0;
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
