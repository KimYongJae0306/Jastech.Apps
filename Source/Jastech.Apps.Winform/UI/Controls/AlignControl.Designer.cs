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
            this.pnlParam = new System.Windows.Forms.Panel();
            this.lblParameter = new System.Windows.Forms.Label();
            this.tlpBasic = new System.Windows.Forms.TableLayoutPanel();
            this.lblTab = new System.Windows.Forms.Label();
            this.lblAddROI = new System.Windows.Forms.Label();
            this.lblPrev = new System.Windows.Forms.Label();
            this.lblNext = new System.Windows.Forms.Label();
            this.lblInspection = new System.Windows.Forms.Label();
            this.pnlPosition = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lblLeft = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRight = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblFPCX = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFPCY = new System.Windows.Forms.Label();
            this.lblPanelY = new System.Windows.Forms.Label();
            this.lblPanelX = new System.Windows.Forms.Label();
            this.chkUseTracking = new System.Windows.Forms.CheckBox();
            this.tlpAlign.SuspendLayout();
            this.tlpBasic.SuspendLayout();
            this.pnlPosition.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAlign
            // 
            this.tlpAlign.ColumnCount = 1;
            this.tlpAlign.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlign.Controls.Add(this.lblParameter, 0, 0);
            this.tlpAlign.Controls.Add(this.tlpBasic, 0, 1);
            this.tlpAlign.Controls.Add(this.pnlPosition, 0, 2);
            this.tlpAlign.Controls.Add(this.chkUseTracking, 0, 4);
            this.tlpAlign.Controls.Add(this.pnlParam, 0, 3);
            this.tlpAlign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAlign.Location = new System.Drawing.Point(0, 0);
            this.tlpAlign.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tlpAlign.Name = "tlpAlign";
            this.tlpAlign.RowCount = 6;
            this.tlpAlign.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpAlign.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpAlign.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpAlign.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tlpAlign.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpAlign.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlign.Size = new System.Drawing.Size(670, 600);
            this.tlpAlign.TabIndex = 1;
            // 
            // pnlParam
            // 
            this.pnlParam.Location = new System.Drawing.Point(0, 202);
            this.pnlParam.Margin = new System.Windows.Forms.Padding(0);
            this.pnlParam.Name = "pnlParam";
            this.pnlParam.Size = new System.Drawing.Size(500, 300);
            this.pnlParam.TabIndex = 15;
            // 
            // lblParameter
            // 
            this.lblParameter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblParameter.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblParameter.Location = new System.Drawing.Point(0, 0);
            this.lblParameter.Margin = new System.Windows.Forms.Padding(0);
            this.lblParameter.Name = "lblParameter";
            this.lblParameter.Size = new System.Drawing.Size(670, 32);
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
            this.tlpBasic.Size = new System.Drawing.Size(670, 50);
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
            this.lblTab.Size = new System.Drawing.Size(200, 50);
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
            this.lblAddROI.Size = new System.Drawing.Size(100, 50);
            this.lblAddROI.TabIndex = 23;
            this.lblAddROI.Text = "Add ROI";
            this.lblAddROI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAddROI.Click += new System.EventHandler(this.lblAddROI_Click);
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
            this.lblPrev.Click += new System.EventHandler(this.lblPrevTab_Click);
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
            this.lblInspection.Size = new System.Drawing.Size(100, 50);
            this.lblInspection.TabIndex = 22;
            this.lblInspection.Text = "Inspect";
            this.lblInspection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblInspection.Click += new System.EventHandler(this.lblInspection_Click);
            // 
            // pnlPosition
            // 
            this.pnlPosition.Controls.Add(this.label4);
            this.pnlPosition.Controls.Add(this.lblLeft);
            this.pnlPosition.Controls.Add(this.label3);
            this.pnlPosition.Controls.Add(this.lblRight);
            this.pnlPosition.Controls.Add(this.label2);
            this.pnlPosition.Controls.Add(this.lblFPCX);
            this.pnlPosition.Controls.Add(this.label1);
            this.pnlPosition.Controls.Add(this.lblFPCY);
            this.pnlPosition.Controls.Add(this.lblPanelY);
            this.pnlPosition.Controls.Add(this.lblPanelX);
            this.pnlPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPosition.Location = new System.Drawing.Point(3, 85);
            this.pnlPosition.Name = "pnlPosition";
            this.pnlPosition.Size = new System.Drawing.Size(664, 114);
            this.pnlPosition.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(543, 67);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 37);
            this.label4.TabIndex = 0;
            this.label4.Text = "PANEL Y";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLeft
            // 
            this.lblLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeft.Location = new System.Drawing.Point(2, 11);
            this.lblLeft.Margin = new System.Windows.Forms.Padding(0);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Size = new System.Drawing.Size(200, 37);
            this.lblLeft.TabIndex = 0;
            this.lblLeft.Text = "Left";
            this.lblLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(433, 67);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 37);
            this.label3.TabIndex = 0;
            this.label3.Text = "PANEL X";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRight
            // 
            this.lblRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRight.Location = new System.Drawing.Point(2, 67);
            this.lblRight.Margin = new System.Windows.Forms.Padding(0);
            this.lblRight.Name = "lblRight";
            this.lblRight.Size = new System.Drawing.Size(200, 37);
            this.lblRight.TabIndex = 0;
            this.lblRight.Text = "Right";
            this.lblRight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(323, 67);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 37);
            this.label2.TabIndex = 0;
            this.label2.Text = "FPC Y";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFPCX
            // 
            this.lblFPCX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFPCX.Location = new System.Drawing.Point(214, 11);
            this.lblFPCX.Margin = new System.Windows.Forms.Padding(0);
            this.lblFPCX.Name = "lblFPCX";
            this.lblFPCX.Size = new System.Drawing.Size(100, 37);
            this.lblFPCX.TabIndex = 0;
            this.lblFPCX.Text = "FPC X";
            this.lblFPCX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(214, 67);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "FPC X";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFPCY
            // 
            this.lblFPCY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFPCY.Location = new System.Drawing.Point(323, 11);
            this.lblFPCY.Margin = new System.Windows.Forms.Padding(0);
            this.lblFPCY.Name = "lblFPCY";
            this.lblFPCY.Size = new System.Drawing.Size(100, 37);
            this.lblFPCY.TabIndex = 0;
            this.lblFPCY.Text = "FPC Y";
            this.lblFPCY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPanelY
            // 
            this.lblPanelY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPanelY.Location = new System.Drawing.Point(543, 11);
            this.lblPanelY.Margin = new System.Windows.Forms.Padding(0);
            this.lblPanelY.Name = "lblPanelY";
            this.lblPanelY.Size = new System.Drawing.Size(100, 37);
            this.lblPanelY.TabIndex = 0;
            this.lblPanelY.Text = "PANEL Y";
            this.lblPanelY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPanelX
            // 
            this.lblPanelX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPanelX.Location = new System.Drawing.Point(433, 11);
            this.lblPanelX.Margin = new System.Windows.Forms.Padding(0);
            this.lblPanelX.Name = "lblPanelX";
            this.lblPanelX.Size = new System.Drawing.Size(100, 37);
            this.lblPanelX.TabIndex = 0;
            this.lblPanelX.Text = "PANEL X";
            this.lblPanelX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkUseTracking
            // 
            this.chkUseTracking.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkUseTracking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.chkUseTracking.Location = new System.Drawing.Point(3, 505);
            this.chkUseTracking.Name = "chkUseTracking";
            this.chkUseTracking.Size = new System.Drawing.Size(78, 19);
            this.chkUseTracking.TabIndex = 13;
            this.chkUseTracking.Text = "ROI Tracking : UNUSE";
            this.chkUseTracking.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkUseTracking.UseVisualStyleBackColor = false;
            this.chkUseTracking.CheckedChanged += new System.EventHandler(this.chkUseTracking_CheckedChanged);
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
            this.Size = new System.Drawing.Size(670, 600);
            this.Load += new System.EventHandler(this.AlignControl_Load);
            this.tlpAlign.ResumeLayout(false);
            this.tlpBasic.ResumeLayout(false);
            this.pnlPosition.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAlign;
        private System.Windows.Forms.Label lblParameter;
        private System.Windows.Forms.TableLayoutPanel tlpBasic;
        private System.Windows.Forms.Label lblPrev;
        private System.Windows.Forms.Label lblNext;
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
        private System.Windows.Forms.Label lblLeft;
        private System.Windows.Forms.Label lblRight;
        private System.Windows.Forms.Panel pnlPosition;
        private System.Windows.Forms.Panel pnlParam;
    }
}
