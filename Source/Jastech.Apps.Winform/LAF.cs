using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static Jastech.Framework.Device.Motions.AxisMovingParam;

namespace Jastech.Apps.Winform
{
    public partial class LAF
    {
        #region 필드
        private HomeSequenceStep _homeSequenceStep = HomeSequenceStep.Stop;

        private bool _isHomeThreadStop = false;

        private Thread _homeThread = null;

        private double _scale = 1.0;

        private double _target = 100;

        private double _standbyPosition = 0.0;
        #endregion

        #region 속성
        public LAFCtrl LafCtrl { get; set; } = null;

        private Task statusTask { get; set; } = null;

        private CancellationTokenSource cancelTokenSource { get; set; }
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public LAF(LAFCtrl lafCtrl)
        {
            LafCtrl = lafCtrl;
        }
        #endregion

        #region 메서드
        public void Initialize()
        {
            if (LafCtrl != null)
            {
                LafCtrl.DataReceived += DataReceived;

                cancelTokenSource = new CancellationTokenSource();
                statusTask = new Task(() => RequestStatusData(cancelTokenSource), cancelTokenSource.Token);
                statusTask.Start();
            }
        }

        public void Release()
        {
            cancelTokenSource.Cancel();

            if (LafCtrl != null)
            {
                LafCtrl.DataReceived -= DataReceived;
            }
        }

        public void RequestStatusData(CancellationTokenSource cancellationTokenSource)
        {

            while (true)
            {
                if (cancellationTokenSource.IsCancellationRequested)
                    break;

                if (LafCtrl is NuriOneLAFCtrl laf)
                {
                    // cog, mpos, ls1, ls2 값 동시 요청
                    string command = "uc rep cog mpos ls1 ls2 mbusy";
                    laf.RequestData(command);
                }
                Thread.Sleep(150);
            }
        }

        public void DataReceived(string name, byte[] data)
        {
            if (DeviceManager.Instance().LAFCtrlHandler.Get(name) is NuriOneLAFCtrl laf)
            {
                var status = laf.Status;
                string dataString = Encoding.Default.GetString(data);

                if (int.TryParse(GetValue(dataString, "cog:"), out int cog))
                    status.CenterofGravity = cog;
                if (double.TryParse(GetValue(dataString, "mpos:"), out double mposPulse))
                    status.MPosPulse = mposPulse;

                if (int.TryParse(GetValue(dataString, "ls1:"), out int ls1))
                {
                    status.IsNegativeLimit = Convert.ToBoolean(ls1);

                    if (status.IsNegativeLimit)
                    {
                        Logger.Debug(LogType.Device, $"Detected -Limit / Deivce name : {name} / MPos : {status.MPosPulse}");
                        Logger.Debug(LogType.Device, $"Raw Packet : {dataString}");
                    }
                }

                if (int.TryParse(GetValue(dataString, "ls2:"), out int ls2))
                    status.IsPositiveLimit = Convert.ToBoolean(ls2);
                if (int.TryParse(GetValue(dataString, "mbusy:"), out int mbusy))
                    status.IsBusy = Convert.ToBoolean(mbusy);

                status.IsLaserOn = laf.IsLaserOn;
                status.IsTrackingOn = laf.IsTrackingOn;
            }
        }

        public void SetCenterOfGravity(int value)
        {
            LafCtrl?.SetCenterOfGravity(value);
        }

        public void ServoOnOff(bool isOn)
        {
            LafCtrl?.SetMotionEnable(isOn);
        }

        public void LaserOnOff(bool isOn)
        {
            LafCtrl?.SetLaserOnOff(isOn);
        }

        public void TrackingOnOff(bool isOn)
        {
            LafCtrl?.SetTrackingOnOFF(isOn);
        }

        public bool StartHomeThread(double standbyPosition)
        {
            if (_homeThread != null)
                return false;

            _isHomeThreadStop = false;

            _standbyPosition = standbyPosition;

            _homeThread = new Thread(HomeSequenceThread);
            _homeThread.Start();

            return true;
        }

        private void HomeSequenceThread()
        {
            _homeSequenceStep = HomeSequenceStep.Start;

            while (_isHomeThreadStop == false)
            {
                HomeSequence();
                Thread.Sleep(50);
            }
            _homeThread = null;
        }

