namespace ATT_UT_IPAD.UI.Controls
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
            this.tlpAlignResultViewer = new System.Windows.Forms.TableLayoutPanel();
            this.lblAlignResultViewer = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpAlignData = new System.Windows.Forms.TableLayoutPanel();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblLog = new System.Windows.Forms.Label();
            this.pnlAlignData = new System.Windows.Forms.Panel();
            this.pnlAlignResultDisplay = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpResultDisplay = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTabButton = new System.Windows.Forms.Panel();
            this.tlpAlignResultViewer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tlpAlignData.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tlpResultDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAlignResultViewer
            // 
            this.tlpAlignResultViewer.ColumnCount = 1;
            this.tlpAlignResultViewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlignResultViewer.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlpAlignResultViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAlignResultViewer.Location = new System.Drawing.Point(0, 0);
            this.tlpAlignResultViewer.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAlignResultViewer.Name = "tlpAlignResultViewer";
            this.tlpAlignResultViewer.RowCount = 1;
            this.tlpAlignResultViewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlignResultViewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpAlignResultViewer.Size = new System.Drawing.Size(900, 300);
            this.tlpAlignResultViewer.TabIndex = 5;
            // 
            // lblAlignResultViewer
            // 
            this.lblAlignResultViewer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblAlignResultViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAlignResultViewer.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblAlignResultViewer.ForeColor = System.Drawing.Color.White;
            this.lblAlignResultViewer.Location = new System.Drawing.Point(0, 0);
            this.lblAlignResultViewer.Margin = new System.Windows.Forms.Padding(0);
            this.lblAlignResultViewer.Name = "lblAlignResultViewer";
            this.lblAlignResultViewer.Size = new System.Drawing.Size(480, 40);
            this.lblAlignResultViewer.TabIndex = 1;
            this.lblAlignResultViewer.Text = "ALIGN";
            this.lblAlignResultViewer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 420F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(900, 300);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tlpAlignData, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pnlAlignData, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(480, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(420, 300);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tlpAlignData
            // 
            this.tlpAlignData.ColumnCount = 2;
            this.tlpAlignData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAlignData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAlignData.Controls.Add(this.lblResult, 0, 0);
            this.tlpAlignData.Controls.Add(this.lblLog, 1, 0);
            this.tlpAlignData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAlignData.Location = new System.Drawing.Point(0, 0);
            this.tlpAlignData.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAlignData.Name = "tlpAlignData";
            this.tlpAlignData.RowCount = 1;
            this.tlpAlignData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAlignData.Size = new System.Drawing.Size(420, 40);
            this.tlpAlignData.TabIndex = 0;
            // 
            // lblResult
            // 
            this.lblResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResult.Location = new System.Drawing.Point(0, 0);
            this.lblResult.Margin = new System.Windows.Forms.Padding(0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(210, 40);
            this.lblResult.TabIndex = 297;
            this.lblResult.Text = "Result";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblResult.Click += new System.EventHandler(this.lblResult_Click);
            // 
            // lblLog
            // 
            this.lblLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLog.Location = new System.Drawing.Point(210, 0);
            this.lblLog.Margin = new System.Windows.Forms.Padding(0);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(210, 40);
            this.lblLog.TabIndex = 297;
            this.lblLog.Text = "Log";
            this.lblLog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLog.Click += new System.EventHandler(this.lblLog_Click);
            // 
            // pnlAlignData
            // 
            this.pnlAlignData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAlignData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAlignData.Location = new System.Drawing.Point(0, 40);
            this.pnlAlignData.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAlignData.Name = "pnlAlignData";
            this.pnlAlignData.Size = new System.Drawing.Size(420, 260);
            this.pnlAlignData.TabIndex = 1;
            // 
            // pnlAlignResultDisplay
            // 
            this.pnlAlignResultDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAlignResultDisplay.Location = new System.Drawing.Point(0, 40);
            this.pnlAlignResultDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAlignResultDisplay.Name = "pnlAlignResultDisplay";
            this.pnlAlignResultDisplay.Size = new System.Drawing.Size(480, 220);
            this.pnlAlignResultDisplay.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tlpResultDisplay, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblAlignResultViewer, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(480, 300);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tlpResultDisplay
            // 
            this.tlpResultDisplay.ColumnCount = 1;
            this.tlpResultDisplay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpResultDisplay.Controls.Add(this.pnlTabButton, 0, 0);
            this.tlpResultDisplay.Controls.Add(this.pnlAlignResultDisplay, 0, 1);
            this.tlpResultDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpResultDisplay.Location = new System.Drawing.Point(0, 40);
            this.tlpResultDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.tlpResultDisplay.Name = "tlpResultDisplay";
            this.tlpResultDisplay.RowCount = 2;
            this.tlpResultDisplay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpResultDisplay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpResultDisplay.Size = new System.Drawing.Size(480, 260);
            this.tlpResultDisplay.TabIndex = 0;
            // 
            // pnlTabButton
            // 
            this.pnlTabButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTabButton.Location = new System.Drawing.Point(0, 0);
            this.pnlTabButton.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTabButton.Name = "pnlTabButton";
            this.pnlTabButton.Size = new System.Drawing.Size(480, 40);
            this.pnlTabButton.TabIndex = 2;
            // 
            // AlignViewerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpAlignResultViewer);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "AlignViewerControl";
            this.Size = new System.Drawing.Size(900, 300);
            this.Load += new System.EventHandler(this.AlignResultViewer_Load);
            this.tlpAlignResultViewer.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tlpAlignData.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tlpResultDisplay.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAlignResultViewer;
        private System.Windows.Forms.Label lblAlignResultViewer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel pnlAlignResultDisplay;
        private System.Windows.Forms.TableLayoutPanel tlpAlignData;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.Panel pnlAlignData;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tlpResultDisplay;
        private System.Windows.Forms.Panel pnlTabButton;
    }
}
