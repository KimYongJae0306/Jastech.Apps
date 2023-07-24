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
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Framework.Algorithms.Akkon;
using Jastech.Framework.Algorithms.Akkon.Parameters;
using Jastech.Framework.Algorithms.UI.Controls;
using Jastech.Framework.Config;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using Jastech.Framework.Structure;
using Jastech.Framework.Structure.Service;
using Jastech.Framework.Users;
using Jastech.Framework.Util;
using Jastech.Framework.Winform.VisionPro.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Jastech.Framework.Winform.Forms
{
    public partial class InspectionTeachingForm : Form
    {
        #region 필드
        private Color _selectedColor;

        private Color _nonSelectedColor;

        private bool _isLoading { get; set; } = false;

        private Control _selectControl { get; set; } = null;

        private DisplayType _displayType { get; set; } = DisplayType.Align;

        private string _currentTabNo { get; set; } = "";
        #endregion

        #region 속성
        public InspModelService InspModelService { get; set; } = null;

        public UnitName UnitName { get; set; } = UnitName.Unit0;

        public string TitleCameraName { get; set; } = "";

        public List<Tab> TeachingTabList { get; private set; } = null;

        public List<ATTInspTab> InspTabList { get; set; } = new List<ATTInspTab>();

        public Tab CurrentTab { get; set; } = null;

        public LineCamera LineCamera { get; set; }

        public string TeachingImagePath { get; set; }

        public double Resolution { get; set; }

        private CogTeachingDisplayControl Display { get; set; } = null;

        private AlignControl AlignControl { get; set; } = null;

        private AkkonControl AkkonControl { get; set; } = null;

        private MarkControl MarkControl { get; set; } = null;

        private AlgorithmTool Algorithm = new AlgorithmTool();

        public LAFCtrl LAFCtrl { get; set; } = null;

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
        public OpenMotionPopupDelegate OpenMotionPopupEventHandler;
        #endregion

        #region 델리게이트
        public delegate void OpenMotionPopupDelegate(UnitName unitName);

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

            TeachingTabList = TeachingData.Instance().GetUnit(UnitName.ToString()).GetTabList();
            InitializeTabComboBox();
            AddControl();
            InitailizeUI();

            _isLoading = false;

            lblStageCam.Text = $"STAGE : {UnitName} / CAM : {TitleCameraName}";

            LineCamera.GrabDoneEventHanlder += InspectionTeachingForm_GrabDoneEventHanlder;

            var image = TeachingUIManager.Instance().GetOriginCogImageBuffer(true);

            if (image != null)
                Display.SetImage(image);

            SelectPage(DisplayType.Mark);
        }

        private void InspectionTeachingForm_GrabDoneEventHanlder(string cameraName, bool isGrabDone)
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
            Console.WriteLine("InspectionTeachingForm 이미지 업데이트.");
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

        private void InitailizeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(34, 34, 34);
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
            TeachingUIManager.Instance().SetDisplay(Display.GetDisplay());

            MarkControl = new MarkControl();
            MarkControl.Dock = DockStyle.Fill;
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

            // Teaching Item
            if (LineCamera.Camera.Name.ToUpper().Contains("ALIGN"))
            {
                tlpTeachingItems.Controls.Add(btnAlign, 2, 0);
                btnAlign.Visible = true;
                btnAkkon.Visible = false;
            }
            else if (LineCamera.Camera.Name.ToUpper().Contains("AKKON"))
            {
                tlpTeachingItems.Controls.Add(btnAkkon, 2, 0);
                btnAlign.Visible = false;
                btnAkkon.Visible = true;
                MarkControl.TeachingItem = TeachingItem.Akkon;
            }
            else { }
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

            switch (type)
            {
                case DisplayType.Mark:
                    btnMark.BackColor = _selectedColor;
                    MarkControl.SetParams(CurrentTab);
                    pnlTeach.Controls.Add(MarkControl);
                    break;

                case DisplayType.Align:
                    btnAlign.BackColor = _selectedColor;
                    AlignControl.SetParams(CurrentTab);

                    var camera = LineCamera.Camera;
                    AlignControl.Resolution_um = camera.PixelResolution_um / camera.LensScale;

                    pnlTeach.Controls.Add(AlignControl);
                    break;

                case DisplayType.Akkon:
                    btnAkkon.BackColor = _selectedColor;
                    AkkonControl.SetParams(CurrentTab);
                    AkkonControl.Resolution = LineCamera.Camera.PixelResolution_um / LineCamera.Camera.LensScale;

                    if (UserManager.Instance().CurrentUser.Type == AuthorityType.Maker)
                        AkkonControl.SetUserMaker(true);
                    else
                        AkkonControl.SetUserMaker(false);
                    pnlTeach.Controls.Add(AkkonControl);
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

            SaveModelData(model);

            MessageConfirmForm form = new MessageConfirmForm();
            form.Message = "Save Model Completed.";
            form.ShowDialog();
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
            //SaveScanImage();
            //return;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ReadOnlyChecked = true;
            dlg.Filter = "BMP Files (*.bmp)|*.bmp";
            dlg.ShowDialog();

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
                Display.SetImage(TeachingUIManager.Instance().GetOriginCogImageBuffer(false));

                int tabNo = Convert.ToInt32(_currentTabNo);
                UpdateDisplayImage(tabNo);
            }
        }

        private void SaveScanImage()
        {
            for (int i = 0; i < 5; i++)
            {
                var teacingData = TeachingData.Instance().GetBufferImage(i);
                teacingData.TabImage.Save(string.Format(@"D:\tab_{0}.bmp", i));
            }

        }

        private void btnMotionPopup_Click(object sender, EventArgs e)
        {
            OpenMotionPopupEventHandler?.Invoke(UnitName);
        }

        private void btnGrabStart_Click(object sender, EventArgs e)
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            TeachingImagePath = Path.Combine(ConfigSet.Instance().Path.Model, inspModel.Name, "TeachingImage", DateTime.Now.ToString("yyyyMMdd_HHmmss"));

            LAFManager.Instance().TrackingOnOff(LAFCtrl.Name, true);

            TeachingData.Instance().ClearTeachingImageBuffer();
            LineCamera.InitGrabSettings();
            InitalizeInspTab(LineCamera.TabScanBufferList);

            LineCamera.StartGrab();
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
            Display.DisposeImage();
            MarkControl.DisposeImage();
            DisposeInspTabList();
            LineCamera.GrabDoneEventHanlder -= InspectionTeachingForm_GrabDoneEventHanlder;
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

                ICogImage cogImage = teachingData.ConvertCogGrayImage(buffer.TabImage);

                Display.SetImage(cogImage);
                TeachingUIManager.Instance().SetOrginCogImageBuffer(cogImage);
                TeachingUIManager.Instance().SetOriginMatImageBuffer(buffer.TabImage.Clone());

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
                MarkControl.Inspection();
            else if (_displayType == DisplayType.Align)
                AlignControl.Run();
            else if (_displayType == DisplayType.Akkon)
                AkkonControl.Run();
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

        private void lblAlign_Click(object sender, EventArgs e)
        {
            ExecuteCoordinate();
        }

        private void ExecuteCoordinate()
        {
            var display = TeachingUIManager.Instance().GetDisplay();
            display.ClearGraphic();

            Tab tabOriginData = new Tab();
            tabOriginData = CurrentTab.DeepCopy();

            ICogImage cogImage = display.GetImage();

            if (cogImage == null)
                return;

            // 티칭한 Left Fpc 좌표
            MarkParam teachedLeftFpcMarkParam = tabOriginData.GetFPCMark(MarkDirection.Left, MarkName.Main);
            CogTransform2DLinear teachedLeftFpc2D = teachedLeftFpcMarkParam.InspParam.GetOrigin();
            PointF teachedLeftFpc = new PointF(Convert.ToSingle(teachedLeftFpc2D.TranslationX), Convert.ToSingle(teachedLeftFpc2D.TranslationY));

            // 티칭한 Right FPC 좌표
            MarkParam teachedRightFpcMarkparam = tabOriginData.GetFPCMark(MarkDirection.Right, MarkName.Main);
            CogTransform2DLinear teachedRightFpc2D = teachedRightFpcMarkparam.InspParam.GetOrigin();
            PointF teachedRightFpc = new PointF(Convert.ToSingle(teachedRightFpc2D.TranslationX), Convert.ToSingle(teachedRightFpc2D.TranslationY));

            // 찾은 Left FPC 좌표
            VisionProPatternMatchingResult leftReferenceMarkResult = Algorithm.RunPatternMatch(cogImage, teachedLeftFpcMarkParam.InspParam);
            PointF searchedLeftFpcPoint = leftReferenceMarkResult.MaxMatchPos.FoundPos;

            // 찾은 Right FPC 좌표
            VisionProPatternMatchingResult rightReferenceFpcMarkResult = Algorithm.RunPatternMatch(cogImage, teachedRightFpcMarkparam.InspParam);
            PointF searchedRightFpcPoint = rightReferenceFpcMarkResult.MaxMatchPos.FoundPos;


            // 티칭한 Left Panel 좌표
            MarkParam teachedLeftPanelMarkParam = tabOriginData.GetPanelMark(MarkDirection.Left, MarkName.Main);
            CogTransform2DLinear teachedLeftPanel2D = teachedLeftPanelMarkParam.InspParam.GetOrigin();
            PointF teachedLeftPanel = new PointF(Convert.ToSingle(teachedLeftPanel2D.TranslationX), Convert.ToSingle(teachedLeftPanel2D.TranslationY));

            // 티칭한 Right Panel 좌표
            MarkParam teachedRightPanelMarkparam = tabOriginData.GetPanelMark(MarkDirection.Right, MarkName.Main);
            CogTransform2DLinear teachedRightPanel2D = teachedRightPanelMarkparam.InspParam.GetOrigin();
            PointF teachedRightPanel = new PointF(Convert.ToSingle(teachedRightPanel2D.TranslationX), Convert.ToSingle(teachedRightPanel2D.TranslationY));

            // 찾은 Left Panel 좌표
            VisionProPatternMatchingResult leftReferencePanelMarkResult = Algorithm.RunPatternMatch(cogImage, teachedLeftPanelMarkParam.InspParam);
            PointF searchedLeftPanelPoint = leftReferencePanelMarkResult.MaxMatchPos.FoundPos;

            // 찾은 Right Panel 좌표
            VisionProPatternMatchingResult rightReferencePanelMarkResult = Algorithm.RunPatternMatch(cogImage, teachedRightPanelMarkparam.InspParam);
            PointF searchedRightPanelPoint = rightReferencePanelMarkResult.MaxMatchPos.FoundPos;

            // Set Coordinage Params
            CoordinateTransform fpcCoordi = new CoordinateTransform();
            fpcCoordi.SetReferenceData(teachedLeftFpc, teachedRightFpc);
            fpcCoordi.SetTargetData(searchedLeftFpcPoint, searchedRightFpcPoint);
            fpcCoordi.ExecuteCoordinate();

            CoordinateTransform panelCoordi = new CoordinateTransform();
            panelCoordi.SetReferenceData(teachedLeftPanel, teachedRightPanel);
            panelCoordi.SetTargetData(searchedLeftPanelPoint, searchedRightPanelPoint);
            panelCoordi.ExecuteCoordinate();

            var fpcLeftXAlignParam = tabOriginData.GetAlignParam(ATTTabAlignName.LeftFPCX).DeepCopy();
            var fpcLeftYAlignParam = tabOriginData.GetAlignParam(ATTTabAlignName.LeftFPCY).DeepCopy();

            var fpcRightXAlignParam = tabOriginData.GetAlignParam(ATTTabAlignName.RightFPCX).DeepCopy();
            var fpcRightYAlignParam = tabOriginData.GetAlignParam(ATTTabAlignName.RightFPCY).DeepCopy();

            var panelLeftXAlignParam = tabOriginData.GetAlignParam(ATTTabAlignName.LeftPanelX).DeepCopy();
            var panelLeftYAlignParam = tabOriginData.GetAlignParam(ATTTabAlignName.LeftPanelY).DeepCopy();

            var panelRightXAlignParam = tabOriginData.GetAlignParam(ATTTabAlignName.RightPanelX).DeepCopy();
            var panelRightYAlignParam = tabOriginData.GetAlignParam(ATTTabAlignName.RightPanelY).DeepCopy();

            // Align inspection
            MainAlgorithmTool AlgorithmTool = new MainAlgorithmTool();
            CogGraphicInteractiveCollection alignCollect = new CogGraphicInteractiveCollection();

            foreach (var item in tabOriginData.AlignParamList)
            {
                if (item.Name.ToLower().Contains("fpc"))
                {
                    if (item.Name.ToLower().Contains("left"))
                    {
                        if (item.Name.ToLower().Contains("x"))
                        {
                            var region = fpcLeftXAlignParam.CaliperParams.GetRegion() as CogRectangleAffine;
                            PointF oldPoint = new PointF();
                            oldPoint.X = Convert.ToSingle(region.CenterX);
                            oldPoint.Y = Convert.ToSingle(region.CenterY);

                            var newPoint = fpcCoordi.GetCoordinate(oldPoint);
                            region.CenterX = newPoint.X;
                            region.CenterY = newPoint.Y;

                            fpcLeftXAlignParam.CaliperParams.SetRegion(region);

                            alignCollect.Add(region);
                        }
                        else if (item.Name.ToLower().Contains("y"))
                        {
                            var region = fpcLeftYAlignParam.CaliperParams.GetRegion() as CogRectangleAffine;
                            PointF oldPoint = new PointF();
                            oldPoint.X = Convert.ToSingle(region.CenterX);
                            oldPoint.Y = Convert.ToSingle(region.CenterY);

                            var newPoint = fpcCoordi.GetCoordinate(oldPoint);
                            region.CenterX = newPoint.X;
                            region.CenterY = newPoint.Y;

                            fpcLeftYAlignParam.CaliperParams.SetRegion(region);

                            alignCollect.Add(region);
                        }
                        else { }
                    }
                    else if (item.Name.ToLower().Contains("right"))
                    {
                        if (item.Name.ToLower().Contains("x"))
                        {
                            var region = fpcRightXAlignParam.CaliperParams.GetRegion() as CogRectangleAffine;
                            PointF oldPoint = new PointF();
                            oldPoint.X = Convert.ToSingle(region.CenterX);
                            oldPoint.Y = Convert.ToSingle(region.CenterY);

                            var newPoint = fpcCoordi.GetCoordinate(oldPoint);
                            region.CenterX = newPoint.X;
                            region.CenterY = newPoint.Y;

                            fpcRightXAlignParam.CaliperParams.SetRegion(region);

                            alignCollect.Add(region);
                        }
                        else if (item.Name.ToLower().Contains("y"))
                        {
                            var region = fpcRightYAlignParam.CaliperParams.GetRegion() as CogRectangleAffine;
                            PointF oldPoint = new PointF();
                            oldPoint.X = Convert.ToSingle(region.CenterX);
                            oldPoint.Y = Convert.ToSingle(region.CenterY);

                            var newPoint = fpcCoordi.GetCoordinate(oldPoint);
                            region.CenterX = newPoint.X;
                            region.CenterY = newPoint.Y;

                            fpcRightYAlignParam.CaliperParams.SetRegion(region);

                            alignCollect.Add(region);
                        }
                        else { }
                    }
                    else { }
                }
                else if (item.Name.ToLower().Contains("panel"))
                {
                    if (item.Name.ToLower().Contains("left"))
                    {
                        if (item.Name.ToLower().Contains("x"))
                        {
                            var region = panelLeftXAlignParam.CaliperParams.GetRegion() as CogRectangleAffine;
                            PointF oldPoint = new PointF();
                            oldPoint.X = Convert.ToSingle(region.CenterX);
                            oldPoint.Y = Convert.ToSingle(region.CenterY);

                            var newPoint = panelCoordi.GetCoordinate(oldPoint);
                            region.CenterX = newPoint.X;
                            region.CenterY = newPoint.Y;

                            panelLeftXAlignParam.CaliperParams.SetRegion(region);

                            alignCollect.Add(region);
                        }
                        else if (item.Name.ToLower().Contains("y"))
                        {
                            var region = panelLeftYAlignParam.CaliperParams.GetRegion() as CogRectangleAffine;
                            PointF oldPoint = new PointF();
                            oldPoint.X = Convert.ToSingle(region.CenterX);
                            oldPoint.Y = Convert.ToSingle(region.CenterY);

                            var newPoint = panelCoordi.GetCoordinate(oldPoint);
                            region.CenterX = newPoint.X;
                            region.CenterY = newPoint.Y;

                            panelLeftYAlignParam.CaliperParams.SetRegion(region);

                            alignCollect.Add(region);
                        }
                        else { }
                    }
                    else if (item.Name.ToLower().Contains("right"))
                    {
                        if (item.Name.ToLower().Contains("x"))
                        {
                            var region = panelRightXAlignParam.CaliperParams.GetRegion() as CogRectangleAffine;
                            PointF oldPoint = new PointF();
                            oldPoint.X = Convert.ToSingle(region.CenterX);
                            oldPoint.Y = Convert.ToSingle(region.CenterY);

                            var newPoint = panelCoordi.GetCoordinate(oldPoint);
                            region.CenterX = newPoint.X;
                            region.CenterY = newPoint.Y;

                            panelRightXAlignParam.CaliperParams.SetRegion(region);

                            alignCollect.Add(region);
                        }
                        else if (item.Name.ToLower().Contains("y"))
                        {
                            var region = panelRightYAlignParam.CaliperParams.GetRegion() as CogRectangleAffine;
                            PointF oldPoint = new PointF();
                            oldPoint.X = Convert.ToSingle(region.CenterX);
                            oldPoint.Y = Convert.ToSingle(region.CenterY);

                            var newPoint = panelCoordi.GetCoordinate(oldPoint);
                            region.CenterX = newPoint.X;
                            region.CenterY = newPoint.Y;

                            panelRightYAlignParam.CaliperParams.SetRegion(region);

                            alignCollect.Add(region);
                        }
                        else { }
                    }
                    else { }
                }
            }

            

            display.SetInteractiveGraphics("tool", alignCollect);

            //var leftFpcX = AlgorithmTool.RunAlignX(cogImage, fpcLeftXAlignParam.CaliperParams, fpcLeftXAlignParam.LeadCount);
            //display.UpdateResult(leftFpcX);

            //var leftFpcY = AlgorithmTool.RunAlignY(cogImage, fpcLeftYAlignParam.CaliperParams);
            //display.UpdateResult(leftFpcY);

            //var rightFpcX = AlgorithmTool.RunAlignX(cogImage, fpcRightXAlignParam.CaliperParams, fpcRightXAlignParam.LeadCount);
            //display.UpdateResult(rightFpcX);

            //var rightFpcY = AlgorithmTool.RunAlignY(cogImage, fpcRightYAlignParam.CaliperParams);
            //display.UpdateResult(rightFpcY);

            //var leftPanelX = AlgorithmTool.RunAlignX(cogImage, panelLeftXAlignParam.CaliperParams, panelLeftXAlignParam.LeadCount);
            //display.UpdateResult(leftPanelX);

            //var leftPanelY = AlgorithmTool.RunAlignY(cogImage, panelLeftYAlignParam.CaliperParams);
            //display.UpdateResult(leftPanelY);

            //var rightPanelX = AlgorithmTool.RunAlignX(cogImage, panelRightXAlignParam.CaliperParams, panelRightXAlignParam.LeadCount);
            //display.UpdateResult(rightPanelX);

            //var rightPanelY = AlgorithmTool.RunAlignY(cogImage, panelRightYAlignParam.CaliperParams);
            //display.UpdateResult(rightPanelY);

            int tlqkf1 = 0;

            // Akkon inspection
            AkkonAlgorithm AkkonAlgorithm = new AkkonAlgorithm();
            int cnt = 0;
            List<AkkonGroup> newAkkonGroup = new List<AkkonGroup>();

            foreach (var item in tabOriginData.AkkonParam.GroupList)
            {
                AkkonGroup group = new AkkonGroup();
                group.Index = cnt;

                var roiList = item.AkkonROIList.ToList();

                List<AkkonROI> akkonList = new List<AkkonROI>();

                foreach (var rois in roiList)
                {
                    PointF leftTop = rois.GetLeftTopPoint();
                    PointF rightTop = rois.GetRightTopPoint();
                    PointF leftBottom = rois.GetLeftBottomPoint();
                    PointF rightBottom = rois.GetRightBottomPoint();

                    var newLeftTop = panelCoordi.GetCoordinate(leftTop);
                    var newRightTop = panelCoordi.GetCoordinate(rightTop);
                    var newLeftBottom = panelCoordi.GetCoordinate(leftBottom);
                    var newRightBottom = panelCoordi.GetCoordinate(rightBottom);

                    AkkonROI akkonRoi = new AkkonROI();

                    akkonRoi.SetLeftTopPoint(newLeftTop);
                    akkonRoi.SetRightTopPoint(newRightTop);
                    akkonRoi.SetLeftBottomPoint(newLeftBottom);
                    akkonRoi.SetRightBottomPoint(newRightBottom);

                    akkonList.Add(akkonRoi);
                }

                group.ReNewalROIList(akkonList);

                newAkkonGroup.Add(group);
            }

            tabOriginData.AkkonParam.RenewalGroup(newAkkonGroup);

            int tlqkf2 = 0;
            CogGraphicInteractiveCollection collect = new CogGraphicInteractiveCollection();
            var tt = tabOriginData.AkkonParam.GetAkkonROIList();
            foreach (var roi in tt)
            {
                CogRectangleAffine rect = ConvertAkkonRoiToCogRectAffine(roi);
                collect.Add(rect);
            }
            display.SetInteractiveGraphics("tool", collect);

            int tlqkf3 = 0;
            //AkkonAlgoritmParam akkonAlgorithmParam = tabOriginData.AkkonParam.AkkonAlgoritmParam;

            //Mat matImage = TeachingUIManager.Instance().GetOriginMatImageBuffer(false);

            //var result = AkkonAlgorithm.Run(matImage, tabOriginData.AkkonParam.GetAkkonROIList(), akkonAlgorithmParam, 1.0F);

            //int no = 0;
            //foreach (var lead in result)
            //{
            //    string noString = no.ToString();
            //    int blobCount = lead.BlobList.Count();
            //    double average = 0.0;

            //    for (int index = 0; index < blobCount; index++)
            //        average += lead.BlobList[index].Avg;

            //    average /= blobCount;

            //    string[] row = { no.ToString(), lead.BlobList.Count().ToString(), Math.Round(average, 2).ToString() };
            //    no++;
            //}

            //Mat resultMat = GetDebugResultImage(matImage, result, akkonAlgorithmParam);

            //var resultCogImage = ConvertCogColorImage(resultMat);
            //TeachingUIManager.Instance().SetResultCogImage(resultCogImage);

            //int gg = 0;









            // Set Coordinate Params
            //Coordinate fpcCoordinate = new Coordinate();
            //fpcCoordinate = SetFpcCoordinateParam(cogImage, tabOriginData);

            //Coordinate panelCoordinate = new Coordinate();
            //panelCoordinate = SetPanelCoordinateParam(cogImage, tabOriginData);

            //CoordinateAlign(tabOriginData, fpcCoordinate, panelCoordinate);
            //CoordinateAkkon(tabOriginData, panelCoordinate);
        }

        //private CogRectangleAffine ConvertAkkonRoiToCogRectAffine(AkkonROI akkonRoi)
        //{
        //    PointF leftTop = new PointF(Convert.ToSingle(akkonRoi.LeftTopX), Convert.ToSingle(akkonRoi.LeftTopY));
        //    PointF rightTop = new PointF(Convert.ToSingle(akkonRoi.RightTopX), Convert.ToSingle(akkonRoi.RightTopY));
        //    PointF leftBottom = new PointF(Convert.ToSingle(akkonRoi.LeftBottomX), Convert.ToSingle(akkonRoi.LeftBottomY));

        //    return VisionProShapeHelper.ConvertToCogRectAffine(leftTop, rightTop, leftBottom);
        //}

        public ICogImage ConvertCogColorImage(Mat mat)
        {
            Mat matR = Imaging.Helper.MatHelper.ColorChannelSprate(mat, Imaging.Helper.MatHelper.ColorChannel.R);
            Mat matG = Imaging.Helper.MatHelper.ColorChannelSprate(mat, Imaging.Helper.MatHelper.ColorChannel.G);
            Mat matB = Imaging.Helper.MatHelper.ColorChannelSprate(mat, Imaging.Helper.MatHelper.ColorChannel.B);

            byte[] dataR = new byte[matR.Width * matR.Height];
            Marshal.Copy(matR.DataPointer, dataR, 0, matR.Width * matR.Height);

            byte[] dataG = new byte[matG.Width * matG.Height];
            Marshal.Copy(matG.DataPointer, dataG, 0, matG.Width * matG.Height);

            byte[] dataB = new byte[matB.Width * matB.Height];
            Marshal.Copy(matB.DataPointer, dataB, 0, matB.Width * matB.Height);

            var cogImage = VisionProImageHelper.CovertImage(dataR, dataG, dataB, matB.Width, matB.Height);

            matR.Dispose();
            matG.Dispose();
            matB.Dispose();

            return cogImage;
        }

        public Mat GetDebugResultImage(Mat mat, List<AkkonLeadResult> leadResultList, AkkonAlgoritmParam akkonParameters)
        {
            if (mat == null)
                return null;

            Mat resizeMat = new Mat();
            Size newSize = new Size((int)(mat.Width * akkonParameters.ImageFilterParam.ResizeRatio), (int)(mat.Height * akkonParameters.ImageFilterParam.ResizeRatio));
            CvInvoke.Resize(mat, resizeMat, newSize);
            Mat colorMat = new Mat();
            CvInvoke.CvtColor(resizeMat, colorMat, ColorConversion.Gray2Bgr);
            resizeMat.Dispose();

            float calcResolution = /*Resolution*/1.0F / CurrentTab.AkkonParam.AkkonAlgoritmParam.ImageFilterParam.ResizeRatio;
            MCvScalar redColor = new MCvScalar(50, 50, 230, 255);
            MCvScalar greenColor = new MCvScalar(50, 230, 50, 255);

            foreach (var result in leadResultList)
            {
                var lead = result.Roi;
                var startPoint = new Point((int)result.Offset.ToWorldX, (int)result.Offset.ToWorldY);

                Point leftTop = new Point((int)lead.LeftTopX + startPoint.X, (int)lead.LeftTopY + startPoint.Y);
                Point leftBottom = new Point((int)lead.LeftBottomX + startPoint.X, (int)lead.LeftBottomY + startPoint.Y);
                Point rightTop = new Point((int)lead.RightTopX + startPoint.X, (int)lead.RightTopY + startPoint.Y);
                Point rightBottom = new Point((int)lead.RightBottomX + startPoint.X, (int)lead.RightBottomY + startPoint.Y);


                if (akkonParameters.DrawOption.ContainLeadROI)
                {
                    CvInvoke.Line(colorMat, leftTop, leftBottom, greenColor, 1);
                    CvInvoke.Line(colorMat, leftTop, rightTop, greenColor, 1);
                    CvInvoke.Line(colorMat, rightTop, rightBottom, greenColor, 1);
                    CvInvoke.Line(colorMat, rightBottom, leftBottom, greenColor, 1);
                }

                foreach (var blob in result.BlobList)
                {
                    int offsetX = (int)(result.Offset.ToWorldX + result.Offset.X);
                    int offsetY = (int)(result.Offset.ToWorldY + result.Offset.Y);

                    Rectangle rectRect = new Rectangle();
                    rectRect.X = blob.BoundingRect.X + offsetX;
                    rectRect.Y = blob.BoundingRect.Y + offsetY;
                    rectRect.Width = blob.BoundingRect.Width;
                    rectRect.Height = blob.BoundingRect.Height;

                    Point center = new Point(rectRect.X + (rectRect.Width / 2), rectRect.Y + (rectRect.Height / 2));
                    int radius = rectRect.Width > rectRect.Height ? rectRect.Width : rectRect.Height;

                    int size = blob.BoundingRect.Width * blob.BoundingRect.Height;
                    if (blob.IsAkkonShape)
                    {
                        CvInvoke.Circle(colorMat, center, radius / 2, greenColor, 1);
                    }
                    else
                    {
                        if (akkonParameters.DrawOption.ContainNG)
                        {
                            CvInvoke.Circle(colorMat, center, radius / 2, redColor, 1);
                        }

                    }

                    if (akkonParameters.DrawOption.ContainSize)
                    {
                        int temp = (int)(radius / 2.0);
                        Point pt = new Point(center.X + temp, center.Y - temp);
                        double akkonSize = (blob.BoundingRect.Width + blob.BoundingRect.Height) / 2.0;
                        double blobSize = akkonSize * calcResolution;

                        if (blob.IsAkkonShape)
                            CvInvoke.PutText(colorMat, blobSize.ToString("F1"), pt, FontFace.HersheySimplex, 0.3, greenColor);
                        else
                            CvInvoke.PutText(colorMat, blobSize.ToString("F1"), pt, FontFace.HersheySimplex, 0.3, redColor);
                    }
                    else if (akkonParameters.DrawOption.ContainArea)
                    {
                        int temp = (int)(radius / 2.0);
                        Point pt = new Point(center.X + temp, center.Y - temp);
                        double blobArea = blob.Area * calcResolution;

                        if (blob.IsAkkonShape)
                            CvInvoke.PutText(colorMat, blobArea.ToString("F1"), pt, FontFace.HersheySimplex, 0.3, greenColor);
                        else
                            CvInvoke.PutText(colorMat, blobArea.ToString("F1"), pt, FontFace.HersheySimplex, 0.3, redColor);
                    }
                    else if (akkonParameters.DrawOption.ContainStrength)
                    {
                        int temp = (int)(radius / 2.0);
                        Point pt = new Point(center.X + temp, center.Y - temp);
                        string strength = blob.Strength.ToString("F1");

                        if (blob.IsAkkonShape)
                            CvInvoke.PutText(colorMat, strength, pt, FontFace.HersheySimplex, 0.3, greenColor);
                        else
                            CvInvoke.PutText(colorMat, strength, pt, FontFace.HersheySimplex, 0.3, redColor);
                    }
                }

                if (akkonParameters.DrawOption.ContainLeadCount)
                {
                    string leadIndexString = result.Roi.Index.ToString();
                    string akkonCountString = string.Format("[{0}]", result.AkkonCount);

                    Point centerPt = new Point((int)((leftBottom.X + rightBottom.X) / 2.0), leftBottom.Y);

                    int baseLine = 0;
                    Size textSize = CvInvoke.GetTextSize(leadIndexString, FontFace.HersheyComplex, 0.3, 1, ref baseLine);
                    int textX = centerPt.X - (textSize.Width / 2);
                    int textY = centerPt.Y + (baseLine / 2);
                    CvInvoke.PutText(colorMat, leadIndexString, new Point(textX, textY + 30), FontFace.HersheyComplex, 0.25, new MCvScalar(50, 230, 50, 255));

                    textSize = CvInvoke.GetTextSize(akkonCountString, FontFace.HersheyComplex, 0.3, 1, ref baseLine);
                    textX = centerPt.X - (textSize.Width / 2);
                    textY = centerPt.Y + (baseLine / 2);
                    CvInvoke.PutText(colorMat, akkonCountString, new Point(textX, textY + 60), FontFace.HersheyComplex, 0.25, new MCvScalar(50, 230, 50, 255));
                }
            }
            return colorMat;
        }

        private Coordinate SetFpcCoordinateParam(ICogImage cogImage, Tab tab)
        {
            Coordinate fpcCoordinate = new Coordinate();

            // 티칭한 Left Fpc 좌표
            MarkParam teachedLeftFpcMarkParam = tab.GetFPCMark(MarkDirection.Left, MarkName.Main);
            CogTransform2DLinear teachedLeftFpc2D = teachedLeftFpcMarkParam.InspParam.GetOrigin();
            PointF teachedLeftFpc = new PointF(Convert.ToSingle(teachedLeftFpc2D.TranslationX), Convert.ToSingle(teachedLeftFpc2D.TranslationY));

            // 티칭한 Right FPC 좌표
            MarkParam teachedRightFpcMarkparam = tab.GetFPCMark(MarkDirection.Right, MarkName.Main);
            CogTransform2DLinear teachedRightFpc2D = teachedRightFpcMarkparam.InspParam.GetOrigin();
            PointF teachedRightFpc = new PointF(Convert.ToSingle(teachedRightFpc2D.TranslationX), Convert.ToSingle(teachedRightFpc2D.TranslationY));

            // 찾은 Left FPC 좌표
            VisionProPatternMatchingResult leftReferenceMarkResult = Algorithm.RunPatternMatch(cogImage, teachedLeftFpcMarkParam.InspParam);
            PointF searchedLeftFpcPoint = leftReferenceMarkResult.MaxMatchPos.FoundPos;

            // 찾은 Right FPC 좌표
            VisionProPatternMatchingResult rightReferenceFpcMarkResult = Algorithm.RunPatternMatch(cogImage, teachedRightFpcMarkparam.InspParam);
            PointF searchedRightFpcPoint = rightReferenceFpcMarkResult.MaxMatchPos.FoundPos;

            fpcCoordinate.SetCoordinateParam(teachedLeftFpc, teachedRightFpc, searchedLeftFpcPoint, searchedRightFpcPoint);

            return fpcCoordinate;
        }

        private Coordinate SetPanelCoordinateParam(ICogImage cogImage, Tab tab)
        {
            Coordinate panelCoordinate = new Coordinate();

            // 티칭한 Left Panel 좌표
            MarkParam teachedLeftPanelMarkParam = tab.GetPanelMark(MarkDirection.Left, MarkName.Main);
            CogTransform2DLinear teachedLeftPanel2D = teachedLeftPanelMarkParam.InspParam.GetOrigin();
            PointF teachedLeftPanel = new PointF(Convert.ToSingle(teachedLeftPanel2D.TranslationX), Convert.ToSingle(teachedLeftPanel2D.TranslationY));

            // 티칭한 Right Panel 좌표
            MarkParam teachedRightPanelMarkparam = tab.GetPanelMark(MarkDirection.Right, MarkName.Main);
            CogTransform2DLinear teachedRightPanel2D = teachedRightPanelMarkparam.InspParam.GetOrigin();
            PointF teachedRightPanel = new PointF(Convert.ToSingle(teachedRightPanel2D.TranslationX), Convert.ToSingle(teachedRightPanel2D.TranslationY));

            // 찾은 Left Panel 좌표
            VisionProPatternMatchingResult leftReferencePanelMarkResult = Algorithm.RunPatternMatch(cogImage, teachedLeftPanelMarkParam.InspParam);
            PointF searchedLeftPanelPoint = leftReferencePanelMarkResult.MaxMatchPos.FoundPos;

            // 찾은 Right Panel 좌표
            VisionProPatternMatchingResult rightReferencePanelMarkResult = Algorithm.RunPatternMatch(cogImage, teachedRightPanelMarkparam.InspParam);
            PointF searchedRightPanelPoint = rightReferencePanelMarkResult.MaxMatchPos.FoundPos;

            panelCoordinate.SetCoordinateParam(teachedLeftPanel, teachedRightPanel, searchedLeftPanelPoint, searchedRightPanelPoint);

            return panelCoordinate;
        }

        private void CoordinateAlign(Tab tab, Coordinate fpcCoordinate, Coordinate panelCoordinate)
        {
            MainAlgorithmTool algorithmTool = new MainAlgorithmTool();

            foreach (var item in tab.AlignParamList)
            {
                if (item.Name.ToLower().Contains("fpc"))
                {
                    //var param = tab.GetAlignParam(alignName).DeepCopy();
                    var calcFpcRegion = algorithmTool.CoordinateRectangle(item.CaliperParams.GetRegion() as CogRectangleAffine, fpcCoordinate);
                    item.CaliperParams.SetRegion(calcFpcRegion);
                }
                else if (item.Name.ToLower().Contains("panel"))
                {
                    var calcPanelRegion = algorithmTool.CoordinateRectangle(item.CaliperParams.GetRegion() as CogRectangleAffine, panelCoordinate);
                    item.CaliperParams.SetRegion(calcPanelRegion);
                }
            }
        }

        private void CoordinateAkkon(Tab tab, Coordinate panelCoordinate)
        {
            MainAlgorithmTool algorithmTool = new MainAlgorithmTool();

            List<AkkonGroup> newAkkonGroup = new List<AkkonGroup>();

            foreach (var group in tab.AkkonParam.GroupList)
            {
                List<AkkonROI> newRoiList = new List<AkkonROI>();

                foreach (var lead in group.AkkonROIList)
                {
                    var affineRect = ConvertAkkonRoiToCogRectAffine(lead);
                    var calcPanelRegion = algorithmTool.CoordinateRectangle(affineRect, panelCoordinate);
                    var roi = ConvertCogRectAffineToAkkonRoi(calcPanelRegion);

                    newRoiList.Add(roi);
                }

                newAkkonGroup.Add(group);
            }

            tab.AkkonParam.GroupList.Clear();
            tab.AkkonParam.GroupList.AddRange(newAkkonGroup);
        }

        private CogRectangleAffine ConvertAkkonRoiToCogRectAffine(AkkonROI akkonRoi)
        {
            CogRectangleAffine cogRectAffine = new CogRectangleAffine();

            cogRectAffine.SetOriginCornerXCornerY(akkonRoi.LeftTopX, akkonRoi.LeftTopY,
                                                    akkonRoi.RightTopX, akkonRoi.RightTopY, akkonRoi.LeftBottomX, akkonRoi.LeftBottomY);

            return cogRectAffine;
        }

        private AkkonROI ConvertCogRectAffineToAkkonRoi(CogRectangleAffine cogRectAffine)
        {
            AkkonROI akkonRoi = new AkkonROI();

            akkonRoi.LeftTopX = cogRectAffine.CornerOriginX;
            akkonRoi.LeftTopY = cogRectAffine.CornerOriginY;

            akkonRoi.RightTopX = cogRectAffine.CornerXX;
            akkonRoi.RightTopY = cogRectAffine.CornerXY;

            akkonRoi.LeftBottomX = cogRectAffine.CornerYX;
            akkonRoi.LeftBottomY = cogRectAffine.CornerYY;

            akkonRoi.RightBottomX = cogRectAffine.CornerOppositeX;
            akkonRoi.RightBottomY = cogRectAffine.CornerOppositeY;

            return akkonRoi;
        }

        #endregion

        private void btnCancel_BackColorChanged(object sender, EventArgs e)
        {

        }
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
