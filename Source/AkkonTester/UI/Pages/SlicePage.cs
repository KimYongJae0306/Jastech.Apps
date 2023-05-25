using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro;
using Jastech.Framework.Winform.VisionPro.Controls;
using AkkonTester.Helpers;
using Jastech.Framework.Algorithms.Akkon.Parameters;
using Jastech.Framework.Algorithms.Akkon;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Cognex.VisionPro.Display;

namespace AkkonTester.UI.Pages
{
    public partial class SlicePage : UserControl
    {
        #region 필드
        private bool _isLoading { get; set; } = false;
        #endregion

        #region 속성
        public CogDisplayControl OriginalDisplay { get; set; } = new CogDisplayControl();

        public CogDisplayControl RoiDisplay { get; set; } = new CogDisplayControl();

        public CogDisplayControl EnhanceDisplay { get; set; } = new CogDisplayControl();

        public CogDisplayControl ProcessingDisplay { get; set; } = new CogDisplayControl();

        public CogDisplayControl MaskingDisplay { get; set; } = new CogDisplayControl();

        public CogDisplayControl ResultDisplay { get; set; } = new CogDisplayControl();
        #endregion

        #region 생성자
        public SlicePage()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드

        private void SlicePage_Load(object sender, EventArgs e)
        {
            _isLoading = true;

            InitializeControls();
            InitializeComboBox();
            UpdateParams();

            _isLoading = false;
        }

        private void InitializeControls()
        {
            OriginalDisplay.Dock = DockStyle.Fill;
            OriginalDisplay.MoveImageEventHandler += OriginalDisplay_MoveImageEventHandler;
            OriginalDisplay.SetMouseMode(CogDisplayMouseModeConstants.Pan);
            pnlOriginal.Controls.Add(OriginalDisplay);

            RoiDisplay.Dock = DockStyle.Fill;
            RoiDisplay.MoveImageEventHandler += RoiDisplay_MoveImageEventHandler;
            RoiDisplay.SetMouseMode(CogDisplayMouseModeConstants.Pan);
            pnlROI.Controls.Add(RoiDisplay);

            EnhanceDisplay.Dock = DockStyle.Fill;
            EnhanceDisplay.MoveImageEventHandler += EnhanceDisplay_MoveImageEventHandler;
            EnhanceDisplay.SetMouseMode(CogDisplayMouseModeConstants.Pan);
            pnlEnhance.Controls.Add(EnhanceDisplay);

            ProcessingDisplay.Dock = DockStyle.Fill;
            ProcessingDisplay.MoveImageEventHandler += ProcessingDisplay_MoveImageEventHandler;
            ProcessingDisplay.SetMouseMode(CogDisplayMouseModeConstants.Pan);
            pnlProcessing.Controls.Add(ProcessingDisplay);

            MaskingDisplay.Dock = DockStyle.Fill;
            MaskingDisplay.MoveImageEventHandler += MaskingDisplay_MoveImageEventHandler;
            MaskingDisplay.SetMouseMode(CogDisplayMouseModeConstants.Pan);
            pnlMasking.Controls.Add(MaskingDisplay);

            ResultDisplay.Dock = DockStyle.Fill;
            ResultDisplay.MoveImageEventHandler += ResultDisplay_MoveImageEventHandler;
            ResultDisplay.SetMouseMode(CogDisplayMouseModeConstants.Pan);
            pnlResult.Controls.Add(ResultDisplay);
        }

        private void ResultDisplay_MoveImageEventHandler(double panX, double panY, double zoom)
        {
            OriginalDisplay.SetImagePosition(panX, panY, zoom);
            RoiDisplay.SetImagePosition(panX, panY, zoom);
            EnhanceDisplay.SetImagePosition(panX, panY, zoom);
            ProcessingDisplay.SetImagePosition(panX, panY, zoom);
            MaskingDisplay.SetImagePosition(panX, panY, zoom);
            //ResultDisplay.SetImagePosition(panX, panY, zoom);
        }

        private void MaskingDisplay_MoveImageEventHandler(double panX, double panY, double zoom)
        {
            OriginalDisplay.SetImagePosition(panX, panY, zoom);
            RoiDisplay.SetImagePosition(panX, panY, zoom);
            EnhanceDisplay.SetImagePosition(panX, panY, zoom);
            ProcessingDisplay.SetImagePosition(panX, panY, zoom);
            //MaskingDisplay.SetImagePosition(panX, panY, zoom);
            ResultDisplay.SetImagePosition(panX, panY, zoom);
        }

        private void ProcessingDisplay_MoveImageEventHandler(double panX, double panY, double zoom)
        {
            OriginalDisplay.SetImagePosition(panX, panY, zoom);
            RoiDisplay.SetImagePosition(panX, panY, zoom);
            EnhanceDisplay.SetImagePosition(panX, panY, zoom);
            //ProcessingDisplay.SetImagePosition(panX, panY, zoom);
            MaskingDisplay.SetImagePosition(panX, panY, zoom);
            ResultDisplay.SetImagePosition(panX, panY, zoom);
        }

