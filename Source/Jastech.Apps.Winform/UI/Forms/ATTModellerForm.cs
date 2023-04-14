using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Framework.Structure;
using Jastech.Framework.Winform.Forms;
using Jastech.Framework.Structure.Service;
using System.IO;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Structure.Helper;
using static Jastech.Framework.Modeller.Controls.ModelControl;
using Jastech.Apps.Winform.Settings;
using Jastech.Apps.Structure;

namespace Jastech.Apps.Winform.UI.Forms
{
    public partial class ATTModellerForm : Form
    {
        #region 속성
        public string ModelPath { get; set; } = "";

        public Jastech.Framework.Structure.Service.InspModelService InspModelService = null;
        #endregion

        #region 이벤트
        public event ApplyModelDelegate ApplyModelEventHandler;
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public ATTModellerForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void ATTModellerForm_Load(object sender, EventArgs e)
        {
            ModelPath = AppConfig.Instance().Path.Model;

            UpdateModelList();
        }

        private void UpdateModelList()
        {
            if (ModelPath == "")
                return;

            List<AppsInspModel> models = GetModelList(ModelPath);

            gvModelList.Rows.Clear();

            foreach (var model in models)
            {
                string createDate = model.CreateDate.ToString("yyyy-MM-dd HH:mm:dd");
                string modifiedDate = model.ModifiedDate.ToString("yyyy-MM-dd HH:mm:dd");
                string tabCount = model.TabCount.ToString();

                gvModelList.Rows.Add(model.Name, tabCount, createDate, modifiedDate, model.Description);
            }

            ClearSelected();
        }

        private void ClearSelected()
        {
            gvModelList.ClearSelection();

            lblSelectedName.Text = "";
            lblSelectedCreateDate.Text = "";
            lblSelectedModifiedDate.Text = "";
            lblSelectedDescription.Text = "";
            lblSelectedTabCount.Text = "";
        }

        private void lblCreateModel_Click(object sender, EventArgs e)
        {
            if (ModelPath == "")
                return;

            CreateATTModelForm form = new CreateATTModelForm();
            form.ModelPath = ModelPath;
            form.CreateModelEvent += ATTModellerForm_CreateModelEvent;

            if (form.ShowDialog() == DialogResult.OK)
            {
                UpdateModelList();
            }
            form.CreateModelEvent -= ATTModellerForm_CreateModelEvent;
        }

        private void ATTModellerForm_CreateModelEvent(InspModel model)
        {
            var createModel = model as AppsInspModel;
            if (InspModelService != null)
            {
                AppsInspModel newModel = InspModelService.New() as AppsInspModel;
                newModel.Name = createModel.Name;
                newModel.CreateDate = createModel.CreateDate;
                newModel.ModifiedDate = createModel.ModifiedDate;
                newModel.Description = createModel.Description;
                newModel.TabCount = createModel.TabCount;
                newModel.SpecInfo = createModel.SpecInfo;
                newModel.MaterialInfo = createModel.MaterialInfo;

                InspModelService.AddModelData(newModel);

                ModelFileHelper.Save(AppConfig.Instance().Path.Model, newModel);
            }
        }

        private void lblEditModel_Click(object sender, EventArgs e)
        {
            if (ModelPath == "" || gvModelList.SelectedRows.Count <= 0)
                return;

            EditATTModelForm form = new EditATTModelForm();
            form.PrevModelName = lblSelectedName.Text;
            form.ModelPath = ModelPath;
            form.EditModelEvent += ATTModellerForm_EditModelEvent;

            if (form.ShowDialog() == DialogResult.OK)
            {
                UpdateModelList();
            }
            form.EditModelEvent += ATTModellerForm_EditModelEvent;
        }

