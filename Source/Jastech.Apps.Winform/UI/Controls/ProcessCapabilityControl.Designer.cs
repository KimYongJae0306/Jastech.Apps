namespace Jastech.Apps.Winform.UI.Controls
{
    partial class ProcessCapabilityControl
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpProcessCapability = new System.Windows.Forms.TableLayoutPanel();
            this.tlpData = new System.Windows.Forms.TableLayoutPanel();
            this.cht1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cht2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblDayCounts = new System.Windows.Forms.Label();
            this.lblDayCount = new System.Windows.Forms.Label();
            this.lblDataCounts = new System.Windows.Forms.Label();
            this.lblDataCount = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblAlign = new System.Windows.Forms.Label();
            this.tlpAlignTypes = new System.Windows.Forms.TableLayoutPanel();
            this.lblLx = new System.Windows.Forms.Label();
            this.lblLy = new System.Windows.Forms.Label();
            this.lblCx = new System.Windows.Forms.Label();
            this.lblRx = new System.Windows.Forms.Label();
            this.lblRy = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblUSL = new System.Windows.Forms.Label();
            this.lblLSL = new System.Windows.Forms.Label();
            this.lblUpperSpecLimit = new System.Windows.Forms.Label();
            this.lblLowerSpecLimit = new System.Windows.Forms.Label();
            this.tlpPCResult = new System.Windows.Forms.TableLayoutPanel();
            this.lblResult = new System.Windows.Forms.Label();
            this.dgvPCResult = new System.Windows.Forms.DataGridView();
            this.lblTab = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpTab = new System.Windows.Forms.TableLayoutPanel();
            this.tlpProcessCapability.SuspendLayout();
            this.tlpData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cht1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cht2)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tlpAlignTypes.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlpPCResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPCResult)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpProcessCapability
            // 
            this.tlpProcessCapability.ColumnCount = 2;
            this.tlpProcessCapability.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpProcessCapability.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpProcessCapability.Controls.Add(this.tlpData, 1, 0);
            this.tlpProcessCapability.Controls.Add(this.panel1, 0, 0);
            this.tlpProcessCapability.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpProcessCapability.Location = new System.Drawing.Point(0, 0);
            this.tlpProcessCapability.Margin = new System.Windows.Forms.Padding(0);
            this.tlpProcessCapability.Name = "tlpProcessCapability";
            this.tlpProcessCapability.RowCount = 1;
            this.tlpProcessCapability.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpProcessCapability.Size = new System.Drawing.Size(1072, 554);
            this.tlpProcessCapability.TabIndex = 0;
            // 
            // tlpData
            // 
            this.tlpData.ColumnCount = 1;
            this.tlpData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpData.Controls.Add(this.cht1, 0, 0);
            this.tlpData.Controls.Add(this.cht2, 0, 1);
            this.tlpData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpData.Location = new System.Drawing.Point(536, 0);
            this.tlpData.Margin = new System.Windows.Forms.Padding(0);
            this.tlpData.Name = "tlpData";
            this.tlpData.RowCount = 2;
            this.tlpData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpData.Size = new System.Drawing.Size(536, 554);
            this.tlpData.TabIndex = 0;
            // 
            // cht1
            // 
            this.cht1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            chartArea3.AxisX.LabelStyle.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea3.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea3.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea3.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea3.AxisX.MajorTickMark.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea3.AxisX.MinorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea3.AxisX.MinorTickMark.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea3.AxisX.TitleFont = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea3.AxisX.TitleForeColor = System.Drawing.Color.White;
            chartArea3.AxisY.LabelStyle.Font = new System.Drawing.Font("맑은 고딕", 9.75F);
            chartArea3.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea3.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea3.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea3.AxisY.MajorTickMark.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea3.AxisY.MinorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea3.AxisY.MinorTickMark.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            chartArea3.BackImageTransparentColor = System.Drawing.Color.White;
            chartArea3.BorderColor = System.Drawing.Color.White;
            chartArea3.Name = "ChartArea1";
            this.cht1.ChartAreas.Add(chartArea3);
            this.cht1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            legend3.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            legend3.ForeColor = System.Drawing.Color.White;
            legend3.IsTextAutoFit = false;
            legend3.Name = "Legend1";
            legend3.ShadowColor = System.Drawing.Color.Empty;
            this.cht1.Legends.Add(legend3);
            this.cht1.Location = new System.Drawing.Point(3, 3);
            this.cht1.Name = "cht1";
            this.cht1.Size = new System.Drawing.Size(530, 271);
            this.cht1.TabIndex = 3;
            this.cht1.Text = "chart1";
            // 
            // cht2
            // 
            this.cht2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            chartArea4.AxisX.LabelStyle.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea4.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea4.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea4.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea4.AxisX.MajorTickMark.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea4.AxisX.MinorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea4.AxisX.MinorTickMark.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea4.AxisX.TitleFont = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea4.AxisX.TitleForeColor = System.Drawing.Color.White;
            chartArea4.AxisY.LabelStyle.Font = new System.Drawing.Font("맑은 고딕", 9.75F);
            chartArea4.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea4.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea4.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea4.AxisY.MajorTickMark.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea4.AxisY.MinorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea4.AxisY.MinorTickMark.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            chartArea4.BackImageTransparentColor = System.Drawing.Color.White;
            chartArea4.BorderColor = System.Drawing.Color.White;
            chartArea4.Name = "ChartArea1";
            this.cht2.ChartAreas.Add(chartArea4);
            this.cht2.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            legend4.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            legend4.ForeColor = System.Drawing.Color.White;
            legend4.IsTextAutoFit = false;
            legend4.Name = "Legend1";
            legend4.ShadowColor = System.Drawing.Color.Empty;
            this.cht2.Legends.Add(legend4);
            this.cht2.Location = new System.Drawing.Point(3, 280);
            this.cht2.Name = "cht2";
            this.cht2.Size = new System.Drawing.Size(530, 271);
            this.cht2.TabIndex = 4;
            this.cht2.Text = "chart1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel4);
            this.panel1.Controls.Add(this.tableLayoutPanel3);
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.tlpPCResult);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(536, 554);
            this.panel1.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.lblDayCounts, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblDayCount, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblDataCounts, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblDataCount, 1, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(18, 12);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(240, 80);
            this.tableLayoutPanel3.TabIndex = 7;
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
            this.lblDayCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.lblDataCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.lblAlign, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tlpAlignTypes, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(18, 194);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(500, 80);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // lblAlign
            // 
            this.lblAlign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblAlign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAlign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAlign.Location = new System.Drawing.Point(0, 0);
            this.lblAlign.Margin = new System.Windows.Forms.Padding(0);
            this.lblAlign.Name = "lblAlign";
            this.lblAlign.Size = new System.Drawing.Size(500, 40);
            this.lblAlign.TabIndex = 7;
            this.lblAlign.Text = "Parameter";
            this.lblAlign.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpAlignTypes
            // 
            this.tlpAlignTypes.ColumnCount = 5;
            this.tlpAlignTypes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpAlignTypes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpAlignTypes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpAlignTypes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpAlignTypes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpAlignTypes.Controls.Add(this.lblLx, 0, 0);
            this.tlpAlignTypes.Controls.Add(this.lblLy, 1, 0);
            this.tlpAlignTypes.Controls.Add(this.lblCx, 2, 0);
            this.tlpAlignTypes.Controls.Add(this.lblRx, 3, 0);
            this.tlpAlignTypes.Controls.Add(this.lblRy, 4, 0);
            this.tlpAlignTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAlignTypes.Location = new System.Drawing.Point(0, 40);
            this.tlpAlignTypes.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAlignTypes.Name = "tlpAlignTypes";
            this.tlpAlignTypes.RowCount = 1;
            this.tlpAlignTypes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlignTypes.Size = new System.Drawing.Size(500, 40);
            this.tlpAlignTypes.TabIndex = 8;
            // 
            // lblLx
            // 
            this.lblLx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLx.Location = new System.Drawing.Point(0, 0);
            this.lblLx.Margin = new System.Windows.Forms.Padding(0);
            this.lblLx.Name = "lblLx";
            this.lblLx.Size = new System.Drawing.Size(100, 40);
            this.lblLx.TabIndex = 2;
            this.lblLx.Text = "Lx";
            this.lblLx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLx.Click += new System.EventHandler(this.lblLx_Click);
            // 
            // lblLy
            // 
            this.lblLy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLy.Location = new System.Drawing.Point(100, 0);
            this.lblLy.Margin = new System.Windows.Forms.Padding(0);
            this.lblLy.Name = "lblLy";
            this.lblLy.Size = new System.Drawing.Size(100, 40);
            this.lblLy.TabIndex = 3;
            this.lblLy.Text = "Ly";
            this.lblLy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLy.Click += new System.EventHandler(this.lblLy_Click);
            // 
            // lblCx
            // 
            this.lblCx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCx.Location = new System.Drawing.Point(200, 0);
            this.lblCx.Margin = new System.Windows.Forms.Padding(0);
            this.lblCx.Name = "lblCx";
            this.lblCx.Size = new System.Drawing.Size(100, 40);
            this.lblCx.TabIndex = 4;
            this.lblCx.Text = "Cx";
            this.lblCx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCx.Click += new System.EventHandler(this.lblCx_Click);
            // 
            // lblRx
            // 
            this.lblRx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRx.Location = new System.Drawing.Point(300, 0);
            this.lblRx.Margin = new System.Windows.Forms.Padding(0);
            this.lblRx.Name = "lblRx";
            this.lblRx.Size = new System.Drawing.Size(100, 40);
            this.lblRx.TabIndex = 5;
            this.lblRx.Text = "Rx";
            this.lblRx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRx.Click += new System.EventHandler(this.lblRx_Click);
            // 
            // lblRy
            // 
            this.lblRy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRy.Location = new System.Drawing.Point(400, 0);
            this.lblRy.Margin = new System.Windows.Forms.Padding(0);
            this.lblRy.Name = "lblRy";
            this.lblRy.Size = new System.Drawing.Size(100, 40);
            this.lblRy.TabIndex = 6;
            this.lblRy.Text = "Ry";
            this.lblRy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRy.Click += new System.EventHandler(this.lblRy_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lblUSL, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblLSL, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblUpperSpecLimit, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblLowerSpecLimit, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(278, 12);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(240, 80);
            this.tableLayoutPanel1.TabIndex = 5;
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
            this.lblUSL.Text = "USL";
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
            this.lblLSL.Text = "LSL";
            this.lblLSL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUpperSpecLimit
            // 
            this.lblUpperSpecLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.lblLowerSpecLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.tlpPCResult.Location = new System.Drawing.Point(18, 295);
            this.tlpPCResult.Margin = new System.Windows.Forms.Padding(0);
            this.tlpPCResult.Name = "tlpPCResult";
            this.tlpPCResult.RowCount = 2;
            this.tlpPCResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpPCResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPCResult.Size = new System.Drawing.Size(500, 240);
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
            this.lblResult.Size = new System.Drawing.Size(500, 40);
            this.lblResult.TabIndex = 7;
            this.lblResult.Text = "CPK / PPK Result";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvPCResult
            // 
            this.dgvPCResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPCResult.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPCResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPCResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPCResult.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPCResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPCResult.Location = new System.Drawing.Point(0, 40);
            this.dgvPCResult.Margin = new System.Windows.Forms.Padding(0);
            this.dgvPCResult.Name = "dgvPCResult";
            this.dgvPCResult.ReadOnly = true;
            this.dgvPCResult.RowHeadersVisible = false;
            this.dgvPCResult.RowTemplate.Height = 23;
            this.dgvPCResult.Size = new System.Drawing.Size(500, 200);
            this.dgvPCResult.TabIndex = 6;
            // 
            // lblTab
            // 
            this.lblTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTab.Location = new System.Drawing.Point(0, 0);
            this.lblTab.Margin = new System.Windows.Forms.Padding(0);
            this.lblTab.Name = "lblTab";
            this.lblTab.Size = new System.Drawing.Size(99, 40);
            this.lblTab.TabIndex = 6;
            this.lblTab.Text = "Tab";
            this.lblTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.lblTab, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tlpTab, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(19, 134);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(499, 40);
            this.tableLayoutPanel4.TabIndex = 9;
            // 
            // tlpTab
            // 
            this.tlpTab.ColumnCount = 4;
            this.tlpTab.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTab.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTab.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTab.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTab.Location = new System.Drawing.Point(99, 0);
            this.tlpTab.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTab.Name = "tlpTab";
            this.tlpTab.RowCount = 1;
            this.tlpTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTab.Size = new System.Drawing.Size(400, 40);
            this.tlpTab.TabIndex = 7;
            // 
            // ProcessCapabilityControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpProcessCapability);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ProcessCapabilityControl";
            this.Size = new System.Drawing.Size(1072, 554);
            this.Load += new System.EventHandler(this.ProcessCapabilityControl_Load);
            this.tlpProcessCapability.ResumeLayout(false);
            this.tlpData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cht1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cht2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tlpAlignTypes.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tlpPCResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPCResult)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpProcessCapability;
        private System.Windows.Forms.TableLayoutPanel tlpData;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart cht1;
        private System.Windows.Forms.DataVisualization.Charting.Chart cht2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblUSL;
        private System.Windows.Forms.Label lblLSL;
        private System.Windows.Forms.Label lblUpperSpecLimit;
        private System.Windows.Forms.Label lblLowerSpecLimit;
        private System.Windows.Forms.TableLayoutPanel tlpPCResult;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.DataGridView dgvPCResult;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblDayCounts;
        private System.Windows.Forms.Label lblDayCount;
        private System.Windows.Forms.Label lblDataCounts;
        private System.Windows.Forms.Label lblDataCount;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblAlign;
        private System.Windows.Forms.TableLayoutPanel tlpAlignTypes;
        private System.Windows.Forms.Label lblLx;
        private System.Windows.Forms.Label lblLy;
        private System.Windows.Forms.Label lblCx;
        private System.Windows.Forms.Label lblRx;
        private System.Windows.Forms.Label lblRy;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblTab;
        private System.Windows.Forms.TableLayoutPanel tlpTab;
    }
}
