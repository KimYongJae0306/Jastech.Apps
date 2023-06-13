using ATT_UT_IPAD.UI.Pages;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATT_UT_IPAD.UI.Controls
{
    public partial class AlignViewerControl : UserControl
    {
        private Color _selectedColor;

        private Color _nonSelectedColor;

        public List<TabBtnControl> TabBtnControlList { get; private set; } = new List<TabBtnControl>();

        private int _prevTabCount { get; set; } = -1;

        public int CurrentTabNo { get; set; } = -1;

        public Dictionary<int, TabInspResult> InspResultDic { get; set; } = new Dictionary<int, TabInspResult>();

        public AlignResultDisplayControl AlignResultDisplayControl { get; set; } = new AlignResultDisplayControl() { Dock = DockStyle.Fill };

        public AlignResultControl AlignResultControl { get; set; } = new AlignResultControl() { Dock = DockStyle.Fill };

        public AlignViewerControl()
        {
            InitializeComponent();
        }

        private void AlignResultViewer_Load(object sender, EventArgs e)
        {
            AddControls();
            IniitlaizeUI();
            SelectPage(PageType.Result);
        }

        private void AddControls()
        {
            pnlAlignResultDisplay.Controls.Add(AlignResultDisplayControl);
            pnlAlignData.Controls.Add(AlignResultControl);
        }

        private void IniitlaizeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            if (inspModel == null)
                UpdateTabCount(1);
            else
                UpdateTabCount(inspModel.TabCount);
        }

        public void UpdateTabCount(int tabCount)
        {
            if (_prevTabCount == tabCount)
                return;

            ClearTabBtnList();

            int controlWidth = 100;
            Point point = new Point(0, 0);

            for (int i = 0; i < tabCount; i++)
            {
                TabBtnControl buttonControl = new TabBtnControl();
                buttonControl.SetTabIndex(i);
                buttonControl.SetTabEventHandler += ButtonControl_SetTabEventHandler;
                buttonControl.Size = new Size(controlWidth, (int)(pnlTabButton.Height));
                buttonControl.Location = point;

                pnlTabButton.Controls.Add(buttonControl);
                point.X += controlWidth;
                TabBtnControlList.Add(buttonControl);
            }

            if (TabBtnControlList.Count > 0)
                TabBtnControlList[0].UpdateData();

            _prevTabCount = tabCount;
        }

        private void ClearTabBtnList()
        {
            foreach (var btn in TabBtnControlList)
                btn.SetTabEventHandler -= ButtonControl_SetTabEventHandler;

            TabBtnControlList.Clear();
            pnlTabButton.Controls.Clear();
        }

        private void ButtonControl_SetTabEventHandler(int tabNum)
        {
            TabBtnControlList.ForEach(x => x.SetButtonClickNone());
            TabBtnControlList[tabNum].SetButtonClick();

            CurrentTabNo = tabNum;

            if (InspResultDic.ContainsKey(tabNum))
                AlignResultDisplayControl.UpdateResultDisplay(InspResultDic[tabNum]);
            else
                AlignResultDisplayControl.InspAlignDisplay.ClearImage();

            AlignResultControl.UpdateChart(tabNum);
        }

        private void lblResult_Click(object sender, EventArgs e)
        {
            SelectPage(PageType.Result);
        }

        private void lblLog_Click(object sender, EventArgs e)
        {
            SelectPage(PageType.Log);
        }

        private void ClearSelectItem()
        {
            foreach (Control control in tlpAlignData.Controls)
            {
                if (control is Label)
                    control.BackColor = _nonSelectedColor;
            }
        }

        private void SelectPage(PageType pageType)
        {
            ClearSelectItem();

            switch (pageType)
            {
                case PageType.Result:
                    ShowResult();
                    break;

                case PageType.Log:
                    ShowLog();
                    break;

                default:
                    break;
            }
        }

        private void ShowResult()
        {
            lblResult.BackColor = _selectedColor;
            AlignResultControl.Visible = true;
        }

        private void ShowLog()
        {
            lblLog.BackColor = _selectedColor;
            AlignResultControl.Visible = false;
        }

        public void UpdateMainResult(AppsInspResult result)
        {
            AlignResultDisplayControl.UpdateResultDisplay(result);
            //AlignResultControl.UpdateResult(tabNo: 0);
        }
    }
}
