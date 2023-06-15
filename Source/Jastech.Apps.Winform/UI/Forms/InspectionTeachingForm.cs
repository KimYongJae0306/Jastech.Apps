using Cognex.VisionPro;
using Cognex.VisionPro.Caliper;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.Parameters;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Settings;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Framework.Algorithms.Akkon.Parameters;
using Jastech.Framework.Config;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using Jastech.Framework.Structure;
using Jastech.Framework.Structure.Service;
using Jastech.Framework.Users;
using Jastech.Framework.Winform.Controls;
using Jastech.Framework.Winform.Forms;
using Jastech.Framework.Winform.VisionPro.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Jastech.Framework.Winform.Forms
{
    public partial class InspectionTeachingForm : Form
    {
        #region 필드
        private Color _selectedColor;

        private Color _noneSelectedColor;

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

        private CogTeachingDisplayControl Display { get; set; } = new CogTeachingDisplayControl();

        private AlignControl AlignControl { get; set; } = new AlignControl() { Dock = DockStyle.Fill };

        private AkkonControl AkkonControl { get; set; } = new AkkonControl() { Dock = DockStyle.Fill };

        private MarkControl MarkControl { get; set; } = new MarkControl() { Dock = DockStyle.Fill };

        private AlgorithmTool Algorithm = new AlgorithmTool();

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
            AddControl();
            InitializeTabComboBox();

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

        public delegate void UpdateDisplayDele(ICogImage cogImage);
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
            TeachingUIManager.Instance().SetDisplay(Display.GetDisplay());

            // Teaching Item
            if (LineCamera.Camera.Name == "AlignCamera")
            {
                tlpTeachingItems.Controls.Add(btnAlign, 2, 0);
                btnAlign.Visible = true;
                btnAkkon.Visible = false;
            }
            else if (LineCamera.Camera.Name == "AkkonCamera")
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
                    AkkonControl.CalcResolution = LineCamera.Camera.PixelResolution_um / LineCamera.Camera.LensScale; ;

                    if (UserManager.Instance().CurrentUser.Type == AuthorityType.Maker)
                        AkkonControl.UserMaker = true;
                    else
                        AkkonControl.UserMaker = false;
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
                    control.BackColor = _noneSelectedColor;
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

                var cogImage = VisionProImageHelper.CovertImage(dataArray, image.Width, image.Height, format);


                TeachingUIManager.Instance().SetOrginCogImageBuffer(cogImage);
                TeachingUIManager.Instance().SetOriginMatImageBuffer(new Mat(dlg.FileName, ImreadModes.Grayscale));
                Display.SetImage(TeachingUIManager.Instance().GetOriginCogImageBuffer(false));
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

            LAFManager.Instance().AutoFocusOnOff("Akkon", true);

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

            Tab tabOriginData = new Tab();
            tabOriginData = CurrentTab.DeepCopy();

            ICogImage cogImage = display.GetImage();

            if (cogImage == null)
                return;

            // Set Coordinate Params
            Coordinate fpcCoordinate = new Coordinate();
            fpcCoordinate = SetFpcCoordinateParam(cogImage, tabOriginData);

            Coordinate panelCoordinate = new Coordinate();
            panelCoordinate = SetPanelCoordinateParam(cogImage, tabOriginData);

            CoordinateAlign(tabOriginData, fpcCoordinate, panelCoordinate);
            CoordinateAkkon(tabOriginData, panelCoordinate);

            return;
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

        
    }

    public enum DisplayType
    {
        Mark,
        Align,
        Akkon,
    }
}
