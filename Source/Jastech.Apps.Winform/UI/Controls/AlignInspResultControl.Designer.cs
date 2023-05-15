namespace Jastech.Apps.Winform.UI.Controls
{
    partial class AlignInspResultControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvAlignHistory = new System.Windows.Forms.DataGridView();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPanelID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTab = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colJudge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLenght = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlignHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAlignHistory
            // 
            this.dgvAlignHistory.AllowUserToAddRows = false;
            this.dgvAlignHistory.AllowUserToDeleteRows = false;
            this.dgvAlignHistory.AllowUserToResizeRows = false;
            this.dgvAlignHistory.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.dgvAlignHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAlignHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAlignHistory.ColumnHeadersHeight = 40;
            this.dgvAlignHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvAlignHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTime,
            this.colPanelID,
            this.colTab,
            this.colJudge,
            this.colCount,
            this.colLenght,
            this.Column1,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAlignHistory.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvAlignHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAlignHistory.EnableHeadersVisualStyles = false;
            this.dgvAlignHistory.Location = new System.Drawing.Point(0, 0);
            this.dgvAlignHistory.Margin = new System.Windows.Forms.Padding(0);
            this.dgvAlignHistory.MultiSelect = false;
            this.dgvAlignHistory.Name = "dgvAlignHistory";
            this.dgvAlignHistory.ReadOnly = true;
            this.dgvAlignHistory.RowHeadersVisible = false;
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            this.dgvAlignHistory.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvAlignHistory.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvAlignHistory.RowTemplate.Height = 23;
            this.dgvAlignHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAlignHistory.Size = new System.Drawing.Size(542, 341);
            this.dgvAlignHistory.TabIndex = 2;
            // 
            // colTime
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.colTime.DefaultCellStyle = dataGridViewCellStyle2;
            this.colTime.HeaderText = "Time";
            this.colTime.MinimumWidth = 100;
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            this.colTime.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colPanelID
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colPanelID.DefaultCellStyle = dataGridViewCellStyle3;
            this.colPanelID.HeaderText = "Panel ID";
            this.colPanelID.MinimumWidth = 100;
            this.colPanelID.Name = "colPanelID";
            this.colPanelID.ReadOnly = true;
            this.colPanelID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colPanelID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colTab
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.colTab.DefaultCellStyle = dataGridViewCellStyle4;
            this.colTab.HeaderText = "Tab";
            this.colTab.MinimumWidth = 35;
            this.colTab.Name = "colTab";
            this.colTab.ReadOnly = true;
            this.colTab.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colTab.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTab.Width = 35;
            // 
            // colJudge
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.colJudge.DefaultCellStyle = dataGridViewCellStyle5;
            this.colJudge.HeaderText = "Judge";
            this.colJudge.MinimumWidth = 50;
            this.colJudge.Name = "colJudge";
            this.colJudge.ReadOnly = true;
            this.colJudge.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colJudge.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colJudge.Width = 50;
            // 
            // colCount
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.colCount.DefaultCellStyle = dataGridViewCellStyle6;
            this.colCount.HeaderText = "Lx";
            this.colCount.MinimumWidth = 50;
            this.colCount.Name = "colCount";
            this.colCount.ReadOnly = true;
            this.colCount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colCount.Width = 50;
            // 
            // colLenght
            // 
            this.colLenght.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.colLenght.DefaultCellStyle = dataGridViewCellStyle7;
            this.colLenght.HeaderText = "Ly";
            this.colLenght.MinimumWidth = 50;
            this.colLenght.Name = "colLenght";
            this.colLenght.ReadOnly = true;
            this.colLenght.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colLenght.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Rx";
            this.Column1.MinimumWidth = 50;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 50;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Ry";
            this.Column2.MinimumWidth = 50;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 50;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "Cx";
            this.Column3.MinimumWidth = 50;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AlignInspResultControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvAlignHistory);
            this.Name = "AlignInspResultControl";
            this.Size = new System.Drawing.Size(542, 341);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlignHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAlignHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPanelID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTab;
        private System.Windows.Forms.DataGridViewTextBoxColumn colJudge;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLenght;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}
