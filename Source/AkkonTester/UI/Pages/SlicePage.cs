﻿using AkkonTester.Helpers;
using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Jastech.Framework.Algorithms.Akkon;
using Jastech.Framework.Algorithms.Akkon.Parameters;
using Jastech.Framework.Winform.VisionPro.Controls;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
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
            OriginalDisplay.PixelResolution = SystemManager.Instance().Resolution_um;
            OriginalDisplay.MoveImageEventHandler += OriginalDisplay_MoveImageEventHandler;
            OriginalDisplay.SetMouseMode(CogDisplayMouseModeConstants.Pan);
            pnlOriginal.Controls.Add(OriginalDisplay);

            RoiDisplay.Dock = DockStyle.Fill;
            RoiDisplay.PixelResolution = SystemManager.Instance().Resolution_um;
            RoiDisplay.MoveImageEventHandler += RoiDisplay_MoveImageEventHandler;
            RoiDisplay.SetMouseMode(CogDisplayMouseModeConstants.Pan);
            pnlROI.Controls.Add(RoiDisplay);

            EnhanceDisplay.Dock = DockStyle.Fill;
            EnhanceDisplay.PixelResolution = SystemManager.Instance().Resolution_um;
            EnhanceDisplay.MoveImageEventHandler += EnhanceDisplay_MoveImageEventHandler;
            EnhanceDisplay.SetMouseMode(CogDisplayMouseModeConstants.Pan);
            pnlEnhance.Controls.Add(EnhanceDisplay);

            ProcessingDisplay.Dock = DockStyle.Fill;
            ProcessingDisplay.PixelResolution = SystemManager.Instance().Resolution_um;
            ProcessingDisplay.MoveImageEventHandler += ProcessingDisplay_MoveImageEventHandler;
            ProcessingDisplay.SetMouseMode(CogDisplayMouseModeConstants.Pan);
            pnlProcessing.Controls.Add(ProcessingDisplay);

            MaskingDisplay.Dock = DockStyle.Fill;
            MaskingDisplay.PixelResolution = SystemManager.Instance().Resolution_um;
            MaskingDisplay.MoveImageEventHandler += MaskingDisplay_MoveImageEventHandler;
            MaskingDisplay.SetMouseMode(CogDisplayMouseModeConstants.Pan);
            pnlMasking.Controls.Add(MaskingDisplay);

            ResultDisplay.Dock = DockStyle.Fill;
            ResultDisplay.PixelResolution = SystemManager.Instance().Resolution_um;
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
            var filterList = SystemManager.Instance().AkkonParameters.ImageFilterParam.Filters;

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
            var filter = param.ImageFilterParam.GetCurrentFilter();
            if (filter == null)
            {
                cbxFilterType.SelectedIndex = 0;
                string currentFilterName = cbxFilterType.SelectedItem as string;
                param.ImageFilterParam.CurrentFilterName = currentFilterName;

                var curFilter = param.ImageFilterParam.GetCurrentFilter();
                txtSigma.Text = curFilter.Sigma.ToString();
                txtScaleFactor.Text = curFilter.ScaleFactor.ToString();
                txtGusWidth.Text = curFilter.GusWidth.ToString();
                txtLogWidth.Text = curFilter.LogWidth.ToString();
            }
            else
            {
                int selectIndex = -1;
                for (int i = 0; i < cbxFilterType.Items.Count; i++)
                {
                    if (filter.Name == cbxFilterType.Items[i] as string)
                    {
                        selectIndex = i;
                        break;
                    }
                }
                cbxFilterType.SelectedIndex = selectIndex;
            }
            // Image Processing
            cbxFilterDirection.SelectedIndex = (int)param.ImageFilterParam.FilterDir;
            cbxThresholdMode.SelectedIndex = (int)param.ImageFilterParam.Mode;
            txtThresholdWeight.Text = param.ImageFilterParam.Weight.ToString();

            // Shape
            txtMinSize.Text = param.ShapeFilterParam.MinSize_um.ToString();
            txtMaxSize.Text = param.ShapeFilterParam.MaxSize_um.ToString();
            txtMinArea.Text = param.ShapeFilterParam.MinArea_um.ToString();
            txtMaxArea.Text = param.ShapeFilterParam.MaxArea_um.ToString();
            txtAkkonStrength.Text = param.ShapeFilterParam.MinAkkonStrength.ToString();
            txtAkkonScaleFactor.Text = param.ShapeFilterParam.AkkonStrengthScaleFactor.ToString();
            // Judgement
            txtLeadLengthX.Text = param.JudgementParam.LengthX_um.ToString();
            txtLeadLengthY.Text = param.JudgementParam.LengthY_um.ToString();
            txtLeadStdDev.Text = param.JudgementParam.LeadStdDev.ToString();

            // Draw Options
            ckbContainLeadCount.Checked = param.DrawOption.ContainLeadCount;
            ckbContainLeadCount.Checked = param.DrawOption.ContainLeadROI;
            ckbContainNG.Checked = param.DrawOption.ContainNG;
            ckbDrawSize.Checked = param.DrawOption.ContainSize;
            ckbDrawArea.Checked = param.DrawOption.ContainArea;
            ckbDrawStrength.Checked = param.DrawOption.ContainStrength;
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

            double originX = akkonRoi.LeftTopX;
            double originY = akkonRoi.LeftTopY;

            double cornerXX = akkonRoi.RightTopX;
            double cornerXY = akkonRoi.RightTopY;

            double cornerYX = akkonRoi.LeftBottomX;
            double cornerYY = akkonRoi.LeftBottomY;

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
                Console.WriteLine(err.ToString());
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

            UpdateProcessingImage(cbxSliceList.SelectedIndex);
            UpdateResultImage(cbxSliceList.SelectedIndex);
            UpdateResultValue();
        }

        private void UpdateResultValue()
        {
            var curResultList = SystemManager.Instance().CurrentLeadResult;
            dgvResult.Rows.Clear();

            foreach (var result in curResultList)
            {
                string index = result.Roi.Index.ToString();
                string count = result.AkkonCount.ToString();
                string lengthX = result.LengthX_um.ToString("F2");
                string lengthY = result.LengthY_um.ToString("F2");
                string stdDev = result.StdDev.ToString("F2");

                string message = string.Format("Count : {0} LengthX : {1}, LengthY : {2}, StdDev : {3}",count, lengthX, lengthY, stdDev);

                string[] row = { index, message };
                dgvResult.Rows.Add(row);
            }
        }

        private void UpdateProcessingImage(int index)
        {
            var curSlice = SystemManager.Instance().SliceList[index];
            var curResultList = SystemManager.Instance().CurrentLeadResult;
            var curParam = SystemManager.Instance().AkkonParameters;

            ICogImage cogEnhanceImage = AppsHelper.ConvertCogGrayImage(curSlice.EnhanceMat);
            EnhanceDisplay.SetImage(cogEnhanceImage);

            ICogImage cogProcessingImage = AppsHelper.ConvertCogGrayImage(curSlice.ProcessingMat);
            ProcessingDisplay.SetImage(cogProcessingImage);

            ICogImage cogMaskingImage = AppsHelper.ConvertCogGrayImage(curSlice.MaskingMat);
            MaskingDisplay.SetImage(cogMaskingImage);
        }

        private void UpdateResultImage(int index)
        {
            MCvScalar redColor = new MCvScalar(50, 50, 230, 255);
            MCvScalar greenColor = new MCvScalar(50, 230, 50, 255);

            var curSlice = SystemManager.Instance().SliceList[index];
            var curResultList = SystemManager.Instance().CurrentLeadResult;
            var curParam = SystemManager.Instance().AkkonParameters;

            Mat colorMat = new Mat();
            CvInvoke.CvtColor(curSlice.Image, colorMat, ColorConversion.Gray2Bgr);

            float resolution = SystemManager.Instance().Resolution_um;
            float resize = SystemManager.Instance().AkkonParameters.ImageFilterParam.ResizeRatio;
            float calcResolution = resolution / resize;

            foreach (var result in curResultList)
            {
                if (curParam.DrawOption.ContainLeadROI)
                {
                    var startPoint = new Point((int)result.Offset.ToWorldX, (int)result.Offset.ToWorldY);
                    TempDrawLead(ref colorMat, result.Roi, new Point(0,0));
                }
                foreach (var blob in result.BlobList)
                {
                    
                    int leftFromSlice = (int)(blob.BoundingRect.X + result.Offset.X);
                    int topFromSlice = (int)(blob.BoundingRect.Y + result.Offset.Y);

                    Point center = new Point(leftFromSlice + (blob.BoundingRect.Width / 2), topFromSlice + (blob.BoundingRect.Height / 2));
                    int radius = blob.BoundingRect.Width > blob.BoundingRect.Height ? blob.BoundingRect.Width : blob.BoundingRect.Height;

                    int size = blob.BoundingRect.Width * blob.BoundingRect.Height;

                    if (blob.BoundingRect.Width <= 1 || blob.BoundingRect.Height <= 1)
                        continue;

                    if (blob.IsAkkonShape)
                    {
                        CvInvoke.Circle(colorMat, center, radius / 2, greenColor, 1);
                    }
                    else
                    {
                        if (curParam.DrawOption.ContainNG)
                        {
                            blob.BoundingRect.X = leftFromSlice;
                            blob.BoundingRect.Y = topFromSlice;

                            CvInvoke.Rectangle(colorMat, blob.BoundingRect, redColor, 1);
                        }
                    }

                    if (curParam.DrawOption.ContainSize)
                    {
                        int temp = (int)(radius / 2.0);
                        Point pt = new Point(center.X + temp, center.Y - temp);
                        double akkonSize = (blob.BoundingRect.Width + blob.BoundingRect.Height) / 2.0;
                        double blobSize = akkonSize * calcResolution;

                        if (blob.IsAkkonShape)
                            CvInvoke.PutText(colorMat, blobSize.ToString("F1"), pt, FontFace.HersheySimplex, 0.3, greenColor);
                        else
                            CvInvoke.PutText(colorMat, blobSize.ToString("F1"), pt, FontFace.HersheySimplex, 0.3, redColor);
                    }
                    else if (curParam.DrawOption.ContainArea)
                    {
                        Point pt = new Point(leftFromSlice + blob.BoundingRect.Width, topFromSlice);
                        double blobArea = blob.Area * calcResolution;

                        if (blob.IsAkkonShape)
                            CvInvoke.PutText(colorMat, blobArea.ToString("F1"), pt, FontFace.HersheySimplex, 0.3, greenColor);
                        else
                            CvInvoke.PutText(colorMat, blobArea.ToString("F1"), pt, FontFace.HersheySimplex, 0.3, redColor);
                    }
                    else if (curParam.DrawOption.ContainStrength)
                    {
                        Point pt = new Point(leftFromSlice + blob.BoundingRect.Width, topFromSlice);
                        string strength = blob.Strength.ToString("F2");
                        if (blob.IsAkkonShape)
                            CvInvoke.PutText(colorMat, strength, pt, FontFace.HersheySimplex, 0.3, greenColor);
                        else
                            CvInvoke.PutText(colorMat, strength, pt, FontFace.HersheySimplex, 0.3, redColor);
                    }

                    if (curParam.DrawOption.ContainLeadCount)
                    {
                        string leadIndexString = result.Roi.Index.ToString();
                        string akkonCountString = string.Format("[{0}]", result.AkkonCount);

                        var leftBottom = result.Roi.GetLeftBottomPoint();
                        var rightBottom = result.Roi.GetRightBottomPoint();
                        Point centerPt = new Point((int)((leftBottom.X + rightBottom.X) / 2.0), (int)leftBottom.Y);

                        int baseLine = 0;
                        Size textSize = CvInvoke.GetTextSize(leadIndexString, FontFace.HersheyComplex, 0.3, 1, ref baseLine);
                        int textX = centerPt.X - (textSize.Width / 2);
                        int textY = centerPt.Y + (baseLine / 2);
                        CvInvoke.PutText(colorMat, leadIndexString, new Point(textX, textY + 10), FontFace.HersheyComplex, 0.3, new MCvScalar(50, 230, 50, 255));

                        textSize = CvInvoke.GetTextSize(akkonCountString, FontFace.HersheyComplex, 0.3, 1, ref baseLine);
                        textX = centerPt.X - (textSize.Width / 2);
                        textY = centerPt.Y + (baseLine / 2);
                        CvInvoke.PutText(colorMat, akkonCountString, new Point(textX, textY + 20), FontFace.HersheyComplex, 0.3, new MCvScalar(50, 230, 50, 255));
                    }
                }
            }
            ICogImage cogResultImage = AppsHelper.ConvertCogColorImage(colorMat);
            ResultDisplay.SetImage(cogResultImage);
        }

        private void TempDrawLead(ref Mat mat, AkkonROI roi, Point startPoint)
        {
            MCvScalar greenColor = new MCvScalar(50, 230, 50, 255);

            Point leftTop = new Point((int)roi.LeftTopX + startPoint.X, (int)roi.LeftTopY + startPoint.Y);
            Point leftBottom = new Point((int)roi.LeftBottomX + startPoint.X, (int)roi.LeftBottomY + startPoint.Y);
            Point rightTop = new Point((int)roi.RightTopX + startPoint.X, (int)roi.RightTopY + startPoint.Y);
            Point rightBottom = new Point((int)roi.RightBottomX + startPoint.X, (int)roi.RightBottomY + startPoint.Y);

            CvInvoke.Line(mat, leftTop, leftBottom, greenColor, 1);
            CvInvoke.Line(mat, leftTop, rightTop, greenColor, 1);
            CvInvoke.Line(mat, rightTop, rightBottom, greenColor, 1);
            CvInvoke.Line(mat, rightBottom, leftBottom, greenColor, 1);
        }

        private void cbxFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cbxFilterType.SelectedIndex;
            if (index < 0)
                return;

            string filterName = cbxFilterType.SelectedItem as string;
            UpdateFilterUI(filterName);

            var filter = SystemManager.Instance().AkkonParameters.ImageFilterParam.GetImageFilter(filterName);
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

            param.ImageFilterParam.CurrentFilterName = curFilterName;
            param.ImageFilterParam.GetImageFilter(curFilterName).Sigma = Convert.ToDouble(txtSigma.Text);
            param.ImageFilterParam.GetImageFilter(curFilterName).ScaleFactor = Convert.ToDouble(txtScaleFactor.Text);

            int.TryParse(txtGusWidth.Text, out int gusWidth);
            param.ImageFilterParam.GetImageFilter(curFilterName).GusWidth = gusWidth;

            int.TryParse(txtLogWidth.Text, out int logWidth);
            param.ImageFilterParam.GetImageFilter(curFilterName).LogWidth = logWidth;

            param.ImageFilterParam.FilterDir = (AkkonFilterDir)cbxFilterDirection.SelectedIndex;
            param.ImageFilterParam.Mode = (AkkonThMode)cbxThresholdMode.SelectedIndex;
            param.ImageFilterParam.Weight = Convert.ToDouble(txtThresholdWeight.Text);

            param.ShapeFilterParam.MinSize_um = Convert.ToSingle(txtMinSize.Text);
            param.ShapeFilterParam.MaxSize_um = Convert.ToSingle(txtMaxSize.Text);
            param.ShapeFilterParam.MinArea_um = Convert.ToSingle(txtMinArea.Text);
            param.ShapeFilterParam.MaxArea_um = Convert.ToSingle(txtMaxArea.Text);
            param.ShapeFilterParam.MinAkkonStrength = Convert.ToSingle(txtAkkonStrength.Text);
            param.ShapeFilterParam.AkkonStrengthScaleFactor = Convert.ToSingle(txtAkkonScaleFactor.Text);

            param.JudgementParam.LengthX_um = Convert.ToSingle(txtLeadLengthX.Text);
            param.JudgementParam.LengthY_um = Convert.ToSingle(txtLeadLengthY.Text);
            param.JudgementParam.LeadStdDev = Convert.ToSingle(txtLeadStdDev.Text);

            param.DrawOption.ContainLeadCount = ckbContainLeadCount.Checked;
            param.DrawOption.ContainLeadROI = ckbContainLeadROI.Checked;
            param.DrawOption.ContainNG = ckbContainNG.Checked;
            param.DrawOption.ContainSize = ckbDrawSize.Checked;
            param.DrawOption.ContainArea = ckbDrawArea.Checked;
            param.DrawOption.ContainStrength = ckbDrawStrength.Checked;
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
