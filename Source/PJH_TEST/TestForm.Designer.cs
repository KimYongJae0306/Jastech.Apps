namespace PJH_TEST
{
    partial class Form
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
            this.button1 = new System.Windows.Forms.Button();
            this.btnAlignTest = new System.Windows.Forms.Button();
            this.btnAkkonTest = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(418, 235);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(335, 232);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAlignTest
            // 
            this.btnAlignTest.Location = new System.Drawing.Point(12, 27);
            this.btnAlignTest.Name = "btnAlignTest";
            this.btnAlignTest.Size = new System.Drawing.Size(103, 75);
            this.btnAlignTest.TabIndex = 1;
            this.btnAlignTest.Text = "Align";
            this.btnAlignTest.UseVisualStyleBackColor = true;
            this.btnAlignTest.Click += new System.EventHandler(this.btnAlignTest_Click);
            // 
            // btnAkkonTest
            // 
            this.btnAkkonTest.Location = new System.Drawing.Point(12, 108);
            this.btnAkkonTest.Name = "btnAkkonTest";
            this.btnAkkonTest.Size = new System.Drawing.Size(103, 75);
            this.btnAkkonTest.TabIndex = 2;
            this.btnAkkonTest.Text = "Akkon";
            this.btnAkkonTest.UseVisualStyleBackColor = true;
            this.btnAkkonTest.Click += new System.EventHandler(this.btnAkkonTest_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(121, 27);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(103, 156);
            this.btnTest.TabIndex = 3;
            this.btnTest.Text = "TT";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1029, 750);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnAkkonTest);
            this.Controls.Add(this.btnAlignTest);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnAlignTest;
        private System.Windows.Forms.Button btnAkkonTest;
        private System.Windows.Forms.Button btnTest;
    }
}

