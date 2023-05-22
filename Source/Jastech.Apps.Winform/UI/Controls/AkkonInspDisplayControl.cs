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
using Jastech.Framework.Macron.Akkon.Results;
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

        public DailyInfo DailyInfo = new DailyInfo();
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

            DailyInfo.Load();

            //UpdateDailyDataGridView(DailyInfo);
            UpdateDailyChart(DailyInfo, 0);
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

            UpdateDailyChart(DailyInfo, tabNum);
        }

        public void UpdateMainResult(AppsInspResult inspResult)
        {
            InspDisplayControl.Clear();

            //UpdateDailyInfo(inspResult);

            for (int i = 0; i < inspResult.TabResultList.Count(); i++)
            {
                int tabNo = inspResult.TabResultList[i].TabNo;

                //UpdateDailyChart(DailyInfo, tabNo);

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

            DailyInfo.Save();
        }

        private void UpdateDailyInfo(AppsInspResult inspResult)
        {
            foreach (var item in inspResult.TabResultList)
            {
                AkkonDailyInfo akkonInfo = new AkkonDailyInfo();

                akkonInfo.InspectionTime = inspResult.LastInspTime;
                akkonInfo.PanelID = inspResult.Cell_ID;
                akkonInfo.TabNo = item.TabNo;
                akkonInfo.Judgement = item.Judgement;
                akkonInfo.AvgBlobCount = item.AkkonResult.AvgBlobCount;
                akkonInfo.AvgLength = item.AkkonResult.AvgLength;
                akkonInfo.AvgStrength = item.AkkonResult.AvgStrength;
                akkonInfo.AvgSTD = item.AkkonResult.AvgStd;

                DailyInfo.AddAkkonInfo(akkonInfo);
            }

            //UpdateDailyDataGridView(DailyInfo);
        }

        private void UpdateDailyDataGridView(DailyInfo dailyInfo)
        {
            AkkonInspResultControl.UpdateAkkonDaily(dailyInfo);
        }

        private void UpdateDailyChart(DailyInfo dailyInfo, int tabNo)
        {
            if (dailyInfo == null)
                return;

            if (dailyInfo.AkkonDailyInfoList.Count > 0)
                ResultChartControl.UpdateAkkonDaily(dailyInfo.AkkonDailyInfoList[tabNo]);
        }

        private void ClearAkkonChart()
        {
            ResultChartControl.ClearChart();
        }

        //private void WriteAkkonTempFile(AppsInspResult inspResult)
        //{
        //    string filePath = Path.Combine(AppsConfig.Instance().Path.Temp, @"Akkon.csv");

        //    AppsInspModel model = ModelManager.Instance().CurrentModel as AppsInspModel;

        //    if (File.Exists(filePath) == false)
        //    {
        //        List<string> header = new List<string>
        //        {
        //            "Time",
        //            "Panel",
        //        };

        //        for (int tabNo = 0; tabNo < model.TabCount; tabNo++)
        //        {
        //            header.Add("Tab");
        //            header.Add("Judge");
        //            header.Add("Count");
        //            header.Add("Length");
        //            header.Add("Strength");
        //            header.Add("STD");
        //        }

        //        CSVHelper.WriteHeader(filePath, header);
        //    }

        //    CheckTempFileCount(filePath);

        //    List<string> dataList = new List<string>
        //    {
        //        inspResult.LastInspTime.ToString(),
        //        inspResult.Cell_ID.ToString()
        //    };

        //    foreach (var item in inspResult.TabResultList)
        //    {
        //        dataList.Add(item.TabNo.ToString());
        //        dataList.Add(item.AkkonResult.Judgement.ToString());
        //        dataList.Add(item.AkkonResult.AvgBlobCount.ToString());
        //        dataList.Add(item.AkkonResult.AvgLength.ToString());
        //        dataList.Add(item.AkkonResult.AvgStrength.ToString());
        //        dataList.Add(item.AkkonResult.AvgStd.ToString());
        //    }

        //    CSVHelper.WriteData(filePath, dataList);
        //}

        //private void CheckTempFileCount(string filePath)
        //{
        //    Tuple<string[], List<string[]>> readData = CSVHelper.ReadData(filePath);
        //    string[] header = readData.Item1;
        //    List<string[]> contents = readData.Item2;

        //    if (contents.Count >= AppsConfig.Instance().Operation.AkkonResultCount)
        //    {
        //        contents.RemoveAt(0);
        //        CSVHelper.WriteAllData(filePath, header, contents);
        //    }
        //}

        //private void ReadAkkonTempFile()
        //{
        //    List<AppsInspResult> inspResultList = new List<AppsInspResult>();

        //    string filePath = Path.Combine(AppsConfig.Instance().Path.Temp, @"Akkon.csv");

        //    if (File.Exists(filePath) == false)
        //        return;

        //    Tuple<string[], List<string[]>> readData = CSVHelper.ReadData(filePath);
        //    List<string[]> contents = readData.Item2;

        //    AppsInspModel model = ModelManager.Instance().CurrentModel as AppsInspModel;

        //    for (int readLine = 0; readLine < contents.Count; readLine++)
        //    {
        //        AppsInspResult inspResult = new AppsInspResult();

        //        inspResult.LastInspTime = contents[readLine][0].ToString();
        //        inspResult.Cell_ID = contents[readLine][1].ToString();

        //        for (int tabNo = 0; tabNo < model.TabCount; tabNo++)
        //        {
        //            int startIndex = 2;
        //            int interval = 6;
        //            startIndex = startIndex + interval * tabNo;

        //            TabInspResult tabInspResult = new TabInspResult();
        //            tabInspResult.AkkonResult = new AkkonResult();

        //            tabInspResult.AkkonResult.TabNo = Convert.ToInt32(contents[readLine][startIndex]);
        //            tabInspResult.AkkonResult.Judgement = (Judgement)Enum.Parse(typeof(Judgement), contents[readLine][startIndex + 1].ToString());
        //            tabInspResult.AkkonResult.AvgBlobCount = Convert.ToInt32(contents[readLine][startIndex + 2]);
        //            tabInspResult.AkkonResult.AvgLength = Convert.ToSingle(contents[readLine][startIndex + 3]);
        //            tabInspResult.AkkonResult.AvgStrength = Convert.ToSingle(contents[readLine][startIndex + 4]);
        //            tabInspResult.AkkonResult .AvgStd = Convert.ToSingle(contents[readLine][startIndex + 5]);

        //            inspResult.TabResultList.Add(tabInspResult);
        //        }

        //        inspResultList.Add(inspResult);
        //    }

        //    SetTempAkkonResultList(inspResultList);
        //}

        //private List<AppsInspResult> _akkonResultList { get; set; } = new List<AppsInspResult>();
        //private void SetTempAkkonResultList(List<AppsInspResult> inspResultList)
        //{
        //    _akkonResultList = new List<AppsInspResult>();
        //    _akkonResultList = inspResultList.ToList();
        //}

        //private List<AppsInspResult> GetTempAkkonResultList()
        //{
        //    return _akkonResultList;
        //}
        #endregion
    }
}
