using ATT.UI.Controls;
using Jastech.Apps.Structure;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Framework.Device.Motions;
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

namespace ATT.UI.Forms
{
    public partial class MotionPopupForm : Form
    {
        #region 필드
        private TeachingPositionListControl TeachingPositionListControl { get; set; } = new TeachingPositionListControl();

        private MotionJogControl MotionJogControl { get; set; } = new MotionJogControl();
        #endregion

        #region 속성
        private AxisHandler selectedAxisHanlder { get; set; } = null;
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        #endregion

        #region 메서드
        public MotionPopupForm()
        {
            InitializeComponent();
        }

        private void MotionPopupForm_Load(object sender, EventArgs e)
        {
            AddControl();
            InitializeUI();
        }

        private void AddControl()
        {
            AddTeachingPositionControl();
            AddParameterControl();
            AddJogControl();
        }

        private void AddTeachingPositionControl()
        {
            TeachingPositionListControl.Dock = DockStyle.Fill;
            TeachingPositionListControl.UnitName = AxisHandlerName.Unit0.ToString();
            pnlTeachingPositionList.Controls.Add(TeachingPositionListControl);
            pnlTeachingPositionList.Dock = DockStyle.Fill;
        }

        private void AddParameterControl()
        {
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;
            tlp.RowCount = selectedAxisHanlder.AxisList.Count;

            for (int rowIndex = 0; rowIndex < tlp.RowCount; rowIndex++)
                tlp.RowStyles.Add(new RowStyle(SizeType.Percent, (float)(100 / tlp.RowCount)));

            foreach (var axis in selectedAxisHanlder.AxisList)
            {
                MotionParameterVariableControl motionParameterVariableControl = new MotionParameterVariableControl();
                motionParameterVariableControl.SetAxis(axis);
                motionParameterVariableControl.Dock = DockStyle.Fill;
                tlp.Controls.Add(motionParameterVariableControl);
            }

            pnlMotionParameter.Controls.Add(tlp);
            pnlMotionParameter.Dock = DockStyle.Fill;
        }

        private void AddJogControl()
        {
            MotionJogControl.Dock = DockStyle.Fill;
            MotionJogControl.SetAxisHanlder(selectedAxisHanlder);
            pnlJog.Controls.Add(MotionJogControl);
            pnlJog.Dock = DockStyle.Fill;
        }

        private void InitializeUI()
        {
            this.Location = new Point(1000, 40);
            this.Size = new Size(720, 800);

            ShowCommandPage();
        }

        public void SetAxisHandler(AxisHandler axisHandler)
        {
            selectedAxisHanlder = axisHandler;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCommand_Click(object sender, EventArgs e)
        {
            ShowCommandPage();
        }

        private void ShowCommandPage()
        {
            pnlJog.Visible = true;
            pnlMotionParameter.Visible = false;
        }

        private void btnParameter_Click(object sender, EventArgs e)
        {
            ShowParameterPage();
        }

        private void ShowParameterPage()
        {
            pnlJog.Visible = false;
            pnlMotionParameter.Visible = true;
        }
    }
    #endregion
}
