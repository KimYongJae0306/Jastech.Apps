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
using Jastech.Apps.Winform.UI.Controls;
using ATT_UT_Remodeling.UI.Controls;

namespace ATT_UT_Remodeling.UI.Pages
{
    public partial class MainPage : UserControl
    {
        #region 필드
        #endregion

        #region 속성
        public AkkonViewerControl AkkonViewerControl { get; set; } = new AkkonViewerControl() { Dock = DockStyle.Fill };

        public AlignViewerControl AlignViewerControl { get; set; } = new AlignViewerControl() { Dock = DockStyle.Fill };

        public PreAlignDisplayControl PreAlignDisplayControl { get; set; } = new PreAlignDisplayControl() { Dock = DockStyle.Fill };

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
            pnlAkkon.Controls.Add(AkkonViewerControl);
            pnlAlign.Controls.Add(AlignViewerControl);
            pnlPreAlign.Controls.Add(PreAlignDisplayControl);
        }

        public void UpdateTabCount(int tabCount)
        {
            AkkonViewerControl.UpdateTabCount(tabCount);
            AlignViewerControl.UpdateTabCount(tabCount);
        }

        public void UpdateMainResult(AppsInspResult result)
        {
            AkkonViewerControl.UpdateMainResult(result);
            AlignViewerControl.UpdateMainResult(result);
        }
        #endregion
    }
}
