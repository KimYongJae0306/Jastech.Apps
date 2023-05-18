using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Apps.Structure;
using Jastech.Framework.Winform.VisionPro.Controls;
using static Jastech.Apps.Winform.UI.Controls.ResultChartControl;
using Jastech.Apps.Structure.Data;
using Cognex.VisionPro;
using Emgu.CV;
using System.Runtime.InteropServices;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.VisionPro;
using System.IO;
using Jastech.Framework.Util.Helper;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Imaging.Result;
using System.Drawing.Text;
using Emgu.CV.Dnn;
using Jastech.Apps.Winform.Service;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class AlignInspDisplayControl : UserControl
    {
        #region 필드
        private int _prevTabCount = -1;

        private Color _selectedColor;

        private Color _noneSelectedColor;

        private AppsInspResult _curAppsInspResult = null;
        #endregion

        #region 속성
        public List<TabBtnControl> TabBtnControlList { get; private set; } = new List<TabBtnControl>();

        public CogInspAlignDisplayControl InspAlignDisplay { get; private set; } = new CogInspAlignDisplayControl();

        public AlignInspResultControl AlignInspResultControl { get; private set; } = new AlignInspResultControl();

        public ResultChartControl ResultChartControl { get; private set; } = new ResultChartControl();

        public Dictionary<int, TabInspResult> InspResultDic { get; set; } = new Dictionary<int, TabInspResult>();

        private int CurrentTabNo { get; set; } = -1;

        public DailyInfo DailyInfo = new DailyInfo();
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public AlignInspDisplayControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void AlignInspDisplayControl_Load(object sender, EventArgs e)
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _noneSelectedColor = Color.FromArgb(52, 52, 52);

            AddControls();

            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            if (inspModel == null)
                UpdateTabCount();
            else
                UpdateTabCount(inspModel.TabCount);

            DailyInfo.Load();

            UpdateDailyDataGridView(DailyInfo);
            UpdateDailyChart(DailyInfo, 0);
        }

        private void AddControls()
        {
            InspAlignDisplay.Dock = DockStyle.Fill;
            pnlInspDisplay.Controls.Add(InspAlignDisplay);

            AlignInspResultControl.Dock = DockStyle.Fill;
            pnlAlignResult.Controls.Add(AlignInspResultControl);

            ResultChartControl.Dock = DockStyle.Fill;
            ResultChartControl.SetInspChartType(InspChartType.Align);
            pnlAlignGraph.Controls.Add(ResultChartControl);
        }

        public void UpdateTabCount(int tabCount = 1)
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
            TabBtnControlList.ForEach(x => x.BackColor = _noneSelectedColor);
            TabBtnControlList[tabNum].BackColor = _selectedColor;

            CurrentTabNo = tabNum;

            if (InspResultDic.ContainsKey(tabNum))
            {
                UpdateLeftAlignResult(InspResultDic[tabNum]);
                UpdateRightAlignResult(InspResultDic[tabNum]);
            }
            else
            {
                InspAlignDisplay.ClearImage();
            }

            UpdateDailyChart(DailyInfo, tabNum);
        }

        public void UpdateMainResult(AppsInspResult inspResult)
        {
           // _curAppsInspResult = inspResult.DeepCopy();

            InspAlignDisplay.ClearImage();

            UpdateDailyInfo(inspResult);

            for (int i = 0; i < inspResult.TabResultList.Count(); i++)
            {
                int tabNo = inspResult.TabResultList[i].TabNo;

                UpdateDailyChart(DailyInfo, tabNo);

                if (InspResultDic.ContainsKey(tabNo))
                {
                    InspResultDic[tabNo].Dispose();
                    InspResultDic.Remove(tabNo);
                }

                InspResultDic.Add(tabNo, inspResult.TabResultList[i]);

                if(CurrentTabNo == tabNo)
                {
                    UpdateLeftAlignResult(inspResult.TabResultList[i]);
                    UpdateRightAlignResult(inspResult.TabResultList[i]);
                }
            }

            DailyInfo.Save();
        }

        private void UpdateDailyInfo(AppsInspResult inspResult)
        {
            foreach (var item in inspResult.TabResultList)
            {
                AlignDailyInfo alignInfo = new AlignDailyInfo();

                alignInfo.InspectionTime = inspResult.LastInspTime;
                alignInfo.PanelID = inspResult.Cell_ID;
                alignInfo.TabNo = item.TabNo;
                alignInfo.Judgement = item.Judgement;
                alignInfo.LX = item.LeftAlignX.ResultValue;
                alignInfo.LY = item.LeftAlignY.ResultValue;
                alignInfo.RX = item.RightAlignX.ResultValue;
                alignInfo.RY = item.RightAlignY.ResultValue;
                alignInfo.CX = item.CenterX;

                DailyInfo.AddAlignInfo(alignInfo);
            }

            UpdateDailyDataGridView(DailyInfo);
        }

        private void UpdateDailyDataGridView(DailyInfo dailyInfo)
        {
            AlignInspResultControl.UpdateAlignDaily(dailyInfo);
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

        //private void UpdateAlignResult()
        //{
        //    var resultList = GetTempAlignResultList();
        //    if (resultList.Count > 0)
        //    {
        //        for (int resultIndex = 0; resultIndex < resultList.Count; resultIndex++)
        //            AlignInspResultControl.UpdateAlignResult(resultList[resultIndex]);
        //    }
        //}

        //private void UpdateAlignChart()
        //{
        //    //ClearAlignChart();

        //    //var resultList = GetTempAlignResultList();
        //    //if (resultList.Count > 0)
        //    //{
        //    //    for (int resultIndex = 0; resultIndex < resultList.Count; resultIndex++)
        //    //        ResultChartControl.UpdateAlignChart(resultList[resultIndex].TabResultList[CurrentTabNo]);
        //    //}
        //}

        //private void ClearAlignChart()
        //{
        //    //ResultChartControl.ClearAkkonChart();
        //}

        //private void WriteAlignResultTempFile(AppsInspResult inspResult)
        //{
        //    string filePath = Path.Combine(AppsConfig.Instance().Path.Temp, @"Align.csv");

        //    if (File.Exists(filePath) == false)
        //    {
        //        AppsInspModel model = ModelManager.Instance().CurrentModel as AppsInspModel;

        //        List<string> header = new List<string>
        //        {
        //            "Time",
        //            "Panel"
        //        };

        //        for (int tabNo = 0; tabNo < model.TabCount; tabNo++)
        //        {
        //            header.Add("Tab");
        //            header.Add("Judge");
        //            header.Add("Lx");
        //            header.Add("Ly");
        //            header.Add("Rx");
        //            header.Add("Ry");
        //            header.Add("Cx");
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
        //        dataList.Add(item.AlignJudgment.ToString());
        //        dataList.Add(item.LeftAlignX.ResultValue.ToString("F2"));
        //        dataList.Add(item.LeftAlignY.ResultValue.ToString("F2"));
        //        dataList.Add(item.RightAlignX.ResultValue.ToString("F2"));
        //        dataList.Add(item.RightAlignY.ResultValue.ToString("F2"));
        //        dataList.Add(item.CenterX.ToString("F2"));
        //    }

        //    CSVHelper.WriteData(filePath, dataList);
        //}

        //private void CheckTempFileCount(string filePath)
        //{
        //    Tuple<string[], List<string[]>> readData = CSVHelper.ReadData(filePath);
        //    string[] header = readData.Item1;
        //    List<string[]> contents = readData.Item2;

        //    if (contents.Count >= AppsConfig.Instance().Operation.AlignResultCount)
        //    {
        //        contents.RemoveAt(0);
        //        CSVHelper.WriteAllData(filePath, header, contents);
        //    }
        //}

        //private void ReadAlignTempFile()
        //{
        //    List<AppsInspResult> inspResultList = new List<AppsInspResult>();

        //    string filePath = Path.Combine(AppsConfig.Instance().Path.Temp, @"Align.csv");

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
        //            int interval = 7;
        //            startIndex = startIndex + interval * tabNo;

        //            TabInspResult tabInspResult = new TabInspResult();

        //            tabInspResult.LeftAlignX = new AlignResult();
        //            tabInspResult.LeftAlignY = new AlignResult();
        //            tabInspResult.RightAlignX = new AlignResult();
        //            tabInspResult.RightAlignY = new AlignResult();

        //            tabInspResult.TabNo = Convert.ToInt32(contents[readLine][startIndex]);

        //            tabInspResult.LeftAlignX.ResultValue = Convert.ToSingle(contents[readLine][startIndex + 2].ToString());
        //            tabInspResult.LeftAlignY.ResultValue = Convert.ToSingle(contents[readLine][startIndex + 3].ToString());
        //            tabInspResult.RightAlignX.ResultValue = Convert.ToSingle(contents[readLine][startIndex + 4].ToString());
        //            tabInspResult.RightAlignY.ResultValue = Convert.ToSingle(contents[readLine][startIndex + 5].ToString());
        //            tabInspResult.CenterX = Convert.ToSingle(contents[readLine][startIndex + 6].ToString());

        //            inspResult.TabResultList.Add(tabInspResult);
        //        }

        //        inspResultList.Add(inspResult);
        //    }

        //    SetTempAlignResultList(inspResultList);
        //}

        //private List<AppsInspResult> _alignResultList { get; set; } = new List<AppsInspResult>();
        //private void SetTempAlignResultList(List<AppsInspResult> inspResultList)
        //{
        //    _alignResultList = new List<AppsInspResult>();
        //    _alignResultList = inspResultList.ToList();
        //}

        //private List<AppsInspResult> GetTempAlignResultList()
        //{
        //    return _alignResultList;
        //}

        public void InitalizeResultData(int tabCount)
        {
            if (InspResultDic.Count() > 0)
            {
                for (int i = 0; i < InspResultDic.Count(); i++)
                {
                    if(InspResultDic.ContainsKey(i))
                    {
                        
                        InspResultDic[i]?.Dispose();
                        InspResultDic[i] = null;
                    }
                }
            }
            InspResultDic.Clear();
        }

        private void UpdateLeftAlignResult(TabInspResult result)
        {
            List<CogCompositeShape> leftResultList = new List<CogCompositeShape>();
            List<PointF> pointList = new List<PointF>();

            var leftAlignX = result.LeftAlignX;
            if (leftAlignX != null)
            {
                if (leftAlignX.Fpc.CogAlignResult.Count > 0)
                {
                    foreach (var fpc in leftAlignX.Fpc.CogAlignResult)
                    {
                        pointList.Add(fpc.MaxCaliperMatch.FoundPos);

                        var leftFpcX = fpc.MaxCaliperMatch.ResultGraphics;
                        leftResultList.Add(leftFpcX);
                    }
                }
                if (leftAlignX.Panel.CogAlignResult.Count() > 0)
                {
                    foreach (var panel in leftAlignX.Panel.CogAlignResult)
                    {
                        pointList.Add(panel.MaxCaliperMatch.FoundPos);

                        var leftPanelX = panel.MaxCaliperMatch.ResultGraphics;
                        leftResultList.Add(leftPanelX);
                    }
                }
            }

            var leftAlignY = result.LeftAlignY;
            if(leftAlignY != null)
            {
                if(leftAlignY.Fpc.CogAlignResult.Count >0)
                {
                    if (leftAlignY.Fpc.CogAlignResult[0].MaxCaliperMatch != null)
                    {
                        pointList.Add(leftAlignY.Fpc.CogAlignResult[0].MaxCaliperMatch.FoundPos);

                        var leftFpcY = leftAlignY.Fpc.CogAlignResult[0].MaxCaliperMatch.ResultGraphics;
                        leftResultList.Add(leftFpcY);
                    }
                }
                
                if(leftAlignY.Panel.CogAlignResult.Count > 0)
                {
                    if (leftAlignY.Panel.CogAlignResult[0].MaxCaliperMatch != null)
                    {
                        pointList.Add(leftAlignY.Panel.CogAlignResult[0].MaxCaliperMatch.FoundPos);
                        var leftPanelY = leftAlignY.Panel.CogAlignResult[0].MaxCaliperMatch.ResultGraphics;
                        leftResultList.Add(leftPanelY);
                    }
                }
            }

            var deepCopyImage = result.CogImage.CopyBase(CogImageCopyModeConstants.CopyPixels);
            InspAlignDisplay.UpdateLeftDisplay(deepCopyImage, leftResultList, GetCenterPoint(pointList));
        }

        private void UpdateRightAlignResult(TabInspResult result)
        {
            List<CogCompositeShape> rightResultList = new List<CogCompositeShape>();
            List<PointF> pointList = new List<PointF>();

            var rightAlignX = result.RightAlignX;
            if(rightAlignX != null)
            {
                if (rightAlignX.Fpc.CogAlignResult.Count > 0)
                {
                    foreach (var fpc in rightAlignX.Fpc.CogAlignResult)
                    {
                        pointList.Add(fpc.MaxCaliperMatch.FoundPos);

                        var rightFpcX = fpc.MaxCaliperMatch.ResultGraphics;
                        rightResultList.Add(rightFpcX);
                    }
                }
                if (rightAlignX.Panel.CogAlignResult.Count() > 0)
                {
                    foreach (var panel in rightAlignX.Panel.CogAlignResult)
                    {
                        pointList.Add(panel.MaxCaliperMatch.FoundPos);

                        var rightPanelX = panel.MaxCaliperMatch.ResultGraphics;
                        rightResultList.Add(rightPanelX);
                    }
                }
            }

            var rightAlignY = result.RightAlignY;
            if(rightAlignY != null)
            {
                if(rightAlignY.Fpc.CogAlignResult.Count > 0)
                {
                    if (rightAlignY.Fpc.CogAlignResult[0].MaxCaliperMatch != null)
                    {
                        pointList.Add(rightAlignY.Fpc.CogAlignResult[0].MaxCaliperMatch.FoundPos);

                        var rightFpcY = rightAlignY.Fpc.CogAlignResult[0].MaxCaliperMatch.ResultGraphics;
                        rightResultList.Add(rightFpcY);
                    }
                }
                
                if(rightAlignY.Panel.CogAlignResult.Count >0 )
                {
                    if (rightAlignY.Panel.CogAlignResult[0].MaxCaliperMatch != null)
                    {
                        pointList.Add(rightAlignY.Panel.CogAlignResult[0].MaxCaliperMatch.FoundPos);
                        var rightPanelY = rightAlignY.Panel.CogAlignResult[0].MaxCaliperMatch.ResultGraphics;
                        rightResultList.Add(rightPanelY);
                    }
                }
            }
            var deepCopyImage = result.CogImage.CopyBase(CogImageCopyModeConstants.CopyPixels);
            InspAlignDisplay.UpdateRightDisplay(deepCopyImage, rightResultList, GetCenterPoint(pointList));
        }

        private Point GetCenterPoint(List<PointF> pointList)
        {
            if (pointList == null)
                return new Point();

            if (pointList.Count == 0)
                return new Point();

            float minX = pointList.Select(point => point.X).Min();
            float maxX = pointList.Select(point => point.X).Max();

            float minY = pointList.Select(point => point.Y).Min();
            float maxY = pointList.Select(point => point.Y).Max();

            float width = (maxX - minX) / 2.0f;
            float height = (maxY - minY) / 2.0f;

            return new Point((int)(minX + width), (int)(minY + height));
        }

        public ICogImage ConvertCogImage(Mat image)
        {
            if (image == null)
                return null;

            int size = image.Width * image.Height * image.NumberOfChannels;
            byte[] dataArray = new byte[size];
            Marshal.Copy(image.DataPointer, dataArray, 0, size);

            ColorFormat format = image.NumberOfChannels == 1 ? ColorFormat.Gray : ColorFormat.RGB24;

            var cogImage = CogImageHelper.CovertImage(dataArray, image.Width, image.Height, format);

            return cogImage;
        }
        #endregion
    }
}
