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
using Jastech.Framework.Device.Motions;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;

namespace ATT.UI.Controls
{
    public partial class TeachingPositionListControl : UserControl
    {
        #region 필드
        private Color _selectedColor;

        private Color _nonSelectedColor;
        #endregion

        #region 속성
        public UnitName UnitName { get; set; } = UnitName.Unit0;

        public AxisHandler AxisHandler = null;
        #endregion

        #region 이벤트
        public event SetTeachingPositionListDelegate SendEventHandler;
        #endregion

        #region 델리게이트
        public delegate void SetTeachingPositionListDelegate(TeachingPosType teachingPositionType);
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

        public void SetAxisHanlder(AxisHandler axisHandler)
        {
            AxisHandler = axisHandler;
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104,104,104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);
            lblUnit.Text = UnitName.ToString();
            SetButtonStatus(btnStandby);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            //SendEventHandler()
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

        }

        private void btnStandby_Click(object sender, EventArgs e)
        {
            SetButtonStatus(sender);
            SendEventHandler(TeachingPosType.Standby);
        }

        private void btnScanStart_Click(object sender, EventArgs e)
        {
            SetButtonStatus(sender);
            SendEventHandler(TeachingPosType.Stage1_Scan_Start);
        }

        private void btnScanEnd_Click(object sender, EventArgs e)
        {
            SetButtonStatus(sender);
            SendEventHandler(TeachingPosType.Stage1_Scan_End);
        }

        private void SetButtonStatus(object sender)
        {
            Button btn = sender as Button;

            foreach (Button button in tlpTeachingPosition.Controls)
                button.BackColor = _nonSelectedColor;

            btn.BackColor = _selectedColor;
        }
        #endregion
    }
}
