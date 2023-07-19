namespace Jastech.Apps.Winform.UI.Controls
{
    partial class SystemLogControl
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
            this.tlpSystemLog = new System.Windows.Forms.TableLayoutPanel();
            this.lblAkkonViewer = new System.Windows.Forms.Label();
            this.lstLogMessage = new System.Windows.Forms.ListBox();
            this.tlpSystemLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpSystemLog
            // 
            this.tlpSystemLog.ColumnCount = 1;
            this.tlpSystemLog.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSystemLog.Controls.Add(this.lblAkkonViewer, 0, 0);
            this.tlpSystemLog.Controls.Add(this.lstLogMessage, 0, 1);
            this.tlpSystemLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSystemLog.Location = new System.Drawing.Point(0, 0);
            this.tlpSystemLog.Margin = new System.Windows.Forms.Padding(0);
            this.tlpSystemLog.Name = "tlpSystemLog";
            this.tlpSystemLog.RowCount = 2;
            this.tlpSystemLog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpSystemLog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSystemLog.Size = new System.Drawing.Size(300, 300);
            this.tlpSystemLog.TabIndex = 0;
            // 
            // lblAkkonViewer
            // 
            this.lblAkkonViewer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblAkkonViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAkkonViewer.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblAkkonViewer.ForeColor = System.Drawing.Color.White;
            this.lblAkkonViewer.Location = new System.Drawing.Point(0, 0);
            this.lblAkkonViewer.Margin = new System.Windows.Forms.Padding(0);
            this.lblAkkonViewer.Name = "lblAkkonViewer";
            this.lblAkkonViewer.Size = new System.Drawing.Size(300, 40);
            this.lblAkkonViewer.TabIndex = 3;
            this.lblAkkonViewer.Text = "System Log";
            this.lblAkkonViewer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstLogMessage
            // 
            this.lstLogMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lstLogMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLogMessage.ForeColor = System.Drawing.Color.White;
            this.lstLogMessage.FormattingEnabled = true;
            this.lstLogMessage.ItemHeight = 20;
            this.lstLogMessage.Location = new System.Drawing.Point(0, 40);
            this.lstLogMessage.Margin = new System.Windows.Forms.Padding(0);
            this.lstLogMessage.Name = "lstLogMessage";
            this.lstLogMessage.Size = new System.Drawing.Size(300, 260);
            this.lstLogMessage.TabIndex = 4;
            this.lstLogMessage.DoubleClick += new System.EventHandler(this.lstLogMessage_DoubleClick);
            // 
            // SystemLogControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpSystemLog);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "SystemLogControl";
            this.Size = new System.Drawing.Size(300, 300);
            this.tlpSystemLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpSystemLog;
        private System.Windows.Forms.Label lblAkkonViewer;
        private System.Windows.Forms.ListBox lstLogMessage;
    }
}
