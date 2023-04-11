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
using Jastech.Apps.Structure.VisionTool;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Cognex.VisionPro;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms;
using Jastech.Apps.Structure;
using Cognex.VisionPro.PMAlign;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using Jastech.Framework.Winform.Forms;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class PreAlignControl : UserControl
    {
        #region 필드
        private string _prevName { get; set; } = "";
        #endregion
        private CogPatternMatchingParamControl ParamControl { get; set; } = new CogPatternMatchingParamControl();

        private List<PreAlignParam> PreAlignList { get; set; } = null;

        private AlgorithmTool Algorithm = new AlgorithmTool();

        public PreAlignControl()
        {
            InitializeComponent();
        }

        private void PreAlignControl_Load(object sender, EventArgs e)
        {
            AddControl();
        }

        private void AddControl()
        {
            ParamControl.Dock = DockStyle.Fill;
            ParamControl.GetOriginImageHandler += PreAlignControl_GetOriginImageHandler;
            pnlParam.Controls.Add(ParamControl);
        }

        private ICogImage PreAlignControl_GetOriginImageHandler()
        {
            return AppsTeachingUIManager.Instance().GetDisplay().GetImage();
        }

        public void SetParams(List<PreAlignParam> preAligns)
        {
            if (preAligns.Count <= 0)
                return;

            PreAlignList = preAligns;
            InitializeComboBox();

            string name = cbxPreAlignList.SelectedItem as string;
            UpdateParam(name);
        }

        private void InitializeComboBox()
        {
            cbxPreAlignList.Items.Clear();

            foreach (var item in PreAlignList)
            {
                cbxPreAlignList.Items.Add(item.Name);
            }
            cbxPreAlignList.SelectedIndex = 0;
        }

        private void UpdateParam(string name)
        {
            var param = PreAlignList.Where(x => x.Name == name).First().InspParam;
            ParamControl.UpdateData(param);
        }

        private void lblAddROI_Click(object sender, EventArgs e)
        {
            var display = AppsTeachingUIManager.Instance().GetDisplay();
            if (display.GetImage() == null)
                return;

            if (ModelManager.Instance().CurrentModel == null)
                return;

            SetNewROI(display);
            DrawROI();
        }

        public void DrawROI()
        {
            var display = AppsTeachingUIManager.Instance().GetDisplay();
            if (display.GetImage() == null)
                return;

            CogPMAlignCurrentRecordConstants constants = CogPMAlignCurrentRecordConstants.InputImage | CogPMAlignCurrentRecordConstants.SearchRegion
                | CogPMAlignCurrentRecordConstants.TrainImage | CogPMAlignCurrentRecordConstants.TrainRegion | CogPMAlignCurrentRecordConstants.PatternOrigin;

            var currentParam = ParamControl.GetCurrentParam();
        
            display.SetInteractiveGraphics("tool", currentParam.CreateCurrentRecord(constants));
        }

        private void SetNewROI(CogDisplayControl display)
        {
            ICogImage cogImage = display.GetImage();

            double centerX = display.ImageWidth() / 2.0;
            double centerY = display.ImageHeight() / 2.0;

            CogRectangle roi = CogImageHelper.CreateRectangle(centerX - display.GetPan().X, centerY - display.GetPan().Y, 100, 100);
            CogRectangle searchRoi = CogImageHelper.CreateRectangle(roi.CenterX, roi.CenterY, roi.Width * 2, roi.Height * 2);

            var currentParam = ParamControl.GetCurrentParam();

            currentParam.SetTrainRegion(roi);
            currentParam.SetSearchRegion(searchRoi);
        }

        private void cbxPreAlignList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = cbxPreAlignList.SelectedItem as string;

            if (_prevName == name)
                return;

            var display = AppsTeachingUIManager.Instance().GetDisplay();
            if (display == null)
                return;
            UpdateParam(name);
            display.ClearGraphic();

            DrawROI();
            _prevName = name;
        }

        private void lblPrev_Click(object sender, EventArgs e)
        {
            if (cbxPreAlignList.SelectedIndex <= 0)
                return;

            cbxPreAlignList.SelectedIndex -= 1;
        }

        private void lblNext_Click(object sender, EventArgs e)
        {
            int nextIndex = cbxPreAlignList.SelectedIndex + 1;
            if (cbxPreAlignList.Items.Count > nextIndex)
            {
                cbxPreAlignList.SelectedIndex = nextIndex;
            }
        }

        public List<PreAlignParam> GetTeachingData()
        {
            return PreAlignList;
        }

        private void cbxPreAlignList_DrawItem(object sender, DrawItemEventArgs e)
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

        private void lblInspection_Click(object sender, EventArgs e)
        {
            var display = AppsTeachingUIManager.Instance().GetDisplay();
            var currentParam = ParamControl.GetCurrentParam();

            if (display == null || currentParam == null)
                return;

            if(currentParam.IsTrained() == false)
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "Pattern is not trained.";
                form.ShowDialog();
                return;
            }

            ICogImage cogImage = display.GetImage();
            CogPatternMatchingResult result = Algorithm.RunPreAlign(cogImage, currentParam);

            UpdateGridResult(result);

            if(result.MatchPosList.Count > 0)
            {
                display.ClearGraphic();
                display.UpdateResult(result);
            }
            else
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "Pattern is Not Found.";
                form.ShowDialog();
            }
        }

        private void UpdateGridResult(CogPatternMatchingResult result)
        {
            gvResult.Rows.Clear();

            int count = 1;
            foreach (var matchPos in result.MatchPosList)
            {
                string no = count.ToString();
                string score = string.Format("{0:0.000}", (matchPos.Score * 100));
                string x = string.Format("{0:0.000}", matchPos.FoundPos.X);
                string y = string.Format("{0:0.000}", matchPos.FoundPos.Y);
                string angle = string.Format("{0:0.000}", matchPos.Angle);

                gvResult.Rows.Add(no, score, x, y, angle);
                count++;
            }
        }
    }
}
