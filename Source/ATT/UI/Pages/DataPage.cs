using System;
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
            MotionSettingsForm form = new MotionSettingsForm();
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
