namespace YSHTest
{
    partial class Form1
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
            this.pnlPage = new System.Windows.Forms.Panel();
            this.btnImageLoad = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnShowROI = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pnlPage
            // 
            this.pnlPage.Location = new System.Drawing.Point(12, 12);
            this.pnlPage.Name = "pnlPage";
            this.pnlPage.Size = new System.Drawing.Size(786, 696);
            this.pnlPage.TabIndex = 0;
            // 
            // btnImageLoad
            // 
            this.btnImageLoad.Location = new System.Drawing.Point(865, 12);
            this.btnImageLoad.Name = "btnImageLoad";
            this.btnImageLoad.Size = new System.Drawing.Size(107, 53);
            this.btnImageLoad.TabIndex = 1;
            this.btnImageLoad.Text = "Image Load";
            this.btnImageLoad.UseVisualStyleBackColor = true;
            this.btnImageLoad.Click += new System.EventHandler(this.btnImageLoad_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(865, 199);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(107, 53);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnShowROI
            // 
            this.btnShowROI.Location = new System.Drawing.Point(865, 104);
            this.btnShowROI.Name = "btnShowROI";
            this.btnShowROI.Size = new System.Drawing.Size(107, 53);
            this.btnShowROI.TabIndex = 3;
            this.btnShowROI.Text = "Show ROI";
            this.btnShowROI.UseVisualStyleBackColor = true;
            this.btnShowROI.Click += new System.EventHandler(this.btnShowROI_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 720);
            this.Controls.Add(this.btnShowROI);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnImageLoad);
            this.Controls.Add(this.pnlPage);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPage;
        private System.Windows.Forms.Button btnImageLoad;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnShowROI;
    }
}

