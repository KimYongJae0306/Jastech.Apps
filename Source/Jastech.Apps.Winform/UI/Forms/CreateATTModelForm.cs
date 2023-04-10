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

            DateTime time = DateTime.Now;

            if (modelName == "")
            {
                ShowMessageBox("Enter your model name.");
                return;
            }


            if (ModelFileHelper.IsExistModel(ModelPath, modelName))
            {
                ShowMessageBox("The same model exists.");
                return;
            }

            InspModel model = new InspModel
            {
                Name = modelName,
                Description = description,
                CreateDate = time,
                ModifiedDate = time,
            };

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
            //숫자만 입력되도록 필터링             
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))    //숫자와 백스페이스를 제외한 나머지를 바로 처리             
            {
                e.Handled = true;
            }
        }
    }
}
