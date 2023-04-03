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
using Jastech.Apps.Winform.UI.Controls;
using Jastech.Apps.Structure;
using Jastech.Framework.Winform.Forms;

namespace ATT.UI.Pages
{
    public partial class LineTeachingPage : UserControl
    {
        #region 필드
        #endregion

        #region 속성
        public string UnitName { get; set; } = "";

        private CogThumbnailDisplayControl Display { get; set; } = new CogThumbnailDisplayControl();

        private AlignControl AlignControl { get; set; } = new AlignControl();

        private AkkonControl AkkonControl { get; set; } = new AkkonControl();
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public LineTeachingPage()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        public void UpdateSelectPage()
        {
            SelectAlign();
        }

        private void SelectAlign()
        {
            if (ModelManager.Instance().CurrentModel == null || UnitName == "")
                return;

            btnAlign.ForeColor = Color.Blue;
            pnlTeach.Controls.Add(AlignControl);

            var alignParam = SystemManager.Instance().GetTeachingData().GetAlignParameters(UnitName);
            AlignControl.SetParams(alignParam);
        }

        private void btnMotionPopup_Click(object sender, EventArgs e)
        {
            MotionPopupForm motionPopupForm = new MotionPopupForm();
            motionPopupForm.ShowDialog();
        }

        #endregion
    }
}
