﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Framework.Winform.Controls;
using Jastech.Apps.Winform.Settings;
using ATT.UI.Forms;
using Jastech.Framework.Structure.Service;
using static Jastech.Framework.Modeller.Controls.ModelControl;
using Jastech.Apps.Winform.UI.Forms;
using Jastech.Apps.Winform.Core;
using ATT.Core;
using Jastech.Apps.Structure;
using Jastech.Framework.Winform.Forms;
using Jastech.Apps.Winform;
using Jastech.Apps.Structure.Data;
using Jastech.Framework.Winform;

namespace ATT.UI.Pages
{
    public partial class DataPage : UserControl
    {
        private ATTInspModelService ATTInspModelService { get; set; } = null;

        public event ApplyModelDelegate ApplyModelEventHandler;

        public DataPage()
        {
            InitializeComponent();
        }

        private void btnModelPage_Click(object sender, EventArgs e)
        {
            ATTModellerForm form = new ATTModellerForm();
            form.InspModelService = ATTInspModelService;
            form.ApplyModelEventHandler += Form_ApplyModelEventHandler;
            form.ShowDialog();
        }

        private void Form_ApplyModelEventHandler(string modelName)
        {
            ApplyModelEventHandler?.Invoke(modelName);
        }

        private void btnMotionData_Click(object sender, EventArgs e)
        {
            if(ModelManager.Instance().CurrentModel == null)
            {
                MessageConfirmForm confirmForm = new MessageConfirmForm();
                confirmForm.Message = "None Current Model.";
                confirmForm.ShowDialog();
                return;
            }
            MotionSettingsForm form = new MotionSettingsForm() { UnitName = "0" };
            form.ShowDialog();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            OperationSettingsForm form = new OperationSettingsForm();
            form.ShowDialog();
        }

        public void SetInspModelService(ATTInspModelService inspModelService)
        {
            ATTInspModelService = inspModelService;
        }
    }
}
