namespace ATT.UI.Controls
{
    partial class TrendViewControl
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
            this.tlpTrend = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // tlpTrend
            // 
            this.tlpTrend.ColumnCount = 2;
            this.tlpTrend.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTrend.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTrend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTrend.Location = new System.Drawing.Point(0, 0);
            this.tlpTrend.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTrend.Name = "tlpTrend";
            this.tlpTrend.RowCount = 1;
            this.tlpTrend.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTrend.Size = new System.Drawing.Size(951, 506);
            this.tlpTrend.TabIndex = 0;
            // 
            // TrendControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpTrend);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "TrendControl";
            this.Size = new System.Drawing.Size(951, 506);
            this.Load += new System.EventHandler(this.TrendControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpTrend;
    }
}
