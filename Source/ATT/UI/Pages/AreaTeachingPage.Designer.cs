namespace ATT.UI.Pages
{
    partial class AreaTeachingPage
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
            this.tlpTeaching = new System.Windows.Forms.TableLayoutPanel();
            this.pnlDisplay = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.pnlTeach = new System.Windows.Forms.Panel();
            this.pnlTeachingItem = new System.Windows.Forms.Panel();
            this.tlpTeachingItem = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPreAlign = new System.Windows.Forms.Button();
            this.btnMotionPopup = new System.Windows.Forms.Button();
            this.btnLinescan = new System.Windows.Forms.Button();
            this.tlpTeachingPage.SuspendLayout();
            this.pnlTeachingPage.SuspendLayout();
            this.tlpTeaching.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlTeachingItem.SuspendLayout();
            this.tlpTeachingItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpTeachingPage
            // 
            this.tlpTeachingPage.ColumnCount = 2;
            this.tlpTeachingPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTeachingPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 144F));
            this.tlpTeachingPage.Controls.Add(this.pnlTeachingPage, 0, 0);
            this.tlpTeachingPage.Controls.Add(this.pnlTeachingItem, 1, 0);
            this.tlpTeachingPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTeachingPage.Location = new System.Drawing.Point(0, 0);
            this.tlpTeachingPage.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTeachingPage.Name = "tlpTeachingPage";
            this.tlpTeachingPage.RowCount = 1;
            this.tlpTeachingPage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTeachingPage.Size = new System.Drawing.Size(1245, 720);
            this.tlpTeachingPage.TabIndex = 0;
            // 
            // pnlTeachingPage
            // 
            this.pnlTeachingPage.Controls.Add(this.tlpTeaching);
            this.pnlTeachingPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTeachingPage.Location = new System.Drawing.Point(0, 0);
            this.pnlTeachingPage.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTeachingPage.Name = "pnlTeachingPage";
            this.pnlTeachingPage.Size = new System.Drawing.Size(1101, 720);
            this.pnlTeachingPage.TabIndex = 0;
            // 
            // tlpTeaching
            // 
            this.tlpTeaching.ColumnCount = 2;
            this.tlpTeaching.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTeaching.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTeaching.Controls.Add(this.pnlDisplay, 0, 0);
            this.tlpTeaching.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.tlpTeaching.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTeaching.Location = new System.Drawing.Point(0, 0);
            this.tlpTeaching.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTeaching.Name = "tlpTeaching";
            this.tlpTeaching.RowCount = 1;
            this.tlpTeaching.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTeaching.Size = new System.Drawing.Size(1101, 720);
            this.tlpTeaching.TabIndex = 0;
            // 
            // pnlDisplay
            // 
            this.pnlDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDisplay.Location = new System.Drawing.Point(0, 0);
            this.pnlDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.pnlDisplay.Name = "pnlDisplay";
            this.pnlDisplay.Size = new System.Drawing.Size(550, 720);
            this.pnlDisplay.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlTeach, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(552, 2);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(547, 716);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnLoadImage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(543, 76);
            this.panel1.TabIndex = 1;
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.BackColor = System.Drawing.Color.White;
            this.btnLoadImage.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.btnLoadImage.ForeColor = System.Drawing.Color.Black;
            this.btnLoadImage.Location = new System.Drawing.Point(11, 13);
            this.btnLoadImage.Margin = new System.Windows.Forms.Padding(2);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(116, 38);
            this.btnLoadImage.TabIndex = 20;
            this.btnLoadImage.Text = "Load Image";
            this.btnLoadImage.UseVisualStyleBackColor = false;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // pnlTeach
            // 
            this.pnlTeach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTeach.Location = new System.Drawing.Point(0, 80);
            this.pnlTeach.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTeach.Name = "pnlTeach";
            this.pnlTeach.Size = new System.Drawing.Size(547, 636);
            this.pnlTeach.TabIndex = 0;
            // 
            // pnlTeachingItem
            // 
            this.pnlTeachingItem.Controls.Add(this.tlpTeachingItem);
            this.pnlTeachingItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTeachingItem.Location = new System.Drawing.Point(1101, 0);
            this.pnlTeachingItem.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTeachingItem.Name = "pnlTeachingItem";
            this.pnlTeachingItem.Size = new System.Drawing.Size(144, 720);
            this.pnlTeachingItem.TabIndex = 1;
            // 
            // tlpTeachingItem
            // 
            this.tlpTeachingItem.ColumnCount = 1;
            this.tlpTeachingItem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTeachingItem.Controls.Add(this.btnSave, 0, 6);
            this.tlpTeachingItem.Controls.Add(this.btnPreAlign, 0, 0);
            this.tlpTeachingItem.Controls.Add(this.btnMotionPopup, 0, 5);
            this.tlpTeachingItem.Controls.Add(this.btnLinescan, 0, 3);
            this.tlpTeachingItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTeachingItem.Location = new System.Drawing.Point(0, 0);
            this.tlpTeachingItem.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTeachingItem.Name = "tlpTeachingItem";
            this.tlpTeachingItem.RowCount = 7;
            this.tlpTeachingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpTeachingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpTeachingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpTeachingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpTeachingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTeachingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpTeachingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpTeachingItem.Size = new System.Drawing.Size(144, 720);
            this.tlpTeachingItem.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.White;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(2, 642);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(140, 76);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPreAlign
            // 
            this.btnPreAlign.BackColor = System.Drawing.Color.White;
            this.btnPreAlign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPreAlign.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btnPreAlign.ForeColor = System.Drawing.Color.Black;
            this.btnPreAlign.Location = new System.Drawing.Point(2, 2);
            this.btnPreAlign.Margin = new System.Windows.Forms.Padding(2);
            this.btnPreAlign.Name = "btnPreAlign";
            this.btnPreAlign.Size = new System.Drawing.Size(140, 76);
            this.btnPreAlign.TabIndex = 19;
            this.btnPreAlign.Text = "PreAlign";
            this.btnPreAlign.UseVisualStyleBackColor = false;
            this.btnPreAlign.Click += new System.EventHandler(this.btnPreAlign_Click);
            // 
            // btnMotionPopup
            // 
            this.btnMotionPopup.BackColor = System.Drawing.Color.White;
            this.btnMotionPopup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMotionPopup.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btnMotionPopup.ForeColor = System.Drawing.Color.Black;
            this.btnMotionPopup.Location = new System.Drawing.Point(2, 562);
            this.btnMotionPopup.Margin = new System.Windows.Forms.Padding(2);
            this.btnMotionPopup.Name = "btnMotionPopup";
            this.btnMotionPopup.Size = new System.Drawing.Size(140, 76);
            this.btnMotionPopup.TabIndex = 19;
            this.btnMotionPopup.Text = "Motion\r\nPopup";
            this.btnMotionPopup.UseVisualStyleBackColor = false;
            this.btnMotionPopup.Click += new System.EventHandler(this.btnMotionPopup_Click);
            // 
            // btnLinescan
            // 
            this.btnLinescan.BackColor = System.Drawing.Color.White;
            this.btnLinescan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLinescan.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btnLinescan.ForeColor = System.Drawing.Color.Black;
            this.btnLinescan.Location = new System.Drawing.Point(2, 242);
            this.btnLinescan.Margin = new System.Windows.Forms.Padding(2);
            this.btnLinescan.Name = "btnLinescan";
            this.btnLinescan.Size = new System.Drawing.Size(140, 76);
            this.btnLinescan.TabIndex = 19;
            this.btnLinescan.Text = "Linescan";
            this.btnLinescan.UseVisualStyleBackColor = false;
            this.btnLinescan.Visible = false;
            this.btnLinescan.Click += new System.EventHandler(this.btnLinescan_Click);
            // 
            // AreaTeachingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpTeachingPage);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "AreaTeachingPage";
            this.Size = new System.Drawing.Size(1245, 720);
            this.Load += new System.EventHandler(this.TeachingPage_Load);
            this.tlpTeachingPage.ResumeLayout(false);
            this.pnlTeachingPage.ResumeLayout(false);
            this.tlpTeaching.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlTeachingItem.ResumeLayout(false);
            this.tlpTeachingItem.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpTeachingPage;
        private System.Windows.Forms.Panel pnlTeachingPage;
        private System.Windows.Forms.Panel pnlTeachingItem;
        private System.Windows.Forms.TableLayoutPanel tlpTeachingItem;
        private System.Windows.Forms.TableLayoutPanel tlpTeaching;
        private System.Windows.Forms.Panel pnlDisplay;
        private System.Windows.Forms.Panel pnlTeach;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLinescan;
        private System.Windows.Forms.Button btnPreAlign;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.Button btnMotionPopup;
    }
}
