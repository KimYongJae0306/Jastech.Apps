namespace Jastech.Apps.Winform.UI.Controls
{
    partial class LightControl
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpLight = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblLightOff = new System.Windows.Forms.Label();
            this.lblLightOn = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTrbDimmingLevelValue = new System.Windows.Forms.Panel();
            this.trbLightLevelValue = new System.Windows.Forms.TrackBar();
            this.label12 = new System.Windows.Forms.Label();
            this.nupdnLightLevel = new System.Windows.Forms.NumericUpDown();
            this.lblLight = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.cbxChannelNameList = new System.Windows.Forms.ComboBox();
            this.lblNextChannel = new System.Windows.Forms.Label();
            this.lblPrevChannel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblNextControl = new System.Windows.Forms.Label();
            this.lblPrevControl = new System.Windows.Forms.Label();
            this.cbxControlNameList = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.tlpLight.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.pnlTrbDimmingLevelValue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbLightLevelValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupdnLightLevel)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpLight
            // 
            this.tlpLight.ColumnCount = 1;
            this.tlpLight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tlpLight.Controls.Add(this.tableLayoutPanel1, 0, 4);
            this.tlpLight.Controls.Add(this.tableLayoutPanel4, 0, 3);
            this.tlpLight.Controls.Add(this.lblLight, 0, 0);
            this.tlpLight.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tlpLight.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tlpLight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLight.Location = new System.Drawing.Point(0, 0);
            this.tlpLight.Margin = new System.Windows.Forms.Padding(0);
            this.tlpLight.Name = "tlpLight";
            this.tlpLight.RowCount = 6;
            this.tlpLight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpLight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpLight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpLight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpLight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpLight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLight.Size = new System.Drawing.Size(472, 242);
            this.tlpLight.TabIndex = 296;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tableLayoutPanel1.Controls.Add(this.lblLightOff, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblLightOn, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 188);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(472, 50);
            this.tableLayoutPanel1.TabIndex = 305;
            // 
            // lblLightOff
            // 
            this.lblLightOff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblLightOff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLightOff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLightOff.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.lblLightOff.Location = new System.Drawing.Point(378, 0);
            this.lblLightOff.Margin = new System.Windows.Forms.Padding(0);
            this.lblLightOff.Name = "lblLightOff";
            this.lblLightOff.Size = new System.Drawing.Size(94, 50);
            this.lblLightOff.TabIndex = 2;
            this.lblLightOff.Text = "Off";
            this.lblLightOff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLightOff.Click += new System.EventHandler(this.lblLightOff_Click);
            // 
            // lblLightOn
            // 
            this.lblLightOn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblLightOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLightOn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLightOn.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.lblLightOn.Location = new System.Drawing.Point(284, 0);
            this.lblLightOn.Margin = new System.Windows.Forms.Padding(0);
            this.lblLightOn.Name = "lblLightOn";
            this.lblLightOn.Size = new System.Drawing.Size(94, 50);
            this.lblLightOn.TabIndex = 1;
            this.lblLightOn.Text = "On";
            this.lblLightOn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLightOn.Click += new System.EventHandler(this.lblLightOn_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.Controls.Add(this.pnlTrbDimmingLevelValue, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.label12, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.nupdnLightLevel, 2, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.tableLayoutPanel4.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 138);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(472, 50);
            this.tableLayoutPanel4.TabIndex = 305;
            // 
            // pnlTrbDimmingLevelValue
            // 
            this.pnlTrbDimmingLevelValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTrbDimmingLevelValue.Controls.Add(this.trbLightLevelValue);
            this.pnlTrbDimmingLevelValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTrbDimmingLevelValue.Location = new System.Drawing.Point(173, 0);
            this.pnlTrbDimmingLevelValue.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTrbDimmingLevelValue.Name = "pnlTrbDimmingLevelValue";
            this.pnlTrbDimmingLevelValue.Padding = new System.Windows.Forms.Padding(0, 11, 0, 0);
            this.pnlTrbDimmingLevelValue.Size = new System.Drawing.Size(173, 50);
            this.pnlTrbDimmingLevelValue.TabIndex = 209;
            // 
            // trbLightLevelValue
            // 
            this.trbLightLevelValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.trbLightLevelValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trbLightLevelValue.Location = new System.Drawing.Point(0, 11);
            this.trbLightLevelValue.Margin = new System.Windows.Forms.Padding(0);
            this.trbLightLevelValue.Maximum = 255;
            this.trbLightLevelValue.Name = "trbLightLevelValue";
            this.trbLightLevelValue.Size = new System.Drawing.Size(171, 37);
            this.trbLightLevelValue.TabIndex = 208;
            this.trbLightLevelValue.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trbLightLevelValue.Scroll += new System.EventHandler(this.trbLightLevelValue_Scroll);
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Margin = new System.Windows.Forms.Padding(0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(173, 50);
            this.label12.TabIndex = 302;
            this.label12.Text = "Value";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nupdnLightLevel
            // 
            this.nupdnLightLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.nupdnLightLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nupdnLightLevel.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.nupdnLightLevel.ForeColor = System.Drawing.Color.White;
            this.nupdnLightLevel.Location = new System.Drawing.Point(346, 0);
            this.nupdnLightLevel.Margin = new System.Windows.Forms.Padding(0);
            this.nupdnLightLevel.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nupdnLightLevel.Name = "nupdnLightLevel";
            this.nupdnLightLevel.ReadOnly = true;
            this.nupdnLightLevel.Size = new System.Drawing.Size(125, 32);
            this.nupdnLightLevel.TabIndex = 303;
            this.nupdnLightLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nupdnLightLevel.ValueChanged += new System.EventHandler(this.nupdnLightLevel_ValueChanged);
            // 
            // lblLight
            // 
            this.lblLight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblLight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLight.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblLight.ForeColor = System.Drawing.Color.White;
            this.lblLight.Location = new System.Drawing.Point(0, 0);
            this.lblLight.Margin = new System.Windows.Forms.Padding(0);
            this.lblLight.Name = "lblLight";
            this.lblLight.Size = new System.Drawing.Size(472, 38);
            this.lblLight.TabIndex = 299;
            this.lblLight.Text = "LIGHT";
            this.lblLight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.Controls.Add(this.cbxChannelNameList, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblNextChannel, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblPrevChannel, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.tableLayoutPanel3.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 88);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(472, 50);
            this.tableLayoutPanel3.TabIndex = 304;
            // 
            // cbxChannelNameList
            // 
            this.cbxChannelNameList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxChannelNameList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxChannelNameList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxChannelNameList.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.cbxChannelNameList.FormattingEnabled = true;
            this.cbxChannelNameList.Location = new System.Drawing.Point(174, 0);
            this.cbxChannelNameList.Margin = new System.Windows.Forms.Padding(0);
            this.cbxChannelNameList.Name = "cbxChannelNameList";
            this.cbxChannelNameList.Size = new System.Drawing.Size(174, 33);
            this.cbxChannelNameList.TabIndex = 307;
            this.cbxChannelNameList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Combobox_DrawItem);
            this.cbxChannelNameList.SelectedIndexChanged += new System.EventHandler(this.cbxChannelNameList_SelectedIndexChanged);
            // 
            // lblNextChannel
            // 
            this.lblNextChannel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblNextChannel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNextChannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNextChannel.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblNextChannel.Image = global::Jastech.Apps.Winform.Properties.Resources.Next_White;
            this.lblNextChannel.Location = new System.Drawing.Point(410, 0);
            this.lblNextChannel.Margin = new System.Windows.Forms.Padding(0);
            this.lblNextChannel.Name = "lblNextChannel";
            this.lblNextChannel.Size = new System.Drawing.Size(62, 50);
            this.lblNextChannel.TabIndex = 306;
            this.lblNextChannel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNextChannel.Click += new System.EventHandler(this.lblNextChannel_Click);
            // 
            // lblPrevChannel
            // 
            this.lblPrevChannel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblPrevChannel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPrevChannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPrevChannel.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblPrevChannel.Image = global::Jastech.Apps.Winform.Properties.Resources.Prev_White;
            this.lblPrevChannel.Location = new System.Drawing.Point(348, 0);
            this.lblPrevChannel.Margin = new System.Windows.Forms.Padding(0);
            this.lblPrevChannel.Name = "lblPrevChannel";
            this.lblPrevChannel.Size = new System.Drawing.Size(62, 50);
            this.lblPrevChannel.TabIndex = 305;
            this.lblPrevChannel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPrevChannel.Click += new System.EventHandler(this.lblPrevChannel_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(174, 50);
            this.label6.TabIndex = 302;
            this.label6.Text = "Channel";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.Controls.Add(this.lblNextControl, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblPrevControl, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbxControlNameList, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblType, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.tableLayoutPanel2.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 38);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(472, 50);
            this.tableLayoutPanel2.TabIndex = 301;
            // 
            // lblNextControl
            // 
            this.lblNextControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblNextControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNextControl.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblNextControl.Image = global::Jastech.Apps.Winform.Properties.Resources.Next_White;
            this.lblNextControl.Location = new System.Drawing.Point(410, 0);
            this.lblNextControl.Margin = new System.Windows.Forms.Padding(0);
            this.lblNextControl.Name = "lblNextControl";
            this.lblNextControl.Size = new System.Drawing.Size(62, 50);
            this.lblNextControl.TabIndex = 307;
            this.lblNextControl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNextControl.Click += new System.EventHandler(this.lblNextControl_Click);
            // 
            // lblPrevControl
            // 
            this.lblPrevControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblPrevControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPrevControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPrevControl.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblPrevControl.Image = global::Jastech.Apps.Winform.Properties.Resources.Prev_White;
            this.lblPrevControl.Location = new System.Drawing.Point(348, 0);
            this.lblPrevControl.Margin = new System.Windows.Forms.Padding(0);
            this.lblPrevControl.Name = "lblPrevControl";
            this.lblPrevControl.Size = new System.Drawing.Size(62, 50);
            this.lblPrevControl.TabIndex = 306;
            this.lblPrevControl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPrevControl.Click += new System.EventHandler(this.lblPrevControl_Click);
            // 
            // cbxControlNameList
            // 
            this.cbxControlNameList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxControlNameList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxControlNameList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxControlNameList.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.cbxControlNameList.FormattingEnabled = true;
            this.cbxControlNameList.Location = new System.Drawing.Point(174, 0);
            this.cbxControlNameList.Margin = new System.Windows.Forms.Padding(0);
            this.cbxControlNameList.Name = "cbxControlNameList";
            this.cbxControlNameList.Size = new System.Drawing.Size(174, 33);
            this.cbxControlNameList.TabIndex = 304;
            this.cbxControlNameList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Combobox_DrawItem);
            this.cbxControlNameList.SelectedIndexChanged += new System.EventHandler(this.cbxControlNameList_SelectedIndexChanged);
            // 
            // lblType
            // 
            this.lblType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblType.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblType.Location = new System.Drawing.Point(0, 0);
            this.lblType.Margin = new System.Windows.Forms.Padding(0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(174, 50);
            this.lblType.TabIndex = 302;
            this.lblType.Text = "TYPE";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LightControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpLight);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "LightControl";
            this.Size = new System.Drawing.Size(472, 242);
            this.Load += new System.EventHandler(this.LightControl_Load);
            this.tlpLight.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.pnlTrbDimmingLevelValue.ResumeLayout(false);
            this.pnlTrbDimmingLevelValue.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbLightLevelValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupdnLightLevel)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpLight;
        private System.Windows.Forms.Panel pnlTrbDimmingLevelValue;
        private System.Windows.Forms.TrackBar trbLightLevelValue;
        private System.Windows.Forms.Label lblLightOff;
        private System.Windows.Forms.Label lblLightOn;
        private System.Windows.Forms.Label lblLight;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblPrevChannel;
        private System.Windows.Forms.Label lblNextChannel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.NumericUpDown nupdnLightLevel;
        private System.Windows.Forms.ComboBox cbxChannelNameList;
        private System.Windows.Forms.ComboBox cbxControlNameList;
        private System.Windows.Forms.Label lblNextControl;
        private System.Windows.Forms.Label lblPrevControl;
    }
}
