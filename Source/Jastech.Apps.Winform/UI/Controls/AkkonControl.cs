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

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AkkonControl : UserControl
    {
        #region 필드
        private string _prevTabName { get; set; } = string.Empty;
        private Color _selectedColor = new Color();
        private Color _nonSelectedColor = new Color();
        private CogRectangleAffine _firstCogRectAffine { get; set; } = new CogRectangleAffine();
        private AkkonROI _firstAkkonRoi { get; set; } = new AkkonROI();
        #endregion

        private MacronAkkonParamControl MacronAkkonParamControl { get; set; } = new MacronAkkonParamControl();
        private List<Tab> TeachingTabList { get; set; } = new List<Tab>();
        //private List<VisionProCaliperParam> CaliperList { get; set; } = null;

        private AlgorithmTool Algorithm = new AlgorithmTool();

        #region 속성
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
            InitializeUI();
            AddControl();
        }

        private void AddControl()
        {
            MacronAkkonParamControl.Dock = DockStyle.Fill;
            MacronAkkonParamControl.GetOriginImageHandler += AkkonControl_GetOriginImageHandler;
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

        private ICogImage AkkonControl_GetOriginImageHandler()
        {
            return AppsTeachingUIManager.Instance().GetPrevImage();
        }

        public void SetParams(List<Tab> tabList)
        {
            if (tabList.Count <= 0)
                return;

            TeachingTabList = tabList;
            InitializeTabComboBox();
            InitializeGroupComboBox();

            string tabName = cmbTabList.SelectedItem as string;
            UpdateParam(tabName, 0);
        }

        private void InitializeTabComboBox()
        {
            cmbTabList.Items.Clear();

            foreach (var item in TeachingTabList)
                cmbTabList.Items.Add(item.Name);

            cmbTabList.SelectedIndex = 0;
        }

        private void cmbTabList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tabName = cmbTabList.SelectedItem as string;

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

            var tabList = TeachingTabList.Where(x => x.Name == tabName).First();
            var groupParam = tabList.GetAkkonGroup(groupIndex);

            if (groupParam == null)
                return;

            if (cmbGroupNumber.SelectedIndex == -1)
                return;

            lblGroupCountValue.Text = tabList.AkkonParam.GroupList.Count.ToString();
            cmbGroupNumber.SelectedIndex = groupIndex;

            lblLeadCountValue.Text = tabList.AkkonParam.GroupList[groupIndex].Count.ToString();
            lblLeadPitchValue.Text = tabList.AkkonParam.GroupList[groupIndex].Pitch.ToString();
            lblROIWidthValue.Text = tabList.AkkonParam.GroupList[groupIndex].Width.ToString();
            lblROIHeightValue.Text = tabList.AkkonParam.GroupList[groupIndex].Height.ToString();

            MacronAkkonParamControl.UpdateData(groupParam.MacronAkkonParam);

            DrawROI();
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
        
        private void SetFirstROI(AkkonROI roi)
        {
            _firstAkkonRoi = roi.DeepCopy();
        }

        private AkkonROI GetFirstROI()
        {
            return _firstAkkonRoi;
        }

        private void RegisterROI()
        {
            string tabName = cmbTabList.SelectedItem as string;
            var tabList = TeachingTabList.Where(x => x.Name == tabName).First();
            int groupIndex = cmbGroupNumber.SelectedIndex;

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

            string tabName = cmbTabList.SelectedItem as string;
            var tabList = TeachingTabList.Where(x => x.Name == tabName).First();
            int groupIndex = cmbGroupNumber.SelectedIndex;

            if (groupIndex < 0)
                return;

            var roiList = tabList.GetAkkonGroup(groupIndex).AkkonROIList;

            CogGraphicInteractiveCollection collect = new CogGraphicInteractiveCollection();

            int count = 0;
            foreach (var roi in roiList)
            {
                CogRectangleAffine rect = ConvertAkkonRoiToCogRectAffine(roi);
                collect.Add(rect);
                _cogRectAffineList.Add(rect);

                CogGraphicLabel cogLabel = new CogGraphicLabel();
                cogLabel.Color = CogColorConstants.Green;
                cogLabel.Font = new Font("맑은 고딕", 20, FontStyle.Bold);
                cogLabel.Text = count.ToString();
                cogLabel.X = (rect.CornerOppositeX + rect.CornerYX) / 2;
                cogLabel.Y = rect.CornerYY + 40;
                collect.Add(cogLabel);
                count++;
            }

            var teachingDisplay = AppsTeachingUIManager.Instance().GetDisplay();
            if (teachingDisplay.GetImage() == null)
                return;
            teachingDisplay.SetInteractiveGraphics("tool", collect);
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
            if (cmbTabList.SelectedIndex <= 0)
                return;

            cmbTabList.SelectedIndex -= 1;
        }

        private void lblNext_Click(object sender, EventArgs e)
        {
            int nextIndex = cmbTabList.SelectedIndex + 1;

            if (cmbTabList.Items.Count > nextIndex)
                cmbTabList.SelectedIndex = nextIndex;
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

            string tabName = cmbTabList.SelectedItem as string;
            TeachingTabList.Where(x => x.Name == tabName).First().AkkonParam.AdjustGroupCount(groupCount);
            InitializeGroupComboBox();
        }

        private void InitializeGroupComboBox()
        {
            cmbGroupNumber.Items.Clear();

            string tabName = cmbTabList.SelectedItem as string;
            var akkonParam = TeachingTabList.Where(x => x.Name == tabName).First().AkkonParam;

            if (akkonParam.GroupList.Count() <= 0)
                return;

            for (int groupIndex = 0; groupIndex < akkonParam.GroupList.Count(); groupIndex++)
                cmbGroupNumber.Items.Add(akkonParam.GroupList[groupIndex].Index.ToString());

            cmbGroupNumber.SelectedIndex = 0;
        }

        private void lblLeadCountValue_Click(object sender, EventArgs e)
        {
            int leadCount = SetLabelIntegerData(sender);

            int groupIndex = cmbGroupNumber.SelectedIndex;
            string tabName = cmbTabList.SelectedItem as string;

            TeachingTabList.Where(x => x.Name == tabName).First().AkkonParam.GroupList[groupIndex].Count = leadCount;
        }

        private void lblLeadPitchValue_Click(object sender, EventArgs e)
        {
            double leadPitch = SetLabelDoubleData(sender);

            string tabName = cmbTabList.SelectedItem as string;
            int groupIndex = cmbGroupNumber.SelectedIndex;

            TeachingTabList.Where(x => x.Name == tabName).First().AkkonParam.GroupList[groupIndex].Pitch = leadPitch;
        }

        private void lblROIWidthValue_Click(object sender, EventArgs e)
        {
            double leadWidth = SetLabelDoubleData(sender);

            string tabName = cmbTabList.SelectedItem as string;
            int groupIndex = cmbGroupNumber.SelectedIndex;

            TeachingTabList.Where(x => x.Name == tabName).First().AkkonParam.GroupList[groupIndex].Width = leadWidth;
        }

        private void lblROIHeightValue_Click(object sender, EventArgs e)
        {
            double leadHeight = SetLabelDoubleData(sender);

            string tabName = cmbTabList.SelectedItem as string;
            int groupIndex = cmbGroupNumber.SelectedIndex;

            TeachingTabList.Where(x => x.Name == tabName).First().AkkonParam.GroupList[groupIndex].Height = leadHeight;
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

        private ROICloneDirection _cloneDirection = ROICloneDirection.Horizontal;
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
            string tabName = cmbTabList.SelectedItem as string;
            var tabList = TeachingTabList.Where(x => x.Name == tabName).First();

            int groupIndex = cmbGroupNumber.SelectedIndex;
            var group = tabList.AkkonParam.GroupList[groupIndex];
            group.AkkonROIList.Clear();
            _cogRectAffineList.Clear();

            var leadCount = tabList.GetAkkonGroup(groupIndex).Count;
            var camera = DeviceManager.Instance().CameraHandler.Get(CameraName.LinscanMIL0.ToString());
            double calcResolution = /*camera.PixelResolution_mm*/0.0035 / /*camera.LensScale*/5 / 1000;

            AkkonROI firstRoi = GetFirstROI();
            
            for (int leadIndex = 0; leadIndex < leadCount; leadIndex++)
            {
                AkkonROI newRoi = firstRoi.DeepCopy();

                if (_cloneDirection == ROICloneDirection.Horizontal)
                {
                    newRoi.CornerOriginX += (group.Pitch * leadIndex/* / calcResolution*/);
                    newRoi.CornerXX += (group.Pitch * leadIndex/* / calcResolution*/);
                    newRoi.CornerYX += (group.Pitch * leadIndex/* / calcResolution*/);
                    newRoi.CornerOppositeX += (group.Pitch * leadIndex/* / calcResolution*/);
                }
                else
                {
                    newRoi.CornerOriginY += (group.Pitch * leadIndex/* / calcResolution*/);
                    newRoi.CornerXY += (group.Pitch * leadIndex/* / calcResolution*/);
                    newRoi.CornerYY += (group.Pitch * leadIndex/* / calcResolution*/);
                    newRoi.CornerOppositeY += (group.Pitch * leadIndex/* / calcResolution*/);
                }

                group.AddROI(newRoi);
                
                CogRectangleAffine cogRect = ConvertAkkonRoiToCogRectAffine(newRoi);
                cogRect.GraphicDOFEnable = CogRectangleAffineDOFConstants.Position | CogRectangleAffineDOFConstants.Size | CogRectangleAffineDOFConstants.Skew;
                //UpdateROIDataGridView(leadIndex, cogRect);
            }

            UpdateROIDataGridView(group.AkkonROIList);
            DrawROI();
        }

        private void cmbGroupNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedGroupIndex = cmbGroupNumber.SelectedIndex;

            string tabName = cmbTabList.SelectedItem as string;
            UpdateParam(tabName, selectedGroupIndex);
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
            string tabName = cmbTabList.SelectedItem as string;
            var tabList = TeachingTabList.Where(x => x.Name == tabName).First();
            int groupIndex = cmbGroupNumber.SelectedIndex;
            var group = tabList.AkkonParam.GroupList[groupIndex];

            foreach (DataGridViewRow row in dgvAkkonROI.SelectedRows)
            {
                int index = row.Index;
                dgvAkkonROI.Rows.RemoveAt(index);
                group.DeleteROI(index);
            }
        }

        private void dgvAkkonROI_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SetSelectAkkonROI(e.RowIndex);
        }

        private void SetSelectAkkonROI(int index)
        {
            string tabName = cmbTabList.SelectedItem as string;
            var tabList = TeachingTabList.Where(x => x.Name == tabName).First();

            int groupIndex = cmbGroupNumber.SelectedIndex;
            var group = tabList.AkkonParam.GroupList[groupIndex];

            foreach (DataGridViewRow row in dgvAkkonROI.Rows)
                _cogRectAffineList[row.Index].Color = CogColorConstants.Blue;

            _cogRectAffineList[index].Color = CogColorConstants.DarkRed;

            CogGraphicInteractiveCollection collect = new CogGraphicInteractiveCollection();
            foreach (var item in _cogRectAffineList)
                collect.Add(item);

            var display = AppsTeachingUIManager.Instance().GetDisplay();
            display.ClearGraphic();
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
            if (_cogRectAffineList.Count <= 0)
                return;

            var display = AppsTeachingUIManager.Instance().GetDisplay();

            string tabName = cmbTabList.SelectedItem as string;
            var tabList = TeachingTabList.Where(x => x.Name == tabName).First();

            int groupIndex = cmbGroupNumber.SelectedIndex;
            var group = tabList.AkkonParam.GroupList[groupIndex];
            var leadCount = tabList.GetAkkonGroup(groupIndex).Count;

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

        private void lblROIJog_Click(object sender, EventArgs e)
        {
            ROIJogControl roiJogForm = new ROIJogControl();
            roiJogForm.SendEventHandler += new ROIJogControl.SendClickEventDelegate(ReceiveClickEvent);
            roiJogForm.ShowDialog();
        }

        private void ReceiveClickEvent(string jogType, int jogScale)
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

            int cellCount = dgvAkkonROI.GetCellCount(DataGridViewElementStates.Selected);
            if (cellCount < 0)
                return;

            int selectedIndex = dgvAkkonROI.CurrentCell.RowIndex;
            if (isSkewZero)
                _cogRectAffineList[selectedIndex].Skew = 0;
            else
                _cogRectAffineList[selectedIndex].Skew += skewUnit;

            UpdateROIDataGridView(selectedIndex, _cogRectAffineList[selectedIndex]);

            CogGraphicInteractiveCollection collect = new CogGraphicInteractiveCollection();
            collect.Add(_cogRectAffineList[selectedIndex]);
            display.SetInteractiveGraphics("lead", collect);
        }

        private void MoveMode(string moveType, int jogScale)
        {
            var display = AppsTeachingUIManager.Instance().GetDisplay();

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

            int cellCount = dgvAkkonROI.GetCellCount(DataGridViewElementStates.Selected);
            if (cellCount < 0)
                return;

            int selectedIndex = dgvAkkonROI.CurrentCell.RowIndex;

            _cogRectAffineList[selectedIndex].CenterX += jogMoveX;
            _cogRectAffineList[selectedIndex].CenterY += jogMoveY;

            UpdateROIDataGridView(selectedIndex, _cogRectAffineList[selectedIndex]);

            CogGraphicInteractiveCollection collect = new CogGraphicInteractiveCollection();
            collect.Add(_cogRectAffineList[selectedIndex]);
            display.SetInteractiveGraphics("lead", collect);
        }

        private void SizeMode(string sizeType, int jogScale)
        {
            var display = AppsTeachingUIManager.Instance().GetDisplay();

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

            int cellCount = dgvAkkonROI.GetCellCount(DataGridViewElementStates.Selected);
            if (cellCount < 0)
                return;

            int selectedIndex = dgvAkkonROI.CurrentCell.RowIndex;

            _cogRectAffineList[selectedIndex].SideXLength += jogSizeX;
            _cogRectAffineList[selectedIndex].SideYLength += jogSizeY;

            UpdateROIDataGridView(selectedIndex, _cogRectAffineList[selectedIndex]);

            CogGraphicInteractiveCollection collect = new CogGraphicInteractiveCollection();
            collect.Add(_cogRectAffineList[selectedIndex]);
            display.SetInteractiveGraphics("lead", collect);
        }

        public void SaveAkkonParam()
        {
            string tabName = cmbTabList.SelectedItem as string;
            var tabList = TeachingTabList.Where(x => x.Name == tabName).First();

            int groupIndex = cmbGroupNumber.SelectedIndex;
            var group = tabList.AkkonParam.GroupList[groupIndex];
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
    }
}
