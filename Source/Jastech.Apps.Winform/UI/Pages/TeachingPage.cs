using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Framework.Winform.VisionPro.Controls;

namespace Jastech.Apps.Winform.UI.Pages
{
    public partial class TeachingPage : UserControl
    {
        #region 속성
        // Display
        private CogDisplayControl TeachDisplay = null;

        // Teach Controls
        private CogPatternMatchingParamControl PatternMatchControl { get; set; } = new CogPatternMatchingParamControl();

        // Control List
        private List<UserControl> TeachControlList = null;
        private List<Button> TeachButtonList = null;
        #endregion

        public TeachingPage()
        {
            InitializeComponent();
        }

        private void TeachingPage_Load(object sender, EventArgs e)
        {
            AddControl();
        }

        private void AddControl()
        {
            // Display Control
            TeachDisplay = new CogDisplayControl();
            TeachDisplay.Dock = DockStyle.Fill;
            pnlDisplay.Controls.Add(TeachDisplay);

            // Teach Control List
            TeachControlList = new List<UserControl>();
            TeachControlList.Add(PatternMatchControl);

            // Button List
            TeachButtonList = new List<Button>();
            TeachButtonList.Add(btnLinescan);
            TeachButtonList.Add(btnPatternMatch);
            TeachButtonList.Add(btnAlign);
            TeachButtonList.Add(btnAkkon);

        }

        private void btnLinescan_Click(object sender, EventArgs e)
        {
            SetSelectButton(sender);
            //SetSelectTeachPage(PatternMatchControl);
        }

        private void btnPatternMatch_Click(object sender, EventArgs e)
        {
            SetSelectButton(sender);
            SetSelectTeachPage(PatternMatchControl);
        }

        private void btnAlign_Click(object sender, EventArgs e)
        {
            SetSelectButton(sender);
            //SetSelectTeachPage(PatternMatchControl);
        }

        private void btnAkkon_Click(object sender, EventArgs e)
        {
            SetSelectButton(sender);
            //SetSelectTeachPage(PatternMatchControl);
        }

        private void SetSelectButton(object sender)
        {
            foreach (Button button in TeachButtonList)
                button.ForeColor = Color.Black;

            Button btn = sender as Button;
            btn.ForeColor = Color.Blue;
        }

        private void SetSelectTeachPage(UserControl selectedControl)
        {
            foreach (UserControl control in TeachControlList)
                control.Visible = false;

            selectedControl.Visible = true;
            selectedControl.Dock = DockStyle.Fill;
            pnlTeach.Controls.Add(selectedControl);
        }
    }
}
