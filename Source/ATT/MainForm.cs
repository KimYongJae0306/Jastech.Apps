using ATT.Core;
using ATT.UI.Pages;
using Jastech.Apps.Structure;
using Jastech.Apps.Winform.Settings;
using Jastech.Apps.Winform.UI.Forms;
using Jastech.Framework.Config;
using Jastech.Framework.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATT
{
    public partial class MainForm : Form
    {
        #region 속성
        public ATTTeachingData TeachingData { get; set; } = new ATTTeachingData();
        // Page Control
        private MainPage MainPageControl { get; set; } = new MainPage();

        private TeachingPage TeachingPageControl { get; set; } = new TeachingPage();

        private DataPage DataPageControl { get; set; } = new DataPage();

        private LogPage LogPageControl { get; set; } = new LogPage();

        private List<UserControl> PageControlList = null;

        private List<Label> PageLabelList = null;

        public ATTInspModelService ATTInspModelService { get; set; } = new ATTInspModelService();
        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            AddControls();
            SelectMainPage();

            ModelManager.Instance().CurrentModelChangedEvent += MainForm_CurrentModelChangedEvent;

            if (ModelManager.Instance().CurrentModel != null)
            {
                lblCurrentModel.Text = ModelManager.Instance().CurrentModel.Name;
                ModelManager.Instance().ApplyChangedEvent();
            }

            tmrMainForm.Start();
        }

        private void tmrMainForm_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            lblCurrentTime.Text = now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void MainForm_CurrentModelChangedEvent(InspModel inspModel)
        {
            ATTInspModel model = inspModel as ATTInspModel;

            TeachingData.Dispose();
            TeachingData.Initialize(model);

            //AreaTeachingPageControl.UpdateSelectPage();
            //LineTeachingPageControl.UpdateSelectPage();
        }

        private void AddControls()
        {
            // Page Control List
            PageControlList = new List<UserControl>();
            PageControlList.Add(MainPageControl);
            PageControlList.Add(DataPageControl);
            PageControlList.Add(TeachingPageControl);
            PageControlList.Add(LogPageControl);

            DataPageControl.SetInspModelService(ATTInspModelService);
            DataPageControl.ApplyModelEventHandler += ModelPageControl_ApplyModelEventHandler;

            // Button List
            PageLabelList = new List<Label>();
            PageLabelList.Add(lblMainPage);
            PageLabelList.Add(lblTeachingPage);
            PageLabelList.Add(lblDataPage);
            PageLabelList.Add(lblLogPage);
        }

        private void ModelPageControl_ApplyModelEventHandler(string modelName)
        {
            string modelDir = AppConfig.Instance().Path.Model;
            string filePath = Path.Combine(modelDir, modelName, InspModel.FileName);

            ModelManager.Instance().CurrentModel = ATTInspModelService.Load(filePath);
            SelectMainPage();

            lblCurrentModel.Text = modelName;

            AppConfig.Instance().Operation.LastModelName = modelName;

            AppConfig.Instance().Operation.Save(AppConfig.Instance().Path.Config);
        }

        private void SetSelectLabel(object sender)
        {
            foreach (Label label in PageLabelList)
            {
                label.ForeColor = Color.White;
            }

            Label currentLabel = sender as Label;
            currentLabel.ForeColor = Color.Blue;
        }

        private void SetSelectPage(UserControl selectedControl)
        {
            foreach (UserControl control in PageControlList)
                control.Visible = false;

            selectedControl.Visible = true;
            selectedControl.Dock = DockStyle.Fill;
            pnlPage.Controls.Add(selectedControl);
        }

        private void SelectMainPage()
        {
            SetSelectLabel(lblMainPage);
            SetSelectPage(selectedControl: MainPageControl);
        }

        
        private void DataPage_Click(object sender, EventArgs e)
        {
            SetSelectLabel(sender);
            SetSelectPage(selectedControl: DataPageControl);
        }

        private void lblMainPage_Click(object sender, EventArgs e)
        {
            SetSelectLabel(sender);
            SetSelectPage(selectedControl: MainPageControl);
        }

        private void TeachPage_Click(object sender, EventArgs e)
        {
            if (ModelManager.Instance().CurrentModel == null)
                return;

            SetSelectLabel(sender);
            SetSelectPage(selectedControl: TeachingPageControl);

            //UnitSelectForm form = new UnitSelectForm();

            //if(form.ShowDialog() == DialogResult.OK)
            //{
            //    if (form.SensorType == Jastech.Framework.Device.Cameras.SensorType.Area)
            //    {
            //        AreaTeachingPageControl.UnitName = form.UnitName;

            //        SetSelectLabel(sender);
            //        SetSelectPage(selectedControl: AreaTeachingPageControl);
            //    }
            //    else if (form.SensorType == Jastech.Framework.Device.Cameras.SensorType.Line)
            //    {
            //        LineTeachingPageControl.UnitName = form.UnitName;

            //        SetSelectLabel(sender);
            //        SetSelectPage(selectedControl: LineTeachingPageControl);
            //    }
            //    else { }
            //}
        }

        private void LogPage_Click(object sender, EventArgs e)
        {
            SetSelectLabel(sender);
            SetSelectPage(selectedControl: LogPageControl);
        }
    }
}
