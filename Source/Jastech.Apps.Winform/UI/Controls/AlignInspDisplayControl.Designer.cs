namespace Jastech.Apps.Winform.UI.Controls
{
    partial class AlignInspDisplayControl
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
            this.tlpAlignViewer = new System.Windows.Forms.TableLayoutPanel();
            this.lblAlignViewer = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpAkkonHistory = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAlignHistory = new System.Windows.Forms.Label();
            this.pnlAlignResult = new System.Windows.Forms.Panel();
            this.pnlAlignGraph = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTabButton = new System.Windows.Forms.Panel();
            this.pnlInspDisplay = new System.Windows.Forms.Panel();
            this.tlpAlignViewer.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tlpAkkonHistory.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAlignViewer
            // 
            this.tlpAlignViewer.ColumnCount = 1;
            this.tlpAlignViewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlignViewer.Controls.Add(this.lblAlignViewer, 0, 0);
            this.tlpAlignViewer.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tlpAlignViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAlignViewer.Location = new System.Drawing.Point(0, 0);
            this.tlpAlignViewer.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAlignViewer.Name = "tlpAlignViewer";
            this.tlpAlignViewer.RowCount = 2;
            this.tlpAlignViewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpAlignViewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlignViewer.Size = new System.Drawing.Size(1160, 485);
            this.tlpAlignViewer.TabIndex = 3;
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
            this.lblAlignViewer.Size = new System.Drawing.Size(1160, 40);
            this.lblAlignViewer.TabIndex = 1;
            this.lblAlignViewer.Text = "ALIGN";
            this.lblAlignViewer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1160, 445);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // tlpAkkonHistory
            // 
            this.tlpAkkonHistory.ColumnCount = 1;
            this.tlpAkkonHistory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAkkonHistory.Controls.Add(this.label1, 0, 2);
            this.tlpAkkonHistory.Controls.Add(this.lblAlignHistory, 0, 0);
            this.tlpAkkonHistory.Controls.Add(this.pnlAlignResult, 0, 1);
            this.tlpAkkonHistory.Controls.Add(this.pnlAlignGraph, 0, 3);
            this.tlpAkkonHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAkkonHistory.Location = new System.Drawing.Point(740, 0);
            this.tlpAkkonHistory.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAkkonHistory.Name = "tlpAkkonHistory";
            this.tlpAkkonHistory.RowCount = 4;
            this.tlpAkkonHistory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpAkkonHistory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAkkonHistory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpAkkonHistory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAkkonHistory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpAkkonHistory.Size = new System.Drawing.Size(420, 445);
            this.tlpAkkonHistory.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1, 223);
            this.label1.Margin = new System.Windows.Forms.Padding(1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(418, 38);
            this.label1.TabIndex = 3;
            this.label1.Text = "TREND";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAlignHistory
            // 
            this.lblAlignHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblAlignHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAlignHistory.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblAlignHistory.ForeColor = System.Drawing.Color.White;
            this.lblAlignHistory.Location = new System.Drawing.Point(1, 1);
            this.lblAlignHistory.Margin = new System.Windows.Forms.Padding(1);
            this.lblAlignHistory.Name = "lblAlignHistory";
            this.lblAlignHistory.Size = new System.Drawing.Size(418, 38);
            this.lblAlignHistory.TabIndex = 2;
            this.lblAlignHistory.Text = "ALIGN LOG";
            this.lblAlignHistory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlAlignResult
            // 
            this.pnlAlignResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAlignResult.Location = new System.Drawing.Point(1, 41);
            this.pnlAlignResult.Margin = new System.Windows.Forms.Padding(1);
            this.pnlAlignResult.Name = "pnlAlignResult";
            this.pnlAlignResult.Size = new System.Drawing.Size(418, 180);
            this.pnlAlignResult.TabIndex = 3;
            // 
            // pnlAlignGraph
            // 
            this.pnlAlignGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAlignGraph.Location = new System.Drawing.Point(1, 263);
            this.pnlAlignGraph.Margin = new System.Windows.Forms.Padding(1);
            this.pnlAlignGraph.Name = "pnlAlignGraph";
            this.pnlAlignGraph.Size = new System.Drawing.Size(418, 181);
            this.pnlAlignGraph.TabIndex = 4;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(740, 445);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // pnlTabButton
            // 
            this.pnlTabButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTabButton.Location = new System.Drawing.Point(0, 0);
            this.pnlTabButton.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTabButton.Name = "pnlTabButton";
            this.pnlTabButton.Size = new System.Drawing.Size(740, 40);
            this.pnlTabButton.TabIndex = 1;
            // 
            // pnlInspDisplay
            // 
            this.pnlInspDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInspDisplay.Location = new System.Drawing.Point(0, 40);
            this.pnlInspDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.pnlInspDisplay.Name = "pnlInspDisplay";
            this.pnlInspDisplay.Size = new System.Drawing.Size(740, 405);
            this.pnlInspDisplay.TabIndex = 2;
            // 
            // AlignInspDisplayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpAlignViewer);
            this.Name = "AlignInspDisplayControl";
            this.Size = new System.Drawing.Size(1160, 485);
            this.Load += new System.EventHandler(this.AlignInspDisplayControl_Load);
            this.tlpAlignViewer.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tlpAkkonHistory.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAlignViewer;
        private System.Windows.Forms.Label lblAlignViewer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tlpAkkonHistory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAlignHistory;
        private System.Windows.Forms.Panel pnlAlignResult;
        private System.Windows.Forms.Panel pnlAlignGraph;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlTabButton;
        private System.Windows.Forms.Panel pnlInspDisplay;
    }
}
