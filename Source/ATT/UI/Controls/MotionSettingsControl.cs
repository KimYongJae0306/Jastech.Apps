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

namespace ATT.UI.Controls
{
    public partial class MotionSettingsControl : UserControl
    {
        #region 필드
        #endregion

        #region 속성
        private MotionFunctionControl MotionFunctionControl { get; set; } = new MotionFunctionControl();
        private List<MotionCommandControl> MotionCommandControlList { get; set; } = new List<MotionCommandControl>();
        private JogControl JogControl { get; set; } = new JogControl();
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
                if (i == 1)
                    motionCommandControl.AxisName = "X";
                if (i == 2)
                {
                    motionCommandControl.AxisName = "Y";

                }
                else
                    motionCommandControl.AxisName = "Z";
                motionCommandControl.Dock = DockStyle.Fill;
                tlpStatus.Controls.Add(motionCommandControl);
                MotionCommandControlList.Add(motionCommandControl);
            }

            JogControl.Dock = DockStyle.Fill;
            pnlJog.Controls.Add(JogControl);
        }
        #endregion
    }
}
