using ATT_UT_Remodeling.Core;
using ATT_UT_Remodeling.UI.Forms;
using Jastech.Apps.Structure;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.UI.Forms;
using Jastech.Framework.Winform;
using Jastech.Framework.Winform.Forms;
using System;
using System.Windows.Forms;
using static Jastech.Framework.Modeller.Controls.ModelControl;

namespace ATT_UT_Remodeling.UI.Pages
{
    public partial class DataPage : UserControl
    {
        #region 속성
        private MotionSettingsForm MotionSettingsForm { get; set; } = null;

        private PlcStatusForm PlcStatusForm { get; set; } = null;

        private ATTInspModelService ATTInspModelService { get; set; } = null;
        #endregion

        #region 이벤트
        public event ApplyModelDelegate ApplyModelEventHandler;
        #endregion

        #region 생성자
        public DataPage()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
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
            if (ModelManager.Instance().CurrentModel == null)
            {
                MessageConfirmForm confirmForm = new MessageConfirmForm();
                confirmForm.Message = "None Current Model.";
                confirmForm.ShowDialog();
                return;
            }

            
            if (MotionSettingsForm == null)
            {
                MotionSettingsForm = new MotionSettingsForm();
                MotionSettingsForm.UnitName = UnitName.Unit0;
                MotionSettingsForm.AxisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
                MotionSettingsForm.LafCtrl = LAFManager.Instance().GetLAFCtrl("Laf");
                MotionSettingsForm.InspModelService = ATTInspModelService;
                MotionSettingsForm.CloseEventDelegate = () => MotionSettingsForm = null;
                MotionSettingsForm.Show();
            }
            else
                MotionSettingsForm.Focus();
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

        private void btnOpenPLCViewer_Click(object sender, EventArgs e)
        {
            if (PlcStatusForm == null)
            {
                PlcStatusForm = new PlcStatusForm();
                PlcStatusForm.CloseEventDelegate = () => PlcStatusForm = null;
                PlcStatusForm.Show();
            }
            else
                PlcStatusForm.Focus();
        }
        #endregion
    }
}
