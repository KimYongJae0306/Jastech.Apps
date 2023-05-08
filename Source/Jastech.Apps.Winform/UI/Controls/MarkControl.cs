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
            pnlParam.Controls.Add(ParamControl);
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

            CogRectangle roi = CogImageHelper.CreateRectangle(centerX - display.GetPan().X, centerY - display.GetPan().Y, 100, 100);
            CogRectangle searchRoi = CogImageHelper.CreateRectangle(roi.CenterX, roi.CenterY, roi.Width * 2, roi.Height * 2);

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

            ICogImage cogImage = display.GetImage();
            CogPatternMatchingResult result = Algorithm.RunPatternMatch(cogImage, currentParam);

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
    }
}
