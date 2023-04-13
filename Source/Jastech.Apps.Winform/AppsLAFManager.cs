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
        private Thread _statusThread { get; set; } = null;

        private bool _isStop { get; set; } = false;

        private static AppsLAFManager _instance = null;

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
            _isStop = false;
            _statusThread = new Thread(RequestStatusData);
            _statusThread.Start();
        }

        public void Release()
        {
            _isStop = true;

            Thread.Sleep(300);


        }

        private void RequestStatusData()
        {
            while(_isStop == false)
            {
                if(DeviceManager.Instance().LAFCtrlHandler.Get("LaserAutoFocus") is NuriOneLAFCtrl laf)
                {
                    // cog, mpos, ls1, ls2 값 동시 요청
                    string command = "uc rep cog mpos ls1 ls2";

                    laf.RequestData(command);
                }
                Thread.Sleep(300);
            }
        }

        public void DataReceived(byte[] data)
        {
            string dataString = Encoding.Default.GetString(data);

            string cogValue = GetValue(dataString, "cog");

            string mPosValue = GetValue(dataString, "mpos");

            string ls1Value = GetValue(dataString, "ls1");

            string ls2Value = GetValue(dataString, "ls2");
        }

        private string GetValue(string data, string dataName)
        {
            if (data.Contains(dataName))
            {
                //Regex.IsMatch(str, "^[a-zA-Z0-9]*$")
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
    }
}
