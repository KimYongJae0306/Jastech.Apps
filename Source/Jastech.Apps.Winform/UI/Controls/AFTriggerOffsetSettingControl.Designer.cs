namespace Jastech.Apps.Winform.UI.Controls
{
    partial class AFTriggerOffsetSettingControl
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
            this.pnlPosition = new System.Windows.Forms.Panel();
            this.lblOffset = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblRight = new System.Windows.Forms.Label();
            this.lblParameter = new System.Windows.Forms.Label();
            this.tlpPattern = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblLeftOffset = new System.Windows.Forms.Label();
            this.lblRightOffset = new System.Windows.Forms.Label();
            this.pnlPosition.SuspendLayout();
            this.tlpPattern.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPosition
            // 
            this.pnlPosition.Controls.Add(this.lblRightOffset);
            this.pnlPosition.Controls.Add(this.lblLeftOffset);
            this.pnlPosition.Controls.Add(this.label2);
            this.pnlPosition.Controls.Add(this.label1);
            this.pnlPosition.Controls.Add(this.lblRight);
            this.pnlPosition.Controls.Add(this.label8);
            this.pnlPosition.Controls.Add(this.lblOffset);
            this.pnlPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPosition.Location = new System.Drawing.Point(3, 35);
            this.pnlPosition.Name = "pnlPosition";
            this.pnlPosition.Size = new System.Drawing.Size(867, 700);
            this.pnlPosition.TabIndex = 7;
            // 
            // lblOffset
            // 
            this.lblOffset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblOffset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOffset.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblOffset.Location = new System.Drawing.Point(7, 12);
            this.lblOffset.Margin = new System.Windows.Forms.Padding(0);
            this.lblOffset.Name = "lblOffset";
            this.lblOffset.Size = new System.Drawing.Size(263, 40);
            this.lblOffset.TabIndex = 0;
            this.lblOffset.Text = "Offset";
            this.lblOffset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.label8.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(7, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 33);
            this.label8.TabIndex = 9;
            this.label8.Text = "Left";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRight
            // 
            this.lblRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblRight.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblRight.ForeColor = System.Drawing.Color.White;
            this.lblRight.Location = new System.Drawing.Point(7, 111);
            this.lblRight.Name = "lblRight";
            this.lblRight.Size = new System.Drawing.Size(119, 33);
            this.lblRight.TabIndex = 11;
            this.lblRight.Text = "Right";
            this.lblRight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblParameter
            // 
            this.lblParameter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblParameter.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblParameter.Location = new System.Drawing.Point(0, 0);
            this.lblParameter.Margin = new System.Windows.Forms.Padding(0);
            this.lblParameter.Name = "lblParameter";
            this.lblParameter.Size = new System.Drawing.Size(873, 32);
            this.lblParameter.TabIndex = 0;
            this.lblParameter.Text = "Parameter";
            this.lblParameter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpPattern
            // 
            this.tlpPattern.ColumnCount = 1;
            this.tlpPattern.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPattern.Controls.Add(this.lblParameter, 0, 0);
            this.tlpPattern.Controls.Add(this.pnlPosition, 0, 1);
            this.tlpPattern.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPattern.Location = new System.Drawing.Point(0, 0);
            this.tlpPattern.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tlpPattern.Name = "tlpPattern";
            this.tlpPattern.RowCount = 2;
            this.tlpPattern.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpPattern.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tlpPattern.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpPattern.Size = new System.Drawing.Size(873, 738);
            this.tlpPattern.TabIndex = 75;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 10.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(230, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 47);
            this.label1.TabIndex = 76;
            this.label1.Text = "mm";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 10.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(230, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 47);
            this.label2.TabIndex = 78;
            this.label2.Text = "mm";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLeftOffset
            // 
            this.lblLeftOffset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblLeftOffset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeftOffset.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblLeftOffset.ForeColor = System.Drawing.Color.White;
            this.lblLeftOffset.Location = new System.Drawing.Point(147, 69);
            this.lblLeftOffset.Margin = new System.Windows.Forms.Padding(0);
            this.lblLeftOffset.Name = "lblLeftOffset";
            this.lblLeftOffset.Size = new System.Drawing.Size(80, 33);
            this.lblLeftOffset.TabIndex = 80;
            this.lblLeftOffset.Text = "0";
            this.lblLeftOffset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLeftOffset.Click += new System.EventHandler(this.lblLeftOffset_Click);
            // 
            // lblRightOffset
            // 
            this.lblRightOffset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblRightOffset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRightOffset.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblRightOffset.ForeColor = System.Drawing.Color.White;
            this.lblRightOffset.Location = new System.Drawing.Point(147, 111);
            this.lblRightOffset.Margin = new System.Windows.Forms.Padding(0);
            this.lblRightOffset.Name = "lblRightOffset";
            this.lblRightOffset.Size = new System.Drawing.Size(80, 33);
            this.lblRightOffset.TabIndex = 81;
            this.lblRightOffset.Text = "0";
            this.lblRightOffset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRightOffset.Click += new System.EventHandler(this.lblRightOffset_Click);
            // 
            // AFTriggerOffsetSettingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpPattern);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "AFTriggerOffsetSettingControl";
            this.Size = new System.Drawing.Size(873, 738);
            this.Load += new System.EventHandler(this.AFTriggerOffsetSettingControl_Load);
            this.pnlPosition.ResumeLayout(false);
            this.tlpPattern.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlPosition;
        private System.Windows.Forms.Label lblRight;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblOffset;
        private System.Windows.Forms.Label lblParameter;
        private System.Windows.Forms.TableLayoutPanel tlpPattern;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRightOffset;
        private System.Windows.Forms.Label lblLeftOffset;
    }
}
