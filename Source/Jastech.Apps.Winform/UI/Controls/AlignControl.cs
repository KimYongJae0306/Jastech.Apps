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
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms;
using Cognex.VisionPro;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AlignControl : UserControl
    {
        #region 필드
        private string _prevName { get; set; } = string.Empty;
        #endregion

        private CogCaliperParamControl CogCaliperParamControl { get; set; } = new CogCaliperParamControl();
        private List<CogCaliperParam> CaliperList { get; set; } = null;

        private CogCaliper CogCaliperAlgorithm = new CogCaliper();

        #region 속성
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
            // Add Control
            CogCaliperParamControl.Dock = DockStyle.Fill;
            pnlParam.Controls.Add(CogCaliperParamControl);
        }


        public void SetParams(List<CogCaliperParam> paramList)
        {
            CaliperList = paramList;

            cmbAlignList.Items.Clear();

            foreach (var item in CaliperList)
                cmbAlignList.Items.Add(item.Name);

            cmbAlignList.SelectedIndex = 0;

            string name = cmbAlignList.SelectedItem as string;
            CogCaliperParam(name);
        }

        private void CogCaliperParam(string name)
        {
            var param = CaliperList.Where(x => x.Name == name).First();
            CogCaliperParamControl.UpdateData(param);

            CogCaliperAlgorithm.CaliperTool = param.CaliperTool;
        }

        private void btnShowROI_Click(object sender, EventArgs e)
        {
            var display = TeachingUIManager.Instance().TeachingDisplay;
            if (display.GetImage() == null)
                return;

            SetNewROI(display);
            DrawROI();
        }

        private void SetNewROI(CogDisplayControl display)
        {
            ICogImage cogImage = display.GetImage();

            double centerX = display.ImageWidth() / 2.0 - display.GetPan().X;
            double centerY = display.ImageHeight() / 2.0 - display.GetPan().Y;

            CogRectangleAffine cogRectAffine = new CogRectangleAffine(/*CaliperList[0].CaliperTool.Region*/);

            if (cogRectAffine.CenterX <= 70)
                cogRectAffine.SetCenterLengthsRotationSkew(centerX, centerY, 500, 500, 0, 0);

            CogCaliperAlgorithm.SetRegion(cogRectAffine);
        }

        private void DrawROI()
        {
            var display = TeachingUIManager.Instance().TeachingDisplay;
            if (display.GetImage() == null)
                return;

            display.AddGraphics("tool", CogCaliperAlgorithm.GetRegion());
        }

        private void cmbAlignList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = cmbAlignList.SelectedItem as string;

            if (_prevName == name)
                return;

            var display = TeachingUIManager.Instance().TeachingDisplay;

            if (display == null)
                return;

            CogCaliperParam(name);
            display.ClearGraphic();

            DrawROI();
            _prevName = name;
        }

        public List<CogCaliperParam> GetTeachingData()
        {
            return CaliperList;
        }

        private void lblPrev_Click(object sender, EventArgs e)
        {
            if (cmbAlignList.SelectedIndex <= 0)
                return;

            cmbAlignList.SelectedIndex -= 1;
        }

        private void lblNext_Click(object sender, EventArgs e)
        {
            if (cmbAlignList.SelectedIndex <= 0)
                return;

            int nextIndex = cmbAlignList.SelectedIndex + 1;
            if (cmbAlignList.Items.Count > nextIndex)
                cmbAlignList.SelectedIndex = nextIndex;
        }

        private void cmbAlignList_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawComboboxCenterAlign(sender, e);
        }

        private void DrawComboboxCenterAlign(object sender, DrawItemEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            if (cmb != null)
            {
                e.DrawBackground();
                cmb.ItemHeight = lblPrev.Height - 6;

                if (e.Index >= 0)
                {
                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;

                    Brush brush = new SolidBrush(cmb.ForeColor);

                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                        brush = SystemBrushes.HighlightText;

                    e.Graphics.DrawString(cmb.Items[e.Index].ToString(), cmb.Font, brush, e.Bounds, sf);
                }
            }
        }
        #endregion
    }
}
