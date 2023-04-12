﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Jastech.Apps.Structure.Data;
using Cognex.VisionPro;
using Jastech.Framework.Winform.VisionPro.Controls;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Framework.Winform.Controls;
using System.Linq;
using System.Drawing;
using Jastech.Framework.Macron.Akkon.Controls;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AkkonControl : UserControl
    {
        #region 필드
        private string _prevTabName { get; set; } = string.Empty;
        #endregion

        private AkkonParamControl AkkonParamControl { get; set; } = new AkkonParamControl();
        private List<Tab> TeachingTabList { get; set; } = new List<Tab>();

        //private List<VisionProCaliperParam> CaliperList { get; set; } = null;

        private AlgorithmTool Algorithm = new AlgorithmTool();

        #region 속성
        #endregion

        #region 이벤트

        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public AkkonControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void AkkonControl_Load(object sender, EventArgs e)
        {
            AddControl();
        }

        private void AddControl()
        {
            AkkonParamControl.Dock = DockStyle.Fill;
            AkkonParamControl.GetOriginImageHandler += AkkonControl_GetOriginImageHandler;
            pnlParam.Controls.Add(AkkonParamControl);
        }

        private ICogImage AkkonControl_GetOriginImageHandler()
        {
            return AppsTeachingUIManager.Instance().GetPrevImage();
        }

        public void SetParams(List<Tab> tabList)
        {
            if (tabList.Count <= 0)
                return;

            TeachingTabList = tabList;
            InitializeComboBox();

            string tabName = cmbTabList.SelectedItem as string;
            UpdateParam(tabName);
        }

        private void InitializeComboBox()
        {
            cmbTabList.Items.Clear();

            foreach (var item in TeachingTabList)
                cmbTabList.Items.Add(item.Name);

            cmbTabList.SelectedIndex = 0;
        }

        private void UpdateParam(string tabName)
        {
            if (TeachingTabList.Count <= 0)
                return;

            var param = TeachingTabList.Where(x => x.Name == tabName).First().AkkonParam;
            AkkonParamControl.UpdateData(param);
            //lblLeadCount.Text = param.LeadCount.ToString();

            DrawROI();
        }

        public void DrawROI()
        {
            var display = AppsTeachingUIManager.Instance().GetDisplay();

            display.ClearGraphic();

            if (display.GetImage() == null)
                return;

            //CogCaliperCurrentRecordConstants constants = CogCaliperCurrentRecordConstants.All;  //CogCaliperCurrentRecordConstants.InputImage | CogCaliperCurrentRecordConstants.Region;
            //var currentParam = CogCaliperParamControl.GetCurrentParam();

            //display.SetInteractiveGraphics("tool", currentParam.CaliperParams.CreateCurrentRecord(constants));
        }
        #endregion

        private void cmbTabList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tabName = cmbTabList.SelectedItem as string;

            if (_prevTabName == tabName)
                return;

            var display = AppsTeachingUIManager.Instance().GetDisplay();
            if (display == null)
                return;

            UpdateParam(tabName);
            display.ClearGraphic();

            DrawROI();
            _prevTabName = tabName;
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

        private void lblPrev_Click(object sender, EventArgs e)
        {
            if (cmbTabList.SelectedIndex <= 0)
                return;

            cmbTabList.SelectedIndex -= 1;
        }

        private void lblNext_Click(object sender, EventArgs e)
        {
            int nextIndex = cmbTabList.SelectedIndex + 1;

            if (cmbTabList.Items.Count > nextIndex)
                cmbTabList.SelectedIndex = nextIndex;
        }

        public List<Tab> GetTeachingData()
        {
            return TeachingTabList;
        }

        private void lblInspection_Click(object sender, EventArgs e)
        {
            Inspection();
        }

        private void Inspection()
        {
            var display = AppsTeachingUIManager.Instance().GetDisplay();

            //var currentParam = CogCaliperParamControl.GetCurrentParam();

            //if (display == null || currentParam == null)
            //    return;

            //ICogImage cogImage = display.GetImage();

            //CogAlignCaliperResult result = new CogAlignCaliperResult();

            //if (_alignName.ToString().Contains("X"))
            //    result = Algorithm.RunAlignX(cogImage, currentParam.CaliperParams, currentParam.LeadCount);
            //else
            //    result = Algorithm.RunAlignY(cogImage, currentParam.CaliperParams);

            //if (result.Judgement == Result.Fail)
            //{
            //    MessageConfirmForm form = new MessageConfirmForm();
            //    form.Message = "Caliper is Not Found.";
            //    form.ShowDialog();
            //}
            //else
            //{
            //    display.ClearGraphic();
            //    display.UpdateResult(result);
            //}
        }

        private void lblAddROI_Click(object sender, EventArgs e)
        {

        }

        private void rdoAkkonROI_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdoAkkonResult_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
