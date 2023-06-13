using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Winform.Service;
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Framework.Winform.VisionPro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATT_UT_IPAD.UI.Controls
{
    public partial class AkkonResultDisplayControl : UserControl
    {
        public Dictionary<int, TabInspResult> InspResultDic { get; set; } = new Dictionary<int, TabInspResult>();

        public CogInspDisplayControl InspDisplayControl { get; private set; } = new CogInspDisplayControl() { Dock = DockStyle.Fill };

        public int CurrentTabNo { get; set; } = -1;

        public AkkonResultDisplayControl()
        {
            InitializeComponent();
        }

        private void AkkonResultDisplayControl_Load(object sender, EventArgs e)
        {
            AddControl();
        }

        private void AddControl()
        {
            pnlInspDisplay.Controls.Add(InspDisplayControl);
        }

        public void UpdateResultDisplay(AppsInspResult inspResult)
        {
            InspDisplayControl.Clear();

            for (int i = 0; i < inspResult.TabResultList.Count(); i++)
            {
                int tabNo = inspResult.TabResultList[i].TabNo;

                if (InspResultDic.ContainsKey(tabNo))
                {
                    InspResultDic[tabNo].Dispose();
                    InspResultDic.Remove(tabNo);
                }

                InspResultDic.Add(tabNo, inspResult.TabResultList[i]);

                if (CurrentTabNo == tabNo)
                {
                    InspDisplayControl.SetImage(inspResult.TabResultList[i].AkkonResultImage);
                }
            }
        }
    }
}
