using Jastech.Framework.Device.Cameras;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class UnitSelectControl : UserControl
    {
        #region 속성
        public string UnitName { get; set; } = string.Empty;
        #endregion

        #region 이벤트
        public event SendClickEventDelegate SendEventHandler;
        #endregion

        #region 델리게이트
        public delegate void SendClickEventDelegate(string unitName, SensorType sensorType);
        #endregion

        #region 생성자
        public UnitSelectControl()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void UnitSelectControl_Load(object sender, EventArgs e)
        {
            InitializeUI();
            MakeUnitSelectList();
        }

        private void InitializeUI()
        {
            lblUnitName.Text = UnitName;
        }

        private void MakeUnitSelectList()
        {
            tlpUnitSelect.RowStyles.Clear();
            tlpUnitSelect.ColumnStyles.Clear();
            tlpUnitSelect.Dock = DockStyle.Fill;
            tlpUnitSelect.ColumnCount = Enum.GetValues(typeof(SensorType)).Length;

            for (int columnIndex = 0; columnIndex < tlpUnitSelect.ColumnCount; columnIndex++)
                tlpUnitSelect.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, (float)(100 / tlpUnitSelect.ColumnCount)));

            Button btn1 = new Button();
            btn1.ForeColor = Color.White;
            btn1.Font = new Font("맑은 고딕", 20, FontStyle.Bold);
            btn1.Text = "PreAlign";
            btn1.Dock = DockStyle.Fill;
            btn1.Click += btnAreaClick;
            tlpUnitSelect.Controls.Add(btn1, 0, 0);

            Button btn2 = new Button();
            btn2.ForeColor = Color.White;
            btn2.Font = new Font("맑은 고딕", 20, FontStyle.Bold);
            btn2.Text = "Inspection";
            btn2.Dock = DockStyle.Fill;
            btn2.Click += btnLineClick;
            tlpUnitSelect.Controls.Add(btn2, 1, 0);
        }

        private void btnAreaClick(object sender, EventArgs e)
        {
            SendEventHandler(UnitName, SensorType.Area);
        }

        private void btnLineClick(object sender, EventArgs e)
        {
            SendEventHandler(UnitName, SensorType.Line);
        }
        #endregion
    }
}
