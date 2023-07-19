using Jastech.Framework.Util.Helper;
using System;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class SystemLogControl : UserControl
    {
        #region 델리게이트
        private delegate void AddLogMessageDelegate(string logMessage);
        #endregion

        #region 생성자
        public SystemLogControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void ClearLogMessage()
        {
            lstLogMessage.Items.Clear();
        }

        private void WriteLogMessage(string logMessage)
        {
            string content = "[" + Logger.GetTimeString(DateTime.Now) + "] ";

            content += logMessage;

            lstLogMessage.Items.Add(content);
            lstLogMessage.SelectedIndex = lstLogMessage.Items.Count - 1;
        }

        public void AddLogMessage(string logMessage)
        {
            if (this.InvokeRequired)
            {
                AddLogMessageDelegate callback = AddLogMessage;
                BeginInvoke(callback, logMessage);
                return;
            }

            if (lstLogMessage.Items.Count >= 1000)
                ClearLogMessage();

            WriteLogMessage(logMessage);
        }
        #endregion

        private void lstLogMessage_DoubleClick(object sender, EventArgs e)
        {
            ClearLogMessage();
        }
    }
}
