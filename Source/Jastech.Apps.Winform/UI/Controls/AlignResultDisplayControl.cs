using Cognex.VisionPro;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform.VisionPro.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace ATT_UT_IPAD.UI.Controls
{
    public partial class AlignResultDisplayControl : UserControl
    {
        #region 필드
        private int _prevTabCount { get; set; } = -1;

        private Color _selectedColor;

        private Color _nonSelectedColor;
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
            pnlInspDisplay.Controls.Add(InspAlignDisplay);
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

        public void UpdateResultDisplay(int tabNo)
        {
            if (TabBtnControlList[tabNo].Enabled == false)
                return;

            var tabInspResult = GetTabInspResultEvent?.Invoke(tabNo);
            if (tabInspResult == null)
                return;
            
            TabBtnControlList[tabNo].SetAlignImage(tabInspResult.CogImage);

            TabBtnControlList[tabNo].SetLeftAlignShape(GetLeftAlignShape(tabInspResult));
            TabBtnControlList[tabNo].SetRightAlignShape(GetRightAlignShape(tabInspResult));

            if (tabInspResult.AlignResult != null)
                TabBtnControlList[tabNo].SetCenterImage(tabInspResult.AlignResult.CenterImage);

            if (tabInspResult != null)
            {
                if (CurrentTabNo == tabNo)
                {
                    UpdateImage(tabNo);
                    // 확인 필요
                    foreach (var result in tabInspResult.AlignResult.LeftX.AlignResultList)
                    {
                        PointF orginPoint = new PointF(tabInspResult.CogImage.Width / 2, tabInspResult.CogImage.Height / 2);
                        PointF fpcCenter = new PointF((float)(result.FpcCenterX - orginPoint.X), (float)(result.FpcCenterY - orginPoint.Y));
                        PointF panelCenter = new PointF((float)(result.PanelCenterX - orginPoint.X), (float)(result.PanelCenterY - orginPoint.Y));

                        double m = MathHelper.RadToDeg(result.FpcSkew);
                        //PointF aPoint = new PointF(3363, 573);
                        //PointF bPoint = new PointF(3391, 769);

                        //var m5 = (bPoint.Y - aPoint.Y) / (bPoint.X - aPoint.X);

                        var degree = Math.Atan(m) * 180.0 / Math.PI;
                        degree += 180;
                        var g22222 = Math.Tan(degree * Math.PI / 180) * -1; ;
                        //y = mx + b

                        var b = fpcCenter.Y - (g22222 * fpcCenter.X);

                        var x = (panelCenter.Y - b ) / g22222;

                        CogLineSegment fpcLine = new CogLineSegment();
                        fpcLine.SetStartEnd(fpcCenter.X + orginPoint.X, fpcCenter.Y +orginPoint.Y, x + orginPoint.X, panelCenter.Y + orginPoint.Y);
                        //fpcLine.SetStartEnd(fpcCenter.X, fpcCenter.Y, x, 1000);
                        //fpcLine.SetStartLengthRotation(fpcCenter.X, fpcCenter.Y, x, m);

                        //fpcLine.SetStartLengthRotation(fpcCenter.X, fpcCenter.Y, x, fpcDegree);
                        fpcLine.Color = CogColorConstants.Blue;
                        InspAlignDisplay.DrawLine(fpcLine);

                        //CogLine cogLine = new CogLine();
                        //cogLine.set(fpcCenter.X, fpcCenter.Y, result.FpcSkew, result.PanelSkew);
                        //InspAlignDisplay.DrawLine(cogLine);

                    }
                }
            }
            else
                InspAlignDisplay.ClearImage();
          
        }

        private void UpdateImage(int tabNo)
        {
            var image = TabBtnControlList[tabNo].GetAlignImage();
            var leftShape = TabBtnControlList[tabNo].GetLeftShape();
            var rightShape = TabBtnControlList[tabNo].GetRightShape();

            InspAlignDisplay.UpdateLeftDisplay(image, leftShape, GetCenterPoint(LeftPointList[tabNo]));
            InspAlignDisplay.UpdateRightDisplay(image, rightShape, GetCenterPoint(RightPointList[tabNo]));

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
                            leftResultList.Add(leftFpcX);
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
                            leftResultList.Add(leftPanelX);
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
                        leftResultList.Add(leftFpcY);
                    }
                }

                if (leftAlignY.Panel.CogAlignResult.Count > 0)
                {
                    if (leftAlignY.Panel.CogAlignResult[0].MaxCaliperMatch != null)
                    {
                        LeftPointList[result.TabNo].Add(leftAlignY.Panel.CogAlignResult[0].MaxCaliperMatch.FoundPos);

                        var leftPanelY = leftAlignY.Panel.CogAlignResult[0].MaxCaliperMatch.ResultGraphics;
                        leftResultList.Add(leftPanelY);
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
                            rightResultList.Add(rightFpcX);
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
                            rightResultList.Add(rightPanelX);
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
                        rightResultList.Add(rightFpcY);
                    }
                }

                if (rightAlignY.Panel.CogAlignResult.Count > 0)
                {
                    if (rightAlignY.Panel.CogAlignResult[0].MaxCaliperMatch != null)
                    {
                        RightPointList[result.TabNo].Add(rightAlignY.Panel.CogAlignResult[0].MaxCaliperMatch.FoundPos);

                        var rightPanelY = rightAlignY.Panel.CogAlignResult[0].MaxCaliperMatch.ResultGraphics;
                        rightResultList.Add(rightPanelY);
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

                float width = (maxX - minX) / 2.0f;
                float height = (maxY - minY) / 2.0f;

                return new PointF((minX + width), (minY + height));
            }
            else
                return new PointF();
        }
        #endregion
    }
}
