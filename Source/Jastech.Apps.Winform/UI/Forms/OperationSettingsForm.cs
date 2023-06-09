﻿using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Config;
using Jastech.Framework.Imaging;
using System;
using System.Windows.Forms;

namespace Jastech.Framework.Winform.Forms
{
    public partial class OperationSettingsForm : Form
    {
        #region 필드
        #endregion

        #region 속성
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public OperationSettingsForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void OperationSettingsForm_Load(object sender, EventArgs e)
        {
            foreach (ImageExtension type in Enum.GetValues(typeof(ImageExtension)))
            {
                mcbxOKExtension.Items.Add(type.ToString());
                mcbxNGExtension.Items.Add(type.ToString());
            }

            LoadData();
        }

        private void LoadData()
        {
            var operation = ConfigSet.Instance().Operation;
            var appsConfig = AppsConfig.Instance();

            txtDistanceX.Text = appsConfig.DistanceFromPreAlignToLineScanX.ToString();
            txtDistanceY.Text = appsConfig.DistanceFromPreAlignToLineScanY.ToString();

            txtPreAlignToleranceX.Text = appsConfig.PreAlignToleranceX.ToString();
            txtPreAlignToleranceY.Text = appsConfig.PreAlignToleranceY.ToString();
            txtPreAlignToleranceTheta.Text = appsConfig.PreAlignToleranceTheta.ToString();

            mtgEnableAlign.Checked = appsConfig.EnableAlign;
            mtgEnableAkkon.Checked = appsConfig.EnableAkkon;

            txtDataStoringDays.Text = operation.DataStoringDuration.ToString();
            txtDataStoringCapcity.Text = operation.DataStiringCapcity.ToString();

            mtgSaveOK.Checked = operation.SaveImageOK;
            mtgSaveNG.Checked = operation.SaveImageNG;

            for (int i = 0; i < mcbxOKExtension.Items.Count; i++)
            {
                ImageExtension type = (ImageExtension)Enum.Parse(typeof(ImageExtension), mcbxOKExtension.Items[i] as string);
                if (type == operation.ExtensionOKImage)
                    mcbxOKExtension.SelectedIndex = i;
            }

            for (int i = 0; i < mcbxNGExtension.Items.Count; i++)
            {
                ImageExtension type = (ImageExtension)Enum.Parse(typeof(ImageExtension), mcbxNGExtension.Items[i] as string);
                if (type == operation.ExtensionNGImage)
                    mcbxNGExtension.SelectedIndex = i;
            }

            txtAlignResultCount.Text = AppsConfig.Instance().AlignResultCount.ToString();
            txtAkkonResultCount.Text = AppsConfig.Instance().AkkonResultCount.ToString();

            mtgDisplayNG.Checked = AppsConfig.Instance().UseNGDisplay;
            txtNGCount.Text = AppsConfig.Instance().NGSendingCycle.ToString();
        }

        public void UpdateCuurentData()
        {
            var operation = ConfigSet.Instance().Operation;
            var appsConfig = AppsConfig.Instance();

            appsConfig.DistanceFromPreAlignToLineScanX = Convert.ToSingle(GetValue(txtDistanceX.Text));
            appsConfig.DistanceFromPreAlignToLineScanY = Convert.ToSingle(GetValue(txtDistanceY.Text));

            appsConfig.PreAlignToleranceX = Convert.ToSingle(GetValue(txtPreAlignToleranceX.Text));
            appsConfig.PreAlignToleranceY = Convert.ToSingle(GetValue(txtPreAlignToleranceY.Text));
            appsConfig.PreAlignToleranceTheta = Convert.ToSingle(GetValue(txtPreAlignToleranceTheta.Text));

            appsConfig.EnableAlign = mtgEnableAlign.Checked;
            appsConfig.EnableAkkon = mtgEnableAkkon.Checked;

            operation.DataStoringDuration = (int)Convert.ToDouble(GetValue(txtDataStoringDays.Text));
            operation.DataStiringCapcity = (int)Convert.ToDouble(GetValue(txtDataStoringCapcity.Text));

            operation.SaveImageOK = mtgSaveOK.Checked;
            operation.SaveImageNG = mtgSaveNG.Checked;

            operation.ExtensionOKImage = (ImageExtension)Enum.Parse(typeof(ImageExtension), mcbxOKExtension.SelectedItem as string);
            operation.ExtensionNGImage = (ImageExtension)Enum.Parse(typeof(ImageExtension), mcbxNGExtension.SelectedItem as string);

            AppsConfig.Instance().AlignResultCount = Convert.ToInt32(GetValue(txtAlignResultCount.Text));
            AppsConfig.Instance().AkkonResultCount = Convert.ToInt32(GetValue(txtAkkonResultCount.Text));

            AppsConfig.Instance().UseNGDisplay = mtgDisplayNG.Checked;
            AppsConfig.Instance().NGSendingCycle = Convert.ToInt32(txtNGCount.Text);
        }

        public string GetValue(string value)
        {
            if (value == "")
                value = "0";
            return value;
        }

        private void lblSave_Click(object sender, EventArgs e)
        {
            UpdateCuurentData();

            ConfigSet.Instance().Save();
            AppsConfig.Instance().Save();

            MessageConfirmForm form = new MessageConfirmForm();
            form.Message = "Save Completed.";
            form.ShowDialog();
        }

        private void lblCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtKeyPad_KeyPress(object sender, KeyPressEventArgs e)
        {
            //숫자, 백스페이스, '.' 를 제외한 나머지를 바로 처리             
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back) || e.KeyChar == Convert.ToChar('.')))
            {
                e.Handled = true;
            }
        }

        private void txtDataStoringDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            //숫자, 백스페이스 를 제외한 나머지를 바로 처리             
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void txtDataStoringCapcity_Leave(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;

            if (double.TryParse(textBox.Text, out double value))
                textBox.Text = string.Format("{0:0.00}", value);
            else
                textBox.Text = "0.00";
        }

        private void txtKeyPad_Leave(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;

            if (double.TryParse(textBox.Text, out double value))
                textBox.Text = string.Format("{0:0.000}", value);
            else
                textBox.Text = "0.000";
        }

        private void textbox_KeyPad_Click(object sender, EventArgs e)
        {
            if (OperationConfig.UseKeyboard)
            {
                var textBox = (TextBox)sender;

                if (textBox.Text == "")
                    textBox.Text = "0";

                KeyPadForm keyPadForm = new KeyPadForm();
                keyPadForm.PreviousValue = Convert.ToDouble(textBox.Text);
                keyPadForm.ShowDialog();

                textBox.Text = keyPadForm.PadValue.ToString();
            }
        }

        private void txtDataStoringDays_Leave(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;

            if (double.TryParse(textBox.Text, out double value))
                textBox.Text = string.Format("{0:0}", value);
            else
                textBox.Text = "0";
        }
        #endregion
    }
}
