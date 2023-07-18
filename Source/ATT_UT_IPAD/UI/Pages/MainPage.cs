using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.UI.Controls;
using System;
using System.Windows.Forms;

namespace ATT_UT_IPAD.UI.Pages
{
    public partial class MainPage : UserControl
    {
        #region 필드
        #endregion

        #region 속성
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public MainPage()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void MainPage_Load(object sender, EventArgs e)
        {
            AddControls();
        }

        private void AddControls()
        {
        }

        public void UpdateTabCount(int tabCount)
        {
        }

        public void UpdateMainResult(AppsInspResult result)
        {
        }

        public void AddSystemLogMessage(string logMessage)
        {
        }
        #endregion
    }

    public enum PageType
    {
        Result,
        Log,
    }
}
