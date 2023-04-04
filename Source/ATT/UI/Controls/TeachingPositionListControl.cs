using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Framework.Structure;

namespace ATT.UI.Controls
{
    public partial class TeachingPositionListControl : UserControl
    {
        #region 필드
        #endregion

        #region 속성
        public string UnitName { get; set; } = string.Empty;
        #endregion

        #region 이벤트
        public event SetTeachingPositionListDelegate SendEventHandler;
        #endregion

        #region 델리게이트
        public delegate void SetTeachingPositionListDelegate(TeachingPosition teachingPosition);
        #endregion

        #region 생성자
        public TeachingPositionListControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void TeachingPositionListControl_Load(object sender, EventArgs e)
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            lblUnit.Text = UnitName;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {

        }

        private void btnStandby_Click(object sender, EventArgs e)
        {

        }

        private void btnPreAlignLeft_Click(object sender, EventArgs e)
        {

        }

        private void btnPreAlignRight_Click(object sender, EventArgs e)
        {

        }

        private void btnScanStart_Click(object sender, EventArgs e)
        {

        }

        private void btnScanEnd_Click(object sender, EventArgs e)
        {
            SendEventHandler(Jastech.Apps.Structure.ModelManager.Instance().CurrentModel.PositionList[(int)Jastech.Apps.Structure.TeachingPositionType.Stage1_Scan_End]);
        }
        #endregion
    }
}
