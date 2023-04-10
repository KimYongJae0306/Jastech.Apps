using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Apps.Winform.UI.Controls;

namespace ATT.UI.Pages
{
    public partial class MainPage : UserControl
    {

        #region 필드
        #endregion

        #region 속성
        public AkkonInspDisplayControl AkkonInspControl { get; private set; } = new AkkonInspDisplayControl();

        public AlignInspDisplayControl AlignInspControl { get; private set; } = new AlignInspDisplayControl();

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public MainPage()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void MainPage_Load(object sender, EventArgs e)
        {
            AddControls();
        }

        private void AddControls()
        {
            AkkonInspControl.Dock = DockStyle.Fill;
            pnlAkkon.Controls.Add(AkkonInspControl);

            AlignInspControl.Dock = DockStyle.Fill;
            pnlAlign.Controls.Add(AlignInspControl);
        }

        public void UpdateTabCount(int tabCount)
        {
            AkkonInspControl.UpdateTabCount(tabCount);
            AlignInspControl.UpdateTabCount(tabCount);
        }
        #endregion
    }
}
