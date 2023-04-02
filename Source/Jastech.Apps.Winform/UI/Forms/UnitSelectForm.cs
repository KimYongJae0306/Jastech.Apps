using Jastech.Apps.Structure;
using Jastech.Framework.Device.Cameras;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jastech.Apps.Winform.UI.Forms
{
    public partial class UnitSelectForm : Form
    {
        #region 필드
        #endregion

        #region 속성
        public string UnitName { get; set; } = "";
        public SensorType SensorType { get; set; } = SensorType.Area;
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public UnitSelectForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 메서드
        private void SelectUnit()
        {
            UnitName = "0";
            SensorType = SensorType.Area;

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void UnitSelectForm_Load(object sender, EventArgs e)
        {
            MakeUnitSelect();
        }

        private TableLayoutPanel CreateColumnIndex(int rowIndex)
        {
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;
            tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            tlp.ColumnCount = Enum.GetValues(typeof(SensorType)).Length + 1;

            for (int columnIndex = 0; columnIndex < tlp.ColumnCount; columnIndex++)
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, (float)(100 / tlp.ColumnCount)));

            Label lbl = new Label();
            lbl.BackColor = Color.White;
            lbl.ForeColor = Color.Black;
            lbl.BorderStyle = BorderStyle.FixedSingle;
            lbl.Font = new Font("맑은 고딕", 20, FontStyle.Bold);
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Text = "Unit " + rowIndex.ToString();
            lbl.Dock = DockStyle.Fill;
            lbl.Margin = new Padding(3);
            tlp.Controls.Add(lbl, 0, 0);

            Button btn1 = new Button();
            btn1.BackColor = Color.White;
            btn1.ForeColor = Color.Black;
            btn1.Font = new Font("맑은 고딕", 20, FontStyle.Bold);
            btn1.Text = lbl.Text + "\r\n" + "PreAlign"; // currentModel.UnitList[columnIndex].Name;
            btn1.Dock = DockStyle.Fill;
            btn1.Click += btnClick;
            tlp.Controls.Add(btn1, 1, 0);

            Button btn2 = new Button();
            btn2.BackColor = Color.White;
            btn2.ForeColor = Color.Black;
            btn2.Font = new Font("맑은 고딕", 20, FontStyle.Bold);
            btn2.Text = lbl.Text + "\r\n" + "Inspection"; // currentModel.UnitList[columnIndex].Name;
            btn2.Dock = DockStyle.Fill;
            btn2.Click += btnClick;
            tlp.Controls.Add(btn2, 2, 0);

            return tlp;
        }

        private void btnClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            string tt = btn.Text;
            Console.WriteLine(tt);
        }

        private void MakeUnitSelect()
        {
            var currentModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            tlpUnitSelect.RowCount = 2;//currentModel.UnitCount;

            for (int rowIndex = 0; rowIndex < tlpUnitSelect.RowCount; rowIndex++)
            {
                tlpUnitSelect.RowStyles.Add(new RowStyle(SizeType.Percent, (float)(100 / tlpUnitSelect.RowCount)));
                tlpUnitSelect.Controls.Add(CreateColumnIndex(rowIndex), 0, rowIndex);
            }
        }
        #endregion

        private void lblCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
