using Jastech.Apps.Structure;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Config;
using Jastech.Framework.Structure.Helper;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform.Forms;
using System;
using System.Windows.Forms;
using static Jastech.Framework.Modeller.Controls.ModelControl;

namespace Jastech.Apps.Winform.UI.Forms
{
    public partial class CreateATTModelForm : Form
    {
        #region 속성
        public string ModelPath { get; set; } = "";
        #endregion

        #region 이벤트
        public event ModelDelegate CreateModelEvent;
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

            if(Convert.ToInt16(tabCount) > AppsConfig.Instance().TabMaxCount)
            {
                ShowMessageBox("TabCount Max is 10.");
                return;
            }

            if (ModelFileHelper.IsExistModel(ModelPath, modelName))
            {
                ShowMessageBox("The same model exists.");
                return;
            }

            AppsInspModel model = new AppsInspModel
            {
                Name = modelName,
                Description = description,
                CreateDate = time,
                ModifiedDate = time,
                TabCount = Convert.ToInt32(tabCount),
            };

            if (AppsConfig.Instance().UseMaterialInfo)
            {
                ATTMaterialInfoForm form = new ATTMaterialInfoForm();
                form.TabCount = model.TabCount;
                if (form.ShowDialog() == DialogResult.OK)
                    model.MaterialInfo = form.NewMaterialInfo;
                else
                    return;
            }

            DialogResult = DialogResult.OK;
            Close();

            CreateModelEvent?.Invoke(model);

            Logger.Write(LogType.GUI, "Clicked CreateATTModelForm Apply Button");
        }

        private void lblCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();

            Logger.Write(LogType.GUI, "Clicked CreateATTModelForm Cancle Button");
        }

        private void ShowMessageBox(string message)
        {
            MessageConfirmForm form = new MessageConfirmForm();
            form.Message = message;
            form.ShowDialog();
        }

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

                if (form.ShowDialog() == DialogResult.OK)
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
        #endregion
    }
}
