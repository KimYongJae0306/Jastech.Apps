using Jastech.Framework.Imaging.Result;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class TabBtnControl : UserControl
    {
        #region 속성
        public int _tabIndex { get; protected set; } = -1;
        #endregion

        #region 이벤트
        public event SetTabDelegate SetTabEventHandler;
        #endregion

        #region 델리게이트
        public delegate void SetTabDelegate(int tabNum);
        #endregion

        #region 생성자
        public TabBtnControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void TabBtnControl_Load(object sender, EventArgs e)
        {
            // UI 상 Tab 1부터 보여주기 위함
            btnTab.Text = "TAB " + (_tabIndex + 1).ToString();
        }

        public void SetTabIndex(int tabIndex)
        {
            _tabIndex = tabIndex;
        }

        public void UpdateData()
        {
            SetTabEventHandler?.Invoke(_tabIndex);
        }

        private void btnTab_Click(object sender, EventArgs e)
        {
            UpdateData();
        }

        public void SetResult(Judgment judgement)
        {
            if (judgement == Judgment.OK)
                btnTab.BackColor = Color.FromArgb(52, 52, 52);
            else
                btnTab.BackColor = Color.Red;
        }

        public void SetButtonClick()
        {
            btnTab.Text = "*TAB " + (_tabIndex + 1).ToString();
        }

        public void SetButtonClickNone()
        {
            btnTab.Text = "TAB " + (_tabIndex + 1).ToString();
        }
        #endregion
    }
}
