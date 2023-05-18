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

        public List<AppsInspResult> ResultList = new List<AppsInspResult>();
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

            ReadAkkonTempFile();
            UpdateAkkonResult();
            UpdateAkkonChart();
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

            UpdateAkkonChart();
        }

        private AppsInspResult ParseInspResult(List<string> fileList, int index)
        {
            AppsInspResult result = new AppsInspResult();
            result.TabResultList = new List<TabInspResult>();

            AppsInspModel model = ModelManager.Instance().CurrentModel as AppsInspModel;

            for (int tabNo = 0; tabNo < model.TabCount; tabNo++)
            {
                TabInspResult tabResult = new TabInspResult();
                tabResult.AkkonResult = new AkkonResult();

                var panelResultPath = fileList[tabNo + index];

                Tuple<string[], List<string[]>> readData = CSVHelper.ReadData(panelResultPath);
                List<string[]> contents = readData.Item2;
                foreach (var item in contents)
                {
                    int searchIndex = panelResultPath.IndexOf("][");
                    result.Cell_ID = panelResultPath.Substring(searchIndex + 2, 8);
                    result.LastInspTime = item[0].ToString();

                    LeadResult leadResult = new LeadResult();

                    leadResult.BlobCount = Convert.ToInt32(item[3].ToString());
                    leadResult.Length = Convert.ToSingle(item[4].ToString());
                    leadResult.AvgStrength = Convert.ToSingle(item[5].ToString());
                    leadResult.LeadStdDev = Convert.ToSingle(item[7].ToString());

                    tabResult.AkkonResult.LeadResultList.Add(leadResult);
                }

                tabResult.TabNo = tabNo + 1;
                tabResult.AkkonResult.TabNo = tabNo + 1;
                tabResult.AkkonResult.AvgBlobCount = Convert.ToInt32(Math.Truncate(tabResult.AkkonResult.LeadResultList.Average(x => x.BlobCount)));
                tabResult.AkkonResult.AvgLength = tabResult.AkkonResult.LeadResultList.Average(x => x.Length);

                result.TabResultList.Add(tabResult);
            }

            return result;
        }

        //private List<AppsInspResult> LoadResult()
        //{
        //    List<AppsInspResult> inspResultList = new List<AppsInspResult>();

        //    AppsInspModel model = ModelManager.Instance().CurrentModel as AppsInspModel;

        //    string dataFilePath = Path.Combine(AppsConfig.Instance().Path.Result + @"\Akkon");

        //    string[] files = Directory.GetFiles(dataFilePath);

        //    List<string> sortFileList = files.OrderBy(x => x).ToList();

        //    for (int index = 0; index < sortFileList.Count; index += model.TabCount)
        //    {
        //        AppsInspResult inspResult = new AppsInspResult();

        //        inspResult = ParseInspResult(sortFileList, index);

        //        inspResultList.Add(inspResult);
        //    }

        //    return CheckResultCount(AppsConfig.Instance().Operation.AkkonResultCount, inspResultList.ToList());
        //}

        //private List<AppsInspResult> CheckResultCount(int maximumCount, List<AppsInspResult> inspResultList)
        //{
        //    if (inspResultList.Count <= 0)
        //        return null;

        //    if (inspResultList.Count > maximumCount)
        //        inspResultList.RemoveRange(0, inspResultList.Count - maximumCount);

        //    return inspResultList;
        //}

        public void UpdateMainResult(AppsInspResult inspResult)
        {
            InspDisplayControl.Clear();

            WriteAkkonTempFile(inspResult);

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
                {
                    InspDisplayControl.SetImage(inspResult.TabResultList[i].AkkonResultImage);
                }
            }

            ReadAkkonTempFile();
        }

        private void UpdateAkkonResult()
        {
            var resultList = GetTempAkkonResultList();
            if (resultList.Count > 0)
            {
                for (int resultIndex = 0; resultIndex < resultList.Count; resultIndex++)
                    AkkonInspResultControl.UpdateAkkonResult(resultList[resultIndex]);
            }
        }

        private void UpdateAkkonChart()
        {
            ClearAkkonChart();

            var resultList = GetTempAkkonResultList();
            if (resultList.Count > 0)
            {
                for (int resultIndex = 0; resultIndex < resultList.Count; resultIndex++)
                    ResultChartControl.UpdateAkkonChart(resultList[resultIndex].TabResultList[CurrentTabNo]);
            }
        }

        private void ClearAkkonChart()
        {
            ResultChartControl.ClearChart();
        }

        //private void WriteAkkonResult(string filePath, int tabNo, AppsInspResult inspResult)
        //{
        //    // TEST
        //    filePath = Path.Combine(AppsConfig.Instance().Path.Result, @"Akkon\");
        //    filePath += DateTime.Now.ToString("[yyyyMMdd_HHmmss]") + "[CellID]Akkon_Tab" + tabNo + ".csv";

        //    List<string> header = new List<string>
        //    {
        //        "Inspection Time",
        //        "Cell ID",
        //        "Bump No.",
        //        "Count",
        //        "Length",
        //        "Strength",
        //        "Judgement",
        //        "STD"
        //    };

        //    CSVHelper.WriteHeader(filePath, header);

        //    List<List<string>> dataList = new List<List<string>>();

        //    foreach (var item in inspResult.TabResultList[tabNo].AkkonResult.LeadResultList)
        //    {
        //        List<string> datas = new List<string>
        //        {
        //            inspResult.LastInspTime.ToString(),
        //            inspResult.Cell_ID.ToString(),
        //            item.Id.ToString(),
        //            item.BlobCount.ToString(),
        //            item.Length.ToString("F2"),
        //            item.AvgStrength.ToString("F2")
        //        };

        //        if (item.IsGood)
        //            datas.Add(Judgement.OK.ToString());
        //        else
        //            datas.Add(Judgement.NG.ToString());

        //        datas.Add(item.LeadStdDev.ToString());

        //        dataList.Add(datas);
        //    }

        //    CSVHelper.WriteData(filePath, dataList);
        //}


        private void WriteAkkonTempFile(AppsInspResult inspResult)
        {
            string filePath = Path.Combine(AppsConfig.Instance().Path.Temp, @"Akkon.csv");

            AppsInspModel model = ModelManager.Instance().CurrentModel as AppsInspModel;

            if (File.Exists(filePath) == false)
            {
                List<string> header = new List<string>
                {
                    "Time",
                    "Panel",
                };

                for (int tabNo = 0; tabNo < model.TabCount; tabNo++)
                {
                    header.Add("Tab");
                    header.Add("Judge");
                    header.Add("Count");
                    header.Add("Length");
                    header.Add("Strength");
                    header.Add("STD");
                }

                CSVHelper.WriteHeader(filePath, header);
            }

            List<string> dataList = new List<string>
            {
                inspResult.LastInspTime.ToString(),
                inspResult.Cell_ID.ToString()
            };

            foreach (var item in inspResult.TabResultList)
            {
                dataList.Add(item.TabNo.ToString());
                dataList.Add(item.AkkonResult.Judgement.ToString());
                dataList.Add(item.AkkonResult.AvgBlobCount.ToString());
                dataList.Add(item.AkkonResult.AvgLength.ToString());
                dataList.Add(item.AkkonResult.AvgStrength.ToString());
                dataList.Add(item.AkkonResult.AvgStd.ToString());
            }

            CSVHelper.WriteData(filePath, dataList);
        }

        //private List<string> CheckTempFileCount(List<string> inputData)
        //{
        //    string filePath = Path.Combine(AppsConfig.Instance().Path.Temp, @"Akkon.csv");

        //    AppsInspModel model = ModelManager.Instance().CurrentModel as AppsInspModel;

        //    Tuple<string[], List<string[]>> readData = CSVHelper.ReadData(filePath);
        //    List<string[]> contents = readData.Item2;

        //    if (contents.Count >= AppsConfig.Instance().Operation.AkkonResultCount)
        //        contents.RemoveRange(0, model.TabCount);
        //}

        private void ReadAkkonTempFile()
        {
            List<AppsInspResult> inspResultList = new List<AppsInspResult>();

            string filePath = Path.Combine(AppsConfig.Instance().Path.Temp, @"Akkon.csv");

            if (File.Exists(filePath) == false)
                return;

            Tuple<string[], List<string[]>> readData = CSVHelper.ReadData(filePath);
            List<string[]> contents = readData.Item2;

            AppsInspModel model = ModelManager.Instance().CurrentModel as AppsInspModel;

            for (int readLine = 0; readLine < contents.Count; readLine++)
            {
                AppsInspResult inspResult = new AppsInspResult();

                inspResult.LastInspTime = contents[readLine][0].ToString();
                inspResult.Cell_ID = contents[readLine][1].ToString();

                for (int tabNo = 0; tabNo < model.TabCount; tabNo++)
                {
                    int startIndex = 2;
                    int interval = 6;
                    startIndex = startIndex + interval * tabNo;

                    TabInspResult tabInspResult = new TabInspResult();
                    tabInspResult.AkkonResult = new AkkonResult();

                    tabInspResult.AkkonResult.TabNo = Convert.ToInt32(contents[readLine][startIndex]);
                    tabInspResult.AkkonResult.Judgement = (Judgement)Enum.Parse(typeof(Judgement), contents[readLine][startIndex + 1].ToString());
                    tabInspResult.AkkonResult.AvgBlobCount = Convert.ToInt32(contents[readLine][startIndex + 2]);
                    tabInspResult.AkkonResult.AvgLength = Convert.ToSingle(contents[readLine][startIndex + 3]);
                    tabInspResult.AkkonResult.AvgStrength = Convert.ToSingle(contents[readLine][startIndex + 4]);
                    tabInspResult.AkkonResult .AvgStd = Convert.ToSingle(contents[readLine][startIndex + 5]);

                    inspResult.TabResultList.Add(tabInspResult);
                }

                inspResultList.Add(inspResult);
            }

            SetTempAkkonResultList(inspResultList);
        }

        private List<AppsInspResult> _resultList { get; set; } = new List<AppsInspResult>();
        private void SetTempAkkonResultList(List<AppsInspResult> inspResultList)
        {
            _resultList = new List<AppsInspResult>();
            _resultList = inspResultList.ToList();
        }

        private List<AppsInspResult> GetTempAkkonResultList()
        {
            return _resultList;
        }
        #endregion
    }
}
