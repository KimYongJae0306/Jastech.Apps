using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Service;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Framework.Winform.VisionPro.Controls;
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
    public partial class AkkonResultDisplayControl : UserControl
    {
        #region 필드
        private int _prevTabCount { get; set; } = -1;

        private Color _selectedColor;

        private Color _nonSelectedColor;
        #endregion

        #region 속성
        public List<TabBtnControl> TabBtnControlList { get; private set; } = new List<TabBtnControl>();

        public Dictionary<int, TabInspResult> InspResultDic { get; set; } = new Dictionary<int, TabInspResult>();

        public CogInspDisplayControl InspDisplayControl { get; private set; } = new CogInspDisplayControl() { Dock = DockStyle.Fill };

        public int CurrentTabNo { get; set; } = -1;
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public AkkonResultDisplayControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void AkkonResultDisplayControl_Load(object sender, EventArgs e)
        {
            AddControl();
            InitializeUI();
        }

        private void AddControl()
        {
            pnlInspDisplay.Controls.Add(InspDisplayControl);
        }

        private void InitializeUI()
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
            {
                btn.SetTabEventHandler -= ButtonControl_SetTabEventHandler;
            }
            TabBtnControlList.Clear();
            pnlTabButton.Controls.Clear();
        }

        private void ButtonControl_SetTabEventHandler(int tabNum)
        {
            TabBtnControlList.ForEach(x => x.SetButtonClickNone());
            TabBtnControlList[tabNum].SetButtonClick();

            CurrentTabNo = tabNum;

            if (InspResultDic.ContainsKey(tabNum))
                InspDisplayControl.SetImage(InspResultDic[tabNum].AkkonResultImage);
            else
                InspDisplayControl.Clear();
        }

        public void UpdateResultDisplay(AppsInspResult inspResult)
        {
            InspDisplayControl.Clear();

            for (int i = 0; i < inspResult.TabResultList.Count(); i++)
            {
                int tabNo = inspResult.TabResultList[i].TabNo;

                if (InspResultDic.ContainsKey(tabNo))
                {
                    InspResultDic[tabNo].Dispose();
                    InspResultDic.Remove(tabNo);
                }

                InspResultDic.Add(tabNo, inspResult.TabResultList[i]);

                if (CurrentTabNo == tabNo)
                    InspDisplayControl.SetImage(inspResult.TabResultList[i].AkkonResultImage);
            }
        }
        #endregion
    }
}
