﻿using System.Drawing;

namespace Jastech.Apps.Winform
{
    public class AppsStatus
    {
        #region 필드
        private static AppsStatus _instance = null;
        #endregion

        #region 속성
        public bool IsInspRunnerRunning { get; set; } = false;

        public bool IsPreAlignRunnerRunning { get; set; } = false;

        public bool IsInspRunnerFlagFromPlc { get; set; } = false;

        public bool IsPreAlignRunnerFlagFromPlc { get; set; } = false;

        public bool IsLastAutoInspGrab { get; set; } = false;

        public bool IsManualJudgeCompleted { get; set; } = false;

        public bool IsManual_OK { get; set; } = false;

        public bool IsCalibrationing { get; set; } = false;

        public bool IsManualMatching_OK { get; set; } = false;

        public PointF ManualMatchingPoint { get; set; } = new PointF();

        public bool IsRepeat { get; set; } = false;

        public bool IsModelChanging { get; set; } = false;

        public bool IsAlignMonitoringView { get; set; } = false;

        public bool AutoRunTest { get; set; } = false;
        #endregion

        #region 메서드
        public static AppsStatus Instance()
        {
            if (_instance == null)
            {
                _instance = new AppsStatus();
            }

            return _instance;
        }
        #endregion
    }
}
