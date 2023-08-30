using Cognex.VisionPro;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Jastech.Apps.Structure;
using Jastech.Apps.Winform.UI.Controls;
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
        private LogControl LogControl { get; set; } = new LogControl() { Dock = DockStyle.Fill };

        private CogDisplayControl CogDisplayControl { get; set; } = new CogDisplayControl() { Dock = DockStyle.Fill };

        private AlignTrendControl AlignTrendControl { get; set; } = new AlignTrendControl() { Dock = DockStyle.Fill };

        private AkkonTrendControl AkkonTrendControl { get; set; } = new AkkonTrendControl() { Dock = DockStyle.Fill };

        private UPHControl UPHControl { get; set; } = new UPHControl() { Dock = DockStyle.Fill };

        private ProcessCapabilityIndexControl ProcessCapabilityControl { get; set; } = new ProcessCapabilityIndexControl() { Dock = DockStyle.Fill };

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
            InitializeUI();
        }

        private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CogDisplayControl.DisposeImage();
            ControlDisplayHelper.DisposeChildControls(CogDisplayControl);
            ControlDisplayHelper.DisposeChildControls(LogControl);
            ControlDisplayHelper.DisposeChildControls(AlignTrendControl);
            ControlDisplayHelper.DisposeChildControls(AkkonTrendControl);
            ControlDisplayHelper.DisposeChildControls(UPHControl);
            ControlDisplayHelper.DisposeChildControls(ProcessCapabilityControl);
        }

        private void InitializeUI()
        {
            _selectedColor = Color.FromArgb(104, 104, 104);
            _nonSelectedColor = Color.FromArgb(52, 52, 52);

            SetPageType(PageType.Log);
            cdrMonthCalendar.SelectionStart = DateTime.Now;
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

                    pnlContents.Controls.Add(CogDisplayControl);
                    break;

                case PageType.AlignTrend:
                    _selectedPagePath = _resultPath;
                    btnSelectionAlignTrend.BackColor = _selectedColor;

                    AlignTrendControl.MakeTabListControl(inspModel.TabCount);
                    pnlContents.Controls.Add(AlignTrendControl);
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

        private void btnSelectionAlignTrend_Click(object sender, EventArgs e)
        {
            SetPageType(PageType.AlignTrend);
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
            DateTime = cdrMonthCalendar.SelectionStart;

            string path = Path.Combine(_selectedPagePath, DateTime.Month.ToString("D2"), DateTime.Day.ToString("D2"));
            if (path == string.Empty)
                return;

            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            if (Directory.Exists(directoryInfo.FullName))
            {
                TreeNode treeNode = new TreeNode(directoryInfo.Name);
                tvwLogPath.Nodes.Add(treeNode);
                RecursiveDirectory(directoryInfo, treeNode);
            }

            if(tvwLogPath.Nodes.Count > 0 && tvwLogPath.Nodes[0].Nodes.Count > 0)
            {
                tvwLogPath.SelectedNode = tvwLogPath.Nodes[0].Nodes[0];
                tvwLogPath_NodeMouseClick(null, null);
            }
        }

        private void RecursiveDirectory(DirectoryInfo directoryInfo, TreeNode treeNode)
        {
            try
            {
                FileInfo[] files = directoryInfo.GetFiles();
                foreach (FileInfo files2 in files)
                {
                    TreeNode node = new TreeNode(files2.Name);

                    if (_selectedPageType == PageType.Image && files2.Name.ToLower().Contains(".csv"))
                        continue;

                    if ((_selectedPageType == PageType.AlignTrend || _selectedPageType == PageType.ProcessCapability) && (files2.Name.ToLower().Contains("align") == false || files2.Name.ToLower().Contains("summary")))
                        continue;

                    if (_selectedPageType == PageType.AkkonTrend && (files2.Name.ToLower().Contains("akkon") == false || files2.Name.ToLower().Contains("summary")))
                        continue;

                    if (_selectedPageType == PageType.UPH && files2.Name.ToLower().Contains("uph") == false)
                        continue;

                    treeNode.Nodes.Add(node);
                }

                if (_selectedPageType != PageType.Image)
                    return;

                // PageType이 Image인 경우에만 Recursive로 순회하여 경로 취득
                DirectoryInfo[] dirs = directoryInfo.GetDirectories();
                foreach (DirectoryInfo dirInfo in dirs)
                {
                    TreeNode upperNode = new TreeNode(dirInfo.Name);

                    treeNode.Nodes.Add(upperNode);

                    files = dirInfo.GetFiles();
                    foreach (FileInfo fileInfo in files)
                    {
                        TreeNode underNode = new TreeNode(fileInfo.Name);
                        upperNode.Nodes.Add(underNode);
                    }

                    try
                    {
                        if (dirInfo.GetDirectories().Length > 0)
                            RecursiveDirectory(dirInfo, upperNode);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void tvwLogPath_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (tvwLogPath.SelectedNode == null)
                    return;


                string basePath = Path.Combine(_selectedPagePath, DateTime.Month.ToString("D2"));
                string fullPath = Path.Combine(basePath, tvwLogPath.SelectedNode.FullPath);


                string extension = Path.GetExtension(fullPath);
                if (extension == string.Empty)
                    return;



                DisplaySelectedNode(extension, fullPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void DisplaySelectedNode(string extension, string fullPath)
        {
            switch (extension.ToLower())
            {
                case ".log":
                case ".txt":
                    DisplayTextFile(fullPath);
                    break;

                case ".bmp":
                case ".jpg":
                case ".png":
                    DisplayImageFile(fullPath);
                    break;


                case ".csv":
                    DisplayCSVFile(fullPath);
                    
                    break;

                default:
                    break;
            }
        }

        private void DisplayTextFile(string fullPath)
        {
            LogControl.DisplayOnLogFile(fullPath);
        }

        private void DisplayImageFile(string fullPath)
        {
            try
            {
                CogDisplayControl.ClearImage();

                //string filePath = Path.Combine(Path.GetDirectoryName(fullPath), "Origin", Path.GetFileName(fullPath));
                string filePath = fullPath;

                Mat image = new Mat(filePath, ImreadModes.Grayscale);
                int size = image.Width * image.Height * image.NumberOfChannels;
                byte[] dataArray = new byte[size];
                Marshal.Copy(image.DataPointer, dataArray, 0, size);

                ColorFormat format = image.NumberOfChannels == 1 ? ColorFormat.Gray : ColorFormat.RGB24;

                var cogImage = VisionProImageHelper.ConvertImage(dataArray, image.Width, image.Height, format);
                CogDisplayControl.SetImage(cogImage.CopyBase(CogImageCopyModeConstants.CopyPixels));
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
                    AlignTrendControl.UpdateDataGridView();
                    AlignTrendControl.SetTabType(TabType.Tab1);
                    break;

                case PageType.AkkonTrend:
                    AkkonTrendControl.SetAkkonResultData(fullPath);
                    AkkonTrendControl.SetAkkonResultType(AkkonResultType.All);
                    AkkonTrendControl.UpdateDataGridView();
                    AkkonTrendControl.SetTabType(TabType.Tab1);
                    break;

                case PageType.UPH:
                    UPHControl.ReadDataFromCSVFile(fullPath);
                    break;

                case PageType.ProcessCapability:
                    ProcessCapabilityControl.SetAlignResultData(fullPath);
                    ProcessCapabilityControl.UpdateAlignDataGridView();
                    ProcessCapabilityControl.SetSelectionStartDate(DateTime);
                    ProcessCapabilityControl.SetTabType(TabType.Tab1);
                    ProcessCapabilityControl.SetAlignResultType(AlignResultType.Lx);
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
