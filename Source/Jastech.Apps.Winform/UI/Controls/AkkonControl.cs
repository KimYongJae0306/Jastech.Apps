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
            InitializeUI();
        }

        private void AddControl()
        {
            AkkonParamControl.Dock = DockStyle.Fill;
            pnlParam.Controls.Add(AkkonParamControl);
        }

        private void InitializeUI()
        {
            lblTab.Text = "tlqkf";
        }

        public void SetParams(List<Tab> tabList)
        {
            TeachingTabList = tabList;
        }
        #endregion
    }
}
