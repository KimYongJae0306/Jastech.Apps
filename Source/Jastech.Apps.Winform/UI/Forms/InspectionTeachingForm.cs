using Cognex.VisionPro;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.Parameters;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Settings;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Apps.Winform.UI.Forms;
using Jastech.Framework.Algorithms.Akkon;
using Jastech.Framework.Algorithms.Akkon.Parameters;
using Jastech.Framework.Algorithms.UI.Controls;
using Jastech.Framework.Config;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Device.LightCtrls;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using Jastech.Framework.Structure;
using Jastech.Framework.Structure.Service;
using Jastech.Framework.Users;
using Jastech.Framework.Util;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform.Helper;
using Jastech.Framework.Winform.VisionPro.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Jastech.Framework.Winform.Forms
{
    public partial class InspectionTeachingForm : Form
    {
        #region 필드
        private Color _selectedColor;

        private Color _nonSelectedColor;

        private bool _isLoading { get; set; } = false;

        private DisplayType _displayType { get; set; } = DisplayType.Align;

        private string _currentTabNo { get; set; } = "";

        private bool _isPrevTrackingOn { get; set; } = false;

        private TabInspResult _tabInspResult { get; set; } = null;

        private MainAlgorithmTool _algorithmTool = new MainAlgorithmTool();
        #endregion

        #region 속성
        public InspModelService InspModelService { get; set; } = null;

        public UnitName UnitName { get; set; } = UnitName.Unit0;

        public ICogImage ScanImage { get; set; } = null;

        public List<Tab> TeachingTabList { get; private set; } = null;

        public List<ATTInspTab> InspTabList { get; set; } = new List<ATTInspTab>();

        public Tab CurrentTab { get; set; } = null;

        public LineCamera LineCamera { get; set; }

        public bool UseDelayStart { get; set; } = false;

        public string TeachingImagePath { get; set; }

        private CogTeachingDisplayControl Display { get; set; } = null;

        private AlignControl AlignControl { get; set; } = null;

        private AkkonControl AkkonControl { get; set; } = null;

        private MarkControl MarkControl { get; set; } = null;

        public LAFCtrl LAFCtrl { get; set; } = null;

        public bool UseAlignTeaching { get; set; } = true;

        public bool UseAkkonTeaching { get; set; } = true;

        public bool UseAlignMark { get; set; } = false;

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
        public MotionPopupDelegate OpenMotionPopupEventHandler;

        public MotionPopupDelegate CloseMotionPopupEventHandler;
        #endregion

        #region 델리게이트
        public delegate void MotionPopupDelegate(UnitName unitName);

        public delegate void UpdateDisplayDele(ICogImage cogImage);
        #endregion

        #region 생성자
        public InspectionTeachingForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void InspectionTeachingForm_Load(object sender, EventArgs e)
        {
            _isLoading = true;

            TeachingData.Instance().UpdateTeachingData();
            TeachingData.Instance().GetUnit(UnitName.ToString()).GetTabList().Sort((x, y) => x.Index.CompareTo(y.Index));
            TeachingTabList = TeachingData.Instance().GetUnit(UnitName.ToString()).GetTabList();
            InitializeTabComboBox();
            AddControl();
            InitializeUI();

            if (ScanImage != null)
            {
                TeachingUIManager.Instance().SetOrginCogImageBuffer(ScanImage);
                Display.SetImage(TeachingUIManager.Instance().GetOriginCogImageBuffer(false));
            }

            _isLoading = false;

            lblStageCam.Text = $"STAGE : {UnitName} / CAM : {LineCamera.Camera.Name}";

            LineCamera.GrabDoneEventHandler += InspectionTeachingForm_GrabDoneEventHandler;

            var image = TeachingUIManager.Instance().GetOriginCogImageBuffer(true);

            if (image != null)
                Display.SetImage(image);

            SelectPage(DisplayType.Mark);
        }

        private void InitializeTabComboBox()
        {
            cbxTabList.Items.Clear();

            foreach (var item in TeachingTabList)
                cbxTabList.Items.Add(item.Name);

            cbxTabList.SelectedIndex = 0;
            CurrentTab = TeachingTabList[0];
            _currentTabNo = cbxTabList.SelectedItem as string;
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);
        }

        private void AddControl()
        {
            // Display Control
            Display = new CogTeachingDisplayControl();
            Display.Dock = DockStyle.Fill;

            //Event 연결
            Display.DeleteEventHandler += Display_DeleteEventHandler;
            pnlDisplay.Controls.Add(Display);

            // TeachingUIManager 참조
            TeachingUIManager.Instance().TeachingDisplayControl = Display;

            MarkControl = new MarkControl();
            MarkControl.Dock = DockStyle.Fill;
            MarkControl.UseAlignMark = UseAlignMark;
            MarkControl.SetParams(CurrentTab);
            pnlTeach.Controls.Add(MarkControl);

            AlignControl = new AlignControl();
            AlignControl.Dock = DockStyle.Fill;
            AlignControl.SetParams(CurrentTab);
            pnlTeach.Controls.Add(AlignControl);

            AkkonControl = new AkkonControl();
            AkkonControl.Dock = DockStyle.Fill;
            AkkonControl.SetParams(CurrentTab);
            pnlTeach.Controls.Add(AkkonControl);

            if (UseAlignTeaching)
                tlpTeachingItems.Controls.Add(btnAlign, 2, 0); 
            else
                btnAlign.Visible = false;

            if (UseAkkonTeaching)
                tlpTeachingItems.Controls.Add(btnAkkon, 2, 0); 
            else
                btnAkkon.Visible = false;
        }

        private void InspectionTeachingForm_GrabDelayStartEventHandler(string cameraName)
        {
            LAFCtrl.SetTrackingOnOFF(true);
        }

        private void InspectionTeachingForm_GrabDoneEventHandler(string cameraName, bool isGrabDone)
        {
            int tabNo = Convert.ToInt32(_currentTabNo);
            UpdateDisplayImage(tabNo);
        }

        private void UpdateDisplay(ICogImage cogImage)
        {
            if (this.InvokeRequired)
            {
                UpdateDisplayDele callback = UpdateDisplay;
                BeginInvoke(callback, cogImage);
                return;
            }

            if (cogImage == null)
                return;

            Display.SetImage(cogImage);
        }

        private void Display_DeleteEventHandler(object sender, EventArgs e)
        {
            if (pnlTeach.Controls.Count > 0)
            {
                if (pnlTeach.Controls[0] as MarkControl != null)
                    MarkControl.DrawROI();
                else if (pnlTeach.Controls[0] as AlignControl != null)
                    AlignControl.DrawROI();
                else if (pnlTeach.Controls[0] as AkkonControl != null)
                    AkkonControl.DrawROI();
            }
        }

        private void btnMark_Click(object sender, EventArgs e)
        {
            if (_isPrevTrackingOn)
                SetTrackingOnOff(false);
            SelectPage(DisplayType.Mark);
        }

        private void btnAlign_Click(object sender, EventArgs e)
        {
            SelectPage(DisplayType.Align);
        }

        private void btnAkkon_Click(object sender, EventArgs e)
        {
            SelectPage(DisplayType.Akkon);
        }

        private void SelectPage(DisplayType type)
        {
            if (ModelManager.Instance().CurrentModel == null || UnitName.ToString() == "")
                return;

            ClearSelectedButton();

            _displayType = type;

            pnlTeach.Controls.Clear();
            TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay().ClearGraphic();

            var orgImage = TeachingUIManager.Instance().GetOriginCogImageBuffer(false);
            if (orgImage != null)
            {
                TeachingUIManager.Instance().TeachingDisplayControl.SetImage(orgImage);
                TeachingUIManager.Instance().TeachingDisplayControl.SetThumbnailImage(orgImage);
            }
            switch (type)
            {
                case DisplayType.Mark:
                    btnMark.BackColor = _selectedColor;
                    MarkControl.SetParams(CurrentTab);
                    pnlTeach.Controls.Add(MarkControl);
                    MarkControl.DrawROI();
                    break;

                case DisplayType.Align:
                    btnAlign.BackColor = _selectedColor;
                    AlignControl.SetParams(CurrentTab);

                    var camera = LineCamera.Camera;
                    AlignControl.Resolution_um = camera.PixelResolution_um / camera.LensScale;

                    pnlTeach.Controls.Add(AlignControl);
                    AlignControl.DrawROI();
                    break;

                case DisplayType.Akkon:
                    btnAkkon.BackColor = _selectedColor;
                    AkkonControl.SetParams(CurrentTab);
                    AkkonControl.Resolution_um = LineCamera.Camera.PixelResolution_um / LineCamera.Camera.LensScale;

                    if (UserManager.Instance().CurrentUser.Type == AuthorityType.Maker)
                        AkkonControl.SetUserMaker(true);
                    else
                        AkkonControl.SetUserMaker(false);
                    pnlTeach.Controls.Add(AkkonControl);
                    AkkonControl.DrawROI();
                    break;

                default:
                    break;
            }
        }

        private void ClearSelectedButton()
        {
            foreach (Control control in tlpTeachingItems.Controls)
            {
                if (control is Button)
                    control.BackColor = _nonSelectedColor;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AppsInspModel model = ModelManager.Instance().CurrentModel as AppsInspModel;

            if (model == null)
                return;

            MessageYesNoForm yesNoForm = new MessageYesNoForm();
            yesNoForm.Message = "Teaching data will change.\nDo you agree?";

            if (yesNoForm.ShowDialog() == DialogResult.Yes)
            {
                if (_isPrevTrackingOn == true)
                    SetTrackingOnOff(false);
                SaveModelData(model);

                MessageConfirmForm confirmForm = new MessageConfirmForm();
                confirmForm.Message = "Save Model Completed.";
                confirmForm.ShowDialog();
            }
        }

        private void SaveModelData(AppsInspModel model)
        {
            AkkonControl.SaveAkkonParam();
            model.SetUnitList(TeachingData.Instance().UnitList);

            string fileName = Path.Combine(ConfigSet.Instance().Path.Model, model.Name, InspModel.FileName);
            InspModelService?.Save(fileName, model);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ReadOnlyChecked = true;
            dlg.Filter = "BMP Files (*.bmp)|*.bmp";
            dlg.ShowDialog();

            if (_isPrevTrackingOn == true)
                SetTrackingOnOff(false);

            if (dlg.FileName != "")
            {
                Mat image = new Mat(dlg.FileName, ImreadModes.Grayscale);

                int size = image.Width * image.Height * image.NumberOfChannels;
                byte[] dataArray = new byte[size];
                Marshal.Copy(image.DataPointer, dataArray, 0, size);

                ColorFormat format = image.NumberOfChannels == 1 ? ColorFormat.Gray : ColorFormat.RGB24;

                var cogImage = VisionProImageHelper.ConvertImage(dataArray, image.Width, image.Height, format);

                TeachingUIManager.Instance().SetOrginCogImageBuffer(cogImage);
                TeachingUIManager.Instance().SetOriginMatImageBuffer(new Mat(dlg.FileName, ImreadModes.Grayscale));

                var orgImage = TeachingUIManager.Instance().GetOriginCogImageBuffer(false);
                Display.SetImage(orgImage);
                Display.SetThumbnailImage(orgImage);

                int tabNo = Convert.ToInt32(_currentTabNo);
                UpdateDisplayImage(tabNo);
            }
        }

        private void SaveScanImage()
        {
            for (int i = 0; i < 5; i++)
            {
                var teacingData = TeachingData.Instance().GetBufferImage(i);
                if (teacingData == null)
                    return;
                teacingData.TabImage.Save(string.Format(@"D:\tab_{0}.bmp", i));
            }
        }

        private void btnMotionPopup_Click(object sender, EventArgs e)
        {
            OpenMotionPopupEventHandler?.Invoke(UnitName);
        }

        private void btnGrabStart_Click(object sender, EventArgs e)
        {
            LineCamera.StopGrab();

            LAFCtrl.SetTrackingOnOFF(false);
            Thread.Sleep(100);

            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            TeachingImagePath = Path.Combine(ConfigSet.Instance().Path.Model, inspModel.Name, "TeachingImage", DateTime.Now.ToString("yyyyMMdd_HHmmss"));

            var teachingInfo = inspModel.GetUnit(UnitName.Unit0).GetTeachingInfo(TeachingPosType.Stage1_Scan_Start);

            double targetPosZ = 0.0;

            if (UseAlignMark == false)
                targetPosZ = teachingInfo.GetTargetPosition(AxisName.Z0.ToString());
            else
                targetPosZ = teachingInfo.GetTargetPosition(AxisName.Z1.ToString());

            var cameraGap = AppsConfig.Instance().CameraGap_mm;

            TeachingData.Instance().ClearTeachingImageBuffer();
            LineCamera.ClearTabScanBuffer();

            if (UseDelayStart)
                LineCamera.InitGrabSettings(cameraGap);
            else
                LineCamera.InitGrabSettings();

            InitalizeInspTab(LineCamera.TabScanBufferList);

            LAFCtrl.SetTrackingOnOFF(true);
            Thread.Sleep(100);

            MotionManager.Instance().MoveTo(TeachingPosType.Stage1_Scan_Start);

            string cameraName = LineCamera.Camera.Name;
            var unit = inspModel.GetUnit(UnitName);
            DeviceManager.Instance().LightCtrlHandler.TurnOn(unit.LightParam);
            Thread.Sleep(100);

            LineCamera.StartGrab();
            Thread.Sleep(50);
            if (UseDelayStart)
                MotionManager.Instance().MoveTo(TeachingPosType.Stage1_Scan_End, cameraGap);
            else
                MotionManager.Instance().MoveTo(TeachingPosType.Stage1_Scan_End);

            LAFCtrl.SetTrackingOnOFF(false);
            Thread.Sleep(100);
            DeviceManager.Instance().LightCtrlHandler.TurnOff();
        }

        public void InitalizeInspTab(List<TabScanBuffer> bufferList)
        {
            DisposeInspTabList();
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            foreach (var buffer in bufferList)
            {
                ATTInspTab inspTab = new ATTInspTab();
                inspTab.TabScanBuffer = buffer;
                inspTab.TeachingEvent += TeachingEventFunction;
                inspTab.StartTeacingTask();
                InspTabList.Add(inspTab);
            }
        }

        private void DisposeInspTabList()
        {
            foreach (var inspTab in InspTabList)
            {
                inspTab.TeachingEvent -= TeachingEventFunction;
                inspTab.StopTeachingTask();
                inspTab.Dispose();
            }
            InspTabList.Clear();
        }

        private void TeachingEventFunction(ATTInspTab inspTab)
        {
            var teachingData = TeachingData.Instance();
            teachingData.AddBufferImage(inspTab.TabScanBuffer.TabNo, inspTab.MergeMatImage);
        }

        private void btnGrabStop_Click(object sender, EventArgs e)
        {
            LineCamera.StopGrab();
        }

        private void InspectionTeachingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseMotionPopupEventHandler?.Invoke(UnitName);

            if (LineCamera.Camera.IsGrabbing())
                LineCamera.StopGrab();

            LAFCtrl?.SetTrackingOnOFF(false);
            LineCamera.StopGrab();
            Display.DisposeImage();
            MarkControl.DisposeImage();
            DisposeInspTabList();
            LineCamera.GrabDoneEventHandler -= InspectionTeachingForm_GrabDoneEventHandler;

            ControlDisplayHelper.DisposeChildControls(AkkonControl);
            ControlDisplayHelper.DisposeChildControls(AlignControl);
        }

        private void cbxTabList_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawComboboxCenterAlign(sender, e);
        }

        private void DrawComboboxCenterAlign(object sender, DrawItemEventArgs e)
        {
            try
            {
                ComboBox cmb = sender as ComboBox;

                if (cmb != null)
                {
                    e.DrawBackground();

                    if (cmb.Name.ToString().ToLower().Contains("group"))
                        cmb.ItemHeight = lblPrev.Height - 6;
                    else
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
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
                throw;
            }
        }

        private void cbxTabList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoading)
                return;

            string tabIndex = cbxTabList.SelectedItem as string;
            int tabNo = Convert.ToInt32(tabIndex);

            if (_currentTabNo == tabIndex)
                return;

            if (_isPrevTrackingOn)
                SetTrackingOnOff(false);

            CurrentTab = TeachingTabList.Where(x => x.Index == tabNo).FirstOrDefault();
            _currentTabNo = tabIndex;

            UpdateDisplayImage(tabNo);
        }

        private void UpdateDisplayImage(int tabNo)
        {
            var teachingData = TeachingData.Instance();

            if (teachingData.GetBufferImage(tabNo) is TeachingImageBuffer buffer)
            {
                if (buffer.TabImage == null)
                    return;

                Mat temp = buffer.TabImage.Clone() as Mat;
                ICogImage cogImage = teachingData.ConvertCogGrayImage(temp).CopyBase(CogImageCopyModeConstants.CopyPixels);

                Display.SetImage(cogImage);
                TeachingUIManager.Instance().SetOrginCogImageBuffer(cogImage);
                TeachingUIManager.Instance().SetOriginMatImageBuffer(temp);

                (cogImage as CogImage8Grey).Dispose();
            }

            if (_displayType == DisplayType.Mark)
            {
                MarkControl.SetParams(CurrentTab);
                MarkControl.DrawROI();
            }
            else if (_displayType == DisplayType.Align)
            {
                AlignControl.SetParams(CurrentTab);
                AlignControl.UpdateCurrentParam();
                AlignControl.DrawROI();
            }
            else if (_displayType == DisplayType.Akkon)
            {
                AkkonControl.SetParams(CurrentTab);
                AkkonControl.DrawROI();
            }

            GC.Collect();
        }

        private void lblPrev_Click(object sender, EventArgs e)
        {
            if (cbxTabList.SelectedIndex <= 0)
                return;

            cbxTabList.SelectedIndex -= 1;
        }

        private void lblNext_Click(object sender, EventArgs e)
        {
            int nextIndex = cbxTabList.SelectedIndex + 1;

            if (cbxTabList.Items.Count > nextIndex)
                cbxTabList.SelectedIndex = nextIndex;
        }

        private void lblInspection_Click(object sender, EventArgs e)
        {
            if (_displayType == DisplayType.Mark)
                InspectionMark();
            else if (_displayType == DisplayType.Align)
                InspectionAlign();
            else if (_displayType == DisplayType.Akkon)
                InspectionAkkon();
        }

        private void lblAddROI_Click(object sender, EventArgs e)
        {
            if (_displayType == DisplayType.Mark)
                MarkControl.AddROI();
            else if (_displayType == DisplayType.Align)
                AlignControl.AddROI();
            else if (_displayType == DisplayType.Akkon)
                AkkonControl.AddROI();
        }

        private void lblROIJog_Click(object sender, EventArgs e)
        {
            if (_displayType == DisplayType.Mark)
                MarkControl.ShowROIJog();
            else if (_displayType == DisplayType.Align)
                AlignControl.ShowROIJog();
            else if (_displayType == DisplayType.Akkon)
                AkkonControl.ShowROIJog();
        }

        private void lblTracking_Click(object sender, EventArgs e)
        {
            if (_displayType == DisplayType.Akkon)
                AkkonControl.SetOrginImageView();

            if (_isPrevTrackingOn == false)
                SetTrackingOnOff(true);
            else
                SetTrackingOnOff(false);
        }

        private Tab _originTabData { get; set; } = null;

        private void SetTrackingOnOff(bool isOn)
        {
            var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();
            if (display == null)
                return;

            ICogImage cogImage = display.GetImage();
            if (cogImage == null)
                return;

            if (_tabInspResult != null)
            {
                _tabInspResult.Dispose();
                _tabInspResult = null;
            }

            _tabInspResult = new TabInspResult();
            if (UseAkkonTeaching && UseAlignTeaching == false)
            {
                _tabInspResult.MarkResult.FpcMark = new MarkResult();
                _tabInspResult.MarkResult.FpcMark.Judgement = Judgement.OK;

                _tabInspResult.MarkResult.PanelMark = _algorithmTool.RunPanelMark(cogImage, CurrentTab, false);
            }
            else
            {
                _tabInspResult.MarkResult.FpcMark = _algorithmTool.RunFpcMark(cogImage, CurrentTab, UseAlignMark);
                _tabInspResult.MarkResult.PanelMark = _algorithmTool.RunPanelMark(cogImage, CurrentTab, UseAlignMark);
            }

            var coordinate = TeachingData.Instance().Coordinate;
            var markResult = _tabInspResult.MarkResult;

            if (_tabInspResult.MarkResult.Judgement != Judgement.OK)
            {
                // 검사 실패
                string message = string.Format("Mark Insp NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", CurrentTab.Index + 1,
                    markResult.FpcMark.Judgement, markResult.PanelMark.Judgement);

                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = message;
                form.ShowDialog();

                _tabInspResult.Dispose();
                _tabInspResult = null;
                return;
            }

            if (isOn)
            {
                lblTracking.BackColor = _selectedColor;
                coordinate.SetCoordinateAkkon(CurrentTab, markResult.PanelMark.FoundedMark);

                coordinate.SetCoordinateFpcAlign(CurrentTab, markResult.FpcMark.FoundedMark);
                coordinate.SetCoordinatePanelAlign(CurrentTab, markResult.PanelMark.FoundedMark);
            }
            else
            {
                lblTracking.BackColor = _nonSelectedColor;
                coordinate.SetReverseCoordinateAkkon(CurrentTab, markResult.PanelMark.FoundedMark);

                coordinate.SetReverseCoordinateFpcAlign(CurrentTab, markResult.FpcMark.FoundedMark);
                coordinate.SetReverseCoordinatePanelAlign(CurrentTab, markResult.PanelMark.FoundedMark);
            }

            coordinate.ExcuteCoordinateAkkon(CurrentTab);
            coordinate.ExecuteCoordinateAlign(CurrentTab);


            if (_displayType == DisplayType.Akkon)
            {
                AkkonControl.DrawROI();
                AkkonControl.SetParams(CurrentTab);
            }
            else if (_displayType == DisplayType.Align)
                AlignControl.DrawROI();
            else { }

            _isPrevTrackingOn = isOn;
        }

        //private MarkParam SetCoordinateMark(MarkParam param, VisionProPatternMatchingResult result)
        //{
        //    var newParam = param.DeepCopy();

        //    CogTransform2DLinear newOrigin = new CogTransform2DLinear();
        //    newOrigin.TranslationX = result.MaxMatchPos.FoundPos.X;
        //    newOrigin.TranslationY = result.MaxMatchPos.FoundPos.Y;
        //    newParam.InspParam.SetOrigin(newOrigin);

        //    CogRectangle newTrainRegion = new CogRectangle(newParam.InspParam.GetTrainRegion() as CogRectangle);
        //    newTrainRegion.SetCenterWidthHeight(newOrigin.TranslationX, newOrigin.TranslationY, newTrainRegion.Width, newTrainRegion.Height);
        //    newParam.InspParam.SetTrainRegion(newTrainRegion);

        //    CogRectangle newSearchRegion = new CogRectangle(newParam.InspParam.GetSearchRegion() as CogRectangle);
        //    newSearchRegion.SetCenterWidthHeight(newOrigin.TranslationX, newOrigin.TranslationY, newSearchRegion.Width, newSearchRegion.Height);
        //    newParam.InspParam.SetSearchRegion(newSearchRegion);

        //    return newParam;
        //}

        private void lblStageCam_Click(object sender, EventArgs e)
        {
            String dir = ConfigSet.Instance().Path.Model;
            Process.Start(dir);
        }

        private void lblImageSave_Click(object sender, EventArgs e)
        {
            SaveScanImage();
        }

        private void lblROICopy_Click(object sender, EventArgs e)
        {
            ROICopyForm form = new ROICopyForm();
            form.SetUnitName(UnitName.Unit0);
            form.ShowDialog();
        }

        public void InspectionMark()
        {
            var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();
            if (display == null)
                return;

            ICogImage cogImage = display.GetImage();
            if (cogImage == null)
                return;

            display.ClearGraphic();
            display.DisplayRefresh();

            MainAlgorithmTool algorithmTool = new MainAlgorithmTool();
            TabInspResult tabInspResult = new TabInspResult();

            if(UseAkkonTeaching && UseAlignTeaching == false)
                algorithmTool.MainPanelMarkInspect(cogImage, CurrentTab, ref tabInspResult);
            else
                algorithmTool.MainMarkInspect(cogImage, CurrentTab, ref tabInspResult, UseAlignMark);

            if (tabInspResult.MarkResult.Judgement != Judgement.OK)
            {
                // 검사 실패
                string message = string.Format("Mark Insp NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", CurrentTab.Index,
                    tabInspResult.MarkResult.FpcMark.Judgement, tabInspResult.MarkResult.PanelMark.Judgement);

                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = message;
                form.ShowDialog();
            }
            display.ClearGraphic();
            display.UpdateGraphic(GetMarkResultGrapics(tabInspResult));
        }


        public void InspectionAlign()
        {
            var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();
            if (display == null)
                return;

            ICogImage cogImage = display.GetImage();
            if (cogImage == null)
                return;

            display.ClearGraphic();
            display.DisplayRefresh();

            MainAlgorithmTool algorithmTool = new MainAlgorithmTool();
            TabInspResult tabInspResult = new TabInspResult();

            var camera = LineCamera.Camera;
            double resolution_um = camera.PixelResolution_um / camera.LensScale;

            double judgementX = CurrentTab.AlignSpec.LeftSpecX_um / resolution_um;
            double judgementY = CurrentTab.AlignSpec.LeftSpecY_um / resolution_um;

            tabInspResult.AlignResult.LeftX = algorithmTool.RunMainLeftAutoAlignX(cogImage, CurrentTab, new PointF(0, 0), new PointF(0, 0), judgementX);
            tabInspResult.AlignResult.LeftY = algorithmTool.RunMainLeftAlignY(cogImage, CurrentTab, new PointF(0, 0), new PointF(0, 0), judgementY);
            tabInspResult.AlignResult.RightX = algorithmTool.RunMainRightAutoAlignX(cogImage, CurrentTab, new PointF(0, 0), new PointF(0, 0), judgementX);
            tabInspResult.AlignResult.RightY = algorithmTool.RunMainRightAlignY(cogImage, CurrentTab, new PointF(0, 0), new PointF(0, 0), judgementY);

            display.ClearGraphic();

            List<CogCompositeShape> shapeList = new List<CogCompositeShape>();

            shapeList.AddRange(GetMarkResultGrapics(tabInspResult));

            shapeList.AddRange(GetAlignResultGraphics(tabInspResult.AlignResult.LeftX));
            shapeList.AddRange(GetAlignResultGraphics(tabInspResult.AlignResult.LeftY));
            shapeList.AddRange(GetAlignResultGraphics(tabInspResult.AlignResult.RightX));
            shapeList.AddRange(GetAlignResultGraphics(tabInspResult.AlignResult.RightY));

            display.UpdateGraphic(shapeList);
            AlignControl.UpdateData(tabInspResult);
        }

        public void InspectionAkkon()
        {
            var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();
            if (display == null)
                return;

            ICogImage cogImage = display.GetImage();
            if (cogImage == null)
                return;

            Mat matImage = TeachingUIManager.Instance().GetOriginMatImageBuffer(false);
            if (matImage == null)
                return;

            ICogImage orgCogImage = TeachingUIManager.Instance().GetOriginCogImageBuffer(false);

            MainAlgorithmTool algorithmTool = new MainAlgorithmTool();
            TabInspResult tabInspResult = new TabInspResult();

            var akkonParam = CurrentTab.AkkonParam.AkkonAlgoritmParam;

            var roiList = CurrentTab.AkkonParam.GetAkkonROIList();

            Judgement tabJudgement = Judgement.NG;

            var camera = LineCamera.Camera;
            double resolution_um = camera.PixelResolution_um / camera.LensScale;

            var akkonAlgorithm = AkkonControl.AkkonAlgorithm;
            akkonAlgorithm.UseOverCount = AppsConfig.Instance().EnableTest2;
            var leadResultList = akkonAlgorithm.Run(matImage, roiList, akkonParam, (float)resolution_um, ref tabJudgement);
            tabInspResult.AkkonResult = new Framework.Algorithms.Akkon.Results.AkkonResult();
            tabInspResult.AkkonResult.Judgement = tabJudgement;
            tabInspResult.AkkonInspMatImage = akkonAlgorithm.ResizeMat;

            Mat resultMat = AkkonControl.GetResultImage(matImage, leadResultList, akkonParam, ref tabInspResult.AkkonNGAffineList);
            tabInspResult.AkkonResultCogImage = AkkonControl.ConvertCogColorImage(resultMat);

            Mat resizeMat = MatHelper.Resize(matImage, akkonParam.ImageFilterParam.ResizeRatio);
            var akkonCogImage = AkkonControl.ConvertCogGrayImage(resizeMat);
            TeachingUIManager.Instance().SetAkkonCogImage(akkonCogImage);

            var resultCogImage = AkkonControl.ConvertCogColorImage(resultMat);
            TeachingUIManager.Instance().SetResultCogImage(resultCogImage, tabInspResult.AkkonNGAffineList);

            resizeMat.Dispose();
            resultMat.Dispose();
            display.ClearGraphic();

            AkkonControl.InspectionDoneUI();
        }

        private List<CogCompositeShape> GetMarkResultGrapics(TabInspResult tabInspResult)
        {
            List<CogCompositeShape> shapeList = new List<CogCompositeShape>();

            if (tabInspResult.MarkResult.FpcMark != null)
            {
                var foundedFpcMark = tabInspResult.MarkResult.FpcMark.FoundedMark;
                if(foundedFpcMark != null)
                {
                    var leftFpc = foundedFpcMark.Left.MaxMatchPos.ResultGraphics;

                    var rightFpc = foundedFpcMark.Right.MaxMatchPos.ResultGraphics;

                    if (foundedFpcMark.Left.Found)
                        shapeList.Add(leftFpc);

                    if (foundedFpcMark.Right.Found)
                        shapeList.Add(rightFpc);
                }
            }

            if (tabInspResult.MarkResult.PanelMark != null)
            {
                var foundedPanelMark = tabInspResult.MarkResult.PanelMark.FoundedMark;
                if(foundedPanelMark != null)
                {
                    var leftPanel = foundedPanelMark.Left.MaxMatchPos.ResultGraphics;
                    var rightPanel = foundedPanelMark.Right.MaxMatchPos.ResultGraphics;

                    if (foundedPanelMark.Left.Found)
                        shapeList.Add(leftPanel);
                    if (foundedPanelMark.Right.Found)
                        shapeList.Add(rightPanel);
                }
            }

            return shapeList;
        }

        private List<CogCompositeShape> GetAlignResultGraphics(AlignResult alignResult)
        {
            List<CogCompositeShape> shapeList = new List<CogCompositeShape>();
            if (alignResult.Fpc.CogAlignResult.Count() > 0)
            {
                foreach (var result in alignResult.Fpc.CogAlignResult)
                {
                    if (result != null)
                    {
                        if (result.Found)
                        {
                            var display = TeachingUIManager.Instance().TeachingDisplayControl.GetDisplay();
                            shapeList.Add(result.MaxCaliperMatch.ResultGraphics);
                            CogPointMarker mark = new CogPointMarker();

                            mark.SetCenterRotationSize(result.MaxCaliperMatch.FoundPosX, result.MaxCaliperMatch.FoundPosY, 0, 10);
                            CogGraphicInteractiveCollection collect = new CogGraphicInteractiveCollection();
                            collect.Add(mark);

                            display.SetInteractiveGraphics("test", collect);

                        }
                    }
                }
            }

            if (alignResult.Panel.CogAlignResult.Count() > 0)
            {
                foreach (var result in alignResult.Panel.CogAlignResult)
                {
                    if (result != null)
                    {
                        if (result.Found)
                            shapeList.Add(result.MaxCaliperMatch.ResultGraphics);
                    }
                }
            }

            return shapeList;
        }
        #endregion
    }

    public enum DisplayType
    {
        Mark,
        Align,
        Akkon,

        PreAlign,
        Calibration,
    }
}
