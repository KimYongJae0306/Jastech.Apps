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

        public List<AppsInspResult> ResultList = new List<AppsInspResult>();
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

            ResultList = LoadResult();

            ClearAlignChart();

            for (int resultCount = ResultList.Count - 1; resultCount >= 0 ; resultCount--)
            {
                UpdateAlignResult(ResultList[resultCount]);
                UpdateAlignChart(ResultList[resultCount].TabResultList[0]);
            }
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

            ClearAlignChart();

            for (int resultCount = 0; resultCount < ResultList.Count; resultCount++)
                UpdateAlignChart(ResultList[resultCount].TabResultList[tabNum]);
        }

        private List<AppsInspResult> LoadResult()
        {
            List<AppsInspResult> inspResultList = new List<AppsInspResult>();

            string dir = Path.Combine(AppsConfig.Instance().Path.Result, @"Align\AlignInspection_Stage1_Top.csv");

            if (File.Exists(dir) == false)
                return new List<AppsInspResult>();

            Tuple<string[], List<string[]>> readData = CSVHelper.ReadData(dir);
            List<string[]> contents = readData.Item2;

            AppsInspModel model = ModelManager.Instance().CurrentModel as AppsInspModel;

            foreach (var item in contents)
            {
                AppsInspResult inspResult = new AppsInspResult();

                inspResult.LastInspTime = item[0].ToString();
                inspResult.Cell_ID = item[1].ToString();

                for (int tabIndex = 0; tabIndex < model.TabCount; tabIndex++)
                    inspResult.TabResultList.Add(AdjustData(tabIndex, item));

                inspResultList.Add(inspResult);
            }

            return CheckResultCount(AppsConfig.Instance().Operation.AlignResultCount, inspResultList.ToList());
        }

        private List<AppsInspResult> CheckResultCount(int maximumCount, List<AppsInspResult> inspResultList)
        {
            if (inspResultList.Count <= 0)
                return null;

            if (inspResultList.Count > maximumCount)
                inspResultList.RemoveRange(0, inspResultList.Count - maximumCount);

            return inspResultList;
        }

        private TabInspResult AdjustData(int tabNo, string[] datas)
        {
            TabInspResult data = new TabInspResult();

            data.LeftAlignX = new AlignResult();
            data.LeftAlignY = new AlignResult();
            data.RightAlignX = new AlignResult();
            data.RightAlignY = new AlignResult();

            int startIndex = 2;
            int interval = 7;
            startIndex = startIndex + interval * tabNo;

            data.TabNo = Convert.ToInt32(datas[startIndex].ToString());
            data.Judgement = (Judgement)Enum.Parse(typeof(Judgement), datas[startIndex + 1].ToString());

            data.LeftAlignX.ResultValue = (float)Convert.ToDouble(datas[startIndex + 2].ToString());
            data.LeftAlignY.ResultValue = (float)Convert.ToDouble(datas[startIndex + 3].ToString());
            data.RightAlignX.ResultValue = (float)Convert.ToDouble(datas[startIndex + 4].ToString());
            data.RightAlignY.ResultValue = (float)Convert.ToDouble(datas[startIndex + 5].ToString());
            data.CenterX = (float)Convert.ToDouble(datas[startIndex + 6].ToString());

            return data;
        }

        public void UpdateMainResult(AppsInspResult inspResult)
        {
           // _curAppsInspResult = inspResult.DeepCopy();

            InspAlignDisplay.ClearImage();

            ResultList.Add(inspResult);
            WriteAlignResult(null, inspResult);

            for (int i = 0; i < inspResult.TabResultList.Count(); i++)
            {
                int tabNo = inspResult.TabResultList[i].TabNo;
                if(InspResultDic.ContainsKey(tabNo))
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
            //UpdateAlignResult(inspResult);
            //UpdateAlignChart(inspResult);
        }

        private void UpdateAlignResult(AppsInspResult inspResult)
        {
            AlignInspResultControl.UpdateAlignResult(inspResult);
        }

        private void UpdateAlignChart(TabInspResult tabInspResult)
        {
            ResultChartControl.UpdateAlignChart(tabInspResult);
        }

        private void ClearAlignChart()
        {
            ResultChartControl.ClearChart();
        }

        private void WriteAlignResult(string filePath, AppsInspResult inspResult)
        {
            // TEST
            filePath = Path.Combine(AppsConfig.Instance().Path.Result, @"Align\AlignInspection_Stage1_Top.csv");

            if (File.Exists(filePath) == false)
            {
                AppsInspModel model = ModelManager.Instance().CurrentModel as AppsInspModel;

                List<string> header = new List<string>
                {
                    "Inspection Time",
                    "Panel ID"
                };

                for (int tabNo = 0; tabNo < model.TabCount; tabNo++)
                {
                    header.Add("Tab No");
                    header.Add("Judge");
                    header.Add("Lx");
                    header.Add("Ly");
                    header.Add("Rx");
                    header.Add("Ry");
                    header.Add("Cx");
                }

                CSVHelper.WriteHeader(filePath, header);
            }

            List<string> data = new List<string>
            {
                inspResult.LastInspTime.ToString(),
                inspResult.Cell_ID.ToString()
            };

            foreach (var item in inspResult.TabResultList)
            {
                data.Add(item.TabNo.ToString());
                data.Add(item.IsAlignGood().ToString());
                data.Add(item.LeftAlignX.ResultValue.ToString("F2"));
                data.Add(item.LeftAlignY.ResultValue.ToString("F2"));
                data.Add(item.RightAlignX.ResultValue.ToString("F2"));
                data.Add(item.RightAlignY.ResultValue.ToString("F2"));
                data.Add(item.CenterX.ToString("F2"));
            }

            CSVHelper.WriteData(filePath, data);
        }

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
