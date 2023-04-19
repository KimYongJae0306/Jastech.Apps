using Jastech.Apps.Structure.Data;
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

        public void DataReceived(string name, byte[] data)
        {
            if (DeviceManager.Instance().LAFCtrlHandler.Get(name) is NuriOneLAFCtrl laf)
            {
                string dataString = Encoding.Default.GetString(data);

                int centerofGravity = -1;
                double mPos = -1;
                bool isNegativeLimit = false;
                bool isPositiveLimit = false;
                // ex : "4\rcog: -2139 mpos: +98050 ls1: 0 ls2: 0 "

                bool isContain = false;
                if (int.TryParse(GetValue(dataString, "cog"), out int cog))
                {
                    isContain = true;
                    centerofGravity = cog;
                }

                if (double.TryParse(GetValue(dataString, "mpos"), out double mpos))
                {
                    isContain = true;
                    mPos = mpos;
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
                    laf.Status.MPos = mPos;
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
        #endregion
    }
}
