namespace Jastech.Apps.Winform.UI.Controls
{
    partial class AlignControl
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
            this.tlpAlign = new System.Windows.Forms.TableLayoutPanel();
            this.lblParameter = new System.Windows.Forms.Label();
            this.tlpBasic = new System.Windows.Forms.TableLayoutPanel();
            this.lblTab = new System.Windows.Forms.Label();
            this.lblAddROI = new System.Windows.Forms.Label();
            this.lblPrev = new System.Windows.Forms.Label();
            this.lblNext = new System.Windows.Forms.Label();
            this.lblInspection = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.chkUseTracking = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlParam = new System.Windows.Forms.Panel();
            this.lblFPCX = new System.Windows.Forms.Label();
            this.lblFPCY = new System.Windows.Forms.Label();
            this.lblPanelX = new System.Windows.Forms.Label();
            this.lblPanelY = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tlpPosition = new System.Windows.Forms.TableLayoutPanel();
            this.lblLeft = new System.Windows.Forms.Label();
            this.lblRight = new System.Windows.Forms.Label();
            this.tlpAlign.SuspendLayout();
            this.tlpBasic.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tlpPosition.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAlign
            // 
            this.tlpAlign.ColumnCount = 1;
            this.tlpAlign.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlign.Controls.Add(this.tableLayoutPanel7, 0, 3);
            this.tlpAlign.Controls.Add(this.lblParameter, 0, 0);
            this.tlpAlign.Controls.Add(this.tlpBasic, 0, 1);
            this.tlpAlign.Controls.Add(this.tlpPosition, 0, 2);
            this.tlpAlign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAlign.Location = new System.Drawing.Point(0, 0);
            this.tlpAlign.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tlpAlign.Name = "tlpAlign";
            this.tlpAlign.RowCount = 5;
            this.tlpAlign.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpAlign.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpAlign.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpAlign.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAlign.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAlign.Size = new System.Drawing.Size(674, 575);
            this.tlpAlign.TabIndex = 1;
            // 
            // lblParameter
            // 
            this.lblParameter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblParameter.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblParameter.Location = new System.Drawing.Point(0, 0);
            this.lblParameter.Margin = new System.Windows.Forms.Padding(0);
            this.lblParameter.Name = "lblParameter";
            this.lblParameter.Size = new System.Drawing.Size(674, 32);
            this.lblParameter.TabIndex = 0;
            this.lblParameter.Text = "Parameter";
            this.lblParameter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpBasic
            // 
            this.tlpBasic.ColumnCount = 6;
            this.tlpBasic.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpBasic.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpBasic.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpBasic.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpBasic.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpBasic.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBasic.Controls.Add(this.lblTab, 0, 0);
            this.tlpBasic.Controls.Add(this.lblAddROI, 4, 0);
            this.tlpBasic.Controls.Add(this.lblPrev, 1, 0);
            this.tlpBasic.Controls.Add(this.lblNext, 2, 0);
            this.tlpBasic.Controls.Add(this.lblInspection, 3, 0);
            this.tlpBasic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBasic.Location = new System.Drawing.Point(0, 32);
            this.tlpBasic.Margin = new System.Windows.Forms.Padding(0);
            this.tlpBasic.Name = "tlpBasic";
            this.tlpBasic.RowCount = 1;
            this.tlpBasic.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBasic.Size = new System.Drawing.Size(674, 50);
            this.tlpBasic.TabIndex = 0;
            // 
            // lblTab
            // 
            this.lblTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTab.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTab.Location = new System.Drawing.Point(0, 0);
            this.lblTab.Margin = new System.Windows.Forms.Padding(0);
            this.lblTab.Name = "lblTab";
            this.lblTab.Size = new System.Drawing.Size(200, 46);
            this.lblTab.TabIndex = 25;
            this.lblTab.Text = "TAB : 1";
            this.lblTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAddROI
            // 
            this.lblAddROI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAddROI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAddROI.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblAddROI.Location = new System.Drawing.Point(400, 0);
            this.lblAddROI.Margin = new System.Windows.Forms.Padding(0);
            this.lblAddROI.Name = "lblAddROI";
            this.lblAddROI.Size = new System.Drawing.Size(100, 46);
            this.lblAddROI.TabIndex = 23;
            this.lblAddROI.Text = "Add ROI";
            this.lblAddROI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAddROI.Click += new System.EventHandler(this.lblAddROI_Click);
            // 
            // lblPrev
            // 
            this.lblPrev.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPrev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPrev.Image = global::Jastech.Apps.Winform.Properties.Resources.Prev;
            this.lblPrev.Location = new System.Drawing.Point(200, 0);
            this.lblPrev.Margin = new System.Windows.Forms.Padding(0);
            this.lblPrev.Name = "lblPrev";
            this.lblPrev.Size = new System.Drawing.Size(50, 46);
            this.lblPrev.TabIndex = 5;
            this.lblPrev.Click += new System.EventHandler(this.lblPrevTab_Click);
            // 
            // lblNext
            // 
            this.lblNext.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNext.Image = global::Jastech.Apps.Winform.Properties.Resources.Next;
            this.lblNext.Location = new System.Drawing.Point(250, 0);
            this.lblNext.Margin = new System.Windows.Forms.Padding(0);
            this.lblNext.Name = "lblNext";
            this.lblNext.Size = new System.Drawing.Size(50, 46);
            this.lblNext.TabIndex = 2;
            this.lblNext.Click += new System.EventHandler(this.lblNextTab_Click);
            // 
            // lblInspection
            // 
            this.lblInspection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInspection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInspection.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblInspection.Location = new System.Drawing.Point(300, 0);
            this.lblInspection.Margin = new System.Windows.Forms.Padding(0);
            this.lblInspection.Name = "lblInspection";
            this.lblInspection.Size = new System.Drawing.Size(100, 46);
            this.lblInspection.TabIndex = 22;
            this.lblInspection.Text = "Inspect";
            this.lblInspection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblInspection.Click += new System.EventHandler(this.lblInspection_Click);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel8, 0, 0);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(337, 0);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(337, 49);
            this.tableLayoutPanel6.TabIndex = 5;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Controls.Add(this.chkUseTracking, 1, 1);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(168, 49);
            this.tableLayoutPanel8.TabIndex = 3;
            // 
            // chkUseTracking
            // 
            this.chkUseTracking.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkUseTracking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.chkUseTracking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkUseTracking.Location = new System.Drawing.Point(87, 27);
            this.chkUseTracking.Name = "chkUseTracking";
            this.chkUseTracking.Size = new System.Drawing.Size(78, 19);
            this.chkUseTracking.TabIndex = 13;
            this.chkUseTracking.Text = "ROI Tracking : UNUSE";
            this.chkUseTracking.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkUseTracking.UseVisualStyleBackColor = false;
            this.chkUseTracking.CheckedChanged += new System.EventHandler(this.chkUseTracking_CheckedChanged);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.pnlParam, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel6, 1, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 182);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(674, 196);
            this.tableLayoutPanel7.TabIndex = 5;
            // 
            // pnlParam
            // 
            this.pnlParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlParam.Location = new System.Drawing.Point(0, 0);
            this.pnlParam.Margin = new System.Windows.Forms.Padding(0);
            this.pnlParam.Name = "pnlParam";
            this.pnlParam.Size = new System.Drawing.Size(337, 196);
            this.pnlParam.TabIndex = 1;
            // 
            // lblFPCX
            // 
            this.lblFPCX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFPCX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFPCX.Location = new System.Drawing.Point(200, 0);
            this.lblFPCX.Margin = new System.Windows.Forms.Padding(0);
            this.lblFPCX.Name = "lblFPCX";
            this.lblFPCX.Size = new System.Drawing.Size(100, 50);
            this.lblFPCX.TabIndex = 0;
            this.lblFPCX.Text = "FPC X";
            this.lblFPCX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFPCY
            // 
            this.lblFPCY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFPCY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFPCY.Location = new System.Drawing.Point(300, 0);
            this.lblFPCY.Margin = new System.Windows.Forms.Padding(0);
            this.lblFPCY.Name = "lblFPCY";
            this.lblFPCY.Size = new System.Drawing.Size(100, 50);
            this.lblFPCY.TabIndex = 0;
            this.lblFPCY.Text = "FPC Y";
            this.lblFPCY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPanelX
            // 
            this.lblPanelX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPanelX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPanelX.Location = new System.Drawing.Point(400, 0);
            this.lblPanelX.Margin = new System.Windows.Forms.Padding(0);
            this.lblPanelX.Name = "lblPanelX";
            this.lblPanelX.Size = new System.Drawing.Size(100, 50);
            this.lblPanelX.TabIndex = 0;
            this.lblPanelX.Text = "PANEL X";
            this.lblPanelX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPanelY
            // 
            this.lblPanelY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPanelY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPanelY.Location = new System.Drawing.Point(500, 0);
            this.lblPanelY.Margin = new System.Windows.Forms.Padding(0);
            this.lblPanelY.Name = "lblPanelY";
            this.lblPanelY.Size = new System.Drawing.Size(100, 50);
            this.lblPanelY.TabIndex = 0;
            this.lblPanelY.Text = "PANEL Y";
            this.lblPanelY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(200, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "FPC X";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(300, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 50);
            this.label2.TabIndex = 0;
            this.label2.Text = "FPC Y";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(400, 50);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 50);
            this.label3.TabIndex = 0;
            this.label3.Text = "PANEL X";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(500, 50);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 50);
            this.label4.TabIndex = 0;
            this.label4.Text = "PANEL Y";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpPosition
            // 
            this.tlpPosition.ColumnCount = 6;
            this.tlpPosition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpPosition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpPosition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpPosition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpPosition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpPosition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPosition.Controls.Add(this.label4, 4, 1);
            this.tlpPosition.Controls.Add(this.label3, 3, 1);
            this.tlpPosition.Controls.Add(this.label2, 2, 1);
            this.tlpPosition.Controls.Add(this.label1, 1, 1);
            this.tlpPosition.Controls.Add(this.lblPanelY, 4, 0);
            this.tlpPosition.Controls.Add(this.lblPanelX, 3, 0);
            this.tlpPosition.Controls.Add(this.lblFPCY, 2, 0);
            this.tlpPosition.Controls.Add(this.lblFPCX, 1, 0);
            this.tlpPosition.Controls.Add(this.lblLeft, 0, 0);
            this.tlpPosition.Controls.Add(this.lblRight, 0, 1);
            this.tlpPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPosition.Location = new System.Drawing.Point(0, 82);
            this.tlpPosition.Margin = new System.Windows.Forms.Padding(0);
            this.tlpPosition.Name = "tlpPosition";
            this.tlpPosition.RowCount = 2;
            this.tlpPosition.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpPosition.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpPosition.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpPosition.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpPosition.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpPosition.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpPosition.Size = new System.Drawing.Size(674, 100);
            this.tlpPosition.TabIndex = 6;
            // 
            // lblLeft
            // 
            this.lblLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLeft.Location = new System.Drawing.Point(0, 0);
            this.lblLeft.Margin = new System.Windows.Forms.Padding(0);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Size = new System.Drawing.Size(200, 50);
            this.lblLeft.TabIndex = 0;
            this.lblLeft.Text = "FPC X";
            this.lblLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRight
            // 
            this.lblRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRight.Location = new System.Drawing.Point(0, 50);
            this.lblRight.Margin = new System.Windows.Forms.Padding(0);
            this.lblRight.Name = "lblRight";
            this.lblRight.Size = new System.Drawing.Size(200, 50);
            this.lblRight.TabIndex = 0;
            this.lblRight.Text = "FPC X";
            this.lblRight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AlignControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpAlign);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AlignControl";
            this.Size = new System.Drawing.Size(674, 575);
            this.Load += new System.EventHandler(this.AlignControl_Load);
            this.tlpAlign.ResumeLayout(false);
            this.tlpBasic.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tlpPosition.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAlign;
        private System.Windows.Forms.Label lblParameter;
        private System.Windows.Forms.TableLayoutPanel tlpBasic;
        private System.Windows.Forms.Label lblPrev;
        private System.Windows.Forms.Label lblNext;
        private System.Windows.Forms.Panel pnlParam;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.CheckBox chkUseTracking;
        private System.Windows.Forms.Label lblFPCX;
        private System.Windows.Forms.Label lblFPCY;
        private System.Windows.Forms.Label lblPanelX;
        private System.Windows.Forms.Label lblPanelY;
        private System.Windows.Forms.Label lblInspection;
        private System.Windows.Forms.Label lblAddROI;
        private System.Windows.Forms.Label lblTab;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tlpPosition;
        private System.Windows.Forms.Label lblLeft;
        private System.Windows.Forms.Label lblRight;
    }
}
