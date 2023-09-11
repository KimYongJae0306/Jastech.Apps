using Cognex.VisionPro;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media.Animation;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class TabBtnControl : UserControl
    {
        #region 필드
        public int _tabIndex { get; protected set; } = -1;

        private object _lock = new object();
        #endregion

        #region 속성
        private ICogImage CogOrgImage { get; set; } = null;

        private ICogImage CogResultImage { get; set; } = null;

        private ICogImage CogInspImage { get; set; } = null;

        private ICogImage CogAlignImage { get; set; } = null;

        private ICogImage CogCenterImage { get; set; } = null;

        private AlignShapeResult LeftShapeResultList { get; set; } = new AlignShapeResult();

        private AlignShapeResult RightShapeResultList { get; set; } = new AlignShapeResult();

        private List<CogRectangleAffine> AkkonNGAffineList { get; set; } = new List<CogRectangleAffine>();

        public string Lx { get; set; } = "-";//0.0F;

        public string Ly { get; set; } = "-";//0.0F;

        public string Rx { get; set; } = "-";//0.0F;

        public string Ry { get; set; } = "-";//0.0F;

        #endregion

        #region 이벤트
        public event SetTabDelegate SetTabEventHandler;
        #endregion

        #region 델리게이트
        public delegate void SetTabDelegate(int tabNum);
        #endregion

        #region 생성자
        public TabBtnControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void TabBtnControl_Load(object sender, EventArgs e)
        {
            // UI 상 Tab 1부터 보여주기 위함
            btnTab.Text = "TAB " + (_tabIndex + 1).ToString();
        }

        public void SetTabIndex(int tabIndex)
        {
            _tabIndex = tabIndex;
        }

        public void SetOrgImage(ICogImage cogImage)
        {
            lock(_lock)
            {
                if (CogOrgImage != null)
                {
                    if (CogOrgImage is CogImage8Grey grey)
                    {
                        grey.Dispose();
                        grey = null;
                    }
                    if (CogOrgImage is CogImage24PlanarColor color)
                    {
                        color.Dispose();
                        color = null;
                    }
                }
                CogOrgImage = cogImage;//?.CopyBase(CogImageCopyModeConstants.CopyPixels);
            }
        }

        public void SetResultImage(ICogImage cogImage)
        {
            lock (_lock)
            {
                if (CogResultImage != null)
                {
                    if (CogResultImage is CogImage8Grey grey)
                    {
                        grey.Dispose();
                        grey = null;
                    }
                    if (CogResultImage is CogImage24PlanarColor color)
                    {
                        color.Dispose();
                        color = null;
                    }
                }
                CogResultImage = cogImage;//?.CopyBase(CogImageCopyModeConstants.CopyPixels);
            }
        }

        public void SetAkkonNGAffineRectList(List<CogRectangleAffine> akkonNGList)
        {
            AkkonNGAffineList.ForEach(x => x.Dispose());
            AkkonNGAffineList.Clear();

            AkkonNGAffineList = akkonNGList;
        }

        public void SetInspImage(ICogImage cogImage)
        {
            lock (_lock)
            {
                if (CogInspImage != null)
                {
                    if (CogInspImage is CogImage8Grey grey)
                    {
                        grey.Dispose();
                        grey = null;
                    }
                    if (CogInspImage is CogImage24PlanarColor color)
                    {
                        color.Dispose();
                        color = null;
                    }
                }
                CogInspImage = cogImage;//?;.CopyBase(CogImageCopyModeConstants.CopyPixels);
            }
        }

        public void SetAlignImage(ICogImage cogImage)
        {
            lock (_lock)
            {
                if (CogAlignImage != null)
                {
                    if (CogAlignImage is CogImage8Grey grey)
                    {
                        grey.Dispose();
                        grey = null;
                    }
                    if (CogAlignImage is CogImage24PlanarColor color)
                    {
                        color.Dispose();
                        color = null;
                    }
                }
                CogAlignImage = cogImage;//?.CopyBase(CogImageCopyModeConstants.CopyPixels);
            }
        }

        public void SetCenterImage(ICogImage cogImage)
        {
            lock (_lock)
            {
                if (CogCenterImage != null)
                {
                    if (CogCenterImage is CogImage8Grey grey)
                    {
                        grey.Dispose();
                        grey = null;
                    }
                    if (CogCenterImage is CogImage24PlanarColor color)
                    {
                        color.Dispose();
                        color = null;
                    }
                }
                CogCenterImage = cogImage;//?.CopyBase(CogImageCopyModeConstants.CopyPixels);
            }
        }


        public void SetLeftAlignShapeResult(List<CogCompositeShape> shapeList, List<CogLineSegment> lineSegmentList)
        {
            foreach (var shape in LeftShapeResultList.CaliperShapeList)
            {
                if(shape != null)
                    shape.Dispose();
            }
            LeftShapeResultList.CaliperShapeList.Clear();

            foreach (var lineSegment in LeftShapeResultList.LineSegmentList)
            {
                if (lineSegment != null)
                    lineSegment.Dispose();
            }
            LeftShapeResultList.LineSegmentList.Clear();

            LeftShapeResultList.CaliperShapeList.AddRange(shapeList);
            LeftShapeResultList.LineSegmentList.AddRange(lineSegmentList);
        }

        public void SetRightAlignShapeResult(List<CogCompositeShape> shapeList, List<CogLineSegment> lineSegmentList)
        {
            foreach (var shape in RightShapeResultList.CaliperShapeList)
            {
                if (shape != null)
                    shape.Dispose();
            }
            RightShapeResultList.CaliperShapeList.Clear();

            foreach (var lineSegment in RightShapeResultList.LineSegmentList)
            {
                if (lineSegment != null)
                    lineSegment.Dispose();
            }
            RightShapeResultList.LineSegmentList.Clear();

            RightShapeResultList.CaliperShapeList.AddRange(shapeList);
            RightShapeResultList.LineSegmentList.AddRange(lineSegmentList);
        }

        public ICogImage GetCogInspImage()
        {
            lock (_lock)
                return CogInspImage;
        }

        public void UpdateData()
        {
            SetTabEventHandler?.Invoke(_tabIndex);
        }

        private void btnTab_Click(object sender, EventArgs e)
        {
            UpdateData();
        }

        public void SetButtonClick()
        {
            btnTab.Text = "*TAB " + (_tabIndex + 1).ToString();
        }

        public void SetButtonClickNone()
        {
            btnTab.Text = "TAB " + (_tabIndex + 1).ToString();
        }

        public ICogImage GetOrgImage()
        {
            lock (_lock)
                return CogOrgImage;
        }

        public ICogImage GetCogResultImage()
        {
            lock (_lock)
                return CogResultImage;
        }
        public ICogImage GetAlignImage()
        {
            lock (_lock)
                return CogAlignImage;
        }

        public ICogImage GetCenterImage()
        {
            lock (_lock)
                return CogCenterImage;
        }

        public List<CogRectangleAffine> GetAkkonNGAffineRectList()
        {
            return AkkonNGAffineList;
        }

        public AlignShapeResult GetLeftShapeResult()
        {
            return LeftShapeResultList;
        }

        public AlignShapeResult GetRightShapeResult()
        {
            return RightShapeResultList;
        }
        #endregion
    }

    public class AlignShapeResult
    {
        public List<CogCompositeShape> CaliperShapeList { get; set; } = new List<CogCompositeShape>();

        public List<CogLineSegment> LineSegmentList { get; set; } = new List<CogLineSegment>();
    }
}
