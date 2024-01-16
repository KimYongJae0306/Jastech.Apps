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
using Jastech.Framework.Winform.Forms;
using Jastech.Apps.Structure;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Structure.Data;
using ATT.Core.Data;

namespace ATT.UI.Controls
{
    public partial class MainViewControl : UserControl
    {
        #region 필드
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
        public AkkonViewerControl AkkonViewerControl { get; set; } = null;

        public AlignViewerControl AlignViewerControl { get; set; } = null;

        public DailyInfoViewerControl DailyInfoViewerControl { get; set; } = null;

        public SystemLogControl SystemLogControl { get; set; } = null;
        #endregion

        #region 생성자
        public MainViewControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void MainViewControl_Load(object sender, EventArgs e)
        {
            AddControls();
        }

        private void AddControls()
        {
            DailyInfoViewerControl = new DailyInfoViewerControl();
            DailyInfoViewerControl.Dock = DockStyle.Fill;
            pnlDailyInfo.Controls.Add(DailyInfoViewerControl);

            AkkonViewerControl = new AkkonViewerControl();
            AkkonViewerControl.Dock = DockStyle.Fill;
            AkkonViewerControl.SetTabEventHandler += AkkonViewerControl_SetTabEventHandler;
            AkkonViewerControl.GetTabInspResultEvent += AkkonViewerControl_GetTabInspResultEvent;
            pnlAkkon.Controls.Add(AkkonViewerControl);

            AlignViewerControl = new AlignViewerControl();
            AlignViewerControl.Dock = DockStyle.Fill;
            AlignViewerControl.SetTabEventHandler += AlignViewerControl_SetTabEventHandler;
            AlignViewerControl.GetTabInspResultEvent += AlignViewerControl_GetTabInspResultEvent;
            pnlAlign.Controls.Add(AlignViewerControl);

            SystemLogControl = new SystemLogControl();
            SystemLogControl.Dock = DockStyle.Fill;
            pnlSystemLog.Controls.Add(SystemLogControl);
        }

        private TabInspResult AkkonViewerControl_GetTabInspResultEvent(int tabNo)
        {
            return AppsInspResult.Instance().GetAkkon(tabNo);
        }

        private TabInspResult AlignViewerControl_GetTabInspResultEvent(int tabNo)
        {
            return AppsInspResult.Instance().GetAlign(tabNo);
        }

        private void AkkonViewerControl_SetTabEventHandler(int tabNo)
        {
            //DailyInfoViewerControl.UpdateAkkonResult(tabNo);
        }

        private void AlignViewerControl_SetTabEventHandler(int tabNo)
        {
            //DailyInfoViewerControl.UpdateAlignResult(tabNo);
        }

        public void UpdateTabCount(int tabCount)
        {
           // AkkonViewerControl.UpdateTabCount(tabCount);
           // AlignViewerControl.UpdateTabCount(tabCount);
        }

        public void UpdateMainAkkonResultDisplay(int tabNo)
        {
            AkkonViewerControl.UpdateMainResult(tabNo);
        }

        public void UpdateMainAkkonResultData(int tabNo)
        {
            AkkonViewerControl.UpdateMainResult(tabNo);
           // DailyInfoViewerControl.UpdateAkkonResult(tabNo);
        }

        public void UpdateMainAlignResult(int tabNo)
        {
            AlignViewerControl.UpdateMainResult(tabNo);
            //DailyInfoViewerControl.UpdateAlignResult(tabNo);
        }

        public void UpdateAkkonResultTabButton(int tabNo)
        {
            AkkonViewerControl.UpdateResultTabButton(tabNo);
        }

        public void UpdateAlignResultTabButton(int tabNo)
        {
            AlignViewerControl.UpdateResultTabButton(tabNo);
        }

        public void TabButtonResetColor()
        {
            AkkonViewerControl.TabButtonResetColor();
            AlignViewerControl.TabButtonResetColor();
        }

        public void AddSystemLogMessage(string logMessage)
        {
            SystemLogControl.AddLogMessage(logMessage);
        }

        public void Enable(bool isEnable)
        {
            AkkonViewerControl.Enable(isEnable);
            AlignViewerControl.Enable(isEnable);
        }
        #endregion
    }
}