        private void EnhanceDisplay_MoveImageEventHandler(double panX, double panY, double zoom)
        {
            OriginalDisplay.SetImagePosition(panX, panY, zoom);
            RoiDisplay.SetImagePosition(panX, panY, zoom);
            //EnhanceDisplay.SetImagePosition(panX, panY, zoom);
            ProcessingDisplay.SetImagePosition(panX, panY, zoom);
            MaskingDisplay.SetImagePosition(panX, panY, zoom);
            ResultDisplay.SetImagePosition(panX, panY, zoom);
        }

        private void OriginalDisplay_MoveImageEventHandler(double panX, double panY, double zoom)
        {
            //OriginalDisplay.SetImagePosition(panX, panY, zoom);
            RoiDisplay.SetImagePosition(panX, panY, zoom);
            EnhanceDisplay.SetImagePosition(panX, panY, zoom);
            ProcessingDisplay.SetImagePosition(panX, panY, zoom);
            MaskingDisplay.SetImagePosition(panX, panY, zoom);
            ResultDisplay.SetImagePosition(panX, panY, zoom);
        }

        private void RoiDisplay_MoveImageEventHandler(double panX, double panY, double zoom)
        {
            OriginalDisplay.SetImagePosition(panX, panY, zoom);
            //RoiDisplay.SetImagePosition(panX, panY, zoom);
            EnhanceDisplay.SetImagePosition(panX, panY, zoom);
            ProcessingDisplay.SetImagePosition(panX, panY, zoom);
            MaskingDisplay.SetImagePosition(panX, panY, zoom);
            ResultDisplay.SetImagePosition(panX, panY, zoom);
        }

        private void InitializeComboBox()
        {
            var filterList = SystemManager.Instance().AkkonParameters.GetImageFilter();

            foreach (var filter in filterList)
                cbxFilterType.Items.Add(filter.Name);

            foreach (AkkonFilterDir type in Enum.GetValues(typeof(AkkonFilterDir)))
                cbxFilterDirection.Items.Add(type.ToString());

            foreach (AkkonThMode type in Enum.GetValues(typeof(AkkonThMode)))
                cbxThresholdMode.Items.Add(type.ToString());
        }

        private void UpdateParams()
        {
            var param = SystemManager.Instance().AkkonParameters;

            // Filter
            var filter = param.GetCurrentFilter();
            if(filter == null)
            {
                cbxFilterType.SelectedIndex = 0;
                string currentFilterName = cbxFilterType.SelectedItem as string;
                param.CurrentFilterName = currentFilterName;

                var curFilter = param.GetCurrentFilter();
                txtSigma.Text = curFilter.Sigma.ToString();
                txtScaleFactor.Text = curFilter.ScaleFactor.ToString();
                txtGusWidth.Text = curFilter.GusWidth.ToString();
                txtLogWidth.Text = curFilter.LogWidth.ToString();
            }

            // Image Processing
            cbxFilterDirection.SelectedIndex = (int)param.FilterDir;
            cbxThresholdMode.SelectedIndex = (int)param.ThresParam.Mode;
            txtThresholdWeight.Text = param.ThresParam.Weight.ToString();

            // Filters
            txtMinSize.Text = param.ResultFilter.MinSize.ToString();
            txtMaxSize.Text = param.ResultFilter.MaxSize.ToString();

            // Draw Options
            ckbContainLeadCount.Checked = param.DrawOption.ContainLeadCount;
            ckbContainLeadCount.Checked = param.DrawOption.ContainLeadROI;
            ckbContainNG.Checked = param.DrawOption.ContainNG;
        }

        private void UpdateFilterUI(string filterName)
        {
            if (filterName == "Filter2_M" || filterName == "Filter4_M")
            {
                txtSigma.Enabled = false;
                txtScaleFactor.Enabled = false;
                txtGusWidth.Enabled = false;
                txtLogWidth.Enabled = false;
            }
            else
            {
                txtSigma.Enabled = true;
                txtScaleFactor.Enabled = true;
                txtGusWidth.Enabled = true;
                txtLogWidth.Enabled = true;
            }
        }

        public void UpdateSliceInfo()
        {
            _isLoading = true;

            cbxSliceList.Items.Clear();
            if (SystemManager.Instance().SliceList.Count() == 0)
            {
                ClearAllDisplay();
            }
            else
            {
                var inspectorList = SystemManager.Instance().SliceList;

                for (int i = 0; i < inspectorList.Count(); i++)
                    cbxSliceList.Items.Add(i.ToString());
            }

            _isLoading = false;

            if (cbxSliceList.Items.Count > 0)
                cbxSliceList.SelectedIndex = 0;
        }

