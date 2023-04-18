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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlROIData = new System.Windows.Forms.Panel();
            this.dgvAkkonROI = new System.Windows.Forms.DataGridView();
            this.colROINo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLeftTop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRightTop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLeftBottom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRightBottom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlGroup = new System.Windows.Forms.Panel();
            this.lblSort = new System.Windows.Forms.Label();
            this.lblDelete = new System.Windows.Forms.Label();
            this.lblRegister = new System.Windows.Forms.Label();
            this.lblCloneExecute = new System.Windows.Forms.Label();
            this.lblROIHeightValue = new System.Windows.Forms.Label();
            this.lblCloneHorizontal = new System.Windows.Forms.Label();
            this.cmbGroupNumber = new System.Windows.Forms.ComboBox();
            this.lblCloneVertical = new System.Windows.Forms.Label();
            this.lblClone = new System.Windows.Forms.Label();
            this.lblGroupCountValue = new System.Windows.Forms.Label();
            this.lblGroupNumber = new System.Windows.Forms.Label();
            this.lblGroupCount = new System.Windows.Forms.Label();
            this.lblROIHeight = new System.Windows.Forms.Label();
            this.lblLeadPitchValue = new System.Windows.Forms.Label();
            this.lblROIWidthValue = new System.Windows.Forms.Label();
            this.lblLeadCount = new System.Windows.Forms.Label();
            this.lblROIWidth = new System.Windows.Forms.Label();
            this.lblLeadCountValue = new System.Windows.Forms.Label();
            this.lblLeadPitch = new System.Windows.Forms.Label();
            this.dgvAkkonResult = new System.Windows.Forms.DataGridView();
            this.colResultNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStrength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colJudgement = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblNext = new System.Windows.Forms.Label();
            this.lblPrev = new System.Windows.Forms.Label();
            this.cmbTabList = new System.Windows.Forms.ComboBox();
            this.lblAddROI = new System.Windows.Forms.Label();
            this.lblInspection = new System.Windows.Forms.Label();
            this.lblROIJog = new System.Windows.Forms.Label();
            this.lblParameter = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblGroup = new System.Windows.Forms.Label();
            this.tlpAkkon.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlROIData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAkkonROI)).BeginInit();
            this.panel2.SuspendLayout();
            this.pnlGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAkkonResult)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlParam
            // 
            this.pnlParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlParam.Location = new System.Drawing.Point(0, 482);
            this.pnlParam.Margin = new System.Windows.Forms.Padding(0);
            this.pnlParam.Name = "pnlParam";
            this.pnlParam.Size = new System.Drawing.Size(965, 400);
            this.pnlParam.TabIndex = 0;
            // 
            // tlpAkkon
            // 
            this.tlpAkkon.ColumnCount = 1;
            this.tlpAkkon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAkkon.Controls.Add(this.tableLayoutPanel1, 0, 3);
            this.tlpAkkon.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tlpAkkon.Controls.Add(this.pnlParam, 0, 4);
            this.tlpAkkon.Controls.Add(this.lblParameter, 0, 0);
            this.tlpAkkon.Controls.Add(this.panel1, 0, 2);
            this.tlpAkkon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAkkon.Location = new System.Drawing.Point(0, 0);
            this.tlpAkkon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tlpAkkon.Name = "tlpAkkon";
            this.tlpAkkon.RowCount = 6;
            this.tlpAkkon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpAkkon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpAkkon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpAkkon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 340F));
            this.tlpAkkon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tlpAkkon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAkkon.Size = new System.Drawing.Size(965, 762);
            this.tlpAkkon.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.pnlROIData, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 142);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(965, 340);
            this.tableLayoutPanel1.TabIndex = 294;
            // 
            // pnlROIData
            // 
            this.pnlROIData.Controls.Add(this.dgvAkkonROI);
            this.pnlROIData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlROIData.Location = new System.Drawing.Point(482, 6);
            this.pnlROIData.Margin = new System.Windows.Forms.Padding(0);
            this.pnlROIData.Name = "pnlROIData";
            this.pnlROIData.Size = new System.Drawing.Size(483, 328);
            this.pnlROIData.TabIndex = 0;
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
            this.colLeftBottom,
            this.colRightBottom});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAkkonROI.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAkkonROI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAkkonROI.Location = new System.Drawing.Point(0, 0);
            this.dgvAkkonROI.Margin = new System.Windows.Forms.Padding(0);
            this.dgvAkkonROI.Name = "dgvAkkonROI";
            this.dgvAkkonROI.ReadOnly = true;
            this.dgvAkkonROI.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.dgvAkkonROI.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAkkonROI.RowTemplate.Height = 23;
            this.dgvAkkonROI.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAkkonROI.Size = new System.Drawing.Size(483, 328);
            this.dgvAkkonROI.TabIndex = 0;
            this.dgvAkkonROI.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAkkonROI_CellClick);
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
            // colLeftBottom
            // 
            this.colLeftBottom.FillWeight = 140F;
            this.colLeftBottom.HeaderText = "LEFT BOTTOM";
            this.colLeftBottom.MinimumWidth = 160;
            this.colLeftBottom.Name = "colLeftBottom";
            this.colLeftBottom.ReadOnly = true;
            this.colLeftBottom.Width = 160;
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
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlGroup);
            this.panel2.Controls.Add(this.dgvAkkonResult);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 6);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(482, 328);
            this.panel2.TabIndex = 1;
            // 
            // pnlGroup
            // 
            this.pnlGroup.Controls.Add(this.lblSort);
            this.pnlGroup.Controls.Add(this.lblDelete);
            this.pnlGroup.Controls.Add(this.lblRegister);
            this.pnlGroup.Controls.Add(this.lblCloneExecute);
            this.pnlGroup.Controls.Add(this.lblROIHeightValue);
            this.pnlGroup.Controls.Add(this.lblCloneHorizontal);
            this.pnlGroup.Controls.Add(this.cmbGroupNumber);
            this.pnlGroup.Controls.Add(this.lblCloneVertical);
            this.pnlGroup.Controls.Add(this.lblClone);
            this.pnlGroup.Controls.Add(this.lblGroupCountValue);
            this.pnlGroup.Controls.Add(this.lblGroupNumber);
            this.pnlGroup.Controls.Add(this.lblGroupCount);
            this.pnlGroup.Controls.Add(this.lblROIHeight);
            this.pnlGroup.Controls.Add(this.lblLeadPitchValue);
            this.pnlGroup.Controls.Add(this.lblROIWidthValue);
            this.pnlGroup.Controls.Add(this.lblLeadCount);
            this.pnlGroup.Controls.Add(this.lblROIWidth);
            this.pnlGroup.Controls.Add(this.lblLeadCountValue);
            this.pnlGroup.Controls.Add(this.lblLeadPitch);
            this.pnlGroup.Location = new System.Drawing.Point(81, 0);
            this.pnlGroup.Margin = new System.Windows.Forms.Padding(0);
            this.pnlGroup.Name = "pnlGroup";
            this.pnlGroup.Size = new System.Drawing.Size(497, 330);
            this.pnlGroup.TabIndex = 2;
            // 
            // lblSort
            // 
            this.lblSort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblSort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSort.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblSort.ForeColor = System.Drawing.Color.White;
            this.lblSort.Location = new System.Drawing.Point(260, 144);
            this.lblSort.Margin = new System.Windows.Forms.Padding(0);
            this.lblSort.Name = "lblSort";
            this.lblSort.Size = new System.Drawing.Size(140, 40);
            this.lblSort.TabIndex = 149;
            this.lblSort.Text = "Sort";
            this.lblSort.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSort.Click += new System.EventHandler(this.lblSort_Click);
            // 
            // lblDelete
            // 
            this.lblDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblDelete.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDelete.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblDelete.ForeColor = System.Drawing.Color.White;
            this.lblDelete.Location = new System.Drawing.Point(260, 190);
            this.lblDelete.Margin = new System.Windows.Forms.Padding(0);
            this.lblDelete.Name = "lblDelete";
            this.lblDelete.Size = new System.Drawing.Size(140, 40);
            this.lblDelete.TabIndex = 148;
            this.lblDelete.Text = "Delete";
            this.lblDelete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDelete.Click += new System.EventHandler(this.lblDelete_Click);
            // 
            // lblRegister
            // 
            this.lblRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblRegister.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRegister.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblRegister.ForeColor = System.Drawing.Color.White;
            this.lblRegister.Location = new System.Drawing.Point(260, 98);
            this.lblRegister.Margin = new System.Windows.Forms.Padding(0);
            this.lblRegister.Name = "lblRegister";
            this.lblRegister.Size = new System.Drawing.Size(140, 40);
            this.lblRegister.TabIndex = 147;
            this.lblRegister.Text = "ROI Register";
            this.lblRegister.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRegister.Click += new System.EventHandler(this.lblRegister_Click);
            // 
            // lblCloneExecute
            // 
            this.lblCloneExecute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblCloneExecute.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCloneExecute.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblCloneExecute.ForeColor = System.Drawing.Color.White;
            this.lblCloneExecute.Location = new System.Drawing.Point(297, 282);
            this.lblCloneExecute.Margin = new System.Windows.Forms.Padding(0);
            this.lblCloneExecute.Name = "lblCloneExecute";
            this.lblCloneExecute.Size = new System.Drawing.Size(140, 40);
            this.lblCloneExecute.TabIndex = 146;
            this.lblCloneExecute.Text = "Execute";
            this.lblCloneExecute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCloneExecute.Click += new System.EventHandler(this.lblCloneExecute_Click);
            // 
            // lblROIHeightValue
            // 
            this.lblROIHeightValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblROIHeightValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblROIHeightValue.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblROIHeightValue.ForeColor = System.Drawing.Color.White;
            this.lblROIHeightValue.Location = new System.Drawing.Point(154, 236);
            this.lblROIHeightValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblROIHeightValue.Name = "lblROIHeightValue";
            this.lblROIHeightValue.Size = new System.Drawing.Size(80, 40);
            this.lblROIHeightValue.TabIndex = 17;
            this.lblROIHeightValue.Text = "0";
            this.lblROIHeightValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblROIHeightValue.Click += new System.EventHandler(this.lblROIHeightValue_Click);
            // 
            // lblCloneHorizontal
            // 
            this.lblCloneHorizontal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblCloneHorizontal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCloneHorizontal.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblCloneHorizontal.ForeColor = System.Drawing.Color.White;
            this.lblCloneHorizontal.Image = global::Jastech.Apps.Winform.Properties.Resources.Copy_Horizontal_White;
            this.lblCloneHorizontal.Location = new System.Drawing.Point(225, 282);
            this.lblCloneHorizontal.Margin = new System.Windows.Forms.Padding(0);
            this.lblCloneHorizontal.Name = "lblCloneHorizontal";
            this.lblCloneHorizontal.Size = new System.Drawing.Size(60, 40);
            this.lblCloneHorizontal.TabIndex = 145;
            this.lblCloneHorizontal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCloneHorizontal.Click += new System.EventHandler(this.lblCloneHorizontal_Click);
            // 
            // cmbGroupNumber
            // 
            this.cmbGroupNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.cmbGroupNumber.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGroupNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroupNumber.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbGroupNumber.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.cmbGroupNumber.ForeColor = System.Drawing.Color.White;
            this.cmbGroupNumber.FormattingEnabled = true;
            this.cmbGroupNumber.IntegralHeight = false;
            this.cmbGroupNumber.ItemHeight = 22;
            this.cmbGroupNumber.Location = new System.Drawing.Point(154, 52);
            this.cmbGroupNumber.Margin = new System.Windows.Forms.Padding(0);
            this.cmbGroupNumber.Name = "cmbGroupNumber";
            this.cmbGroupNumber.Size = new System.Drawing.Size(80, 28);
            this.cmbGroupNumber.TabIndex = 26;
            this.cmbGroupNumber.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbGroupNumber.SelectedIndexChanged += new System.EventHandler(this.cmbGroupNumber_SelectedIndexChanged);
            // 
            // lblCloneVertical
            // 
            this.lblCloneVertical.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblCloneVertical.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCloneVertical.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblCloneVertical.ForeColor = System.Drawing.Color.White;
            this.lblCloneVertical.Image = global::Jastech.Apps.Winform.Properties.Resources.Copy_Vertical_White;
            this.lblCloneVertical.Location = new System.Drawing.Point(154, 282);
            this.lblCloneVertical.Margin = new System.Windows.Forms.Padding(0);
            this.lblCloneVertical.Name = "lblCloneVertical";
            this.lblCloneVertical.Size = new System.Drawing.Size(60, 40);
            this.lblCloneVertical.TabIndex = 145;
            this.lblCloneVertical.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCloneVertical.Click += new System.EventHandler(this.lblCloneVertical_Click);
            // 
            // lblClone
            // 
            this.lblClone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblClone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblClone.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblClone.ForeColor = System.Drawing.Color.White;
            this.lblClone.Location = new System.Drawing.Point(6, 282);
            this.lblClone.Margin = new System.Windows.Forms.Padding(0);
            this.lblClone.Name = "lblClone";
            this.lblClone.Size = new System.Drawing.Size(140, 40);
            this.lblClone.TabIndex = 27;
            this.lblClone.Text = "Clone";
            this.lblClone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGroupCountValue
            // 
            this.lblGroupCountValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblGroupCountValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGroupCountValue.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblGroupCountValue.ForeColor = System.Drawing.Color.White;
            this.lblGroupCountValue.Location = new System.Drawing.Point(154, 6);
            this.lblGroupCountValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblGroupCountValue.Name = "lblGroupCountValue";
            this.lblGroupCountValue.Size = new System.Drawing.Size(80, 40);
            this.lblGroupCountValue.TabIndex = 8;
            this.lblGroupCountValue.Text = "0";
            this.lblGroupCountValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGroupCountValue.Click += new System.EventHandler(this.lblGroupCountValue_Click);
            // 
            // lblGroupNumber
            // 
            this.lblGroupNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblGroupNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGroupNumber.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblGroupNumber.ForeColor = System.Drawing.Color.White;
            this.lblGroupNumber.Location = new System.Drawing.Point(6, 52);
            this.lblGroupNumber.Margin = new System.Windows.Forms.Padding(0);
            this.lblGroupNumber.Name = "lblGroupNumber";
            this.lblGroupNumber.Size = new System.Drawing.Size(140, 40);
            this.lblGroupNumber.TabIndex = 9;
            this.lblGroupNumber.Text = "Group No.";
            this.lblGroupNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGroupCount
            // 
            this.lblGroupCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblGroupCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGroupCount.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblGroupCount.ForeColor = System.Drawing.Color.White;
            this.lblGroupCount.Location = new System.Drawing.Point(6, 6);
            this.lblGroupCount.Margin = new System.Windows.Forms.Padding(0);
            this.lblGroupCount.Name = "lblGroupCount";
            this.lblGroupCount.Size = new System.Drawing.Size(140, 40);
            this.lblGroupCount.TabIndex = 1;
            this.lblGroupCount.Text = "Group Count";
            this.lblGroupCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblROIHeight
            // 
            this.lblROIHeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblROIHeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblROIHeight.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblROIHeight.ForeColor = System.Drawing.Color.White;
            this.lblROIHeight.Location = new System.Drawing.Point(6, 236);
            this.lblROIHeight.Margin = new System.Windows.Forms.Padding(0);
            this.lblROIHeight.Name = "lblROIHeight";
            this.lblROIHeight.Size = new System.Drawing.Size(140, 40);
            this.lblROIHeight.TabIndex = 11;
            this.lblROIHeight.Text = "ROI Height (㎛)";
            this.lblROIHeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLeadPitchValue
            // 
            this.lblLeadPitchValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblLeadPitchValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeadPitchValue.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblLeadPitchValue.ForeColor = System.Drawing.Color.White;
            this.lblLeadPitchValue.Location = new System.Drawing.Point(154, 144);
            this.lblLeadPitchValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblLeadPitchValue.Name = "lblLeadPitchValue";
            this.lblLeadPitchValue.Size = new System.Drawing.Size(80, 40);
            this.lblLeadPitchValue.TabIndex = 18;
            this.lblLeadPitchValue.Text = "0";
            this.lblLeadPitchValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLeadPitchValue.Click += new System.EventHandler(this.lblLeadPitchValue_Click);
            // 
            // lblROIWidthValue
            // 
            this.lblROIWidthValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblROIWidthValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblROIWidthValue.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblROIWidthValue.ForeColor = System.Drawing.Color.White;
            this.lblROIWidthValue.Location = new System.Drawing.Point(154, 190);
            this.lblROIWidthValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblROIWidthValue.Name = "lblROIWidthValue";
            this.lblROIWidthValue.Size = new System.Drawing.Size(80, 40);
            this.lblROIWidthValue.TabIndex = 16;
            this.lblROIWidthValue.Text = "0";
            this.lblROIWidthValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblROIWidthValue.Click += new System.EventHandler(this.lblROIWidthValue_Click);
            // 
            // lblLeadCount
            // 
            this.lblLeadCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblLeadCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeadCount.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblLeadCount.ForeColor = System.Drawing.Color.White;
            this.lblLeadCount.Location = new System.Drawing.Point(6, 98);
            this.lblLeadCount.Margin = new System.Windows.Forms.Padding(0);
            this.lblLeadCount.Name = "lblLeadCount";
            this.lblLeadCount.Size = new System.Drawing.Size(140, 40);
            this.lblLeadCount.TabIndex = 14;
            this.lblLeadCount.Text = "Lead Count";
            this.lblLeadCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblROIWidth
            // 
            this.lblROIWidth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblROIWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblROIWidth.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblROIWidth.ForeColor = System.Drawing.Color.White;
            this.lblROIWidth.Location = new System.Drawing.Point(6, 190);
            this.lblROIWidth.Margin = new System.Windows.Forms.Padding(0);
            this.lblROIWidth.Name = "lblROIWidth";
            this.lblROIWidth.Size = new System.Drawing.Size(140, 40);
            this.lblROIWidth.TabIndex = 10;
            this.lblROIWidth.Text = "ROI Width(㎛)";
            this.lblROIWidth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLeadCountValue
            // 
            this.lblLeadCountValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblLeadCountValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeadCountValue.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblLeadCountValue.ForeColor = System.Drawing.Color.White;
            this.lblLeadCountValue.Location = new System.Drawing.Point(154, 98);
            this.lblLeadCountValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblLeadCountValue.Name = "lblLeadCountValue";
            this.lblLeadCountValue.Size = new System.Drawing.Size(80, 40);
            this.lblLeadCountValue.TabIndex = 19;
            this.lblLeadCountValue.Text = "0";
            this.lblLeadCountValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLeadCountValue.Click += new System.EventHandler(this.lblLeadCountValue_Click);
            // 
            // lblLeadPitch
            // 
            this.lblLeadPitch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblLeadPitch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeadPitch.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblLeadPitch.ForeColor = System.Drawing.Color.White;
            this.lblLeadPitch.Location = new System.Drawing.Point(6, 144);
            this.lblLeadPitch.Margin = new System.Windows.Forms.Padding(0);
            this.lblLeadPitch.Name = "lblLeadPitch";
            this.lblLeadPitch.Size = new System.Drawing.Size(140, 40);
            this.lblLeadPitch.TabIndex = 13;
            this.lblLeadPitch.Text = "Lead Pitch (㎛)";
            this.lblLeadPitch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvAkkonResult
            // 
            this.dgvAkkonResult.AllowUserToAddRows = false;
            this.dgvAkkonResult.AllowUserToDeleteRows = false;
            this.dgvAkkonResult.BackgroundColor = System.Drawing.Color.White;
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
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAkkonResult.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvAkkonResult.Location = new System.Drawing.Point(6, 14);
            this.dgvAkkonResult.Margin = new System.Windows.Forms.Padding(0);
            this.dgvAkkonResult.Name = "dgvAkkonResult";
            this.dgvAkkonResult.ReadOnly = true;
            this.dgvAkkonResult.RowTemplate.Height = 23;
            this.dgvAkkonResult.Size = new System.Drawing.Size(62, 308);
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
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 7;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.lblNext, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblPrev, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmbTabList, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblAddROI, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblInspection, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblROIJog, 5, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 32);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(965, 50);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // lblNext
            // 
            this.lblNext.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNext.Image = global::Jastech.Apps.Winform.Properties.Resources.Next_White;
            this.lblNext.Location = new System.Drawing.Point(250, 0);
            this.lblNext.Margin = new System.Windows.Forms.Padding(0);
            this.lblNext.Name = "lblNext";
            this.lblNext.Size = new System.Drawing.Size(50, 50);
            this.lblNext.TabIndex = 27;
            this.lblNext.Click += new System.EventHandler(this.lblNext_Click);
            // 
            // lblPrev
            // 
            this.lblPrev.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPrev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPrev.Image = global::Jastech.Apps.Winform.Properties.Resources.Prev_White;
            this.lblPrev.Location = new System.Drawing.Point(200, 0);
            this.lblPrev.Margin = new System.Windows.Forms.Padding(0);
            this.lblPrev.Name = "lblPrev";
            this.lblPrev.Size = new System.Drawing.Size(50, 50);
            this.lblPrev.TabIndex = 26;
            this.lblPrev.Click += new System.EventHandler(this.lblPrev_Click);
            // 
            // cmbTabList
            // 
            this.cmbTabList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbTabList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTabList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTabList.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold);
            this.cmbTabList.FormattingEnabled = true;
            this.cmbTabList.Location = new System.Drawing.Point(0, 0);
            this.cmbTabList.Margin = new System.Windows.Forms.Padding(0);
            this.cmbTabList.Name = "cmbTabList";
            this.cmbTabList.Size = new System.Drawing.Size(200, 34);
            this.cmbTabList.TabIndex = 25;
            this.cmbTabList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DrawItem);
            this.cmbTabList.SelectedIndexChanged += new System.EventHandler(this.cmbTabList_SelectedIndexChanged);
            // 
            // lblAddROI
            // 
            this.lblAddROI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAddROI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAddROI.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblAddROI.Location = new System.Drawing.Point(400, 0);
            this.lblAddROI.Margin = new System.Windows.Forms.Padding(0);
            this.lblAddROI.Name = "lblAddROI";
            this.lblAddROI.Size = new System.Drawing.Size(100, 50);
            this.lblAddROI.TabIndex = 23;
            this.lblAddROI.Text = "Add ROI";
            this.lblAddROI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAddROI.Click += new System.EventHandler(this.lblAddROI_Click);
            // 
            // lblInspection
            // 
            this.lblInspection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInspection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInspection.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblInspection.Location = new System.Drawing.Point(300, 0);
            this.lblInspection.Margin = new System.Windows.Forms.Padding(0);
            this.lblInspection.Name = "lblInspection";
            this.lblInspection.Size = new System.Drawing.Size(100, 50);
            this.lblInspection.TabIndex = 22;
            this.lblInspection.Text = "Inspect";
            this.lblInspection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblInspection.Click += new System.EventHandler(this.lblInspection_Click);
            // 
            // lblROIJog
            // 
            this.lblROIJog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblROIJog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblROIJog.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblROIJog.Location = new System.Drawing.Point(500, 0);
            this.lblROIJog.Margin = new System.Windows.Forms.Padding(0);
            this.lblROIJog.Name = "lblROIJog";
            this.lblROIJog.Size = new System.Drawing.Size(100, 50);
            this.lblROIJog.TabIndex = 23;
            this.lblROIJog.Text = "ROI Jog";
            this.lblROIJog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblROIJog.Click += new System.EventHandler(this.lblROIJog_Click);
            // 
            // lblParameter
            // 
            this.lblParameter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblParameter.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblParameter.ForeColor = System.Drawing.Color.White;
            this.lblParameter.Location = new System.Drawing.Point(0, 0);
            this.lblParameter.Margin = new System.Windows.Forms.Padding(0);
            this.lblParameter.Name = "lblParameter";
            this.lblParameter.Size = new System.Drawing.Size(965, 32);
            this.lblParameter.TabIndex = 1;
            this.lblParameter.Text = "Parameter";
            this.lblParameter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblResult);
            this.panel1.Controls.Add(this.lblGroup);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 82);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(965, 60);
            this.panel1.TabIndex = 295;
            // 
            // lblResult
            // 
            this.lblResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResult.Location = new System.Drawing.Point(223, 10);
            this.lblResult.Margin = new System.Windows.Forms.Padding(0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(200, 40);
            this.lblResult.TabIndex = 295;
            this.lblResult.Text = "Result";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblResult.Click += new System.EventHandler(this.lblResult_Click);
            // 
            // lblGroup
            // 
            this.lblGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGroup.Location = new System.Drawing.Point(6, 10);
            this.lblGroup.Margin = new System.Windows.Forms.Padding(0);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(200, 40);
            this.lblGroup.TabIndex = 295;
            this.lblGroup.Text = "Group";
            this.lblGroup.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGroup.Click += new System.EventHandler(this.lblGroup_Click);
            // 
            // AkkonControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpAkkon);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AkkonControl";
            this.Size = new System.Drawing.Size(965, 762);
            this.Load += new System.EventHandler(this.AkkonControl_Load);
            this.tlpAkkon.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlROIData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAkkonROI)).EndInit();
            this.panel2.ResumeLayout(false);
            this.pnlGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAkkonResult)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlParam;
        private System.Windows.Forms.TableLayoutPanel tlpAkkon;
        private System.Windows.Forms.Label lblParameter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblAddROI;
        private System.Windows.Forms.Label lblInspection;
        private System.Windows.Forms.DataGridView dgvAkkonROI;
        private System.Windows.Forms.DataGridView dgvAkkonResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn colResultNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStrength;
        private System.Windows.Forms.DataGridViewTextBoxColumn colJudgement;
        private System.Windows.Forms.ComboBox cmbTabList;
        private System.Windows.Forms.Label lblPrev;
        private System.Windows.Forms.Label lblNext;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlROIData;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblGroupCount;
        private System.Windows.Forms.Label lblGroupCountValue;
        private System.Windows.Forms.ComboBox cmbGroupNumber;
        private System.Windows.Forms.Label lblGroupNumber;
        private System.Windows.Forms.Label lblROIWidth;
        private System.Windows.Forms.Label lblROIWidthValue;
        private System.Windows.Forms.Label lblROIHeight;
        private System.Windows.Forms.Label lblROIHeightValue;
        private System.Windows.Forms.Label lblLeadCount;
        private System.Windows.Forms.Label lblLeadCountValue;
        private System.Windows.Forms.Label lblLeadPitch;
        private System.Windows.Forms.Label lblLeadPitchValue;
        private System.Windows.Forms.Label lblClone;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlGroup;
        private System.Windows.Forms.Label lblCloneVertical;
        private System.Windows.Forms.Label lblCloneHorizontal;
        private System.Windows.Forms.Label lblCloneExecute;
        private System.Windows.Forms.Label lblRegister;
        private System.Windows.Forms.DataGridViewTextBoxColumn colROINo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLeftTop;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRightTop;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLeftBottom;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRightBottom;
        private System.Windows.Forms.Label lblDelete;
        private System.Windows.Forms.Label lblROIJog;
        private System.Windows.Forms.Label lblSort;
    }
}
