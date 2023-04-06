using System;
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

            operation.DistanceFromPreAlignToLineScanX = Convert.ToSingle(txtDistanceX.Text);
            operation.DistanceFromPreAlignToLineScanY = Convert.ToSingle(txtDistanceY.Text);

            operation.PreAlignToleranceX = Convert.ToSingle(txtPreAlignToleranceX.Text);
            operation.PreAlignToleranceY = Convert.ToSingle(txtPreAlignToleranceY.Text);
            operation.PreAlignToleranceTheta = Convert.ToSingle(txtPreAlignToleranceTheta.Text);

            operation.EnablePreAlign = mtgEnablePreAlign.Checked;
            operation.EnableAkkon = mtgEnableAkkon.Checked;

            operation.DataStoringDuration = Convert.ToInt32(txtDataStoringDays.Text);
            operation.DataStiringCapcity = Convert.ToInt32(txtDataStoringCapcity.Text);

            operation.SaveImageOK = mtgSaveOK.Checked;
            operation.SaveImageNG = mtgSaveNG.Checked;

            operation.ExtensionOKImage = (ImageExtension)Enum.Parse(typeof(ImageExtension), mcbxOKExtension.SelectedItem as string);
            operation.ExtensionNGImage = (ImageExtension)Enum.Parse(typeof(ImageExtension), mcbxNGExtension.SelectedItem as string);
        }
        #endregion

        private void lblSave_Click(object sender, EventArgs e)
        {
            UpdateCuurentData();
            AppConfig.Instance().Operation.Save(AppConfig.Instance().Path.Config);
        }

        private void lblCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
