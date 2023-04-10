namespace Jastech.Framework.Modeller.Forms
{
    partial class ModelForm
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
            this.pnlModelForm = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlModelForm
            // 
            this.pnlModelForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlModelForm.Location = new System.Drawing.Point(0, 0);
            this.pnlModelForm.Margin = new System.Windows.Forms.Padding(0);
            this.pnlModelForm.Name = "pnlModelForm";
            this.pnlModelForm.Size = new System.Drawing.Size(1389, 804);
            this.pnlModelForm.TabIndex = 0;
            // 
            // ModelForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1389, 804);
            this.ControlBox = false;
            this.Controls.Add(this.pnlModelForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "ModelForm";
            this.Text = " ";
            this.Load += new System.EventHandler(this.ModelForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlModelForm;
    }
}