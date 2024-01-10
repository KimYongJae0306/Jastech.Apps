using Cognex.VisionPro;
using Cognex.VisionPro.Caliper;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.Parameters;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using Jastech.Framework.Util;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform.Forms;
using Jastech.Framework.Winform.Helper;
using Jastech.Framework.Winform.VisionPro.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AlignControl : UserControl
    {
        #region 필드
        private string _prevTabName { get; set; } = string.Empty;

        private Color _selectedColor = new Color();

        private Color _nonSelectedColor = new Color();

        private ROIJogForm _roiJogForm { get; set; } = null;

        private List<VisionProAlignCaliperResult> _displayCaliperResult = null;
        #endregion

        #region 속성
        private CogCaliperParamControl CogCaliperParamControl { get; set; } = null;

        private Tab CurrentTab { get; set; } = null;

        private ATTTabAlignName CurrentAlignName { get; set; } = ATTTabAlignName.LeftFPCX;

        private List<VisionProCaliperParam> CaliperList { get; set; } = null;

        private MainAlgorithmTool AlgorithmTool = new MainAlgorithmTool();

        public double Resolution_um { get; set; } = 1.0;
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public AlignControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void AlignControl_Load(object sender, EventArgs e)
        {
            AddControl();
            InitializeLabelColor();
            
            UpdateSelectedAlignName(lblLeftFPCX);
            UpdateParam(ATTTabAlignName.LeftFPCX);
        }

        private void AddControl()
        {
            CogCaliperParamControl = new CogCaliperParamControl();
            CogCaliperParamControl.Dock = DockStyle.Fill;
            CogCaliperParamControl.GetOriginImageHandler += AlignControl_GetOriginImageHandler;
            CogCaliperParamControl.TestActionEvent += AlignControl_TestActionEvent;
            CogCaliperParamControl.CaliperParamChanged += CogCaliperParamControl_CaliperParamChanged;
            pnlCaliperParam.Controls.Add(CogCaliperParamControl);
        }

        private void CogCaliperParamControl_CaliperParamChanged(string component, string parameter, double oldValue, double newValue)
        {
            ParamTrackingLogger.AddChangeHistory($"Tab{CurrentTab.Name} {component}", parameter, oldValue, newValue);
        }

        private void AlignControl_TestActionEvent()
        {
            RunForTest();
        }

        private ICogImage AlignControl_GetOriginImageHandler()
        {
            return TeachingUIManager.Instance().GetOriginCogImageBuffer(true);
        }

        public void SetParams(Tab tab)
        {
            if (tab == null)
                return;

            CurrentTab = tab;
        }

        public void UpdateCurrentParam()
        {
            UpdateParam(CurrentAlignName);
        }

        private void InitializeLabelColor()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);
        }

        private void UpdateParam(ATTTabAlignName alignName)
        {
            CurrentAlignName = alignName;

            var alignParam = CurrentTab.GetAlignParam(alignName);
            if (alignParam == null)
            {
                alignParam = new AlignParam();
                alignParam.Name = alignName.ToString();
                CurrentTab.AlignParamList.Add(alignParam);
            }

            CogCaliperParamControl.UpdateData(alignParam.CaliperParams);

            if(alignName == ATTTabAlignName.CenterFPC)
            {
                pnlLeadParam.Visible = false;
                pnlCaliperParam.Visible = false;
            }
            else
            {
                pnlLeadParam.Visible = true;
                pnlCaliperParam.Visible = true;
            }

            lblLeadCount.Text = alignParam.LeadCount.ToString();
            lblPanelToFpcOffset.Text = alignParam.PanelToFpcOffset.ToString("F2");

            lblLeftAlignSpecX.Text = CurrentTab.AlignSpec.LeftSpecX_um.ToString();
            lblLeftAlignSpecY.Text = CurrentTab.AlignSpec.LeftSpecY_um.ToString();
            mtgUseAutoTracking.Checked = CurrentTab.AlignSpec.UseAutoTracking;

            lblRightAlignSpecX.Text = CurrentTab.AlignSpec.RightSpecX_um.ToString();
            lblRightAlignSpecY.Text = CurrentTab.AlignSpec.RightSpecY_um.ToString();

			lblCenterAlignSpecX.Text = CurrentTab.AlignSpec.CenterSpecX_um.ToString();
 			EnableUIPanelToFpcOffset(alignName);
            DrawROI();
        }

        private void lblLeftFPCX_Click(object sender, EventArgs e)
        {
            UpdateSelectedAlignName(sender);
            UpdateParam(ATTTabAlignName.LeftFPCX);
        }

        private void lblLeftFPCY_Click(object sender, EventArgs e)
        {
            UpdateSelectedAlignName(sender);
            UpdateParam(ATTTabAlignName.LeftFPCY);
        }

        private void lblLeftPanelX_Click(object sender, EventArgs e)
        {
            UpdateSelectedAlignName(sender);
            UpdateParam(ATTTabAlignName.LeftPanelX);
        }

        private void lblLeftPanelY_Click(object sender, EventArgs e)
        {
            UpdateSelectedAlignName(sender);
            UpdateParam(ATTTabAlignName.LeftPanelY);
        }

        private void lblRightFPCX_Click(object sender, EventArgs e)
        {
            UpdateSelectedAlignName(sender);
            UpdateParam(ATTTabAlignName.RightFPCX);
        }

        private void lblRightFPCY_Click(object sender, EventArgs e)
        {
            UpdateSelectedAlignName(sender);
            UpdateParam(ATTTabAlignName.RightFPCY);
        }

        private void lblRightPanelX_Click(object sender, EventArgs e)
        {
            UpdateSelectedAlignName(sender);
            UpdateParam(ATTTabAlignName.RightPanelX);
        }

        private void lblRightPanelY_Click(object sender, EventArgs e)
        {
            UpdateSelectedAlignName(sender);
            UpdateParam(ATTTabAlignName.RightPanelY);
        }

        private void UpdateSelectedAlignName(object sender)
        {
            lblLeftFPCX.BackColor = _nonSelectedColor;
            lblLeftFPCY.BackColor = _nonSelectedColor;
            lblLeftPanelX.BackColor = _nonSelectedColor;
            lblLeftPanelY.BackColor = _nonSelectedColor;

            lblRightFPCX.BackColor = _nonSelectedColor;
            lblRightFPCY.BackColor = _nonSelectedColor;
            lblRightPanelX.BackColor = _nonSelectedColor;
            lblRightPanelY.BackColor = _nonSelectedColor;
            lblCenter.BackColor = _nonSelectedColor;

            Label lbl = sender as Label;
            lbl.BackColor = _selectedColor;
        }

        private void EnableUIPanelToFpcOffset(ATTTabAlignName alignName)
        {
            if (alignName == ATTTabAlignName.LeftPanelX || alignName == ATTTabAlignName.RightPanelX)
                lblPanelToFpcOffset.Enabled = true;
            else
                lblPanelToFpcOffset.Enabled = false;
        }

        private void lblLeadCount_Click(object sender, EventArgs e)
        {
            int leadCount = KeyPadHelper.SetLabelIntegerData((Label)sender);

            var alignParam = CurrentTab.GetAlignParam(CurrentAlignName);
            alignParam.LeadCount = leadCount;
        }

        private void lblPanelToFpcOffset_Click(object sender, EventArgs e)
        {
            double offset = KeyPadHelper.SetLabelDoubleData((Label)sender);

            var alignParam = CurrentTab.GetAlignParam(CurrentAlignName);
            alignParam.PanelToFpcOffset = offset;
        }

        private void lblApply_Click(object sender, EventArgs e)
        {
            Apply();
        }

        public void Apply()
        {
            var alignParam = CurrentTab.GetAlignParam(CurrentAlignName);

            int leadCount = alignParam.LeadCount;

            var currentParam = CogCaliperParamControl.GetCurrentParam();
            CogRectangleAffine rect = new CogRectangleAffine(currentParam.CaliperTool.Region);

            List<CogRectangleAffine> cropRectList = VisionProShapeHelper.DivideRegion(rect, leadCount);

            var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();

            display.ClearGraphic();

            if (display.GetImage() == null)
                return;

            foreach (var cogRect in cropRectList)
                display.SetStaticGraphics("tool", cogRect);
        }

        public void AddROI()
        {
            var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();
            if (display.GetImage() == null)
                return;

            if (ModelManager.Instance().CurrentModel == null)
                return;

            SetNewROI(display);
            DrawROI();
        }

        private void SetNewROI(CogDisplayControl display)
        {
            ICogImage cogImage = display.GetImage();

            double centerX = display.GetImageWidth() / 2.0 - display.GetPan().X;
            double centerY = display.GetImageHeight() / 2.0 - display.GetPan().Y;

            CogRectangleAffine roi = VisionProImageHelper.CreateRectangleAffine(centerX, centerY, 100, 100);

            var currentParam = CogCaliperParamControl.GetCurrentParam();

            currentParam.SetRegion(roi);
        }

        public void DrawROI()
        {
            var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();

            display.ClearGraphic();

            if (display.GetImage() == null)
                return;

            var currentParam = CogCaliperParamControl.GetCurrentParam();
            if (currentParam == null)
                return;

            CogCaliperCurrentRecordConstants constants = CogCaliperCurrentRecordConstants.All;
            display.SetInteractiveGraphics("tool", currentParam.CreateCurrentRecord(constants));

            var rect = currentParam.GetRegion() as CogRectangleAffine;
            if (rect.CenterX > 100 && rect.CenterY > 100)       // 100은 임시..초기값을 구분할 수 있는 인자가 존재하지 않음.
                display.SetDisplayToCenter(new Point((int)rect.CenterX, (int)rect.CenterY));
        }

        public void RunForTest()
        {
            var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();

            var currentParam = CogCaliperParamControl.GetCurrentParam();

            if (display == null || currentParam == null || CurrentTab == null)
                return;

            if (display.GetImage() == null)
                return;

            display.ClearGraphic();
            display.DisplayRefresh();

            var param = CurrentTab.GetAlignParam(CurrentAlignName);

            ICogImage cogImage = display.GetImage();
            ICogImage copyCogImage = cogImage.CopyBase(CogImageCopyModeConstants.CopyPixels);

            VisionProAlignCaliperResult result = new VisionProAlignCaliperResult();

            VisionProCaliperParam inspParam = currentParam.DeepCopy();
    
            bool usePanel = false;
            if (CurrentAlignName.ToString().ToUpper().Contains("PANEL"))
                usePanel = true;

            if (CurrentAlignName.ToString().Contains("X"))
                result = AlgorithmTool.RunAlignX(copyCogImage, inspParam, param.LeadCount, usePanel);
            else
                result = AlgorithmTool.RunAlignY(copyCogImage, inspParam);

            if (result.Judgement == Judgement.FAIL)
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "Caliper is Not Found.";
                form.ShowDialog();
            }
            else
            {
                display.ClearGraphic();
                display.UpdateResult(result);
            }

            result.Dispose();
            inspParam.Dispose();

            VisionProImageHelper.Dispose(ref copyCogImage);
        }

        public void ShowROIJog()
        {
            //ROIJogControl roiJogForm = new ROIJogControl();
            //roiJogForm.SetTeachingItem(TeachingItem.Align);
            //roiJogForm.SendEventHandler += new ROIJogControl.SendClickEventDelegate(ReceiveClickEvent);
            //roiJogForm.ShowDialog();

            if (_roiJogForm == null)
            {
                _roiJogForm = new ROIJogForm();
                _roiJogForm.SetTeachingItem(TeachingItem.Akkon);
                _roiJogForm.SendEventHandler += new ROIJogForm.SendClickEventDelegate(ReceiveClickEvent);
                _roiJogForm.CloseEventDelegate = () => _roiJogForm = null;
                _roiJogForm.Show();
            }
            else
                _roiJogForm.Focus();
        }

        private void ReceiveClickEvent(string jogType, int jogScale, ROIType roiType)
        {
            if (jogType.Contains("Skew"))
                SkewMode(jogType, jogScale);
            else if (jogType.Contains("Move"))
                MoveMode(jogType, jogScale);
            else if (jogType.Contains("Zoom"))
                SizeMode(jogType, jogScale);
            else { }
        }

        private void SkewMode(string skewType, int jogScale)
        {
            if (CurrentTab == null)
                return;

            var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();

            double skewUnit = (double)jogScale / 1000;
            double zoom = display.GetZoomValue();
            skewUnit /= zoom;

            bool isSkewZero = false;

            if (skewType.ToLower().Contains("skewccw"))
                skewUnit *= -1;
            else if (skewType.ToLower().Contains("skewcw"))
                skewUnit *= 1;
            else if (skewType.ToLower().Contains("skewzero"))
                isSkewZero = true;
            else { }

            var currentParam = CogCaliperParamControl.GetCurrentParam();
            CogRectangleAffine roi = new CogRectangleAffine(currentParam.CaliperTool.Region);

            if (isSkewZero)
                roi.Skew = 0;
            else
                roi.Skew += skewUnit;

            currentParam.CaliperTool.Region = roi;
            DrawROI();
        }

        private void MoveMode(string moveType, int jogScale)
        {
            if (CurrentTab == null)
                return;

            int movePixel = jogScale;
            int jogMoveX = 0;
            int jogMoveY = 0;

            if (moveType.ToLower().Contains("moveleft"))
                jogMoveX = movePixel * (-1);
            else if (moveType.ToLower().Contains("moveright"))
                jogMoveX = movePixel * (1);
            else if (moveType.ToLower().Contains("movedown"))
                jogMoveY = movePixel * (1);
            else if (moveType.ToLower().Contains("moveup"))
                jogMoveY = movePixel * (-1);
            else { }

            var currentParam = CogCaliperParamControl.GetCurrentParam();
            CogRectangleAffine roi = new CogRectangleAffine(currentParam.CaliperTool.Region);

            roi.CenterX += jogMoveX;
            roi.CenterY += jogMoveY;

            currentParam.CaliperTool.Region = roi;
            DrawROI();
        }

        private void SizeMode(string sizeType, int jogScale)
        {
            if (CurrentTab == null)
                return;

            int sizePixel = jogScale;
            int jogSizeX = 0;
            int jogSizeY = 0;

            if (sizeType.Contains("ZoomOutHorizontal"))
                jogSizeX = sizePixel * (-1);
            else if (sizeType.Contains("ZoomInHorizontal"))
                jogSizeX = sizePixel * (1);
            else if (sizeType.Contains("ZoomOutVertical"))
                jogSizeY = sizePixel * (-1);
            else if (sizeType.Contains("ZoomInVertical"))
                jogSizeY = sizePixel * (1);
            else { }

            var currentParam = CogCaliperParamControl.GetCurrentParam();
            CogRectangleAffine roi = new CogRectangleAffine(currentParam.CaliperTool.Region);

            roi.SideXLength += jogSizeX;
            roi.SideYLength += jogSizeY;

            currentParam.CaliperTool.Region = roi;
            DrawROI();
        }

        private void lblLeftAlignSpecX_Click(object sender, EventArgs e)
        {
            if (sender is Label label)
            {
                float oldSpec = Convert.ToSingle(label.Text);
                float newSpec = KeyPadHelper.SetLabelFloatData(label);

                if (CurrentTab != null)
                {
                    CurrentTab.AlignSpec.LeftSpecX_um = newSpec;
                    ParamTrackingLogger.AddChangeHistory($"Tab{CurrentTab.Name}", label.Name.Replace("lbl", ""), oldSpec, newSpec);
                }
            }
        }

        private void lblLeftAlignSpecY_Click(object sender, EventArgs e)
        {
            if (sender is Label label)
            {
                float oldSpec = Convert.ToSingle(label.Text);
                float newSpec = KeyPadHelper.SetLabelFloatData(label);

                if (CurrentTab != null)
                {
                    CurrentTab.AlignSpec.LeftSpecY_um = newSpec;
                    ParamTrackingLogger.AddChangeHistory($"Tab{CurrentTab.Name}", label.Name.Replace("lbl", ""), oldSpec, newSpec);
                }
            }
        }

        private void lblRightAlignSpecX_Click(object sender, EventArgs e)
        {
            if (sender is Label label)
            {
                float oldSpec = Convert.ToSingle(label.Text);
                float newSpec = KeyPadHelper.SetLabelFloatData(label);

                if (CurrentTab != null)
                {
                    CurrentTab.AlignSpec.RightSpecX_um = newSpec;
                    ParamTrackingLogger.AddChangeHistory($"Tab{CurrentTab.Name}", label.Name.Replace("lbl", ""), oldSpec, newSpec);
                }
            }
        }

        private void lblRightAlignSpecY_Click(object sender, EventArgs e)
        {
            if (sender is Label label)
            {
                float oldSpec = Convert.ToSingle(label.Text);
                float newSpec = KeyPadHelper.SetLabelFloatData(label);

                if (CurrentTab != null)
                {
                    CurrentTab.AlignSpec.RightSpecY_um = newSpec;
                    ParamTrackingLogger.AddChangeHistory($"Tab{CurrentTab.Name}", label.Name.Replace("lbl", ""), oldSpec, newSpec);
                }
            }
        }

        private void lblCenterAlignSpecX_Click(object sender, EventArgs e)
        {
            if (sender is Label label)
            {
                float oldSpec = Convert.ToSingle(label.Text);
                float newSpec = KeyPadHelper.SetLabelFloatData(label);

                if (CurrentTab != null)
                {
                    CurrentTab.AlignSpec.CenterSpecX_um = newSpec;
                    ParamTrackingLogger.AddChangeHistory($"Tab{CurrentTab.Name}", label.Name.Replace("lbl", ""), oldSpec, newSpec);
                }
            }
        }

        public void UpdateData(TabInspResult inspResult)
        {
            var leftResultX = inspResult.AlignResult.LeftX;
            lblLeftX_Judgement.Text = leftResultX.Judgement.ToString();
            lblLeftX_Value.Text = (leftResultX.ResultValue_pixel * Resolution_um).ToString("F2");

            var leftResultY = inspResult.AlignResult.LeftY;
            lblLeftY_Judgement.Text = leftResultY.Judgement.ToString();
            lblLeftY_Value.Text = (leftResultY.ResultValue_pixel * Resolution_um).ToString("F2");

            var rightResultX = inspResult.AlignResult.RightX;
            lblRightX_Judgement.Text = rightResultX.Judgement.ToString();
            lblRightX_Value.Text = (rightResultX.ResultValue_pixel * Resolution_um).ToString("F2");

            var rightResultY = inspResult.AlignResult.RightY;
            lblRightY_Judgement.Text = rightResultY.Judgement.ToString();
            lblRightY_Value.Text = (rightResultY.ResultValue_pixel * Resolution_um).ToString("F2");

            lblCx_Value.Text = (((leftResultX.ResultValue_pixel * Resolution_um) + (rightResultX.ResultValue_pixel * Resolution_um)) / 2).ToString("F2");
        }

        private void SetFpcCoordinateData(CoordinateTransform fpc, TabInspResult tabInspResult, double leftOffsetX, double leftOffsetY, double rightOffsetX, double rightOffsetY)
        {
            var teachingLeftPoint = tabInspResult.MarkResult.FpcMark.FoundedMark.Left.MaxMatchPos.ReferencePos;
            PointF teachedLeftPoint = new PointF(teachingLeftPoint.X + (float)leftOffsetX, teachingLeftPoint.Y + (float)leftOffsetY);

            PointF searchedLeftPoint = tabInspResult.MarkResult.FpcMark.FoundedMark.Left.MaxMatchPos.FoundPos;

            var teachingRightPoint = tabInspResult.MarkResult.FpcMark.FoundedMark.Right.MaxMatchPos.ReferencePos;
            PointF teachedRightPoint = new PointF(teachingRightPoint.X + (float)rightOffsetX, teachingRightPoint.Y + (float)rightOffsetY);

            PointF searchedRightPoint = tabInspResult.MarkResult.FpcMark.FoundedMark.Right.MaxMatchPos.FoundPos;

            fpc.SetReferenceData(teachedLeftPoint, teachedRightPoint);
            fpc.SetTargetData(searchedLeftPoint, searchedRightPoint);
        }

        private void SetPanelCoordinateData(CoordinateTransform panel, TabInspResult tabInspResult, double leftOffsetX, double leftOffsetY, double rightOffsetX, double rightOffsetY)
        {
            var teachingLeftPoint = tabInspResult.MarkResult.PanelMark.FoundedMark.Left.MaxMatchPos.ReferencePos;
            PointF teachedLeftPoint = new PointF(teachingLeftPoint.X + (float)leftOffsetX, teachingLeftPoint.Y + (float)leftOffsetY);
            PointF searchedLeftPoint = tabInspResult.MarkResult.PanelMark.FoundedMark.Left.MaxMatchPos.FoundPos;

            var teachingRightPoint = tabInspResult.MarkResult.PanelMark.FoundedMark.Right.MaxMatchPos.ReferencePos;
            PointF teachedRightPoint = new PointF(teachingRightPoint.X + (float)rightOffsetX, teachingRightPoint.Y + (float)rightOffsetY);
            PointF searchedRightPoint = tabInspResult.MarkResult.PanelMark.FoundedMark.Right.MaxMatchPos.FoundPos;

            panel.SetReferenceData(teachedLeftPoint, teachedRightPoint);
            panel.SetTargetData(searchedLeftPoint, searchedRightPoint);
        }

        private void AddAlignCaliperResult(VisionProAlignCaliperResult caliperResult)
        {
            _displayCaliperResult = new List<VisionProAlignCaliperResult>();
            _displayCaliperResult.Add(caliperResult);
        }

        private List<VisionProAlignCaliperResult> GetDisplayAlignCaliperResult()
        {
            if (_displayCaliperResult.Count <= 0)
                return null;

            return _displayCaliperResult;
        }

        private void lblCenter_Click(object sender, EventArgs e)
        {
            UpdateSelectedAlignName(sender);
            UpdateParam(ATTTabAlignName.CenterFPC);
        }

        private void lblTest_Click(object sender, EventArgs e)
        {
            if (CurrentAlignName == ATTTabAlignName.LeftPanelX || CurrentAlignName == ATTTabAlignName.RightPanelX)
            {
                double offsetY = Convert.ToDouble(lblOffsetY.Text);

                if (CurrentAlignName == ATTTabAlignName.LeftPanelX)
                    CalcFpcFromPanel(ATTTabAlignName.LeftFPCX, offsetY);

                if (CurrentAlignName == ATTTabAlignName.RightPanelX)
                    CalcFpcFromPanel(ATTTabAlignName.RightFPCX, offsetY);
            }
            else
            {
                MessageConfirmForm confirmForm = new MessageConfirmForm();
                confirmForm.Message = "Select a panel and try again";
                confirmForm.ShowDialog();
                return;
            }
        }

        private void CalcFpcFromPanel(ATTTabAlignName alignName, double offsetY)
        {
            var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();
            if (display.GetImage() == null)
                return;

            var currentParam = CogCaliperParamControl.GetCurrentParam();
            if (currentParam == null)
                return;

            var newRegion = VisionProShapeHelper.MoveTranslationY(currentParam.CaliperTool.Region, offsetY);

            var alignParam = CurrentTab.GetAlignParam(alignName);
            alignParam.CaliperParams.SetRegion(newRegion);

            CogGraphicInteractiveCollection collection = new CogGraphicInteractiveCollection();
            collection.Add(newRegion);
            display.SetInteractiveGraphics("tool", collection);
        }

        private void lblOffsetY_Click(object sender, EventArgs e)
        {
            KeyPadHelper.SetLabelDoubleData((Label)sender);
        }

        private void lblNewTest_Click(object sender, EventArgs e)
        {
            if (CurrentAlignName == ATTTabAlignName.LeftPanelX || CurrentAlignName == ATTTabAlignName.RightPanelX)
            {
                if (CurrentAlignName == ATTTabAlignName.LeftPanelX)
                    tt(ATTTabAlignName.LeftFPCX);

                if (CurrentAlignName == ATTTabAlignName.RightPanelX)
                    tt(ATTTabAlignName.RightFPCX);
            }
            else
            {
                MessageConfirmForm confirmForm = new MessageConfirmForm();
                confirmForm.Message = "Select a panel and try again";
                confirmForm.ShowDialog();
                return;
            }
        }

        private void mtgUseAutoTracking_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrentTab == null)
                return;

            CurrentTab.AlignSpec.UseAutoTracking = mtgUseAutoTracking.Checked;
        }

        private void tt(ATTTabAlignName alignName)
        {
            var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();
            if (display.GetImage() == null)
                return;

            var currentParam = CogCaliperParamControl.GetCurrentParam();
            if (currentParam == null)
                return;

            var alignParam = CurrentTab.GetAlignParam(alignName);

            var diff = VisionProShapeHelper.GetOffsetBetweenCenterPointOfAffineRectangles(alignParam.CaliperParams.GetRegion() as CogRectangleAffine, currentParam.GetRegion() as CogRectangleAffine);
            var diffY = diff.Y;

            double offsetY = Convert.ToDouble(lblPanelToFpcOffset.Text);

            var newRegion = VisionProShapeHelper.MoveTranslationY(alignParam.CaliperParams.GetRegion() as CogRectangleAffine, diffY - offsetY);
            //var newRegion = VisionProShapeHelper.MoveOffsetY(currentParam.CaliperTool.Region, -offset, (alignParam.CaliperParams.GetRegion() as CogRectangleAffine).Skew);
            alignParam.CaliperParams.SetRegion(newRegion);

            CogGraphicInteractiveCollection collection = new CogGraphicInteractiveCollection();
            collection.Add(newRegion);
            display.SetInteractiveGraphics("tool", collection);
        }
        #endregion
    }
}
