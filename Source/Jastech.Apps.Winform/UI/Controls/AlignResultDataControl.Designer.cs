namespace ATT_UT_IPAD.UI.Controls
{
    partial class AlignResultDataControl
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
            this.tlpAlignResultData = new System.Windows.Forms.TableLayoutPanel();
            this.lblAlignResultData = new System.Windows.Forms.Label();
            this.pnlAlignResultData = new System.Windows.Forms.Panel();
            this.tlpAlignResultData.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAlignResultData
            // 
            this.tlpAlignResultData.ColumnCount = 1;
            this.tlpAlignResultData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlignResultData.Controls.Add(this.lblAlignResultData, 0, 0);
            this.tlpAlignResultData.Controls.Add(this.pnlAlignResultData, 0, 1);
            this.tlpAlignResultData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAlignResultData.Location = new System.Drawing.Point(0, 0);
            this.tlpAlignResultData.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAlignResultData.Name = "tlpAlignResultData";
            this.tlpAlignResultData.RowCount = 2;
            this.tlpAlignResultData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpAlignResultData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlignResultData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpAlignResultData.Size = new System.Drawing.Size(300, 300);
            this.tlpAlignResultData.TabIndex = 2;
            // 
            // lblAlignResultData
            // 
            this.lblAlignResultData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblAlignResultData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAlignResultData.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblAlignResultData.ForeColor = System.Drawing.Color.White;
            this.lblAlignResultData.Location = new System.Drawing.Point(1, 1);
            this.lblAlignResultData.Margin = new System.Windows.Forms.Padding(1);
            this.lblAlignResultData.Name = "lblAlignResultData";
            this.lblAlignResultData.Size = new System.Drawing.Size(298, 38);
            this.lblAlignResultData.TabIndex = 3;
            this.lblAlignResultData.Text = "Result";
            this.lblAlignResultData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlAlignResultData
            // 
            this.pnlAlignResultData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAlignResultData.Location = new System.Drawing.Point(0, 40);
            this.pnlAlignResultData.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAlignResultData.Name = "pnlAlignResultData";
            this.pnlAlignResultData.Size = new System.Drawing.Size(300, 260);
            this.pnlAlignResultData.TabIndex = 0;
            // 
            // AlignResultDataControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpAlignResultData);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "AlignResultDataControl";
            this.Size = new System.Drawing.Size(300, 300);
            this.Load += new System.EventHandler(this.AlignResultControl_Load);
            this.tlpAlignResultData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAlignResultData;
        private System.Windows.Forms.Label lblAlignResultData;
        private System.Windows.Forms.Panel pnlAlignResultData;
    }
}
