using Jastech.Apps.Structure.Data;
using Jastech.Framework.Config;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform.Forms;
using Jastech.Framework.Winform.Helper;
using System;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Forms
{
    public partial class ATTMaterialInfoForm : Form
    {
        #region 속성
        public MaterialInfo PrevMaterialInfo { get; set; } = null;

        public MaterialInfo NewMaterialInfo { get; set; } = null;
        #endregion

        #region 생성자
        public ATTMaterialInfoForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void ATTMaterialInfoForm_Load(object sender, EventArgs e)
        {
            NewMaterialInfo = PrevMaterialInfo?.Clone();
            if (NewMaterialInfo != null)
            {
                // Data
                txtPanelXSize.DataBindings.Add("Text", NewMaterialInfo, "PanelXSize_mm", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtMarkToMark.DataBindings.Add("Text", NewMaterialInfo, "MarkToMark_mm", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtPanelEdgeToFirst.DataBindings.Add("Text", NewMaterialInfo, "PanelEdgeToFirst_mm", true, DataSourceUpdateMode.OnPropertyChanged, "0");

                // Tab Width
                txtTab1Width.DataBindings.Add("Text", NewMaterialInfo.TabWidth_mm, "Tab0", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtTab2Width.DataBindings.Add("Text", NewMaterialInfo.TabWidth_mm, "Tab1", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtTab3Width.DataBindings.Add("Text", NewMaterialInfo.TabWidth_mm, "Tab2", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtTab4Width.DataBindings.Add("Text", NewMaterialInfo.TabWidth_mm, "Tab3", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtTab5Width.DataBindings.Add("Text", NewMaterialInfo.TabWidth_mm, "Tab4", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtTab6Width.DataBindings.Add("Text", NewMaterialInfo.TabWidth_mm, "Tab5", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtTab7Width.DataBindings.Add("Text", NewMaterialInfo.TabWidth_mm, "Tab6", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtTab8Width.DataBindings.Add("Text", NewMaterialInfo.TabWidth_mm, "Tab7", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtTab9Width.DataBindings.Add("Text", NewMaterialInfo.TabWidth_mm, "Tab8", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtTab10Width.DataBindings.Add("Text", NewMaterialInfo.TabWidth_mm, "Tab9", true, DataSourceUpdateMode.OnPropertyChanged, "0");

                // Tab To Tab Distance
                txtDistance1.DataBindings.Add("Text", NewMaterialInfo.TabToTabDistance_mm, "Tab0ToTab1", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtDistance2.DataBindings.Add("Text", NewMaterialInfo.TabToTabDistance_mm, "Tab1ToTab2", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtDistance3.DataBindings.Add("Text", NewMaterialInfo.TabToTabDistance_mm, "Tab2ToTab3", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtDistance4.DataBindings.Add("Text", NewMaterialInfo.TabToTabDistance_mm, "Tab3ToTab4", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtDistance5.DataBindings.Add("Text", NewMaterialInfo.TabToTabDistance_mm, "Tab4ToTab5", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtDistance6.DataBindings.Add("Text", NewMaterialInfo.TabToTabDistance_mm, "Tab5ToTab6", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtDistance7.DataBindings.Add("Text", NewMaterialInfo.TabToTabDistance_mm, "Tab6ToTab7", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtDistance8.DataBindings.Add("Text", NewMaterialInfo.TabToTabDistance_mm, "Tab7ToTab8", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtDistance9.DataBindings.Add("Text", NewMaterialInfo.TabToTabDistance_mm, "Tab8ToTab9", true, DataSourceUpdateMode.OnPropertyChanged, "0");

                // Left Offset
                txtLeftOffset1.DataBindings.Add("Text", NewMaterialInfo.LeftOffset, "Tab0", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtLeftOffset2.DataBindings.Add("Text", NewMaterialInfo.LeftOffset, "Tab1", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtLeftOffset3.DataBindings.Add("Text", NewMaterialInfo.LeftOffset, "Tab2", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtLeftOffset4.DataBindings.Add("Text", NewMaterialInfo.LeftOffset, "Tab3", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtLeftOffset5.DataBindings.Add("Text", NewMaterialInfo.LeftOffset, "Tab4", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtLeftOffset6.DataBindings.Add("Text", NewMaterialInfo.LeftOffset, "Tab5", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtLeftOffset7.DataBindings.Add("Text", NewMaterialInfo.LeftOffset, "Tab6", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtLeftOffset8.DataBindings.Add("Text", NewMaterialInfo.LeftOffset, "Tab7", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtLeftOffset9.DataBindings.Add("Text", NewMaterialInfo.LeftOffset, "Tab8", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtLeftOffset10.DataBindings.Add("Text", NewMaterialInfo.LeftOffset, "Tab9", true, DataSourceUpdateMode.OnPropertyChanged, "0");

                // Right Offset
                txtRightOffset1.DataBindings.Add("Text", NewMaterialInfo.RightOffset, "Tab0", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtRightOffset2.DataBindings.Add("Text", NewMaterialInfo.RightOffset, "Tab1", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtRightOffset3.DataBindings.Add("Text", NewMaterialInfo.RightOffset, "Tab2", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtRightOffset4.DataBindings.Add("Text", NewMaterialInfo.RightOffset, "Tab3", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtRightOffset5.DataBindings.Add("Text", NewMaterialInfo.RightOffset, "Tab4", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtRightOffset6.DataBindings.Add("Text", NewMaterialInfo.RightOffset, "Tab5", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtRightOffset7.DataBindings.Add("Text", NewMaterialInfo.RightOffset, "Tab6", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtRightOffset8.DataBindings.Add("Text", NewMaterialInfo.RightOffset, "Tab7", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtRightOffset9.DataBindings.Add("Text", NewMaterialInfo.RightOffset, "Tab8", true, DataSourceUpdateMode.OnPropertyChanged, "0");
                txtRightOffset10.DataBindings.Add("Text", NewMaterialInfo.RightOffset, "Tab9", true, DataSourceUpdateMode.OnPropertyChanged, "0");
            }
        }

        private void textbox_KeyPad_Click(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                double oldValue = Convert.ToDouble(textBox.Text);
                double newValue = KeyPadHelper.SetLabelDoubleData(textBox);

                ParamTrackingLogger.AddChangeHistory("MaterialInfo", textBox.Name.Replace("txt", ""), oldValue, newValue);
            }
        }

        private void txtKeyPad_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
        }

        private void lblApply_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void lblCancel_Click(object sender, EventArgs e)
        {
            NewMaterialInfo = PrevMaterialInfo;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtKeyPad_Leave(object sender, EventArgs e)
        {
            return;
        }
        #endregion
    }
}
