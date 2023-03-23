namespace ATT
{
    partial class MainForm
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tlpMainForm = new System.Windows.Forms.TableLayoutPanel();
            this.pnlFunctionButtons = new System.Windows.Forms.Panel();
            this.tlpFunctionButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnConfigPage = new System.Windows.Forms.Button();
            this.btnAutoPage = new System.Windows.Forms.Button();
            this.btnTeachPage = new System.Windows.Forms.Button();
            this.btnRecipePage = new System.Windows.Forms.Button();
            this.btnLogPage = new System.Windows.Forms.Button();
            this.pnlMachineStatus = new System.Windows.Forms.Panel();
            this.tlpMachineStatus = new System.Windows.Forms.TableLayoutPanel();
            this.tlpDateTime = new System.Windows.Forms.TableLayoutPanel();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblSystemName = new System.Windows.Forms.Label();
            this.tlpModel = new System.Windows.Forms.TableLayoutPanel();
            this.lblModel = new System.Windows.Forms.Label();
            this.lblAppliedModel = new System.Windows.Forms.Label();
            this.lblMachineStatus = new System.Windows.Forms.Label();
            this.pnlPage = new System.Windows.Forms.Panel();
            this.tmrMainForm = new System.Windows.Forms.Timer(this.components);
            this.lblUser = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.btnModelPage = new System.Windows.Forms.Button();
            this.tlpMainForm.SuspendLayout();
            this.pnlFunctionButtons.SuspendLayout();
            this.tlpFunctionButtons.SuspendLayout();
            this.pnlMachineStatus.SuspendLayout();
            this.tlpMachineStatus.SuspendLayout();
            this.tlpDateTime.SuspendLayout();
            this.tlpModel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMainForm
            // 
            this.tlpMainForm.ColumnCount = 1;
            this.tlpMainForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainForm.Controls.Add(this.pnlFunctionButtons, 0, 2);
            this.tlpMainForm.Controls.Add(this.pnlMachineStatus, 0, 0);
            this.tlpMainForm.Controls.Add(this.pnlPage, 0, 1);
            this.tlpMainForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainForm.Location = new System.Drawing.Point(0, 0);
            this.tlpMainForm.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMainForm.Name = "tlpMainForm";
            this.tlpMainForm.RowCount = 3;
            this.tlpMainForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpMainForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpMainForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMainForm.Size = new System.Drawing.Size(1264, 921);
            this.tlpMainForm.TabIndex = 0;
            // 
            // pnlFunctionButtons
            // 
            this.pnlFunctionButtons.Controls.Add(this.tlpFunctionButtons);
            this.pnlFunctionButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFunctionButtons.Location = new System.Drawing.Point(0, 801);
            this.pnlFunctionButtons.Margin = new System.Windows.Forms.Padding(0);
            this.pnlFunctionButtons.Name = "pnlFunctionButtons";
            this.pnlFunctionButtons.Size = new System.Drawing.Size(1264, 120);
            this.pnlFunctionButtons.TabIndex = 0;
            // 
            // tlpFunctionButtons
            // 
            this.tlpFunctionButtons.ColumnCount = 9;
            this.tlpFunctionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpFunctionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpFunctionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpFunctionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpFunctionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpFunctionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpFunctionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpFunctionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpFunctionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFunctionButtons.Controls.Add(this.btnAutoPage, 0, 0);
            this.tlpFunctionButtons.Controls.Add(this.btnTeachPage, 1, 0);
            this.tlpFunctionButtons.Controls.Add(this.btnConfigPage, 5, 0);
            this.tlpFunctionButtons.Controls.Add(this.btnLogPage, 4, 0);
            this.tlpFunctionButtons.Controls.Add(this.btnRecipePage, 3, 0);
            this.tlpFunctionButtons.Controls.Add(this.btnModelPage, 2, 0);
            this.tlpFunctionButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFunctionButtons.Location = new System.Drawing.Point(0, 0);
            this.tlpFunctionButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tlpFunctionButtons.Name = "tlpFunctionButtons";
            this.tlpFunctionButtons.RowCount = 1;
            this.tlpFunctionButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFunctionButtons.Size = new System.Drawing.Size(1264, 120);
            this.tlpFunctionButtons.TabIndex = 0;
            // 
            // btnConfigPage
            // 
            this.btnConfigPage.BackColor = System.Drawing.Color.White;
            this.btnConfigPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConfigPage.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btnConfigPage.ForeColor = System.Drawing.Color.Black;
            this.btnConfigPage.Location = new System.Drawing.Point(803, 3);
            this.btnConfigPage.Name = "btnConfigPage";
            this.btnConfigPage.Size = new System.Drawing.Size(154, 114);
            this.btnConfigPage.TabIndex = 19;
            this.btnConfigPage.Text = "Config";
            this.btnConfigPage.UseVisualStyleBackColor = false;
            this.btnConfigPage.Click += new System.EventHandler(this.btnConfigPage_Click);
            // 
            // btnAutoPage
            // 
            this.btnAutoPage.BackColor = System.Drawing.Color.White;
            this.btnAutoPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAutoPage.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btnAutoPage.ForeColor = System.Drawing.Color.Black;
            this.btnAutoPage.Location = new System.Drawing.Point(3, 3);
            this.btnAutoPage.Name = "btnAutoPage";
            this.btnAutoPage.Size = new System.Drawing.Size(154, 114);
            this.btnAutoPage.TabIndex = 17;
            this.btnAutoPage.Text = "Auto";
            this.btnAutoPage.UseVisualStyleBackColor = false;
            this.btnAutoPage.Click += new System.EventHandler(this.btnAutoPage_Click);
            // 
            // btnTeachPage
            // 
            this.btnTeachPage.BackColor = System.Drawing.Color.White;
            this.btnTeachPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTeachPage.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btnTeachPage.ForeColor = System.Drawing.Color.Black;
            this.btnTeachPage.Location = new System.Drawing.Point(163, 3);
            this.btnTeachPage.Name = "btnTeachPage";
            this.btnTeachPage.Size = new System.Drawing.Size(154, 114);
            this.btnTeachPage.TabIndex = 18;
            this.btnTeachPage.Text = "Teach";
            this.btnTeachPage.UseVisualStyleBackColor = false;
            this.btnTeachPage.Click += new System.EventHandler(this.btnTeachPage_Click);
            // 
            // btnRecipePage
            // 
            this.btnRecipePage.BackColor = System.Drawing.Color.White;
            this.btnRecipePage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRecipePage.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btnRecipePage.ForeColor = System.Drawing.Color.Black;
            this.btnRecipePage.Location = new System.Drawing.Point(483, 3);
            this.btnRecipePage.Name = "btnRecipePage";
            this.btnRecipePage.Size = new System.Drawing.Size(154, 114);
            this.btnRecipePage.TabIndex = 17;
            this.btnRecipePage.Text = "Recipe";
            this.btnRecipePage.UseVisualStyleBackColor = false;
            this.btnRecipePage.Click += new System.EventHandler(this.btnRecipePage_Click);
            // 
            // btnLogPage
            // 
            this.btnLogPage.BackColor = System.Drawing.Color.White;
            this.btnLogPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLogPage.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btnLogPage.ForeColor = System.Drawing.Color.Black;
            this.btnLogPage.Location = new System.Drawing.Point(643, 3);
            this.btnLogPage.Name = "btnLogPage";
            this.btnLogPage.Size = new System.Drawing.Size(154, 114);
            this.btnLogPage.TabIndex = 20;
            this.btnLogPage.Text = "Log";
            this.btnLogPage.UseVisualStyleBackColor = false;
            this.btnLogPage.Click += new System.EventHandler(this.btnLogPage_Click);
            // 
            // pnlMachineStatus
            // 
            this.pnlMachineStatus.Controls.Add(this.tlpMachineStatus);
            this.pnlMachineStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMachineStatus.Location = new System.Drawing.Point(0, 0);
            this.pnlMachineStatus.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMachineStatus.Name = "pnlMachineStatus";
            this.pnlMachineStatus.Size = new System.Drawing.Size(1264, 120);
            this.pnlMachineStatus.TabIndex = 2;
            // 
            // tlpMachineStatus
            // 
            this.tlpMachineStatus.ColumnCount = 6;
            this.tlpMachineStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpMachineStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMachineStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpMachineStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpMachineStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpMachineStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpMachineStatus.Controls.Add(this.tlpDateTime, 3, 0);
            this.tlpMachineStatus.Controls.Add(this.picLogo, 0, 0);
            this.tlpMachineStatus.Controls.Add(this.lblSystemName, 1, 0);
            this.tlpMachineStatus.Controls.Add(this.tlpModel, 2, 0);
            this.tlpMachineStatus.Controls.Add(this.lblMachineStatus, 4, 0);
            this.tlpMachineStatus.Controls.Add(this.lblUser, 5, 0);
            this.tlpMachineStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMachineStatus.Location = new System.Drawing.Point(0, 0);
            this.tlpMachineStatus.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMachineStatus.Name = "tlpMachineStatus";
            this.tlpMachineStatus.RowCount = 1;
            this.tlpMachineStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMachineStatus.Size = new System.Drawing.Size(1264, 120);
            this.tlpMachineStatus.TabIndex = 1;
            // 
            // tlpDateTime
            // 
            this.tlpDateTime.ColumnCount = 1;
            this.tlpDateTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDateTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpDateTime.Controls.Add(this.lblDate, 0, 0);
            this.tlpDateTime.Controls.Add(this.lblTime, 0, 1);
            this.tlpDateTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDateTime.Location = new System.Drawing.Point(787, 3);
            this.tlpDateTime.Name = "tlpDateTime";
            this.tlpDateTime.RowCount = 2;
            this.tlpDateTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDateTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDateTime.Size = new System.Drawing.Size(154, 114);
            this.tlpDateTime.TabIndex = 3;
            // 
            // lblDate
            // 
            this.lblDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDate.Location = new System.Drawing.Point(3, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(148, 57);
            this.lblDate.TabIndex = 0;
            this.lblDate.Text = "Date";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTime
            // 
            this.lblTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTime.Location = new System.Drawing.Point(3, 57);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(148, 57);
            this.lblTime.TabIndex = 1;
            this.lblTime.Text = "Time";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSystemName
            // 
            this.lblSystemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSystemName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSystemName.Location = new System.Drawing.Point(203, 3);
            this.lblSystemName.Margin = new System.Windows.Forms.Padding(3);
            this.lblSystemName.Name = "lblSystemName";
            this.lblSystemName.Size = new System.Drawing.Size(418, 114);
            this.lblSystemName.TabIndex = 1;
            this.lblSystemName.Text = "Name";
            this.lblSystemName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpModel
            // 
            this.tlpModel.ColumnCount = 1;
            this.tlpModel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpModel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpModel.Controls.Add(this.lblModel, 0, 0);
            this.tlpModel.Controls.Add(this.lblAppliedModel, 0, 1);
            this.tlpModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpModel.Location = new System.Drawing.Point(627, 3);
            this.tlpModel.Name = "tlpModel";
            this.tlpModel.RowCount = 2;
            this.tlpModel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpModel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpModel.Size = new System.Drawing.Size(154, 114);
            this.tlpModel.TabIndex = 2;
            // 
            // lblModel
            // 
            this.lblModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblModel.Location = new System.Drawing.Point(3, 0);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(148, 57);
            this.lblModel.TabIndex = 0;
            this.lblModel.Text = "Model";
            this.lblModel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAppliedModel
            // 
            this.lblAppliedModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAppliedModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAppliedModel.Location = new System.Drawing.Point(3, 57);
            this.lblAppliedModel.Name = "lblAppliedModel";
            this.lblAppliedModel.Size = new System.Drawing.Size(148, 57);
            this.lblAppliedModel.TabIndex = 1;
            this.lblAppliedModel.Text = "Applied Model";
            this.lblAppliedModel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMachineStatus
            // 
            this.lblMachineStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMachineStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMachineStatus.Location = new System.Drawing.Point(947, 3);
            this.lblMachineStatus.Margin = new System.Windows.Forms.Padding(3);
            this.lblMachineStatus.Name = "lblMachineStatus";
            this.lblMachineStatus.Size = new System.Drawing.Size(154, 114);
            this.lblMachineStatus.TabIndex = 22;
            this.lblMachineStatus.Text = "Machine Status";
            this.lblMachineStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlPage
            // 
            this.pnlPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPage.Location = new System.Drawing.Point(0, 120);
            this.pnlPage.Margin = new System.Windows.Forms.Padding(0);
            this.pnlPage.Name = "pnlPage";
            this.pnlPage.Size = new System.Drawing.Size(1264, 681);
            this.pnlPage.TabIndex = 3;
            // 
            // tmrMainForm
            // 
            this.tmrMainForm.Tick += new System.EventHandler(this.tmrMainForm_Tick);
            // 
            // lblUser
            // 
            this.lblUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUser.Location = new System.Drawing.Point(1107, 3);
            this.lblUser.Margin = new System.Windows.Forms.Padding(3);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(154, 114);
            this.lblUser.TabIndex = 22;
            this.lblUser.Text = "User";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picLogo
            // 
            this.picLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picLogo.Location = new System.Drawing.Point(3, 3);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(194, 114);
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // btnModelPage
            // 
            this.btnModelPage.BackColor = System.Drawing.Color.White;
            this.btnModelPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnModelPage.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btnModelPage.ForeColor = System.Drawing.Color.Black;
            this.btnModelPage.Location = new System.Drawing.Point(323, 3);
            this.btnModelPage.Name = "btnModelPage";
            this.btnModelPage.Size = new System.Drawing.Size(154, 114);
            this.btnModelPage.TabIndex = 17;
            this.btnModelPage.Text = "Model";
            this.btnModelPage.UseVisualStyleBackColor = false;
            this.btnModelPage.Click += new System.EventHandler(this.btnModelPage_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1264, 921);
            this.Controls.Add(this.tlpMainForm);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tlpMainForm.ResumeLayout(false);
            this.pnlFunctionButtons.ResumeLayout(false);
            this.tlpFunctionButtons.ResumeLayout(false);
            this.pnlMachineStatus.ResumeLayout(false);
            this.tlpMachineStatus.ResumeLayout(false);
            this.tlpDateTime.ResumeLayout(false);
            this.tlpModel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMainForm;
        private System.Windows.Forms.Panel pnlFunctionButtons;
        private System.Windows.Forms.TableLayoutPanel tlpFunctionButtons;
        private System.Windows.Forms.Button btnLogPage;
        private System.Windows.Forms.Button btnConfigPage;
        private System.Windows.Forms.Button btnTeachPage;
        private System.Windows.Forms.Button btnRecipePage;
        private System.Windows.Forms.Panel pnlMachineStatus;
        private System.Windows.Forms.TableLayoutPanel tlpMachineStatus;
        private System.Windows.Forms.TableLayoutPanel tlpDateTime;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblSystemName;
        private System.Windows.Forms.TableLayoutPanel tlpModel;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.Label lblAppliedModel;
        private System.Windows.Forms.Label lblMachineStatus;
        private System.Windows.Forms.Button btnAutoPage;
        private System.Windows.Forms.Panel pnlPage;
        private System.Windows.Forms.Timer tmrMainForm;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Button btnModelPage;
    }
}

