using Cognex.VisionPro;
using Emgu.CV;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Framework.Algorithms.Akkon.Parameters;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using Jastech.Framework.Util;
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

        public TeachingCoordinate coordinate = new TeachingCoordinate();
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
            {
                _instance = new TeachingData();
            }

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
            foreach (var unit in inspModel.GetUnitList())
            {
                UnitList.Add(unit.DeepCopy());
            }
        }

        public void Dispose()
        {
            foreach (var unit in UnitList)
                unit.Dispose();

            UnitList.Clear();

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
            return UnitList.Where(x => x.Name == name).FirstOrDefault();
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
            if(TabImage != null)
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
        public CoordinateTransform Fpc = new CoordinateTransform();

        public CoordinateTransform Panel = new CoordinateTransform();

        public double FpcLeftOffsetX { get; private set; }

        public double FpcLeftOffsetY { get; private set; }

        public double FpcRightOffsetX { get; private set; }

        public double FpcRightOffsetY { get; private set; }

        public double PanelLeftOffsetX { get; private set; }

        public double PanelLeftOffsetY { get; private set; }

        public double PanelRightOffsetX { get; private set; }

        public double PanelRightOffsetY { get; private set; }
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        #endregion

        #region 메서드
        #endregion

        public void SetAlignFpcLeftOffset(Tab tab, VisionProPatternMatchingResult markResult)
        {
            double fpcRefX = markResult.MaxMatchPos.ReferencePos.X;
            double fpcRefY = markResult.MaxMatchPos.ReferencePos.Y;

            var mainFPCMark = tab.MarkParamter.GetFPCMark(MarkDirection.Left, MarkName.Main, true);
            var mainOrigin = mainFPCMark.InspParam.GetOrigin();

            FpcLeftOffsetX = mainOrigin.TranslationX - fpcRefX;
            FpcLeftOffsetY = mainOrigin.TranslationY - fpcRefY;
        }

        public void SetAlignFpcRightOffset(Tab tab, VisionProPatternMatchingResult markResult)
        {
            double fpcRefX = markResult.MaxMatchPos.ReferencePos.X;
            double fpcRefY = markResult.MaxMatchPos.ReferencePos.Y;

            var mainFPCMark = tab.MarkParamter.GetFPCMark(MarkDirection.Right, MarkName.Main, true);
            var mainOrigin = mainFPCMark.InspParam.GetOrigin();

            FpcRightOffsetX = mainOrigin.TranslationX - fpcRefX;
            FpcRightOffsetY = mainOrigin.TranslationY - fpcRefY;
        }

        public void SetAlignPanelLeftOffset(Tab tab, VisionProPatternMatchingResult markResult)
        {
            double panelRefX = markResult.MaxMatchPos.ReferencePos.X;
            double panelRefY = markResult.MaxMatchPos.ReferencePos.Y;

            var mainPanelMark = tab.MarkParamter.GetPanelMark(MarkDirection.Left, MarkName.Main, true);
            var mainOrigin = mainPanelMark.InspParam.GetOrigin();

            PanelLeftOffsetX = mainOrigin.TranslationX - panelRefX;
            PanelLeftOffsetY = mainOrigin.TranslationY - panelRefY;
        }

        public void SetAlignPanelRightOffset(Tab tab, VisionProPatternMatchingResult markResult)
        {
            double panelRefX = markResult.MaxMatchPos.ReferencePos.X;
            double panelRefY = markResult.MaxMatchPos.ReferencePos.Y;

            var mainPanelMark = tab.MarkParamter.GetPanelMark(MarkDirection.Right, MarkName.Main, true);
            var mainOrigin = mainPanelMark.InspParam.GetOrigin();

            PanelRightOffsetX = mainOrigin.TranslationX - panelRefX;
            PanelRightOffsetY = mainOrigin.TranslationY - panelRefY;
        }

        public void SetFpcTransform(MarkMatchingResult fpcMarkResult)
        {
            var teachLeftPointX = fpcMarkResult.Left.MaxMatchPos.ReferencePos.X + FpcLeftOffsetX;
            var teachLeftPointY = fpcMarkResult.Left.MaxMatchPos.ReferencePos.Y + FpcLeftOffsetY;
            PointF calcTeachLeftPoint = new PointF((float)teachLeftPointX, (float)teachLeftPointY);
            PointF searchedLeftPoint = fpcMarkResult.Left.MaxMatchPos.FoundPos;

            var teachRightPointX = fpcMarkResult.Right.MaxMatchPos.ReferencePos.X + FpcRightOffsetX;
            var teachRightPointY = fpcMarkResult.Right.MaxMatchPos.ReferencePos.Y + FpcRightOffsetY;
            PointF calcTeachRightPoint = new PointF((float)teachRightPointX, (float)teachRightPointY);
            PointF searchedRightPoint = fpcMarkResult.Right.MaxMatchPos.FoundPos;

            Fpc.SetReferenceData(calcTeachLeftPoint, calcTeachRightPoint);
            Fpc.SetTargetData(searchedLeftPoint, searchedRightPoint);

            Fpc.ExecuteCoordinate();
        }

        public void SetFpcReverseTransform(MarkMatchingResult fpcMarkResult)
        {
            var teachLeftPointX = fpcMarkResult.Left.MaxMatchPos.ReferencePos.X + FpcLeftOffsetX;
            var teachLeftPointY = fpcMarkResult.Left.MaxMatchPos.ReferencePos.Y + FpcLeftOffsetY;
            PointF calcTeachLeftPoint = new PointF((float)teachLeftPointX, (float)teachLeftPointY);
            PointF searchedLeftPoint = fpcMarkResult.Left.MaxMatchPos.FoundPos;

            var teachRightPointX = fpcMarkResult.Right.MaxMatchPos.ReferencePos.X + FpcRightOffsetX;
            var teachRightPointY = fpcMarkResult.Right.MaxMatchPos.ReferencePos.Y + FpcRightOffsetY;
            PointF calcTeachRightPoint = new PointF((float)teachRightPointX, (float)teachRightPointY);
            PointF searchedRightPoint = fpcMarkResult.Right.MaxMatchPos.FoundPos;

            Fpc.SetReferenceData(searchedLeftPoint, searchedRightPoint);
            Fpc.SetTargetData(calcTeachLeftPoint, calcTeachRightPoint);

            Fpc.ExecuteCoordinate();
        }

        public void SetPanelTransform(MarkMatchingResult panelMarkResult)
        {
            var teachLeftPointX = panelMarkResult.Left.MaxMatchPos.ReferencePos.X + PanelLeftOffsetX;
            var teachLeftPointY = panelMarkResult.Left.MaxMatchPos.ReferencePos.Y + PanelLeftOffsetY;
            PointF calcTeachLeftPoint = new PointF((float)teachLeftPointX, (float)teachLeftPointY);
            PointF searchedLeftPoint = panelMarkResult.Left.MaxMatchPos.FoundPos;

            var teachRightPointX = panelMarkResult.Right.MaxMatchPos.ReferencePos.X + PanelRightOffsetX;
            var teachRightPointY = panelMarkResult.Right.MaxMatchPos.ReferencePos.Y + PanelRightOffsetY;
            PointF calcTeachRightPoint = new PointF((float)teachRightPointX, (float)teachRightPointY);
            PointF searchedRightPoint = panelMarkResult.Right.MaxMatchPos.FoundPos;

            Panel.SetReferenceData(calcTeachLeftPoint, calcTeachRightPoint);
            Panel.SetTargetData(searchedLeftPoint, searchedRightPoint);

            Panel.ExecuteCoordinate();
        }

        public void SetPanelReverseTransform(MarkMatchingResult panelMarkResult)
        {
            var teachLeftPointX = panelMarkResult.Left.MaxMatchPos.ReferencePos.X + PanelLeftOffsetX;
            var teachLeftPointY = panelMarkResult.Left.MaxMatchPos.ReferencePos.Y + PanelLeftOffsetY;
            PointF calcTeachLeftPoint = new PointF((float)teachLeftPointX, (float)teachLeftPointY);
            PointF searchedLeftPoint = panelMarkResult.Left.MaxMatchPos.FoundPos;

            var teachRightPointX = panelMarkResult.Right.MaxMatchPos.ReferencePos.X + PanelRightOffsetX;
            var teachRightPointY = panelMarkResult.Right.MaxMatchPos.ReferencePos.Y + PanelRightOffsetY;
            PointF calcTeachRightPoint = new PointF((float)teachRightPointX, (float)teachRightPointY);
            PointF searchedRightPoint = panelMarkResult.Right.MaxMatchPos.FoundPos;

            Panel.SetReferenceData(searchedLeftPoint, searchedRightPoint);
            Panel.SetTargetData(calcTeachLeftPoint, calcTeachRightPoint);

            Panel.ExecuteCoordinate();
        }

        public void Excute(Tab tab)
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
            
            foreach (var alignParam in tab.AlignParamList)
            {
                var alignRegion = alignParam.CaliperParams.GetRegion();
                var calcAlignRegion = CoordinateFromFpc(alignRegion as CogRectangleAffine);
                alignParam.CaliperParams.SetRegion(calcAlignRegion);
            }
        }
       
        private CogRectangleAffine CoordinateFromFpc(CogRectangleAffine cogRegion, bool isReverse = false)
        {
            CogRectangleAffine roi = new CogRectangleAffine(cogRegion);
            PointF inputPoint = new PointF();
            inputPoint.X = (float)roi.CenterX;
            inputPoint.Y = (float)roi.CenterY;

            var newPoint = Fpc.GetCoordinate(inputPoint);

            roi.CenterX = newPoint.X;
            roi.CenterY = newPoint.Y;

            return roi;
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
    }
}
