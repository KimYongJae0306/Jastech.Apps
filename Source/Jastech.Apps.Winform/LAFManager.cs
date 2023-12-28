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
        public List<LAF> LAFList = new List<LAF>();
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

            foreach (var lafCtrl in lafCtrlHandler)
            {
                var laf = new LAF(lafCtrl);
                laf.Initialize();
                LAFList.Add(laf);
            }
        }

        public void Release()
        {
            foreach (var laf in LAFList)
                laf.Release();
        }

        public LAF GetLAF(string name)
        {
            return LAFList.Where(x => x.LafCtrl.Name == name).FirstOrDefault();
        }
        #endregion
    }
}
