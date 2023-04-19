namespace Jastech.Apps.Winform.UI.Controls
{
    partial class MarkControl
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
            this.pnlPosition = new System.Windows.Forms.Panel();
            this.lblRight = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblRightSub4 = new System.Windows.Forms.Label();
            this.lblRightMain = new System.Windows.Forms.Label();
            this.lblRightSub1 = new System.Windows.Forms.Label();
            this.lblRightSub3 = new System.Windows.Forms.Label();
            this.lblRightSub2 = new System.Windows.Forms.Label();
            this.lblLeftSub4 = new System.Windows.Forms.Label();
            this.lblFpc = new System.Windows.Forms.Label();
            this.lblPanel = new System.Windows.Forms.Label();
            this.lblLeftMain = new System.Windows.Forms.Label();
            this.lblLeftSub1 = new System.Windows.Forms.Label();
            this.lblLeftSub3 = new System.Windows.Forms.Label();
            this.lblLeftSub2 = new System.Windows.Forms.Label();
            this.tlpBasic = new System.Windows.Forms.TableLayoutPanel();
            this.lblAddROI = new System.Windows.Forms.Label();
            this.lblInspection = new System.Windows.Forms.Label();
            this.cbxTabNumList = new System.Windows.Forms.ComboBox();
            this.lblParameter = new System.Windows.Forms.Label();
            this.tlpPattern = new System.Windows.Forms.TableLayoutPanel();
            this.pnlParam = new System.Windows.Forms.Panel();
            this.lblPrev = new System.Windows.Forms.Label();
            this.lblNext = new System.Windows.Forms.Label();
            this.pnlPosition.SuspendLayout();
            this.tlpBasic.SuspendLayout();
            this.tlpPattern.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPosition
            // 
            this.pnlPosition.Controls.Add(this.lblRight);
            this.pnlPosition.Controls.Add(this.label8);
            this.pnlPosition.Controls.Add(this.lblRightSub4);
            this.pnlPosition.Controls.Add(this.lblRightMain);
            this.pnlPosition.Controls.Add(this.lblRightSub1);
            this.pnlPosition.Controls.Add(this.lblRightSub3);
            this.pnlPosition.Controls.Add(this.lblRightSub2);
            this.pnlPosition.Controls.Add(this.lblLeftSub4);
            this.pnlPosition.Controls.Add(this.lblFpc);
            this.pnlPosition.Controls.Add(this.lblPanel);
            this.pnlPosition.Controls.Add(this.lblLeftMain);
            this.pnlPosition.Controls.Add(this.lblLeftSub1);
            this.pnlPosition.Controls.Add(this.lblLeftSub3);
            this.pnlPosition.Controls.Add(this.lblLeftSub2);
            this.pnlPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPosition.Location = new System.Drawing.Point(3, 85);
            this.pnlPosition.Name = "pnlPosition";
            this.pnlPosition.Size = new System.Drawing.Size(679, 294);
            this.pnlPosition.TabIndex = 7;
            // 
            // lblRight
            // 
            this.lblRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblRight.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblRight.ForeColor = System.Drawing.Color.White;
            this.lblRight.Location = new System.Drawing.Point(8, 167);
            this.lblRight.Name = "lblRight";
            this.lblRight.Size = new System.Drawing.Size(119, 33);
            this.lblRight.TabIndex = 11;
            this.lblRight.Text = "Right";
            this.lblRight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.label8.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(8, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 33);
            this.label8.TabIndex = 9;
            this.label8.Text = "Left";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRightSub4
            // 
            this.lblRightSub4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRightSub4.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblRightSub4.Location = new System.Drawing.Point(450, 216);
            this.lblRightSub4.Margin = new System.Windows.Forms.Padding(0);
            this.lblRightSub4.Name = "lblRightSub4";
            this.lblRightSub4.Size = new System.Drawing.Size(100, 40);
            this.lblRightSub4.TabIndex = 6;
            this.lblRightSub4.Text = "SUB4";
            this.lblRightSub4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRightSub4.Click += new System.EventHandler(this.lblRightSub4_Click);
            // 
            // lblRightMain
            // 
            this.lblRightMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRightMain.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblRightMain.Location = new System.Drawing.Point(11, 216);
            this.lblRightMain.Margin = new System.Windows.Forms.Padding(0);
            this.lblRightMain.Name = "lblRightMain";
            this.lblRightMain.Size = new System.Drawing.Size(100, 40);
            this.lblRightMain.TabIndex = 2;
            this.lblRightMain.Text = "MAIN";
            this.lblRightMain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRightMain.Click += new System.EventHandler(this.lblRightMain_Click);
            // 
            // lblRightSub1
            // 
            this.lblRightSub1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRightSub1.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblRightSub1.Location = new System.Drawing.Point(120, 216);
            this.lblRightSub1.Margin = new System.Windows.Forms.Padding(0);
            this.lblRightSub1.Name = "lblRightSub1";
            this.lblRightSub1.Size = new System.Drawing.Size(100, 40);
            this.lblRightSub1.TabIndex = 3;
            this.lblRightSub1.Text = "SUB1";
            this.lblRightSub1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRightSub1.Click += new System.EventHandler(this.lblRightSub1_Click);
            // 
            // lblRightSub3
            // 
            this.lblRightSub3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRightSub3.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblRightSub3.Location = new System.Drawing.Point(340, 216);
            this.lblRightSub3.Margin = new System.Windows.Forms.Padding(0);
            this.lblRightSub3.Name = "lblRightSub3";
            this.lblRightSub3.Size = new System.Drawing.Size(100, 40);
            this.lblRightSub3.TabIndex = 4;
            this.lblRightSub3.Text = "SUB3";
            this.lblRightSub3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRightSub3.Click += new System.EventHandler(this.lblRightSub3_Click);
            // 
            // lblRightSub2
            // 
            this.lblRightSub2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRightSub2.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblRightSub2.Location = new System.Drawing.Point(230, 216);
            this.lblRightSub2.Margin = new System.Windows.Forms.Padding(0);
            this.lblRightSub2.Name = "lblRightSub2";
            this.lblRightSub2.Size = new System.Drawing.Size(100, 40);
            this.lblRightSub2.TabIndex = 5;
            this.lblRightSub2.Text = "SUB2";
            this.lblRightSub2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRightSub2.Click += new System.EventHandler(this.lblRightSub2_Click);
            // 
            // lblLeftSub4
            // 
            this.lblLeftSub4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeftSub4.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblLeftSub4.Location = new System.Drawing.Point(450, 111);
            this.lblLeftSub4.Margin = new System.Windows.Forms.Padding(0);
            this.lblLeftSub4.Name = "lblLeftSub4";
            this.lblLeftSub4.Size = new System.Drawing.Size(100, 40);
            this.lblLeftSub4.TabIndex = 1;
            this.lblLeftSub4.Text = "SUB4";
            this.lblLeftSub4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLeftSub4.Click += new System.EventHandler(this.lblLeftSub4_Click);
            // 
            // lblFpc
            // 
            this.lblFpc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFpc.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblFpc.Location = new System.Drawing.Point(7, 12);
            this.lblFpc.Margin = new System.Windows.Forms.Padding(0);
            this.lblFpc.Name = "lblFpc";
            this.lblFpc.Size = new System.Drawing.Size(200, 40);
            this.lblFpc.TabIndex = 0;
            this.lblFpc.Text = "FPC";
            this.lblFpc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFpc.Click += new System.EventHandler(this.lblFpc_Click);
            // 
            // lblPanel
            // 
            this.lblPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPanel.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblPanel.Location = new System.Drawing.Point(217, 12);
            this.lblPanel.Margin = new System.Windows.Forms.Padding(0);
            this.lblPanel.Name = "lblPanel";
            this.lblPanel.Size = new System.Drawing.Size(200, 40);
            this.lblPanel.TabIndex = 0;
            this.lblPanel.Text = "Panel";
            this.lblPanel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPanel.Click += new System.EventHandler(this.lblPanel_Click);
            // 
            // lblLeftMain
            // 
            this.lblLeftMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeftMain.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblLeftMain.Location = new System.Drawing.Point(11, 111);
            this.lblLeftMain.Margin = new System.Windows.Forms.Padding(0);
            this.lblLeftMain.Name = "lblLeftMain";
            this.lblLeftMain.Size = new System.Drawing.Size(100, 40);
            this.lblLeftMain.TabIndex = 0;
            this.lblLeftMain.Text = "MAIN";
            this.lblLeftMain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLeftMain.Click += new System.EventHandler(this.lblLeftMain_Click);
            // 
            // lblLeftSub1
            // 
            this.lblLeftSub1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeftSub1.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblLeftSub1.Location = new System.Drawing.Point(120, 111);
            this.lblLeftSub1.Margin = new System.Windows.Forms.Padding(0);
            this.lblLeftSub1.Name = "lblLeftSub1";
            this.lblLeftSub1.Size = new System.Drawing.Size(100, 40);
            this.lblLeftSub1.TabIndex = 0;
            this.lblLeftSub1.Text = "SUB1";
            this.lblLeftSub1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLeftSub1.Click += new System.EventHandler(this.lblLeftSub1_Click);
            // 
            // lblLeftSub3
            // 
            this.lblLeftSub3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeftSub3.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblLeftSub3.Location = new System.Drawing.Point(340, 111);
            this.lblLeftSub3.Margin = new System.Windows.Forms.Padding(0);
            this.lblLeftSub3.Name = "lblLeftSub3";
            this.lblLeftSub3.Size = new System.Drawing.Size(100, 40);
            this.lblLeftSub3.TabIndex = 0;
            this.lblLeftSub3.Text = "SUB3";
            this.lblLeftSub3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLeftSub3.Click += new System.EventHandler(this.lblLeftSub3_Click);
            // 
            // lblLeftSub2
            // 
            this.lblLeftSub2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeftSub2.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblLeftSub2.Location = new System.Drawing.Point(230, 111);
            this.lblLeftSub2.Margin = new System.Windows.Forms.Padding(0);
            this.lblLeftSub2.Name = "lblLeftSub2";
            this.lblLeftSub2.Size = new System.Drawing.Size(100, 40);
            this.lblLeftSub2.TabIndex = 0;
            this.lblLeftSub2.Text = "SUB2";
            this.lblLeftSub2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLeftSub2.Click += new System.EventHandler(this.lblLeftSub2_Click);
            // 
            // tlpBasic
            // 
            this.tlpBasic.ColumnCount = 7;
            this.tlpBasic.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpBasic.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpBasic.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpBasic.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpBasic.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpBasic.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpBasic.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBasic.Controls.Add(this.lblAddROI, 4, 0);
            this.tlpBasic.Controls.Add(this.lblPrev, 1, 0);
            this.tlpBasic.Controls.Add(this.lblNext, 2, 0);
            this.tlpBasic.Controls.Add(this.lblInspection, 3, 0);
            this.tlpBasic.Controls.Add(this.cbxTabNumList, 0, 0);
            this.tlpBasic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBasic.Location = new System.Drawing.Point(0, 32);
            this.tlpBasic.Margin = new System.Windows.Forms.Padding(0);
            this.tlpBasic.Name = "tlpBasic";
            this.tlpBasic.RowCount = 1;
            this.tlpBasic.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBasic.Size = new System.Drawing.Size(685, 50);
            this.tlpBasic.TabIndex = 0;
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
            // cbxTabNumList
            // 
            this.cbxTabNumList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxTabNumList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxTabNumList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTabNumList.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold);
            this.cbxTabNumList.FormattingEnabled = true;
            this.cbxTabNumList.Location = new System.Drawing.Point(3, 3);
            this.cbxTabNumList.Name = "cbxTabNumList";
            this.cbxTabNumList.Size = new System.Drawing.Size(194, 34);
            this.cbxTabNumList.TabIndex = 24;
            this.cbxTabNumList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbxTabNumList_DrawItem);
            this.cbxTabNumList.SelectedIndexChanged += new System.EventHandler(this.cbxTabNumList_SelectedIndexChanged);
            // 
            // lblParameter
            // 
            this.lblParameter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblParameter.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblParameter.Location = new System.Drawing.Point(0, 0);
            this.lblParameter.Margin = new System.Windows.Forms.Padding(0);
            this.lblParameter.Name = "lblParameter";
            this.lblParameter.Size = new System.Drawing.Size(685, 32);
            this.lblParameter.TabIndex = 0;
            this.lblParameter.Text = "Parameter";
            this.lblParameter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpPattern
            // 
            this.tlpPattern.ColumnCount = 1;
            this.tlpPattern.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPattern.Controls.Add(this.lblParameter, 0, 0);
            this.tlpPattern.Controls.Add(this.tlpBasic, 0, 1);
            this.tlpPattern.Controls.Add(this.pnlPosition, 0, 2);
            this.tlpPattern.Controls.Add(this.pnlParam, 0, 3);
            this.tlpPattern.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPattern.Location = new System.Drawing.Point(0, 0);
            this.tlpPattern.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tlpPattern.Name = "tlpPattern";
            this.tlpPattern.RowCount = 4;
            this.tlpPattern.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpPattern.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpPattern.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tlpPattern.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPattern.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpPattern.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpPattern.Size = new System.Drawing.Size(685, 685);
            this.tlpPattern.TabIndex = 2;
            // 
            // pnlParam
            // 
            this.pnlParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlParam.Location = new System.Drawing.Point(3, 385);
            this.pnlParam.Name = "pnlParam";
            this.pnlParam.Size = new System.Drawing.Size(679, 297);
            this.pnlParam.TabIndex = 8;
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
            this.lblPrev.TabIndex = 5;
            this.lblPrev.Click += new System.EventHandler(this.lblPrev_Click);
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
            this.lblNext.TabIndex = 2;
            this.lblNext.Click += new System.EventHandler(this.lblNext_Click);
            // 
            // MarkControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpPattern);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "MarkControl";
            this.Size = new System.Drawing.Size(685, 685);
            this.Load += new System.EventHandler(this.MarkControl_Load);
            this.pnlPosition.ResumeLayout(false);
            this.tlpBasic.ResumeLayout(false);
            this.tlpPattern.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlPosition;
        private System.Windows.Forms.Label lblFpc;
        private System.Windows.Forms.Label lblPanel;
        private System.Windows.Forms.Label lblLeftMain;
        private System.Windows.Forms.Label lblLeftSub1;
        private System.Windows.Forms.Label lblLeftSub3;
        private System.Windows.Forms.Label lblLeftSub2;
        private System.Windows.Forms.TableLayoutPanel tlpBasic;
        private System.Windows.Forms.Label lblAddROI;
        private System.Windows.Forms.Label lblPrev;
        private System.Windows.Forms.Label lblNext;
        private System.Windows.Forms.Label lblInspection;
        private System.Windows.Forms.Label lblParameter;
        private System.Windows.Forms.TableLayoutPanel tlpPattern;
        private System.Windows.Forms.Label lblLeftSub4;
        private System.Windows.Forms.Label lblRightSub4;
        private System.Windows.Forms.Label lblRightMain;
        private System.Windows.Forms.Label lblRightSub1;
        private System.Windows.Forms.Label lblRightSub3;
        private System.Windows.Forms.Label lblRightSub2;
        private System.Windows.Forms.Label lblRight;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pnlParam;
        private System.Windows.Forms.ComboBox cbxTabNumList;
    }
}
