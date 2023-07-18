using Cognex.VisionPro;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Jastech.Apps.Structure;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Winform.Controls;
using Jastech.Framework.Winform.VisionPro.Controls;
using System;
using System.Drawing;
using System.IO;
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

        private string _selectedDirectoryFullPath { get; set; } = string.Empty;
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
                    lblLog.BackColor = _selectedColor;

                    pnlContents.Controls.Add(LogControl);
                    break;

                case PageType.Image:
                    _selectedPagePath = _resultPath;
                    lblImage.BackColor = _selectedColor;

                    pnlContents.Controls.Add(CogDisplayControl);
                    break;

                case PageType.AlignTrend:
                    _selectedPagePath = _resultPath;
                    lblAlignTrend.BackColor = _selectedColor;

                    AlignTrendControl.MakeTabListControl(inspModel.TabCount);
                    pnlContents.Controls.Add(AlignTrendControl);
                    break;

                case PageType.AkkonTrend:
                    _selectedPagePath = _resultPath;
                    lblAkkonTrend.BackColor = _selectedColor;

                    AkkonTrendControl.MakeTabListControl(inspModel.TabCount);
                    pnlContents.Controls.Add(AkkonTrendControl);
                    break;

                case PageType.UPH:
                    _selectedPagePath = _resultPath;
                    lblUPH.BackColor = _selectedColor;

                    pnlContents.Controls.Add(UPHControl);
                    break;

                case PageType.ProcessCapability:
                    _selectedPagePath = _resultPath;
                    lblProcessCapability.BackColor = _selectedColor;

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
                if (control is Label)
                    control.BackColor = _nonSelectedColor;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblLog_Click(object sender, EventArgs e)
        {
            SetPageType(PageType.Log);
        }

        private void lblImage_Click(object sender, EventArgs e)
        {
            SetPageType(PageType.Image);
        }

        private void lblAlignTrend_Click(object sender, EventArgs e)
        {
            SetPageType(PageType.AlignTrend);
        }

        private void lblAkkonTrend_Click(object sender, EventArgs e)
        {
            SetPageType(PageType.AkkonTrend);
        }

        private void lblUPH_Click(object sender, EventArgs e)
        {
            SetPageType(PageType.UPH);
        }

        private void lblProcessCapability_Click(object sender, EventArgs e)
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

            for (int index = tvwLogPath.Nodes.Count; index < 0; index--)
                tvwLogPath.Nodes.RemoveAt(index - 1);

            DateTime date = cdrMonthCalendar.SelectionStart;
            SetSelectionStartDate(date);

            string path = Path.Combine(_selectedPagePath, date.Month.ToString("D2"), date.Day.ToString("D2"));

            SetSelectedDirectoryFullPath(path);
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            if (Directory.Exists(directoryInfo.FullName))
            {
                TreeNode treeNode = new TreeNode(directoryInfo.Name);
                tvwLogPath.Nodes.Add(treeNode);
                RecursiveDirectory(directoryInfo, treeNode);
            }
        }

        private void SetSelectionStartDate(DateTime date)
        {
            DateTime = date;
        }

        public DateTime GetSelectionStartDate()
        { 
            return DateTime;
        }
        
        private void SetSelectedDirectoryFullPath(string path)
        {
            _selectedDirectoryFullPath = path;
        }

        private string GetSelectedDirectoryFullPath()
        {
            return _selectedDirectoryFullPath;
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

                    if ((_selectedPageType == PageType.AlignTrend || _selectedPageType == PageType.ProcessCapability ) && files2.Name.ToLower().Contains("align") == false)
                        continue;

                    if (_selectedPageType == PageType.AkkonTrend && files2.Name.ToLower().Contains("akkon") == false)
                        continue;

                    if (_selectedPageType == PageType.UPH && files2.Name.ToLower().Contains("uph") == false)
                        continue;

                    treeNode.Nodes.Add(node);
                }

                DirectoryInfo[] dirs = directoryInfo.GetDirectories();
                foreach (DirectoryInfo dirInfo in dirs)
                {
                    TreeNode upperNode = new TreeNode(dirInfo.Name);

                    if (_selectedPageType == PageType.AlignTrend || _selectedPageType == PageType.AkkonTrend || _selectedPageType == PageType.UPH || _selectedPageType == PageType.ProcessCapability)
                    {
                        if (dirInfo.Name.ToLower().Contains("origin"))
                            continue;
                    }

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

                string extension = Path.GetExtension(tvwLogPath.SelectedNode.FullPath);
                string fullPath = Path.Combine(GetSelectedDirectoryFullPath(), tvwLogPath.SelectedNode.Text);

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

                string filePath = Path.Combine(Path.GetDirectoryName(fullPath), "Origin", Path.GetFileName(fullPath));

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
                    ProcessCapabilityControl.SetSelectionStartDate(GetSelectionStartDate());
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

    public enum AlignResultType
    {
        All,
        Lx,
        Ly,
        Cx,
        Rx,
        Ry,
    }
}
