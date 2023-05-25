using System.Windows.Forms;
using System.IO;

namespace Jastech.Framework.Winform.Controls
{
    public partial class LogControl : UserControl
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
        public LogControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        public void DisplayOnLogFile(string path)
        {
            StreamReader sr = new StreamReader(path);
            string contents = sr.ReadToEnd();
            rtxLogMessage.Text = contents;

            sr.Close();
            sr.Dispose();
        }
        #endregion
    }
}
