﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Framework.Winform.VisionPro.Controls;
using Emgu.CV.CvEnum;
using Emgu.CV;
using AkkonTester.Helpers;
using Cognex.VisionPro;
using System.IO;

namespace AkkonTester.UI.Pages
{
    public partial class OriginalPage : UserControl
    {
        private CogTeachingDisplayControl cogOrgDisplay { get; set; } = null;

        private CogTeachingDisplayControl cogResultDisplay { get; set; } = null;

        public OriginalPage()
        {
            InitializeComponent();
        }

        private void OriginalPage_Load(object sender, EventArgs e)
        {
            AddFullImageControls();
        }

        private void AddFullImageControls()
        {
            cogOrgDisplay = new CogTeachingDisplayControl();
            cogOrgDisplay.Dock = DockStyle.Fill;
            pnlOriginalImage.Controls.Add(cogOrgDisplay);

            cogResultDisplay = new CogTeachingDisplayControl();
            cogResultDisplay.Dock = DockStyle.Fill;
            pnlResultImage.Controls.Add(cogResultDisplay);
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image file(*.jpeg,*.jpg,*.bmp) |*.jpeg;*.jpg;*.bmp;|"
                    + "jpeg file(*.jpeg)|*.jpeg; |"
                    + "jpg file(*.jpg)|*.jpg; |"
                    + "bmp file(*.bmp) | *.bmp;";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (SystemManager.Instance().OrginalImage != null)
                {
                    SystemManager.Instance().OrginalImage.Dispose();
                    SystemManager.Instance().OrginalImage = null;
                }

                SystemManager.Instance().OrginalImage = new Mat(dialog.FileName, ImreadModes.Grayscale);

                SetCogImage(cogOrgDisplay, SystemManager.Instance().OrginalImage);

                if(SystemManager.Instance().CurrentAkkonROI.Count() == 0)
                {
                    string path = Path.GetDirectoryName(dialog.FileName);
                    string roiFileName = Path.GetFileNameWithoutExtension(dialog.FileName) + ".txt";
                    string roiPath = Path.Combine(path, roiFileName);

                    LoadROI(roiPath);
                }
            }
        }

        private void SetCogImage(CogTeachingDisplayControl display, Mat mat)
        {
            ICogImage cogImage = AppsHelper.ConvertCogGrayImage(mat);
            display.SetImage(cogImage);
        }

        private void btnMakeSliceImage_Click(object sender, EventArgs e)
        {
            if(SystemManager.Instance().OrginalImage == null)
            {
                MessageBox.Show("Image is not exist.");
                return;
            }
            if(SystemManager.Instance().CurrentAkkonROI.Count() == 0)
            {
                MessageBox.Show("ROI data is not exist.");
                return;
            }
            SystemManager.Instance().AkkonParameters.ResizeRatio = Convert.ToDouble(txtResizeRatio.Text);

            SystemManager.Instance().CalcSliceImage();
            SystemManager.Instance().UpdateSliceData();

            MessageBox.Show("Slice Data Update Completed.");
        }

        private void txtAkkonRoiOffset_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))    //숫자와 백스페이스를 제외한 나머지를 바로 처리             
            {
                e.Handled = true;
            }
        }

        private void btnLoadRoiData_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "text file(*.txt) |*.txt;|;"
                    + "text file(*.txt) | *.txt;";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string roiPath = Path.GetFileNameWithoutExtension(dialog.FileName) + ".txt";

                LoadROI(roiPath);
            }
        }

        private void txtResizeRatio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back) || e.KeyChar == '.'))    //숫자와 백스페이스를 제외한 나머지를 바로 처리             
            {
                e.Handled = true;
            }
        }

        private void LoadROI(string roiPath)
        {
            if (File.Exists(roiPath))
            {
                lblMessage.Text = "Akkon ROI Data is exist.";

                int offsetX = Convert.ToInt32(txtAkkonRoiOffsetX.Text);
                int offsetY = Convert.ToInt32(txtAkkonRoiOffsetX.Text);
                var roiList = AppsHelper.ReadROI(roiPath, offsetX, offsetY);

                SystemManager.Instance().CurrentAkkonROI.Clear();
                SystemManager.Instance().CurrentAkkonROI.AddRange(roiList);
            }
        }

        private void btnInspection_Click(object sender, EventArgs e)
        {
            SystemManager.Instance().AkkonParameters.ResizeRatio = Convert.ToDouble(txtResizeRatio.Text);
            SystemManager.Instance().UpdateParam();

            var resultList = SystemManager.Instance().Run();
            if(resultList == null)
            {
                MessageBox.Show("Inspection Fail. Image is Null");
                return;
            }

            Mat resultMat = SystemManager.Instance().GetResultImage(resultList);
            var cogImage = AppsHelper.ConvertCogColorImage(resultMat);

            cogResultDisplay.SetImage(cogImage);
            resultMat.Dispose();
        }
    }
}
