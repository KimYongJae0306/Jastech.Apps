namespace Jastech.Apps.Winform.UI.Controls
{
    partial class AlignTrendControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpAlignTrend = new System.Windows.Forms.TableLayoutPanel();
            this.pnlAlignType = new System.Windows.Forms.Panel();
            this.lblRy = new System.Windows.Forms.Label();
            this.lblRx = new System.Windows.Forms.Label();
            this.lblCx = new System.Windows.Forms.Label();
            this.lblLy = new System.Windows.Forms.Label();
            this.lblLx = new System.Windows.Forms.Label();
            this.lblAlign = new System.Windows.Forms.Label();
            this.tlpData = new System.Windows.Forms.TableLayoutPanel();
            this.dgvAlignTrendData = new System.Windows.Forms.DataGridView();
            this.pnlTabs = new System.Windows.Forms.Panel();
            this.lblTab = new System.Windows.Forms.Label();
            this.pnlChart = new System.Windows.Forms.Panel();
            this.tlpAlignTrend.SuspendLayout();
            this.pnlAlignType.SuspendLayout();
            this.tlpData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlignTrendData)).BeginInit();
            this.pnlTabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAlignTrend
            // 
            this.tlpAlignTrend.ColumnCount = 1;
            this.tlpAlignTrend.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlignTrend.Controls.Add(this.pnlAlignType, 0, 1);
            this.tlpAlignTrend.Controls.Add(this.tlpData, 0, 2);
            this.tlpAlignTrend.Controls.Add(this.pnlTabs, 0, 0);
            this.tlpAlignTrend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAlignTrend.Location = new System.Drawing.Point(0, 0);
            this.tlpAlignTrend.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAlignTrend.Name = "tlpAlignTrend";
            this.tlpAlignTrend.RowCount = 3;
            this.tlpAlignTrend.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpAlignTrend.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpAlignTrend.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlignTrend.Size = new System.Drawing.Size(860, 540);
            this.tlpAlignTrend.TabIndex = 0;
            // 
            // pnlAlignType
            // 
            this.pnlAlignType.Controls.Add(this.lblRy);
            this.pnlAlignType.Controls.Add(this.lblRx);
            this.pnlAlignType.Controls.Add(this.lblCx);
            this.pnlAlignType.Controls.Add(this.lblLy);
            this.pnlAlignType.Controls.Add(this.lblLx);
            this.pnlAlignType.Controls.Add(this.lblAlign);
            this.pnlAlignType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAlignType.Location = new System.Drawing.Point(0, 60);
            this.pnlAlignType.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAlignType.Name = "pnlAlignType";
            this.pnlAlignType.Size = new System.Drawing.Size(860, 60);
            this.pnlAlignType.TabIndex = 2;
            // 
            // lblRy
            // 
            this.lblRy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRy.Location = new System.Drawing.Point(720, 0);
            this.lblRy.Margin = new System.Windows.Forms.Padding(0);
            this.lblRy.Name = "lblRy";
            this.lblRy.Size = new System.Drawing.Size(120, 60);
            this.lblRy.TabIndex = 5;
            this.lblRy.Text = "Ry";
            this.lblRy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRy.Click += new System.EventHandler(this.lblRy_Click);
            // 
            // lblRx
            // 
            this.lblRx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRx.Location = new System.Drawing.Point(580, 0);
            this.lblRx.Margin = new System.Windows.Forms.Padding(0);
            this.lblRx.Name = "lblRx";
            this.lblRx.Size = new System.Drawing.Size(120, 60);
            this.lblRx.TabIndex = 4;
            this.lblRx.Text = "Rx";
            this.lblRx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRx.Click += new System.EventHandler(this.lblRx_Click);
            // 
            // lblCx
            // 
            this.lblCx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCx.Location = new System.Drawing.Point(440, 0);
            this.lblCx.Margin = new System.Windows.Forms.Padding(0);
            this.lblCx.Name = "lblCx";
            this.lblCx.Size = new System.Drawing.Size(120, 60);
            this.lblCx.TabIndex = 3;
            this.lblCx.Text = "Cx";
            this.lblCx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCx.Click += new System.EventHandler(this.lblCx_Click);
            // 
            // lblLy
            // 
            this.lblLy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLy.Location = new System.Drawing.Point(300, 0);
            this.lblLy.Margin = new System.Windows.Forms.Padding(0);
            this.lblLy.Name = "lblLy";
            this.lblLy.Size = new System.Drawing.Size(120, 60);
            this.lblLy.TabIndex = 2;
            this.lblLy.Text = "Ly";
            this.lblLy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLy.Click += new System.EventHandler(this.lblLy_Click);
            // 
            // lblLx
            // 
            this.lblLx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLx.Location = new System.Drawing.Point(160, 0);
            this.lblLx.Margin = new System.Windows.Forms.Padding(0);
            this.lblLx.Name = "lblLx";
            this.lblLx.Size = new System.Drawing.Size(120, 60);
            this.lblLx.TabIndex = 1;
            this.lblLx.Text = "Lx";
            this.lblLx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLx.Click += new System.EventHandler(this.lblLx_Click);
            // 
            // lblAlign
            // 
            this.lblAlign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAlign.Location = new System.Drawing.Point(0, 0);
            this.lblAlign.Margin = new System.Windows.Forms.Padding(0);
            this.lblAlign.Name = "lblAlign";
            this.lblAlign.Size = new System.Drawing.Size(120, 60);
            this.lblAlign.TabIndex = 0;
            this.lblAlign.Text = "Align";
            this.lblAlign.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAlign.Click += new System.EventHandler(this.lblAlign_Click);
            // 
            // tlpData
            // 
            this.tlpData.ColumnCount = 2;
            this.tlpData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tlpData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tlpData.Controls.Add(this.dgvAlignTrendData, 1, 0);
            this.tlpData.Controls.Add(this.pnlChart, 0, 0);
            this.tlpData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpData.Location = new System.Drawing.Point(0, 120);
            this.tlpData.Margin = new System.Windows.Forms.Padding(0);
            this.tlpData.Name = "tlpData";
            this.tlpData.RowCount = 1;
            this.tlpData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpData.Size = new System.Drawing.Size(860, 420);
            this.tlpData.TabIndex = 3;
            // 
            // dgvAlignTrendData
            // 
            this.dgvAlignTrendData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvAlignTrendData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAlignTrendData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAlignTrendData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAlignTrendData.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvAlignTrendData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAlignTrendData.Location = new System.Drawing.Point(476, 3);
            this.dgvAlignTrendData.Name = "dgvAlignTrendData";
            this.dgvAlignTrendData.ReadOnly = true;
            this.dgvAlignTrendData.RowHeadersVisible = false;
            this.dgvAlignTrendData.RowTemplate.Height = 23;
            this.dgvAlignTrendData.Size = new System.Drawing.Size(381, 414);
            this.dgvAlignTrendData.TabIndex = 0;
            // 
            // pnlTabs
            // 
            this.pnlTabs.Controls.Add(this.lblTab);
            this.pnlTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTabs.Location = new System.Drawing.Point(0, 0);
            this.pnlTabs.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTabs.Name = "pnlTabs";
            this.pnlTabs.Size = new System.Drawing.Size(860, 60);
            this.pnlTabs.TabIndex = 4;
            // 
            // lblTab
            // 
            this.lblTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTab.Location = new System.Drawing.Point(0, 0);
            this.lblTab.Margin = new System.Windows.Forms.Padding(0);
            this.lblTab.Name = "lblTab";
            this.lblTab.Size = new System.Drawing.Size(120, 60);
            this.lblTab.TabIndex = 6;
            this.lblTab.Text = "Tab";
            this.lblTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlChart
            // 
            this.pnlChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChart.Location = new System.Drawing.Point(0, 0);
            this.pnlChart.Margin = new System.Windows.Forms.Padding(0);
            this.pnlChart.Name = "pnlChart";
            this.pnlChart.Size = new System.Drawing.Size(473, 420);
            this.pnlChart.TabIndex = 1;
            // 
            // AlignTrendControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpAlignTrend);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "AlignTrendControl";
            this.Size = new System.Drawing.Size(860, 540);
            this.Load += new System.EventHandler(this.AlignTrendControl_Load);
            this.tlpAlignTrend.ResumeLayout(false);
            this.pnlAlignType.ResumeLayout(false);
            this.tlpData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlignTrendData)).EndInit();
            this.pnlTabs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAlignTrend;
        private System.Windows.Forms.Panel pnlAlignType;
        private System.Windows.Forms.Label lblRy;
        private System.Windows.Forms.Label lblRx;
        private System.Windows.Forms.Label lblCx;
        private System.Windows.Forms.Label lblLy;
        private System.Windows.Forms.Label lblLx;
        private System.Windows.Forms.Label lblAlign;
        private System.Windows.Forms.TableLayoutPanel tlpData;
        private System.Windows.Forms.Panel pnlTabs;
        private System.Windows.Forms.DataGridView dgvAlignTrendData;
        private System.Windows.Forms.Label lblTab;
        private System.Windows.Forms.Panel pnlChart;
    }
}
