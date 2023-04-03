namespace Jastech.Apps.Winform.UI.Controls
{
    partial class UnitSelectControl
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
            this.tlpUnitSelectControl = new System.Windows.Forms.TableLayoutPanel();
            this.tlpUnitSelect = new System.Windows.Forms.TableLayoutPanel();
            this.lblUnitName = new System.Windows.Forms.Label();
            this.tlpUnitSelectControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpUnitSelectControl
            // 
            this.tlpUnitSelectControl.ColumnCount = 1;
            this.tlpUnitSelectControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpUnitSelectControl.Controls.Add(this.lblUnitName, 0, 0);
            this.tlpUnitSelectControl.Controls.Add(this.tlpUnitSelect, 0, 1);
            this.tlpUnitSelectControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpUnitSelectControl.Location = new System.Drawing.Point(0, 0);
            this.tlpUnitSelectControl.Margin = new System.Windows.Forms.Padding(0);
            this.tlpUnitSelectControl.Name = "tlpUnitSelectControl";
            this.tlpUnitSelectControl.RowCount = 2;
            this.tlpUnitSelectControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpUnitSelectControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpUnitSelectControl.Size = new System.Drawing.Size(193, 250);
            this.tlpUnitSelectControl.TabIndex = 0;
            // 
            // tlpUnitSelect
            // 
            this.tlpUnitSelect.ColumnCount = 1;
            this.tlpUnitSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpUnitSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpUnitSelect.Location = new System.Drawing.Point(0, 40);
            this.tlpUnitSelect.Margin = new System.Windows.Forms.Padding(0);
            this.tlpUnitSelect.Name = "tlpUnitSelect";
            this.tlpUnitSelect.RowCount = 1;
            this.tlpUnitSelect.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpUnitSelect.Size = new System.Drawing.Size(193, 210);
            this.tlpUnitSelect.TabIndex = 0;
            // 
            // lblUnitName
            // 
            this.lblUnitName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(174)))), ((int)(((byte)(224)))));
            this.lblUnitName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUnitName.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblUnitName.Location = new System.Drawing.Point(3, 0);
            this.lblUnitName.Name = "lblUnitName";
            this.lblUnitName.Size = new System.Drawing.Size(187, 40);
            this.lblUnitName.TabIndex = 5;
            this.lblUnitName.Text = "Unit Name";
            this.lblUnitName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UnitSelectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tlpUnitSelectControl);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.Name = "UnitSelectControl";
            this.Size = new System.Drawing.Size(193, 250);
            this.Load += new System.EventHandler(this.UnitSelectControl_Load);
            this.tlpUnitSelectControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpUnitSelectControl;
        private System.Windows.Forms.TableLayoutPanel tlpUnitSelect;
        private System.Windows.Forms.Label lblUnitName;
    }
}
