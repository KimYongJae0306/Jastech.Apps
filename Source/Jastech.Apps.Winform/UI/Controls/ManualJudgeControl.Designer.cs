namespace Jastech.Apps.Winform.UI.Controls
{
    partial class ManualJudgeControl
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
            this.tlpManualJudge = new System.Windows.Forms.TableLayoutPanel();
            this.lblTab = new System.Windows.Forms.Label();
            this.lblAlign = new System.Windows.Forms.Label();
            this.lblAkkon = new System.Windows.Forms.Label();
            this.tlpManualJudge.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpManualJudge
            // 
            this.tlpManualJudge.ColumnCount = 3;
            this.tlpManualJudge.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpManualJudge.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpManualJudge.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpManualJudge.Controls.Add(this.lblAkkon, 0, 0);
            this.tlpManualJudge.Controls.Add(this.lblAlign, 0, 0);
            this.tlpManualJudge.Controls.Add(this.lblTab, 0, 0);
            this.tlpManualJudge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpManualJudge.Location = new System.Drawing.Point(0, 0);
            this.tlpManualJudge.Margin = new System.Windows.Forms.Padding(0);
            this.tlpManualJudge.Name = "tlpManualJudge";
            this.tlpManualJudge.RowCount = 1;
            this.tlpManualJudge.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpManualJudge.Size = new System.Drawing.Size(443, 92);
            this.tlpManualJudge.TabIndex = 0;
            // 
            // lblTab
            // 
            this.lblTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTab.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblTab.ForeColor = System.Drawing.Color.White;
            this.lblTab.Location = new System.Drawing.Point(0, 0);
            this.lblTab.Margin = new System.Windows.Forms.Padding(0);
            this.lblTab.Name = "lblTab";
            this.lblTab.Size = new System.Drawing.Size(147, 92);
            this.lblTab.TabIndex = 1;
            this.lblTab.Text = "Tab";
            this.lblTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAlign
            // 
            this.lblAlign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAlign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAlign.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblAlign.ForeColor = System.Drawing.Color.Black;
            this.lblAlign.Location = new System.Drawing.Point(147, 0);
            this.lblAlign.Margin = new System.Windows.Forms.Padding(0);
            this.lblAlign.Name = "lblAlign";
            this.lblAlign.Size = new System.Drawing.Size(147, 92);
            this.lblAlign.TabIndex = 2;
            this.lblAlign.Text = "Align";
            this.lblAlign.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAkkon
            // 
            this.lblAkkon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAkkon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAkkon.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblAkkon.ForeColor = System.Drawing.Color.Black;
            this.lblAkkon.Location = new System.Drawing.Point(294, 0);
            this.lblAkkon.Margin = new System.Windows.Forms.Padding(0);
            this.lblAkkon.Name = "lblAkkon";
            this.lblAkkon.Size = new System.Drawing.Size(149, 92);
            this.lblAkkon.TabIndex = 3;
            this.lblAkkon.Text = "Akkon";
            this.lblAkkon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ManualJudgeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpManualJudge);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ManualJudgeControl";
            this.Size = new System.Drawing.Size(443, 92);
            this.Load += new System.EventHandler(this.ManualJudgeControl_Load);
            this.tlpManualJudge.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpManualJudge;
        private System.Windows.Forms.Label lblTab;
        private System.Windows.Forms.Label lblAkkon;
        private System.Windows.Forms.Label lblAlign;
    }
}
