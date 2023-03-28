﻿using ATT.Core;
using Jastech.Apps.Structure;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Structure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATT
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppConfig.Instance().Initialize();
            AppConfig.Instance().Load();
            var mainForm = new MainForm();
           
            SystemManager.Instance().Initialize(mainForm);

            Application.Run(mainForm);
        }
    }
}
