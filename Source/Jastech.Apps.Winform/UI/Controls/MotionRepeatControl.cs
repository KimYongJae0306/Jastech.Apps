using Jastech.Framework.Device.Motions;
using Jastech.Framework.Winform.Helper;
using System;
using System.Drawing;
using System.Windows.Forms;
using static Jastech.Framework.Device.Motions.AxisMovingParam;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class MotionRepeatControl : UserControl
    {
        #region 필드
        private bool _isLoading { get; set; } = false;

        private Color _selectedColor;

        private Color _nonSelectedColor;

        private System.Threading.Thread _repeatThread = null;

        private bool _isRepeat = false;

        private bool _isInfinite = false;

        private int _remainCount = 0;
        #endregion

        #region 속성
        private Axis SelectedAxis { get; set; } = null;

        private AxisHandler AxisHandler { get; set; } = null;

        private Direction _direction = Direction.CW;
        #endregion

        #region 생성자
        public MotionRepeatControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        public void SetAxisHandler(AxisHandler axisHandler)
        {
            AxisHandler = axisHandler;
            SetAxis(AxisHandler);
            InitializeUI();
        }

        private void SetAxis(AxisHandler axisHandler)
        {
            SelectedAxis = axisHandler.GetAxis(AxisName.X);
        }

        private void InitializeUI()
        {
            lblOperationAxis.Text = "Axis " + SelectedAxis.Name;

            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            SetScanDirection(Direction.CW);
        }

        private void lblRepeatVelocityValue_Click(object sender, EventArgs e)
        {
            KeyPadHelper.SetLabelDoubleData((Label)sender);
        }

        private void lblRepeatAccelerationValue_Click(object sender, EventArgs e)
        {
            KeyPadHelper.SetLabelDoubleData((Label)sender);
        }

        private void lblDwellTimeValue_Click(object sender, EventArgs e)
        {
            KeyPadHelper.SetLabelIntegerData((Label)sender);
        }

        private void lblFoward_Click(object sender, EventArgs e)
        {
            SetScanDirection(Direction.CW);
        }

        private void lblBackward_Click(object sender, EventArgs e)
        {
            SetScanDirection(Direction.CCW);
        }

        private void SetScanDirection(Direction direction)
        {
            if (direction == Direction.CW)
            {
                lblForward.BackColor = _selectedColor;
                lblBackward.BackColor = _nonSelectedColor;
            }
            else
            {
                lblForward.BackColor = _nonSelectedColor;
                lblBackward.BackColor = _selectedColor;
            }

            _direction = direction;
        }

        private void lblScanXLength_Click(object sender, EventArgs e)
        {
            KeyPadHelper.SetLabelDoubleData((Label)sender);
        }


        private void lblRepeatCount_Click(object sender, EventArgs e)
        {
            KeyPadHelper.SetLabelIntegerData((Label)sender);
        }

        public double GetScanLength()
        {
            return Convert.ToDouble(lblScanXLength.Text);
        }

        private void MoveRepeat(bool isRepeat)
        {
            if (isRepeat)
            {
                double currentPosition = SelectedAxis.GetActualPosition();
                double distance = Convert.ToDouble(lblScanXLength.Text);
                double targetPosition = 0.0;

                if (_direction == Direction.CW)
                    targetPosition = currentPosition + distance;
                else
                    targetPosition = currentPosition - distance;

                RepeatParam param = new RepeatParam();

                param.Velocity = Convert.ToDouble(lblRepeatVelocityValue.Text);
                param.AccDec = Convert.ToDouble(lblRepeatAccelerationValue.Text);
                param.DwellTime = Convert.ToInt16(lblDwellTimeValue.Text);
                param.StartPosition = currentPosition;
                param.EndPosition = targetPosition;
                param.RepeatCount = Convert.ToInt32(lblRepeatCount.Text);

                if (param.RepeatCount == 0)
                    _isInfinite = true;

                _isRepeat = true;

                bool tlqkf = false;

                _repeatThread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(this.MoveRepeatThread));
                _repeatThread.Start(tlqkf);
            }
            else
            {
                _isInfinite = false;
                _isRepeat = false;
                _repeatThread.Interrupt();
                _repeatThread.Abort();
                lblStart.Text = "Start";
            }
        }

        private void MoveRepeatThread(object param)
        {
            var repeatParam = param as RepeatParam;

            AxisMovingParam movingParam = new AxisMovingParam();
            movingParam.Velocity = repeatParam.Velocity;
            movingParam.Acceleration = repeatParam.AccDec;
            movingParam.AfterWaitTime = repeatParam.DwellTime;

            int repeatCount = repeatParam.RepeatCount;
            int count = 0;

            if (repeatCount == 0)
            { }
            else
                _remainCount = repeatCount;

            while (_isRepeat)
            {

                SelectedAxis.StartAbsoluteMove(repeatParam.EndPosition, movingParam);
                while (!SelectedAxis.WaitForDone())
                    System.Threading.Thread.Sleep(Convert.ToInt16(movingParam.AfterWaitTime));

                SelectedAxis.StartAbsoluteMove(repeatParam.StartPosition, movingParam);
                while (!SelectedAxis.WaitForDone())
                    System.Threading.Thread.Sleep(Convert.ToInt16(movingParam.AfterWaitTime));

                count++;

                if (repeatCount == count)
                    _isRepeat = false;

                if (_isInfinite)
                    _isRepeat = true;

                _remainCount = repeatCount - count;
                Console.WriteLine("Set Repeat Count : " + repeatCount.ToString() + " / Complete Count : " + count.ToString() + " / Remain Count : " + _remainCount.ToString());
            }
        }

        public void UpdateRepeatCount()
        {
            lblRepeatRemain.Text = _remainCount + " / " + lblRepeatCount.Text;
        }

        private void lblStart_Click(object sender, EventArgs e)
        {
            MoveRepeat(true);
        }
        #endregion

        #region 클래스
        private class RepeatParam
        {
            public double Velocity { get; set; } = 0.0;

            public double AccDec { get; set; } = 0.0;

            public int DwellTime { get; set; } = 0;

            public double StartPosition { get; set; } = 0.0;

            public double EndPosition { get; set; } = 0.0;

            public int RepeatCount { get; set; } = 0;
        }
        #endregion
    }
}
