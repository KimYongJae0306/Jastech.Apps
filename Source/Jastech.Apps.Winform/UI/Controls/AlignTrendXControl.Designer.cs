namespace Jastech.Apps.Winform.UI.Controls
{
    partial class AlignTrendXControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTabName = new System.Windows.Forms.Label();
            this.pnlLxChart = new System.Windows.Forms.Panel();
            this.pnlCxChart = new System.Windows.Forms.Panel();
            this.pnlRxChart = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblLx = new System.Windows.Forms.Label();
            this.lblCx = new System.Windows.Forms.Label();
            this.lblRx = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.lblTabName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(877, 282);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblTabName
            // 
            this.lblTabName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblTabName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTabName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTabName.Location = new System.Drawing.Point(0, 0);
            this.lblTabName.Margin = new System.Windows.Forms.Padding(0);
            this.lblTabName.Name = "lblTabName";
            this.lblTabName.Size = new System.Drawing.Size(80, 282);
            this.lblTabName.TabIndex = 9;
            this.lblTabName.Text = "Tab";
            this.lblTabName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLxChart
            // 
            this.pnlLxChart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLxChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLxChart.Location = new System.Drawing.Point(0, 40);
            this.pnlLxChart.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLxChart.Name = "pnlLxChart";
            this.pnlLxChart.Size = new System.Drawing.Size(265, 242);
            this.pnlLxChart.TabIndex = 10;
            // 
            // pnlCxChart
            // 
            this.pnlCxChart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCxChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCxChart.Location = new System.Drawing.Point(265, 40);
            this.pnlCxChart.Margin = new System.Windows.Forms.Padding(0);
            this.pnlCxChart.Name = "pnlCxChart";
            this.pnlCxChart.Size = new System.Drawing.Size(265, 242);
            this.pnlCxChart.TabIndex = 11;
            // 
            // pnlRxChart
            // 
            this.pnlRxChart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRxChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRxChart.Location = new System.Drawing.Point(530, 40);
            this.pnlRxChart.Margin = new System.Windows.Forms.Padding(0);
            this.pnlRxChart.Name = "pnlRxChart";
            this.pnlRxChart.Size = new System.Drawing.Size(267, 242);
            this.pnlRxChart.TabIndex = 12;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.Controls.Add(this.lblRx, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblCx, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblLx, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pnlLxChart, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.pnlRxChart, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.pnlCxChart, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(80, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(797, 282);
            this.tableLayoutPanel2.TabIndex = 13;
            // 
            // lblLx
            // 
            this.lblLx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblLx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLx.Location = new System.Drawing.Point(0, 0);
            this.lblLx.Margin = new System.Windows.Forms.Padding(0);
            this.lblLx.Name = "lblLx";
            this.lblLx.Size = new System.Drawing.Size(265, 40);
            this.lblLx.TabIndex = 13;
            this.lblLx.Text = "Lx";
            this.lblLx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCx
            // 
            this.lblCx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblCx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCx.Location = new System.Drawing.Point(265, 0);
            this.lblCx.Margin = new System.Windows.Forms.Padding(0);
            this.lblCx.Name = "lblCx";
            this.lblCx.Size = new System.Drawing.Size(265, 40);
            this.lblCx.TabIndex = 13;
            this.lblCx.Text = "Cx";
            this.lblCx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRx
            // 
            this.lblRx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblRx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRx.Location = new System.Drawing.Point(530, 0);
            this.lblRx.Margin = new System.Windows.Forms.Padding(0);
            this.lblRx.Name = "lblRx";
            this.lblRx.Size = new System.Drawing.Size(267, 40);
            this.lblRx.TabIndex = 13;
            this.lblRx.Text = "Rx";
            this.lblRx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AlignTrendXControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AlignTrendXControl";
            this.Size = new System.Drawing.Size(877, 282);
            this.Load += new System.EventHandler(this.AlignTrendXControl_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblTabName;
        private System.Windows.Forms.Panel pnlRxChart;
        private System.Windows.Forms.Panel pnlCxChart;
        private System.Windows.Forms.Panel pnlLxChart;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblRx;
        private System.Windows.Forms.Label lblCx;
        private System.Windows.Forms.Label lblLx;
    }
}
