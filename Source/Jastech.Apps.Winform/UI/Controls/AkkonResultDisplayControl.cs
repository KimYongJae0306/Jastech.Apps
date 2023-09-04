using Cognex.VisionPro;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Framework.Algorithms.Akkon.Results;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Winform.VisionPro.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public CogInspDisplayControl InspDisplayControl { get; private set; } = null; 

        public int CurrentTabNo { get; set; } = -1;

        private bool IsResultImageView { get; set; } = true;
        #endregion

        #region 이벤트
        public event SendTabNumberDelegate SendTabNumberEvent;

        public event GetTabInspResultDele GetTabInspResultEvent;
        #endregion

        #region 델리게이트
        public delegate void SendTabNumberDelegate(int tabNo);

        public delegate TabInspResult GetTabInspResultDele(int tabNo);
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
            InspDisplayControl = new CogInspDisplayControl();
            InspDisplayControl.Dock = DockStyle.Fill;
            InspDisplayControl.UseAllContextMenu(false);

            pnlInspDisplay.Controls.Add(InspDisplayControl);
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _noneSelectedColor = Color.FromArgb(52, 52, 52);

            UpdateButton();

            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            if (inspModel == null)
                UpdateTabCount(1);
            else
                UpdateTabCount(inspModel.TabCount);
        }

        public void Enable(bool isEnable)
        {
            InspDisplayControl.Enable(isEnable);
            BeginInvoke(new Action(() => Enabled = isEnable));
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
            InspDisplayControl.ClearImage();

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

        private void ButtonControl_SetTabEventHandler(int tabNo)
        {
            if (AppsStatus.Instance().IsInspRunnerFlagFromPlc)
                return;

            TabBtnControlList.ForEach(x => x.SetButtonClickNone());
            TabBtnControlList[tabNo].SetButtonClick();

            CurrentTabNo = tabNo;
            UpdateImage(tabNo);
            SendTabNumberEvent(tabNo);
        }

        public delegate void TabButtonResetColorDele();
        public void TabButtonResetColor()
        {
            if (this.InvokeRequired)
            {
                TabButtonResetColorDele callback = TabButtonResetColor;
                BeginInvoke(callback);
                return;
            }

            TabBtnControlList.ForEach(x => x.BackColor = Color.FromArgb(52, 52, 52));
        }

        private void UpdateImage(int tabNo)
        {
            //var tabInspResult = GetTabInspResultEvent?.Invoke(tabNo);
            var tabControl = TabBtnControlList[tabNo];
            if (tabControl.GetOrgImage() is ICogImage orgImage)
            {
                if (IsResultImageView)
                {
                    if (tabControl.GetCogResultImage() is ICogImage resultImage)
                        InspDisplayControl.SetImage(resultImage, tabControl.GetAkkonNGAffineRectList(), false);
                    else
                        InspDisplayControl.SetImage(orgImage, false);
                }
                else
                {
                    if(tabControl.GetCogInspImage() is ICogImage inspImage)
                        InspDisplayControl.SetImage(inspImage, false);
                    else
                        InspDisplayControl.SetImage(orgImage, false);
                }
            }
            else
            {
                InspDisplayControl.ClearGraphic();
                InspDisplayControl.ClearThumbnail();
            }
        }

        public void UpdateResultDisplay(int tabNo)
        {
            var tabInspResult = GetTabInspResultEvent?.Invoke(tabNo);

            TabBtnControlList[tabNo].SetOrgImage(tabInspResult.CogImage);
            TabBtnControlList[tabNo].SetResultImage(tabInspResult.AkkonResultCogImage);
            TabBtnControlList[tabNo].SetAkkonNGAffineRectList(tabInspResult.AkkonNGAffineList);
            TabBtnControlList[tabNo].SetInspImage(tabInspResult.AkkonInspCogImage);

            if (tabInspResult != null)
            {
                if (CurrentTabNo == tabNo)
                {
                    UpdateImage(tabNo);
                }
            }
        }

        public delegate void UpdateTabButtonDele(int tabNo);
        public void UpdateResultTabButton(int tabNo)
        {
            if (this.InvokeRequired)
            {
                UpdateTabButtonDele callback = UpdateResultTabButton;
                BeginInvoke(callback, tabNo);
                return;
            }

            var tabInspResult = GetTabInspResultEvent?.Invoke(tabNo);

            if(tabInspResult != null)
            {
                if (tabInspResult.AkkonResult.Judgement == Judgement.OK)
                    TabBtnControlList[tabNo].BackColor = Color.MediumSeaGreen;
                else
                    TabBtnControlList[tabNo].BackColor = Color.Red;

                TabBtnControlList[tabNo].Enabled = true;
            }
        }

        private void btnResizeImage_Click(object sender, EventArgs e)
        {
            IsResultImageView = false;

            UpdateButton();
            UpdateImage(CurrentTabNo);
            SendTabNumberEvent(CurrentTabNo);
        }

        private void btnResultImage_Click(object sender, EventArgs e)
        {
            IsResultImageView = true;

            UpdateButton();
            UpdateImage(CurrentTabNo);
            SendTabNumberEvent(CurrentTabNo);
        }
        #endregion
    }
}
