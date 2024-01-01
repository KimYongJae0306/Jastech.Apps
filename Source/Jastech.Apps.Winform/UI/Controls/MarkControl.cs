using Cognex.VisionPro;
using Cognex.VisionPro.Caliper;
using Cognex.VisionPro.PMAlign;
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
using Jastech.Framework.Winform.VisionPro.Controls;
using Jastech.Framework.Winform.VisionPro.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class MarkControl : UserControl
    {
        #region 필드
        private AlgorithmTool Algorithm = new AlgorithmTool();

        private string _curTabNo { get; set; } = "";

        private Color _selectedColor = new Color();

        private Color _nonSelectedColor = new Color();

        private Material _curMaterial = Material.Panel;

        private MarkName _curMarkName = MarkName.Main;

        private MarkDirection _curDirection = MarkDirection.Left;

        private ROIJogForm _roiJogForm { get; set; } = null;
        #endregion

        #region 속성
        private VisionProPointMarkerParam PointMarkerParam { get; set; } = null;

        private CogPatternMatchingParamControl ParamControl { get; set; } = null;

        private Tab CurrentTab { get; set; } = null;

        public bool UseAlignCamMark { get; set; } = false;
        #endregion

        #region 이벤트
        public event SetOriginDelegate SetOriginEventHandler;

        public event ParameterValueChangedEventHandler MarkParamChanged;
        #endregion

        #region 델리게이트
        public delegate void SetOriginDelegate(PointF originPoint);

        public delegate void ParameterValueChangedEventHandler(string component, string parameter, double oldValue, double newValue);
        #endregion

        #region 생성자
        public MarkControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void MarkControl_Load(object sender, EventArgs e)
        {
            AddControl();
            InitializeUI();

            SelectMaterial(Material.Panel);
            lblLeftMain.BackColor = _selectedColor;

            _curDirection = MarkDirection.Left;
            _curMarkName = MarkName.Main;
            UpdateParam();
        }

        private void AddControl()
        {
            ParamControl = new CogPatternMatchingParamControl();
            ParamControl.Dock = DockStyle.Fill;
            ParamControl.GetOriginImageHandler += PatternControl_GetOriginImageHandler;
            ParamControl.TestActionEvent += PatternControl_TestActionEvent;
            ParamControl.ClearActionEvent += PatternControl_ClearActionEvent;
            ParamControl.MarkParamChanged += ParamControl_MarkParamChanged;
            pnlParam.Controls.Add(ParamControl);

            if (PointMarkerParam != null)
                PointMarkerParam.SetOriginEventHandler -= PointMarkerParam_SetOriginEventHandler;

            PointMarkerParam = new VisionProPointMarkerParam();
            PointMarkerParam.SetOriginEventHandler += PointMarkerParam_SetOriginEventHandler;
        }

        private void PointMarkerParam_SetOriginEventHandler(CogTransform2DLinear originPoint)
        {
            var currentParam = ParamControl.GetCurrentParam();
            currentParam.SetOrigin(originPoint);
        }

        private void PatternControl_ClearActionEvent()
        {
            var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();
            var currentParam = ParamControl.GetCurrentParam();

            if (display == null || currentParam == null)
                return;

            ICogImage cogImage = display.GetImage();
            if (cogImage == null)
                return;
            display.ClearGraphic();
        }

        private void PatternControl_TestActionEvent()
        {
            RunForTest();
        }

        private ICogImage PatternControl_GetOriginImageHandler()
        {
            return TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay().GetImage();
        }

        private void ParamControl_MarkParamChanged(string component, string parameter, double oldValue, double newValue)
        {
            MarkParamChanged?.Invoke($"{CurrentTab.Name} {component}", parameter, oldValue, newValue);
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            if (CurrentTab == null)
                return;

            var markParam = CurrentTab.GetMarkParamter(UseAlignCamMark);

            if (markParam.PanelMarkList.Count > 0)
                lblPanel.Visible = true;
            else
                lblPanel.Visible = false;

            if (markParam.FpcMarkList.Count > 0)
                lblFpc.Visible = true;
            else
                lblFpc.Visible = false;
        }

        public void SetParams(Tab tab)
        {
            if (tab == null)
                return;

            CurrentTab = tab;
            UpdateParam();
        }

        private void UpdateParam()
        {
            if (CurrentTab == null)
                return;

            MarkParam currentParam = null;

            if (_curMaterial == Material.Fpc)
                currentParam = CurrentTab.GetMarkParamter(UseAlignCamMark).GetFPCMark(_curDirection, _curMarkName);
            else
                currentParam = CurrentTab.GetMarkParamter(UseAlignCamMark).GetPanelMark(_curDirection, _curMarkName);

            if (currentParam != null)
                ParamControl?.UpdateData(currentParam.InspParam);

            DrawROI();
        }

        private void lblFpc_Click(object sender, EventArgs e)
        {
            SelectMaterial(Material.Fpc);
            UpdateParam();
        }

        private void lblPanel_Click(object sender, EventArgs e)
        {
            SelectMaterial(Material.Panel);
            UpdateParam();
        }

        private void SelectMaterial(Material material)
        {
            _curMaterial = material;

            if (material == Material.Fpc)
            {
                lblFpc.BackColor = _selectedColor;
                lblPanel.BackColor = _nonSelectedColor;
            }
            else
            {
                lblFpc.BackColor = _nonSelectedColor;
                lblPanel.BackColor = _selectedColor;
            }
        }

        private void lblLeftMain_Click(object sender, EventArgs e)
        {
            if (CurrentTab == null)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirection.Left;
            _curMarkName = MarkName.Main;
            UpdateParam();
        }

        private void lblLeftSub1_Click(object sender, EventArgs e)
        {
            if (CurrentTab == null)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirection.Left;
            _curMarkName = MarkName.Sub1;
            UpdateParam();
        }

        private void lblLeftSub2_Click(object sender, EventArgs e)
        {
            if (CurrentTab == null)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirection.Left;
            _curMarkName = MarkName.Sub2;
            UpdateParam();
        }

        private void lblLeftSub3_Click(object sender, EventArgs e)
        {
            if (CurrentTab == null)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirection.Left;
            _curMarkName = MarkName.Sub3;
            UpdateParam();
        }

        private void lblLeftSub4_Click(object sender, EventArgs e)
        {
            if (CurrentTab == null)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirection.Left;
            _curMarkName = MarkName.Sub4;
            UpdateParam();
        }

        private void lblRightMain_Click(object sender, EventArgs e)
        {
            if (CurrentTab == null)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirection.Right;
            _curMarkName = MarkName.Main;
            UpdateParam();
        }

        private void lblRightSub1_Click(object sender, EventArgs e)
        {
            if (CurrentTab == null)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirection.Right;
            _curMarkName = MarkName.Sub1;
            UpdateParam();
        }

        private void lblRightSub2_Click(object sender, EventArgs e)
        {
            if (CurrentTab == null)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirection.Right;
            _curMarkName = MarkName.Sub2;
            UpdateParam();
        }

        private void lblRightSub3_Click(object sender, EventArgs e)
        {
            if (CurrentTab == null)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirection.Right;
            _curMarkName = MarkName.Sub3;
            UpdateParam();
        }

        private void lblRightSub4_Click(object sender, EventArgs e)
        {
            if (CurrentTab == null)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirection.Right;
            _curMarkName = MarkName.Sub4;
            UpdateParam();
        }

        private void UpdateBtnBackColor(object sender)
        {
            lblLeftMain.BackColor = _nonSelectedColor;
            lblLeftSub1.BackColor = _nonSelectedColor;
            lblLeftSub2.BackColor = _nonSelectedColor;
            lblLeftSub3.BackColor = _nonSelectedColor;
            lblLeftSub4.BackColor = _nonSelectedColor;

            lblRightMain.BackColor = _nonSelectedColor;
            lblRightSub1.BackColor = _nonSelectedColor;
            lblRightSub2.BackColor = _nonSelectedColor;
            lblRightSub3.BackColor = _nonSelectedColor;
            lblRightSub4.BackColor = _nonSelectedColor;

            Label lbl = sender as Label;
            lbl.BackColor = _selectedColor;
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

            CogRectangle roi = VisionProImageHelper.CreateRectangle(centerX, centerY, 100, 100);
            CogRectangle searchRoi = VisionProImageHelper.CreateRectangle(roi.CenterX, roi.CenterY, roi.Width * 2, roi.Height * 2);

            var currentParam = ParamControl.GetCurrentParam();
            roi.Changed += Roi_Changed;
            currentParam.SetTrainRegion(roi);
            currentParam.SetSearchRegion(searchRoi);
            int size = 200;
            DrawOriginPointMark(display, new PointF(Convert.ToSingle(roi.X), Convert.ToSingle(roi.Y)), size);
            ParamControl.UpdateData(currentParam);
        }

        private void DrawOriginPointMark(CogDisplayControl display, PointF centerPoint, int size)
        {
            PointMarkerParam.SetOriginPoint(Convert.ToSingle(centerPoint.X), Convert.ToSingle(centerPoint.Y), size);
            display.SetInteractiveGraphics("tool", PointMarkerParam.GetCurrentRecord());
            //display.SetInteractiveGraphics("tool", currentParam.CreateCurrentRecord(constants));

            //double centerX = display.ImageWidth() / 2.0 - display.GetPan().X;
            //double centerY = display.ImageHeight() / 2.0 - display.GetPan().Y;
            ////double centerX = trainRegion.CenterX;
            ////double centerY = trainRegion.CenterY;


            //CogLineSegment cogLineSegmentX = new CogLineSegment();
            //double horizontalStartX = centerX - length;
            //double horizontalStartY = centerY;
            //cogLineSegmentX.SetStartLengthRotation(horizontalStartX, horizontalStartY, length * 2, MathHelper.DegToRad(0));
            //cogLineSegmentX.Interactive = true;
            //cogLineSegmentX.Color = CogColorConstants.Cyan;

            //CogLineSegment cogLineSegmentY = new CogLineSegment();
            //double verticalStartX = centerX;
            //double verticalStartY = centerY - length;
            //cogLineSegmentY.SetStartLengthRotation(verticalStartX, verticalStartY, length * 2, MathHelper.DegToRad(90));
            //cogLineSegmentY.Interactive = true;
            //cogLineSegmentY.Color = CogColorConstants.Cyan;

            //CogGraphicInteractiveCollection collection = new CogGraphicInteractiveCollection
            //{
            //    cogLineSegmentX,
            //    cogLineSegmentY
            //};

            //OriginShape.SetOriginShape(collection as ICogRecord);
        }

        private void Roi_Changed(object sender, CogChangedEventArgs e)
        {
        }

        public void DrawROI()
        {
            if (Enabled == false)
                return;
            
            var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();
            if (display.GetImage() == null)
                return;

            display.ClearGraphic();

            var currentParam = ParamControl.GetCurrentParam();
            if (currentParam == null)
                return;

            CogPMAlignCurrentRecordConstants constants = CogPMAlignCurrentRecordConstants.InputImage | CogPMAlignCurrentRecordConstants.SearchRegion
                | CogPMAlignCurrentRecordConstants.TrainImage | CogPMAlignCurrentRecordConstants.TrainRegion /*| CogPMAlignCurrentRecordConstants.PatternOrigin*/;
            display.SetInteractiveGraphics("tool", currentParam.CreateCurrentRecord(constants));

            var originPoint = currentParam.GetOrigin();
            PointF centerPoint = new PointF(Convert.ToSingle(originPoint.TranslationX), Convert.ToSingle(originPoint.TranslationY));
            int size = 200;
            DrawOriginPointMark(display, centerPoint, size);

            var rect = currentParam.GetTrainRegion() as CogRectangle;
            if(rect != null)
                display.SetDisplayToCenter(new Point((int)rect.CenterX, (int)rect.CenterY));
        }

        //public void Run()
        //{
        //    var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();
        //    if (display == null)
        //        return;

        //    ICogImage cogImage = display.GetImage();
        //    if (cogImage == null)
        //        return;

        //    display.ClearGraphic();
        //    display.DisplayRefresh();

        //    MainAlgorithmTool algorithmTool = new MainAlgorithmTool();
        //    TabInspResult tabInspResult = new TabInspResult();
           
        //    //algorithmTool.MainMarkInspect(cogImage, CurrentTab, ref tabInspResult, UseAlignMark);
        //    algorithmTool.MainPanelMarkInspect(cogImage, CurrentTab, ref tabInspResult);

        //    if (tabInspResult.MarkResult.Judgement != Judgement.OK)
        //    {
        //        // 검사 실패
        //        string message = string.Format("Mark Insp NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", CurrentTab.Index,
        //            tabInspResult.MarkResult.FpcMark.Judgement, tabInspResult.MarkResult.PanelMark.Judgement);

        //        MessageConfirmForm form = new MessageConfirmForm();
        //        form.Message = message;
        //        form.ShowDialog();
        //    }
        //    display.ClearGraphic();
        //    List<VisionProPatternMatchingResult> matchingResultList = new List<VisionProPatternMatchingResult>();

        //    var foundedFpcMark = tabInspResult.MarkResult.FpcMark.FoundedMark;
        //    if(foundedFpcMark != null)
        //    {
        //        var leftFpc = foundedFpcMark.Left.MaxMatchPos.ResultGraphics;
        //        var rightFpc = foundedFpcMark.Right.MaxMatchPos.ResultGraphics;
        //        matchingResultList.Add(foundedFpcMark.Left);
        //        matchingResultList.Add(foundedFpcMark.Right);
        //    }
            

        //    var foundedPanelMark = tabInspResult.MarkResult.PanelMark.FoundedMark;

        //    if(foundedPanelMark != null)
        //    {
        //        var leftPanel = foundedPanelMark.Left.MaxMatchPos.ResultGraphics;
        //        var rightPanel = foundedPanelMark.Right.MaxMatchPos.ResultGraphics;

        //        matchingResultList.Add(foundedPanelMark.Left);
        //        matchingResultList.Add(foundedPanelMark.Right);
        //    }
            
        //    display.UpdateResult(matchingResultList);
        //}

        private void SetNGColor(CogGraphicChildren shapes)
        {

            CogColorConstants color = CogColorConstants.Red;
            foreach (var shape in shapes)
            {
                if (shape is CogPointMarker pointMarker)
                    pointMarker.Color = color;

                if (shape is CogRectangleAffine cogRectangleAffine)
                    cogRectangleAffine.Color = color;

                if (shape is CogRectangle cogRectangle)
                    cogRectangle.Color = color;
            }
        }

        private PointF GetMainOriginPoint()
        {
            if (_curMaterial == Material.Panel)
            {
                //var panelMainMark = CurrentTab.GetMarkParamter(UseAlignCamMark).PanelMarkList.Where(x => x.Name == MarkName.Main).First();
                var panelMainMark = CurrentTab.GetMarkParamter(UseAlignCamMark).GetPanelMark(_curDirection, MarkName.Main);
                var origin = panelMainMark.InspParam.GetOrigin();
                double orginX = origin.TranslationX;
                double originY = origin.TranslationY;
                return new PointF((float)orginX, (float)originY);
            }
            else
            {
                //var fpcMainMark = CurrentTab.GetMarkParamter(UseAlignCamMark).FpcMarkList.Where(x => x.Name == MarkName.Main).First();
                var fpcMainMark = CurrentTab.GetMarkParamter(UseAlignCamMark).GetFPCMark(_curDirection, MarkName.Main);
                var orgin = fpcMainMark.InspParam.GetOrigin();

                double originX = orgin.TranslationX;
                double originY = orgin.TranslationY;
                return new PointF((float)originX, (float)originY);
            }
        }

        public void ShowROIJog()
        {
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
            if (jogType.Contains("Move"))
                MoveMode(jogType, jogScale, roiType);
            else if (jogType.Contains("Zoom"))
                SizeMode(jogType, jogScale, roiType);
            else { }
        }

        public void DisposeImage()
        {
            ParamControl?.DisposeImage();
        }

        private void MoveMode(string moveType, int jogScale, ROIType roiType)
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

            var currentParam = ParamControl.GetCurrentParam();

            if (roiType == ROIType.Origin)
            {
                CogTransform2DLinear origin = currentParam.GetOrigin();
                origin.TranslationX += jogMoveX;
                origin.TranslationY += jogMoveY;
            }
            else
            {
                CogRectangle roi = new CogRectangle();

                if (roiType == ROIType.ROI)
                    roi = currentParam.GetSearchRegion() as CogRectangle;
                else if (roiType == ROIType.Train)
                    roi = currentParam.GetTrainRegion() as CogRectangle;
                else { }

                roi.X += jogMoveX;
                roi.Y += jogMoveY;
            }

            //currentParam.SetSearchRegion(roi);
            DrawROI();
        }

        private void SizeMode(string sizeType, int jogScale, ROIType roiType)
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

            var currentParam = ParamControl.GetCurrentParam();
            if (roiType == ROIType.Origin)
            {
                CogTransform2DLinear origin = currentParam.GetOrigin();
                origin.TranslationX += jogSizeX;
                origin.TranslationY += jogSizeY;
            }
            else
            {
                CogRectangle roi = new CogRectangle();

                if (roiType == ROIType.ROI)
                    roi = currentParam.GetSearchRegion() as CogRectangle;
                else if (roiType == ROIType.Train)
                    roi = currentParam.GetTrainRegion() as CogRectangle;
                else { }

                roi.Width += jogSizeX;
                roi.Height += jogSizeY;
            }

            //currentParam.SetSearchRegion(roi);
            DrawROI();
        }

        public void RunForTest()
        {
            var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();
            var currentParam = ParamControl.GetCurrentParam();

            if (display == null || currentParam == null)
                return;

            ICogImage cogImage = display.GetImage();
            if (cogImage == null)
                return;

            display.ClearGraphic();
            display.DisplayRefresh();

            if (currentParam.IsTrained() == false)
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "Pattern is not trained.";
                form.ShowDialog();
                return;
            }

            VisionProPatternMatchingParam inspParam = currentParam.DeepCopy();
            ICogImage copyCogImage = cogImage.CopyBase(CogImageCopyModeConstants.CopyPixels);

            PointF originPoint = GetMainOriginPoint();

            VisionProPatternMatchingResult result = Algorithm.RunPatternMatch(copyCogImage, inspParam);

            if (result == null)
            {
                inspParam.Dispose();
                VisionProImageHelper.Dispose(ref copyCogImage);
                return;
            }
            if (result.MatchPosList.Count > 0)
            {
                display.ClearGraphic();
                display.UpdateResult(result);
            }
            else
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "Pattern is Not Found.";
                form.ShowDialog();
            }

            result.Dispose();
            inspParam.Dispose();
            VisionProImageHelper.Dispose(ref copyCogImage);
        }

        //public void CopyMark(UnitName unitName)
        //{
        //    var TeachingTabList = TeachingData.Instance().GetUnit(unitName.ToString()).GetTabList();

        //    foreach (Tab tab in TeachingTabList)
        //    {
        //        if (tab.Index == CurrentTab.Index)
        //            continue;

        //        switch (_curMaterial)
        //        {
        //            case Material.Fpc:
        //                var fpcMark = CurrentTab.MarkParamter.GetFPCMark(_curDirection, _curMarkName, UseAlignMark).DeepCopy();
        //                tab.MarkParamter.SetFPCMark(_curDirection, _curMarkName, fpcMark, UseAlignMark);
        //                break;

        //            case Material.Panel:
        //                var panelMark = CurrentTab.MarkParamter.GetPanelMark(_curDirection, _curMarkName, UseAlignMark).DeepCopy();
        //                tab.MarkParamter.SetPanelMark(_curDirection, _curMarkName, panelMark, UseAlignMark);
        //                break;

        //            default:
        //                break;
        //        }
                
        //    }
        //}

        private void lblTest_Click(object sender, EventArgs e)
        {
        }
        #endregion
    }
}
