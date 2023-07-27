using ATT.UI.Controls;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Core;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ATT.UI.Pages
{
    public partial class MainPage : UserControl
    {
        #region 필드
        #endregion

        #region 속성
        //public AkkonInspDisplayControl AkkonInspControl { get; private set; } = new AkkonInspDisplayControl();

        //public AlignInspDisplayControl AlignInspControl { get; private set; } = new AlignInspDisplayControl();

        public AkkonViewerControl AkkonViewerControl { get; set; } = new AkkonViewerControl() { Dock = DockStyle.Fill };
        public AlignViewerControl AlignViewerControl { get; set; } = new AlignViewerControl() { Dock = DockStyle.Fill };

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
            //AkkonInspControl.Dock = DockStyle.Fill;
            //pnlAkkon.Controls.Add(AkkonInspControl);

            //AlignInspControl.Dock = DockStyle.Fill;
            //pnlAlign.Controls.Add(AlignInspControl);

            pnlAkkon.Controls.Add(AkkonViewerControl);
            pnlAlign.Controls.Add(AlignViewerControl);
        }

        public void UpdateTabCount(int tabCount)
        {
            //AkkonInspControl.UpdateTabCount(tabCount);
            //AlignInspControl.UpdateTabCount(tabCount);

            AkkonViewerControl.UpdateTabCount(tabCount);
            AlignViewerControl.UpdateTabCount(tabCount);
        }

        public void UpdateMainResult(AppsInspResult result)
        {
            //AkkonInspControl.UpdateMainResult(result);
            //AlignInspControl.UpdateMainResult(result);

            AkkonViewerControl.UpdateMainResult(result);
            AlignViewerControl.UpdateMainResult(result);
        }

        //public void InitializeResult(int tabCount)
        //{
        //    AlignInspControl.InitalizeResultData(tabCount);
        //}
        #endregion

        private void lblStart_Click(object sender, EventArgs e)
        {
            SystemManager.Instance().StartRun();
        }

        private void lblStop_Click(object sender, EventArgs e)
        {
            SystemManager.Instance().StopRun();
        }

        public void UpdateButton()
        {
            if (SystemManager.Instance().MachineStatus == MachineStatus.RUN)
            {
                lblStartText.ForeColor = Color.Blue;
                lblStopText.ForeColor = Color.White;
                //lblStart.Enabled = false;
                //lblStartText.Enabled = false;

                //lblStop.Enabled = true;
                //lblStopText.Enabled = true;

              
            }
            else
            {
                lblStartText.ForeColor = Color.White;
                lblStopText.ForeColor = Color.Blue;

                //lblStart.Enabled = true;
                //lblStartText.Enabled = true;

                //lblStop.Enabled = false;
                //lblStopText.Enabled = false;

                
            }
        }
    }
}
