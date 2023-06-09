﻿using Cognex.VisionPro;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.UI.Controls;
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
        #endregion

        #region 속성
        public Dictionary<int, TabInspResult> InspResultDic { get; set; } = new Dictionary<int, TabInspResult>();

        public List<TabBtnControl> TabBtnControlList { get; private set; } = new List<TabBtnControl>();

        public CogInspAlignDisplayControl InspAlignDisplay { get; private set; } = new CogInspAlignDisplayControl() { Dock = DockStyle.Fill };

        public int CurrentTabNo { get; set; } = -1;
        #endregion

        #region 이벤트
        public event SendTabNumberDelegate SendTabNumber;
        #endregion

        #region 델리게이트
        public delegate void SendTabNumberDelegate(int tabNumber);
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

        private void ButtonControl_SetTabEventHandler(int tabNum)
        {
            TabBtnControlList.ForEach(x => x.SetButtonClickNone());
            TabBtnControlList[tabNum].SetButtonClick();

            CurrentTabNo = tabNum;
            SendTabNumber(tabNum);

            if (InspResultDic.ContainsKey(tabNum))
            {
                UpdateLeftAlignResult(InspResultDic[tabNum]);
                UpdateRightAlignResult(InspResultDic[tabNum]);
            }
            else
                InspAlignDisplay.ClearImage();
        }

        public void UpdateResultDisplay(AppsInspResult inspResult)
        {
            InspAlignDisplay.ClearImage();

            for (int i = 0; i < inspResult.TabResultList.Count(); i++)
            {
                int tabNo = inspResult.TabResultList[i].TabNo;

                if (InspResultDic.ContainsKey(tabNo))
                {
                    InspResultDic[tabNo].Dispose();
                    InspResultDic.Remove(tabNo);
                }

                InspResultDic.Add(tabNo, inspResult.TabResultList[i]);

                if (CurrentTabNo == tabNo)
                {
                    UpdateLeftAlignResult(inspResult.TabResultList[i]);
                    UpdateRightAlignResult(inspResult.TabResultList[i]);
                }
            }
        }

        private void UpdateLeftAlignResult(TabInspResult result)
        {
            List<CogCompositeShape> leftResultList = new List<CogCompositeShape>();
            List<PointF> pointList = new List<PointF>();

            var leftAlignX = result.LeftAlignX;
            if (leftAlignX != null)
            {
                if (leftAlignX.Fpc.CogAlignResult.Count > 0)
                {
                    foreach (var fpc in leftAlignX.Fpc.CogAlignResult)
                    {
                        pointList.Add(fpc.MaxCaliperMatch.FoundPos);

                        var leftFpcX = fpc.MaxCaliperMatch.ResultGraphics;
                        leftResultList.Add(leftFpcX);
                    }
                }
                if (leftAlignX.Panel.CogAlignResult.Count() > 0)
                {
                    foreach (var panel in leftAlignX.Panel.CogAlignResult)
                    {
                        pointList.Add(panel.MaxCaliperMatch.FoundPos);

                        var leftPanelX = panel.MaxCaliperMatch.ResultGraphics;
                        leftResultList.Add(leftPanelX);
                    }
                }
            }

            var leftAlignY = result.LeftAlignY;
            if (leftAlignY != null)
            {
                if (leftAlignY.Fpc.CogAlignResult.Count > 0)
                {
                    if (leftAlignY.Fpc.CogAlignResult[0].MaxCaliperMatch != null)
                    {
                        pointList.Add(leftAlignY.Fpc.CogAlignResult[0].MaxCaliperMatch.FoundPos);

                        var leftFpcY = leftAlignY.Fpc.CogAlignResult[0].MaxCaliperMatch.ResultGraphics;
                        leftResultList.Add(leftFpcY);
                    }
                }

                if (leftAlignY.Panel.CogAlignResult.Count > 0)
                {
                    if (leftAlignY.Panel.CogAlignResult[0].MaxCaliperMatch != null)
                    {
                        pointList.Add(leftAlignY.Panel.CogAlignResult[0].MaxCaliperMatch.FoundPos);
                        var leftPanelY = leftAlignY.Panel.CogAlignResult[0].MaxCaliperMatch.ResultGraphics;
                        leftResultList.Add(leftPanelY);
                    }
                }
            }

            var deepCopyImage = result.CogImage.CopyBase(CogImageCopyModeConstants.CopyPixels);
            InspAlignDisplay.UpdateLeftDisplay(deepCopyImage, leftResultList, GetCenterPoint(pointList));
        }

        private void UpdateRightAlignResult(TabInspResult result)
        {
            List<CogCompositeShape> rightResultList = new List<CogCompositeShape>();
            List<PointF> pointList = new List<PointF>();

            var rightAlignX = result.RightAlignX;
            if (rightAlignX != null)
            {
                if (rightAlignX.Fpc.CogAlignResult.Count > 0)
                {
                    foreach (var fpc in rightAlignX.Fpc.CogAlignResult)
                    {
                        pointList.Add(fpc.MaxCaliperMatch.FoundPos);

                        var rightFpcX = fpc.MaxCaliperMatch.ResultGraphics;
                        rightResultList.Add(rightFpcX);
                    }
                }
                if (rightAlignX.Panel.CogAlignResult.Count() > 0)
                {
                    foreach (var panel in rightAlignX.Panel.CogAlignResult)
                    {
                        pointList.Add(panel.MaxCaliperMatch.FoundPos);

                        var rightPanelX = panel.MaxCaliperMatch.ResultGraphics;
                        rightResultList.Add(rightPanelX);
                    }
                }
            }

            var rightAlignY = result.RightAlignY;
            if (rightAlignY != null)
            {
                if (rightAlignY.Fpc.CogAlignResult.Count > 0)
                {
                    if (rightAlignY.Fpc.CogAlignResult[0].MaxCaliperMatch != null)
                    {
                        pointList.Add(rightAlignY.Fpc.CogAlignResult[0].MaxCaliperMatch.FoundPos);

                        var rightFpcY = rightAlignY.Fpc.CogAlignResult[0].MaxCaliperMatch.ResultGraphics;
                        rightResultList.Add(rightFpcY);
                    }
                }

                if (rightAlignY.Panel.CogAlignResult.Count > 0)
                {
                    if (rightAlignY.Panel.CogAlignResult[0].MaxCaliperMatch != null)
                    {
                        pointList.Add(rightAlignY.Panel.CogAlignResult[0].MaxCaliperMatch.FoundPos);
                        var rightPanelY = rightAlignY.Panel.CogAlignResult[0].MaxCaliperMatch.ResultGraphics;
                        rightResultList.Add(rightPanelY);
                    }
                }
            }
            var deepCopyImage = result.CogImage.CopyBase(CogImageCopyModeConstants.CopyPixels);
            InspAlignDisplay.UpdateRightDisplay(deepCopyImage, rightResultList, GetCenterPoint(pointList));
        }

        private Point GetCenterPoint(List<PointF> pointList)
        {
            if (pointList == null)
                return new Point();

            if (pointList.Count == 0)
                return new Point();

            float minX = pointList.Select(point => point.X).Min();
            float maxX = pointList.Select(point => point.X).Max();

            float minY = pointList.Select(point => point.Y).Min();
            float maxY = pointList.Select(point => point.Y).Max();

            float width = (maxX - minX) / 2.0f;
            float height = (maxY - minY) / 2.0f;

            return new Point((int)(minX + width), (int)(minY + height));
        }
        #endregion
    }
}
