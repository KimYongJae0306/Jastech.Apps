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

namespace ATT.UI.Pages
{
    public partial class LogPage : UserControl
    {
        #region 속성
        private List<LogControl> LogControlList { get; set; } = new List<LogControl>();
        #endregion

        public LogPage()
        {
            InitializeComponent();
        }

        private void LogPage_Load(object sender, EventArgs e)
        {
            AddControl();
        }

        private void AddControl()
        {
            for (int unitIndex = 0; unitIndex < 2; unitIndex++)
            {
                string tempText = string.Empty;
                if (unitIndex == 0)
                    tempText = "PREALIGN";
                else if (unitIndex == 1)
                    tempText = "INSPECTION";

                this.tabLogPage.DrawMode = TabDrawMode.OwnerDrawFixed;
                this.tabLogPage.SizeMode = TabSizeMode.Fixed;

                Size tabSize = this.tabLogPage.ItemSize;
                tabSize.Width = 160;
                tabSize.Height += 6;
                this.tabLogPage.ItemSize = tabSize;

                string pageName = tempText;
                TabPage tp = new TabPage(pageName);
                tp.BackColor = Color.White;
                tp.Size = new Size(200, 200);

                LogControl logControl = new LogControl();
                logControl.Dock = DockStyle.Fill;
                LogControlList.Add(logControl);

                tp.Controls.Add(logControl);
                tabLogPage.TabPages.Add(tp);
            }
        }

        private void tabLogPage_DrawItem(object sender, DrawItemEventArgs e)
        {
            Brush textBrush;
            Brush boxBrush;
            Pen boxPen;

            int tabMargin = 3;

            Rectangle tabRectangle = this.tabLogPage.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(Brushes.Aquamarine, tabRectangle);

                e.DrawFocusRectangle();

                textBrush = Brushes.Blue;
                boxBrush = Brushes.Silver;
                boxPen = Pens.DarkBlue;
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.White, tabRectangle);

                textBrush = Brushes.Black;
                boxBrush = Brushes.LightGray;
                boxPen = Pens.DarkBlue;
            }

            RectangleF layoutRectangle = new RectangleF
            (
                tabRectangle.Left + tabMargin,
                tabRectangle.Y + tabMargin,
                tabRectangle.Width - 2 * tabMargin,
                tabRectangle.Height - 2 * tabMargin
            );

            using (StringFormat stringFormat = new StringFormat())
            {
                using (Font largeFont = new Font("맑은 고딕", 11.25F, FontStyle.Bold))
                {
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    e.Graphics.DrawString
                    (
                        this.tabLogPage.TabPages[e.Index].Text,
                        largeFont,
                        textBrush,
                        layoutRectangle,
                        stringFormat
                    );
                }
            }
        }
    }
}
