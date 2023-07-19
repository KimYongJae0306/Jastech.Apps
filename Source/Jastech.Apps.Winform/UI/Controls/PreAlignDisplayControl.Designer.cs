namespace Jastech.Apps.Winform.UI.Controls
{
    partial class PreAlignDisplayControl
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
            this.tlpPreAlignDisplay = new System.Windows.Forms.TableLayoutPanel();
            this.lblAkkonResultViewer = new System.Windows.Forms.Label();
            this.pnlPreAlignDisplay = new System.Windows.Forms.Panel();
            this.tlpPreAlignDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpPreAlignDisplay
            // 
            this.tlpPreAlignDisplay.ColumnCount = 1;
            this.tlpPreAlignDisplay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPreAlignDisplay.Controls.Add(this.pnlPreAlignDisplay, 0, 1);
            this.tlpPreAlignDisplay.Controls.Add(this.lblAkkonResultViewer, 0, 0);
            this.tlpPreAlignDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPreAlignDisplay.Location = new System.Drawing.Point(0, 0);
            this.tlpPreAlignDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.tlpPreAlignDisplay.Name = "tlpPreAlignDisplay";
            this.tlpPreAlignDisplay.RowCount = 2;
            this.tlpPreAlignDisplay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpPreAlignDisplay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPreAlignDisplay.Size = new System.Drawing.Size(405, 240);
            this.tlpPreAlignDisplay.TabIndex = 0;
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
            this.lblAkkonResultViewer.Size = new System.Drawing.Size(405, 40);
            this.lblAkkonResultViewer.TabIndex = 2;
            this.lblAkkonResultViewer.Text = "PreAlign";
            this.lblAkkonResultViewer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlPreAlignDisplay
            // 
            this.pnlPreAlignDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPreAlignDisplay.Location = new System.Drawing.Point(0, 40);
            this.pnlPreAlignDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.pnlPreAlignDisplay.Name = "pnlPreAlignDisplay";
            this.pnlPreAlignDisplay.Size = new System.Drawing.Size(405, 200);
            this.pnlPreAlignDisplay.TabIndex = 3;
            // 
            // PreAlignDisplayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpPreAlignDisplay);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "PreAlignDisplayControl";
            this.Size = new System.Drawing.Size(405, 240);
            this.Load += new System.EventHandler(this.PreAlignDisplayControl_Load);
            this.tlpPreAlignDisplay.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpPreAlignDisplay;
        private System.Windows.Forms.Label lblAkkonResultViewer;
        private System.Windows.Forms.Panel pnlPreAlignDisplay;
    }
}
