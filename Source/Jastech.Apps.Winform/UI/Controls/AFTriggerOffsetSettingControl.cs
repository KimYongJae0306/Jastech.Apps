using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Apps.Structure.Data;
using Jastech.Framework.Winform.Helper;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AFTriggerOffsetSettingControl : UserControl
    {
        #region 속성
        private Tab CurrentTab { get; set; } = null;
        #endregion

        #region 생성자
        public AFTriggerOffsetSettingControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void AFTriggerOffsetSettingControl_Load(object sender, EventArgs e)
        {
            UpdateData();
        }

        public void SetParams(Tab tab)
        {
            if (tab == null)
                return;

            CurrentTab = tab;
            UpdateData();
        }

        public void UpdateData()
        {
            if (CurrentTab == null)
                return;

            lblLeftOffset.Text = CurrentTab.LafTriggerOffset.Left.ToString();
            lblRightOffset.Text = CurrentTab.LafTriggerOffset.Right.ToString();
        }

        private void lblLeftOffset_Click(object sender, EventArgs e)
        {
            if (CurrentTab == null)
                return;

            float leftOffset = KeyPadHelper.SetLabelFloatData((Label)sender);

            CurrentTab.LafTriggerOffset.Left = leftOffset;
            lblLeftOffset.Text = leftOffset.ToString();
        }

        private void lblRightOffset_Click(object sender, EventArgs e)
        {
            if (CurrentTab == null)
                return;

            float rightOffset = KeyPadHelper.SetLabelFloatData((Label)sender);

            CurrentTab.LafTriggerOffset.Right = rightOffset;
            lblRightOffset.Text = rightOffset.ToString();
        }
        #endregion
    }
}
