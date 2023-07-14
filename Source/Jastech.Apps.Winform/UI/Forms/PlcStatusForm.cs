using Jastech.Apps.Winform.UI.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Forms
{
    public partial class PlcStatusForm : Form
    {
        #region 필드
        private Color _selectedColor = new Color();

        private Color _nonSelectedColor = new Color();

        private bool _isLoading { get; set; } = false;

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
        public PlcCommandControl PlcCommandControl { get; set; } = null;

        public PlcModelInfoControl PlcModelInfoControl { get; set; } = null;
        #endregion

        #region 델리게이트
        public Action CloseEventDelegate;
        #endregion

        #region 생성자
        public PlcStatusForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void PlcStatusForm_Load(object sender, EventArgs e)
        {
            _isLoading = true;
            AddControls();
            InitializeUI();
            UpdateTimer.Start();

            _isLoading = false;
        }

        private void AddControls()
        {
            PlcCommandControl = new PlcCommandControl();
            PlcCommandControl.Dock = DockStyle.Fill;

            PlcModelInfoControl = new PlcModelInfoControl();
            PlcModelInfoControl.Dock = DockStyle.Fill;
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            SelectDataPage();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if(PlcCommandControl != null)
            {
                if (PlcCommandControl.Visible)
                    PlcCommandControl?.UpdateData();
            }

            if (PlcModelInfoControl != null)
            {
                if (PlcModelInfoControl.Visible)
                    PlcModelInfoControl?.UpdateData();
            }
        }

        private void btnDataPage_Click(object sender, EventArgs e)
        {
            SelectDataPage();
        }

        private void SelectDataPage()
        {
            PlcCommandControl.Visible = true;
            PlcModelInfoControl.Visible = false;

            btnDataPage.BackColor = _selectedColor;
            btnModelPage.BackColor = _nonSelectedColor;

            pnlDisplay.Controls.Clear();
            pnlDisplay.Controls.Add(PlcCommandControl);
        }

        private void SelectModelPage()
        {
            PlcCommandControl.Visible = false;
            PlcModelInfoControl.Visible = true;

            btnDataPage.BackColor = _nonSelectedColor; 
            btnModelPage.BackColor = _selectedColor;

            pnlDisplay.Controls.Clear();
            pnlDisplay.Controls.Add(PlcModelInfoControl);
        }

        private void btnModelPage_Click(object sender, EventArgs e)
        {
            SelectModelPage();
        }

        private void PlcStatusForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateTimer.Stop();

            if (UpdateTimer != null)
            {
                UpdateTimer.Dispose();
                UpdateTimer = null;
            }
        }

        private void PlcStatusForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CloseEventDelegate != null)
                CloseEventDelegate();
        }
        #endregion
    }
}
