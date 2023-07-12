using Jastech.Apps.Structure.Data;
using Jastech.Framework.Config;
using Jastech.Framework.Winform.Forms;
using System;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Forms
{
    public partial class ATTMaterialInfoForm : Form
    {
        public MaterialInfo PrevMaterialInfo { get; set; } = null;
        public MaterialInfo NewMaterialInfo { get; set; } = null;
        public ATTMaterialInfoForm()
        {
            InitializeComponent();
        }

        private void ATTMaterialInfoForm_Load(object sender, EventArgs e)
        {
            if(PrevMaterialInfo != null)
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

                // Tab To Tab Distance
                txtDistance1.Text = PrevMaterialInfo.TabToTabDistance_mm.Tab0ToTab1.ToString();
                txtDistance2.Text = PrevMaterialInfo.TabToTabDistance_mm.Tab1ToTab2.ToString();
                txtDistance3.Text = PrevMaterialInfo.TabToTabDistance_mm.Tab2ToTab3.ToString();
                txtDistance4.Text = PrevMaterialInfo.TabToTabDistance_mm.Tab3ToTab4.ToString();
                txtDistance5.Text = PrevMaterialInfo.TabToTabDistance_mm.Tab4ToTab5.ToString();
                txtDistance6.Text = PrevMaterialInfo.TabToTabDistance_mm.Tab5ToTab6.ToString();
                txtDistance7.Text = PrevMaterialInfo.TabToTabDistance_mm.Tab6ToTab7.ToString();
                txtDistance8.Text = PrevMaterialInfo.TabToTabDistance_mm.Tab7ToTab8.ToString();
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
                textBox.Text = string.Format("{0:0.000}", value);
            else
                textBox.Text = "0.000";
        }

        private void lblApply_Click(object sender, EventArgs e)
        {
            NewMaterialInfo = new MaterialInfo();
            NewMaterialInfo.PanelXSize_mm = Convert.ToDouble(txtPanelXSize.Text);
            NewMaterialInfo.MarkToMark_mm = Convert.ToDouble(txtMarkToMark.Text);
            NewMaterialInfo.PanelEdgeToFirst_mm = Convert.ToDouble(txtPanelEdgeToFirst.Text);

            NewMaterialInfo.TabWidth_mm.Tab0 = Convert.ToDouble(txtTab1Width.Text);
            NewMaterialInfo.TabWidth_mm.Tab1 = Convert.ToDouble(txtTab2Width.Text);
            NewMaterialInfo.TabWidth_mm.Tab2 = Convert.ToDouble(txtTab3Width.Text);
            NewMaterialInfo.TabWidth_mm.Tab3 = Convert.ToDouble(txtTab4Width.Text);
            NewMaterialInfo.TabWidth_mm.Tab4 = Convert.ToDouble(txtTab5Width.Text);
            NewMaterialInfo.TabWidth_mm.Tab5 = Convert.ToDouble(txtTab6Width.Text);
            NewMaterialInfo.TabWidth_mm.Tab6 = Convert.ToDouble(txtTab7Width.Text);
            NewMaterialInfo.TabWidth_mm.Tab7 = Convert.ToDouble(txtTab8Width.Text);

            NewMaterialInfo.TabToTabDistance_mm.Tab0ToTab1 = Convert.ToDouble(txtDistance1.Text);
            NewMaterialInfo.TabToTabDistance_mm.Tab1ToTab2 = Convert.ToDouble(txtDistance2.Text);
            NewMaterialInfo.TabToTabDistance_mm.Tab2ToTab3 = Convert.ToDouble(txtDistance3.Text);
            NewMaterialInfo.TabToTabDistance_mm.Tab3ToTab4 = Convert.ToDouble(txtDistance4.Text);
            NewMaterialInfo.TabToTabDistance_mm.Tab4ToTab5 = Convert.ToDouble(txtDistance5.Text);
            NewMaterialInfo.TabToTabDistance_mm.Tab5ToTab6 = Convert.ToDouble(txtDistance6.Text);
            NewMaterialInfo.TabToTabDistance_mm.Tab6ToTab7 = Convert.ToDouble(txtDistance7.Text);
            NewMaterialInfo.TabToTabDistance_mm.Tab7ToTab8 = Convert.ToDouble(txtDistance8.Text);

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
            var textBox = (TextBox)sender;
            if (textBox == null)
                return;
            if (double.TryParse(textBox.Text, out double value))
                textBox.Text = string.Format("{0:0.000}", value);
            else
                textBox.Text = "0.000";
        }
    }
}
