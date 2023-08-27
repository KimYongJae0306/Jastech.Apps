using Cognex.VisionPro;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform.VisionPro.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ATT_UT_IPAD.UI.Controls
{
    public partial class AlignResultDisplayControl : UserControl
    {
        #region 필드
        private int _prevTabCount { get; set; } = -1;

        private Color _selectedColor;

        private Color _nonSelectedColor;

        private CogColorConstants _fpcColor = CogColorConstants.Purple;

        private CogColorConstants _panelColor = CogColorConstants.Orange;
        #endregion

        #region 속성
        public List<TabBtnControl> TabBtnControlList { get; private set; } = new List<TabBtnControl>();

        public CogInspAlignDisplayControl InspAlignDisplay { get; private set; } = null;

        public int CurrentTabNo { get; set; } = -1;

        private List<PointF>[] LeftPointList { get; set; } = null;

        private List<PointF>[] RightPointList { get; set; } = null;
        #endregion

        #region 이벤트
        public event SendTabNumberDelegate SendTabNumberEvent;

        public event GetTabInspResultDele GetTabInspResultEvent;
        #endregion

        #region 델리게이트
        public delegate void SendTabNumberDelegate(int tabNo);

        public delegate TabInspResult GetTabInspResultDele(int tabNo);
        #endregion

        #region 생성자
        public AlignResultDisplayControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void AlignResultDisplayControl_Load(object sender, EventArgs e)
        {
            AddControl();
            InitializeUI();
        }

        private void AddControl()
        {
            InspAlignDisplay = new CogInspAlignDisplayControl();
            InspAlignDisplay.Dock = DockStyle.Fill;
            InspAlignDisplay.UpdateLeftImage += UpdateLeftImageEvent;
            InspAlignDisplay.UpdateRightImage += UpdateRightImageEvent;

            pnlInspDisplay.Controls.Add(InspAlignDisplay);
        }

        private void UpdateRightImageEvent(bool isResult)
        {
            int tabNo = CurrentTabNo;

            var image = TabBtnControlList[tabNo].GetAlignImage();
            if (image == null)
                return;

            if (isResult)
            {
                var rightShape = TabBtnControlList[tabNo].GetRightShapeResult();

                string lx = TabBtnControlList[tabNo].Lx.ToString("F2");
                string rx = TabBtnControlList[tabNo].Rx.ToString("F2");
                string ry = TabBtnControlList[tabNo].Ry.ToString("F2");
                string cx = ((Convert.ToDouble(lx) + Convert.ToDouble(rx)) / 2.0).ToString("F2");

                InspAlignDisplay.UpdateRightDisplay(image, rightShape.CaliperShapeList, rightShape.LineSegmentList, GetCenterPoint(RightPointList[tabNo]));
                InspAlignDisplay.DrawRightResult("X Align : " + rx + " um", 0);
                InspAlignDisplay.DrawRightResult("Y Align : " + ry + " um", 1);
                InspAlignDisplay.DrawRightResult("CX Align : " + cx + " um", 2);
            }
            else
            {
                InspAlignDisplay.UpdateRightDisplay(image);
            }
        }

        private void UpdateLeftImageEvent(bool isResult)
        {
            int tabNo = CurrentTabNo;

            var image = TabBtnControlList[tabNo].GetAlignImage();

            if (image == null)
                return;

            if(isResult)
            {
                var leftShape = TabBtnControlList[tabNo].GetLeftShapeResult();

                string lx = TabBtnControlList[tabNo].Lx.ToString("F2");
                string ly = TabBtnControlList[tabNo].Ly.ToString("F2");
                string rx = TabBtnControlList[tabNo].Rx.ToString("F2");
                string cx = ((Convert.ToDouble(lx) + Convert.ToDouble(rx)) / 2.0).ToString("F2");

                InspAlignDisplay.UpdateLeftDisplay(image, leftShape.CaliperShapeList, leftShape.LineSegmentList, GetCenterPoint(LeftPointList[tabNo]));
                InspAlignDisplay.DrawLeftResult("X Align : " + lx + " um", 0);
                InspAlignDisplay.DrawLeftResult("Y Align : " + ly + " um", 1);
                InspAlignDisplay.DrawLeftResult("CX Align : " + cx + " um", 2);
            }
            else
            {
                InspAlignDisplay.UpdateLeftDisplay(image);
            }
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            if (inspModel == null)
                UpdateTabCount(1);
            else
                UpdateTabCount(inspModel.TabCount);
        }

        public void Enable(bool isEnable)
        {
            InspAlignDisplay.Enable(isEnable);
            BeginInvoke(new Action(() => Enabled = isEnable));
        }

        public void UpdateTabCount(int tabCount)
        {
            if (_prevTabCount == tabCount)
                return;

            ClearTabBtnList();

            int controlWidth = 100;
            Point point = new Point(0, 0);

            for (int i = 0; i < tabCount; i++)
            {
                TabBtnControl buttonControl = new TabBtnControl();
                buttonControl.SetTabIndex(i);
                buttonControl.SetTabEventHandler += ButtonControl_SetTabEventHandler;
                buttonControl.Size = new Size(controlWidth, (int)(pnlTabButton.Height));
                buttonControl.Location = point;

                pnlTabButton.Controls.Add(buttonControl);
                point.X += controlWidth;
                TabBtnControlList.Add(buttonControl);
            }

            LeftPointList = new List<PointF>[tabCount];
            RightPointList = new List<PointF>[tabCount];

            if (TabBtnControlList.Count > 0)
                TabBtnControlList[0].UpdateData();

            _prevTabCount = tabCount;
        }

        private void ClearTabBtnList()
        {
            foreach (var btn in TabBtnControlList)
            {
                btn.SetTabEventHandler -= ButtonControl_SetTabEventHandler;
            }
            TabBtnControlList.Clear();
            pnlTabButton.Controls.Clear();
        }

        private void ButtonControl_SetTabEventHandler(int tabNo)
        {
            if (AppsStatus.Instance().IsInspRunnerFlagFromPlc)
                return;

            TabBtnControlList.ForEach(x => x.SetButtonClickNone());
            TabBtnControlList[tabNo].SetButtonClick();

            CurrentTabNo = tabNo;
            UpdateImage(tabNo);
            SendTabNumberEvent(tabNo);
        }

        public delegate void TabButtonResetColorDele();
        public void TabButtonResetColor()
        {
            if (this.InvokeRequired)
            {
                TabButtonResetColorDele callback = TabButtonResetColor;
                BeginInvoke(callback);
                return;
            }

            TabBtnControlList.ForEach(x => x.BackColor = Color.FromArgb(52, 52, 52));
        }

        private List<CogLineSegment> GetCenterLineByAlignLeftResult(int tabNo)
        {
            var tabInspResult = GetTabInspResultEvent?.Invoke(tabNo);

            List<CogLineSegment> cogLineSegmentList = new List<CogLineSegment>();

            if (tabInspResult == null || tabInspResult.AlignResult.LeftX == null)
                return cogLineSegmentList;

            foreach (var result in tabInspResult.AlignResult.LeftX.AlignResultList)
            {
                PointF fpcCenter = new PointF((float)(result.FpcCenterX), (float)(result.FpcCenterY));
                var fpcSkew = result.FpcSkew;

                PointF panelCenter = new PointF((float)(result.PanelCenterX), (float)(result.PanelCenterY));
                var panelSkew = result.PanelSkew;

                double length = MathHelper.GetDistance(fpcCenter, panelCenter);

                CogLineSegment fpcLine = new CogLineSegment();
                fpcLine.Color = _fpcColor;
                fpcLine.SetStartLengthRotation(fpcCenter.X, fpcCenter.Y, length, fpcSkew + MathHelper.DegToRad(90));
                cogLineSegmentList.Add(fpcLine);

                CogLineSegment panelLine = new CogLineSegment();
                panelLine.Color = _panelColor;
                panelLine.SetStartLengthRotation(panelCenter.X, panelCenter.Y, length, panelSkew + MathHelper.DegToRad(270));
                cogLineSegmentList.Add(panelLine);
            }
            return cogLineSegmentList;
        }

        private List<CogLineSegment> GetCenterLineByAlignRightResult(int tabNo)
        {
            var tabInspResult = GetTabInspResultEvent?.Invoke(tabNo);

            List<CogLineSegment> cogLineSegmentList = new List<CogLineSegment>();

            if (tabInspResult == null || tabInspResult.AlignResult.RightX == null)
                return cogLineSegmentList;

            foreach (var result in tabInspResult.AlignResult.RightX.AlignResultList)
            {
                PointF fpcCenter = new PointF((float)(result.FpcCenterX), (float)(result.FpcCenterY));
                var fpcSkew = result.FpcSkew;

                PointF panelCenter = new PointF((float)(result.PanelCenterX), (float)(result.PanelCenterY));
                var panelSkew = result.PanelSkew;

                double length = MathHelper.GetDistance(fpcCenter, panelCenter);

                CogLineSegment fpcLine = new CogLineSegment();
                fpcLine.Color = _fpcColor;
                fpcLine.SetStartLengthRotation(fpcCenter.X, fpcCenter.Y, length, fpcSkew + MathHelper.DegToRad(90));
                cogLineSegmentList.Add(fpcLine);

                CogLineSegment panelLine = new CogLineSegment();
                panelLine.Color = _panelColor;
                panelLine.SetStartLengthRotation(panelCenter.X, panelCenter.Y, length, panelSkew + MathHelper.DegToRad(270));
                cogLineSegmentList.Add(panelLine);
            }
            return cogLineSegmentList;
        }

        public void UpdateResultDisplay(int tabNo)
        {
            var tabInspResult = GetTabInspResultEvent?.Invoke(tabNo);
            if (tabInspResult == null)
                return;
            
            TabBtnControlList[tabNo].SetAlignImage(tabInspResult.CogImage);

            TabBtnControlList[tabNo].SetLeftAlignShapeResult(GetLeftAlignShape(tabInspResult), GetCenterLineByAlignLeftResult(tabNo));
            TabBtnControlList[tabNo].SetRightAlignShapeResult(GetRightAlignShape(tabInspResult), GetCenterLineByAlignRightResult(tabNo));

            if(tabInspResult.AlignResult.LeftX != null)
                TabBtnControlList[tabNo].Lx = tabInspResult.AlignResult.LeftX.ResultValue_pixel * tabInspResult.Resolution_um;
            if(tabInspResult.AlignResult.LeftY != null)
                TabBtnControlList[tabNo].Ly = tabInspResult.AlignResult.LeftY.ResultValue_pixel * tabInspResult.Resolution_um;
            if(tabInspResult.AlignResult.RightX != null)
                TabBtnControlList[tabNo].Rx = tabInspResult.AlignResult.RightX.ResultValue_pixel * tabInspResult.Resolution_um;
            if(tabInspResult.AlignResult.RightY != null)
                TabBtnControlList[tabNo].Ry = tabInspResult.AlignResult.RightY.ResultValue_pixel * tabInspResult.Resolution_um;

            if (tabInspResult.AlignResult != null)
                TabBtnControlList[tabNo].SetCenterImage(tabInspResult.AlignResult.CenterImage);

            if (tabInspResult != null)
            {
                if (CurrentTabNo == tabNo)
                    UpdateImage(tabNo);
            }
            else
                InspAlignDisplay.ClearImage();
        }

        private void UpdateImage(int tabNo)
        {
            var image = TabBtnControlList[tabNo].GetAlignImage();
            var leftShape = TabBtnControlList[tabNo].GetLeftShapeResult();
            var rightShape = TabBtnControlList[tabNo].GetRightShapeResult();

            string lx = TabBtnControlList[tabNo].Lx.ToString("F2");
            string ly = TabBtnControlList[tabNo].Ly.ToString("F2");
            string rx = TabBtnControlList[tabNo].Rx.ToString("F2");
            string ry = TabBtnControlList[tabNo].Ry.ToString("F2");

            string cx = ((Convert.ToDouble(lx) + Convert.ToDouble(rx)) / 2.0).ToString("F2");
            
            InspAlignDisplay.UpdateLeftDisplay(image, leftShape.CaliperShapeList, leftShape.LineSegmentList, GetCenterPoint(LeftPointList[tabNo]));
            if(InspAlignDisplay.IsLeftResultImageView)
            {
                InspAlignDisplay.DrawLeftResult("X Align : " + lx + " um", 0);
                InspAlignDisplay.DrawLeftResult("Y Align : " + ly + " um", 1);
                InspAlignDisplay.DrawLeftResult("CX Align : " + cx + " um", 2);
            }

            InspAlignDisplay.UpdateRightDisplay(image, rightShape.CaliperShapeList, rightShape.LineSegmentList, GetCenterPoint(RightPointList[tabNo]));
            if(InspAlignDisplay.IsRightResultImageView)
            {
                InspAlignDisplay.DrawRightResult("X Align : " + rx + " um", 0);
                InspAlignDisplay.DrawRightResult("Y Align : " + ry + " um", 1);
                InspAlignDisplay.DrawRightResult("CX Align : " + cx + " um", 2);
            }

            var centerImage = TabBtnControlList[tabNo].GetCenterImage();
            InspAlignDisplay.UpdateCenterDisplay(centerImage);
        }

        public delegate void UpdateTabButtonDele(int tabNo);
        public void UpdateResultTabButton(int tabNo)
        {
            if(this.InvokeRequired)
            {
                UpdateTabButtonDele callback = UpdateResultTabButton;
                BeginInvoke(callback, tabNo);
                return;
            }

            var tabInspResult = GetTabInspResultEvent?.Invoke(tabNo);

            if(tabInspResult != null)
            {
                if (tabInspResult.AlignResult.Judgement == Judgement.OK)
                    TabBtnControlList[tabNo].BackColor = Color.MediumSeaGreen;
                else
                    TabBtnControlList[tabNo].BackColor = Color.Red;
            }
        }

        private List<CogCompositeShape> GetLeftAlignShape(TabInspResult result)
        {
            if (result == null)
                return new List<CogCompositeShape>();

            List<CogCompositeShape> leftResultList = new List<CogCompositeShape>();
            LeftPointList[result.TabNo] = new List<PointF>();

            var leftAlignX = result.AlignResult.LeftX;
            if (leftAlignX != null)
            {
                if (leftAlignX.Fpc.CogAlignResult.Count > 0)
                {
                    foreach (var fpc in leftAlignX.Fpc.CogAlignResult)
                    {
                        if (fpc != null)
                        {
                            LeftPointList[result.TabNo].Add(fpc.MaxCaliperMatch.FoundPos);

                            var leftFpcX = fpc.MaxCaliperMatch.ResultGraphics;
                            if (leftFpcX != null)
                            {
                                leftFpcX.Color = _fpcColor;
                                leftFpcX.LineWidthInScreenPixels = 2;
                                leftResultList.Add(leftFpcX);
                            }
                        }
                    }
                }

                if (leftAlignX.Panel.CogAlignResult.Count() > 0)
                {
                    foreach (var panel in leftAlignX.Panel.CogAlignResult)
                    {
                        if (panel != null)
                        {
                            LeftPointList[result.TabNo].Add(panel.MaxCaliperMatch.FoundPos);

                            var leftPanelX = panel.MaxCaliperMatch.ResultGraphics;
                            if (leftPanelX != null)
                            {
                                leftPanelX.Color = _panelColor;
                                leftPanelX.LineWidthInScreenPixels = 2;
                                leftResultList.Add(leftPanelX);
                            }
                        }
                    }
                }
            }

            var leftAlignY = result.AlignResult.LeftY;
            if (leftAlignY != null)
            {
                if (leftAlignY.Fpc.CogAlignResult.Count > 0)
                {
                    if (leftAlignY.Fpc.CogAlignResult[0].MaxCaliperMatch != null)
                    {
                        LeftPointList[result.TabNo].Add(leftAlignY.Fpc.CogAlignResult[0].MaxCaliperMatch.FoundPos);

                        var leftFpcY = leftAlignY.Fpc.CogAlignResult[0].MaxCaliperMatch.ResultGraphics;
                        if (leftFpcY != null)
                        {
                            leftFpcY.Color = _fpcColor;
                            leftResultList.Add(leftFpcY);
                        }
                    }
                }

                if (leftAlignY.Panel.CogAlignResult.Count > 0)
                {
                    if (leftAlignY.Panel.CogAlignResult[0].MaxCaliperMatch != null)
                    {
                        LeftPointList[result.TabNo].Add(leftAlignY.Panel.CogAlignResult[0].MaxCaliperMatch.FoundPos);

                        var leftPanelY = leftAlignY.Panel.CogAlignResult[0].MaxCaliperMatch.ResultGraphics;
                        if (leftPanelY != null)
                        {
                            leftPanelY.Color = _panelColor;
                            leftResultList.Add(leftPanelY);
                        }
                    }
                }
            }

            return leftResultList;
        }

        private List<CogCompositeShape> GetRightAlignShape(TabInspResult result)
        {
            if (result == null)
                return new List<CogCompositeShape>();

            List<CogCompositeShape> rightResultList = new List<CogCompositeShape>();
            RightPointList[result.TabNo] = new List<PointF>();

            var rightAlignX = result.AlignResult.RightX;
            if (rightAlignX != null)
            {
                if (rightAlignX.Fpc.CogAlignResult.Count > 0)
                {
                    foreach (var fpc in rightAlignX.Fpc.CogAlignResult)
                    {
                        if (fpc != null)
                        {
                            RightPointList[result.TabNo].Add(fpc.MaxCaliperMatch.FoundPos);

                            var rightFpcX = fpc.MaxCaliperMatch.ResultGraphics;
                            if (rightFpcX != null)
                            {
                                rightFpcX.Color = _fpcColor;
                                rightFpcX.LineWidthInScreenPixels = 2;
                                rightResultList.Add(rightFpcX);
                            }
                        }
                    }
                }
                if (rightAlignX.Panel.CogAlignResult.Count() > 0)
                {
                    foreach (var panel in rightAlignX.Panel.CogAlignResult)
                    {
                        if (panel != null)
                        {
                            RightPointList[result.TabNo].Add(panel.MaxCaliperMatch.FoundPos);

                            var rightPanelX = panel.MaxCaliperMatch.ResultGraphics;
                            if (rightPanelX != null)
                            {
                                rightPanelX.Color = _panelColor;
                                rightPanelX.LineWidthInScreenPixels = 2;
                                rightResultList.Add(rightPanelX);
                            }
                        }
                    }
                }
            }

            var rightAlignY = result.AlignResult.RightY;
            if (rightAlignY != null)
            {
                if (rightAlignY.Fpc.CogAlignResult.Count > 0)
                {
                    if (rightAlignY.Fpc.CogAlignResult[0].MaxCaliperMatch != null)
                    {
                        RightPointList[result.TabNo].Add(rightAlignY.Fpc.CogAlignResult[0].MaxCaliperMatch.FoundPos);

                        var rightFpcY = rightAlignY.Fpc.CogAlignResult[0].MaxCaliperMatch.ResultGraphics;
                        if (rightFpcY != null)
                        {
                            rightFpcY.Color = _fpcColor;
                            rightResultList.Add(rightFpcY);
                        }
                    }
                }

                if (rightAlignY.Panel.CogAlignResult.Count > 0)
                {
                    if (rightAlignY.Panel.CogAlignResult[0].MaxCaliperMatch != null)
                    {
                        RightPointList[result.TabNo].Add(rightAlignY.Panel.CogAlignResult[0].MaxCaliperMatch.FoundPos);

                        var rightPanelY = rightAlignY.Panel.CogAlignResult[0].MaxCaliperMatch.ResultGraphics;
                        if (rightPanelY != null)
                        {
                            rightPanelY.Color = _panelColor;
                            rightResultList.Add(rightPanelY);
                        }
                    }
                }
            }
            return rightResultList;
        }

        private PointF GetCenterPoint(List<PointF> pointList)
        {
            if (pointList == null)
                return new PointF();
            if (pointList.Count() == 0)
                return new PointF();

            List<PointF> tempPointList = new List<PointF>();

            foreach (var point in pointList)
            {
                if (point.X != 0 && point.Y != 0)
                    tempPointList.Add(point);
            }

            if (tempPointList.Count() > 0)
            {
                float minX = tempPointList.Select(point => point.X).Min();
                float maxX = tempPointList.Select(point => point.X).Max();

                float minY = tempPointList.Select(point => point.Y).Min();
                float maxY = tempPointList.Select(point => point.Y).Max();

                float width =  Math.Abs(maxX - minX) / 2.0f;
                float height = Math.Abs(maxY - minY) / 2.0f;

                return new PointF((minX + width), (minY + height));
                //return new PointF((minX + width), minY + 300);
            }
            else
                return new PointF();
        }
        #endregion
    }
}
