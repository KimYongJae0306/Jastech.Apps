using Cognex.VisionPro;
using Cognex.VisionPro.Caliper;
using Cognex.VisionPro.PMAlign;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.Parameters;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using Jastech.Framework.Winform.Forms;
using Jastech.Framework.Winform.VisionPro.Controls;
using System;
using System.Drawing;
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
        private CogPatternMatchingParamControl ParamControl { get; set; } = null;

        private Tab CurrentTab { get; set; } = null;

        public TeachingItem TeachingItem = TeachingItem.Mark;
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
            pnlParam.Controls.Add(ParamControl);
        }

        private void PatternControl_TestActionEvent()
        {
            Inspection();
        }

        private ICogImage PatternControl_GetOriginImageHandler()
        {
            return TeachingUIManager.Instance().GetDisplay().GetImage();
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            if (TeachingItem == TeachingItem.Akkon)
            {
                lblFpc.Visible = false;
            }
            else
            {
                lblFpc.Visible = true;
            }
        }

        public void SetParams(Tab tab)
        {
            if (tab == null)
                return;

            CurrentTab = tab;
            UpdateParam();

            // Test
            DeepCopy(tab);
        }

        private void UpdateParam()
        {
            if (CurrentTab == null)
                return;

            MarkParam currentParam = null;
            if (_curMaterial == Material.Fpc)
                currentParam = CurrentTab.GetFPCMark(_curDirection, _curMarkName);
            else
                currentParam = CurrentTab.GetPanelMark(_curDirection, _curMarkName);

            if(currentParam != null)
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

            double centerX = display.ImageWidth() / 2.0;
            double centerY = display.ImageHeight() / 2.0;

            CogRectangle roi = VisionProImageHelper.CreateRectangle(centerX - display.GetPan().X, centerY - display.GetPan().Y, 100, 100);
            CogRectangle searchRoi = VisionProImageHelper.CreateRectangle(roi.CenterX, roi.CenterY, roi.Width * 2, roi.Height * 2);

            var currentParam = ParamControl.GetCurrentParam();
            roi.Changed += Roi_Changed;
            currentParam.SetTrainRegion(roi);
            currentParam.SetSearchRegion(searchRoi);
            ParamControl.UpdateData(currentParam);
        }

        private void Roi_Changed(object sender, CogChangedEventArgs e)
        {
        }

        public void DrawROI()
        {
            if (Enabled == false)
                return;
            
            var display = TeachingUIManager.Instance().GetDisplay();
            if (display.GetImage() == null)
                return;
            display.ClearGraphic();

            CogPMAlignCurrentRecordConstants constants = CogPMAlignCurrentRecordConstants.InputImage | CogPMAlignCurrentRecordConstants.SearchRegion
                | CogPMAlignCurrentRecordConstants.TrainImage | CogPMAlignCurrentRecordConstants.TrainRegion | CogPMAlignCurrentRecordConstants.PatternOrigin;

            var currentParam = ParamControl.GetCurrentParam();

            display.SetInteractiveGraphics("tool", currentParam.CreateCurrentRecord(constants));
            
            var rect = currentParam.GetTrainRegion() as CogRectangle;
            if(rect != null)
                display.SetDisplayToCenter(new Point((int)rect.CenterX, (int)rect.CenterY));
        }

        public void Inspection()
        {
            var display = TeachingUIManager.Instance().GetDisplay();
            var currentParam = ParamControl.GetCurrentParam();

            if (display == null || currentParam == null)
                return;

            ICogImage cogImage = display.GetImage();
            if (cogImage == null)
                return;

            if (currentParam.IsTrained() == false)
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "Pattern is not trained.";
                form.ShowDialog();
                return;
            }

            VisionProPatternMatchingParam inspParam = currentParam.DeepCopy();
            ICogImage copyCogImage = cogImage.CopyBase(CogImageCopyModeConstants.CopyPixels);

            VisionProPatternMatchingResult result = Algorithm.RunPatternMatch(copyCogImage, inspParam);

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

        private Tab _origin = null;

        private void DeepCopy(Tab tab)
        {
            if (tab == null)
                return;

            _origin = new Tab();
            _origin = tab.DeepCopy();
        }

        private Tab GetOriginData()
        {
            return _origin;
        }

        private void lblTest_Click(object sender, EventArgs e)
        {
            var display = TeachingUIManager.Instance().GetDisplay();
            var currentParam = ParamControl.GetCurrentParam();

            Tab origin = new Tab();
            origin = GetOriginData();

            ICogImage cogImage = display.GetImage();

            if (cogImage == null)
                return;

            // 1. FPC Mark 기반으로 Align의 FPC ROI 틀기
            // 2. Panel Mark 기반으로 Align의 Panel ROI 틀기
            // 3. Panel Mark 기반으로 Akkon의 ROI 틀기

            if (_curMarkName != MarkName.Main)
                return;

            // 티칭한 Left Fpc
            MarkParam teachedLeftFpcMarkParam = origin.GetFPCMark(MarkDirection.Left, MarkName.Main);
            CogTransform2DLinear teachedLeftFpc2D = teachedLeftFpcMarkParam.InspParam.GetOrigin();
            PointF teachedLeftFpc = new PointF(Convert.ToSingle(teachedLeftFpc2D.TranslationX), Convert.ToSingle(teachedLeftFpc2D.TranslationY));

            // 티칭한 Right FPC 좌표
            MarkParam teachedRightFpcMarkparam = origin.GetFPCMark(MarkDirection.Right, MarkName.Main);
            CogTransform2DLinear teachedRightFpc2D = teachedRightFpcMarkparam.InspParam.GetOrigin();
            PointF teachedRightFpc = new PointF(Convert.ToSingle(teachedRightFpc2D.TranslationX), Convert.ToSingle(teachedRightFpc2D.TranslationY));

            // 찾은 Left FPC 좌표
            VisionProPatternMatchingResult leftReferenceMarkResult = Algorithm.RunPatternMatch(cogImage, teachedLeftFpcMarkParam.InspParam);
            PointF searchedLeftFpcPoint = leftReferenceMarkResult.MaxMatchPos.FoundPos;

            // 찾은 Right FPC 좌표
            VisionProPatternMatchingResult rightReferenceFpcMarkResult = Algorithm.RunPatternMatch(cogImage, teachedRightFpcMarkparam.InspParam);
            PointF searchedRightFpcPoint = rightReferenceFpcMarkResult.MaxMatchPos.FoundPos;

            MainAlgorithmTool algorithmTool = new MainAlgorithmTool();
            algorithmTool.Coordinate = new Coordinate();
            algorithmTool.Coordinate.SetCoordinateParam(teachedLeftFpc, teachedRightFpc, searchedLeftFpcPoint, searchedRightFpcPoint);

            foreach (var item in origin.AlignParamList)
            {
                CogRectangleAffine roi = new CogRectangleAffine(item.CaliperParams.CaliperTool.Region);

                PointF oldPoint = new PointF();
                oldPoint.X = (float)roi.CenterX;
                oldPoint.Y = (float)roi.CenterY;

                var newPoint = algorithmTool.Coordinate.GetCoordinate(oldPoint);
                roi.CenterX = newPoint.X;
                roi.CenterY = newPoint.Y;

                item.CaliperParams.CaliperTool.Region = roi;

                CogCaliperCurrentRecordConstants constants = CogCaliperCurrentRecordConstants.All;
                display.SetInteractiveGraphics("tool", item.CaliperParams.CreateCurrentRecord(constants));
            }

            foreach (var item in origin.AkkonParam.GetAkkonROIList())
            {
                PointF leftTop = new PointF(Convert.ToSingle(item.LeftTopX), Convert.ToSingle(item.LeftTopY));
                PointF rightTop = new PointF(Convert.ToSingle(item.RightTopX), Convert.ToSingle(item.RightTopY));
                PointF leftBottom = new PointF(Convert.ToSingle(item.LeftBottomX), Convert.ToSingle(item.LeftBottomY));
                CogRectangleAffine roi = VisionProShapeHelper.ConvertToCogRectAffine(leftTop, rightTop, leftBottom);

                PointF oldPoint = new PointF();
                oldPoint.X = (float)roi.CenterX;
                oldPoint.Y = (float)roi.CenterY;

                var newPoint = algorithmTool.Coordinate.GetCoordinate(oldPoint);
                roi.CenterX = newPoint.X;
                roi.CenterY = newPoint.Y;
            }

            return;
        }

        private MarkParam CoordinateMark(double x, double y)
        {
            return null;
        }

        private AlignParam CoordinateAlign()
        {
            return null;
        }

        private AkkonParam CoordinateAkkon()
        {
            return null;
        }
        #endregion
    }
}
