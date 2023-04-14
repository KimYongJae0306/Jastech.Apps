﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Imaging;
using Jastech.Framework.Config;
using Jastech.Framework.Winform.Forms;

namespace ATT.UI.Forms
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
            var operation = AppConfig.Instance().Operation;

            txtDistanceX.Text = operation.DistanceFromPreAlignToLineScanX.ToString();
            txtDistanceY.Text = operation.DistanceFromPreAlignToLineScanY.ToString();

            txtPreAlignToleranceX.Text = operation.PreAlignToleranceX.ToString();
            txtPreAlignToleranceY.Text = operation.PreAlignToleranceY.ToString();
            txtPreAlignToleranceTheta.Text = operation.PreAlignToleranceTheta.ToString();

            mtgEnablePreAlign.Checked = operation.EnablePreAlign;
            mtgEnableAkkon.Checked = operation.EnableAkkon;

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
        }

        public void UpdateCuurentData()
        {
            var operation = AppConfig.Instance().Operation;

            operation.DistanceFromPreAlignToLineScanX = Convert.ToSingle(GetValue(txtDistanceX.Text));
            operation.DistanceFromPreAlignToLineScanY = Convert.ToSingle(GetValue(txtDistanceY.Text));

            operation.PreAlignToleranceX = Convert.ToSingle(GetValue(txtPreAlignToleranceX.Text));
            operation.PreAlignToleranceY = Convert.ToSingle(GetValue(txtPreAlignToleranceY.Text));
            operation.PreAlignToleranceTheta = Convert.ToSingle(GetValue(txtPreAlignToleranceTheta.Text));

            operation.EnablePreAlign = mtgEnablePreAlign.Checked;
            operation.EnableAkkon = mtgEnableAkkon.Checked;

            operation.DataStoringDuration = (int)Convert.ToDouble(GetValue(txtDataStoringDays.Text));
            operation.DataStiringCapcity = (int)Convert.ToDouble(GetValue(txtDataStoringCapcity.Text));

            operation.SaveImageOK = mtgSaveOK.Checked;
            operation.SaveImageNG = mtgSaveNG.Checked;

            operation.ExtensionOKImage = (ImageExtension)Enum.Parse(typeof(ImageExtension), mcbxOKExtension.SelectedItem as string);
            operation.ExtensionNGImage = (ImageExtension)Enum.Parse(typeof(ImageExtension), mcbxNGExtension.SelectedItem as string);
        }

        public string GetValue(string value)
        {
            if (value == "")
                value = "0";
            return value;
        }
        #endregion

        private void lblSave_Click(object sender, EventArgs e)
        {
            UpdateCuurentData();
            AppConfig.Instance().Operation.Save(AppConfig.Instance().Path.Config);

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
    }
}
