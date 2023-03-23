namespace ATT.UI.Pages
{
    partial class InspPage
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
            this.tlpInspPage = new System.Windows.Forms.TableLayoutPanel();
            this.pnlInspPage = new System.Windows.Forms.Panel();
            this.pnlInspItem = new System.Windows.Forms.Panel();
            this.tlpInspItem = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tlpInspPage.SuspendLayout();
            this.pnlInspPage.SuspendLayout();
            this.pnlInspItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpInspPage
            // 
            this.tlpInspPage.ColumnCount = 2;
            this.tlpInspPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInspPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpInspPage.Controls.Add(this.pnlInspPage, 0, 0);
            this.tlpInspPage.Controls.Add(this.pnlInspItem, 1, 0);
            this.tlpInspPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpInspPage.Location = new System.Drawing.Point(0, 0);
            this.tlpInspPage.Margin = new System.Windows.Forms.Padding(0);
            this.tlpInspPage.Name = "tlpInspPage";
            this.tlpInspPage.RowCount = 1;
            this.tlpInspPage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInspPage.Size = new System.Drawing.Size(600, 600);
            this.tlpInspPage.TabIndex = 1;
            // 
            // pnlInspPage
            // 
            this.pnlInspPage.Controls.Add(this.label1);
            this.pnlInspPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInspPage.Location = new System.Drawing.Point(0, 0);
            this.pnlInspPage.Margin = new System.Windows.Forms.Padding(0);
            this.pnlInspPage.Name = "pnlInspPage";
            this.pnlInspPage.Size = new System.Drawing.Size(440, 600);
            this.pnlInspPage.TabIndex = 0;
            // 
            // pnlInspItem
            // 
            this.pnlInspItem.Controls.Add(this.tlpInspItem);
            this.pnlInspItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInspItem.Location = new System.Drawing.Point(440, 0);
            this.pnlInspItem.Margin = new System.Windows.Forms.Padding(0);
            this.pnlInspItem.Name = "pnlInspItem";
            this.pnlInspItem.Size = new System.Drawing.Size(160, 600);
            this.pnlInspItem.TabIndex = 1;
            // 
            // tlpInspItem
            // 
            this.tlpInspItem.ColumnCount = 1;
            this.tlpInspItem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInspItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpInspItem.Location = new System.Drawing.Point(0, 0);
            this.tlpInspItem.Margin = new System.Windows.Forms.Padding(0);
            this.tlpInspItem.Name = "tlpInspItem";
            this.tlpInspItem.RowCount = 6;
            this.tlpInspItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpInspItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpInspItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpInspItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpInspItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInspItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpInspItem.Size = new System.Drawing.Size(160, 600);
            this.tlpInspItem.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(175, 290);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "InspPage";
            // 
            // InspPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tlpInspPage);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.Name = "InspPage";
            this.Size = new System.Drawing.Size(600, 600);
            this.tlpInspPage.ResumeLayout(false);
            this.pnlInspPage.ResumeLayout(false);
            this.pnlInspPage.PerformLayout();
            this.pnlInspItem.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpInspPage;
        private System.Windows.Forms.Panel pnlInspPage;
        private System.Windows.Forms.Panel pnlInspItem;
        private System.Windows.Forms.TableLayoutPanel tlpInspItem;
        private System.Windows.Forms.Label label1;
    }
}
