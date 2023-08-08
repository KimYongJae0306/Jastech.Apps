using Cognex.VisionPro;
using Cognex.VisionPro.Caliper;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using Jastech.Framework.Winform.Forms;
using Jastech.Framework.Winform.Helper;
using Jastech.Framework.Winform.VisionPro.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        #endregion

        #region 속성
        private CogCaliperParamControl CogCaliperParamControl { get; set; } = null;

        private Tab CurrentTab { get; set; } = null;

        private ATTTabAlignName CurrentAlignName { get; set; } = ATTTabAlignName.LeftFPCX;

        private List<VisionProCaliperParam> CaliperList { get; set; } = null;

        private MainAlgorithmTool AlgorithmTool = new MainAlgorithmTool();

        public double Resolution_um { get; set; } = 1.0;
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
            UpdateParam(CurrentAlignName);
        }

        private void AddControl()
        {
            CogCaliperParamControl = new CogCaliperParamControl();
            CogCaliperParamControl.Dock = DockStyle.Fill;
            CogCaliperParamControl.GetOriginImageHandler += AlignControl_GetOriginImageHandler;
            CogCaliperParamControl.TestActionEvent += AlignControl_TestActionEvent;
            pnlCaliperParam.Controls.Add(CogCaliperParamControl);
        }

        private void AlignControl_TestActionEvent()
        {
            Inspection();
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

        private void InitializeLabelColor()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);
        }

        private void UpdateParam(ATTTabAlignName alignName)
        {
            CurrentAlignName = alignName;

            var alignParam = CurrentTab.GetAlignParam(alignName);
            CogCaliperParamControl.UpdateData(alignParam.CaliperParams);
            lblLeadCount.Text = alignParam.LeadCount.ToString();

            lblLeftAlignSpecX.Text = CurrentTab.AlignSpec.LeftSpecX_um.ToString();
            lblLeftAlignSpecY.Text = CurrentTab.AlignSpec.LeftSpecY_um.ToString();

            lblRightAlignSpecX.Text = CurrentTab.AlignSpec.RightSpecX_um.ToString();
            lblRightAlignSpecY.Text = CurrentTab.AlignSpec.RightSpecY_um.ToString();

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

            Label lbl = sender as Label;
            lbl.BackColor = _selectedColor;
        }

        private void lblLeadCount_Click(object sender, EventArgs e)
        {
            int leadCount = KeyPadHelper.SetLabelIntegerData((Label)sender);

            var alignParam = CurrentTab.GetAlignParam(CurrentAlignName);
            alignParam.LeadCount = leadCount;
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

            List<CogRectangleAffine> cropRectList = VisionProImageHelper.DivideRegion(rect, leadCount);

            var display = TeachingUIManager.Instance().GetDisplay();

            display.ClearGraphic();

            if (display.GetImage() == null)
                return;

            foreach (var cogRect in cropRectList)
                display.SetStaticGraphics("tool", cogRect);
        }

        public void AddROI()
        {
            var display = TeachingUIManager.Instance().GetDisplay();
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

            double centerX = display.ImageWidth() / 2.0 - display.GetPan().X;
            double centerY = display.ImageHeight() / 2.0 - display.GetPan().Y;

            CogRectangleAffine roi = VisionProImageHelper.CreateRectangleAffine(centerX, centerY, 100, 100);

            var currentParam = CogCaliperParamControl.GetCurrentParam();

            currentParam.SetRegion(roi);
        }

        public void DrawROI()
        {
            var display = TeachingUIManager.Instance().GetDisplay();

            display.ClearGraphic();

            if (display.GetImage() == null)
                return;

            CogCaliperCurrentRecordConstants constants = CogCaliperCurrentRecordConstants.All;
            var currentParam = CogCaliperParamControl.GetCurrentParam();

            display.SetInteractiveGraphics("tool", currentParam.CreateCurrentRecord(constants));

            var rect = currentParam.GetRegion() as CogRectangleAffine;
            if (rect != null)
                display.SetDisplayToCenter(new Point((int)rect.CenterX, (int)rect.CenterY));
        }

        public void Inspection()
        {
            var display = TeachingUIManager.Instance().GetDisplay();

            var currentParam = CogCaliperParamControl.GetCurrentParam();

            if (display == null || currentParam == null || CurrentTab == null)
                return;

            if (display.GetImage() == null)
                return;

            var param = CurrentTab.GetAlignParam(CurrentAlignName);

            ICogImage cogImage = display.GetImage();
            ICogImage copyCogImage = cogImage.CopyBase(CogImageCopyModeConstants.CopyPixels);

            VisionProAlignCaliperResult result = new VisionProAlignCaliperResult();

            VisionProCaliperParam inspParam = currentParam.DeepCopy();

            if (CurrentAlignName.ToString().Contains("X"))
                result = AlgorithmTool.RunAlignX(copyCogImage, inspParam, param.LeadCount);
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

            var display = TeachingUIManager.Instance().GetDisplay();

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
            float specX = KeyPadHelper.SetLabelFloatData((Label)sender);

            if (CurrentTab != null)
                CurrentTab.AlignSpec.LeftSpecX_um = specX;
        }

        private void lblLeftAlignSpecY_Click(object sender, EventArgs e)
        {
            float specY = KeyPadHelper.SetLabelFloatData((Label)sender);

            if (CurrentTab != null)
                CurrentTab.AlignSpec.LeftSpecY_um = specY;
        }

        private void lblRightAlignSpecX_Click(object sender, EventArgs e)
        {
            float specX = KeyPadHelper.SetLabelFloatData((Label)sender);

            if (CurrentTab != null)
                CurrentTab.AlignSpec.RightSpecX_um = specX;
        }

        private void lblRightAlignSpecY_Click(object sender, EventArgs e)
        {
            float specY = KeyPadHelper.SetLabelFloatData((Label)sender);

            if (CurrentTab != null)
                CurrentTab.AlignSpec.RightSpecY_um = specY;
        }

        public void Run()
        {
            var display = TeachingUIManager.Instance().GetDisplay();

            var currentParam = CogCaliperParamControl.GetCurrentParam();

            if (display == null || currentParam == null || CurrentTab == null)
                return;

            if (display.GetImage() == null)
                return;

            var param = CurrentTab.GetAlignParam(CurrentAlignName);

            ICogImage cogImage = display.GetImage();
            double lx = RunLeftX(cogImage);
            double ly = RunLeftY(cogImage);
            double rx = RunRightX(cogImage);
            double ry = RunRightY(cogImage);

            if (lblLeftX_Value.Text == "-" || lblRightX_Value.Text == "-")
                lblCx_Value.Text = "";
            else
            {
                double cx = (lx + rx) / 2.0;
                lblCx_Value.Text = cx.ToString("F2");
            }
        }

        private double RunLeftX(ICogImage cogImage)
        {
            double judgementX = Resolution_um * CurrentTab.AlignSpec.LeftSpecX_um;

            AlignResult alignResultLeftX = AlgorithmTool.RunMainLeftAlignX(cogImage, CurrentTab, null, null, judgementX);
            double value_um = alignResultLeftX.ResultValue_pixel / Resolution_um;

            lblLeftX_Judgement.Text = alignResultLeftX.Judgement.ToString();
            if (alignResultLeftX.Judgement != Judgement.FAIL)
            {
                lblLeftX_Value.Text = value_um.ToString("F2");
            }
            else
                lblLeftX_Value.Text = "-";

            lblLeftX_FpcX.Text = alignResultLeftX.Fpc.Judgement.ToString();
            lblLeftX_PanelX.Text = alignResultLeftX.Panel.Judgement.ToString();

            return value_um;
        }

        private double RunLeftY(ICogImage cogImage)
        {
            double judgementY = Resolution_um * CurrentTab.AlignSpec.LeftSpecY_um;

            AlignResult alignResultLeftY = AlgorithmTool.RunMainLeftAlignY(cogImage, CurrentTab, null, null, judgementY);

            lblLeftY_Judgement.Text = alignResultLeftY.Judgement.ToString();
            double value_um = alignResultLeftY.ResultValue_pixel / Resolution_um;

            if (alignResultLeftY.Judgement != Judgement.FAIL)
            {
                lblLeftY_Value.Text = value_um.ToString("F2");
            }
            else
                lblLeftY_Value.Text = "-";

            lblLeftY_FpcY.Text = alignResultLeftY.Fpc.Judgement.ToString();
            lblLeftY_PanelY.Text = alignResultLeftY.Panel.Judgement.ToString();

            return value_um;
        }

        private double RunRightX(ICogImage cogImage)
        {
            double judgementX = Resolution_um * CurrentTab.AlignSpec.RightSpecX_um;

            AlignResult alignResultRightX = AlgorithmTool.RunMainRightAlignX(cogImage, CurrentTab, null, null, judgementX);
            double value_um = alignResultRightX.ResultValue_pixel / Resolution_um;

            lblRightX_Judgement.Text = alignResultRightX.Judgement.ToString();
            if (alignResultRightX.Judgement != Judgement.FAIL)
            {
                lblRightX_Value.Text = value_um.ToString("F2");
            }
            else
                lblRightX_Value.Text = "-";

            lblRightX_FpcX.Text = alignResultRightX.Fpc.Judgement.ToString();
            lblRightX_PanelX.Text = alignResultRightX.Panel.Judgement.ToString();

            return value_um;
        }

        private double RunRightY(ICogImage cogImage)
        {
            double judgementY = Resolution_um * CurrentTab.AlignSpec.RightSpecY_um;

            AlignResult alignResultRightY = AlgorithmTool.RunMainRightAlignY(cogImage, CurrentTab, null, null, judgementY);
            double value_um = alignResultRightY.ResultValue_pixel / Resolution_um;

            lblRightY_Judgement.Text = alignResultRightY.Judgement.ToString();
            if (alignResultRightY.Judgement != Judgement.FAIL)
            {
                lblRightY_Value.Text = value_um.ToString("F2");
            }
            else
                lblRightY_Value.Text = "-";

            lblRightY_FpcY.Text = alignResultRightY.Fpc.Judgement.ToString();
            lblRightY_PanelY.Text = alignResultRightY.Panel.Judgement.ToString();

            return value_um;
        }
        #endregion
    }
}
