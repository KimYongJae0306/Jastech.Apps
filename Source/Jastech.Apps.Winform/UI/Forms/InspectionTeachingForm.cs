using Cognex.VisionPro;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Settings;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Apps.Winform.UI.Forms;
using Jastech.Framework.Config;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Structure;
using Jastech.Framework.Structure.Service;
using Jastech.Framework.Users;
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
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Jastech.Framework.Winform.Forms
{
    public partial class InspectionTeachingForm : Form
    {
        #region 필드
        private bool _isWaitingUpdateUI { get; set; } = false;

        private Color _selectedColor;

        private Color _nonSelectedColor;

        private bool _isLoading { get; set; } = false;

        private DisplayType _displayType { get; set; } = DisplayType.Align;

        private string _currentTabNo { get; set; } = "";

        private bool _isPrevTrackingOn { get; set; } = false;

        private TabInspResult _tabInspResult { get; set; } = null;

        private MainAlgorithmTool _algorithmTool = new MainAlgorithmTool();

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

        #region 속성
        public InspModelService InspModelService { get; set; } = null;

        public UnitName UnitName { get; set; } = UnitName.Unit0;

        public AxisHandlerName AxisHandlerName { get; set; } = AxisHandlerName.Handler0;

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

        private AFTriggerOffsetSettingControl TriggerSettingControl { get; set; } = null;

        public LAFCtrl LAFCtrl { get; set; } = null;

        public bool UseAlignTeaching { get; set; } = true;

        public bool UseAkkonTeaching { get; set; } = true;

        public bool UseAlignCamMark { get; set; } = false;

        private ROIJogForm ROIJogForm { get; set; } = null;
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
            if (LineCamera != null)
            {
                var camera = LineCamera.Camera;
                Display.PixelResolution = camera.PixelResolution_um / camera.LensScale;
            }
            Display.Dock = DockStyle.Fill;

            //Event 연결
            Display.DeleteEventHandler += Display_DeleteEventHandler;
            pnlDisplay.Controls.Add(Display);

            // TeachingUIManager 참조
            TeachingUIManager.Instance().TeachingDisplayControl = Display;

            MarkControl = new MarkControl();
            MarkControl.Dock = DockStyle.Fill;
            MarkControl.UseAlignCamMark = UseAlignCamMark;
            MarkControl.SetParams(CurrentTab);
            MarkControl.MarkParamChanged += MarkControl_MarkParamChanged;
            MarkControl.OpenROIJogEventHandler += OpenRoiJogEventHandler;
            MarkControl.CloseROIJogEventHandler += CloseRoiJogEventHandler;
            pnlTeach.Controls.Add(MarkControl);

            AlignControl = new AlignControl();
            AlignControl.Dock = DockStyle.Fill;
            AlignControl.SetParams(CurrentTab);
            AlignControl.OpenROIJogEventHandler += OpenRoiJogEventHandler;
            AlignControl.CloseROIJogEventHandler += CloseRoiJogEventHandler;
            pnlTeach.Controls.Add(AlignControl);

            AkkonControl = new AkkonControl();
            AkkonControl.Dock = DockStyle.Fill;
            AkkonControl.SetParams(CurrentTab);
            AkkonControl.OpenROIJogEventHandler += OpenRoiJogEventHandler;
            AkkonControl.CloseROIJogEventHandler += CloseRoiJogEventHandler;
            pnlTeach.Controls.Add(AkkonControl);

            TriggerSettingControl = new AFTriggerOffsetSettingControl();
            TriggerSettingControl.Dock = DockStyle.Fill;
            TriggerSettingControl.UseAlignCamMark = UseAlignCamMark;
            pnlTeach.Controls.Add(TriggerSettingControl);

            OptimizeUIByTeachingItem();
        }

        private void OpenRoiJogEventHandler()
        {
            if (ROIJogForm == null)
            {
                ROIJogForm = new ROIJogForm();
                ROIJogForm.CloseEventDelegate = () => ROIJogForm = null;
                ROIJogForm.SetTeachingItem(ConvertDisplayTypeToTeachingItem(_displayType));
                ROIJogForm.SendEventHandler += ROIJogForm_SendEventHandler;
                ROIJogForm.Show();
            }
            else
                ROIJogForm.Focus();
        }

        private TeachingItem ConvertDisplayTypeToTeachingItem(DisplayType displayType)
        {
            switch (displayType)
            {
                case DisplayType.Mark:
                    return TeachingItem.Mark;
                    
                case DisplayType.Align:
                    return TeachingItem.Align;

                case DisplayType.Akkon:
                    return TeachingItem.Akkon;

                case DisplayType.PreAlign:
                case DisplayType.Calibration:
                case DisplayType.Trigger:
                default:
                    return TeachingItem.Mark;
            }
        }

        private void ROIJogForm_SendEventHandler(string jogType, int jogScale, ROIType roiType = ROIType.ROI)
        {
            switch (_displayType)
            {
                case DisplayType.Mark:
                    MarkControl.ReceiveClickEvent(jogType, jogScale, roiType);
                    break;

                case DisplayType.Align:
                    AlignControl.ReceiveClickEvent(jogType, jogScale, roiType);
                    break;

                case DisplayType.Akkon:
                    AkkonControl.ReceiveClickEvent(jogType, jogScale, roiType);
                    break;

                case DisplayType.PreAlign:
                    break;
                case DisplayType.Calibration:
                    break;
                case DisplayType.Trigger:
                    break;
                default:
                    break;
            }
        }

        private void CloseRoiJogEventHandler()
        {
            ROIJogForm?.Close();
        }

        private void OptimizeUIByTeachingItem()
        {
            List<Button> buttonList = new List<Button>();

            if (UseAlignTeaching)
                buttonList.Add(btnAlign);
            else
                btnAlign.Visible = false;

            if (UseAkkonTeaching)
                buttonList.Add(btnAkkon);
            else
                btnAkkon.Visible = false;

            if (AppsConfig.Instance().EnableLafTrigger)
                buttonList.Add(btnAFOffset);
            else
                btnAFOffset.Visible = false;

            for (int i = 0; i < buttonList.Count(); i++)
            {
                tlpTeachingItems.Controls.Add(buttonList[i], 0, i + 1);
            }
        }

        private void InspectionTeachingForm_GrabDelayStartEventHandler(string cameraName)
        {
            LAFCtrl.SetTrackingOnOFF(true);
        }

        private void InspectionTeachingForm_GrabDoneEventHandler(string cameraName, bool isGrabDone)
        {
            int tabNo = Convert.ToInt32(_currentTabNo);
            UpdateDisplayImage(tabNo);

            _isWaitingUpdateUI = false;
        }

        private void MarkControl_MarkParamChanged(string component, string parameter, double oldValue, double newValue)
        {
            ParamTrackingLogger.AddChangeHistory($"{_displayType} {component}", parameter, oldValue, newValue);
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
            Display.SetThumbnailImage(cogImage);
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

            Logger.Write(LogType.GUI, "Clicked InpectionTeachingForm Mark Button");
        }

        private void btnAlign_Click(object sender, EventArgs e)
        {
            SelectPage(DisplayType.Align);
            Logger.Write(LogType.GUI, "Clicked InpectionTeachingForm Align Button");
        }

        private void btnAkkon_Click(object sender, EventArgs e)
        {
            SelectPage(DisplayType.Akkon);
            Logger.Write(LogType.GUI, "Clicked InpectionTeachingForm Akkon Button");
        }

        private void SelectPage(DisplayType type)
        {
            if (ModelManager.Instance().CurrentModel == null || UnitName.ToString() == "")
                return;

            ClearSelectedButton();
            CloseRoiJogEventHandler();

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

                case DisplayType.Trigger:
                    btnAFOffset.BackColor = _selectedColor;
                    TriggerSettingControl.SetParams(CurrentTab);
                    pnlTeach.Controls.Add(TriggerSettingControl);

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

                if (ParamTrackingLogger.IsEmpty == false)
                {
                    ParamTrackingLogger.AddLog($"{_displayType} Teaching Saved.");
                    ParamTrackingLogger.WriteLogToFile();
                }
            }

            Logger.Write(LogType.GUI, "Clicked InpectionTeachingForm Save Button");
        }

        private void SaveModelData(AppsInspModel model)
        {
            if (_displayType == DisplayType.Akkon)
                AkkonControl.SaveAkkonParam();
            model.SetUnitList(TeachingData.Instance().UnitList);

            string fileName = Path.Combine(ConfigSet.Instance().Path.Model, model.Name, InspModel.FileName);
            InspModelService?.Save(fileName, model);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ParamTrackingLogger.ClearChangedLog();
            this.Close();

            Logger.Write(LogType.GUI, "Clicked InpectionTeachingForm Cancle Button");
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            if (_isPrevTrackingOn == true)
                SetTrackingOnOff(false);

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ReadOnlyChecked = true;
            dlg.Filter = "BMP Files (*.bmp)|*.bmp; | "
                + "JPG Files (*.jpg, *.jpeg)|*.jpg; *.jpeg; |"
                + "모든 파일(*.*) | *.*;";
            dlg.ShowDialog();

            if (dlg.FileName != "")
            {
                string extension = Path.GetExtension(dlg.FileName);
                Mat image = null;

                if (extension == ".bmp")
                    image = new Mat(dlg.FileName, ImreadModes.Grayscale);
                else if (extension == ".jpg" || extension == ".jpeg")
                {
                    if (GetHalfFilePath(dlg.FileName, out string leftFilePath, out string rightFilePath))
                    {
                        Mat leftMatImage = new Mat(leftFilePath, ImreadModes.Grayscale);
                        Mat rightMatImage = new Mat(rightFilePath, ImreadModes.Grayscale);

                        Size mergeSize = new Size(leftMatImage.Width + rightMatImage.Width, leftMatImage.Height);
                        image = new Mat(mergeSize, DepthType.Cv8U, 1);
                        CvInvoke.HConcat(leftMatImage, rightMatImage, image);

                        leftMatImage.Dispose();
                        rightMatImage.Dispose();
                    }
                    else
                    {
                        MessageConfirmForm form = new MessageConfirmForm();
                        form.Message = "The file name format is incorrect.";
                        form.ShowDialog();
                        return;
                    }
                }

                if (image == null)
                    return;

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

                Match match = Regex.Match(dlg.FileName, $"[Tab_][0-{cbxTabList.Items.Count - 1}][_]");
                if (match.Success == true)
                    cbxTabList.SelectedIndex = Convert.ToInt32(match.Value.Replace("_", ""));

                cbxTabList_SelectedIndexChanged(null, EventArgs.Empty);
            }

            Logger.Write(LogType.GUI, "Clicked InpectionTeachingForm Load Image Button");
        }

        private bool GetHalfFilePath(string fileName, out string leftFilePath, out string rightFilePath)
        {
            leftFilePath = "";
            rightFilePath = "";

            string dir = Path.GetDirectoryName(fileName);
            string name = Path.GetFileName(fileName);

            if (name.Contains("Left"))
            {
                string rightName = name.Replace("Left", "Right");
                leftFilePath = fileName;
                rightFilePath = Path.Combine(dir, rightName);
            }
            else if (name.Contains("Right"))
            {
                rightFilePath = fileName;
                string leftName = name.Replace("Right", "Left");
                leftFilePath = Path.Combine(dir, leftName);
            }

            if (leftFilePath != "" && rightFilePath != "")
            {
                bool isLeftExist = File.Exists(leftFilePath);
                bool isRightExist = File.Exists(rightFilePath);

                if (isLeftExist && isRightExist)
                    return true;
            }

            return false;
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
            Logger.Write(LogType.GUI, "Clicked InpectionTeachingForm Motion Button");
        }

        private void btnGrabStart_Click(object sender, EventArgs e)
        {
            if (_isWaitingUpdateUI)
                return;

            _isWaitingUpdateUI = true;

            LineCamera.StopGrab();
            LAFCtrl.SetTrackingOnOFF(false);

            TeachingData.Instance().ClearTeachingImageBuffer();

            var cameraGap = AppsConfig.Instance().CameraGap_mm;
            LineCamera.ClearTabScanBuffer();

            var unit = TeachingData.Instance().GetUnit(UnitName.ToString());
            if (UseDelayStart)
            {
                LineCamera.InitGrabSettings(cameraGap);
            }
            else
            {
                LineCamera.InitGrabSettings();
            }
            ACSBufferManager.Instance().SetLafTriggerPosition(unit, LAFCtrl.Name, LineCamera.TabScanBufferList, UseAlignCamMark);

            InitalizeInspTab(LineCamera.TabScanBufferList);

            if (MotionManager.Instance().MoveAxisX(AxisHandlerName, UnitName, TeachingPosType.Stage1_Scan_Start) == false)
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "Axis X Moving Error.(Scan Start)";
                form.ShowDialog();
                return;
            }

            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            TeachingImagePath = Path.Combine(ConfigSet.Instance().Path.Model, inspModel.Name, "TeachingImage", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            var teachingInfo = inspModel.GetUnit(UnitName).GetTeachingInfo(TeachingPosType.Stage1_Scan_Start);

            ACSBufferManager.Instance().SetAutoMode(LAFCtrl.Name);

            LAFCtrl.SetTrackingOnOFF(true);
            Thread.Sleep(100);

            DeviceManager.Instance().LightCtrlHandler.TurnOn(inspModel.GetUnit(UnitName).LightParam);
            Thread.Sleep(100);

            LineCamera.StartGrab();
            Thread.Sleep(50);

            if (UseDelayStart)
                MotionManager.Instance().MoveAxisX(AxisHandlerName, UnitName, TeachingPosType.Stage1_Scan_End, cameraGap);
            else
                MotionManager.Instance().MoveAxisX(AxisHandlerName, UnitName, TeachingPosType.Stage1_Scan_End);

            ACSBufferManager.Instance().SetStopMode();

            LAFCtrl.SetTrackingOnOFF(false);
            Thread.Sleep(100);
            DeviceManager.Instance().LightCtrlHandler.TurnOff();
 
            Logger.Write(LogType.GUI, "Clicked InpectionTeachingForm Grab Start Button");
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
            Logger.Write(LogType.GUI, "Clicked InpectionTeachingForm Grab Stop Button");
        }

        private void InspectionTeachingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseMotionPopupEventHandler?.Invoke(UnitName);
            CloseRoiJogEventHandler();

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

            Logger.Write(LogType.GUI, "Clicked InpectionTeachingForm Tab Combo Button");
        }

        private void UpdateDisplayImage(int tabNo)
        {
            var teachingData = TeachingData.Instance();

            // 임시. Sleep 그랩 순서 변경해야함
            // 마지막 Tab teachingData Add(Merge) 하기전에 GrabDone이 들어와서 생긴 문제
            Thread.Sleep(100);

            if (teachingData.GetBufferImage(tabNo) is TeachingImageBuffer buffer)
            {
                if (buffer.TabImage == null)
                    return;

                Mat temp = buffer.TabImage.Clone() as Mat;
                ICogImage cogImage = teachingData.ConvertCogGrayImage(temp).CopyBase(CogImageCopyModeConstants.CopyPixels);

                Display.SetImage(cogImage);
                Display.SetThumbnailImage(cogImage);
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
            else if (_displayType == DisplayType.Trigger)
            {
                TriggerSettingControl.SetParams(CurrentTab);
            }
             
            GC.Collect();
        }

        private void lblPrev_Click(object sender, EventArgs e)
        {
            if (cbxTabList.SelectedIndex <= 0)
                return;

            cbxTabList.SelectedIndex -= 1;

            Logger.Write(LogType.GUI, "Clicked InpectionTeachingForm Prev Tab Button");
        }

        private void lblNext_Click(object sender, EventArgs e)
        {
            int nextIndex = cbxTabList.SelectedIndex + 1;

            if (cbxTabList.Items.Count > nextIndex)
                cbxTabList.SelectedIndex = nextIndex;

            Logger.Write(LogType.GUI, "Clicked InpectionTeachingForm Next Tab Button");
        }

        private void lblInspection_Click(object sender, EventArgs e)
        {
            if (_displayType == DisplayType.Mark)
                InspectionMark();
            else if (_displayType == DisplayType.Align)
                InspectionAlign();
            else if (_displayType == DisplayType.Akkon)
                InspectionAkkon();

            Logger.Write(LogType.GUI, "Clicked InpectionTeachingForm Inspection Button");
        }

        private void lblAddROI_Click(object sender, EventArgs e)
        {
            if (_displayType == DisplayType.Mark)
                MarkControl.AddROI();
            else if (_displayType == DisplayType.Align)
                AlignControl.AddROI();
            else if (_displayType == DisplayType.Akkon)
                AkkonControl.AddROI();

            Logger.Write(LogType.GUI, "Clicked InpectionTeachingForm Add ROI Button");
        }

        private void lblROIJog_Click(object sender, EventArgs e)
        {
            OpenRoiJogEventHandler();
            Logger.Write(LogType.GUI, "Clicked InpectionTeachingForm ROi Jog Button");
        }

        private void lblTracking_Click(object sender, EventArgs e)
        {
            if (_displayType == DisplayType.Akkon)
                AkkonControl.SetOrginImageView();

            if (_isPrevTrackingOn == false)
                SetTrackingOnOff(true);
            else
                SetTrackingOnOff(false);

            Logger.Write(LogType.GUI, "Clicked InpectionTeachingForm Tracking Button");
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

            if (UseAkkonTeaching == true)
            {
                _tabInspResult.MarkResult.FpcMark = new MarkResult();
                _tabInspResult.MarkResult.FpcMark.Judgement = Judgement.OK;

                _tabInspResult.MarkResult.PanelMark = _algorithmTool.RunPanelMark(cogImage, CurrentTab, false);
            }

            if (UseAlignTeaching == true)
            {
                _tabInspResult.MarkResult.FpcMark = _algorithmTool.RunFpcMark(cogImage, CurrentTab, UseAlignCamMark);
                _tabInspResult.MarkResult.PanelMark = _algorithmTool.RunPanelMark(cogImage, CurrentTab, UseAlignCamMark);
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

                if (UseAkkonTeaching == true)
                    coordinate.SetCoordinateAkkon(CurrentTab, markResult.PanelMark.FoundedMark);

                if (UseAlignTeaching == true)
                {
                    coordinate.SetCoordinateFpcAlign(CurrentTab, markResult.FpcMark.FoundedMark, UseAlignCamMark);
                    coordinate.SetCoordinatePanelAlign(CurrentTab, markResult.PanelMark.FoundedMark, UseAlignCamMark);
                }
            }
            else
            {
                lblTracking.BackColor = _nonSelectedColor;

                if (UseAkkonTeaching == true)
                    coordinate.SetReverseCoordinateAkkon(CurrentTab, markResult.PanelMark.FoundedMark);

                if (UseAlignTeaching == true)
                {
                    coordinate.SetReverseCoordinateFpcAlign(CurrentTab, markResult.FpcMark.FoundedMark, UseAlignCamMark);
                    coordinate.SetReverseCoordinatePanelAlign(CurrentTab, markResult.PanelMark.FoundedMark, UseAlignCamMark);
                }
            }

            if (UseAkkonTeaching == true)
                coordinate.ExcuteCoordinateAkkon(CurrentTab);

            if (UseAlignTeaching == true)
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

        private void lblStageCam_Click(object sender, EventArgs e)
        {
            if (UserManager.Instance().CurrentUser.Type == AuthorityType.Maker)
            {
                string dir = ConfigSet.Instance().Path.Model;
                Process.Start(dir);
            }
        }

        private void lblImageSave_Click(object sender, EventArgs e)
        {
            SaveScanImage();
            Logger.Write(LogType.GUI, "Clicked InpectionTeachingForm Image Save Button");
        }

        private void lblROICopy_Click(object sender, EventArgs e)
        {
            ROICopyForm form = new ROICopyForm();
            form.SetUnitName(UnitName);
            form.UseAlignCamMark = UseAlignCamMark;
            form.ShowDialog();

            Logger.Write(LogType.GUI, "Clicked InpectionTeachingForm ROI Copy Button");
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

            if (UseAkkonTeaching && UseAlignTeaching == false)
                algorithmTool.MainPanelMarkInspect(cogImage, CurrentTab, ref tabInspResult);
            else
                algorithmTool.MainMarkInspect(cogImage, CurrentTab, ref tabInspResult, UseAlignCamMark);

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

            CalcOriginDistanceFromMark(display, tabInspResult, true);
        }

        private void CalcOriginDistanceFromMark(CogDisplayControl display, TabInspResult tabInspResult, bool showEdges = false)
        {
            var cogImage = display.GetImage();

            MainAlgorithmTool mainAlgorithmTool = new MainAlgorithmTool();
            mainAlgorithmTool.CompensatePanelMarkOrigin(cogImage, tabInspResult.MarkResult, MarkDirection.Left, 0, out PointF leftEdge);
            mainAlgorithmTool.CompensatePanelMarkOrigin(cogImage, tabInspResult.MarkResult, MarkDirection.Right, 0, out PointF rightEdge);

            if (showEdges)
            {
                AddEdgeToDisplay(display, leftEdge);
                AddEdgeToDisplay(display, rightEdge);
            }
        }

        private void AddEdgeToDisplay(CogDisplayControl display, PointF anchor, double length = 1)
        {
            CogPolygon edge = new CogPolygon();
            edge.Color = CogColorConstants.Yellow;
            edge.AddVertex(anchor.X - length, anchor.Y, 0);
            edge.AddVertex(anchor.X, anchor.Y - length, 0);
            edge.AddVertex(anchor.X + length, anchor.Y, 0);
            edge.AddVertex(anchor.X, anchor.Y + length, 0);
            display.AddGraphics("Result", edge);
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
            tabInspResult.AlignResult = new TabAlignResult();
            var camera = LineCamera.Camera;
            double resolution_um = camera.PixelResolution_um / camera.LensScale;

            double judgementX = CurrentTab.AlignSpec.LeftSpecX_um / resolution_um;
            double judgementY = CurrentTab.AlignSpec.LeftSpecY_um / resolution_um;

            if (CurrentTab.AlignSpec.UseAutoTracking)
            {
                tabInspResult.AlignResult.LeftX = algorithmTool.RunMainLeftAutoAlignX(cogImage, CurrentTab, new PointF(0, 0), new PointF(0, 0), judgementX);
                tabInspResult.AlignResult.RightX = algorithmTool.RunMainRightAutoAlignX(cogImage, CurrentTab, new PointF(0, 0), new PointF(0, 0), judgementX);
            }
            else
            {
                tabInspResult.AlignResult.LeftX = algorithmTool.RunMainLeftAlignX(cogImage, CurrentTab, new PointF(0, 0), new PointF(0, 0), judgementX);
                tabInspResult.AlignResult.RightX = algorithmTool.RunMainRightAlignX(cogImage, CurrentTab, new PointF(0, 0), new PointF(0, 0), judgementX);
            }

            tabInspResult.AlignResult.LeftY = algorithmTool.RunMainLeftAlignY(cogImage, CurrentTab, new PointF(0, 0), new PointF(0, 0), judgementY);
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
                if (foundedFpcMark != null)
                {
                    if (foundedFpcMark.Left != null)
                    {
                        var leftFpc = foundedFpcMark.Left.MaxMatchPos.ResultGraphics;
                        if (foundedFpcMark.Left.Found)
                            shapeList.Add(leftFpc);
                    }

                    if (foundedFpcMark.Right != null)
                    {
                        var rightFpc = foundedFpcMark.Right.MaxMatchPos.ResultGraphics;
                        if (foundedFpcMark.Right.Found)
                            shapeList.Add(rightFpc);
                    }
                }
            }

            if (tabInspResult.MarkResult.PanelMark != null)
            {
                var foundedPanelMark = tabInspResult.MarkResult.PanelMark.FoundedMark;
                if (foundedPanelMark != null)
                {
                    if (foundedPanelMark.Left != null)
                    {
                        var leftPanel = foundedPanelMark.Left.MaxMatchPos.ResultGraphics;
                        if (foundedPanelMark.Left.Found)
                            shapeList.Add(leftPanel);
                    }
                    
                    if (foundedPanelMark.Right != null)
                    {
                        var rightPanel = foundedPanelMark.Right.MaxMatchPos.ResultGraphics;
                        if (foundedPanelMark.Right.Found)
                            shapeList.Add(rightPanel);
                    }
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

        private void btnAFOffset_Click(object sender, EventArgs e)
        {
            SelectPage(DisplayType.Trigger);
            Logger.Write(LogType.GUI, "Clicked InpectionTeachingForm AF Trigger Button");
        }
    }

    public enum DisplayType
    {
        Mark,
        Align,
        Akkon,

        PreAlign,
        Calibration,

        Trigger,
    }
}
