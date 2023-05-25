using Jastech.Framework.Winform.Controls;
using Jastech.Framework.Winform.VisionPro.Controls;
using System;
using System.Drawing;
using System.IO;
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

        private LogControl LogControl { get; set; } = new LogControl() { Dock = DockStyle.Fill };

        private CogDisplayControl DisplayControl { get; set; } = new CogDisplayControl() { Dock = DockStyle.Fill };

        private UPHControl_old UPHControl { get; set; } = new UPHControl_old() { Dock = DockStyle.Fill };
        #endregion

        #region 속성
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
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
                    break;

                case PageType.AlignTrend:
                    _selectedPagePath = _resultPath;
                    lblAlignTrend.BackColor = _selectedColor;
                    break;

                case PageType.AkkonTrend:
                    _selectedPagePath = _resultPath;
                    lblAkkonTrend.BackColor = _selectedColor;
                    break;

                case PageType.UPH:
                    _selectedPagePath = _resultPath;
                    lblUPH.BackColor = _selectedColor;

                    pnlContents.Controls.Add(UPHControl);
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
                    treeNode.Nodes.Add(node);
                }

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

                string extension = Path.GetExtension(tvwLogPath.SelectedNode.FullPath);
                string fullPath = Path.Combine(GetSelectedDirectoryFullPath(), tvwLogPath.SelectedNode.Text);

                if (extension == string.Empty)
                    return;

                DisplayNode(extension, fullPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void DisplayNode(string extension, string fullPath)
        {
            switch (extension.ToLower())
            {
                case ".bmp":
                case ".jpg":
                case ".png":

                    break;

                case ".log":
                case ".txt":
                    LogControl.DisplayOnLogFile(fullPath);
                    break;

                case ".csv":

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
    }
}
