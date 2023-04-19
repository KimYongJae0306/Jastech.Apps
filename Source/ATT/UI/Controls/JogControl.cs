using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Jastech.Framework.Device.Motions.AxisMovingParam;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Winform.Forms;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Winform.Controls;

namespace ATT.UI.Controls
{
    public partial class JogControl : UserControl
    {
        #region 필드
        private JogSpeedMode _jogSpeedMode = JogSpeedMode.Slow;

        private JogMode _jogMode = JogMode.Jog;

        private Color _selectedColor;

        private Color _noneSelectedColor;
        #endregion

        #region 속성
        private AxisHandler AxisHanlder { get; set; } = null;

        public double JogPitchXY { get; set; } = 1.0;
        public double JogPitchZ { get; set; } = 0.1;
        private MotionJogControl MotionJogControl { get; set; } = new MotionJogControl() { Dock = DockStyle.Fill };
        private LAFJogControl LAFJogControl { get; set; } = new LAFJogControl() { Dock = DockStyle.Fill };

        public LAFCtrl LAFCtrl { get; set; } = null;
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public JogControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void MotionJogControl_Load(object sender, EventArgs e)
        {
            AddControl();
            InitializeUI();
        }

        private void AddControl()
        {
            pnlMotionJog.Controls.Add(MotionJogControl);

            LAFJogControl.SetSelectedLafCtrl(LAFCtrl);
            pnlLAFJog.Controls.Add(LAFJogControl);
        }
        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _noneSelectedColor = Color.FromArgb(52, 52, 52);

            rdoJogSlowMode.Checked = true;
            rdoJogMode.Checked = true;
        }

        public void SetAxisHanlder(AxisHandler axisHandler, LAFCtrl lafCtrl)
        {
            AxisHanlder = axisHandler;
            LAFCtrl = lafCtrl;
        }

        private void SetSelectJogSpeedMode(JogSpeedMode jogSpeedMode)
        {
            _jogSpeedMode = jogSpeedMode;
        }

        private void SetSelectJogMode(JogMode jogMode)
        {
            _jogMode = jogMode;
        }

        private void rdoJogSlowMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoJogSlowMode.Checked)
            {
                SetSelectJogSpeedMode(JogSpeedMode.Slow);
                rdoJogSlowMode.BackColor = _selectedColor;
            }
            else
                rdoJogSlowMode.BackColor = _noneSelectedColor;
        }

        private void rdoJogFastMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoJogFastMode.Checked)
            {
                SetSelectJogSpeedMode(JogSpeedMode.Fast);
                rdoJogFastMode.BackColor = _selectedColor;
            }
            else
                rdoJogFastMode.BackColor = _noneSelectedColor;
        }

        private void rdoJogMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoJogMode.Checked)
            {
                SetSelectJogMode(JogMode.Jog);
                rdoJogMode.BackColor = _selectedColor;
            }
            else
                rdoJogMode.BackColor = _noneSelectedColor;
        }

        private void rdoIncreaseMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoIncreaseMode.Checked)
            {
                SetSelectJogMode(JogMode.Increase);
                rdoIncreaseMode.BackColor = _selectedColor;
            }
            else
                rdoIncreaseMode.BackColor = _noneSelectedColor;
        }

        private void lblPitchXYValue_Click(object sender, EventArgs e)
        {
            SetLabelDoubleData(sender);
        }

        private void lblPitchZValue_Click(object sender, EventArgs e)
        {
            SetLabelDoubleData(sender);
        }

        private double SetLabelDoubleData(object sender)
        {
            Label lbl = sender as Label;
            double prevData = Convert.ToDouble(lbl.Text);

            KeyPadForm keyPadForm = new KeyPadForm();
            keyPadForm.PreviousValue = prevData;
            keyPadForm.ShowDialog();

            double inputData = keyPadForm.PadValue;

            Label label = (Label)sender;
            label.Text = inputData.ToString();

            return inputData;
        }

        private void btnJogLeftX_MouseDown(object sender, MouseEventArgs e)
        {
            Axis axis = AxisHanlder.GetAxis(AxisName.X);
            MoveJog(axis, Direction.CCW);
        }

        private void btnJogLeftX_MouseUp(object sender, MouseEventArgs e)
        {
            if (AxisHanlder == null)
                return;

            AxisHanlder.GetAxis(AxisName.X).StopMove();
        }

        private void btnJogRightX_MouseDown(object sender, MouseEventArgs e)
        {
            Axis axis = AxisHanlder.GetAxis(AxisName.X);
            MoveJog(axis, Direction.CW);
        }

        private void btnJogRightX_MouseUp(object sender, MouseEventArgs e)
        {
            if (AxisHanlder == null)
                return;

            AxisHanlder.GetAxis(AxisName.X).StopMove();
        }

        private void btnJogDownY_MouseDown(object sender, MouseEventArgs e)
        {
            Axis axis = AxisHanlder.GetAxis(AxisName.Y);
            MoveJog(axis, Direction.CW);
        }

        private void btnJogDownY_MouseUp(object sender, MouseEventArgs e)
        {
            if (AxisHanlder == null)
                return;

            AxisHanlder.GetAxis(AxisName.Y).StopMove();
        }

        private void btnJogUpY_MouseDown(object sender, MouseEventArgs e)
        {
            Axis axis = AxisHanlder.GetAxis(AxisName.Y);
            MoveJog(axis, Direction.CCW);
        }

        private void btnJogUpY_MouseUp(object sender, MouseEventArgs e)
        {
            if (AxisHanlder == null)
                return;

            AxisHanlder.GetAxis(AxisName.Y).StopMove();
        }

        private void MoveJog(Axis axis, Direction direction)
        {
            if (AxisHanlder == null)
                return;

            if(rdoJogMode.Checked)
            {
                double currentPosition = axis.GetActualPosition();
                double targetPosition = 0.0;
                if (direction == Direction.CW)
                    targetPosition = currentPosition - Convert.ToDouble(lblPitchXYValue.Text);
                else if (direction == Direction.CCW)
                    targetPosition = currentPosition + Convert.ToDouble(lblPitchXYValue.Text);

                axis.MoveTo(targetPosition);
            }
            else if(rdoIncreaseMode.Checked)
            {
                axis.JogMove(direction);
            }
        }
        #endregion
    }
}
