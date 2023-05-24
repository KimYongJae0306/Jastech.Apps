namespace ATT.UI.Pages
{
    partial class LogPage
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
            this.tlpLogPage = new System.Windows.Forms.TableLayoutPanel();
            this.pnlContents = new System.Windows.Forms.Panel();
            this.pnlHisotryType = new System.Windows.Forms.Panel();
            this.lblTrend = new System.Windows.Forms.Label();
            this.lblUPH = new System.Windows.Forms.Label();
            this.tlpLogPage.SuspendLayout();
            this.pnlHisotryType.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpLogPage
            // 
            this.tlpLogPage.ColumnCount = 1;
            this.tlpLogPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLogPage.Controls.Add(this.pnlContents, 0, 1);
            this.tlpLogPage.Controls.Add(this.pnlHisotryType, 0, 0);
            this.tlpLogPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLogPage.Location = new System.Drawing.Point(0, 0);
            this.tlpLogPage.Margin = new System.Windows.Forms.Padding(0);
            this.tlpLogPage.Name = "tlpLogPage";
            this.tlpLogPage.RowCount = 2;
            this.tlpLogPage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpLogPage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLogPage.Size = new System.Drawing.Size(1056, 562);
            this.tlpLogPage.TabIndex = 0;
            // 
            // pnlContents
            // 
            this.pnlContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContents.Location = new System.Drawing.Point(0, 40);
            this.pnlContents.Margin = new System.Windows.Forms.Padding(0);
            this.pnlContents.Name = "pnlContents";
            this.pnlContents.Size = new System.Drawing.Size(1056, 522);
            this.pnlContents.TabIndex = 1;
            // 
            // pnlHisotryType
            // 
            this.pnlHisotryType.Controls.Add(this.lblUPH);
            this.pnlHisotryType.Controls.Add(this.lblTrend);
            this.pnlHisotryType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHisotryType.Location = new System.Drawing.Point(0, 0);
            this.pnlHisotryType.Margin = new System.Windows.Forms.Padding(0);
            this.pnlHisotryType.Name = "pnlHisotryType";
            this.pnlHisotryType.Size = new System.Drawing.Size(1056, 40);
            this.pnlHisotryType.TabIndex = 2;
            // 
            // lblTrend
            // 
            this.lblTrend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblTrend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTrend.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblTrend.ForeColor = System.Drawing.Color.White;
            this.lblTrend.Location = new System.Drawing.Point(99, 0);
            this.lblTrend.Margin = new System.Windows.Forms.Padding(0);
            this.lblTrend.Name = "lblTrend";
            this.lblTrend.Size = new System.Drawing.Size(140, 40);
            this.lblTrend.TabIndex = 2;
            this.lblTrend.Text = "Trend";
            this.lblTrend.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTrend.Click += new System.EventHandler(this.lblTrend_Click);
            // 
            // lblUPH
            // 
            this.lblUPH.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblUPH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUPH.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblUPH.ForeColor = System.Drawing.Color.White;
            this.lblUPH.Location = new System.Drawing.Point(458, 0);
            this.lblUPH.Margin = new System.Windows.Forms.Padding(0);
            this.lblUPH.Name = "lblUPH";
            this.lblUPH.Size = new System.Drawing.Size(140, 40);
            this.lblUPH.TabIndex = 3;
            this.lblUPH.Text = "UPH";
            this.lblUPH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUPH.Click += new System.EventHandler(this.lblUPH_Click);
            // 
            // LogPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpLogPage);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "LogPage";
            this.Size = new System.Drawing.Size(1056, 562);
            this.Load += new System.EventHandler(this.LogPage_Load);
            this.tlpLogPage.ResumeLayout(false);
            this.pnlHisotryType.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpLogPage;
        private System.Windows.Forms.Panel pnlContents;
        private System.Windows.Forms.Panel pnlHisotryType;
        private System.Windows.Forms.Label lblUPH;
        private System.Windows.Forms.Label lblTrend;
    }
}
