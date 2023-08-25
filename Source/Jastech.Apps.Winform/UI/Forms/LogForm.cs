using Cognex.VisionPro;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Jastech.Apps.Structure;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Winform.Controls;
using Jastech.Framework.Winform.Helper;
using Jastech.Framework.Winform.VisionPro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Linq;

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

        private StyledCalender Calender { get; set; } = null;

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

            Calender = new StyledCalender
            {
                BackColor = Color.FromArgb(104, 104, 104),
                ForeColor = Color.White,
                MaxSelectionCount = 1,
                TitleBackColor = Color.Black,
                TitleForeColor = Color.White,
                TrailingForeColor = Color.FromArgb(52, 52, 52)
            };
            Calender.DateChanged += new DateRangeEventHandler(cdrMonthCalendar_DateChanged);
            Calender.SelectionStart = DateTime.Now;

            pnlCalendar.Controls.Add(Calender);

            SetPageType(PageType.Log);
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
            DateTime = Calender.SelectionStart;

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
                    AlignTrendControl.UpdateDataGridView(fullPath);
                    AlignTrendControl.SetAlignResultType(AlignResultType.All);
                    AlignTrendControl.SetTabType(TabType.Tab1);
                    break;

                case PageType.AkkonTrend:
                    AkkonTrendControl.UpdateDataGridView(fullPath);
                    AkkonTrendControl.SetAkkonResultType(AkkonResultType.All);
                    AkkonTrendControl.SetTabType(TabType.Tab1);
                    break;

                case PageType.UPH:
                    UPHControl.ReadDataFromCSVFile(fullPath);
                    break;

                case PageType.ProcessCapability:
                    ProcessCapabilityControl.UpdateAlignDataGridView(fullPath);
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


    public class ResultLogData
    {
        public PageType PageType { get; private set; }
        private const string NullString = "NaN";
        private const string NullInteger = "-1";

        private readonly Dictionary<Enum, string> _datas = new Dictionary<Enum, string>();

        public ResultLogData(PageType type)
        {
            PageType = type;

            _datas.Add(CommonResultType.Inspection_Time, $"{DateTime.UtcNow:HH:mm:ss}");
            _datas.Add(CommonResultType.Panel_ID, $"{DateTime.UtcNow:yyMMddhhmmssff}");
            _datas.Add(CommonResultType.Stage_No, NullInteger);
            _datas.Add(CommonResultType.Tab, NullInteger);
            switch (PageType)
            {
                case PageType.AkkonTrend:
                    _datas.Add(CommonResultType.Judge, NullString);
                    _datas.Add(AkkonResultType.AkkonJudge, NullString);
                    //_datas.Add(AkkonResultType.Min_Count, NullInteger);
                    _datas.Add(AkkonResultType.Avg_Count, NullInteger);
                    //_datas.Add(AkkonResultType.Min_Length, NullInteger);
                    _datas.Add(AkkonResultType.Avg_Length, NullInteger);
                    break;
                case PageType.AlignTrend:
                    _datas.Add(CommonResultType.Judge, NullString);
                    _datas.Add(AlignResultType.AlignJudge, NullString);
                    _datas.Add(AlignResultType.Lx, NullInteger);
                    _datas.Add(AlignResultType.Ly, NullInteger);
                    _datas.Add(AlignResultType.Rx, NullInteger);
                    _datas.Add(AlignResultType.Ry, NullInteger);
                    _datas.Add(AlignResultType.Cx, NullInteger);
                    break;
                case PageType.UPH:
                    _datas.Add(AkkonResultType.Min_Count, NullInteger);
                    _datas.Add(AkkonResultType.Avg_Count, NullInteger);
                    _datas.Add(AkkonResultType.Min_Length, NullInteger);
                    _datas.Add(AkkonResultType.Avg_Length, NullInteger);
                    _datas.Add(AkkonResultType.Strength_Min, NullString);   //미사용 시 제외
                    _datas.Add(AkkonResultType.Strength_Avg, NullString);   //미사용 시 제외

                    _datas.Add(AlignResultType.Lx, NullInteger);
                    _datas.Add(AlignResultType.Ly, NullInteger);
                    _datas.Add(AlignResultType.Rx, NullInteger);
                    _datas.Add(AlignResultType.Ry, NullInteger);
                    _datas.Add(AlignResultType.Cx, NullInteger);

                    _datas.Add(CommonResultType.ACF_Head, NullInteger);
                    _datas.Add(CommonResultType.Pre_Head, NullInteger);
                    _datas.Add(CommonResultType.Main_Head, NullInteger);
                    break;
            }
        }

        public void SetData(Enum key, string value, [CallerMemberName] string caller = "")
        {
            if (_datas.ContainsKey(key))
                _datas[key] = value;
            else
                Console.WriteLine($"{caller}().InspResultLogData.SetData({key}, {value})\r\nKey is not exist, Please check container initializer.");
        }

        public static string ParseHeader(Enum value) => $"{value}".Replace('_', ' ');

        // KeyCollection(Header)이 변경될 경우 기존 CSV파일 Header를 변경하거나 신규파일로 작성 필요
        public List<string> GetHeaderData() => _datas.Keys.Select(key => ParseHeader(key)).ToList();

        public List<string> GetBodyData() => _datas.Values.Select(value => value).ToList();
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

    public enum CommonResultType
    {
        Inspection_Time,
        Panel_ID,
        Stage_No,
        Tab,
        Judge,
        Reason,

        ACF_Head,
        Pre_Head,
        Main_Head,
    }

    public enum AkkonResultType
    {
        All,
        AkkonJudge,
        Min_Count,
        Avg_Count,
        Min_Length,
        Avg_Length,
        Strength_Min,
        Strength_Avg,
    }

    public enum AlignResultType
    {
        All,
        AlignJudge,
        Lx,
        Ly,
        Cx,
        Rx,
        Ry,
    }
}
