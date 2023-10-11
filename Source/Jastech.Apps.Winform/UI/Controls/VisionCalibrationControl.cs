using Cognex.VisionPro;
using Cognex.VisionPro.PMAlign;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Apps.Winform.Core.Calibrations;
using Jastech.Framework.Device.Motions;
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
    public partial class VisionCalibrationControl : UserControl
    {
        #region 필드
        private Color _selectedColor = new Color();

        private Color _nonSelectedColor = new Color();
        #endregion

        #region 속성
        private CogPatternMatchingParamControl ParamControl { get; set; } = null;

        private Unit CurrentUnit { get; set; } = null;

        private AlgorithmTool Algorithm = new AlgorithmTool();

        private VisionXCalibration VisionXCalibration { get; set; } = new VisionXCalibration();
        #endregion

        #region 이벤트
        public AreaCameraGrabDelegate AreaCameraGrabEventHandler;
        #endregion

        #region 델리게이트
        public delegate void AreaCameraGrabDelegate(bool isGrabStart);
        #endregion

        #region 생성자
        public VisionCalibrationControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void VisionCalibrationControl_Load(object sender, EventArgs e)
        {
            AddControl();
            InitializeUI();
            UpdateParam();
            SelectCalibrationMode(CalibrationMode.XYT);
        }

        private void AddControl()
        {
            ParamControl = new CogPatternMatchingParamControl();
            ParamControl.Dock = DockStyle.Fill;
            ParamControl.GetOriginImageHandler += VisionCalibrationControl_GetOriginImageHandler;
            ParamControl.TestActionEvent += VisionCalibrationControl_TestActionEvent;
            ParamControl.ClearActionEvent += PatternControl_ClearActionEvent;
            pnlParam.Controls.Add(ParamControl);
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

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);
        }

        private ICogImage VisionCalibrationControl_GetOriginImageHandler()
        {
            return TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay().GetImage();
        }

        private void VisionCalibrationControl_TestActionEvent()
        {
            Inspection();
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

            var param = CurrentUnit.GetCalibrationMark();

            if (param != null)
                ParamControl.UpdateData(param.InspParam);

            DrawROI();
        }

        private void Inspection()
        {
            AreaCameraGrabEventHandler.Invoke(false);

            var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();
            var currentParam = ParamControl.GetCurrentParam();

            if (display == null || currentParam == null)
                return;

            ICogImage cogImage = display.GetImage();
            if (cogImage == null)
                return;
            display.ClearGraphic();
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

            AreaCameraGrabEventHandler.Invoke(true);
        }

        private void lblAddROI_Click(object sender, EventArgs e)
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

            double centerX = display.GetImageWidth() / 2.0;
            double centerY = display.GetImageHeight() / 2.0;

            CogRectangle roi = VisionProImageHelper.CreateRectangle(centerX - display.GetPan().X, centerY - display.GetPan().Y, 100, 100);
            CogRectangle searchRoi = VisionProImageHelper.CreateRectangle(roi.CenterX, roi.CenterY, roi.Width * 2, roi.Height * 2);

            var currentParam = ParamControl.GetCurrentParam();

            currentParam.SetTrainRegion(roi);
            currentParam.SetSearchRegion(searchRoi);
        }

        public void DrawROI()
        {
            var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();
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

        private void lblAuto_Click(object sender, EventArgs e)
        {
            if (VisionXCalibration == null)
                return;

            VisionXCalibration.SetParam(ParamControl.GetCurrentParam());
            VisionXCalibration.SetCalibrationMode(CalibrationMode.XY);
            VisionXCalibration.StartCalSeqRun();
        }

        private void lblStop_Click(object sender, EventArgs e)
        {
            if (VisionXCalibration == null)
                return;

            VisionXCalibration.CalSeqStop();
        }

        public void SetAxisHandler(AxisHandler axisHandler)
        {
            if (VisionXCalibration == null)
                return;

            VisionXCalibration.SetAxisHandler(axisHandler);
        }

        public void SetAreaCamera(AreaCamera areaCamera)
        {
            if (VisionXCalibration == null)
                return;

            VisionXCalibration.SetCamera(areaCamera);
        }

        private void lblXY_Click(object sender, EventArgs e)
        {
            SelectCalibrationMode(CalibrationMode.XY);
        }

        private void lblXYT_Click(object sender, EventArgs e)
        {
            SelectCalibrationMode(CalibrationMode.XYT);
        }

        private void SelectCalibrationMode(CalibrationMode calibrationMode)
        {
            ClearCalibrationModeColor();
            VisionXCalibration.SetCalibrationMode(calibrationMode);

            switch (calibrationMode)
            {
                case CalibrationMode.XY:
                    lblXY.BackColor = _selectedColor;
                    break;

                case CalibrationMode.XYT:
                    lblXYT.BackColor = _selectedColor;
                    break;

                default:
                    break;
            }
        }

        private void ClearCalibrationModeColor()
        {
            foreach (Label label in tlpCalibrationMode.Controls)
                label.BackColor = _nonSelectedColor;
        }
        #endregion
    }
}
