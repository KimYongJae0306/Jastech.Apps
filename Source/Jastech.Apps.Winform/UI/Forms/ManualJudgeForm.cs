using Jastech.Apps.Winform.Core;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Winform.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Forms
{
    public partial class ManualJudgeForm : Form
    {
        #region 필드
        private bool _onFocus { get; set; } = false;

        private Point _mousePoint;
        #endregion

        #region 속성
        public string Message { get; set; } = string.Empty;

        private Judgement Judgement { get; set; } = Judgement.NG;
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public ManualJudgeForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void ManualJudgeForm_Load(object sender, EventArgs e)
        {
            tmrManualJudge.Start();
        }

        private void tmrManualJudge_Tick(object sender, EventArgs e)
        {
            if (_onFocus)
                return;

            if (NGManager.Instance().IsExistNG())
            {
                if (this.Visible)
                {
                    this.Show();
                    UpdateMessage();
                    this.Focus();
                }
                else
                {
                    Hide();
                }
            }
        }

        private void UpdateMessage()
        {
            lblMessage.Text = Message;
        }

        private void lblOK_Click(object sender, EventArgs e)
        {
            _onFocus = false;

            MessageYesNoForm form = new MessageYesNoForm();
            form.Message = "Will it be decided as OK?";
            if (form.ShowDialog() == DialogResult.Yes)
            {
                Judgement = Judgement.OK;
                this.Hide();
            }

            _onFocus = true;
        }

        private void lblNG_Click(object sender, EventArgs e)
        {
            _onFocus = false;

            MessageYesNoForm form = new MessageYesNoForm();
            form.Message = "Will it be decided as NG?";
            if (form.ShowDialog() == DialogResult.Yes)
            {
                Judgement = Judgement.NG;
                this.Hide();
            }

            _onFocus = true;
        }

        private void lblFail_Click(object sender, EventArgs e)
        {
            _onFocus = false;

            MessageYesNoForm form = new MessageYesNoForm();
            form.Message = "Will it be decided as FAIL?";
            if (form.ShowDialog() == DialogResult.Yes)
            {
                Judgement = Judgement.FAIL;
                this.Hide();
            }

            _onFocus = true;
        }

        public Judgement GetManualJudgement()
        {
            return Judgement;
        }

        private void pnlTop_MouseDown(object sender, MouseEventArgs e)
        {
            _mousePoint = new Point(e.X, e.Y);
        }

        private void pnlTop_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                Location = new Point(this.Left - (_mousePoint.X - e.X), this.Top - (_mousePoint.Y - e.Y));
        }
        #endregion
    }
}
