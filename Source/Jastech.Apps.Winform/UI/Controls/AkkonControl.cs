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
using Jastech.Framework.Winform.Forms;
using Jastech.Apps.Structure;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Winform;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Imaging.Helper;
using Emgu.CV;
using Emgu.CV.CvEnum;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Jastech.Apps.Structure.Parameters;
using Jastech.Framework.Winform.Helper;
using static Jastech.Framework.Device.Motions.AxisMovingParam;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Algorithms.UI.Controls;
using Jastech.Framework.Algorithms.Akkon.Parameters;
using Jastech.Framework.Algorithms.Akkon;
using Jastech.Framework.Algorithms.Akkon.Results;
using Emgu.CV.Structure;

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

        private int _curSelectedGroup { get; set; } = -1;
        #endregion

        #region 속성
        public AlgorithmTool AlgorithmTool = new AlgorithmTool();

        public AkkonParamControl AkkonParamControl { get; private set; } = null;

        public Tab CurrentTab { get; private set; } = null;

        public AlgorithmTool Algorithm { get; private set; } = new AlgorithmTool();

        public AkkonAlgorithm AkkonAlgorithm { get; set; } = new AkkonAlgorithm();

        public bool UserMaker { get; set; } = false;

        public double CalcResolution { get; set; } = 0.0; // ex :  /camera.PixelResolution_mm(0.0035) / camera.LensScale(5) / 1000;
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
            if(this.InvokeRequired)
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
            {
                _curSelectedGroup = -1;
            }
            _prevTabName = CurrentTab.Name;
            _isLoading = false;
        }

        public delegate void UpdateDataDele();
        private void UpdateData()
        {
            if(this.InvokeRequired)
            {
                UpdateDataDele callback = UpdateData;
                BeginInvoke(callback);
                return;
            }
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
            lblLeadPitchValue.Text = group.Pitch.ToString("F2");
            lblROIWidthValue.Text = group.Width.ToString("F2");
            lblROIHeightValue.Text = group.Height.ToString("F2");

            //Auto Teaching
            lblAutoThresholdValue.Text = group.Threshold.ToString();
            lblAutoLeadPitch.Text = group.Pitch.ToString("F2");

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
            var display = TeachingUIManager.Instance().GetDisplay();
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
            _firstCogRectAffine = VisionProImageHelper.CreateRectangleAffine(centerX, centerY, GetCalcumToPixel(roiwidth), GetCalcumToPixel(roiheight), constants: constants);

            var teachingDisplay = TeachingUIManager.Instance().GetDisplay();
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

            _autoTeachingRect = VisionProImageHelper.CreateRectangle(centerX, centerY, display.ImageWidth(), display.ImageHeight());
            //_autoTeachingRect = CogImageHelper.CreateRectangle(centerX, centerY, 100, 100);
            _autoTeachingRect.DraggingStopped += AutoTeachingRect_DraggingStopped;

            var teachingDisplay = TeachingUIManager.Instance().GetDisplay();
            if (teachingDisplay.GetImage() == null)
                return;

            _autoTeachingCollect.Clear();
            _autoTeachingCollect.Add(_autoTeachingRect);
            teachingDisplay.DeleteInInteractiveGraphics("tool");
            teachingDisplay.SetInteractiveGraphics("tool", _autoTeachingCollect);
        }

        private void AutoTeachingRect_DraggingStopped(object sender, CogDraggingEventArgs e)
        {
            var display = TeachingUIManager.Instance().GetDisplay();
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
            var display = TeachingUIManager.Instance().GetDisplay();
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
            var display = TeachingUIManager.Instance().GetDisplay();

            display.ClearGraphic();

            if (display.GetImage() == null)
                return;

            if (CurrentTab == null)
                return;

            int groupIndex = _curSelectedGroup;// cbxGroupNumber.SelectedIndex;
            if (groupIndex < 0)
                return;

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

                if(isDrawLeadIndex)
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

            var teachingDisplay = TeachingUIManager.Instance().GetDisplay();
            if (teachingDisplay.GetImage() == null)
                return;
            teachingDisplay.SetInteractiveGraphics("tool", collect);
        }

        public void ClearDisplay()
        {
            var display = TeachingUIManager.Instance().GetDisplay();

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

        private void UpdateResult(List<AkkonBlob> blobList)
        {
            int no = 0;
            dgvJastechAkkonResult.Rows.Clear();
       
            foreach (var lead in blobList)
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
            AkkonROI firstRoi = GetFirstROI();

            if (CalcResolution == 0)
                return;

            for (int leadIndex = 0; leadIndex < leadCount; leadIndex++)
            {
                AkkonROI newRoi = firstRoi.DeepCopy();

                if (_cloneDirection == ROICloneDirection.Horizontal)
                {
                    newRoi.LeftTopX += (group.Pitch * leadIndex / CalcResolution);
                    newRoi.RightTopX += (group.Pitch * leadIndex / CalcResolution);
                    newRoi.LeftBottomX += (group.Pitch * leadIndex / CalcResolution);
                    newRoi.RightBottomX += (group.Pitch * leadIndex / CalcResolution);
                }
                else
                {
                    newRoi.LeftTopY += (group.Pitch * leadIndex / CalcResolution);
                    newRoi.RightTopY += (group.Pitch * leadIndex / CalcResolution);
                    newRoi.LeftBottomY += (group.Pitch * leadIndex / CalcResolution);
                    newRoi.RightBottomY += (group.Pitch * leadIndex / CalcResolution);
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
                int gg = 0;
            }
            else
            {
                foreach (DataGridViewRow row in dgvAkkonROI.SelectedRows)
                {
                    group.DeleteROI(row.Index);
                    _cogRectAffineList.RemoveAt(row.Index);
                }
            }

            group.Count = _cogRectAffineList.Count;
            lblLeadCountValue.Text = group.Count.ToString();
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
            _selectedIndexList = new List<int>();

            for (int i = 0; i < dgvAkkonROI.Rows.Count; i++)
            {
                var row = dgvAkkonROI.Rows[i];
                if(row.Selected)
                {
                    _selectedIndexList.Add(i);
                    _cogRectAffineList[i].Color = CogColorConstants.DarkRed;
                }
                else
                {
                    _cogRectAffineList[row.Index].Color = CogColorConstants.Blue;

                }
            }
            //foreach (DataGridViewRow row in dgvAkkonROI.Rows)
            //{
            //    if(row.Selected)
            //    {
            //        _selectedIndexList.Add(index);
            //        _cogRectAffineList[index].Color = CogColorConstants.DarkRed;
            //    }
            //    else
            //    {
            //        _cogRectAffineList[row.Index].Color = CogColorConstants.Blue;
            //    }
            //    index++;
            //}
               

          

            CogGraphicInteractiveCollection collect = new CogGraphicInteractiveCollection();
            foreach (var item in _cogRectAffineList)
                collect.Add(item);

            var display = TeachingUIManager.Instance().GetDisplay();

            if(lblResultImage.BackColor == _selectedColor)
                SetOrginImageView();

            display.SetInteractiveGraphics("lead", collect);
        }

        private List<CogRectangleAffine> _cogRectAffineList = new List<CogRectangleAffine>();

        private CogRectangleAffine ConvertAkkonRoiToCogRectAffine(AkkonROI akkonRoi)
        {
            CogRectangleAffine cogRectAffine = new CogRectangleAffine();

            cogRectAffine.SetOriginCornerXCornerY(akkonRoi.LeftTopX, akkonRoi.LeftTopY, 
                                                    akkonRoi.RightTopX, akkonRoi.RightTopY, akkonRoi.LeftBottomX, akkonRoi.LeftBottomY);

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

            var display = TeachingUIManager.Instance().GetDisplay();

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

                //UpdateROIDataGridView(leadIndex, _cogRectAffineList[leadIndex]);
                //var group = CurrentTab.AkkonParam.GroupList[groupIndex];
                //group.AkkonROIList[selectedIndex] = ConvertCogRectAffineToAkkonRoi(_cogRectAffineList[selectedIndex]).DeepCopy();
                group.AkkonROIList.Add(ConvertCogRectAffineToAkkonRoi(_cogRectAffineList[leadIndex]).DeepCopy());

                CogGraphicInteractiveCollection collect = new CogGraphicInteractiveCollection();
                collect.Add(_cogRectAffineList[leadIndex]);
                display.SetInteractiveGraphics("lead", collect);
            }

            UpdateROIDataGridView(group.AkkonROIList);
        }

        public void ShowROIJog()
        {
            ROIJogControl roiJogForm = new ROIJogControl();
            roiJogForm.SetTeachingItem(TeachingItem.Akkon);
            roiJogForm.SendEventHandler += new ROIJogControl.SendClickEventDelegate(ReceiveClickEvent);
            roiJogForm.Show();
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

        List<int> _selectedIndexList = null;
        private void SkewMode(string skewType, int jogScale)
        {
            if (CurrentTab == null)
                return;

            var display = TeachingUIManager.Instance().GetDisplay();

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

            UpdateROIDataGridView(group.AkkonROIList, _selectedIndexList);
            DrawROI();

            // 원본
            //if (dgvAkkonROI.CurrentCell == null)
            //{
            //    if (isSkewZero)
            //        _firstCogRectAffine.Skew = 0;
            //    else
            //        _firstCogRectAffine.Skew += skewUnit;
            //}
            //else
            //{
            //    int selectedIndex = dgvAkkonROI.CurrentCell.RowIndex;
            //    if (isSkewZero)
            //        _cogRectAffineList[selectedIndex].Skew = 0;
            //    else
            //        _cogRectAffineList[selectedIndex].Skew += skewUnit;

            //    UpdateROIDataGridView(selectedIndex, _cogRectAffineList[selectedIndex]);

            //    int groupIndex = cbxGroupNumber.SelectedIndex;
            //    var group = CurrentTab.AkkonParam.GroupList[groupIndex];
            //    group.AkkonROIList[selectedIndex] = ConvertCogRectAffineToAkkonRoi(_cogRectAffineList[selectedIndex]).DeepCopy();
            //    DrawROI();
            //    SetSelectAkkonROI(selectedIndex);
            //}
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

            UpdateROIDataGridView(group.AkkonROIList, _selectedIndexList);
            DrawROI();

            // 원본
            //if (dgvAkkonROI.CurrentCell == null)
            //{
            //    _firstCogRectAffine.CenterX += jogMoveX;
            //    _firstCogRectAffine.CenterY += jogMoveY;
            //}
            //else
            //{
            //    int selectedIndex = dgvAkkonROI.CurrentCell.RowIndex;

            //    _cogRectAffineList[selectedIndex].CenterX += jogMoveX;
            //    _cogRectAffineList[selectedIndex].CenterY += jogMoveY;

            //    UpdateROIDataGridView(selectedIndex, _cogRectAffineList[selectedIndex]);

            //    int groupIndex = cbxGroupNumber.SelectedIndex;
            //    var group = CurrentTab.AkkonParam.GroupList[groupIndex];
            //    group.AkkonROIList[selectedIndex] = ConvertCogRectAffineToAkkonRoi(_cogRectAffineList[selectedIndex]).DeepCopy();
            //    DrawROI();
            //    SetSelectAkkonROI(selectedIndex);
            //}
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

            UpdateROIDataGridView(group.AkkonROIList, _selectedIndexList);
            DrawROI();

            // 원본
            //if (dgvAkkonROI.CurrentCell == null)
            //{
            //    _firstCogRectAffine.SideXLength += jogSizeX;
            //    _firstCogRectAffine.SideYLength += jogSizeY;
            //}
            //else
            //{
            //    int selectedIndex = dgvAkkonROI.CurrentCell.RowIndex;

            //    double minimumX = _cogRectAffineList[selectedIndex].SideXLength + jogSizeX;
            //    double minimumY = _cogRectAffineList[selectedIndex].SideYLength + jogSizeY;
            //    if (minimumX <= 0 || minimumY <= 0)
            //        return;

            //    _cogRectAffineList[selectedIndex].SideXLength += jogSizeX;
            //    _cogRectAffineList[selectedIndex].SideYLength += jogSizeY;

            //    UpdateROIDataGridView(selectedIndex, _cogRectAffineList[selectedIndex]);

            //    int groupIndex = cbxGroupNumber.SelectedIndex;
            //    var group = CurrentTab.AkkonParam.GroupList[groupIndex];
            //    group.AkkonROIList[selectedIndex] = ConvertCogRectAffineToAkkonRoi(_cogRectAffineList[selectedIndex]).DeepCopy();
            //    DrawROI();
            //    SetSelectAkkonROI(selectedIndex);
            //}
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
                var teachingDisplay = TeachingUIManager.Instance().GetDisplay();
                if (teachingDisplay.GetImage() == null)
                    return;

                lblThresholdPreview.BackColor = _nonSelectedColor;

                _autoTeachingCollect.Clear();
                _autoTeachingCollect.Add(_autoTeachingRect);

                teachingDisplay.SetImage(TeachingUIManager.Instance().GetOriginCogImageBuffer(true));

                teachingDisplay.DeleteInInteractiveGraphics("tool");
                teachingDisplay.SetInteractiveGraphics("tool", _autoTeachingCollect);
            }
            else
            {
                lblThresholdPreview.BackColor = _selectedColor;
                int threshold = Convert.ToInt32(lblAutoThresholdValue.Text);

                if (_autoTeachingCollect.Count > 0)
                {
                    var display = TeachingUIManager.Instance().GetDisplay();
                    if (display.GetImage() == null)
                        return;

                    ICogImage cogImage = display.GetImage();
                    var roi = _autoTeachingCollect[0] as CogRectangle;

                    var cropImage = VisionProImageHelper.CropImage(cogImage, roi);
                    var binaryImage = VisionProImageHelper.Threshold(cropImage as CogImage8Grey, threshold, 255);
                    var convertImage = VisionProImageHelper.CogCopyRegionTool(cogImage, binaryImage, roi, true);
                    TeachingUIManager.Instance().SetBinaryCogImageBuffer(convertImage as CogImage8Grey);
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
            var image = TeachingUIManager.Instance().GetOriginCogImageBuffer(true);
            if (image == null)
                return;

            var teachingDisplay = TeachingUIManager.Instance().GetDisplay();
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
            var cropImage = VisionProImageHelper.CropImage(image, _autoTeachingRect);
            var binaryImage = VisionProImageHelper.Threshold(cropImage as CogImage8Grey, threshold, 255, true);

            byte[] topDataArray = VisionProImageHelper.GetWidthDataArray(binaryImage, 0);
            byte[] bottomDataArray = VisionProImageHelper.GetWidthDataArray(binaryImage, cropImage.Height - 1);

            List<int> topEdgePointList = new List<int>();
            List<int> bottomEdgePointList = new List<int>();

            ImageHelper.GetEdgePoint(topDataArray, bottomDataArray, 0, (int)21/*CalcResolution*/, ref topEdgePointList, ref bottomEdgePointList);

            if (topEdgePointList.Count != bottomEdgePointList.Count)
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "The number of edge points found is different.";
                form.ShowDialog();
                return new List<CogRectangleAffine>();
            }

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

            var roiList = VisionProImageHelper.CreateRectangleAffine(topPointList, bottomPointList);
            if (roiList == null)
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "The top and bottom of the found edge points are different.";
                form.ShowDialog();
                return new List<CogRectangleAffine>();
            }
            return roiList;
        }

        private void AddAutoTeachingAkkonRoi(List<CogRectangleAffine> roiList)
        {
            if (GetGroup() is AkkonGroup group)
            {
                group.AkkonROIList.Clear();
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

        public void Inspection(bool isDebug = true)
        {
            int groupIndex = cbxGroupNumber.SelectedIndex;
            if (groupIndex < 0 || CurrentTab == null)
                return;
     
            Mat matImage = TeachingUIManager.Instance().GetOriginMatImageBuffer(false);
            if (matImage == null)
                return;

            AppsInspModel appsInspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            var akkonParam = CurrentTab.AkkonParam;
            int akkonThreadCount = AppsConfig.Instance().AkkonThreadCount;

            AkkonAlgoritmParam akkonAlgorithmParam = AkkonParamControl.GetCurrentParam();
            var roiList = CurrentTab.AkkonParam.GetAkkonROIList();
            var tabResult = AkkonAlgorithm.Run(matImage, roiList, akkonAlgorithmParam);
            AkkonResult akkonResult = new AkkonResult();

            akkonResult.StageNo = CurrentTab.StageIndex;
            akkonResult.TabNo = CurrentTab.Index;

            UpdateResult(tabResult);

            Mat resultMat = null;
            if (isDebug)
                resultMat = GetDebugResultImage(matImage, tabResult, akkonAlgorithmParam);
            else
                resultMat = GetResultImage(matImage, tabResult, akkonAlgorithmParam);

            var cogImage = ConvertCogColorImage(resultMat);
            TeachingUIManager.Instance().SetResultCogImage(cogImage);
            resultMat.Dispose();
            ClearDisplay();
            Console.WriteLine("Completed.");

            lblOrginalImage.BackColor = _nonSelectedColor;
            lblResultImage.BackColor = _selectedColor;
        }

        public Mat GetDebugResultImage(Mat mat, List<AkkonBlob> resultList, AkkonAlgoritmParam AkkonParameters)
        {
            if (mat == null)
                return null;

            Mat resizeMat = new Mat();
            Size newSize = new Size((int)(mat.Width * AkkonParameters.ImageFilterParam.ResizeRatio), (int)(mat.Height * AkkonParameters.ImageFilterParam.ResizeRatio));
            CvInvoke.Resize(mat, resizeMat, newSize);
            Mat colorMat = new Mat();
            CvInvoke.CvtColor(resizeMat, colorMat, ColorConversion.Gray2Bgr);
            resizeMat.Dispose();

            foreach (var result in resultList)
            {
                var lead = result.Lead;
                var startPoint = new Point((int)result.OffsetToWorldX, (int)result.OffsetToWorldY);

                Point leftTop = new Point((int)lead.LeftTopX + startPoint.X, (int)lead.LeftTopY + startPoint.Y);
                Point leftBottom = new Point((int)lead.LeftBottomX + startPoint.X, (int)lead.LeftBottomY + startPoint.Y);
                Point rightTop = new Point((int)lead.RightTopX + startPoint.X, (int)lead.RightTopY + startPoint.Y);
                Point rightBottom = new Point((int)lead.RightBottomX + startPoint.X, (int)lead.RightBottomY + startPoint.Y);

                MCvScalar redColor = new MCvScalar(50, 50, 230, 255);
                MCvScalar greenColor = new MCvScalar(50, 230, 50, 255);

                if (AkkonParameters.DrawOption.ContainLeadROI)
                {
                    CvInvoke.Line(colorMat, leftTop, leftBottom, greenColor, 1);
                    CvInvoke.Line(colorMat, leftTop, rightTop, greenColor, 1);
                    CvInvoke.Line(colorMat, rightTop, rightBottom, greenColor, 1);
                    CvInvoke.Line(colorMat, rightBottom, leftBottom, greenColor, 1);
                }
            
                foreach (var blob in result.BlobList)
                {
                    Rectangle rectRect = new Rectangle();
                    rectRect.X = (int)(blob.BoundingRect.X + result.OffsetToWorldX + result.LeadOffsetX);
                    rectRect.Y = (int)(blob.BoundingRect.Y + result.OffsetToWorldY + result.LeadOffsetY);
                    rectRect.Width = blob.BoundingRect.Width;
                    rectRect.Height = blob.BoundingRect.Height;

                    Point center = new Point(rectRect.X + (rectRect.Width / 2), rectRect.Y + (rectRect.Height / 2));
                    int radius = rectRect.Width > rectRect.Height ? rectRect.Width : rectRect.Height;

                    int size = blob.BoundingRect.Width * blob.BoundingRect.Height;
                    if (blob.IsPass)
                    {
                        CvInvoke.Circle(colorMat, center, radius / 2, greenColor, 1);
                    }
                    else
                    {
                        if (AkkonParameters.DrawOption.ContainNG)
                            CvInvoke.Circle(colorMat, center, radius / 2, redColor, 1);
                    }

                    if (AkkonParameters.DrawOption.ContainArea)
                    {
                        if (blob.IsPass)
                        {
                            int temp = (int)(radius / 2.0);
                            Point pt = new Point(center.X + temp, center.Y - temp);
                            CvInvoke.PutText(colorMat, ((int)blob.Area).ToString(), pt, FontFace.HersheyScriptSimplex, 0.25, greenColor);
                        }
                    }
                    else
                    {
                        if (AkkonParameters.DrawOption.ContainStrength)
                        {
                            if (blob.IsPass)
                            {
                                int temp = (int)(radius / 2.0);
                                Point pt = new Point(center.X + temp, center.Y - temp);
                                CvInvoke.PutText(colorMat, ((int)blob.Strength).ToString(), pt, FontFace.HersheyScriptSimplex, 0.25, greenColor);
                            }
                        }
                    }
                }

                if (AkkonParameters.DrawOption.ContainLeadCount)
                {
                    string leadIndexString = result.LeadIndex.ToString();
                    string blobCountString = string.Format("[{0}]", result.DetectCount);

                    Point centerPt = new Point((int)((leftBottom.X + rightBottom.X) / 2.0), leftBottom.Y);

                    int baseLine = 0;
                    Size textSize = CvInvoke.GetTextSize(leadIndexString, FontFace.HersheyComplex, 0.3, 1, ref baseLine);
                    int textX = centerPt.X - (textSize.Width / 2);
                    int textY = centerPt.Y + (baseLine / 2);
                    CvInvoke.PutText(colorMat, leadIndexString, new Point(textX, textY + 30), FontFace.HersheyComplex, 0.25, new MCvScalar(50, 230, 50, 255));

                    textSize = CvInvoke.GetTextSize(blobCountString, FontFace.HersheyComplex, 0.3, 1, ref baseLine);
                    textX = centerPt.X - (textSize.Width / 2);
                    textY = centerPt.Y + (baseLine / 2);
                    CvInvoke.PutText(colorMat, blobCountString, new Point(textX, textY + 60), FontFace.HersheyComplex, 0.25, new MCvScalar(50, 230, 50, 255));
                }
            }
            return colorMat;
        }

        public Mat GetResultImage(Mat mat, List<AkkonBlob> resultList, AkkonAlgoritmParam AkkonParameters)
        {
            if (mat == null)
                return null;

            Mat resizeMat = new Mat();
            Size newSize = new Size((int)(mat.Width * AkkonParameters.ImageFilterParam.ResizeRatio), (int)(mat.Height * AkkonParameters.ImageFilterParam.ResizeRatio));
            CvInvoke.Resize(mat, resizeMat, newSize);
            Mat colorMat = new Mat();
            CvInvoke.CvtColor(resizeMat, colorMat, ColorConversion.Gray2Bgr);
            resizeMat.Dispose();

            foreach (var result in resultList)
            {
                var lead = result.Lead;
                var startPoint = new Point((int)result.OffsetToWorldX, (int)result.OffsetToWorldY);

            
                MCvScalar redColor = new MCvScalar(50, 50, 230, 255);
                MCvScalar greenColor = new MCvScalar(50, 230, 50, 255);

            
                foreach (var blob in result.BlobList)
                {
                    Rectangle rectRect = new Rectangle();
                    rectRect.X = (int)(blob.BoundingRect.X + result.OffsetToWorldX + result.LeadOffsetX);
                    rectRect.Y = (int)(blob.BoundingRect.Y + result.OffsetToWorldY + result.LeadOffsetY);
                    rectRect.Width = blob.BoundingRect.Width;
                    rectRect.Height = blob.BoundingRect.Height;

                    Point center = new Point(rectRect.X + (rectRect.Width / 2), rectRect.Y + (rectRect.Height / 2));
                    int radius = rectRect.Width > rectRect.Height ? rectRect.Width : rectRect.Height;

                    int size = blob.BoundingRect.Width * blob.BoundingRect.Height;
                    if (blob.IsPass)
                        CvInvoke.Circle(colorMat, center, radius / 2, greenColor, 1);
                }

                string leadIndexString = result.LeadIndex.ToString();
                string blobCountString = string.Format("[{0}]", result.DetectCount);

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

                textSize = CvInvoke.GetTextSize(blobCountString, FontFace.HersheyComplex, 0.3, 1, ref baseLine);
                textX = centerPt.X - (textSize.Width / 2);
                textY = centerPt.Y + (baseLine / 2);
                CvInvoke.PutText(colorMat, blobCountString, new Point(textX, textY + 60), FontFace.HersheyComplex, 0.25, new MCvScalar(50, 230, 50, 255));

                if(result.DetectCount >= AkkonParameters.JudgementParam.AkkonCount)
                {
                    CvInvoke.Line(colorMat, leftTop, leftBottom, redColor, 1);
                    CvInvoke.Line(colorMat, leftTop, rightTop, redColor, 1);
                    CvInvoke.Line(colorMat, rightTop, rightBottom, redColor, 1);
                    CvInvoke.Line(colorMat, rightBottom, leftBottom, redColor, 1);
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

        private void SetOrginImageView()
        {
            lblOrginalImage.BackColor = _selectedColor;
            lblResultImage.BackColor = _nonSelectedColor;

            var orgImage = TeachingUIManager.Instance().GetOriginCogImageBuffer(true);
            if(orgImage != null)
                TeachingUIManager.Instance().GetDisplay().SetImage(orgImage);
        }

        private void SetResultImageView()
        {
            lblOrginalImage.BackColor = _nonSelectedColor;
            lblResultImage.BackColor = _selectedColor;

            var orgImage = TeachingUIManager.Instance().GetResultCogImage(false);
            if(orgImage != null)
                TeachingUIManager.Instance().GetDisplay().SetImage(orgImage);
        }

        private double GetCalcumToPixel(double value)
        {
            return value * CalcResolution;
        }

        private void lblAdd_Click(object sender, EventArgs e)
        {
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

        private void lblROIHeight_Click(object sender, EventArgs e)
        {
            MoveROIXY(0, 0); // x, y : 330 ,200           (332, 102) * 5 = 1660, 510
        }

        private void MoveROIXY(int moveX, int moveY)
        {
            int groupIndex = cbxGroupNumber.SelectedIndex;
            var group = CurrentTab.AkkonParam.GroupList[groupIndex];

            List<AkkonROI> newRoiList = new List<AkkonROI>();

            foreach (var roi in group.AkkonROIList)
            {
                roi.LeftTopX += moveX;
                roi.LeftTopY += moveY;

                roi.RightTopX += moveX;
                roi.RightTopY += moveY;

                roi.LeftBottomX += moveX;
                roi.LeftBottomY += moveY;

                roi.RightBottomX += moveX;
                roi.RightBottomY += moveY;

                CogRectangleAffine cogRect = ConvertAkkonRoiToCogRectAffine(roi);
                cogRect.GraphicDOFEnable = CogRectangleAffineDOFConstants.Position | CogRectangleAffineDOFConstants.Size | CogRectangleAffineDOFConstants.Skew;

                newRoiList.Add(roi.DeepCopy());
            }

            UpdateROIDataGridView(newRoiList);

            group.AkkonROIList.Clear();

            foreach (var item in newRoiList)
                group.AddROI(item);

            DrawROI();
        }

        public ICogImage ConvertCogColorImage(Mat mat)
        {
            Mat matR = MatHelper.ColorChannelSprate(mat, MatHelper.ColorChannel.R);
            Mat matG = MatHelper.ColorChannelSprate(mat, MatHelper.ColorChannel.G);
            Mat matB = MatHelper.ColorChannelSprate(mat, MatHelper.ColorChannel.B);

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
            Inspection(true);
        }

        public void Run()
        {
            Inspection(false);
        }
    }
}
