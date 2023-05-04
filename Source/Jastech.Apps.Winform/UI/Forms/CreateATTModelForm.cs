using Jastech.Apps.Structure;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Config;
using Jastech.Framework.Structure;
using Jastech.Framework.Structure.Helper;
using Jastech.Framework.Structure.Service;
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
using static Jastech.Framework.Modeller.Controls.ModelControl;

namespace Jastech.Apps.Winform.UI.Forms
{
    public partial class CreateATTModelForm : Form
    {
        #region 필드
        #endregion

        #region 속성
        public string ModelPath { get; set; } = "";
        #endregion

        #region 이벤트
        public event ModelDelegate CreateModelEvent;
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public CreateATTModelForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드

        private void lblOK_Click(object sender, EventArgs e)
        {
            string modelName = txtModelName.Text;
            string description = txtModelDescription.Text;
            string tabCount = txtTabCount.Text;

            DateTime time = DateTime.Now;

            if (modelName == "")
            {
                ShowMessageBox("Enter your model name.");
                return;
            }

            if(tabCount == "" || tabCount == "0")
            {
                ShowMessageBox("Enter your tab count.");
                return;
            }

            if(Convert.ToInt16(tabCount) > 8)
            {
                ShowMessageBox("TabCount Max is 8.");
                return;
            }

            if (ModelFileHelper.IsExistModel(ModelPath, modelName))
            {
                ShowMessageBox("The same model exists.");
                return;
            }

            if (txtSpecInfoX.Text == "")
                txtSpecInfoX.Text = "0";

            if (txtSpecInfoY.Text == "")
                txtSpecInfoY.Text = "0";

            if (txtSpecInfoCx.Text == "")
                txtSpecInfoCx.Text = "0";

            if (txtSpecInfoStandardValue.Text == "")
                txtSpecInfoStandardValue.Text = "0";

            AppsInspModel model = new AppsInspModel
            {
                Name = modelName,
                Description = description,
                CreateDate = time,
                ModifiedDate = time,
                SpecInfo = new Structure.Data.SpecInfo
                {
                    AlignToleranceX_um = Convert.ToDouble(txtSpecInfoX.Text),
                    AlignToleranceY_um = Convert.ToDouble(txtSpecInfoY.Text),
                    AlignToleranceCx_um = Convert.ToDouble(txtSpecInfoCx.Text),
                    AlignStandard_um = Convert.ToDouble(txtSpecInfoStandardValue.Text),
                },
                TabCount = Convert.ToInt32(tabCount),
            };

            if (AppsConfig.Instance().UseMaterialInfo)
            {
                ATTMaterialInfoForm form = new ATTMaterialInfoForm();
                if (form.ShowDialog() == DialogResult.OK)
                    model.MaterialInfo = form.NewMaterialInfo;
                else
                    return;
            }

            DialogResult = DialogResult.OK;
            Close();

            CreateModelEvent?.Invoke(model);
        }

        private void lblCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ShowMessageBox(string message)
        {
            MessageConfirmForm form = new MessageConfirmForm();
            form.Message = message;
            form.ShowDialog();
        }
        #endregion

        private void txtTabCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //숫자, 백스페이스 를 제외한 나머지를 바로 처리             
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void lblKeyPadForm_Click(object sender, EventArgs e)
        {

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

        private void textbox_KeyBoard_Click(object sender, EventArgs e)
        {
            if (OperationConfig.UseKeyboard)
            {
                KeyBoardForm form = new KeyBoardForm();

                if(form.ShowDialog() == DialogResult.OK)
                {
                    var textBox = (TextBox)sender;
                    textBox.Text = form.KeyValue;
                }
            }
        }

        private void txtKeyPad_KeyPress(object sender, KeyPressEventArgs e)
        {
            //숫자, 백스페이스, '.' 를 제외한 나머지를 바로 처리             
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back) || e.KeyChar == Convert.ToChar('.')))
            {
                e.Handled = true;
            }
        }

        private void txtKeyPad_Leave(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;

            if (double.TryParse(textBox.Text, out double value))
                textBox.Text = string.Format("{0:0.000}", value);
            else
                textBox.Text = "0.000";
        }
    }
}