        private void ClearAllDisplay()
        {
            OriginalDisplay.Clear();
            RoiDisplay.Clear();
            EnhanceDisplay.Clear();
            ProcessingDisplay.Clear();
            MaskingDisplay.Clear();
            ResultDisplay.Clear();
        }

        private void cbxSliceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoading)
                return;

            var index = cbxSliceList.SelectedIndex;
            if (index < 0)
                return;

            SelectSlice(index);
        }

        public void SelectSlice(int index)
        {
            ClearAllDisplay();

            var slice = SystemManager.Instance().SliceList[index];
            ICogImage cogImage = AppsHelper.ConvertCogGrayImage(slice.Image);


            SetOriginalDisplay(cogImage);
            SetTeachingRoiDisplay(cogImage, slice);

        }

        private void SetOriginalDisplay(ICogImage cogImage)
        {
            OriginalDisplay.SetImage(cogImage);
        }

  
        private void SetTeachingRoiDisplay(ICogImage cogImage, AkkonSlice slice)
        {
            if (slice.CalcAkkonROIs.Count() == 0)
            {
                RoiDisplay.SetImage(cogImage);
                return;
            }

            CogGraphicInteractiveCollection collect = new CogGraphicInteractiveCollection();

            int count = 0;
            foreach (var roi in slice.CalcAkkonROIs)
            {
                CogRectangleAffine rect = ConvertAkkonRoiToCogRectAffine(roi);
                collect.Add(rect);

                CogGraphicLabel cogLabel = new CogGraphicLabel();
                cogLabel.Color = CogColorConstants.Green;
                cogLabel.Font = new Font("맑은 고딕", 20, FontStyle.Bold);
                cogLabel.Text = count.ToString();
                cogLabel.X = (rect.CornerOppositeX + rect.CornerYX) / 2;
                cogLabel.Y = rect.CornerYY + 40;
                collect.Add(cogLabel);
                count++;
            }

            RoiDisplay.SetImage(cogImage);
            RoiDisplay.SetInteractiveGraphics("tool", collect);
        }

        public CogRectangleAffine ConvertAkkonRoiToCogRectAffine(AkkonROI akkonRoi)
        {
            CogRectangleAffine cogRectAffine = new CogRectangleAffine();

            double originX = akkonRoi.LeftTop.X;
            double originY = akkonRoi.LeftTop.Y;

            double cornerXX = akkonRoi.RightTop.X;
            double cornerXY = akkonRoi.RightTop.Y;

            double cornerYX = akkonRoi.LeftBottom.X;
            double cornerYY = akkonRoi.LeftBottom.Y;

            cogRectAffine.SetOriginCornerXCornerY(originX, originY, cornerXX, cornerXY, cornerYX, cornerYY);

            return cogRectAffine;
        }

        private void cbxSliceList_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawComboboxCenterAlign(sender, e);
        }

        private void DrawComboboxCenterAlign(object sender, DrawItemEventArgs e)
        {
            try
            {
                ComboBox cmb = sender as ComboBox;

                if (cmb != null)
                {
                    e.DrawBackground();
                    if (e.Index >= 0)
                    {
                        StringFormat sf = new StringFormat();
                        sf.LineAlignment = StringAlignment.Center;
                        sf.Alignment = StringAlignment.Center;

                        Brush brush = new SolidBrush(cmb.ForeColor);

                        if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                            brush = SystemBrushes.HighlightText;

                        e.Graphics.DrawString(cmb.Items[e.Index].ToString(), cmb.Font, brush, e.Bounds, sf);
                    }
                }
            }
            catch (Exception err)
            {

                throw;
            }

        }
        #endregion

        private void lblInspection_Click(object sender, EventArgs e)
        {
            if (cbxSliceList.SelectedIndex < 0)
                return;

            UpdateData();
            SystemManager.Instance().RunForDebug(cbxSliceList.SelectedIndex);

            UpdateResult(cbxSliceList.SelectedIndex);
        }

        private void UpdateResult(int index)
        {
            var curSlice = SystemManager.Instance().SliceList[index];
            var curResultList = SystemManager.Instance().CurrentResult;
            var curParam = SystemManager.Instance().AkkonParameters;

            ICogImage cogEnhanceImage = AppsHelper.ConvertCogGrayImage(curSlice.EnhanceMat);
            EnhanceDisplay.SetImage(cogEnhanceImage);

            ICogImage cogProcessingImage = AppsHelper.ConvertCogGrayImage(curSlice.ProcessingMat);
            ProcessingDisplay.SetImage(cogProcessingImage);

            ICogImage cogMaskingImage = AppsHelper.ConvertCogGrayImage(curSlice.MaskingMat);
            MaskingDisplay.SetImage(cogMaskingImage);

            Mat colorMat = new Mat();
            CvInvoke.CvtColor(curSlice.Image, colorMat, ColorConversion.Gray2Bgr);

            foreach (var result in curResultList)
            {
                if (curParam.DrawOption.ContainLeadROI)
                {
                    var startPoint = new Point((int)result.OffsetToWorldX, (int)result.OffsetToWorldY);
                    TempDrawLead(ref colorMat, result.Lead, new Point(0,0));
                }

                foreach (var blob in result.BlobList)
                {
                    int leftFromSlice = (int)(blob.BoundingRect.X + result.LeadOffsetX);
                    int topFromSlice = (int)(blob.BoundingRect.Y + result.LeadOffsetY);

                    Point center = new Point(leftFromSlice + (blob.BoundingRect.Width / 2), topFromSlice + (blob.BoundingRect.Height / 2));
                    int radius = blob.BoundingRect.Width > blob.BoundingRect.Height ? blob.BoundingRect.Width : blob.BoundingRect.Height;

                    int size = blob.BoundingRect.Width * blob.BoundingRect.Height;

                    if(curParam.ResultFilter.MinSize <= size && size <= curParam.ResultFilter.MaxSize)
                        CvInvoke.Circle(colorMat, center, radius / 2, new MCvScalar(255,0,0), 1);
                    else
                    {
                        if(curParam.DrawOption.ContainNG)
                            CvInvoke.Circle(colorMat, center, radius / 2, new MCvScalar(0,0,255), 1);
                    }
                }
            }

            ICogImage cogResultImage = AppsHelper.ConvertCogColorImage(colorMat);
            ResultDisplay.SetImage(cogResultImage);
        }

        private void TempDrawLead(ref Mat mat, AkkonROI roi, Point startPoint)
        {
            Point leftTop = new Point(roi.LeftTop.X + startPoint.X, roi.LeftTop.Y + startPoint.Y);
            Point leftBottom = new Point(roi.LeftBottom.X + startPoint.X, roi.LeftBottom.Y + startPoint.Y);
            Point rightTop = new Point(roi.RightTop.X + startPoint.X, roi.RightTop.Y + startPoint.Y);
            Point rightBottom = new Point(roi.RightBottom.X + startPoint.X, roi.RightBottom.Y + startPoint.Y);

            CvInvoke.Line(mat, leftTop, leftBottom, new MCvScalar(255), 1);
            CvInvoke.Line(mat, leftTop, rightTop, new MCvScalar(255), 1);
            CvInvoke.Line(mat, rightTop, rightBottom, new MCvScalar(255), 1);
            CvInvoke.Line(mat, rightBottom, leftBottom, new MCvScalar(255), 1);
        }

        private void cbxFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cbxFilterType.SelectedIndex;
            if (index < 0)
                return;

            string filterName = cbxFilterType.SelectedItem as string;
            UpdateFilterUI(filterName);

            var filter = SystemManager.Instance().AkkonParameters.GetImageFilter(filterName);
            txtSigma.Text = filter.Sigma.ToString();
            txtScaleFactor.Text = filter.ScaleFactor.ToString();
            txtGusWidth.Text = filter.GusWidth.ToString();
            txtLogWidth.Text = filter.LogWidth.ToString();
        }

        public void UpdateData()
        {
            var param = SystemManager.Instance().AkkonParameters;

            string curFilterName = cbxFilterType.SelectedItem as string;

            if (curFilterName == null)
                return;

            param.CurrentFilterName = curFilterName;
            param.GetImageFilter(curFilterName).Sigma = Convert.ToDouble(txtSigma.Text);
            param.GetImageFilter(curFilterName).ScaleFactor = Convert.ToDouble(txtScaleFactor.Text);
            param.GetImageFilter(curFilterName).GusWidth = Convert.ToInt16(txtGusWidth.Text);
            param.GetImageFilter(curFilterName).LogWidth = Convert.ToInt16(txtLogWidth.Text);

            param.FilterDir = (AkkonFilterDir)cbxFilterDirection.SelectedIndex;
            param.ThresParam.Mode = (AkkonThMode)cbxThresholdMode.SelectedIndex;
            param.ThresParam.Weight = Convert.ToDouble(txtThresholdWeight.Text);

            param.ResultFilter.MinSize = Convert.ToDouble(txtMinSize.Text);
            param.ResultFilter.MaxSize = Convert.ToDouble(txtMaxSize.Text);

            param.DrawOption.ContainLeadCount = ckbContainLeadCount.Checked;
            param.DrawOption.ContainLeadROI = ckbContainLeadROI.Checked;
            param.DrawOption.ContainNG = ckbContainNG.Checked;
        }
    }

    public enum Display
    {
        Original,
        ROI,
        Enhance,
        Processing,
        Masking,
        Result,
    }
}
