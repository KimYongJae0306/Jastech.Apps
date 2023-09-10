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
            this.tmrManualJudge = new System.Windows.Forms.Timer(this.components);
            this.tlpManualJudge = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTop = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tlpItem = new System.Windows.Forms.TableLayoutPanel();
            this.lblItem = new System.Windows.Forms.Label();
            this.tlpJudgeItem = new System.Windows.Forms.TableLayoutPanel();
            this.tlpJudge = new System.Windows.Forms.TableLayoutPanel();
            this.imageIcon = new System.Windows.Forms.PictureBox();
            this.lblAlign = new System.Windows.Forms.Label();
            this.lblAkkon = new System.Windows.Forms.Label();
            this.lblOK = new System.Windows.Forms.Label();
            this.lblNG = new System.Windows.Forms.Label();
            this.tlpManualJudge.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tlpItem.SuspendLayout();
            this.tlpJudgeItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrManualJudge
            // 
            this.tmrManualJudge.Tick += new System.EventHandler(this.tmrManualJudge_Tick);
            // 
            // tlpManualJudge
            // 
            this.tlpManualJudge.ColumnCount = 1;
            this.tlpManualJudge.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpManualJudge.Controls.Add(this.pnlTop, 0, 0);
            this.tlpManualJudge.Controls.Add(this.panel2, 0, 1);
            this.tlpManualJudge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpManualJudge.Location = new System.Drawing.Point(0, 0);
            this.tlpManualJudge.Margin = new System.Windows.Forms.Padding(0);
            this.tlpManualJudge.Name = "tlpManualJudge";
            this.tlpManualJudge.RowCount = 2;
            this.tlpManualJudge.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpManualJudge.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpManualJudge.Size = new System.Drawing.Size(663, 433);
            this.tlpManualJudge.TabIndex = 2;
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
            this.pnlTop.Size = new System.Drawing.Size(663, 60);
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lblOK, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblNG, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 313);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(543, 60);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 60);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(663, 373);
            this.panel2.TabIndex = 4;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.imageIcon, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(663, 373);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tlpJudgeItem);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(120, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(543, 373);
            this.panel3.TabIndex = 3;
            // 
            // tlpItem
            // 
            this.tlpItem.ColumnCount = 3;
            this.tlpItem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpItem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpItem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpItem.Controls.Add(this.lblItem, 0, 0);
            this.tlpItem.Controls.Add(this.lblAkkon, 1, 0);
            this.tlpItem.Controls.Add(this.lblAlign, 1, 0);
            this.tlpItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpItem.Location = new System.Drawing.Point(0, 0);
            this.tlpItem.Margin = new System.Windows.Forms.Padding(0);
            this.tlpItem.Name = "tlpItem";
            this.tlpItem.RowCount = 1;
            this.tlpItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpItem.Size = new System.Drawing.Size(543, 60);
            this.tlpItem.TabIndex = 291;
            // 
            // lblItem
            // 
            this.lblItem.BackColor = System.Drawing.Color.DarkSlateGray;
            this.lblItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblItem.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblItem.ForeColor = System.Drawing.Color.White;
            this.lblItem.Location = new System.Drawing.Point(0, 0);
            this.lblItem.Margin = new System.Windows.Forms.Padding(0);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(181, 60);
            this.lblItem.TabIndex = 5;
            this.lblItem.Text = "Item";
            this.lblItem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpJudgeItem
            // 
            this.tlpJudgeItem.ColumnCount = 1;
            this.tlpJudgeItem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpJudgeItem.Controls.Add(this.tableLayoutPanel1, 0, 2);
            this.tlpJudgeItem.Controls.Add(this.tlpItem, 0, 0);
            this.tlpJudgeItem.Controls.Add(this.tlpJudge, 0, 1);
            this.tlpJudgeItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpJudgeItem.Location = new System.Drawing.Point(0, 0);
            this.tlpJudgeItem.Margin = new System.Windows.Forms.Padding(0);
            this.tlpJudgeItem.Name = "tlpJudgeItem";
            this.tlpJudgeItem.RowCount = 3;
            this.tlpJudgeItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpJudgeItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpJudgeItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpJudgeItem.Size = new System.Drawing.Size(543, 373);
            this.tlpJudgeItem.TabIndex = 292;
            // 
            // tlpJudge
            // 
            this.tlpJudge.ColumnCount = 1;
            this.tlpJudge.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpJudge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpJudge.Location = new System.Drawing.Point(0, 60);
            this.tlpJudge.Margin = new System.Windows.Forms.Padding(0);
            this.tlpJudge.Name = "tlpJudge";
            this.tlpJudge.RowCount = 1;
            this.tlpJudge.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpJudge.Size = new System.Drawing.Size(543, 253);
            this.tlpJudge.TabIndex = 292;
            // 
            // imageIcon
            // 
            this.imageIcon.BackColor = System.Drawing.Color.Transparent;
            this.imageIcon.BackgroundImage = global::Jastech.Apps.Winform.Properties.Resources.Warning;
            this.imageIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.imageIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageIcon.ErrorImage = null;
            this.imageIcon.Location = new System.Drawing.Point(0, 0);
            this.imageIcon.Margin = new System.Windows.Forms.Padding(0);
            this.imageIcon.Name = "imageIcon";
            this.imageIcon.Size = new System.Drawing.Size(120, 373);
            this.imageIcon.TabIndex = 0;
            this.imageIcon.TabStop = false;
            // 
            // lblAlign
            // 
            this.lblAlign.BackColor = System.Drawing.Color.Black;
            this.lblAlign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAlign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAlign.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblAlign.ForeColor = System.Drawing.Color.White;
            this.lblAlign.Location = new System.Drawing.Point(181, 0);
            this.lblAlign.Margin = new System.Windows.Forms.Padding(0);
            this.lblAlign.Name = "lblAlign";
            this.lblAlign.Size = new System.Drawing.Size(181, 60);
            this.lblAlign.TabIndex = 3;
            this.lblAlign.Text = "Align";
            this.lblAlign.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAkkon
            // 
            this.lblAkkon.BackColor = System.Drawing.Color.Black;
            this.lblAkkon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAkkon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAkkon.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblAkkon.ForeColor = System.Drawing.Color.White;
            this.lblAkkon.Location = new System.Drawing.Point(362, 0);
            this.lblAkkon.Margin = new System.Windows.Forms.Padding(0);
            this.lblAkkon.Name = "lblAkkon";
            this.lblAkkon.Size = new System.Drawing.Size(181, 60);
            this.lblAkkon.TabIndex = 4;
            this.lblAkkon.Text = "Akkon";
            this.lblAkkon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOK
            // 
            this.lblOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblOK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblOK.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.lblOK.ForeColor = System.Drawing.Color.White;
            this.lblOK.Location = new System.Drawing.Point(0, 0);
            this.lblOK.Margin = new System.Windows.Forms.Padding(0);
            this.lblOK.Name = "lblOK";
            this.lblOK.Size = new System.Drawing.Size(261, 60);
            this.lblOK.TabIndex = 1;
            this.lblOK.Text = "OK";
            this.lblOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOK.Click += new System.EventHandler(this.lblOK_Click);
            // 
            // lblNG
            // 
            this.lblNG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblNG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNG.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblNG.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.lblNG.ForeColor = System.Drawing.Color.White;
            this.lblNG.Location = new System.Drawing.Point(281, 0);
            this.lblNG.Margin = new System.Windows.Forms.Padding(0);
            this.lblNG.Name = "lblNG";
            this.lblNG.Size = new System.Drawing.Size(262, 60);
            this.lblNG.TabIndex = 1;
            this.lblNG.Text = "NG";
            this.lblNG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNG.Click += new System.EventHandler(this.lblNG_Click);
            // 
            // ManualJudgeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.ClientSize = new System.Drawing.Size(663, 433);
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
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tlpItem.ResumeLayout(false);
            this.tlpJudgeItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer tmrManualJudge;
        private System.Windows.Forms.TableLayoutPanel tlpManualJudge;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTop;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.PictureBox imageIcon;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tlpItem;
        private System.Windows.Forms.TableLayoutPanel tlpJudgeItem;
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.TableLayoutPanel tlpJudge;
        private System.Windows.Forms.Label lblAkkon;
        private System.Windows.Forms.Label lblAlign;
        private System.Windows.Forms.Label lblOK;
        private System.Windows.Forms.Label lblNG;
    }
}