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
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblOK = new System.Windows.Forms.Label();
            this.lblNG = new System.Windows.Forms.Label();
            this.tmrManualJudge = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.Location = new System.Drawing.Point(245, 47);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(185, 139);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "label1";
            // 
            // lblOK
            // 
            this.lblOK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOK.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.lblOK.ForeColor = System.Drawing.Color.White;
            this.lblOK.Location = new System.Drawing.Point(63, 231);
            this.lblOK.Name = "lblOK";
            this.lblOK.Size = new System.Drawing.Size(138, 44);
            this.lblOK.TabIndex = 1;
            this.lblOK.Text = "OK";
            this.lblOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOK.Click += new System.EventHandler(this.lblOK_Click);
            // 
            // lblNG
            // 
            this.lblNG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNG.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.lblNG.ForeColor = System.Drawing.Color.White;
            this.lblNG.Location = new System.Drawing.Point(292, 231);
            this.lblNG.Name = "lblNG";
            this.lblNG.Size = new System.Drawing.Size(138, 44);
            this.lblNG.TabIndex = 1;
            this.lblNG.Text = "NG";
            this.lblNG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNG.Click += new System.EventHandler(this.lblNG_Click);
            // 
            // tmrManualJudge
            // 
            this.tmrManualJudge.Tick += new System.EventHandler(this.tmrManualJudge_Tick);
            // 
            // ManualJudgeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.ClientSize = new System.Drawing.Size(491, 316);
            this.Controls.Add(this.lblNG);
            this.Controls.Add(this.lblOK);
            this.Controls.Add(this.lblMessage);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ManualJudgeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ManualJudgeForm";
            this.Load += new System.EventHandler(this.ManualJudgeForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblOK;
        private System.Windows.Forms.Label lblNG;
        private System.Windows.Forms.Timer tmrManualJudge;
    }
}