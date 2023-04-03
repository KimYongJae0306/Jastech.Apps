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

        private OperationSettingsControl OperationSettingsControl = null;
        private MotionSettingsControl MotionSettingsControl = null;
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
            OperationSettingsControl = new OperationSettingsControl();
            OperationSettingsControl.Dock = DockStyle.Fill;
            pnlSettingPage.Controls.Add(OperationSettingsControl);

            MotionSettingsControl = new MotionSettingsControl();
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
        #endregion
    }
}
