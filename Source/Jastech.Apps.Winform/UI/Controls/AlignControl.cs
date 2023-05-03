using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Framework.Winform.VisionPro.Controls;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms;
using Cognex.VisionPro;
using Jastech.Apps.Structure;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Framework.Imaging.VisionPro;
using Cognex.VisionPro.Caliper;
using Jastech.Framework.Winform.Forms;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.Parameters;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Winform.Helper;
using Jastech.Framework.Winform.Controls;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AlignControl : UserControl
    {
        #region 필드
        private string _prevTabName { get; set; } = string.Empty;

    
        private Color _selectedColor = new Color();

        private Color _nonSelectedColor = new Color();
        #endregion

        #region 속성
        private CogCaliperParamControl CogCaliperParamControl { get; set; } = new CogCaliperParamControl();

        private Tab CurrentTab { get; set; } = null;

        private ATTTabAlignName CurrentAlignName { get; set; } = ATTTabAlignName.LeftFPCX;

        private List<VisionProCaliperParam> CaliperList { get; set; } = null;

        private AlgorithmTool Algorithm = new AlgorithmTool();
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public AlignControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void AlignControl_Load(object sender, EventArgs e)
        {
            AddControl();
            InitializeUI();
        }

        private void AddControl()
        {
            CogCaliperParamControl.Dock = DockStyle.Fill;
            CogCaliperParamControl.GetOriginImageHandler += AlignControl_GetOriginImageHandler;
            pnlCaliperParam.Controls.Add(CogCaliperParamControl);
        }

        private void InitializeUI()
        {
            InitializeLabelColor();
            InitializeAlignName();
        }

        private ICogImage AlignControl_GetOriginImageHandler()
        {
            return AppsTeachingUIManager.Instance().GetOriginCogImageBuffer(true);
        }

        public void SetParams(Tab tab)
        {
            if(tab == null)
                return;

            CurrentTab = tab;
            UpdateParam(CurrentAlignName);
        }

        private void InitializeLabelColor()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);
        }

        private void InitializeAlignName()
        {
            UpdateSelectedAlignName(lblLeftFPCX);
            UpdateParam(ATTTabAlignName.LeftFPCX);
        }

        private void UpdateParam(ATTTabAlignName alignName)
        {
            CurrentAlignName = alignName;

            var alignParam = CurrentTab.GetAlignParam(alignName);
            CogCaliperParamControl.UpdateData(alignParam.CaliperParams);
            lblLeadCount.Text = alignParam.LeadCount.ToString();

            DrawROI();
        }

        private void chkUseTracking_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseTracking.Checked)
            {
                chkUseTracking.Text = "ROI Tracking : USE";
                chkUseTracking.BackColor = Color.DeepSkyBlue;
            }
            else
            {
                chkUseTracking.Text = "ROI Tracking : UNUSE";
                chkUseTracking.BackColor = Color.White;
            }
        }

        private void lblLeftFPCX_Click(object sender, EventArgs e)
        {
            UpdateSelectedAlignName(sender);
            UpdateParam(ATTTabAlignName.LeftFPCX);
        }

        private void lblLeftFPCY_Click(object sender, EventArgs e)
        {
            UpdateSelectedAlignName(sender);
            UpdateParam(ATTTabAlignName.LeftFPCY);
        }

        private void lblLeftPanelX_Click(object sender, EventArgs e)
        {
            UpdateSelectedAlignName(sender);
            UpdateParam(ATTTabAlignName.LeftPanelX);
        }

        private void lblLeftPanelY_Click(object sender, EventArgs e)
        {
            UpdateSelectedAlignName(sender);
            UpdateParam(ATTTabAlignName.LeftPanelY);
        }

        private void lblRightFPCX_Click(object sender, EventArgs e)
        {
            UpdateSelectedAlignName(sender);
            UpdateParam(ATTTabAlignName.RightFPCX);
        }

        private void lblRightFPCY_Click(object sender, EventArgs e)
        {
            UpdateSelectedAlignName(sender);
            UpdateParam(ATTTabAlignName.RightFPCY);
        }

        private void lblRightPanelX_Click(object sender, EventArgs e)
        {
            UpdateSelectedAlignName(sender);
            UpdateParam(ATTTabAlignName.RightPanelX);
        }

        private void lblRightPanelY_Click(object sender, EventArgs e)
        {
            UpdateSelectedAlignName(sender);
            UpdateParam(ATTTabAlignName.RightPanelY);
        }

        private void UpdateSelectedAlignName(object sender)
        {
            lblLeftFPCX.BackColor = _nonSelectedColor;
            lblLeftFPCY.BackColor = _nonSelectedColor;
            lblLeftPanelX.BackColor = _nonSelectedColor;
            lblLeftPanelY.BackColor = _nonSelectedColor;

            lblRightFPCX.BackColor = _nonSelectedColor;
            lblRightFPCY.BackColor = _nonSelectedColor;
            lblRightPanelX.BackColor = _nonSelectedColor;
            lblRightPanelY.BackColor = _nonSelectedColor;

            Label lbl = sender as Label;
            lbl.BackColor = _selectedColor;
        }

        private void lblLeadCount_Click(object sender, EventArgs e)
        {
            int leadCount = KeyPadHelper.SetLabelIntegerData((Label)sender);

            var alignParam = CurrentTab.GetAlignParam(CurrentAlignName);
            alignParam.LeadCount = leadCount;
        }

        private void lblApply_Click(object sender, EventArgs e)
        {
            Apply();
        }

        public void Apply()
        {
            var alignParam = CurrentTab.GetAlignParam(CurrentAlignName);

            int leadCount = alignParam.LeadCount;

            var currentParam = CogCaliperParamControl.GetCurrentParam();
            CogRectangleAffine rect = new CogRectangleAffine(currentParam.CaliperTool.Region);

            List<CogRectangleAffine> cropRectList = CogImageHelper.DivideRegion(rect, leadCount);

            var display = AppsTeachingUIManager.Instance().GetDisplay();

            display.ClearGraphic();

            if (display.GetImage() == null)
                return;

            foreach (var cogRect in cropRectList)
                display.SetStaticGraphics("tool", cogRect);
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

            double centerX = display.ImageWidth() / 2.0 - display.GetPan().X;
            double centerY = display.ImageHeight() / 2.0 - display.GetPan().Y;

            CogRectangleAffine roi = CogImageHelper.CreateRectangleAffine(centerX, centerY, 100, 100);

            var currentParam = CogCaliperParamControl.GetCurrentParam();

            currentParam.SetRegion(roi);
        }

        public void DrawROI()
        {
            var display = AppsTeachingUIManager.Instance().GetDisplay();

            display.ClearGraphic();

            if (display.GetImage() == null)
                return;

            CogCaliperCurrentRecordConstants constants = CogCaliperCurrentRecordConstants.All;
            var currentParam = CogCaliperParamControl.GetCurrentParam();

            display.SetInteractiveGraphics("tool", currentParam.CreateCurrentRecord(constants));
        }

        public void Inspection()
        {
            var display = AppsTeachingUIManager.Instance().GetDisplay();

            var currentParam = CogCaliperParamControl.GetCurrentParam();

            if (display == null || currentParam == null || CurrentTab == null)
                return;

            var param = CurrentTab.GetAlignParam(CurrentAlignName);

            ICogImage cogImage = display.GetImage();

            CogAlignCaliperResult result = new CogAlignCaliperResult();

            if (CurrentAlignName.ToString().Contains("X"))
                result = Algorithm.RunAlignX(cogImage, currentParam, param.LeadCount);
            else
                result = Algorithm.RunAlignY(cogImage, currentParam);

            if (result.Result == Result.Fail)
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "Caliper is Not Found.";
                form.ShowDialog();
            }
            else
            {
                display.ClearGraphic();
                display.UpdateResult(result);
            }
        }

        public void ShowROIJog()
        {
            ROIJogControl roiJogForm = new ROIJogControl();
            roiJogForm.SendEventHandler += new ROIJogControl.SendClickEventDelegate(ReceiveClickEvent);
            roiJogForm.ShowDialog();
        }

        private void ReceiveClickEvent(string jogType, int jogScale)
        {
            if (jogType.Contains("Skew")) { }
            //SkewMode(jogType, jogScale);
            else if (jogType.Contains("Move"))
                MoveMode(jogType, jogScale);
            else if (jogType.Contains("Zoom"))
                SizeMode(jogType, jogScale);
            else { }
        }

        private void MoveMode(string moveType, int jogScale)
        {
            if (CurrentTab == null)
                return;

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


            var currentParam = CogCaliperParamControl.GetCurrentParam();
            CogRectangleAffine roi = new CogRectangleAffine(currentParam.CaliperTool.Region);
            //roi.Interactive = true;

            roi.CenterX += jogMoveX;
            roi.CenterY += jogMoveY;

            currentParam.CaliperTool.Region = roi;
            DrawROI();

            //if (dgvAkkonROI.CurrentCell == null)
            //    return;

            //int selectedIndex = dgvAkkonROI.CurrentCell.RowIndex;

            //_cogRectAffineList[selectedIndex].CenterX += jogMoveX;
            //_cogRectAffineList[selectedIndex].CenterY += jogMoveY;

            //UpdateROIDataGridView(selectedIndex, _cogRectAffineList[selectedIndex]);

            //int groupIndex = cbxGroupNumber.SelectedIndex;
            //var group = CurrentTab.AkkonParam.GroupList[groupIndex];
            //group.AkkonROIList[selectedIndex] = ConvertCogRectAffineToAkkonRoi(_cogRectAffineList[selectedIndex]).DeepCopy();
            //DrawROI();
            //SetSelectAkkonROI(selectedIndex);
        }

        private void SizeMode(string sizeType, int jogScale)
        {
            if (CurrentTab == null)
                return;

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

            //if (dgvAkkonROI.CurrentCell == null)
            //    return;

            //int selectedIndex = dgvAkkonROI.CurrentCell.RowIndex;

            //double minimumX = _cogRectAffineList[selectedIndex].SideXLength + jogSizeX;
            //double minimumY = _cogRectAffineList[selectedIndex].SideYLength + jogSizeY;
            //if (minimumX <= 0 || minimumY <= 0)
            //    return;

            //_cogRectAffineList[selectedIndex].SideXLength += jogSizeX;
            //_cogRectAffineList[selectedIndex].SideYLength += jogSizeY;

            //UpdateROIDataGridView(selectedIndex, _cogRectAffineList[selectedIndex]);

            //int groupIndex = cbxGroupNumber.SelectedIndex;
            //var group = CurrentTab.AkkonParam.GroupList[groupIndex];
            //group.AkkonROIList[selectedIndex] = ConvertCogRectAffineToAkkonRoi(_cogRectAffineList[selectedIndex]).DeepCopy();
            //DrawROI();
            //SetSelectAkkonROI(selectedIndex);
        }

        #endregion
    }
}
