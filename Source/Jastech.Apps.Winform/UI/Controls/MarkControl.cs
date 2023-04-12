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

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class MarkControl : UserControl
    {
        private Color _selectedColor = new Color();

        private Color _nonSelectedColor = new Color();

        private Material _curMaterial = Material.Fpc;

        private MarkName _curMarkName = MarkName.Main;

        private MarkDirecton _curDirection = MarkDirecton.Left;

        private List<Tab> TeachingTabList { get; set; } = new List<Tab>();

        private CogPatternMatchingParamControl ParamControl { get; set; } = new CogPatternMatchingParamControl();

        public MarkControl()
        {
            InitializeComponent();
        }

        private void MarkControl_Load(object sender, EventArgs e)
        {
            AddControl();
            InitializeLabelColor();

            lblFpc.BackColor = _selectedColor;
            lblLeftMain.BackColor = _selectedColor;
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

        private void InitializeComboBox()
        {
            cmbTabList.Items.Clear();

            foreach (var item in TeachingTabList)
                cmbTabList.Items.Add(item.Name);

            cmbTabList.SelectedIndex = 0;
        }

        private void UpdateParam(string tabName)
        {
            if (TeachingTabList.Count <= 0)
                return;
            //TeachingTabList.Where(x => x.Name == tabName).First().AlignParamList[0].
            //var param = PreAlignList.Where(x => x.Name == name).First().InspParam;
            //ParamControl.UpdateData(param);

            //var param = TeachingTabList.Where(x => x.Name == tabName).First().AlignParamList[(int)_alignName];
            ////var param = TeachingTabList.Where(x => x.Name == tabName).First().AlignParams.Where(x => x.Name == _alignName).First();
            //CogCaliperParamControl.UpdateData(param);
            //lblLeadCount.Text = param.LeadCount.ToString();

            //DrawROI();
        }

        private void lblFpc_Click(object sender, EventArgs e)
        {
            SelectMaterial(Material.Fpc);
        }

        private void lblPanel_Click(object sender, EventArgs e)
        {
            SelectMaterial(Material.Panel);
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
            if (TeachingTabList.Count <= 0)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirecton.Left;
            _curMarkName = MarkName.Main;
        }

        private void lblLeftSub1_Click(object sender, EventArgs e)
        {
            if (TeachingTabList.Count <= 0)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirecton.Left;
            _curMarkName = MarkName.Sub1;
        }

        private void lblLeftSub2_Click(object sender, EventArgs e)
        {
            if (TeachingTabList.Count <= 0)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirecton.Left;
            _curMarkName = MarkName.Sub2;
        }

        private void lblLeftSub3_Click(object sender, EventArgs e)
        {
            if (TeachingTabList.Count <= 0)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirecton.Left;
            _curMarkName = MarkName.Sub3;
        }

        private void lblLeftSub4_Click(object sender, EventArgs e)
        {
            if (TeachingTabList.Count <= 0)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirecton.Left;
            _curMarkName = MarkName.Sub4;
        }

        private void lblRightMain_Click(object sender, EventArgs e)
        {
            if (TeachingTabList.Count <= 0)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirecton.Right;
            _curMarkName = MarkName.Main;
        }

        private void lblRightSub1_Click(object sender, EventArgs e)
        {
            if (TeachingTabList.Count <= 0)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirecton.Right;
            _curMarkName = MarkName.Sub1;
        }

        private void lblRightSub2_Click(object sender, EventArgs e)
        {
            if (TeachingTabList.Count <= 0)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirecton.Right;
            _curMarkName = MarkName.Sub2;
        }

        private void lblRightSub3_Click(object sender, EventArgs e)
        {
            if (TeachingTabList.Count <= 0)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirecton.Right;
            _curMarkName = MarkName.Sub3;
        }

        private void lblRightSub4_Click(object sender, EventArgs e)
        {
            if (TeachingTabList.Count <= 0)
                return;

            UpdateBtnBackColor(sender);

            _curDirection = MarkDirecton.Right;
            _curMarkName = MarkName.Sub4;
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
    }
}
