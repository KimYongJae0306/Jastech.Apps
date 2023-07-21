using ATT_UT_IPAD.Core.Data;
using ATT_UT_IPAD.UI.Controls;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Framework.Winform.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ATT_UT_IPAD.UI.Pages
{
    public partial class MainPage : UserControl
    {
        #region 필드
        private Color _noneSelectColor { get; set; } = Color.FromArgb(52, 52, 52);

        private Color _selectedColor { get; set; } = Color.FromArgb(104, 104, 104);

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

        #region 속성
        public MainViewControl MainViewControl { get; set; } = null;

        public TabAlignViewControl TabAlignViewControl { get; set; } = null;

        public TabAkkonViewControl TabAkkonViewControl { get; set; } = null;
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
            MainViewControl = new MainViewControl();
            MainViewControl.Dock = DockStyle.Fill;
            MainViewControl.Visible = false;
            pnlView.Controls.Add(MainViewControl);

            TabAlignViewControl = new TabAlignViewControl();
            TabAlignViewControl.Dock = DockStyle.Fill;
            TabAlignViewControl.Visible = false;
            pnlView.Controls.Add(TabAlignViewControl);

            TabAkkonViewControl = new TabAkkonViewControl();
            TabAkkonViewControl.Dock = DockStyle.Fill;
            TabAkkonViewControl.Visible = false;
            pnlView.Controls.Add(TabAkkonViewControl);

            SelectMainView();
        }

        private void lblMainButton_Click(object sender, EventArgs e)
        {
            SelectMainView();
        }

        private void lblAkkonButton_Click(object sender, EventArgs e)
        {
            SelectAkkonView();
        }

        private void lblAlignButton_Click(object sender, EventArgs e)
        {
            SelectAlignView();
        }

        private void SelectMainView()
        {
            if (MainViewControl.Visible)
                return;

            lblMainButton.BackColor = _selectedColor;
            lblAkkonButton.BackColor = _noneSelectColor;
            lblAlignButton.BackColor = _noneSelectColor;

            MainViewControl.Visible = true;
            TabAlignViewControl.Visible = false;
            TabAkkonViewControl.Visible = false;

            MainViewControl.Dock = DockStyle.Fill;
            pnlView.Controls.Add(MainViewControl);
        }

        public void SelectAlignView()
        {
            if (TabAlignViewControl.Visible)
                return;

            lblMainButton.BackColor = _noneSelectColor;
            lblAkkonButton.BackColor = _noneSelectColor;
            lblAlignButton.BackColor = _selectedColor;

            MainViewControl.Visible = false;
            TabAlignViewControl.Visible = true;
            TabAkkonViewControl.Visible = false;

            TabAlignViewControl.Dock = DockStyle.Fill;
            pnlView.Controls.Add(TabAlignViewControl);
        }

        public void SelectAkkonView()
        {
            if (TabAkkonViewControl.Visible)
                return;

            lblMainButton.BackColor = _noneSelectColor;
            lblAkkonButton.BackColor = _selectedColor;
            lblAlignButton.BackColor = _noneSelectColor;

            MainViewControl.Visible = false;
            TabAlignViewControl.Visible = false;
            TabAkkonViewControl.Visible = true;

            TabAkkonViewControl.Dock = DockStyle.Fill;
            pnlView.Controls.Add(TabAkkonViewControl);
        }

        #endregion
    }
}
