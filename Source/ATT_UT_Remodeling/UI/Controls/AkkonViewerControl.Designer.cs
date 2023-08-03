namespace ATT_UT_Remodeling.UI.Controls
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
            this.tlpAkkonViewer = new System.Windows.Forms.TableLayoutPanel();
            this.pnlResultDisplay = new System.Windows.Forms.Panel();
            this.lblAkkonViewer = new System.Windows.Forms.Label();
            this.tlpAkkonViewer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAkkonViewer
            // 
            this.tlpAkkonViewer.ColumnCount = 1;
            this.tlpAkkonViewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAkkonViewer.Controls.Add(this.pnlResultDisplay, 0, 1);
            this.tlpAkkonViewer.Controls.Add(this.lblAkkonViewer, 0, 0);
            this.tlpAkkonViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAkkonViewer.Location = new System.Drawing.Point(0, 0);
            this.tlpAkkonViewer.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAkkonViewer.Name = "tlpAkkonViewer";
            this.tlpAkkonViewer.RowCount = 2;
            this.tlpAkkonViewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpAkkonViewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAkkonViewer.Size = new System.Drawing.Size(900, 300);
            this.tlpAkkonViewer.TabIndex = 0;
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
            // lblAkkonViewer
            // 
            this.lblAkkonViewer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblAkkonViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAkkonViewer.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblAkkonViewer.ForeColor = System.Drawing.Color.White;
            this.lblAkkonViewer.Location = new System.Drawing.Point(0, 0);
            this.lblAkkonViewer.Margin = new System.Windows.Forms.Padding(0);
            this.lblAkkonViewer.Name = "lblAkkonViewer";
            this.lblAkkonViewer.Size = new System.Drawing.Size(900, 40);
            this.lblAkkonViewer.TabIndex = 2;
            this.lblAkkonViewer.Text = "AKKON";
            this.lblAkkonViewer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AkkonViewerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpAkkonViewer);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "AkkonViewerControl";
            this.Size = new System.Drawing.Size(900, 300);
            this.Load += new System.EventHandler(this.AkkonViewerControl_Load);
            this.tlpAkkonViewer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tlpAkkonViewer;
        private System.Windows.Forms.Label lblAkkonViewer;
        private System.Windows.Forms.Panel pnlResultDisplay;
    }
}
