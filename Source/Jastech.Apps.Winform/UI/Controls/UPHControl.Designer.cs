using Jastech.Framework.Winform.Controls;

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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpUPH = new System.Windows.Forms.TableLayoutPanel();
            this.chtBar = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tlpData = new System.Windows.Forms.TableLayoutPanel();
            this.chtPie = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvUPHData = new Jastech.Framework.Winform.Controls.DoubleBufferedDatagridView();
            this.pnlBasicFunction = new System.Windows.Forms.Panel();
            this.lblExport = new System.Windows.Forms.Label();
            this.lblTotalFail = new System.Windows.Forms.Label();
            this.lblTotalNG = new System.Windows.Forms.Label();
            this.lblTotalOK = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.tlpJudgeResult = new System.Windows.Forms.TableLayoutPanel();
            this.lblResult = new System.Windows.Forms.Label();
            this.tlpUPH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtBar)).BeginInit();
            this.tlpData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtPie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUPHData)).BeginInit();
            this.pnlBasicFunction.SuspendLayout();
            this.tlpJudgeResult.SuspendLayout();
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
            this.chtBar.ChartAreas.Add(chartArea3);
            this.chtBar.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            legend3.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            legend3.ForeColor = System.Drawing.Color.White;
            legend3.IsTextAutoFit = false;
            legend3.Name = "Legend1";
            legend3.ShadowColor = System.Drawing.Color.Empty;
            this.chtBar.Legends.Add(legend3);
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
            this.tlpData.Controls.Add(this.tlpJudgeResult, 1, 0);
            this.tlpData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpData.Location = new System.Drawing.Point(0, 120);
            this.tlpData.Margin = new System.Windows.Forms.Padding(0);
            this.tlpData.Name = "tlpData";
            this.tlpData.RowCount = 1;
            this.tlpData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpData.Size = new System.Drawing.Size(860, 210);
            this.tlpData.TabIndex = 3;
            // 
            // chtPie
            // 
            this.chtPie.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
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
            this.chtPie.ChartAreas.Add(chartArea4);
            this.chtPie.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            legend4.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            legend4.ForeColor = System.Drawing.Color.White;
            legend4.IsTextAutoFit = false;
            legend4.Name = "Legend1";
            legend4.ShadowColor = System.Drawing.Color.Empty;
            this.chtPie.Legends.Add(legend4);
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUPHData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvUPHData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUPHData.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvUPHData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUPHData.EnableHeadersVisualStyles = false;
            this.dgvUPHData.Location = new System.Drawing.Point(3, 33);
            this.dgvUPHData.Name = "dgvUPHData";
            this.dgvUPHData.ReadOnly = true;
            this.dgvUPHData.RowTemplate.Height = 23;
            this.dgvUPHData.Size = new System.Drawing.Size(375, 168);
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
            this.lblExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblExport.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblExport.Location = new System.Drawing.Point(680, 30);
            this.lblExport.Margin = new System.Windows.Forms.Padding(0);
            this.lblExport.Name = "lblExport";
            this.lblExport.Size = new System.Drawing.Size(130, 60);
            this.lblExport.TabIndex = 6;
            this.lblExport.Text = "DATA EXPORT";
            this.lblExport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblExport.Click += new System.EventHandler(this.lblExport_Click);
            // 
            // lblTotalFail
            // 
            this.lblTotalFail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblTotalFail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalFail.ForeColor = System.Drawing.Color.DarkGray;
            this.lblTotalFail.Location = new System.Drawing.Point(520, 30);
            this.lblTotalFail.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotalFail.Name = "lblTotalFail";
            this.lblTotalFail.Size = new System.Drawing.Size(130, 60);
            this.lblTotalFail.TabIndex = 6;
            this.lblTotalFail.Text = "TOTAL FAIL";
            this.lblTotalFail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTotalFail.Click += new System.EventHandler(this.lblTotalFail_Click);
            // 
            // lblTotalNG
            // 
            this.lblTotalNG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblTotalNG.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalNG.ForeColor = System.Drawing.Color.Red;
            this.lblTotalNG.Location = new System.Drawing.Point(360, 30);
            this.lblTotalNG.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotalNG.Name = "lblTotalNG";
            this.lblTotalNG.Size = new System.Drawing.Size(130, 60);
            this.lblTotalNG.TabIndex = 6;
            this.lblTotalNG.Text = "TOTAL NG";
            this.lblTotalNG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTotalNG.Click += new System.EventHandler(this.lblTotalNG_Click);
            // 
            // lblTotalOK
            // 
            this.lblTotalOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblTotalOK.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalOK.ForeColor = System.Drawing.Color.LawnGreen;
            this.lblTotalOK.Location = new System.Drawing.Point(200, 30);
            this.lblTotalOK.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotalOK.Name = "lblTotalOK";
            this.lblTotalOK.Size = new System.Drawing.Size(130, 60);
            this.lblTotalOK.TabIndex = 6;
            this.lblTotalOK.Text = "TOTAL OK";
            this.lblTotalOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTotalOK.Click += new System.EventHandler(this.lblTotalOK_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal.ForeColor = System.Drawing.Color.Yellow;
            this.lblTotal.Location = new System.Drawing.Point(40, 30);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(130, 60);
            this.lblTotal.TabIndex = 6;
            this.lblTotal.Text = "TOTAL";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTotal.Click += new System.EventHandler(this.lblTotal_Click);
            // 
            // tlpJudgeResult
            // 
            this.tlpJudgeResult.ColumnCount = 1;
            this.tlpJudgeResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpJudgeResult.Controls.Add(this.lblResult, 0, 0);
            this.tlpJudgeResult.Controls.Add(this.dgvUPHData, 0, 1);
            this.tlpJudgeResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpJudgeResult.Location = new System.Drawing.Point(476, 3);
            this.tlpJudgeResult.Name = "tlpJudgeResult";
            this.tlpJudgeResult.RowCount = 2;
            this.tlpJudgeResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpJudgeResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpJudgeResult.Size = new System.Drawing.Size(381, 204);
            this.tlpJudgeResult.TabIndex = 3;
            // 
            // lblResult
            // 
            this.lblResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.lblResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResult.Location = new System.Drawing.Point(0, 0);
            this.lblResult.Margin = new System.Windows.Forms.Padding(0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(381, 30);
            this.lblResult.TabIndex = 9;
            this.lblResult.Text = "Judge Result";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.tlpJudgeResult.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpUPH;
        private System.Windows.Forms.TableLayoutPanel tlpData;
        private DoubleBufferedDatagridView dgvUPHData;
        private System.Windows.Forms.Panel pnlBasicFunction;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblExport;
        private System.Windows.Forms.Label lblTotalFail;
        private System.Windows.Forms.Label lblTotalNG;
        private System.Windows.Forms.Label lblTotalOK;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtBar;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtPie;
        private System.Windows.Forms.TableLayoutPanel tlpJudgeResult;
        private System.Windows.Forms.Label lblResult;
    }
}
