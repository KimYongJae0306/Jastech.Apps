﻿using ATT_UT_IPAD.Core;
using ATT_UT_IPAD.UI.Forms;
using Cognex.VisionPro;
using Jastech.Apps.Structure;
using Jastech.Apps.Winform;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform.Forms;
using System;
using System.Windows.Forms;

namespace ATT_UT_IPAD.UI.Pages
{
    public partial class TeachingPage : UserControl
    {
        #region 속성
        private ATTInspModelService ATTInspModelService { get; set; } = null;

        private MotionPopupForm MotionPopupForm { get; set; } = null;

        public ICogImage ScanImage { get; set; } = null;
        #endregion

        #region 생성자
        public TeachingPage()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void btnAlignCameraSetting_Click(object sender, EventArgs e)
        {
            if (LAFManager.Instance().GetLAF("AkkonLaf") is LAF akkonLAF)
                akkonLAF.LafCtrl.SetTrackingOnOFF(false);

            OpticTeachingForm form = new OpticTeachingForm();
            form.LineCamera = LineCameraManager.Instance().GetLineCamera("AlignCamera");
            form.LAFCtrl = LAFManager.Instance().GetLAF("AlignLaf").LafCtrl;
            form.UnitName = UnitName.Unit0;
            form.AxisNameZ = Jastech.Framework.Device.Motions.AxisName.Z1;
            form.AxisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.CloseMotionPopupEventHandler += CloseMotionPopupEventHandler;
            form.ShowDialog();

            Logger.Write(LogType.GUI, "Clicked Align Camera Setting Dialog");
        }

        private void CloseMotionPopupEventHandler(UnitName unitName)
        {
            MotionPopupForm?.Close();
        }

        private void btnAkkonCameraSetting_Click(object sender, EventArgs e)
        {
            if (LAFManager.Instance().GetLAF("AlignLaf") is LAF alignLAF)
                alignLAF.LafCtrl.SetTrackingOnOFF(false);

            OpticTeachingForm form = new OpticTeachingForm();
            form.LineCamera = LineCameraManager.Instance().GetLineCamera("AkkonCamera");
            form.LAFCtrl = LAFManager.Instance().GetLAF("AkkonLaf").LafCtrl;
            form.UnitName = UnitName.Unit0;
            form.AxisNameZ = Jastech.Framework.Device.Motions.AxisName.Z0;
            form.AxisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.CloseMotionPopupEventHandler += CloseMotionPopupEventHandler;
            form.ShowDialog();

            Logger.Write(LogType.GUI, "Clicked Akkon Camera Setting Dialog");
        }

        private void btnAlignInspectionPage_Click(object sender, EventArgs e)
        {
            InspectionTeachingForm form = new InspectionTeachingForm();
            form.ScanImage = ScanImage?.CopyBase(CogImageCopyModeConstants.CopyPixels);
            form.AxisHandlerName = AxisHandlerName.Handler0;
            form.UnitName = UnitName.Unit0;
            form.LineCamera = LineCameraManager.Instance().GetLineCamera("AlignCamera");
            form.UseDelayStart = true;
            form.LAFCtrl = LAFManager.Instance().GetLAF("AlignLaf").LafCtrl;
            form.UseAlignCamMark = true;
            form.UseAlignTeaching = true;
            form.UseAkkonTeaching = false;
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.CloseMotionPopupEventHandler += CloseMotionPopupEventHandler;
            form.ShowDialog();

            Logger.Write(LogType.GUI, "Clicked Align Inspection Setting Dialog");
        }

        private void btnAkkonInspectionPage_Click(object sender, EventArgs e)
        {
            InspectionTeachingForm form = new InspectionTeachingForm();
            form.ScanImage = ScanImage?.CopyBase(CogImageCopyModeConstants.CopyPixels);
            form.AxisHandlerName = AxisHandlerName.Handler0;
            form.UnitName = UnitName.Unit0;
            form.LineCamera = LineCameraManager.Instance().GetLineCamera("AkkonCamera");
            form.LAFCtrl = LAFManager.Instance().GetLAF("AkkonLaf").LafCtrl;
            form.UseAlignCamMark = false;
            form.UseAlignTeaching = false;
            form.UseAkkonTeaching = true;
            form.InspModelService = ATTInspModelService;
            form.OpenMotionPopupEventHandler += OpenMotionPopupEventHandler;
            form.CloseMotionPopupEventHandler += CloseMotionPopupEventHandler;
            form.ShowDialog();

            Logger.Write(LogType.GUI, "Clicked Akkon Inspection Setting Dialog");
        }

        private void OpenMotionPopupEventHandler(UnitName unitName)
        {
            if (MotionPopupForm == null)
            {
                MotionPopupForm = new MotionPopupForm();
                MotionPopupForm.UnitName = unitName;
                MotionPopupForm.AxisHandler = MotionManager.Instance().GetAxisHandler(AxisHandlerName.Handler0);
                MotionPopupForm.AkkonLafCtrl = LAFManager.Instance().GetLAF("AkkonLaf").LafCtrl;
                MotionPopupForm.AlignLafCtrl = LAFManager.Instance().GetLAF("AlignLaf").LafCtrl;
                MotionPopupForm.InspModelService = ATTInspModelService;
                MotionPopupForm.CloseEventDelegate = () => MotionPopupForm = null;
                MotionPopupForm.Show();
            }
            else
                MotionPopupForm.Focus();
        }

        internal void SetInspModelService(ATTInspModelService inspModelService)
        {
            ATTInspModelService = inspModelService;
        }
        #endregion
    }
}
