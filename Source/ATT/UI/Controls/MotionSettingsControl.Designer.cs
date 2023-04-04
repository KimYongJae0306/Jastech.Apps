﻿namespace ATT.UI.Controls
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
            this.pnlTeachingPositionList = new System.Windows.Forms.Panel();
            this.tlpMotionParameter = new System.Windows.Forms.TableLayoutPanel();
            this.tlpVariableParameters = new System.Windows.Forms.TableLayoutPanel();
            this.lblVariableParameter = new System.Windows.Forms.Label();
            this.tlpCommonParameters = new System.Windows.Forms.TableLayoutPanel();
            this.lblCommonParameter = new System.Windows.Forms.Label();
            this.tlpCommonParameter = new System.Windows.Forms.TableLayoutPanel();
            this.tlpVariableParameter = new System.Windows.Forms.TableLayoutPanel();
            this.tlpMotionSettings.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlpMotionFunction.SuspendLayout();
            this.tlpMotionParameter.SuspendLayout();
            this.tlpVariableParameters.SuspendLayout();
            this.tlpCommonParameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMotionSettings
            // 
            this.tlpMotionSettings.ColumnCount = 2;
            this.tlpMotionSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tlpMotionSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tlpMotionSettings.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlpMotionSettings.Controls.Add(this.tlpMotionParameter, 1, 0);
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
            this.tableLayoutPanel1.Controls.Add(this.pnlTeachingPositionList, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(495, 600);
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
            this.tlpMotionFunction.Location = new System.Drawing.Point(0, 100);
            this.tlpMotionFunction.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMotionFunction.Name = "tlpMotionFunction";
            this.tlpMotionFunction.RowCount = 2;
            this.tlpMotionFunction.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMotionFunction.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMotionFunction.Size = new System.Drawing.Size(495, 400);
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
            this.tlpStatus.Size = new System.Drawing.Size(495, 200);
            this.tlpStatus.TabIndex = 0;
            // 
            // pnlJog
            // 
            this.pnlJog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlJog.Location = new System.Drawing.Point(0, 200);
            this.pnlJog.Margin = new System.Windows.Forms.Padding(0);
            this.pnlJog.Name = "pnlJog";
            this.pnlJog.Size = new System.Drawing.Size(495, 200);
            this.pnlJog.TabIndex = 1;
            // 
            // pnlTeachingPositionList
            // 
            this.pnlTeachingPositionList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTeachingPositionList.Location = new System.Drawing.Point(0, 0);
            this.pnlTeachingPositionList.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTeachingPositionList.Name = "pnlTeachingPositionList";
            this.pnlTeachingPositionList.Size = new System.Drawing.Size(495, 100);
            this.pnlTeachingPositionList.TabIndex = 1;
            // 
            // tlpMotionParameter
            // 
            this.tlpMotionParameter.ColumnCount = 1;
            this.tlpMotionParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMotionParameter.Controls.Add(this.tlpVariableParameters, 0, 1);
            this.tlpMotionParameter.Controls.Add(this.tlpCommonParameters, 0, 0);
            this.tlpMotionParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMotionParameter.Location = new System.Drawing.Point(495, 0);
            this.tlpMotionParameter.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMotionParameter.Name = "tlpMotionParameter";
            this.tlpMotionParameter.RowCount = 2;
            this.tlpMotionParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMotionParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMotionParameter.Size = new System.Drawing.Size(605, 600);
            this.tlpMotionParameter.TabIndex = 1;
            // 
            // tlpVariableParameters
            // 
            this.tlpVariableParameters.ColumnCount = 1;
            this.tlpVariableParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpVariableParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVariableParameters.Controls.Add(this.lblVariableParameter, 0, 0);
            this.tlpVariableParameters.Controls.Add(this.tlpVariableParameter, 0, 1);
            this.tlpVariableParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpVariableParameters.Location = new System.Drawing.Point(0, 300);
            this.tlpVariableParameters.Margin = new System.Windows.Forms.Padding(0);
            this.tlpVariableParameters.Name = "tlpVariableParameters";
            this.tlpVariableParameters.RowCount = 2;
            this.tlpVariableParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpVariableParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpVariableParameters.Size = new System.Drawing.Size(605, 300);
            this.tlpVariableParameters.TabIndex = 1;
            // 
            // lblVariableParameter
            // 
            this.lblVariableParameter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(174)))), ((int)(((byte)(224)))));
            this.lblVariableParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVariableParameter.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblVariableParameter.Location = new System.Drawing.Point(3, 0);
            this.lblVariableParameter.Name = "lblVariableParameter";
            this.lblVariableParameter.Size = new System.Drawing.Size(599, 40);
            this.lblVariableParameter.TabIndex = 6;
            this.lblVariableParameter.Text = "Variable";
            this.lblVariableParameter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpCommonParameters
            // 
            this.tlpCommonParameters.ColumnCount = 1;
            this.tlpCommonParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCommonParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpCommonParameters.Controls.Add(this.lblCommonParameter, 0, 0);
            this.tlpCommonParameters.Controls.Add(this.tlpCommonParameter, 0, 1);
            this.tlpCommonParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCommonParameters.Location = new System.Drawing.Point(0, 0);
            this.tlpCommonParameters.Margin = new System.Windows.Forms.Padding(0);
            this.tlpCommonParameters.Name = "tlpCommonParameters";
            this.tlpCommonParameters.RowCount = 2;
            this.tlpCommonParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpCommonParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCommonParameters.Size = new System.Drawing.Size(605, 300);
            this.tlpCommonParameters.TabIndex = 0;
            // 
            // lblCommonParameter
            // 
            this.lblCommonParameter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(174)))), ((int)(((byte)(224)))));
            this.lblCommonParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCommonParameter.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblCommonParameter.Location = new System.Drawing.Point(3, 0);
            this.lblCommonParameter.Name = "lblCommonParameter";
            this.lblCommonParameter.Size = new System.Drawing.Size(599, 40);
            this.lblCommonParameter.TabIndex = 6;
            this.lblCommonParameter.Text = "Common";
            this.lblCommonParameter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpCommonParameter
            // 
            this.tlpCommonParameter.ColumnCount = 1;
            this.tlpCommonParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCommonParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpCommonParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCommonParameter.Location = new System.Drawing.Point(0, 40);
            this.tlpCommonParameter.Margin = new System.Windows.Forms.Padding(0);
            this.tlpCommonParameter.Name = "tlpCommonParameter";
            this.tlpCommonParameter.RowCount = 3;
            this.tlpCommonParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpCommonParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpCommonParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpCommonParameter.Size = new System.Drawing.Size(605, 260);
            this.tlpCommonParameter.TabIndex = 7;
            // 
            // tlpVariableParameter
            // 
            this.tlpVariableParameter.ColumnCount = 1;
            this.tlpVariableParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpVariableParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVariableParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpVariableParameter.Location = new System.Drawing.Point(0, 40);
            this.tlpVariableParameter.Margin = new System.Windows.Forms.Padding(0);
            this.tlpVariableParameter.Name = "tlpVariableParameter";
            this.tlpVariableParameter.RowCount = 3;
            this.tlpVariableParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpVariableParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpVariableParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpVariableParameter.Size = new System.Drawing.Size(605, 260);
            this.tlpVariableParameter.TabIndex = 7;
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
            this.tlpMotionParameter.ResumeLayout(false);
            this.tlpVariableParameters.ResumeLayout(false);
            this.tlpCommonParameters.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMotionSettings;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tlpMotionFunction;
        private System.Windows.Forms.TableLayoutPanel tlpStatus;
        private System.Windows.Forms.Panel pnlJog;
        private System.Windows.Forms.TableLayoutPanel tlpMotionParameter;
        private System.Windows.Forms.Panel pnlTeachingPositionList;
        private System.Windows.Forms.TableLayoutPanel tlpCommonParameters;
        private System.Windows.Forms.TableLayoutPanel tlpVariableParameters;
        private System.Windows.Forms.Label lblVariableParameter;
        private System.Windows.Forms.Label lblCommonParameter;
        private System.Windows.Forms.TableLayoutPanel tlpCommonParameter;
        private System.Windows.Forms.TableLayoutPanel tlpVariableParameter;
    }
}
