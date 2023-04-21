namespace CameraTeseter
{
    partial class MainForm
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.tlpLoadImage = new System.Windows.Forms.TableLayoutPanel();
            this.btnSaveImageCam0 = new System.Windows.Forms.Button();
            this.btnGrabStopCam0 = new System.Windows.Forms.Button();
            this.btnGrabStartCam0 = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSaveImageCam1 = new System.Windows.Forms.Button();
            this.btnGrabStopCam1 = new System.Windows.Forms.Button();
            this.btnGrabStartCam1 = new System.Windows.Forms.Button();
            this.pnlCam0Display = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlCam1Display = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tlpLoadImage.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel6, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.pnlCam0Display, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlCam1Display, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1225, 529);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.tlpLoadImage);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 429);
            this.panel6.Margin = new System.Windows.Forms.Padding(0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(612, 100);
            this.panel6.TabIndex = 8;
            // 
            // tlpLoadImage
            // 
            this.tlpLoadImage.ColumnCount = 3;
            this.tlpLoadImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpLoadImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpLoadImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpLoadImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpLoadImage.Controls.Add(this.btnSaveImageCam0, 0, 0);
            this.tlpLoadImage.Controls.Add(this.btnGrabStopCam0, 0, 0);
            this.tlpLoadImage.Controls.Add(this.btnGrabStartCam0, 0, 0);
            this.tlpLoadImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLoadImage.Location = new System.Drawing.Point(0, 0);
            this.tlpLoadImage.Margin = new System.Windows.Forms.Padding(0);
            this.tlpLoadImage.Name = "tlpLoadImage";
            this.tlpLoadImage.RowCount = 1;
            this.tlpLoadImage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLoadImage.Size = new System.Drawing.Size(612, 100);
            this.tlpLoadImage.TabIndex = 4;
            // 
            // btnSaveImageCam0
            // 
            this.btnSaveImageCam0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.btnSaveImageCam0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSaveImageCam0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveImageCam0.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.btnSaveImageCam0.ForeColor = System.Drawing.Color.White;
            this.btnSaveImageCam0.Location = new System.Drawing.Point(406, 0);
            this.btnSaveImageCam0.Margin = new System.Windows.Forms.Padding(0);
            this.btnSaveImageCam0.Name = "btnSaveImageCam0";
            this.btnSaveImageCam0.Size = new System.Drawing.Size(206, 100);
            this.btnSaveImageCam0.TabIndex = 201;
            this.btnSaveImageCam0.Text = "LOAD IMAGE";
            this.btnSaveImageCam0.UseVisualStyleBackColor = false;
            // 
            // btnGrabStopCam0
            // 
            this.btnGrabStopCam0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.btnGrabStopCam0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnGrabStopCam0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGrabStopCam0.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.btnGrabStopCam0.ForeColor = System.Drawing.Color.White;
            this.btnGrabStopCam0.Location = new System.Drawing.Point(203, 0);
            this.btnGrabStopCam0.Margin = new System.Windows.Forms.Padding(0);
            this.btnGrabStopCam0.Name = "btnGrabStopCam0";
            this.btnGrabStopCam0.Size = new System.Drawing.Size(203, 100);
            this.btnGrabStopCam0.TabIndex = 200;
            this.btnGrabStopCam0.Text = "GRAB STOP";
            this.btnGrabStopCam0.UseVisualStyleBackColor = false;
            this.btnGrabStopCam0.Click += new System.EventHandler(this.btnGrabStopCam0_Click);
            // 
            // btnGrabStartCam0
            // 
            this.btnGrabStartCam0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.btnGrabStartCam0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnGrabStartCam0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGrabStartCam0.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.btnGrabStartCam0.ForeColor = System.Drawing.Color.White;
            this.btnGrabStartCam0.Location = new System.Drawing.Point(0, 0);
            this.btnGrabStartCam0.Margin = new System.Windows.Forms.Padding(0);
            this.btnGrabStartCam0.Name = "btnGrabStartCam0";
            this.btnGrabStartCam0.Size = new System.Drawing.Size(203, 100);
            this.btnGrabStartCam0.TabIndex = 199;
            this.btnGrabStartCam0.Text = "GRAB START";
            this.btnGrabStartCam0.UseVisualStyleBackColor = false;
            this.btnGrabStartCam0.Click += new System.EventHandler(this.btnGrabStartCam0_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.tableLayoutPanel2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(612, 429);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(613, 100);
            this.panel5.TabIndex = 7;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.btnSaveImageCam1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnGrabStopCam1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnGrabStartCam1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(613, 100);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // btnSaveImageCam1
            // 
            this.btnSaveImageCam1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.btnSaveImageCam1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSaveImageCam1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveImageCam1.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.btnSaveImageCam1.ForeColor = System.Drawing.Color.White;
            this.btnSaveImageCam1.Location = new System.Drawing.Point(408, 0);
            this.btnSaveImageCam1.Margin = new System.Windows.Forms.Padding(0);
            this.btnSaveImageCam1.Name = "btnSaveImageCam1";
            this.btnSaveImageCam1.Size = new System.Drawing.Size(205, 100);
            this.btnSaveImageCam1.TabIndex = 201;
            this.btnSaveImageCam1.Text = "LOAD IMAGE";
            this.btnSaveImageCam1.UseVisualStyleBackColor = false;
            // 
            // btnGrabStopCam1
            // 
            this.btnGrabStopCam1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.btnGrabStopCam1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnGrabStopCam1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGrabStopCam1.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.btnGrabStopCam1.ForeColor = System.Drawing.Color.White;
            this.btnGrabStopCam1.Location = new System.Drawing.Point(204, 0);
            this.btnGrabStopCam1.Margin = new System.Windows.Forms.Padding(0);
            this.btnGrabStopCam1.Name = "btnGrabStopCam1";
            this.btnGrabStopCam1.Size = new System.Drawing.Size(204, 100);
            this.btnGrabStopCam1.TabIndex = 200;
            this.btnGrabStopCam1.Text = "GRAB STOP";
            this.btnGrabStopCam1.UseVisualStyleBackColor = false;
            this.btnGrabStopCam1.Click += new System.EventHandler(this.btnGrabStopCam1_Click);
            // 
            // btnGrabStartCam1
            // 
            this.btnGrabStartCam1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.btnGrabStartCam1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnGrabStartCam1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGrabStartCam1.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.btnGrabStartCam1.ForeColor = System.Drawing.Color.White;
            this.btnGrabStartCam1.Location = new System.Drawing.Point(0, 0);
            this.btnGrabStartCam1.Margin = new System.Windows.Forms.Padding(0);
            this.btnGrabStartCam1.Name = "btnGrabStartCam1";
            this.btnGrabStartCam1.Size = new System.Drawing.Size(204, 100);
            this.btnGrabStartCam1.TabIndex = 199;
            this.btnGrabStartCam1.Text = "GRAB START";
            this.btnGrabStartCam1.UseVisualStyleBackColor = false;
            this.btnGrabStartCam1.Click += new System.EventHandler(this.btnGrabStartCam1_Click);
            // 
            // pnlCam0Display
            // 
            this.pnlCam0Display.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCam0Display.Location = new System.Drawing.Point(0, 40);
            this.pnlCam0Display.Margin = new System.Windows.Forms.Padding(0);
            this.pnlCam0Display.Name = "pnlCam0Display";
            this.pnlCam0Display.Size = new System.Drawing.Size(612, 389);
            this.pnlCam0Display.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(612, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(613, 40);
            this.panel3.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 13.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(613, 40);
            this.label2.TabIndex = 1;
            this.label2.Text = "Cam1";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(612, 40);
            this.panel2.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 13.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(612, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cam0";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCam1Display
            // 
            this.pnlCam1Display.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCam1Display.Location = new System.Drawing.Point(612, 40);
            this.pnlCam1Display.Margin = new System.Windows.Forms.Padding(0);
            this.pnlCam1Display.Name = "pnlCam1Display";
            this.pnlCam1Display.Size = new System.Drawing.Size(613, 389);
            this.pnlCam1Display.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1225, 529);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.tlpLoadImage.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TableLayoutPanel tlpLoadImage;
        private System.Windows.Forms.Button btnSaveImageCam0;
        private System.Windows.Forms.Button btnGrabStopCam0;
        private System.Windows.Forms.Button btnGrabStartCam0;
        private System.Windows.Forms.Panel pnlCam0Display;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlCam1Display;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnSaveImageCam1;
        private System.Windows.Forms.Button btnGrabStopCam1;
        private System.Windows.Forms.Button btnGrabStartCam1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

