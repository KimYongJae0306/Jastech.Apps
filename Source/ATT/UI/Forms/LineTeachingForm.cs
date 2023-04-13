﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Framework.Winform.VisionPro.Controls;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Apps.Structure;
using Jastech.Framework.Winform.Forms;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.UI.Forms;
using ATT.UI.Forms;
using Cognex.VisionPro;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Structure;
using Jastech.Framework.Winform.Controls;
using Jastech.Apps.Winform.Core;
using Jastech.Framework.Winform;
using Jastech.Framework.Device.Cameras;
using OpenCvSharp;
using Jastech.Apps.Structure.Data;

namespace ATT.UI.Forms
{
    public partial class LineTeachingForm : Form
    {
        #region 필드
        private Color _selectedColor;

        private Color _noneSelectedColor;
        #endregion

        #region 속성
        public string UnitName { get; set; } = "";

        public string TitleCameraName { get; set; } = "";

        private CogTeachingDisplayControl Display { get; set; } = new CogTeachingDisplayControl();

        private AlignControl AlignControl { get; set; } = new AlignControl() { Dock = DockStyle.Fill };

        private AkkonControl AkkonControl { get; set; } = new AkkonControl() { Dock = DockStyle.Fill };

        private MarkControl MarkControl { get; set; } = new MarkControl() { Dock = DockStyle.Fill };

        private List<UserControl> TeachControlList = null;

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public LineTeachingForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void LineTeachingForm_Load(object sender, EventArgs e)
        {
            AddControl();
            SelectAlign();

            lblStageCam.Text = $"STAGE : {UnitName} / CAM : {TitleCameraName}";

            AppsLineCameraManager.Instance().TeachingImageGrabbed += LineTeachingForm_TeachingImageGrabbed;

            var image = AppsTeachingUIManager.Instance().GetPrevImage();

            if (image != null)
                Display.SetImage(image);
        }

        private void AddControl()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _noneSelectedColor = Color.FromArgb(52, 52, 52);

            // Display Control
            Display = new CogTeachingDisplayControl();
            Display.Dock = DockStyle.Fill;

            //Event 연결
            Display.DeleteEventHandler += Display_DeleteEventHandler;
            pnlDisplay.Controls.Add(Display);

            // TeachingUIManager 참조
            AppsTeachingUIManager.Instance().SetDisplay(Display.GetDisplay());

            // Teach Control List
            //TeachControlList = new List<UserControl>();
            //TeachControlList.Add(AlignControl);
        }

        private void Display_DeleteEventHandler(object sender, EventArgs e)
        {
            AlignControl.DrawROI();
        }

        private void btnMark_Click(object sender, EventArgs e)
        {
            SelectMark();
        }

        private void btnAlign_Click(object sender, EventArgs e)
        {
            SelectAlign();
        }

        private void btnAkkon_Click(object sender, EventArgs e)
        {
            SelectAkkon();
        }

        public void UpdateSelectPage()
        {
            SelectAlign();
        }

        private void ClearSelectedButton()
        {
            btnMark.ForeColor = Color.White;
            btnAlign.ForeColor = Color.White;
            btnAkkon.ForeColor = Color.White;
            pnlTeach.Controls.Clear();
        }

        private void SelectMark()
        {
            if (ModelManager.Instance().CurrentModel == null || UnitName == "")
                return;

            ClearSelectedButton();

            var tabList = SystemManager.Instance().GetTeachingData().GetTabList(UnitName);
            MarkControl.SetParams(tabList);

            pnlTeach.Controls.Add(MarkControl);
            btnMark.ForeColor = Color.Blue;
        }

        private void SelectAlign()
        {
            if (ModelManager.Instance().CurrentModel == null || UnitName == "")
                return;

            ClearSelectedButton();

            var tabList = SystemManager.Instance().GetTeachingData().GetTabList(UnitName);
            AlignControl.SetParams(tabList);

            btnAlign.ForeColor = Color.Blue;
            pnlTeach.Controls.Add(AlignControl);
        }

        private void SelectAkkon()
        {
            if (ModelManager.Instance().CurrentModel == null || UnitName == "")
                return;

            ClearSelectedButton();

            var tabList = SystemManager.Instance().GetTeachingData().GetTabList(UnitName);
            AkkonControl.SetParams(tabList);

            btnAkkon.ForeColor = Color.Blue;
            pnlTeach.Controls.Add(AkkonControl);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AppsInspModel model = ModelManager.Instance().CurrentModel as AppsInspModel;

            if (model == null)
                return;

            SaveModelData(model);

            MessageConfirmForm form = new MessageConfirmForm();
            form.Message = "Save Model Completed.";
            form.ShowDialog();
        }

        private void SaveModelData(AppsInspModel model)
        {
            model.SetUnitList(SystemManager.Instance().GetTeachingData().UnitList);

            string fileName = System.IO.Path.Combine(AppConfig.Instance().Path.Model, model.Name, InspModel.FileName);
            SystemManager.Instance().SaveModel(fileName, model);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAutoFocus_Click(object sender, EventArgs e)
        {

        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ReadOnlyChecked = true;
            dlg.Filter = "BMP Files (*.bmp)|*.bmp";
            dlg.ShowDialog();

            if (dlg.FileName != "")
            {
                ICogImage cogImage = CogImageHelper.Load(dlg.FileName);
                Display.SetImage(cogImage);
                AppsTeachingUIManager.Instance().SetImage(cogImage);
                //AlignControl.DrawROI();
            }
        }

        private void btnMotionPopup_Click(object sender, EventArgs e)
        {
            MotionPopupForm motionPopupForm = new MotionPopupForm();
            motionPopupForm.SetAxisHandler(AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Unit0));
            motionPopupForm.ShowDialog();
        }

        private void btnGrabStart_Click(object sender, EventArgs e)
        {
            AppsLineCameraManager.Instance().StartGrab(CameraName.LinscanMIL0);
        }

        private void btnGrabStop_Click(object sender, EventArgs e)
        {
            AppsLineCameraManager.Instance().StopGrab(CameraName.LinscanMIL0);
        }

        private void LineTeachingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AppsLineCameraManager.Instance().TeachingImageGrabbed -= LineTeachingForm_TeachingImageGrabbed;
        }

        private void LineTeachingForm_TeachingImageGrabbed(Mat image)
        {
            if (image == null)
                return;

            // Display Update
        }
        #endregion
    }
}