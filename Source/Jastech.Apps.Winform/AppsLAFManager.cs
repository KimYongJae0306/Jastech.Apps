using Jastech.Framework.Device.LAFCtrl;
using Jastech.Framework.Winform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

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

        public List<LAFStatus> StatusList { get; private set; } = new List<LAFStatus>();
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

            _isStop = false;

            foreach (var laf in lafCtrlHandler)
            {
                Thread statusThread = new Thread(() => RequestStatusData(laf.Name));

                LAFStatus status = new LAFStatus();
                status.Name = laf.Name;

                StatusList.Add(status);
                StatusThreadList.Add(statusThread);

                statusThread.Start();
            }
        }

        public void Release()
        {
            _isStop = true;

            Thread.Sleep(300);

            foreach (var thread in StatusThreadList)
            {
                thread.Interrupt();
                thread.Abort();
            }
            StatusThreadList.Clear();
            StatusList.Clear();
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

        public void DataReceived(string name, byte[] data)
        {
            if(StatusList.Where(x => x.Name == name).First() is LAFStatus status)
            {
                string dataString = Encoding.Default.GetString(data);

                int centerofGravity = -1;
                double mPos = -1;
                bool isNegativeLimit = false;
                bool isPositiveLimit = false;
                // ex : "4\rcog: -2139 mpos: +98050 ls1: 0 ls2: 0 "
                if (int.TryParse(GetValue(dataString, "cog"), out int cog))
                    centerofGravity = cog;

                if (double.TryParse(GetValue(dataString, "mpos"), out double mpos))
                    mPos = cog;

                if (bool.TryParse(GetValue(dataString, "ls1"), out bool ls1))
                    isNegativeLimit = ls1;

                if (bool.TryParse(GetValue(dataString, "ls2"), out bool ls2))
                    isPositiveLimit = ls2;

                status.SetStatus(centerofGravity, mPos, isNegativeLimit, isPositiveLimit);
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

                return content.Substring(0, nextAlphabetIndex).Replace(" ", "");
            }
            return "";
        }
        #endregion
    }

    public class LAFStatus
    {
        private object _lock { get; set; } = new object();

        private string Name { get; set; }

        private int CenterofGravity { get; set; }

        private double MPos { get; set; }

        private bool IsNegativeLimit { get; set; }

        private bool IsPositiveLimit { get; set; }

        public void SetStatus(int centerOfCravity, double mPos, bool isNegativeLimit, bool isPositiveLimit)
        {
            lock(_lock)
            {
                CenterofGravity = centerOfCravity;
                MPos = mPos;
                IsNegativeLimit = isNegativeLimit;
                IsPositiveLimit = isPositiveLimit;
            }
        }

        public LAFStatus GetStatus()
        {
            lock (_lock)
                return this;
        }
    }
}
