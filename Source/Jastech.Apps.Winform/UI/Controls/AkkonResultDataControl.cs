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
    public partial class AkkonResultDataControl : UserControl
    {
        private delegate void UpdateResultDataDelegate();

        public AkkonInspResultControl AkkonInspResultControl { get; private set; } = new AkkonInspResultControl();

        public AkkonResultDataControl()
        {
            InitializeComponent();
        }

        private void AkkonResultControl_Load(object sender, EventArgs e)
        {
            AddControl();
            UpdateDailyDataGridView();
        }

        private void AddControl()
        {
            AkkonInspResultControl.Dock = DockStyle.Fill;
            pnlAkkonResultData.Controls.Add(AkkonInspResultControl);
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
            //AkkonInspResultControl.UpdateAkkonDaily(dailyInfo);

            AkkonInspResultControl.UpdateAkkonDaily();
        }
    }
}
