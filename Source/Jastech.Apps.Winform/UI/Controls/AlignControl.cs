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
using Jastech.Apps.Structure;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Framework.Imaging.VisionPro;
using Cognex.VisionPro.Caliper;
using CogCaliperResult = Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results.CogCaliperResult;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AlignControl : UserControl
    {
        #region 필드
        private string _prevName { get; set; } = string.Empty;
        #endregion

        private CogCaliperParamControl CogCaliperParamControl { get; set; } = new CogCaliperParamControl();
        private List<CogCaliperParam> CaliperList { get; set; } = null;

        private AlgorithmTool Algorithm = new AlgorithmTool();

        //private CogCaliper CogCaliperAlgorithm = new CogCaliper();

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
            CogCaliperParamControl.Dock = DockStyle.Fill;
            pnlParam.Controls.Add(CogCaliperParamControl);
        }

        public void SetParams(List<CogCaliperParam> paramList)
        {
            if (paramList.Count <= 0)
                return;

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

            //CogCaliperAlgorithm.CaliperTool = param.CaliperTool;
        }


        private void SetNewROI(CogDisplayControl display)
        {
            ICogImage cogImage = display.GetImage();

            double centerX = display.ImageWidth() / 2.0 - display.GetPan().X;
            double centerY = display.ImageHeight() / 2.0 - display.GetPan().Y;

            CogRectangleAffine roi = CogImageHelper.CreateRectangleAffine(centerX - display.GetPan().X, centerY - display.GetPan().Y, 100, 100);

            if (roi.CenterX <= 70)
                roi.SetCenterLengthsRotationSkew(centerX, centerY, 500, 500, 0, 0);

            var currentParam = CogCaliperParamControl.GetCurrentParam();

            currentParam.SetRegion(roi);
        }

        public void DrawROI()
        {
            var display = TeachingUIManager.Instance().TeachingDisplay;
            if (display.GetImage() == null)
                return;

            CogCaliperCurrentRecordConstants constants = CogCaliperCurrentRecordConstants.InputImage | CogCaliperCurrentRecordConstants.Region;
            var currentParam = CogCaliperParamControl.GetCurrentParam();
            display.SetInteractiveGraphics("tool", currentParam.CreateCurrentRecord(constants));
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

        private void lblPrevTab_Click(object sender, EventArgs e)
        {
            PrevTab();
        }

        private void PrevTab()
        {
            if (cmbTabList.SelectedIndex <= 0)
                return;

            cmbTabList.SelectedIndex -= 1;
        }

        private void lblNextTab_Click(object sender, EventArgs e)
        {
            NextTab();
        }

        private void NextTab()
        {
            if (cmbTabList.SelectedIndex <= 0)
                return;

            int nextIndex = cmbTabList.SelectedIndex + 1;

            if (cmbTabList.Items.Count > nextIndex)
                cmbTabList.SelectedIndex = nextIndex;
        }

        private void cmbTabList_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawComboboxCenterAlign(sender, e);
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

        private void lblInspection_Click(object sender, EventArgs e)
        {
            Inspection();
        }

        private void Inspection()
        {
            var display = TeachingUIManager.Instance().TeachingDisplay;
            var currentParam = CogCaliperParamControl.GetCurrentParam();

            if (display == null || currentParam == null)
                return;

            ICogImage cogImage = display.GetImage();

            CogCaliperResult result = Algorithm.CaliperAlgorithm.Run(cogImage, currentParam);
        }

        private void lblAddROI_Click(object sender, EventArgs e)
        {
            AddROI();
        }

        private void AddROI()
        {
            var display = TeachingUIManager.Instance().TeachingDisplay;
            if (display.GetImage() == null)
                return;

            if (ModelManager.Instance().CurrentModel == null)
                return;

            SetNewROI(display);
            DrawROI();
        }
    }
}
