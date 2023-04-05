using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATT.UI.Controls;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Apps.Winform;
using Jastech.Apps.Structure;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Structure;
using ATT.Core;
using Jastech.Framework.Util.Helper;

namespace ATT.UI.Pages
{
    public partial class SettingPage : UserControl
    {
        #region 필드
        #endregion

        #region 속성
        // Control List
        private List<UserControl> SettingControlList = null;

        private List<Button> SettingButtonList = null;

        private OperationSettingsControl OperationSettingsControl = new OperationSettingsControl();
        private MotionSettingsControl MotionSettingsControl = new MotionSettingsControl();
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public SettingPage()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드        
        private void SettingPage_Load(object sender, EventArgs e)
        {
            InitializeUI();
            AddControl();
        }

        private void InitializeUI()
        {
            btnSettings.ForeColor = Color.Blue;
        }

        private void AddControl()
        {
            OperationSettingsControl.Dock = DockStyle.Fill;
            pnlSettingPage.Controls.Add(OperationSettingsControl);

            MotionSettingsControl.Dock = DockStyle.Fill;
            pnlSettingPage.Controls.Add(MotionSettingsControl);
            
                
            // Teach Control List
            SettingControlList = new List<UserControl>();
            SettingControlList.Add(OperationSettingsControl);
            SettingControlList.Add(MotionSettingsControl);

            // Button List
            SettingButtonList = new List<Button>();
            SettingButtonList.Add(btnSettings);
            SettingButtonList.Add(btnMotion);
        }

        private void SetSelectButton(object sender)
        {
            foreach (Button button in SettingButtonList)
                button.ForeColor = Color.Black;

            Button btn = sender as Button;
            btn.ForeColor = Color.Blue;
        }

        private void SetSelectSettingPage(UserControl selectedControl)
        {
            foreach (UserControl control in SettingControlList)
                control.Visible = false;

            selectedControl.Visible = true;
            selectedControl.Dock = DockStyle.Fill;
            pnlSettingPage.Controls.Add(selectedControl);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SetSelectButton(sender);
            SetSelectSettingPage(OperationSettingsControl);
        }

        private void btnMotion_Click(object sender, EventArgs e)
        {
            SetSelectButton(sender);
            SetSelectSettingPage(MotionSettingsControl);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateCurrentData();
            Save();
        }

        private void UpdateCurrentData()
        {
            OperationSettingsControl.UpdateCuurentData();
            MotionSettingsControl.UpdateCuurentData();
        }

        private void Save()
        {
            // Save AxisHandler
            var axisHandler = AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Unit0);
            AppsMotionManager.Instance().Save(axisHandler);

            // Save Model
            var model = ModelManager.Instance().CurrentModel as ATTInspModel;
            model.SetUnitList(SystemManager.Instance().GetTeachingData().UnitList);

            string fileName = System.IO.Path.Combine(AppConfig.Instance().Path.Model, model.Name, InspModel.FileName);
            SystemManager.Instance().SaveModel(fileName, model);
        }

        public void UpdateModelData()
        {
            MotionSettingsControl.SetParams();
        }
    }
    #endregion


}
