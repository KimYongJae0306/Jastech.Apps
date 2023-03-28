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

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AkkonControl : UserControl
    {
        #region 필드
        private AkkonParamControl AkkonParamControl { get; set; } = new AkkonParamControl();
        #endregion

        #region 속성
        #endregion

        #region 이벤트
        private void AkkonControl_Load(object sender, EventArgs e)
        {
            AddControl();
        }
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
        private void AddControl()
        {
            AkkonParamControl.Dock = DockStyle.Fill;
            pnlParam.Controls.Add(AkkonParamControl);
        }
        #endregion
    }
}
