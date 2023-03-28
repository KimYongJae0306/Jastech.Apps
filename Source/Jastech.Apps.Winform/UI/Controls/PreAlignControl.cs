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

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class PreAlignControl : UserControl
    {
        #region 필드
        private string _prevName { get; set; } = "";
        #endregion
        private CogPatternMatchingParamControl CogPatternMatchingParamControl { get; set; } = new CogPatternMatchingParamControl();

        private List<CogPatternMatchingParam> PatternMatchingList { get; set; } = null;

        private CogPatternMatching CogPMAlignAlgorithm = new CogPatternMatching();

        public PreAlignControl()
        {
            InitializeComponent();
        }

        private void PreAlignControl_Load(object sender, EventArgs e)
        {
            CogPatternMatchingParamControl.Dock = DockStyle.Fill;
            CogPatternMatchingParamControl.SetTrainEventHandler += PreAlignControl_SetTrainEventHandler;
            pnlParam.Controls.Add(CogPatternMatchingParamControl);
        }

        private void PreAlignControl_SetTrainEventHandler()
        {
            var display = TeachingUIManager.Instance().TeachingDisplay;
            if (display == null)
                return;

           var g = CogPMAlignAlgorithm.GetTrainRegion();
            // Lockkey 때매 임시 잠금
            //CogPMAlignAlgorithm.Train(display.GetImage());
        }

        public void SetParams(List<CogPatternMatchingParam> paramList)
        {
            PatternMatchingList = paramList;

            cbxPreAlignList.Items.Clear();

            foreach (var item in PatternMatchingList)
            {
                cbxPreAlignList.Items.Add(item.Name);
            }
            cbxPreAlignList.SelectedIndex = 0;

            string name = cbxPreAlignList.SelectedItem as string;
            CogPatternMachingParam(name);
        }

        private void CogPatternMachingParam(string name)
        {
            var param = PatternMatchingList.Where(x => x.Name == name).First();
             CogPatternMatchingParamControl.UpdateData(param);

            CogPMAlignAlgorithm.MatchingTool = param.MatchingTool;
        }

        private void lblAddROI_Click(object sender, EventArgs e)
        {
            var display = TeachingUIManager.Instance().TeachingDisplay;
            if (display.GetImage() == null)
                return;

            SetNewROI(display);
            DrawROI();
        }

        private void DrawROI()
        {
            var display = TeachingUIManager.Instance().TeachingDisplay;
            if (display.GetImage() == null)
                return;

            if (CogPMAlignAlgorithm.IsTrained())
            {
                display.AddGraphics("tool", CogPMAlignAlgorithm.GetTrainRegion());
                display.AddGraphics("tool", CogPMAlignAlgorithm.GetSearchRegion());

                CogRectangle roi = CogPMAlignAlgorithm.GetTrainRegion() as CogRectangle;
                PointToScreen(new Point((int)roi.CenterX, (int)roi.CenterY));
            }
            else
            {
                display.AddGraphics("tool", CogPMAlignAlgorithm.GetTrainRegion());
                display.AddGraphics("tool", CogPMAlignAlgorithm.GetSearchRegion());
            }
        }

        private void SetNewROI(CogDisplayControl display)
        {
            ICogImage cogImage = display.GetImage();

            double centerX = display.ImageWidth() / 2.0;
            double centerY = display.ImageHeight() / 2.0;

            CogRectangle roi = CogImageHelper.CreateRectangle(centerX - display.GetPan().X, centerY - display.GetPan().Y, 100, 100);
            CogRectangle searchRoi = CogImageHelper.CreateRectangle(roi.CenterX, roi.CenterY, roi.Width * 2, roi.Height * 2);

            CogPMAlignAlgorithm.SetTrainRegion(roi);
            CogPMAlignAlgorithm.SetSearchRegion(searchRoi);
        }

        private void cbxPreAlignList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = cbxPreAlignList.SelectedItem as string;

            if (_prevName == name)
                return;

            var display = TeachingUIManager.Instance().TeachingDisplay;
            if (display == null)
                return;
            CogPatternMachingParam(name);
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
            if (cbxPreAlignList.SelectedIndex < 0)
                return;

            int nextIndex = cbxPreAlignList.SelectedIndex + 1;
            if (cbxPreAlignList.Items.Count > nextIndex)
            {
                cbxPreAlignList.SelectedIndex = nextIndex;
            }
        }

        public List<CogPatternMatchingParam> GetTeachingData()
        {
            return PatternMatchingList;
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
    }
}
