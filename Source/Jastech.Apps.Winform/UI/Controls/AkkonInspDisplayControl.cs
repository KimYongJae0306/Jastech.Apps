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

            ResultList = LoadResult();

            ClearAkkonChart();

            for (int resultCount = ResultList.Count - 1; resultCount >= 0; resultCount--)
            {
                UpdateAkkonResult(ResultList[resultCount]);
                UpdateAkkonChart(ResultList[resultCount].TabResultList[0]);
            }
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

            ClearAkkonChart();

            for (int resultCount = 0; resultCount < ResultList.Count; resultCount++)
                UpdateAkkonChart(ResultList[resultCount].TabResultList[tabNum]);
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

                var panelResult = fileList[tabNo + index];

                Tuple<string[], List<string[]>> readData = CSVHelper.ReadData(panelResult);
                List<string[]> contents = readData.Item2;
                foreach (var item in contents)
                {
                    int searchIndex = panelResult.IndexOf("][");
                    result.Cell_ID = panelResult.Substring(searchIndex + 2, 8);
                    result.LastInspTime = item[0].ToString();

                    LeadResult leadResult = new LeadResult();

                    leadResult.BlobCount = Convert.ToInt32(item[3].ToString());
                    leadResult.Length = (float)Convert.ToDouble(item[4].ToString());
                    leadResult.AvgStrength = (float)Convert.ToDouble(item[5].ToString());
                    leadResult.LeadStdDev = (float)Convert.ToDouble(item[7].ToString());

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

        private List<AppsInspResult> LoadResult()
        {
            List<AppsInspResult> inspResultList = new List<AppsInspResult>();

            AppsInspModel model = ModelManager.Instance().CurrentModel as AppsInspModel;

            string dataFilePath = Path.Combine(AppsConfig.Instance().Path.Result + @"\Akkon");

            string[] files = Directory.GetFiles(dataFilePath);

            List<string> sortFileList = files.OrderBy(x => x).ToList();

            for (int index = 0; index < sortFileList.Count; index += model.TabCount)
            {
                AppsInspResult inspResult = new AppsInspResult();

                inspResult = ParseInspResult(sortFileList, index);

                inspResultList.Add(inspResult);
            }

            return CheckResultCount(AppsConfig.Instance().Operation.AkkonResultCount, inspResultList.ToList());
        }

        private List<AppsInspResult> CheckResultCount(int maximumCount, List<AppsInspResult> inspResultList)
        {
            if (inspResultList.Count <= 0)
                return null;

            if (inspResultList.Count > maximumCount)
                inspResultList.RemoveRange(0, inspResultList.Count - maximumCount);

            return inspResultList;
        }

        public void UpdateMainResult(AppsInspResult inspResult)
        {
            InspDisplayControl.Clear();

            ResultList.Add(inspResult);
            for (int i = 0; i < inspResult.TabResultList.Count(); i++)
            {
                int tabNo = inspResult.TabResultList[i].TabNo;

                WriteAkkonResult(null, tabNo, inspResult);

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
        }

        private void UpdateAkkonResult(AppsInspResult inspResult)
        {
            AkkonInspResultControl.UpdateAkkonResult(inspResult);
        }

        private void UpdateAkkonChart(TabInspResult tabInspResult)
        {
            ResultChartControl.UpdateAkkonChart(tabInspResult);
        }

        private void ClearAkkonChart()
        {
            ResultChartControl.ClearChart();
        }

        private void WriteAkkonResult(string filePath, int tabNo, AppsInspResult inspResult)
        {
            // TEST
            filePath = Path.Combine(AppsConfig.Instance().Path.Result, @"Akkon\");
            filePath += DateTime.Now.ToString("[yyyyMMdd_HHmmss]") + "[CellID]Akkon_Tab" + tabNo + ".csv";

            List<string> header = new List<string>
            {
                "Inspection Time",
                "Cell ID",
                "Bump No.",
                "Count",
                "Length",
                "Strength",
                "Judgement",
                "STD"
            };

            CSVHelper.WriteHeader(filePath, header);

            List<string> data = new List<string>
            {
                inspResult.LastInspTime.ToString(),
                inspResult.Cell_ID.ToString()
            };

            foreach (var item in inspResult.TabResultList)
            {
                data.Add(item.AkkonResult.LeadResultList[tabNo].Id.ToString());
                data.Add(item.AkkonResult.LeadResultList[tabNo].BlobCount.ToString());
                data.Add(item.AkkonResult.LeadResultList[tabNo].Length.ToString());
                data.Add(item.AkkonResult.LeadResultList[tabNo].AvgStrength.ToString());

                if (item.AkkonResult.LeadResultList[tabNo].IsGood)
                    data.Add(Judgement.OK.ToString());
                else
                    data.Add(Judgement.NG.ToString());

                data.Add(item.AkkonResult.LeadResultList[tabNo].LeadStdDev.ToString());
            }

            CSVHelper.WriteData(filePath, header);
        }
        #endregion
    }
}
