using Jastech.Apps.Structure.Data;
using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
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
        #endregion

        #region 메서드
        public static LAFManager Instance()
        {
            if (_instance == null)
            {
                _instance = new LAFManager();
            }

            return _instance;
        }

        public void Initialize()
        {
            var lafCtrlHandler = DeviceManager.Instance().LAFCtrlHandler;

            if (lafCtrlHandler == null)
                return;

            foreach (var laf in lafCtrlHandler)
            {
                laf.DataReceived += DataReceived;

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
            {
                cancelTask.Cancel();
            }

            var lafCtrlHandler = DeviceManager.Instance().LAFCtrlHandler;

            if (lafCtrlHandler != null)
            {
                foreach (var laf in lafCtrlHandler)
                    laf.DataReceived -= DataReceived;
            }

            for (int i = 0; i < StatusTaskList.Count(); i++)
            {
                StatusTaskList[i].Wait();
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
                    if (count % 3 == 0)
                    { // cog, mpos, ls1, ls2 값 동시 요청
                        string command = "uc rep cog mpos ls1 ls2";
                        laf.RequestData(command);
                    }
                    else if (count % 3 == 1)
                    {
                        string command = "uc lasergate";
                        laf.RequestData(command);
                    }
                    else if (count % 3 == 2)
                    {
                        string command = "uc motiontrack";
                        laf.RequestData(command);
                    }
                    if (count >= int.MaxValue)
                        count = 0;
                    count++;
                }
                Thread.Sleep(300);
            }
        }

        public void SetCenterOfGravity(string name, int value)
        {
            if (DeviceManager.Instance().LAFCtrlHandler.Get(name) is NuriOneLAFCtrl laf)
                laf.SetCenterOfGravity(value);
        }

        public void AutoFocusOnOff(string name, bool isOn)
        {
            if (DeviceManager.Instance().LAFCtrlHandler.Get(name) is NuriOneLAFCtrl laf)
                laf.SetAutoFocusOnOFF(isOn);
        }

        public void DataReceived(string name, byte[] data)
        {
            if (DeviceManager.Instance().LAFCtrlHandler.Get(name) is NuriOneLAFCtrl laf)
            {
                string dataString = Encoding.Default.GetString(data);

                //int centerofGravity = -1;
                //double mPosPulse = -1;
                //bool isNegativeLimit = false;
                //bool isPositiveLimit = false;
                // ex : "4\rcog: -2139 mpos: +98050 ls1: 0 ls2: 0 "
                var status = laf.Status;
                if (int.TryParse(GetValue(dataString, "cog"), out int cog))
                    status.CenterofGravity = cog;
                if (double.TryParse(GetValue(dataString, "mpos"), out double mposPulse))
                    status.MPosPulse = mposPulse;
                if (int.TryParse(GetValue(dataString, "ls1"), out int ls1))
                    status.IsNegativeLimit = Convert.ToBoolean(ls1);
                if (int.TryParse(GetValue(dataString, "ls2"), out int ls2))
                    status.IsPositiveLimit = Convert.ToBoolean(ls2);

                if (int.TryParse(GetValue(dataString, "lasergate"), out int lasergate))
                    status.IsLaserOn = Convert.ToBoolean(lasergate);
                if (int.TryParse(GetValue(dataString, "motiontrack"), out int motiontrack))
                    status.IsAutoFocusOn = Convert.ToBoolean(motiontrack);
            }
        }

        private string GetValue(string data, string dataName)
        {
            if (data.Contains(dataName))
            {
                while (data.Contains("\n4"))
                    data = data.Replace("\n4", "");

                while (data.Contains(":"))
                    data = data.Replace(":", "");

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

        public LAFCtrl GetLAFCtrl(string name)
        {
            var lafCtrlHandler = DeviceManager.Instance().LAFCtrlHandler;

            if (lafCtrlHandler == null)
                return null;

            return lafCtrlHandler.Where(x => x.Name == name).First();
        }

        private HomeSequenceStep _homeSequenceStep = HomeSequenceStep.Stop;
        private enum HomeSequenceStep
        {
            Stop,
            Start,

            MoveToNegativeLimit,
            MoveToStep,
            ReleaseNegativeLimitDetection,
            MoveAfterDeceleration1,
            MoveAfterDeceleration2,
            ZeroSet,

            End,
        }

        private bool _isHomeThreadStop = false;
        private Thread _homeThread = null;

        public void StartHomeThread(string lafName)
        {
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
                Thread.Sleep(100);
            }
        }

        private void PrepareHome(string lafName)
        {
            var lafCtrl = GetLAFCtrl(lafName);

            lafCtrl.SetMotionNegativeLimit(0);
            lafCtrl.SetMotionPositiveLimit(0);
            lafCtrl.SetMotionMaxSpeed(1);
            lafCtrl.SetMotionZeroSet();
            Thread.Sleep(50);
        }

        const double HOMING_SEARCH_DISTANCE = 0.100;        // mm
        const double HOMING_DISTANCE_AWAY_FROM_LIMIT = 0.5; // mm

        private void HomeSequence(string lafName)
        {
            var lafCtrl = GetLAFCtrl(lafName);
            var status = lafCtrl.Status;
            try
            {
                switch (_homeSequenceStep)
                {
                    case HomeSequenceStep.Stop:
                        break;

                    case HomeSequenceStep.Start:
                        PrepareHome(lafName);
                        _homeSequenceStep = HomeSequenceStep.MoveToNegativeLimit;
                        break;

                    case HomeSequenceStep.MoveToNegativeLimit:
                        if (status.IsNegativeLimit)
                        {
                            lafCtrl.SetMotionStop();
                            Thread.Sleep(1000);

                            lafCtrl.SetMotionZeroSet();
                            Thread.Sleep(500);

                            _homeSequenceStep = HomeSequenceStep.MoveToStep;
                        }
                        break;

                    case HomeSequenceStep.MoveToStep:
                        lafCtrl.SetMotionRelativeMove(Direction.CCW, HOMING_SEARCH_DISTANCE);
                        _homeSequenceStep = HomeSequenceStep.ReleaseNegativeLimitDetection;
                        break;

                    case HomeSequenceStep.ReleaseNegativeLimitDetection:
                        if (!status.IsNegativeLimit)
                        {
                            lafCtrl.SetMotionStop();
                            Thread.Sleep(100);

                            lafCtrl.SetMotionZeroSet();
                            Thread.Sleep(500);

                            _homeSequenceStep = HomeSequenceStep.MoveAfterDeceleration1;
                        }
                        else
                            _homeSequenceStep = HomeSequenceStep.MoveToStep;
                        break;

                    case HomeSequenceStep.MoveAfterDeceleration1:
                        if (status.IsNegativeLimit)
                        {
                            lafCtrl.SetMotionStop();
                            Thread.Sleep(100);

                            lafCtrl.SetMotionZeroSet();
                            Thread.Sleep(500);

                            _homeSequenceStep = HomeSequenceStep.MoveAfterDeceleration2;
                        }
                        else
                            lafCtrl.SetMotionRelativeMove(Direction.CW, HOMING_SEARCH_DISTANCE / 10);
                        break;

                    case HomeSequenceStep.MoveAfterDeceleration2:
                        if (status.IsNegativeLimit)
                        {
                            lafCtrl.SetMotionStop();
                            Thread.Sleep(500);

                            lafCtrl.SetMotionZeroSet();
                            Thread.Sleep(500);

                            lafCtrl.SetMotionRelativeMove(Direction.CW, HOMING_DISTANCE_AWAY_FROM_LIMIT);

                            _homeSequenceStep = HomeSequenceStep.MoveAfterDeceleration2;
                        }
                        else
                            lafCtrl.SetMotionRelativeMove(Direction.CW, HOMING_SEARCH_DISTANCE / 10);
                        break;

                    case HomeSequenceStep.ZeroSet:
                        if (Math.Abs(status.MPosPulse - HOMING_DISTANCE_AWAY_FROM_LIMIT) <= float.Epsilon)
                        {
                            lafCtrl.SetMotionStop();
                            Thread.Sleep(500);

                            lafCtrl.SetMotionZeroSet();
                            Thread.Sleep(100);

                            lafCtrl.SetMotionMaxSpeed(15);
                            Thread.Sleep(100);

                            // 대기 위치로 이동 - 포지션 필요함
                            lafCtrl.SetMotionAbsoluteMove(0);
                            Thread.Sleep(1000);
                        }
                        _homeSequenceStep = HomeSequenceStep.End;
                        break;

                    case HomeSequenceStep.End:
                        _isHomeThreadStop = true;
                        _homeSequenceStep = HomeSequenceStep.Stop;
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _isHomeThreadStop = true;
            }
        }
        #endregion
    }
}
