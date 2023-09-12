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

        //public TabAlignViewControl TabAlignViewControl { get; set; } = null;

        //public TabAkkonViewControl TabAkkonViewControl { get; set; } = null;
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
        }

        private void lblStart_Click(object sender, EventArgs e)
        {
            if (ModelManager.Instance().CurrentModel == null)
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "Current Model is null.";
                form.ShowDialog();
                return;
            }

            SystemManager.Instance().StartRun();
        }

        private void lblStop_Click(object sender, EventArgs e)
        {
            SystemManager.Instance().StopRun();
        }

        public void UpdateButton()
        {
            if (PlcControlManager.Instance().MachineStatus == MachineStatus.RUN)
            {
                lblStartText.ForeColor = Color.Blue;
                lblStopText.ForeColor = Color.White;
            }
            else
            {
                lblStartText.ForeColor = Color.White;
                lblStopText.ForeColor = Color.Blue;
            }
        }

        public void Enable(bool isEnable)
        {
            MainViewControl.Enable(isEnable);
        }
        #endregion
    }
}
