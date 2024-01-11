using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATT_UT_IPAD.UI.Controls;
using Jastech.Apps.Structure.Data;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AlignMonitoringControl : UserControl
    {

        #region 필드
        private double _leftDisplayZoomRatio { get; set; } = 0.1;

        private double _rightDisplayZoomRatio { get; set; } = 0.1;

        private double _centerDisplayZoomRatio { get; set; } = 0.1;
        #endregion

        #region 속성
        private List<AlignResultDisplayControl> DisplayControlList { get; set; } = new List<AlignResultDisplayControl>();

        private List<Panel> TabPanelList { get; set; } = new List<Panel>();

        private AlignResultDisplayControl Tab0DisplayControl { get; set; } = null;

        private AlignResultDisplayControl Tab1DisplayControl { get; set; } = null;

        private AlignResultDisplayControl Tab2DisplayControl { get; set; } = null;

        private AlignResultDisplayControl Tab3DisplayControl { get; set; } = null;

        private AlignResultDisplayControl Tab4DisplayControl { get; set; } = null;

        private AlignResultDisplayControl Tab5DisplayControl { get; set; } = null;

        private AlignResultDisplayControl Tab6DisplayControl { get; set; } = null;

        private AlignResultDisplayControl Tab7DisplayControl { get; set; } = null;
        #endregion

        #region 이벤트
        public event SetTabDelegate SetTabEventHandler;

        public event GetTabInspResultDele GetTabInspResultEvent;
        #endregion

        #region 델리게이트
        public delegate void SetTabDelegate(int tabNo);

        public delegate TabInspResult GetTabInspResultDele(int tabNo);

        public delegate void UpdateRefreshControlDelegate(int tabCount);
        #endregion

        #region 생성자
        public AlignMonitoringControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void AlignMonitoringControl_Load(object sender, EventArgs e)
        {
            AddControl();
        }

        private void AddControl()
        {
            Tab0DisplayControl = new AlignResultDisplayControl();
            Tab0DisplayControl.UseTabFixed = true;
            Tab0DisplayControl.FixedTabIndex = 0;
            Tab0DisplayControl.SendTabNumberEvent += UpdateResultChart;
            Tab0DisplayControl.GetTabInspResultEvent += DisplayControl_GetTabInspResultEvent;
            Tab0DisplayControl.Dock = DockStyle.Fill;
            pnlTab0.Controls.Add(Tab0DisplayControl);

            Tab1DisplayControl = new AlignResultDisplayControl();
            Tab1DisplayControl.UseTabFixed = true;
            Tab1DisplayControl.FixedTabIndex = 1;
            Tab1DisplayControl.SendTabNumberEvent += UpdateResultChart;
            Tab1DisplayControl.GetTabInspResultEvent += DisplayControl_GetTabInspResultEvent;
            Tab1DisplayControl.Dock = DockStyle.Fill;
            pnlTab1.Controls.Add(Tab1DisplayControl);

            Tab2DisplayControl = new AlignResultDisplayControl();
            Tab2DisplayControl.UseTabFixed = true;
            Tab2DisplayControl.FixedTabIndex = 2;
            Tab2DisplayControl.SendTabNumberEvent += UpdateResultChart;
            Tab2DisplayControl.GetTabInspResultEvent += DisplayControl_GetTabInspResultEvent;
            Tab2DisplayControl.Dock = DockStyle.Fill;
            pnlTab2.Controls.Add(Tab2DisplayControl);

            Tab3DisplayControl = new AlignResultDisplayControl();
            Tab3DisplayControl.UseTabFixed = true;
            Tab3DisplayControl.FixedTabIndex = 3;
            Tab3DisplayControl.SendTabNumberEvent += UpdateResultChart;
            Tab3DisplayControl.GetTabInspResultEvent += DisplayControl_GetTabInspResultEvent;
            Tab3DisplayControl.Dock = DockStyle.Fill;
            pnlTab3.Controls.Add(Tab3DisplayControl);

            Tab4DisplayControl = new AlignResultDisplayControl();
            Tab4DisplayControl.UseTabFixed = true;
            Tab4DisplayControl.FixedTabIndex = 4;
            Tab4DisplayControl.SendTabNumberEvent += UpdateResultChart;
            Tab4DisplayControl.GetTabInspResultEvent += DisplayControl_GetTabInspResultEvent;
            Tab4DisplayControl.Dock = DockStyle.Fill;
            pnlTab4.Controls.Add(Tab4DisplayControl);

            Tab5DisplayControl = new AlignResultDisplayControl();
            Tab5DisplayControl.UseTabFixed = true;
            Tab5DisplayControl.FixedTabIndex = 5;
            Tab5DisplayControl.SendTabNumberEvent += UpdateResultChart;
            Tab5DisplayControl.GetTabInspResultEvent += DisplayControl_GetTabInspResultEvent;
            Tab5DisplayControl.Dock = DockStyle.Fill;
            pnlTab5.Controls.Add(Tab5DisplayControl);

            Tab6DisplayControl = new AlignResultDisplayControl();
            Tab6DisplayControl.UseTabFixed = true;
            Tab6DisplayControl.FixedTabIndex = 6;
            Tab6DisplayControl.SendTabNumberEvent += UpdateResultChart;
            Tab6DisplayControl.GetTabInspResultEvent += DisplayControl_GetTabInspResultEvent;
            Tab6DisplayControl.Dock = DockStyle.Fill;
            pnlTab6.Controls.Add(Tab6DisplayControl);

            Tab7DisplayControl = new AlignResultDisplayControl();
            Tab7DisplayControl.UseTabFixed = true;
            Tab7DisplayControl.FixedTabIndex = 7;
            Tab7DisplayControl.SendTabNumberEvent += UpdateResultChart;
            Tab7DisplayControl.GetTabInspResultEvent += DisplayControl_GetTabInspResultEvent;
            Tab7DisplayControl.Dock = DockStyle.Fill;
            pnlTab7.Controls.Add(Tab7DisplayControl);

            DisplayControlList.Add(Tab0DisplayControl);
            DisplayControlList.Add(Tab1DisplayControl);
            DisplayControlList.Add(Tab2DisplayControl);
            DisplayControlList.Add(Tab3DisplayControl);
            DisplayControlList.Add(Tab4DisplayControl);
            DisplayControlList.Add(Tab5DisplayControl);
            DisplayControlList.Add(Tab6DisplayControl);
            DisplayControlList.Add(Tab7DisplayControl);

            TabPanelList.Add(pnlTab0);
            TabPanelList.Add(pnlTab1);
            TabPanelList.Add(pnlTab2);
            TabPanelList.Add(pnlTab3);
            TabPanelList.Add(pnlTab4);
            TabPanelList.Add(pnlTab5);
            TabPanelList.Add(pnlTab6);
            TabPanelList.Add(pnlTab7);
        }

        public void UpdateMainResult(int tabNo)
        {
            var display = DisplayControlList.Where(x => x.FixedTabIndex == tabNo).FirstOrDefault();
            DisplayControlList.ForEach(x => x.SetRatio(_leftDisplayZoomRatio, _centerDisplayZoomRatio, _rightDisplayZoomRatio));

            display?.UpdateResultDisplay(tabNo);
        }

        private TabInspResult DisplayControl_GetTabInspResultEvent(int tabNo)
        {
            if (GetTabInspResultEvent == null)
                return null;
            return GetTabInspResultEvent.Invoke(tabNo);
        }

        private void UpdateResultChart(int tabNo)
        {
            SetTabEventHandler?.Invoke(tabNo);
        }

        public void Enable(bool isEnable)
        {
            BeginInvoke(new Action(() => Enabled = isEnable));
        }

        public void UpdateRefreshControl(int tabCount)
        {
            if (this.InvokeRequired)
            {
                UpdateRefreshControlDelegate callback = UpdateRefreshControl;
                BeginInvoke(callback, tabCount);
                return;
            }

            RefreshControl(tabCount);
        }

        public void UpdateResultTabButton(int tabNo)
        {
            var display = DisplayControlList.Where(x => x.FixedTabIndex == tabNo).FirstOrDefault();

            display?.UpdateResultTabButton(tabNo);
        }

        public void TabButtonResetColor()
        {
            DisplayControlList.ForEach(x => x.TabButtonResetColor());
        }

        private void RefreshControl(int tabCount)
        {
            DisplayControlList.ForEach(x => x.UpdateTabButtons(tabCount));

            for (int i = 0; i < DisplayControlList.Count(); i++)
            {
                var display = DisplayControlList[i];

                display.UpdateTabButtons(1);

                if (i <= tabCount - 1)
                    display.Visible = true;
                else
                    display.Visible = false;
            }
        }

        private void UpdatePanelView(int tabIndex, bool visible)
        {
            if (tabIndex == 0)
                pnlTab0.Visible = visible;
            else if (tabIndex == 1)
                pnlTab1.Visible = visible;
            else if (tabIndex == 2)
                pnlTab2.Visible = visible;
            else if (tabIndex == 3)
                pnlTab3.Visible = visible;
            else if (tabIndex == 4)
                pnlTab4.Visible = visible;
            else if (tabIndex == 5)
                pnlTab5.Visible = visible;
            else if (tabIndex == 6)
                pnlTab6.Visible = visible;
            else if (tabIndex == 7)
                pnlTab7.Visible = visible;
        }

        #endregion
    }
}
