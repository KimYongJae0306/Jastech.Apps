﻿namespace ATT_UT_IPAD.UI.Controls
{
    partial class TeachingPositionListControl
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
            this.tlpTeachingUnitControl = new System.Windows.Forms.TableLayoutPanel();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.tlpTeachingPosition = new System.Windows.Forms.TableLayoutPanel();
            this.btnScanStart = new System.Windows.Forms.Button();
            this.btnScanEnd = new System.Windows.Forms.Button();
            this.btnStandby = new System.Windows.Forms.Button();
            this.tlpTeachingUnitControl.SuspendLayout();
            this.tlpTeachingPosition.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpTeachingUnitControl
            // 
            this.tlpTeachingUnitControl.ColumnCount = 3;
            this.tlpTeachingUnitControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpTeachingUnitControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTeachingUnitControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpTeachingUnitControl.Controls.Add(this.btnNext, 2, 0);
            this.tlpTeachingUnitControl.Controls.Add(this.btnPrev, 0, 0);
            this.tlpTeachingUnitControl.Controls.Add(this.tlpTeachingPosition, 1, 0);
            this.tlpTeachingUnitControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTeachingUnitControl.Location = new System.Drawing.Point(0, 0);
            this.tlpTeachingUnitControl.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTeachingUnitControl.Name = "tlpTeachingUnitControl";
            this.tlpTeachingUnitControl.RowCount = 1;
            this.tlpTeachingUnitControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTeachingUnitControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpTeachingUnitControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpTeachingUnitControl.Size = new System.Drawing.Size(606, 87);
            this.tlpTeachingUnitControl.TabIndex = 2;
            // 
            // btnNext
            // 
            this.btnNext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNext.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Image = global::ATT_UT_IPAD.Properties.Resources.Next_White;
            this.btnNext.Location = new System.Drawing.Point(566, 0);
            this.btnNext.Margin = new System.Windows.Forms.Padding(0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(40, 87);
            this.btnNext.TabIndex = 14;
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrev.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnPrev.ForeColor = System.Drawing.Color.White;
            this.btnPrev.Image = global::ATT_UT_IPAD.Properties.Resources.Prev_White;
            this.btnPrev.Location = new System.Drawing.Point(0, 0);
            this.btnPrev.Margin = new System.Windows.Forms.Padding(0);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(40, 87);
            this.btnPrev.TabIndex = 13;
            this.btnPrev.UseVisualStyleBackColor = false;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // tlpTeachingPosition
            // 
            this.tlpTeachingPosition.ColumnCount = 7;
            this.tlpTeachingPosition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpTeachingPosition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTeachingPosition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpTeachingPosition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpTeachingPosition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTeachingPosition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpTeachingPosition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpTeachingPosition.Controls.Add(this.btnStandby, 0, 0);
            this.tlpTeachingPosition.Controls.Add(this.btnScanEnd, 6, 0);
            this.tlpTeachingPosition.Controls.Add(this.btnScanStart, 5, 0);
            this.tlpTeachingPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTeachingPosition.Location = new System.Drawing.Point(40, 0);
            this.tlpTeachingPosition.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTeachingPosition.Name = "tlpTeachingPosition";
            this.tlpTeachingPosition.RowCount = 1;
            this.tlpTeachingPosition.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTeachingPosition.Size = new System.Drawing.Size(526, 87);
            this.tlpTeachingPosition.TabIndex = 1;
            // 
            // btnScanStart
            // 
            this.btnScanStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnScanStart.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnScanStart.ForeColor = System.Drawing.Color.White;
            this.btnScanStart.Location = new System.Drawing.Point(329, 3);
            this.btnScanStart.Name = "btnScanStart";
            this.btnScanStart.Size = new System.Drawing.Size(94, 81);
            this.btnScanStart.TabIndex = 12;
            this.btnScanStart.Text = "Scan\r\nStart";
            this.btnScanStart.UseVisualStyleBackColor = false;
            this.btnScanStart.Click += new System.EventHandler(this.btnScanStart_Click);
            // 
            // btnScanEnd
            // 
            this.btnScanEnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnScanEnd.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnScanEnd.ForeColor = System.Drawing.Color.White;
            this.btnScanEnd.Location = new System.Drawing.Point(429, 3);
            this.btnScanEnd.Name = "btnScanEnd";
            this.btnScanEnd.Size = new System.Drawing.Size(94, 81);
            this.btnScanEnd.TabIndex = 12;
            this.btnScanEnd.Text = "Scan\r\nEnd";
            this.btnScanEnd.UseVisualStyleBackColor = false;
            this.btnScanEnd.Click += new System.EventHandler(this.btnScanEnd_Click);
            // 
            // btnStandby
            // 
            this.btnStandby.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStandby.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnStandby.ForeColor = System.Drawing.Color.White;
            this.btnStandby.Location = new System.Drawing.Point(3, 3);
            this.btnStandby.Name = "btnStandby";
            this.btnStandby.Size = new System.Drawing.Size(94, 81);
            this.btnStandby.TabIndex = 12;
            this.btnStandby.Text = "Standby";
            this.btnStandby.UseVisualStyleBackColor = false;
            this.btnStandby.Click += new System.EventHandler(this.btnStandby_Click);
            // 
            // TeachingPositionListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.tlpTeachingUnitControl);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.Name = "TeachingPositionListControl";
            this.Size = new System.Drawing.Size(606, 87);
            this.Load += new System.EventHandler(this.TeachingPositionListControl_Load);
            this.tlpTeachingUnitControl.ResumeLayout(false);
            this.tlpTeachingPosition.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpTeachingUnitControl;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.TableLayoutPanel tlpTeachingPosition;
        private System.Windows.Forms.Button btnStandby;
        private System.Windows.Forms.Button btnScanEnd;
        private System.Windows.Forms.Button btnScanStart;
    }
}
