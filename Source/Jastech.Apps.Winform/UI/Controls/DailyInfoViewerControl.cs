﻿using Jastech.Apps.Winform.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Jastech.Apps.Winform.UI.Controls.ResultChartControl;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class DailyInfoViewerControl : UserControl
    {
        #region 필드
        private Color _selectedColor = new Color();

        private Color _nonSelectedColor = new Color();
        #endregion

        #region 속성
        public AkkonResultDataControl AkkonResultDataControl { get; set; } = null;

        public AlignResultDataControl AlignResultDataControl { get; set; } = null;
        #endregion

        #region 생성자
        public DailyInfoViewerControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void DailyInfoViewerControl_Load(object sender, EventArgs e)
        {
            AddControls();
            InitializeUI();
        }
        
        private void AddControls()
        {
            AkkonResultDataControl = new AkkonResultDataControl() { Dock = DockStyle.Fill};
            pnlDailyResult.Controls.Add(AkkonResultDataControl);
            AkkonResultDataControl.RefreshData();

            AlignResultDataControl = new AlignResultDataControl() { Dock = DockStyle.Fill };
            pnlDailyResult.Controls.Add(AlignResultDataControl);
            AlignResultDataControl.RefreshData();
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            ShowAlignDailyInfo();
        }

        private void ShowAkkonDailyInfo()
        {
            if (lblAkkon.BackColor != _selectedColor)
            {
                lblAkkon.BackColor = _selectedColor;
                lblAlign.BackColor = _nonSelectedColor;

                pnlDailyResult.Controls.Clear();
                pnlDailyResult.Controls.Add(AkkonResultDataControl);
            }
        }

        private void ShowAlignDailyInfo()
        {
            if (lblAlign.BackColor != _selectedColor)
            {
                lblAkkon.BackColor = _nonSelectedColor;
                lblAlign.BackColor = _selectedColor;

                pnlDailyResult.Controls.Clear();
                pnlDailyResult.Controls.Add(AlignResultDataControl);
            }
        }

        private void lblAkkon_Click(object sender, EventArgs e)
        {
            ShowAkkonDailyInfo();
        }

        private void lblAlign_Click(object sender, EventArgs e)
        {
            ShowAlignDailyInfo();
        }

        public void UpdateAkkonChart(int tabNo)
        {
            ShowAkkonDailyInfo();
            if (lblAkkon.BackColor == _selectedColor)
                AkkonResultDataControl.UpdateAkkonDaily(tabNo);
        }

        public void UpdateAlignChart(int tabNo)
        {
            ShowAlignDailyInfo();
            if (lblAlign.BackColor == _selectedColor)
                AlignResultDataControl.UpdateAlignDaily(tabNo);
        }
       
        public void UpdateAllRefreshData()
        {
            AkkonResultDataControl.RefreshData();
            AlignResultDataControl.RefreshData();
        }

        public void Enable(bool isEnable)
        {
            BeginInvoke(new Action(() =>
            {
                AkkonResultDataControl.Enabled = isEnable;
                AlignResultDataControl.Enabled = isEnable;
            }));
        }
        #endregion
    }
}
