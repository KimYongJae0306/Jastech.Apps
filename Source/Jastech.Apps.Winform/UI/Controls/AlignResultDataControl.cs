using Jastech.Apps.Winform.Service;
using Jastech.Apps.Winform.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATT_UT_IPAD.UI.Controls
{
    public partial class AlignResultDataControl : UserControl
    {
        private delegate void UpdateResultDataDelegate();

        public AlignInspResultControl AlignInspResultControl { get; private set; } = new AlignInspResultControl();

        public AlignResultDataControl()
        {
            InitializeComponent();
        }

        private void AlignResultControl_Load(object sender, EventArgs e)
        {
            AddControl();
            UpdateDailyDataGridView();
        }

        private void AddControl()
        {
            AlignInspResultControl.Dock = DockStyle.Fill;
            pnlAlignResultData.Controls.Add(AlignInspResultControl);
        }

        public void UpdateResultData()
        {
            if (this.InvokeRequired)
            {
                UpdateResultDataDelegate callback = UpdateResultData;
                BeginInvoke(callback);
                return;
            }

            UpdateDailyDataGridView();
        }

        private void UpdateDailyDataGridView()
        {
            //var dailyInfo = DailyInfoService.GetDailyInfo();
            //AlignInspResultControl.UpdateAlignDaily(dailyInfo);

            AlignInspResultControl.UpdateAlignDaily();
        }
    }
}
