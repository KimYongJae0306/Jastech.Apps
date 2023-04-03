using Jastech.Apps.Structure;
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

namespace Jastech.Apps.Winform.UI.Forms
{
    public partial class MotionPopupForm : Form
    {
        #region 필드
        private TeachingPositionControl TeachingPositionControl { get; set; } = new TeachingPositionControl();
        private MotionJogControl MotionJogControl { get; set; } = new MotionJogControl();
        #endregion

        #region 속성
        private AxisHandler selectedAxisHanlder { get; set; } = null;
        #endregion

        #region 이벤트
        public Action CloseEventDelegate;
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
            TeachingPositionControl.Dock = DockStyle.Fill;
            pnlTeachingPositionList.Controls.Add(TeachingPositionControl);
            pnlTeachingPositionList.Dock = DockStyle.Fill;

            //tlpTeachingList.ColumnStyles.Clear();
            //tlpTeachingList.RowStyles.Clear();
            //tlpTeachingList.ColumnCount = Enum.GetValues(typeof(TeachingPositionType)).Length;
            //tlpTeachingList.Dock = DockStyle.Fill;

            //for (int columnIndex = 0; columnIndex < tlpTeachingList.ColumnCount; columnIndex++)
            //{
            //    tlpTeachingList.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, (float)(100 / tlpTeachingList.ColumnCount)));
            //    tlpTeachingList.Controls.Add(CreateTeachingPositionButton(ModelManager.Instance().CurrentModel.PositionList[columnIndex].Description), columnIndex, 0);
            //}
        }

        private Button CreateTeachingPositionButton(string description)
        {
            Button btn = new Button() { BackColor = Color.White, Dock = DockStyle.Fill, Text = description };
            btn.Click += Btn_Click;
            return btn;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
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
                MotionPopupParameterControl motionPopupParameterControl = new MotionPopupParameterControl();
                motionPopupParameterControl.SetAxis(axis);
                motionPopupParameterControl.Dock = DockStyle.Fill;
                tlp.Controls.Add(motionPopupParameterControl);
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

        private void MakeTeachingListControl()
        {

        }

        private void MotionPopupForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CloseEventDelegate != null)
                CloseEventDelegate();
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
