namespace Jastech.Apps.Winform.UI.Controls
{
    partial class AlignMonitoringControl
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
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpAlignViewer = new System.Windows.Forms.TableLayoutPanel();
            this.pnlResultDisplay = new System.Windows.Forms.Panel();
            this.lblAlignViewer = new System.Windows.Forms.Label();
            this.pnlTab1 = new System.Windows.Forms.Panel();
            this.pnlTab2 = new System.Windows.Forms.Panel();
            this.pnlTab3 = new System.Windows.Forms.Panel();
            this.pnlTab4 = new System.Windows.Forms.Panel();
            this.pnlTab0 = new System.Windows.Forms.Panel();
            this.pnlTab5 = new System.Windows.Forms.Panel();
            this.pnlTab6 = new System.Windows.Forms.Panel();
            this.pnlTab7 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel11.SuspendLayout();
            this.tlpAlignViewer.SuspendLayout();
            this.pnlResultDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 2;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.Controls.Add(this.pnlTab0, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.pnlTab7, 1, 3);
            this.tableLayoutPanel11.Controls.Add(this.pnlTab3, 1, 1);
            this.tableLayoutPanel11.Controls.Add(this.pnlTab6, 0, 3);
            this.tableLayoutPanel11.Controls.Add(this.pnlTab1, 1, 0);
            this.tableLayoutPanel11.Controls.Add(this.pnlTab5, 1, 2);
            this.tableLayoutPanel11.Controls.Add(this.pnlTab2, 0, 1);
            this.tableLayoutPanel11.Controls.Add(this.pnlTab4, 0, 2);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel11.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 4;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(1365, 820);
            this.tableLayoutPanel11.TabIndex = 1;
            // 
            // tlpAlignViewer
            // 
            this.tlpAlignViewer.ColumnCount = 1;
            this.tlpAlignViewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlignViewer.Controls.Add(this.pnlResultDisplay, 0, 1);
            this.tlpAlignViewer.Controls.Add(this.lblAlignViewer, 0, 0);
            this.tlpAlignViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAlignViewer.Location = new System.Drawing.Point(0, 0);
            this.tlpAlignViewer.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAlignViewer.Name = "tlpAlignViewer";
            this.tlpAlignViewer.RowCount = 2;
            this.tlpAlignViewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpAlignViewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlignViewer.Size = new System.Drawing.Size(1365, 860);
            this.tlpAlignViewer.TabIndex = 2;
            // 
            // pnlResultDisplay
            // 
            this.pnlResultDisplay.Controls.Add(this.tableLayoutPanel11);
            this.pnlResultDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResultDisplay.Location = new System.Drawing.Point(0, 40);
            this.pnlResultDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.pnlResultDisplay.Name = "pnlResultDisplay";
            this.pnlResultDisplay.Size = new System.Drawing.Size(1365, 820);
            this.pnlResultDisplay.TabIndex = 3;
            // 
            // lblAlignViewer
            // 
            this.lblAlignViewer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblAlignViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAlignViewer.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblAlignViewer.ForeColor = System.Drawing.Color.White;
            this.lblAlignViewer.Location = new System.Drawing.Point(0, 0);
            this.lblAlignViewer.Margin = new System.Windows.Forms.Padding(0);
            this.lblAlignViewer.Name = "lblAlignViewer";
            this.lblAlignViewer.Size = new System.Drawing.Size(1365, 40);
            this.lblAlignViewer.TabIndex = 2;
            this.lblAlignViewer.Text = "ALIGN MONITORING";
            this.lblAlignViewer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTab1
            // 
            this.pnlTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTab1.Location = new System.Drawing.Point(682, 0);
            this.pnlTab1.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTab1.Name = "pnlTab1";
            this.pnlTab1.Size = new System.Drawing.Size(683, 205);
            this.pnlTab1.TabIndex = 3;
            // 
            // pnlTab2
            // 
            this.pnlTab2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTab2.Location = new System.Drawing.Point(0, 205);
            this.pnlTab2.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTab2.Name = "pnlTab2";
            this.pnlTab2.Size = new System.Drawing.Size(682, 205);
            this.pnlTab2.TabIndex = 4;
            // 
            // pnlTab3
            // 
            this.pnlTab3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTab3.Location = new System.Drawing.Point(682, 205);
            this.pnlTab3.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTab3.Name = "pnlTab3";
            this.pnlTab3.Size = new System.Drawing.Size(683, 205);
            this.pnlTab3.TabIndex = 4;
            // 
            // pnlTab4
            // 
            this.pnlTab4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTab4.Location = new System.Drawing.Point(0, 410);
            this.pnlTab4.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTab4.Name = "pnlTab4";
            this.pnlTab4.Size = new System.Drawing.Size(682, 205);
            this.pnlTab4.TabIndex = 4;
            // 
            // pnlTab0
            // 
            this.pnlTab0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTab0.Location = new System.Drawing.Point(0, 0);
            this.pnlTab0.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTab0.Name = "pnlTab0";
            this.pnlTab0.Size = new System.Drawing.Size(682, 205);
            this.pnlTab0.TabIndex = 4;
            // 
            // pnlTab5
            // 
            this.pnlTab5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTab5.Location = new System.Drawing.Point(682, 410);
            this.pnlTab5.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTab5.Name = "pnlTab5";
            this.pnlTab5.Size = new System.Drawing.Size(683, 205);
            this.pnlTab5.TabIndex = 5;
            // 
            // pnlTab6
            // 
            this.pnlTab6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTab6.Location = new System.Drawing.Point(0, 615);
            this.pnlTab6.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTab6.Name = "pnlTab6";
            this.pnlTab6.Size = new System.Drawing.Size(682, 205);
            this.pnlTab6.TabIndex = 5;
            // 
            // pnlTab7
            // 
            this.pnlTab7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTab7.Location = new System.Drawing.Point(682, 615);
            this.pnlTab7.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTab7.Name = "pnlTab7";
            this.pnlTab7.Size = new System.Drawing.Size(683, 205);
            this.pnlTab7.TabIndex = 5;
            // 
            // AlignMonitoringControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpAlignViewer);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AlignMonitoringControl";
            this.Size = new System.Drawing.Size(1365, 860);
            this.Load += new System.EventHandler(this.AlignMonitoringControl_Load);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tlpAlignViewer.ResumeLayout(false);
            this.pnlResultDisplay.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.TableLayoutPanel tlpAlignViewer;
        private System.Windows.Forms.Panel pnlResultDisplay;
        private System.Windows.Forms.Label lblAlignViewer;
        private System.Windows.Forms.Panel pnlTab0;
        private System.Windows.Forms.Panel pnlTab7;
        private System.Windows.Forms.Panel pnlTab3;
        private System.Windows.Forms.Panel pnlTab6;
        private System.Windows.Forms.Panel pnlTab1;
        private System.Windows.Forms.Panel pnlTab5;
        private System.Windows.Forms.Panel pnlTab2;
        private System.Windows.Forms.Panel pnlTab4;
    }
}
