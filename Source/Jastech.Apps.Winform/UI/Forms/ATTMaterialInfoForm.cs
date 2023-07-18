using Jastech.Apps.Structure.Data;
using Jastech.Framework.Config;
using Jastech.Framework.Winform.Forms;
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
            if (PrevMaterialInfo != null)
            {
                // Data
                txtPanelXSize.Text = PrevMaterialInfo.PanelXSize_mm.ToString();
                txtMarkToMark.Text = PrevMaterialInfo.MarkToMark_mm.ToString();
                txtPanelEdgeToFirst.Text = PrevMaterialInfo.PanelEdgeToFirst_mm.ToString();

                // Tab Width
                txtTab1Width.Text = PrevMaterialInfo.TabWidth_mm.Tab0.ToString();
                txtTab2Width.Text = PrevMaterialInfo.TabWidth_mm.Tab1.ToString();
                txtTab3Width.Text = PrevMaterialInfo.TabWidth_mm.Tab2.ToString();
                txtTab4Width.Text = PrevMaterialInfo.TabWidth_mm.Tab3.ToString();
                txtTab5Width.Text = PrevMaterialInfo.TabWidth_mm.Tab4.ToString();
                txtTab6Width.Text = PrevMaterialInfo.TabWidth_mm.Tab5.ToString();
                txtTab7Width.Text = PrevMaterialInfo.TabWidth_mm.Tab6.ToString();
                txtTab8Width.Text = PrevMaterialInfo.TabWidth_mm.Tab7.ToString();
                txtTab9Width.Text = PrevMaterialInfo.TabWidth_mm.Tab8.ToString();
                txtTab10Width.Text = PrevMaterialInfo.TabWidth_mm.Tab9.ToString();

                // Tab To Tab Distance
                txtDistance1.Text = PrevMaterialInfo.TabToTabDistance_mm.Tab0ToTab1.ToString();
                txtDistance2.Text = PrevMaterialInfo.TabToTabDistance_mm.Tab1ToTab2.ToString();
                txtDistance3.Text = PrevMaterialInfo.TabToTabDistance_mm.Tab2ToTab3.ToString();
                txtDistance4.Text = PrevMaterialInfo.TabToTabDistance_mm.Tab3ToTab4.ToString();
                txtDistance5.Text = PrevMaterialInfo.TabToTabDistance_mm.Tab4ToTab5.ToString();
                txtDistance6.Text = PrevMaterialInfo.TabToTabDistance_mm.Tab5ToTab6.ToString();
                txtDistance7.Text = PrevMaterialInfo.TabToTabDistance_mm.Tab6ToTab7.ToString();
                txtDistance8.Text = PrevMaterialInfo.TabToTabDistance_mm.Tab7ToTab8.ToString();
                txtDistance9.Text = PrevMaterialInfo.TabToTabDistance_mm.Tab8ToTab9.ToString();

                // Left Offset
                txtLeftOffset1.Text = PrevMaterialInfo.LeftOffset.Tab0.ToString();
                txtLeftOffset2.Text = PrevMaterialInfo.LeftOffset.Tab1.ToString();
                txtLeftOffset3.Text = PrevMaterialInfo.LeftOffset.Tab2.ToString();
                txtLeftOffset4.Text = PrevMaterialInfo.LeftOffset.Tab3.ToString();
                txtLeftOffset5.Text = PrevMaterialInfo.LeftOffset.Tab4.ToString();
                txtLeftOffset6.Text = PrevMaterialInfo.LeftOffset.Tab5.ToString();
                txtLeftOffset7.Text = PrevMaterialInfo.LeftOffset.Tab6.ToString();
                txtLeftOffset8.Text = PrevMaterialInfo.LeftOffset.Tab7.ToString();
                txtLeftOffset9.Text = PrevMaterialInfo.LeftOffset.Tab8.ToString();
                txtLeftOffset10.Text = PrevMaterialInfo.LeftOffset.Tab9.ToString();

                // Right Offset
                txtRightOffset1.Text = PrevMaterialInfo.RightOffset.Tab0.ToString();
                txtRightOffset2.Text = PrevMaterialInfo.RightOffset.Tab1.ToString();
                txtRightOffset3.Text = PrevMaterialInfo.RightOffset.Tab2.ToString();
                txtRightOffset4.Text = PrevMaterialInfo.RightOffset.Tab3.ToString();
                txtRightOffset5.Text = PrevMaterialInfo.RightOffset.Tab4.ToString();
                txtRightOffset6.Text = PrevMaterialInfo.RightOffset.Tab5.ToString();
                txtRightOffset7.Text = PrevMaterialInfo.RightOffset.Tab6.ToString();
                txtRightOffset8.Text = PrevMaterialInfo.RightOffset.Tab7.ToString();
                txtRightOffset9.Text = PrevMaterialInfo.RightOffset.Tab8.ToString();
                txtRightOffset10.Text = PrevMaterialInfo.RightOffset.Tab9.ToString();
            }
        }

        private void textbox_KeyPad_Click(object sender, EventArgs e)
        {
            if (OperationConfig.UseKeyboard)
            {
                var textBox = (TextBox)sender;

                if (textBox.Text == "")
                    textBox.Text = "0";

                KeyPadForm keyPadForm = new KeyPadForm();
                keyPadForm.PreviousValue = Convert.ToDouble(textBox.Text);
                keyPadForm.ShowDialog();

                textBox.Text = keyPadForm.PadValue.ToString();
            }
        }

        private void txtKeyPad_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtKeyPad_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;

            if (double.TryParse(textBox.Text, out double value))
                textBox.Text = string.Format("{0:0.0000}", value);
            else
                textBox.Text = "0.0000";
        }

        private void lblApply_Click(object sender, EventArgs e)
        {
            NewMaterialInfo = new MaterialInfo();
            NewMaterialInfo.PanelXSize_mm = Convert.ToDouble(txtPanelXSize.Text);
            NewMaterialInfo.MarkToMark_mm = Convert.ToDouble(txtMarkToMark.Text);
            NewMaterialInfo.PanelEdgeToFirst_mm = Convert.ToDouble(txtPanelEdgeToFirst.Text);

            // Tab Width
            NewMaterialInfo.TabWidth_mm.Tab0 = Convert.ToDouble(txtTab1Width.Text);
            NewMaterialInfo.TabWidth_mm.Tab1 = Convert.ToDouble(txtTab2Width.Text);
            NewMaterialInfo.TabWidth_mm.Tab2 = Convert.ToDouble(txtTab3Width.Text);
            NewMaterialInfo.TabWidth_mm.Tab3 = Convert.ToDouble(txtTab4Width.Text);
            NewMaterialInfo.TabWidth_mm.Tab4 = Convert.ToDouble(txtTab5Width.Text);
            NewMaterialInfo.TabWidth_mm.Tab5 = Convert.ToDouble(txtTab6Width.Text);
            NewMaterialInfo.TabWidth_mm.Tab6 = Convert.ToDouble(txtTab7Width.Text);
            NewMaterialInfo.TabWidth_mm.Tab7 = Convert.ToDouble(txtTab8Width.Text);
            NewMaterialInfo.TabWidth_mm.Tab8 = Convert.ToDouble(txtTab9Width.Text);
            NewMaterialInfo.TabWidth_mm.Tab9 = Convert.ToDouble(txtTab10Width.Text);

            // Tab to Tab
            NewMaterialInfo.TabToTabDistance_mm.Tab0ToTab1 = Convert.ToDouble(txtDistance1.Text);
            NewMaterialInfo.TabToTabDistance_mm.Tab1ToTab2 = Convert.ToDouble(txtDistance2.Text);
            NewMaterialInfo.TabToTabDistance_mm.Tab2ToTab3 = Convert.ToDouble(txtDistance3.Text);
            NewMaterialInfo.TabToTabDistance_mm.Tab3ToTab4 = Convert.ToDouble(txtDistance4.Text);
            NewMaterialInfo.TabToTabDistance_mm.Tab4ToTab5 = Convert.ToDouble(txtDistance5.Text);
            NewMaterialInfo.TabToTabDistance_mm.Tab5ToTab6 = Convert.ToDouble(txtDistance6.Text);
            NewMaterialInfo.TabToTabDistance_mm.Tab6ToTab7 = Convert.ToDouble(txtDistance7.Text);
            NewMaterialInfo.TabToTabDistance_mm.Tab7ToTab8 = Convert.ToDouble(txtDistance8.Text);
            NewMaterialInfo.TabToTabDistance_mm.Tab8ToTab9 = Convert.ToDouble(txtDistance9.Text);

            // Left Offset
            NewMaterialInfo.LeftOffset.Tab0 = Convert.ToDouble(txtLeftOffset1.Text);
            NewMaterialInfo.LeftOffset.Tab1 = Convert.ToDouble(txtLeftOffset2.Text);
            NewMaterialInfo.LeftOffset.Tab2 = Convert.ToDouble(txtLeftOffset3.Text);
            NewMaterialInfo.LeftOffset.Tab3 = Convert.ToDouble(txtLeftOffset4.Text);
            NewMaterialInfo.LeftOffset.Tab4 = Convert.ToDouble(txtLeftOffset5.Text);
            NewMaterialInfo.LeftOffset.Tab5 = Convert.ToDouble(txtLeftOffset6.Text);
            NewMaterialInfo.LeftOffset.Tab6 = Convert.ToDouble(txtLeftOffset7.Text);
            NewMaterialInfo.LeftOffset.Tab7 = Convert.ToDouble(txtLeftOffset8.Text);
            NewMaterialInfo.LeftOffset.Tab8 = Convert.ToDouble(txtLeftOffset9.Text);
            NewMaterialInfo.LeftOffset.Tab9 = Convert.ToDouble(txtLeftOffset10.Text);

            // Right Offset
            NewMaterialInfo.RightOffset.Tab0 = Convert.ToDouble(txtRightOffset1.Text);
            NewMaterialInfo.RightOffset.Tab1 = Convert.ToDouble(txtRightOffset2.Text);
            NewMaterialInfo.RightOffset.Tab2 = Convert.ToDouble(txtRightOffset3.Text);
            NewMaterialInfo.RightOffset.Tab3 = Convert.ToDouble(txtRightOffset4.Text);
            NewMaterialInfo.RightOffset.Tab4 = Convert.ToDouble(txtRightOffset5.Text);
            NewMaterialInfo.RightOffset.Tab5 = Convert.ToDouble(txtRightOffset6.Text);
            NewMaterialInfo.RightOffset.Tab6 = Convert.ToDouble(txtRightOffset7.Text);
            NewMaterialInfo.RightOffset.Tab7 = Convert.ToDouble(txtRightOffset8.Text);
            NewMaterialInfo.RightOffset.Tab8 = Convert.ToDouble(txtRightOffset9.Text);
            NewMaterialInfo.RightOffset.Tab9 = Convert.ToDouble(txtRightOffset10.Text);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void lblCancel_Click(object sender, EventArgs e)
        {
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
