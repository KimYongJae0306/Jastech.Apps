namespace Jastech.Apps.Winform.UI.Pages
{
    partial class LogPage
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
            this.tlpLogPage = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLogPage = new System.Windows.Forms.Panel();
            this.pnlLogItem = new System.Windows.Forms.Panel();
            this.tlpLogItem = new System.Windows.Forms.TableLayoutPanel();
            this.btnAlarm = new System.Windows.Forms.Button();
            this.btnOperation = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tlpLogPage.SuspendLayout();
            this.pnlLogPage.SuspendLayout();
            this.pnlLogItem.SuspendLayout();
            this.tlpLogItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpLogPage
            // 
            this.tlpLogPage.ColumnCount = 2;
            this.tlpLogPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLogPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpLogPage.Controls.Add(this.pnlLogPage, 0, 0);
            this.tlpLogPage.Controls.Add(this.pnlLogItem, 1, 0);
            this.tlpLogPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLogPage.Location = new System.Drawing.Point(0, 0);
            this.tlpLogPage.Margin = new System.Windows.Forms.Padding(0);
            this.tlpLogPage.Name = "tlpLogPage";
            this.tlpLogPage.RowCount = 1;
            this.tlpLogPage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLogPage.Size = new System.Drawing.Size(600, 600);
            this.tlpLogPage.TabIndex = 2;
            // 
            // pnlLogPage
            // 
            this.pnlLogPage.Controls.Add(this.label1);
            this.pnlLogPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLogPage.Location = new System.Drawing.Point(0, 0);
            this.pnlLogPage.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLogPage.Name = "pnlLogPage";
            this.pnlLogPage.Size = new System.Drawing.Size(440, 600);
            this.pnlLogPage.TabIndex = 0;
            // 
            // pnlLogItem
            // 
            this.pnlLogItem.Controls.Add(this.tlpLogItem);
            this.pnlLogItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLogItem.Location = new System.Drawing.Point(440, 0);
            this.pnlLogItem.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLogItem.Name = "pnlLogItem";
            this.pnlLogItem.Size = new System.Drawing.Size(160, 600);
            this.pnlLogItem.TabIndex = 1;
            // 
            // tlpLogItem
            // 
            this.tlpLogItem.ColumnCount = 1;
            this.tlpLogItem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLogItem.Controls.Add(this.btnAlarm, 0, 1);
            this.tlpLogItem.Controls.Add(this.btnOperation, 0, 0);
            this.tlpLogItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLogItem.Location = new System.Drawing.Point(0, 0);
            this.tlpLogItem.Margin = new System.Windows.Forms.Padding(0);
            this.tlpLogItem.Name = "tlpLogItem";
            this.tlpLogItem.RowCount = 6;
            this.tlpLogItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpLogItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpLogItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpLogItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpLogItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLogItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpLogItem.Size = new System.Drawing.Size(160, 600);
            this.tlpLogItem.TabIndex = 0;
            // 
            // btnAlarm
            // 
            this.btnAlarm.BackColor = System.Drawing.Color.White;
            this.btnAlarm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAlarm.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btnAlarm.ForeColor = System.Drawing.Color.Black;
            this.btnAlarm.Location = new System.Drawing.Point(3, 103);
            this.btnAlarm.Name = "btnAlarm";
            this.btnAlarm.Size = new System.Drawing.Size(154, 94);
            this.btnAlarm.TabIndex = 12;
            this.btnAlarm.Text = "Alarm";
            this.btnAlarm.UseVisualStyleBackColor = false;
            // 
            // btnOperation
            // 
            this.btnOperation.BackColor = System.Drawing.Color.White;
            this.btnOperation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOperation.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btnOperation.ForeColor = System.Drawing.Color.Black;
            this.btnOperation.Location = new System.Drawing.Point(3, 3);
            this.btnOperation.Name = "btnOperation";
            this.btnOperation.Size = new System.Drawing.Size(154, 94);
            this.btnOperation.TabIndex = 11;
            this.btnOperation.Text = "OP";
            this.btnOperation.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(184, 290);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "LogPage";
            // 
            // LogPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tlpLogPage);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.Name = "LogPage";
            this.Size = new System.Drawing.Size(600, 600);
            this.tlpLogPage.ResumeLayout(false);
            this.pnlLogPage.ResumeLayout(false);
            this.pnlLogPage.PerformLayout();
            this.pnlLogItem.ResumeLayout(false);
            this.tlpLogItem.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpLogPage;
        private System.Windows.Forms.Panel pnlLogPage;
        private System.Windows.Forms.Panel pnlLogItem;
        private System.Windows.Forms.TableLayoutPanel tlpLogItem;
        private System.Windows.Forms.Button btnAlarm;
        private System.Windows.Forms.Button btnOperation;
        private System.Windows.Forms.Label label1;
    }
}
