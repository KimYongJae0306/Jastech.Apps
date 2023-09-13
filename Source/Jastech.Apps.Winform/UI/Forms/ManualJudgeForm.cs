using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.UI.Controls;
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

        private Color _selectedColor;

        private Color _nonSelectedColor;

        private List<ManualJudgeControl> _manualJudgeControlList = null;

        private List<TabInspResult> _tabAlignInspResultList = new List<TabInspResult>();

        private List<TabInspResult> _tabAkkonInspResultList = new List<TabInspResult>();

        private List<ManualJudge> _manualJudgeList = new List<ManualJudge>();
        #endregion

        #region 속성
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        //private delegate void UpdateMessageDelegate(string message);
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
            InitializeUI();
            AddControl();
            tmrManualJudge.Start();

            //foreach (var item in _manualJudgeControlList)
            //{
            //    item.UpdateResult(null);
            //}
        }

        public void SetManualJudge(List<ManualJudge> manualJudgeList)
        {
            _manualJudgeList = manualJudgeList;
        }

        private void tmrManualJudge_Tick(object sender, EventArgs e)
        {
            if (_onFocus)
                return;

            if (IsNg())
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

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            InitializeJudgeStatus();
        }

        private void AddControl()
        {
            _manualJudgeControlList = new List<ManualJudgeControl>();

            for (int tabIndex = 0; tabIndex < tlpJudge.RowCount; tabIndex++)
            {
                ManualJudgeControl manualJudgeControl = new ManualJudgeControl() { Dock  = DockStyle.Fill };
                manualJudgeControl.SetTabCount(tabIndex);
                _manualJudgeControlList.Add(manualJudgeControl);
                tlpJudge.Controls.Add(manualJudgeControl, 0, tabIndex);
            }
        }

        private void InitializeJudgeStatus()
        {
            var appInspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            if (appInspModel == null)
                return;

            var tabCount = appInspModel.TabCount;

            tlpJudge.ColumnStyles.Clear();
            tlpJudge.RowStyles.Clear();

            tlpJudge.RowCount = tabCount;

            for (int rowIndex = 0; rowIndex < tlpJudge.RowCount; rowIndex++)
                tlpJudge.RowStyles.Add(new RowStyle(SizeType.Percent, (float)(100 / tlpJudge.RowCount)));
        }

        //public void UpdateJudgeMessage(string message)
        //{
        //    if (this.InvokeRequired)
        //    {
        //        UpdateMessageDelegate callback = UpdateJudgeMessage;
        //        BeginInvoke(callback, message);
        //        return;
        //    }

        //    UpdateMessage(message);
        //}

        private void UpdateMessage()
        {
            //lblMessage.Text = Message;
        }

        private bool IsNg()
        {
            if (_tabAlignInspResultList == null)
                return false;

            var alignResult = _tabAlignInspResultList.Where(x => x.Judgement == TabJudgement.NG).FirstOrDefault();
            if (alignResult == null)
                return false;

            if (alignResult.MarkResult.Judgement == Judgement.NG)
                return true;

            if (alignResult.Judgement == TabJudgement.NG)
                return true;

            if (_tabAkkonInspResultList == null)
                return false;

            var akkonResult = _tabAkkonInspResultList.Where(x => x.Judgement == TabJudgement.NG).FirstOrDefault();
            if (akkonResult == null)
                return false;

            if (akkonResult.MarkResult.Judgement == Judgement.NG)
                return true;

            if (akkonResult.Judgement == TabJudgement.NG)
                return true;

            return false;
        }

        public void SetTabAkkonInspectionResult(TabInspResult tabAkkonInspResult)
        {
            _tabAkkonInspResultList.Add(tabAkkonInspResult);
        }

        public void SetTabAlignInspectionResult(TabInspResult tabAlignResult)
        {
            _tabAlignInspResultList.Add(tabAlignResult);
        }

        public void SetTabInspectionResult(TabInspResult tabInspResult)
        {
            _tabAkkonInspResultList.Add(tabInspResult);
            _tabAlignInspResultList.Add(tabInspResult);
        }

        public void SetInspectionResult()
        {
            int tabNo = 0;
            foreach (var manualJudgeControl in _manualJudgeControlList)
            {
                manualJudgeControl.UpdateResult(_manualJudgeList[tabNo]);
                tabNo++;
            }
        }

        private void ClearInspectionResult()
        {
            _tabAlignInspResultList.Clear();
            _tabAkkonInspResultList.Clear();
        }

        private void lblOK_Click(object sender, EventArgs e)
        {
            _onFocus = false;

            var appInspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            var tabCount = appInspModel.TabCount;

            MessageYesNoForm form = new MessageYesNoForm();
            form.Message = "Will it be decided as OK?";
            if (form.ShowDialog() == DialogResult.Yes)
            {
                for (int tabNo = 0; tabNo < tabCount; tabNo++)
                {
                    _tabAlignInspResultList[tabNo].IsManualOK = true;
                    _tabAkkonInspResultList[tabNo].IsManualOK = true;
                }

                ClearInspectionResult();
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
                ClearInspectionResult();
                this.Hide();
            }

            _onFocus = true;
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

    public class ManualJudge
    {
        public Judgement AlignJudgement { get; set; }

        // Spec
        public double Lx { get; set; } = 0.0;

        public double Ly { get; set; } = 0.0;

        public double Cx { get; set; } = 0.0;

        public double Rx { get; set; } = 0.0;

        public double Ry { get; set; } = 0.0;

        public Judgement AkkonJudgement { get; set; }

        // Spec
        public int Count { get; set; } = 0;

        public double Length { get; set; } = 0.0;
    }
}
