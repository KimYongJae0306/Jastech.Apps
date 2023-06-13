namespace ATT_UT_IPAD.UI.Controls
{
    partial class AkkonResultDisplayControl
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
            this.tlpAkkonResultDisplay = new System.Windows.Forms.TableLayoutPanel();
            this.pnlInspDisplay = new System.Windows.Forms.Panel();
            this.tlpAkkonResultDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAkkonResultDisplay
            // 
            this.tlpAkkonResultDisplay.ColumnCount = 1;
            this.tlpAkkonResultDisplay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAkkonResultDisplay.Controls.Add(this.pnlInspDisplay, 0, 0);
            this.tlpAkkonResultDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAkkonResultDisplay.Location = new System.Drawing.Point(0, 0);
            this.tlpAkkonResultDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAkkonResultDisplay.Name = "tlpAkkonResultDisplay";
            this.tlpAkkonResultDisplay.RowCount = 1;
            this.tlpAkkonResultDisplay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAkkonResultDisplay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpAkkonResultDisplay.Size = new System.Drawing.Size(600, 300);
            this.tlpAkkonResultDisplay.TabIndex = 4;
            // 
            // pnlInspDisplay
            // 
            this.pnlInspDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInspDisplay.Location = new System.Drawing.Point(0, 0);
            this.pnlInspDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.pnlInspDisplay.Name = "pnlInspDisplay";
            this.pnlInspDisplay.Size = new System.Drawing.Size(600, 300);
            this.pnlInspDisplay.TabIndex = 2;
            // 
            // AkkonResultDisplayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpAkkonResultDisplay);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "AkkonResultDisplayControl";
            this.Size = new System.Drawing.Size(600, 300);
            this.Load += new System.EventHandler(this.AkkonResultDisplayControl_Load);
            this.tlpAkkonResultDisplay.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAkkonResultDisplay;
        private System.Windows.Forms.Panel pnlInspDisplay;
    }
}
