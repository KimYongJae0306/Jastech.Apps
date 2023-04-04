namespace ATT.UI.Forms
{
    partial class MotionPopupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MotionPopupForm));
            this.tlpMotionPopup = new System.Windows.Forms.TableLayoutPanel();
            this.tlpFormFunction = new System.Windows.Forms.TableLayoutPanel();
            this.btnMoveTeachingPosition = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnStopTeachingPosition = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCommand = new System.Windows.Forms.Button();
            this.btnParameter = new System.Windows.Forms.Button();
            this.pnlFunction = new System.Windows.Forms.Panel();
            this.pnlMotionParameter = new System.Windows.Forms.Panel();
            this.pnlJog = new System.Windows.Forms.Panel();
            this.pnlTeachingPositionList = new System.Windows.Forms.Panel();
            this.tlpMotionPopup.SuspendLayout();
            this.tlpFormFunction.SuspendLayout();
            this.pnlFunction.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMotionPopup
            // 
            this.tlpMotionPopup.ColumnCount = 1;
            this.tlpMotionPopup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMotionPopup.Controls.Add(this.tlpFormFunction, 0, 2);
            this.tlpMotionPopup.Controls.Add(this.pnlFunction, 0, 1);
            this.tlpMotionPopup.Controls.Add(this.pnlTeachingPositionList, 0, 0);
            this.tlpMotionPopup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMotionPopup.Location = new System.Drawing.Point(0, 0);
            this.tlpMotionPopup.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMotionPopup.Name = "tlpMotionPopup";
            this.tlpMotionPopup.RowCount = 3;
            this.tlpMotionPopup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpMotionPopup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMotionPopup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpMotionPopup.Size = new System.Drawing.Size(684, 661);
            this.tlpMotionPopup.TabIndex = 2;
            // 
            // tlpFormFunction
            // 
            this.tlpFormFunction.ColumnCount = 8;
            this.tlpFormFunction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpFormFunction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpFormFunction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpFormFunction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpFormFunction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpFormFunction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpFormFunction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpFormFunction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpFormFunction.Controls.Add(this.btnMoveTeachingPosition, 3, 0);
            this.tlpFormFunction.Controls.Add(this.btnExit, 7, 0);
            this.tlpFormFunction.Controls.Add(this.btnStopTeachingPosition, 4, 0);
            this.tlpFormFunction.Controls.Add(this.btnSave, 6, 0);
            this.tlpFormFunction.Controls.Add(this.btnCommand, 0, 0);
            this.tlpFormFunction.Controls.Add(this.btnParameter, 1, 0);
            this.tlpFormFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFormFunction.Location = new System.Drawing.Point(0, 561);
            this.tlpFormFunction.Margin = new System.Windows.Forms.Padding(0);
            this.tlpFormFunction.Name = "tlpFormFunction";
            this.tlpFormFunction.RowCount = 1;
            this.tlpFormFunction.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFormFunction.Size = new System.Drawing.Size(684, 100);
            this.tlpFormFunction.TabIndex = 1;
            // 
            // btnMoveTeachingPosition
            // 
            this.btnMoveTeachingPosition.BackColor = System.Drawing.Color.White;
            this.btnMoveTeachingPosition.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMoveTeachingPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMoveTeachingPosition.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.btnMoveTeachingPosition.Location = new System.Drawing.Point(245, 3);
            this.btnMoveTeachingPosition.Name = "btnMoveTeachingPosition";
            this.btnMoveTeachingPosition.Size = new System.Drawing.Size(94, 94);
            this.btnMoveTeachingPosition.TabIndex = 199;
            this.btnMoveTeachingPosition.Text = "MOVE TARGET";
            this.btnMoveTeachingPosition.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMoveTeachingPosition.UseVisualStyleBackColor = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.White;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.btnExit.Location = new System.Drawing.Point(587, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(94, 94);
            this.btnExit.TabIndex = 293;
            this.btnExit.Text = "EXIT";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnStopTeachingPosition
            // 
            this.btnStopTeachingPosition.BackColor = System.Drawing.Color.White;
            this.btnStopTeachingPosition.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnStopTeachingPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStopTeachingPosition.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.btnStopTeachingPosition.Location = new System.Drawing.Point(345, 3);
            this.btnStopTeachingPosition.Name = "btnStopTeachingPosition";
            this.btnStopTeachingPosition.Size = new System.Drawing.Size(94, 94);
            this.btnStopTeachingPosition.TabIndex = 200;
            this.btnStopTeachingPosition.Text = "SET";
            this.btnStopTeachingPosition.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnStopTeachingPosition.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.White;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(487, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 94);
            this.btnSave.TabIndex = 292;
            this.btnSave.Text = "SAVE";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCommand
            // 
            this.btnCommand.BackColor = System.Drawing.Color.White;
            this.btnCommand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCommand.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.btnCommand.Location = new System.Drawing.Point(3, 3);
            this.btnCommand.Name = "btnCommand";
            this.btnCommand.Size = new System.Drawing.Size(94, 94);
            this.btnCommand.TabIndex = 292;
            this.btnCommand.Text = "PREV PAGE";
            this.btnCommand.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCommand.UseVisualStyleBackColor = false;
            this.btnCommand.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // btnParameter
            // 
            this.btnParameter.BackColor = System.Drawing.Color.White;
            this.btnParameter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnParameter.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.btnParameter.Location = new System.Drawing.Point(103, 3);
            this.btnParameter.Name = "btnParameter";
            this.btnParameter.Size = new System.Drawing.Size(94, 94);
            this.btnParameter.TabIndex = 292;
            this.btnParameter.Text = "NEXT PAGE";
            this.btnParameter.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnParameter.UseVisualStyleBackColor = false;
            this.btnParameter.Click += new System.EventHandler(this.btnParameter_Click);
            // 
            // pnlFunction
            // 
            this.pnlFunction.Controls.Add(this.pnlMotionParameter);
            this.pnlFunction.Controls.Add(this.pnlJog);
            this.pnlFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFunction.Location = new System.Drawing.Point(0, 100);
            this.pnlFunction.Margin = new System.Windows.Forms.Padding(0);
            this.pnlFunction.Name = "pnlFunction";
            this.pnlFunction.Size = new System.Drawing.Size(684, 461);
            this.pnlFunction.TabIndex = 2;
            // 
            // pnlMotionParameter
            // 
            this.pnlMotionParameter.Location = new System.Drawing.Point(395, 15);
            this.pnlMotionParameter.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMotionParameter.Name = "pnlMotionParameter";
            this.pnlMotionParameter.Size = new System.Drawing.Size(380, 380);
            this.pnlMotionParameter.TabIndex = 6;
            // 
            // pnlJog
            // 
            this.pnlJog.Location = new System.Drawing.Point(9, 15);
            this.pnlJog.Margin = new System.Windows.Forms.Padding(0);
            this.pnlJog.Name = "pnlJog";
            this.pnlJog.Size = new System.Drawing.Size(380, 380);
            this.pnlJog.TabIndex = 5;
            // 
            // pnlTeachingPositionList
            // 
            this.pnlTeachingPositionList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTeachingPositionList.Location = new System.Drawing.Point(0, 0);
            this.pnlTeachingPositionList.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTeachingPositionList.Name = "pnlTeachingPositionList";
            this.pnlTeachingPositionList.Size = new System.Drawing.Size(684, 100);
            this.pnlTeachingPositionList.TabIndex = 3;
            // 
            // MotionPopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(684, 661);
            this.ControlBox = false;
            this.Controls.Add(this.tlpMotionPopup);
            this.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "MotionPopupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = " ";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MotionPopupForm_FormClosed);
            this.Load += new System.EventHandler(this.MotionPopupForm_Load);
            this.tlpMotionPopup.ResumeLayout(false);
            this.tlpFormFunction.ResumeLayout(false);
            this.pnlFunction.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMotionPopup;
        private System.Windows.Forms.TableLayoutPanel tlpFormFunction;
        private System.Windows.Forms.Button btnMoveTeachingPosition;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnStopTeachingPosition;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCommand;
        private System.Windows.Forms.Button btnParameter;
        private System.Windows.Forms.Panel pnlFunction;
        private System.Windows.Forms.Panel pnlJog;
        private System.Windows.Forms.Panel pnlMotionParameter;
        private System.Windows.Forms.Panel pnlTeachingPositionList;
    }
}