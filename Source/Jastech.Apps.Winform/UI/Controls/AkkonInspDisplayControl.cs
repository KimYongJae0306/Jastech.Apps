using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Framework.Winform.VisionPro.Controls;
using Jastech.Apps.Structure;
using static Jastech.Apps.Winform.UI.Controls.ResultChartControl;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Util.Helper;
using System.IO;
using Jastech.Framework.Imaging.Result;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Emgu.CV.Flann;
using System.Runtime.InteropServices.ComTypes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Emgu.CV.Dnn;
using Jastech.Apps.Winform.Service;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AkkonInspDisplayControl : UserControl
    {
        #region 필드
        private int _prevTabCount { get; set; } = -1;

        private Color _selectedColor;

        private Color _nonSelectedColor;
        #endregion

        #region 속성
        public List<TabBtnControl> TabBtnControlList { get; private set; } = new List<TabBtnControl>();

        public CogInspDisplayControl InspDisplayControl { get; private set; } = new CogInspDisplayControl();

        public AkkonInspResultControl AkkonInspResultControl { get; private set; } = new AkkonInspResultControl();

        public ResultChartControl ResultChartControl { get; private set; } = new ResultChartControl();

        public Dictionary<int, TabInspResult> InspResultDic { get; set; } = new Dictionary<int, TabInspResult>();

        public int CurrentTabNo { get; set; } = -1;

        //public DailyInfo DailyInfo = new DailyInfo();
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public AkkonInspDisplayControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void AkkonInspControl_Load(object sender, EventArgs e)
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            AddControls();

            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            if (inspModel == null)
                UpdateTabCount(1);
            else
                UpdateTabCount(inspModel.TabCount);

            var dailyInfo = DailyInfoService.GetDailyInfo();
            foreach (var item in dailyInfo.DailyDataList)
                UpdateDailyDataGridView(item);
            UpdateDailyChart(dailyInfo, 0);
        }

        private void AddControls()
        {
            InspDisplayControl.Dock = DockStyle.Fill;
            pnlInspDisplay.Controls.Add(InspDisplayControl);

            AkkonInspResultControl.Dock = DockStyle.Fill;
            pnlAkkonResult.Controls.Add(AkkonInspResultControl);

            ResultChartControl.Dock = DockStyle.Fill;
            ResultChartControl.SetInspChartType(InspChartType.Akkon);
            pnlAkkonGraph.Controls.Add(ResultChartControl);
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
                buttonControl.Size = new Size(controlWidth, (int)(pnlTabButton.Height * 0.7));
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
            TabBtnControlList.ForEach(x => x.BackColor = _nonSelectedColor);
            TabBtnControlList[tabNum].BackColor = _selectedColor;

            CurrentTabNo = tabNum;

            if (InspResultDic.ContainsKey(tabNum))
            {
                InspDisplayControl.SetImage(InspResultDic[tabNum].AkkonResultImage);
            }
            else
            {
                InspDisplayControl.Clear();
            }

            var dailyInfo = DailyInfoService.GetDailyInfo();
            UpdateDailyChart(dailyInfo, tabNum);
        }

        public void UpdateMainResult(AppsInspResult inspResult)
        {
            InspDisplayControl.Clear();

            var dailyInfo = DailyInfoService.GetDailyInfo();

            UpdateDailyInfo(inspResult);

            for (int i = 0; i < inspResult.TabResultList.Count(); i++)
            {
                int tabNo = inspResult.TabResultList[i].TabNo;

                UpdateDailyChart(dailyInfo, tabNo);

                if (InspResultDic.ContainsKey(tabNo))
                {
                    InspResultDic[tabNo].Dispose();
                    InspResultDic.Remove(tabNo);
                }

                InspResultDic.Add(tabNo, inspResult.TabResultList[i]);

                if (CurrentTabNo == tabNo)
                {
                    InspDisplayControl.SetImage(inspResult.TabResultList[i].AkkonResultImage);
                }
            }

            //DailyInfoService.Save();
        }

        private void UpdateDailyInfo(AppsInspResult inspResult)
        {
            var dailyData = DailyInfoService.GetDailyData();
            dailyData.ClearAkkonInfo();

            foreach (var item in inspResult.TabResultList)
            {
                AkkonDailyInfo akkonInfo = new AkkonDailyInfo();

                akkonInfo.InspectionTime = inspResult.EndInspTime.ToString("HH:mm:ss");
                akkonInfo.PanelID = inspResult.Cell_ID;
                akkonInfo.TabNo = item.TabNo;
                akkonInfo.Judgement = item.Judgement;
                //akkonInfo.AvgBlobCount = item.MacronAkkonResult.AvgBlobCount;
                //akkonInfo.AvgLength = item.MacronAkkonResult.AvgLength;
                //akkonInfo.AvgStrength = item.MacronAkkonResult.AvgStrength;
                //akkonInfo.AvgSTD = item.MacronAkkonResult.AvgStd;

                akkonInfo.AvgBlobCount = 10;
                akkonInfo.AvgLength = 10;
                akkonInfo.AvgStrength = 10;
                akkonInfo.AvgSTD = 10;

                dailyData.AddAkkonInfo(akkonInfo);
            }

            UpdateDailyDataGridView(dailyData);
        }

        private void UpdateDailyDataGridView(DailyData dailyData)
        {
            AkkonInspResultControl.UpdateAkkonDaily(dailyData);
        }

        private void UpdateDailyChart(DailyInfo dailyInfo, int tabNo)
        {
            if (dailyInfo == null)
                return;

            if (dailyInfo.DailyDataList.Count > 0)
                ResultChartControl.UpdateAkkonDaily(dailyInfo, tabNo);
        }

        private void ClearAkkonChart()
        {
            ResultChartControl.ClearChart();
        }
        #endregion
    }
}
