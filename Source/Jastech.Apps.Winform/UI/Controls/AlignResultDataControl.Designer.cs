namespace Jastech.Apps.Winform.UI.Controls
{
    partial class AlignResultDataControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvAlignHistory = new System.Windows.Forms.DataGridView();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPanelID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTab = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colJudge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPreHead = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFinalHead = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlChart = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlignHistory)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvAlignHistory
            // 
            this.dgvAlignHistory.AllowUserToAddRows = false;
            this.dgvAlignHistory.AllowUserToDeleteRows = false;
            this.dgvAlignHistory.AllowUserToResizeRows = false;
            this.dgvAlignHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAlignHistory.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.dgvAlignHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAlignHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvAlignHistory.ColumnHeadersHeight = 40;
            this.dgvAlignHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTime,
            this.colPanelID,
            this.colTab,
            this.colJudge,
            this.colPreHead,
            this.colFinalHead,
            this.colCount,
            this.colLength,
            this.Column3,
            this.Column1,
            this.Column2});
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAlignHistory.DefaultCellStyle = dataGridViewCellStyle17;
            this.dgvAlignHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAlignHistory.EnableHeadersVisualStyles = false;
            this.dgvAlignHistory.Location = new System.Drawing.Point(0, 0);
            this.dgvAlignHistory.Margin = new System.Windows.Forms.Padding(0);
            this.dgvAlignHistory.MultiSelect = false;
            this.dgvAlignHistory.Name = "dgvAlignHistory";
            this.dgvAlignHistory.ReadOnly = true;
            this.dgvAlignHistory.RowHeadersVisible = false;
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.Black;
            this.dgvAlignHistory.RowsDefaultCellStyle = dataGridViewCellStyle18;
            this.dgvAlignHistory.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvAlignHistory.RowTemplate.Height = 23;
            this.dgvAlignHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAlignHistory.Size = new System.Drawing.Size(933, 265);
            this.dgvAlignHistory.TabIndex = 2;
            this.dgvAlignHistory.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAlignHistory_CellContentDoubleClick);
            // 
            // colTime
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.colTime.DefaultCellStyle = dataGridViewCellStyle11;
            this.colTime.HeaderText = "Time";
            this.colTime.MinimumWidth = 90;
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            this.colTime.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTime.Width = 90;
            // 
            // colPanelID
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colPanelID.DefaultCellStyle = dataGridViewCellStyle12;
            this.colPanelID.HeaderText = "ID";
            this.colPanelID.MinimumWidth = 150;
            this.colPanelID.Name = "colPanelID";
            this.colPanelID.ReadOnly = true;
            this.colPanelID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colPanelID.Width = 150;
            // 
            // colTab
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.colTab.DefaultCellStyle = dataGridViewCellStyle13;
            this.colTab.HeaderText = "Tab";
            this.colTab.MinimumWidth = 35;
            this.colTab.Name = "colTab";
            this.colTab.ReadOnly = true;
            this.colTab.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTab.Width = 37;
            // 
            // colJudge
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.colJudge.DefaultCellStyle = dataGridViewCellStyle14;
            this.colJudge.HeaderText = "Judge";
            this.colJudge.MinimumWidth = 50;
            this.colJudge.Name = "colJudge";
            this.colJudge.ReadOnly = true;
            this.colJudge.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colJudge.Width = 51;
            // 
            // colPreHead
            // 
            this.colPreHead.HeaderText = "P";
            this.colPreHead.MinimumWidth = 50;
            this.colPreHead.Name = "colPreHead";
            this.colPreHead.ReadOnly = true;
            this.colPreHead.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colPreHead.Width = 50;
            // 
            // colFinalHead
            // 
            this.colFinalHead.HeaderText = "F";
            this.colFinalHead.MinimumWidth = 50;
            this.colFinalHead.Name = "colFinalHead";
            this.colFinalHead.ReadOnly = true;
            this.colFinalHead.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colFinalHead.Width = 50;
            // 
            // colCount
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.colCount.DefaultCellStyle = dataGridViewCellStyle15;
            this.colCount.HeaderText = "Lx";
            this.colCount.MinimumWidth = 60;
            this.colCount.Name = "colCount";
            this.colCount.ReadOnly = true;
            this.colCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colCount.Width = 60;
            // 
            // colLength
            // 
            this.colLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.colLength.DefaultCellStyle = dataGridViewCellStyle16;
            this.colLength.HeaderText = "Ly";
            this.colLength.MinimumWidth = 60;
            this.colLength.Name = "colLength";
            this.colLength.ReadOnly = true;
            this.colLength.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colLength.Width = 60;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column3.HeaderText = "Cx";
            this.Column3.MinimumWidth = 60;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 60;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Rx";
            this.Column1.MinimumWidth = 60;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 60;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Ry";
            this.Column2.MinimumWidth = 60;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgvAlignHistory, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlChart, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(933, 443);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // pnlChart
            // 
            this.pnlChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChart.Location = new System.Drawing.Point(0, 265);
            this.pnlChart.Margin = new System.Windows.Forms.Padding(0);
            this.pnlChart.Name = "pnlChart";
            this.pnlChart.Size = new System.Drawing.Size(933, 178);
            this.pnlChart.TabIndex = 3;
            // 
            // AlignResultDataControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "AlignResultDataControl";
            this.Size = new System.Drawing.Size(933, 443);
            this.Load += new System.EventHandler(this.AlignResultDataControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlignHistory)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAlignHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPanelID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTab;
        private System.Windows.Forms.DataGridViewTextBoxColumn colJudge;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPreHead;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFinalHead;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlChart;
    }
}
