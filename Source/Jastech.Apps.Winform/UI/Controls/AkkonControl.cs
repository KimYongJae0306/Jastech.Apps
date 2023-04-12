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
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Cognex.VisionPro;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AkkonControl : UserControl
    {
        #region 필드
        #endregion

        private AkkonParamControl AkkonParamControl { get; set; } = new AkkonParamControl();
        private List<Tab> TeachingTabList { get; set; } = new List<Tab>();
        #region 속성
        #endregion

        #region 이벤트

        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public AkkonControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void AkkonControl_Load(object sender, EventArgs e)
        {
            AddControl();
            //InitializeUI();
        }

        private void AddControl()
        {
            AkkonParamControl.Dock = DockStyle.Fill;
            //AkkonParamControl.GetOriginImageHandler += AkkonControl_GetOriginImageHandler;
            pnlParam.Controls.Add(AkkonParamControl);
        }

        private ICogImage AkkonControl_GetOriginImageHandler()
        {
            return AppsTeachingUIManager.Instance().GetPrevImage();
        }

        private void InitializeUI()
        {
            //lblTab.Text = "tlqkf";
        }

        public void SetParams(List<Tab> tabList)
        {
            if (tabList.Count <= 0)
                return;

            TeachingTabList = tabList;
            InitializeComboBox();

            string tabName = cmbTabList.SelectedItem as string;
            UpdateParam(tabName);
        }

        private void InitializeComboBox()
        {
            cmbTabList.Items.Clear();

            foreach (var item in TeachingTabList)
                cmbTabList.Items.Add(item.Name);

            cmbTabList.SelectedIndex = 0;
        }

        private void UpdateParam(string tabName)
        {
            //if (TeachingTabList.Count <= 0)
            //    return;

            //var param = TeachingTabList.Where(x => x.Name == tabName).First().AlignParamList[(int)_alignName];

            //CogCaliperParamControl.UpdateData(param);
            //lblLeadCount.Text = param.LeadCount.ToString();

            //DrawROI();
        }
        #endregion

        private void cmbTabList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbTabList_DrawItem(object sender, DrawItemEventArgs e)
        {

        }

        private void lblPrev_Click(object sender, EventArgs e)
        {

        }

        private void lblNext_Click(object sender, EventArgs e)
        {

        }

        private void lblInspection_Click(object sender, EventArgs e)
        {

        }

        private void lblAddROI_Click(object sender, EventArgs e)
        {

        }
    }
}
