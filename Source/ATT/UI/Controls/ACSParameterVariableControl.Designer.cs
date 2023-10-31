namespace Jastech.Apps.Winform.UI.Controls
{
    partial class ACSParameterVariableControl
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
            this.tlpACSVariableParameter = new System.Windows.Forms.TableLayoutPanel();
            this.lblVariable4Value = new System.Windows.Forms.Label();
            this.lblVariable4 = new System.Windows.Forms.Label();
            this.lblVariable1Value = new System.Windows.Forms.Label();
            this.lblVariable1 = new System.Windows.Forms.Label();
            this.lblVariable2 = new System.Windows.Forms.Label();
            this.lblVariable2Value = new System.Windows.Forms.Label();
            this.lblVariable3 = new System.Windows.Forms.Label();
            this.lblVariable3Value = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.lblVariable5 = new System.Windows.Forms.Label();
            this.lblVariable5Value = new System.Windows.Forms.Label();
            this.grpAxisName = new System.Windows.Forms.GroupBox();
            this.tlpACSVariableParameter.SuspendLayout();
            this.grpAxisName.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpACSVariableParameter
            // 
            this.tlpACSVariableParameter.ColumnCount = 8;
            this.tlpACSVariableParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpACSVariableParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpACSVariableParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tlpACSVariableParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpACSVariableParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpACSVariableParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tlpACSVariableParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpACSVariableParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpACSVariableParameter.Controls.Add(this.lblVariable4Value, 0, 1);
            this.tlpACSVariableParameter.Controls.Add(this.lblVariable4, 0, 1);
            this.tlpACSVariableParameter.Controls.Add(this.lblVariable1Value, 1, 0);
            this.tlpACSVariableParameter.Controls.Add(this.lblVariable1, 0, 0);
            this.tlpACSVariableParameter.Controls.Add(this.lblVariable2, 3, 0);
            this.tlpACSVariableParameter.Controls.Add(this.lblVariable2Value, 4, 0);
            this.tlpACSVariableParameter.Controls.Add(this.lblVariable3, 6, 0);
            this.tlpACSVariableParameter.Controls.Add(this.lblVariable3Value, 7, 0);
            this.tlpACSVariableParameter.Controls.Add(this.btnApply, 6, 1);
            this.tlpACSVariableParameter.Controls.Add(this.lblVariable5, 3, 1);
            this.tlpACSVariableParameter.Controls.Add(this.lblVariable5Value, 4, 1);
            this.tlpACSVariableParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpACSVariableParameter.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.tlpACSVariableParameter.Location = new System.Drawing.Point(3, 23);
            this.tlpACSVariableParameter.Margin = new System.Windows.Forms.Padding(0);
            this.tlpACSVariableParameter.Name = "tlpACSVariableParameter";
            this.tlpACSVariableParameter.RowCount = 2;
            this.tlpACSVariableParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpACSVariableParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpACSVariableParameter.Size = new System.Drawing.Size(694, 134);
            this.tlpACSVariableParameter.TabIndex = 1;
            // 
            // lblVariable4Value
            // 
            this.lblVariable4Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblVariable4Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVariable4Value.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVariable4Value.Location = new System.Drawing.Point(144, 70);
            this.lblVariable4Value.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.lblVariable4Value.Name = "lblVariable4Value";
            this.lblVariable4Value.Size = new System.Drawing.Size(57, 61);
            this.lblVariable4Value.TabIndex = 16;
            this.lblVariable4Value.Text = "0.0";
            this.lblVariable4Value.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblVariable4Value.Click += new System.EventHandler(this.lblVariable4Value_Click);
            // 
            // lblVariable4
            // 
            this.lblVariable4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblVariable4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVariable4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVariable4.Location = new System.Drawing.Point(6, 70);
            this.lblVariable4.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.lblVariable4.Name = "lblVariable4";
            this.lblVariable4.Size = new System.Drawing.Size(126, 61);
            this.lblVariable4.TabIndex = 15;
            this.lblVariable4.Text = "ACS Variable4";
            this.lblVariable4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVariable1Value
            // 
            this.lblVariable1Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblVariable1Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVariable1Value.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVariable1Value.Location = new System.Drawing.Point(144, 3);
            this.lblVariable1Value.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.lblVariable1Value.Name = "lblVariable1Value";
            this.lblVariable1Value.Size = new System.Drawing.Size(57, 61);
            this.lblVariable1Value.TabIndex = 3;
            this.lblVariable1Value.Text = "0.0";
            this.lblVariable1Value.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblVariable1Value.Click += new System.EventHandler(this.lblVariable1Value_Click);
            // 
            // lblVariable1
            // 
            this.lblVariable1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblVariable1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVariable1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVariable1.Location = new System.Drawing.Point(6, 3);
            this.lblVariable1.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.lblVariable1.Name = "lblVariable1";
            this.lblVariable1.Size = new System.Drawing.Size(126, 61);
            this.lblVariable1.TabIndex = 1;
            this.lblVariable1.Text = "ACS Variable1";
            this.lblVariable1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVariable2
            // 
            this.lblVariable2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblVariable2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVariable2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVariable2.Location = new System.Drawing.Point(247, 3);
            this.lblVariable2.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.lblVariable2.Name = "lblVariable2";
            this.lblVariable2.Size = new System.Drawing.Size(126, 61);
            this.lblVariable2.TabIndex = 6;
            this.lblVariable2.Text = "ACS Variable2";
            this.lblVariable2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVariable2Value
            // 
            this.lblVariable2Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblVariable2Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVariable2Value.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVariable2Value.Location = new System.Drawing.Point(385, 3);
            this.lblVariable2Value.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.lblVariable2Value.Name = "lblVariable2Value";
            this.lblVariable2Value.Size = new System.Drawing.Size(57, 61);
            this.lblVariable2Value.TabIndex = 8;
            this.lblVariable2Value.Text = "0.0";
            this.lblVariable2Value.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblVariable2Value.Click += new System.EventHandler(this.lblVariable2Value_Click);
            // 
            // lblVariable3
            // 
            this.lblVariable3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblVariable3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVariable3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVariable3.Location = new System.Drawing.Point(488, 3);
            this.lblVariable3.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.lblVariable3.Name = "lblVariable3";
            this.lblVariable3.Size = new System.Drawing.Size(126, 61);
            this.lblVariable3.TabIndex = 10;
            this.lblVariable3.Text = "ACS Variable3";
            this.lblVariable3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVariable3Value
            // 
            this.lblVariable3Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblVariable3Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVariable3Value.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVariable3Value.Location = new System.Drawing.Point(626, 3);
            this.lblVariable3Value.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.lblVariable3Value.Name = "lblVariable3Value";
            this.lblVariable3Value.Size = new System.Drawing.Size(62, 61);
            this.lblVariable3Value.TabIndex = 11;
            this.lblVariable3Value.Text = "0.0";
            this.lblVariable3Value.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblVariable3Value.Click += new System.EventHandler(this.lblVariable3Value_Click);
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.tlpACSVariableParameter.SetColumnSpan(this.btnApply, 2);
            this.btnApply.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.ForeColor = System.Drawing.Color.White;
            this.btnApply.Location = new System.Drawing.Point(485, 81);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(206, 50);
            this.btnApply.TabIndex = 12;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // lblVariable5
            // 
            this.lblVariable5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.lblVariable5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVariable5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVariable5.Location = new System.Drawing.Point(247, 70);
            this.lblVariable5.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.lblVariable5.Name = "lblVariable5";
            this.lblVariable5.Size = new System.Drawing.Size(126, 61);
            this.lblVariable5.TabIndex = 13;
            this.lblVariable5.Text = "ACS Variable5";
            this.lblVariable5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblVariable5.Click += new System.EventHandler(this.lblVariable5_Click);
            // 
            // lblVariable5Value
            // 
            this.lblVariable5Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblVariable5Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVariable5Value.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVariable5Value.Location = new System.Drawing.Point(385, 70);
            this.lblVariable5Value.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.lblVariable5Value.Name = "lblVariable5Value";
            this.lblVariable5Value.Size = new System.Drawing.Size(57, 61);
            this.lblVariable5Value.TabIndex = 14;
            this.lblVariable5Value.Text = "0.0";
            this.lblVariable5Value.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpAxisName
            // 
            this.grpAxisName.Controls.Add(this.tlpACSVariableParameter);
            this.grpAxisName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpAxisName.ForeColor = System.Drawing.Color.White;
            this.grpAxisName.Location = new System.Drawing.Point(0, 0);
            this.grpAxisName.Name = "grpAxisName";
            this.grpAxisName.Size = new System.Drawing.Size(700, 160);
            this.grpAxisName.TabIndex = 2;
            this.grpAxisName.TabStop = false;
            this.grpAxisName.Text = "Axis Name";
            // 
            // ACSParameterVariableControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.grpAxisName);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ACSParameterVariableControl";
            this.Size = new System.Drawing.Size(700, 160);
            this.Load += new System.EventHandler(this.MotionParameterVariableControl_Load);
            this.tlpACSVariableParameter.ResumeLayout(false);
            this.grpAxisName.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpACSVariableParameter;
        private System.Windows.Forms.Label lblVariable1Value;
        private System.Windows.Forms.Label lblVariable1;
        private System.Windows.Forms.Label lblVariable2;
        private System.Windows.Forms.Label lblVariable2Value;
        private System.Windows.Forms.Label lblVariable3;
        private System.Windows.Forms.Label lblVariable3Value;
        private System.Windows.Forms.GroupBox grpAxisName;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label lblVariable5Value;
        private System.Windows.Forms.Label lblVariable5;
        private System.Windows.Forms.Label lblVariable4Value;
        private System.Windows.Forms.Label lblVariable4;
    }
}
