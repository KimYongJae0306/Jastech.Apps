using Jastech.Apps.Structure;
using Jastech.Apps.Winform.UI.Controls;
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
        public string UnitName { get; private set; } = "";
        public SensorType SensorType { get; private set; } = SensorType.Area;
        private UnitSelectControl UnitSelectControl { get; set; } = new UnitSelectControl();
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
        private void UnitSelectForm_Load(object sender, EventArgs e)
        {
            AddControl();
        }

        private void AddControl()
        {
            var currentModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            tlpUnitSelect.RowCount = currentModel.UnitCount;
            for (int rowIndex = 0; rowIndex < tlpUnitSelect.RowCount; rowIndex++)
            {
                tlpUnitSelect.RowStyles.Add(new RowStyle(SizeType.Percent, (float)(100 / tlpUnitSelect.RowCount)));
                UnitSelectControl.Dock = DockStyle.Fill;
                UnitSelectControl.UnitName = currentModel.GetUnitList()[rowIndex].Name;
                UnitSelectControl.SendEventHandler += new UnitSelectControl.SendClickEventDelegate(ReceiveClickEvent);
                tlpUnitSelect.Controls.Add(UnitSelectControl, 0, rowIndex);
            }
        }

        private void ReceiveClickEvent(string unitName, SensorType sensorType)
        {
            UnitName = unitName;
            SensorType = sensorType;

            Console.WriteLine("UnitName : " + unitName);
            Console.WriteLine("SensorType : " + sensorType.ToString());

            this.DialogResult = DialogResult.OK;
        }

        private void lblCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void UnitSelectForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}
