using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Service;
using Jastech.Framework.Config;
using Jastech.Framework.Structure;
using Jastech.Framework.Structure.Helper;
using Jastech.Framework.Structure.Service;
using Jastech.Framework.Users;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using static Jastech.Framework.Modeller.Controls.ModelControl;

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
            ModelPath = ConfigSet.Instance().Path.Model;

            ButtonEnable();
            UpdateModelList();
        }

        private void ATTModellerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // TODO: Tracking Model Parameter Changed
            ParamTrackingLogger.ClearChangedLog();
        }

        private void ButtonEnable()
        {
            if (UserManager.Instance().CurrentUser.Type == AuthorityType.Maker)
                tlpnlMenu.Visible = true;
            else
                tlpnlMenu.Visible = false;
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

            Logger.Write(LogType.GUI, "Clicked ATTModellerFom Create Button");
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
                newModel.MaterialInfo = createModel.MaterialInfo;

                InspModelService.AddModelData(newModel);

                ModelFileHelper.Save(ConfigSet.Instance().Path.Model, newModel);
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

            Logger.Write(LogType.GUI, "Clicked ATTModellerFom Edit Button");
        }

        private void ATTModellerForm_EditModelEvent(string prevModelName, InspModel editModel)
        {
            if (InspModelService != null)
            {
                string modelDir = ConfigSet.Instance().Path.Model;
                string filePath = Path.Combine(modelDir, prevModelName, InspModel.FileName);
                var prevModel = InspModelService.Load(filePath) as AppsInspModel;
                var editInspModel = editModel as AppsInspModel;

                prevModel.MaterialInfo = editInspModel.MaterialInfo;

                ModelFileHelper.Edit(modelDir, prevModel, editModel);

                if (ParamTrackingLogger.IsEmpty == false)
                {
                    ParamTrackingLogger.AddLog($"Inspection model '{prevModel.Name} : {prevModel.Description}' has been modified.");
                    ParamTrackingLogger.WriteLogToFile();
                }
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

            Logger.Write(LogType.GUI, "Clicked ATTModellerFom Delete Button");
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

            Logger.Write(LogType.GUI, "Clicked ATTModellerFom Copy Button");
        }

        private void ATTModellerForm_CopyModelEvent(string prevModelName, string newModelName)
        {
            if (InspModelService != null)
            {
                string modelDir = ConfigSet.Instance().Path.Model;
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
            Logger.Write(LogType.GUI, "Clicked ATTModellerFom Apply Button");
        }

        private void ApplyModel()
        {
            if (lblSelectedName.Text == "")
                return;

            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
         
            string selectedModel = lblSelectedName.Text;
			
            ApplyModelEventHandler?.Invoke(selectedModel);

            string previousModel = inspModel?.Name;
            if (previousModel?.Equals(selectedModel) == false)
                ParamTrackingLogger.AddChangeHistory("Inspector", "InspectionModel", previousModel, selectedModel);

            if (ParamTrackingLogger.IsEmpty == false)
            {
                ParamTrackingLogger.AddLog("Inspection Model Changed.");
                ParamTrackingLogger.WriteLogToFile();
            }

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

        private void gvModelList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            UpdateSelectedModel(e.RowIndex);

            ApplyModel();
        }

        private void lblCancel_Click(object sender, EventArgs e)
        {
            ParamTrackingLogger.ClearChangedLog();
            this.Close();

            Logger.Write(LogType.GUI, "Clicked ATTModellerFom Cancle Button");
        }
        #endregion
    }
}
