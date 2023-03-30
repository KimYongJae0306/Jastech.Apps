namespace ATT.UI.Controls
{
    partial class MotionSettingsControl
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
            this.tlpMotionSettings = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpMotionFunction = new System.Windows.Forms.TableLayoutPanel();
            this.tlpStatus = new System.Windows.Forms.TableLayoutPanel();
            this.pnlJog = new System.Windows.Forms.Panel();
            this.tlpMotionSettings.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlpMotionFunction.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMotionSettings
            // 
            this.tlpMotionSettings.ColumnCount = 2;
            this.tlpMotionSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpMotionSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlpMotionSettings.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlpMotionSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMotionSettings.Location = new System.Drawing.Point(0, 0);
            this.tlpMotionSettings.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMotionSettings.Name = "tlpMotionSettings";
            this.tlpMotionSettings.RowCount = 1;
            this.tlpMotionSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMotionSettings.Size = new System.Drawing.Size(1100, 600);
            this.tlpMotionSettings.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tlpMotionFunction, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(440, 600);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tlpMotionFunction
            // 
            this.tlpMotionFunction.ColumnCount = 1;
            this.tlpMotionFunction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMotionFunction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMotionFunction.Controls.Add(this.tlpStatus, 0, 0);
            this.tlpMotionFunction.Controls.Add(this.pnlJog, 0, 1);
            this.tlpMotionFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMotionFunction.Location = new System.Drawing.Point(0, 120);
            this.tlpMotionFunction.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMotionFunction.Name = "tlpMotionFunction";
            this.tlpMotionFunction.RowCount = 2;
            this.tlpMotionFunction.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMotionFunction.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMotionFunction.Size = new System.Drawing.Size(440, 360);
            this.tlpMotionFunction.TabIndex = 0;
            // 
            // tlpStatus
            // 
            this.tlpStatus.ColumnCount = 4;
            this.tlpStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpStatus.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
            this.tlpStatus.Location = new System.Drawing.Point(0, 0);
            this.tlpStatus.Margin = new System.Windows.Forms.Padding(0);
            this.tlpStatus.Name = "tlpStatus";
            this.tlpStatus.RowCount = 1;
            this.tlpStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStatus.Size = new System.Drawing.Size(440, 180);
            this.tlpStatus.TabIndex = 0;
            // 
            // pnlJog
            // 
            this.pnlJog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlJog.Location = new System.Drawing.Point(3, 180);
            this.pnlJog.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlJog.Name = "pnlJog";
            this.pnlJog.Size = new System.Drawing.Size(434, 180);
            this.pnlJog.TabIndex = 1;
            // 
            // MotionSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tlpMotionSettings);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold);
            this.Name = "MotionSettingsControl";
            this.Size = new System.Drawing.Size(1100, 600);
            this.Load += new System.EventHandler(this.MotionSettingsControl_Load);
            this.tlpMotionSettings.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tlpMotionFunction.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMotionSettings;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tlpMotionFunction;
        private System.Windows.Forms.TableLayoutPanel tlpStatus;
        private System.Windows.Forms.Panel pnlJog;
    }
}
