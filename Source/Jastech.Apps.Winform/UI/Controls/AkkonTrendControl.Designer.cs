namespace Jastech.Apps.Winform.UI.Controls
{
    partial class AkkonTrendControl
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
            this.tlpAkkonTrend = new System.Windows.Forms.TableLayoutPanel();
            this.pnlAlignType = new System.Windows.Forms.Panel();
            this.lblStd = new System.Windows.Forms.Label();
            this.lblStrength = new System.Windows.Forms.Label();
            this.lblLength = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblAlign = new System.Windows.Forms.Label();
            this.tlpData = new System.Windows.Forms.TableLayoutPanel();
            this.dgvAkkonTrendData = new System.Windows.Forms.DataGridView();
            this.pnlTabs = new System.Windows.Forms.Panel();
            this.lblTab = new System.Windows.Forms.Label();
            this.tlpAkkonTrend.SuspendLayout();
            this.pnlAlignType.SuspendLayout();
            this.tlpData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAkkonTrendData)).BeginInit();
            this.pnlTabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAkkonTrend
            // 
            this.tlpAkkonTrend.ColumnCount = 1;
            this.tlpAkkonTrend.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAkkonTrend.Controls.Add(this.pnlAlignType, 0, 1);
            this.tlpAkkonTrend.Controls.Add(this.tlpData, 0, 2);
            this.tlpAkkonTrend.Controls.Add(this.pnlTabs, 0, 0);
            this.tlpAkkonTrend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAkkonTrend.Location = new System.Drawing.Point(0, 0);
            this.tlpAkkonTrend.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAkkonTrend.Name = "tlpAkkonTrend";
            this.tlpAkkonTrend.RowCount = 3;
            this.tlpAkkonTrend.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpAkkonTrend.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpAkkonTrend.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAkkonTrend.Size = new System.Drawing.Size(851, 498);
            this.tlpAkkonTrend.TabIndex = 1;
            // 
            // pnlAlignType
            // 
            this.pnlAlignType.Controls.Add(this.lblStd);
            this.pnlAlignType.Controls.Add(this.lblStrength);
            this.pnlAlignType.Controls.Add(this.lblLength);
            this.pnlAlignType.Controls.Add(this.lblCount);
            this.pnlAlignType.Controls.Add(this.lblAlign);
            this.pnlAlignType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAlignType.Location = new System.Drawing.Point(0, 60);
            this.pnlAlignType.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAlignType.Name = "pnlAlignType";
            this.pnlAlignType.Size = new System.Drawing.Size(851, 60);
            this.pnlAlignType.TabIndex = 2;
            // 
            // lblStd
            // 
            this.lblStd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStd.Location = new System.Drawing.Point(580, 0);
            this.lblStd.Margin = new System.Windows.Forms.Padding(0);
            this.lblStd.Name = "lblStd";
            this.lblStd.Size = new System.Drawing.Size(120, 60);
            this.lblStd.TabIndex = 4;
            this.lblStd.Text = "STD";
            this.lblStd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStrength
            // 
            this.lblStrength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStrength.Location = new System.Drawing.Point(440, 0);
            this.lblStrength.Margin = new System.Windows.Forms.Padding(0);
            this.lblStrength.Name = "lblStrength";
            this.lblStrength.Size = new System.Drawing.Size(120, 60);
            this.lblStrength.TabIndex = 3;
            this.lblStrength.Text = "Strength";
            this.lblStrength.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLength
            // 
            this.lblLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLength.Location = new System.Drawing.Point(300, 0);
            this.lblLength.Margin = new System.Windows.Forms.Padding(0);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(120, 60);
            this.lblLength.TabIndex = 2;
            this.lblLength.Text = "Length";
            this.lblLength.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCount
            // 
            this.lblCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCount.Location = new System.Drawing.Point(160, 0);
            this.lblCount.Margin = new System.Windows.Forms.Padding(0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(120, 60);
            this.lblCount.TabIndex = 1;
            this.lblCount.Text = "Count";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // 
            // tlpData
            // 
            this.tlpData.ColumnCount = 2;
            this.tlpData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tlpData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tlpData.Controls.Add(this.dgvAkkonTrendData, 1, 0);
            this.tlpData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpData.Location = new System.Drawing.Point(0, 120);
            this.tlpData.Margin = new System.Windows.Forms.Padding(0);
            this.tlpData.Name = "tlpData";
            this.tlpData.RowCount = 1;
            this.tlpData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpData.Size = new System.Drawing.Size(851, 378);
            this.tlpData.TabIndex = 3;
            // 
            // dgvAkkonTrendData
            // 
            this.dgvAkkonTrendData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvAkkonTrendData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAkkonTrendData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAkkonTrendData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAkkonTrendData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAkkonTrendData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAkkonTrendData.Location = new System.Drawing.Point(471, 3);
            this.dgvAkkonTrendData.Name = "dgvAkkonTrendData";
            this.dgvAkkonTrendData.ReadOnly = true;
            this.dgvAkkonTrendData.RowHeadersVisible = false;
            this.dgvAkkonTrendData.RowTemplate.Height = 23;
            this.dgvAkkonTrendData.Size = new System.Drawing.Size(377, 372);
            this.dgvAkkonTrendData.TabIndex = 0;
            // 
            // pnlTabs
            // 
            this.pnlTabs.Controls.Add(this.lblTab);
            this.pnlTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTabs.Location = new System.Drawing.Point(0, 0);
            this.pnlTabs.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTabs.Name = "pnlTabs";
            this.pnlTabs.Size = new System.Drawing.Size(851, 60);
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
            // AkkonTrendControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpAkkonTrend);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "AkkonTrendControl";
            this.Size = new System.Drawing.Size(851, 498);
            this.tlpAkkonTrend.ResumeLayout(false);
            this.pnlAlignType.ResumeLayout(false);
            this.tlpData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAkkonTrendData)).EndInit();
            this.pnlTabs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAkkonTrend;
        private System.Windows.Forms.Panel pnlAlignType;
        private System.Windows.Forms.Label lblStd;
        private System.Windows.Forms.Label lblStrength;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblAlign;
        private System.Windows.Forms.TableLayoutPanel tlpData;
        private System.Windows.Forms.DataGridView dgvAkkonTrendData;
        private System.Windows.Forms.Panel pnlTabs;
        private System.Windows.Forms.Label lblTab;
    }
}