        private bool _isHomeActionStop = false;
        public bool HomeSequenceAction()
        {
            _isHomeActionStop = false;
            _homeSequenceStep = HomeSequenceStep.Start;

            while (_isHomeActionStop == false)
            {
                HomeSequence();
                Thread.Sleep(50);
                if (_homeSequenceStep == HomeSequenceStep.Error)
                    return false;
            }

            bool isHomeComplete = _homeSequenceStep == HomeSequenceStep.Stop;

            _homeSequenceStep = HomeSequenceStep.Stop;
            return isHomeComplete;
        }

        public void StopHomeSequence()
        {
            _isHomeActionStop = true;
            LafCtrl.SetMotionStop();
        }

        Stopwatch sw = null;
        private void HomeSequence()
        {
            if (LafCtrl == null)
                return;

            var status = LafCtrl.Status;

            if (sw == null)
                sw = new Stopwatch();

            switch (_homeSequenceStep)
            {
                case HomeSequenceStep.Stop:
                    break;

                case HomeSequenceStep.Start:
                    sw?.Restart();
                    _scale = 1.0;
                    _homeSequenceStep = HomeSequenceStep.MoveFirstLimit;
                    break;

                case HomeSequenceStep.MoveFirstLimit:
                    PrepareHomeSequence(isFirst: true);
                    Logger.Write(LogType.Device, "Prepare to laf home sequence.");

                    if (status.IsNegativeLimit == false)
                    {
                        LafCtrl.SetMotionRelativeMove(Direction.CW, HOMING_FIRST_TARGET);      // -Limit 감지까지 이동
                        Logger.Write(LogType.Device, "Move to first minus limit detection.");
                        _homeSequenceStep = HomeSequenceStep.CheckFirstLimit;
                    }
                    else
                    {
                        _homeSequenceStep = HomeSequenceStep.UnitOperation;
                    }

                    break;
                case HomeSequenceStep.CheckFirstLimit:
                    if (sw.ElapsedMilliseconds > HOMING_TIME_OUT)
                    {
                        Logger.Write(LogType.Device, "Time over check first limit.");
                        _homeSequenceStep = HomeSequenceStep.Error;
                        break;
                    }

                    if (status.IsNegativeLimit == false)
                        _homeSequenceStep = HomeSequenceStep.MoveLimit;

                    break;
                case HomeSequenceStep.MoveLimit:
                    PrepareHomeSequence();
                    double target2 = _target * _scale;
                    Console.WriteLine("target2 : " + target2);
                    LafCtrl.SetMotionRelativeMove(Direction.CW, target2);      // -Limit 감지까지 이동
                    _homeSequenceStep = HomeSequenceStep.CheckLimit;
                    break;
                case HomeSequenceStep.CheckLimit:

                    if (sw.ElapsedMilliseconds > HOMING_TIME_OUT)
                    {
                        Logger.Write(LogType.Device, "Time over check limit.");
                        _homeSequenceStep = HomeSequenceStep.Error;
                        break;
                    }

                    if (status.IsNegativeLimit == false)
                        break;

                    sw.Stop();
                    _homeSequenceStep = HomeSequenceStep.UnitOperation;
                    break;

                case HomeSequenceStep.UnitOperation:

                    bool isUnitOperation = ExecuteUnit(HOMING_SEARCH_VELOCITY * _scale);
                    Logger.Write(LogType.Device, "Execute unit operation.");
                    if (isUnitOperation == true)
                        _homeSequenceStep = HomeSequenceStep.CheckZeroConvergence;
                    else
                        _homeSequenceStep = HomeSequenceStep.Error;
                    break;

                case HomeSequenceStep.CheckZeroConvergence:
                    
                    double mPos = status.MPosPulse / LafCtrl.ResolutionAxisZ;
                    double calcMPos = mPos - LafCtrl.HomePosition_mm;
                    double vel = HOMING_SEARCH_VELOCITY * _scale;
                    Console.WriteLine("mPos : " + mPos + "   calc :  " + calcMPos + " Ve; : " + vel);

                    if (mPos >= 0 && mPos < LafCtrl.HomePosition_mm && vel < 0.01)
                    {
                        Logger.Write(LogType.Device, "Complete zero convergence.");
                        _homeSequenceStep = HomeSequenceStep.ZeroSet;
                    }
                    else
                    {
                        Logger.Write(LogType.Device, "Retry after scailing.");
                        Console.WriteLine("Retry after scailing.");

                        if (calcMPos > 2)
                            _scale *= 0.7;
                        else
                            _scale *= 0.3;

                        _homeSequenceStep = HomeSequenceStep.MoveLimit;
                    }
                    break;

                case HomeSequenceStep.ZeroSet:

                    LafCtrl.SetMotionStop();
                    Thread.Sleep(100);

                    LafCtrl.SetMotionZeroSet();
                    Thread.Sleep(100);

                    LafCtrl.SetMotionMaxSpeed(LafCtrl.MaxSppedAxisZ);
                    Thread.Sleep(100);

                    Logger.Write(LogType.Device, "Complete zeroset.");

                    // 대기 위치로 이동 - 포지션 필요함
                    //var unit = TeachingData.Instance().GetUnit(UnitName.Unit0.ToString());
                    //double standbyPosition = 0.0;
                    //if (unit != null)
                    //{
                    //    standbyPosition = unit.GetTeachingInfo(TeachingPosType.Stage1_Scan_Start).GetTargetPosition(LafCtrl.AxisName);
                    //    string tt = string.Format("LAF Axis Name : {0}", LafCtrl.AxisName.ToString());
                    //}
                    //else
                    //{
                    //    Logger.Write(LogType.Device, "Failed Move To Target Position [Unit is null].");
                    //}

                    LafCtrl.SetMotionAbsoluteMove(_standbyPosition);
                    Logger.Write(LogType.Device, $"Move to home position.[Name : {LafCtrl.Name}]");
                    Thread.Sleep(2000);

                    Console.WriteLine("Completed Homming : " + LafCtrl.Status.MPosPulse);

                    Logger.Write(LogType.Device, $"Complete LAF home.[Name : {LafCtrl.Name}]");
                    _homeSequenceStep = HomeSequenceStep.End;
                    break;

                case HomeSequenceStep.Error:
                    Logger.Write(LogType.Device, $"Failed LAF home.[Name : {LafCtrl.Name}]");
                    _homeSequenceStep = HomeSequenceStep.End;
                    break;

                case HomeSequenceStep.End:
                    Logger.Write(LogType.Device, $"End of LAF home sequence.[Name : {LafCtrl.Name}]");
                    _scale = 1.0;

                    _isHomeThreadStop = true;
                    _isHomeActionStop = true;
                    LafCtrl?.SetDefaultParameter();
                    LafCtrl.Status.NeedHomming = false;
                    _homeSequenceStep = HomeSequenceStep.Stop;
                    break;

                default:
                    break;
            }
        }

