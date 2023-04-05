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
using Jastech.Apps.Structure;
using System.Reflection;

namespace ATT.UI.Controls
{
    public partial class MotionSettingsControl : UserControl
    {
        #region 필드
        private System.Threading.Timer _controlTimer = null;

        private TeachingPositionListControl TeachingPositionListControl { get; set; } = new TeachingPositionListControl();

        private MotionFunctionControl MotionFunctionControl { get; set; } = new MotionFunctionControl();

        private List<MotionCommandControl> MotionCommandControlList { get; set; } = new List<MotionCommandControl>();

        private MotionJogControl MotionJogControl { get; set; } = new MotionJogControl();

        private List<MotionParameterCommonControl> MotionParameterCommonControlList { get; set; } = new List<MotionParameterCommonControl>();

        private List<MotionParameterVariableControl> MotionParameterVariableControlList { get; set; } = new List<MotionParameterVariableControl>();
        #endregion

        MotionCommandControl XCommandControl = new MotionCommandControl();

        MotionCommandControl YCommandControl = new MotionCommandControl();

        MotionCommandControl ZCommandControl = new MotionCommandControl();

        MotionParameterCommonControl XCommonControl = new MotionParameterCommonControl();

        MotionParameterCommonControl YCommonControl = new MotionParameterCommonControl();

        MotionParameterCommonControl ZCommonControl = new MotionParameterCommonControl();

        MotionParameterVariableControl XVariableControl = new MotionParameterVariableControl();

        MotionParameterVariableControl YVariableControl = new MotionParameterVariableControl();

        MotionParameterVariableControl ZVariableControl = new MotionParameterVariableControl();
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
            StartTimer();

            //if(ModelManager.Instance().CurrentModel != null)
                SetParams();
        }

