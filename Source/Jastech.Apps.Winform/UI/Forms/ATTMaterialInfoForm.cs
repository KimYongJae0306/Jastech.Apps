using Jastech.Apps.Structure.Data;
using Jastech.Framework.Config;
using Jastech.Framework.Winform.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                txtTabWidth.Text = PrevMaterialInfo.TabWidth_mm.ToString();

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
                KeyPadForm keyPadForm = new KeyPadForm();
                keyPadForm.ShowDialog();

                var textBox = (TextBox)sender;
                textBox.Text = keyPadForm.PadValue.ToString();
            }
        }

        private void txtKeyPad_KeyPress(object sender, KeyPressEventArgs e)
        {
            //숫자만 입력되도록 필터링             
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))    //숫자와 백스페이스를 제외한 나머지를 바로 처리             
            {
                e.Handled = true;
            }
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
                TabWidth_mm = Convert.ToDouble(txtTabWidth.Text),
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
    }
}
