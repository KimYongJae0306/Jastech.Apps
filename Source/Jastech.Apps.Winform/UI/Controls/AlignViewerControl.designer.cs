namespace Jastech.Apps.Winform.UI.Controls
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
            this.pnlResultDisplay = new System.Windows.Forms.Panel();
            this.lblAlignViewer = new System.Windows.Forms.Label();
            this.tlpAlignViewer.SuspendLayout();
            this.SuspendLayout();
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
            this.tlpAlignViewer.Size = new System.Drawing.Size(900, 300);
            this.tlpAlignViewer.TabIndex = 0;
            // 
            // pnlResultDisplay
            // 
            this.pnlResultDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResultDisplay.Location = new System.Drawing.Point(0, 40);
            this.pnlResultDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.pnlResultDisplay.Name = "pnlResultDisplay";
            this.pnlResultDisplay.Size = new System.Drawing.Size(900, 260);
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
            this.lblAlignViewer.Size = new System.Drawing.Size(900, 40);
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
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tlpAlignViewer;
        private System.Windows.Forms.Panel pnlResultDisplay;
        private System.Windows.Forms.Label lblAlignViewer;
    }
}
