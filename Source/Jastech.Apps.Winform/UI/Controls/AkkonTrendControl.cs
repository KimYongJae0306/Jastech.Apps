using Jastech.Framework.Winform.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AkkonTrendControl : UserControl
    {
        #region 필드
        private Color _selectedColor;

        private Color _nonSelectedColor;

        private List<Label> _tabLabelList = new List<Label>();

        private TabType _tabType { get; set; } = TabType.Tab1;

        private AkkonResultType _akkonResultType { get; set; } = AkkonResultType.All;

        private ResultChartControl ChartControl = new ResultChartControl() { Dock = DockStyle.Fill };
        #endregion

        #region 속성
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        #endregion

        #region 메서드
        #endregion

        public AkkonTrendControl()
        {
            InitializeComponent();
        }
    }

    public enum AkkonResultType
    {
        All,
        Lx,
        Ly,
        Cx,
        Rx,
        Ry,
    }


}
