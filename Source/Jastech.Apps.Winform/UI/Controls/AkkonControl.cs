﻿using Cognex.VisionPro;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Flann;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Apps.Winform.Settings;
using Jastech.Apps.Winform.UI.Forms;
using Jastech.Framework.Algorithms.Akkon;
using Jastech.Framework.Algorithms.Akkon.Parameters;
using Jastech.Framework.Algorithms.UI.Controls;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionAlgorithms;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Util;
using Jastech.Framework.Winform.Forms;
using Jastech.Framework.Winform.Helper;
using Jastech.Framework.Winform.VisionPro.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AkkonControl : UserControl
    {
        #region 필드
        private CogPolygon _autoTeachingPolygon { get; set; } = null;

        private CogGraphicInteractiveCollection _autoTeachingCollect { get; set; } = new CogGraphicInteractiveCollection();

        private ROICloneDirection _cloneDirection = ROICloneDirection.Horizontal;

        private string _prevTabName { get; set; } = string.Empty;

        private Color _selectedColor = new Color();

        private Color _nonSelectedColor = new Color();

        private AkkonTeachingMode _teachingMode { get; set; } = AkkonTeachingMode.Manual;

        private CogRectangleAffine _firstCogRectAffine { get; set; } = new CogRectangleAffine();

        private AkkonROI _firstAkkonRoi { get; set; } = new AkkonROI();

        private bool _isLoading { get; set; } = false;

        private int _curSelectedGroup { get; set; } = -1;
        #endregion

        #region 속성
        public AlgorithmTool AlgorithmTool = new AlgorithmTool();

        public AkkonParamControl AkkonParamControl { get; private set; } = null;

        public Tab CurrentTab { get; private set; } = null;

        public AlgorithmTool Algorithm { get; private set; } = new AlgorithmTool();

        public AkkonAlgorithm AkkonAlgorithm { get; set; } = new AkkonAlgorithm();

        private bool UserMaker { get; set; } = false;

        public float Resolution_um { get; set; } = 0.0F; // ex :  /camera.PixelResolution_mm(0.0035) / camera.LensScale(5) / 1000;

        public bool IsReScaling { get; set; } = false;

        public bool IsMoving { get; set; } = false;

        public bool IsSkewing { get; set; } = false;
        #endregion

        #region 이벤트
        public ROIJogDelegate OpenROIJogEventHandler;

        public ROIJogDelegate CloseROIJogEventHandler;
        #endregion

        #region 델리게이트
        public delegate void ROIJogDelegate();
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
            InitializeGroupInfo(true);

            _isLoading = false;
          
            UpdateData();
        }

        private void AddControl()
        {
            AkkonParamControl = new AkkonParamControl();
            AkkonParamControl.ViewerPath = System.IO.Directory.GetCurrentDirectory();
            AkkonParamControl.Dock = DockStyle.Fill;
            AkkonParamControl.UserMaker = UserMaker;
            pnlParam.Controls.Add(AkkonParamControl);
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            dgvAkkonROI.DoubleBuffered(true);

            dgvJastechAkkonResult.Dock = DockStyle.Fill;
            tlpnlGroup.Dock = DockStyle.Fill;
            pnlManual.Dock = DockStyle.Fill;
            pnlAuto.Dock = DockStyle.Fill;

            pnlInfo.Controls.Add(dgvJastechAkkonResult);
            pnlTeachingMode.Controls.Add(pnlManual);
            pnlTeachingMode.Controls.Add(pnlAuto);
            ShowGroup();

            lblOrginalImage.BackColor = _selectedColor;
            lblResultImage.BackColor = _nonSelectedColor;
        }

        public void SetUserMaker(bool isMaker)
        {
            UserMaker = isMaker;
        }

        public void SetParams(Tab tab)
        {
            if (tab == null)
                return;

            CurrentTab = tab;
            InitializeGroupInfo(false);
            UpdateData();
        }

        public delegate void InitializeGroupInfoDele(bool isLoading);
        private void InitializeGroupInfo(bool isLoading = false)
        {
            if (this.InvokeRequired)
            {
                InitializeGroupInfoDele callback = InitializeGroupInfo;
                BeginInvoke(callback, isLoading);
                return;
            }

            _isLoading = isLoading;

            if (CurrentTab == null)
                return;

            int groupNum = CurrentTab.AkkonParam.GroupList.Count;

            cbxGroupNumber.Items.Clear();
            for (int i = 0; i < groupNum; i++)
                cbxGroupNumber.Items.Add(i);

            if (cbxGroupNumber.Items.Count > 0)
                cbxGroupNumber.SelectedIndex = 0;
            else
                _curSelectedGroup = -1;

            _prevTabName = CurrentTab.Name;
            _isLoading = false;
        }

        public delegate void UpdateDataDele();
        private void UpdateData()
        {
            if (this.InvokeRequired)
            {
                UpdateDataDele callback = UpdateData;
                BeginInvoke(callback);
                return;
            }
            int groupNo = cbxGroupNumber.SelectedIndex;
            if (groupNo < 0 || CurrentTab == null)
            {
                lblGroupCountValue.Text = "0";
                return;
            }

            var akkonParam = CurrentTab.AkkonParam;

            var group = akkonParam.GroupList[groupNo];

            // Manual Teaching
            lblGroupCountValue.Text = akkonParam.GroupList.Count.ToString();
            lblLeadCountValue.Text = group.Count.ToString();
            lblLeadPitchValue_um.Text = group.Pitch.ToString("F2");
            lblROIWidthValue_um.Text = group.Width.ToString("F2");
            lblROIHeightValue_um.Text = group.Height.ToString("F2");

            //Auto Teaching
            lblAutoThresholdValue.Text = group.Threshold.ToString();
            lblAutoLeadPitch_um.Text = group.Pitch.ToString("F2");

            UpdateROIDataGridView(CurrentTab.GetAkkonGroup(groupNo).AkkonROIList);

            if (AkkonParamControl != null)
            {
                AkkonParamControl.SetParam(akkonParam.AkkonAlgoritmParam);
                AkkonParamControl.UpdateData();
            }

            DrawROI();
        }

        private void UpdateParam(int groupIndex)
        {
            var groupParam = CurrentTab.GetAkkonGroup(groupIndex);
            if (groupParam == null || CurrentTab == null)
                return;

            var akkonParam = CurrentTab.AkkonParam;

            lblGroupCountValue.Text = akkonParam.GroupList.Count.ToString();
            AkkonGroup group = akkonParam.GroupList[groupIndex];

            // Manual Teaching
            lblLeadCountValue.Text = group.Count.ToString();
            lblLeadPitchValue_um.Text = group.Pitch.ToString("F2");
            lblROIWidthValue_um.Text = group.Width.ToString("F2");
            lblROIHeightValue_um.Text = group.Height.ToString("F2");

            //Auto Teaching
            lblAutoThresholdValue.Text = group.Threshold.ToString();
            lblAutoLeadPitch_um.Text = group.Pitch.ToString("F2");

            UpdateROIDataGridView(groupParam.AkkonROIList);

            if (AkkonParamControl != null)
            {
                AkkonParamControl.SetParam(akkonParam.AkkonAlgoritmParam);
                AkkonParamControl.UpdateData();
            }

            DrawROI();
        }

        public void AddROI()
        {
            var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();
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

            double centerX = display.GetImageWidth() / 2.0 - display.GetPan().X;
            double centerY = display.GetImageHeight() / 2.0 - display.GetPan().Y;

            float roiwidth_um = Convert.ToSingle(lblROIWidthValue_um.Text);
            float roiheight_um = Convert.ToSingle(lblROIHeightValue_um.Text);

            CogRectangleAffineDOFConstants constants = CogRectangleAffineDOFConstants.Position | CogRectangleAffineDOFConstants.Size | CogRectangleAffineDOFConstants.Skew;

            float akkonResizeRatio = CurrentTab.AkkonParam.AkkonAlgoritmParam.ImageFilterParam.ResizeRatio;

            _firstCogRectAffine = VisionProImageHelper.CreateRectangleAffine(centerX, centerY, roiwidth_um, roiheight_um, constants: constants);

            var teachingDisplay = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();
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

            double centerX = display.GetImageWidth() / 2.0 - display.GetPan().X;
            double centerY = display.GetImageHeight() / 2.0 - display.GetPan().Y;

            _autoTeachingPolygon = new CogPolygon();
            _autoTeachingPolygon.GraphicDOFEnable = CogPolygonDOFConstants.All;
            _autoTeachingPolygon.GraphicDOFEnableBase = CogGraphicDOFConstants.All;

            _autoTeachingPolygon.AddVertex(centerX - 1000, centerY - 1000, 0);
            _autoTeachingPolygon.AddVertex(centerX + 1000, centerY - 1000, 1);
            _autoTeachingPolygon.AddVertex(centerX + 1000, centerY + 1000, 2);
            _autoTeachingPolygon.AddVertex(centerX - 1000, centerY + 1000, 3);
            _autoTeachingPolygon.DraggingStopped += AutoTeachingRect_DraggingStopped;
            _autoTeachingPolygon.Interactive = true;

            var teachingDisplay = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();
            if (teachingDisplay.GetImage() == null)
                return;

            _autoTeachingCollect.Clear();
            _autoTeachingCollect.Add(_autoTeachingPolygon);
            teachingDisplay.DeleteInInteractiveGraphics("tool");
            teachingDisplay.SetInteractiveGraphics("tool", _autoTeachingCollect);
        }

        private void AutoTeachingRect_DraggingStopped(object sender, CogDraggingEventArgs e)
        {
            var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();
            if (display.GetImage() == null)
                return;

            display.SetImage(TeachingUIManager.Instance().GetOriginCogImageBuffer(true));
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
            var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();
            if (display == null)
                return;

            int groupIndex = cbxGroupNumber.SelectedIndex;

            List<AkkonROI> roiList = new List<AkkonROI>();
            var akkonRoi = ConvertCogRectAffineToAkkonRoi(_firstCogRectAffine);
            roiList.Add(akkonRoi);

            _cogRectAffineList.Add(_firstCogRectAffine);
            var group = CurrentTab.AkkonParam.GroupList[groupIndex];
            group.AkkonROIList.Add(akkonRoi);

            SetFirstROI(akkonRoi);
            UpdateROIDataGridView(roiList);
        }

        private AkkonROI ConvertCogRectAffineToAkkonRoi(CogRectangleAffine cogRectAffine)
        {
            AkkonROI akkonRoi = new AkkonROI();

            akkonRoi.LeftTopX = cogRectAffine.CornerOriginX;
            akkonRoi.LeftTopY = cogRectAffine.CornerOriginY;

            akkonRoi.RightTopX = cogRectAffine.CornerXX;
            akkonRoi.RightTopY = cogRectAffine.CornerXY;

            akkonRoi.LeftBottomX = cogRectAffine.CornerYX;
            akkonRoi.LeftBottomY = cogRectAffine.CornerYY;

            akkonRoi.RightBottomX = cogRectAffine.CornerOppositeX;
            akkonRoi.RightBottomY = cogRectAffine.CornerOppositeY;

            return akkonRoi;
        }

        public void ClearDataGridView()
        {
            dgvAkkonROI.Rows.Clear();
        }

        private void UpdateROIDataGridView(List<AkkonROI> roiList, List<int> selectedIndex = null)
        {
            dgvAkkonROI.Rows.Clear();

            if (roiList.Count <= 0)
                return;

            int index = 0;
            foreach (var item in roiList)
            {
                string leftTop = item.LeftTopX.ToString("F2") + " , " + item.LeftTopY.ToString("F2");
                string rightTop = item.RightTopX.ToString("F2") + " , " + item.RightTopY.ToString("F2");
                string leftBottom = item.LeftBottomX.ToString("F2") + " , " + item.LeftBottomY.ToString("F2");
                string rightBottom = item.RightBottomX.ToString("F2") + " , " + item.RightBottomY.ToString("F2");

                dgvAkkonROI.Rows.Add(index.ToString(), leftTop, rightTop, leftBottom, rightBottom);
                dgvAkkonROI.Rows[index].Selected = false;
                index++;
            }

            if (selectedIndex != null)
            {
                foreach (var item in selectedIndex)
                    dgvAkkonROI.Rows[item].Selected = true;
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
            var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();

            display.ClearGraphic();

            if (display.GetImage() == null)
                return;

            if (CurrentTab == null)
                return;

            int groupIndex = _curSelectedGroup;
            if (groupIndex < 0)
            {
                dgvAkkonROI.Rows.Clear();
                return;
            }

            var roiList = CurrentTab.GetAkkonGroup(groupIndex).AkkonROIList;

            CogGraphicInteractiveCollection collect = new CogGraphicInteractiveCollection();

            int count = 0;
            _cogRectAffineList.Clear();

            bool isDrawLeadIndex = ckbDrawIndex.Checked;
            foreach (var roi in roiList)
            {
                CogRectangleAffine rect = ConvertAkkonRoiToCogRectAffine(roi);
                collect.Add(rect);
                _cogRectAffineList.Add(rect);

                if (isDrawLeadIndex)
                {
                    CogGraphicLabel cogLabel = new CogGraphicLabel();
                    cogLabel.Color = CogColorConstants.Green;
                    cogLabel.Font = new Font("맑은 고딕", 20, FontStyle.Bold);
                    cogLabel.Text = count.ToString();
                    cogLabel.X = (rect.CornerOppositeX + rect.CornerYX) / 2;
                    cogLabel.Y = rect.CornerYY + 40;
                    collect.Add(cogLabel);
                }
               
                count++;
            }

            var teachingDisplay = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();
            if (teachingDisplay.GetImage() == null)
                return;
            teachingDisplay.ClearGraphic();
            teachingDisplay.SetInteractiveGraphics("tool", collect);
        }

        public void ClearDisplay()
        {
            var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();

            display.ClearGraphic();
        }

        public void InspectionDoneUI()
        {
            lblOrginalImage.BackColor = _nonSelectedColor;
            lblResizeImage.BackColor = _nonSelectedColor;
            lblResultImage.BackColor = _selectedColor;
        }

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

        private void UpdateResult(List<AkkonLeadResult> leadResultList)
        {
            int no = 0;
            dgvJastechAkkonResult.Rows.Clear();

            foreach (var lead in leadResultList)
            {
                string noString = no.ToString();
                int blobCount = lead.BlobList.Count();

                double avg = 0.0;
                for (int i = 0; i < blobCount; i++)
                    avg += lead.BlobList[i].Avg;

                avg /= blobCount;

                string[] row = { no.ToString(), lead.BlobList.Count().ToString(), Math.Round(avg, 2).ToString() };
                dgvJastechAkkonResult.Rows.Add(row);

                no++;
            }
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
            dgvJastechAkkonResult.Visible = false;

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
            dgvJastechAkkonResult.Visible = true;

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
            lblAutoLeadPitch_um.Text = leadPitch.ToString();
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
            AkkonROI firstRoi = GetFirstROI();

            if (firstRoi.CheckValidRoi() == false)
            {
                MessageConfirmForm confirmForm = new MessageConfirmForm();
                confirmForm.Message = "Not valid ROI.";
                confirmForm.ShowDialog();
                return;
            }

            if (Resolution_um == 0)
                return;

            float calcResolution = Resolution_um / CurrentTab.AkkonParam.AkkonAlgoritmParam.ImageFilterParam.ResizeRatio;

            for (int leadIndex = 0; leadIndex < leadCount; leadIndex++)
            {
                AkkonROI newRoi = firstRoi.DeepCopy();

                if (_cloneDirection == ROICloneDirection.Horizontal)
                {
                    newRoi.LeftTopX += (group.Pitch * leadIndex / calcResolution);
                    newRoi.RightTopX += (group.Pitch * leadIndex / calcResolution);
                    newRoi.LeftBottomX += (group.Pitch * leadIndex / calcResolution);
                    newRoi.RightBottomX += (group.Pitch * leadIndex / calcResolution);
                }
                else
                {
                    newRoi.LeftTopY += (group.Pitch * leadIndex / calcResolution);
                    newRoi.RightTopY += (group.Pitch * leadIndex / calcResolution);
                    newRoi.LeftBottomY += (group.Pitch * leadIndex / calcResolution);
                    newRoi.RightBottomY += (group.Pitch * leadIndex / calcResolution);
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

            _curSelectedGroup = cbxGroupNumber.SelectedIndex;
            UpdateParam(_curSelectedGroup);
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

            if (dgvAkkonROI.SelectedRows.Count > 1)
            {
                List<int> selectedIndexList = new List<int>();
                foreach (DataGridViewRow row in dgvAkkonROI.SelectedRows)
                    selectedIndexList.Add(row.Index);

                if (selectedIndexList.Count > 0)
                    selectedIndexList.Sort((x, y) => y.CompareTo(x));

                foreach (var index in selectedIndexList)
                {
                    group.DeleteROI(index);
                    if (_cogRectAffineList.Count > index)
                        _cogRectAffineList.RemoveAt(index);
                }
            }
            else
            {
                foreach (DataGridViewRow row in dgvAkkonROI.SelectedRows)
                {
                    group.DeleteROI(row.Index);
                    if (_cogRectAffineList.Count > row.Index)
                        _cogRectAffineList.RemoveAt(row.Index);
                }
            }

            group.Count = _cogRectAffineList.Count;
            lblLeadCountValue.Text = group.Count.ToString();
            UpdateROIDataGridView(group.AkkonROIList);
            DrawROI();
        }

        private void SetSelectAkkonROI()
        {
            if (_cogRectAffineList.Count <= 0 || CurrentTab == null || IsReScaling || IsMoving || IsSkewing)
                return;

            int groupIndex = cbxGroupNumber.SelectedIndex;
            if (groupIndex < 0)
            {
                dgvAkkonROI.Rows.Clear();
                return;
            }
            var group = CurrentTab.AkkonParam.GroupList[groupIndex];
            _selectedIndexList = new List<int>();

            for (int i = 0; i < dgvAkkonROI.Rows.Count; i++)
            {
                var row = dgvAkkonROI.Rows[i];
                if (row.Selected)
                {
                    _selectedIndexList.Add(i);
                    _cogRectAffineList[i].Color = CogColorConstants.DarkRed;
                }
                else
                {
                    if (row.Index < _cogRectAffineList.Count)
                        _cogRectAffineList[row.Index].Color = CogColorConstants.Blue;
                }
            }

            CogGraphicInteractiveCollection collect = new CogGraphicInteractiveCollection();
            foreach (var item in _cogRectAffineList)
                collect.Add(item);

            var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();

            if (lblResultImage.BackColor == _selectedColor)
                SetOrginImageView();

            ckbDrawIndex.Checked = false;
            display.ClearGraphic();
            display.SetInteractiveGraphics("lead", collect);
        }

        private List<CogRectangleAffine> _cogRectAffineList = new List<CogRectangleAffine>();

        private CogRectangleAffine ConvertAkkonRoiToCogRectAffine(AkkonROI akkonRoi)
        {
            PointF leftTop = new PointF(Convert.ToSingle(akkonRoi.LeftTopX), Convert.ToSingle(akkonRoi.LeftTopY));
            PointF rightTop = new PointF(Convert.ToSingle(akkonRoi.RightTopX), Convert.ToSingle(akkonRoi.RightTopY));
            PointF leftBottom = new PointF(Convert.ToSingle(akkonRoi.LeftBottomX), Convert.ToSingle(akkonRoi.LeftBottomY));

            return VisionProShapeHelper.ConvertToCogRectAffine(leftTop, rightTop, leftBottom);
        }

        private void lblSort_Click(object sender, EventArgs e)
        {
            SortROI();
        }

        private void SortROI()
        {
            if (_cogRectAffineList.Count <= 0 || CurrentTab == null)
                return;

            var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();

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
            group.AkkonROIList.Clear();

            for (int leadIndex = 0; leadIndex < leadCount; leadIndex++)
            {
                newCenterX = _cogRectAffineList[0].CenterX + (deltaX * leadIndex);
                newCenterY = _cogRectAffineList[0].CenterY + (deltaY * leadIndex);
                newSkew = _cogRectAffineList[0].Skew + (deltaAngle.Value * leadIndex);

                _cogRectAffineList[leadIndex].CenterX = newCenterX;
                _cogRectAffineList[leadIndex].CenterY = newCenterY;
                _cogRectAffineList[leadIndex].Skew = newSkew.Value;

                group.AkkonROIList.Add(ConvertCogRectAffineToAkkonRoi(_cogRectAffineList[leadIndex]).DeepCopy());

                CogGraphicInteractiveCollection collect = new CogGraphicInteractiveCollection();
                collect.Add(_cogRectAffineList[leadIndex]);
                display.SetInteractiveGraphics("lead", collect);
            }

            UpdateROIDataGridView(group.AkkonROIList);
        }

        public void ReceiveClickEvent(string jogType, int jogScale, ROIType roiType)
        {
            if (jogType.Contains("Skew"))
                SkewMode(jogType, jogScale);
            else if (jogType.Contains("Move"))
                MoveMode(jogType, jogScale);
            else if (jogType.Contains("Zoom"))
                SizeMode(jogType, jogScale);
            else { }
        }

        List<int> _selectedIndexList = null;
        private void SkewMode(string skewType, int jogScale)
        {
            if (CurrentTab == null)
                return;

            var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();

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

            int groupIndex = cbxGroupNumber.SelectedIndex;
            var group = CurrentTab.AkkonParam.GroupList[groupIndex];

            int selectedRowCount = dgvAkkonROI.Rows.GetRowCount(DataGridViewElementStates.Selected);
            _selectedIndexList = new List<int>();

            for (int selectedRowIndex = 0; selectedRowIndex < selectedRowCount; selectedRowIndex++)
                _selectedIndexList.Add(dgvAkkonROI.SelectedRows[selectedRowIndex].Index);

            foreach (int index in _selectedIndexList)
            {
                if (isSkewZero)
                    _cogRectAffineList[index].Skew = 0;
                else
                    _cogRectAffineList[index].Skew += skewUnit;

                group.AkkonROIList[index] = ConvertCogRectAffineToAkkonRoi(_cogRectAffineList[index]).DeepCopy();
            }

            IsSkewing = true;
            UpdateROIDataGridView(group.AkkonROIList, _selectedIndexList);
            IsSkewing = false;
            SetSelectAkkonROI();
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

            int groupIndex = cbxGroupNumber.SelectedIndex;
            var group = CurrentTab.AkkonParam.GroupList[groupIndex];

            int selectedRowCount = dgvAkkonROI.Rows.GetRowCount(DataGridViewElementStates.Selected);
            _selectedIndexList = new List<int>();

            for (int selectedRowIndex = 0; selectedRowIndex < selectedRowCount; selectedRowIndex++)
                _selectedIndexList.Add(dgvAkkonROI.SelectedRows[selectedRowIndex].Index);

            foreach (int index in _selectedIndexList)
            {
                _cogRectAffineList[index].CenterX += jogMoveX;
                _cogRectAffineList[index].CenterY += jogMoveY;

                group.AkkonROIList[index] = ConvertCogRectAffineToAkkonRoi(_cogRectAffineList[index]).DeepCopy();
            }

            IsMoving = true;
            UpdateROIDataGridView(group.AkkonROIList, _selectedIndexList);
            IsMoving = false;
            SetSelectAkkonROI();
            //DrawROI();
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

            int groupIndex = cbxGroupNumber.SelectedIndex;
            var group = CurrentTab.AkkonParam.GroupList[groupIndex];

            int selectedRowCount = dgvAkkonROI.Rows.GetRowCount(DataGridViewElementStates.Selected);
            _selectedIndexList = new List<int>();

            for (int selectedRowIndex = 0; selectedRowIndex < selectedRowCount; selectedRowIndex++)
                _selectedIndexList.Add(dgvAkkonROI.SelectedRows[selectedRowIndex].Index);

            foreach (int index in _selectedIndexList)
            {
                double minimumX = _cogRectAffineList[index].SideXLength + jogSizeX;
                double minimumY = _cogRectAffineList[index].SideYLength + jogSizeY;
                if (minimumX <= 0 || minimumY <= 0)
                    break;

                _cogRectAffineList[index].SideXLength += jogSizeX;
                _cogRectAffineList[index].SideYLength += jogSizeY;

                group.AkkonROIList[index] = ConvertCogRectAffineToAkkonRoi(_cogRectAffineList[index]).DeepCopy();
            }

            IsReScaling = true;
            UpdateROIDataGridView(group.AkkonROIList, _selectedIndexList);
            IsReScaling = false;
            SetSelectAkkonROI();
            //DrawROI();
        }

        public void SaveAkkonParam()
        {
            if (CurrentTab == null)
                return;

            int groupIndex = cbxGroupNumber.SelectedIndex;
            if (groupIndex < 0)
                return;
            if (CurrentTab.AkkonParam.GroupList.Count() <= 0)
                return;

            var group = CurrentTab.AkkonParam.GroupList[groupIndex];
            group.AkkonROIList.Clear();

            foreach (DataGridViewRow row in dgvAkkonROI.Rows)
            {
                List<Tuple<double, double>> parsedData = ParseDataGridViewData(row);

                AkkonROI akkonRoi = new AkkonROI();

                akkonRoi.LeftTopX = parsedData[0].Item1;
                akkonRoi.LeftTopY = parsedData[0].Item2;

                akkonRoi.RightTopX = parsedData[1].Item1;
                akkonRoi.RightTopY = parsedData[1].Item2;

                akkonRoi.LeftBottomX = parsedData[2].Item1;
                akkonRoi.LeftBottomY = parsedData[2].Item2;

                akkonRoi.RightBottomX = parsedData[3].Item1;
                akkonRoi.RightBottomY = parsedData[3].Item2;

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
            int groupIndex = cbxGroupNumber.SelectedIndex;

            if (groupIndex < 0)
                return;

            int threshold = KeyPadHelper.SetLabelIntegerData((Label)sender);
            CurrentTab.AkkonParam.GroupList[groupIndex].Threshold = threshold;
        }

        private void lblThresholdPreview_Click(object sender, EventArgs e)
        {
            if (lblThresholdPreview.BackColor == _selectedColor)
            {
                var teachingDisplay = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();
                if (teachingDisplay.GetImage() == null)
                    return;

                lblThresholdPreview.BackColor = _nonSelectedColor;

                if (_autoTeachingPolygon != null)
                {
                    _autoTeachingCollect.Clear();
                    _autoTeachingCollect.Add(_autoTeachingPolygon);

                    teachingDisplay.SetImage(TeachingUIManager.Instance().GetOriginCogImageBuffer(true));

                    teachingDisplay.DeleteInInteractiveGraphics("tool");
                    teachingDisplay.SetInteractiveGraphics("tool", _autoTeachingCollect);
                }
            }
            else
            {
                lblThresholdPreview.BackColor = _selectedColor;
                int threshold = Convert.ToInt32(lblAutoThresholdValue.Text);

                if (_autoTeachingCollect.Count > 0)
                {
                    var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();
                    if (display.GetImage() == null)
                        return;

                    ICogImage cogImage = display.GetImage();
                    var polygon = _autoTeachingCollect[0] as CogPolygon;
                    var cropCogImage = VisionProImageHelper.CropImage(cogImage, polygon);
                    
                    var targetMat = GetPrevImage(cogImage, polygon, threshold);

                    var cropImage = AlgorithmTool.ConvertCogImage(targetMat);
                    cropImage.PixelFromRootTransform = cropCogImage.PixelFromRootTransform;

                    var convertImage = VisionProImageHelper.CogCopyRegionTool(cogImage, cropImage, polygon, true);
                    TeachingUIManager.Instance().SetBinaryCogImageBuffer(convertImage as CogImage8Grey);
                }
            }
        }

        private Mat GetPrevImage(ICogImage cogImage, CogPolygon polygon, int threshold)
        {
            Rectangle rect = GetRectangle(polygon);
            CogRectangle cogRect = VisionProImageHelper.CreateRectangle(rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2), rect.Width, rect.Height);
            Mat sourceMat = TeachingUIManager.Instance().GetOriginMatImageBuffer(false);
            Mat cropMat = MatHelper.CropRoi(sourceMat, rect);
            CvInvoke.Threshold(cropMat, cropMat, threshold, 255, ThresholdType.Binary);

            Mat maskingMat = MakeMaskImage(new Size(cropMat.Width, cropMat.Height), cogRect, rect.X, rect.Y);
            Mat targetMat = new Mat();
            CvInvoke.BitwiseAnd(maskingMat, cropMat, targetMat);
            Mat filterdMat = GetFilterImage(targetMat);
            cropMat.Dispose();
            maskingMat.Dispose();
            targetMat.Dispose();

            return filterdMat;
        }

        private Rectangle GetRectangle(CogPolygon polygon)
        {
            List<PointF> boundingPoint = new List<PointF>();
            var vertices = polygon.GetVertices();

            boundingPoint.Add(new PointF((float)vertices[0, 0], (float)vertices[0, 1]));
            boundingPoint.Add(new PointF((float)vertices[1, 0], (float)vertices[1, 1]));
            boundingPoint.Add(new PointF((float)vertices[2, 0], (float)vertices[2, 1]));
            boundingPoint.Add(new PointF((float)vertices[3, 0], (float)vertices[3, 1]));

            float minX = boundingPoint.Select(point => point.X).Min();
            float maxX = boundingPoint.Select(point => point.X).Max();

            float minY = boundingPoint.Select(point => point.Y).Min();
            float maxY = boundingPoint.Select(point => point.Y).Max();

            float width = (maxX - minX);
            float height = (maxY - minY);

            return new Rectangle((int)minX, (int)minY, (int)width, (int)height);
        }

        private List<AkkonROI> GetAutoROIList(Mat mat)
        {
            List<AkkonROI> akkonRoiList = new List<AkkonROI>();

            int ignoreSize = 10000;
            var contours = new VectorOfVectorOfPoint();
            Mat hierarchy = new Mat();
            CvInvoke.FindContours(mat, contours, hierarchy, RetrType.External, ChainApproxMethod.ChainApproxSimple);

            List<VectorOfPoint> filteredContourList = new List<VectorOfPoint>();
            if (contours.Size != 0)
            {
                float[] hierarchyArray = MatHelper.MatToFloatArray(hierarchy);
                for (int idxContour = 0; idxContour < contours.Size; ++idxContour)
                {
                    if (hierarchyArray[idxContour * 4 + 3] > -0.5)
                        continue;

                    var contour = contours[idxContour];
                    double area = CvInvoke.ContourArea(contour);

                    if (area >= ignoreSize)
                    {
                        Console.WriteLine(area.ToString());
                        RotatedRect rotateRect = CvInvoke.MinAreaRect(contour);
                        var vertex = CvInvoke.BoxPoints(rotateRect);

                        List<Point> pointList = new List<Point>();
                        Point point1 = new Point((int)vertex[0].X, (int)vertex[0].Y);
                        Point point2 = new Point((int)vertex[1].X, (int)vertex[1].Y);
                        Point point3 = new Point((int)vertex[2].X, (int)vertex[2].Y);
                        Point point4 = new Point((int)vertex[3].X, (int)vertex[3].Y);

                        pointList.Add(point1);
                        pointList.Add(point2);
                        pointList.Add(point3);
                        pointList.Add(point4);

                        AkkonROI akkonRoi = new AkkonROI();
                        var ascending = pointList.OrderBy(p => p.Y).ToList();
                        if (ascending[0].X < ascending[1].X)
                        {
                            akkonRoi.LeftTopX = ascending[0].X;
                            akkonRoi.LeftTopY = ascending[0].Y;

                            akkonRoi.RightTopX = ascending[1].X;
                            akkonRoi.RightTopY = ascending[0].Y;
                        }
                        else
                        {
                            akkonRoi.LeftTopX = ascending[1].X;
                            akkonRoi.LeftTopY = ascending[1].Y;

                            akkonRoi.RightTopX = ascending[0].X;
                            akkonRoi.RightTopY = ascending[1].Y;
                        }

                        var descending = pointList.OrderByDescending(p => p.Y).ToList();
                        if (descending[0].X < descending[1].X)
                        {
                            akkonRoi.LeftBottomX = descending[0].X;
                            akkonRoi.LeftBottomY = descending[1].Y;

                            akkonRoi.RightBottomX = descending[1].X;
                            akkonRoi.RightBottomY = descending[1].Y;
                        }
                        else
                        {
                            akkonRoi.LeftBottomX = descending[1].X;
                            akkonRoi.LeftBottomY = descending[0].Y;

                            akkonRoi.RightBottomX = descending[0].X;
                            akkonRoi.RightBottomY = descending[0].Y;
                        }

                        akkonRoiList.Add(akkonRoi);
                    }
                }
            }

            return akkonRoiList;
        }

        private Mat GetFilterImage(Mat mat)
        {
            int ignoreSize = 10000;
            //int ignoreSize = 10;
            var contours = new VectorOfVectorOfPoint();
            Mat hierarchy = new Mat();
            CvInvoke.FindContours(mat, contours, hierarchy, RetrType.External, ChainApproxMethod.ChainApproxSimple);
            List<VectorOfPoint> filteredContourList = new List<VectorOfPoint>();
            if (contours.Size != 0)
            {
                float[] hierarchyArray = MatHelper.MatToFloatArray(hierarchy);
                for (int idxContour = 0; idxContour < contours.Size; ++idxContour)
                {
                    if (hierarchyArray[idxContour * 4 + 3] > -0.5)
                        continue;

                    var contour = contours[idxContour];
                    double area = CvInvoke.ContourArea(contour);

                    if (area >= ignoreSize)
                        filteredContourList.Add(contour);
                }
            }
            
            Mat filteredImage = new Mat(new Size(mat.Width, mat.Height), DepthType.Cv8U, 1);
            filteredImage.SetTo(new MCvScalar(0));

            IInputArrayOfArrays contoursArray = new VectorOfVectorOfPoint(filteredContourList.Select(vector => vector.ToArray()).ToArray());
            CvInvoke.DrawContours(filteredImage, contoursArray, -1, new MCvScalar(255), -1);
            //hierarchy.Dispose();

            return filteredImage;
        }

        private Mat MakeMaskImage(Size size, CogRectangle roi, int offsetX, int offsetY)
        {
            Mat maskImage = new Mat(size, DepthType.Cv8U, 1);
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            VectorOfPoint contour = new VectorOfPoint(new[]
            {
                new Point((int)roi.X - offsetX,  (int)roi.Y - offsetY),
                new Point((int)(roi.X + roi.Width - offsetX), (int)(roi.Y - offsetY)),
                new Point((int)(roi.X + roi.Width - offsetX), (int)(roi.Y + roi.Height - offsetY)),
                new Point((int)roi.X- offsetX, (int)(roi.Y + roi.Height - offsetY)),
            });
            contours.Push(contour);
            CvInvoke.DrawContours(maskImage, contours, -1, new MCvScalar(255), -1);
            return maskImage;
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
            var image = TeachingUIManager.Instance().GetOriginCogImageBuffer(true);
            if (image == null)
                return;

            var teachingDisplay = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();
            teachingDisplay.SetImage(image);

            if (ModelManager.Instance().CurrentModel == null)
                return;

            var roiList = GetAutoTeachingRoiList(image);
            //teachingDisplay.ClearGraphic();
            if (roiList == null)
                return;
            if (roiList.Count == 0)
                return;
            
            roiList = roiList.OrderBy(x => x.CornerXX).ToList();
            AddAutoTeachingAkkonRoi(roiList);

            UpdateROIDataGridView(GetGroup().AkkonROIList);
            DrawROI();
        }

        private List<CogRectangleAffine> GetAutoTeachingRoiList(ICogImage image)
        {
            List<CogRectangleAffine> affineRectList = new List<CogRectangleAffine>();

            int threshold = Convert.ToInt32(lblAutoThresholdValue.Text);
            var cropCogImage = VisionProImageHelper.CropImage(image, _autoTeachingPolygon);

            var cropTargetMat = GetPrevImage(image, _autoTeachingPolygon, threshold);

            Rectangle roi = GetRectangle(_autoTeachingPolygon);
            Mat targetImage = new Mat(new Size(image.Width, image.Height), DepthType.Cv8U, 1);
            Mat temp = new Mat(targetImage, roi);
            cropTargetMat.CopyTo(temp);
            Mat polygonMaskImage = GetPolygonMaskingImage(new Size(image.Width, image.Height));

            Mat targetMat = new Mat();
            CvInvoke.BitwiseAnd(polygonMaskImage, targetImage, targetMat);
            var roiList = GetAutoROIList(targetMat);
            if (roiList == null)
                return affineRectList;

            foreach (var akkonRoi in roiList)
                affineRectList.Add(ConvertAkkonRoiToCogRectAffine(akkonRoi));

            return affineRectList;
        }

        public List<PointF> GetPointListOfLine(PointF startPoint, PointF endPoint, float yOffset = 0)
        {
            List<PointF> rstPoints = new List<PointF>();

            float x, y, dxD, dyD, step;
            int cnt = 0;

            dxD = (endPoint.X - startPoint.X);
            dyD = (endPoint.Y - startPoint.Y);

            if (Math.Abs(dxD) >= Math.Abs(dyD))
                step = Math.Abs(dxD);
            else
                step = Math.Abs(dyD);

            dxD = dxD / step;
            dyD = dyD / step;
            x = startPoint.X;
            y = startPoint.Y;

            int prevX = -1;
            int prevY = -1;
            while (cnt <= step)
            {
                int calcX = (int)Math.Round(x, 0);
                int calcY = (int)Math.Round(y, 0);

                if (prevX != calcX)
                {
                    Point point = new Point(calcX, calcY);
                    rstPoints.Add(point);
                }

                x = x + dxD;
                y = y + dyD ;

                cnt += 1;

                prevX = calcX;
                prevY = calcY;
            }
            return rstPoints;
        }

        private Mat GetPolygonMaskingImage(Size size)
        {
            Rectangle roi = GetRectangle(_autoTeachingPolygon);
            Mat orgMaskImage = new Mat(size, DepthType.Cv8U, 1);

            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            var vertices = _autoTeachingPolygon.GetVertices();
            VectorOfPoint contour = new VectorOfPoint(new[]
                {
                    new Point((int)vertices[0, 0], (int)vertices[0, 1]),
                    new Point((int)vertices[1, 0], (int)vertices[1, 1]),
                    new Point((int)vertices[2, 0], (int)vertices[2, 1]),
                    new Point((int)vertices[3, 0], (int)vertices[3, 1]),
                });
            contours.Push(contour);
            CvInvoke.DrawContours(orgMaskImage, contours, -1, new MCvScalar(255), -1);

            return orgMaskImage;
        }

        private List<Point> GetRectPointList(Mat mat)
        {
            var contours = new VectorOfVectorOfPoint();
            Mat hierarchy = new Mat();
            CvInvoke.FindContours(mat, contours, hierarchy, RetrType.External, ChainApproxMethod.ChainApproxSimple);

            if (contours.Size != 0)
            {
                for (int idxContour = 0; idxContour < contours.Size; ++idxContour)
                {
                    //var contour = contours[idxContour];
                    //Point[] points = contour.ToArray();
                    //PointF[] pointfs = new PointF[points.Length];

                    //for (int i = 0; i < points.Length; i++)
                    //{
                    //    pointfs[i] = new PointF(points[i].X, points[i].Y);
                    //}
                    //RotatedRect rotatedRect = CvInvoke.MinAreaRect(pointfs);

                    //// RotatedRect 정보를 행렬로 변환
                    //Matrix<float> rotationMatrix = new Matrix<float>(2, 3);
                    //CvInvoke.GetRotationMatrix2D(rotatedRect.Center, rotatedRect.Angle, 1.0, rotationMatrix);

                    //// 변환된 행렬로 CogRectAffine 생성
                    //CogRectangleAffine cogRectAffine = new CogRectangleAffine();
                    //cogRectAffine.SetCenterLengthsRotationSkew
                }
            }

            return new List<Point>();
        }

        private void AddAutoTeachingAkkonRoi(List<CogRectangleAffine> roiList)
        {
            if (GetGroup() is AkkonGroup group)
            {
                //group.AkkonROIList.Clear();
                foreach (var roi in roiList)
                {
                    AkkonROI akkonRoi = new AkkonROI
                    {
                        LeftTopX = roi.CornerOriginX,
                        LeftTopY = roi.CornerOriginY,

                        RightTopX = roi.CornerXX,
                        RightTopY = roi.CornerXY,

                        LeftBottomX = roi.CornerYX,
                        LeftBottomY = roi.CornerYY,

                        RightBottomX = roi.CornerOppositeX,
                        RightBottomY = roi.CornerOppositeY,
                    };

                    group.AddROI(akkonRoi);
                }
            }
        }

        private AkkonGroup GetGroup()
        {
            if (cbxGroupNumber.SelectedIndex < 0 || CurrentTab == null)
                return null;

            var group = CurrentTab.AkkonParam.GroupList[cbxGroupNumber.SelectedIndex];

            return group;
        }

        public void RunForTest()
        {
            if (_cogRectAffineList.Count < 0)
                return;

            List<AkkonROI> roiList = new List<AkkonROI>();

            foreach (var item in _cogRectAffineList)
                roiList.Add(ConvertCogRectAffineToAkkonRoi(item));
            
            if (roiList.Count < 0)
                return;

            Mat matImage = TeachingUIManager.Instance().GetOriginMatImageBuffer(false);
            if (matImage == null)
                return;

            AkkonAlgoritmParam akkonAlgorithmParam = AkkonParamControl.GetCurrentParam();

            Judgement tabJudgement = Judgement.NG;
            AkkonAlgorithm.UseOverCount = AppsConfig.Instance().EnableTest2;
            var tabResult = AkkonAlgorithm.Run(matImage, roiList, akkonAlgorithmParam, Resolution_um, ref tabJudgement);

            UpdateResult(tabResult);

            Mat resultMat = GetDebugResultImage(matImage, tabResult, akkonAlgorithmParam);
            //Mat resizeMat = MatHelper.Resize(matImage, akkonAlgorithmParam.ImageFilterParam.ResizeRatio);
            //var akkonCogImage = ConvertCogGrayImage(resizeMat);
            //TeachingUIManager.Instance().SetAkkonCogImage(akkonCogImage);
            var resultCogImage = ConvertCogColorImage(resultMat);
            TeachingUIManager.Instance().SetResultCogImage(resultCogImage);

            //resizeMat.Dispose();
            resultMat.Dispose();
            ClearDisplay();
            Console.WriteLine("Completed.");

            lblOrginalImage.BackColor = _nonSelectedColor;
            lblResizeImage.BackColor = _nonSelectedColor;
            lblResultImage.BackColor = _selectedColor;
        }

        public Mat GetDebugResultImage(Mat mat, List<AkkonLeadResult> leadResultList, AkkonAlgoritmParam akkonParameters)
        {
            if (mat == null)
                return null;

            Mat resizeMat = new Mat();
            Size newSize = new Size((int)(mat.Width * akkonParameters.ImageFilterParam.ResizeRatio), (int)(mat.Height * akkonParameters.ImageFilterParam.ResizeRatio));
            CvInvoke.Resize(mat, resizeMat, newSize);
            Mat colorMat = new Mat();
            CvInvoke.CvtColor(resizeMat, colorMat, ColorConversion.Gray2Bgr);
            resizeMat.Dispose();

            float calcResolution = Resolution_um / CurrentTab.AkkonParam.AkkonAlgoritmParam.ImageFilterParam.ResizeRatio;
            MCvScalar redColor = new MCvScalar(50, 50, 230, 255);
            MCvScalar greenColor = new MCvScalar(50, 230, 50, 255);
            MCvScalar orangeColor = new MCvScalar(0, 165, 255); 

            foreach (var result in leadResultList)
            {
                var lead = result.Roi;
                var startPoint = new Point((int)result.Offset.ToWorldX, (int)result.Offset.ToWorldY);

                Point leftTop = new Point((int)lead.LeftTopX + startPoint.X, (int)lead.LeftTopY + startPoint.Y);
                Point leftBottom = new Point((int)lead.LeftBottomX + startPoint.X, (int)lead.LeftBottomY + startPoint.Y);
                Point rightTop = new Point((int)lead.RightTopX + startPoint.X, (int)lead.RightTopY + startPoint.Y);
                Point rightBottom = new Point((int)lead.RightBottomX + startPoint.X, (int)lead.RightBottomY + startPoint.Y);

                if (akkonParameters.DrawOption.ContainLeadROI)
                {
                    CvInvoke.Line(colorMat, leftTop, leftBottom, greenColor, 1);
                    CvInvoke.Line(colorMat, leftTop, rightTop, greenColor, 1);
                    CvInvoke.Line(colorMat, rightTop, rightBottom, greenColor, 1);
                    CvInvoke.Line(colorMat, rightBottom, leftBottom, greenColor, 1);
                }

                foreach (var blob in result.BlobList)
                {
                    int offsetX = (int)(result.Offset.ToWorldX + result.Offset.X);
                    int offsetY = (int)(result.Offset.ToWorldY + result.Offset.Y);

                    Rectangle rectRect = new Rectangle();
                    rectRect.X = blob.BoundingRect.X + offsetX;
                    rectRect.Y = blob.BoundingRect.Y + offsetY;
                    rectRect.Width = blob.BoundingRect.Width;
                    rectRect.Height = blob.BoundingRect.Height;

                    Point center = new Point(rectRect.X + (rectRect.Width / 2), rectRect.Y + (rectRect.Height / 2));
                    int radius = rectRect.Width > rectRect.Height ? rectRect.Width : rectRect.Height;

                    int size = blob.BoundingRect.Width * blob.BoundingRect.Height;
                    if (blob.IsAkkonShape)
                    {
                        CvInvoke.Circle(colorMat, center, radius / 2, greenColor, 1);
                    }
                    else
                    {
                        if (akkonParameters.DrawOption.ContainNG)
                        {
                            CvInvoke.Circle(colorMat, center, radius / 2, redColor, 1);
                        }
                    }

                    if (akkonParameters.DrawOption.ContainSize)
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
                    else if (akkonParameters.DrawOption.ContainArea)
                    {
                        int temp = (int)(radius / 2.0);
                        Point pt = new Point(center.X + temp, center.Y - temp);
                        double blobArea = blob.Area * calcResolution;

                        if (blob.IsAkkonShape)
                            CvInvoke.PutText(colorMat, blobArea.ToString("F1"), pt, FontFace.HersheySimplex, 0.3, greenColor);
                        else
                            CvInvoke.PutText(colorMat, blobArea.ToString("F1"), pt, FontFace.HersheySimplex, 0.3, redColor);
                    }
                    else if (akkonParameters.DrawOption.ContainStrength)
                    {
                        int temp = (int)(radius / 2.0);
                        Point pt = new Point(center.X + temp, center.Y - temp);
                        string strength = blob.Strength.ToString("F1");

                        if (blob.IsAkkonShape)
                            CvInvoke.PutText(colorMat, strength, pt, FontFace.HersheySimplex, 0.3, greenColor);
                        else
                            CvInvoke.PutText(colorMat, strength, pt, FontFace.HersheySimplex, 0.3, redColor);
                    }

                    if (blob.IsAkkonShape == false)
                    {
                        double strengthValue = Math.Abs(blob.Strength - akkonParameters.ShapeFilterParam.MinAkkonStrength);
                        if (strengthValue <= 1)
                        {
                            int temp = (int)(radius / 2.0);
                            Point pt = new Point(center.X + temp, center.Y - temp);
                            string strength = blob.Strength.ToString("F1");

                            CvInvoke.Circle(colorMat, center, radius / 2, orangeColor, 1);
                            CvInvoke.PutText(colorMat, strength, pt, FontFace.HersheySimplex, 0.3, orangeColor);
                        }
                    }
                }

                if (akkonParameters.DrawOption.ContainLeadCount)
                {
                    string leadIndexString = result.Roi.Index.ToString();
                    string akkonCountString = string.Format("[{0}]", result.AkkonCount);

                    Point centerPt = new Point((int)((leftBottom.X + rightBottom.X) / 2.0), leftBottom.Y);

                    int baseLine = 0;
                    Size textSize = CvInvoke.GetTextSize(leadIndexString, FontFace.HersheyComplex, 0.3, 1, ref baseLine);
                    int textX = centerPt.X - (textSize.Width / 2);
                    int textY = centerPt.Y + (baseLine / 2);
                    CvInvoke.PutText(colorMat, leadIndexString, new Point(textX, textY + 30), FontFace.HersheyComplex, 0.25, new MCvScalar(50, 230, 50, 255));

                    textSize = CvInvoke.GetTextSize(akkonCountString, FontFace.HersheyComplex, 0.3, 1, ref baseLine);
                    textX = centerPt.X - (textSize.Width / 2);
                    textY = centerPt.Y + (baseLine / 2);
                    CvInvoke.PutText(colorMat, akkonCountString, new Point(textX, textY + 60), FontFace.HersheyComplex, 0.25, new MCvScalar(50, 230, 50, 255));
                }
            }
            return colorMat;
        }

        public PointF[] GetCalcPoint(BlobPos blob, int offsetX, int offsetY)
        {
            PointF[] newPoints = new PointF[blob.Points.Count()];

            for (int i = 0; i < newPoints.Count(); i++)
            {
                newPoints[i].X = blob.Points[i].X + offsetX;
                newPoints[i].Y = blob.Points[i].Y + offsetY;
            }

            return newPoints;
        }

        public Point[] GetCalcPoint2(BlobPos blob, int offsetX, int offsetY)
        {
            Point[] newPoints = new Point[blob.Points.Count()];

            for (int i = 0; i < newPoints.Count(); i++)
            {
                newPoints[i].X = blob.Points[i].X + offsetX;
                newPoints[i].Y = blob.Points[i].Y + offsetY;
            }

            return newPoints;
        }

        public Mat GetResultImage(Mat mat, List<AkkonLeadResult> leadResultList, AkkonAlgoritmParam AkkonParameters, ref List<CogRectangleAffine> akkonNGAffineList)
        {
            if (mat == null)
                return null;

            Mat resizeMat = new Mat();
            Size newSize = new Size((int)(mat.Width * AkkonParameters.ImageFilterParam.ResizeRatio), (int)(mat.Height * AkkonParameters.ImageFilterParam.ResizeRatio));
            CvInvoke.Resize(mat, resizeMat, newSize);
            Mat colorMat = new Mat();
            CvInvoke.CvtColor(resizeMat, colorMat, ColorConversion.Gray2Bgr);
            resizeMat.Dispose();

            foreach (var result in leadResultList)
            {
                var lead = result.Roi;
                var startPoint = new Point((int)result.Offset.ToWorldX, (int)result.Offset.ToWorldY);

                MCvScalar redColor = new MCvScalar(50, 50, 230, 255);
                MCvScalar greenColor = new MCvScalar(50, 230, 50, 255);

                foreach (var blob in result.BlobList)
                {
                    Rectangle rectRect = new Rectangle();
                    rectRect.X = (int)(blob.BoundingRect.X + result.Offset.ToWorldX + result.Offset.X);
                    rectRect.Y = (int)(blob.BoundingRect.Y + result.Offset.ToWorldY + result.Offset.Y);
                    rectRect.Width = blob.BoundingRect.Width;
                    rectRect.Height = blob.BoundingRect.Height;

                    Point center = new Point(rectRect.X + (rectRect.Width / 2), rectRect.Y + (rectRect.Height / 2));
                    int radius = rectRect.Width > rectRect.Height ? rectRect.Width : rectRect.Height;

                    int size = blob.BoundingRect.Width * blob.BoundingRect.Height;
                    if (blob.IsAkkonShape)
                        CvInvoke.Circle(colorMat, center, radius / 2, greenColor, 1);
                }

                string leadIndexString = result.Roi.Index.ToString();
                string akkonCountString = string.Format("[{0}]", result.AkkonCount);

                Point leftTop = new Point((int)lead.LeftTopX + startPoint.X, (int)lead.LeftTopY + startPoint.Y);
                Point leftBottom = new Point((int)lead.LeftBottomX + startPoint.X, (int)lead.LeftBottomY + startPoint.Y);
                Point rightTop = new Point((int)lead.RightTopX + startPoint.X, (int)lead.RightTopY + startPoint.Y);
                Point rightBottom = new Point((int)lead.RightBottomX + startPoint.X, (int)lead.RightBottomY + startPoint.Y);

                Point centerPt = new Point((int)((leftBottom.X + rightBottom.X) / 2.0), leftBottom.Y);

                int baseLine = 0;
                Size textSize = CvInvoke.GetTextSize(leadIndexString, FontFace.HersheyComplex, 0.3, 1, ref baseLine);
                int textX = centerPt.X - (textSize.Width / 2);
                int textY = centerPt.Y + (baseLine / 2);
                CvInvoke.PutText(colorMat, leadIndexString, new Point(textX, textY + 30), FontFace.HersheyComplex, 0.25, new MCvScalar(50, 230, 50, 255));

                textSize = CvInvoke.GetTextSize(akkonCountString, FontFace.HersheyComplex, 0.3, 1, ref baseLine);
                textX = centerPt.X - (textSize.Width / 2);
                textY = centerPt.Y + (baseLine / 2);
                CvInvoke.PutText(colorMat, akkonCountString, new Point(textX, textY + 60), FontFace.HersheyComplex, 0.25, new MCvScalar(50, 230, 50, 255));

                if (result.Judgement == Judgement.NG)
                {
                    CvInvoke.Line(colorMat, leftTop, leftBottom, redColor, 1);
                    CvInvoke.Line(colorMat, leftTop, rightTop, redColor, 1);
                    CvInvoke.Line(colorMat, rightTop, rightBottom, redColor, 1);
                    CvInvoke.Line(colorMat, rightBottom, leftBottom, redColor, 1);

                    var rect = VisionProShapeHelper.ConvertToCogRectAffine(leftTop, rightTop, leftBottom);
                    akkonNGAffineList.Add(rect);
                }
            }

            return colorMat;
        }

        private Rectangle GetContainInROI(List<AkkonROI> akkonROIs)
        {
            var leftTopX = akkonROIs.Select(x => x.LeftTopX).Min();
            var leftTopY = akkonROIs.Select(x => x.LeftTopY).Min();
            var leftBottomX = akkonROIs.Select(x => x.LeftBottomX).Min();
            var leftBottomY = akkonROIs.Select(x => x.LeftBottomY).Max();

            var rightTopX = akkonROIs.Select(x => x.RightTopX).Max();
            var rightTopY = akkonROIs.Select(x => x.RightTopY).Min();
            var rightBottomX = akkonROIs.Select(x => x.RightBottomX).Max();
            var rightBottomY = akkonROIs.Select(x => x.RightBottomY).Max();

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

        private void lblResizeImage_Click(object sender, EventArgs e)
        {
            SetResizeImageView();
        }

        public void SetOrginImageView()
        {
            lblOrginalImage.BackColor = _selectedColor;
            lblResizeImage.BackColor = _nonSelectedColor;
            lblResultImage.BackColor = _nonSelectedColor;

            var orgImage = TeachingUIManager.Instance().GetOriginCogImageBuffer(true);
            if (orgImage != null)
                TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay().SetImage(orgImage);
        }

        private void SetResultImageView()
        {
            lblOrginalImage.BackColor = _nonSelectedColor;
            lblResizeImage.BackColor = _nonSelectedColor;
            lblResultImage.BackColor = _selectedColor;

            var resultImage = TeachingUIManager.Instance().GetResultCogImage(false);
            if (resultImage != null)
                TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay().SetImage(resultImage);
        }

        private void SetResizeImageView()
        {
            lblOrginalImage.BackColor = _nonSelectedColor;
            lblResizeImage.BackColor = _selectedColor;
            lblResultImage.BackColor = _nonSelectedColor;

            var akkonImage = TeachingUIManager.Instance().GetAkkonCogImage(false);
            if (akkonImage != null)
                TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay().SetImage(akkonImage);
        }

        private void lblAdd_Click(object sender, EventArgs e)
        {
            if (CurrentTab.AkkonParam.GroupList.Count == 0)
                return;
            
            int groupIndex = cbxGroupNumber.SelectedIndex;
            var group = CurrentTab.AkkonParam.GroupList[groupIndex];

            if (dgvAkkonROI.SelectedRows.Count <= 0)
                return;

            var lastRoi = group.AkkonROIList.Last().DeepCopy();

            lastRoi.LeftTopX += group.Pitch;
            lastRoi.RightTopX += group.Pitch;
            lastRoi.LeftBottomX += group.Pitch;
            lastRoi.RightBottomX += group.Pitch;

            group.AddROI(lastRoi);

            group.Count = _cogRectAffineList.Count;
            lblLeadCountValue.Text = group.Count.ToString();

            CogRectangleAffine cogRect = ConvertAkkonRoiToCogRectAffine(lastRoi);
            cogRect.GraphicDOFEnable = CogRectangleAffineDOFConstants.Position | CogRectangleAffineDOFConstants.Size | CogRectangleAffineDOFConstants.Skew;

            UpdateROIDataGridView(group.AkkonROIList);
            DrawROI();
        }

        public ICogImage ConvertCogColorImage(Mat mat)
        {
            Mat matR = MatHelper.ColorChannelSeperate(mat, MatHelper.ColorChannel.R);
            Mat matG = MatHelper.ColorChannelSeperate(mat, MatHelper.ColorChannel.G);
            Mat matB = MatHelper.ColorChannelSeperate(mat, MatHelper.ColorChannel.B);

            byte[] dataR = new byte[matR.Width * matR.Height];
            Marshal.Copy(matR.DataPointer, dataR, 0, matR.Width * matR.Height);

            byte[] dataG = new byte[matG.Width * matG.Height];
            Marshal.Copy(matG.DataPointer, dataG, 0, matG.Width * matG.Height);

            byte[] dataB = new byte[matB.Width * matB.Height];
            Marshal.Copy(matB.DataPointer, dataB, 0, matB.Width * matB.Height);

            var cogImage = VisionProImageHelper.CovertImage(dataR, dataG, dataB, matB.Width, matB.Height);

            matR.Dispose();
            matG.Dispose();
            matB.Dispose();

            return cogImage;
        }

        private void ckbDrawIndex_CheckedChanged(object sender, EventArgs e)
        {
            DrawROI();
        }

        private void lblTest_Click(object sender, EventArgs e)
        {
            RunForTest();
        }

        private List<AkkonROI> RenewalAkkonRoi(List<AkkonROI> roiList, CoordinateTransform panelCoordinate)
        {
            List<AkkonROI> newList = new List<AkkonROI>();

            foreach (var item in roiList)
            {
                PointF leftTop = item.GetLeftTopPoint();
                PointF rightTop = item.GetRightTopPoint();
                PointF leftBottom = item.GetLeftBottomPoint();
                PointF rightBottom = item.GetRightBottomPoint();

                var newLeftTop = panelCoordinate.GetCoordinate(leftTop);
                var newRightTop = panelCoordinate.GetCoordinate(rightTop);
                var newLeftBottom = panelCoordinate.GetCoordinate(leftBottom);
                var newRightBottom = panelCoordinate.GetCoordinate(rightBottom);

                AkkonROI akkonRoi = new AkkonROI();

                akkonRoi.SetLeftTopPoint(newLeftTop);
                akkonRoi.SetRightTopPoint(newRightTop);
                akkonRoi.SetLeftBottomPoint(newLeftBottom);
                akkonRoi.SetRightBottomPoint(newRightBottom);

                newList.Add(akkonRoi);
            }

            return newList;
        }

        private void SetFpcCoordinateData(CoordinateTransform fpc, TabInspResult tabInspResult, double leftOffsetX, double leftOffsetY, double rightOffsetX, double rightOffsetY)
        {
            var teachingLeftPoint = tabInspResult.MarkResult.FpcMark.FoundedMark.Left.MaxMatchPos.ReferencePos;
            PointF teachedLeftPoint = new PointF(teachingLeftPoint.X + (float)leftOffsetX, teachingLeftPoint.Y + (float)leftOffsetY);

            PointF searchedLeftPoint = tabInspResult.MarkResult.FpcMark.FoundedMark.Left.MaxMatchPos.FoundPos;

            var teachingRightPoint = tabInspResult.MarkResult.FpcMark.FoundedMark.Right.MaxMatchPos.ReferencePos;
            PointF teachedRightPoint = new PointF(teachingRightPoint.X + (float)rightOffsetX, teachingRightPoint.Y + (float)rightOffsetY);

            PointF searchedRightPoint = tabInspResult.MarkResult.FpcMark.FoundedMark.Right.MaxMatchPos.FoundPos;

            fpc.SetReferenceData(teachedLeftPoint, teachedRightPoint);
            fpc.SetTargetData(searchedLeftPoint, searchedRightPoint);
        }

        private void SetPanelCoordinateData(CoordinateTransform panel, TabInspResult tabInspResult, double leftOffsetX, double leftOffsetY, double rightOffsetX, double rightOffsetY)
        {
            var teachingLeftPoint = tabInspResult.MarkResult.PanelMark.FoundedMark.Left.MaxMatchPos.ReferencePos;
            PointF teachedLeftPoint = new PointF(teachingLeftPoint.X + (float)leftOffsetX, teachingLeftPoint.Y + (float)leftOffsetY);
            PointF searchedLeftPoint = tabInspResult.MarkResult.PanelMark.FoundedMark.Left.MaxMatchPos.FoundPos;

            var teachingRightPoint = tabInspResult.MarkResult.PanelMark.FoundedMark.Right.MaxMatchPos.ReferencePos;
            PointF teachedRightPoint = new PointF(teachingRightPoint.X + (float)rightOffsetX, teachingRightPoint.Y + (float)rightOffsetY);
            PointF searchedRightPoint = tabInspResult.MarkResult.PanelMark.FoundedMark.Right.MaxMatchPos.FoundPos;

            panel.SetReferenceData(teachedLeftPoint, teachedRightPoint);
            panel.SetTargetData(searchedLeftPoint, searchedRightPoint);
        }

        private void dgvAkkonROI_SelectionChanged(object sender, EventArgs e)
        {
            SetSelectAkkonROI();
        }

        public CogImage8Grey ConvertCogGrayImage(Mat mat)
        {
            if (mat == null)
                return null;

            int size = mat.Width * mat.Height * mat.NumberOfChannels;
            var cogImage = VisionProImageHelper.CovertImage(mat.DataPointer, mat.Width, mat.Height, mat.Step, ColorFormat.Gray) as CogImage8Grey;
            return cogImage;
        }
        #endregion
    }

    public static class ExtensionMethods
    {
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }
}
