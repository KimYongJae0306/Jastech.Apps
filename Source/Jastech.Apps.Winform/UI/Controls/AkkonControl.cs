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
using Jastech.Apps.Structure;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Macron.Akkon.Parameters;
using Jastech.Framework.Macron.Akkon;
using Jastech.Framework.Winform;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Imaging.Helper;
using Emgu.CV;
using Emgu.CV.CvEnum;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Jastech.Apps.Structure.Parameters;
using Jastech.Framework.Macron.Akkon.Results;
using Jastech.Framework.Winform.Helper;
using static Jastech.Framework.Device.Motions.AxisMovingParam;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AkkonControl : UserControl
    {
        #region 필드
        private CogRectangle _autoTeachingRect { get; set; } = null;

        private CogGraphicInteractiveCollection _autoTeachingCollect { get; set; } = new CogGraphicInteractiveCollection();

        private ROICloneDirection _cloneDirection = ROICloneDirection.Horizontal;

        private string _prevTabName { get; set; } = string.Empty;

        private Color _selectedColor = new Color();

        private Color _nonSelectedColor = new Color();

        private AkkonTeachingMode _teachingMode { get; set; } = AkkonTeachingMode.Manual;

        private CogRectangleAffine _firstCogRectAffine { get; set; } = new CogRectangleAffine();

        private AkkonROI _firstAkkonRoi { get; set; } = new AkkonROI();

        private bool _isLoading { get; set; } = false;

        #endregion

        #region 속성
        public AlgorithmTool AlgorithmTool = new AlgorithmTool();

        public MacronAkkonParamControl MacronAkkonParamControl { get; private set; } = null;

        public Tab CurrentTab { get; private set; } = null;

        public AlgorithmTool Algorithm { get; private set; } = new AlgorithmTool();

        public double CalcResolution { get; private set; } = 0.0; // ex :  /camera.PixelResolution_mm(0.0035) / camera.LensScale(5) / 1000;
        #endregion

        #region 이벤트

        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public AkkonControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void AkkonControl_Load(object sender, EventArgs e)
        {
            _isLoading = true;

            AddControl();
            InitializeUI();
            InitializeGroupInfo();

            _isLoading = false;

          
            UpdateData();
        }

        private void AddControl()
        {
            MacronAkkonParamControl = new MacronAkkonParamControl();
            MacronAkkonParamControl.Dock = DockStyle.Fill;
            pnlParam.Controls.Add(MacronAkkonParamControl);
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            dgvAkkonResult.Dock = DockStyle.Fill;
            tlpnlGroup.Dock = DockStyle.Fill;
            pnlManual.Dock = DockStyle.Fill;
            pnlAuto.Dock = DockStyle.Fill;

            ShowGroup();

            lblOrginalImage.BackColor = _selectedColor;
            lblResultImage.BackColor = _nonSelectedColor;
        }

        public void SetParams(Tab tab)
        {
            if (tab == null)
                return;

            CurrentTab = tab;
            InitializeGroupInfo();
            UpdateData();
        }

        private void InitializeGroupInfo()
        {
            _isLoading = true;

            if (CurrentTab == null)
                return;

            int groupNum = CurrentTab.AkkonParam.GroupList.Count;

            cbxGroupNumber.Items.Clear();
            for (int i = 0; i < groupNum; i++)
                cbxGroupNumber.Items.Add(i);

            if (cbxGroupNumber.Items.Count > 0)
                cbxGroupNumber.SelectedIndex = 0;

            _prevTabName = CurrentTab.Name;
            _isLoading = false;
        }

        private void UpdateData()
        {
            int groupNo = cbxGroupNumber.SelectedIndex;
            if (groupNo < 0 || CurrentTab == null)
            {
                lblGroupCountValue.Text = "0";
                DrawROI();
                return;
            }

            var akkonParam = CurrentTab.AkkonParam;

            var group = akkonParam.GroupList[groupNo];

            // Manual Teaching
            lblGroupCountValue.Text = akkonParam.GroupList.Count.ToString();
            lblLeadCountValue.Text = group.Count.ToString();
            lblLeadPitchValue.Text = group.Pitch.ToString("F2");
            lblROIWidthValue.Text = group.Width.ToString("F2");
            lblROIHeightValue.Text = group.Height.ToString("F2");

            //Auto Teaching
            lblAutoThresholdValue.Text = group.Threshold.ToString();
            lblAutoLeadPitch.Text = group.Pitch.ToString("F2");

            UpdateROIDataGridView(CurrentTab.GetAkkonGroup(groupNo).AkkonROIList);

            MacronAkkonParamControl.SetParam(akkonParam.MacronAkkonParam);
            MacronAkkonParamControl.UpdateData();

            DrawROI();

        }

        private void UpdateParam(int groupIndex)
        {
            var groupParam = CurrentTab.GetAkkonGroup(groupIndex);
            if (groupParam == null || CurrentTab == null)
                return;

            var akkonParam = CurrentTab.AkkonParam;

            lblGroupCountValue.Text = akkonParam.GroupList.Count.ToString();
            MacronAkkonGroup group = akkonParam.GroupList[groupIndex];

            // Manual Teaching
            lblLeadCountValue.Text = group.Count.ToString();
            lblLeadPitchValue.Text = group.Pitch.ToString("F2");
            lblROIWidthValue.Text = group.Width.ToString("F2");
            lblROIHeightValue.Text = group.Height.ToString("F2");

            //Auto Teaching
            lblAutoThresholdValue.Text = group.Threshold.ToString();
            lblAutoLeadPitch.Text = group.Pitch.ToString("F2");

            UpdateROIDataGridView(groupParam.AkkonROIList);

            MacronAkkonParamControl.SetParam(akkonParam.MacronAkkonParam);
            MacronAkkonParamControl.UpdateData();

            DrawROI();

        }

        public void AddROI()
        {
            var display = AppsTeachingUIManager.Instance().GetDisplay();
            if (display.GetImage() == null)
                return;

            if (ModelManager.Instance().CurrentModel == null)
                return;

            if (CurrentTab == null)
                return;

            if (CurrentTab.AkkonParam.GroupList.Count <= 0)
                return;

            if (_teachingMode == AkkonTeachingMode.Manual)
                SetNewManualROI(display);
            else
                SetNewAutoROI(display);
        }

        private void SetNewManualROI(CogDisplayControl display)
        {
            display.ClearGraphic();
            ICogImage cogImage = display.GetImage();

            double centerX = display.ImageWidth() / 2.0 - display.GetPan().X;
            double centerY = display.ImageHeight() / 2.0 - display.GetPan().Y;

            double roiwidth = Convert.ToDouble(lblROIWidthValue.Text);
            double roiheight = Convert.ToDouble(lblROIHeightValue.Text);

            CogRectangleAffineDOFConstants constants = CogRectangleAffineDOFConstants.Position | CogRectangleAffineDOFConstants.Size | CogRectangleAffineDOFConstants.Skew;
            _firstCogRectAffine = CogImageHelper.CreateRectangleAffine(centerX, centerY, roiwidth, roiheight, constants: constants);

            var teachingDisplay = AppsTeachingUIManager.Instance().GetDisplay();
            if (teachingDisplay.GetImage() == null)
                return;

            CogGraphicInteractiveCollection collect = new CogGraphicInteractiveCollection();
            collect.Add(_firstCogRectAffine);
            teachingDisplay.SetInteractiveGraphics("tool", collect);
        }

        private void SetNewAutoROI(CogDisplayControl display)
        {
            display.ClearGraphic();

            ICogImage cogImage = display.GetImage();

            double centerX = display.ImageWidth() / 2.0 - display.GetPan().X;
            double centerY = display.ImageHeight() / 2.0 - display.GetPan().Y;

            _autoTeachingRect = CogImageHelper.CreateRectangle(centerX, centerY, display.ImageWidth(), display.ImageHeight());
            //_autoTeachingRect = CogImageHelper.CreateRectangle(centerX, centerY, 100, 100);
            _autoTeachingRect.DraggingStopped += AutoTeachingRect_DraggingStopped;

            var teachingDisplay = AppsTeachingUIManager.Instance().GetDisplay();
            if (teachingDisplay.GetImage() == null)
                return;

            _autoTeachingCollect.Clear();
            _autoTeachingCollect.Add(_autoTeachingRect);
            teachingDisplay.DeleteInInteractiveGraphics("tool");
            teachingDisplay.SetInteractiveGraphics("tool", _autoTeachingCollect);
        }

        private void AutoTeachingRect_DraggingStopped(object sender, CogDraggingEventArgs e)
        {
            var display = AppsTeachingUIManager.Instance().GetDisplay();
            if (display.GetImage() == null)
                return;

            display.SetImage(AppsTeachingUIManager.Instance().GetOriginCogImageBuffer(true));
        }

        private void SetFirstROI(AkkonROI roi)
        {
            _firstAkkonRoi = roi.DeepCopy();
        }

        private AkkonROI GetFirstROI()
        {
            return _firstAkkonRoi;
        }

        public void RegisterROI()
        {
            var display = AppsTeachingUIManager.Instance().GetDisplay();
            if (display == null)
                return;

            int groupIndex = cbxGroupNumber.SelectedIndex;

            List<AkkonROI> roiList = new List<AkkonROI>();
            var akkonRoi = ConvertCogRectAffineToAkkonRoi(_firstCogRectAffine);
            roiList.Add(akkonRoi);

            SetFirstROI(akkonRoi);
            UpdateROIDataGridView(roiList);
        }

        private AkkonROI ConvertCogRectAffineToAkkonRoi(CogRectangleAffine cogRectAffine)
        {
            AkkonROI akkonRoi = new AkkonROI();

            akkonRoi.CornerOriginX = cogRectAffine.CornerOriginX;
            akkonRoi.CornerOriginY = cogRectAffine.CornerOriginY;
            akkonRoi.CornerXX = cogRectAffine.CornerXX;
            akkonRoi.CornerXY = cogRectAffine.CornerXY;
            akkonRoi.CornerYX = cogRectAffine.CornerYX;
            akkonRoi.CornerYY = cogRectAffine.CornerYY;
            akkonRoi.CornerOppositeX = cogRectAffine.CornerOppositeX;
            akkonRoi.CornerOppositeY = cogRectAffine.CornerOppositeY;

            return akkonRoi;
        }

        private void UpdateROIDataGridView(List<AkkonROI> roiList)
        {
            if (roiList.Count <= 0)
                return;

            dgvAkkonROI.Rows.Clear();

            int index = 0;
            foreach (var item in roiList)
            {
                string leftTop = item.CornerOriginX.ToString("F2") + " , " + item.CornerOriginY.ToString("F2");
                string rightTop = item.CornerXX.ToString("F2") + " , " + item.CornerXY.ToString("F2");
                string leftBottom = item.CornerYX.ToString("F2") + " , " + item.CornerYY.ToString("F2");
                string rightBottom = item.CornerOppositeX.ToString("F2") + " , " + item.CornerOppositeY.ToString("F2");

                dgvAkkonROI.Rows.Add(index.ToString(), leftTop, rightTop, leftBottom, rightBottom);

                index++;
            }
        }

        private void UpdateROIDataGridView(int index, CogRectangleAffine cogRectAffine)
        {
            string leftTop = cogRectAffine.CornerOriginX.ToString("F2") + " , " + cogRectAffine.CornerOriginY.ToString("F2");
            string rightTop = cogRectAffine.CornerXX.ToString("F2") + " , " + cogRectAffine.CornerXY.ToString("F2");
            string leftBottom = cogRectAffine.CornerYX.ToString("F2") + " , " + cogRectAffine.CornerYY.ToString("F2");
            string rightBottom = cogRectAffine.CornerOppositeX.ToString("F2") + " , " + cogRectAffine.CornerOppositeY.ToString("F2");

            dgvAkkonROI[0, index].Value = index.ToString();
            dgvAkkonROI[1, index].Value = leftTop;
            dgvAkkonROI[2, index].Value = rightTop;
            dgvAkkonROI[3, index].Value = leftBottom;
            dgvAkkonROI[4, index].Value = rightBottom;
        }

        public void DrawROI()
        {
            var display = AppsTeachingUIManager.Instance().GetDisplay();

            display.ClearGraphic();

            if (display.GetImage() == null)
                return;

            if (CurrentTab == null)
                return;

            int groupIndex = cbxGroupNumber.SelectedIndex;
            if (groupIndex < 0)
                return;

            var roiList = CurrentTab.GetAkkonGroup(groupIndex).AkkonROIList;

            CogGraphicInteractiveCollection collect = new CogGraphicInteractiveCollection();

            int count = 0;
            _cogRectAffineList.Clear();
            foreach (var roi in roiList)
            {
                CogRectangleAffine rect = ConvertAkkonRoiToCogRectAffine(roi);
                collect.Add(rect);
                _cogRectAffineList.Add(rect);

                //CogGraphicLabel cogLabel = new CogGraphicLabel();
                //cogLabel.Color = CogColorConstants.Green;
                //cogLabel.Font = new Font("맑은 고딕", 20, FontStyle.Bold);
                //cogLabel.Text = count.ToString();
                //cogLabel.X = (rect.CornerOppositeX + rect.CornerYX) / 2;
                //cogLabel.Y = rect.CornerYY + 40;
                //collect.Add(cogLabel);
                //count++;
            }

            var teachingDisplay = AppsTeachingUIManager.Instance().GetDisplay();
            if (teachingDisplay.GetImage() == null)
                return;
            teachingDisplay.SetInteractiveGraphics("tool", collect);
        }

        public void ClearDisplay()
        {
            var display = AppsTeachingUIManager.Instance().GetDisplay();

            display.ClearGraphic();
        }
        #endregion

        private void DrawComboboxCenterAlign(object sender, DrawItemEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            if (cmb != null)
            {
                e.DrawBackground();

                if (cmb.Name.ToString().ToLower().Contains("group"))
                    cmb.ItemHeight = lblGroupNumber.Height - 6;
                else
                    cmb.ItemHeight = lblGroupNumber.Height - 6;

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

        private void UpdateResult(List<AkkonResult> akkonResults)
        {
            int no = 0;
            foreach (var tabResult in akkonResults)
            {
                foreach (var lead in tabResult.LeadResultList)
                {
                    string[] row = { no.ToString(), lead.BlobCount.ToString(), lead.Length.ToString(), lead.LeadStdDev.ToString(), lead.IsGood.ToString() };
                    dgvAkkonResult.Rows.Add(row);

                    no++;
                }
            }
        }

        private ICogImage GetResultImage(Mat mat, AkkonParam akkonParam, float resize)
        {
            double width = Math.Truncate(mat.Width * resize);
            double height = Math.Truncate(mat.Height * resize);

            Mat testMat = new Mat((int)height, (int)width, DepthType.Cv8U, 1);
            Mat resultMatImage = Algorithm.LastAkkonResultImage(testMat, akkonParam, 0, 0);

            Mat matR = MatHelper.ColorChannelSprate(resultMatImage, MatHelper.ColorChannel.R);
            Mat matG = MatHelper.ColorChannelSprate(resultMatImage, MatHelper.ColorChannel.G);
            Mat matB = MatHelper.ColorChannelSprate(resultMatImage, MatHelper.ColorChannel.B);

            byte[] dataR = new byte[matR.Width * matR.Height];
            Marshal.Copy(matR.DataPointer, dataR, 0, matR.Width * matR.Height);

            byte[] dataG = new byte[matG.Width * matG.Height];
            Marshal.Copy(matG.DataPointer, dataG, 0, matG.Width * matG.Height);

            byte[] dataB = new byte[matB.Width * matB.Height];
            Marshal.Copy(matB.DataPointer, dataB, 0, matB.Width * matB.Height);

            var cogImage = CogImageHelper.CovertImage(dataR, dataG, dataB, matB.Width, matB.Height);

            return cogImage;
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
            tlpnlGroup.Visible = true;

            if (_teachingMode == AkkonTeachingMode.Manual)
            {
                pnlManual.Visible = true;
                pnlAuto.Visible = false;
            }
            else
            {
                pnlManual.Visible = false;
                pnlAuto.Visible = true;
            }

            lblResult.BackColor = _nonSelectedColor;
            dgvAkkonResult.Visible = false;

            if (_cloneDirection == ROICloneDirection.Horizontal)
            {
                lblCloneHorizontal.BackColor = _selectedColor;
                lblCloneVertical.BackColor = _nonSelectedColor;
            }
            else
            {
                lblCloneHorizontal.BackColor = _nonSelectedColor;
                lblCloneVertical.BackColor = _selectedColor;
            }

        }

        private void ShowResult()
        {
            lblResult.BackColor = _selectedColor;
            dgvAkkonResult.Visible = true;

            lblGroup.BackColor = _nonSelectedColor;
            tlpnlGroup.Visible = false;
        }

        private void cbx_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawComboboxCenterAlign(sender, e);
        }

        private void lblGroupCountValue_Click(object sender, EventArgs e)
        {
            if (CurrentTab == null)
                return;

            int groupCount = KeyPadHelper.SetLabelIntegerData((Label)sender);

            CurrentTab.AkkonParam.AdjustGroupCount(groupCount);

            InitializeGroupComboBox();
        }

        private void InitializeGroupComboBox()
        {
            cbxGroupNumber.Items.Clear();

            if (CurrentTab == null)
                return;

            var akkonParam = CurrentTab.AkkonParam;

            if (akkonParam.GroupList.Count() <= 0)
                return;

            for (int groupIndex = 0; groupIndex < akkonParam.GroupList.Count(); groupIndex++)
                cbxGroupNumber.Items.Add(akkonParam.GroupList[groupIndex].Index.ToString());

            cbxGroupNumber.SelectedIndex = 0;
        }

        private void lblLeadCountValue_Click(object sender, EventArgs e)
        {
            if (CurrentTab == null)
                return;

            int leadCount = KeyPadHelper.SetLabelIntegerData((Label)sender);
            int groupIndex = cbxGroupNumber.SelectedIndex;

            CurrentTab.AkkonParam.GroupList[groupIndex].Count = leadCount;
        }

        private void lblLeadPitchValue_Click(object sender, EventArgs e)
        {
            if (CurrentTab == null)
                return;

            double leadPitch = KeyPadHelper.SetLabelDoubleData((Label)sender);
            int groupIndex = cbxGroupNumber.SelectedIndex;

            CurrentTab.AkkonParam.GroupList[groupIndex].Pitch = leadPitch;

            //AutoTeaching
            lblAutoLeadPitch.Text = leadPitch.ToString();
        }

        private void lblROIWidthValue_Click(object sender, EventArgs e)
        {
            if (CurrentTab == null)
                return;

            double leadWidth = KeyPadHelper.SetLabelDoubleData((Label)sender);
            int groupIndex = cbxGroupNumber.SelectedIndex;

            CurrentTab.AkkonParam.GroupList[groupIndex].Width = leadWidth;
        }

        private void lblROIHeightValue_Click(object sender, EventArgs e)
        {
            if (CurrentTab == null)
                return;

            double leadHeight = KeyPadHelper.SetLabelDoubleData((Label)sender);
            int groupIndex = cbxGroupNumber.SelectedIndex;

            CurrentTab.AkkonParam.GroupList[groupIndex].Height = leadHeight;
        }

        private void lblCloneVertical_Click(object sender, EventArgs e)
        {
            lblCloneVertical.BackColor = _selectedColor;
            lblCloneHorizontal.BackColor = _nonSelectedColor;
            _cloneDirection = ROICloneDirection.Vertical;
        }

        private void lblCloneHorizontal_Click(object sender, EventArgs e)
        {
            lblCloneHorizontal.BackColor = _selectedColor;
            lblCloneVertical.BackColor = _nonSelectedColor;
            _cloneDirection = ROICloneDirection.Horizontal;
        }

        private void lblCloneExecute_Click(object sender, EventArgs e)
        {
            CloneROI();
        }

        private void CloneROI()
        {
            if (CurrentTab == null)
                return;

            int groupIndex = cbxGroupNumber.SelectedIndex;
            var group = CurrentTab.AkkonParam.GroupList[groupIndex];
            group.AkkonROIList.Clear();
            _cogRectAffineList.Clear();

            var leadCount = CurrentTab.GetAkkonGroup(groupIndex).Count;
            var camera = DeviceManager.Instance().CameraHandler.Get(CameraName.LinscanMIL0.ToString());
          
            AkkonROI firstRoi = GetFirstROI();

            for (int leadIndex = 0; leadIndex < leadCount; leadIndex++)
            {
                AkkonROI newRoi = firstRoi.DeepCopy();

                if (_cloneDirection == ROICloneDirection.Horizontal)
                {
                    newRoi.CornerOriginX += (group.Pitch * leadIndex / CalcResolution);
                    newRoi.CornerXX += (group.Pitch * leadIndex / CalcResolution);
                    newRoi.CornerYX += (group.Pitch * leadIndex / CalcResolution);
                    newRoi.CornerOppositeX += (group.Pitch * leadIndex / CalcResolution);
                }
                else
                {
                    newRoi.CornerOriginY += (group.Pitch * leadIndex / CalcResolution);
                    newRoi.CornerXY += (group.Pitch * leadIndex / CalcResolution);
                    newRoi.CornerYY += (group.Pitch * leadIndex / CalcResolution);
                    newRoi.CornerOppositeY += (group.Pitch * leadIndex / CalcResolution);
                }

                group.AddROI(newRoi);

                CogRectangleAffine cogRect = ConvertAkkonRoiToCogRectAffine(newRoi);
                cogRect.GraphicDOFEnable = CogRectangleAffineDOFConstants.Position | CogRectangleAffineDOFConstants.Size | CogRectangleAffineDOFConstants.Skew;
            }

            UpdateROIDataGridView(group.AkkonROIList);
            DrawROI();
        }

        private void cbxGroupNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoading)
                return;

            int selectedGroupIndex = cbxGroupNumber.SelectedIndex;
            UpdateParam(selectedGroupIndex);
        }

        private void lblRegister_Click(object sender, EventArgs e)
        {
            RegisterROI();
        }

        private void lblDelete_Click(object sender, EventArgs e)
        {
            DeleteROI();
        }

        private void DeleteROI()
        {
            if (CurrentTab == null)
                return;

            int groupIndex = cbxGroupNumber.SelectedIndex;
            var group = CurrentTab.AkkonParam.GroupList[groupIndex];

            foreach (DataGridViewRow row in dgvAkkonROI.SelectedRows)
            {
                int index = row.Index;

                group.DeleteROI(index);
                _cogRectAffineList.RemoveAt(index);
            }
            UpdateROIDataGridView(group.AkkonROIList);
            DrawROI();
        }

        private void dgvAkkonROI_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SetSelectAkkonROI(e.RowIndex);
        }

        private void SetSelectAkkonROI(int index)
        {
            if (_cogRectAffineList.Count <= 0 || CurrentTab == null)
                return;
            
            int groupIndex = cbxGroupNumber.SelectedIndex;
            var group = CurrentTab.AkkonParam.GroupList[groupIndex];

            foreach (DataGridViewRow row in dgvAkkonROI.Rows)
                _cogRectAffineList[row.Index].Color = CogColorConstants.Blue;

            _cogRectAffineList[index].Color = CogColorConstants.DarkRed;

            CogGraphicInteractiveCollection collect = new CogGraphicInteractiveCollection();
            foreach (var item in _cogRectAffineList)
                collect.Add(item);

            var display = AppsTeachingUIManager.Instance().GetDisplay();

            if(lblResultImage.BackColor == _selectedColor)
                SetOrginImageView();

            display.SetInteractiveGraphics("lead", collect);
        }

        private List<CogRectangleAffine> _cogRectAffineList = new List<CogRectangleAffine>();

        private CogRectangleAffine ConvertAkkonRoiToCogRectAffine(AkkonROI akkonRoi)
        {
            CogRectangleAffine cogRectAffine = new CogRectangleAffine();

            cogRectAffine.SetOriginCornerXCornerY(akkonRoi.CornerOriginX, akkonRoi.CornerOriginY, akkonRoi.CornerXX, akkonRoi.CornerXY, akkonRoi.CornerYX, akkonRoi.CornerYY);

            return cogRectAffine;
        }

        private void lblSort_Click(object sender, EventArgs e)
        {
            SortROI();
        }

        private void SortROI()
        {
            if (_cogRectAffineList.Count <= 0 || CurrentTab == null)
                return;

            var display = AppsTeachingUIManager.Instance().GetDisplay();

            int groupIndex = cbxGroupNumber.SelectedIndex;
            var group = CurrentTab.AkkonParam.GroupList[groupIndex];
            var leadCount = CurrentTab.GetAkkonGroup(groupIndex).Count;

            float deltaX = 0, deltaY = 0;
            CogRadian firstAngle = 0, lastAngle = 0, deltaAngle = 0;
            int intervalCount = leadCount - 1;

            deltaX = ((float)(_cogRectAffineList.Last().CenterX - _cogRectAffineList[0].CenterX) / intervalCount);
            deltaY = ((float)(_cogRectAffineList.Last().CenterY - _cogRectAffineList[0].CenterY) / intervalCount);
            group.Pitch = deltaX;

            firstAngle = _cogRectAffineList[0].Skew;
            lastAngle = _cogRectAffineList.Last().Skew;
            deltaAngle = (float)(lastAngle.Value - firstAngle.Value) / intervalCount;

            double newCenterX = 0.0, newCenterY = 0.0;
            CogRadian newSkew = 0;
            for (int leadIndex = 0; leadIndex < leadCount; leadIndex++)
            {
                newCenterX = _cogRectAffineList[0].CenterX + (deltaX * leadIndex);
                newCenterY = _cogRectAffineList[0].CenterY + (deltaY * leadIndex);
                newSkew = _cogRectAffineList[0].Skew + (deltaAngle.Value * leadIndex);

                _cogRectAffineList[leadIndex].CenterX = newCenterX;
                _cogRectAffineList[leadIndex].CenterY = newCenterY;
                _cogRectAffineList[leadIndex].Skew = newSkew.Value;

                UpdateROIDataGridView(leadIndex, _cogRectAffineList[leadIndex]);

                CogGraphicInteractiveCollection collect = new CogGraphicInteractiveCollection();
                collect.Add(_cogRectAffineList[leadIndex]);
                display.SetInteractiveGraphics("lead", collect);
            }
        }

        public void ShowROIJog()
        {
            ROIJogControl roiJogForm = new ROIJogControl();
            roiJogForm.SetTeachingItem(TeachingItem.Akkon);
            roiJogForm.SendEventHandler += new ROIJogControl.SendClickEventDelegate(ReceiveClickEvent);
            roiJogForm.ShowDialog();
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

            var display = AppsTeachingUIManager.Instance().GetDisplay();

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

            if (dgvAkkonROI.CurrentCell == null)
            {
                if (isSkewZero)
                    _firstCogRectAffine.Skew = 0;
                else
                    _firstCogRectAffine.Skew += skewUnit;
            }
            else
            {
                int selectedIndex = dgvAkkonROI.CurrentCell.RowIndex;
                if (isSkewZero)
                    _cogRectAffineList[selectedIndex].Skew = 0;
                else
                    _cogRectAffineList[selectedIndex].Skew += skewUnit;

                UpdateROIDataGridView(selectedIndex, _cogRectAffineList[selectedIndex]);

                int groupIndex = cbxGroupNumber.SelectedIndex;
                var group = CurrentTab.AkkonParam.GroupList[groupIndex];
                group.AkkonROIList[selectedIndex] = ConvertCogRectAffineToAkkonRoi(_cogRectAffineList[selectedIndex]).DeepCopy();
                DrawROI();
                SetSelectAkkonROI(selectedIndex);
            }
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

            if (dgvAkkonROI.CurrentCell == null)
            {
                _firstCogRectAffine.CenterX += jogMoveX;
                _firstCogRectAffine.CenterY += jogMoveY;
            }
            else
            {
                int selectedIndex = dgvAkkonROI.CurrentCell.RowIndex;

                _cogRectAffineList[selectedIndex].CenterX += jogMoveX;
                _cogRectAffineList[selectedIndex].CenterY += jogMoveY;

                UpdateROIDataGridView(selectedIndex, _cogRectAffineList[selectedIndex]);

                int groupIndex = cbxGroupNumber.SelectedIndex;
                var group = CurrentTab.AkkonParam.GroupList[groupIndex];
                group.AkkonROIList[selectedIndex] = ConvertCogRectAffineToAkkonRoi(_cogRectAffineList[selectedIndex]).DeepCopy();
                DrawROI();
                SetSelectAkkonROI(selectedIndex);
            }
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

            if (dgvAkkonROI.CurrentCell == null)
            {
                _firstCogRectAffine.SideXLength += jogSizeX;
                _firstCogRectAffine.SideYLength += jogSizeY;
            }
            else
            {
                int selectedIndex = dgvAkkonROI.CurrentCell.RowIndex;

                double minimumX = _cogRectAffineList[selectedIndex].SideXLength + jogSizeX;
                double minimumY = _cogRectAffineList[selectedIndex].SideYLength + jogSizeY;
                if (minimumX <= 0 || minimumY <= 0)
                    return;

                _cogRectAffineList[selectedIndex].SideXLength += jogSizeX;
                _cogRectAffineList[selectedIndex].SideYLength += jogSizeY;

                UpdateROIDataGridView(selectedIndex, _cogRectAffineList[selectedIndex]);

                int groupIndex = cbxGroupNumber.SelectedIndex;
                var group = CurrentTab.AkkonParam.GroupList[groupIndex];
                group.AkkonROIList[selectedIndex] = ConvertCogRectAffineToAkkonRoi(_cogRectAffineList[selectedIndex]).DeepCopy();
                DrawROI();
                SetSelectAkkonROI(selectedIndex);
            }
        }

        public void SaveAkkonParam()
        {
            if (CurrentTab == null)
                return;

            int groupIndex = cbxGroupNumber.SelectedIndex;
            if (groupIndex < 0)
                return;

            var group = CurrentTab.AkkonParam.GroupList[groupIndex];
            group.AkkonROIList.Clear();

            foreach (DataGridViewRow row in dgvAkkonROI.Rows)
            {
                List<Tuple<double, double>> parsedData = ParseDataGridViewData(row);

                AkkonROI akkonRoi = new AkkonROI();

                akkonRoi.CornerOriginX = parsedData[0].Item1;
                akkonRoi.CornerOriginY = parsedData[0].Item2;
                akkonRoi.CornerXX = parsedData[1].Item1;
                akkonRoi.CornerXY = parsedData[1].Item2;
                akkonRoi.CornerYX = parsedData[2].Item1;
                akkonRoi.CornerYY = parsedData[2].Item2;
                akkonRoi.CornerOppositeX = parsedData[3].Item1;
                akkonRoi.CornerOppositeY = parsedData[3].Item2;

                group.AddROI(akkonRoi);
            }
        }

        private List<Tuple<double, double>> ParseDataGridViewData(DataGridViewRow row)
        {
            string[] leftTop = row.Cells[1].Value.ToString().Split(',');
            string[] rightTop = row.Cells[2].Value.ToString().Split(',');
            string[] leftBottom = row.Cells[3].Value.ToString().Split(',');
            string[] rightBottom = row.Cells[4].Value.ToString().Split(',');

            List<Tuple<double, double>> outputData = new List<Tuple<double, double>>();

            outputData.Add(new Tuple<double, double>(Convert.ToDouble(leftTop[0].Trim()), Convert.ToDouble(leftTop[1].Trim())));
            outputData.Add(new Tuple<double, double>(Convert.ToDouble(rightTop[0].Trim()), Convert.ToDouble(rightTop[1].Trim())));
            outputData.Add(new Tuple<double, double>(Convert.ToDouble(leftBottom[0].Trim()), Convert.ToDouble(leftBottom[1].Trim())));
            outputData.Add(new Tuple<double, double>(Convert.ToDouble(rightBottom[0].Trim()), Convert.ToDouble(rightBottom[1].Trim())));

            return outputData;
        }

        private void lblAutoLeadPitch_Click(object sender, EventArgs e)
        {
            if (CurrentTab == null)
                return;

            double leadPitch = KeyPadHelper.SetLabelDoubleData((Label)sender);
            int groupIndex = cbxGroupNumber.SelectedIndex;

            CurrentTab.AkkonParam.GroupList[groupIndex].Pitch = leadPitch;

            //Manual Teaching
            lblLeadPitch.Text = leadPitch.ToString();
        }

        private void lblAutoThresholdValue_Click(object sender, EventArgs e)
        {
            if (CurrentTab == null)
                return;

            int threshold = KeyPadHelper.SetLabelIntegerData((Label)sender);
            int groupIndex = cbxGroupNumber.SelectedIndex;

            CurrentTab.AkkonParam.GroupList[groupIndex].Threshold = threshold;
        }

        private void lblThresholdPreview_Click(object sender, EventArgs e)
        {
            if (lblThresholdPreview.BackColor == _selectedColor)
            {
                var teachingDisplay = AppsTeachingUIManager.Instance().GetDisplay();
                if (teachingDisplay.GetImage() == null)
                    return;

                lblThresholdPreview.BackColor = _nonSelectedColor;

                _autoTeachingCollect.Clear();
                _autoTeachingCollect.Add(_autoTeachingRect);

                teachingDisplay.SetImage(AppsTeachingUIManager.Instance().GetOriginCogImageBuffer(true));

                teachingDisplay.DeleteInInteractiveGraphics("tool");
                teachingDisplay.SetInteractiveGraphics("tool", _autoTeachingCollect);
            }
            else
            {
                lblThresholdPreview.BackColor = _selectedColor;
                int threshold = Convert.ToInt32(lblAutoThresholdValue.Text);

                if (_autoTeachingCollect.Count > 0)
                {
                    var display = AppsTeachingUIManager.Instance().GetDisplay();
                    if (display.GetImage() == null)
                        return;

                    ICogImage cogImage = display.GetImage();
                    var roi = _autoTeachingCollect[0] as CogRectangle;

                    var cropImage = CogImageHelper.CropImage(cogImage, roi);
                    var binaryImage = CogImageHelper.Threshold(cropImage as CogImage8Grey, threshold, 255);
                    var convertImage = CogImageHelper.CogCopyRegionTool(cogImage, binaryImage, roi, true);
                    AppsTeachingUIManager.Instance().SetBinaryCogImageBuffer(convertImage as CogImage8Grey);
                }
            }
        }

        private void lblTeachingMode_Click(object sender, EventArgs e)
        {
            if (_teachingMode == AkkonTeachingMode.Manual)
            {
                _teachingMode = AkkonTeachingMode.Auto;
                pnlManual.Visible = false;
                pnlAuto.Visible = true;
            }
            else
            {
                _teachingMode = AkkonTeachingMode.Manual;
                pnlManual.Visible = true;
                pnlAuto.Visible = false;
            }

            lblTeachingMode.Text = _teachingMode.ToString() + " Mode";
        }

        private void lblAutoTeachingExcute_Click(object sender, EventArgs e)
        {
            var image = AppsTeachingUIManager.Instance().GetOriginCogImageBuffer(true);
            if (image == null)
                return;

            var teachingDisplay = AppsTeachingUIManager.Instance().GetDisplay();
            teachingDisplay.SetImage(image);

            if (ModelManager.Instance().CurrentModel == null)
                return;

            var roiList = GetAutoTeachingRoiList(image);
            if (roiList.Count == 0)
                return;

            AddAutoTeachingAkkonRoi(roiList);

            UpdateROIDataGridView(GetGroup().AkkonROIList);
            DrawROI();
        }
        
        private List<CogRectangleAffine> GetAutoTeachingRoiList(ICogImage image)
        {
            int threshold = Convert.ToInt32(lblAutoThresholdValue.Text);
            var cropImage = CogImageHelper.CropImage(image, _autoTeachingRect);
            var binaryImage = CogImageHelper.Threshold(cropImage as CogImage8Grey, threshold, 255, true);

            byte[] topDataArray = CogImageHelper.GetWidthDataArray(binaryImage, 0);
            byte[] bottomDataArray = CogImageHelper.GetWidthDataArray(binaryImage, cropImage.Height - 1);

            List<int> topEdgePointList = new List<int>();
            List<int> bottomEdgePointList = new List<int>();

            ImageHelper.GetEdgePoint(topDataArray, bottomDataArray, 0, (int)30/*CalcResolution*/, ref topEdgePointList, ref bottomEdgePointList);

            //if (topEdgePointList.Count != bottomEdgePointList.Count)
            //{
            //    MessageConfirmForm form = new MessageConfirmForm();
            //    form.Message = "The number of edge points found is different.";
            //    form.ShowDialog();
            //    return new List<CogRectangleAffine>();
            //}

            List<PointF> topPointList = new List<PointF>();
            foreach (var xIndex in topEdgePointList)
            {
                float pointX = (float)(_autoTeachingRect.X + xIndex);
                float pointY = (float)_autoTeachingRect.Y;
                topPointList.Add(new PointF(pointX, pointY));
            }

            List<PointF> bottomPointList = new List<PointF>();
            foreach (var xIndex in bottomEdgePointList)
            {
                float pointX = (float)(_autoTeachingRect.X + xIndex);
                float pointY = (float)(_autoTeachingRect.Y + _autoTeachingRect.Height);
                bottomPointList.Add(new PointF(pointX, pointY));
            }

            var roiList = CogImageHelper.CreateRectangleAffine(topPointList, bottomPointList);
            return roiList;
        }

        private void AddAutoTeachingAkkonRoi(List<CogRectangleAffine> roiList)
        {
            if (GetGroup() is MacronAkkonGroup group)
            {
                group.AkkonROIList.Clear();
                foreach (var roi in roiList)
                {
                    AkkonROI akkonRoi = new AkkonROI
                    {
                        CornerOppositeX = roi.CornerOppositeX,
                        CornerOppositeY = roi.CornerOppositeY,
                        CornerOriginX = roi.CornerOriginX,
                        CornerOriginY = roi.CornerOriginY,
                        CornerXX = roi.CornerXX,
                        CornerXY = roi.CornerXY,
                        CornerYX = roi.CornerYX,
                        CornerYY = roi.CornerYY,
                    };
                    group.AddROI(akkonRoi);
                }
            }
        }

        private MacronAkkonGroup GetGroup()
        {
            if (cbxGroupNumber.SelectedIndex < 0 || CurrentTab == null)
                return null;

            var group = CurrentTab.AkkonParam.GroupList[cbxGroupNumber.SelectedIndex];

            return group;
        }

        private void lblTest_Click(object sender, EventArgs e)
        {
            CropInspection();
        }

        public void Inspection()
        {
            int groupIndex = cbxGroupNumber.SelectedIndex;
            if (groupIndex < 0 || CurrentTab == null)
                return;

            var akkonParam = CurrentTab.AkkonParam;
            int akkonThreadCount = AppsConfig.Instance().AkkonThreadCount;
            akkonParam.MacronAkkonParam = MacronAkkonParamControl.GetCurrentParam();

            Mat matImage = AppsTeachingUIManager.Instance().GetOriginMatImageBuffer(false);
            if (matImage == null)
                return;

            MacronAkkonParam macron = akkonParam.MacronAkkonParam;

            float resizeRatio = 1.0f;
            if (macron.InspParam.PanelInfo == (int)TargetType.COG)
                resizeRatio = 1.0f;
            else if(macron.InspParam.PanelInfo == (int)TargetType.COF)
                resizeRatio = 0.5f;
            else if(macron.InspParam.PanelInfo == (int)TargetType.FOG)
                resizeRatio = 0.6f;

            akkonParam.MacronAkkonParam.InspOption.InspResizeRatio = resizeRatio;
            akkonParam.MacronAkkonParam.DrawOption.DrawResizeRatio = resizeRatio;

            macron.SliceHeight = matImage.Height;

            var tabResults = Algorithm.RunAkkon(matImage, CurrentTab.AkkonParam, CurrentTab.StageIndex, CurrentTab.Index);

            dgvAkkonResult.Rows.Clear();

            UpdateResult(tabResults);

            var resultImage = GetResultImage(matImage, CurrentTab.AkkonParam, CurrentTab.AkkonParam.MacronAkkonParam.InspOption.InspResizeRatio);

            if (resultImage != null)
            {
                lblOrginalImage.BackColor = _nonSelectedColor;
                lblResultImage.BackColor = _selectedColor;

                AppsTeachingUIManager.Instance().SetResultCogImage(resultImage);
                ClearDisplay();
            }
        }

        private void CropInspection()
        {
            int groupIndex = cbxGroupNumber.SelectedIndex;
            if (groupIndex < 0 || CurrentTab == null)
                return;

            var akkonParam = CurrentTab.AkkonParam;

            int akkonThreadCount = AppsConfig.Instance().AkkonThreadCount;
            akkonParam.MacronAkkonParam = MacronAkkonParamControl.GetCurrentParam();

            Mat matImage = AppsTeachingUIManager.Instance().GetOriginMatImageBuffer(false);
            if (matImage == null)
                return;

            MacronAkkonParam macron = CurrentTab.AkkonParam.MacronAkkonParam;
            float resizeRatio = 1.0f;
            if (macron.InspParam.PanelInfo == (int)TargetType.COG)
                resizeRatio = 1.0f;
            else if (macron.InspParam.PanelInfo == (int)TargetType.COF)
                resizeRatio = 0.5f;
            else if (macron.InspParam.PanelInfo == (int)TargetType.FOG)
                resizeRatio = 0.6f;

            akkonParam.MacronAkkonParam.InspOption.InspResizeRatio = resizeRatio;
            akkonParam.MacronAkkonParam.DrawOption.DrawResizeRatio = resizeRatio;

            var roiList = akkonParam.GetAkkonROIList();
            var rect = GetContainInROI(roiList);

            Stopwatch cropSW = new Stopwatch();
            cropSW.Restart();

            Mat cropImage = MatHelper.CropRoi(matImage, rect);
            
            macron.SliceHeight = cropImage.Height;
            var tabResults = Algorithm.RunCropAkkon(cropImage, new PointF(rect.X, rect.Y), akkonParam, CurrentTab.Index);

            cropSW.Stop();
            Console.WriteLine("Crop : " + cropSW.ElapsedMilliseconds.ToString());

            dgvAkkonResult.Rows.Clear();

            UpdateResult(tabResults);

            var resultImage = GetResultImage(cropImage, CurrentTab.AkkonParam, CurrentTab.AkkonParam.MacronAkkonParam.InspOption.InspResizeRatio);
            if (resultImage != null)
            {
                lblOrginalImage.BackColor = _nonSelectedColor;
                lblResultImage.BackColor = _selectedColor;

                AppsTeachingUIManager.Instance().SetResultCogImage(resultImage);
                ClearDisplay();
            }
        }

        private Rectangle GetContainInROI(List<AkkonROI> akkonROIs)
        {
            var leftTopX = akkonROIs.Select(x => x.CornerOriginX).Min();
            var leftTopY = akkonROIs.Select(x => x.CornerOriginY).Min();
            var leftBottomX = akkonROIs.Select(x => x.CornerYX).Min();
            var leftBottomY = akkonROIs.Select(x => x.CornerYY).Max();

            var rightTopX = akkonROIs.Select(x => x.CornerXX).Max();
            var rightTopY = akkonROIs.Select(x => x.CornerXY).Min();
            var rightBottomX = akkonROIs.Select(x => x.CornerOppositeX).Max();
            var rightBottomY = akkonROIs.Select(x => x.CornerOppositeY).Max();

            double left = leftTopX > leftBottomX ? leftBottomX : leftTopX;
            double top = leftTopY > rightTopY ? rightTopY : leftTopY;
            double right = rightTopX > rightBottomX ? rightTopX : rightBottomX;
            double bottom = rightBottomY > leftBottomY ? rightBottomY : leftBottomY;

            Rectangle rect = new Rectangle();
            double interval = 30;


            rect.X = (int)(left - interval);
            rect.Y = (int)(top - interval);
            rect.Width = (int)(Math.Abs(right - left) + (interval * 2));
            rect.Height = (int)(Math.Abs(bottom - top) + (interval * 2));

            return rect;
        }

        private void lblOrginalImage_Click(object sender, EventArgs e)
        {
            SetOrginImageView();
        }

        private void lblResultImage_Click(object sender, EventArgs e)
        {
            SetResultImageView();
        }

        private void SetOrginImageView()
        {
            lblOrginalImage.BackColor = _selectedColor;
            lblResultImage.BackColor = _nonSelectedColor;

            var orgImage = AppsTeachingUIManager.Instance().GetOriginCogImageBuffer(true);
            if(orgImage != null)
                AppsTeachingUIManager.Instance().GetDisplay().SetImage(orgImage);
        }

        private void SetResultImageView()
        {
            lblOrginalImage.BackColor = _nonSelectedColor;
            lblResultImage.BackColor = _selectedColor;

            var orgImage = AppsTeachingUIManager.Instance().GetResultCogImage(false);
            if(orgImage != null)
                AppsTeachingUIManager.Instance().GetDisplay().SetImage(orgImage);
        }
    }
}
