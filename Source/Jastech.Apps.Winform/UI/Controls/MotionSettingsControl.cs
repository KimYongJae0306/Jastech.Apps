using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Framework.Winform.Controls;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class MotionSettingsControl : UserControl
    {
        #region 필드
        private MotionFunctionControl MotionFunctionControl { get; set; } = new MotionFunctionControl();
        private List<MotionCommandControl> MotionCommandControlList { get; set; } = new List<MotionCommandControl>();
        private List<MotionParameterControl> MotionParameterControlList { get; set; } = new List<MotionParameterControl>();
        private JogControl JogControl { get; set; } = new JogControl();
        #endregion

        #region 속성

        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public MotionSettingsControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void MotionSettingsControl_Load(object sender, EventArgs e)
        {
            AddControl();
        }

        private void AddControl()
        {
            MotionFunctionControl.Dock = DockStyle.Fill;
            tlpStatus.Controls.Add(MotionFunctionControl);

            for (int i = 0; i < 3; i++)
            {
                MotionCommandControl motionCommandControl = new MotionCommandControl();
                MotionParameterControl motionParameterControl = new MotionParameterControl();

                if (i == 0)
                {
                    motionCommandControl.AxisName = "X";
                    motionParameterControl.AxisName = "X";
                }
                if (i == 1)
                {
                    motionCommandControl.AxisName = "Y";
                    motionParameterControl.AxisName = "Y";
                }
                else if (i == 2)
                {
                    motionCommandControl.AxisName = "Z";
                    motionParameterControl.AxisName = "Z";
                }
                else { }

                motionCommandControl.Dock = DockStyle.Fill;
                tlpStatus.Controls.Add(motionCommandControl);
                MotionCommandControlList.Add(motionCommandControl);

                motionParameterControl.Dock = DockStyle.Fill;
                tlpMotionParameter.Controls.Add(motionParameterControl);
                MotionParameterControlList.Add(motionParameterControl);

            }

            JogControl.Dock = DockStyle.Fill;
            pnlJog.Controls.Add(JogControl);
        }

        public void UpdateUI()
        {

        }
        #endregion
    }
}
