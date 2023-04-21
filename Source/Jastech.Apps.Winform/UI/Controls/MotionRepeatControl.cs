using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Structure;
using Jastech.Framework.Winform.Forms;
using static Jastech.Framework.Device.Motions.AxisMovingParam;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class MotionRepeatControl : UserControl
    {
        private Axis SelectedAxis { get; set; } = null;

        public TeachingAxisInfo AxisInfo { get; set; } = null;
        
        private AxisHandler AxisHandler { get; set; } = null;

        private Direction _direction = Direction.CW;

        private Color _selectedColor;

        private Color _noneSelectedColor;

        public MotionRepeatControl()
        {
            InitializeComponent();
        }

        public void SetAxisHanlder(AxisHandler axisHandler)
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
            _noneSelectedColor = Color.FromArgb(52, 52, 52);

            SetScanDirection(Direction.CW);
        }

        private void lblRepeatVelocityValue_Click(object sender, EventArgs e)
        {
            SetLabelDoubleData(sender);
        }

        private void lblRepeatAccelerationValue_Click(object sender, EventArgs e)
        {
            SetLabelDoubleData(sender);
        }

        private void lblDwellTimeValue_Click(object sender, EventArgs e)
        {
            SetLabelIntegerData(sender);
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
                lblBackward.BackColor = _noneSelectedColor;
            }
            else
            {
                lblForward.BackColor = _noneSelectedColor;
                lblBackward.BackColor = _selectedColor;
            }

            _direction = direction;
        }

        private void lblScanXLength_Click(object sender, EventArgs e)
        {
            SetLabelDoubleData(sender);
        }

        private void SetLabelDoubleData(object sender)
        {
            KeyPadForm keyPadForm = new KeyPadForm();
            keyPadForm.ShowDialog();

            double inputData = keyPadForm.PadValue;

            Label label = (Label)sender;
            label.Text = inputData.ToString();
        }

        private void lblRepeatCount_Click(object sender, EventArgs e)
        {
            SetLabelIntegerData(sender);
        }

        private int SetLabelIntegerData(object sender)
        {
            KeyPadForm keyPadForm = new KeyPadForm();
            keyPadForm.ShowDialog();

            int inputData = Convert.ToInt32(keyPadForm.PadValue);

            Label label = (Label)sender;
            label.Text = inputData.ToString();

            return inputData;
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
                _repeatThread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(this.MoveRepeatThread));
                _repeatThread.Start(param);
            }
            else
            {
                _isRepeat = false;
                _repeatThread.Interrupt();
                _repeatThread.Abort();
                lblStart.Text = "Start";
            }
        }

        private class RepeatParam
        {
            public double Velocity { get; set; } = 0.0;

            public double AccDec { get; set; } = 0.0;

            public int DwellTime { get; set; } = 0;
            public double StartPosition { get; set; } = 0.0;

            public double EndPosition { get; set; } = 0.0;

            public int RepeatCount { get; set; } = 0;
        }

        private System.Threading.Thread _repeatThread = null;
        private bool _isRepeat = false;
        private bool _isInfinite = false;
        private int _remainCount = 0;
        private void MoveRepeatThread(object param)
        {
            var repeatParam = param as RepeatParam;

            AxisMovingParam movingParam = new AxisMovingParam();
            movingParam.Velocity = repeatParam.Velocity;
            movingParam.Acceleration = repeatParam.AccDec;
            movingParam.AfterWaitTime = repeatParam.DwellTime;

            int repeatCount = repeatParam.RepeatCount;
            int count = 0;

            while (_isRepeat)
            {
                SelectedAxis.MoveTo(repeatParam.EndPosition, movingParam);
                while (!SelectedAxis.WaitForDone())
                    System.Threading.Thread.Sleep(Convert.ToInt16(movingParam.AfterWaitTime));

                SelectedAxis.MoveTo(repeatParam.StartPosition, movingParam);
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

            //lblStart.Text = "Stop";
        }

        //private delegate void UpdateRepeatCountDelegate(object obj);
        //public void UpdateRepeatCount(object obj)
        //{
        //    if (this.InvokeRequired)
        //    {
        //        UpdateRepeatCountDelegate callback = UpdateRepeatCount;
        //        BeginInvoke(callback, obj);
        //        return;
        //    }

        //    UpdateRepeatCount();
        //}

        public void UpdateRepeatCount()
        {
            if (!_isRepeat)
                return;

            lblRepeatRemain.Text = _remainCount + " / " + lblRepeatCount.Text;
        }

        private void lblStart_Click(object sender, EventArgs e)
        {
            MoveRepeat(true);
        }
    }
}
