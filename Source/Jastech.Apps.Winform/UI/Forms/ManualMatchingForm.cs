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
using Jastech.Apps.Structure.Parameters;
using Jastech.Framework.Device.LightCtrls;

namespace Jastech.Apps.Winform.UI.Forms
{
    public partial class ManualMatchingForm : Form
    {
        #region 필드
        private int _pitch { get; set; } = 0;
        #endregion

        #region 속성
        public MarkDirection MarkDirection { get; set; } = MarkDirection.Left;

        public MarkName MarkName { get; set; } = MarkName.Main;

        private Unit Unit { get; set; } = null;

        private CogDisplayControl CogDisplayControl { get; set; } = null;

        private CogRecordDisplay PatternDisplay { get; set; } = null;

        private LightControl LightControl { get; set; } = null;

        private VisionProPointMarkerParam PointMarkerParam { get; set; } = new VisionProPointMarkerParam();

        public AreaCamera AreaCamera { get; set; } = null;

        public CogTransform2DLinear CogTransformOrigin { get; set; }

        public int MarkSize { get; set; } = 200;

        public PointF NewOriginPoint { get; set; } = new PointF();
        #endregion

        #region 이벤트
        public Action CloseEventDelegate;

        public event ManualMatchingEventHandler ManualMatchingHandler;
        #endregion

        #region 델리게이트
        public delegate void ManualMatchingEventHandler(bool isManualMatchCompleted, PointF originPoint);
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
            AddControls();
            LoadOriginPatternImage();

            if (AreaCamera != null)
                AreaCamera.OnImageGrabbed += AreaCamera_OnImageGrabbed;

            Initialize();
        }

        private void AddControls()
        {
            CogDisplayControl = new CogDisplayControl();
            CogDisplayControl.Dock = DockStyle.Fill;
            pnlDisplay.Controls.Add(CogDisplayControl);

            LightControl = new LightControl();
            LightControl.Dock = DockStyle.Fill;
            if(Unit != null)
            {
                var lightParameter = MarkDirection == MarkDirection.Left ? Unit.PreAlign.LeftLightParam : Unit.PreAlign.RightLightParam;
                LightControl.SetParam(DeviceManager.Instance().LightCtrlHandler, lightParameter);
            }

            pnlLight.Controls.Add(LightControl);

        }

        private void Initialize()
        {
            AreaCamera.StartGrabContinous();
            DrawOriginPointMark(new PointF(AreaCamera.Camera.ImageWidth / 2, AreaCamera.Camera.ImageHeight / 2));
        }

        private void VerifyOriginPointWithDrawingArea(CogTransform2DLinear originPoint)
        {
            // originPoint의 X,Y를 Display 너비,높이 범위 내에 들어오도록 변환
            PointF drawPoint = new PointF
            {
                X = Math.Min(Math.Max((float)originPoint.TranslationX, 0), CogDisplayControl.GetImageWidth()),
                Y = Math.Min(Math.Max((float)originPoint.TranslationY, 0), CogDisplayControl.GetImageHeight())
            };

            // 인자랑 다르면 다시 그리기
            if (drawPoint.X != (float)originPoint.TranslationX || drawPoint.Y != (float)originPoint.TranslationY)
                DrawOriginPointMark(drawPoint);
        }

        private void Release()
        {
            AreaCamera.StopGrab();
            if (AreaCamera != null)
                AreaCamera.OnImageGrabbed -= AreaCamera_OnImageGrabbed;

            CogDisplayControl.DisposeImage();
            DisposePatternDisplay();
        }

        public void SetParams(AreaCamera areaCamera, Unit unit, MarkDirection markDirection)
        {
            AreaCamera = areaCamera;
            Unit = unit;
            MarkDirection = MarkDirection;
        }

        public void SetImage(ICogImage image)
        {
            CogDisplayControl.SetImage(image);
        }

