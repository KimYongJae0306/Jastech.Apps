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
    public class AppsLAFManager
    {
        #region 필드
        private static AppsLAFManager _instance = null;

        private bool _isStop { get; set; } = false;
        #endregion

        #region 속성
        public List<Thread> StatusThreadList { get; private set; } = new List<Thread>();
        #endregion

        #region 메서드
        public static AppsLAFManager Instance()
        {
            if (_instance == null)
            {
                _instance = new AppsLAFManager();
            }

            return _instance;
        }

        public void Initialize()
        {
            var lafCtrlHandler = DeviceManager.Instance().LAFCtrlHandler;

            if (lafCtrlHandler == null)
                return;

            _isStop = false;

            foreach (var laf in lafCtrlHandler)
            {
                laf.DataReceived += DataReceived;

                Thread statusThread = new Thread(() => RequestStatusData(laf.Name));

                LAFStatus status = new LAFStatus();
                status.Name = laf.Name;

                StatusThreadList.Add(statusThread);

                statusThread.Start();
            }
        }

        public void Release()
        {
            _isStop = true;

            Thread.Sleep(300);

            var lafCtrlHandler = DeviceManager.Instance().LAFCtrlHandler;

            if (lafCtrlHandler != null)
            {
                foreach (var laf in lafCtrlHandler)
                    laf.DataReceived -= DataReceived;
            }

            foreach (var thread in StatusThreadList)
            {
                thread.Interrupt();
                thread.Abort();
            }
            StatusThreadList.Clear();
        }

        public void RequestStatusData(string name)
        {
            while (_isStop == false)
            {
                if (DeviceManager.Instance().LAFCtrlHandler.Get(name) is NuriOneLAFCtrl laf)
                {
                    // cog, mpos, ls1, ls2 값 동시 요청
                    string command = "uc rep cog mpos ls1 ls2";


                    laf.RequestData(command);
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

                int centerofGravity = -1;
                double mPosPulse = -1;
                bool isNegativeLimit = false;
                bool isPositiveLimit = false;
                // ex : "4\rcog: -2139 mpos: +98050 ls1: 0 ls2: 0 "

                bool isContain = false;
                if (int.TryParse(GetValue(dataString, "cog"), out int cog))
                {
                    isContain = true;
                    centerofGravity = cog;
                }

                if (double.TryParse(GetValue(dataString, "mpos"), out double mposPulse))
                {
                    isContain = true;
                    mPosPulse = mposPulse;
                }

                if (int.TryParse(GetValue(dataString, "ls1"), out int ls1))
                {
                    isContain = true;
                    isNegativeLimit = Convert.ToBoolean(ls1);
                }

                if (int.TryParse(GetValue(dataString, "ls2"), out int ls2))
                {
                    isContain = true;
                    isPositiveLimit = Convert.ToBoolean(ls2);
                }

                if(isContain)
                {
                    laf.Status.CenterofGravity = centerofGravity;
                    laf.Status.MPosPulse = mPosPulse;
                    laf.Status.IsNegativeLimit = isNegativeLimit;
                    laf.Status.IsPositiveLimit = isPositiveLimit;
                }
            }
        }

        private string GetValue(string data, string dataName)
        {
            if (data.Contains(dataName))
            {
                int startIndex = data.IndexOf(dataName) + dataName.Length + 1; // +1 => ':' 길이 계산 값
                string content = data.Substring(startIndex);

                int nextAlphabetIndex = -1;
                for (int i = 0; i < content.Length; i++)
                {
                    string gg = content[i].ToString();
                    nextAlphabetIndex = i;
                    if (Regex.IsMatch(content[i].ToString(), "^[a-zA-Z]"))
                        break;
                }
                if (content.Length > 0)
                    return content.Substring(0, nextAlphabetIndex).Replace(" ", "");
                else
                    return "";
            }
            return "";
        }

        public LAFCtrl GetLAFCtrl(LAFName name)
        {
            var lafCtrlHandler = DeviceManager.Instance().LAFCtrlHandler;

            if (lafCtrlHandler == null)
                return null;

            return lafCtrlHandler.Where(x => x.Name == name.ToString()).First();
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

        public void StartHomeThread(LAFName lafName)
        {
            _isHomeThreadStop = false;
            _homeThread = new Thread(new ParameterizedThreadStart(HomeSequenceThread));
            _homeThread.Start(lafName);
        }

        private void HomeSequenceThread(object obj)
        {
            LAFName lafName = (LAFName)obj;
            _homeSequenceStep = HomeSequenceStep.Start;

            while (!_isHomeThreadStop)
            {
                HomeSequence(lafName);
                Thread.Sleep(100);
            }
        }

        private void PrepareHome(LAFName lafName)
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

        private void HomeSequence(LAFName lafName)
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
