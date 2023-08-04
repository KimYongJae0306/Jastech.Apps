namespace AkkonTester.UI.Pages
{
    partial class OriginalPage
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
            this.pnlResultImage = new System.Windows.Forms.Panel();
            this.pnlOriginalImage = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.txtResolution = new System.Windows.Forms.TextBox();
            this.btnInspection = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtResizeRatio = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLoadRoiData = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAkkonRoiOffsetY = new System.Windows.Forms.TextBox();
            this.txtAkkonRoiOffsetX = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnMakeSliceImage = new System.Windows.Forms.Button();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblAkkonViewer = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 40);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1763, 823);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.pnlResultImage, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.pnlOriginalImage, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1363, 823);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // pnlResultImage
            // 
            this.pnlResultImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResultImage.Location = new System.Drawing.Point(0, 411);
            this.pnlResultImage.Margin = new System.Windows.Forms.Padding(0);
            this.pnlResultImage.Name = "pnlResultImage";
            this.pnlResultImage.Size = new System.Drawing.Size(1363, 412);
            this.pnlResultImage.TabIndex = 2;
            // 
            // pnlOriginalImage
            // 
            this.pnlOriginalImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOriginalImage.Location = new System.Drawing.Point(0, 0);
            this.pnlOriginalImage.Margin = new System.Windows.Forms.Padding(0);
            this.pnlOriginalImage.Name = "pnlOriginalImage";
            this.pnlOriginalImage.Size = new System.Drawing.Size(1363, 411);
            this.pnlOriginalImage.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtResolution);
            this.panel2.Controls.Add(this.btnInspection);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtResizeRatio);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnLoadRoiData);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtAkkonRoiOffsetY);
            this.panel2.Controls.Add(this.txtAkkonRoiOffsetX);
            this.panel2.Controls.Add(this.lblMessage);
            this.panel2.Controls.Add(this.btnMakeSliceImage);
            this.panel2.Controls.Add(this.btnLoadImage);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1366, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(394, 817);
            this.panel2.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(9, 553);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 33);
            this.label7.TabIndex = 21;
            this.label7.Text = "Resolution";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtResolution
            // 
            this.txtResolution.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtResolution.Location = new System.Drawing.Point(115, 558);
            this.txtResolution.Name = "txtResolution";
            this.txtResolution.Size = new System.Drawing.Size(100, 25);
            this.txtResolution.TabIndex = 20;
            this.txtResolution.Text = "0.35";
            this.txtResolution.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnInspection
            // 
            this.btnInspection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.btnInspection.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnInspection.ForeColor = System.Drawing.Color.White;
            this.btnInspection.Location = new System.Drawing.Point(19, 12);
            this.btnInspection.Name = "btnInspection";
            this.btnInspection.Size = new System.Drawing.Size(134, 43);
            this.btnInspection.TabIndex = 19;
            this.btnInspection.Text = "Inspection";
            this.btnInspection.UseVisualStyleBackColor = false;
            this.btnInspection.Click += new System.EventHandler(this.btnInspection_Click);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(9, 510);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 33);
            this.label6.TabIndex = 18;
            this.label6.Text = "Enhance";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtResizeRatio
            // 
            this.txtResizeRatio.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtResizeRatio.Location = new System.Drawing.Point(115, 515);
            this.txtResizeRatio.Name = "txtResizeRatio";
            this.txtResizeRatio.Size = new System.Drawing.Size(100, 25);
            this.txtResizeRatio.TabIndex = 17;
            this.txtResizeRatio.Text = "0.5";
            this.txtResizeRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtResizeRatio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtResizeRatio_KeyPress);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel4.Location = new System.Drawing.Point(6, 490);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(370, 2);
            this.panel4.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(8, 457);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(357, 33);
            this.label5.TabIndex = 15;
            this.label5.Text = "Slice";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(15, 388);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 33);
            this.label4.TabIndex = 14;
            this.label4.Text = "OffsetY";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(15, 349);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 33);
            this.label3.TabIndex = 13;
            this.label3.Text = "OffsetX";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnLoadRoiData
            // 
            this.btnLoadRoiData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.btnLoadRoiData.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnLoadRoiData.ForeColor = System.Drawing.Color.White;
            this.btnLoadRoiData.Location = new System.Drawing.Point(208, 354);
            this.btnLoadRoiData.Name = "btnLoadRoiData";
            this.btnLoadRoiData.Size = new System.Drawing.Size(148, 64);
            this.btnLoadRoiData.TabIndex = 12;
            this.btnLoadRoiData.Text = "Load ROI Data";
            this.btnLoadRoiData.UseVisualStyleBackColor = false;
            this.btnLoadRoiData.Click += new System.EventHandler(this.btnLoadRoiData_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel3.Location = new System.Drawing.Point(12, 329);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(370, 2);
            this.panel3.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(14, 296);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(357, 33);
            this.label2.TabIndex = 10;
            this.label2.Text = "ROI Data";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel1.Location = new System.Drawing.Point(12, 220);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(370, 2);
            this.panel1.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(14, 187);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(357, 33);
            this.label1.TabIndex = 8;
            this.label1.Text = "Image";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel7.Location = new System.Drawing.Point(12, 105);
            this.panel7.Margin = new System.Windows.Forms.Padding(0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(370, 2);
            this.panel7.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(14, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(357, 33);
            this.label8.TabIndex = 5;
            this.label8.Text = "Message";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAkkonRoiOffsetY
            // 
            this.txtAkkonRoiOffsetY.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtAkkonRoiOffsetY.Location = new System.Drawing.Point(98, 393);
            this.txtAkkonRoiOffsetY.Name = "txtAkkonRoiOffsetY";
            this.txtAkkonRoiOffsetY.Size = new System.Drawing.Size(100, 25);
            this.txtAkkonRoiOffsetY.TabIndex = 4;
            this.txtAkkonRoiOffsetY.Text = "0";
            this.txtAkkonRoiOffsetY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAkkonRoiOffsetY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAkkonRoiOffset_KeyPress);
            // 
            // txtAkkonRoiOffsetX
            // 
            this.txtAkkonRoiOffsetX.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtAkkonRoiOffsetX.Location = new System.Drawing.Point(98, 354);
            this.txtAkkonRoiOffsetX.Name = "txtAkkonRoiOffsetX";
            this.txtAkkonRoiOffsetX.Size = new System.Drawing.Size(100, 25);
            this.txtAkkonRoiOffsetX.TabIndex = 3;
            this.txtAkkonRoiOffsetX.Text = "0";
            this.txtAkkonRoiOffsetX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAkkonRoiOffsetX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAkkonRoiOffset_KeyPress);
            // 
            // lblMessage
            // 
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMessage.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(12, 113);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(370, 39);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "Akkon ROI Data is not exist.";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnMakeSliceImage
            // 
            this.btnMakeSliceImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.btnMakeSliceImage.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnMakeSliceImage.ForeColor = System.Drawing.Color.White;
            this.btnMakeSliceImage.Location = new System.Drawing.Point(231, 505);
            this.btnMakeSliceImage.Name = "btnMakeSliceImage";
            this.btnMakeSliceImage.Size = new System.Drawing.Size(134, 43);
            this.btnMakeSliceImage.TabIndex = 1;
            this.btnMakeSliceImage.Text = "Make Slice Image";
            this.btnMakeSliceImage.UseVisualStyleBackColor = false;
            this.btnMakeSliceImage.Click += new System.EventHandler(this.btnMakeSliceImage_Click);
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.btnLoadImage.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnLoadImage.ForeColor = System.Drawing.Color.White;
            this.btnLoadImage.Location = new System.Drawing.Point(12, 225);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(134, 43);
            this.btnLoadImage.TabIndex = 0;
            this.btnLoadImage.Text = "Load Image";
            this.btnLoadImage.UseVisualStyleBackColor = false;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.lblAkkonViewer, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1763, 863);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // lblAkkonViewer
            // 
            this.lblAkkonViewer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblAkkonViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAkkonViewer.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblAkkonViewer.ForeColor = System.Drawing.Color.White;
            this.lblAkkonViewer.Location = new System.Drawing.Point(0, 0);
            this.lblAkkonViewer.Margin = new System.Windows.Forms.Padding(0);
            this.lblAkkonViewer.Name = "lblAkkonViewer";
            this.lblAkkonViewer.Size = new System.Drawing.Size(1763, 40);
            this.lblAkkonViewer.TabIndex = 4;
            this.lblAkkonViewer.Text = "Original Akkon Inspection";
            this.lblAkkonViewer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OriginalPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tableLayoutPanel3);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "OriginalPage";
            this.Size = new System.Drawing.Size(1763, 863);
            this.Load += new System.EventHandler(this.OriginalPage_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel pnlResultImage;
        private System.Windows.Forms.Panel pnlOriginalImage;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.Button btnMakeSliceImage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblAkkonViewer;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtAkkonRoiOffsetX;
        private System.Windows.Forms.TextBox txtAkkonRoiOffsetY;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLoadRoiData;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtResizeRatio;
        private System.Windows.Forms.Button btnInspection;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtResolution;
    }
}
