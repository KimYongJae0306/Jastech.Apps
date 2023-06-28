namespace ATT.UI.Controls
{
    partial class AlignViewerControl
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
            this.tlpAlignViewer = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlResultChart = new System.Windows.Forms.Panel();
            this.lblAlignLog = new System.Windows.Forms.Label();
            this.pnlResultData = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlResultDisplay = new System.Windows.Forms.Panel();
            this.lblAlignViewer = new System.Windows.Forms.Label();
            this.tlpAlignViewer.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAlignViewer
            // 
            this.tlpAlignViewer.ColumnCount = 3;
            this.tlpAlignViewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tlpAlignViewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tlpAlignViewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpAlignViewer.Controls.Add(this.tableLayoutPanel2, 2, 0);
            this.tlpAlignViewer.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlpAlignViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAlignViewer.Location = new System.Drawing.Point(0, 0);
            this.tlpAlignViewer.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAlignViewer.Name = "tlpAlignViewer";
            this.tlpAlignViewer.RowCount = 1;
            this.tlpAlignViewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlignViewer.Size = new System.Drawing.Size(900, 300);
            this.tlpAlignViewer.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.pnlResultChart, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblAlignLog, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pnlResultData, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(676, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(224, 300);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // pnlResultChart
            // 
            this.pnlResultChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResultChart.Location = new System.Drawing.Point(0, 170);
            this.pnlResultChart.Margin = new System.Windows.Forms.Padding(0);
            this.pnlResultChart.Name = "pnlResultChart";
            this.pnlResultChart.Size = new System.Drawing.Size(224, 130);
            this.pnlResultChart.TabIndex = 5;
            // 
            // lblAlignLog
            // 
            this.lblAlignLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblAlignLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAlignLog.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblAlignLog.ForeColor = System.Drawing.Color.White;
            this.lblAlignLog.Location = new System.Drawing.Point(0, 0);
            this.lblAlignLog.Margin = new System.Windows.Forms.Padding(0);
            this.lblAlignLog.Name = "lblAlignLog";
            this.lblAlignLog.Size = new System.Drawing.Size(224, 40);
            this.lblAlignLog.TabIndex = 2;
            this.lblAlignLog.Text = "Log";
            this.lblAlignLog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlResultData
            // 
            this.pnlResultData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResultData.Location = new System.Drawing.Point(0, 40);
            this.pnlResultData.Margin = new System.Windows.Forms.Padding(0);
            this.pnlResultData.Name = "pnlResultData";
            this.pnlResultData.Size = new System.Drawing.Size(224, 130);
            this.pnlResultData.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pnlResultDisplay, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblAlignViewer, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(671, 300);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pnlResultDisplay
            // 
            this.pnlResultDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResultDisplay.Location = new System.Drawing.Point(0, 40);
            this.pnlResultDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.pnlResultDisplay.Name = "pnlResultDisplay";
            this.pnlResultDisplay.Size = new System.Drawing.Size(671, 260);
            this.pnlResultDisplay.TabIndex = 3;
            // 
            // lblAlignViewer
            // 
            this.lblAlignViewer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblAlignViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAlignViewer.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblAlignViewer.ForeColor = System.Drawing.Color.White;
            this.lblAlignViewer.Location = new System.Drawing.Point(0, 0);
            this.lblAlignViewer.Margin = new System.Windows.Forms.Padding(0);
            this.lblAlignViewer.Name = "lblAlignViewer";
            this.lblAlignViewer.Size = new System.Drawing.Size(671, 40);
            this.lblAlignViewer.TabIndex = 2;
            this.lblAlignViewer.Text = "ALIGN";
            this.lblAlignViewer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AlignViewerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpAlignViewer);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "AlignViewerControl";
            this.Size = new System.Drawing.Size(900, 300);
            this.Load += new System.EventHandler(this.AlignViewerControl_Load);
            this.tlpAlignViewer.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAlignViewer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblAlignLog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlResultDisplay;
        private System.Windows.Forms.Label lblAlignViewer;
        private System.Windows.Forms.Panel pnlResultData;
        private System.Windows.Forms.Panel pnlResultChart;
    }
}
