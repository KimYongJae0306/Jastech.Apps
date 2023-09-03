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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpProcessCapability = new System.Windows.Forms.TableLayoutPanel();
            this.pnlChart = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tlpTabSelection = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTab = new System.Windows.Forms.Label();
            this.pnlTabs = new System.Windows.Forms.Panel();
            this.pnlChartTypes = new System.Windows.Forms.Panel();
            this.lblLx = new System.Windows.Forms.Label();
            this.lblLy = new System.Windows.Forms.Label();
            this.lblCx = new System.Windows.Forms.Label();
            this.lblRx = new System.Windows.Forms.Label();
            this.lblRy = new System.Windows.Forms.Label();
            this.lblAllData = new System.Windows.Forms.Label();
            this.tlpSpecLimits = new System.Windows.Forms.TableLayoutPanel();
            this.lblUSL = new System.Windows.Forms.Label();
            this.lblLSL = new System.Windows.Forms.Label();
            this.lblUpperSpecLimit = new System.Windows.Forms.Label();
            this.lblLowerSpecLimit = new System.Windows.Forms.Label();
            this.tlpPCResult = new System.Windows.Forms.TableLayoutPanel();
            this.lblResultTitle1 = new System.Windows.Forms.Label();
            this.dgvPCResult = new Jastech.Framework.Winform.Controls.DoubleBufferedDatagridView();
            this.tlpAlignData = new System.Windows.Forms.TableLayoutPanel();
            this.lblResultTitle2 = new System.Windows.Forms.Label();
            this.dgvAlignData = new Jastech.Framework.Winform.Controls.DoubleBufferedDatagridView();
            this.tlpProcessCapability.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tlpTabSelection.SuspendLayout();
            this.pnlChartTypes.SuspendLayout();
            this.tlpSpecLimits.SuspendLayout();
            this.tlpPCResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPCResult)).BeginInit();
            this.tlpAlignData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlignData)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpProcessCapability
            // 
            this.tlpProcessCapability.ColumnCount = 2;
            this.tlpProcessCapability.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpProcessCapability.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpProcessCapability.Controls.Add(this.pnlChart, 1, 0);
            this.tlpProcessCapability.Controls.Add(this.panel1, 0, 0);
            this.tlpProcessCapability.Controls.Add(this.tlpPCResult, 0, 1);
            this.tlpProcessCapability.Controls.Add(this.tlpAlignData, 1, 1);
            this.tlpProcessCapability.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpProcessCapability.Location = new System.Drawing.Point(0, 0);
            this.tlpProcessCapability.Margin = new System.Windows.Forms.Padding(0);
            this.tlpProcessCapability.Name = "tlpProcessCapability";
            this.tlpProcessCapability.RowCount = 3;
            this.tlpProcessCapability.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpProcessCapability.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpProcessCapability.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlpProcessCapability.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpProcessCapability.Size = new System.Drawing.Size(1072, 554);
            this.tlpProcessCapability.TabIndex = 0;
            // 
            // pnlChart
            // 
            this.pnlChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChart.Location = new System.Drawing.Point(536, 0);
            this.pnlChart.Margin = new System.Windows.Forms.Padding(0);
            this.pnlChart.Name = "pnlChart";
            this.pnlChart.Size = new System.Drawing.Size(536, 254);
            this.pnlChart.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tlpTabSelection);
            this.panel1.Controls.Add(this.tlpSpecLimits);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(536, 254);
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
            this.tlpTabSelection.Location = new System.Drawing.Point(18, 16);
            this.tlpTabSelection.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTabSelection.Name = "tlpTabSelection";
            this.tlpTabSelection.RowCount = 2;
            this.tlpTabSelection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTabSelection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTabSelection.Size = new System.Drawing.Size(500, 124);
            this.tlpTabSelection.TabIndex = 7;
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
            // pnlChartTypes
            // 
            this.pnlChartTypes.Controls.Add(this.lblLx);
            this.pnlChartTypes.Controls.Add(this.lblLy);
            this.pnlChartTypes.Controls.Add(this.lblCx);
            this.pnlChartTypes.Controls.Add(this.lblRx);
            this.pnlChartTypes.Controls.Add(this.lblRy);
            this.pnlChartTypes.Controls.Add(this.lblAllData);
            this.pnlChartTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChartTypes.Location = new System.Drawing.Point(100, 62);
            this.pnlChartTypes.Margin = new System.Windows.Forms.Padding(0);
            this.pnlChartTypes.Name = "pnlChartTypes";
            this.pnlChartTypes.Size = new System.Drawing.Size(400, 62);
            this.pnlChartTypes.TabIndex = 8;
            // 
            // lblLx
            // 
            this.lblLx.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLx.Location = new System.Drawing.Point(72, 6);
            this.lblLx.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblLx.Name = "lblLx";
            this.lblLx.Size = new System.Drawing.Size(55, 50);
            this.lblLx.TabIndex = 1;
            this.lblLx.Text = "Lx";
            this.lblLx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLx.Click += new System.EventHandler(this.lblLx_Click);
            // 
            // lblLy
            // 
            this.lblLy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLy.Location = new System.Drawing.Point(139, 6);
            this.lblLy.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblLy.Name = "lblLy";
            this.lblLy.Size = new System.Drawing.Size(55, 50);
            this.lblLy.TabIndex = 2;
            this.lblLy.Text = "Ly";
            this.lblLy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLy.Click += new System.EventHandler(this.lblLy_Click);
            // 
            // lblCx
            // 
            this.lblCx.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCx.Location = new System.Drawing.Point(206, 6);
            this.lblCx.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblCx.Name = "lblCx";
            this.lblCx.Size = new System.Drawing.Size(55, 50);
            this.lblCx.TabIndex = 3;
            this.lblCx.Text = "Cx";
            this.lblCx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCx.Click += new System.EventHandler(this.lblCx_Click);
            // 
            // lblRx
            // 
            this.lblRx.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRx.Location = new System.Drawing.Point(273, 6);
            this.lblRx.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblRx.Name = "lblRx";
            this.lblRx.Size = new System.Drawing.Size(55, 50);
            this.lblRx.TabIndex = 4;
            this.lblRx.Text = "Rx";
            this.lblRx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRx.Click += new System.EventHandler(this.lblRx_Click);
            // 
            // lblRy
            // 
            this.lblRy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRy.Location = new System.Drawing.Point(340, 6);
            this.lblRy.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblRy.Name = "lblRy";
            this.lblRy.Size = new System.Drawing.Size(55, 50);
            this.lblRy.TabIndex = 5;
            this.lblRy.Text = "Ry";
            this.lblRy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRy.Click += new System.EventHandler(this.lblRy_Click);
            // 
            // lblAllData
            // 
            this.lblAllData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAllData.Location = new System.Drawing.Point(5, 6);
            this.lblAllData.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.lblAllData.Name = "lblAllData";
            this.lblAllData.Size = new System.Drawing.Size(55, 50);
            this.lblAllData.TabIndex = 0;
            this.lblAllData.Text = "All";
            this.lblAllData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAllData.Click += new System.EventHandler(this.lblAllData_Click);
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
            this.tlpSpecLimits.Location = new System.Drawing.Point(18, 156);
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
            this.tlpPCResult.Controls.Add(this.lblResultTitle1, 0, 0);
            this.tlpPCResult.Controls.Add(this.dgvPCResult, 0, 1);
            this.tlpPCResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPCResult.Location = new System.Drawing.Point(3, 257);
            this.tlpPCResult.Name = "tlpPCResult";
            this.tlpPCResult.RowCount = 2;
            this.tlpPCResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpPCResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPCResult.Size = new System.Drawing.Size(530, 248);
            this.tlpPCResult.TabIndex = 3;
            // 
            // lblResultTitle1
            // 
            this.lblResultTitle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.lblResultTitle1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResultTitle1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResultTitle1.Location = new System.Drawing.Point(0, 0);
            this.lblResultTitle1.Margin = new System.Windows.Forms.Padding(0);
            this.lblResultTitle1.Name = "lblResultTitle1";
            this.lblResultTitle1.Size = new System.Drawing.Size(530, 30);
            this.lblResultTitle1.TabIndex = 7;
            this.lblResultTitle1.Text = "CPK / PPK Result";
            this.lblResultTitle1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvPCResult
            // 
            this.dgvPCResult.AllowUserToAddRows = false;
            this.dgvPCResult.AllowUserToDeleteRows = false;
            this.dgvPCResult.AllowUserToResizeRows = false;
            this.dgvPCResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPCResult.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPCResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPCResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPCResult.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPCResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPCResult.EnableHeadersVisualStyles = false;
            this.dgvPCResult.Location = new System.Drawing.Point(0, 30);
            this.dgvPCResult.Margin = new System.Windows.Forms.Padding(0);
            this.dgvPCResult.Name = "dgvPCResult";
            this.dgvPCResult.ReadOnly = true;
            this.dgvPCResult.RowHeadersVisible = false;
            this.dgvPCResult.RowTemplate.Height = 23;
            this.dgvPCResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPCResult.Size = new System.Drawing.Size(530, 218);
            this.dgvPCResult.TabIndex = 6;
            this.dgvPCResult.DataSourceChanged += new System.EventHandler(this.dgvPCResult_DataSourceChanged);
            // 
            // tlpAlignData
            // 
            this.tlpAlignData.ColumnCount = 1;
            this.tlpAlignData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlignData.Controls.Add(this.lblResultTitle2, 0, 0);
            this.tlpAlignData.Controls.Add(this.dgvAlignData, 0, 1);
            this.tlpAlignData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAlignData.Location = new System.Drawing.Point(539, 257);
            this.tlpAlignData.Name = "tlpAlignData";
            this.tlpAlignData.RowCount = 2;
            this.tlpAlignData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpAlignData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlignData.Size = new System.Drawing.Size(530, 248);
            this.tlpAlignData.TabIndex = 4;
            // 
            // lblResultTitle2
            // 
            this.lblResultTitle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.lblResultTitle2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResultTitle2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResultTitle2.Location = new System.Drawing.Point(0, 0);
            this.lblResultTitle2.Margin = new System.Windows.Forms.Padding(0);
            this.lblResultTitle2.Name = "lblResultTitle2";
            this.lblResultTitle2.Size = new System.Drawing.Size(530, 30);
            this.lblResultTitle2.TabIndex = 8;
            this.lblResultTitle2.Text = "Align Result";
            this.lblResultTitle2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvAlignData
            // 
            this.dgvAlignData.AllowUserToAddRows = false;
            this.dgvAlignData.AllowUserToDeleteRows = false;
            this.dgvAlignData.AllowUserToResizeRows = false;
            this.dgvAlignData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAlignData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAlignData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAlignData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAlignData.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvAlignData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAlignData.EnableHeadersVisualStyles = false;
            this.dgvAlignData.Location = new System.Drawing.Point(3, 33);
            this.dgvAlignData.Name = "dgvAlignData";
            this.dgvAlignData.ReadOnly = true;
            this.dgvAlignData.RowHeadersVisible = false;
            this.dgvAlignData.RowTemplate.Height = 23;
            this.dgvAlignData.Size = new System.Drawing.Size(524, 212);
            this.dgvAlignData.TabIndex = 3;
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
            this.panel1.ResumeLayout(false);
            this.tlpTabSelection.ResumeLayout(false);
            this.pnlChartTypes.ResumeLayout(false);
            this.tlpSpecLimits.ResumeLayout(false);
            this.tlpPCResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPCResult)).EndInit();
            this.tlpAlignData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlignData)).EndInit();
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
        private System.Windows.Forms.TableLayoutPanel tlpAlignData;
        private System.Windows.Forms.Label lblResultTitle2;
        private System.Windows.Forms.TableLayoutPanel tlpPCResult;
        private System.Windows.Forms.Label lblResultTitle1;
        private DoubleBufferedDatagridView dgvPCResult;
    }
}
