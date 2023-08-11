using Cognex.VisionPro.Exceptions;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Xml.Linq;
using static Jastech.Framework.Device.Motions.AxisMovingParam;

namespace Jastech.Apps.Winform
{
    public class LAFManager
    {
        #region 필드
        private static LAFManager _instance = null;
        #endregion

        #region 속성
        public List<Task> StatusTaskList { get; private set; } = new List<Task>();

        public List<CancellationTokenSource> StatusCancelTaskList { get; private set; } = new List<CancellationTokenSource>();

        public string Command1 = "";

        public string Command2 = "";

        public string Command3 = "";
        #endregion

        #region 메서드
        public static LAFManager Instance()
        {
            if (_instance == null)
                _instance = new LAFManager();

            return _instance;
        }

        public void Initialize()
        {
            var lafCtrlHandler = DeviceManager.Instance().LAFCtrlHandler;

            if (lafCtrlHandler == null)
                return;

            foreach (var laf in lafCtrlHandler)
            {
                var nuriOne = laf as NuriOneLAFCtrl;
                laf.DataReceived += DataReceived;
                //nuriOne.GetLaserOnValue();

                CancellationTokenSource cancel = new CancellationTokenSource();
                Task statusTask = new Task(() => RequestStatusData(laf.Name, cancel), cancel.Token);

                statusTask.Start();

                StatusTaskList.Add(statusTask);
                StatusCancelTaskList.Add(cancel);
            }
        }

        public void Release()
        {
            foreach (var cancelTask in StatusCancelTaskList)
                cancelTask.Cancel();

            var lafCtrlHandler = DeviceManager.Instance().LAFCtrlHandler;

            if (lafCtrlHandler != null)
            {
                foreach (var laf in lafCtrlHandler)
                    laf.DataReceived -= DataReceived;
            }

            for (int i = 0; i < StatusTaskList.Count(); i++)
            {
                //StatusTaskList[i].Wait();
                StatusTaskList[i] = null;
            }
            StatusTaskList.Clear();
        }

        public void RequestStatusData(string name, CancellationTokenSource cancellationTokenSource)
        {
         
            int count = 0;
            while(true)
            {
                if (cancellationTokenSource.IsCancellationRequested)
                {
                    break;
                }

                if (DeviceManager.Instance().LAFCtrlHandler.Get(name) is NuriOneLAFCtrl laf)
                {
                    // cog, mpos, ls1, ls2 값 동시 요청
                    string command = "uc rep cog mpos ls1 ls2 mbusy";
                    laf.RequestData(command);
                }
                Thread.Sleep(150);
            }
        }

        public void SetCenterOfGravity(string name, int value)
        {
            if (DeviceManager.Instance().LAFCtrlHandler.Get(name) is LAFCtrl laf)
                laf.SetCenterOfGravity(value);
        }

        public void ServoOnOff(string name, bool isOn)
        {
            if (DeviceManager.Instance().LAFCtrlHandler.Get(name) is LAFCtrl laf)
                laf.SetMotionEnable(isOn);
        }

        public void LaserOnOff(string name, bool isOn)
        {
            if (DeviceManager.Instance().LAFCtrlHandler.Get(name) is LAFCtrl laf)
                laf.SetLaserOnOff(isOn);
        }

        public void TrackingOnOff(string name, bool isOn)
        {
            if (DeviceManager.Instance().LAFCtrlHandler.Get(name) is LAFCtrl laf)
                laf.SetTrackingOnOFF(isOn);
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
                {
                    //if (mposPulse > -50000) // 임시 -> 누리원한테 물어봐야함  정지 상태 일 때 Mpos가 4300 -> -53123 -> 4300 -> -53123
                        status.MPosPulse = mposPulse;
                }

                if (int.TryParse(GetValue(dataString, "ls1:"), out int ls1))
                    status.IsNegativeLimit = Convert.ToBoolean(ls1);
                if (int.TryParse(GetValue(dataString, "ls2:"), out int ls2))
                    status.IsPositiveLimit = Convert.ToBoolean(ls2);
                if (int.TryParse(GetValue(dataString, "mbusy:"), out int mbusy))
                    status.IsBusy = Convert.ToBoolean(mbusy);

                status.IsLaserOn = laf.IsLaserOn;
                status.IsTrackingOn = laf.IsTrackingOn;
            }
        }

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

                while(data.Contains("0x00000001"))
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

