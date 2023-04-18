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

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AlignControl : UserControl
    {
        #region 필드
        private string _prevTabName { get; set; } = string.Empty;
        private ATTTabAlignName _alignName { get; set; } = ATTTabAlignName.LeftFPCX;
        private Color _selectedColor = new Color();
        private Color _nonSelectedColor = new Color();
        #endregion

        private CogCaliperParamControl CogCaliperParamControl { get; set; } = new CogCaliperParamControl();

        private List<Tab> TeachingTabList { get; set; } = new List<Tab>();

        private List<VisionProCaliperParam> CaliperList { get; set; } = null;

        private AlgorithmTool Algorithm = new AlgorithmTool();

        //private CogCaliper CogCaliperAlgorithm = new CogCaliper();

        #region 속성
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
            return AppsTeachingUIManager.Instance().GetPrevImage();
        }

        public void SetParams(List<Tab> tabList)
        {
            if (tabList.Count <= 0)
                return;

            TeachingTabList = tabList;
            InitializeComboBox();

            string tabName = cmbTabList.SelectedItem as string;
            UpdateParam(tabName);
        }

        private void InitializeLabelColor()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);
        }

        private void InitializeComboBox()
        {
            cmbTabList.Items.Clear();

            foreach (var item in TeachingTabList)
                cmbTabList.Items.Add(item.Name);

            cmbTabList.SelectedIndex = 0;
        }

        private void InitializeAlignName()
        {
            _alignName = ATTTabAlignName.LeftFPCX;
            UpdateSelectedAlignName(lblLeftFPCX);
        }

        private void UpdateParam(string tabName)
        {
            if (TeachingTabList.Count <= 0)
                return;

            var param = TeachingTabList.Where(x => x.Name == tabName).First().GetAlignParam(_alignName);
            CogCaliperParamControl.UpdateData(param.CaliperParams);
            lblLeadCount.Text = param.LeadCount.ToString();

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
            DrawROI();
        }

        private void SetNewROI(CogDisplayControl display)
        {
            ICogImage cogImage = display.GetImage();

            double centerX = display.ImageWidth() / 2.0 - display.GetPan().X;
            double centerY = display.ImageHeight() / 2.0 - display.GetPan().Y;

            CogRectangleAffine roi = CogImageHelper.CreateRectangleAffine(centerX, centerY, 100, 100);

            //if (roi.CenterX <= 70)
            //    roi.SetCenterLengthsRotationSkew(centerX, centerY, 500, 500, 0, 0);

            var currentParam = CogCaliperParamControl.GetCurrentParam();

            currentParam.SetRegion(roi);
        }

        public void DrawROI()
        {
                bool g = Focused;
            bool g1 = Enabled;
            var display = AppsTeachingUIManager.Instance().GetDisplay();

            display.ClearGraphic();

            if (display.GetImage() == null)
                return;

            CogCaliperCurrentRecordConstants constants = CogCaliperCurrentRecordConstants.All;
            var currentParam = CogCaliperParamControl.GetCurrentParam();

            display.SetInteractiveGraphics("tool", currentParam.CreateCurrentRecord(constants));
        }

        private void cmbTabList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tabName = cmbTabList.SelectedItem as string;

            if (_prevTabName == tabName)
                return;

            var display = AppsTeachingUIManager.Instance().GetDisplay();
            if (display == null)
                return;

            UpdateParam(tabName);
            display.ClearGraphic();

            DrawROI();
            _prevTabName = tabName;
        }

        private void cmbTabList_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawComboboxCenterAlign(sender, e);
        }

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

        private void lblPrevTab_Click(object sender, EventArgs e)
        {
            if (cmbTabList.SelectedIndex <= 0)
                return;

            cmbTabList.SelectedIndex -= 1;
        }

        private void lblNextTab_Click(object sender, EventArgs e)
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

            var currentParam = CogCaliperParamControl.GetCurrentParam();

            if (display == null || currentParam == null)
                return;

            string tabName = cmbTabList.SelectedItem as string;
            var param = TeachingTabList.Where(x => x.Name == tabName).First().GetAlignParam(_alignName);

            ICogImage cogImage = display.GetImage();

            CogAlignCaliperResult result = new CogAlignCaliperResult();

            if (_alignName.ToString().Contains("X"))
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
        #endregion

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
            _alignName = ATTTabAlignName.LeftFPCX;
            UpdateSelectedAlignName(sender);

            string tabName = cmbTabList.SelectedItem as string;
            UpdateParam(tabName);
        }

        private void lblLeftFPCY_Click(object sender, EventArgs e)
        {
            _alignName = ATTTabAlignName.LeftFPCY;
            UpdateSelectedAlignName(sender);

            string tabName = cmbTabList.SelectedItem as string;
            UpdateParam(tabName);
        }

        private void lblLeftPanelX_Click(object sender, EventArgs e)
        {
            _alignName = ATTTabAlignName.LeftPanelX;
            UpdateSelectedAlignName(sender);

            string tabName = cmbTabList.SelectedItem as string;
            UpdateParam(tabName);
        }

        private void lblLeftPanelY_Click(object sender, EventArgs e)
        {
            _alignName = ATTTabAlignName.LeftPanelY;
            UpdateSelectedAlignName(sender);

            string tabName = cmbTabList.SelectedItem as string;
            UpdateParam(tabName);
        }

        private void lblRightFPCX_Click(object sender, EventArgs e)
        {
            _alignName = ATTTabAlignName.RightFPCX;
            UpdateSelectedAlignName(sender);

            string tabName = cmbTabList.SelectedItem as string;
            UpdateParam(tabName);
        }

        private void lblRightFPCY_Click(object sender, EventArgs e)
        {
            _alignName = ATTTabAlignName.RightFPCY;
            UpdateSelectedAlignName(sender);

            string tabName = cmbTabList.SelectedItem as string;
            UpdateParam(tabName);
        }

        private void lblRightPanelX_Click(object sender, EventArgs e)
        {
            _alignName = ATTTabAlignName.RightPanelX;
            UpdateSelectedAlignName(sender);

            string tabName = cmbTabList.SelectedItem as string;
            UpdateParam(tabName);
        }

        private void lblRightPanelY_Click(object sender, EventArgs e)
        {
            _alignName = ATTTabAlignName.RightPanelY;
            UpdateSelectedAlignName(sender);

            string tabName = cmbTabList.SelectedItem as string;
            UpdateParam(tabName);
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

            //if (_alignName == ATTTabAlignName.LeftFPCY || _alignName == ATTTabAlignName.RightFPCY
            //    || _alignName == ATTTabAlignName.LeftPanelY || _alignName == ATTTabAlignName.RightPanelY)
            //{
            //    lblLeadCount.Enabled = false;
            //}
            //else
            //    lblLeadCount.Enabled = true;
        }

        private void lblLeadCount_Click(object sender, EventArgs e)
        {
            string tabName = cmbTabList.SelectedItem as string;
            
            int leadCount = SetLabelIntegerData(sender);

            TeachingTabList.Where(x => x.Name == tabName).First().GetAlignParam(_alignName).LeadCount = leadCount;
        }

        private int SetLabelIntegerData(object sender)
        {
            KeyPadForm keyPadForm = new KeyPadForm();
            keyPadForm.ShowDialog();

            int inputData = Convert.ToInt16(keyPadForm.PadValue);

            Label label = (Label)sender;
            label.Text = inputData.ToString();

            return inputData;
        }

        private void lblApply_Click(object sender, EventArgs e)
        {
            Apply();
        }

        private void Apply()
        {
            string tabName = cmbTabList.SelectedItem as string;
            int leadCount = TeachingTabList.Where(x => x.Name == tabName).First().GetAlignParam(_alignName).LeadCount;

            var currentParam = CogCaliperParamControl.GetCurrentParam();
            CogRectangleAffine rect = new CogRectangleAffine(currentParam.CaliperTool.Region);

            List<CogRectangleAffine> cropRectList = CogImageHelper.DivideRegion(rect, leadCount);

            var display = AppsTeachingUIManager.Instance().GetDisplay();

            display.ClearGraphic();

            if (display.GetImage() == null)
                return;

            foreach (var cogRect in cropRectList)
            {
                display.SetStaticGraphics("tool", cogRect);
            }
        }
    }
}
