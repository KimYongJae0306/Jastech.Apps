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

            for (int resultCount = 0; resultCount < ResultList.Count; resultCount++)
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

            int maximumCount = 100;
            List<string> sortFileList = files.OrderByDescending(x => x).Take(model.TabCount * maximumCount).ToList();
            sortFileList.Reverse();

            for (int index = 0; index < maximumCount * model.TabCount; index += model.TabCount)
            {
                AppsInspResult inspResult = new AppsInspResult();

                inspResult = ParseInspResult(sortFileList, index);

                inspResultList.Add(inspResult);
            }

            return inspResultList;
        }

        //private List<TabInspResult> CheckResultCount(int maximumCount, List<TabInspResult> resultList)
        //{
        //    if (resultList.Count <= 0)
        //        return null;

        //    if (resultList.Count > maximumCount)
        //        resultList.RemoveRange(0, resultList.Count - maximumCount);

        //    return resultList;
        //}

        //private TabInspResult AdjustData(int tabNo, string[] datas)
        //{
        //    TabInspResult data = new TabInspResult();

        //    data.LeftAlignX = new AlignResult();
        //    data.LeftAlignY = new AlignResult();
        //    data.RightAlignX = new AlignResult();
        //    data.RightAlignY = new AlignResult();

        //    int startIndex = 2;
        //    int interval = 7;
        //    startIndex = startIndex + interval * tabNo;

        //    data.TabNo = Convert.ToInt32(datas[startIndex].ToString());
        //    data.Judgement = (Judgement)Enum.Parse(typeof(Judgement), datas[startIndex + 1].ToString());

        //    data.LeftAlignX.ResultValue = (float)Convert.ToDouble(datas[startIndex + 2].ToString());
        //    data.LeftAlignY.ResultValue = (float)Convert.ToDouble(datas[startIndex + 3].ToString());
        //    data.RightAlignX.ResultValue = (float)Convert.ToDouble(datas[startIndex + 4].ToString());
        //    data.RightAlignY.ResultValue = (float)Convert.ToDouble(datas[startIndex + 5].ToString());
        //    data.CenterX = (float)Convert.ToDouble(datas[startIndex + 6].ToString());

        //    return data;
        //}

        public void UpdateMainResult(AppsInspResult inspResult)
        {
            InspDisplayControl.Clear();

            ResultList.Add(inspResult);

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
        #endregion
    }
}
