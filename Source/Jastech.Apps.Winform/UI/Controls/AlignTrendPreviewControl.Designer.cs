namespace Jastech.Apps.Winform.UI.Controls
{
    partial class AlignTrendPreviewControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpAlignTrendPreview = new System.Windows.Forms.TableLayoutPanel();
            this.tlpData = new System.Windows.Forms.TableLayoutPanel();
            this.tlpAlignResult = new System.Windows.Forms.TableLayoutPanel();
            this.lblResult = new System.Windows.Forms.Label();
            this.dgvAlignTrendData = new Jastech.Framework.Winform.Controls.DoubleBufferedDatagridView();
            this.tlpChart = new System.Windows.Forms.TableLayoutPanel();
            this.tlpDataCount = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDataCount = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.tlpAlignTrendPreview.SuspendLayout();
            this.tlpData.SuspendLayout();
            this.tlpAlignResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlignTrendData)).BeginInit();
            this.tlpDataCount.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAlignTrendPreview
            // 
            this.tlpAlignTrendPreview.ColumnCount = 1;
            this.tlpAlignTrendPreview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlignTrendPreview.Controls.Add(this.tlpData, 0, 1);
            this.tlpAlignTrendPreview.Controls.Add(this.tlpDataCount, 0, 0);
            this.tlpAlignTrendPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAlignTrendPreview.Location = new System.Drawing.Point(0, 0);
            this.tlpAlignTrendPreview.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAlignTrendPreview.Name = "tlpAlignTrendPreview";
            this.tlpAlignTrendPreview.RowCount = 2;
            this.tlpAlignTrendPreview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpAlignTrendPreview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlignTrendPreview.Size = new System.Drawing.Size(860, 540);
            this.tlpAlignTrendPreview.TabIndex = 0;
            // 
            // tlpData
            // 
            this.tlpData.ColumnCount = 2;
            this.tlpData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 435F));
            this.tlpData.Controls.Add(this.tlpAlignResult, 1, 0);
            this.tlpData.Controls.Add(this.tlpChart, 0, 0);
            this.tlpData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpData.Location = new System.Drawing.Point(0, 60);
            this.tlpData.Margin = new System.Windows.Forms.Padding(0);
            this.tlpData.Name = "tlpData";
            this.tlpData.RowCount = 1;
            this.tlpData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpData.Size = new System.Drawing.Size(860, 480);
            this.tlpData.TabIndex = 10;
            // 
            // tlpAlignResult
            // 
            this.tlpAlignResult.ColumnCount = 1;
            this.tlpAlignResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlignResult.Controls.Add(this.lblResult, 0, 0);
            this.tlpAlignResult.Controls.Add(this.dgvAlignTrendData, 0, 1);
            this.tlpAlignResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAlignResult.Location = new System.Drawing.Point(428, 3);
            this.tlpAlignResult.Name = "tlpAlignResult";
            this.tlpAlignResult.RowCount = 2;
            this.tlpAlignResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpAlignResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlignResult.Size = new System.Drawing.Size(429, 474);
            this.tlpAlignResult.TabIndex = 2;
            // 
            // lblResult
            // 
            this.lblResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResult.Location = new System.Drawing.Point(0, 0);
            this.lblResult.Margin = new System.Windows.Forms.Padding(0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(429, 30);
            this.lblResult.TabIndex = 8;
            this.lblResult.Text = "Align Result";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvAlignTrendData
            // 
            this.dgvAlignTrendData.AllowUserToAddRows = false;
            this.dgvAlignTrendData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAlignTrendData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAlignTrendData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAlignTrendData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAlignTrendData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAlignTrendData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAlignTrendData.EnableHeadersVisualStyles = false;
            this.dgvAlignTrendData.Location = new System.Drawing.Point(3, 33);
            this.dgvAlignTrendData.Name = "dgvAlignTrendData";
            this.dgvAlignTrendData.ReadOnly = true;
            this.dgvAlignTrendData.RowHeadersVisible = false;
            this.dgvAlignTrendData.RowTemplate.Height = 23;
            this.dgvAlignTrendData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAlignTrendData.Size = new System.Drawing.Size(423, 438);
            this.dgvAlignTrendData.TabIndex = 0;
            // 
            // tlpChart
            // 
            this.tlpChart.ColumnCount = 1;
            this.tlpChart.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpChart.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpChart.Location = new System.Drawing.Point(0, 0);
            this.tlpChart.Margin = new System.Windows.Forms.Padding(0);
            this.tlpChart.Name = "tlpChart";
            this.tlpChart.RowCount = 1;
            this.tlpChart.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpChart.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 480F));
            this.tlpChart.Size = new System.Drawing.Size(425, 480);
            this.tlpChart.TabIndex = 3;
            // 
            // tlpDataCount
            // 
            this.tlpDataCount.ColumnCount = 5;
            this.tlpDataCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpDataCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpDataCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpDataCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpDataCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDataCount.Controls.Add(this.label1, 0, 0);
            this.tlpDataCount.Controls.Add(this.lblDataCount, 1, 0);
            this.tlpDataCount.Controls.Add(this.lblSearch, 3, 0);
            this.tlpDataCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDataCount.Location = new System.Drawing.Point(0, 0);
            this.tlpDataCount.Margin = new System.Windows.Forms.Padding(0);
            this.tlpDataCount.Name = "tlpDataCount";
            this.tlpDataCount.RowCount = 1;
            this.tlpDataCount.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDataCount.Size = new System.Drawing.Size(860, 60);
            this.tlpDataCount.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 60);
            this.label1.TabIndex = 8;
            this.label1.Text = "Data Count";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDataCount
            // 
            this.lblDataCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDataCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDataCount.Location = new System.Drawing.Point(120, 0);
            this.lblDataCount.Margin = new System.Windows.Forms.Padding(0);
            this.lblDataCount.Name = "lblDataCount";
            this.lblDataCount.Size = new System.Drawing.Size(120, 60);
            this.lblDataCount.TabIndex = 9;
            this.lblDataCount.Text = "10";
            this.lblDataCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDataCount.Click += new System.EventHandler(this.lblDataCount_Click);
            // 
            // lblSearch
            // 
            this.lblSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSearch.Location = new System.Drawing.Point(360, 0);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(120, 60);
            this.lblSearch.TabIndex = 8;
            this.lblSearch.Text = "Search";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSearch.Click += new System.EventHandler(this.lblSearch_Click);
            // 
            // AlignTrendPreviewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpAlignTrendPreview);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "AlignTrendPreviewControl";
            this.Size = new System.Drawing.Size(860, 540);
            this.Load += new System.EventHandler(this.AlignTrendControl_Load);
            this.tlpAlignTrendPreview.ResumeLayout(false);
            this.tlpData.ResumeLayout(false);
            this.tlpAlignResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlignTrendData)).EndInit();
            this.tlpDataCount.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAlignTrendPreview;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tlpDataCount;
        private System.Windows.Forms.Label lblDataCount;
        private System.Windows.Forms.TableLayoutPanel tlpData;
        private System.Windows.Forms.TableLayoutPanel tlpAlignResult;
        private System.Windows.Forms.Label lblResult;
        private Framework.Winform.Controls.DoubleBufferedDatagridView dgvAlignTrendData;
        private System.Windows.Forms.TableLayoutPanel tlpChart;
        private System.Windows.Forms.Label lblSearch;
    }
}
