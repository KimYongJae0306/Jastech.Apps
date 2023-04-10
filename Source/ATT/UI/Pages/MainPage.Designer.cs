namespace ATT.UI.Pages
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
            this.pnlAkkon = new System.Windows.Forms.Panel();
            this.pnlAlign = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.pnlAlign, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlAkkon, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1056, 562);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pnlAkkon
            // 
            this.pnlAkkon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAkkon.Location = new System.Drawing.Point(0, 0);
            this.pnlAkkon.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAkkon.Name = "pnlAkkon";
            this.pnlAkkon.Size = new System.Drawing.Size(1056, 281);
            this.pnlAkkon.TabIndex = 0;
            // 
            // pnlAlign
            // 
            this.pnlAlign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAlign.Location = new System.Drawing.Point(0, 281);
            this.pnlAlign.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAlign.Name = "pnlAlign";
            this.pnlAlign.Size = new System.Drawing.Size(1056, 281);
            this.pnlAlign.TabIndex = 1;
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainPage";
            this.Size = new System.Drawing.Size(1056, 562);
            this.Load += new System.EventHandler(this.MainPage_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlAlign;
        private System.Windows.Forms.Panel pnlAkkon;
    }
}
