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
        private const int MaxTabCount = 8;

        private double _leftDisplayZoomRatio { get; set; } = 0.1;

        private double _rightDisplayZoomRatio { get; set; } = 0.1;

        private double _centerDisplayZoomRatio { get; set; } = 0.2;
        #endregion

        #region 속성
        private List<AlignResultDisplayControl> DisplayControlList { get; set; } = new List<AlignResultDisplayControl>();

        private List<Panel> TabPanelList { get; set; } = new List<Panel>();
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
            for (int tabNo = 0; tabNo < MaxTabCount; tabNo++)
            {
                Panel panel = new Panel();
                panel.Margin = new Padding(0);
                panel.Dock = DockStyle.Fill;

                AlignResultDisplayControl displayControl = new AlignResultDisplayControl();
                displayControl.UseTabFixed = true;
                displayControl.FixedTabIndex = tabNo;
                displayControl.SendTabNumberEvent += UpdateResultChart;
                displayControl.GetTabInspResultEvent += DisplayControl_GetTabInspResultEvent;
                displayControl.Dock = DockStyle.Fill;
                panel.Controls.Add(displayControl);

                TabPanelList.Add(panel);
                DisplayControlList.Add(displayControl);
            }
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
            CreateTableLayout(tabCount);
            RefreshControl(tabCount);
        }

        private void CreateTableLayout(int tabCount)
        {
            tlpSplitView.RowStyles.Clear();
            tlpSplitView.ColumnStyles.Clear();

            if (tabCount <= 0)
                return;

            if(tabCount <= 2)
            {
                tlpSplitView.ColumnCount = 1;
                tlpSplitView.RowCount = tabCount;
            }
            else
            {
                tlpSplitView.ColumnCount = 2;
                var remain = tabCount % 2.0;


                if (remain == 0)
                    tlpSplitView.RowCount = (tabCount / 2);
                else
                    tlpSplitView.RowCount = (tabCount / 2) + 1;
            }

            for (int rowIndex = 0; rowIndex < tlpSplitView.RowCount; rowIndex++)
                tlpSplitView.RowStyles.Add(new RowStyle(SizeType.Percent, (float)(100 / tlpSplitView.RowCount)));

            for (int columnIndex = 0; columnIndex < tlpSplitView.ColumnCount; columnIndex++)
                tlpSplitView.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, (float)(100 / tlpSplitView.ColumnCount)));

            for (int i = 0; i < DisplayControlList.Count(); i++)
            {
                var display = DisplayControlList[i];

                display.UpdateTabButtons(1);

                if (i <= tabCount - 1)
                    display.Visible = true;
                else
                    display.Visible = false;

                tlpSplitView.Controls.Add(display);
            }
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
        #endregion
    }
}