        private bool ExecuteUnit(double velocity)
        {
            if (LafCtrl == null)
                return true;

            if (velocity < 0.001)
                velocity = 0.001;

            var status = LafCtrl.Status;
            Stopwatch sw = new Stopwatch();

            bool isComplete = false;
            bool result = false;
            UnitOperation step = UnitOperation.Init;
            while (isComplete == false)
            {
                switch (step)
                {
                    case UnitOperation.Init:// Litmit 된 상태에서 여기로 들어옴
                        LafCtrl.SetMotionMaxSpeed(velocity);
                        Thread.Sleep(100);
                        LafCtrl.SetMotionStop();
                        Thread.Sleep(100);
                        LafCtrl.SetMotionZeroSet();
                        Thread.Sleep(500);
                        step = UnitOperation.MoveToPositive;
                        break;
						
                    case UnitOperation.MoveToPositive:
                        LafCtrl.SetMotionRelativeMove(Direction.CCW, _scale / 10.0);

                        Thread.Sleep(200);
                        //sw.Restart();
                        step = UnitOperation.ReleaseNegativeLimitDetection;

                        break;

                    case UnitOperation.ReleaseNegativeLimitDetection:
                        if (sw.ElapsedMilliseconds > HOMING_TIME_OUT)
                        {
                            Logger.Write(LogType.Device, "Failed to move time out.");
                            step = UnitOperation.Error;
                            break;
                        }

                        if (status.IsNegativeLimit == true)                                    // -Limit 감지 해제
                        {
                            step = UnitOperation.MoveToPositive;
                        }
                        else
                        {
                            LafCtrl.SetMotionStop();
                            Thread.Sleep(500);
                            step = UnitOperation.Complete;
                        }
                        break;

                    case UnitOperation.Complete:
                        isComplete = true;
                        Logger.Write(LogType.Device, "Completed to unit operation.");
                        result = true;
                        break;

                    case UnitOperation.Error:
                        Logger.Write(LogType.Device, "Failed to unit operation.");
                        LafCtrl.SetMotionStop();
                        LafCtrl.SetDefaultParameter();
                        Logger.Write(LogType.Device, "Stop.");
                        isComplete = true;
                        result = false;
                        break;

                    default:
                        break;
                }
            }

            return result;
        }

