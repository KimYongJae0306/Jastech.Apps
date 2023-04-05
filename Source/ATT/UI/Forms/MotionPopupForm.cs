﻿using ATT.UI.Controls;
using Jastech.Apps.Structure;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Winform.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATT.UI.Forms
{
    public partial class MotionPopupForm : Form
    {
        #region 필드
        private System.Threading.Timer _formTimer = null;

        private TeachingPositionListControl TeachingPositionListControl { get; set; } = new TeachingPositionListControl();

        private MotionJogControl MotionJogControl { get; set; } = new MotionJogControl();
        #endregion

        #region 속성
        private AxisHandler selectedAxisHanlder { get; set; } = null;
        #endregion

        #region 이벤트
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
            StartTimer();
            InitializeUI();
            SetParams();
        }

        private void AddControl()
        {
            AddTeachingPositionControl();
            
            AddCommandControl();
            AddJogControl();
            //AddParameterControl();
            AddVariableControl();
        }

        private void AddTeachingPositionControl()
        {
            TeachingPositionListControl.Dock = DockStyle.Fill;
            TeachingPositionListControl.UnitName = "0";
            pnlTeachingPositionList.Controls.Add(TeachingPositionListControl);
            pnlTeachingPositionList.Dock = DockStyle.Fill;
        }

        //private void AddParameterControl()
        //{
        //    TableLayoutPanel tlp = new TableLayoutPanel();
        //    tlp.Dock = DockStyle.Fill;
        //    tlp.RowCount = selectedAxisHanlder.AxisList.Count;

        //    for (int rowIndex = 0; rowIndex < tlp.RowCount; rowIndex++)
        //        tlp.RowStyles.Add(new RowStyle(SizeType.Percent, (float)(100 / tlp.RowCount)));

        //    foreach (var axis in selectedAxisHanlder.AxisList)
        //    {
        //        MotionParameterVariableControl motionParameterVariableControl = new MotionParameterVariableControl();
        //        motionParameterVariableControl.SetAxis(axis);
        //        motionParameterVariableControl.Dock = DockStyle.Fill;
        //        tlp.Controls.Add(motionParameterVariableControl);
        //    }

        //    pnlMotionParameter.Controls.Add(tlp);
        //    pnlMotionParameter.Dock = DockStyle.Fill;
        //}
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

            tlpVariableParameters.Dock = DockStyle.Fill;
        }

        MotionCommandControl XCommandControl = new MotionCommandControl();

        MotionCommandControl YCommandControl = new MotionCommandControl();

        MotionCommandControl ZCommandControl = new MotionCommandControl();

        MotionParameterVariableControl XVariableControl = new MotionParameterVariableControl();

        MotionParameterVariableControl YVariableControl = new MotionParameterVariableControl();

        MotionParameterVariableControl ZVariableControl = new MotionParameterVariableControl();

        private MotionFunctionControl MotionFunctionControl { get; set; } = new MotionFunctionControl();

        private void AddTitleControl()
        {
            MotionFunctionControl.Dock = DockStyle.Fill;
            tlpStatus.Controls.Add(MotionFunctionControl);
        }

        public void SetParams()
        {
            var inspModel = ModelManager.Instance().CurrentModel as Core.ATTInspModel;
            string unitName = TeachingPositionListControl.UnitName;

            if (inspModel == null || unitName == "")
                return;

            var posData = SystemManager.Instance().GetTeachingData().GetUnit(unitName).PositionList[0];

            XVariableControl.Initialize(/*AxisName.X, */posData.GetMovingParams(AxisName.X));
            YVariableControl.Initialize(/*AxisName.Y, */posData.GetMovingParams(AxisName.Y));
            ZVariableControl.Initialize(/*AxisName.Z, */posData.GetMovingParams(AxisName.Z));
        }

        private void AddCommandControl()
        {
            AddTitleControl();

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

            tlpMotionFunction.Dock = DockStyle.Fill;
        }

        private void AddJogControl()
        {
            MotionJogControl.Dock = DockStyle.Fill;
            MotionJogControl.SetAxisHanlder(selectedAxisHanlder);
            pnlJog.Controls.Add(MotionJogControl);
            pnlJog.Dock = DockStyle.Fill;
        }

        private void StartTimer()
        {
            _formTimer = new System.Threading.Timer(UpdateMotionStatus, null, 1000, 1000);
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
            tlpMotionFunction.Visible = true;
            tlpVariableParameters.Visible = false;
            //pnlMotionParameter.Visible = false;
        }

        private void btnParameter_Click(object sender, EventArgs e)
        {
            ShowParameterPage();
        }

        private void ShowParameterPage()
        {
            tlpMotionFunction.Visible = false;
            tlpVariableParameters.Visible = true;
            //pnlMotionParameter.Visible = true;
        }
    }
    #endregion
}
