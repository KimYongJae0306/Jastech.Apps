using Cognex.VisionPro;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Apps.Winform.Service.Plc.Maps;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform;
using Jastech.Framework.Winform.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.Core.Calibrations
{
    public class VisionXCalibration
    {
        #region 필드
        private const int MATRIX_DIMENSION = 9;

        private double[] _absoluteAmountX = new double[MATRIX_DIMENSION];
        private double[] _absoluteAmountY = new double[MATRIX_DIMENSION];
        private double[] _absoluteAmountT = new double[MATRIX_DIMENSION];

        private double[] _motionPositionX = new double[MATRIX_DIMENSION];
        private double[] _motionPositionY = new double[MATRIX_DIMENSION];
        private double[] _motionPositionT = new double[MATRIX_DIMENSION];

        private double _currPositionX = 0.0;
        private double _currPositionY = 0.0;
        private double _currPositionT = 0.0;

        private double _moveToPositionX = 0.0;
        private double _moveToPositionY = 0.0;
        private double _moveToPositionT = 0.0;

        private double[] _calStepPositionX = new double[MATRIX_DIMENSION];
        private double[] _calStepPositionY = new double[MATRIX_DIMENSION];
        private double[] _calStepPositionT = new double[MATRIX_DIMENSION];

        private double[] _prevPositionX = new double[MATRIX_DIMENSION];
        private double[] _prevPositionY = new double[MATRIX_DIMENSION];
        private double[] _prevPositionT = new double[MATRIX_DIMENSION];

        private const int CAL_TIME_OUT = 5 * 1000;

        private double _initPositionX { get; set; } = 0.0;

        private double _initPositionY { get; set; } = 0.0;

        private double _initPositionT { get; set; } = 0.0;

        private double[] _calibrationResult = new double[MATRIX_DIMENSION];

        public CalibrationData CalibrationData = new CalibrationData();

        private int _matrixStepPoint = 0;
        private List<MatrixPointResult> _matrixPointResultList = new List<MatrixPointResult>();
        #endregion

        #region 속성
        private AxisHandler AxisHandler { get; set; } = null;

        private AxisMovingParam AxisMovingParam { get; set; } = null;

        private CalSeqStep CalSeqStep { get; set; } = CalSeqStep.CAL_SEQ_INIT;

        private AreaCamera AreaCamera { get; set; } = null;
        private byte[] imageData = null;

        //private Task CalSeqTask { get; set; }

        private Thread _workingThread = null;
        private bool _isWorking = false;
        //private CancellationTokenSource SeqTaskCancellationTokenSource { get; set; }

        private VisionProPatternMatchingParam VisionProPatternMatchingParam { get; set; } = null;

        private AlgorithmTool Algorithm = new AlgorithmTool();

        private CalibrationMode CalibrationMode { get; set; } = CalibrationMode.XY;

        public UnitName UnitName { get; set; } = UnitName.Unit0;

        public double MovePitchT { get; set; } = 0.7;

        private double BackLashPitchT = 0.1;

        private double _robotCenterX { get; set; } = 0.0;

        private double _robotCenterY { get; set; } = 0.0;

        public double IntervalX { get; private set; } = 0.0;

        public double IntervalY { get; private set; } = 0.0;
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public VisionXCalibration()
        {
            SetAxisMovingParam();
        }
        #endregion

        #region 메서드
        private void SetAxisMovingParam()
        {
            AxisMovingParam param = new AxisMovingParam();

            param.Velocity = 10;
            param.Acceleration = 10;
            param.Deceleration = 30;
            param.MovingTimeOut = 3 * 1000; // ms
            param.AfterWaitTime = 0; // ms

            AxisMovingParam = param;
        }

        public void SetCamera(AreaCamera camera)
        {
            AreaCamera = camera;
        }

        public void SetAxisHandler(AxisHandler axisHandler)
        {
            AxisHandler = axisHandler;
        }

        public void SetParam(VisionProPatternMatchingParam param)
        {
            VisionProPatternMatchingParam = new VisionProPatternMatchingParam();
            VisionProPatternMatchingParam = param.DeepCopy();
        }

        public void SetCalibrationMode(CalibrationMode calibrationMode)
        {
            CalibrationMode = calibrationMode;
        }
        #endregion

        public void SetInterval(double intervalX, double intervalY)
        {
            IntervalX = intervalX;
            IntervalY = intervalY;
        }

        public void StartCalSeqRun()
        {
            _isWorking = true;

            if (_workingThread != null)
            {
                CalSeqStep = CalSeqStep.CAL_SEQ_IDLE;
                return;
            }

            _workingThread = new Thread(new ThreadStart(CalibrationThread));
            _workingThread.Start();
        }

        public void CalSeqStop()
        {
            var areaCamera = AreaCameraManager.Instance().GetAppsCamera("PreAlign");
            areaCamera.StopGrab();
            AreaCameraManager.Instance().GetAreaCamera("PreAlign").StopGrab();

            if (_workingThread == null)
                return;

            AppsStatus.Instance().IsCalibrationing = false;
            _workingThread = null;
        }

        private void CalibrationThread()
        {
            //var cancellationToken = SeqTaskCancellationTokenSource.Token;
            //cancellationToken.ThrowIfCancellationRequested();
            CalSeqStep = CalSeqStep.CAL_SEQ_IDLE;

            while (_isWorking)
            {
                CalibrationSequence();
                Thread.Sleep(50);
            }

            if (_isWorking == false)
            {
                AppsStatus.Instance().IsCalibrationing = false;
                _workingThread = null;
            }
        }

        private double _x1, _x2, _y1, _y2, _theta;

        private void CalibrationSequence()
        {
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            if (inspModel == null)
                return;

            var unit = inspModel.GetUnit(UnitName.Unit0);
            if (unit == null)
                return;

            var light = DeviceManager.Instance().LightCtrlHandler;

            string logMessage = string.Empty;


            int directionT = 1;
            int directionX = -1;
            string savePath = string.Empty;

            Stopwatch sw = new Stopwatch();

            var camera = AreaCamera.Camera;
            VisionProPatternMatchingParam inspParam = VisionProPatternMatchingParam;
            ICogImage cogImage = null;


            switch (CalSeqStep)
            {
                case CalSeqStep.CAL_SEQ_IDLE:
                    Logger.Write(LogType.Device, "Start calibration sequence.");
                    CalSeqStep = CalSeqStep.CAL_SEQ_INIT;
                    break;

                case CalSeqStep.CAL_SEQ_INIT:
                    _x1 = 0.0;
                    _x2 = 0.0;
                    _y1 = 0.0;
                    _y2 = 0.0;
                    _theta = 0.0;
                    camera.Stop();

                    Logger.Write(LogType.Device, "Initialize calibration.");

                    MotionManager.Instance().MoveAxisX(UnitName, TeachingPosType.Stage1_PreAlign_Left);
                    Logger.Write(LogType.Device, "Move to Calibration start position.");

                    ClearMatrixPointResultList();
                    Logger.Write(LogType.Device, "Clear matrix.");

                    // 조명 켜기
                    light.TurnOn(unit.PreAlign.LeftLightParam);

                    Logger.Write(LogType.Device, "Light on.");

                    InitailizeOpticSetting();
                    Logger.Write(LogType.Device, "Initialize Camera.");

                    double initX = AxisHandler.GetAxis(AxisName.X).GetActualPosition();
                    double initY = PlcControlManager.Instance().GetReadPosition(AxisName.Y);
                    double initT = PlcControlManager.Instance().GetReadPosition(AxisName.T);

                    SetInitPoistion(initX, initY, initT);
                    Logger.Write(LogType.Device, "Initialize position data.");

                    CalSeqStep = CalSeqStep.CAL_SEQ_XY_INIT;
                    break;

                #region XY
                case CalSeqStep.CAL_SEQ_XY_INIT:
                    if (IntervalX == 0.0 && IntervalY == 0.0)
                    {
                        CalSeqStep = CalSeqStep.CAL_SEQ_COMPLETED;
                        break;
                    }

                    InitializeMoveAmount(IntervalX, IntervalY);
                    InitializeMotionPosition(IntervalX, IntervalY);
                    Logger.Write(LogType.Device, "Initialize XY calibration.");

                    _matrixStepPoint = 0;
                    CalSeqStep = CalSeqStep.CAL_SEQ_XY_MOVE;
                    break;

                case CalSeqStep.CAL_SEQ_XY_MOVE:
                    RequestMove(_absoluteAmountX[_matrixStepPoint] * (directionX), _absoluteAmountY[_matrixStepPoint], 0);
                    Console.WriteLine("_motionPositionY[_matrixStepPoint] : " + _motionPositionY[_matrixStepPoint].ToString("F3"));
                    logMessage = string.Format("Request move command. - position step : {0}", _matrixStepPoint);
                    Logger.Write(LogType.Device, logMessage);

                    sw.Restart();
                    CalSeqStep = CalSeqStep.CAL_SEQ_XY_MOVE_COMPLETED;
                    break;

                case CalSeqStep.CAL_SEQ_XY_MOVE_COMPLETED:
                    if (sw.ElapsedMilliseconds > CAL_TIME_OUT)
                    {
                        Logger.Write(LogType.Error, "Failed to move request. - Timeout error");
                        CalSeqStep = CalSeqStep.CAL_SEQ_ERROR;
                        break;
                    }

                    if (RequestMoveDone() == false)
                        break;
                    else
                    {
                        RequestMoveOff();

                        if (IsInPosition() == false)
                        {
                            Logger.Write(LogType.Error, "Failed to move request. - In-Position error");
                            CalSeqStep = CalSeqStep.CAL_SEQ_ERROR;
                            break;
                        }
                    }

                    CalSeqStep = CalSeqStep.CAL_SEQ_XY_GRAB;
                    break;

                case CalSeqStep.CAL_SEQ_XY_GRAB:
                    // 그랩
                    logMessage = string.Format("Start grab. - position step : {0}", _matrixStepPoint);
                    Logger.Write(LogType.Device, logMessage);

                    cogImage = GetAreaCameraImage(camera);
                    savePath = string.Format(@"D:\PositionStep_{0}.bmp", _matrixStepPoint);
                    VisionProImageHelper.Save(cogImage, savePath);
                    Thread.Sleep(50);

                    // 패턴 매칭
                    Logger.Write(LogType.Device, "Start pattern matching.");
                    MatrixPointResult matrixPointResult = new MatrixPointResult();
                    matrixPointResult = GetMatrixPoint(cogImage, inspParam);

                    AddMatrixPointResultList(matrixPointResult);

                    _matrixStepPoint++;
                    CalSeqStep = CalSeqStep.CAL_SEQ_XY_CHECK_MATRIX;
                    break;

                case CalSeqStep.CAL_SEQ_XY_CHECK_MATRIX:
                    if (_matrixStepPoint < MATRIX_DIMENSION)
                        CalSeqStep = CalSeqStep.CAL_SEQ_XY_MOVE;
                    else
                        CalSeqStep = CalSeqStep.CAL_SEQ_XY_OFFSET;
                    break;

                case CalSeqStep.CAL_SEQ_XY_OFFSET:
                    var resultList = GetMatrixPointResultList();

                    // offset = 카메라 중심 - 9캘 중심
                    Logger.Write(LogType.Device, "Calculate offset.");
                    double offsetX = (camera.ImageWidth / 2) - resultList[4].PixelX;
                    double offsetY = (camera.ImageHeight / 2) - resultList[4].PixelY;

                    foreach (var item in GetMatrixPointResultList())
                    {
                        item.PixelX += offsetX;
                        item.PixelY += offsetY;
                    }

                    CalSeqStep = CalSeqStep.CAL_SEQ_XY_CALIBRATION;
                    break;

                case CalSeqStep.CAL_SEQ_XY_CALIBRATION:
                    Logger.Write(LogType.Device, "Calculate calibration.");
                    if (CalculateCalibrationMatrix(GetMatrixPointResultList(), ref _calibrationResult) == false)
                    {
                        Logger.Write(LogType.Error, "Failed to calculate calibration.");
                        CalSeqStep = CalSeqStep.CAL_SEQ_ERROR;
                        break;
                    }
                    CalibrationData.Instance().SetCalibrationData(_calibrationResult.ToList());

                    CalSeqStep = CalSeqStep.CAL_SEQ_XY_MOVE_REF;
                    break;

                case CalSeqStep.CAL_SEQ_XY_MOVE_REF:
                    RequestMove(_absoluteAmountX[0] * (directionX), _absoluteAmountY[0], 0);
                    Logger.Write(LogType.Device, "Request move command. - init position");
                    sw.Restart();

                    CalSeqStep = CalSeqStep.CAL_SEQ_XY_MOVE_REF_COMPLETED;
                    break;

                case CalSeqStep.CAL_SEQ_XY_MOVE_REF_COMPLETED:
                    if (sw.ElapsedMilliseconds > CAL_TIME_OUT)
                    {
                        Logger.Write(LogType.Error, "Failed to move request. - Timeout error");
                        CalSeqStep = CalSeqStep.CAL_SEQ_ERROR;
                        break;
                    }

                    if (RequestMoveDone() == false)
                        break;
                    else
                    {
                        RequestMoveOff();

                        if (IsInPosition() == false)
                        {
                            Logger.Write(LogType.Error, "Failed to move request. - In-Position error");
                            CalSeqStep = CalSeqStep.CAL_SEQ_ERROR;
                            break;
                        }
                    }

                    CalSeqStep = CalSeqStep.CAL_SEQ_XY_REF_GRAB;
                    break;

                case CalSeqStep.CAL_SEQ_XY_REF_GRAB:
                    Thread.Sleep(50);

                    // 그랩
                    Logger.Write(LogType.Device, "Start grab. - init position");
                    camera.GrabOnce();
                    imageData = camera.GetGrabbedImage();

                    Thread.Sleep(50);
                    cogImage = VisionProImageHelper.ConvertImage(imageData, camera.ImageWidth, camera.ImageHeight, Framework.Imaging.ColorFormat.Gray);

                    savePath = string.Format(@"D:\CAL_SEQ_XY_REF_GRAB.bmp", _matrixStepPoint);
                    VisionProImageHelper.Save(cogImage, savePath);
                    Thread.Sleep(50);

                    // 마지막 포인트는 패턴 매칭 안해도 될 듯
                    //// 패턴 매칭
                    //MatrixPointResult matrixReferencePointResult = new MatrixPointResult();
                    //matrixReferencePointResult = GetMatrixPoint(_matrixStepPoint, copyCogImage, inspParam);

                    //AddMatrixPointResultList(matrixReferencePointResult);

                    CalSeqStep = CalSeqStep.CAL_SEQ_XY_COMPLETED;
                    break;

                case CalSeqStep.CAL_SEQ_XY_COMPLETED:
                    if (CalibrationMode == CalibrationMode.XY)
                    {
                        Logger.Write(LogType.Device, "Complete XY calibration.");
                        CalSeqStep = CalSeqStep.CAL_SEQ_SAVE_RESULT_DATA;
                    }
                    else
                    {
                        Logger.Write(LogType.Device, "Ready to T calibration.");
                        CalSeqStep = CalSeqStep.CAL_SEQ_T_INIT;
                    }
                    break;
                #endregion

                #region T
                case CalSeqStep.CAL_SEQ_T_INIT:
                    Logger.Write(LogType.Device, "Initialize calibration T.");

                    CalibrationData.Instance().SetCalibrationStartPosition(GetCurrentX(), GetCurrentY());

                    if (MovePitchT == 0.0)
                    {
                        CalSeqStep = CalSeqStep.CAL_SEQ_T_COMPLETED;
                        break;
                    }

                    CalSeqStep = CalSeqStep.CAL_SEQ_T_MOVE;
                    break;

                case CalSeqStep.CAL_SEQ_T_MOVE:
                    directionT = -1;

                    RequestMove(0, 0, (MovePitchT + BackLashPitchT) * directionT);
                    Logger.Write(LogType.Device, "Request move command.");

                    sw.Restart();

                    CalSeqStep = CalSeqStep.CAL_SEQ_T_MOVE_COMPLETED;
                    break;

                case CalSeqStep.CAL_SEQ_T_MOVE_COMPLETED:
                    if (sw.ElapsedMilliseconds > CAL_TIME_OUT)
                    {
                        Logger.Write(LogType.Error, "Failed to move request. - Timeout error");
                        CalSeqStep = CalSeqStep.CAL_SEQ_ERROR;
                        break;
                    }

                    if (RequestMoveDone() == false)
                        break;
                    else
                    {
                        RequestMoveOff();

                        if (IsInPosition() == false)
                        {
                            Logger.Write(LogType.Error, "Failed to move request. - In-Position error");
                            CalSeqStep = CalSeqStep.CAL_SEQ_ERROR;
                            break;
                        }
                    }

                    CalSeqStep = CalSeqStep.CAL_SEQ_T_COMPENSATION_BACKLASH;
                    break;

                case CalSeqStep.CAL_SEQ_T_COMPENSATION_BACKLASH:
                    directionT = 1;
                    RequestMove(0, 0, BackLashPitchT * directionT);
                    Logger.Write(LogType.Device, "Request compensation move command.");
                    sw.Restart();

                    CalSeqStep = CalSeqStep.CAL_SEQ_T_COMPENSATION_BACKLASH_COMPLETED;
                    break;

                case CalSeqStep.CAL_SEQ_T_COMPENSATION_BACKLASH_COMPLETED:
                    if (sw.ElapsedMilliseconds > CAL_TIME_OUT)
                    {
                        Logger.Write(LogType.Error, "Failed to move request. - Timeout error");
                        CalSeqStep = CalSeqStep.CAL_SEQ_ERROR;
                        break;
                    }

                    if (RequestMoveDone() == false)
                        break;
                    else
                    {
                        RequestMoveOff();

                        if (IsInPosition() == false)
                        {
                            Logger.Write(LogType.Error, "Failed to move request. - In-Position error");
                            CalSeqStep = CalSeqStep.CAL_SEQ_ERROR;
                            break;
                        }
                    }

                    CalSeqStep = CalSeqStep.CAL_SEQ_T_GRAB;
                    break;

                case CalSeqStep.CAL_SEQ_T_GRAB:
                    // 그랩
                    Logger.Write(LogType.Device, "Start grab.");

                    cogImage = GetAreaCameraImage(camera);
                    savePath = string.Format(@"D:\Position_T_Step_1.bmp");
                    VisionProImageHelper.Save(cogImage, savePath);
                    Thread.Sleep(50);

                    // 패턴 매칭
                    Logger.Write(LogType.Device, "Start pattern matching.");
                    var t1 = GetMatrixPoint(cogImage, inspParam);

                    PointF calVision = new PointF();
                    calVision.X = Convert.ToSingle(t1.PixelX);
                    calVision.Y = Convert.ToSingle(t1.PixelY);

                    PointF calReal = CalibrationData.Instance().ConvertVisionToReal(calVision, true);

                    _x1 = GetCurrentX() + calReal.X;
                    _y1 = GetCurrentY() + calReal.Y;

                    //matrixPointResultList.Add(matrixReferencePointResult);

                    CalSeqStep = CalSeqStep.CAL_SEQ_T_MOVE_X2;
                    break;

                case CalSeqStep.CAL_SEQ_T_MOVE_X2:
                    directionT = 1;

                    RequestMove(0, 0, (MovePitchT * 2) * directionT);        // 설정값의 2배 돌리기
                    Logger.Write(LogType.Device, "Request x2 move command.");
                    sw.Restart();

                    CalSeqStep = CalSeqStep.CAL_SEQ_T_MOVE_X2_COMPLETED;
                    break;

                case CalSeqStep.CAL_SEQ_T_MOVE_X2_COMPLETED:
                    if (sw.ElapsedMilliseconds > CAL_TIME_OUT)
                    {
                        Logger.Write(LogType.Error, "Failed to move request. - Timeout error");
                        CalSeqStep = CalSeqStep.CAL_SEQ_ERROR;
                        break;
                    }

                    if (RequestMoveDone() == false)
                        break;
                    else
                    {
                        RequestMoveOff();

                        if (IsInPosition() == false)
                        {
                            Logger.Write(LogType.Error, "Failed to move request. - In-Position error");
                            CalSeqStep = CalSeqStep.CAL_SEQ_ERROR;
                            break;
                        }
                    }

                    CalSeqStep = CalSeqStep.CAL_SEQ_T_GRAB_X2;
                    break;

                case CalSeqStep.CAL_SEQ_T_GRAB_X2:
                    // 그랩
                    Logger.Write(LogType.Device, "Start grab.");
                    Thread.Sleep(2000);
                    cogImage = GetAreaCameraImage(camera);
                    savePath = string.Format(@"D:\Position_T_Step_2.bmp");
                    VisionProImageHelper.Save(cogImage, savePath);
                    Thread.Sleep(50);

                    // 패턴 매칭
                    Logger.Write(LogType.Device, "Start pattern matching.");
                    var t2 = GetMatrixPoint(cogImage, inspParam);

                    PointF calVision2 = new PointF();
                    calVision2.X = Convert.ToSingle(t2.PixelX);
                    calVision2.Y = Convert.ToSingle(t2.PixelY);

                    PointF calReal2 = CalibrationData.Instance().ConvertVisionToReal(calVision2, true);

                    _x2 = (GetCurrentX()) + calReal2.X;
                    _y2 = (GetCurrentY()) + calReal2.Y;

                    //matrixPointResultList.Add(matrixReferencePointResult);

                    CalSeqStep = CalSeqStep.CAL_SEQ_T_CALC_THETA;
                    break;

                case CalSeqStep.CAL_SEQ_T_CALC_THETA:
                    _theta = (MovePitchT * 2) * Math.PI / 180.0;
                    _robotCenterX = _x1 - 0.5 * ((_x1 - _x2) + (_y1 - _y2) * Math.Sin(_theta) / (Math.Cos(_theta) - 1.0));
                    _robotCenterY = _y1 - 0.5 * ((_y1 - _y2) - (_x1 - _x2) * Math.Sin(_theta) / (Math.Cos(_theta) - 1.0));

                    Logger.Write(LogType.Device, "Calculate theta.");

                    CalSeqStep = CalSeqStep.CAL_SEQ_T_MOVE_OPPOSITE;
                    break;

                case CalSeqStep.CAL_SEQ_T_MOVE_OPPOSITE:
                    directionT = -1;

                    RequestMove(0, 0, (MovePitchT * directionT));
                    Logger.Write(LogType.Device, "Request opposite direction move command.");
                    sw.Restart();

                    CalSeqStep = CalSeqStep.CAL_SEQ_T_MOVE_OPPOSITE_COMPLETED;
                    break;

                case CalSeqStep.CAL_SEQ_T_MOVE_OPPOSITE_COMPLETED:
                    if (sw.ElapsedMilliseconds > CAL_TIME_OUT)
                    {
                        Logger.Write(LogType.Error, "Failed to move request. - Timeout error");
                        CalSeqStep = CalSeqStep.CAL_SEQ_ERROR;
                        break;
                    }

                    if (RequestMoveDone() == false)
                        break;
                    else
                    {
                        RequestMoveOff();

                        if (IsInPosition() == false)
                        {
                            Logger.Write(LogType.Error, "Failed to move request. - In-Position error");
                            CalSeqStep = CalSeqStep.CAL_SEQ_ERROR;
                            break;
                        }
                    }

                    CalSeqStep = CalSeqStep.CAL_SEQ_T_COMPLETED;
                    break;

                case CalSeqStep.CAL_SEQ_T_COMPLETED:
                    Logger.Write(LogType.Device, "Complete T calibration.");

                    CalSeqStep = CalSeqStep.CAL_SEQ_SAVE_RESULT_DATA;
                    break;

                case CalSeqStep.CAL_SEQ_T_ADV_INIT:
                    break;

                case CalSeqStep.CAL_SEQ_T_ADV_GRAB:
                    break;

                case CalSeqStep.CAL_SEQ_T_ADV_MOVE:
                    break;

                case CalSeqStep.CAL_SEQ_T_ADV_MOVE_COMPLETED:
                    break;

                case CalSeqStep.CAL_SEQ_T_ADV_GRAB2:
                    break;

                case CalSeqStep.CAL_SEQ_T_ADV_MOVE2:
                    break;

                case CalSeqStep.CAL_SEQ_T_ADV_MOVE2_COMPLETED:
                    break;

                case CalSeqStep.CAL_SEQ_T_ADV_CALC:
                    break;
                #endregion

                case CalSeqStep.CAL_SEQ_ERROR:
                    Logger.Write(LogType.Error, "Occur error at calibration sequence.");

                    string message = string.Format("Calibration Error.\nDo you want to go to Origin?\nX : {0}, Y : {1}, T : {2}", 
                        GetInitPositionX().ToString("F4"), GetInitPositionY().ToString("F4"), GetInitPositionT().ToString("F4"));

                    MessageYesNoForm form = new MessageYesNoForm();
                    form.Message = message;

                    if (form.ShowDialog() == DialogResult.Yes)
                    {
                        Logger.Write(LogType.Error, "Run return to origin.");
                        CalSeqStep = CalSeqStep.CAL_SEQ_ERROR_MOVE_REF;
                    }
                    else
                        CalSeqStep = CalSeqStep.CAL_SEQ_ERROR_SEND;
                    break;

                case CalSeqStep.CAL_SEQ_ERROR_MOVE_REF:
                    directionT = 1;

                    RequestMove(GetInitPositionX(), GetInitPositionY(), GetInitPositionT());
                    Logger.Write(LogType.Error, "Request initial position move command.");
                    sw.Restart();

                    CalSeqStep = CalSeqStep.CAL_SEQ_ERROR_MOVE_REF_TIMEOUT;
                    break;

                case CalSeqStep.CAL_SEQ_ERROR_MOVE_REF_TIMEOUT:
                    if (sw.ElapsedMilliseconds > CAL_TIME_OUT)
                    {
                        Logger.Write(LogType.Error, "Failed to move request. - Timeout error");
                        CalSeqStep = CalSeqStep.CAL_SEQ_ERROR;
                        break;
                    }

                    if (RequestMoveDone() == false)
                        break;

                    RequestMoveOff();

                    if (IsInPosition() == false)
                    {
                        CalSeqStep = CalSeqStep.CAL_SEQ_ERROR;
                        Logger.Write(LogType.Error, "Failed to move request. - In-Position error");
                        break;
                    }

                    CalSeqStep = CalSeqStep.CAL_SEQ_ERROR_SEND;
                    break;

                case CalSeqStep.CAL_SEQ_ERROR_SEND:
                    Logger.Write(LogType.Device, "Failed calibration.");

                    PlcControlManager.Instance().WritePcStatus(PlcCommand.Calibration, false);
                    Logger.Write(LogType.Device, "Send calibration abnormal complete signal.");

                    CalSeqStep = CalSeqStep.CAL_SEQ_COMPLETED;
                    break;

                case CalSeqStep.CAL_SEQ_SAVE_RESULT_DATA:
                    // Set calibration data
                    //CalibrationData.Instance().SetCalibrationData(_calibrationResult.ToList());
                    CalibrationData.Instance().SetRotationCenter(_robotCenterX, _robotCenterY);

                    Logger.Write(LogType.Device, "Set calibration data.");

                    var matrixPointResultList = GetMatrixPointResultList();

                    // Set calibration log
                    CalibrationData.Instance().SetCalibrationLogData(matrixPointResultList);
                    Logger.Write(LogType.Device, "Set calibration log.");

                    // Save calibration data
                    CalibrationData.Instance().SaveCalibrationResultData();
                    Logger.Write(LogType.Device, "Save calibration data.");

                    // Save calibration log
                    CalibrationData.Instance().SaveCalibrationLogData(matrixPointResultList);
                    Logger.Write(LogType.Device, "Save calibration log.");

                    CalSeqStep = CalSeqStep.CAL_SEQ_SEND_RESULT_DATA;
                    break;

                case CalSeqStep.CAL_SEQ_SEND_RESULT_DATA:
                    
                    //PlcControlManager.Instance().ClearAddress(PlcCommonMap.PLC_Command);
                    Logger.Write(LogType.Device, "Send calibration data.");

                    // Send calibration abnormal complete signal
                    PlcControlManager.Instance().WritePcStatus(PlcCommand.Calibration);
                    Logger.Write(LogType.Device, "Send calibration normal complete signal.");

                    CalSeqStep = CalSeqStep.CAL_SEQ_COMPLETED;
                    break;

                case CalSeqStep.CAL_SEQ_COMPLETED:
                    Logger.Write(LogType.Device, "Complete calibration.");

                    // Light off
                    light.TurnOff();
                    Logger.Write(LogType.Device, "Light off.");

                    CalSeqStep = CalSeqStep.CAL_SEQ_STOP;
                    break;

                case CalSeqStep.CAL_SEQ_STOP:
                    // Light off
                    light.TurnOff();
                    Logger.Write(LogType.Device, "Light off.");
                    _isWorking = false; 
                    _workingThread = null;
                    AppsStatus.Instance().IsCalibrationing = false;
                    Console.WriteLine("CalSeqStep : CAL_SEQ_STOP");
                    break;

                default:
                    break;
            }
        }

        private ICogImage GetAreaCameraImage(Camera camera)
        {
            camera.GrabOnce();
            byte[] dataArrayRight = camera.GetGrabbedImage();
            var cogImage = VisionProImageHelper.ConvertImage(dataArrayRight, camera.ImageWidth, camera.ImageHeight, camera.ColorFormat);

            return cogImage;
        }

        private void InitailizeOpticSetting()
        {
            AreaCamera.Camera.SetExposureTime(1000);
            AreaCamera.Camera.SetAnalogGain(1);
            Thread.Sleep(50);
        }

        private void SetInitPoistion(double positionX, double positionY, double positionT)
        {
            _initPositionX = positionX;
            _initPositionY = positionY;
            _initPositionT = positionT;
        }

        private double GetInitPositionX()
        {
            return _initPositionX;
        }

        private double GetInitPositionY()
        {
            return _initPositionY;
        }

        private double GetInitPositionT()
        {
            return _initPositionT;
        }

        private double GetCurrentX()
        {
            return AxisHandler.GetAxis(AxisName.X).GetActualPosition();
        }

        private double GetCurrentY()
        {
            return PlcControlManager.Instance().GetReadPosition(AxisName.Y);
        }

        private double GetCurrentT()
        {
            return PlcControlManager.Instance().GetReadPosition(AxisName.T);
        }

        private void InitializeMoveAmount(double amountX, double amountY)
        {
            for (int matrixPoint = 0; matrixPoint < MATRIX_DIMENSION; matrixPoint++)
            {
                switch (matrixPoint)
                {
                    case 0:
                        _absoluteAmountX[matrixPoint] = -amountX;
                        _absoluteAmountY[matrixPoint] = amountY;
                        break;

                    case 1:
                        _absoluteAmountX[matrixPoint] = amountX;
                        _absoluteAmountY[matrixPoint] = 0;
                        break;

                    case 2:
                        _absoluteAmountX[matrixPoint] = amountX;
                        _absoluteAmountY[matrixPoint] = 0;
                        break;

                    case 3:
                        _absoluteAmountX[matrixPoint] = -amountX * 2;
                        _absoluteAmountY[matrixPoint] = -amountY;
                        break;

                    case 4:
                        _absoluteAmountX[matrixPoint] = amountX;
                        _absoluteAmountY[matrixPoint] = 0;
                        break;

                    case 5:
                        _absoluteAmountX[matrixPoint] = amountX;
                        _absoluteAmountY[matrixPoint] = 0;
                        break;

                    case 6:
                        _absoluteAmountX[matrixPoint] = -amountX * 2;
                        _absoluteAmountY[matrixPoint] = -amountY;
                        break;

                    case 7:
                        _absoluteAmountX[matrixPoint] = amountX;
                        _absoluteAmountY[matrixPoint] = 0;
                        break;

                    case 8:
                        _absoluteAmountX[matrixPoint] = amountX;
                        _absoluteAmountY[matrixPoint] = 0;
                        break;

                    default:
                        break;
                }
            }
        }

        private void InitializeMotionPosition(double amountX, double amountY)
        {
            for (int matrixPoint = 0; matrixPoint < MATRIX_DIMENSION; matrixPoint++)
            {
                if (matrixPoint % 3 == 0)
                {
                    _motionPositionX[matrixPoint] = -amountX;
                    // -P * *
                    // * * *
                    // * * *
                }
                else if (matrixPoint % 3 == 1)
                {
                    _motionPositionX[matrixPoint] = 0;
                    // -P 0 *
                    // * * *
                    // * * *
                }
                else if (matrixPoint % 3 == 2)
                {
                    _motionPositionX[matrixPoint] = amountX;
                    // -P 0 P
                    // * * *
                    // * * *
                }

                //------------------Y------------------//
                if (matrixPoint < 3)
                {
                    _motionPositionY[matrixPoint] = amountY;
                    // (,P) (,P) (,P)
                    // * * *
                    // * * *
                }
                else if (matrixPoint < 6)
                {
                    _motionPositionY[matrixPoint] = 0;
                    // (,P) (,P) (,P)
                    // (,0) (,0) (,0)
                    // * * *
                }
                else if (matrixPoint < 9)
                {
                    _motionPositionY[matrixPoint] = -amountY;
                    // (,P) (,P) (,P)
                    // (,0) (,0) (,0)
                    // (,-P) (,-P) (,-P)
                }
            }
        }

        private void RequestMove(double amountX, double amountY, double amountT)
        {
            SetCurrentPosition();

            // Move X (PC)
            AxisHandler.GetAxis(AxisName.X).StartRelativeMove(amountX, AxisMovingParam);

            // Move Y (PLC)
            PlcControlManager.Instance().WriteAlignData(0, amountY, amountT);     // Relative Move
            Thread.Sleep(50);

            PlcControlManager.Instance().WriteMoveRequest();

            string logMessage = string.Format("move amount - X : {0} / Y : {1}", amountX.ToString("F3"), amountY.ToString("F3"));
            Logger.Write(LogType.Device, logMessage);

            SetMoveToPosition(amountX, amountY, amountT);
        }

        private void RequestMoveOff()
        {
            PlcControlManager.Instance().WriteMoveRequest(true);
        }

        private void SetCurrentPosition()
        {
            _currPositionX = AxisHandler.GetAxis(AxisName.X).GetActualPosition() / 1000.0;
            _currPositionY = PlcControlManager.Instance().GetReadPosition(AxisName.Y) / 1000.0;
            _currPositionT = PlcControlManager.Instance().GetReadPosition(AxisName.T) / 1000.0;
        }

        private void SetMoveToPosition(double amountX, double amountY, double amountT)
        {
            _moveToPositionX = _currPositionX + amountX;
            _moveToPositionY = _currPositionY + amountY;
            _moveToPositionT = _currPositionT + amountT;
        }

        private bool RequestMoveDone()
        {
            int step = 0;
            int count = 0;
            bool doneX = false;
            bool doneY = false;
            bool done = false;

            do
            {
                switch (step)
                {
                    case 0:
                        doneX = AxisHandler.GetAxis(AxisName.X).GetCurrentMotionStatus() == "NORMAL" ? true : false;
                        if (doneX)
                            step++;

                        break;

                    case 1:
                        doneY = false;
                        doneY = PlcControlManager.Instance().GetAddressMap(PlcCommonMap.PLC_Move_END).Value == "5" ? true : false;
                        if (doneY)
                            count++;

                        if (count == 3)
                            step++;
                        break;

                    case 2:
                        done = doneX && doneY;
                        PlcControlManager.Instance().ClearAddress(PlcCommonMap.PLC_Move_END);
                        break;

                    default:
                        break;
                }
            } while (!done);

            return done;
        }

        private bool IsInPosition()
        {
            return IsInPositionX() || IsInPositionY() || IsInPositionT();
        }

        private bool IsInPositionX()
        {
            return Math.Abs(_currPositionX - _moveToPositionX) <= double.Epsilon;
        }

        private bool IsInPositionY()
        {
            return Math.Abs(_currPositionY - _moveToPositionY) <= double.Epsilon;
        }

        private bool IsInPositionT()
        {
            return Math.Abs(_currPositionT - _moveToPositionT) <= double.Epsilon;
        }

        //private void SetCalibrationStepPosition(int matrixPoint, double x, double y, double t)
        //{
        //    _calStepPositionX[matrixPoint] = x;
        //    _calStepPositionY[matrixPoint] = y;
        //    _calStepPositionT[matrixPoint] = t;
        //}

        //private double GetCalStepPositionX(int matrixPoint)
        //{
        //    return _calStepPositionX[matrixPoint];
        //}

        //private double GetCalStepPositionY(int matrixPoint)
        //{
        //    return _calStepPositionY[matrixPoint];
        //}

        //private double GetCalStepPositionT(int matrixPoint)
        //{
        //    return _calStepPositionT[matrixPoint];
        //}

        private bool CalculateCalibrationMatrix(List<MatrixPointResult> resultList, ref double[] resultMatrix)
        {
            double[] visionCoordinates = new double[MATRIX_DIMENSION * 2];
            double[] robotCoordinates = new double[MATRIX_DIMENSION * 2];

            int columnCount = 8;
            int matrixCount = 2 * columnCount;

            double[] matrix1 = new double[MATRIX_DIMENSION * matrixCount];
            double[] matrix2 = new double[MATRIX_DIMENSION * matrixCount];

            double[] buffer1 = new double[8 * 8];
            double[] buffer2 = new double[8 * 8];

            double[] calTemp = new double[8];

            for (int index = 0; index < MATRIX_DIMENSION; index++)
            {
                visionCoordinates[index * 2] = resultList[index].PixelX;
                visionCoordinates[index * 2 + 1] = resultList[index].PixelY;

                robotCoordinates[index * 2] = _motionPositionX[index];
                robotCoordinates[index * 2 + 1] = _motionPositionY[index];
                Console.WriteLine("_motionPositionX[index] : " + _motionPositionX[index]);
                Console.WriteLine("_motionPositionY[index] : " + _motionPositionY[index]);
            }

            for (int index = 0; index < MATRIX_DIMENSION; index++)
            {
                matrix1[index * matrixCount + 0] = visionCoordinates[index * 2];
                matrix1[index * matrixCount + 1] = visionCoordinates[index * 2 + 1];
                matrix1[index * matrixCount + 2] = 1.0;
                matrix1[index * matrixCount + 3] = 0.0;
                matrix1[index * matrixCount + 4] = 0.0;
                matrix1[index * matrixCount + 5] = 0.0;
                matrix1[index * matrixCount + 6] = (-1.0) * robotCoordinates[index * 2] * visionCoordinates[index * 2];
                matrix1[index * matrixCount + 7] = (-1.0) * robotCoordinates[index * 2] * visionCoordinates[index * 2 + 1];

                matrix1[index * matrixCount + 8] = 0.0;
                matrix1[index * matrixCount + 9] = 0.0;
                matrix1[index * matrixCount + 10] = 0.0;
                matrix1[index * matrixCount + 11] = visionCoordinates[index * 2];
                matrix1[index * matrixCount + 12] = visionCoordinates[index * 2 + 1];
                matrix1[index * matrixCount + 13] = 1.0;
                matrix1[index * matrixCount + 14] = (-1.0) * robotCoordinates[index * 2 + 1] * visionCoordinates[index * 2];
                matrix1[index * matrixCount + 15] = (-1.0) * robotCoordinates[index * 2 + 1] * visionCoordinates[index * 2 + 1];
            }

            TransposeMatrix(matrix1, ref matrix2, MATRIX_DIMENSION * 2, 8);

            MultiplyMatrix(matrix2, matrix1, ref buffer1, 8, MATRIX_DIMENSION * 2, 8);

            if (InverseMatrix(buffer1, 8, ref buffer2) == false)
                return false;

            MultiplyMatrix(matrix2, robotCoordinates, ref calTemp, 8, MATRIX_DIMENSION * 2, 1);
            MultiplyMatrix(buffer2, calTemp, ref resultMatrix, 8, 8, 1);

            resultMatrix[8] = 1.0;

            return true;
        }

        private void TransposeMatrix(double[] matrix1, ref double[] matrix2, int rowCount, int columnCount)
        {
            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                    matrix2[column * rowCount + row] = matrix1[row * columnCount + column];
            }
        }

        private void MultiplyMatrix(double[] matrix1, double[] matrix2, ref double[] buffer, int l, int rowCount, int columnCount)
        {
            double t = 0.0;

            for (int i = 0; i < l; i++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    t = 0.0;

                    for (int row = 0; row < rowCount; row++)
                        t += matrix1[i * rowCount + row] * matrix2[row * columnCount + column];

                    buffer[i * columnCount + column] = t;
                }
            }
        }

        private bool InverseMatrix(double[] buffer1, int dimension, ref double[] buffer2)
        {
            int i, j, k, l, l1;
            double c, d;
            const double SMALLEST = 1.0e-50;

            for (i = 0; i < dimension; i++)
            {
                for (j = 0; j < dimension; j++)
                {
                    buffer2[i * dimension + j] = 0.0;
                }
                buffer2[i * dimension + i] = 1.0;
            }

            for (l = 0; l < dimension; l++)
            {
                d = Math.Abs(buffer1[l * dimension + l]);
                if (d < SMALLEST)
                {
                    l1 = l;
                    d = Math.Abs(buffer1[l1 * dimension + l]);

                    while (d < SMALLEST && (++l1 < dimension))
                        d = Math.Abs(buffer1[l1 * dimension + l]);

                    if (l1 >= dimension)
                        return false;

                    for (j = 0; j < dimension; j++)
                    {
                        buffer1[l * dimension + j] += buffer1[l1 * dimension + j];
                        buffer2[l * dimension + j] += buffer2[l1 * dimension + j];
                    }
                }
                c = 1.0 / (buffer1[l * dimension + l]);

                for (j = l; j < dimension; j++)
                    buffer1[l * dimension + j] *= c;

                for (j = 0; j < dimension; j++)
                    buffer2[l * dimension + j] *= c;

                k = l + 1;

                for (i = k; i < dimension; i++)
                {
                    c = buffer1[i * dimension + l];
                    for (j = l; j < dimension; j++)
                        buffer1[i * dimension + j] -= buffer1[l * dimension + j] * c;
                    for (j = 0; j < dimension; j++)
                        buffer2[i * dimension + j] -= buffer2[l * dimension + j] * c;
                }
            }

            for (l = dimension - 1; l >= 0; l--)
            {
                for (k = 1; k <= l; k++)
                {
                    i = l - k;
                    c = buffer1[i * dimension + l];
                    for (j = l; j < dimension; j++)
                        buffer1[i * dimension + j] -= buffer1[l * dimension + j] * c;

                    for (j = 0; j < dimension; j++)
                        buffer2[i * dimension + j] -= buffer2[l * dimension + j] * c;
                }
            }

            return true;
        }

        private MatrixPointResult GetMatrixPoint(ICogImage cogImage, VisionProPatternMatchingParam inspParam)
        {
            // 패턴 매칭
            if (cogImage == null)
                return null;

            VisionProPatternMatchingResult patternMatchResult = Algorithm.RunPatternMatch(cogImage, inspParam);
            MatrixPointResult matrixPointResult = new MatrixPointResult();

            matrixPointResult.PixelX = patternMatchResult.MaxMatchPos.FoundPos.X;
            matrixPointResult.PixelY = patternMatchResult.MaxMatchPos.FoundPos.Y;
            matrixPointResult.MotionX = GetCurrentX();
            matrixPointResult.MotionY = GetCurrentY();
            matrixPointResult.MotionT = GetCurrentT();
            matrixPointResult.Score = patternMatchResult.MaxMatchPos.Score;

            return matrixPointResult;
        }

        private void ClearMatrixPointResultList()
        {
            _matrixPointResultList.Clear();
        }

        private void AddMatrixPointResultList(MatrixPointResult matrixPointResult)
        {
            _matrixPointResultList.Add(matrixPointResult);
        }

        private List<MatrixPointResult> GetMatrixPointResultList()
        {
            return _matrixPointResultList;
        }
    }

    public enum CalibrationMode
    {
        XY,
        XYT,
    }

    public enum CalSeqStep
    {
        CAL_SEQ_IDLE,
        CAL_SEQ_INIT,
        CAL_SEQ_XY_INIT,
        CAL_SEQ_XY_MOVE,                            // +1 이동
        CAL_SEQ_XY_MOVE_COMPLETED,                  // +2 이동 확인
        CAL_SEQ_XY_GRAB,                            // +3 찍어
        CAL_SEQ_XY_CHECK_MATRIX,                    // +4 매트릭스 디멘젼 확인 후 반복 or 진행
        CAL_SEQ_XY_OFFSET,                          // +5 캘 시작점이 센터가 아닐 때, 옵셋 적용
        CAL_SEQ_XY_CALIBRATION,                     // +6 캘 계산
        CAL_SEQ_XY_MOVE_REF,                        // +7 시작점으로 복귀
        CAL_SEQ_XY_MOVE_REF_COMPLETED,             // +8 시작점 이동 확인
        CAL_SEQ_XY_REF_GRAB,                        // +9 시작점 찍어
        CAL_SEQ_XY_COMPLETED,                       // XY 종료

        CAL_SEQ_T_INIT,
        CAL_SEQ_T_MOVE,                             // +1 이동
        CAL_SEQ_T_MOVE_COMPLETED,                   // +2 이동 확인
        CAL_SEQ_T_COMPENSATION_BACKLASH,            // +3 백래쉬 보상 이동
        CAL_SEQ_T_COMPENSATION_BACKLASH_COMPLETED,  // +4 백래쉬 보상 이동 확인
        CAL_SEQ_T_GRAB,                             // +5 찍어
        CAL_SEQ_T_MOVE_X2,                            // +6 2배 이동
        CAL_SEQ_T_MOVE_X2_COMPLETED,                  // +7 2배 이동 확인
        CAL_SEQ_T_GRAB_X2,                            // +8 마지막 촬영
        CAL_SEQ_T_MOVE_OPPOSITE,                    // +9 반대로 이동
        CAL_SEQ_T_CALC_THETA,
        CAL_SEQ_T_MOVE_OPPOSITE_COMPLETED,          // +10 반대 이동 확인
        CAL_SEQ_T_COMPLETED,                        // T 종료

        CAL_SEQ_T_ADV_INIT,
        CAL_SEQ_T_ADV_GRAB,
        CAL_SEQ_T_ADV_MOVE,
        CAL_SEQ_T_ADV_MOVE_COMPLETED,
        CAL_SEQ_T_ADV_GRAB2,
        CAL_SEQ_T_ADV_MOVE2,
        CAL_SEQ_T_ADV_MOVE2_COMPLETED,
        CAL_SEQ_T_ADV_CALC,                         // 반복 9포인트 : CAL_SEQ_T_ADV_INIT 

        CAL_SEQ_ERROR,                              // 원점 복귀 할래?
        CAL_SEQ_ERROR_MOVE_REF,                           // OK : 원점 ㄱ
        CAL_SEQ_ERROR_MOVE_REF_TIMEOUT,                       // NO : 타임아웃 + PLC한테 통보
        CAL_SEQ_ERROR_SEND,

        CAL_SEQ_SAVE_RESULT_DATA,
        CAL_SEQ_SEND_RESULT_DATA,
        CAL_SEQ_COMPLETED,                          // CAL 종료

        CAL_SEQ_STOP,
    }
}
