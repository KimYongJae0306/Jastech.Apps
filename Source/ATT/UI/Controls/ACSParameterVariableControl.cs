using ATT;
using System.Collections.Generic;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform.Helper;
using System;
using System.Windows.Forms;
using Emgu.CV.Util;
using System.Threading.Tasks;
using System.Threading;
using ATT.UI.Forms;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class ACSParameterVariableControl : UserControl
    {
        #region 속성
        private Axis SelectedAxis { get; set; } = null;

        private Task UpdateValuesTask { get; set; } = null;

        public CancellationTokenSource TaskCancellation { get; set; } = null;
        #endregion

        #region 생성자
        public ACSParameterVariableControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void MotionParameterVariableControl_Load(object sender, EventArgs e)
        {
            InitializeUI();
            TaskCancellation = new CancellationTokenSource();
            UpdateValuesTask = new Task(() =>
            {
                while (TaskCancellation.IsCancellationRequested == false)
                {
                    if (SelectedAxis?.Motion is ACSMotion motion)
                        UpdateValues(motion);
                    Thread.Sleep(30);
                }
            }, TaskCancellation.Token);
            UpdateValuesTask.Start();
        }

        private void InitializeUI()
        {
            grpAxisName.Text = SelectedAxis.Name.ToString() + " Axis Parameter";

            lblVariable1.Text = nameof(ACSGlobalVariable.AF_SW);
            lblVariable2.Text = nameof(ACSGlobalVariable.AF_OFFSET);
            lblVariable3.Text = nameof(ACSGlobalVariable.AF_FACTOR);
            lblVariable4.Text = nameof(ACSGlobalVariable.JUDGE_RANGE);
            lblVariable5.Text = nameof(ACSGlobalVariable.AF_DIFF);
        }

        private void UpdateValues(ACSMotion motion)
        {
            BeginInvoke(new Action(() =>
            {
                lblVariable1Value.Text = motion.ReadRealVariable(lblVariable1.Text).ToString();
                lblVariable2Value.Text = motion.ReadRealVariable(lblVariable2.Text).ToString();
                lblVariable3Value.Text = motion.ReadRealVariable(lblVariable3.Text).ToString();
                lblVariable4Value.Text = motion.ReadRealVariable(lblVariable4.Text).ToString();
                lblVariable5Value.Text = motion.ReadRealVariable(lblVariable5.Text).ToString();
            }));
        }

        public void SetAxis(Axis axis)
        {
            SelectedAxis = axis;
        }

        private void lblVariable1Value_Click(object sender, EventArgs e)
        {
            if (SelectedAxis?.Motion is ACSMotion motion)
            {
                double oldValue = motion.ReadRealVariable(lblVariable1.Text);
                double newValue = oldValue == 1 ? 0 : 1;

                motion.WriteRealVariable(lblVariable1.Text, newValue);

                ParamTrackingLogger.AddChangeHistory($"{SelectedAxis.Name}", lblVariable1.Text, oldValue, newValue);
            }
        }

        private void lblVariable2Value_Click(object sender, EventArgs e)
        {
            if (SelectedAxis?.Motion is ACSMotion motion)
            {
                double oldValue = motion.ReadRealVariable(lblVariable2.Text);
                double newValue = KeyPadHelper.SetLabelDoubleData((Label)sender);

                motion.WriteRealVariable(lblVariable2.Text, newValue);

                ParamTrackingLogger.AddChangeHistory($"{SelectedAxis.Name}", lblVariable2.Text, oldValue, newValue);
            }
        }

        private void lblVariable3Value_Click(object sender, EventArgs e)
        {
            if (SelectedAxis?.Motion is ACSMotion motion)
            {
                double oldValue = motion.ReadRealVariable(lblVariable3.Text);
                double newValue = KeyPadHelper.SetLabelDoubleData((Label)sender);

                motion.WriteRealVariable(lblVariable3.Text, newValue);

                ParamTrackingLogger.AddChangeHistory($"{SelectedAxis.Name}", lblVariable3.Text, oldValue, newValue);
            }
        }

        private void lblVariable4Value_Click(object sender, EventArgs e)
        {
            if (SelectedAxis?.Motion is ACSMotion motion)
            {
                double oldValue = motion.ReadRealVariable(lblVariable4.Text);
                double newValue = KeyPadHelper.SetLabelDoubleData((Label)sender);

                motion.WriteRealVariable(lblVariable4.Text, newValue);

                ParamTrackingLogger.AddChangeHistory($"{SelectedAxis.Name}", lblVariable4.Text, oldValue, newValue);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            //Thread.Sleep(1000);
            //if (SelectedAxis?.Motion is ACSMotion motion)
            //    motion.ApplyLafParameters(ACSBufferNumber.CameraTrigger_Unit2, nameof(ACSGlobalVariable.AF_SW));
        }
        #endregion

        private void lblVariable5_Click(object sender, EventArgs e)
        {
            new DataChartingForm().Show();
        }
    }
}
