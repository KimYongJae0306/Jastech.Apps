namespace ATT_UT_IPAD.UI.Controls
{
    partial class AlignResultControl
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
            this.tlpAlignResult = new System.Windows.Forms.TableLayoutPanel();
            this.pnlAlignResultData = new System.Windows.Forms.Panel();
            this.pnlAlignResultChart = new System.Windows.Forms.Panel();
            this.tlpAlignResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAlignResult
            // 
            this.tlpAlignResult.ColumnCount = 1;
            this.tlpAlignResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAlignResult.Controls.Add(this.pnlAlignResultData, 0, 0);
            this.tlpAlignResult.Controls.Add(this.pnlAlignResultChart, 0, 1);
            this.tlpAlignResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAlignResult.Location = new System.Drawing.Point(0, 0);
            this.tlpAlignResult.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAlignResult.Name = "tlpAlignResult";
            this.tlpAlignResult.RowCount = 2;
            this.tlpAlignResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAlignResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAlignResult.Size = new System.Drawing.Size(300, 300);
            this.tlpAlignResult.TabIndex = 0;
            // 
            // pnlAlignResultData
            // 
            this.pnlAlignResultData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAlignResultData.Location = new System.Drawing.Point(0, 0);
            this.pnlAlignResultData.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAlignResultData.Name = "pnlAlignResultData";
            this.pnlAlignResultData.Size = new System.Drawing.Size(300, 150);
            this.pnlAlignResultData.TabIndex = 0;
            // 
            // pnlAlignResultChart
            // 
            this.pnlAlignResultChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAlignResultChart.Location = new System.Drawing.Point(0, 150);
            this.pnlAlignResultChart.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAlignResultChart.Name = "pnlAlignResultChart";
            this.pnlAlignResultChart.Size = new System.Drawing.Size(300, 150);
            this.pnlAlignResultChart.TabIndex = 0;
            // 
            // AlignResultControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpAlignResult);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "AlignResultControl";
            this.Size = new System.Drawing.Size(300, 300);
            this.Load += new System.EventHandler(this.AlignResultControl_Load);
            this.tlpAlignResult.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAlignResult;
        private System.Windows.Forms.Panel pnlAlignResultData;
        private System.Windows.Forms.Panel pnlAlignResultChart;
    }
}
