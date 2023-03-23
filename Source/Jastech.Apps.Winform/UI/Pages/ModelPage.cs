using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Framework.Winform.Controls;
using Jastech.Framework.Config;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Structure;

namespace Jastech.Apps.Winform.UI.Pages
{
    public partial class ModelPage : UserControl
    {
        #region 속성
        private ModelControl ModelControl { get; set; } = new ModelControl();
        #endregion

        public ModelPage()
        {
            InitializeComponent();
        }

        private void ModelPage_Load(object sender, EventArgs e)
        {
            AddControl();
        }

        private void AddControl()
        {
            ModelControl.ModelPath = AppSettings.Instance().Path.Model;
            ModelControl.ApplyModelDelegate += new ModelControl.ApplyModelHandler(ApplyModel);

            ModelControl.Dock = DockStyle.Fill;
            pnlModelPage.Controls.Add(ModelControl);
        }

        public delegate void InvokeApplyModelDelegate(InspModel model);
        public void ApplyModel(InspModel model)
        {
            try
            {
                int gg = 0;
                //AppSettings.Instance
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
