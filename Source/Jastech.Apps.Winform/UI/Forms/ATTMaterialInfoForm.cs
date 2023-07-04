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
                txtTab1Width.Text = PrevMaterialInfo.Tab0Width_mm.ToString();
                txtTab2Width.Text = PrevMaterialInfo.Tab1Width_mm.ToString();
                txtTab3Width.Text = PrevMaterialInfo.Tab2Width_mm.ToString();
                txtTab4Width.Text = PrevMaterialInfo.Tab3Width_mm.ToString();
                txtTab5Width.Text = PrevMaterialInfo.Tab4Width_mm.ToString();
                txtTab6Width.Text = PrevMaterialInfo.Tab5Width_mm.ToString();
                txtTab7Width.Text = PrevMaterialInfo.Tab6Width_mm.ToString();
                txtTab8Width.Text = PrevMaterialInfo.Tab7Width_mm.ToString();

                // Tab To Tab Distance
                txtDistance1.Text = PrevMaterialInfo.TabToTabDistance0_mm.ToString();
                txtDistance2.Text = PrevMaterialInfo.TabToTabDistance1_mm.ToString();
                txtDistance3.Text = PrevMaterialInfo.TabToTabDistance2_mm.ToString();
                txtDistance4.Text = PrevMaterialInfo.TabToTabDistance3_mm.ToString();
                txtDistance5.Text = PrevMaterialInfo.TabToTabDistance4_mm.ToString();
                txtDistance6.Text = PrevMaterialInfo.TabToTabDistance5_mm.ToString();
                txtDistance7.Text = PrevMaterialInfo.TabToTabDistance6_mm.ToString();
                txtDistance8.Text = PrevMaterialInfo.TabToTabDistance7_mm.ToString();
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
            NewMaterialInfo = new MaterialInfo
            {
                PanelXSize_mm = Convert.ToDouble(txtPanelXSize.Text),
                MarkToMark_mm = Convert.ToDouble(txtMarkToMark.Text),
                PanelEdgeToFirst_mm = Convert.ToDouble(txtPanelEdgeToFirst.Text),

                Tab0Width_mm = Convert.ToDouble(txtTab1Width.Text),
                Tab1Width_mm = Convert.ToDouble(txtTab2Width.Text),
                Tab2Width_mm = Convert.ToDouble(txtTab3Width.Text),
                Tab3Width_mm = Convert.ToDouble(txtTab4Width.Text),
                Tab4Width_mm = Convert.ToDouble(txtTab5Width.Text),
                Tab5Width_mm = Convert.ToDouble(txtTab6Width.Text),
                Tab6Width_mm = Convert.ToDouble(txtTab7Width.Text),
                Tab7Width_mm = Convert.ToDouble(txtTab8Width.Text),

                TabToTabDistance0_mm = Convert.ToDouble(txtDistance1.Text),
                TabToTabDistance1_mm = Convert.ToDouble(txtDistance2.Text),
                TabToTabDistance2_mm = Convert.ToDouble(txtDistance3.Text),
                TabToTabDistance3_mm = Convert.ToDouble(txtDistance4.Text),
                TabToTabDistance4_mm = Convert.ToDouble(txtDistance5.Text),
                TabToTabDistance5_mm = Convert.ToDouble(txtDistance6.Text),
                TabToTabDistance6_mm = Convert.ToDouble(txtDistance7.Text),
                TabToTabDistance7_mm = Convert.ToDouble(txtDistance8.Text),
            };

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
