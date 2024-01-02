using Jastech.Apps.Winform.UI.Controls;
using Jastech.Framework.Winform.VisionPro.Controls;
using Jastech.Framework.Winform;
using System;
using System.Drawing;
using System.Windows.Forms;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Winform.Helper;
using Cognex.VisionPro;
using Jastech.Framework.Winform.VisionPro.Helper;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Cognex.VisionPro.PMAlign;

namespace Jastech.Apps.Winform.UI.Forms
{
    public partial class ManualMatchingForm : Form
    {
        #region 필드
        private int _pitch = 0;
        #endregion

        #region 속성
        private CogDisplayControl Display { get; set; } = null;

        private CogRecordDisplay PatternDisplay { get; set; } = null;

        private LightControl LightControl { get; set; } = null;

        //private VisionProPointMarkerParam PointMarkerParam { get; set; } = null;
        private VisionProPointMarkerParam PointMarkerParam { get; set; } = new VisionProPointMarkerParam();

        public AreaCamera AreaCamera { get; set; } = null;

        public LAFCtrl LAFCtrl { get; set; } = null;

        public Unit CurrentUnit { get; private set; } = null;

        public UnitName UnitName { get; set; } = UnitName.Unit0;

        public MarkDirection MarkDirection { get; set; } = MarkDirection.Left;

        public MarkName MarkName { get; set; } = MarkName.Main;

        public CogTransform2DLinear CogTransformOrigin { get; set; }
        #endregion

        #region 이벤트
        public event ManualMatchingEventHandler ManualMatchingHandler;
        #endregion

        #region 델리게이트
        public delegate void ManualMatchingEventHandler(bool isManualMatchCompleted);
        #endregion

        #region 생성자
        public ManualMatchingForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void ManualMatchingForm_Load(object sender, EventArgs e)
        {
            TeachingData.Instance().UpdateTeachingData();
            CurrentUnit = TeachingData.Instance().GetUnit(UnitName.ToString());

            if (CurrentUnit != null)
            {
                SetPitch(Convert.ToInt32(lblPitch.Text));
                AddControls();
            }
        }

        public void SetParams(UnitName unitName, AreaCamera areaCamera, MarkDirection markDirection)
        {
            SetUnit(unitName);
            SetAreaCamera(areaCamera);
            RegisterAreaCameraEvent(areaCamera);
            SetMarkDirection(markDirection);
            AreaCamera.StartGrabContinous();
            // 트레인 이미지 불러오기
            LoadOriginPatternImage();
            PointMarkerParam.SetOriginEventHandler += VerifyOriginPointWithDrawingArea;
            DrawOriginPointMark(Display, new PointF(areaCamera.Camera.ImageWidth / 2, areaCamera.Camera.ImageHeight / 2), 200);   
        }

        private void VerifyOriginPointWithDrawingArea(CogTransform2DLinear originPoint)
        {
            // originPoint의 X,Y를 Display 너비,높이 범위 내에 들어오도록 변환
            PointF drawPoint = new PointF
            {
                X = Math.Min(Math.Max((float)originPoint.TranslationX, 0), Display.GetImageWidth()),
                Y = Math.Min(Math.Max((float)originPoint.TranslationY, 0), Display.GetImageHeight())
            };

            // 인자랑 다르면 다시 그리기
            if (drawPoint.X != (float)originPoint.TranslationX || drawPoint.Y != (float)originPoint.TranslationY)
                DrawOriginPointMark(Display, drawPoint, 200);
        }

        private void SetUnit(UnitName unitName)
        {
            UnitName = unitName;
            TeachingData.Instance().UpdateTeachingData();
            CurrentUnit = TeachingData.Instance().GetUnit(UnitName.ToString());
        }

        private void SetAreaCamera(AreaCamera areaCamera)
        {
            AreaCamera = areaCamera;
        }

        private void RegisterAreaCameraEvent(AreaCamera areaCamera)
        {
            AreaCamera.OnImageGrabbed += AreaCamera_OnImageGrabbed;
        }

        private void SetMarkDirection(MarkDirection PrealignMarkDirection)
        {
            MarkDirection = PrealignMarkDirection;
        }

        public void SetImage(ICogImage image)
        {
            Display.SetImage(image);
        }

        public PointF GetOriginPoint()
        {
            PointF originPoint = new PointF();

            originPoint.X = Convert.ToSingle(CogTransformOrigin.TranslationX);
            originPoint.Y = Convert.ToSingle(CogTransformOrigin.TranslationY);

            return originPoint;
        }

        private void SetOriginPoint()
        {
            CogTransformOrigin = PointMarkerParam.GetOriginPoint();
        }