        public LAFCtrl GetLAFCtrl(string name)
        {
            var lafCtrlHandler = DeviceManager.Instance().LAFCtrlHandler;

            if (lafCtrlHandler == null)
                return null;

            return lafCtrlHandler.Where(x => x.Name == name).FirstOrDefault();
        }

        private HomeSequenceStep _homeSequenceStep = HomeSequenceStep.Stop;
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

        

        private bool _isHomeThreadStop = false;
        private Thread _homeThread = null;

        public void StartHomeThread(string lafName)
        {
            if (_homeThread != null)
                return;

            _isHomeThreadStop = false;
            _homeThread = new Thread(new ParameterizedThreadStart(HomeSequenceThread));
            _homeThread.Start(lafName);
        }

        private void HomeSequenceThread(object obj)
        {
            string lafName = (string)obj;
            _homeSequenceStep = HomeSequenceStep.Start;

            while (!_isHomeThreadStop)
            {
                HomeSequence(lafName);
                Thread.Sleep(50);
            }
            _homeThread = null;
        }

        private void PrepareHomeSequence(string lafName)
        {
            var lafCtrl = GetLAFCtrl(lafName);

            lafCtrl.SetMotionNegativeLimit(0);
            lafCtrl.SetMotionPositiveLimit(0);
            lafCtrl.SetMotionMaxSpeed(_scale);
            //lafCtrl.SetMotionZeroSet();
            Thread.Sleep(50);
        }

        const double HOMING_FIRST_TARGET = 999999.0;
        const int HOMING_TIME_OUT = 60000;
        const double HOMING_SEARCH_DISTANCE = 0.100;        // mm
        const double HOMING_SEARCH_VELOCITY = 1.0;
        const double HOMING_DISTANCE_AWAY_FROM_LIMIT = 0.5; // mm
        double target = 100;
        private double _scale = 1.0;

