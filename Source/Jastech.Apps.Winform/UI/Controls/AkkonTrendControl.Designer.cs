﻿namespace Jastech.Apps.Winform.UI.Controls
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpAkkonTrend = new System.Windows.Forms.TableLayoutPanel();
            this.pnlAkkonType = new System.Windows.Forms.Panel();
            this.lblStd = new System.Windows.Forms.Label();
            this.lblStrength = new System.Windows.Forms.Label();
            this.lblLength = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblAkkon = new System.Windows.Forms.Label();
            this.tlpData = new System.Windows.Forms.TableLayoutPanel();
            this.dgvAkkonTrendData = new System.Windows.Forms.DataGridView();
            this.pnlTabs = new System.Windows.Forms.Panel();
            this.lblTab = new System.Windows.Forms.Label();
            this.pnlChart = new System.Windows.Forms.Panel();
            this.tlpAkkonTrend.SuspendLayout();
            this.pnlAkkonType.SuspendLayout();
            this.tlpData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAkkonTrendData)).BeginInit();
            this.pnlTabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAkkonTrend
            // 
            this.tlpAkkonTrend.ColumnCount = 1;
            this.tlpAkkonTrend.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAkkonTrend.Controls.Add(this.pnlAkkonType, 0, 1);
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
            this.tlpAkkonTrend.Size = new System.Drawing.Size(860, 540);
            this.tlpAkkonTrend.TabIndex = 1;
            // 
            // pnlAkkonType
            // 
            this.pnlAkkonType.Controls.Add(this.lblStd);
            this.pnlAkkonType.Controls.Add(this.lblStrength);
            this.pnlAkkonType.Controls.Add(this.lblLength);
            this.pnlAkkonType.Controls.Add(this.lblCount);
            this.pnlAkkonType.Controls.Add(this.lblAkkon);
            this.pnlAkkonType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAkkonType.Location = new System.Drawing.Point(0, 60);
            this.pnlAkkonType.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAkkonType.Name = "pnlAkkonType";
            this.pnlAkkonType.Size = new System.Drawing.Size(860, 60);
            this.pnlAkkonType.TabIndex = 2;
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
            this.lblStd.Click += new System.EventHandler(this.lblStd_Click);
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
            this.lblStrength.Click += new System.EventHandler(this.lblStrength_Click);
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
            this.lblLength.Click += new System.EventHandler(this.lblLength_Click);
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
            this.lblCount.Click += new System.EventHandler(this.lblCount_Click);
            // 
            // lblAkkon
            // 
            this.lblAkkon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAkkon.Location = new System.Drawing.Point(0, 0);
            this.lblAkkon.Margin = new System.Windows.Forms.Padding(0);
            this.lblAkkon.Name = "lblAkkon";
            this.lblAkkon.Size = new System.Drawing.Size(120, 60);
            this.lblAkkon.TabIndex = 0;
            this.lblAkkon.Text = "Akkon";
            this.lblAkkon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAkkon.Click += new System.EventHandler(this.lblAkkon_Click);
            // 
            // tlpData
            // 
            this.tlpData.ColumnCount = 2;
            this.tlpData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tlpData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tlpData.Controls.Add(this.pnlChart, 0, 0);
            this.tlpData.Controls.Add(this.dgvAkkonTrendData, 1, 0);
            this.tlpData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpData.Location = new System.Drawing.Point(0, 120);
            this.tlpData.Margin = new System.Windows.Forms.Padding(0);
            this.tlpData.Name = "tlpData";
            this.tlpData.RowCount = 1;
            this.tlpData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpData.Size = new System.Drawing.Size(860, 420);
            this.tlpData.TabIndex = 3;
            // 
            // dgvAkkonTrendData
            // 
            this.dgvAkkonTrendData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvAkkonTrendData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAkkonTrendData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvAkkonTrendData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAkkonTrendData.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvAkkonTrendData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAkkonTrendData.Location = new System.Drawing.Point(476, 3);
            this.dgvAkkonTrendData.Name = "dgvAkkonTrendData";
            this.dgvAkkonTrendData.ReadOnly = true;
            this.dgvAkkonTrendData.RowHeadersVisible = false;
            this.dgvAkkonTrendData.RowTemplate.Height = 23;
            this.dgvAkkonTrendData.Size = new System.Drawing.Size(381, 414);
            this.dgvAkkonTrendData.TabIndex = 0;
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
            this.pnlChart.TabIndex = 2;
            // 
            // AkkonTrendControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpAkkonTrend);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "AkkonTrendControl";
            this.Size = new System.Drawing.Size(860, 540);
            this.Load += new System.EventHandler(this.AkkonTrendControl_Load);
            this.tlpAkkonTrend.ResumeLayout(false);
            this.pnlAkkonType.ResumeLayout(false);
            this.tlpData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAkkonTrendData)).EndInit();
            this.pnlTabs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAkkonTrend;
        private System.Windows.Forms.Panel pnlAkkonType;
        private System.Windows.Forms.Label lblStd;
        private System.Windows.Forms.Label lblStrength;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblAkkon;
        private System.Windows.Forms.TableLayoutPanel tlpData;
        private System.Windows.Forms.DataGridView dgvAkkonTrendData;
        private System.Windows.Forms.Panel pnlTabs;
        private System.Windows.Forms.Label lblTab;
        private System.Windows.Forms.Panel pnlChart;
    }
}
