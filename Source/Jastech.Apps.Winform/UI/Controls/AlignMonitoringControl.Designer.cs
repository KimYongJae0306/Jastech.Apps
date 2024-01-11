namespace Jastech.Apps.Winform.UI.Controls
{
    partial class AlignMonitoringControl
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
            this.tlpSplitView = new System.Windows.Forms.TableLayoutPanel();
            this.tlpAlignViewer = new System.Windows.Forms.TableLayoutPanel();
            this.pnlResultDisplay = new System.Windows.Forms.Panel();
            this.lblAlignViewer = new System.Windows.Forms.Label();
            this.tlpAlignViewer.SuspendLayout();
            this.pnlResultDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpSplitView
            // 
            this.tlpSplitView.ColumnCount = 2;
            this.tlpSplitView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSplitView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSplitView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSplitView.Location = new System.Drawing.Point(0, 0);
            this.tlpSplitView.Margin = new System.Windows.Forms.Padding(0);
            this.tlpSplitView.Name = "tlpSplitView";
            this.tlpSplitView.RowCount = 4;
            this.tlpSplitView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpSplitView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpSplitView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpSplitView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpSplitView.Size = new System.Drawing.Size(1365, 820);
            this.tlpSplitView.TabIndex = 1;
            // 
            // tlpAlignViewer
            // 
            this.tlpAlignViewer.ColumnCount = 1;
            this.tlpAlignViewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlignViewer.Controls.Add(this.pnlResultDisplay, 0, 1);
            this.tlpAlignViewer.Controls.Add(this.lblAlignViewer, 0, 0);
            this.tlpAlignViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAlignViewer.Location = new System.Drawing.Point(0, 0);
            this.tlpAlignViewer.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAlignViewer.Name = "tlpAlignViewer";
            this.tlpAlignViewer.RowCount = 2;
            this.tlpAlignViewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpAlignViewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlignViewer.Size = new System.Drawing.Size(1365, 860);
            this.tlpAlignViewer.TabIndex = 2;
            // 
            // pnlResultDisplay
            // 
            this.pnlResultDisplay.Controls.Add(this.tlpSplitView);
            this.pnlResultDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResultDisplay.Location = new System.Drawing.Point(0, 40);
            this.pnlResultDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.pnlResultDisplay.Name = "pnlResultDisplay";
            this.pnlResultDisplay.Size = new System.Drawing.Size(1365, 820);
            this.pnlResultDisplay.TabIndex = 3;
            // 
            // lblAlignViewer
            // 
            this.lblAlignViewer.BackColor = System.Drawing.Color.DodgerBlue;
            this.lblAlignViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAlignViewer.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblAlignViewer.ForeColor = System.Drawing.Color.White;
            this.lblAlignViewer.Location = new System.Drawing.Point(0, 0);
            this.lblAlignViewer.Margin = new System.Windows.Forms.Padding(0);
            this.lblAlignViewer.Name = "lblAlignViewer";
            this.lblAlignViewer.Size = new System.Drawing.Size(1365, 40);
            this.lblAlignViewer.TabIndex = 2;
            this.lblAlignViewer.Text = "ALIGN MONITORING";
            this.lblAlignViewer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AlignMonitoringControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpAlignViewer);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AlignMonitoringControl";
            this.Size = new System.Drawing.Size(1365, 860);
            this.Load += new System.EventHandler(this.AlignMonitoringControl_Load);
            this.tlpAlignViewer.ResumeLayout(false);
            this.pnlResultDisplay.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpSplitView;
        private System.Windows.Forms.TableLayoutPanel tlpAlignViewer;
        private System.Windows.Forms.Panel pnlResultDisplay;
        private System.Windows.Forms.Label lblAlignViewer;
    }
}
