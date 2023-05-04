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

        private Color _noneSelectedColor;
        #endregion

        #region 속성
        public string UnitName { get; set; } = string.Empty;

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
            _noneSelectedColor = Color.FromArgb(52, 52, 52);
            lblUnit.Text = UnitName;
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

        private void btnPreAlignLeft_Click(object sender, EventArgs e)
        {
            SetButtonStatus(sender);
            SendEventHandler(TeachingPosType.Stage1_PreAlign_Left);
        }

        private void btnPreAlignRight_Click(object sender, EventArgs e)
        {
            SetButtonStatus(sender);
            SendEventHandler(TeachingPosType.Stage1_PreAlign_Right);
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

            btnStandby.BackColor = _noneSelectedColor;
            btnPreAlignLeft.BackColor = _noneSelectedColor;
            btnPreAlignRight.BackColor = _noneSelectedColor;
            btnScanStart.BackColor = _noneSelectedColor;
            btnScanEnd.BackColor = _noneSelectedColor;

            btn.BackColor = _selectedColor;
        }
        #endregion
    }
}
