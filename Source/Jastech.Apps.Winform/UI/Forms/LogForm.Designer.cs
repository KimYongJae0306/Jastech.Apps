namespace Jastech.Framework.Winform.Forms
{
    partial class LogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpLog = new System.Windows.Forms.TableLayoutPanel();
            this.tlpBasicFunction = new System.Windows.Forms.TableLayoutPanel();
            this.tvwLogPath = new System.Windows.Forms.TreeView();
            this.cdrMonthCalendar = new System.Windows.Forms.MonthCalendar();
            this.btnExit = new System.Windows.Forms.Button();
            this.tlpContents = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLogType = new System.Windows.Forms.Panel();
            this.lblUPH = new System.Windows.Forms.Label();
            this.lblAkkonTrend = new System.Windows.Forms.Label();
            this.lblLog = new System.Windows.Forms.Label();
            this.lblImage = new System.Windows.Forms.Label();
            this.lblAlignTrend = new System.Windows.Forms.Label();
            this.pnlContents = new System.Windows.Forms.Panel();
            this.tlpLog.SuspendLayout();
            this.tlpBasicFunction.SuspendLayout();
            this.tlpContents.SuspendLayout();
            this.pnlLogType.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpLog
            // 
            this.tlpLog.ColumnCount = 2;
            this.tlpLog.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLog.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tlpLog.Controls.Add(this.tlpBasicFunction, 1, 0);
            this.tlpLog.Controls.Add(this.tlpContents, 0, 0);
            this.tlpLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLog.Location = new System.Drawing.Point(0, 0);
            this.tlpLog.Margin = new System.Windows.Forms.Padding(0);
            this.tlpLog.Name = "tlpLog";
            this.tlpLog.RowCount = 1;
            this.tlpLog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 162F));
            this.tlpLog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpLog.Size = new System.Drawing.Size(1184, 981);
            this.tlpLog.TabIndex = 0;
            // 
            // tlpBasicFunction
            // 
            this.tlpBasicFunction.ColumnCount = 1;
            this.tlpBasicFunction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tlpBasicFunction.Controls.Add(this.tvwLogPath, 0, 1);
            this.tlpBasicFunction.Controls.Add(this.cdrMonthCalendar, 0, 0);
            this.tlpBasicFunction.Controls.Add(this.btnExit, 0, 2);
            this.tlpBasicFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBasicFunction.Location = new System.Drawing.Point(784, 0);
            this.tlpBasicFunction.Margin = new System.Windows.Forms.Padding(0);
            this.tlpBasicFunction.Name = "tlpBasicFunction";
            this.tlpBasicFunction.RowCount = 3;
            this.tlpBasicFunction.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 162F));
            this.tlpBasicFunction.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBasicFunction.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpBasicFunction.Size = new System.Drawing.Size(400, 981);
            this.tlpBasicFunction.TabIndex = 9;
            // 
            // tvwLogPath
            // 
            this.tvwLogPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.tvwLogPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwLogPath.ForeColor = System.Drawing.Color.White;
            this.tvwLogPath.Location = new System.Drawing.Point(0, 162);
            this.tvwLogPath.Margin = new System.Windows.Forms.Padding(0);
            this.tvwLogPath.Name = "tvwLogPath";
            this.tvwLogPath.Size = new System.Drawing.Size(400, 659);
            this.tvwLogPath.TabIndex = 11;
            this.tvwLogPath.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvwLogPath_NodeMouseClick);
            // 
            // cdrMonthCalendar
            // 
            this.cdrMonthCalendar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.cdrMonthCalendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cdrMonthCalendar.ForeColor = System.Drawing.Color.White;
            this.cdrMonthCalendar.Location = new System.Drawing.Point(0, 0);
            this.cdrMonthCalendar.Margin = new System.Windows.Forms.Padding(0);
            this.cdrMonthCalendar.Name = "cdrMonthCalendar";
            this.cdrMonthCalendar.TabIndex = 7;
            this.cdrMonthCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.cdrMonthCalendar_DateChanged);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.btnExit.Location = new System.Drawing.Point(3, 824);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(120, 94);
            this.btnExit.TabIndex = 294;
            this.btnExit.Text = "EXIT";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // tlpContents
            // 
            this.tlpContents.ColumnCount = 1;
            this.tlpContents.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContents.Controls.Add(this.pnlLogType, 0, 0);
            this.tlpContents.Controls.Add(this.pnlContents, 0, 1);
            this.tlpContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpContents.Location = new System.Drawing.Point(0, 0);
            this.tlpContents.Margin = new System.Windows.Forms.Padding(0);
            this.tlpContents.Name = "tlpContents";
            this.tlpContents.RowCount = 2;
            this.tlpContents.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 162F));
            this.tlpContents.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContents.Size = new System.Drawing.Size(784, 981);
            this.tlpContents.TabIndex = 10;
            // 
            // pnlLogType
            // 
            this.pnlLogType.Controls.Add(this.lblUPH);
            this.pnlLogType.Controls.Add(this.lblAkkonTrend);
            this.pnlLogType.Controls.Add(this.lblLog);
            this.pnlLogType.Controls.Add(this.lblImage);
            this.pnlLogType.Controls.Add(this.lblAlignTrend);
            this.pnlLogType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLogType.Location = new System.Drawing.Point(0, 0);
            this.pnlLogType.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLogType.Name = "pnlLogType";
            this.pnlLogType.Size = new System.Drawing.Size(784, 162);
            this.pnlLogType.TabIndex = 8;
            // 
            // lblUPH
            // 
            this.lblUPH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUPH.Location = new System.Drawing.Point(648, 34);
            this.lblUPH.Margin = new System.Windows.Forms.Padding(0);
            this.lblUPH.Name = "lblUPH";
            this.lblUPH.Size = new System.Drawing.Size(120, 60);
            this.lblUPH.TabIndex = 2;
            this.lblUPH.Text = "UPH";
            this.lblUPH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUPH.Click += new System.EventHandler(this.lblUPH_Click);
            // 
            // lblAkkonTrend
            // 
            this.lblAkkonTrend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAkkonTrend.Location = new System.Drawing.Point(494, 34);
            this.lblAkkonTrend.Margin = new System.Windows.Forms.Padding(0);
            this.lblAkkonTrend.Name = "lblAkkonTrend";
            this.lblAkkonTrend.Size = new System.Drawing.Size(120, 60);
            this.lblAkkonTrend.TabIndex = 1;
            this.lblAkkonTrend.Text = "Akkon Trend";
            this.lblAkkonTrend.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAkkonTrend.Click += new System.EventHandler(this.lblAkkonTrend_Click);
            // 
            // lblLog
            // 
            this.lblLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLog.Location = new System.Drawing.Point(20, 34);
            this.lblLog.Margin = new System.Windows.Forms.Padding(0);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(120, 60);
            this.lblLog.TabIndex = 1;
            this.lblLog.Text = "Log";
            this.lblLog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLog.Click += new System.EventHandler(this.lblLog_Click);
            // 
            // lblImage
            // 
            this.lblImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblImage.Location = new System.Drawing.Point(179, 34);
            this.lblImage.Margin = new System.Windows.Forms.Padding(0);
            this.lblImage.Name = "lblImage";
            this.lblImage.Size = new System.Drawing.Size(120, 60);
            this.lblImage.TabIndex = 1;
            this.lblImage.Text = "Image";
            this.lblImage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblImage.Click += new System.EventHandler(this.lblImage_Click);
            // 
            // lblAlignTrend
            // 
            this.lblAlignTrend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAlignTrend.Location = new System.Drawing.Point(342, 34);
            this.lblAlignTrend.Margin = new System.Windows.Forms.Padding(0);
            this.lblAlignTrend.Name = "lblAlignTrend";
            this.lblAlignTrend.Size = new System.Drawing.Size(120, 60);
            this.lblAlignTrend.TabIndex = 1;
            this.lblAlignTrend.Text = "Align Trend";
            this.lblAlignTrend.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAlignTrend.Click += new System.EventHandler(this.lblAlignTrend_Click);
            // 
            // pnlContents
            // 
            this.pnlContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContents.Location = new System.Drawing.Point(0, 162);
            this.pnlContents.Margin = new System.Windows.Forms.Padding(0);
            this.pnlContents.Name = "pnlContents";
            this.pnlContents.Size = new System.Drawing.Size(784, 819);
            this.pnlContents.TabIndex = 9;
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.ClientSize = new System.Drawing.Size(1184, 981);
            this.ControlBox = false;
            this.Controls.Add(this.tlpLog);
            this.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LogForm_Load);
            this.tlpLog.ResumeLayout(false);
            this.tlpBasicFunction.ResumeLayout(false);
            this.tlpContents.ResumeLayout(false);
            this.pnlLogType.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpLog;
        private System.Windows.Forms.MonthCalendar cdrMonthCalendar;
        private System.Windows.Forms.Panel pnlLogType;
        private System.Windows.Forms.Label lblUPH;
        private System.Windows.Forms.Label lblAkkonTrend;
        private System.Windows.Forms.Label lblAlignTrend;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TableLayoutPanel tlpBasicFunction;
        private System.Windows.Forms.TableLayoutPanel tlpContents;
        private System.Windows.Forms.Panel pnlContents;
        private System.Windows.Forms.TreeView tvwLogPath;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.Label lblImage;
    }
}