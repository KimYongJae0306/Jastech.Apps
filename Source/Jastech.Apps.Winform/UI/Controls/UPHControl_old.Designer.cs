namespace Jastech.Framework.Winform.Controls
{
    partial class UPHControl_old
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tlpUPHControl = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chartPie = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvUPHData = new System.Windows.Forms.DataGridView();
            this.tlpFunctions = new System.Windows.Forms.TableLayoutPanel();
            this.lblDownload = new System.Windows.Forms.Label();
            this.lblTotalFail = new System.Windows.Forms.Label();
            this.lblTotalNG = new System.Windows.Forms.Label();
            this.lblTotalOK = new System.Windows.Forms.Label();
            this.lblTotalProduction = new System.Windows.Forms.Label();
            this.chartBar = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tlpUPHControl.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUPHData)).BeginInit();
            this.tlpFunctions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartBar)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpUPHControl
            // 
            this.tlpUPHControl.ColumnCount = 1;
            this.tlpUPHControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpUPHControl.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tlpUPHControl.Controls.Add(this.tlpFunctions, 0, 0);
            this.tlpUPHControl.Controls.Add(this.chartBar, 0, 2);
            this.tlpUPHControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpUPHControl.ForeColor = System.Drawing.Color.White;
            this.tlpUPHControl.Location = new System.Drawing.Point(0, 0);
            this.tlpUPHControl.Margin = new System.Windows.Forms.Padding(0);
            this.tlpUPHControl.Name = "tlpUPHControl";
            this.tlpUPHControl.RowCount = 3;
            this.tlpUPHControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpUPHControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpUPHControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpUPHControl.Size = new System.Drawing.Size(1000, 700);
            this.tlpUPHControl.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.chartPie, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvUPHData, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 80);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1000, 310);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // chartPie
            // 
            this.chartPie.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.chartPie.BorderlineColor = System.Drawing.Color.Black;
            this.chartPie.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.Name = "ChartArea1";
            this.chartPie.ChartAreas.Add(chartArea1);
            this.chartPie.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartPie.Legends.Add(legend1);
            this.chartPie.Location = new System.Drawing.Point(3, 3);
            this.chartPie.Name = "chartPie";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartPie.Series.Add(series1);
            this.chartPie.Size = new System.Drawing.Size(494, 304);
            this.chartPie.TabIndex = 0;
            this.chartPie.Text = "chart1";
            // 
            // dgvUPHData
            // 
            this.dgvUPHData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUPHData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUPHData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUPHData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvUPHData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUPHData.Location = new System.Drawing.Point(503, 3);
            this.dgvUPHData.Name = "dgvUPHData";
            this.dgvUPHData.RowTemplate.Height = 23;
            this.dgvUPHData.Size = new System.Drawing.Size(494, 304);
            this.dgvUPHData.TabIndex = 1;
            // 
            // tlpFunctions
            // 
            this.tlpFunctions.ColumnCount = 5;
            this.tlpFunctions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpFunctions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpFunctions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpFunctions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpFunctions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpFunctions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpFunctions.Controls.Add(this.lblDownload, 4, 0);
            this.tlpFunctions.Controls.Add(this.lblTotalFail, 3, 0);
            this.tlpFunctions.Controls.Add(this.lblTotalNG, 2, 0);
            this.tlpFunctions.Controls.Add(this.lblTotalOK, 1, 0);
            this.tlpFunctions.Controls.Add(this.lblTotalProduction, 0, 0);
            this.tlpFunctions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFunctions.Location = new System.Drawing.Point(3, 3);
            this.tlpFunctions.Name = "tlpFunctions";
            this.tlpFunctions.RowCount = 1;
            this.tlpFunctions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFunctions.Size = new System.Drawing.Size(994, 74);
            this.tlpFunctions.TabIndex = 1;
            // 
            // lblDownload
            // 
            this.lblDownload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblDownload.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDownload.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblDownload.Location = new System.Drawing.Point(795, 0);
            this.lblDownload.Name = "lblDownload";
            this.lblDownload.Size = new System.Drawing.Size(196, 74);
            this.lblDownload.TabIndex = 5;
            this.lblDownload.Text = "DOWNLOAD";
            this.lblDownload.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalFail
            // 
            this.lblTotalFail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblTotalFail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalFail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalFail.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotalFail.Location = new System.Drawing.Point(597, 0);
            this.lblTotalFail.Name = "lblTotalFail";
            this.lblTotalFail.Size = new System.Drawing.Size(192, 74);
            this.lblTotalFail.TabIndex = 4;
            this.lblTotalFail.Text = "TOTAL\r\nFAIL : 0";
            this.lblTotalFail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalNG
            // 
            this.lblTotalNG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblTotalNG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalNG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalNG.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotalNG.Location = new System.Drawing.Point(399, 0);
            this.lblTotalNG.Name = "lblTotalNG";
            this.lblTotalNG.Size = new System.Drawing.Size(192, 74);
            this.lblTotalNG.TabIndex = 3;
            this.lblTotalNG.Text = "TOTAL\r\nNG : 0";
            this.lblTotalNG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalOK
            // 
            this.lblTotalOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblTotalOK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalOK.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotalOK.Location = new System.Drawing.Point(201, 0);
            this.lblTotalOK.Name = "lblTotalOK";
            this.lblTotalOK.Size = new System.Drawing.Size(192, 74);
            this.lblTotalOK.TabIndex = 2;
            this.lblTotalOK.Text = "TOTAL\r\nOK : 0";
            this.lblTotalOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalProduction
            // 
            this.lblTotalProduction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalProduction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalProduction.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotalProduction.Location = new System.Drawing.Point(3, 0);
            this.lblTotalProduction.Name = "lblTotalProduction";
            this.lblTotalProduction.Size = new System.Drawing.Size(192, 74);
            this.lblTotalProduction.TabIndex = 1;
            this.lblTotalProduction.Text = "TOTAL";
            this.lblTotalProduction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chartBar
            // 
            this.chartBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.chartBar.BorderlineColor = System.Drawing.Color.Black;
            this.chartBar.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea2.Name = "ChartArea1";
            this.chartBar.ChartAreas.Add(chartArea2);
            this.chartBar.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chartBar.Legends.Add(legend2);
            this.chartBar.Location = new System.Drawing.Point(3, 393);
            this.chartBar.Name = "chartBar";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartBar.Series.Add(series2);
            this.chartBar.Size = new System.Drawing.Size(994, 304);
            this.chartBar.TabIndex = 2;
            this.chartBar.Text = "chart2";
            // 
            // UPHControl_old
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpUPHControl);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.Name = "UPHControl_old";
            this.Size = new System.Drawing.Size(1000, 700);
            this.Load += new System.EventHandler(this.UPHControl_Load);
            this.tlpUPHControl.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartPie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUPHData)).EndInit();
            this.tlpFunctions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpUPHControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tlpFunctions;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPie;
        private System.Windows.Forms.DataGridView dgvUPHData;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartBar;
        private System.Windows.Forms.Label lblTotalProduction;
        private System.Windows.Forms.Label lblDownload;
        private System.Windows.Forms.Label lblTotalFail;
        private System.Windows.Forms.Label lblTotalNG;
        private System.Windows.Forms.Label lblTotalOK;
    }
}
