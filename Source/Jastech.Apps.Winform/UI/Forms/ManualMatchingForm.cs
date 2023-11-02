using Jastech.Apps.Winform.UI.Controls;
using Jastech.Framework.Winform.VisionPro.Controls;
using Jastech.Framework.Winform;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Winform.Controls;
using Jastech.Framework.Winform.Helper;
using Cognex.VisionPro;
using static Jastech.Framework.Device.Motions.AxisMovingParam;
using Jastech.Framework.Winform.VisionPro.Helper;
using Cognex.VisionPro.Display;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;

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

        private VisionProPointMarkerParam PointMarkerParam { get; set; } = null;

        public AreaCamera AreaCamera { get; set; } = null;

        public LAFCtrl LAFCtrl { get; set; } = null;

        public Unit CurrentUnit { get; private set; } = null;

        public UnitName UnitName { get; set; } = UnitName.Unit0;

        public MarkDirection MarkDirection { get; set; } = MarkDirection.Left;

        public MarkName MarkName { get; set; } = MarkName.Main;

        public CogTransform2DLinear OriginPoint { get; set; }
        #endregion

        #region 이벤트
        public GetOriginImageDelegate GetOriginImageHandler;

        public event ManualMatchingEventHandler ManualMatchingHandler;
        #endregion

        #region 델리게이트
        public delegate ICogImage GetOriginImageDelegate();

        public delegate void ManualMatchingEventHandler(AreaCamera areaCamera, MarkDirection markDirection);
        #endregion

        #region 생성자
        public ManualMatchingForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        public CogTransform2DLinear GetOriginPosition()
        {
            OriginPoint = PointMarkerParam.GetOriginPoint();
            return OriginPoint;
        }

        public void SetParams(AreaCamera areaCamera, MarkDirection markDirection)
        {
            SetAreaCamera(areaCamera);
            SetMarkDirection(markDirection);

            // 트레인 이미지 불러오기
        }

        private void SetAreaCamera(AreaCamera areaCamera)
        {
            AreaCamera = AreaCamera;
        }

        private void SetMarkDirection(MarkDirection PrealignMarkDirection)
        {
            MarkDirection = PrealignMarkDirection;
        }

        public void SetImage(ICogImage image)
        {
            Display.SetImage(image);
        }

        private void ManualMatchingForm_Load(object sender, EventArgs e)
        {
            TeachingData.Instance().UpdateTeachingData();
            CurrentUnit = TeachingData.Instance().GetUnit(UnitName.ToString());

            SetPitch(Convert.ToInt32(lblPitch.Text));
            AddControls();
            
            // TeachingUIManager 참조
            //TeachingUIManager.Instance().TeachingDisplayControl = Display;

            //AreaCamera.OnImageGrabbed += AreaCamera_OnImageGrabbed;
            //LoadPatternImage();

            //var param = CurrentUnit.GetPreAlignMark(MarkDirection, MarkName.Main);

            //if (param != null)
            //{
            //    CogTransform2DLinear OriginPosition = param.InspParam.GetOrigin();
            //    DrawOriginPointMark(Display, new PointF((float)OriginPosition.TranslationX, (float)OriginPosition.TranslationY), 200);
            //}
            //else
            //    DrawOriginPointMark(Display, new PointF(100, 100), 200);
        }

        public void RegisterAreaCameraEvent(AreaCamera areaCamera)
        {
            AreaCamera = areaCamera;
            AreaCamera.OnImageGrabbed += AreaCamera_OnImageGrabbed;
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

        private void LoadPatternImage()
        {
            if (GetOriginImage() != null)
            {
                CogDisplayHelper.DisposeDisplay(PatternDisplay);

                ICogImage originImage = GetOriginImageHandler();

                var preAlignMarkParam = CurrentUnit.PreAlign.AlignParamList.Where(x => x.Direction == MarkDirection && x.Name == MarkName).FirstOrDefault().InspParam;
                if (preAlignMarkParam.Train(originImage))
                {
                    DisposePatternDisplay();
                    PatternDisplay.Image = preAlignMarkParam.GetTrainedPatternImage();
                    PatternDisplay.StaticGraphics.Add(preAlignMarkParam.GetOrigin() as ICogGraphic, "Origin");
                }
            }
        }

        private ICogImage GetOriginImage()
        {
            if (GetOriginImageHandler != null)
            {
                ICogImage originImage = GetOriginImageHandler();
                return originImage;
            }

            return null;
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
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void lblCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
        #endregion
    }
}
