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
using Jastech.Framework.Device.Motions;

namespace ATT.UI.Controls
{
    public partial class MotionSettingsControl : UserControl
    {
        #region 필드
        private MotionFunctionControl MotionFunctionControl { get; set; } = new MotionFunctionControl();

        private List<MotionCommandControl> MotionCommandControlList { get; set; } = new List<MotionCommandControl>();

        private List<MotionParameterControl> MotionParameterControlList { get; set; } = new List<MotionParameterControl>();

        private MotionJogControl MotionJogControl { get; set; } = new MotionJogControl();
        #endregion

        #region 속성
       private List<AxisHandler> AxisHanlderList { get; set; } = new List<AxisHandler>();
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

            foreach (var axis in AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Unit0).AxisList)
            {
                MotionCommandControl motionCommandControl = new MotionCommandControl();
                MotionParameterControl motionParameterControl = new MotionParameterControl();
                motionCommandControl.SetAxis(axis);
                motionParameterControl.SetAxis(axis);

                motionCommandControl.Dock = DockStyle.Fill;
                tlpStatus.Controls.Add(motionCommandControl);
                MotionCommandControlList.Add(motionCommandControl);

                motionParameterControl.Dock = DockStyle.Fill;
                tlpMotionParameter.Controls.Add(motionParameterControl);
                MotionParameterControlList.Add(motionParameterControl);
            }
     
            MotionJogControl.SetAxisHanlder(AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Unit0));
            MotionJogControl.Dock = DockStyle.Fill;
            pnlJog.Controls.Add(MotionJogControl);
        }

        public void UpdateUI()
        {

        }
        #endregion
    }
}
