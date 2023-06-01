namespace Jastech.Apps.Winform.UI.Controls
{
    partial class AutoFocusControl
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
            this.tlpAutoFocusControl = new System.Windows.Forms.TableLayoutPanel();
            this.lblAutoFocusOnOff = new System.Windows.Forms.Label();
            this.lblTargetPositionValue = new System.Windows.Forms.Label();
            this.lblTargetPosition = new System.Windows.Forms.Label();
            this.lblCurrentCogValue = new System.Windows.Forms.Label();
            this.lblCurrentCog = new System.Windows.Forms.Label();
            this.lblTeachCog = new System.Windows.Forms.Label();
            this.lblTeachCogValue = new System.Windows.Forms.Label();
            this.lblCuttentPositionValue = new System.Windows.Forms.Label();
            this.lblCurrentPosition = new System.Windows.Forms.Label();
            this.lblSetCurrentToTarget = new System.Windows.Forms.Label();
            this.lblCurrentToTeach = new System.Windows.Forms.Label();
            this.lblAFOff = new System.Windows.Forms.Label();
            this.lblAFOn = new System.Windows.Forms.Label();
            this.tlpAutoFocusControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAutoFocusControl
            // 
            this.tlpAutoFocusControl.ColumnCount = 3;
            this.tlpAutoFocusControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpAutoFocusControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tlpAutoFocusControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tlpAutoFocusControl.Controls.Add(this.lblAutoFocusOnOff, 0, 4);
            this.tlpAutoFocusControl.Controls.Add(this.lblTargetPositionValue, 1, 0);
            this.tlpAutoFocusControl.Controls.Add(this.lblTargetPosition, 0, 0);
            this.tlpAutoFocusControl.Controls.Add(this.lblCurrentCogValue, 1, 3);
            this.tlpAutoFocusControl.Controls.Add(this.lblCurrentCog, 0, 3);
            this.tlpAutoFocusControl.Controls.Add(this.lblTeachCog, 0, 2);
            this.tlpAutoFocusControl.Controls.Add(this.lblTeachCogValue, 1, 2);
            this.tlpAutoFocusControl.Controls.Add(this.lblCuttentPositionValue, 1, 1);
            this.tlpAutoFocusControl.Controls.Add(this.lblCurrentPosition, 0, 1);
            this.tlpAutoFocusControl.Controls.Add(this.lblSetCurrentToTarget, 2, 1);
            this.tlpAutoFocusControl.Controls.Add(this.lblCurrentToTeach, 2, 3);
            this.tlpAutoFocusControl.Controls.Add(this.lblAFOff, 2, 4);
            this.tlpAutoFocusControl.Controls.Add(this.lblAFOn, 1, 4);
            this.tlpAutoFocusControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAutoFocusControl.Location = new System.Drawing.Point(0, 0);
            this.tlpAutoFocusControl.Margin = new System.Windows.Forms.Padding(0);
            this.tlpAutoFocusControl.Name = "tlpAutoFocusControl";
            this.tlpAutoFocusControl.RowCount = 5;
            this.tlpAutoFocusControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpAutoFocusControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpAutoFocusControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpAutoFocusControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpAutoFocusControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpAutoFocusControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpAutoFocusControl.Size = new System.Drawing.Size(400, 200);
            this.tlpAutoFocusControl.TabIndex = 3;
            // 
            // lblAutoFocusOnOff
            // 
            this.lblAutoFocusOnOff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblAutoFocusOnOff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAutoFocusOnOff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAutoFocusOnOff.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblAutoFocusOnOff.Location = new System.Drawing.Point(0, 160);
            this.lblAutoFocusOnOff.Margin = new System.Windows.Forms.Padding(0);
            this.lblAutoFocusOnOff.Name = "lblAutoFocusOnOff";
            this.lblAutoFocusOnOff.Size = new System.Drawing.Size(133, 40);
            this.lblAutoFocusOnOff.TabIndex = 205;
            this.lblAutoFocusOnOff.Text = "AUTO FOCUS\r\nON/OFF";
            this.lblAutoFocusOnOff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTargetPositionValue
            // 
            this.lblTargetPositionValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblTargetPositionValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTargetPositionValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTargetPositionValue.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblTargetPositionValue.Location = new System.Drawing.Point(133, 0);
            this.lblTargetPositionValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblTargetPositionValue.Name = "lblTargetPositionValue";
            this.lblTargetPositionValue.Size = new System.Drawing.Size(133, 40);
            this.lblTargetPositionValue.TabIndex = 1;
            this.lblTargetPositionValue.Text = "0.0";
            this.lblTargetPositionValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTargetPositionValue.Click += new System.EventHandler(this.lblTargetPositionZValue_Click);
            // 
            // lblTargetPosition
            // 
            this.lblTargetPosition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblTargetPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTargetPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTargetPosition.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblTargetPosition.Location = new System.Drawing.Point(0, 0);
            this.lblTargetPosition.Margin = new System.Windows.Forms.Padding(0);
            this.lblTargetPosition.Name = "lblTargetPosition";
            this.lblTargetPosition.Size = new System.Drawing.Size(133, 40);
            this.lblTargetPosition.TabIndex = 1;
            this.lblTargetPosition.Text = "TARGET\r\nPOSITION";
            this.lblTargetPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentCogValue
            // 
            this.lblCurrentCogValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblCurrentCogValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrentCogValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentCogValue.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblCurrentCogValue.Location = new System.Drawing.Point(133, 120);
            this.lblCurrentCogValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblCurrentCogValue.Name = "lblCurrentCogValue";
            this.lblCurrentCogValue.Size = new System.Drawing.Size(133, 40);
            this.lblCurrentCogValue.TabIndex = 1;
            this.lblCurrentCogValue.Text = "0";
            this.lblCurrentCogValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentCog
            // 
            this.lblCurrentCog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblCurrentCog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrentCog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentCog.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblCurrentCog.Location = new System.Drawing.Point(0, 120);
            this.lblCurrentCog.Margin = new System.Windows.Forms.Padding(0);
            this.lblCurrentCog.Name = "lblCurrentCog";
            this.lblCurrentCog.Size = new System.Drawing.Size(133, 40);
            this.lblCurrentCog.TabIndex = 203;
            this.lblCurrentCog.Text = "CURRENT COG";
            this.lblCurrentCog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTeachCog
            // 
            this.lblTeachCog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblTeachCog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTeachCog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTeachCog.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblTeachCog.Location = new System.Drawing.Point(0, 80);
            this.lblTeachCog.Margin = new System.Windows.Forms.Padding(0);
            this.lblTeachCog.Name = "lblTeachCog";
            this.lblTeachCog.Size = new System.Drawing.Size(133, 40);
            this.lblTeachCog.TabIndex = 203;
            this.lblTeachCog.Text = "COG";
            this.lblTeachCog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTeachCogValue
            // 
            this.lblTeachCogValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblTeachCogValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTeachCogValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTeachCogValue.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblTeachCogValue.Location = new System.Drawing.Point(133, 80);
            this.lblTeachCogValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblTeachCogValue.Name = "lblTeachCogValue";
            this.lblTeachCogValue.Size = new System.Drawing.Size(133, 40);
            this.lblTeachCogValue.TabIndex = 202;
            this.lblTeachCogValue.Text = "0";
            this.lblTeachCogValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTeachCogValue.Click += new System.EventHandler(this.lblTeachCogValue_Click);
            // 
            // lblCuttentPositionValue
            // 
            this.lblCuttentPositionValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblCuttentPositionValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCuttentPositionValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCuttentPositionValue.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblCuttentPositionValue.Location = new System.Drawing.Point(133, 40);
            this.lblCuttentPositionValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblCuttentPositionValue.Name = "lblCuttentPositionValue";
            this.lblCuttentPositionValue.Size = new System.Drawing.Size(133, 40);
            this.lblCuttentPositionValue.TabIndex = 1;
            this.lblCuttentPositionValue.Text = "0.0";
            this.lblCuttentPositionValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentPosition
            // 
            this.lblCurrentPosition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblCurrentPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrentPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentPosition.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblCurrentPosition.Location = new System.Drawing.Point(0, 40);
            this.lblCurrentPosition.Margin = new System.Windows.Forms.Padding(0);
            this.lblCurrentPosition.Name = "lblCurrentPosition";
            this.lblCurrentPosition.Size = new System.Drawing.Size(133, 40);
            this.lblCurrentPosition.TabIndex = 1;
            this.lblCurrentPosition.Text = "CURRENT\r\nPOSITION";
            this.lblCurrentPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSetCurrentToTarget
            // 
            this.lblSetCurrentToTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblSetCurrentToTarget.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSetCurrentToTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSetCurrentToTarget.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblSetCurrentToTarget.Location = new System.Drawing.Point(266, 40);
            this.lblSetCurrentToTarget.Margin = new System.Windows.Forms.Padding(0);
            this.lblSetCurrentToTarget.Name = "lblSetCurrentToTarget";
            this.lblSetCurrentToTarget.Size = new System.Drawing.Size(134, 40);
            this.lblSetCurrentToTarget.TabIndex = 202;
            this.lblSetCurrentToTarget.Text = "SET";
            this.lblSetCurrentToTarget.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSetCurrentToTarget.Click += new System.EventHandler(this.lblSetCurrentToTarget_Click);
            // 
            // lblCurrentToTeach
            // 
            this.lblCurrentToTeach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblCurrentToTeach.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrentToTeach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentToTeach.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblCurrentToTeach.Location = new System.Drawing.Point(266, 120);
            this.lblCurrentToTeach.Margin = new System.Windows.Forms.Padding(0);
            this.lblCurrentToTeach.Name = "lblCurrentToTeach";
            this.lblCurrentToTeach.Size = new System.Drawing.Size(134, 40);
            this.lblCurrentToTeach.TabIndex = 202;
            this.lblCurrentToTeach.Text = "SET";
            this.lblCurrentToTeach.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCurrentToTeach.Click += new System.EventHandler(this.lblCurrentToTeach_Click);
            // 
            // lblAFOff
            // 
            this.lblAFOff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblAFOff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAFOff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAFOff.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblAFOff.Location = new System.Drawing.Point(266, 160);
            this.lblAFOff.Margin = new System.Windows.Forms.Padding(0);
            this.lblAFOff.Name = "lblAFOff";
            this.lblAFOff.Size = new System.Drawing.Size(134, 40);
            this.lblAFOff.TabIndex = 202;
            this.lblAFOff.Text = "A/F OFF";
            this.lblAFOff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAFOff.Click += new System.EventHandler(this.lblAFOff_Click);
            // 
            // lblAFOn
            // 
            this.lblAFOn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblAFOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAFOn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAFOn.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblAFOn.Location = new System.Drawing.Point(133, 160);
            this.lblAFOn.Margin = new System.Windows.Forms.Padding(0);
            this.lblAFOn.Name = "lblAFOn";
            this.lblAFOn.Size = new System.Drawing.Size(133, 40);
            this.lblAFOn.TabIndex = 202;
            this.lblAFOn.Text = "A/F ON";
            this.lblAFOn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAFOn.Click += new System.EventHandler(this.lblAFOn_Click);
            // 
            // AutoFocusControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpAutoFocusControl);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "AutoFocusControl";
            this.Size = new System.Drawing.Size(400, 200);
            this.Load += new System.EventHandler(this.AutoFocusControl_Load);
            this.tlpAutoFocusControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tlpAutoFocusControl;
        private System.Windows.Forms.Label lblAutoFocusOnOff;
        public System.Windows.Forms.Label lblTargetPositionValue;
        private System.Windows.Forms.Label lblTargetPosition;
        private System.Windows.Forms.Label lblCurrentCogValue;
        private System.Windows.Forms.Label lblCurrentCog;
        private System.Windows.Forms.Label lblTeachCog;
        private System.Windows.Forms.Label lblTeachCogValue;
        private System.Windows.Forms.Label lblCuttentPositionValue;
        private System.Windows.Forms.Label lblCurrentPosition;
        private System.Windows.Forms.Label lblSetCurrentToTarget;
        private System.Windows.Forms.Label lblCurrentToTeach;
        private System.Windows.Forms.Label lblAFOff;
        private System.Windows.Forms.Label lblAFOn;
    }
}
