namespace Jastech.Apps.Winform.UI.Controls
{
    partial class AkkonInspDisplayControl
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
            this.tlpAkkonViewer = new System.Windows.Forms.TableLayoutPanel();
            this.lblAkkonViewer = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpAkkonHistory = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAkkonHistory = new System.Windows.Forms.Label();
            this.pnlAkkonResult = new System.Windows.Forms.Panel();
            this.pnlAkkonGraph = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTabButton = new System.Windows.Forms.Panel();
            this.pnlInspDisplay = new System.Windows.Forms.Panel();
            this.tlpAkkonViewer.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tlpAkkonHistory.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAkkonViewer
            // 
            this.tlpAkkonViewer.ColumnCount = 1;
            this.tlpAkkonViewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAkkonViewer.Controls.Add(this.lblAkkonViewer, 0, 0);
            this.tlpAkkonViewer.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tlpAkkonViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAkkonViewer.Location = new System.Drawing.Point(0, 0);
            this.tlpAkkonViewer.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAkkonViewer.Name = "tlpAkkonViewer";
            this.tlpAkkonViewer.RowCount = 2;
            this.tlpAkkonViewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpAkkonViewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAkkonViewer.Size = new System.Drawing.Size(792, 387);
            this.tlpAkkonViewer.TabIndex = 2;
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
            this.lblAkkonViewer.Size = new System.Drawing.Size(792, 40);
            this.lblAkkonViewer.TabIndex = 1;
            this.lblAkkonViewer.Text = "AKKON";
            this.lblAkkonViewer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 420F));
            this.tableLayoutPanel2.Controls.Add(this.tlpAkkonHistory, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 40);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(792, 347);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // tlpAkkonHistory
            // 
            this.tlpAkkonHistory.ColumnCount = 1;
            this.tlpAkkonHistory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAkkonHistory.Controls.Add(this.label1, 0, 2);
            this.tlpAkkonHistory.Controls.Add(this.lblAkkonHistory, 0, 0);
            this.tlpAkkonHistory.Controls.Add(this.pnlAkkonResult, 0, 1);
            this.tlpAkkonHistory.Controls.Add(this.pnlAkkonGraph, 0, 3);
            this.tlpAkkonHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAkkonHistory.Location = new System.Drawing.Point(372, 0);
            this.tlpAkkonHistory.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAkkonHistory.Name = "tlpAkkonHistory";
            this.tlpAkkonHistory.RowCount = 4;
            this.tlpAkkonHistory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpAkkonHistory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAkkonHistory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpAkkonHistory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAkkonHistory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpAkkonHistory.Size = new System.Drawing.Size(420, 347);
            this.tlpAkkonHistory.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1, 174);
            this.label1.Margin = new System.Windows.Forms.Padding(1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(418, 38);
            this.label1.TabIndex = 3;
            this.label1.Text = "TREND";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAkkonHistory
            // 
            this.lblAkkonHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblAkkonHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAkkonHistory.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblAkkonHistory.ForeColor = System.Drawing.Color.White;
            this.lblAkkonHistory.Location = new System.Drawing.Point(1, 1);
            this.lblAkkonHistory.Margin = new System.Windows.Forms.Padding(1);
            this.lblAkkonHistory.Name = "lblAkkonHistory";
            this.lblAkkonHistory.Size = new System.Drawing.Size(418, 38);
            this.lblAkkonHistory.TabIndex = 2;
            this.lblAkkonHistory.Text = "AKKON LOG";
            this.lblAkkonHistory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlAkkonResult
            // 
            this.pnlAkkonResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAkkonResult.Location = new System.Drawing.Point(1, 41);
            this.pnlAkkonResult.Margin = new System.Windows.Forms.Padding(1);
            this.pnlAkkonResult.Name = "pnlAkkonResult";
            this.pnlAkkonResult.Size = new System.Drawing.Size(418, 131);
            this.pnlAkkonResult.TabIndex = 3;
            // 
            // pnlAkkonGraph
            // 
            this.pnlAkkonGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAkkonGraph.Location = new System.Drawing.Point(1, 214);
            this.pnlAkkonGraph.Margin = new System.Windows.Forms.Padding(1);
            this.pnlAkkonGraph.Name = "pnlAkkonGraph";
            this.pnlAkkonGraph.Size = new System.Drawing.Size(418, 132);
            this.pnlAkkonGraph.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pnlTabButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlInspDisplay, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(372, 347);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // pnlTabButton
            // 
            this.pnlTabButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTabButton.Location = new System.Drawing.Point(0, 0);
            this.pnlTabButton.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTabButton.Name = "pnlTabButton";
            this.pnlTabButton.Size = new System.Drawing.Size(372, 40);
            this.pnlTabButton.TabIndex = 1;
            // 
            // pnlInspDisplay
            // 
            this.pnlInspDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInspDisplay.Location = new System.Drawing.Point(0, 40);
            this.pnlInspDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.pnlInspDisplay.Name = "pnlInspDisplay";
            this.pnlInspDisplay.Size = new System.Drawing.Size(372, 307);
            this.pnlInspDisplay.TabIndex = 2;
            // 
            // AkkonInspDisplayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpAkkonViewer);
            this.Name = "AkkonInspDisplayControl";
            this.Size = new System.Drawing.Size(792, 387);
            this.Load += new System.EventHandler(this.AkkonInspControl_Load);
            this.tlpAkkonViewer.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tlpAkkonHistory.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAkkonViewer;
        private System.Windows.Forms.Label lblAkkonViewer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tlpAkkonHistory;
        private System.Windows.Forms.Label lblAkkonHistory;
        private System.Windows.Forms.Panel pnlAkkonResult;
        private System.Windows.Forms.Panel pnlAkkonGraph;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlTabButton;
        private System.Windows.Forms.Panel pnlInspDisplay;
        private System.Windows.Forms.Label label1;
    }
}
