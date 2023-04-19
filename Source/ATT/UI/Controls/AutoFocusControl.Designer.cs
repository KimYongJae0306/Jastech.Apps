namespace ATT.UI.Controls
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
            this.btnCurrentToTeach = new System.Windows.Forms.Button();
            this.lblCurrentCogValue = new System.Windows.Forms.Label();
            this.lblCurrentCog = new System.Windows.Forms.Label();
            this.lblTeachCog = new System.Windows.Forms.Label();
            this.lblTeachCogValue = new System.Windows.Forms.Label();
            this.btnSetCurrentToTarget = new System.Windows.Forms.Button();
            this.lblCuttentPositionValue = new System.Windows.Forms.Label();
            this.lblCurrentPosition = new System.Windows.Forms.Label();
            this.btnAFOff = new System.Windows.Forms.Button();
            this.bnAFOn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpAutoFocusControl.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAutoFocusControl
            // 
            this.tlpAutoFocusControl.ColumnCount = 3;
            this.tlpAutoFocusControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.tlpAutoFocusControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tlpAutoFocusControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tlpAutoFocusControl.Controls.Add(this.lblAutoFocusOnOff, 0, 4);
            this.tlpAutoFocusControl.Controls.Add(this.lblTargetPositionValue, 1, 0);
            this.tlpAutoFocusControl.Controls.Add(this.lblTargetPosition, 0, 0);
            this.tlpAutoFocusControl.Controls.Add(this.btnCurrentToTeach, 2, 3);
            this.tlpAutoFocusControl.Controls.Add(this.lblCurrentCogValue, 1, 3);
            this.tlpAutoFocusControl.Controls.Add(this.lblCurrentCog, 0, 3);
            this.tlpAutoFocusControl.Controls.Add(this.lblTeachCog, 0, 2);
            this.tlpAutoFocusControl.Controls.Add(this.lblTeachCogValue, 1, 2);
            this.tlpAutoFocusControl.Controls.Add(this.btnSetCurrentToTarget, 2, 1);
            this.tlpAutoFocusControl.Controls.Add(this.lblCuttentPositionValue, 1, 1);
            this.tlpAutoFocusControl.Controls.Add(this.lblCurrentPosition, 0, 1);
            this.tlpAutoFocusControl.Controls.Add(this.btnAFOff, 2, 4);
            this.tlpAutoFocusControl.Controls.Add(this.bnAFOn, 1, 4);
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
            this.tlpAutoFocusControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpAutoFocusControl.Size = new System.Drawing.Size(448, 342);
            this.tlpAutoFocusControl.TabIndex = 3;
            // 
            // lblAutoFocusOnOff
            // 
            this.lblAutoFocusOnOff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblAutoFocusOnOff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAutoFocusOnOff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAutoFocusOnOff.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblAutoFocusOnOff.Location = new System.Drawing.Point(0, 272);
            this.lblAutoFocusOnOff.Margin = new System.Windows.Forms.Padding(0);
            this.lblAutoFocusOnOff.Name = "lblAutoFocusOnOff";
            this.lblAutoFocusOnOff.Size = new System.Drawing.Size(149, 70);
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
            this.lblTargetPositionValue.Location = new System.Drawing.Point(149, 0);
            this.lblTargetPositionValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblTargetPositionValue.Name = "lblTargetPositionValue";
            this.lblTargetPositionValue.Size = new System.Drawing.Size(149, 68);
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
            this.lblTargetPosition.Size = new System.Drawing.Size(149, 68);
            this.lblTargetPosition.TabIndex = 1;
            this.lblTargetPosition.Text = "TARGET\r\nPOSITION";
            this.lblTargetPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCurrentToTeach
            // 
            this.btnCurrentToTeach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.btnCurrentToTeach.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCurrentToTeach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCurrentToTeach.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.btnCurrentToTeach.Location = new System.Drawing.Point(298, 204);
            this.btnCurrentToTeach.Margin = new System.Windows.Forms.Padding(0);
            this.btnCurrentToTeach.Name = "btnCurrentToTeach";
            this.btnCurrentToTeach.Size = new System.Drawing.Size(150, 68);
            this.btnCurrentToTeach.TabIndex = 200;
            this.btnCurrentToTeach.Text = "SET";
            this.btnCurrentToTeach.UseVisualStyleBackColor = false;
            this.btnCurrentToTeach.Click += new System.EventHandler(this.btnCurrentToTeach_Click);
            // 
            // lblCurrentCogValue
            // 
            this.lblCurrentCogValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblCurrentCogValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrentCogValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentCogValue.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblCurrentCogValue.Location = new System.Drawing.Point(149, 204);
            this.lblCurrentCogValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblCurrentCogValue.Name = "lblCurrentCogValue";
            this.lblCurrentCogValue.Size = new System.Drawing.Size(149, 68);
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
            this.lblCurrentCog.Location = new System.Drawing.Point(0, 204);
            this.lblCurrentCog.Margin = new System.Windows.Forms.Padding(0);
            this.lblCurrentCog.Name = "lblCurrentCog";
            this.lblCurrentCog.Size = new System.Drawing.Size(149, 68);
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
            this.lblTeachCog.Location = new System.Drawing.Point(0, 136);
            this.lblTeachCog.Margin = new System.Windows.Forms.Padding(0);
            this.lblTeachCog.Name = "lblTeachCog";
            this.lblTeachCog.Size = new System.Drawing.Size(149, 68);
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
            this.lblTeachCogValue.Location = new System.Drawing.Point(149, 136);
            this.lblTeachCogValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblTeachCogValue.Name = "lblTeachCogValue";
            this.lblTeachCogValue.Size = new System.Drawing.Size(149, 68);
            this.lblTeachCogValue.TabIndex = 202;
            this.lblTeachCogValue.Text = "0";
            this.lblTeachCogValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTeachCogValue.Click += new System.EventHandler(this.lblTeachCogValue_Click);
            // 
            // btnSetCurrentToTarget
            // 
            this.btnSetCurrentToTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.btnSetCurrentToTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSetCurrentToTarget.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.btnSetCurrentToTarget.Location = new System.Drawing.Point(298, 68);
            this.btnSetCurrentToTarget.Margin = new System.Windows.Forms.Padding(0);
            this.btnSetCurrentToTarget.Name = "btnSetCurrentToTarget";
            this.btnSetCurrentToTarget.Size = new System.Drawing.Size(150, 68);
            this.btnSetCurrentToTarget.TabIndex = 4;
            this.btnSetCurrentToTarget.Text = "SET";
            this.btnSetCurrentToTarget.UseVisualStyleBackColor = false;
            this.btnSetCurrentToTarget.Click += new System.EventHandler(this.btnSetCurrentToTarget_Click);
            // 
            // lblCuttentPositionValue
            // 
            this.lblCuttentPositionValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.lblCuttentPositionValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCuttentPositionValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCuttentPositionValue.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lblCuttentPositionValue.Location = new System.Drawing.Point(149, 68);
            this.lblCuttentPositionValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblCuttentPositionValue.Name = "lblCuttentPositionValue";
            this.lblCuttentPositionValue.Size = new System.Drawing.Size(149, 68);
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
            this.lblCurrentPosition.Location = new System.Drawing.Point(0, 68);
            this.lblCurrentPosition.Margin = new System.Windows.Forms.Padding(0);
            this.lblCurrentPosition.Name = "lblCurrentPosition";
            this.lblCurrentPosition.Size = new System.Drawing.Size(149, 68);
            this.lblCurrentPosition.TabIndex = 1;
            this.lblCurrentPosition.Text = "CURRENT\r\nPOSITION";
            this.lblCurrentPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAFOff
            // 
            this.btnAFOff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.btnAFOff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAFOff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAFOff.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.btnAFOff.Location = new System.Drawing.Point(298, 272);
            this.btnAFOff.Margin = new System.Windows.Forms.Padding(0);
            this.btnAFOff.Name = "btnAFOff";
            this.btnAFOff.Size = new System.Drawing.Size(150, 70);
            this.btnAFOff.TabIndex = 204;
            this.btnAFOff.Text = "A/F OFF";
            this.btnAFOff.UseVisualStyleBackColor = false;
            this.btnAFOff.Click += new System.EventHandler(this.btnAFOff_Click);
            // 
            // bnAFOn
            // 
            this.bnAFOn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.bnAFOn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bnAFOn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bnAFOn.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.bnAFOn.Location = new System.Drawing.Point(149, 272);
            this.bnAFOn.Margin = new System.Windows.Forms.Padding(0);
            this.bnAFOn.Name = "bnAFOn";
            this.bnAFOn.Size = new System.Drawing.Size(149, 70);
            this.bnAFOn.TabIndex = 204;
            this.bnAFOn.Text = "A/F ON";
            this.bnAFOn.UseVisualStyleBackColor = false;
            this.bnAFOn.Click += new System.EventHandler(this.bnAFOn_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel1.Controls.Add(this.tlpAutoFocusControl, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(448, 342);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // AutoFocusControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "AutoFocusControl";
            this.Size = new System.Drawing.Size(448, 342);
            this.tlpAutoFocusControl.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tlpAutoFocusControl;
        private System.Windows.Forms.Label lblAutoFocusOnOff;
        public System.Windows.Forms.Label lblTargetPositionValue;
        private System.Windows.Forms.Label lblTargetPosition;
        private System.Windows.Forms.Button btnCurrentToTeach;
        private System.Windows.Forms.Label lblCurrentCogValue;
        private System.Windows.Forms.Label lblCurrentCog;
        private System.Windows.Forms.Label lblTeachCog;
        private System.Windows.Forms.Label lblTeachCogValue;
        private System.Windows.Forms.Button btnSetCurrentToTarget;
        private System.Windows.Forms.Label lblCuttentPositionValue;
        private System.Windows.Forms.Label lblCurrentPosition;
        private System.Windows.Forms.Button btnAFOff;
        private System.Windows.Forms.Button bnAFOn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
