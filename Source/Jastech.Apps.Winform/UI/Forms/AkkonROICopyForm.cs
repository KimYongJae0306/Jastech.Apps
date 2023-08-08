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
    public partial class AkkonROICopyForm : Form
    {
        #region 필드
        private Color _selectedColor;

        private Color _nonSelectedColor;

        private int _selectedSourceTabNumber { get; set; } = -1;

        private List<int> _selectedTargetTabNumber { get; set; } = null;
        #endregion

        #region 속성
        public UnitName UnitName { get; set; } = UnitName.Unit0;

        public List<Tab> TeachingTabList { get; private set; } = null;
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public AkkonROICopyForm()
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

        public void SetUnitName(UnitName unitName)
        {
            UnitName = unitName;
        }

        private void lblApply_Click(object sender, EventArgs e)
        {
            MessageYesNoForm form = new MessageYesNoForm();
            form.Message = "Do you want to overwrite?";

            if (form.ShowDialog() == DialogResult.Yes)
                Apply();
        }

        private void Apply()
        {
            if (ModelManager.Instance().CurrentModel == null)
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "Current Model is null.";
                return;
            }

            if (_selectedSourceTabNumber <= 0)
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "No Source selected.";
                return;
            }

            if (_selectedTargetTabNumber.Count <= 0)
            {
                MessageConfirmForm form = new MessageConfirmForm();
                form.Message = "No Target selected.";
                return;
            }

            CopyROI();
        }

        private void CopyROI()
        {
            TeachingTabList = TeachingData.Instance().GetUnit(UnitName.ToString()).GetTabList();

            Tab selectedSourceTab = TeachingTabList.Where(x => x.Index == _selectedSourceTabNumber).FirstOrDefault();

            if (ExistMarkParam(selectedSourceTab) == false)
                return;

            // 복사할놈, 복사당할놈 마크 티칭 비교 후 ROI 복사하고나서 티칭값 기준 틀어버리기 필요

            var sourceROIList = selectedSourceTab.AkkonParam.GetAkkonROIList();

            foreach (var item in _selectedTargetTabNumber)
            {
                Tab selectedTargetTab = TeachingData.Instance().GetUnit(UnitName.ToString()).GetTab(item);

                foreach (var group in selectedTargetTab.AkkonParam.GroupList)
                {
                    List<AkkonROI> akkonList = new List<AkkonROI>();

                    akkonList.AddRange(sourceROIList.ToList());
                    group.AkkonROIList.Clear();
                    group.AkkonROIList.AddRange(akkonList);
                }
            }
        }

        private bool ExistMarkParam(Tab tab)
        {
            if (tab.MarkParamter.GetPanelMark(MarkDirection.Left, MarkName.Main, false).InspParam.IsTrained() == false)
                return false;

            if (tab.MarkParamter.GetPanelMark(MarkDirection.Right, MarkName.Main, false).InspParam.IsTrained() == false)
                return false;

            return true;
        }

        private void lblCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
