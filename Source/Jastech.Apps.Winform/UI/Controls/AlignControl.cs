using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Framework.Winform.VisionPro.Controls;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AlignControl : UserControl
    {
        #region 필드
        private CogCaliperParamControl CogCaliperParamControl { get; set; } = new CogCaliperParamControl();
        private AlignPosition _alignPosition = AlignPosition.Left;
        private TargetObject _targetObject = TargetObject.FPC;
        private EdgeType _edgeType = EdgeType.X;
        private Tlqkf _tlqkf = Tlqkf.FPC_X;
        private List<Label> LabelList = null;
        private int _index = 0;
        #endregion

        #region 속성
        public enum AlignPosition
        {
            Left,
            Right
        }

        public enum Tlqkf
        {
            FPC_X,
            FPC_Y,
            PANEL_X,
            PANEL_Y
        }

        public enum TargetObject
        {
            FPC,
            PANEL
        }

        public enum EdgeType
        {
            X,
            Y
        }
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public AlignControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void AlignControl_Load(object sender, EventArgs e)
        {
            AddControl();
            InitializeUI();
        }

        private void rdoLeft_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoLeft.Checked)
            {
                MoveDisplayFocus();
                SetAlignTeachingPosition(AlignPosition.Left);
                rdoLeft.BackColor = Color.DarkCyan;
            }
            else
                rdoLeft.BackColor = Color.PaleTurquoise;
        }

        private void rdoRight_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoRight.Checked)
            {
                MoveDisplayFocus();
                SetAlignTeachingPosition(AlignPosition.Right);
                rdoRight.BackColor = Color.DarkCyan;
            }
            else
                rdoRight.BackColor = Color.PaleTurquoise;
        }

        private void lblPrev_Click(object sender, EventArgs e)
        {
            int index = (int)_tlqkf;

            if (index == 0)
                RefreshTlqkf(Tlqkf.FPC_X);
            else
            {
                index--;
                RefreshTlqkf((Tlqkf)index);
            }

            _tlqkf = (Tlqkf)index;
        }

        private void lblNext_Click(object sender, EventArgs e)
        {
            int index = (int)_tlqkf;

            if (index == 3)
                RefreshTlqkf(Tlqkf.PANEL_Y);
            else
            {
                index++;
                RefreshTlqkf((Tlqkf)index);
            }

            _tlqkf = (Tlqkf)index;
        }

        private void chkUseTracking_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseTracking.Checked)
            {
                chkUseTracking.Text = "ROI Tracking : USE";
                chkUseTracking.BackColor = Color.DarkCyan;
            }
            else
            {
                chkUseTracking.Text = "ROI Tracking : UNUSE";
                chkUseTracking.BackColor = Color.White;
            }
        }

        private void AddControl()
        {
            // Label List
            LabelList = new List<Label>();
            LabelList.Add(lblFPCX);
            LabelList.Add(lblFPCY);
            LabelList.Add(lblPanelX);
            LabelList.Add(lblPanelY);

            // Add Control
            CogCaliperParamControl.Dock = DockStyle.Fill;
            pnlParam.Controls.Add(CogCaliperParamControl);
        }

        private void InitializeUI()
        {
            rdoLeft.Checked = true;

            RefreshTlqkf(Tlqkf.FPC_X);
        }

        private void RefreshTlqkf(Tlqkf tlqkf = Tlqkf.FPC_X)
        {
            foreach (Label label in LabelList)
                label.BackColor = Color.White;

            foreach (Label label in LabelList)
            {
                switch (tlqkf)
                {
                    case Tlqkf.FPC_X:
                        lblFPCX.BackColor = Color.DarkCyan;
                        break;
                    case Tlqkf.FPC_Y:
                        lblFPCY.BackColor = Color.DarkCyan;
                        break;
                    case Tlqkf.PANEL_X:
                        lblPanelX.BackColor = Color.DarkCyan;
                        break;
                    case Tlqkf.PANEL_Y:
                        lblPanelY.BackColor = Color.DarkCyan;
                        break;
                    default:
                        break;
                }
            }
        }

        private void MoveDisplayFocus()
        {
            // Teaching Display의 좌, 우 Focus 이동
        }

        private void SetAlignTeachingPosition(AlignPosition alignPosition)
        {
            _alignPosition = alignPosition;
        }
        #endregion
    }
}
