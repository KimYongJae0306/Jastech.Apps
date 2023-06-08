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

        private Color _nonSelectedColor;

        private AppsInspResult _curAppsInspResult = null;
        #endregion

        #region 속성
        public List<TabBtnControl> TabBtnControlList { get; private set; } = new List<TabBtnControl>();

        public CogInspAlignDisplayControl InspAlignDisplay { get; private set; } = new CogInspAlignDisplayControl();

        public AlignInspResultControl AlignInspResultControl { get; private set; } = new AlignInspResultControl();

        public ResultChartControl ResultChartControl { get; private set; } = new ResultChartControl();

        public Dictionary<int, TabInspResult> InspResultDic { get; set; } = new Dictionary<int, TabInspResult>();

        private int CurrentTabNo { get; set; } = -1;

        //public DailyInfo DailyInfo = new DailyInfo();
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
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            AddControls();

            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            if (inspModel == null)
                UpdateTabCount();
            else
                UpdateTabCount(inspModel.TabCount);

            var dailyInfo = DailyInfoService.GetDailyInfo();
            UpdateDailyDataGridView(dailyInfo);
            UpdateDailyChart(dailyInfo, 0);
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
            {
                UpdateLeftAlignResult(InspResultDic[tabNum]);
                UpdateRightAlignResult(InspResultDic[tabNum]);
            }
            else
            {
                InspAlignDisplay.ClearImage();
            }

            var dailyInfo = DailyInfoService.GetDailyInfo();
            UpdateDailyChart(dailyInfo, tabNum);
        }

        public void UpdateMainResult(AppsInspResult inspResult)
        {
           // _curAppsInspResult = inspResult.DeepCopy();

            InspAlignDisplay.ClearImage();

            //UpdateDailyInfo(inspResult);
            var dailyInfo = DailyInfoService.GetDailyInfo();
            UpdateDailyDataGridView(dailyInfo);

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

                if(CurrentTabNo == tabNo)
                {
                    UpdateLeftAlignResult(inspResult.TabResultList[i]);
                    UpdateRightAlignResult(inspResult.TabResultList[i]);
                }
            }
        }

        private void UpdateDailyDataGridView(DailyInfo dailyInfo)
        {
            AlignInspResultControl.UpdateAlignDaily(dailyInfo);
        }

        private void UpdateDailyChart(DailyInfo dailyInfo, int tabNo)
        {
            if (dailyInfo == null)
                return;

            if (dailyInfo.DailyDataList.Count > 0)
                ResultChartControl.UpdateAlignDaily(dailyInfo, tabNo);
        }

        private void ClearAlignChart()
        {
            ResultChartControl.ClearChart();
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

            float centerX = (maxX + minX) / 2.0f;
            float centerY = (maxY + minY) / 2.0f;

            return new Point((int)centerX, (int)centerY);
        }

        public ICogImage ConvertCogImage(Mat image)
        {
            if (image == null)
                return null;

            int size = image.Width * image.Height * image.NumberOfChannels;
            byte[] dataArray = new byte[size];
            Marshal.Copy(image.DataPointer, dataArray, 0, size);

            ColorFormat format = image.NumberOfChannels == 1 ? ColorFormat.Gray : ColorFormat.RGB24;

            var cogImage = VisionProImageHelper.CovertImage(dataArray, image.Width, image.Height, format);

            return cogImage;
        }
        #endregion
    }
}