        private void HomeSequence(string lafName)
        {
            var lafCtrl = GetLAFCtrl(lafName);
            var status = lafCtrl.Status;

            Stopwatch sw = new Stopwatch();
            
            switch (_homeSequenceStep)
            {
                case HomeSequenceStep.Stop:
                   
                    break;

                case HomeSequenceStep.Start:
                    _scale = 1.0;
                    _homeSequenceStep = HomeSequenceStep.MoveFirstLimit;
                    break;
                case HomeSequenceStep.MoveFirstLimit:
                    PrepareHomeSequence(lafName);
                    Logger.Write(LogType.Device, "Prepare to laf home sequence.");

                    if (status.IsNegativeLimit == false)
                    {
                       
                        lafCtrl.SetMotionRelativeMove(Direction.CW, HOMING_FIRST_TARGET);      // -Limit 감지까지 이동
                        Logger.Write(LogType.Device, "Move to first minus limit detection.");
                        sw.Restart();
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
                    {
                        _homeSequenceStep = HomeSequenceStep.MoveLimit;
                        break;
                    }
                    break;
                case HomeSequenceStep.MoveLimit:
                    PrepareHomeSequence(lafName);
                    double target2 = target * _scale;
                    Console.WriteLine("target2 : " + target2 );
                    lafCtrl.SetMotionRelativeMove(Direction.CW, target2);      // -Limit 감지까지 이동
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
                   
                    bool isUnitOperation = ExecuteUnit(lafCtrl, HOMING_SEARCH_VELOCITY * _scale);
                    Logger.Write(LogType.Device, "Execute unit operation.");
                    if (isUnitOperation == true)
                        _homeSequenceStep = HomeSequenceStep.CheckZeroConvergence;
                    else
                        _homeSequenceStep = HomeSequenceStep.Error;
                    break;

                case HomeSequenceStep.CheckZeroConvergence:
                    //if (Math.Abs(status.MPosPulse - /*HOMING_DISTANCE_AWAY_FROM_LIMIT*/0.0002) <= 0.0002/*float.Epsilon*/)
                    double mPos = status.MPosPulse / lafCtrl.ResolutionAxisZ;
                    double calcMPos = mPos - lafCtrl.HomePosition_mm;
                    Console.WriteLine("mPos : " + mPos + "   calc : " + calcMPos);
                    if (mPos >=0 && mPos < lafCtrl.HomePosition_mm)
                    {
                        Logger.Write(LogType.Device, "Complete zero convergence.");
                        _homeSequenceStep = HomeSequenceStep.ZeroSet;
                    }
                    else
                    {
                        Logger.Write(LogType.Device, "Retry after scailing.");
                        Console.WriteLine("Retry after scailing.");
                        if (calcMPos > 3)
                        {
                            _scale *= 0.9;
                        }
                        else
                        {
                            _scale *= 0.5;
                        }
                        _homeSequenceStep = HomeSequenceStep.MoveLimit;
                    }
                    break;

                case HomeSequenceStep.ZeroSet:
                    //lafCtrl.SetMotionRelativeMove(Direction.CCW, HOMING_DISTANCE_AWAY_FROM_LIMIT);
                    //Thread.Sleep(1000);

                    lafCtrl.SetMotionStop();
                    Thread.Sleep(500);

                    lafCtrl.SetMotionZeroSet();
                    Thread.Sleep(100);

                    lafCtrl.SetMotionMaxSpeed(30);
                    Thread.Sleep(100);

                    Logger.Write(LogType.Device, "Complete zeroset.");

                    // 대기 위치로 이동 - 포지션 필요함
                    var unit = TeachingData.Instance().GetUnit(UnitName.Unit0.ToString());
                    var standbyPosition = unit.GetTeachingInfo(TeachingPosType.Stage1_Scan_Start).GetTargetPosition(lafCtrl.AxisName);
                    lafCtrl.SetMotionAbsoluteMove(standbyPosition);
                    Logger.Write(LogType.Device, "Move to home position.");
                    Thread.Sleep(3000);

                    //EnableSoftwareLimit(lafCtrl);
                    Console.WriteLine("tlqkf : " + lafCtrl.Status.MPosPulse);

                    Logger.Write(LogType.Device, "Complete LAF home.");
                    _homeSequenceStep = HomeSequenceStep.End;
                    break;

                case HomeSequenceStep.Error:
                    Logger.Write(LogType.Device, "Failed LAF home.");
                    _homeSequenceStep = HomeSequenceStep.End;
                    break;

                case HomeSequenceStep.End:
                    Logger.Write(LogType.Device, "End of LAF home sequence.");
                    _scale = 1.0;
                     
                    Console.WriteLine("tlqkf : " + lafCtrl.Status.MPosPulse);
                    _isHomeThreadStop = true;
                    _homeSequenceStep = HomeSequenceStep.Stop;
                    break;
               
                default:
                    break;
            }
        }

        private bool ExecuteUnit(LAFCtrl lafCtrl, double velocity)
        {
            if (velocity < 0.001)
                velocity = 0.001;
            var status = lafCtrl.Status;
            NuriOneLAFCtrl nuriOneLAFCtrl = lafCtrl as NuriOneLAFCtrl;
            Stopwatch sw = new Stopwatch();

            bool isComplete = false;
            bool result = false;
            UnitOperation step = UnitOperation.Init;
            while (isComplete == false)
            {
                switch (step)
                {
                    case UnitOperation.Init:// Litmit 된 상태에서 여기로 들어옴
                        lafCtrl.SetMotionMaxSpeed(velocity);
                        Thread.Sleep(100);
                        lafCtrl.SetMotionStop();
                        Thread.Sleep(100);
                        lafCtrl.SetMotionZeroSet();
                        Thread.Sleep(500);
                        step = UnitOperation.MoveToPositive;

                        break;
                    case UnitOperation.MoveToPositive:
                        lafCtrl.SetMotionRelativeMove(Direction.CCW, _scale / 10.0);

                        Thread.Sleep(200);
                        sw.Restart();
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
                            lafCtrl.SetMotionStop();
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
                        lafCtrl.SetMotionStop();
                        lafCtrl.SetDefaultParameter();
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

        public void EnableSoftwareLimit(LAFCtrl lafCtrl)
        {
            var negativeLimit = MotionManager.Instance().GetAxis(AxisHandlerName.Handler0, lafCtrl.AxisName).AxisCommonParams.NegativeLimit;
            var positiveLimit = MotionManager.Instance().GetAxis(AxisHandlerName.Handler0, lafCtrl.AxisName).AxisCommonParams.PositiveLimit;

            lafCtrl?.SetMotionNegativeLimit(negativeLimit);
            lafCtrl?.SetMotionPositiveLimit(positiveLimit);
        }

        public void DisableSoftwareLimit(LAFCtrl lafCtrl)
        {
            lafCtrl?.SetMotionNegativeLimit(0);
            lafCtrl?.SetMotionNegativeLimit(0);
        }
        #endregion
    }
}
