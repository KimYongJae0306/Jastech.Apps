using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.Exceptions;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Jastech.Apps.Structure;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Apps.Winform.Core;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Winform.Helper;
using Jastech.Framework.Winform.VisionPro.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Reflection;

namespace Jastech.Framework.Winform.Forms
{
    public partial class LogForm : Form
    {
        #region 필드
        private Color _selectedColor;

        private Color _nonSelectedColor;

        private string _logPath { get; set; } = string.Empty;

        private string _resultPath { get; set; } = string.Empty;

        private PageType _selectedPageType { get; set; } = PageType.Log;

        private string _selectedPagePath { get; set; } = string.Empty;
        #endregion

        #region 속성
        private LogControl LogControl { get; set; } = null;

        private CogInspDisplayControl InspDisplayControl { get; set; } = null;

        private AlignTrendControl AlignTrendControl { get; set; } = null;

        private AlignTrendPreviewControl AlignTrendPreviewControl { get; set; } = null;

        private AkkonTrendControl AkkonTrendControl { get; set; } = null;

        private UPHControl UPHControl { get; set; } = null;

        private ProcessCapabilityIndexControl ProcessCapabilityControl { get; set; } = null;

        public DateTime DateTime { get; set; } = DateTime.Now;
        #endregion

        #region 생성자
        public LogForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void LogForm_Load(object sender, EventArgs e)
        {
            AddControls();
            InitializeUI();
            InspDisplayControl.UseAllContextMenu(false);
        }

        private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            InspDisplayControl.ClearImage();
            InspDisplayControl.ClearThumbnail();
            InspDisplayControl.ClearGraphic();
            ControlDisplayHelper.DisposeChildControls(InspDisplayControl);
            ControlDisplayHelper.DisposeChildControls(LogControl);
            ControlDisplayHelper.DisposeChildControls(AlignTrendControl);
            ControlDisplayHelper.DisposeChildControls(AlignTrendPreviewControl);
            ControlDisplayHelper.DisposeChildControls(AkkonTrendControl);
            ControlDisplayHelper.DisposeChildControls(UPHControl);
            ControlDisplayHelper.DisposeChildControls(ProcessCapabilityControl);
        }

        private void AddControls()
        {
            LogControl = new LogControl { Dock = DockStyle.Fill };
            InspDisplayControl = new CogInspDisplayControl { Dock = DockStyle.Top, Height = tvwLogPath.Height + cdrMonthCalendar.Height - pnlLogType.Height};
            AlignTrendControl = new AlignTrendControl { Dock = DockStyle.Fill };
            AlignTrendPreviewControl = new AlignTrendPreviewControl { Dock = DockStyle.Fill };
            AkkonTrendControl = new AkkonTrendControl { Dock = DockStyle.Fill };
            UPHControl = new UPHControl { Dock = DockStyle.Fill };
            ProcessCapabilityControl = new ProcessCapabilityIndexControl { Dock = DockStyle.Fill };
;        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            SetPageType(PageType.Log);
            cdrMonthCalendar.SelectionRange = new SelectionRange { Start = DateTime.Now.AddDays((cdrMonthCalendar.MaxSelectionCount - 1)*-1), End = DateTime.Now.AddDays(1) };
        }

