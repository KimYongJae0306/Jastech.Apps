namespace ATT_UT_IPAD.UI.Controls
{
    partial class DailyInfoViewerControl
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
            this.tlpDailyInfoViewer = new System.Windows.Forms.TableLayoutPanel();
            this.lblAlignLog = new System.Windows.Forms.Label();
            this.tlpDailyInfoType = new System.Windows.Forms.TableLayoutPanel();
            this.lblAlign = new System.Windows.Forms.Label();
            this.lblAkkon = new System.Windows.Forms.Label();
            this.pnlDailyResult = new System.Windows.Forms.Panel();
            this.pnlDailyChart = new System.Windows.Forms.Panel();
            this.tlpDailyInfoViewer.SuspendLayout();
            this.tlpDailyInfoType.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpDailyInfoViewer
            // 
            this.tlpDailyInfoViewer.ColumnCount = 1;
            this.tlpDailyInfoViewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDailyInfoViewer.Controls.Add(this.lblAlignLog, 0, 0);
            this.tlpDailyInfoViewer.Controls.Add(this.tlpDailyInfoType, 0, 1);
            this.tlpDailyInfoViewer.Controls.Add(this.pnlDailyResult, 0, 2);
            this.tlpDailyInfoViewer.Controls.Add(this.pnlDailyChart, 0, 3);
            this.tlpDailyInfoViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDailyInfoViewer.Location = new System.Drawing.Point(0, 0);
            this.tlpDailyInfoViewer.Margin = new System.Windows.Forms.Padding(0);
            this.tlpDailyInfoViewer.Name = "tlpDailyInfoViewer";
            this.tlpDailyInfoViewer.RowCount = 4;
            this.tlpDailyInfoViewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpDailyInfoViewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpDailyInfoViewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlpDailyInfoViewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpDailyInfoViewer.Size = new System.Drawing.Size(339, 255);
            this.tlpDailyInfoViewer.TabIndex = 0;
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
            this.lblAlignLog.Size = new System.Drawing.Size(339, 40);
            this.lblAlignLog.TabIndex = 3;
            this.lblAlignLog.Text = "DAILY INFO";
            this.lblAlignLog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpDailyInfoType
            // 
            this.tlpDailyInfoType.ColumnCount = 2;
            this.tlpDailyInfoType.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDailyInfoType.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDailyInfoType.Controls.Add(this.lblAlign, 1, 0);
            this.tlpDailyInfoType.Controls.Add(this.lblAkkon, 0, 0);
            this.tlpDailyInfoType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDailyInfoType.Location = new System.Drawing.Point(0, 40);
            this.tlpDailyInfoType.Margin = new System.Windows.Forms.Padding(0);
            this.tlpDailyInfoType.Name = "tlpDailyInfoType";
            this.tlpDailyInfoType.RowCount = 1;
            this.tlpDailyInfoType.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDailyInfoType.Size = new System.Drawing.Size(339, 40);
            this.tlpDailyInfoType.TabIndex = 4;
            // 
            // lblAlign
            // 
            this.lblAlign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblAlign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAlign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAlign.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblAlign.ForeColor = System.Drawing.Color.White;
            this.lblAlign.Location = new System.Drawing.Point(169, 0);
            this.lblAlign.Margin = new System.Windows.Forms.Padding(0);
            this.lblAlign.Name = "lblAlign";
            this.lblAlign.Size = new System.Drawing.Size(170, 40);
            this.lblAlign.TabIndex = 4;
            this.lblAlign.Text = "ALIGN [um]";
            this.lblAlign.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAlign.Click += new System.EventHandler(this.lblAlign_Click);
            // 
            // lblAkkon
            // 
            this.lblAkkon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblAkkon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAkkon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAkkon.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblAkkon.ForeColor = System.Drawing.Color.White;
            this.lblAkkon.Location = new System.Drawing.Point(0, 0);
            this.lblAkkon.Margin = new System.Windows.Forms.Padding(0);
            this.lblAkkon.Name = "lblAkkon";
            this.lblAkkon.Size = new System.Drawing.Size(169, 40);
            this.lblAkkon.TabIndex = 5;
            this.lblAkkon.Text = "AKKON [um]";
            this.lblAkkon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAkkon.Click += new System.EventHandler(this.lblAkkon_Click);
            // 
            // pnlDailyResult
            // 
            this.pnlDailyResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDailyResult.Location = new System.Drawing.Point(0, 80);
            this.pnlDailyResult.Margin = new System.Windows.Forms.Padding(0);
            this.pnlDailyResult.Name = "pnlDailyResult";
            this.pnlDailyResult.Size = new System.Drawing.Size(339, 122);
            this.pnlDailyResult.TabIndex = 5;
            // 
            // pnlDailyChart
            // 
            this.pnlDailyChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDailyChart.Location = new System.Drawing.Point(0, 202);
            this.pnlDailyChart.Margin = new System.Windows.Forms.Padding(0);
            this.pnlDailyChart.Name = "pnlDailyChart";
            this.pnlDailyChart.Size = new System.Drawing.Size(339, 53);
            this.pnlDailyChart.TabIndex = 5;
            // 
            // DailyInfoViewerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpDailyInfoViewer);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "DailyInfoViewerControl";
            this.Size = new System.Drawing.Size(339, 255);
            this.Load += new System.EventHandler(this.DailyInfoViewerControl_Load);
            this.tlpDailyInfoViewer.ResumeLayout(false);
            this.tlpDailyInfoType.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpDailyInfoViewer;
        private System.Windows.Forms.Label lblAlignLog;
        private System.Windows.Forms.TableLayoutPanel tlpDailyInfoType;
        private System.Windows.Forms.Label lblAkkon;
        private System.Windows.Forms.Label lblAlign;
        private System.Windows.Forms.Panel pnlDailyResult;
        private System.Windows.Forms.Panel pnlDailyChart;
    }
}
