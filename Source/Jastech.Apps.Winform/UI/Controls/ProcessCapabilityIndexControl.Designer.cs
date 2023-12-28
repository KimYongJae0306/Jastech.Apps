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
            this.tlpProcessCapabilityIndicies = new System.Windows.Forms.TableLayoutPanel();
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
            this.lblCapaUSL = new System.Windows.Forms.Label();
            this.lblCapabilityUSL = new System.Windows.Forms.Label();
            this.lblCapabilityLSL = new System.Windows.Forms.Label();
            this.lblCapaLSL = new System.Windows.Forms.Label();
            this.lblPerfUSL_Center = new System.Windows.Forms.Label();
            this.lblPerformanceUSL_Center = new System.Windows.Forms.Label();
            this.lblPerfLSL_Center = new System.Windows.Forms.Label();
            this.lblPerformanceLSL_Center = new System.Windows.Forms.Label();
            this.lblPerfUSL_Side = new System.Windows.Forms.Label();
            this.lblPerformanceUSL_Side = new System.Windows.Forms.Label();
            this.lblPerfLSL_Side = new System.Windows.Forms.Label();
            this.lblPerformanceLSL_Side = new System.Windows.Forms.Label();
            this.tlpPCResult = new System.Windows.Forms.TableLayoutPanel();
            this.lblResultTitle1 = new System.Windows.Forms.Label();
            this.dgvPCResult = new Jastech.Framework.Winform.Controls.DoubleBufferedDatagridView();
            this.tlpAlignData = new System.Windows.Forms.TableLayoutPanel();
            this.lblResultTitle2 = new System.Windows.Forms.Label();
            this.dgvAlignData = new Jastech.Framework.Winform.Controls.DoubleBufferedDatagridView();
            this.tlpProcessCapabilityIndicies.SuspendLayout();
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
            // tlpProcessCapabilityIndicies
            // 
            this.tlpProcessCapabilityIndicies.ColumnCount = 2;
            this.tlpProcessCapabilityIndicies.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpProcessCapabilityIndicies.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpProcessCapabilityIndicies.Controls.Add(this.pnlChart, 1, 0);
            this.tlpProcessCapabilityIndicies.Controls.Add(this.panel1, 0, 0);
            this.tlpProcessCapabilityIndicies.Controls.Add(this.tlpPCResult, 0, 1);
            this.tlpProcessCapabilityIndicies.Controls.Add(this.tlpAlignData, 1, 1);
            this.tlpProcessCapabilityIndicies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpProcessCapabilityIndicies.Location = new System.Drawing.Point(0, 0);
            this.tlpProcessCapabilityIndicies.Margin = new System.Windows.Forms.Padding(0);
            this.tlpProcessCapabilityIndicies.Name = "tlpProcessCapabilityIndicies";
            this.tlpProcessCapabilityIndicies.RowCount = 3;
            this.tlpProcessCapabilityIndicies.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpProcessCapabilityIndicies.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpProcessCapabilityIndicies.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlpProcessCapabilityIndicies.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpProcessCapabilityIndicies.Size = new System.Drawing.Size(1072, 900);
            this.tlpProcessCapabilityIndicies.TabIndex = 0;
            // 
            // pnlChart
            // 
            this.pnlChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChart.Location = new System.Drawing.Point(536, 0);
            this.pnlChart.Margin = new System.Windows.Forms.Padding(0);
            this.pnlChart.Name = "pnlChart";
            this.pnlChart.Size = new System.Drawing.Size(536, 427);
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
            this.panel1.Size = new System.Drawing.Size(536, 427);
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
            this.tlpSpecLimits.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlpSpecLimits.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpSpecLimits.Controls.Add(this.lblCapaUSL, 0, 0);
            this.tlpSpecLimits.Controls.Add(this.lblCapabilityUSL, 1, 0);
            this.tlpSpecLimits.Controls.Add(this.lblCapabilityLSL, 1, 1);
            this.tlpSpecLimits.Controls.Add(this.lblCapaLSL, 0, 1);
            this.tlpSpecLimits.Controls.Add(this.lblPerfUSL_Center, 0, 2);
            this.tlpSpecLimits.Controls.Add(this.lblPerformanceUSL_Center, 1, 2);
            this.tlpSpecLimits.Controls.Add(this.lblPerfLSL_Center, 0, 3);
            this.tlpSpecLimits.Controls.Add(this.lblPerformanceLSL_Center, 1, 3);
            this.tlpSpecLimits.Controls.Add(this.lblPerfUSL_Side, 0, 4);
            this.tlpSpecLimits.Controls.Add(this.lblPerformanceUSL_Side, 1, 4);
            this.tlpSpecLimits.Controls.Add(this.lblPerfLSL_Side, 0, 5);
            this.tlpSpecLimits.Controls.Add(this.lblPerformanceLSL_Side, 1, 5);
            this.tlpSpecLimits.Location = new System.Drawing.Point(18, 207);
            this.tlpSpecLimits.Margin = new System.Windows.Forms.Padding(0);
            this.tlpSpecLimits.Name = "tlpSpecLimits";
            this.tlpSpecLimits.RowCount = 6;
            this.tlpSpecLimits.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpSpecLimits.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpSpecLimits.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpSpecLimits.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpSpecLimits.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpSpecLimits.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpSpecLimits.Size = new System.Drawing.Size(495, 159);
            this.tlpSpecLimits.TabIndex = 5;
            // 
            // lblCapaUSL
            // 
            this.lblCapaUSL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblCapaUSL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCapaUSL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCapaUSL.Location = new System.Drawing.Point(0, 0);
            this.lblCapaUSL.Margin = new System.Windows.Forms.Padding(0);
            this.lblCapaUSL.Name = "lblCapaUSL";
            this.lblCapaUSL.Size = new System.Drawing.Size(346, 26);
            this.lblCapaUSL.TabIndex = 2;
            this.lblCapaUSL.Text = "Capability Upper Spec Limit";
            this.lblCapaUSL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCapabilityUSL
            // 
            this.lblCapabilityUSL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCapabilityUSL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCapabilityUSL.Location = new System.Drawing.Point(346, 0);
            this.lblCapabilityUSL.Margin = new System.Windows.Forms.Padding(0);
            this.lblCapabilityUSL.Name = "lblCapabilityUSL";
            this.lblCapabilityUSL.Size = new System.Drawing.Size(149, 26);
            this.lblCapabilityUSL.TabIndex = 2;
            this.lblCapabilityUSL.Text = "4";
            this.lblCapabilityUSL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCapabilityUSL.Click += new System.EventHandler(this.lblSpecLimit_Click);
            // 
            // lblCapabilityLSL
            // 
            this.lblCapabilityLSL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCapabilityLSL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCapabilityLSL.Location = new System.Drawing.Point(346, 26);
            this.lblCapabilityLSL.Margin = new System.Windows.Forms.Padding(0);
            this.lblCapabilityLSL.Name = "lblCapabilityLSL";
            this.lblCapabilityLSL.Size = new System.Drawing.Size(149, 26);
            this.lblCapabilityLSL.TabIndex = 2;
            this.lblCapabilityLSL.Text = "-4";
            this.lblCapabilityLSL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCapabilityLSL.Click += new System.EventHandler(this.lblSpecLimit_Click);
            // 
            // lblCapaLSL
            // 
            this.lblCapaLSL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblCapaLSL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCapaLSL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCapaLSL.Location = new System.Drawing.Point(0, 26);
            this.lblCapaLSL.Margin = new System.Windows.Forms.Padding(0);
            this.lblCapaLSL.Name = "lblCapaLSL";
            this.lblCapaLSL.Size = new System.Drawing.Size(346, 26);
            this.lblCapaLSL.TabIndex = 2;
            this.lblCapaLSL.Text = "Capability Lower Spec Limit";
            this.lblCapaLSL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPerfUSL_Center
            // 
            this.lblPerfUSL_Center.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblPerfUSL_Center.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPerfUSL_Center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPerfUSL_Center.Location = new System.Drawing.Point(0, 52);
            this.lblPerfUSL_Center.Margin = new System.Windows.Forms.Padding(0);
            this.lblPerfUSL_Center.Name = "lblPerfUSL_Center";
            this.lblPerfUSL_Center.Size = new System.Drawing.Size(346, 26);
            this.lblPerfUSL_Center.TabIndex = 3;
            this.lblPerfUSL_Center.Text = "Performance Upper Spec Limit - Center";
            this.lblPerfUSL_Center.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPerformanceUSL_Center
            // 
            this.lblPerformanceUSL_Center.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPerformanceUSL_Center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPerformanceUSL_Center.Location = new System.Drawing.Point(346, 52);
            this.lblPerformanceUSL_Center.Margin = new System.Windows.Forms.Padding(0);
            this.lblPerformanceUSL_Center.Name = "lblPerformanceUSL_Center";
            this.lblPerformanceUSL_Center.Size = new System.Drawing.Size(149, 26);
            this.lblPerformanceUSL_Center.TabIndex = 10;
            this.lblPerformanceUSL_Center.Text = "4";
            this.lblPerformanceUSL_Center.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPerformanceUSL_Center.Click += new System.EventHandler(this.lblSpecLimit_Click);
            // 
            // lblPerfLSL_Center
            // 
            this.lblPerfLSL_Center.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblPerfLSL_Center.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPerfLSL_Center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPerfLSL_Center.Location = new System.Drawing.Point(0, 78);
            this.lblPerfLSL_Center.Margin = new System.Windows.Forms.Padding(0);
            this.lblPerfLSL_Center.Name = "lblPerfLSL_Center";
            this.lblPerfLSL_Center.Size = new System.Drawing.Size(346, 26);
            this.lblPerfLSL_Center.TabIndex = 4;
            this.lblPerfLSL_Center.Text = "Performance Lower Spec Limit - Center";
            this.lblPerfLSL_Center.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPerformanceLSL_Center
            // 
            this.lblPerformanceLSL_Center.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPerformanceLSL_Center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPerformanceLSL_Center.Location = new System.Drawing.Point(346, 78);
            this.lblPerformanceLSL_Center.Margin = new System.Windows.Forms.Padding(0);
            this.lblPerformanceLSL_Center.Name = "lblPerformanceLSL_Center";
            this.lblPerformanceLSL_Center.Size = new System.Drawing.Size(149, 26);
            this.lblPerformanceLSL_Center.TabIndex = 9;
            this.lblPerformanceLSL_Center.Text = "-4";
            this.lblPerformanceLSL_Center.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPerformanceLSL_Center.Click += new System.EventHandler(this.lblSpecLimit_Click);
            // 
            // lblPerfUSL_Side
            // 
            this.lblPerfUSL_Side.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblPerfUSL_Side.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPerfUSL_Side.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPerfUSL_Side.Location = new System.Drawing.Point(0, 104);
            this.lblPerfUSL_Side.Margin = new System.Windows.Forms.Padding(0);
            this.lblPerfUSL_Side.Name = "lblPerfUSL_Side";
            this.lblPerfUSL_Side.Size = new System.Drawing.Size(346, 26);
            this.lblPerfUSL_Side.TabIndex = 5;
            this.lblPerfUSL_Side.Text = "Performance Upper Spec Limit - Side";
            this.lblPerfUSL_Side.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPerformanceUSL_Side
            // 
            this.lblPerformanceUSL_Side.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPerformanceUSL_Side.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPerformanceUSL_Side.Location = new System.Drawing.Point(346, 104);
            this.lblPerformanceUSL_Side.Margin = new System.Windows.Forms.Padding(0);
            this.lblPerformanceUSL_Side.Name = "lblPerformanceUSL_Side";
            this.lblPerformanceUSL_Side.Size = new System.Drawing.Size(149, 26);
            this.lblPerformanceUSL_Side.TabIndex = 8;
            this.lblPerformanceUSL_Side.Text = "9";
            this.lblPerformanceUSL_Side.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPerformanceUSL_Side.Click += new System.EventHandler(this.lblSpecLimit_Click);
            // 
            // lblPerfLSL_Side
            // 
            this.lblPerfLSL_Side.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblPerfLSL_Side.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPerfLSL_Side.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPerfLSL_Side.Location = new System.Drawing.Point(0, 130);
            this.lblPerfLSL_Side.Margin = new System.Windows.Forms.Padding(0);
            this.lblPerfLSL_Side.Name = "lblPerfLSL_Side";
            this.lblPerfLSL_Side.Size = new System.Drawing.Size(346, 29);
            this.lblPerfLSL_Side.TabIndex = 6;
            this.lblPerfLSL_Side.Text = "Performance Lower Spec Limit - Side";
            this.lblPerfLSL_Side.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPerformanceLSL_Side
            // 
            this.lblPerformanceLSL_Side.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPerformanceLSL_Side.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPerformanceLSL_Side.Location = new System.Drawing.Point(346, 130);
            this.lblPerformanceLSL_Side.Margin = new System.Windows.Forms.Padding(0);
            this.lblPerformanceLSL_Side.Name = "lblPerformanceLSL_Side";
            this.lblPerformanceLSL_Side.Size = new System.Drawing.Size(149, 29);
            this.lblPerformanceLSL_Side.TabIndex = 7;
            this.lblPerformanceLSL_Side.Text = "-9";
            this.lblPerformanceLSL_Side.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPerformanceLSL_Side.Click += new System.EventHandler(this.lblSpecLimit_Click);
            // 
            // tlpPCResult
            // 
            this.tlpPCResult.ColumnCount = 1;
            this.tlpPCResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPCResult.Controls.Add(this.lblResultTitle1, 0, 0);
            this.tlpPCResult.Controls.Add(this.dgvPCResult, 0, 1);
            this.tlpPCResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPCResult.Location = new System.Drawing.Point(3, 430);
            this.tlpPCResult.Name = "tlpPCResult";
            this.tlpPCResult.RowCount = 2;
            this.tlpPCResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpPCResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPCResult.Size = new System.Drawing.Size(530, 421);
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
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPCResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvPCResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPCResult.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvPCResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPCResult.EnableHeadersVisualStyles = false;
            this.dgvPCResult.Location = new System.Drawing.Point(0, 30);
            this.dgvPCResult.Margin = new System.Windows.Forms.Padding(0);
            this.dgvPCResult.Name = "dgvPCResult";
            this.dgvPCResult.ReadOnly = true;
            this.dgvPCResult.RowHeadersVisible = false;
            this.dgvPCResult.RowTemplate.Height = 23;
            this.dgvPCResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPCResult.Size = new System.Drawing.Size(530, 391);
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
            this.tlpAlignData.Location = new System.Drawing.Point(539, 430);
            this.tlpAlignData.Name = "tlpAlignData";
            this.tlpAlignData.RowCount = 2;
            this.tlpAlignData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpAlignData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlignData.Size = new System.Drawing.Size(530, 421);
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
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAlignData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvAlignData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAlignData.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvAlignData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAlignData.EnableHeadersVisualStyles = false;
            this.dgvAlignData.Location = new System.Drawing.Point(3, 33);
            this.dgvAlignData.Name = "dgvAlignData";
            this.dgvAlignData.ReadOnly = true;
            this.dgvAlignData.RowHeadersVisible = false;
            this.dgvAlignData.RowTemplate.Height = 23;
            this.dgvAlignData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAlignData.Size = new System.Drawing.Size(524, 385);
            this.dgvAlignData.TabIndex = 3;
            this.dgvAlignData.SelectionChanged += new System.EventHandler(this.dgvAlignData_SelectionChanged);
            // 
            // ProcessCapabilityIndexControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpProcessCapabilityIndicies);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ProcessCapabilityIndexControl";
            this.Size = new System.Drawing.Size(1072, 900);
            this.Load += new System.EventHandler(this.ProcessCapabilityIndexControl_Load);
            this.tlpProcessCapabilityIndicies.ResumeLayout(false);
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

        private System.Windows.Forms.TableLayoutPanel tlpProcessCapabilityIndicies;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tlpSpecLimits;
        private System.Windows.Forms.Label lblCapaUSL;
        private System.Windows.Forms.Label lblCapaLSL;
        private System.Windows.Forms.Label lblCapabilityUSL;
        private System.Windows.Forms.Label lblCapabilityLSL;
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
        private System.Windows.Forms.Label lblPerfUSL_Center;
        private System.Windows.Forms.Label lblPerfLSL_Center;
        private System.Windows.Forms.Label lblPerfUSL_Side;
        private System.Windows.Forms.Label lblPerfLSL_Side;
        private System.Windows.Forms.Label lblPerformanceLSL_Side;
        private System.Windows.Forms.Label lblPerformanceUSL_Side;
        private System.Windows.Forms.Label lblPerformanceLSL_Center;
        private System.Windows.Forms.Label lblPerformanceUSL_Center;
    }
}
