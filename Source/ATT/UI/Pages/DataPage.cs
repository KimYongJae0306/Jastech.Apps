using ATT.Core;
using ATT.UI.Forms;
using Jastech.Apps.Structure;
using Jastech.Apps.Winform.UI.Forms;
using Jastech.Framework.Winform.Forms;
using System;
using System.Windows.Forms;
using static Jastech.Framework.Modeller.Controls.ModelControl;

namespace ATT.UI.Pages
{
    public partial class DataPage : UserControl
    {
        #region 필드
        private ATTInspModelService ATTInspModelService { get; set; } = null;

        private MotionSettingsForm _motionSettingsForm { get; set; } = null;
        #endregion

        #region 속성
        #endregion

        #region 이벤트
        public event ApplyModelDelegate ApplyModelEventHandler;
        #endregion

        #region 델리게이트
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
            if(ModelManager.Instance().CurrentModel == null)
            {
                MessageConfirmForm confirmForm = new MessageConfirmForm();
                confirmForm.Message = "None Current Model.";
                confirmForm.ShowDialog();
                return;
            }

            //MotionSettingsForm form = new MotionSettingsForm() { UnitName = UnitName.Unit0 };
            //form.InspModelService = ATTInspModelService;
            //form.ShowDialog();

            if (_motionSettingsForm == null)
            {
                _motionSettingsForm = new MotionSettingsForm();
                _motionSettingsForm.UnitName = UnitName.Unit0;
                _motionSettingsForm.InspModelService = ATTInspModelService;
                _motionSettingsForm.CloseEventDelegate = () => _motionSettingsForm = null;
                _motionSettingsForm.Show();
            }
            else
                _motionSettingsForm.Focus();
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
        #endregion
    }
}
