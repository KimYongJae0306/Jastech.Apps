using Cognex.VisionPro;
using Cognex.VisionPro.PMAlign;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
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
    public partial class PreAlignControl : UserControl
    {
        #region 필드
        private Color _selectedColor = new Color();

        private Color _nonSelectedColor = new Color();

        private string _prevName { get; set; } = "";

        private MarkName _curMarkName = MarkName.Main;

        private MarkDirection _curDirection = MarkDirection.Left;
        #endregion

        #region 속성
        private CogPatternMatchingParamControl ParamControl { get; set; } = null;

        private Unit CurrentUnit { get; set; } = null;

        private AlgorithmTool Algorithm = new AlgorithmTool();
        #endregion

        #region 이벤트
        public ChangeDirectionDelegate MarkDirectionChanged { get; set; }
        #endregion

        #region 델리게이트
        public delegate void ChangeDirectionDelegate(MarkDirection direction);
        #endregion

        #region 생성자
        public PreAlignControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void PreAlignControl_Load(object sender, EventArgs e)
        {
            AddControl();
            InitializeUI();

            lblLeftMain.BackColor = _selectedColor;
            _curDirection = MarkDirection.Left;
            _curMarkName = MarkName.Main;
            UpdateParam();
        }

        private void AddControl()
        {
            ParamControl = new CogPatternMatchingParamControl();
            ParamControl.Dock = DockStyle.Fill;
            ParamControl.GetOriginImageHandler += PreAlignControl_GetOriginImageHandler;
            ParamControl.TestActionEvent += PreAlignControl_TestActionEvent;
            ParamControl.ClearActionEvent += PatternControl_ClearActionEvent;
            pnlParam.Controls.Add(ParamControl);
        }

        private void PatternControl_ClearActionEvent()
        {
            var display = TeachingUIManager.Instance().GetDisplay();
            var currentParam = ParamControl.GetCurrentParam();

            if (display == null || currentParam == null)
                return;

            ICogImage cogImage = display.GetImage();
            if (cogImage == null)
                return;
            display.Clear();

        }

        private void PreAlignControl_TestActionEvent()
        {
            Inspection();
        }

        private void Inspection()
        {
            var display = TeachingUIManager.Instance().GetDisplay();
            var currentParam = ParamControl.GetCurrentParam();

            if (display == null || currentParam == null)
                return;

            ICogImage cogImage = display.GetImage();
            if (cogImage == null)
                return;
            display.Clear();

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

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);
        }

        private ICogImage PreAlignControl_GetOriginImageHandler()
        {
            return TeachingUIManager.Instance().GetDisplay().GetImage();
        }

        public void SetParams(Unit unit)
        {
            if (unit == null)
                return;

            CurrentUnit = unit;
            UpdateParam();
        }

        private void UpdateParam()
        {
            if (CurrentUnit == null)
                return;

            var param = CurrentUnit.GetPreAlignMark(_curDirection, _curMarkName);

            if (param != null)
                ParamControl.UpdateData(param.InspParam);

            DrawROI();

            MarkDirectionChanged?.Invoke(_curDirection);
        }

        private void lblAddROI_Click(object sender, EventArgs e)
        {
            var display = TeachingUIManager.Instance().GetDisplay();
            if (display.GetImage() == null)
                return;

            if (ModelManager.Instance().CurrentModel == null)
                return;

            SetNewROI(display);
            DrawROI();
        }

        public void DrawROI()
        {
            var display = TeachingUIManager.Instance().GetDisplay();
            if (display.GetImage() == null)
                return;

            display.ClearGraphic();

            CogPMAlignCurrentRecordConstants constants = CogPMAlignCurrentRecordConstants.InputImage | CogPMAlignCurrentRecordConstants.SearchRegion
                | CogPMAlignCurrentRecordConstants.TrainImage | CogPMAlignCurrentRecordConstants.TrainRegion | CogPMAlignCurrentRecordConstants.PatternOrigin;

            var currentParam = ParamControl.GetCurrentParam();

            display.SetInteractiveGraphics("tool", currentParam.CreateCurrentRecord(constants));

            var rect = currentParam.GetTrainRegion() as CogRectangle;
            if (rect != null)
                display.SetDisplayToCenter(new Point((int)rect.CenterX, (int)rect.CenterY));
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

        private void lblInspection_Click(object sender, EventArgs e)
        {
            var display = TeachingUIManager.Instance().GetDisplay();
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
            VisionProPatternMatchingResult result = Algorithm.RunPatternMatch(cogImage, currentParam);

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

        public void DisposeImage()
        {
            ParamControl?.DisposeImage();
        }

        private void lblLeftMain_Click(object sender, EventArgs e)
        {
            UpdateBtnBackColor(sender);

            _curDirection = MarkDirection.Left;
            _curMarkName = MarkName.Main;
            UpdateParam();
        }

        private void lblLeftSub1_Click(object sender, EventArgs e)
        {
            UpdateBtnBackColor(sender);

            _curDirection = MarkDirection.Left;
            _curMarkName = MarkName.Sub1;
            UpdateParam();
        }

        private void lblLeftSub2_Click(object sender, EventArgs e)
        {
            UpdateBtnBackColor(sender);

            _curDirection = MarkDirection.Left;
            _curMarkName = MarkName.Sub2;
            UpdateParam();
        }

        private void lblLeftSub3_Click(object sender, EventArgs e)
        {
            UpdateBtnBackColor(sender);

            _curDirection = MarkDirection.Left;
            _curMarkName = MarkName.Sub3;
            UpdateParam();
        }

        private void lblLeftSub4_Click(object sender, EventArgs e)
        {
            UpdateBtnBackColor(sender);

            _curDirection = MarkDirection.Left;
            _curMarkName = MarkName.Sub4;
            UpdateParam();
        }

        private void lblRightMain_Click(object sender, EventArgs e)
        {
            UpdateBtnBackColor(sender);

            _curDirection = MarkDirection.Right;
            _curMarkName = MarkName.Main;
            UpdateParam();
        }

        private void lblRightSub1_Click(object sender, EventArgs e)
        {
            UpdateBtnBackColor(sender);

            _curDirection = MarkDirection.Right;
            _curMarkName = MarkName.Sub1;
            UpdateParam();
        }

        private void lblRightSub2_Click(object sender, EventArgs e)
        {
            UpdateBtnBackColor(sender);

            _curDirection = MarkDirection.Right;
            _curMarkName = MarkName.Sub2;
            UpdateParam();
        }

        private void lblRightSub3_Click(object sender, EventArgs e)
        {
            UpdateBtnBackColor(sender);

            _curDirection = MarkDirection.Right;
            _curMarkName = MarkName.Sub3;
            UpdateParam();
        }

        private void lblRightSub4_Click(object sender, EventArgs e)
        {
            UpdateBtnBackColor(sender);

            _curDirection = MarkDirection.Right;
            _curMarkName = MarkName.Main;
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
        #endregion
    }
}
