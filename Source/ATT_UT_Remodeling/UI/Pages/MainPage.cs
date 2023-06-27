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

namespace ATT_UT_Remodeling.UI.Pages
{
    public partial class MainPage : UserControl
    {
        #region 필드
        #endregion

        #region 속성
        //public AkkonViewerControl AkkonResultViewer { get; set; } = new AkkonViewerControl() { Dock = DockStyle.Fill };

        //public AlignViewerControl AlignResultViewer { get; set; } = new AlignViewerControl() { Dock = DockStyle.Fill };

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
        #endregion



        //public AkkonViewerControl AkkonResultViewer { get; set; } = new AkkonViewerControl() { Dock = DockStyle.Fill };

        //public AlignViewerControl AlignResultViewer { get; set; } = new AlignViewerControl() { Dock = DockStyle.Fill };


        private void MainPage_Load(object sender, EventArgs e)
        {
            AddControls();
        }

        private void AddControls()
        {
            //pnlAkkon.Controls.Add(AkkonResultViewer);
            //pnlAlign.Controls.Add(AlignResultViewer);
        }

        public void UpdateTabCount(int tabCount)
        {
            //AkkonResultViewer.UpdateTabCount(tabCount);
            //AlignResultViewer.UpdateTabCount(tabCount);
        }

        public void UpdateMainResult(AppsInspResult result)
        {
            //AkkonResultViewer.UpdateMainResult(result);
            //AlignResultViewer.UpdateMainResult(result);
        }
    }

    public enum PageType
    {
        Result,
        Log,
    }
}