        private PointF GetOriginPoint()
        {
            var origin = PointMarkerParam.GetOriginPoint();

            PointF currentOrigin = new PointF();
            currentOrigin.X = Convert.ToSingle(origin.TranslationX);
            currentOrigin.Y = Convert.ToSingle(origin.TranslationY);

            return currentOrigin;
        }

        private void DrawOriginPointMark(PointF centerPoint)
        {
            CogDisplayControl.ClearGraphic();
            PointMarkerParam.SetOriginPoint(Convert.ToSingle(centerPoint.X), Convert.ToSingle(centerPoint.Y), MarkSize);
            CogDisplayControl.SetInteractiveGraphics("tool", PointMarkerParam.GetCurrentRecord());
        }

        private void AreaCamera_OnImageGrabbed(Camera camera)
        {
            byte[] byteData = camera.GetGrabbedImage();
            if (byteData == null)
                return;

            var image = VisionProImageHelper.ConvertImage(byteData, camera.ImageWidth, camera.ImageHeight, camera.ColorFormat);

            CogDisplayControl.SetImage(image);
        }

        private void LoadOriginPatternImage()
        {
            if(Unit != null)
            {
                var preAlignParam = Unit.GetPreAlignMark(MarkDirection, MarkName).InspParam;

                cogPatternDisplay.InteractiveGraphics.Clear();
                cogPatternDisplay.StaticGraphics.Clear();

                CogDisplayHelper.DisposeDisplay(cogPatternDisplay);

                if (preAlignParam.IsTrained())
                {
                    cogPatternDisplay.Image = null;
                    cogPatternDisplay.Image = preAlignParam.GetTrainedPatternImage();
                }
                else
                    DisposePatternDisplay();
            }
        }

        private void DisposePatternDisplay()
        {
            CogDisplayHelper.DisposeDisplay(cogPatternDisplay);
            cogPatternDisplay.Image = null;
        }

        private void lblPitch_Click(object sender, EventArgs e)
        {
            _pitch = KeyPadHelper.SetLabelIntegerData((Label)sender);
        }

        private void lblMoveUp_Click(object sender, EventArgs e)
        {
            float moveY = (float)PointMarkerParam.GetOriginPoint().TranslationY - _pitch;
            DrawOriginPointMark(new PointF((float)PointMarkerParam.GetOriginPoint().TranslationX, moveY));
        }

        private void lblMoveLeft_Click(object sender, EventArgs e)
        {
            float moveX = (float)PointMarkerParam.GetOriginPoint().TranslationX - _pitch;
            DrawOriginPointMark(new PointF(moveX, (float)PointMarkerParam.GetOriginPoint().TranslationY));
        }

        private void lblMoveRight_Click(object sender, EventArgs e)
        {
            float moveX = (float)PointMarkerParam.GetOriginPoint().TranslationX + _pitch;
            DrawOriginPointMark(new PointF(moveX, (float)PointMarkerParam.GetOriginPoint().TranslationY));
        }

        private void lblMoveDown_Click(object sender, EventArgs e)
        {
            float moveY = (float)PointMarkerParam.GetOriginPoint().TranslationY + _pitch;
            DrawOriginPointMark(new PointF((float)PointMarkerParam.GetOriginPoint().TranslationX, moveY));
        }

        private void lblApply_Click(object sender, EventArgs e)
        {
            AreaCamera.StopGrab();
            AreaCamera.OnImageGrabbed -= AreaCamera_OnImageGrabbed;
            CogDisplayControl.DisposeImage();

            ManualMatchingHandler?.Invoke(true, GetOriginPoint());
            DialogResult = DialogResult.OK;
            this.Close();
        }


        private void lblCancel_Click(object sender, EventArgs e)
        {
            Release();
            ManualMatchingHandler?.Invoke(false, GetOriginPoint());
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        private void ManualMatchingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Release();
            CloseEventDelegate?.Invoke();
        }
    }
}
