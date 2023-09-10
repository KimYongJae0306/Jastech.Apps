using Jastech.Apps.Structure.Data;
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
        public void UpdateResult(TabInspResult tabAlignInspResult, TabInspResult tabAkkonInspResult)
        {
            if (tabAlignInspResult == null)
                return;

            UpdateAkkonResult(tabAkkonInspResult);
            UpdateAlignResult(tabAlignInspResult);

            UpdateResult();
        }

        private void UpdateMarkResult(TabMarkResult markResult)
        {
            if (markResult != null)
            {
                if (markResult.FpcMark.Judgement == Framework.Imaging.Result.Judgement.OK)
                    _alignResult = markResult.FpcMark.Judgement.ToString();
                else if (markResult.FpcMark.Judgement == Framework.Imaging.Result.Judgement.NG)
                    _alignResult = "COF Mark NG";
                else if (markResult.FpcMark.Judgement == Framework.Imaging.Result.Judgement.FAIL)
                    _alignResult = "COF Mark FAIL";
                else { }

                if (markResult.PanelMark.Judgement == Framework.Imaging.Result.Judgement.OK)
                {
                    _alignResult = markResult.PanelMark.Judgement.ToString();
                    _akkonResult = markResult.PanelMark.Judgement.ToString();
                }
                else if (markResult.PanelMark.Judgement == Framework.Imaging.Result.Judgement.NG)
                {
                    _alignResult = "Panel Mark NG";
                    _akkonResult = "Panel Mark NG";
                }
                else if (markResult.PanelMark.Judgement == Framework.Imaging.Result.Judgement.FAIL)
                {
                    _alignResult = "Panel Mark FAIL";
                    _akkonResult = "Panel Mark FAIL";
                }
                else { }
            }
        }

        private void UpdateAlignResult(TabInspResult tabAlignResult)
        {
            if (tabAlignResult != null)
                _alignResult = tabAlignResult.AlignResult.Judgement.ToString();

            if (tabAlignResult.MarkResult.Judgement == Framework.Imaging.Result.Judgement.NG)
                _alignResult = "Mark " + tabAlignResult.MarkResult.Judgement.ToString();
        }

        private void UpdateAkkonResult(TabInspResult tabAkkonResult)
        {
            if (tabAkkonResult != null)
                _akkonResult = tabAkkonResult.Judgement.ToString();

            if (tabAkkonResult.MarkResult.Judgement == Framework.Imaging.Result.Judgement.NG)
                _akkonResult = "Mark" + tabAkkonResult.MarkResult.Judgement.ToString();
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
