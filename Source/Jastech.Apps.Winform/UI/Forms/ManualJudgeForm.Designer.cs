namespace Jastech.Apps.Winform.UI.Forms
{
    partial class ManualJudgeForm
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
            this.lblOK = new System.Windows.Forms.Label();
            this.lblNG = new System.Windows.Forms.Label();
            this.tmrManualJudge = new System.Windows.Forms.Timer(this.components);
            this.tlpManualJudge = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTop = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblFail = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblMessageText = new System.Windows.Forms.Label();
            this.imageIcon = new System.Windows.Forms.PictureBox();
            this.tlpManualJudge.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblOK
            // 
            this.lblOK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOK.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.lblOK.ForeColor = System.Drawing.Color.White;
            this.lblOK.Location = new System.Drawing.Point(60, 0);
            this.lblOK.Margin = new System.Windows.Forms.Padding(0);
            this.lblOK.Name = "lblOK";
            this.lblOK.Size = new System.Drawing.Size(120, 70);
            this.lblOK.TabIndex = 1;
            this.lblOK.Text = "OK";
            this.lblOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOK.Click += new System.EventHandler(this.lblOK_Click);
            // 
            // lblNG
            // 
            this.lblNG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNG.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.lblNG.ForeColor = System.Drawing.Color.White;
            this.lblNG.Location = new System.Drawing.Point(240, 0);
            this.lblNG.Margin = new System.Windows.Forms.Padding(0);
            this.lblNG.Name = "lblNG";
            this.lblNG.Size = new System.Drawing.Size(120, 70);
            this.lblNG.TabIndex = 1;
            this.lblNG.Text = "NG";
            this.lblNG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNG.Click += new System.EventHandler(this.lblNG_Click);
            // 
            // tmrManualJudge
            // 
            this.tmrManualJudge.Tick += new System.EventHandler(this.tmrManualJudge_Tick);
            // 
            // tlpManualJudge
            // 
            this.tlpManualJudge.ColumnCount = 1;
            this.tlpManualJudge.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpManualJudge.Controls.Add(this.pnlTop, 0, 0);
            this.tlpManualJudge.Controls.Add(this.panel1, 0, 2);
            this.tlpManualJudge.Controls.Add(this.panel2, 0, 1);
            this.tlpManualJudge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpManualJudge.Location = new System.Drawing.Point(0, 0);
            this.tlpManualJudge.Margin = new System.Windows.Forms.Padding(0);
            this.tlpManualJudge.Name = "tlpManualJudge";
            this.tlpManualJudge.RowCount = 3;
            this.tlpManualJudge.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpManualJudge.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 72.65625F));
            this.tlpManualJudge.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.34375F));
            this.tlpManualJudge.Size = new System.Drawing.Size(600, 316);
            this.tlpManualJudge.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 246);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 70);
            this.panel1.TabIndex = 2;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.pnlTop.Controls.Add(this.lblTop);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.ForeColor = System.Drawing.Color.White;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(600, 60);
            this.pnlTop.TabIndex = 3;
            this.pnlTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTop_MouseDown);
            this.pnlTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlTop_MouseMove);
            // 
            // lblTop
            // 
            this.lblTop.AutoSize = true;
            this.lblTop.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTop.ForeColor = System.Drawing.Color.White;
            this.lblTop.Location = new System.Drawing.Point(12, 14);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(154, 30);
            this.lblTop.TabIndex = 1;
            this.lblTop.Text = "Manual Judge";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 60);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(600, 186);
            this.panel2.TabIndex = 4;
            // 
            // lblFail
            // 
            this.lblFail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFail.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.lblFail.ForeColor = System.Drawing.Color.White;
            this.lblFail.Location = new System.Drawing.Point(420, 0);
            this.lblFail.Margin = new System.Windows.Forms.Padding(0);
            this.lblFail.Name = "lblFail";
            this.lblFail.Size = new System.Drawing.Size(120, 70);
            this.lblFail.TabIndex = 2;
            this.lblFail.Text = "FAIL";
            this.lblFail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFail.Click += new System.EventHandler(this.lblFail_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.lblFail, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblOK, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblNG, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(600, 70);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.lblMessage, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.imageIcon, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblMessageText, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(600, 186);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // lblMessage
            // 
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessage.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(243, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(354, 186);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMessageText
            // 
            this.lblMessageText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblMessageText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMessageText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessageText.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMessageText.ForeColor = System.Drawing.Color.White;
            this.lblMessageText.Location = new System.Drawing.Point(123, 0);
            this.lblMessageText.Name = "lblMessageText";
            this.lblMessageText.Size = new System.Drawing.Size(114, 186);
            this.lblMessageText.TabIndex = 2;
            this.lblMessageText.Text = "Message";
            this.lblMessageText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imageIcon
            // 
            this.imageIcon.BackColor = System.Drawing.Color.Transparent;
            this.imageIcon.BackgroundImage = global::Jastech.Apps.Winform.Properties.Resources.Warning;
            this.imageIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.imageIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageIcon.ErrorImage = null;
            this.imageIcon.Location = new System.Drawing.Point(3, 3);
            this.imageIcon.Name = "imageIcon";
            this.imageIcon.Size = new System.Drawing.Size(114, 180);
            this.imageIcon.TabIndex = 0;
            this.imageIcon.TabStop = false;
            // 
            // ManualJudgeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.ClientSize = new System.Drawing.Size(600, 316);
            this.Controls.Add(this.tlpManualJudge);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ManualJudgeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ManualJudgeForm";
            this.Load += new System.EventHandler(this.ManualJudgeForm_Load);
            this.tlpManualJudge.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblOK;
        private System.Windows.Forms.Label lblNG;
        private System.Windows.Forms.Timer tmrManualJudge;
        private System.Windows.Forms.TableLayoutPanel tlpManualJudge;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTop;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblFail;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.PictureBox imageIcon;
        private System.Windows.Forms.Label lblMessageText;
    }
}