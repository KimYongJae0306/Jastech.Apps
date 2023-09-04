using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Config;
using Jastech.Framework.Structure;
using Jastech.Framework.Structure.Helper;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform.Forms;
using System;
using System.IO;
using System.Windows.Forms;
using static Jastech.Framework.Modeller.Controls.ModelControl;

namespace Jastech.Apps.Winform.UI.Forms
{
    public partial class EditATTModelForm : Form
    {
        #region 속성
        private AppsInspModel PrevModel { get; set; } = new AppsInspModel();

        public string PrevModelName { get; set; }

        public string ModelPath { get; set; }
        #endregion

        #region 이벤트
        public event EditModelDelegate EditModelEvent;
        #endregion

        #region 생성자
        public EditATTModelForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void EditATTModelForm_Load(object sender, EventArgs e)
        {
            txtModelName.Text = PrevModelName;

            string filePath = Path.Combine(ModelPath, PrevModelName, InspModel.FileName);

            JsonConvertHelper.LoadToExistingTarget<AppsInspModel>(filePath, PrevModel);

            txtDescription.Text = PrevModel.Description;
            txtTabCount.Text = PrevModel.TabCount.ToString();
            txtSpecInfoX.Text = PrevModel.SpecInfo.AlignToleranceX_um.ToString();
            txtSpecInfoY.Text = PrevModel.SpecInfo.AlignToleranceY_um.ToString();
            txtSpecInfoCx.Text = PrevModel.SpecInfo.AlignToleranceCx_um.ToString();
            txtSpecInfoStandardValue.Text = PrevModel.SpecInfo.AlignStandard_um.ToString();
        }

        private void lblOK_Click(object sender, EventArgs e)
        {
            if (PrevModelName != txtModelName.Text)
            {
                if (ModelFileHelper.IsExistModel(ModelPath, txtModelName.Text))
                {
                    MessageConfirmForm form = new MessageConfirmForm();
                    form.Message = "The same model exists.";
                    form.ShowDialog();
                    return;
                }
            }

            AppsInspModel inspModel = new AppsInspModel
            {
                Name = txtModelName.Text,
                Description = txtDescription.Text,
                TabCount = Convert.ToInt32(txtTabCount.Text),
                SpecInfo = new SpecInfo
                {
                    AlignToleranceX_um = Convert.ToDouble(txtSpecInfoX.Text),
                    AlignToleranceY_um = Convert.ToDouble(txtSpecInfoY.Text),
                    AlignToleranceCx_um = Convert.ToDouble(txtSpecInfoCx.Text),
                    AlignStandard_um = Convert.ToDouble(txtSpecInfoStandardValue.Text),
                },
            };
            if (AppsConfig.Instance().UseMaterialInfo)
            {
                ATTMaterialInfoForm form = new ATTMaterialInfoForm();
                form.PrevMaterialInfo = PrevModel.MaterialInfo;

                if (form.ShowDialog() == DialogResult.OK)
                    inspModel.MaterialInfo = form.NewMaterialInfo;
                else
                    return;
            }

            DialogResult = DialogResult.OK;
            Close();

            EditModelEvent?.Invoke(PrevModelName, inspModel);
        }

        private void lblCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
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

        private void CompareModelParameter(AppsInspModel previousModel, AppsInspModel selectedModel)
        {
            if (previousModel == null || selectedModel == null)
                return;

            //if (previousModel.Name == selectedModel.Name)
            //{
            //    previousModel.SpecInfo.
            //    AddChangeHistory("Inspector", "Description", )
            //}
            //else
            //{

            //}
        }
        #endregion
    }
}
