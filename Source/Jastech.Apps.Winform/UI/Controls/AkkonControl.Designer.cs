namespace Jastech.Apps.Winform.UI.Controls
{
    partial class AkkonControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlParam = new System.Windows.Forms.Panel();
            this.tlpAkkon = new System.Windows.Forms.TableLayoutPanel();
            this.tlpAkkonDataGridView = new System.Windows.Forms.TableLayoutPanel();
            this.tabAkkonData = new System.Windows.Forms.TabControl();
            this.tpAkkonROI = new System.Windows.Forms.TabPage();
            this.dgvAkkonROI = new System.Windows.Forms.DataGridView();
            this.colROINo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLeftTop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRightTop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRightBottom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLeftBottom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpAkkonResult = new System.Windows.Forms.TabPage();
            this.dgvAkkonResult = new System.Windows.Forms.DataGridView();
            this.colResultNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStrength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colJudgement = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlpAkkonDataShow = new System.Windows.Forms.TableLayoutPanel();
            this.rdoAkkonResult = new System.Windows.Forms.RadioButton();
            this.rdoAkkonROI = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblAddROI = new System.Windows.Forms.Label();
            this.lblPrev = new System.Windows.Forms.Label();
            this.lblNext = new System.Windows.Forms.Label();
            this.lblInspection = new System.Windows.Forms.Label();
            this.lblParameter = new System.Windows.Forms.Label();
            this.lblTab = new System.Windows.Forms.Label();
            this.tlpAkkon.SuspendLayout();
            this.tlpAkkonDataGridView.SuspendLayout();
            this.tabAkkonData.SuspendLayout();
            this.tpAkkonROI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAkkonROI)).BeginInit();
            this.tpAkkonResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAkkonResult)).BeginInit();
            this.tlpAkkonDataShow.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlParam
            // 
            this.pnlParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlParam.Location = new System.Drawing.Point(0, 327);
            this.pnlParam.Margin = new System.Windows.Forms.Padding(0);
            this.pnlParam.Name = "pnlParam";
            this.pnlParam.Size = new System.Drawing.Size(571, 248);
            this.pnlParam.TabIndex = 0;
            // 
            // tlpAkkon
            // 
            this.tlpAkkon.ColumnCount = 1;
            this.tlpAkkon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAkkon.Controls.Add(this.tlpAkkonDataGridView, 0, 2);
            this.tlpAkkon.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tlpAkkon.Controls.Add(this.pnlParam, 0, 3);
            this.tlpAkkon.Controls.Add(this.lblParameter, 0, 0);
            this.tlpAkkon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAkkon.Location = new System.Drawing.Point(0, 0);
            this.tlpAkkon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tlpAkkon.Name = "tlpAkkon";
            this.tlpAkkon.RowCount = 4;
            this.tlpAkkon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpAkkon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tlpAkkon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAkkon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAkkon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpAkkon.Size = new System.Drawing.Size(571, 575);
            this.tlpAkkon.TabIndex = 1;
            // 
            // tlpAkkonDataGridView
            // 
            this.tlpAkkonDataGridView.ColumnCount = 1;
            this.tlpAkkonDataGridView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAkkonDataGridView.Controls.Add(this.tabAkkonData, 0, 0);
            this.tlpAkkonDataGridView.Controls.Add(this.tlpAkkonDataShow, 0, 1);
            this.tlpAkkonDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAkkonDataGridView.Location = new System.Drawing.Point(0, 80);
            this.tlpAkkonDataGridView.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAkkonDataGridView.Name = "tlpAkkonDataGridView";
            this.tlpAkkonDataGridView.RowCount = 2;
            this.tlpAkkonDataGridView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAkkonDataGridView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpAkkonDataGridView.Size = new System.Drawing.Size(571, 247);
            this.tlpAkkonDataGridView.TabIndex = 294;
            // 
            // tabAkkonData
            // 
            this.tabAkkonData.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabAkkonData.Controls.Add(this.tpAkkonROI);
            this.tabAkkonData.Controls.Add(this.tpAkkonResult);
            this.tabAkkonData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabAkkonData.ItemSize = new System.Drawing.Size(0, 1);
            this.tabAkkonData.Location = new System.Drawing.Point(0, 0);
            this.tabAkkonData.Margin = new System.Windows.Forms.Padding(0);
            this.tabAkkonData.Name = "tabAkkonData";
            this.tabAkkonData.SelectedIndex = 0;
            this.tabAkkonData.Size = new System.Drawing.Size(571, 217);
            this.tabAkkonData.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabAkkonData.TabIndex = 292;
            // 
            // tpAkkonROI
            // 
            this.tpAkkonROI.Controls.Add(this.dgvAkkonROI);
            this.tpAkkonROI.Location = new System.Drawing.Point(4, 5);
            this.tpAkkonROI.Margin = new System.Windows.Forms.Padding(0);
            this.tpAkkonROI.Name = "tpAkkonROI";
            this.tpAkkonROI.Size = new System.Drawing.Size(563, 208);
            this.tpAkkonROI.TabIndex = 0;
            this.tpAkkonROI.Text = "tabPage1";
            this.tpAkkonROI.UseVisualStyleBackColor = true;
            // 
            // dgvAkkonROI
            // 
            this.dgvAkkonROI.AllowUserToAddRows = false;
            this.dgvAkkonROI.AllowUserToDeleteRows = false;
            this.dgvAkkonROI.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAkkonROI.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAkkonROI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAkkonROI.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colROINo,
            this.colLeftTop,
            this.colRightTop,
            this.colRightBottom,
            this.colLeftBottom});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAkkonROI.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAkkonROI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAkkonROI.Location = new System.Drawing.Point(0, 0);
            this.dgvAkkonROI.Margin = new System.Windows.Forms.Padding(0);
            this.dgvAkkonROI.Name = "dgvAkkonROI";
            this.dgvAkkonROI.ReadOnly = true;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.dgvAkkonROI.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAkkonROI.RowTemplate.Height = 23;
            this.dgvAkkonROI.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAkkonROI.Size = new System.Drawing.Size(563, 208);
            this.dgvAkkonROI.TabIndex = 0;
            // 
            // colROINo
            // 
            this.colROINo.FillWeight = 110F;
            this.colROINo.HeaderText = "NO";
            this.colROINo.MinimumWidth = 70;
            this.colROINo.Name = "colROINo";
            this.colROINo.ReadOnly = true;
            this.colROINo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colROINo.Width = 70;
            // 
            // colLeftTop
            // 
            this.colLeftTop.FillWeight = 140F;
            this.colLeftTop.HeaderText = "LEFT TOP";
            this.colLeftTop.MinimumWidth = 160;
            this.colLeftTop.Name = "colLeftTop";
            this.colLeftTop.ReadOnly = true;
            this.colLeftTop.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colLeftTop.Width = 160;
            // 
            // colRightTop
            // 
            this.colRightTop.FillWeight = 140F;
            this.colRightTop.HeaderText = "RIGHT TOP";
            this.colRightTop.MinimumWidth = 160;
            this.colRightTop.Name = "colRightTop";
            this.colRightTop.ReadOnly = true;
            this.colRightTop.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colRightTop.Width = 160;
            // 
            // colRightBottom
            // 
            this.colRightBottom.FillWeight = 140F;
            this.colRightBottom.HeaderText = "RIGHT BOTTOM";
            this.colRightBottom.MinimumWidth = 160;
            this.colRightBottom.Name = "colRightBottom";
            this.colRightBottom.ReadOnly = true;
            this.colRightBottom.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colRightBottom.Width = 160;
            // 
            // colLeftBottom
            // 
            this.colLeftBottom.FillWeight = 140F;
            this.colLeftBottom.HeaderText = "LEFT BOTTOM";
            this.colLeftBottom.MinimumWidth = 160;
            this.colLeftBottom.Name = "colLeftBottom";
            this.colLeftBottom.ReadOnly = true;
            this.colLeftBottom.Width = 160;
            // 
            // tpAkkonResult
            // 
            this.tpAkkonResult.Controls.Add(this.dgvAkkonResult);
            this.tpAkkonResult.Location = new System.Drawing.Point(4, 5);
            this.tpAkkonResult.Margin = new System.Windows.Forms.Padding(0);
            this.tpAkkonResult.Name = "tpAkkonResult";
            this.tpAkkonResult.Size = new System.Drawing.Size(563, 208);
            this.tpAkkonResult.TabIndex = 1;
            this.tpAkkonResult.Text = "tabPage2";
            this.tpAkkonResult.UseVisualStyleBackColor = true;
            // 
            // dgvAkkonResult
            // 
            this.dgvAkkonResult.AllowUserToAddRows = false;
            this.dgvAkkonResult.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAkkonResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvAkkonResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAkkonResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colResultNo,
            this.colCount,
            this.colLength,
            this.colStrength,
            this.colJudgement});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAkkonResult.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvAkkonResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAkkonResult.Location = new System.Drawing.Point(0, 0);
            this.dgvAkkonResult.Name = "dgvAkkonResult";
            this.dgvAkkonResult.ReadOnly = true;
            this.dgvAkkonResult.RowTemplate.Height = 23;
            this.dgvAkkonResult.Size = new System.Drawing.Size(563, 208);
            this.dgvAkkonResult.TabIndex = 1;
            // 
            // colResultNo
            // 
            this.colResultNo.HeaderText = "NO";
            this.colResultNo.MinimumWidth = 70;
            this.colResultNo.Name = "colResultNo";
            this.colResultNo.ReadOnly = true;
            this.colResultNo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colResultNo.Width = 80;
            // 
            // colCount
            // 
            this.colCount.HeaderText = "COUNT";
            this.colCount.MinimumWidth = 160;
            this.colCount.Name = "colCount";
            this.colCount.ReadOnly = true;
            this.colCount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colCount.Width = 160;
            // 
            // colLength
            // 
            this.colLength.HeaderText = "LENGTH";
            this.colLength.MinimumWidth = 160;
            this.colLength.Name = "colLength";
            this.colLength.ReadOnly = true;
            this.colLength.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colLength.Width = 160;
            // 
            // colStrength
            // 
            this.colStrength.HeaderText = "STRENGTH";
            this.colStrength.MinimumWidth = 160;
            this.colStrength.Name = "colStrength";
            this.colStrength.ReadOnly = true;
            this.colStrength.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colStrength.Width = 160;
            // 
            // colJudgement
            // 
            this.colJudgement.HeaderText = "JUDGEMENT";
            this.colJudgement.MinimumWidth = 160;
            this.colJudgement.Name = "colJudgement";
            this.colJudgement.ReadOnly = true;
            this.colJudgement.Width = 160;
            // 
            // tlpAkkonDataShow
            // 
            this.tlpAkkonDataShow.ColumnCount = 2;
            this.tlpAkkonDataShow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAkkonDataShow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAkkonDataShow.Controls.Add(this.rdoAkkonResult, 0, 0);
            this.tlpAkkonDataShow.Controls.Add(this.rdoAkkonROI, 0, 0);
            this.tlpAkkonDataShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAkkonDataShow.Location = new System.Drawing.Point(0, 217);
            this.tlpAkkonDataShow.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAkkonDataShow.Name = "tlpAkkonDataShow";
            this.tlpAkkonDataShow.RowCount = 1;
            this.tlpAkkonDataShow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAkkonDataShow.Size = new System.Drawing.Size(571, 30);
            this.tlpAkkonDataShow.TabIndex = 293;
            // 
            // rdoAkkonResult
            // 
            this.rdoAkkonResult.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoAkkonResult.BackColor = System.Drawing.Color.White;
            this.rdoAkkonResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdoAkkonResult.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.rdoAkkonResult.ForeColor = System.Drawing.Color.Black;
            this.rdoAkkonResult.Location = new System.Drawing.Point(285, 0);
            this.rdoAkkonResult.Margin = new System.Windows.Forms.Padding(0);
            this.rdoAkkonResult.Name = "rdoAkkonResult";
            this.rdoAkkonResult.Size = new System.Drawing.Size(286, 30);
            this.rdoAkkonResult.TabIndex = 144;
            this.rdoAkkonResult.Tag = "0";
            this.rdoAkkonResult.Text = "AKKON INSPECTION RESULT";
            this.rdoAkkonResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoAkkonResult.UseVisualStyleBackColor = false;
            // 
            // rdoAkkonROI
            // 
            this.rdoAkkonROI.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoAkkonROI.BackColor = System.Drawing.Color.White;
            this.rdoAkkonROI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdoAkkonROI.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.rdoAkkonROI.ForeColor = System.Drawing.Color.Black;
            this.rdoAkkonROI.Location = new System.Drawing.Point(0, 0);
            this.rdoAkkonROI.Margin = new System.Windows.Forms.Padding(0);
            this.rdoAkkonROI.Name = "rdoAkkonROI";
            this.rdoAkkonROI.Size = new System.Drawing.Size(285, 30);
            this.rdoAkkonROI.TabIndex = 143;
            this.rdoAkkonROI.Tag = "0";
            this.rdoAkkonROI.Text = "LEAD ROI LIST";
            this.rdoAkkonROI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoAkkonROI.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.lblTab, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblAddROI, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblPrev, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblNext, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblInspection, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 34);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(565, 44);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // lblAddROI
            // 
            this.lblAddROI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAddROI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAddROI.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblAddROI.Location = new System.Drawing.Point(425, 0);
            this.lblAddROI.Name = "lblAddROI";
            this.lblAddROI.Size = new System.Drawing.Size(137, 44);
            this.lblAddROI.TabIndex = 23;
            this.lblAddROI.Text = "Add ROI";
            this.lblAddROI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPrev
            // 
            this.lblPrev.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPrev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPrev.Image = global::Jastech.Apps.Winform.Properties.Resources.Prev;
            this.lblPrev.Location = new System.Drawing.Point(178, 0);
            this.lblPrev.Name = "lblPrev";
            this.lblPrev.Size = new System.Drawing.Size(46, 44);
            this.lblPrev.TabIndex = 5;
            // 
            // lblNext
            // 
            this.lblNext.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNext.Image = global::Jastech.Apps.Winform.Properties.Resources.Next;
            this.lblNext.Location = new System.Drawing.Point(230, 0);
            this.lblNext.Name = "lblNext";
            this.lblNext.Size = new System.Drawing.Size(46, 44);
            this.lblNext.TabIndex = 2;
            // 
            // lblInspection
            // 
            this.lblInspection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInspection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInspection.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblInspection.Location = new System.Drawing.Point(282, 0);
            this.lblInspection.Name = "lblInspection";
            this.lblInspection.Size = new System.Drawing.Size(137, 44);
            this.lblInspection.TabIndex = 22;
            this.lblInspection.Text = "Inspect";
            this.lblInspection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblParameter
            // 
            this.lblParameter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(175)))), ((int)(((byte)(223)))));
            this.lblParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblParameter.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblParameter.Location = new System.Drawing.Point(0, 0);
            this.lblParameter.Margin = new System.Windows.Forms.Padding(0);
            this.lblParameter.Name = "lblParameter";
            this.lblParameter.Size = new System.Drawing.Size(571, 32);
            this.lblParameter.TabIndex = 1;
            this.lblParameter.Text = "Parameter";
            this.lblParameter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTab
            // 
            this.lblTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTab.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTab.Location = new System.Drawing.Point(3, 0);
            this.lblTab.Name = "lblTab";
            this.lblTab.Size = new System.Drawing.Size(169, 44);
            this.lblTab.TabIndex = 24;
            this.lblTab.Text = "TAB : 1";
            this.lblTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AkkonControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tlpAkkon);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.Name = "AkkonControl";
            this.Size = new System.Drawing.Size(571, 575);
            this.Load += new System.EventHandler(this.AkkonControl_Load);
            this.tlpAkkon.ResumeLayout(false);
            this.tlpAkkonDataGridView.ResumeLayout(false);
            this.tabAkkonData.ResumeLayout(false);
            this.tpAkkonROI.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAkkonROI)).EndInit();
            this.tpAkkonResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAkkonResult)).EndInit();
            this.tlpAkkonDataShow.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlParam;
        private System.Windows.Forms.TableLayoutPanel tlpAkkon;
        private System.Windows.Forms.Label lblParameter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblAddROI;
        private System.Windows.Forms.Label lblPrev;
        private System.Windows.Forms.Label lblNext;
        private System.Windows.Forms.Label lblInspection;
        private System.Windows.Forms.TableLayoutPanel tlpAkkonDataGridView;
        private System.Windows.Forms.TabControl tabAkkonData;
        private System.Windows.Forms.TabPage tpAkkonROI;
        private System.Windows.Forms.DataGridView dgvAkkonROI;
        private System.Windows.Forms.DataGridViewTextBoxColumn colROINo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLeftTop;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRightTop;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRightBottom;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLeftBottom;
        private System.Windows.Forms.TabPage tpAkkonResult;
        private System.Windows.Forms.DataGridView dgvAkkonResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn colResultNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStrength;
        private System.Windows.Forms.DataGridViewTextBoxColumn colJudgement;
        private System.Windows.Forms.TableLayoutPanel tlpAkkonDataShow;
        private System.Windows.Forms.RadioButton rdoAkkonResult;
        private System.Windows.Forms.RadioButton rdoAkkonROI;
        private System.Windows.Forms.Label lblTab;
    }
}