        private void SetPageType(PageType pageType)
        {
            _selectedPageType = pageType;

            ClearSelectedLabel();
            pnlContents.Controls.Clear();

            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            switch (pageType)
            {
                case PageType.Log:
                    _selectedPagePath = _logPath;
                    btnSelectionLog.BackColor = _selectedColor;

                    pnlContents.Controls.Add(LogControl);
                    break;

                case PageType.Image:
                    _selectedPagePath = _resultPath;
                    btnSelectionImage.BackColor = _selectedColor;

                    pnlContents.Controls.Add(InspDisplayControl);
                    break;

                case PageType.AlignTrend:
                    _selectedPagePath = _resultPath;
                    btnSelectionAlignTrend_Old.BackColor = _selectedColor;

                    AlignTrendControl.MakeTabListControl(inspModel.TabCount);
                    pnlContents.Controls.Add(AlignTrendControl);
                    break;

                case PageType.AlignTrendPreview:
                    _selectedPagePath = _resultPath;
                    btnSelectionAlignTrend.BackColor = _selectedColor;

                    pnlContents.Controls.Add(AlignTrendPreviewControl);
                    break;

                case PageType.AkkonTrend:
                    _selectedPagePath = _resultPath;
                    btnSelectionAkkonTrend.BackColor = _selectedColor;

                    AkkonTrendControl.MakeTabListControl(inspModel.TabCount);
                    pnlContents.Controls.Add(AkkonTrendControl);
                    break;

                case PageType.UPH:
                    _selectedPagePath = _resultPath;
                    btnSelectionUPH.BackColor = _selectedColor;

                    pnlContents.Controls.Add(UPHControl);
                    break;

                case PageType.ProcessCapability:
                    _selectedPagePath = _resultPath;
                    btnSelectionProcessCapability.BackColor = _selectedColor;

                    ProcessCapabilityControl.MakeTabListControl(inspModel.TabCount);
                    pnlContents.Controls.Add(ProcessCapabilityControl);
                    break;

                default:
                    break;
            }

            SetDateChange();
        }