        private void DrawOriginPointMark(CogDisplayControl display, PointF centerPoint, int size)
        {
            display.ClearGraphic();
            PointMarkerParam.SetOriginPoint(Convert.ToSingle(centerPoint.X), Convert.ToSingle(centerPoint.Y), size);
            display.SetInteractiveGraphics("tool", PointMarkerParam.GetCurrentRecord());
        }

        private void SetPitch(int pitch)
        {
            _pitch = pitch;
        }

        private void AddControls()
        {
            Display = new CogDisplayControl();
            Display.Dock = DockStyle.Fill;
            Display.DeleteEventHandler += Display_DeleteEventHandler;
            pnlDisplay.Controls.Add(Display);

            LightControl = new LightControl();
            LightControl.Dock = DockStyle.Fill;
            LightControl.SetParam(DeviceManager.Instance().LightCtrlHandler, CurrentUnit.PreAlign.LeftLightParam);
            pnlLight.Controls.Add(LightControl);
        }

        private void AreaCamera_OnImageGrabbed(Camera camera)
        {
            byte[] byteData = camera.GetGrabbedImage();
            if (byteData == null)
                return;

            var image = VisionProImageHelper.ConvertImage(byteData, camera.ImageWidth, camera.ImageHeight, camera.ColorFormat);

            Display.SetImage(image);
        }

        private void LoadOriginPatternImage()
        {
            var currentParam = CurrentUnit.GetPreAlignMark(MarkDirection, MarkName.Main).InspParam;

            cogPatternDisplay.InteractiveGraphics.Clear();
            cogPatternDisplay.StaticGraphics.Clear();

            CogDisplayHelper.DisposeDisplay(cogPatternDisplay);

            if (currentParam.IsTrained())
            {
                cogPatternDisplay.Image = null;
                cogPatternDisplay.Image = currentParam.GetTrainedPatternImage();
                CogPMAlignCurrentRecordConstants constants = CogPMAlignCurrentRecordConstants.TrainImage |
                                                        CogPMAlignCurrentRecordConstants.TrainImageMask;
            }
            else
                DisposePatternDisplay();
        }

        private void DisposePatternDisplay()
        {
            CogDisplayHelper.DisposeDisplay(cogPatternDisplay);
            cogPatternDisplay.Image = null;
        }

        private void Display_DeleteEventHandler(object sender, EventArgs e)
        {
            
        }

        private void lblPitch_Click(object sender, EventArgs e)
        {
            int pitch = KeyPadHelper.SetLabelIntegerData((Label)sender);
            SetPitch(pitch);
        }

        private void lblMoveUp_Click(object sender, EventArgs e)
        {
            float moveY = (float)PointMarkerParam.GetOriginPoint().TranslationY - _pitch;
            DrawOriginPointMark(Display, new PointF((float)PointMarkerParam.GetOriginPoint().TranslationX, moveY), 200);
        }

        private void lblMoveLeft_Click(object sender, EventArgs e)
        {
            float moveX = (float)PointMarkerParam.GetOriginPoint().TranslationX - _pitch;
            DrawOriginPointMark(Display, new PointF(moveX, (float)PointMarkerParam.GetOriginPoint().TranslationY), 200);
        }

        private void lblMoveRight_Click(object sender, EventArgs e)
        {
            float moveX = (float)PointMarkerParam.GetOriginPoint().TranslationX + _pitch;
            DrawOriginPointMark(Display, new PointF(moveX, (float)PointMarkerParam.GetOriginPoint().TranslationY), 200);
        }

        private void lblMoveDown_Click(object sender, EventArgs e)
        {
            float moveY = (float)PointMarkerParam.GetOriginPoint().TranslationY + _pitch;
            DrawOriginPointMark(Display, new PointF((float)PointMarkerParam.GetOriginPoint().TranslationX, moveY), 200);
        }

        private void lblApply_Click(object sender, EventArgs e)
        {
            SetManualMatching();
            AreaCamera.StopGrab();
            AreaCamera.OnImageGrabbed -= AreaCamera_OnImageGrabbed;
            Display.DisposeImage();
            ManualMatchingHandler?.Invoke(true);
            this.Close();
        }

        private void SetManualMatching()
        {
            SetOriginPoint();
        }

        private void lblCancel_Click(object sender, EventArgs e)
        {
            ManualMatchingHandler?.Invoke(false);
            this.Close();
        }
        #endregion

        private void ManualMatchingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //AreaCamera.StopGrab();
            //AreaCamera.OnImageGrabbed -= AreaCamera_OnImageGrabbed;
            //Display.DisposeImage();
        }
    }
}
