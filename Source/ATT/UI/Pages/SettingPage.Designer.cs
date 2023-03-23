namespace ATT.UI.Pages
{
    partial class SettingPage
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
            this.tlpSettingPage = new System.Windows.Forms.TableLayoutPanel();
            this.pnlSettingPage = new System.Windows.Forms.Panel();
            this.pnlSettingItem = new System.Windows.Forms.Panel();
            this.tlpSettingItem = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tlpSettingPage.SuspendLayout();
            this.pnlSettingPage.SuspendLayout();
            this.pnlSettingItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpSettingPage
            // 
            this.tlpSettingPage.ColumnCount = 2;
            this.tlpSettingPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSettingPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpSettingPage.Controls.Add(this.pnlSettingPage, 0, 0);
            this.tlpSettingPage.Controls.Add(this.pnlSettingItem, 1, 0);
            this.tlpSettingPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSettingPage.Location = new System.Drawing.Point(0, 0);
            this.tlpSettingPage.Margin = new System.Windows.Forms.Padding(0);
            this.tlpSettingPage.Name = "tlpSettingPage";
            this.tlpSettingPage.RowCount = 1;
            this.tlpSettingPage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSettingPage.Size = new System.Drawing.Size(600, 600);
            this.tlpSettingPage.TabIndex = 1;
            // 
            // pnlSettingPage
            // 
            this.pnlSettingPage.Controls.Add(this.label1);
            this.pnlSettingPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSettingPage.Location = new System.Drawing.Point(0, 0);
            this.pnlSettingPage.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSettingPage.Name = "pnlSettingPage";
            this.pnlSettingPage.Size = new System.Drawing.Size(440, 600);
            this.pnlSettingPage.TabIndex = 0;
            // 
            // pnlSettingItem
            // 
            this.pnlSettingItem.Controls.Add(this.tlpSettingItem);
            this.pnlSettingItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSettingItem.Location = new System.Drawing.Point(440, 0);
            this.pnlSettingItem.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSettingItem.Name = "pnlSettingItem";
            this.pnlSettingItem.Size = new System.Drawing.Size(160, 600);
            this.pnlSettingItem.TabIndex = 1;
            // 
            // tlpSettingItem
            // 
            this.tlpSettingItem.ColumnCount = 1;
            this.tlpSettingItem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSettingItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSettingItem.Location = new System.Drawing.Point(0, 0);
            this.tlpSettingItem.Margin = new System.Windows.Forms.Padding(0);
            this.tlpSettingItem.Name = "tlpSettingItem";
            this.tlpSettingItem.RowCount = 6;
            this.tlpSettingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpSettingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpSettingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpSettingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpSettingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSettingItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpSettingItem.Size = new System.Drawing.Size(160, 600);
            this.tlpSettingItem.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(176, 290);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "SettingPage";
            // 
            // SettingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tlpSettingPage);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.Name = "SettingPage";
            this.Size = new System.Drawing.Size(600, 600);
            this.tlpSettingPage.ResumeLayout(false);
            this.pnlSettingPage.ResumeLayout(false);
            this.pnlSettingPage.PerformLayout();
            this.pnlSettingItem.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpSettingPage;
        private System.Windows.Forms.Panel pnlSettingPage;
        private System.Windows.Forms.Panel pnlSettingItem;
        private System.Windows.Forms.TableLayoutPanel tlpSettingItem;
        private System.Windows.Forms.Label label1;
    }
}
