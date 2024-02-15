namespace Jastech.Apps.Winform.UI.Controls
{
    partial class ResultChartControl
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
            this.chtData = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.mtipAlignResult = new MetroFramework.Components.MetroToolTip();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblDataCountValue = new System.Windows.Forms.Label();
            this.lblDataCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chtData)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chtData
            // 
            this.chtData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
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
            this.chtData.ChartAreas.Add(chartArea1);
            this.chtData.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            legend1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            legend1.ForeColor = System.Drawing.Color.White;
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            legend1.ShadowColor = System.Drawing.Color.Empty;
            this.chtData.Legends.Add(legend1);
            this.chtData.Location = new System.Drawing.Point(3, 3);
            this.chtData.Name = "chtData";
            this.chtData.Size = new System.Drawing.Size(489, 362);
            this.chtData.TabIndex = 1;
            this.chtData.Text = "chart1";
            this.chtData.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chtData_MouseClick);
            this.chtData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chtData_MouseDown);
            // 
            // mtipAlignResult
            // 
            this.mtipAlignResult.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtipAlignResult.StyleManager = null;
            this.mtipAlignResult.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.chtData, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(495, 433);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.lblDataCountValue, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblDataCount, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 371);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(489, 59);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // lblDataCountValue
            // 
            this.lblDataCountValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblDataCountValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDataCountValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDataCountValue.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblDataCountValue.ForeColor = System.Drawing.Color.White;
            this.lblDataCountValue.Location = new System.Drawing.Point(244, 0);
            this.lblDataCountValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblDataCountValue.Name = "lblDataCountValue";
            this.lblDataCountValue.Size = new System.Drawing.Size(245, 59);
            this.lblDataCountValue.TabIndex = 3;
            this.lblDataCountValue.Text = "100";
            this.lblDataCountValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDataCountValue.Click += new System.EventHandler(this.lblDataCountValue_Click);
            // 
            // lblDataCount
            // 
            this.lblDataCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblDataCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDataCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDataCount.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblDataCount.ForeColor = System.Drawing.Color.White;
            this.lblDataCount.Location = new System.Drawing.Point(0, 0);
            this.lblDataCount.Margin = new System.Windows.Forms.Padding(0);
            this.lblDataCount.Name = "lblDataCount";
            this.lblDataCount.Size = new System.Drawing.Size(244, 59);
            this.lblDataCount.TabIndex = 2;
            this.lblDataCount.Text = "Data Count";
            this.lblDataCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ResultChartControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ResultChartControl";
            this.Size = new System.Drawing.Size(495, 433);
            this.Load += new System.EventHandler(this.ResultChartControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chtData)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chtData;
        private MetroFramework.Components.MetroToolTip mtipAlignResult;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblDataCount;
        private System.Windows.Forms.Label lblDataCountValue;
    }
}