        private void ATTModellerForm_EditModelEvent(string prevModelName, InspModel editModel)
        {
            if (InspModelService != null)
            {
                string modelDir = AppConfig.Instance().Path.Model;
                string filePath = Path.Combine(modelDir, prevModelName, InspModel.FileName);
                AppsInspModel prevModel = InspModelService.Load(filePath) as AppsInspModel;

                prevModel.SpecInfo.AlignToleranceX_um = (editModel as AppsInspModel).SpecInfo.AlignToleranceX_um;
                prevModel.SpecInfo.AlignToleranceY_um = (editModel as AppsInspModel).SpecInfo.AlignToleranceY_um;
                prevModel.SpecInfo.AlignToleranceCx_um = (editModel as AppsInspModel).SpecInfo.AlignToleranceCx_um;
                prevModel.SpecInfo.AlignStandard_um = (editModel as AppsInspModel).SpecInfo.AlignStandard_um;
                prevModel.MaterialInfo = (editModel as AppsInspModel).MaterialInfo;

                ModelFileHelper.Edit(modelDir, prevModel, editModel);
            }
        }

        private void lblDeleteModel_Click(object sender, EventArgs e)
        {
            if (ModelPath == "" || gvModelList.SelectedRows.Count <= 0)
                return;

            MessageYesNoForm form = new MessageYesNoForm();
            form.Message = "Do you want to delete the selected model?";

            if (form.ShowDialog() == DialogResult.Yes)
            {
                ModelFileHelper.Delete(ModelPath, lblSelectedName.Text);
                UpdateModelList();
            }
        }

        private void lblCopyModel_Click(object sender, EventArgs e)
        {
            if (ModelPath == "" || gvModelList.SelectedRows.Count <= 0)
                return;

            Framework.Modeller.Forms.CopyModelForm form = new Framework.Modeller.Forms.CopyModelForm();
            form.PrevModelName = lblSelectedName.Text;
            form.ModelPath = ModelPath;
            form.CopyModelEvent += ATTModellerForm_CopyModelEvent;

            if (form.ShowDialog() == DialogResult.OK)
            {
                UpdateModelList();
            }
            form.CopyModelEvent -= ATTModellerForm_CopyModelEvent;
        }

        private void ATTModellerForm_CopyModelEvent(string prevModelName, string newModelName)
        {
            if (InspModelService != null)
            {
                string modelDir = AppConfig.Instance().Path.Model;
                string filePath = Path.Combine(modelDir, prevModelName, InspModel.FileName);
                InspModel prevModel = InspModelService.Load(filePath);
                prevModel.Name = newModelName;

                ModelFileHelper.Save(modelDir, prevModel);
            }
        }

        private void gvModelList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            UpdateSelectedModel(e.RowIndex);
        }

        private void UpdateSelectedModel(int selectIndex)
        {
            if (ModelPath == "")
                return;

            lblSelectedName.Text = gvModelList.Rows[selectIndex].Cells[0].Value.ToString();
            lblSelectedTabCount.Text = gvModelList.Rows[selectIndex].Cells[1].Value.ToString();
            lblSelectedCreateDate.Text = gvModelList.Rows[selectIndex].Cells[2].Value.ToString();
            lblSelectedModifiedDate.Text = gvModelList.Rows[selectIndex].Cells[3].Value.ToString();
            lblSelectedDescription.Text = gvModelList.Rows[selectIndex].Cells[4].Value.ToString();
        }

        private void lblApply_Click(object sender, EventArgs e)
        {
            ApplyModel();
        }

        private void ApplyModel()
        {
            if (lblSelectedName.Text == "")
                return;

            ApplyModelEventHandler?.Invoke(lblSelectedName.Text);

            MessageConfirmForm form = new MessageConfirmForm();
            form.Message = "Model Load Completed.";
            form.ShowDialog();
        }

        public List<AppsInspModel> GetModelList(string modelPath)
        {
            if (!Directory.Exists(modelPath))
                Directory.CreateDirectory(modelPath);

            List<AppsInspModel> modelList = new List<AppsInspModel>();

            string[] dirs = Directory.GetDirectories(modelPath);
            for (int i = 0; i < dirs.Length; i++)
            {
                AppsInspModel inspModel = new AppsInspModel();
                string path = Path.Combine(dirs[i], InspModel.FileName);
                JsonConvertHelper.LoadToExistingTarget<AppsInspModel>(path, inspModel);
                modelList.Add(inspModel);
            }

            return modelList;
        }
        #endregion

        private void gvModelList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            UpdateSelectedModel(e.RowIndex);

            ApplyModel();
        }

        private void lblCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
