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
using Jastech.Apps.Winform;

namespace ATT.UI.Controls
{
    public partial class MotionSettingsControl : UserControl
    {
        #region 필드
        private TeachingPositionListControl TeachingPositionListControl { get; set; } = new TeachingPositionListControl();

        private MotionFunctionControl MotionFunctionControl { get; set; } = new MotionFunctionControl();

        private List<MotionCommandControl> MotionCommandControlList { get; set; } = new List<MotionCommandControl>();

        private MotionJogControl MotionJogControl { get; set; } = new MotionJogControl();

        private List<MotionParameterCommonControl> MotionParameterCommonControlList { get; set; } = new List<MotionParameterCommonControl>();

        private List<MotionParameterVariableControl> MotionParameterVariableControlList { get; set; } = new List<MotionParameterVariableControl>();
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
            TeachingPositionListControl.Dock = DockStyle.Fill;
            pnlTeachingPositionList.Controls.Add(TeachingPositionListControl);

            MotionFunctionControl.Dock = DockStyle.Fill;
            tlpStatus.Controls.Add(MotionFunctionControl);

            foreach (var axis in AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Unit0).AxisList)
            {
                MotionCommandControl motionCommandControl = new MotionCommandControl();
                motionCommandControl.SetAxis(axis);
                motionCommandControl.Dock = DockStyle.Fill;
                tlpStatus.Controls.Add(motionCommandControl);
                MotionCommandControlList.Add(motionCommandControl);

                MotionParameterCommonControl motionParameterCommonControl = new MotionParameterCommonControl();
                motionParameterCommonControl.SetAxis(axis);
                motionParameterCommonControl.Dock = DockStyle.Fill;
                tlpCommonParameter.Controls.Add(motionParameterCommonControl);
                MotionParameterCommonControlList.Add(motionParameterCommonControl);

                MotionParameterVariableControl motionParameterVariableControl = new MotionParameterVariableControl();
                motionParameterVariableControl.SetAxis(axis);
                motionParameterVariableControl.Dock = DockStyle.Fill;
                tlpVariableParameter.Controls.Add(motionParameterVariableControl);
                MotionParameterVariableControlList.Add(motionParameterVariableControl);
            }
     
            MotionJogControl.SetAxisHanlder(AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Unit0));
            MotionJogControl.Dock = DockStyle.Fill;
            pnlJog.Controls.Add(MotionJogControl);
        }

        public void UpdateUI()
        {

        }

        private void ReceiveTeachingPosition(Jastech.Framework.Structure.TeachingPosition teachingPosition)
        {
            for (int i = 0; i < 3; i++)
            {
                //MotionParameterControlList[i].UpdateUI(teachingPosition);
            }
            //Jastech.Apps.Structure.ModelManager.Instance().CurrentModel.PositionList
            //int count = 0;
            //foreach (var axis in teachingPosition)
            //{
            //    MotionParameterControlList[count].UpdateUI(axis.);
            //    count++;
            //}
        }
        #endregion
    }
}
