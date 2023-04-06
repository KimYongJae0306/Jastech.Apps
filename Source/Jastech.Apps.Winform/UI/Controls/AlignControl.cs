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
            return;

            if (tabList.Count <= 0)
                return;

            TeachingTabList = tabList;

            string name = tabList[0].Name;
            UpdateParam(name);
        }

        private void UpdateParam(string name)
        {
            var param = CaliperList.Where(x => x.Name == name).First();
            CogCaliperParamControl.UpdateData(param);
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
            var display = AppsTeachingUIManager.Instance().TeachingDisplay;
            if (display.GetImage() == null)
                return;

            CogCaliperCurrentRecordConstants constants = CogCaliperCurrentRecordConstants.InputImage | CogCaliperCurrentRecordConstants.Region;
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

        public List<CogCaliperParam> GetTeachingData()
        {
            return CaliperList;
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
