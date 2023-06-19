namespace Jastech.Apps.Winform.UI.Forms
{
    partial class PlcStatusForm
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
            this.components = new System.ComponentModel.Container();
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel74 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlDisplay = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDataPage = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnModelPage = new System.Windows.Forms.Button();
            this.tableLayoutPanel74.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Interval = 300;
            this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // tableLayoutPanel74
            // 
            this.tableLayoutPanel74.ColumnCount = 1;
            this.tableLayoutPanel74.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel74.Controls.Add(this.pnlDisplay, 0, 0);
            this.tableLayoutPanel74.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanel74.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel74.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel74.Name = "tableLayoutPanel74";
            this.tableLayoutPanel74.RowCount = 2;
            this.tableLayoutPanel74.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel74.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel74.Size = new System.Drawing.Size(1584, 861);
            this.tableLayoutPanel74.TabIndex = 315;
            // 
            // pnlDisplay
            // 
            this.pnlDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDisplay.Location = new System.Drawing.Point(3, 3);
            this.pnlDisplay.Name = "pnlDisplay";
            this.pnlDisplay.Size = new System.Drawing.Size(1578, 755);
            this.pnlDisplay.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnModelPage, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnExit, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDataPage, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 764);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1578, 94);
            this.tableLayoutPanel1.TabIndex = 316;
            // 
            // btnDataPage
            // 
            this.btnDataPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.btnDataPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDataPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDataPage.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.btnDataPage.ForeColor = System.Drawing.Color.White;
            this.btnDataPage.Location = new System.Drawing.Point(3, 3);
            this.btnDataPage.Name = "btnDataPage";
            this.btnDataPage.Size = new System.Drawing.Size(94, 88);
            this.btnDataPage.TabIndex = 316;
            this.btnDataPage.Text = "DATA";
            this.btnDataPage.UseVisualStyleBackColor = false;
            this.btnDataPage.Click += new System.EventHandler(this.btnDataPage_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Image = global::Jastech.Apps.Winform.Properties.Resources.Exit_White;
            this.btnExit.Location = new System.Drawing.Point(1481, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(94, 88);
            this.btnExit.TabIndex = 294;
            this.btnExit.Text = "EXIT";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnModelPage
            // 
            this.btnModelPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.btnModelPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnModelPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnModelPage.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.btnModelPage.ForeColor = System.Drawing.Color.White;
            this.btnModelPage.Location = new System.Drawing.Point(103, 3);
            this.btnModelPage.Name = "btnModelPage";
            this.btnModelPage.Size = new System.Drawing.Size(94, 88);
            this.btnModelPage.TabIndex = 317;
            this.btnModelPage.Text = "MODEL";
            this.btnModelPage.UseVisualStyleBackColor = false;
            this.btnModelPage.Click += new System.EventHandler(this.btnModelPage_Click);
            // 
            // PlcStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.ClientSize = new System.Drawing.Size(1584, 861);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel74);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "PlcStatusForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Load += new System.EventHandler(this.PlcStatusForm_Load);
            this.tableLayoutPanel74.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Timer UpdateTimer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel74;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlDisplay;
        private System.Windows.Forms.Button btnDataPage;
        private System.Windows.Forms.Button btnModelPage;
    }
}