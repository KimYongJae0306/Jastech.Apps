using ATT.UI.Controls;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Framework.Winform.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATT.UI.Pages
{
    public partial class LogPage : UserControl
    {
        private Color _selectedColor;

        private Color _nonSelectedColor;

        private UPHControl UPHControl { get; set; } = new UPHControl() { Dock = DockStyle.Fill };

        private TrendViewControl TrendControl { get; set; } = new TrendViewControl() { Dock = DockStyle.Fill };

        public LogPage()
        {
            InitializeComponent();
        }

        private void LogPage_Load(object sender, EventArgs e)
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);
        }

        private void lblTrend_Click(object sender, EventArgs e)
        {
            SelectHistoryType(HistoryType.Trend);
        }

        private void lblUPH_Click(object sender, EventArgs e)
        {
            SelectHistoryType(HistoryType.UPH);
        }

        private void SelectHistoryType(HistoryType historyType)
        {
            ClearSelectedButton();
            pnlContents.Controls.Clear();

            switch (historyType) 
            {
                case HistoryType.Trend:
                    pnlContents.Controls.Add(TrendControl);
                    lblTrend.BackColor = _selectedColor;
                    break;

                case HistoryType.UPH:
                    pnlContents.Controls.Add(UPHControl);
                    lblUPH.BackColor = _selectedColor;
                    break;
            }
        }

        private void ClearSelectedButton()
        {
            foreach (Control control in pnlHisotryType.Controls)
            {
                if (control is Label)
                    control.BackColor = _nonSelectedColor;
            }
        }
    }

    public enum HistoryType
    {
        Trend,
        UPH,
    }
}
