using Jastech.Framework.Structure;
using Jastech.Framework.Winform.Controls;
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

namespace Jastech.Framework.Modeller.Forms
{
    public partial class ModelForm : Form
    {
        public string ModelPath { get; set; } = "";

        public ModelForm()
        {
            InitializeComponent();
        }

        private void ModelForm_Load(object sender, EventArgs e)
        {
            AddControl();
        }

        private void AddControl()
        {
            ModelControl ModelControl = new ModelControl();
            ModelControl.Dock = DockStyle.Fill;
            pnlModelForm.Controls.Add(ModelControl);
        }
    }
}
