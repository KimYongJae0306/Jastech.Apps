using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Apps.Structure;
using Jastech.Framework.Winform.VisionPro.Controls;
using Cognex.VisionPro;
using Jastech.Apps.Structure.Data;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Apps.Structure.Parameters;
using Jastech.Framework.Imaging.VisionPro;
using Cognex.VisionPro.PMAlign;
using Jastech.Framework.Winform.Forms;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Framework.Winform.Controls;
using static Jastech.Framework.Device.Motions.AxisMovingParam;
using Jastech.Framework.Util.Helper;
using Cognex.VisionPro.Caliper;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class MarkControl : UserControl
    {
        private AlgorithmTool Algorithm = new AlgorithmTool();

        private string _curTabNo { get; set; } = "";

        private Color _selectedColor = new Color();

        private Color _nonSelectedColor = new Color();

        private Material _curMaterial = Material.Fpc;

        private MarkName _curMarkName = MarkName.Main;

        private MarkDirection _curDirection = MarkDirection.Left;

        private Tab CurrentTab { get; set; } = null;

        private CogPatternMatchingParamControl ParamControl { get; set; } = new CogPatternMatchingParamControl();

        public MarkControl()
        {
            InitializeComponent();
        }

        private void MarkControl_Load(object sender, EventArgs e)
        {
            AddControl();
            InitializeUI();

            lblFpc.BackColor = _selectedColor;
            lblLeftMain.BackColor = _selectedColor;

            _curDirection = MarkDirection.Left;
            _curMarkName = MarkName.Main;
            UpdateParam();
        }

        private void AddControl()
        {
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
            return AppsTeachingUIManager.Instance().GetDisplay().GetImage();
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);
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
                ParamControl.UpdateData(currentParam.InspParam);

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
            var display = AppsTeachingUIManager.Instance().GetDisplay();
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

            currentParam.SetTrainRegion(roi);
            currentParam.SetSearchRegion(searchRoi);
        }

        public void DrawROI()
        {
            if (Enabled == false)
                return;
            
            var display = AppsTeachingUIManager.Instance().GetDisplay();
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
            var display = AppsTeachingUIManager.Instance().GetDisplay();
            var currentParam = ParamControl.GetCurrentParam();

            if (display == null || currentParam == null)
                return;

            if (currentParam.IsTrained() == false)
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "Pattern is not trained.";
                form.ShowDialog();
                return;
            }
            VisionProPatternMatchingParam inspParam = currentParam.DeepCopy();
            ICogImage cogImage = display.GetImage();
            ICogImage copyCogImage = cogImage.CopyBase(CogImageCopyModeConstants.CopyPixels);

            VisionProPatternMatchingResult result = Algorithm.RunPatternMatch(copyCogImage, inspParam);
            //result.DeepCopy();
            //UpdateGridResult(result);

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
            ROIJogControl roiJogForm = new ROIJogControl();
            roiJogForm.SetTeachingItem(TeachingItem.Mark);
            roiJogForm.SendEventHandler += new ROIJogControl.SendClickEventDelegate(ReceiveClickEvent);
            roiJogForm.ShowDialog();
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
            var display = AppsTeachingUIManager.Instance().GetDisplay();
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


            /*
            // 티칭한 Left Fpc
            MarkParam teachedLeftFpcMarkParam = origin.GetFPCMark(MarkDirection.Left, MarkName.Main);
            CogTransform2DLinear teachedLeftFpc2D = teachedLeftFpcMarkParam.InspParam.GetOrigin();
            PointF teachedLeftFpc = new PointF(Convert.ToSingle(teachedLeftFpc2D.TranslationX), Convert.ToSingle(teachedLeftFpc2D.TranslationY));

            // 티칭한 Right FPC 좌표
            MarkParam teachedRightFpcMarkparam = origin.GetFPCMark(MarkDirection.Right, MarkName.Main);
            CogTransform2DLinear teachedRightFpc2D = teachedRightFpcMarkparam.InspParam.GetOrigin();
            PointF teachedRightFpc = new PointF(Convert.ToSingle(teachedRightFpc2D.TranslationX), Convert.ToSingle(teachedRightFpc2D.TranslationY));

            // 티칭한 각도
            double teachedTheta = Math.Atan2((teachedRightFpc.Y - teachedLeftFpc.Y), (teachedRightFpc.X - teachedLeftFpc.X));
            if (teachedTheta > 180.0)
                teachedTheta -= 360.0;

            // 찾은 Left FPC 좌표
            VisionProPatternMatchingResult leftReferenceMarkResult = Algorithm.RunPatternMatch(cogImage, teachedLeftFpcMarkParam.InspParam);
            PointF searchedLeftFpcPoint = leftReferenceMarkResult.MaxMatchPos.FoundPos;

            // 찾은 Right FPC 좌표
            VisionProPatternMatchingResult rightReferenceFpcMarkResult = Algorithm.RunPatternMatch(cogImage, teachedRightFpcMarkparam.InspParam);
            PointF searchedRightFpcPoint = rightReferenceFpcMarkResult.MaxMatchPos.FoundPos;

            // 찾은 각도
            double searchedTheta = Math.Atan2((searchedRightFpcPoint.Y - searchedLeftFpcPoint.Y), (searchedRightFpcPoint.X - searchedLeftFpcPoint.X));
            if (searchedTheta > 180.0)
                searchedTheta -= 360.0;

            double diffTheta = searchedTheta - teachedTheta;




            // 찾은 센터포인트
            PointF searchedCenterPoint = new PointF();
            searchedCenterPoint.X = (searchedLeftFpcPoint.X + searchedRightFpcPoint.X) / 2;
            searchedCenterPoint.Y = (searchedLeftFpcPoint.Y + searchedRightFpcPoint.Y) / 2;

            // 티칭한 센터포인트
            PointF teachedCenterPoint = new PointF();
            teachedCenterPoint.X = (teachedLeftFpc.X + teachedRightFpc.X) / 2;
            teachedCenterPoint.Y = (teachedLeftFpc.Y + teachedRightFpc.Y) / 2;

            // 센터끼리 틀어진 오프셋 값
            double offsetX = searchedCenterPoint.X - teachedCenterPoint.X;
            double offsetY = searchedCenterPoint.Y - teachedCenterPoint.Y;
            PointF offset = new PointF((float)offsetX, (float)offsetY);


            // 보정 전 좌표
            PointF oldPoint = new PointF();

            oldPoint.X += offset.X;
            oldPoint.Y += offset.Y;

            // 센터, 옵셋, 각도, 출력
            var xx = Math.Round(searchedCenterPoint.X + ((Math.Cos(diffTheta * (Math.PI / 180.0)) * (oldPoint.X - searchedCenterPoint.X)) - (Math.Sin(diffTheta * (Math.PI / 180.0)) * (oldPoint.Y - searchedCenterPoint.Y))));
            var yy = Math.Round(searchedCenterPoint.Y + ((Math.Sin(diffTheta * (Math.PI / 180.0)) * (oldPoint.X - searchedCenterPoint.X)) + (Math.Cos(diffTheta * (Math.PI / 180.0)) * (oldPoint.Y - searchedCenterPoint.Y))));
            */




            // ToDo
            // 1. 자재 위치별 좌, 우 등록된 패턴 위치로 Theta 및 거리 산출
            // 2. 신규로 움직인 좌, 우 등록된 패턴하고 차이값 구하기

            //// ToDo 1
            //// 티칭한 Left FPC 좌표
            //MarkParam leftReferenceFpcMarkParam = origin.GetFPCMark(MarkDirection.Left, MarkName.Main);
            //CogTransform2DLinear teachedLeftFpc2D = leftReferenceFpcMarkParam.InspParam.GetOrigin();
            //PointF teachedLeftFpc = new PointF(Convert.ToSingle(teachedLeftFpc2D.TranslationX), Convert.ToSingle(teachedLeftFpc2D.TranslationY));

            //// 찾은 Left FPC 좌표
            //VisionProPatternMatchingResult leftReferenceMarkResult = Algorithm.RunPatternMatch(cogImage, leftReferenceFpcMarkParam.InspParam);
            //PointF leftFpcReferencePoint = leftReferenceMarkResult.MaxMatchPos.FoundPos;


            //// 티칭한 Right FPC 좌표
            //MarkParam rightReferenceFpcMarkparam = origin.GetFPCMark(MarkDirection.Right, MarkName.Main);
            //CogTransform2DLinear teachedRightFpc2D = rightReferenceFpcMarkparam.InspParam.GetOrigin();
            //PointF teachedRightFpc = new PointF(Convert.ToSingle(teachedRightFpc2D.TranslationX), Convert.ToSingle(teachedRightFpc2D.TranslationY));

            //// 찾은 Right FPC 좌표
            //VisionProPatternMatchingResult rightReferenceFpcMarkResult = Algorithm.RunPatternMatch(cogImage, rightReferenceFpcMarkparam.InspParam);
            //PointF rightFpcReferencePoint = rightReferenceFpcMarkResult.MaxMatchPos.FoundPos;

            //double teachedFpcDegree = MathHelper.GetTheta(teachedLeftFpc, teachedRightFpc);
            //double searchedFpcDegree = MathHelper.GetTheta(leftFpcReferencePoint, rightFpcReferencePoint);


            //// 티칭한 Left Panel 좌표
            //MarkParam leftReferencePanelMarkParam = origin.GetPanelMark(MarkDirection.Left, MarkName.Main);
            //CogTransform2DLinear teachedLeftPanel2D = leftReferencePanelMarkParam.InspParam.GetOrigin();
            //PointF teachedLeftPanel = new PointF(Convert.ToSingle(teachedLeftPanel2D.TranslationX), Convert.ToSingle(teachedLeftPanel2D.TranslationY));

            //// 찾은 Left Panel 좌표
            //VisionProPatternMatchingResult leftReferencePanelMarkResult = Algorithm.RunPatternMatch(cogImage, leftReferencePanelMarkParam.InspParam);
            //PointF leftPanelReferencePoint = leftReferencePanelMarkResult.MaxMatchPos.FoundPos;

            //// 티칭한 Right Panel 좌표
            //MarkParam rightReferencePanelMarkparam = origin.GetPanelMark(MarkDirection.Right, MarkName.Main);
            //CogTransform2DLinear teachedRightPanel2D = rightReferencePanelMarkparam.InspParam.GetOrigin();
            //PointF teachedRightPanel = new PointF(Convert.ToSingle(teachedRightPanel2D.TranslationX), Convert.ToSingle(teachedRightPanel2D.TranslationY));

            //// 찾은 Right Panel 좌표
            //VisionProPatternMatchingResult rightReferencePanelMarkResult = Algorithm.RunPatternMatch(cogImage, rightReferencePanelMarkparam.InspParam);
            //PointF rightPanelReferencePoint = rightReferencePanelMarkResult.MaxMatchPos.FoundPos;

            //double teachedPanelDegree = MathHelper.GetTheta(teachedLeftPanel, teachedRightPanel);
            //double searchedPanelDegree = MathHelper.GetTheta(leftPanelReferencePoint, rightPanelReferencePoint);

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
                //CogRectangleAffine roi = new CogRectangleAffine();

                int tt = 0;

                //var currentParam = CogCaliperParamControl.GetCurrentParam();
                CogRectangleAffine roi = new CogRectangleAffine(item.CaliperParams.CaliperTool.Region);

                PointF oldPoint = new PointF();
                oldPoint.X = (float)roi.CenterX;
                oldPoint.Y = (float)roi.CenterY;

                var newPoint = algorithmTool.Coordinate.GetCoordinate(oldPoint);
                roi.CenterX = newPoint.X;
                roi.CenterY = newPoint.Y;

                item.CaliperParams.CaliperTool.Region = roi;

                //roi.CenterX += jogMoveX;
                //roi.CenterY += jogMoveY;

                //currentParam.CaliperTool.Region = roi;

                //item.CaliperParams.CaliperTool.Region = roi.;
                //item.CaliperParams.CaliperTool.Region;

                CogCaliperCurrentRecordConstants constants = CogCaliperCurrentRecordConstants.All;
                display.SetInteractiveGraphics("tool", item.CaliperParams.CreateCurrentRecord(constants));
            }


            return;









            MarkParam referenceMarkParam = new MarkParam();
            PointF referencePoint = new PointF();

            if (_curMaterial == Material.Fpc)
            {
                referenceMarkParam = origin.GetFPCMark(_curDirection, MarkName.Main);
                VisionProPatternMatchingResult referenceResult = Algorithm.RunPatternMatch(cogImage, referenceMarkParam.InspParam);
                referencePoint = referenceResult.MaxMatchPos.FoundPos;
            }
            else
            {
                referenceMarkParam = origin.GetPanelMark(_curDirection, MarkName.Main);
                VisionProPatternMatchingResult referenceResult = Algorithm.RunPatternMatch(cogImage, referenceMarkParam.InspParam);
                referencePoint = referenceResult.MaxMatchPos.FoundPos;
            }

            VisionProPatternMatchingResult currentFpcLeftResult = Algorithm.RunPatternMatch(cogImage, currentParam);
            PointF currentPos = currentFpcLeftResult.MaxMatchPos.FoundPos;

            double diffX = referencePoint.X - currentPos.X;
            double diffY = referencePoint.Y - currentPos.Y;

            if (diffX == 0 && diffY == 0)
                return;







            //MarkParam fpcLeft = new MarkParam();
            //fpcLeft = origin.GetFPCMark(MarkDirection.Left, MarkName.Main);

            //MarkParam fpcRight = new MarkParam();
            //fpcRight = origin.GetFPCMark(MarkDirection.Right, MarkName.Main);

            //MarkParam panelLeft = new MarkParam();
            //panelLeft = origin.GetPanelMark(MarkDirection.Left, MarkName.Main);

            //MarkParam panelRight = new MarkParam();
            //panelRight = origin.GetPanelMark(MarkDirection.Right, MarkName.Main);
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
    }
}
