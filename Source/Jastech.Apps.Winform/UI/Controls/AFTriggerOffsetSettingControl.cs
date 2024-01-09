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
using Jastech.Apps.Structure;
using Jastech.Apps.Winform.Service.Plc;
using Jastech.Framework.Winform.Forms;
using Jastech.Framework.Structure;
using System.Windows.Forms.VisualStyles;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AFTriggerOffsetSettingControl : UserControl
    {
        #region 속성
        private Tab CurrentTab { get; set; } = null;

        public bool UseAlignCamMark { get; set; } = false;
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

            lblLeftOffset.Text = GetTriggerOffset().Left.ToString();
            lblRightOffset.Text = GetTriggerOffset().Right.ToString();
        }

        private LafTriggerOffset GetTriggerOffset()
        {
            if (UseAlignCamMark)
                return CurrentTab.AlignLafTriggerOffset;
            else
                return CurrentTab.LafTriggerOffset;
        }

        private void lblLeftOffset_Click(object sender, EventArgs e)
        {
            if (CurrentTab == null)
                return;
            
            string oldOffset = lblLeftOffset.Text;
            float leftOffset = KeyPadHelper.SetLabelFloatData((Label)sender);

            if (CheckOffsetRange(leftOffset, isLeft: true))
            {
                GetTriggerOffset().Left = leftOffset;
                lblLeftOffset.Text = leftOffset.ToString();
            }
            else
                lblLeftOffset.Text = oldOffset;
        }

        private void lblRightOffset_Click(object sender, EventArgs e)
        {
            if (CurrentTab == null)
                return;

            string oldOffset = lblRightOffset.Text;
            float rightOffset = KeyPadHelper.SetLabelFloatData((Label)sender);

            if (CheckOffsetRange(rightOffset, isLeft: false))
            {
                GetTriggerOffset().Right = rightOffset;
                lblRightOffset.Text = rightOffset.ToString();
            }
            else
                lblRightOffset.Text = oldOffset;
        }

        #endregion
        private bool CheckOffsetRange(float offset, bool isLeft)
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            var materialInfo = inspModel.MaterialInfo;
            var tabNo = CurrentTab.Index;

            double distance = 0.0;

            if (isLeft == true) 
            {
                if (tabNo == 0)
                    distance = materialInfo.PanelEdgeToFirst_mm;
                else
                    distance = materialInfo.GetTabToTabDistance(tabNo - 1, inspModel.TabCount) / 2;
            }
            else
            {
                if (tabNo == inspModel.TabCount - 1)    //TabCount 맞추기위해 -1
                    distance = materialInfo.PanelEdgeToFirst_mm;
                else
                    distance = materialInfo.GetTabToTabDistance(tabNo, inspModel.TabCount) / 2;
            }

            if (Math.Abs(offset) >= distance)
            {
                string errorMessage = string.Format($"Enter a value less than ±{distance}");
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = errorMessage;
                form.ShowDialog();
                return false;
            }

            return true;
        }
    }
}
