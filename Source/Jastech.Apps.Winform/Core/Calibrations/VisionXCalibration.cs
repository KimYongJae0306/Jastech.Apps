using Cognex.VisionPro;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using Jastech.Framework.Winform;
using Jastech.Framework.Winform.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        #endregion

        #region 속성
        private AxisHandler AxisHandler { get; set; } = null;

        private AxisMovingParam AxisMovingParam { get; set; } = null;

        private CalSeqStep CalSeqStep { get; set; } = CalSeqStep.CAL_SEQ_INIT;

        private bool SeqStopFlag = true;

        private AreaCamera AreaCamera { get; set; } = null;

        private Task CalSeqTask { get; set; }

        private CancellationTokenSource SeqTaskCancellationTokenSource { get; set; }

        private VisionProPatternMatchingParam VisionProPatternMatchingParam { get; set; } = null;

        private AlgorithmTool Algorithm = new AlgorithmTool();

        private CalibrationMode CalibrationMode { get; set; } = CalibrationMode.XY;
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
            //VisionProPatternMatchingParam = new VisionProPatternMatchingParam();
            VisionProPatternMatchingParam = param.DeepCopy();
        }

        public void SetCalibrationMode(CalibrationMode calibrationMode)
        {
            CalibrationMode = calibrationMode;
        }
        #endregion



        public void CalSeqRun()
        {
            if (CalSeqTask != null)
            {
                CalSeqStep = CalSeqStep.CAL_SEQ_INIT;
                return;
            }

            SeqTaskCancellationTokenSource = new CancellationTokenSource();
            CalSeqTask = new Task(CalibrationTaskAction, SeqTaskCancellationTokenSource.Token);
            CalSeqTask.Start();
        }

        public void CalSeqStop()
        {
            SeqTaskCancellationTokenSource.Cancel();
            CalSeqTask.Wait();
            CalSeqTask = null;
        }

        private void CalibrationTaskAction()
        {
            CalSeqStep = CalSeqStep.CAL_SEQ_INIT;

            SeqStopFlag = false;

            while (true)
            {
                if (SeqStopFlag)
                {
                    CalSeqStep = CalSeqStep.CAL_SEQ_STOP;
                    break;
                }

                CalibrationSequence();
                Thread.Sleep(50);
            }
        }

        private void CalibrationSequence()
        {
            double dX = 0.0;
            double dY = 0.0;
            double dT = 0.0;


            double intervalX = 0.0;
            double intervalY = 0.0;
            double intervalT = 0.0;

            int matrixStepPoint = 0;

            Stopwatch sw = new Stopwatch();

            var display = TeachingUIManager.Instance().GetDisplay();
            ICogImage cogImage = display.GetImage();
            VisionProPatternMatchingParam inspParam = VisionProPatternMatchingParam;
            ICogImage copyCogImage = cogImage.CopyBase(CogImageCopyModeConstants.CopyPixels);

            List<Result> calibrationResultList = new List<Result>();

            switch (CalSeqStep)
            {
                case CalSeqStep.CAL_SEQ_INIT:
                    PlcControlManager.Instance().ClearAddress(Service.Plc.Maps.PlcCommonMap.PLC_Command);

                    InitailizeOpticSetting();
                    
                    double initX = AxisHandler.GetAxis(AxisName.X).GetActualPosition() / 1000.0;
                    double initY = PlcControlManager.Instance().GetReadPosition(AxisName.Y) / 1000.0;
                    double initT = PlcControlManager.Instance().GetReadPosition(AxisName.T) / 1000.0;

                    SetInitPoistion(initX, initY, initT);

                    CalSeqStep = CalSeqStep.CAL_SEQ_XY_INIT;
                    break;

                #region XY
                case CalSeqStep.CAL_SEQ_XY_INIT:
                    if (intervalX == 0.0 && intervalY == 0.0)
                    {
                        CalSeqStep = CalSeqStep.CAL_SEQ_COMPLETED;
                        break;
                    }

                    InitializeMoveAmount(intervalX, intervalY);
                    InitializeMotionPosition(intervalX, intervalY);

                    matrixStepPoint = 0;
                    CalSeqStep = CalSeqStep.CAL_SEQ_XY_MOVE;
                    break;

                case CalSeqStep.CAL_SEQ_XY_MOVE:
                    ReqestMove(_absoluteAmountX[matrixStepPoint], _absoluteAmountY[matrixStepPoint], 0);

                    sw.Restart();
                    CalSeqStep = CalSeqStep.CAL_SEQ_XY_MOVE_COMPLETED;
                    break;

                case CalSeqStep.CAL_SEQ_XY_MOVE_COMPLETED:
                    if (sw.ElapsedMilliseconds > CAL_TIME_OUT)
                    {
                        CalSeqStep = CalSeqStep.CAL_SEQ_ERROR;
                        break;
                    }

                    if (RequestMoveDone() == false)
                        break;
                    else
                    {
                        //RequestMoveOff();

                        if (CheckPosition() == false)
                        {
                            CalSeqStep = CalSeqStep.CAL_SEQ_ERROR;
                            break;
                        }
                    }

                    CalSeqStep = CalSeqStep.CAL_SEQ_XY_GRAB;
                    break;

                case CalSeqStep.CAL_SEQ_XY_GRAB:
                    Thread.Sleep(50);

                    // 그랩

                    // 캘 스탭 포지션 셋
                    SetCalStepPosition(matrixStepPoint, GetCurrentX(), GetCurrentY(), GetCurrentT());

                    // 패턴 매칭
                    VisionProPatternMatchingResult patternMatchResult = Algorithm.RunPatternMatch(copyCogImage, inspParam);
                    Result calibrationResult = new Result();

                    calibrationResult.PixelCoordinates = patternMatchResult.MaxMatchPos.FoundPos;
                    calibrationResult.MotionX = GetCalStepPositionX(matrixStepPoint);
                    calibrationResult.MotionY = GetCalStepPositionY(matrixStepPoint);
                    calibrationResult.MotionT = GetCalStepPositionT(matrixStepPoint);
                    calibrationResult.Score = patternMatchResult.MaxMatchPos.Score;

                    calibrationResultList.Add(calibrationResult);

                    CalSeqStep = CalSeqStep.CAL_SEQ_XY_CHECK_MATRIX;
                    break;

                case CalSeqStep.CAL_SEQ_XY_CHECK_MATRIX:
                    if (matrixStepPoint < MATRIX_DIMENSION)
                    {
                        matrixStepPoint++;
                        CalSeqStep = CalSeqStep.CAL_SEQ_XY_MOVE;
                    }
                    else
                        CalSeqStep = CalSeqStep.CAL_SEQ_XY_OFFSET;
                    break;

                case CalSeqStep.CAL_SEQ_XY_OFFSET:
                    break;

                case CalSeqStep.CAL_SEQ_XY_CALIBRATION:
                    break;

                case CalSeqStep.CAL_SEQ_XY_MOVE_REF:
                    break;

                case CalSeqStep.CAL_SEQ_XY_MOVE_REF_PCOMPLETED:
                    break;

                case CalSeqStep.CAL_SEQ_XY_REF_GRAB:
                    break;

                case CalSeqStep.CAL_SEQ_XY_COMPLETED:

                    if (CalibrationMode == CalibrationMode.XY)
                        CalSeqStep = CalSeqStep.CAL_SEQ_COMPLETED;
                    else
                        CalSeqStep = CalSeqStep.CAL_SEQ_T_INIT;
                    break;
                #endregion

                #region T
                case CalSeqStep.CAL_SEQ_T_INIT:
                    break;

                case CalSeqStep.CAL_SEQ_T_MOVE:
                    break;

                case CalSeqStep.CAL_SEQ_T_MOVE_COMPLETED:
                    break;

                case CalSeqStep.CAL_SEQ_T_COMPENSATION_BACKLASH:
                    break;

                case CalSeqStep.CAL_SEQ_T_COMPENSATION_BACKLASH_COMPLETED:
                    break;

                case CalSeqStep.CAL_SEQ_T_GRAB:
                    break;

                case CalSeqStep.CAL_SEQ_T_MOVE2:
                    break;

                case CalSeqStep.CAL_SEQ_T_MOVE2_COMPLETED:
                    break;

                case CalSeqStep.CAL_SEQ_T_GRAB2:
                    break;

                case CalSeqStep.CAL_SEQ_T_MOVE_OPPOSITE:
                    break;

                case CalSeqStep.CAL_SEQ_T_MOVE_OPPOSITE_COMPLETED:
                    break;

                case CalSeqStep.CAL_SEQ_T_COMPLETED:
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
                    break;

                case CalSeqStep.CAL_SEQ_MOVE_REF:
                    break;

                case CalSeqStep.CAL_SEQ_MOVE_TIMEOUT:
                    break;

                case CalSeqStep.CAL_SEQ_COMPLETED:
                    break;

                case CalSeqStep.CAL_SEQ_STOP:
                    SeqStopFlag = true;
                    break;

                default:
                    break;
            }
        }

        private void InitailizeOpticSetting()
        {
            AreaCamera.Camera.SetExposureTime(0);
            AreaCamera.Camera.SetAnalogGain(0);
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
            return AxisHandler.GetAxis(AxisName.X).GetActualPosition() / 1000.0;
        }

        private double GetCurrentY()
        {
            return PlcControlManager.Instance().GetReadPosition(AxisName.Y) / 1000.0;
        }

        private double GetCurrentT()
        {
            return PlcControlManager.Instance().GetReadPosition(AxisName.T) / 1000.0;
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

        private void ReqestMove(double amountX, double amountY, double amountT)
        {
            SetCurrentPosition();

            // Move X (PC)
            AxisHandler.AxisList[(int)AxisName.X].StartRelativeMove(amountX, AxisMovingParam);

            // Move Y (PLC)
            PlcControlManager.Instance().WriteAlignData(0, amountY, amountT);     // Relative Move
            PlcControlManager.Instance().WriteMoveRequest();

            SetMoveToPosition(amountX, amountY, amountT);
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
            var doneX = AxisHandler.AxisList[(int)AxisName.X].GetCurrentMotionStatus() == "STOP" ? true : false;
            var doneY = PlcControlManager.Instance().GetAddressMap(Service.Plc.Maps.PlcCommonMap.PLC_Move_END).Value == "5" ? true : false;

            return doneX && doneY;
        }

        private bool CheckPosition()
        {
            return IsMoveCompletedX() || IsMoveCompletedY() || IsMoveCompletedT();
        }

        private bool IsMoveCompletedX()
        {
            return Math.Abs(_currPositionX - _moveToPositionX) <= double.Epsilon;
        }

        private bool IsMoveCompletedY()
        {
            return Math.Abs(_currPositionY - _moveToPositionY) <= double.Epsilon;
        }

        private bool IsMoveCompletedT()
        {
            return Math.Abs(_currPositionT - _moveToPositionT) <= double.Epsilon;
        }

        private void SetCalStepPosition(int matrixPoint, double x, double y, double t)
        {
            _calStepPositionX[matrixPoint] = x;
            _calStepPositionY[matrixPoint] = y;
            _calStepPositionT[matrixPoint] = t;
        }

        private double GetCalStepPositionX(int matrixPoint)
        {
            return _calStepPositionX[matrixPoint];
        }

        private double GetCalStepPositionY(int matrixPoint)
        {
            return _calStepPositionY[matrixPoint];
        }

        private double GetCalStepPositionT(int matrixPoint)
        {
            return _calStepPositionT[matrixPoint];
        }
    }

    public enum CalibrationMode
    {
        XY,
        XYT,
    }

    public enum CalSeqStep
    {
        CAL_SEQ_INIT,
        CAL_SEQ_XY_INIT,
        CAL_SEQ_XY_MOVE,                            // +1 이동
        CAL_SEQ_XY_MOVE_COMPLETED,                  // +2 이동 확인
        CAL_SEQ_XY_GRAB,                            // +3 찍어
        CAL_SEQ_XY_CHECK_MATRIX,                    // +4 매트릭스 디멘젼 확인 후 반복 or 진행
        CAL_SEQ_XY_OFFSET,                          // +5 캘 시작점이 센터가 아닐 때, 옵셋 적용
        CAL_SEQ_XY_CALIBRATION,                     // +6 캘 계산
        CAL_SEQ_XY_MOVE_REF,                        // +7 시작점으로 복귀
        CAL_SEQ_XY_MOVE_REF_PCOMPLETED,             // +8 시작점 이동 확인
        CAL_SEQ_XY_REF_GRAB,                        // +9 시작점 찍어
        CAL_SEQ_XY_COMPLETED,                       // XY 종료

        CAL_SEQ_T_INIT,
        CAL_SEQ_T_MOVE,                             // +1 이동
        CAL_SEQ_T_MOVE_COMPLETED,                   // +2 이동 확인
        CAL_SEQ_T_COMPENSATION_BACKLASH,            // +3 백래쉬 보상 이동
        CAL_SEQ_T_COMPENSATION_BACKLASH_COMPLETED,  // +4 백래쉬 보상 이동 확인
        CAL_SEQ_T_GRAB,                             // +5 찍어
        CAL_SEQ_T_MOVE2,                            // +6 2배 이동
        CAL_SEQ_T_MOVE2_COMPLETED,                  // +7 2배 이동 확인
        CAL_SEQ_T_GRAB2,                            // +8 마지막 촬영
        CAL_SEQ_T_MOVE_OPPOSITE,                    // +9 반대로 이동
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
        CAL_SEQ_MOVE_REF,                           // OK : 원점 ㄱ
        CAL_SEQ_MOVE_TIMEOUT,                       // NO : 타임아웃 + PLC한테 통보

        CAL_SEQ_COMPLETED,                          // CAL 종료

        CAL_SEQ_STOP,
    }

    public class Result
    {
        //public double Px { get; set; } = 0.0;

        //public double Py { get; set; } = 0.0;

        public PointF PixelCoordinates { get; set; } = new PointF();

        public double MotionX { get; set; } = 0.0;

        public double MotionY { get; set; } = 0.0;

        public double MotionT { get; set; } = 0.0;

        public double Score { get; set; } = 0.0;
    }
}
