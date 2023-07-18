using Jastech.Framework.Winform.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ATT.UI.Controls
{
    public partial class TrendViewControl : UserControl
    {
        #region 속성
        private List<TrendControl> TrendConrolList { get; set; } = new List<TrendControl>();
        #endregion
        
        #region 생성자
        public TrendViewControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void TrendControl_Load(object sender, EventArgs e)
        {
            AddControl();
        }

        private void AddControl()
        {
            TrendControl t1 = new TrendControl();
            this.tlpTrend.Controls.Add(t1, 0, 0);
        }
        #endregion
    }
}
