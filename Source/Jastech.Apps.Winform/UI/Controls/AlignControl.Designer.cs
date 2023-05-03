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
            this.pnlPosition = new System.Windows.Forms.Panel();
            this.lblRightPanelY = new System.Windows.Forms.Label();
            this.lblLeft = new System.Windows.Forms.Label();
            this.lblRightPanelX = new System.Windows.Forms.Label();
            this.lblRight = new System.Windows.Forms.Label();
            this.lblRightFPCY = new System.Windows.Forms.Label();
            this.lblLeftFPCX = new System.Windows.Forms.Label();
            this.lblRightFPCX = new System.Windows.Forms.Label();
            this.lblLeftFPCY = new System.Windows.Forms.Label();
            this.lblLeftPanelY = new System.Windows.Forms.Label();
            this.lblLeftPanelX = new System.Windows.Forms.Label();
            this.chkUseTracking = new System.Windows.Forms.CheckBox();
            this.tlpParams = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCaliperParam = new System.Windows.Forms.Panel();
            this.pnlLeadParam = new System.Windows.Forms.Panel();
            this.lblLeadCount = new System.Windows.Forms.Label();
            this.lblLead = new System.Windows.Forms.Label();
            this.tlpAlign.SuspendLayout();
            this.pnlPosition.SuspendLayout();
            this.tlpParams.SuspendLayout();
            this.pnlLeadParam.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAlign
            // 
            this.tlpAlign.ColumnCount = 1;
            this.tlpAlign.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlign.Controls.Add(this.lblParameter, 0, 0);
            this.tlpAlign.Controls.Add(this.pnlPosition, 0, 1);
            this.tlpAlign.Controls.Add(this.chkUseTracking, 0, 3);
            this.tlpAlign.Controls.Add(this.tlpParams, 0, 2);
            this.tlpAlign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAlign.Location = new System.Drawing.Point(0, 0);
            this.tlpAlign.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tlpAlign.Name = "tlpAlign";
            this.tlpAlign.RowCount = 5;
            this.tlpAlign.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpAlign.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpAlign.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tlpAlign.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpAlign.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAlign.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpAlign.Size = new System.Drawing.Size(685, 685);
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
            this.lblParameter.Size = new System.Drawing.Size(685, 32);
            this.lblParameter.TabIndex = 0;
            this.lblParameter.Text = "Parameter";
            this.lblParameter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlPosition
            // 
            this.pnlPosition.Controls.Add(this.lblRightPanelY);
            this.pnlPosition.Controls.Add(this.lblLeft);
            this.pnlPosition.Controls.Add(this.lblRightPanelX);
            this.pnlPosition.Controls.Add(this.lblRight);
            this.pnlPosition.Controls.Add(this.lblRightFPCY);
            this.pnlPosition.Controls.Add(this.lblLeftFPCX);
            this.pnlPosition.Controls.Add(this.lblRightFPCX);
            this.pnlPosition.Controls.Add(this.lblLeftFPCY);
            this.pnlPosition.Controls.Add(this.lblLeftPanelY);
            this.pnlPosition.Controls.Add(this.lblLeftPanelX);
            this.pnlPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPosition.Location = new System.Drawing.Point(3, 35);
            this.pnlPosition.Name = "pnlPosition";
            this.pnlPosition.Size = new System.Drawing.Size(679, 114);
            this.pnlPosition.TabIndex = 7;
            // 
            // lblRightPanelY
            // 
            this.lblRightPanelY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRightPanelY.Location = new System.Drawing.Point(543, 63);
            this.lblRightPanelY.Margin = new System.Windows.Forms.Padding(0);
            this.lblRightPanelY.Name = "lblRightPanelY";
            this.lblRightPanelY.Size = new System.Drawing.Size(100, 40);
            this.lblRightPanelY.TabIndex = 0;
            this.lblRightPanelY.Text = "PANEL Y";
            this.lblRightPanelY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRightPanelY.Click += new System.EventHandler(this.lblRightPanelY_Click);
            // 
            // lblLeft
            // 
            this.lblLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeft.Location = new System.Drawing.Point(2, 12);
            this.lblLeft.Margin = new System.Windows.Forms.Padding(0);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Size = new System.Drawing.Size(200, 40);
            this.lblLeft.TabIndex = 0;
            this.lblLeft.Text = "Left";
            this.lblLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRightPanelX
            // 
            this.lblRightPanelX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRightPanelX.Location = new System.Drawing.Point(433, 63);
            this.lblRightPanelX.Margin = new System.Windows.Forms.Padding(0);
            this.lblRightPanelX.Name = "lblRightPanelX";
            this.lblRightPanelX.Size = new System.Drawing.Size(100, 40);
            this.lblRightPanelX.TabIndex = 0;
            this.lblRightPanelX.Text = "PANEL X";
            this.lblRightPanelX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRightPanelX.Click += new System.EventHandler(this.lblRightPanelX_Click);
            // 
            // lblRight
            // 
            this.lblRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRight.Location = new System.Drawing.Point(2, 63);
            this.lblRight.Margin = new System.Windows.Forms.Padding(0);
            this.lblRight.Name = "lblRight";
            this.lblRight.Size = new System.Drawing.Size(200, 40);
            this.lblRight.TabIndex = 0;
            this.lblRight.Text = "Right";
            this.lblRight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRightFPCY
            // 
            this.lblRightFPCY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRightFPCY.Location = new System.Drawing.Point(323, 63);
            this.lblRightFPCY.Margin = new System.Windows.Forms.Padding(0);
            this.lblRightFPCY.Name = "lblRightFPCY";
            this.lblRightFPCY.Size = new System.Drawing.Size(100, 40);
            this.lblRightFPCY.TabIndex = 0;
            this.lblRightFPCY.Text = "FPC Y";
            this.lblRightFPCY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRightFPCY.Click += new System.EventHandler(this.lblRightFPCY_Click);
            // 
            // lblLeftFPCX
            // 
            this.lblLeftFPCX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeftFPCX.Location = new System.Drawing.Point(214, 12);
            this.lblLeftFPCX.Margin = new System.Windows.Forms.Padding(0);
            this.lblLeftFPCX.Name = "lblLeftFPCX";
            this.lblLeftFPCX.Size = new System.Drawing.Size(100, 40);
            this.lblLeftFPCX.TabIndex = 0;
            this.lblLeftFPCX.Text = "FPC X";
            this.lblLeftFPCX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLeftFPCX.Click += new System.EventHandler(this.lblLeftFPCX_Click);
            // 
            // lblRightFPCX
            // 
            this.lblRightFPCX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRightFPCX.Location = new System.Drawing.Point(214, 63);
            this.lblRightFPCX.Margin = new System.Windows.Forms.Padding(0);
            this.lblRightFPCX.Name = "lblRightFPCX";
            this.lblRightFPCX.Size = new System.Drawing.Size(100, 40);
            this.lblRightFPCX.TabIndex = 0;
            this.lblRightFPCX.Text = "FPC X";
            this.lblRightFPCX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRightFPCX.Click += new System.EventHandler(this.lblRightFPCX_Click);
            // 
            // lblLeftFPCY
            // 
            this.lblLeftFPCY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeftFPCY.Location = new System.Drawing.Point(323, 12);
            this.lblLeftFPCY.Margin = new System.Windows.Forms.Padding(0);
            this.lblLeftFPCY.Name = "lblLeftFPCY";
            this.lblLeftFPCY.Size = new System.Drawing.Size(100, 40);
            this.lblLeftFPCY.TabIndex = 0;
            this.lblLeftFPCY.Text = "FPC Y";
            this.lblLeftFPCY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLeftFPCY.Click += new System.EventHandler(this.lblLeftFPCY_Click);
            // 
            // lblLeftPanelY
            // 
            this.lblLeftPanelY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeftPanelY.Location = new System.Drawing.Point(543, 12);
            this.lblLeftPanelY.Margin = new System.Windows.Forms.Padding(0);
            this.lblLeftPanelY.Name = "lblLeftPanelY";
            this.lblLeftPanelY.Size = new System.Drawing.Size(100, 40);
            this.lblLeftPanelY.TabIndex = 0;
            this.lblLeftPanelY.Text = "PANEL Y";
            this.lblLeftPanelY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLeftPanelY.Click += new System.EventHandler(this.lblLeftPanelY_Click);
            // 
            // lblLeftPanelX
            // 
            this.lblLeftPanelX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeftPanelX.Location = new System.Drawing.Point(433, 12);
            this.lblLeftPanelX.Margin = new System.Windows.Forms.Padding(0);
            this.lblLeftPanelX.Name = "lblLeftPanelX";
            this.lblLeftPanelX.Size = new System.Drawing.Size(100, 40);
            this.lblLeftPanelX.TabIndex = 0;
            this.lblLeftPanelX.Text = "PANEL X";
            this.lblLeftPanelX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLeftPanelX.Click += new System.EventHandler(this.lblLeftPanelX_Click);
            // 
            // chkUseTracking
            // 
            this.chkUseTracking.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkUseTracking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.chkUseTracking.Location = new System.Drawing.Point(3, 455);
            this.chkUseTracking.Name = "chkUseTracking";
            this.chkUseTracking.Size = new System.Drawing.Size(214, 53);
            this.chkUseTracking.TabIndex = 13;
            this.chkUseTracking.Text = "ROI Tracking : UNUSE";
            this.chkUseTracking.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkUseTracking.UseVisualStyleBackColor = false;
            this.chkUseTracking.CheckedChanged += new System.EventHandler(this.chkUseTracking_CheckedChanged);
            // 
            // tlpParams
            // 
            this.tlpParams.ColumnCount = 1;
            this.tlpParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpParams.Controls.Add(this.pnlCaliperParam, 0, 1);
            this.tlpParams.Controls.Add(this.pnlLeadParam, 0, 0);
            this.tlpParams.Location = new System.Drawing.Point(0, 152);
            this.tlpParams.Margin = new System.Windows.Forms.Padding(0);
            this.tlpParams.Name = "tlpParams";
            this.tlpParams.RowCount = 2;
            this.tlpParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpParams.Size = new System.Drawing.Size(578, 265);
            this.tlpParams.TabIndex = 25;
            // 
            // pnlCaliperParam
            // 
            this.pnlCaliperParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCaliperParam.Location = new System.Drawing.Point(0, 60);
            this.pnlCaliperParam.Margin = new System.Windows.Forms.Padding(0);
            this.pnlCaliperParam.Name = "pnlCaliperParam";
            this.pnlCaliperParam.Padding = new System.Windows.Forms.Padding(4);
            this.pnlCaliperParam.Size = new System.Drawing.Size(578, 205);
            this.pnlCaliperParam.TabIndex = 15;
            // 
            // pnlLeadParam
            // 
            this.pnlLeadParam.Controls.Add(this.lblLeadCount);
            this.pnlLeadParam.Controls.Add(this.lblLead);
            this.pnlLeadParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeadParam.Location = new System.Drawing.Point(0, 0);
            this.pnlLeadParam.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLeadParam.Name = "pnlLeadParam";
            this.pnlLeadParam.Size = new System.Drawing.Size(578, 60);
            this.pnlLeadParam.TabIndex = 16;
            // 
            // lblLeadCount
            // 
            this.lblLeadCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLeadCount.Location = new System.Drawing.Point(217, 10);
            this.lblLeadCount.Margin = new System.Windows.Forms.Padding(0);
            this.lblLeadCount.Name = "lblLeadCount";
            this.lblLeadCount.Size = new System.Drawing.Size(209, 40);
            this.lblLeadCount.TabIndex = 2;
            this.lblLeadCount.Text = "0";
            this.lblLeadCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLeadCount.Click += new System.EventHandler(this.lblLeadCount_Click);
            // 
            // lblLead
            // 
            this.lblLead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLead.Location = new System.Drawing.Point(5, 10);
            this.lblLead.Margin = new System.Windows.Forms.Padding(0);
            this.lblLead.Name = "lblLead";
            this.lblLead.Size = new System.Drawing.Size(200, 40);
            this.lblLead.TabIndex = 1;
            this.lblLead.Text = "Lead Count";
            this.lblLead.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.Size = new System.Drawing.Size(685, 685);
            this.Load += new System.EventHandler(this.AlignControl_Load);
            this.tlpAlign.ResumeLayout(false);
            this.pnlPosition.ResumeLayout(false);
            this.tlpParams.ResumeLayout(false);
            this.pnlLeadParam.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAlign;
        private System.Windows.Forms.Label lblParameter;
        private System.Windows.Forms.CheckBox chkUseTracking;
        private System.Windows.Forms.Label lblLeftFPCX;
        private System.Windows.Forms.Label lblLeftFPCY;
        private System.Windows.Forms.Label lblLeftPanelX;
        private System.Windows.Forms.Label lblLeftPanelY;
        private System.Windows.Forms.Label lblRightFPCX;
        private System.Windows.Forms.Label lblRightFPCY;
        private System.Windows.Forms.Label lblRightPanelX;
        private System.Windows.Forms.Label lblRightPanelY;
        private System.Windows.Forms.Label lblLeft;
        private System.Windows.Forms.Label lblRight;
        private System.Windows.Forms.Panel pnlPosition;
        private System.Windows.Forms.Panel pnlCaliperParam;
        private System.Windows.Forms.TableLayoutPanel tlpParams;
        private System.Windows.Forms.Panel pnlLeadParam;
        private System.Windows.Forms.Label lblLeadCount;
        private System.Windows.Forms.Label lblLead;
    }
}