        private void PrepareHomeSequence(bool isFirst = false)
        {
            LafCtrl?.SetMotionNegativeLimit(0);
            LafCtrl?.SetMotionPositiveLimit(0);

            if (isFirst)
                LafCtrl?.SetMotionMaxSpeed(10);
            else
                LafCtrl?.SetMotionMaxSpeed(_scale);
            //lafCtrl.SetMotionZeroSet();
            Thread.Sleep(50);
        }

        public void SetHomeStandbyPosition(double standbyPos)
        {
            _standbyPosition = standbyPos;
        }
        #endregion
    }

    public partial class LAF
    {
        #region const
        const double HOMING_FIRST_TARGET = 999999.0;
        const int HOMING_TIME_OUT = 60000;
        const double HOMING_SEARCH_DISTANCE = 0.100;        // mm
        const double HOMING_SEARCH_VELOCITY = 1.0;
        const double HOMING_DISTANCE_AWAY_FROM_LIMIT = 0.5; // mm
        #endregion

        #region enum
        private enum HomeSequenceStep
        {
            Stop,
            Start,
            MoveFirstLimit,
            CheckFirstLimit,
            MoveLimit,
            CheckLimit,

            UnitOperation,
            CheckZeroConvergence,
            ZeroSet,
            Error,

            End,
        }

        private enum UnitOperation
        {
            Init,
            MoveToNegativeLimit,
            WaitingForLimitDetection,
            MoveToPositive,
            ReleaseNegativeLimitDetection,
            Complete,
            Error,
        }
        #endregion

        #region 메서드
        private string GetValue(string data, string dataName)
        {
            if (data.Contains(dataName))
            {
                while (data.Contains("\n4"))
                    data = data.Replace("\n4", "");

                while (data.Contains("\n"))
                    data = data.Replace("\n", "");

                while (data.Contains("\r"))
                    data = data.Replace("\r", "");

                while (data.Contains("0x00000001"))
                    data = data.Replace("0x00000001", "");
                //int startIndex = data.IndexOf(dataName) + dataName.Length + 1; // +1 => ':' 길이 계산 값
                int startIndex = data.IndexOf(dataName) + dataName.Length; // +1 => ':' 길이 계산 값
                string content = data.Substring(startIndex);

                int nextAlphabetIndex = -1;
                for (int i = 0; i < content.Length; i++)
                {
                    char value = content[i];
                    nextAlphabetIndex = i;
                    if (Regex.IsMatch(content[i].ToString(), "^[a-zA-Z]"))
                        break;
                    if (Regex.IsMatch(content[i].ToString(), "\r"))
                        break;
                    if (Regex.IsMatch(content[i].ToString(), "\n"))
                        break;
                }
                if (content.Length > 0)
                {
                    string temp = content.Substring(0, nextAlphabetIndex);
                    string value = content.Substring(0, nextAlphabetIndex).Replace(" ", "");
                    return value;
                }

                else
                    return "";
            }
            return "";
        }

        private string SplitData(string content)
        {
            int nextAlphabetIndex = -1;
            for (int i = 0; i < content.Length; i++)
            {
                char value = content[i];
                nextAlphabetIndex = i;
                if (Regex.IsMatch(content[i].ToString(), "^[a-zA-Z]"))
                    break;
                if (Regex.IsMatch(content[i].ToString(), "\r"))
                    break;
                if (Regex.IsMatch(content[i].ToString(), "\n"))
                    break;
            }
            if (content.Length > 0)
            {
                string temp = content.Substring(0, nextAlphabetIndex);
                string value = content.Substring(0, nextAlphabetIndex).Replace(" ", "");
                return value;
            }

            else
                return "";
        }

        private string ConvertData(string data)
        {
            string converData = data;
            while (converData.Contains("\n4"))
                converData = converData.Replace("\n4", "");

            while (converData.Contains("\n"))
                converData = converData.Replace("\n", "");

            while (converData.Contains("\r"))
                converData = converData.Replace("\r", "");

            while (converData.Contains("0x00000001"))
                converData = converData.Replace("0x00000001", "");

            while (converData.Contains("0x00000000"))
                converData = converData.Replace("0x00000000", "");


            return converData;
        }
        #endregion
    }
}
