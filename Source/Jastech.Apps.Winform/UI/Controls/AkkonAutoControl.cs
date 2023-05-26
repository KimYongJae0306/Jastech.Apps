using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Jastech.Apps.Structure.Data;
using Cognex.VisionPro;
using Jastech.Framework.Winform.VisionPro.Controls;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Framework.Winform.Controls;
using System.Linq;
using System.Drawing;
using Jastech.Framework.Macron.Akkon.Controls;
using Jastech.Framework.Winform.Forms;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Apps.Structure;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Macron.Akkon.Parameters;
using Jastech.Framework.Algorithms.Akkon.Parameters;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AkkonAutoControl : UserControl
    {
        #region 필드
        private string _prevTabName { get; set; } = string.Empty;

        private Color _selectedColor = new Color();

        private Color _nonSelectedColor = new Color();

        private CogRectangle _cogROI = null;

        #endregion

        private MacronAkkonParamControl MacronAkkonParamControl { get; set; } = new MacronAkkonParamControl();
        private List<Tab> TeachingTabList { get; set; } = new List<Tab>();

        //private List<VisionProCaliperParam> CaliperList { get; set; } = null;
       

        private AlgorithmTool Algorithm = new AlgorithmTool();

        #region 속성
        CogGraphicInteractiveCollection CogThresholdCollection { get; set; } = new CogGraphicInteractiveCollection();
        #endregion

        #region 이벤트

        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public AkkonAutoControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void AkkonAutoControl_Load(object sender, EventArgs e)
        {
            AddControl();
            InitializeUI();
        }

        private void AddControl()
        {
            MacronAkkonParamControl.Dock = DockStyle.Fill;
            pnlParam.Controls.Add(MacronAkkonParamControl);
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            dgvAkkonResult.Dock = DockStyle.Fill;
            pnlGroup.Dock = DockStyle.Fill;

            ShowGroup();
        }

        public void SetParams(List<Tab> tabList)
        {
            if (tabList.Count <= 0)
                return;

            TeachingTabList = tabList;
            InitializeTabComboBox();
            InitializeGroupComboBox();

            string tabName = cbxTabList.SelectedItem as string;
            UpdateParam(tabName, 0);
        }

        private void InitializeTabComboBox()
        {
            cbxTabList.Items.Clear();

            foreach (var item in TeachingTabList)
                cbxTabList.Items.Add(item.Name);

            cbxTabList.SelectedIndex = 0;
        }

        private void cbxTabList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tabName = cbxTabList.SelectedItem as string;

            if (_prevTabName == tabName)
                return;

            var display = AppsTeachingUIManager.Instance().GetDisplay();
            if (display == null)
                return;

            UpdateParam(tabName, 0);
            display.ClearGraphic();

            DrawROI();
            _prevTabName = tabName;
        }

        private void UpdateParam(string tabName, int groupIndex)
        {
            if (TeachingTabList.Count <= 0)
                return;

            var tab = TeachingTabList.Where(x => x.Name == tabName).First();
            var groupParam = tab.GetAkkonGroup(groupIndex);

            if (groupParam == null)
                return;

            if (cmbGroupNumber.SelectedIndex == -1)
                return;

            lblGroupCountValue.Text = tab.AkkonParam.GroupList.Count.ToString();
            cmbGroupNumber.SelectedIndex = groupIndex;

            lblThresholdValue.Text = tab.AkkonParam.GroupList[groupIndex].Threshold.ToString();

            //MacronAkkonParamControl.UpdateData(tab.AkkonParam.MacronAkkonParam);

            DrawROI();
        }

        public void DrawROI()
        {
            var display = AppsTeachingUIManager.Instance().GetDisplay();

            display.ClearGraphic();

            if (display.GetImage() == null)
                return;

            if(GetGroup() is MacronAkkonGroup group)
            {
                CogGraphicInteractiveCollection collect = new CogGraphicInteractiveCollection();

                int count = 0;
                foreach (var roi in group.AkkonROIList)
                {
                    CogRectangleAffine rect = ConvertAkkonRoiToCogRectAffine(roi);
                    collect.Add(rect);

                    CogGraphicLabel cogLabel = new CogGraphicLabel();
                    cogLabel.Color = CogColorConstants.Green;
                    cogLabel.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
                    cogLabel.Text = count.ToString();
                    cogLabel.X = (rect.CornerOppositeX + rect.CornerYX) / 2;
                    cogLabel.Y = rect.CornerYY + 40;
                    collect.Add(cogLabel);
                    count++;
                }

                display.SetInteractiveGraphics("tool", collect);
            }
        }

        private CogRectangleAffine ConvertAkkonRoiToCogRectAffine(AkkonROI akkonRoi)
        {
            CogRectangleAffine cogRectAffine = new CogRectangleAffine();

            cogRectAffine.SetOriginCornerXCornerY(akkonRoi.LeftTopX, akkonRoi.LeftTopY, 
                                        akkonRoi.RightTopX, akkonRoi.RightTopY, akkonRoi.LeftBottomX, akkonRoi.LeftBottomY);

            return cogRectAffine;
        }
        #endregion



        private void DrawComboboxCenterAlign(object sender, DrawItemEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            if (cmb != null)
            {
                e.DrawBackground();
                cmb.ItemHeight = lblPrev.Height - 6;

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

        private void lblPrev_Click(object sender, EventArgs e)
        {
            if (cbxTabList.SelectedIndex <= 0)
                return;

            cbxTabList.SelectedIndex -= 1;
        }

        private void lblNext_Click(object sender, EventArgs e)
        {
            int nextIndex = cbxTabList.SelectedIndex + 1;

            if (cbxTabList.Items.Count > nextIndex)
                cbxTabList.SelectedIndex = nextIndex;
        }

        public List<Tab> GetTeachingData()
        {
            return TeachingTabList;
        }

        private void lblInspection_Click(object sender, EventArgs e)
        {
            Inspection();
        }

        private void Inspection()
        {
            var display = AppsTeachingUIManager.Instance().GetDisplay();

            //var currentParam = CogCaliperParamControl.GetCurrentParam();

            //if (display == null || currentParam == null)
            //    return;

            //ICogImage cogImage = display.GetImage();

            //CogAlignCaliperResult result = new CogAlignCaliperResult();

            //if (_alignName.ToString().Contains("X"))
            //    result = Algorithm.RunAlignX(cogImage, currentParam.CaliperParams, currentParam.LeadCount);
            //else
            //    result = Algorithm.RunAlignY(cogImage, currentParam.CaliperParams);

            //if (result.Judgement == Result.Fail)
            //{
            //    MessageConfirmForm form = new MessageConfirmForm();
            //    form.Message = "Caliper is Not Found.";
            //    form.ShowDialog();
            //}
            //else
            //{
            //    display.ClearGraphic();
            //    display.UpdateResult(result);
            //}
        }

        private void lblGroup_Click(object sender, EventArgs e)
        {
            ShowGroup();
        }

        private void lblResult_Click(object sender, EventArgs e)
        {
            ShowResult();
        }

        private void ShowGroup()
        {
            lblGroup.BackColor = _selectedColor;
            pnlGroup.Visible = true;

            lblResult.BackColor = _nonSelectedColor;
            dgvAkkonResult.Visible = false;
        }

        private void ShowResult()
        {
            lblResult.BackColor = _selectedColor;
            dgvAkkonResult.Visible = true;

            lblGroup.BackColor = _nonSelectedColor;
            pnlGroup.Visible = false;
        }

        private void cmb_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawComboboxCenterAlign(sender, e);
        }

        private void lblGroupCountValue_Click(object sender, EventArgs e)
        {
            int groupCount = SetLabelIntegerData(sender);

            string tabName = cbxTabList.SelectedItem as string;
            TeachingTabList.Where(x => x.Name == tabName).First().AkkonParam.AdjustGroupCount(groupCount);
            InitializeGroupComboBox();
        }

        private void InitializeGroupComboBox()
        {
            cmbGroupNumber.Items.Clear();

            string tabName = cbxTabList.SelectedItem as string;
            var akkonParam = TeachingTabList.Where(x => x.Name == tabName).First().AkkonParam;

            for (int groupIndex = 0; groupIndex < akkonParam.GroupList.Count(); groupIndex++)
                cmbGroupNumber.Items.Add(akkonParam.GroupList[groupIndex].Index.ToString());

            if(cmbGroupNumber.Items.Count > 0)
                cmbGroupNumber.SelectedIndex = 0;
        }

        private void lblLeadCountValue_Click(object sender, EventArgs e)
        {
            int leadCount = SetLabelIntegerData(sender);

            int groupIndex = cmbGroupNumber.SelectedIndex;
            string tabName = cbxTabList.SelectedItem as string;

            TeachingTabList.Where(x => x.Name == tabName).First().AkkonParam.GroupList[groupIndex].Count = leadCount;
        }

        private int SetLabelIntegerData(object sender)
        {
            Label lbl = sender as Label;
            int prevData = Convert.ToInt32(lbl.Text);

            KeyPadForm keyPadForm = new KeyPadForm();
            keyPadForm.PreviousValue = (double)prevData;
            keyPadForm.ShowDialog();

            int inputData = Convert.ToInt16(keyPadForm.PadValue);

            Label label = (Label)sender;
            label.Text = inputData.ToString();

            return inputData;
        }

        private void lblROIWidthValue_Click(object sender, EventArgs e)
        {
            double leadWidth = SetLabelDoubleData(sender);

            string tabName = cbxTabList.SelectedItem as string;
            int groupIndex = cmbGroupNumber.SelectedIndex;

            TeachingTabList.Where(x => x.Name == tabName).First().AkkonParam.GroupList[groupIndex].Width = leadWidth;
        }

        private void lblROIHeightValue_Click(object sender, EventArgs e)
        {
            double leadHeight = SetLabelDoubleData(sender);

            string tabName = cbxTabList.SelectedItem as string;
            int groupIndex = cmbGroupNumber.SelectedIndex;

            TeachingTabList.Where(x => x.Name == tabName).First().AkkonParam.GroupList[groupIndex].Height = leadHeight;
        }

        private void lblLeadPitchValue_Click(object sender, EventArgs e)
        {
            double leadPitch = SetLabelDoubleData(sender);

            string tabName = cbxTabList.SelectedItem as string;
            int groupIndex = cmbGroupNumber.SelectedIndex;

            TeachingTabList.Where(x => x.Name == tabName).First().AkkonParam.GroupList[groupIndex].Pitch = leadPitch;
        }

        private double SetLabelDoubleData(object sender)
        {
            Label lbl = sender as Label;
            double prevData = Convert.ToDouble(lbl.Text);

            KeyPadForm keyPadForm = new KeyPadForm();
            keyPadForm.PreviousValue = prevData;
            keyPadForm.ShowDialog();

            double inputData = keyPadForm.PadValue;

            Label label = (Label)sender;
            label.Text = inputData.ToString();

            return inputData;
        }

        private void lblCloneExecute_Click(object sender, EventArgs e)
        {
            var image = AppsTeachingUIManager.Instance().GetOriginCogImageBuffer(true);
            if (image == null)
                return;

            if (ModelManager.Instance().CurrentModel == null)
                return;

            int threshold = Convert.ToInt32(lblThresholdValue.Text);
            var cropImage = VisionProImageHelper.CropImage(image, _cogROI);
            var binaryImage = VisionProImageHelper.Threshold(cropImage as CogImage8Grey, threshold, 255, true);

            byte[] topDataArray = VisionProImageHelper.GetWidthDataArray(binaryImage, 0);
            byte[] bottomDataArray = VisionProImageHelper.GetWidthDataArray(binaryImage, cropImage.Height - 1);

            List<int> topEdgePointList = new List<int>();
            List<int> bottomEdgePointList = new List<int>();

            ImageHelper.GetEdgePoint(topDataArray, bottomDataArray, 0, 30, ref topEdgePointList, ref bottomEdgePointList);

            if(topEdgePointList.Count != bottomEdgePointList.Count)
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "The number of edge points found is different.";
                form.ShowDialog();
                return;
            }

            List<PointF> topPointList = new List<PointF>();
            foreach (var xIndex in topEdgePointList)
            {
                float pointX = (float)(_cogROI.X + xIndex);
                float pointY = (float)_cogROI.Y;
                topPointList.Add(new PointF(pointX, pointY));
            }

            List<PointF> bottomPointList = new List<PointF>();
            foreach (var xIndex in bottomEdgePointList)
            {
                float pointX = (float)(_cogROI.X + xIndex);
                float pointY = (float)(_cogROI.Y + _cogROI.Height);
                bottomPointList.Add(new PointF(pointX, pointY));
            }

            var roiList = VisionProImageHelper.CreateRectangleAffine(topPointList, bottomPointList);

            if (roiList == null)
                return;

            AddAkkonRoi(roiList);
            UpdateROIDataGridView();

            var teachingDisplay = AppsTeachingUIManager.Instance().GetDisplay();
            if (teachingDisplay.GetImage() == null)
                return;
            teachingDisplay.SetImage(image);
            CogThresholdCollection.Clear();
            CogThresholdCollection.Add(_cogROI);
            teachingDisplay.DeleteInInteractiveGraphics("tool");

            CogGraphicInteractiveCollection collect = new CogGraphicInteractiveCollection();

            foreach (var roi in roiList)
            {
                collect.Add(roi);
            }
            teachingDisplay.SetInteractiveGraphics("tool", collect);
        }

        private void AddAkkonRoi(List<CogRectangleAffine> roiList)
        {
            if (GetGroup() is MacronAkkonGroup group)
            {
                group.AkkonROIList.Clear();
                foreach (var roi in roiList)
                {
                    AkkonROI akkonRoi = new AkkonROI
                    {
                        RightBottomX = roi.CornerOppositeX,
                        RightBottomY = roi.CornerOppositeY,
                        LeftTopX = roi.CornerOriginX,
                        LeftTopY = roi.CornerOriginY,
                        RightTopX = roi.CornerXX,
                        RightTopY = roi.CornerXY,
                        LeftBottomX = roi.CornerYX,
                        LeftBottomY = roi.CornerYY,
                    };
                    group.AddROI(akkonRoi);
                }
            }
        }

        private void UpdateROIDataGridView()
        {
            dgvAkkonROI.Rows.Clear();
            if (GetGroup() is MacronAkkonGroup group)
            {
                int index = 0;
                foreach (var roi in group.AkkonROIList)
                {
                    string leftTop = roi.LeftTopX.ToString("F2") + " , " + roi.LeftTopY.ToString("F2");
                    string rightTop = roi.RightTopX.ToString("F2") + " , " + roi.RightTopY.ToString("F2");
                    string leftBottom = roi.LeftBottomX.ToString("F2") + " , " + roi.LeftBottomY.ToString("F2");
                    string rightBottom = roi.RightBottomX.ToString("F2") + " , " + roi.RightBottomY.ToString("F2");

                    dgvAkkonROI.Rows.Add(index.ToString(), leftTop, rightTop, leftBottom, rightBottom);

                    index++;
                }
            }
        }

        private MacronAkkonGroup GetGroup()
        {
            if (cmbGroupNumber.SelectedIndex < 0)
                return null;

            string tabName = cbxTabList.SelectedItem as string;
            var tabList = TeachingTabList.Where(x => x.Name == tabName).First();
            var group = tabList.AkkonParam.GroupList[cmbGroupNumber.SelectedIndex];

            return group;
        }

        private void cmbGroupNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedGroupIndex = cmbGroupNumber.SelectedIndex;

            string tabName = cbxTabList.SelectedItem as string;
            UpdateParam(tabName, selectedGroupIndex);
        }

        private void lblAddROI_Click(object sender, EventArgs e)
        {
            AddROI();
        }

        private void AddROI()
        {
            var display = AppsTeachingUIManager.Instance().GetDisplay();
            if (display.GetImage() == null)
                return;

            if (ModelManager.Instance().CurrentModel == null)
                return;

            SetNewROI(display);
        }

        private void SetNewROI(CogDisplayControl display)
        {
            ICogImage cogImage = display.GetImage();

            double centerX = display.ImageWidth() / 2.0 - display.GetPan().X;
            double centerY = display.ImageHeight() / 2.0 - display.GetPan().Y;

            _cogROI = VisionProImageHelper.CreateRectangle(centerX, centerY, display.ImageWidth(), display.ImageHeight());
            _cogROI.DraggingStopped += _cogROI_DraggingStopped;

            var teachingDisplay = AppsTeachingUIManager.Instance().GetDisplay();
            if (teachingDisplay.GetImage() == null)
                return;

            CogThresholdCollection.Clear();
            CogThresholdCollection.Add(_cogROI);
            teachingDisplay.DeleteInInteractiveGraphics("tool");
            teachingDisplay.SetInteractiveGraphics("tool", CogThresholdCollection);
        }

        private void _cogROI_DraggingStopped(object sender, CogDraggingEventArgs e)
        {
            var display = AppsTeachingUIManager.Instance().GetDisplay();
            if (display.GetImage() == null)
                return;
            //_cogROI.Width = 23000;
            display.SetImage(AppsTeachingUIManager.Instance().GetOriginCogImageBuffer(true));
        }

        private void lblThreshold_Click(object sender, EventArgs e)
        {
            if(lblThreshold.BackColor == _selectedColor)
            {
                var teachingDisplay = AppsTeachingUIManager.Instance().GetDisplay();
                if (teachingDisplay.GetImage() == null)
                    return;

                lblThreshold.BackColor = _nonSelectedColor;

                

                CogThresholdCollection.Clear();
                CogThresholdCollection.Add(_cogROI);

                teachingDisplay.SetImage(AppsTeachingUIManager.Instance().GetOriginCogImageBuffer(true));

                teachingDisplay.DeleteInInteractiveGraphics("tool");
                teachingDisplay.SetInteractiveGraphics("tool", CogThresholdCollection);
            }
            else
            {
                lblThreshold.BackColor = _selectedColor;
                int threshold = Convert.ToInt32(lblThresholdValue.Text);

                if (CogThresholdCollection.Count > 0)
                {
                    var display = AppsTeachingUIManager.Instance().GetDisplay();
                    if (display.GetImage() == null)
                        return;

                    ICogImage cogImage = display.GetImage();
                    var roi = CogThresholdCollection[0] as CogRectangle;

                    var cropImage = VisionProImageHelper.CropImage(cogImage, roi);
                    var binaryImage = VisionProImageHelper.Threshold(cropImage as CogImage8Grey, threshold, 255);
                    var convertImage = VisionProImageHelper.CogCopyRegionTool(cogImage, binaryImage, roi, true);
                    AppsTeachingUIManager.Instance().SetBinaryCogImageBuffer(convertImage as CogImage8Grey);
                }
            }
            
        }
    }
}
