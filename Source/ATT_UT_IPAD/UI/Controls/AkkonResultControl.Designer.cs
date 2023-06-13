namespace ATT_UT_IPAD.UI.Controls
{
    partial class AkkonResultControl
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
            this.tlpAkkonResult = new System.Windows.Forms.TableLayoutPanel();
            this.pnlAkkonResultData = new System.Windows.Forms.Panel();
            this.pnlAkkonResultChart = new System.Windows.Forms.Panel();
            this.tlpAkkonResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAkkonResult
            // 
            this.tlpAkkonResult.ColumnCount = 1;
            this.tlpAkkonResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAkkonResult.Controls.Add(this.pnlAkkonResultData, 0, 0);
            this.tlpAkkonResult.Controls.Add(this.pnlAkkonResultChart, 0, 1);
            this.tlpAkkonResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAkkonResult.Location = new System.Drawing.Point(0, 0);
            this.tlpAkkonResult.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAkkonResult.Name = "tlpAkkonResult";
            this.tlpAkkonResult.RowCount = 2;
            this.tlpAkkonResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAkkonResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAkkonResult.Size = new System.Drawing.Size(300, 300);
            this.tlpAkkonResult.TabIndex = 1;
            // 
            // pnlAkkonResultData
            // 
            this.pnlAkkonResultData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAkkonResultData.Location = new System.Drawing.Point(0, 0);
            this.pnlAkkonResultData.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAkkonResultData.Name = "pnlAkkonResultData";
            this.pnlAkkonResultData.Size = new System.Drawing.Size(300, 150);
            this.pnlAkkonResultData.TabIndex = 0;
            // 
            // pnlAkkonResultChart
            // 
            this.pnlAkkonResultChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAkkonResultChart.Location = new System.Drawing.Point(0, 150);
            this.pnlAkkonResultChart.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAkkonResultChart.Name = "pnlAkkonResultChart";
            this.pnlAkkonResultChart.Size = new System.Drawing.Size(300, 150);
            this.pnlAkkonResultChart.TabIndex = 0;
            // 
            // AkkonResultControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpAkkonResult);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "AkkonResultControl";
            this.Size = new System.Drawing.Size(300, 300);
            this.Load += new System.EventHandler(this.AkkonResultControl_Load);
            this.tlpAkkonResult.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAkkonResult;
        private System.Windows.Forms.Panel pnlAkkonResultData;
        private System.Windows.Forms.Panel pnlAkkonResultChart;
    }
}
