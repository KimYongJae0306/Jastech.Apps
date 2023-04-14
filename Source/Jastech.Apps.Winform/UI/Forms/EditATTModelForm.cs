using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Config;
using Jastech.Framework.Structure;
using Jastech.Framework.Structure.Helper;
using Jastech.Framework.Structure.Service;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Jastech.Framework.Modeller.Controls.ModelControl;

namespace Jastech.Apps.Winform.UI.Forms
{
    public partial class EditATTModelForm : Form
    {
        #region 필드
        //InspModelFileService _inspModelFileService = new InspModelFileService();
        #endregion

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
        #endregion

        #region 메서드
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

            bool isEdit = IsEdit();
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
            if (AppConfig.Instance().UseMaterialInfo)
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

        public bool IsEdit()
        {
            if (PrevModel.Name != txtModelName.Text)
                return true;
            if (PrevModel.TabCount.ToString() != txtTabCount.Text)
                return true;
            if (PrevModel.SpecInfo.AlignToleranceX_um.ToString() != txtSpecInfoX.Text)
                return true;
            if (PrevModel.SpecInfo.AlignToleranceY_um.ToString() != txtSpecInfoY.Text)
                return true;
            if (PrevModel.SpecInfo.AlignToleranceCx_um.ToString() != txtSpecInfoCx.Text)
                return true;
            if (PrevModel.SpecInfo.AlignStandard_um.ToString() != txtSpecInfoStandardValue.Text)
                return true;

            return false;
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
                form.ShowDialog();

                var textBox = (TextBox)sender;
                textBox.Text = form.KeyValue;
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
        #endregion

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
    }
}
