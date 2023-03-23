namespace ATT.UI.Pages
{
    partial class TeachingPage
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
            this.tlpTeachingPage = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTeachingPage = new System.Windows.Forms.Panel();
            this.pnlTeachingItem = new System.Windows.Forms.Panel();
            this.tlpTeachingItem = new System.Windows.Forms.TableLayoutPanel();
            this.tlpTeaching = new System.Windows.Forms.TableLayoutPanel();
            this.pnlDisplay = new System.Windows.Forms.Panel();
            this.pnlTeaching = new System.Windows.Forms.Panel();
            this.tlpTeachingPage.SuspendLayout();
            this.pnlTeachingPage.SuspendLayout();
            this.pnlTeachingItem.SuspendLayout();
            this.tlpTeaching.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpTeachingPage
            // 
            this.tlpTeachingPage.ColumnCount = 2;
            this.tlpTeachingPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTeachingPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpTeachingPage.Controls.Add(this.pnlTeachingPage, 0, 0);
            this.tlpTeachingPage.Controls.Add(this.pnlTeachingItem, 1, 0);
            this.tlpTeachingPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTeachingPage.Location = new System.Drawing.Point(0, 0);
            this.tlpTeachingPage.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTeachingPage.Name = "tlpTeachingPage";
            this.tlpTeachingPage.RowCount = 1;
            this.tlpTeachingPage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTeachingPage.Size = new System.Drawing.Size(900, 900);
            this.tlpTeachingPage.TabIndex = 0;
            // 
            // pnlTeachingPage
            // 
            this.pnlTeachingPage.Controls.Add(this.tlpTeaching);
            this.pnlTeachingPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTeachingPage.Location = new System.Drawing.Point(0, 0);
            this.pnlTeachingPage.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTeachingPage.Name = "pnlTeachingPage";
            this.pnlTeachingPage.Size = new System.Drawing.Size(740, 900);
            this.pnlTeachingPage.TabIndex = 0;
            // 
            // pnlTeachingItem
            // 
            this.pnlTeachingItem.Controls.Add(this.tlpTeachingItem);
            this.pnlTeachingItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTeachingItem.Location = new System.Drawing.Point(740, 0);
            this.pnlTeachingItem.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTeachingItem.Name = "pnlTeachingItem";
            this.pnlTeachingItem.Size = new System.Drawing.Size(160, 900);
            this.pnlTeachingItem.TabIndex = 1;
            // 
            // tlpTeachingItem
            // 
            this.tlpTeachingItem.ColumnCount = 1;
            this.tlpTeachingItem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTeachingItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTeachingItem.Location = new System.Drawing.Point(0, 0);
            this.tlpTeachingItem.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTeachingItem.Name = "tlpTeachingItem";
            this.tlpTeachingItem.RowCount = 6;
            this.tlpTeachingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpTeachingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpTeachingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpTeachingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpTeachingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTeachingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpTeachingItem.Size = new System.Drawing.Size(160, 900);
            this.tlpTeachingItem.TabIndex = 0;
            // 
            // tlpTeaching
            // 
            this.tlpTeaching.ColumnCount = 2;
            this.tlpTeaching.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTeaching.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTeaching.Controls.Add(this.pnlDisplay, 0, 0);
            this.tlpTeaching.Controls.Add(this.pnlTeaching, 1, 0);
            this.tlpTeaching.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTeaching.Location = new System.Drawing.Point(0, 0);
            this.tlpTeaching.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTeaching.Name = "tlpTeaching";
            this.tlpTeaching.RowCount = 1;
            this.tlpTeaching.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTeaching.Size = new System.Drawing.Size(740, 900);
            this.tlpTeaching.TabIndex = 0;
            // 
            // pnlDisplay
            // 
            this.pnlDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDisplay.Location = new System.Drawing.Point(0, 0);
            this.pnlDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.pnlDisplay.Name = "pnlDisplay";
            this.pnlDisplay.Size = new System.Drawing.Size(370, 900);
            this.pnlDisplay.TabIndex = 0;
            // 
            // pnlTeaching
            // 
            this.pnlTeaching.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTeaching.Location = new System.Drawing.Point(370, 0);
            this.pnlTeaching.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTeaching.Name = "pnlTeaching";
            this.pnlTeaching.Size = new System.Drawing.Size(370, 900);
            this.pnlTeaching.TabIndex = 0;
            // 
            // TeachingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tlpTeachingPage);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.Name = "TeachingPage";
            this.Size = new System.Drawing.Size(900, 900);
            this.Load += new System.EventHandler(this.TeachingPage_Load);
            this.tlpTeachingPage.ResumeLayout(false);
            this.pnlTeachingPage.ResumeLayout(false);
            this.pnlTeachingItem.ResumeLayout(false);
            this.tlpTeaching.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpTeachingPage;
        private System.Windows.Forms.Panel pnlTeachingPage;
        private System.Windows.Forms.Panel pnlTeachingItem;
        private System.Windows.Forms.TableLayoutPanel tlpTeachingItem;
        private System.Windows.Forms.TableLayoutPanel tlpTeaching;
        private System.Windows.Forms.Panel pnlDisplay;
        private System.Windows.Forms.Panel pnlTeaching;
    }
}
