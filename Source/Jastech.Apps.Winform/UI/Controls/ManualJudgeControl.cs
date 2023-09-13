using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.UI.Forms;
using Jastech.Framework.Algorithms.Akkon.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class ManualJudgeControl : UserControl
    {
        #region 필드
        private string _alignResult { get; set; } = "OK";

        private string _akkonResult { get; set; } = "OK";
        #endregion

        #region 속성
        public int TabNo { get; private set; } = 0;
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        private delegate void UpdateResultDelegate();
        #endregion

        #region 생성자
        public ManualJudgeControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void ManualJudgeControl_Load(object sender, EventArgs e)
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            lblTab.Text = "Tab " + (TabNo + 1).ToString();
            lblAlign.Text = _alignResult;
            lblAkkon.Text = _akkonResult;
        }

        public void SetTabCount(int tabNo)
        {
            TabNo = tabNo;
        }

        // delegate 추가할 것
        public void UpdateResult(/*TabInspResult tabAlignInspResult, TabInspResult tabAkkonInspResult*/ManualJudge manualJudge)
        {
            if (manualJudge == null)
                return;

            UpdateAkkonResult(manualJudge);
            UpdateAlignResult(manualJudge);

            UpdateResult();
        }

        private void UpdateAlignResult(ManualJudge manualJudge)
        {
            if (manualJudge != null)
                _alignResult = manualJudge.AlignJudgement.ToString();
        }

        private string AlignSpecification(ManualJudge manualJudge)
        {
            string spec = "Lx : " + manualJudge.Lx.ToString() + "\r\n" +
                            "Ly : " + manualJudge.Ly.ToString() + "\r\n" +
                            "Cx : " + manualJudge.Cx.ToString() + "\r\n" +
                            "Rx : " + manualJudge.Rx.ToString() + "\r\n" +
                            "Ry : " + manualJudge.Ry.ToString() + "\r\n";

            return spec;
        }

        private void UpdateAkkonResult(ManualJudge manualJudge)
        {
            if (manualJudge != null)
                _akkonResult = manualJudge.AkkonJudgement.ToString();
        }

        private void UpdateResult()
        {
            if (this.InvokeRequired)
            {
                UpdateResultDelegate callback = UpdateResult;
                BeginInvoke(callback);
                return;
            }

            UpdateUI();
        }

        private void UpdateUI()
        {
            if (_alignResult.ToUpper().ToString().Contains("NG"))
                lblAlign.BackColor = Color.Red;
            else
                lblAlign.BackColor = Color.MediumSeaGreen;

            lblAlign.Text = _alignResult;

            if (_akkonResult.ToUpper().ToString().Contains("NG"))
                lblAkkon.BackColor = Color.Red;
            else
                lblAkkon.BackColor = Color.MediumSeaGreen;

            lblAkkon.Text = _akkonResult;
        }
        #endregion
    }
}
