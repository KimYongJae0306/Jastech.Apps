using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Framework.Imaging.Result;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class TabBtnControl : UserControl
    {
        #region 속성
        public int TabIndex { get; protected set; } = -1;
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
            btnTab.Text = "TAB " + (TabIndex + 1).ToString();
        }

        public void SetTabIndex(int tabIndex)
        {
            TabIndex = tabIndex;
        }

        public void UpdateData()
        {
            SetTabEventHandler?.Invoke(TabIndex);
        }

        private void btnTab_Click(object sender, EventArgs e)
        {
            UpdateData();
        }

        public void SetResult(Judgement judgement)
        {
            if (judgement == Judgement.OK)
                btnTab.BackColor = Color.FromArgb(52, 52, 52);
            else
                btnTab.BackColor = Color.Red;
        }

        public void SetButtonClick()
        {
            btnTab.Text = "*TAB " + (TabIndex + 1).ToString();
        }

        public void SetButtonClickNone()
        {
            btnTab.Text = "TAB " + (TabIndex + 1).ToString();
        }
        #endregion
    }
}
