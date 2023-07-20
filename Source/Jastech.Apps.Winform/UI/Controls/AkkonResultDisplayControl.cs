using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Framework.Winform.VisionPro.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ATT_UT_IPAD.UI.Controls
{
    public partial class AkkonResultDisplayControl : UserControl
    {
        #region 필드
        private int _prevTabCount { get; set; } = -1;

        private Color _selectedColor;

        private Color _noneSelectedColor;

        #endregion

        #region 속성
        public List<TabBtnControl> TabBtnControlList { get; private set; } = new List<TabBtnControl>();

        public Dictionary<int, TabInspResult> InspResultDic { get; set; } = new Dictionary<int, TabInspResult>();

        public CogInspDisplayControl InspDisplayControl { get; private set; } = new CogInspDisplayControl() { Dock = DockStyle.Fill };

        public int CurrentTabNo { get; set; } = -1;

        private bool IsResultImageView { get; set; } = true;
        #endregion

        #region 이벤트
        public event SendTabNumberDelegate SendTabNumber;
        #endregion

        #region 델리게이트
        public delegate void SendTabNumberDelegate(int tabNumber);
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
            _noneSelectedColor = Color.FromArgb(52, 52, 52);

            btnResultImage.Focus();
            UpdateButton();

            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            if (inspModel == null)
                UpdateTabCount(1);
            else
                UpdateTabCount(inspModel.TabCount);
        }

        private void UpdateButton()
        {
            if (IsResultImageView)
            {
                btnResizeImage.BackColor = _noneSelectedColor;
                btnResultImage.BackColor = _selectedColor;
            }
            else
            {
                btnResizeImage.BackColor = _selectedColor;
                btnResultImage.BackColor = _noneSelectedColor;
            }
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
            SendTabNumber(tabNum);

            if (InspResultDic.ContainsKey(tabNum))
            {
                if(IsResultImageView)
                    InspDisplayControl.SetImage(InspResultDic[tabNum].AkkonResultImage);
                else
                    InspDisplayControl.SetImage(InspResultDic[tabNum].AkkonInspImage);
            }
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

                InspResultDic.Add(tabNo, inspResult.TabResultList[i].DeepCopy());

                if (CurrentTabNo == tabNo)
                {
                    if (IsResultImageView)
                        InspDisplayControl.SetImage(inspResult.TabResultList[i].AkkonResultImage);
                    else
                        InspDisplayControl.SetImage(inspResult.TabResultList[i].AkkonInspImage);
                }
            }
        }

        private void btnResizeImage_Click(object sender, EventArgs e)
        {
            IsResultImageView = false;
            UpdateButton();

            SendTabNumber(CurrentTabNo);

            if (InspResultDic.ContainsKey(CurrentTabNo))
            {
                if (IsResultImageView)
                    InspDisplayControl.SetImage(InspResultDic[CurrentTabNo].AkkonResultImage);
                else
                    InspDisplayControl.SetImage(InspResultDic[CurrentTabNo].AkkonInspImage);
            }
            else
                InspDisplayControl.Clear();
        }

        private void btnResultImage_Click(object sender, EventArgs e)
        {
            IsResultImageView = true;
            UpdateButton();

            SendTabNumber(CurrentTabNo);

            if (InspResultDic.ContainsKey(CurrentTabNo))
            {
                if (IsResultImageView)
                    InspDisplayControl.SetImage(InspResultDic[CurrentTabNo].AkkonResultImage);
                else
                    InspDisplayControl.SetImage(InspResultDic[CurrentTabNo].AkkonInspImage);
            }
            else
                InspDisplayControl.Clear();
        }
        #endregion
    }
}
