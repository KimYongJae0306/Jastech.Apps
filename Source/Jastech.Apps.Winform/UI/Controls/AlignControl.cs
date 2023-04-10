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

        private List<Tab> TeachingTabList { get; set; } = new List<Tab>();
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
                chkUseTracking.BackColor = Color.DeepSkyBlue;
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

        public void SetParams(List<Tab> tabList)
        {
            if (tabList.Count <= 0)
                return;

            TeachingTabList = tabList;

            string tabName = tabList[0].Name;
            UpdateParam(tabName);
        }

        private void UpdateParam(string tabName)
        {
            if (TeachingTabList.Count <= 0)
                return;


            //var param = CaliperList.Where(x => x.Name == name).First();
            //var param = TeachingTabList[0].AlignParams.Where(x => x.Name == Name).First();
            var param = TeachingTabList.Where(x => x.Name == tabName).First().AlignParams[0];
            CogCaliperParamControl.UpdateData(param);
        }

        private void SetNewROI(CogDisplayControl display)
        {
            display.ClearGraphic();
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
            var display = AppsTeachingUIManager.Instance().TeachingDisplay;
            if (display.GetImage() == null)
                return;

            CogCaliperCurrentRecordConstants constants = CogCaliperCurrentRecordConstants.All;  //CogCaliperCurrentRecordConstants.InputImage | CogCaliperCurrentRecordConstants.Region;
            var currentParam = CogCaliperParamControl.GetCurrentParam();

            display.SetInteractiveGraphics("tool", currentParam.CreateCurrentRecord(constants));
        }

        //private void cmbAlignList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string name = cmbAlignList.SelectedItem as string;

        //    if (_prevName == name)
        //        return;

        //    var display = AppsTeachingUIManager.Instance().TeachingDisplay;

        //    if (display == null)
        //        return;

        //    UpdateParam(name);
        //    display.ClearGraphic();

        //    DrawROI();
        //    _prevName = name;
        //}

        public List<Tab> GetTeachingData()
        {
            return TeachingTabList;
        }

        private void cmbTabList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = cmbTabList.SelectedItem as string;

            if (_prevName == name)
                return;

            var display = AppsTeachingUIManager.Instance().TeachingDisplay;
            if (display == null)
                return;
            UpdateParam(name);
            display.ClearGraphic();

            DrawROI();
            _prevName = name;
        }

        private void cmbTabList_DrawItem(object sender, DrawItemEventArgs e)
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

        private void lblPrevTab_Click(object sender, EventArgs e)
        {
        }


        private void lblNextTab_Click(object sender, EventArgs e)
        {
        }


        #endregion

        private void lblInspection_Click(object sender, EventArgs e)
        {
            Inspection();
        }

        private void Inspection()
        {
            var display = AppsTeachingUIManager.Instance().TeachingDisplay;
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
            var display = AppsTeachingUIManager.Instance().TeachingDisplay;
            if (display.GetImage() == null)
                return;

            if (ModelManager.Instance().CurrentModel == null)
                return;

            SetNewROI(display);
            DrawROI();
        }


        
    }
}
