using Jastech.Framework.Winform.Controls;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpAlignTrend = new System.Windows.Forms.TableLayoutPanel();
            this.tlpData = new System.Windows.Forms.TableLayoutPanel();
            this.dgvAlignTrendData = new Jastech.Framework.Winform.Controls.DoubleBufferedDatagridView();
            this.pnlChart = new System.Windows.Forms.Panel();
            this.tclChartType = new System.Windows.Forms.TableLayoutPanel();
            this.lblChartType = new System.Windows.Forms.Label();
            this.pnlChartTypes = new System.Windows.Forms.Panel();
            this.lblRy = new System.Windows.Forms.Label();
            this.lblRx = new System.Windows.Forms.Label();
            this.lblCx = new System.Windows.Forms.Label();
            this.lblLy = new System.Windows.Forms.Label();
            this.lblLx = new System.Windows.Forms.Label();
            this.lblAllData = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTabs = new System.Windows.Forms.Panel();
            this.lblTabSelection = new System.Windows.Forms.Label();
            this.tlpAlignTrend.SuspendLayout();
            this.tlpData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlignTrendData)).BeginInit();
            this.tclChartType.SuspendLayout();
            this.pnlChartTypes.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAlignTrend
            // 
            this.tlpAlignTrend.ColumnCount = 1;
            this.tlpAlignTrend.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlignTrend.Controls.Add(this.tlpData, 0, 3);
            this.tlpAlignTrend.Controls.Add(this.tclChartType, 0, 2);
            this.tlpAlignTrend.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tlpAlignTrend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAlignTrend.Location = new System.Drawing.Point(0, 0);
            this.tlpAlignTrend.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAlignTrend.Name = "tlpAlignTrend";
            this.tlpAlignTrend.RowCount = 4;
            this.tlpAlignTrend.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpAlignTrend.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tlpAlignTrend.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpAlignTrend.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlignTrend.Size = new System.Drawing.Size(860, 540);
            this.tlpAlignTrend.TabIndex = 0;
            // 
            // tlpData
            // 
            this.tlpData.ColumnCount = 2;
            this.tlpData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tlpData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tlpData.Controls.Add(this.dgvAlignTrendData, 1, 0);
            this.tlpData.Controls.Add(this.pnlChart, 0, 0);
            this.tlpData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpData.Location = new System.Drawing.Point(0, 135);
            this.tlpData.Margin = new System.Windows.Forms.Padding(0);
            this.tlpData.Name = "tlpData";
            this.tlpData.RowCount = 1;
            this.tlpData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpData.Size = new System.Drawing.Size(860, 405);
            this.tlpData.TabIndex = 3;
            // 
            // dgvAlignTrendData
            // 
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
            this.dgvAlignTrendData.Location = new System.Drawing.Point(476, 3);
            this.dgvAlignTrendData.Name = "dgvAlignTrendData";
            this.dgvAlignTrendData.ReadOnly = true;
            this.dgvAlignTrendData.RowHeadersVisible = false;
            this.dgvAlignTrendData.RowTemplate.Height = 23;
            this.dgvAlignTrendData.Size = new System.Drawing.Size(381, 399);
            this.dgvAlignTrendData.TabIndex = 0;
            // 
            // pnlChart
            // 
            this.pnlChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChart.Location = new System.Drawing.Point(0, 0);
            this.pnlChart.Margin = new System.Windows.Forms.Padding(0);
            this.pnlChart.Name = "pnlChart";
            this.pnlChart.Size = new System.Drawing.Size(473, 405);
            this.pnlChart.TabIndex = 1;
            // 
            // tclChartType
            // 
            this.tclChartType.ColumnCount = 2;
            this.tclChartType.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tclChartType.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tclChartType.Controls.Add(this.lblChartType, 0, 0);
            this.tclChartType.Controls.Add(this.pnlChartTypes, 1, 0);
            this.tclChartType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tclChartType.Location = new System.Drawing.Point(3, 78);
            this.tclChartType.Name = "tclChartType";
            this.tclChartType.RowCount = 1;
            this.tclChartType.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tclChartType.Size = new System.Drawing.Size(854, 54);
            this.tclChartType.TabIndex = 5;
            // 
            // lblChartType
            // 
            this.lblChartType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblChartType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblChartType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChartType.Location = new System.Drawing.Point(0, 0);
            this.lblChartType.Margin = new System.Windows.Forms.Padding(0);
            this.lblChartType.Name = "lblChartType";
            this.lblChartType.Size = new System.Drawing.Size(120, 54);
            this.lblChartType.TabIndex = 7;
            this.lblChartType.Text = "Chart Type";
            this.lblChartType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlChartTypes
            // 
            this.pnlChartTypes.Controls.Add(this.lblRy);
            this.pnlChartTypes.Controls.Add(this.lblRx);
            this.pnlChartTypes.Controls.Add(this.lblCx);
            this.pnlChartTypes.Controls.Add(this.lblLy);
            this.pnlChartTypes.Controls.Add(this.lblLx);
            this.pnlChartTypes.Controls.Add(this.lblAllData);
            this.pnlChartTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChartTypes.Location = new System.Drawing.Point(120, 0);
            this.pnlChartTypes.Margin = new System.Windows.Forms.Padding(0);
            this.pnlChartTypes.Name = "pnlChartTypes";
            this.pnlChartTypes.Size = new System.Drawing.Size(734, 54);
            this.pnlChartTypes.TabIndex = 2;
            // 
            // lblRy
            // 
            this.lblRy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRy.Location = new System.Drawing.Point(570, 0);
            this.lblRy.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblRy.Name = "lblRy";
            this.lblRy.Size = new System.Drawing.Size(100, 54);
            this.lblRy.TabIndex = 5;
            this.lblRy.Text = "Ry";
            this.lblRy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRy.Click += new System.EventHandler(this.lblRy_Click);
            // 
            // lblRx
            // 
            this.lblRx.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRx.Location = new System.Drawing.Point(460, 0);
            this.lblRx.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblRx.Name = "lblRx";
            this.lblRx.Size = new System.Drawing.Size(100, 54);
            this.lblRx.TabIndex = 4;
            this.lblRx.Text = "Rx";
            this.lblRx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRx.Click += new System.EventHandler(this.lblRx_Click);
            // 
            // lblCx
            // 
            this.lblCx.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCx.Location = new System.Drawing.Point(350, 0);
            this.lblCx.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblCx.Name = "lblCx";
            this.lblCx.Size = new System.Drawing.Size(100, 54);
            this.lblCx.TabIndex = 3;
            this.lblCx.Text = "Cx";
            this.lblCx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCx.Click += new System.EventHandler(this.lblCx_Click);
            // 
            // lblLy
            // 
            this.lblLy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLy.Location = new System.Drawing.Point(240, 0);
            this.lblLy.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblLy.Name = "lblLy";
            this.lblLy.Size = new System.Drawing.Size(100, 54);
            this.lblLy.TabIndex = 2;
            this.lblLy.Text = "Ly";
            this.lblLy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLy.Click += new System.EventHandler(this.lblLy_Click);
            // 
            // lblLx
            // 
            this.lblLx.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLx.Location = new System.Drawing.Point(130, 0);
            this.lblLx.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblLx.Name = "lblLx";
            this.lblLx.Size = new System.Drawing.Size(100, 54);
            this.lblLx.TabIndex = 1;
            this.lblLx.Text = "Lx";
            this.lblLx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLx.Click += new System.EventHandler(this.lblLx_Click);
            // 
            // lblAllData
            // 
            this.lblAllData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAllData.Location = new System.Drawing.Point(20, 0);
            this.lblAllData.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.lblAllData.Name = "lblAllData";
            this.lblAllData.Size = new System.Drawing.Size(100, 54);
            this.lblAllData.TabIndex = 0;
            this.lblAllData.Text = "All Data";
            this.lblAllData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAllData.Click += new System.EventHandler(this.lblAllData_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.pnlTabs, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblTabSelection, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(854, 54);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // pnlTabs
            // 
            this.pnlTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTabs.Location = new System.Drawing.Point(120, 0);
            this.pnlTabs.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTabs.Name = "pnlTabs";
            this.pnlTabs.Size = new System.Drawing.Size(734, 54);
            this.pnlTabs.TabIndex = 7;
            // 
            // lblTabSelection
            // 
            this.lblTabSelection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblTabSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTabSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTabSelection.Location = new System.Drawing.Point(0, 0);
            this.lblTabSelection.Margin = new System.Windows.Forms.Padding(0);
            this.lblTabSelection.Name = "lblTabSelection";
            this.lblTabSelection.Size = new System.Drawing.Size(120, 54);
            this.lblTabSelection.TabIndex = 6;
            this.lblTabSelection.Text = "Tab Selection";
            this.lblTabSelection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.tlpData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlignTrendData)).EndInit();
            this.tclChartType.ResumeLayout(false);
            this.pnlChartTypes.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAlignTrend;
        private System.Windows.Forms.Panel pnlChartTypes;
        private System.Windows.Forms.Label lblRy;
        private System.Windows.Forms.Label lblRx;
        private System.Windows.Forms.Label lblCx;
        private System.Windows.Forms.Label lblLy;
        private System.Windows.Forms.Label lblLx;
        private System.Windows.Forms.Label lblAllData;
        private System.Windows.Forms.TableLayoutPanel tlpData;
        private DoubleBufferedDatagridView dgvAlignTrendData;
        private System.Windows.Forms.Label lblTabSelection;
        private System.Windows.Forms.Panel pnlChart;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tclChartType;
        private System.Windows.Forms.Label lblChartType;
        private System.Windows.Forms.Panel pnlTabs;
    }
}
