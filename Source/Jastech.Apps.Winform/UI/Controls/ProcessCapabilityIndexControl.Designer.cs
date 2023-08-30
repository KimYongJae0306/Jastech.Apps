using Jastech.Framework.Winform.Controls;

namespace Jastech.Apps.Winform.UI.Controls
{
    partial class ProcessCapabilityIndexControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpProcessCapability = new System.Windows.Forms.TableLayoutPanel();
            this.pnlChart = new System.Windows.Forms.Panel();
            this.dgvAlignData = new Jastech.Framework.Winform.Controls.DoubleBufferedDatagridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tlpTabSelection = new System.Windows.Forms.TableLayoutPanel();
            this.lblTab = new System.Windows.Forms.Label();
            this.pnlTabs = new System.Windows.Forms.Panel();
            this.tlpCountConfig = new System.Windows.Forms.TableLayoutPanel();
            this.lblDayCounts = new System.Windows.Forms.Label();
            this.lblDayCount = new System.Windows.Forms.Label();
            this.lblDataCounts = new System.Windows.Forms.Label();
            this.lblDataCount = new System.Windows.Forms.Label();
            this.tlpSpecLimits = new System.Windows.Forms.TableLayoutPanel();
            this.lblUSL = new System.Windows.Forms.Label();
            this.lblLSL = new System.Windows.Forms.Label();
            this.lblUpperSpecLimit = new System.Windows.Forms.Label();
            this.lblLowerSpecLimit = new System.Windows.Forms.Label();
            this.tlpPCResult = new System.Windows.Forms.TableLayoutPanel();
            this.lblResult = new System.Windows.Forms.Label();
            this.dgvPCResult = new Jastech.Framework.Winform.Controls.DoubleBufferedDatagridView();
            this.pnlChartTypes = new System.Windows.Forms.Panel();
            this.lblRy = new System.Windows.Forms.Label();
            this.lblRx = new System.Windows.Forms.Label();
            this.lblCx = new System.Windows.Forms.Label();
            this.lblLy = new System.Windows.Forms.Label();
            this.lblLx = new System.Windows.Forms.Label();
            this.lblAllData = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tlpProcessCapability.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlignData)).BeginInit();
            this.panel1.SuspendLayout();
            this.tlpTabSelection.SuspendLayout();
            this.tlpCountConfig.SuspendLayout();
            this.tlpSpecLimits.SuspendLayout();
            this.tlpPCResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPCResult)).BeginInit();
            this.pnlChartTypes.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpProcessCapability
            // 
            this.tlpProcessCapability.ColumnCount = 2;
            this.tlpProcessCapability.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpProcessCapability.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpProcessCapability.Controls.Add(this.pnlChart, 1, 0);
            this.tlpProcessCapability.Controls.Add(this.dgvAlignData, 1, 1);
            this.tlpProcessCapability.Controls.Add(this.panel1, 0, 0);
            this.tlpProcessCapability.Controls.Add(this.tlpPCResult, 0, 1);
            this.tlpProcessCapability.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpProcessCapability.Location = new System.Drawing.Point(0, 0);
            this.tlpProcessCapability.Margin = new System.Windows.Forms.Padding(0);
            this.tlpProcessCapability.Name = "tlpProcessCapability";
            this.tlpProcessCapability.RowCount = 2;
            this.tlpProcessCapability.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpProcessCapability.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpProcessCapability.Size = new System.Drawing.Size(1072, 554);
            this.tlpProcessCapability.TabIndex = 0;
            // 
            // pnlChart
            // 
            this.pnlChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChart.Location = new System.Drawing.Point(536, 0);
            this.pnlChart.Margin = new System.Windows.Forms.Padding(0);
            this.pnlChart.Name = "pnlChart";
            this.pnlChart.Size = new System.Drawing.Size(536, 277);
            this.pnlChart.TabIndex = 2;
            // 
            // dgvAlignData
            // 
            this.dgvAlignData.AllowUserToAddRows = false;
            this.dgvAlignData.AllowUserToDeleteRows = false;
            this.dgvAlignData.AllowUserToResizeRows = false;
            this.dgvAlignData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAlignData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAlignData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvAlignData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAlignData.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvAlignData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAlignData.EnableHeadersVisualStyles = false;
            this.dgvAlignData.Location = new System.Drawing.Point(539, 280);
            this.dgvAlignData.Name = "dgvAlignData";
            this.dgvAlignData.ReadOnly = true;
            this.dgvAlignData.RowHeadersVisible = false;
            this.dgvAlignData.RowTemplate.Height = 23;
            this.dgvAlignData.Size = new System.Drawing.Size(530, 271);
            this.dgvAlignData.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tlpTabSelection);
            this.panel1.Controls.Add(this.tlpCountConfig);
            this.panel1.Controls.Add(this.tlpSpecLimits);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(536, 277);
            this.panel1.TabIndex = 1;
            // 
            // tlpTabSelection
            // 
            this.tlpTabSelection.ColumnCount = 2;
            this.tlpTabSelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpTabSelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTabSelection.Controls.Add(this.label1, 0, 1);
            this.tlpTabSelection.Controls.Add(this.lblTab, 0, 0);
            this.tlpTabSelection.Controls.Add(this.pnlTabs, 1, 0);
            this.tlpTabSelection.Controls.Add(this.pnlChartTypes, 1, 1);
            this.tlpTabSelection.Location = new System.Drawing.Point(18, 102);
            this.tlpTabSelection.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTabSelection.Name = "tlpTabSelection";
            this.tlpTabSelection.RowCount = 2;
            this.tlpTabSelection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTabSelection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTabSelection.Size = new System.Drawing.Size(500, 124);
            this.tlpTabSelection.TabIndex = 7;
            // 
            // lblTab
            // 
            this.lblTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTab.Location = new System.Drawing.Point(0, 0);
            this.lblTab.Margin = new System.Windows.Forms.Padding(0);
            this.lblTab.Name = "lblTab";
            this.lblTab.Size = new System.Drawing.Size(100, 62);
            this.lblTab.TabIndex = 6;
            this.lblTab.Text = "Tab";
            this.lblTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTabs
            // 
            this.pnlTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTabs.Location = new System.Drawing.Point(103, 3);
            this.pnlTabs.Name = "pnlTabs";
            this.pnlTabs.Size = new System.Drawing.Size(394, 56);
            this.pnlTabs.TabIndex = 10;
            // 
            // tlpCountConfig
            // 
            this.tlpCountConfig.ColumnCount = 2;
            this.tlpCountConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCountConfig.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCountConfig.Controls.Add(this.lblDayCounts, 0, 0);
            this.tlpCountConfig.Controls.Add(this.lblDayCount, 1, 0);
            this.tlpCountConfig.Controls.Add(this.lblDataCounts, 0, 1);
            this.tlpCountConfig.Controls.Add(this.lblDataCount, 1, 1);
            this.tlpCountConfig.Location = new System.Drawing.Point(18, 12);
            this.tlpCountConfig.Margin = new System.Windows.Forms.Padding(0);
            this.tlpCountConfig.Name = "tlpCountConfig";
            this.tlpCountConfig.RowCount = 2;
            this.tlpCountConfig.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCountConfig.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCountConfig.Size = new System.Drawing.Size(240, 80);
            this.tlpCountConfig.TabIndex = 7;
            // 
            // lblDayCounts
            // 
            this.lblDayCounts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblDayCounts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDayCounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDayCounts.Location = new System.Drawing.Point(0, 0);
            this.lblDayCounts.Margin = new System.Windows.Forms.Padding(0);
            this.lblDayCounts.Name = "lblDayCounts";
            this.lblDayCounts.Size = new System.Drawing.Size(120, 40);
            this.lblDayCounts.TabIndex = 2;
            this.lblDayCounts.Text = "Day Count";
            this.lblDayCounts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDayCount
            // 
            this.lblDayCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblDayCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDayCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDayCount.Location = new System.Drawing.Point(120, 0);
            this.lblDayCount.Margin = new System.Windows.Forms.Padding(0);
            this.lblDayCount.Name = "lblDayCount";
            this.lblDayCount.Size = new System.Drawing.Size(120, 40);
            this.lblDayCount.TabIndex = 2;
            this.lblDayCount.Text = "1";
            this.lblDayCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDayCount.Click += new System.EventHandler(this.lblDayCount_Click);
            // 
            // lblDataCounts
            // 
            this.lblDataCounts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblDataCounts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDataCounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDataCounts.Location = new System.Drawing.Point(0, 40);
            this.lblDataCounts.Margin = new System.Windows.Forms.Padding(0);
            this.lblDataCounts.Name = "lblDataCounts";
            this.lblDataCounts.Size = new System.Drawing.Size(120, 40);
            this.lblDataCounts.TabIndex = 2;
            this.lblDataCounts.Text = "Data Count";
            this.lblDataCounts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDataCount
            // 
            this.lblDataCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDataCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDataCount.Location = new System.Drawing.Point(120, 40);
            this.lblDataCount.Margin = new System.Windows.Forms.Padding(0);
            this.lblDataCount.Name = "lblDataCount";
            this.lblDataCount.Size = new System.Drawing.Size(120, 40);
            this.lblDataCount.TabIndex = 2;
            this.lblDataCount.Text = "10";
            this.lblDataCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDataCount.Click += new System.EventHandler(this.lblDataCount_Click);
            // 
            // tlpSpecLimits
            // 
            this.tlpSpecLimits.ColumnCount = 2;
            this.tlpSpecLimits.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSpecLimits.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSpecLimits.Controls.Add(this.lblUSL, 0, 0);
            this.tlpSpecLimits.Controls.Add(this.lblLSL, 1, 0);
            this.tlpSpecLimits.Controls.Add(this.lblUpperSpecLimit, 0, 1);
            this.tlpSpecLimits.Controls.Add(this.lblLowerSpecLimit, 1, 1);
            this.tlpSpecLimits.Location = new System.Drawing.Point(278, 12);
            this.tlpSpecLimits.Margin = new System.Windows.Forms.Padding(0);
            this.tlpSpecLimits.Name = "tlpSpecLimits";
            this.tlpSpecLimits.RowCount = 2;
            this.tlpSpecLimits.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSpecLimits.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSpecLimits.Size = new System.Drawing.Size(240, 80);
            this.tlpSpecLimits.TabIndex = 5;
            // 
            // lblUSL
            // 
            this.lblUSL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblUSL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUSL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUSL.Location = new System.Drawing.Point(0, 0);
            this.lblUSL.Margin = new System.Windows.Forms.Padding(0);
            this.lblUSL.Name = "lblUSL";
            this.lblUSL.Size = new System.Drawing.Size(120, 40);
            this.lblUSL.TabIndex = 2;
            this.lblUSL.Text = "Upper\r\nSpec Limit";
            this.lblUSL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLSL
            // 
            this.lblLSL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblLSL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLSL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLSL.Location = new System.Drawing.Point(120, 0);
            this.lblLSL.Margin = new System.Windows.Forms.Padding(0);
            this.lblLSL.Name = "lblLSL";
            this.lblLSL.Size = new System.Drawing.Size(120, 40);
            this.lblLSL.TabIndex = 2;
            this.lblLSL.Text = "Lower\r\nSpec Limit";
            this.lblLSL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUpperSpecLimit
            // 
            this.lblUpperSpecLimit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblUpperSpecLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUpperSpecLimit.Location = new System.Drawing.Point(0, 40);
            this.lblUpperSpecLimit.Margin = new System.Windows.Forms.Padding(0);
            this.lblUpperSpecLimit.Name = "lblUpperSpecLimit";
            this.lblUpperSpecLimit.Size = new System.Drawing.Size(120, 40);
            this.lblUpperSpecLimit.TabIndex = 2;
            this.lblUpperSpecLimit.Text = "4";
            this.lblUpperSpecLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUpperSpecLimit.Click += new System.EventHandler(this.lblUpperSpecLimit_Click);
            // 
            // lblLowerSpecLimit
            // 
            this.lblLowerSpecLimit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLowerSpecLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLowerSpecLimit.Location = new System.Drawing.Point(120, 40);
            this.lblLowerSpecLimit.Margin = new System.Windows.Forms.Padding(0);
            this.lblLowerSpecLimit.Name = "lblLowerSpecLimit";
            this.lblLowerSpecLimit.Size = new System.Drawing.Size(120, 40);
            this.lblLowerSpecLimit.TabIndex = 2;
            this.lblLowerSpecLimit.Text = "-4";
            this.lblLowerSpecLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLowerSpecLimit.Click += new System.EventHandler(this.lblLowerSpecLimit_Click);
            // 
            // tlpPCResult
            // 
            this.tlpPCResult.ColumnCount = 1;
            this.tlpPCResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPCResult.Controls.Add(this.lblResult, 0, 0);
            this.tlpPCResult.Controls.Add(this.dgvPCResult, 0, 1);
            this.tlpPCResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPCResult.Location = new System.Drawing.Point(0, 277);
            this.tlpPCResult.Margin = new System.Windows.Forms.Padding(0);
            this.tlpPCResult.Name = "tlpPCResult";
            this.tlpPCResult.RowCount = 2;
            this.tlpPCResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpPCResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPCResult.Size = new System.Drawing.Size(536, 277);
            this.tlpPCResult.TabIndex = 3;
            // 
            // lblResult
            // 
            this.lblResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResult.Location = new System.Drawing.Point(0, 0);
            this.lblResult.Margin = new System.Windows.Forms.Padding(0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(536, 40);
            this.lblResult.TabIndex = 7;
            this.lblResult.Text = "CPK / PPK Result";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvPCResult
            // 
            this.dgvPCResult.AllowUserToAddRows = false;
            this.dgvPCResult.AllowUserToDeleteRows = false;
            this.dgvPCResult.AllowUserToResizeRows = false;
            this.dgvPCResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPCResult.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPCResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvPCResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPCResult.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvPCResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPCResult.EnableHeadersVisualStyles = false;
            this.dgvPCResult.Location = new System.Drawing.Point(0, 40);
            this.dgvPCResult.Margin = new System.Windows.Forms.Padding(0);
            this.dgvPCResult.Name = "dgvPCResult";
            this.dgvPCResult.ReadOnly = true;
            this.dgvPCResult.RowHeadersVisible = false;
            this.dgvPCResult.RowTemplate.Height = 23;
            this.dgvPCResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPCResult.Size = new System.Drawing.Size(536, 237);
            this.dgvPCResult.TabIndex = 6;
            this.dgvPCResult.DataSourceChanged += new System.EventHandler(this.dgvPCResult_DataSourceChanged);
            this.dgvPCResult.SelectionChanged += new System.EventHandler(this.dgvPCResult_SelectionChanged);
            // 
            // pnlChartTypes
            // 
            this.pnlChartTypes.Controls.Add(this.lblRy);
            this.pnlChartTypes.Controls.Add(this.lblRx);
            this.pnlChartTypes.Controls.Add(this.lblCx);
            this.pnlChartTypes.Controls.Add(this.lblLy);
            this.pnlChartTypes.Controls.Add(this.lblLx);
            this.pnlChartTypes.Controls.Add(this.lblAllData);
            this.pnlChartTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChartTypes.Location = new System.Drawing.Point(100, 62);
            this.pnlChartTypes.Margin = new System.Windows.Forms.Padding(0);
            this.pnlChartTypes.Name = "pnlChartTypes";
            this.pnlChartTypes.Size = new System.Drawing.Size(400, 62);
            this.pnlChartTypes.TabIndex = 8;
            // 
            // lblRy
            // 
            this.lblRy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRy.Location = new System.Drawing.Point(342, 6);
            this.lblRy.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblRy.Name = "lblRy";
            this.lblRy.Size = new System.Drawing.Size(55, 50);
            this.lblRy.TabIndex = 5;
            this.lblRy.Text = "Ry";
            this.lblRy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRy.Click += new System.EventHandler(this.lblRy_Click);
            // 
            // lblRx
            // 
            this.lblRx.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRx.Location = new System.Drawing.Point(275, 6);
            this.lblRx.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblRx.Name = "lblRx";
            this.lblRx.Size = new System.Drawing.Size(55, 50);
            this.lblRx.TabIndex = 4;
            this.lblRx.Text = "Rx";
            this.lblRx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRx.Click += new System.EventHandler(this.lblRx_Click);
            // 
            // lblCx
            // 
            this.lblCx.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCx.Location = new System.Drawing.Point(208, 6);
            this.lblCx.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblCx.Name = "lblCx";
            this.lblCx.Size = new System.Drawing.Size(55, 50);
            this.lblCx.TabIndex = 3;
            this.lblCx.Text = "Cx";
            this.lblCx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCx.Click += new System.EventHandler(this.lblCx_Click);
            // 
            // lblLy
            // 
            this.lblLy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLy.Location = new System.Drawing.Point(141, 6);
            this.lblLy.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblLy.Name = "lblLy";
            this.lblLy.Size = new System.Drawing.Size(55, 50);
            this.lblLy.TabIndex = 2;
            this.lblLy.Text = "Ly";
            this.lblLy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLy.Click += new System.EventHandler(this.lblLy_Click);
            // 
            // lblLx
            // 
            this.lblLx.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLx.Location = new System.Drawing.Point(74, 6);
            this.lblLx.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblLx.Name = "lblLx";
            this.lblLx.Size = new System.Drawing.Size(55, 50);
            this.lblLx.TabIndex = 1;
            this.lblLx.Text = "Lx";
            this.lblLx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLx.Click += new System.EventHandler(this.lblLx_Click);
            // 
            // lblAllData
            // 
            this.lblAllData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAllData.Location = new System.Drawing.Point(7, 6);
            this.lblAllData.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.lblAllData.Name = "lblAllData";
            this.lblAllData.Size = new System.Drawing.Size(55, 50);
            this.lblAllData.TabIndex = 0;
            this.lblAllData.Text = "All";
            this.lblAllData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAllData.Click += new System.EventHandler(this.lblAllData_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 62);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 62);
            this.label1.TabIndex = 11;
            this.label1.Text = "Type";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProcessCapabilityIndexControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpProcessCapability);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ProcessCapabilityIndexControl";
            this.Size = new System.Drawing.Size(1072, 554);
            this.Load += new System.EventHandler(this.ProcessCapabilityIndexControl_Load);
            this.tlpProcessCapability.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlignData)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tlpTabSelection.ResumeLayout(false);
            this.tlpCountConfig.ResumeLayout(false);
            this.tlpSpecLimits.ResumeLayout(false);
            this.tlpPCResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPCResult)).EndInit();
            this.pnlChartTypes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpProcessCapability;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tlpSpecLimits;
        private System.Windows.Forms.Label lblUSL;
        private System.Windows.Forms.Label lblLSL;
        private System.Windows.Forms.Label lblUpperSpecLimit;
        private System.Windows.Forms.Label lblLowerSpecLimit;
        private System.Windows.Forms.TableLayoutPanel tlpPCResult;
        private System.Windows.Forms.Label lblResult;
        private DoubleBufferedDatagridView dgvPCResult;
        private System.Windows.Forms.TableLayoutPanel tlpCountConfig;
        private System.Windows.Forms.Label lblDayCounts;
        private System.Windows.Forms.Label lblDayCount;
        private System.Windows.Forms.Label lblDataCounts;
        private System.Windows.Forms.Label lblDataCount;
        private System.Windows.Forms.Label lblTab;
        private System.Windows.Forms.TableLayoutPanel tlpTabSelection;
        private DoubleBufferedDatagridView dgvAlignData;
        private System.Windows.Forms.Panel pnlTabs;
        private System.Windows.Forms.Panel pnlChart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlChartTypes;
        private System.Windows.Forms.Label lblRy;
        private System.Windows.Forms.Label lblRx;
        private System.Windows.Forms.Label lblCx;
        private System.Windows.Forms.Label lblLy;
        private System.Windows.Forms.Label lblLx;
        private System.Windows.Forms.Label lblAllData;
    }
}
