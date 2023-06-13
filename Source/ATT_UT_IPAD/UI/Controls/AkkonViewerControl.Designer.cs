namespace ATT_UT_IPAD.UI.Controls
{
    partial class AkkonViewerControl
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
            this.tlpAkkonResultViewer = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblAkkonResultViewer = new System.Windows.Forms.Label();
            this.pnlAkkonResultDisplay = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlAkkonData = new System.Windows.Forms.Panel();
            this.tlpAkkonData = new System.Windows.Forms.TableLayoutPanel();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblLog = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTabButton = new System.Windows.Forms.Panel();
            this.tlpAkkonResultViewer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tlpAkkonData.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAkkonResultViewer
            // 
            this.tlpAkkonResultViewer.ColumnCount = 1;
            this.tlpAkkonResultViewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAkkonResultViewer.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlpAkkonResultViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAkkonResultViewer.Location = new System.Drawing.Point(0, 0);
            this.tlpAkkonResultViewer.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAkkonResultViewer.Name = "tlpAkkonResultViewer";
            this.tlpAkkonResultViewer.RowCount = 1;
            this.tlpAkkonResultViewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAkkonResultViewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpAkkonResultViewer.Size = new System.Drawing.Size(900, 300);
            this.tlpAkkonResultViewer.TabIndex = 4;
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
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblAkkonResultViewer, 0, 0);
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
            // lblAkkonResultViewer
            // 
            this.lblAkkonResultViewer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblAkkonResultViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAkkonResultViewer.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblAkkonResultViewer.ForeColor = System.Drawing.Color.White;
            this.lblAkkonResultViewer.Location = new System.Drawing.Point(0, 0);
            this.lblAkkonResultViewer.Margin = new System.Windows.Forms.Padding(0);
            this.lblAkkonResultViewer.Name = "lblAkkonResultViewer";
            this.lblAkkonResultViewer.Size = new System.Drawing.Size(480, 40);
            this.lblAkkonResultViewer.TabIndex = 1;
            this.lblAkkonResultViewer.Text = "AKKON";
            this.lblAkkonResultViewer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlAkkonResultDisplay
            // 
            this.pnlAkkonResultDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAkkonResultDisplay.Location = new System.Drawing.Point(0, 40);
            this.pnlAkkonResultDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAkkonResultDisplay.Name = "pnlAkkonResultDisplay";
            this.pnlAkkonResultDisplay.Size = new System.Drawing.Size(480, 220);
            this.pnlAkkonResultDisplay.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.pnlAkkonData, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tlpAkkonData, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(480, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(420, 300);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // pnlAkkonData
            // 
            this.pnlAkkonData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAkkonData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAkkonData.Location = new System.Drawing.Point(0, 40);
            this.pnlAkkonData.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAkkonData.Name = "pnlAkkonData";
            this.pnlAkkonData.Size = new System.Drawing.Size(420, 260);
            this.pnlAkkonData.TabIndex = 2;
            // 
            // tlpAkkonData
            // 
            this.tlpAkkonData.ColumnCount = 2;
            this.tlpAkkonData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAkkonData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAkkonData.Controls.Add(this.lblResult, 0, 0);
            this.tlpAkkonData.Controls.Add(this.lblLog, 1, 0);
            this.tlpAkkonData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAkkonData.Location = new System.Drawing.Point(0, 0);
            this.tlpAkkonData.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAkkonData.Name = "tlpAkkonData";
            this.tlpAkkonData.RowCount = 1;
            this.tlpAkkonData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAkkonData.Size = new System.Drawing.Size(420, 40);
            this.tlpAkkonData.TabIndex = 0;
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
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.pnlTabButton, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.pnlAkkonResultDisplay, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 40);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(480, 260);
            this.tableLayoutPanel4.TabIndex = 0;
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
            // AkkonViewerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpAkkonResultViewer);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "AkkonViewerControl";
            this.Size = new System.Drawing.Size(900, 300);
            this.Load += new System.EventHandler(this.AkkonResultViewer_Load);
            this.tlpAkkonResultViewer.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tlpAkkonData.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAkkonResultViewer;
        private System.Windows.Forms.Label lblAkkonResultViewer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlAkkonResultDisplay;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tlpAkkonData;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.Panel pnlAkkonData;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Panel pnlTabButton;
    }
}
