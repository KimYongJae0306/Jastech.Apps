namespace ATT_UT_IPAD.UI.Controls
{
    partial class AkkonResultDataControl
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
            this.pnlAkkonResultData = new System.Windows.Forms.Panel();
            this.tlpAkkonResultData = new System.Windows.Forms.TableLayoutPanel();
            this.lblAkkonResultData = new System.Windows.Forms.Label();
            this.tlpAkkonResultData.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlAkkonResultData
            // 
            this.pnlAkkonResultData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAkkonResultData.Location = new System.Drawing.Point(0, 40);
            this.pnlAkkonResultData.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAkkonResultData.Name = "pnlAkkonResultData";
            this.pnlAkkonResultData.Size = new System.Drawing.Size(300, 260);
            this.pnlAkkonResultData.TabIndex = 0;
            // 
            // tlpAkkonResultData
            // 
            this.tlpAkkonResultData.ColumnCount = 1;
            this.tlpAkkonResultData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAkkonResultData.Controls.Add(this.lblAkkonResultData, 0, 0);
            this.tlpAkkonResultData.Controls.Add(this.pnlAkkonResultData, 0, 1);
            this.tlpAkkonResultData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAkkonResultData.Location = new System.Drawing.Point(0, 0);
            this.tlpAkkonResultData.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAkkonResultData.Name = "tlpAkkonResultData";
            this.tlpAkkonResultData.RowCount = 2;
            this.tlpAkkonResultData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpAkkonResultData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAkkonResultData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpAkkonResultData.Size = new System.Drawing.Size(300, 300);
            this.tlpAkkonResultData.TabIndex = 1;
            // 
            // lblAkkonResultData
            // 
            this.lblAkkonResultData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblAkkonResultData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAkkonResultData.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblAkkonResultData.ForeColor = System.Drawing.Color.White;
            this.lblAkkonResultData.Location = new System.Drawing.Point(1, 1);
            this.lblAkkonResultData.Margin = new System.Windows.Forms.Padding(1);
            this.lblAkkonResultData.Name = "lblAkkonResultData";
            this.lblAkkonResultData.Size = new System.Drawing.Size(298, 38);
            this.lblAkkonResultData.TabIndex = 3;
            this.lblAkkonResultData.Text = "Result";
            this.lblAkkonResultData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AkkonResultDataControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpAkkonResultData);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "AkkonResultDataControl";
            this.Size = new System.Drawing.Size(300, 300);
            this.Load += new System.EventHandler(this.AkkonResultControl_Load);
            this.tlpAkkonResultData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlAkkonResultData;
        private System.Windows.Forms.TableLayoutPanel tlpAkkonResultData;
        private System.Windows.Forms.Label lblAkkonResultData;
    }
}
