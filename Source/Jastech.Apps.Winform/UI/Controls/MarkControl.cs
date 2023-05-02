using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Apps.Structure;
using Jastech.Framework.Winform.VisionPro.Controls;
using Cognex.VisionPro;
using Jastech.Apps.Structure.Data;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Apps.Structure.Parameters;
using Jastech.Framework.Imaging.VisionPro;
using Cognex.VisionPro.PMAlign;
using Jastech.Framework.Winform.Forms;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using Jastech.Apps.Structure.VisionTool;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class MarkControl : UserControl
    {
        private AlgorithmTool Algorithm = new AlgorithmTool();

        private string _curTabNo { get; set; } = "";

        private Color _selectedColor = new Color();

        private Color _nonSelectedColor = new Color();

        private Material _curMaterial = Material.Fpc;

        private MarkName _curMarkName = MarkName.Main;

        private MarkDirecton _curDirection = MarkDirecton.Left;

        private List<Tab> TeachingUnitList { get; set; } = new List<Tab>();

        private CogPatternMatchingParamControl ParamControl { get; set; } = new CogPatternMatchingParamControl();

        public MarkControl()
        {
            InitializeComponent();
        }

        private void MarkControl_Load(object sender, EventArgs e)
        {
            AddControl();
            InitializeLabelColor();

            UpdateParam(_curTabNo);
        }

        private void AddControl()
        {
            ParamControl.Dock = DockStyle.Fill;
            ParamControl.GetOriginImageHandler += PatternControl_GetOriginImageHandler;
            pnlParam.Controls.Add(ParamControl);
        }

        private ICogImage PatternControl_GetOriginImageHandler()
        {
            return AppsTeachingUIManager.Instance().GetDisplay().GetImage();
        }

        private void InitializeLabelColor()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            lblFpc.BackColor = _selectedColor;
            lblLeftMain.BackColor = _selectedColor;
        }

        public void SetParams(List<Tab> unitList)
        {
            if (unitList.Count <= 0)
                return;

            TeachingUnitList = unitList;
            InitializeComboBox();

            string name = cbxTabNumList.SelectedItem as string;
            UpdateParam(name);
        }

        private void InitializeComboBox()
        {
            cbxTabNumList.Items.Clear();

            foreach (var item in TeachingUnitList)
                cbxTabNumList.Items.Add(item.Name);

            cbxTabNumList.SelectedIndex = 0;
        }

        private void UpdateParam(string tabName)
        {
            if (TeachingUnitList.Count <= 0)
                return;

            var tab = TeachingUnitList.Where(x => x.Name == tabName).First();
            MarkParam currentParam = null;
            if (_curMaterial == Material.Fpc)
                currentParam = tab.GetFPCMark(_curDirection, _curMarkName);
            else
                currentParam = tab.GetPanelMark(_curDirection, _curMarkName);

            if(currentParam != null)
                ParamControl.UpdateData(currentParam.InspParam);

            DrawROI();
        }

        private void lblFpc_Click(object sender, EventArgs e)
        {
            SelectMaterial(Material.Fpc);
            UpdateParam(_curTabNo);
        }

        private void lblPanel_Click(object sender, EventArgs e)
        {
            SelectMaterial(Material.Panel);
            UpdateParam(_curTabNo);
        }

        private void SelectMaterial(Material material)
        {
            _curMaterial = material;

            if (material == Material.Fpc)
            {
                lblFpc.BackColor = _selectedColor;
                lblPanel.BackColor = _nonSelectedColor;
            }
            else
            {
                lblFpc.BackColor = _nonSelectedColor;
                lblPanel.BackColor = _selectedColor;
            }
        }

        private void lblLeftMain_Click(object sender, EventArgs e)
        {
            if (TeachingUnitList.Count <= 0)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirecton.Left;
            _curMarkName = MarkName.Main;
            UpdateParam(_curTabNo);
        }

        private void lblLeftSub1_Click(object sender, EventArgs e)
        {
            if (TeachingUnitList.Count <= 0)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirecton.Left;
            _curMarkName = MarkName.Sub1;
            UpdateParam(_curTabNo);
        }

        private void lblLeftSub2_Click(object sender, EventArgs e)
        {
            if (TeachingUnitList.Count <= 0)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirecton.Left;
            _curMarkName = MarkName.Sub2;
            UpdateParam(_curTabNo);
        }

        private void lblLeftSub3_Click(object sender, EventArgs e)
        {
            if (TeachingUnitList.Count <= 0)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirecton.Left;
            _curMarkName = MarkName.Sub3;
            UpdateParam(_curTabNo);
        }

        private void lblLeftSub4_Click(object sender, EventArgs e)
        {
            if (TeachingUnitList.Count <= 0)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirecton.Left;
            _curMarkName = MarkName.Sub4;
            UpdateParam(_curTabNo);
        }

        private void lblRightMain_Click(object sender, EventArgs e)
        {
            if (TeachingUnitList.Count <= 0)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirecton.Right;
            _curMarkName = MarkName.Main;
            UpdateParam(_curTabNo);
        }

        private void lblRightSub1_Click(object sender, EventArgs e)
        {
            if (TeachingUnitList.Count <= 0)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirecton.Right;
            _curMarkName = MarkName.Sub1;
            UpdateParam(_curTabNo);
        }

        private void lblRightSub2_Click(object sender, EventArgs e)
        {
            if (TeachingUnitList.Count <= 0)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirecton.Right;
            _curMarkName = MarkName.Sub2;
            UpdateParam(_curTabNo);
        }

        private void lblRightSub3_Click(object sender, EventArgs e)
        {
            if (TeachingUnitList.Count <= 0)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirecton.Right;
            _curMarkName = MarkName.Sub3;
            UpdateParam(_curTabNo);
        }

        private void lblRightSub4_Click(object sender, EventArgs e)
        {
            if (TeachingUnitList.Count <= 0)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirecton.Right;
            _curMarkName = MarkName.Sub4;
            UpdateParam(_curTabNo);
        }

        private void UpdateBtnBackColor(object sender)
        {
            lblLeftMain.BackColor = _nonSelectedColor;
            lblLeftSub1.BackColor = _nonSelectedColor;
            lblLeftSub2.BackColor = _nonSelectedColor;
            lblLeftSub3.BackColor = _nonSelectedColor;
            lblLeftSub4.BackColor = _nonSelectedColor;

            lblRightMain.BackColor = _nonSelectedColor;
            lblRightSub1.BackColor = _nonSelectedColor;
            lblRightSub2.BackColor = _nonSelectedColor;
            lblRightSub3.BackColor = _nonSelectedColor;
            lblRightSub4.BackColor = _nonSelectedColor;

            Label lbl = sender as Label;
            lbl.BackColor = _selectedColor;
        }

        private void cbxTabNumList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tabNo = cbxTabNumList.SelectedItem as string;

            if (_curTabNo == tabNo)
                return;

            UpdateParam(tabNo);

            _curTabNo = tabNo;
        }

        private void cbxTabNumList_DrawItem(object sender, DrawItemEventArgs e)
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

        private void lblPrev_Click(object sender, EventArgs e)
        {
            if (cbxTabNumList.SelectedIndex <= 0)
                return;

            cbxTabNumList.SelectedIndex -= 1;
        }

        private void lblNext_Click(object sender, EventArgs e)
        {
            int nextIndex = cbxTabNumList.SelectedIndex + 1;

            if (cbxTabNumList.Items.Count > nextIndex)
                cbxTabNumList.SelectedIndex = nextIndex;
        }

        private void lblAddROI_Click(object sender, EventArgs e)
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

            double centerX = display.ImageWidth() / 2.0;
            double centerY = display.ImageHeight() / 2.0;

            CogRectangle roi = CogImageHelper.CreateRectangle(centerX - display.GetPan().X, centerY - display.GetPan().Y, 100, 100);
            CogRectangle searchRoi = CogImageHelper.CreateRectangle(roi.CenterX, roi.CenterY, roi.Width * 2, roi.Height * 2);

            var currentParam = ParamControl.GetCurrentParam();

            currentParam.SetTrainRegion(roi);
            currentParam.SetSearchRegion(searchRoi);
        }

        public void DrawROI()
        {
            if (Enabled == false)
                return;
            
            var display = AppsTeachingUIManager.Instance().GetDisplay();
            if (display.GetImage() == null)
                return;
            display.ClearGraphic();

            CogPMAlignCurrentRecordConstants constants = CogPMAlignCurrentRecordConstants.InputImage | CogPMAlignCurrentRecordConstants.SearchRegion
                | CogPMAlignCurrentRecordConstants.TrainImage | CogPMAlignCurrentRecordConstants.TrainRegion | CogPMAlignCurrentRecordConstants.PatternOrigin;

            var currentParam = ParamControl.GetCurrentParam();

            display.SetInteractiveGraphics("tool", currentParam.CreateCurrentRecord(constants));
            
            var rect = currentParam.GetTrainRegion() as CogRectangle;
            if(rect != null)
                display.SetDisplayToCenter(new Point((int)rect.CenterX, (int)rect.CenterY));
        }

        private void lblInspection_Click(object sender, EventArgs e)
        {
            var display = AppsTeachingUIManager.Instance().GetDisplay();
            var currentParam = ParamControl.GetCurrentParam();

            if (display == null || currentParam == null)
                return;

            if (currentParam.IsTrained() == false)
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "Pattern is not trained.";
                form.ShowDialog();
                return;
            }

            ICogImage cogImage = display.GetImage();
            CogPatternMatchingResult result = Algorithm.RunPatternMatch(cogImage, currentParam);

            //UpdateGridResult(result);

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
        }
    }
}