        private void ClearSelectedLabel()
        {
            foreach (Control control in pnlLogType.Controls)
            {
                if (control is Button)
                    control.BackColor = _nonSelectedColor;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelectionLog_Click(object sender, EventArgs e)
        {
            SetPageType(PageType.Log);
        }

        private void btnSelectionImage_Click(object sender, EventArgs e)
        {
            SetPageType(PageType.Image);
        }

        private void btnSelectionAlignTrend_Old_Click(object sender, EventArgs e)
        {
            SetPageType(PageType.AlignTrend);
        }

        private void btnSelectionAlignTrend_Click(object sender, EventArgs e)
        {
            SetPageType(PageType.AlignTrendPreview);
        }

        private void btnSelectionAkkonTrend_Click(object sender, EventArgs e)
        {
            SetPageType(PageType.AkkonTrend);
        }

        private void btnSelectionUPH_Click(object sender, EventArgs e)
        {
            SetPageType(PageType.UPH);
        }

        private void btnSelectionProcessCapability_Click(object sender, EventArgs e)
        {
            SetPageType(PageType.ProcessCapability);
        }

        public void SetLogViewPath(string logPath, string resultPath, string modelName)
        {
            _logPath = logPath;
            _resultPath = Path.Combine(resultPath, modelName);
        }

        private void cdrMonthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            SetDateChange();
        }

        private void SetDateChange()
        {
            if (_selectedPagePath == string.Empty)
                return;

            tvwLogPath.Nodes.Clear();

            for (DateTime dateTime = cdrMonthCalendar.SelectionStart; dateTime <= cdrMonthCalendar.SelectionEnd; dateTime = dateTime.AddDays(1))
            {
                string date = dateTime.ToString("yyyyMMdd");
                //string month = dateTime.Month.ToString("D2");
                //string day = dateTime.Day.ToString("D2");
                string rootPath = Path.Combine(_selectedPagePath, date);

                DirectoryInfo rootDirectoryInfo = new DirectoryInfo(rootPath);
                TreeNode rootNode;
                if (tvwLogPath.Nodes.ContainsKey(rootDirectoryInfo.Name) == false)
                {
                    rootNode = new TreeNode { Name = rootDirectoryInfo.Name, Text = rootDirectoryInfo.Name };
                    tvwLogPath.Nodes.Add(rootNode);
                }
                else
                    rootNode = tvwLogPath.Nodes[rootDirectoryInfo.Name];

                //DirectoryInfo subDirectoryInfo = new DirectoryInfo($@"{rootPath}\{day}");
                DirectoryInfo subDirectoryInfo = new DirectoryInfo($@"{rootPath}");
                if (Directory.Exists(subDirectoryInfo.FullName))
                    rootNode.Nodes.Add(RecursiveDirectory(subDirectoryInfo));
            }
        }

        private TreeNode RecursiveDirectory(DirectoryInfo directoryInfo)
        {
            TreeNode newNode = new TreeNode { Name = directoryInfo.Name, Text = directoryInfo.Name };
            try
            {
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    if (file.Name.ToLower().Contains("summary"))
                        continue;
                    if ((_selectedPageType == PageType.AlignTrend || _selectedPageType == PageType.AlignTrendPreview ||
                        _selectedPageType == PageType.ProcessCapability) && file.Name.ToLower().Contains("align") == false)
                        continue;
                    if (_selectedPageType == PageType.AkkonTrend && file.Name.ToLower().Contains("akkon") == false)
                        continue;
                    if (_selectedPageType == PageType.UPH && file.Name.ToLower().Contains("uph") == false)
                        continue;
                    if (_selectedPageType == PageType.Image && (file.Name.ToLower().Contains(".txt") || file.Name.ToLower().Contains(".csv")))
                        continue;

                    newNode.Nodes.Add(new TreeNode(file.Name));
                }

                // PageType이 Image인 경우에만 Recursive로 순회하여 경로 취득
                if (_selectedPageType == PageType.Image)
                {
                    foreach (DirectoryInfo subDirectory in directoryInfo.GetDirectories())
                        newNode.Nodes.Add(RecursiveDirectory(subDirectory));
                }

                return newNode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        private void tvwLogPath_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (tvwLogPath.SelectedNode == null)
                    return;

                string fullPath = Path.Combine(_selectedPagePath, tvwLogPath.SelectedNode.FullPath);

                string extension = Path.GetExtension(fullPath);
                if (extension == string.Empty)
                    return;

                DisplaySelectedNode(extension, fullPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Cursor = Cursors.Default;
            }
        }

        private void DisplaySelectedNode(string extension, string fullPath)
        {
            Cursor = Cursors.WaitCursor;
            switch (extension.ToLower())
            {
                case ".log":
                case ".txt":
                    DisplayTextFile(fullPath);
                    break;

                case ".bmp":
                case ".jpg":
                case ".jpeg":
                case ".png":
                    DisplayImageFile(fullPath);
                    break;


                case ".csv":
                    DisplayCSVFile(fullPath);
                    break;

                default:
                    break;
            }
            Cursor = Cursors.Default;
        }

        private void DisplayTextFile(string fullPath)
        {
            LogControl.DisplayOnLogFile(fullPath);
        }

        private void DisplayImageFile(string fullPath)
        {
            try
            {
                InspDisplayControl.ClearImage();
                InspDisplayControl.ClearThumbnail();
                InspDisplayControl.ClearGraphic();

                var cogImage = VisionProImageHelper.Load(fullPath);
                InspDisplayControl.SetImage(cogImage.CopyBase(CogImageCopyModeConstants.CopyPixels));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void DisplayCSVFile(string fullPath)
        {
            switch (_selectedPageType)
            {
                case PageType.AlignTrend:
                    AlignTrendControl.SetAlignResultData(fullPath);
                    AlignTrendControl.SetAlignResultType(AlignResultType.All);
                    AlignTrendControl.SetTabType(TabType.Tab1);
                    AlignTrendControl.UpdateDataGridView();
                    break;

                case PageType.AlignTrendPreview:
                    AlignTrendPreviewControl.SetAlignResultData(fullPath);
                    AlignTrendPreviewControl.UpdateDataGridView();
                    break;

                case PageType.AkkonTrend:
                    AkkonTrendControl.SetAkkonResultData(fullPath);
                    AkkonTrendControl.SetAkkonResultType(AkkonResultType.All);
                    AkkonTrendControl.SetTabType(TabType.Tab1);
                    AkkonTrendControl.UpdateDataGridView();
                    break;

                case PageType.UPH:
                    UPHControl.ReadDataFromCSVFile(fullPath);
                    break;

                case PageType.ProcessCapability:
                    ProcessCapabilityControl.SetAlignResultData(fullPath);
                    ProcessCapabilityControl.SetAlignResultType(AlignResultType.All);
                    ProcessCapabilityControl.SetTabType(TabType.Tab1);
                    ProcessCapabilityControl.UpdateAlignDataGridView();
                    break;

                default:
                    break;
            }
        }
        #endregion
    }

    public enum PageType
    {
        Log,
        Image,
        AlignTrend,
        AlignTrendPreview,
        AkkonTrend,
        UPH,
        ProcessCapability,
    }

    public enum TabType
    {
        Tab1,
        Tab2,
        Tab3,
        Tab4,
        Tab5,
    }

    public enum AkkonResultType
    {
        All,
        Count,
        Length,
        Strength,
        STD,
    }

    public enum AlignResultType
    {
        All,
        Lx,
        Ly,
        Cx,
        Rx,
        Ry,
    }

    public class TrendResult
    {
        public string InspectionTime { get; set; }

        public string PanelID { get; set; }

        public int StageNo { get; set; }

        public string FinalHead { get; set; }

        public List<TabAlignTrendResult> TabAlignResults { get; private set; } = new List<TabAlignTrendResult>();

        public List<TabAkkonTrendResult> TabAkkonResults { get; private set; } = new List<TabAkkonTrendResult>();

        public List<string> GetAlignStringDatas()
        {
            List<string> datas = new List<string>
            {
                $"{InspectionTime}",
                $"{PanelID}",
                $"{StageNo}",
                $"{FinalHead}",
            };

            foreach (var tab in TabAlignResults)
            {
                datas.Add($"{tab.Tab}");
                datas.Add($"{tab.Judge}");
                datas.Add($"{tab.PreHead}");
                datas.Add($"{tab.Lx}");
                datas.Add($"{tab.Ly}");
                datas.Add($"{tab.Cx}");
                datas.Add($"{tab.Rx}");
                datas.Add($"{tab.Ry}");
            }

            return datas;
        }

        public List<string> GetAkkonStringDatas()
        {
            List<string> datas = new List<string>
            {
                $"{InspectionTime}",
                $"{PanelID}",
                $"{StageNo}",
            };
            foreach (var tab in TabAkkonResults)
            {
                datas.Add($"{tab.Tab}");
                datas.Add($"{tab.Judge}");
                datas.Add($"{tab.AvgCount}");
                datas.Add($"{tab.AvgLength}");
            }

            return datas;
        }

        public double[] GetAlignDatas(TabType tabType, AlignResultType alignResultType)
        {
            double[] datas = null;

            var resultByTab = TabAlignResults.Where(result => result.Tab == (int)tabType + 1);

            if (alignResultType is AlignResultType.Lx)
                datas = resultByTab.Select(result => result.Lx).ToArray();
            else if (alignResultType is AlignResultType.Ly)
                datas = resultByTab.Select(result => result.Ly).ToArray();
            else if (alignResultType is AlignResultType.Cx)
                datas = resultByTab.Select(result => result.Cx).ToArray();
            else if (alignResultType is AlignResultType.Rx)
                datas = resultByTab.Select(result => result.Rx).ToArray();
            else if (alignResultType is AlignResultType.Ry)
                datas = resultByTab.Select(result => result.Ry).ToArray();

            return datas;
        }

        public double[] GetAkkonDatas(TabType tabType, AkkonResultType akkonResultType)
        {
            double[] datas = null;

            var resultByTab = TabAkkonResults.Where(result => result.Tab == (int)tabType + 1);

            if (akkonResultType is AkkonResultType.Count)
                datas = resultByTab.Select(result => (double)result.AvgCount).ToArray();
            else if (akkonResultType is AkkonResultType.Length)
                datas = resultByTab.Select(result => result.AvgLength).ToArray();

            return datas;
        }
    }

    public class TabAlignTrendResult
    {
        public int Tab { get; set; }
        public string Judge { get; set; }
        public string PreHead { get; set; }
        public double Lx { get; set; }
        public double Ly { get; set; }
        public double Cx { get; set; }
        public double Rx { get; set; }
        public double Ry { get; set; }
    }

    public class TabAkkonTrendResult
    {
        public int Tab { get; set; }
        public string Judge { get; set; }
        public int AvgCount { get; set; }
        public double AvgLength { get; set; }
    }
}
