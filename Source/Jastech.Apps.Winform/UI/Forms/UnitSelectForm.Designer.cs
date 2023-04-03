namespace Jastech.Apps.Winform.UI.Forms
{
    partial class UnitSelectForm
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
            this.tlpUnitSelectForm = new System.Windows.Forms.TableLayoutPanel();
            this.tlpBasicFunction = new System.Windows.Forms.TableLayoutPanel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.label13 = new System.Windows.Forms.Label();
            this.lblCancel = new System.Windows.Forms.Label();
            this.tlpUnitSelect = new System.Windows.Forms.TableLayoutPanel();
            this.tlpUnitSelectForm.SuspendLayout();
            this.tlpBasicFunction.SuspendLayout();
            this.panel10.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpUnitSelectForm
            // 
            this.tlpUnitSelectForm.ColumnCount = 1;
            this.tlpUnitSelectForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpUnitSelectForm.Controls.Add(this.tlpBasicFunction, 0, 1);
            this.tlpUnitSelectForm.Controls.Add(this.tlpUnitSelect, 0, 0);
            this.tlpUnitSelectForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpUnitSelectForm.Location = new System.Drawing.Point(0, 0);
            this.tlpUnitSelectForm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tlpUnitSelectForm.Name = "tlpUnitSelectForm";
            this.tlpUnitSelectForm.RowCount = 2;
            this.tlpUnitSelectForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpUnitSelectForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tlpUnitSelectForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpUnitSelectForm.Size = new System.Drawing.Size(584, 361);
            this.tlpUnitSelectForm.TabIndex = 3;
            // 
            // tlpBasicFunction
            // 
            this.tlpBasicFunction.ColumnCount = 3;
            this.tlpBasicFunction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBasicFunction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tlpBasicFunction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tlpBasicFunction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tlpBasicFunction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tlpBasicFunction.Controls.Add(this.panel10, 2, 0);
            this.tlpBasicFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBasicFunction.Location = new System.Drawing.Point(0, 305);
            this.tlpBasicFunction.Margin = new System.Windows.Forms.Padding(0);
            this.tlpBasicFunction.Name = "tlpBasicFunction";
            this.tlpBasicFunction.RowCount = 1;
            this.tlpBasicFunction.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBasicFunction.Size = new System.Drawing.Size(584, 56);
            this.tlpBasicFunction.TabIndex = 3;
            // 
            // panel10
            // 
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel10.Controls.Add(this.tableLayoutPanel11);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(412, 2);
            this.panel10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(169, 52);
            this.panel10.TabIndex = 4;
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 2;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Controls.Add(this.label13, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.lblCancel, 1, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel11.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(167, 50);
            this.tableLayoutPanel11.TabIndex = 0;
            // 
            // label13
            // 
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Image = global::Jastech.Apps.Winform.Properties.Resources.Cancel;
            this.label13.Location = new System.Drawing.Point(3, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 50);
            this.label13.TabIndex = 1;
            this.label13.Click += new System.EventHandler(this.lblCancel_Click);
            // 
            // lblCancel
            // 
            this.lblCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCancel.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblCancel.Location = new System.Drawing.Point(47, 0);
            this.lblCancel.Name = "lblCancel";
            this.lblCancel.Size = new System.Drawing.Size(117, 50);
            this.lblCancel.TabIndex = 0;
            this.lblCancel.Text = "Cancel";
            this.lblCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancel.Click += new System.EventHandler(this.lblCancel_Click);
            // 
            // tlpUnitSelect
            // 
            this.tlpUnitSelect.ColumnCount = 1;
            this.tlpUnitSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpUnitSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpUnitSelect.Location = new System.Drawing.Point(0, 0);
            this.tlpUnitSelect.Margin = new System.Windows.Forms.Padding(0);
            this.tlpUnitSelect.Name = "tlpUnitSelect";
            this.tlpUnitSelect.RowCount = 1;
            this.tlpUnitSelect.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpUnitSelect.Size = new System.Drawing.Size(584, 305);
            this.tlpUnitSelect.TabIndex = 4;
            // 
            // UnitSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.ControlBox = false;
            this.Controls.Add(this.tlpUnitSelectForm);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UnitSelectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.tlpUnitSelectForm.ResumeLayout(false);
            this.tlpBasicFunction.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tlpUnitSelectForm;
        private System.Windows.Forms.TableLayoutPanel tlpBasicFunction;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblCancel;
        private System.Windows.Forms.TableLayoutPanel tlpUnitSelect;
    }
}