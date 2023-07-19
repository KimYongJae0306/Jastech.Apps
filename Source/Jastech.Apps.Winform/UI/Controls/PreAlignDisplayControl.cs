using Cognex.VisionPro;
using Jastech.Apps.Structure.Data;
using Jastech.Framework.Winform.VisionPro.Controls;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class PreAlignDisplayControl : UserControl
    {
        #region 필드
        #endregion

        #region 속성
        private CogPreAlignDisplayControl PreAlignDisplay { get; private set; } = null;
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public PreAlignDisplayControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void PreAlignDisplayControl_Load(object sender, System.EventArgs e)
        {
            AddControl();
        }

        private void AddControl()
        {
            PreAlignDisplay = new CogPreAlignDisplayControl();
            PreAlignDisplay.Dock = DockStyle.Fill;
            pnlPreAlignDisplay.Controls.Add(PreAlignDisplay);
        }

        public void UpdateLeftDisplay(PreAlignResult result)
        {
            List<CogCompositeShape> resultList = new List<CogCompositeShape>();

            var leftResult = result.PreAlignMark.FoundedMark.Left;
            var graphics = leftResult.MaxMatchPos.ResultGraphics;
            resultList.Add(graphics);
            var deepCopyImage = result.CogImage.CopyBase(CogImageCopyModeConstants.CopyPixels);
            PreAlignDisplay.UpdateLeftDisplay(deepCopyImage, resultList);
        }

        public void UpdateRightDisplay(PreAlignResult result)
        {
            List<CogCompositeShape> resultList = new List<CogCompositeShape>();

            var rightResult = result.PreAlignMark.FoundedMark.Right;
            var graphics = rightResult.MaxMatchPos.ResultGraphics;
            resultList.Add(graphics);
            var deepCopyImage = result.CogImage.CopyBase(CogImageCopyModeConstants.CopyPixels);
            PreAlignDisplay.UpdateRightDisplay(deepCopyImage, resultList);
        }

        public void ClearImage()
        {
            PreAlignDisplay.ClearImage();
        }
        #endregion
    }
}
