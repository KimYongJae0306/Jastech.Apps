namespace Jastech.Apps.Winform.UI.Controls
{
    partial class UPHControl
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpUPH = new System.Windows.Forms.TableLayoutPanel();
            this.chtBar = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tlpData = new System.Windows.Forms.TableLayoutPanel();
            this.chtPie = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvUPHData = new System.Windows.Forms.DataGridView();
            this.pnlBasicFunction = new System.Windows.Forms.Panel();
            this.lblExport = new System.Windows.Forms.Label();
            this.lblTotalFail = new System.Windows.Forms.Label();
            this.lblTotalNG = new System.Windows.Forms.Label();
            this.lblTotalOK = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.tlpUPH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtBar)).BeginInit();
            this.tlpData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtPie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUPHData)).BeginInit();
            this.pnlBasicFunction.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpUPH
            // 
            this.tlpUPH.ColumnCount = 1;
            this.tlpUPH.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpUPH.Controls.Add(this.chtBar, 0, 2);
            this.tlpUPH.Controls.Add(this.tlpData, 0, 1);
            this.tlpUPH.Controls.Add(this.pnlBasicFunction, 0, 0);
            this.tlpUPH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpUPH.Location = new System.Drawing.Point(0, 0);
            this.tlpUPH.Margin = new System.Windows.Forms.Padding(0);
            this.tlpUPH.Name = "tlpUPH";
            this.tlpUPH.RowCount = 3;
            this.tlpUPH.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpUPH.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpUPH.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpUPH.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpUPH.Size = new System.Drawing.Size(860, 540);
            this.tlpUPH.TabIndex = 1;
            // 
            // chtBar
            // 
            this.chtBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea1.AxisX.MinorTickMark.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.White;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("맑은 고딕", 9.75F);
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea1.AxisY.MinorTickMark.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            chartArea1.BackImageTransparentColor = System.Drawing.Color.White;
            chartArea1.BorderColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea1";
            this.chtBar.ChartAreas.Add(chartArea1);
            this.chtBar.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            legend1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            legend1.ForeColor = System.Drawing.Color.White;
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            legend1.ShadowColor = System.Drawing.Color.Empty;
            this.chtBar.Legends.Add(legend1);
            this.chtBar.Location = new System.Drawing.Point(3, 333);
            this.chtBar.Name = "chtBar";
            this.chtBar.Size = new System.Drawing.Size(854, 204);
            this.chtBar.TabIndex = 5;
            this.chtBar.Text = "chart1";
            // 
            // tlpData
            // 
            this.tlpData.ColumnCount = 2;
            this.tlpData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tlpData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tlpData.Controls.Add(this.chtPie, 0, 0);
            this.tlpData.Controls.Add(this.dgvUPHData, 1, 0);
            this.tlpData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpData.Location = new System.Drawing.Point(0, 120);
            this.tlpData.Margin = new System.Windows.Forms.Padding(0);
            this.tlpData.Name = "tlpData";
            this.tlpData.RowCount = 1;
            this.tlpData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpData.Size = new System.Drawing.Size(860, 210);
            this.tlpData.TabIndex = 3;
            // 
            // chtPie
            // 
            this.chtPie.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            chartArea2.AxisX.LabelStyle.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea2.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea2.AxisX.MajorTickMark.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea2.AxisX.MinorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea2.AxisX.MinorTickMark.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea2.AxisX.TitleFont = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisX.TitleForeColor = System.Drawing.Color.White;
            chartArea2.AxisY.LabelStyle.Font = new System.Drawing.Font("맑은 고딕", 9.75F);
            chartArea2.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea2.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea2.AxisY.MajorTickMark.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea2.AxisY.MinorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea2.AxisY.MinorTickMark.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            chartArea2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            chartArea2.BackImageTransparentColor = System.Drawing.Color.White;
            chartArea2.BorderColor = System.Drawing.Color.White;
            chartArea2.Name = "ChartArea1";
            this.chtPie.ChartAreas.Add(chartArea2);
            this.chtPie.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            legend2.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            legend2.ForeColor = System.Drawing.Color.White;
            legend2.IsTextAutoFit = false;
            legend2.Name = "Legend1";
            legend2.ShadowColor = System.Drawing.Color.Empty;
            this.chtPie.Legends.Add(legend2);
            this.chtPie.Location = new System.Drawing.Point(3, 3);
            this.chtPie.Name = "chtPie";
            this.chtPie.Size = new System.Drawing.Size(467, 204);
            this.chtPie.TabIndex = 2;
            this.chtPie.Text = "chart1";
            // 
            // dgvUPHData
            // 
            this.dgvUPHData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvUPHData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUPHData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUPHData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUPHData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvUPHData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUPHData.Location = new System.Drawing.Point(476, 3);
            this.dgvUPHData.Name = "dgvUPHData";
            this.dgvUPHData.ReadOnly = true;
            this.dgvUPHData.RowTemplate.Height = 23;
            this.dgvUPHData.Size = new System.Drawing.Size(381, 204);
            this.dgvUPHData.TabIndex = 0;
            // 
            // pnlBasicFunction
            // 
            this.pnlBasicFunction.Controls.Add(this.lblExport);
            this.pnlBasicFunction.Controls.Add(this.lblTotalFail);
            this.pnlBasicFunction.Controls.Add(this.lblTotalNG);
            this.pnlBasicFunction.Controls.Add(this.lblTotalOK);
            this.pnlBasicFunction.Controls.Add(this.lblTotal);
            this.pnlBasicFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBasicFunction.Location = new System.Drawing.Point(0, 0);
            this.pnlBasicFunction.Margin = new System.Windows.Forms.Padding(0);
            this.pnlBasicFunction.Name = "pnlBasicFunction";
            this.pnlBasicFunction.Size = new System.Drawing.Size(860, 120);
            this.pnlBasicFunction.TabIndex = 4;
            // 
            // lblExport
            // 
            this.lblExport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblExport.Location = new System.Drawing.Point(680, 30);
            this.lblExport.Margin = new System.Windows.Forms.Padding(0);
            this.lblExport.Name = "lblExport";
            this.lblExport.Size = new System.Drawing.Size(120, 60);
            this.lblExport.TabIndex = 6;
            this.lblExport.Text = "Export";
            this.lblExport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblExport.Click += new System.EventHandler(this.lblExport_Click);
            // 
            // lblTotalFail
            // 
            this.lblTotalFail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalFail.Location = new System.Drawing.Point(520, 30);
            this.lblTotalFail.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotalFail.Name = "lblTotalFail";
            this.lblTotalFail.Size = new System.Drawing.Size(120, 60);
            this.lblTotalFail.TabIndex = 6;
            this.lblTotalFail.Text = "Total Fail";
            this.lblTotalFail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTotalFail.Click += new System.EventHandler(this.lblTotalFail_Click);
            // 
            // lblTotalNG
            // 
            this.lblTotalNG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalNG.Location = new System.Drawing.Point(360, 30);
            this.lblTotalNG.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotalNG.Name = "lblTotalNG";
            this.lblTotalNG.Size = new System.Drawing.Size(120, 60);
            this.lblTotalNG.TabIndex = 6;
            this.lblTotalNG.Text = "Total NG";
            this.lblTotalNG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTotalNG.Click += new System.EventHandler(this.lblTotalNG_Click);
            // 
            // lblTotalOK
            // 
            this.lblTotalOK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalOK.Location = new System.Drawing.Point(200, 30);
            this.lblTotalOK.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotalOK.Name = "lblTotalOK";
            this.lblTotalOK.Size = new System.Drawing.Size(120, 60);
            this.lblTotalOK.TabIndex = 6;
            this.lblTotalOK.Text = "Total OK";
            this.lblTotalOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTotalOK.Click += new System.EventHandler(this.lblTotalOK_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotal.Location = new System.Drawing.Point(40, 30);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(120, 60);
            this.lblTotal.TabIndex = 6;
            this.lblTotal.Text = "Total";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTotal.Click += new System.EventHandler(this.lblTotal_Click);
            // 
            // UPHControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpUPH);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "UPHControl";
            this.Size = new System.Drawing.Size(860, 540);
            this.Load += new System.EventHandler(this.UPHControl_Load);
            this.tlpUPH.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chtBar)).EndInit();
            this.tlpData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chtPie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUPHData)).EndInit();
            this.pnlBasicFunction.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpUPH;
        private System.Windows.Forms.TableLayoutPanel tlpData;
        private System.Windows.Forms.DataGridView dgvUPHData;
        private System.Windows.Forms.Panel pnlBasicFunction;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblExport;
        private System.Windows.Forms.Label lblTotalFail;
        private System.Windows.Forms.Label lblTotalNG;
        private System.Windows.Forms.Label lblTotalOK;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtBar;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtPie;
    }
}
