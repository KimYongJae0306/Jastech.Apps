namespace Jastech.Apps.Winform.UI.Controls
{
    partial class VisionCalibrationControl
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
            this.tlpVisionCalibration = new System.Windows.Forms.TableLayoutPanel();
            this.pnlAddROI = new System.Windows.Forms.Panel();
            this.lblAddROI = new System.Windows.Forms.Label();
            this.lblParameter = new System.Windows.Forms.Label();
            this.pnlParam = new System.Windows.Forms.Panel();
            this.lblResult = new System.Windows.Forms.Label();
            this.dgvCalibrationResult = new System.Windows.Forms.DataGridView();
            this.lblAuto = new System.Windows.Forms.Label();
            this.lblStop = new System.Windows.Forms.Label();
            this.lblXY = new System.Windows.Forms.Label();
            this.lblXYT = new System.Windows.Forms.Label();
            this.lblCalibrationMode = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpCalibrationMode = new System.Windows.Forms.TableLayoutPanel();
            this.tlpVisionCalibration.SuspendLayout();
            this.pnlAddROI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalibrationResult)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlpCalibrationMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpVisionCalibration
            // 
            this.tlpVisionCalibration.ColumnCount = 1;
            this.tlpVisionCalibration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpVisionCalibration.Controls.Add(this.pnlAddROI, 0, 1);
            this.tlpVisionCalibration.Controls.Add(this.lblParameter, 0, 0);
            this.tlpVisionCalibration.Controls.Add(this.pnlParam, 0, 2);
            this.tlpVisionCalibration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpVisionCalibration.Location = new System.Drawing.Point(0, 0);
            this.tlpVisionCalibration.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tlpVisionCalibration.Name = "tlpVisionCalibration";
            this.tlpVisionCalibration.RowCount = 3;
            this.tlpVisionCalibration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpVisionCalibration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tlpVisionCalibration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpVisionCalibration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVisionCalibration.Size = new System.Drawing.Size(911, 686);
            this.tlpVisionCalibration.TabIndex = 1;
            // 
            // pnlAddROI
            // 
            this.pnlAddROI.Controls.Add(this.tableLayoutPanel1);
            this.pnlAddROI.Controls.Add(this.lblStop);
            this.pnlAddROI.Controls.Add(this.lblAuto);
            this.pnlAddROI.Controls.Add(this.dgvCalibrationResult);
            this.pnlAddROI.Controls.Add(this.lblResult);
            this.pnlAddROI.Controls.Add(this.lblAddROI);
            this.pnlAddROI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAddROI.Location = new System.Drawing.Point(3, 35);
            this.pnlAddROI.Name = "pnlAddROI";
            this.pnlAddROI.Size = new System.Drawing.Size(905, 294);
            this.pnlAddROI.TabIndex = 8;
            // 
            // lblAddROI
            // 
            this.lblAddROI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAddROI.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblAddROI.Location = new System.Drawing.Point(11, 11);
            this.lblAddROI.Margin = new System.Windows.Forms.Padding(0);
            this.lblAddROI.Name = "lblAddROI";
            this.lblAddROI.Size = new System.Drawing.Size(100, 40);
            this.lblAddROI.TabIndex = 12;
            this.lblAddROI.Text = "Add ROI";
            this.lblAddROI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAddROI.Click += new System.EventHandler(this.lblAddROI_Click);
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
            this.lblParameter.Size = new System.Drawing.Size(911, 32);
            this.lblParameter.TabIndex = 0;
            this.lblParameter.Text = "Parameter";
            this.lblParameter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlParam
            // 
            this.pnlParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlParam.Location = new System.Drawing.Point(3, 335);
            this.pnlParam.Name = "pnlParam";
            this.pnlParam.Size = new System.Drawing.Size(905, 348);
            this.pnlParam.TabIndex = 1;
            // 
            // lblResult
            // 
            this.lblResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResult.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblResult.Location = new System.Drawing.Point(11, 124);
            this.lblResult.Margin = new System.Windows.Forms.Padding(0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(220, 40);
            this.lblResult.TabIndex = 13;
            this.lblResult.Text = "Result of Vision Calibration";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvCalibrationResult
            // 
            this.dgvCalibrationResult.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.dgvCalibrationResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCalibrationResult.Location = new System.Drawing.Point(11, 167);
            this.dgvCalibrationResult.Name = "dgvCalibrationResult";
            this.dgvCalibrationResult.RowTemplate.Height = 23;
            this.dgvCalibrationResult.Size = new System.Drawing.Size(878, 113);
            this.dgvCalibrationResult.TabIndex = 14;
            // 
            // lblAuto
            // 
            this.lblAuto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAuto.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblAuto.Location = new System.Drawing.Point(505, 13);
            this.lblAuto.Margin = new System.Windows.Forms.Padding(0);
            this.lblAuto.Name = "lblAuto";
            this.lblAuto.Size = new System.Drawing.Size(100, 40);
            this.lblAuto.TabIndex = 15;
            this.lblAuto.Text = "Auto";
            this.lblAuto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAuto.Click += new System.EventHandler(this.lblAuto_Click);
            // 
            // lblStop
            // 
            this.lblStop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStop.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblStop.Location = new System.Drawing.Point(639, 13);
            this.lblStop.Margin = new System.Windows.Forms.Padding(0);
            this.lblStop.Name = "lblStop";
            this.lblStop.Size = new System.Drawing.Size(100, 40);
            this.lblStop.TabIndex = 16;
            this.lblStop.Text = "Stop";
            this.lblStop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStop.Click += new System.EventHandler(this.lblStop_Click);
            // 
            // lblXY
            // 
            this.lblXY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblXY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblXY.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblXY.Location = new System.Drawing.Point(0, 0);
            this.lblXY.Margin = new System.Windows.Forms.Padding(0);
            this.lblXY.Name = "lblXY";
            this.lblXY.Size = new System.Drawing.Size(100, 40);
            this.lblXY.TabIndex = 17;
            this.lblXY.Text = "XY";
            this.lblXY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblXY.Click += new System.EventHandler(this.lblXY_Click);
            // 
            // lblXYT
            // 
            this.lblXYT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblXYT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblXYT.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblXYT.Location = new System.Drawing.Point(100, 0);
            this.lblXYT.Margin = new System.Windows.Forms.Padding(0);
            this.lblXYT.Name = "lblXYT";
            this.lblXYT.Size = new System.Drawing.Size(100, 40);
            this.lblXYT.TabIndex = 18;
            this.lblXYT.Text = "XYT";
            this.lblXYT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblXYT.Click += new System.EventHandler(this.lblXYT_Click);
            // 
            // lblCalibrationMode
            // 
            this.lblCalibrationMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblCalibrationMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCalibrationMode.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblCalibrationMode.ForeColor = System.Drawing.Color.White;
            this.lblCalibrationMode.Location = new System.Drawing.Point(0, 0);
            this.lblCalibrationMode.Margin = new System.Windows.Forms.Padding(0);
            this.lblCalibrationMode.Name = "lblCalibrationMode";
            this.lblCalibrationMode.Size = new System.Drawing.Size(200, 40);
            this.lblCalibrationMode.TabIndex = 19;
            this.lblCalibrationMode.Text = "Mode";
            this.lblCalibrationMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lblCalibrationMode, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tlpCalibrationMode, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(278, 13);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 80);
            this.tableLayoutPanel1.TabIndex = 20;
            // 
            // tlpCalibrationMode
            // 
            this.tlpCalibrationMode.ColumnCount = 2;
            this.tlpCalibrationMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCalibrationMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCalibrationMode.Controls.Add(this.lblXY, 0, 0);
            this.tlpCalibrationMode.Controls.Add(this.lblXYT, 1, 0);
            this.tlpCalibrationMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCalibrationMode.Location = new System.Drawing.Point(0, 40);
            this.tlpCalibrationMode.Margin = new System.Windows.Forms.Padding(0);
            this.tlpCalibrationMode.Name = "tlpCalibrationMode";
            this.tlpCalibrationMode.RowCount = 1;
            this.tlpCalibrationMode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCalibrationMode.Size = new System.Drawing.Size(200, 40);
            this.tlpCalibrationMode.TabIndex = 20;
            // 
            // VisionCalibrationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpVisionCalibration);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "VisionCalibrationControl";
            this.Size = new System.Drawing.Size(911, 686);
            this.Load += new System.EventHandler(this.VisionCalibrationControl_Load);
            this.tlpVisionCalibration.ResumeLayout(false);
            this.pnlAddROI.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalibrationResult)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tlpCalibrationMode.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpVisionCalibration;
        private System.Windows.Forms.Panel pnlAddROI;
        private System.Windows.Forms.Label lblAddROI;
        private System.Windows.Forms.Label lblParameter;
        private System.Windows.Forms.Panel pnlParam;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblAuto;
        private System.Windows.Forms.DataGridView dgvCalibrationResult;
        private System.Windows.Forms.Label lblStop;
        private System.Windows.Forms.Label lblXYT;
        private System.Windows.Forms.Label lblXY;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblCalibrationMode;
        private System.Windows.Forms.TableLayoutPanel tlpCalibrationMode;
    }
}
