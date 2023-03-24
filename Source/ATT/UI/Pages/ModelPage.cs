﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Structure;
using Jastech.Framework.Structure.Service;
using System.IO;
using Jastech.Framework.Winform.Controls;
using Jastech.Apps.Structure;
using static Jastech.Framework.Winform.Controls.ModelControl;
using Jastech.Framework.Structure.Helper;

namespace ATT.UI.Pages
{
    public partial class ModelPage : UserControl
    {
        #region 속성
        private ModelControl ModelControl { get; set; } = new ModelControl();
        #endregion

        #region 속성
        public Jastech.Framework.Structure.Service.InspModelService InspModelService = null;
        #endregion

        #region 이벤트
        public event ApplyModelDelegate ApplyModelEventHandler;
        #endregion
        public ModelPage()
        {
            InitializeComponent();
        }

        private void ModelPage_Load(object sender, EventArgs e)
        {
            AddControl();

            
        }

        private void AddControl()
        {
            ModelControl.ModelPath = AppSettings.Instance().Path.Model;
            ModelControl.CreateModelEventHandler += ModelControl_CreateModelEventHandler;
            ModelControl.EditModelEventHandler += ModelControl_EditModelEventHandler;
            ModelControl.CopyModelEventHandler += ModelControl_CopyModelEventHandler;
            ModelControl.ApplyModelEventHandler += ModelControl_ApplyModelEventHandler;

            ModelControl.Dock = DockStyle.Fill;
            pnlModelPage.Controls.Add(ModelControl);
        }

        private void ModelControl_CopyModelEventHandler(string prevModelName, string newModelName)
        {
            if (InspModelService != null)
            {
                string modelDir = AppSettings.Instance().Path.Model;
                string filePath = Path.Combine(modelDir, prevModelName, InspModel.FileName);
                InspModel prevModel = InspModelService.Load(filePath);
                prevModel.Name = newModelName;

                ModelFileHelper.Save(modelDir, prevModel);
            }
        }

        private void ModelControl_EditModelEventHandler(string prevModelName, InspModel editModel)
        {
            if (InspModelService != null)
            {
                string modelDir = AppSettings.Instance().Path.Model;
                string filePath = Path.Combine(modelDir, prevModelName, InspModel.FileName);
                InspModel prevModel = InspModelService.Load(filePath);

                ModelFileHelper.Edit(modelDir, prevModel, editModel);
            }
        }

        private void ModelControl_CreateModelEventHandler(InspModel model)
        {
            if (InspModelService != null)
            {
                InspModel newModel = InspModelService.New();
                newModel.Name = model.Name;
                newModel.CreateDate = model.CreateDate;
                newModel.ModifiedDate = model.ModifiedDate;
                newModel.Description = model.Description;

                ModelFileHelper.Save(AppSettings.Instance().Path.Model, newModel);
            }
        }

        private void ModelControl_ApplyModelEventHandler(string modelName)
        {
            if(InspModelService != null)
            {
                ApplyModelEventHandler?.Invoke(modelName);
            }
        }
    }
}
