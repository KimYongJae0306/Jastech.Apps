using Jastech.Apps.Structure;
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
    public partial class EditATTModelForm : Form
    {
        #region 필드
        //InspModelFileService _inspModelFileService = new InspModelFileService();
        #endregion

        #region 속성
        public string PrevModelName { get; set; }

        public string ModelPath { get; set; }

        public string PrevTabCount { get; set; }

        public string PrevDescription { get; set; }
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
            txtDescription.Text = PrevDescription;
            txtTabCount.Text = PrevTabCount;
        }
        #endregion

        #region 메서드
        private void lblOK_Click(object sender, EventArgs e)
        {
            if (PrevModelName == txtModelName.Text && PrevDescription == txtDescription.Text)
                return;
            bool isEdit = false;
            if (PrevModelName != txtModelName.Text)
            {
                if (ModelFileHelper.IsExistModel(ModelPath, txtModelName.Text))
                {
                    MessageConfirmForm form = new MessageConfirmForm();
                    form.Message = "The same model exists.";
                    form.ShowDialog();
                    return;
                }
                isEdit = true;
            }
            if(PrevDescription != txtDescription.Text)
            {
                isEdit = true;
            }

            if(isEdit)
            {
                AppsInspModel inspModel = new AppsInspModel
                {
                    Name = txtModelName.Text,
                    Description = txtDescription.Text,
                    TabCount = Convert.ToInt32(txtTabCount.Text),
                };

                DialogResult = DialogResult.OK;
                Close();

                EditModelEvent?.Invoke(PrevModelName, inspModel);
            }
            else
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void lblCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion
    }
}
