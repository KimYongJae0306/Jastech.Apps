﻿using Cognex.VisionPro;
using Emgu.CV;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Framework.Algorithms.Akkon.Parameters;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using Jastech.Framework.Util;
using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Jastech.Apps.Structure.Data
{
    public class TeachingData
    {
        #region 필드
        private static TeachingData _instance = null;
        #endregion

        #region 속성
        public List<Unit> UnitList { get; set; } = new List<Unit>();

        private List<TeachingImageBuffer> ImageBufferList { get; set; } = new List<TeachingImageBuffer>();

        public TeachingCoordinate Coordinate = new TeachingCoordinate();
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        #endregion

        #region 메서드
        public static TeachingData Instance()
        {
            if (_instance == null)
                _instance = new TeachingData();

            return _instance;
        }

        public void UpdateTeachingData()
        {
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            if (inspModel != null)
            {
                Dispose();
                Initialize(inspModel);
            }
        }

        public void Initialize(AppsInspModel inspModel)
        {
            Dispose();
            lock(UnitList)
            {
                foreach (var unit in inspModel.GetUnitList())
                {
                    UnitList.Add(unit.DeepCopy());
                }
            }
        }

        public void Dispose()
        {
            lock (UnitList)
            {
                foreach (var unit in UnitList)
                    unit.Dispose();

                UnitList.Clear();

            }
            ClearTeachingImageBuffer();
        }

        public void ClearTeachingImageBuffer()
        {
            lock (ImageBufferList)
            {
                foreach (var buffer in ImageBufferList)
                    buffer.Dispose();

                ImageBufferList.Clear();
            }
        }

        public TeachingImageBuffer GetBufferImage(int tabNo)
        {
            lock (ImageBufferList)
            {
                if (ImageBufferList.Count() > 0)
                {
                    foreach (var buffer in ImageBufferList)
                    {
                        if (tabNo == buffer.TabNo)
                            return buffer;
                    }
                }
                return null;
            }
        }

        public void AddBufferImage(int tabNo, Mat tabImage)
        {
            lock (ImageBufferList)
                ImageBufferList.Add(new TeachingImageBuffer
                {
                    TabNo = tabNo,
                    TabImage = tabImage
                });
        }

        public ICogImage ConvertCogGrayImage(Mat mat)
        {
            if (mat == null)
                return null;

            int size = mat.Width * mat.Height * mat.NumberOfChannels;
            ColorFormat format = mat.NumberOfChannels == 1 ? ColorFormat.Gray : ColorFormat.RGB24;
            var cogImage = VisionProImageHelper.CovertImage(mat.DataPointer, mat.Width, mat.Height, mat.Step, format);
            return cogImage;
        }

        public Unit GetUnit(string name)
        {
            Unit unit = null;
            lock(UnitList)
                unit = UnitList.Where(x => x.Name == name).FirstOrDefault();

            return unit;
        }

        public List<Tab> GetTabList(string unitName)
        {
            List<Tab> tabList = new List<Tab>();

            Unit unit = GetUnit(unitName);

            for (int i = 0; i < unit.GetTabList().Count; i++)
            {
                var tab = unit.GetTab(i);
                tabList.Add(tab);
            }

            return tabList;
        }
        #endregion
    }

    public class TeachingImageBuffer
    {
        public int TabNo { get; set; }

        public Mat TabImage { get; set; } = null;

        public void Dispose()
        {
            if (TabImage != null)
            {
                TabImage.Dispose();
                TabImage = null;
            }
        }
    }

    public class TeachingCoordinate
    {

        #region 필드
        #endregion

        #region 속성
        public CoordinateTransform Panel = new CoordinateTransform();

        public PointF FpcLeftOffset { get; private set; }

        public PointF FpcRightOffset { get; private set; }

        public PointF PanelLeftOffsetX { get; private set; }

        public PointF PanelRightOffsetX { get; private set; }
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        #endregion

        #region 메서드
        public void SetCoordinateAkkon(Tab tab, MarkMatchingResult panelMarkResult)
        {
            PointF teachingLeftPoint =  GetMarkPanelOrigin(tab, MarkDirection.Left);
            PointF searchedLeftPoint = panelMarkResult.Left.MaxMatchPos.FoundPos;

            PointF teachingRightPoint = GetMarkPanelOrigin(tab, MarkDirection.Right);
            PointF searchedRightPoint = panelMarkResult.Right.MaxMatchPos.FoundPos;

            Panel.SetReferenceData(teachingLeftPoint, teachingRightPoint);
            Panel.SetTargetData(searchedLeftPoint, searchedRightPoint);

            Panel.ExecuteCoordinate();
        }

        public void SetReverseCoordinateAkkon(Tab tab, MarkMatchingResult panelMarkResult)
        {
            PointF teachingLeftPoint = GetMarkPanelOrigin(tab, MarkDirection.Left);
            PointF searchedLeftPoint = panelMarkResult.Left.MaxMatchPos.FoundPos;

            PointF teachingRightPoint = GetMarkPanelOrigin(tab, MarkDirection.Right);
            PointF searchedRightPoint = panelMarkResult.Right.MaxMatchPos.FoundPos;

            Panel.SetReferenceData(searchedLeftPoint, searchedRightPoint);
            Panel.SetTargetData(teachingLeftPoint, teachingRightPoint);

            Panel.ExecuteCoordinate();
        }

        public void ExcuteCoordinateAkkon(Tab tab)
        {
            if (tab == null)
                return;

            foreach (var item in tab.AkkonParam.GroupList)
            {
                var group = tab.GetAkkonGroup(item.Index);

                if (group.AkkonROIList.Count() > 0)
                {
                    var coordinateList = RenewalAkkonRoi(group.AkkonROIList);
                    group.AkkonROIList.Clear();
                    group.AkkonROIList.AddRange(coordinateList);
                }
            }
        }

        private List<AkkonROI> RenewalAkkonRoi(List<AkkonROI> roiList)
        {
            List<AkkonROI> newList = new List<AkkonROI>();

            foreach (var item in roiList)
            {
                PointF leftTop = item.GetLeftTopPoint();
                PointF rightTop = item.GetRightTopPoint();
                PointF leftBottom = item.GetLeftBottomPoint();
                PointF rightBottom = item.GetRightBottomPoint();

                var newLeftTop = Panel.GetCoordinate(leftTop);
                var newRightTop = Panel.GetCoordinate(rightTop);
                var newLeftBottom = Panel.GetCoordinate(leftBottom);
                var newRightBottom = Panel.GetCoordinate(rightBottom);

                AkkonROI akkonRoi = new AkkonROI();

                akkonRoi.SetLeftTopPoint(newLeftTop);
                akkonRoi.SetRightTopPoint(newRightTop);
                akkonRoi.SetLeftBottomPoint(newLeftBottom);
                akkonRoi.SetRightBottomPoint(newRightBottom);

                newList.Add(akkonRoi);
            }

            return newList;
        }

        public void SetCoordinateFpcAlign(Tab tab, MarkMatchingResult fpcMarkResult, bool useAlignCamMark)
        {
            if (fpcMarkResult == null)
                return;

            PointF leftMarkOrigin = new PointF();
            PointF rightMarkOrigin = new PointF();

            if (useAlignCamMark)
            {
                leftMarkOrigin = GetAlignCamMarkFpcOrigin(tab, MarkDirection.Left);
                rightMarkOrigin = GetAlignCamMarkFpcOrigin(tab, MarkDirection.Right);
            }
            else
            {
                leftMarkOrigin = GetMarkFpcOrigin(tab, MarkDirection.Left);
                rightMarkOrigin = GetMarkFpcOrigin(tab, MarkDirection.Right);
            }

            PointF leftFpcOffset = MathHelper.GetOffset(leftMarkOrigin, fpcMarkResult.Left.MaxMatchPos.FoundPos);
            SetFpcLeftOffset(leftFpcOffset);

            PointF fpcRightOffset = MathHelper.GetOffset(rightMarkOrigin, fpcMarkResult.Right.MaxMatchPos.FoundPos);
            SettFpcRighOffset(fpcRightOffset);
        }

        public void SetReverseCoordinateFpcAlign(Tab tab, MarkMatchingResult fpcMarkResult, bool useAlignCamMark)
        {
            if (fpcMarkResult == null)
                return;

            PointF leftMarkOrigin = new PointF();
            PointF rightMarkOrigin = new PointF();

            if (useAlignCamMark)
            {
                leftMarkOrigin = GetAlignCamMarkFpcOrigin(tab, MarkDirection.Left);
                rightMarkOrigin = GetAlignCamMarkFpcOrigin(tab, MarkDirection.Right);
            }
            else
            {
                leftMarkOrigin = GetMarkFpcOrigin(tab, MarkDirection.Left);
                rightMarkOrigin = GetMarkFpcOrigin(tab, MarkDirection.Right);
            }

            PointF leftFpcOffset = MathHelper.GetOffset(fpcMarkResult.Left.MaxMatchPos.FoundPos, leftMarkOrigin);
            SetFpcLeftOffset(leftFpcOffset);

            PointF fpcRightOffset = MathHelper.GetOffset(fpcMarkResult.Right.MaxMatchPos.FoundPos, rightMarkOrigin);
            SettFpcRighOffset(fpcRightOffset);
        }

        public void SetCoordinatePanelAlign(Tab tab, MarkMatchingResult panelMarkResult, bool useAlignCamMark)
        {
            if (panelMarkResult == null)
                return;

            PointF leftMarkOrigin = new PointF();
            PointF rightMarkOrigin = new PointF();

            if (useAlignCamMark)
            {
                leftMarkOrigin = GetAlignCamMarkPanelOrigin(tab, MarkDirection.Left);
                rightMarkOrigin = GetAlignCamMarkPanelOrigin(tab, MarkDirection.Right);
            }
            else
            {
                leftMarkOrigin = GetMarkPanelOrigin(tab, MarkDirection.Left);
                rightMarkOrigin = GetMarkPanelOrigin(tab, MarkDirection.Right);
            }

            PointF panelLeftOffset = MathHelper.GetOffset(leftMarkOrigin, panelMarkResult.Left.MaxMatchPos.FoundPos);
            SetPanelLeftOffset(panelLeftOffset);

            PointF panelRightOffset = MathHelper.GetOffset(rightMarkOrigin, panelMarkResult.Right.MaxMatchPos.FoundPos);
            SetPanelRightOffset(panelRightOffset);
        }

        public void SetReverseCoordinatePanelAlign(Tab tab, MarkMatchingResult panelMarkResult, bool useAlignCamMark)
        {
            if (panelMarkResult == null)
                return;

            PointF leftMarkOrigin = new PointF();
            PointF rightMarkOrigin = new PointF();

            if (useAlignCamMark)
            {
                leftMarkOrigin = GetAlignCamMarkPanelOrigin(tab, MarkDirection.Left);
                rightMarkOrigin = GetAlignCamMarkPanelOrigin(tab, MarkDirection.Right);
            }
            else
            {
                leftMarkOrigin = GetMarkPanelOrigin(tab, MarkDirection.Left);
                rightMarkOrigin = GetMarkPanelOrigin(tab, MarkDirection.Right);
            }

            PointF panelLeftOffset = MathHelper.GetOffset(panelMarkResult.Left.MaxMatchPos.FoundPos, leftMarkOrigin);
            SetPanelLeftOffset(panelLeftOffset);

            PointF panelRightOffset = MathHelper.GetOffset(panelMarkResult.Right.MaxMatchPos.FoundPos, rightMarkOrigin);
            SetPanelRightOffset(panelRightOffset);
        }
    
        public void ExecuteCoordinateAlign(Tab tab)
        {
            foreach (ATTTabAlignName alignName in Enum.GetValues(typeof(ATTTabAlignName)))
            {
                var alignParam = tab.GetAlignParam(alignName);
                if (alignParam == null)
                    alignParam = new Parameters.AlignParam();
                var region = alignParam.CaliperParams.GetRegion() as CogRectangleAffine;

                CogRectangleAffine newRegion = VisionProShapeHelper.AddOffsetToCogRectAffine(region, GetAlignOffset(alignName));
                alignParam.CaliperParams.SetRegion(newRegion);
            }
        }

        public PointF GetMarkFpcOrigin(Tab tab, MarkDirection markDirection)
        {
            var mainMarkParam = tab.Mark.GetFPCMark(markDirection, MarkName.Main);
            var mainMarkOrigin = mainMarkParam.InspParam.GetOrigin();

            PointF mainMarkOriginPoint = new PointF(Convert.ToSingle(mainMarkOrigin.TranslationX), Convert.ToSingle(mainMarkOrigin.TranslationY));

            return mainMarkOriginPoint;
        }

        public PointF GetAlignCamMarkFpcOrigin(Tab tab, MarkDirection markDirection)
        {
            var mainMarkParam = tab.AlignCamMark.GetFPCMark(markDirection, MarkName.Main);
            var mainMarkOrigin = mainMarkParam.InspParam.GetOrigin();

            PointF mainMarkOriginPoint = new PointF(Convert.ToSingle(mainMarkOrigin.TranslationX), Convert.ToSingle(mainMarkOrigin.TranslationY));

            return mainMarkOriginPoint;
        }

        public PointF GetMarkPanelOrigin(Tab tab, MarkDirection markDirection)
        {
            var mainMarkParam = tab.Mark.GetPanelMark(markDirection, MarkName.Main);
            var mainMarkOrigin = mainMarkParam.InspParam.GetOrigin();

            PointF mainMarkOriginPoint = new PointF(Convert.ToSingle(mainMarkOrigin.TranslationX), Convert.ToSingle(mainMarkOrigin.TranslationY));

            return mainMarkOriginPoint;
        }

        public PointF GetAlignCamMarkPanelOrigin(Tab tab, MarkDirection markDirection)
        {
            var mainMarkParam = tab.AlignCamMark.GetPanelMark(markDirection, MarkName.Main);
            var mainMarkOrigin = mainMarkParam.InspParam.GetOrigin();

            PointF mainMarkOriginPoint = new PointF(Convert.ToSingle(mainMarkOrigin.TranslationX), Convert.ToSingle(mainMarkOrigin.TranslationY));

            return mainMarkOriginPoint;
        }

        public void SetFpcLeftOffset(PointF leftFpcOffset)
        {
            FpcLeftOffset = leftFpcOffset;
        }

        public void SettFpcRighOffset(PointF rightFpcOffset)
        {
            FpcRightOffset = rightFpcOffset;
        }

        public void SetPanelLeftOffset(PointF leftPanelOffset)
        {
            PanelLeftOffsetX = leftPanelOffset;
        }

        public void SetPanelRightOffset(PointF rightPanelOffset)
        {
            PanelRightOffsetX = rightPanelOffset;
        }

        private PointF GetFpcLeftOffset()
        {
            return FpcLeftOffset;
        }

        private PointF GetFpcRightOffset()
        {
            return FpcRightOffset;
        }

        private PointF GetPanelLeftOffset()
        {
            return PanelLeftOffsetX;
        }

        private PointF GetPanelRightOffset()
        {
            return PanelRightOffsetX;
        }

        public PointF GetAlignOffset(ATTTabAlignName alignName)
        {
            PointF offset = new PointF();

            switch (alignName)
            {
                case ATTTabAlignName.LeftFPCX:
                case ATTTabAlignName.LeftFPCY:
                    offset = GetFpcLeftOffset();
                    break;

                case ATTTabAlignName.RightFPCX:
                case ATTTabAlignName.RightFPCY:
                    offset = GetFpcRightOffset();
                    break;

                case ATTTabAlignName.LeftPanelX:
                case ATTTabAlignName.LeftPanelY:
                    offset = GetPanelLeftOffset();
                    break;

                case ATTTabAlignName.RightPanelX:
                case ATTTabAlignName.RightPanelY:
                    offset = GetPanelRightOffset();
                    break;

                case ATTTabAlignName.CenterFPC:
                    break;
                default:
                    break;
            }

            return offset;
        }
        #endregion
    }
}
