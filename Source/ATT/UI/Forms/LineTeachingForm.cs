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
using System.Runtime.InteropServices;
using Jastech.Framework.Imaging;
using ATT.UI.Controls;

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

        private LinescanControl LinescanControl { get; set; } = new LinescanControl() { Dock = DockStyle.Fill };

        private AlignControl AlignControl { get; set; } = new AlignControl() { Dock = DockStyle.Fill };

        private AkkonControl AkkonControl { get; set; } = new AkkonControl() { Dock = DockStyle.Fill };

        private AkkonAutoControl AkkonAutoControl { get; set; } = new AkkonAutoControl() { Dock = DockStyle.Fill };

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
        private System.Threading.Timer _formTimer = null;
        private void StartTimer()
        {
            _formTimer = new System.Threading.Timer(UpdateStatus, null, 1000, 1000);
        }

        private delegate void UpdateStatusDelegate(object obj);
        private void UpdateStatus(object obj)
        {
            if (this.InvokeRequired)
            {
                UpdateStatusDelegate callback = UpdateStatus;
                BeginInvoke(callback, obj);
                return;
            }

            if (LinescanControl != null && isSetParamLinescanPage)
                LinescanControl.UpdateUI();
        }

        private void LineTeachingForm_Load(object sender, EventArgs e)
        {
            SystemManager.Instance().UpdateTeachingData();
            AddControl();
            SelectAlign();
            StartTimer();

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
            if(pnlTeach.Controls.Count >0)
            {
                if (pnlTeach.Controls[0] as MarkControl != null)
                    MarkControl.DrawROI();
                else if (pnlTeach.Controls[0] as AlignControl != null)
                    AlignControl.DrawROI();
                else if (pnlTeach.Controls[0] as AkkonControl != null)
                    AkkonControl.DrawROI();
                else if (pnlDisplay.Controls[0] as AkkonAutoControl != null)
                    AkkonAutoControl.DrawROI();
            }
        }

        private void btnLinescan_Click(object sender, EventArgs e)
        {
            SelectLinescan();
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
            btnAutoAkkon.ForeColor = Color.White;

            pnlTeach.Controls.Clear();
        }

        bool isSetParamLinescanPage = false;
        private void SelectLinescan()
        {
            if (ModelManager.Instance().CurrentModel == null || UnitName == "")
                return;

            ClearSelectedButton();

            var tabList = SystemManager.Instance().GetTeachingData().GetTabList(UnitName);
            //LinescanControl.SetParams(tabList);

            var posData = SystemManager.Instance().GetTeachingData().GetUnit(UnitName);
            pnlTeach.Controls.Add(LinescanControl);
            //LinescanControl.SetParams(tabList);
            btnLinescan.ForeColor = Color.Blue;

            isSetParamLinescanPage = true;
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

        private void btnAutoAkkon_Click(object sender, EventArgs e)
        {
            if (ModelManager.Instance().CurrentModel == null || UnitName == "")
                return;

            ClearSelectedButton();

            var tabList = SystemManager.Instance().GetTeachingData().GetTabList(UnitName);
            AkkonAutoControl.SetParams(tabList);

            btnAutoAkkon.ForeColor = Color.Blue;
            pnlTeach.Controls.Add(AkkonAutoControl);
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
            model.SetUnitList(SystemManager.Instance().GetTeachingData().UnitList);

            string fileName = System.IO.Path.Combine(AppConfig.Instance().Path.Model, model.Name, InspModel.FileName);
            SystemManager.Instance().SaveModel(fileName, model);
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
            //motionPopupForm.SetAxisHandler(AppsMotionManager.Instance().GetAxisHandler(AxisHandlerName.Unit0));
            //motionPopupForm.SetTeachingPosition(SystemManager.Instance().GetTeachingData().GetUnit(UnitName).TeachingPositions);
            //motionPopupForm.SetLAFCtrl(AppsLAFManager.Instance().GetLAFCtrl(LAFName.Akkon));
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

            int size = image.Width * image.Height * image.Channels();
            byte[] dataArray = new byte[size];
            Marshal.Copy(image.Data, dataArray, 0, size);

            ColorFormat format = image.Channels() == 1 ? ColorFormat.Gray : ColorFormat.RGB24;

            var cogImage = CogImageHelper.CovertImage(dataArray, image.Width, image.Height, format);
            Display.SetImage(cogImage);
            // Display Update
        }
        #endregion

        
    }
}