        private void AddControl()
        {
            TeachingPositionListControl.Dock = DockStyle.Fill;
            TeachingPositionListControl.UnitName = "0";
            pnlTeachingPositionList.Controls.Add(TeachingPositionListControl);

            MotionFunctionControl.Dock = DockStyle.Fill;
            tlpStatus.Controls.Add(MotionFunctionControl);

            AddCommandControl();

            AddCommonControl();

            AddVariableControl();
         
            MotionJogControl.SetAxisHanlder(AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Unit0));
            MotionJogControl.Dock = DockStyle.Fill;
            pnlJog.Controls.Add(MotionJogControl);
        }

        private void AddCommandControl()
        {
            var axisHandler = AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Unit0);

            XCommandControl.Dock = DockStyle.Fill;
            XCommandControl.SetAxis(axisHandler.GetAxis(AxisName.X));

            YCommandControl.Dock = DockStyle.Fill;
            YCommandControl.SetAxis(axisHandler.GetAxis(AxisName.Y));

            ZCommandControl.Dock = DockStyle.Fill;
            ZCommandControl.SetAxis(axisHandler.GetAxis(AxisName.Z));

            tlpStatus.Controls.Add(XCommandControl);
            tlpStatus.Controls.Add(YCommandControl);
            tlpStatus.Controls.Add(ZCommandControl);
        }

        private void AddCommonControl()
        {
            var axisHandler = AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Unit0);

            XCommonControl.Dock = DockStyle.Fill;
            XCommonControl.SetAxis(axisHandler.GetAxis(AxisName.X));

            YCommonControl.Dock = DockStyle.Fill;
            YCommonControl.SetAxis(axisHandler.GetAxis(AxisName.Y));

            ZCommonControl.Dock = DockStyle.Fill;
            ZCommonControl.SetAxis(axisHandler.GetAxis(AxisName.Z));

            tlpCommonParameter.Controls.Add(XCommonControl);
            tlpCommonParameter.Controls.Add(YCommonControl);
            tlpCommonParameter.Controls.Add(ZCommonControl);
        }

        private void AddVariableControl()
        {
            var axisHandler = AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Unit0);

            XVariableControl.Dock = DockStyle.Fill;
            XVariableControl.SetAxis(axisHandler.GetAxis(AxisName.X));

            YVariableControl.Dock = DockStyle.Fill;
            YVariableControl.SetAxis(axisHandler.GetAxis(AxisName.Y));

            ZVariableControl.Dock = DockStyle.Fill;
            ZVariableControl.SetAxis(axisHandler.GetAxis(AxisName.Z));

            tlpVariableParameter.Controls.Add(XVariableControl);
            tlpVariableParameter.Controls.Add(YVariableControl);
            tlpVariableParameter.Controls.Add(ZVariableControl);
        }

        private void StartTimer()
        {
            _controlTimer = new System.Threading.Timer(UpdateMotionStatus, null, 1000, 1000);
        }

        private void UpdateMotionStatus(object obj)
        {
            try
            {
                if (this.XCommandControl != null)
                    XCommandControl.UpdateAxisStatus();

                if (this.XCommandControl != null)
                    YCommandControl.UpdateAxisStatus();

                if (this.XCommandControl != null)
                    ZCommandControl.UpdateAxisStatus();
            }
            catch (Exception err)
            {
                Console.WriteLine(MethodBase.GetCurrentMethod().Name.ToString() + " : " + err.Message);
            }
        }

        public void SetParams()
        {
            var axisHandler = AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Unit0);

            // Common Params
            XCommonControl.Initialize(axisHandler.GetAxis(AxisName.X).AxisCommonParams.DeepCopy());
            YCommonControl.Initialize(axisHandler.GetAxis(AxisName.Y).AxisCommonParams.DeepCopy());
            ZCommonControl.Initialize(axisHandler.GetAxis(AxisName.Y).AxisCommonParams.DeepCopy());

            var inspModel = ModelManager.Instance().CurrentModel as Core.ATTInspModel;
            string unitName = TeachingPositionListControl.UnitName;

            if (inspModel == null || unitName == "")
                return;


            // Variable Params
            var posData = SystemManager.Instance().GetTeachingData().GetUnit(unitName).PositionList[0];

            XVariableControl.Initialize(posData.GetMovingParams(AxisName.X));
            YVariableControl.Initialize(posData.GetMovingParams(AxisName.Y));
            ZVariableControl.Initialize(posData.GetMovingParams(AxisName.Z));


            // Command Params
            XCommandControl.Intialize(posData.AxisInfoList[(int)AxisName.X]);
            YCommandControl.Intialize(posData.AxisInfoList[(int)AxisName.Y]);
            ZCommandControl.Intialize(posData.AxisInfoList[(int)AxisName.Z]);
        }

        public void GetCurrentData()
        {
            // Common Params
            var axisHandler = AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Unit0);
            axisHandler.GetAxis(AxisName.X).AxisCommonParams.SetCommonParams(XCommonControl.GetCurrentData());
            axisHandler.GetAxis(AxisName.Y).AxisCommonParams.SetCommonParams(YCommonControl.GetCurrentData());
            axisHandler.GetAxis(AxisName.Z).AxisCommonParams.SetCommonParams(ZCommonControl.GetCurrentData());


            // Variable Params
            string unitName = TeachingPositionListControl.UnitName;
            var posData = SystemManager.Instance().GetTeachingData().GetUnit(unitName).PositionList[0];

            posData.SetMovingParams(AxisName.X, XVariableControl.GetCurrentData());
            posData.SetMovingParams(AxisName.Y, YVariableControl.GetCurrentData());
            posData.SetMovingParams(AxisName.Z, ZVariableControl.GetCurrentData());


            // Command Params
            posData.SetTargetPosition(AxisName.X, XCommandControl.GetCurrentData().TargetPosition);
            posData.SetOffset(AxisName.X, ZCommandControl.GetCurrentData().Offset);

            posData.SetTargetPosition(AxisName.Y, XCommandControl.GetCurrentData().TargetPosition);
            posData.SetOffset(AxisName.Y, ZCommandControl.GetCurrentData().Offset);

            posData.SetTargetPosition(AxisName.Z, XCommandControl.GetCurrentData().TargetPosition);
            posData.SetOffset(AxisName.Z, ZCommandControl.GetCurrentData().Offset);
        }
        #endregion
    }
}
