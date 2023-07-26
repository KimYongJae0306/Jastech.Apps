namespace Jastech.Apps.Winform.UI.Forms
{
    partial class ProgramSelectForm
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
            System.Windows.Forms.Label lblMenuDesc1;
            this.cbxProgramTypes = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            lblMenuDesc1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMenuDesc1
            // 
            lblMenuDesc1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            lblMenuDesc1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            lblMenuDesc1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            lblMenuDesc1.ForeColor = System.Drawing.Color.White;
            lblMenuDesc1.Location = new System.Drawing.Point(9, 12);
            lblMenuDesc1.Margin = new System.Windows.Forms.Padding(0);
            lblMenuDesc1.Name = "lblMenuDesc1";
            lblMenuDesc1.Size = new System.Drawing.Size(110, 30);
            lblMenuDesc1.TabIndex = 295;
            lblMenuDesc1.Text = "Program Type :";
            lblMenuDesc1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbxProgramTypes
            // 
            this.cbxProgramTypes.BackColor = System.Drawing.Color.White;
            this.cbxProgramTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxProgramTypes.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbxProgramTypes.FormattingEnabled = true;
            this.cbxProgramTypes.Location = new System.Drawing.Point(122, 12);
            this.cbxProgramTypes.Name = "cbxProgramTypes";
            this.cbxProgramTypes.Size = new System.Drawing.Size(271, 28);
            this.cbxProgramTypes.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnOK.FlatAppearance.BorderSize = 2;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(290, 55);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(103, 38);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "Confirm";
            this.btnOK.UseVisualStyleBackColor = false;
            // 
            // ProgramSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.ClientSize = new System.Drawing.Size(403, 106);
            this.Controls.Add(lblMenuDesc1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbxProgramTypes);
            this.Name = "ProgramSelectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choose a Program Type";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.ComboBox cbxProgramTypes;
    }
}