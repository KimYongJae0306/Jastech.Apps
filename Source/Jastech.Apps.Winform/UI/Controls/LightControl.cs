using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jastech.Framework.Device.LightCtrls;

namespace Jastech.Apps.Winform.UI.Controls
{
    public partial class LightControl : UserControl
    {
        private bool _isLoding { get; set; } = false;

        private bool _usingScroll { get; set; } = false;

        private bool _usingNupdn { get; set; } = false;

        private int _curChannelIndex { get; set; } = 0;

        public LightCtrlHandler LightCtrlHandler { get; private set; } = null;

        public LightParameter LightParam { get; private set; } = null;

        public LightControl()
        {
            InitializeComponent();
        }

        private void LightControl_Load(object sender, EventArgs e)
        {
            InitializeData();
            //UpdateLightValue();
        }

        public void SetParam(LightCtrlHandler lightCtrlHandler, LightParameter lightParam)
        {
            LightCtrlHandler = lightCtrlHandler;
            LightParam = lightParam;
        }

        public void UpdateSetParam(LightParameter lightParam)
        {
            LightParam = lightParam;
            InitializeData();
        }

        public void InitializeData()
        {
            if (LightCtrlHandler == null || LightParam == null)
                return;

            _isLoding = true;

            var keys = LightParam.Map.Keys.ToArray();

            cbxControlNameList.Items.Clear();
            foreach (var key in keys)
                cbxControlNameList.Items.Add(key.ToString());

            if(cbxControlNameList.Items.Count > 0)
                cbxControlNameList.SelectedIndex = 0;

            InitializeChannelUI();
            UpdateLightValue();

            _isLoding = false;
        }

        private void UpdateLightValue()
        {
            if (LightCtrlHandler == null)
                return;

            if (cbxControlNameList.Items.Count < 0 || cbxChannelNameList.Items.Count < 0)
                return;

            string ctrlName = cbxControlNameList.SelectedItem as string;
            string channel = cbxChannelNameList.SelectedItem as string;

            var lightControl = LightCtrlHandler.Get(ctrlName);
            if (lightControl == null)
                return;

            var channelNum = lightControl.ChannelNameMap[channel];

            var lightValue = LightParam.Map[ctrlName].LightLevels[channelNum];
            UpdateNupdn(lightValue);
        }

        public delegate void UpdateNupdnDele(decimal lightValue);
        private void UpdateNupdn(decimal lightValue)
        {
            if(this.InvokeRequired)
            {
                UpdateNupdnDele callback = UpdateNupdn;
                this.Invoke(callback, lightValue);
                return;
            }
            nupdnLightLevel.Value = lightValue;
        }

        private void DrawComboboxCenterAlign(object sender, DrawItemEventArgs e)
        {
            try
            {
                ComboBox cmb = sender as ComboBox;

                if (cmb != null)
                {
                    e.DrawBackground();

                    if (cmb.Name.ToString().ToLower().Contains("group"))
                        cmb.ItemHeight = lblType.Height - 6;
                    else
                        cmb.ItemHeight = lblType.Height - 6;

                    if (e.Index >= 0)
                    {
                        StringFormat sf = new StringFormat();
                        sf.LineAlignment = StringAlignment.Center;
                        sf.Alignment = StringAlignment.Center;

                        Brush brush = new SolidBrush(cmb.ForeColor);

                        if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                            brush = SystemBrushes.HighlightText;

                        e.Graphics.DrawString(cmb.Items[e.Index].ToString(), cmb.Font, brush, e.Bounds, sf);
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
                throw;
            }

        }

        private void Combobox_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawComboboxCenterAlign(sender, e);
        }

        private void InitializeChannelUI()
        {
            string ctrlName = cbxControlNameList.SelectedItem as string;
            var lightControl = LightCtrlHandler.Get(ctrlName);

            if (lightControl == null)
                return;

            cbxChannelNameList.Items.Clear();

            foreach (var channel in lightControl.ChannelNameMap.Keys)
                cbxChannelNameList.Items.Add(channel);

            if (cbxChannelNameList.Items.Count > 0)
                cbxChannelNameList.SelectedIndex = _curChannelIndex;
        }

        private void lblPrevControl_Click(object sender, EventArgs e)
        {
            int tempIndex = cbxControlNameList.SelectedIndex - 1;
            if (tempIndex < 0)
                return;

            cbxControlNameList.SelectedIndex = tempIndex;
        }

        private void lblNextControl_Click(object sender, EventArgs e)
        {
            int tempIndex = cbxControlNameList.SelectedIndex + 1;
            if (cbxControlNameList.Items.Count <= tempIndex)
                return;

            cbxControlNameList.SelectedIndex = tempIndex;
        }

        private void trbLightLevelValue_Scroll(object sender, EventArgs e)
        {
            if (_usingNupdn)
                return;

            _usingScroll = true;

            int value = trbLightLevelValue.Value;
            nupdnLightLevel.Value = value;

            SetLightValue(value);

            _usingScroll = false;
        }

        private void nupdnLightLevel_ValueChanged(object sender, EventArgs e)
        {
            if (_usingScroll)
                return;
            _usingNupdn = true;

            int value = (int)nupdnLightLevel.Value;
            trbLightLevelValue.Value = value;

            SetLightValue(value);

            _usingNupdn = false;
        }

        private void SetLightValue(int value)
        {
            string ctrlName = cbxControlNameList.SelectedItem as string;
            string channel = cbxChannelNameList.SelectedItem as string;

            var lightControl = LightCtrlHandler.Get(ctrlName);
            var channelNum = lightControl.ChannelNameMap[channel];

            LightParam.Map[ctrlName].LightLevels[channelNum] = value;

            lightControl.TurnOn(channelNum, value);
        }

        private void lblPrevChannel_Click(object sender, EventArgs e)
        {
            int tempIndex = cbxChannelNameList.SelectedIndex - 1;
            if (tempIndex < 0)
                return;

            cbxChannelNameList.SelectedIndex = tempIndex;
        }

        private void lblNextChannel_Click(object sender, EventArgs e)
        {
            int tempIndex = cbxChannelNameList.SelectedIndex + 1;
            if (cbxChannelNameList.Items.Count <= tempIndex)
                return;

            cbxChannelNameList.SelectedIndex = tempIndex;
        }

        private void cbxControlNameList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoding)
                return;

            _curChannelIndex = 0;

            InitializeChannelUI();
            UpdateLightValue();
        }

        private void cbxChannelNameList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoding)
                return;

            _curChannelIndex = cbxChannelNameList.SelectedIndex;

            UpdateLightValue();
        }

        private void lblLightOn_Click(object sender, EventArgs e)
        {
            if (LightCtrlHandler == null)
                return;

            string ctrlName = cbxControlNameList.SelectedItem as string;
            string channel = cbxChannelNameList.SelectedItem as string;

            var lightControl = LightCtrlHandler.Get(ctrlName);
            var channelNum = lightControl.ChannelNameMap[channel];

            lightControl.TurnOn(LightParam.Map[ctrlName]);
        }

        private void lblLightOff_Click(object sender, EventArgs e)
        {
            if (LightCtrlHandler == null)
                return;

            string ctrlName = cbxControlNameList.SelectedItem as string;
            string channel = cbxChannelNameList.SelectedItem as string;

            var lightControl = LightCtrlHandler.Get(ctrlName);
            var channelNum = lightControl.ChannelNameMap[channel];

            lightControl.TurnOff();
        }
    }
}
