namespace ATT_UT_IPAD.UI.Controls
{
    partial class AkkonResultDisplayControl
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
            this.tlpAkkonResultDisplay = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTabButton = new System.Windows.Forms.Panel();
            this.btnResultImage = new System.Windows.Forms.Button();
            this.btnResizeImage = new System.Windows.Forms.Button();
            this.pnlInspDisplay = new System.Windows.Forms.Panel();
            this.tlpAkkonResultDisplay.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAkkonResultDisplay
            // 
            this.tlpAkkonResultDisplay.ColumnCount = 1;
            this.tlpAkkonResultDisplay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAkkonResultDisplay.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlpAkkonResultDisplay.Controls.Add(this.pnlInspDisplay, 0, 1);
            this.tlpAkkonResultDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAkkonResultDisplay.Location = new System.Drawing.Point(0, 0);
            this.tlpAkkonResultDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAkkonResultDisplay.Name = "tlpAkkonResultDisplay";
            this.tlpAkkonResultDisplay.RowCount = 2;
            this.tlpAkkonResultDisplay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpAkkonResultDisplay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAkkonResultDisplay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpAkkonResultDisplay.Size = new System.Drawing.Size(600, 300);
            this.tlpAkkonResultDisplay.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.Controls.Add(this.pnlTabButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnResultImage, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnResizeImage, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(600, 40);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pnlTabButton
            // 
            this.pnlTabButton.AutoScroll = true;
            this.pnlTabButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTabButton.Location = new System.Drawing.Point(0, 0);
            this.pnlTabButton.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTabButton.Name = "pnlTabButton";
            this.pnlTabButton.Size = new System.Drawing.Size(460, 40);
            this.pnlTabButton.TabIndex = 3;
            // 
            // btnResultImage
            // 
            this.btnResultImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnResultImage.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnResultImage.ForeColor = System.Drawing.Color.White;
            this.btnResultImage.Location = new System.Drawing.Point(530, 0);
            this.btnResultImage.Margin = new System.Windows.Forms.Padding(0);
            this.btnResultImage.Name = "btnResultImage";
            this.btnResultImage.Size = new System.Drawing.Size(70, 40);
            this.btnResultImage.TabIndex = 2;
            this.btnResultImage.Text = "R";
            this.btnResultImage.UseVisualStyleBackColor = false;
            this.btnResultImage.Click += new System.EventHandler(this.btnResultImage_Click);
            // 
            // btnResizeImage
            // 
            this.btnResizeImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnResizeImage.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnResizeImage.ForeColor = System.Drawing.Color.White;
            this.btnResizeImage.Location = new System.Drawing.Point(460, 0);
            this.btnResizeImage.Margin = new System.Windows.Forms.Padding(0);
            this.btnResizeImage.Name = "btnResizeImage";
            this.btnResizeImage.Size = new System.Drawing.Size(70, 40);
            this.btnResizeImage.TabIndex = 1;
            this.btnResizeImage.Text = "S";
            this.btnResizeImage.UseVisualStyleBackColor = false;
            this.btnResizeImage.Click += new System.EventHandler(this.btnResizeImage_Click);
            // 
            // pnlInspDisplay
            // 
            this.pnlInspDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInspDisplay.Location = new System.Drawing.Point(0, 40);
            this.pnlInspDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.pnlInspDisplay.Name = "pnlInspDisplay";
            this.pnlInspDisplay.Size = new System.Drawing.Size(600, 260);
            this.pnlInspDisplay.TabIndex = 2;
            // 
            // AkkonResultDisplayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpAkkonResultDisplay);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "AkkonResultDisplayControl";
            this.Size = new System.Drawing.Size(600, 300);
            this.Load += new System.EventHandler(this.AkkonResultDisplayControl_Load);
            this.tlpAkkonResultDisplay.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAkkonResultDisplay;
        private System.Windows.Forms.Panel pnlInspDisplay;
        private System.Windows.Forms.Panel pnlTabButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnResultImage;
        private System.Windows.Forms.Button btnResizeImage;
    }
}
