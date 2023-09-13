using Emgu.CV.Dnn;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Core;
using Jastech.Framework.Algorithms.Akkon.Parameters;
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
    public partial class ROICopyForm : Form
    {
        #region 필드
        private Color _selectedColor;

        private Color _nonSelectedColor;

        private Point _mousePoint;

        private int _selectedSourceTabNumber { get; set; } = -1;

        private List<int> _selectedTargetTabNumber { get; set; } = null;

        private bool _isMarkCopy { get; set; } = false;

        private bool _isAlignCopy { get; set; } = false;

        private bool _isAkkonCopy { get; set; } = false;
        #endregion

        #region 속성
        public UnitName UnitName { get; set; } = UnitName.Unit0;

        public List<Tab> TeachingTabList { get; private set; } = null;

        public bool UseAlignCamMark { get; set; } = false;
        //public DisplayType DisplayType { get; set; } = DisplayType.Akkon;
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public ROICopyForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void AkkonROICopyForm_Load(object sender, EventArgs e)
        {
            _selectedTargetTabNumber = new List<int>();

            InitilaizeUI();
        }

        private void InitilaizeUI()
        {
            var appInspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            var tabCount = appInspModel.TabCount;

            if (tabCount <= 0)
                return;

            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            //lblTop.Text = DisplayType.ToString() + " ROI Copy";

            InitializeSourceTab(tabCount);
            InitializeTargetTab(tabCount);
        }

        private void InitializeSourceTab(int tabCount)
        {
            tlpSourceTab.ColumnStyles.Clear();
            tlpSourceTab.RowStyles.Clear();

            tlpSourceTab.ColumnCount = 1;
            tlpSourceTab.RowCount = tabCount;

            for (int rowIndex = 0; rowIndex < tlpSourceTab.RowCount; rowIndex++)
                tlpSourceTab.RowStyles.Add(new RowStyle(SizeType.Percent, (float)(100 / tlpSourceTab.RowCount)));

            for (int rowIndex = 0; rowIndex < tlpSourceTab.RowCount; rowIndex++)
            {
                RadioButton rdo = new RadioButton();
                rdo.Appearance = Appearance.Button;
                rdo.BackColor = _nonSelectedColor;
                rdo.ForeColor = Color.White;
                rdo.Margin = new Padding(0);
                rdo.AutoSize = false;
                rdo.TextAlign = ContentAlignment.MiddleCenter;
                rdo.FlatStyle = FlatStyle.Popup;
                rdo.Text = "Tab " + (rowIndex + 1).ToString();
                rdo.Dock = DockStyle.Fill;
                rdo.CheckedChanged += Rdo_CheckedChanged;
                tlpSourceTab.Controls.Add(rdo, 0, rowIndex);
            }
        }

        private void Rdo_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = sender as RadioButton;

            if (rdo.Checked)
            {
                rdo.BackColor = _selectedColor;
                _selectedSourceTabNumber = Convert.ToInt32(rdo.Text.Substring(4, 1)) - 1;
            }
            else
                rdo.BackColor = _nonSelectedColor;
        }

        private void InitializeTargetTab(int tabCount)
        {
            tlpTargetTab.ColumnStyles.Clear();
            tlpTargetTab.RowStyles.Clear();

            tlpTargetTab.ColumnCount = 1;
            tlpTargetTab.RowCount = tabCount;

            for (int rowIndex = 0; rowIndex < tlpTargetTab.RowCount; rowIndex++)
                tlpTargetTab.RowStyles.Add(new RowStyle(SizeType.Percent, (float)(100 / tlpTargetTab.RowCount)));

            for (int rowIndex = 0; rowIndex < tlpTargetTab.RowCount; rowIndex++)
            {
                CheckBox chk = new CheckBox();
                chk.Appearance = Appearance.Button;
                chk.BackColor = _nonSelectedColor;
                chk.ForeColor = Color.White;
                chk.Margin = new Padding(0);
                chk.AutoSize = false;
                chk.TextAlign = ContentAlignment.MiddleCenter;
                chk.FlatStyle = FlatStyle.Popup;
                chk.Text = "Tab " + (rowIndex + 1).ToString();
                chk.Dock = DockStyle.Fill;
                chk.CheckedChanged += Chk_CheckedChanged;
                tlpTargetTab.Controls.Add(chk, 0, rowIndex);
            }
        }

        private void Chk_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;

            if (chk.Checked)
            {
                chk.BackColor = _selectedColor;
                _selectedTargetTabNumber.Add(Convert.ToInt32(chk.Text.Substring(4, 1)) - 1);
            }
            else
            {
                chk.BackColor = _nonSelectedColor;
                _selectedTargetTabNumber.Remove(Convert.ToInt32(chk.Text.Substring(4, 1)) - 1);
            }
        }

        private void chkMark_CheckedChanged(object sender, EventArgs e)
        {
            _isMarkCopy = chkMark.Checked;

            if (_isMarkCopy)
                chkMark.BackColor = _selectedColor;
            else
                chkMark.BackColor = _nonSelectedColor;
        }

        private void chkAlign_CheckedChanged(object sender, EventArgs e)
        {
            _isAlignCopy = chkAlign.Checked;

            if (_isAlignCopy)
                chkAlign.BackColor = _selectedColor;
            else
                chkAlign.BackColor = _nonSelectedColor;
        }

        private void chkAkkon_CheckedChanged(object sender, EventArgs e)
        {
            _isAkkonCopy = chkAkkon.Checked;

            if (_isAkkonCopy)
                chkAkkon.BackColor = _selectedColor;
            else
                chkAkkon.BackColor = _nonSelectedColor;
        }

        public void SetUnitName(UnitName unitName)
        {
            UnitName = unitName;
        }

        private void lblApply_Click(object sender, EventArgs e)
        {
            string message = string.Format("Do you want to overwite\nfrom source Tab {0}\nto selected target Tab ?", _selectedSourceTabNumber + 1);

            MessageYesNoForm form = new MessageYesNoForm();
            form.Message = message;

            if (form.ShowDialog() == DialogResult.Yes)
                Apply();
        }

        private void Apply()
        {
            if (ModelManager.Instance().CurrentModel == null)
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "Current Model is null.";
                form.ShowDialog();
                return;
            }

            if (_selectedSourceTabNumber < 0)
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "No Source selected.";
                form.ShowDialog();
                return;
            }

            if (_selectedTargetTabNumber.Count <= 0)
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "No Target selected.";
                form.ShowDialog();
                return;
            }

            if (_isMarkCopy == false && _isAlignCopy == false && _isAkkonCopy == false)
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "No Item selected.";
                form.ShowDialog();
                return;
            }

            Excute();
        }

        private void Excute()
        {
            if (_isMarkCopy)
                CopyMarkParam();
            
            if (_isAlignCopy)
                CopyAlignParam();

            if (_isAkkonCopy)
                CopyAkkonParam();

            this.Close();

            //TeachingTabList = TeachingData.Instance().GetUnit(UnitName.ToString()).GetTabList();

            //Tab selectedSourceTab = TeachingTabList.Where(x => x.Index == _selectedSourceTabNumber).FirstOrDefault();

            //if (ExistMarkParam(selectedSourceTab) == false)
            //    return;

            // 복사할놈, 복사당할놈 마크 티칭 비교 후 ROI 복사하고나서 티칭값 기준 틀어버리기 필요
        }

        private void CopyMarkParam()
        {
            TeachingTabList = TeachingData.Instance().GetUnit(UnitName.ToString()).GetTabList();
            Tab selectedSourceTab = TeachingTabList.Where(x => x.Index == _selectedSourceTabNumber).FirstOrDefault();

            foreach (var tabNo in _selectedTargetTabNumber)
            {
                Tab selectedTargetTab = TeachingData.Instance().GetUnit(UnitName.ToString()).GetTab(index: tabNo);

                if(UseAlignCamMark)
                    selectedTargetTab.AlignCamMark = selectedSourceTab.AlignCamMark.DeepCopy();
                else
                    selectedTargetTab.Mark = selectedSourceTab.Mark.DeepCopy();
            }
        }

        private void CopyAlignParam()
        {
            TeachingTabList = TeachingData.Instance().GetUnit(UnitName.ToString()).GetTabList();
            Tab selectedSourceTab = TeachingTabList.Where(x => x.Index == _selectedSourceTabNumber).FirstOrDefault();

            foreach (var tabNo in _selectedTargetTabNumber)
            {
                Tab selectedTargetTab = TeachingData.Instance().GetUnit(UnitName.ToString()).GetTab(index: tabNo);

                foreach (ATTTabAlignName alignName in Enum.GetValues(typeof(ATTTabAlignName)))
                {
                    var seledtedSourceAlignParam = selectedSourceTab.GetAlignParam(alignName);
                    selectedTargetTab.SetAlignParam(alignName, seledtedSourceAlignParam);
                }
            }
        }

        private void CopyAkkonParam()
        {
            TeachingTabList = TeachingData.Instance().GetUnit(UnitName.ToString()).GetTabList();
            Tab selectedSourceTab = TeachingTabList.Where(x => x.Index == _selectedSourceTabNumber).FirstOrDefault();

            foreach (var tabNo in _selectedTargetTabNumber)
            {
                Tab selectedTargetTab = TeachingData.Instance().GetUnit(UnitName.ToString()).GetTab(index: tabNo);
                selectedTargetTab.AkkonParam = selectedSourceTab.AkkonParam.DeepCopy();
            }
        }
        
        private void lblCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
