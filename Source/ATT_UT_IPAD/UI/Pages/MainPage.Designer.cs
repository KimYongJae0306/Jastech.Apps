namespace ATT_UT_IPAD.UI.Pages
{
    partial class MainPage
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblAkkonButton = new System.Windows.Forms.Label();
            this.lblMainButton = new System.Windows.Forms.Label();
            this.lblAlignButton = new System.Windows.Forms.Label();
            this.pnlView = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlView, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1016, 498);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.lblAlignButton, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblAkkonButton, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblMainButton, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1016, 50);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // lblAkkonButton
            // 
            this.lblAkkonButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblAkkonButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAkkonButton.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblAkkonButton.ForeColor = System.Drawing.Color.White;
            this.lblAkkonButton.Location = new System.Drawing.Point(200, 0);
            this.lblAkkonButton.Margin = new System.Windows.Forms.Padding(0);
            this.lblAkkonButton.Name = "lblAkkonButton";
            this.lblAkkonButton.Size = new System.Drawing.Size(200, 50);
            this.lblAkkonButton.TabIndex = 3;
            this.lblAkkonButton.Text = "AKKON";
            this.lblAkkonButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAkkonButton.Click += new System.EventHandler(this.lblAkkonButton_Click);
            // 
            // lblMainButton
            // 
            this.lblMainButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblMainButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMainButton.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblMainButton.ForeColor = System.Drawing.Color.White;
            this.lblMainButton.Location = new System.Drawing.Point(0, 0);
            this.lblMainButton.Margin = new System.Windows.Forms.Padding(0);
            this.lblMainButton.Name = "lblMainButton";
            this.lblMainButton.Size = new System.Drawing.Size(200, 50);
            this.lblMainButton.TabIndex = 1;
            this.lblMainButton.Text = "Main";
            this.lblMainButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMainButton.Click += new System.EventHandler(this.lblMainButton_Click);
            // 
            // lblAlignButton
            // 
            this.lblAlignButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblAlignButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAlignButton.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblAlignButton.ForeColor = System.Drawing.Color.White;
            this.lblAlignButton.Location = new System.Drawing.Point(400, 0);
            this.lblAlignButton.Margin = new System.Windows.Forms.Padding(0);
            this.lblAlignButton.Name = "lblAlignButton";
            this.lblAlignButton.Size = new System.Drawing.Size(200, 50);
            this.lblAlignButton.TabIndex = 2;
            this.lblAlignButton.Text = "ALIGN";
            this.lblAlignButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAlignButton.Click += new System.EventHandler(this.lblAlignButton_Click);
            // 
            // pnlView
            // 
            this.pnlView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlView.Location = new System.Drawing.Point(0, 50);
            this.pnlView.Margin = new System.Windows.Forms.Padding(0);
            this.pnlView.Name = "pnlView";
            this.pnlView.Size = new System.Drawing.Size(1016, 448);
            this.pnlView.TabIndex = 2;
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MainPage";
            this.Size = new System.Drawing.Size(1016, 498);
            this.Load += new System.EventHandler(this.MainPage_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblMainButton;
        private System.Windows.Forms.Label lblAlignButton;
        private System.Windows.Forms.Label lblAkkonButton;
        private System.Windows.Forms.Panel pnlView;
    }
}
