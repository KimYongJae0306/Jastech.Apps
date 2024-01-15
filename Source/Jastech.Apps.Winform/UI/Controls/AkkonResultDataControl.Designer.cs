namespace Jastech.Apps.Winform.UI.Controls
{
    partial class AkkonResultDataControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvAkkonHistory = new System.Windows.Forms.DataGridView();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPanelID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTab = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colJudge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlChart = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAkkonHistory)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvAkkonHistory
            // 
            this.dgvAkkonHistory.AllowUserToAddRows = false;
            this.dgvAkkonHistory.AllowUserToDeleteRows = false;
            this.dgvAkkonHistory.AllowUserToResizeRows = false;
            this.dgvAkkonHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAkkonHistory.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.dgvAkkonHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle19.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAkkonHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.dgvAkkonHistory.ColumnHeadersHeight = 40;
            this.dgvAkkonHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTime,
            this.colPanelID,
            this.colTab,
            this.colJudge,
            this.colCount,
            this.colLength});
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle26.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle26.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAkkonHistory.DefaultCellStyle = dataGridViewCellStyle26;
            this.dgvAkkonHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAkkonHistory.EnableHeadersVisualStyles = false;
            this.dgvAkkonHistory.Location = new System.Drawing.Point(0, 0);
            this.dgvAkkonHistory.Margin = new System.Windows.Forms.Padding(0);
            this.dgvAkkonHistory.MultiSelect = false;
            this.dgvAkkonHistory.Name = "dgvAkkonHistory";
            this.dgvAkkonHistory.ReadOnly = true;
            this.dgvAkkonHistory.RowHeadersVisible = false;
            dataGridViewCellStyle27.ForeColor = System.Drawing.Color.Black;
            this.dgvAkkonHistory.RowsDefaultCellStyle = dataGridViewCellStyle27;
            this.dgvAkkonHistory.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvAkkonHistory.RowTemplate.Height = 23;
            this.dgvAkkonHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAkkonHistory.Size = new System.Drawing.Size(672, 357);
            this.dgvAkkonHistory.TabIndex = 1;
            // 
            // colTime
            // 
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.colTime.DefaultCellStyle = dataGridViewCellStyle20;
            this.colTime.HeaderText = "Time";
            this.colTime.MinimumWidth = 100;
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            this.colTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colPanelID
            // 
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colPanelID.DefaultCellStyle = dataGridViewCellStyle21;
            this.colPanelID.HeaderText = "ID";
            this.colPanelID.MinimumWidth = 150;
            this.colPanelID.Name = "colPanelID";
            this.colPanelID.ReadOnly = true;
            this.colPanelID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colPanelID.Width = 150;
            // 
            // colTab
            // 
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.colTab.DefaultCellStyle = dataGridViewCellStyle22;
            this.colTab.HeaderText = "Tab";
            this.colTab.MinimumWidth = 35;
            this.colTab.Name = "colTab";
            this.colTab.ReadOnly = true;
            this.colTab.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTab.Width = 37;
            // 
            // colJudge
            // 
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.colJudge.DefaultCellStyle = dataGridViewCellStyle23;
            this.colJudge.HeaderText = "Judge";
            this.colJudge.MinimumWidth = 50;
            this.colJudge.Name = "colJudge";
            this.colJudge.ReadOnly = true;
            this.colJudge.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colJudge.Width = 51;
            // 
            // colCount
            // 
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.colCount.DefaultCellStyle = dataGridViewCellStyle24;
            this.colCount.HeaderText = "Count";
            this.colCount.MinimumWidth = 50;
            this.colCount.Name = "colCount";
            this.colCount.ReadOnly = true;
            this.colCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colCount.Width = 51;
            // 
            // colLength
            // 
            this.colLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.colLength.DefaultCellStyle = dataGridViewCellStyle25;
            this.colLength.HeaderText = "Length";
            this.colLength.MinimumWidth = 70;
            this.colLength.Name = "colLength";
            this.colLength.ReadOnly = true;
            this.colLength.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgvAkkonHistory, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlChart, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(672, 595);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // pnlChart
            // 
            this.pnlChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChart.Location = new System.Drawing.Point(0, 357);
            this.pnlChart.Margin = new System.Windows.Forms.Padding(0);
            this.pnlChart.Name = "pnlChart";
            this.pnlChart.Size = new System.Drawing.Size(672, 238);
            this.pnlChart.TabIndex = 2;
            // 
            // AkkonResultDataControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AkkonResultDataControl";
            this.Size = new System.Drawing.Size(672, 595);
            this.Load += new System.EventHandler(this.AkkonResultDataControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAkkonHistory)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAkkonHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPanelID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTab;
        private System.Windows.Forms.DataGridViewTextBoxColumn colJudge;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLength;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlChart;
    }
}
