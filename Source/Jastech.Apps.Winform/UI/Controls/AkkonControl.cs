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

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AkkonControl : UserControl
    {
        #region 필드
        private string _prevTabName { get; set; } = string.Empty;
        private Color _selectedColor = new Color();
        private Color _nonSelectedColor = new Color();
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

        public void DrawROI()
        {
            var display = AppsTeachingUIManager.Instance().GetDisplay();

            display.ClearGraphic();

            if (display.GetImage() == null)
                return;

            //CogCaliperCurrentRecordConstants constants = CogCaliperCurrentRecordConstants.All;  //CogCaliperCurrentRecordConstants.InputImage | CogCaliperCurrentRecordConstants.Region;
            //var currentParam = CogCaliperParamControl.GetCurrentParam();

            //display.SetInteractiveGraphics("tool", currentParam.CaliperParams.CreateCurrentRecord(constants));
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

        private void lblAddROI_Click(object sender, EventArgs e)
        {

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

        private void lblLeadPitchValue_Click(object sender, EventArgs e)
        {
            double leadPitch = SetLabelDoubleData(sender);

            string tabName = cmbTabList.SelectedItem as string;
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

        private void lblCloneVertical_Click(object sender, EventArgs e)
        {
            lblCloneVertical.BackColor = _selectedColor;
            lblCloneHorizontal.BackColor = _nonSelectedColor;
        }

        private void lblCloneHorizontal_Click(object sender, EventArgs e)
        {
            lblCloneHorizontal.BackColor = _selectedColor;
            lblCloneVertical.BackColor = _nonSelectedColor;
        }

        private void lblCloneExecute_Click(object sender, EventArgs e)
        {

        }

        private void cmbGroupNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedGroupIndex = cmbGroupNumber.SelectedIndex;

            string tabName = cmbTabList.SelectedItem as string;
            UpdateParam(tabName, selectedGroupIndex);
        }
    }
}
